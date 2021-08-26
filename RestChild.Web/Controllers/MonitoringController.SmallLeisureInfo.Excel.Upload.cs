using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using OfficeOpenXml;
using RestChild.Booking.Logic.Extensions;
using RestChild.Comon;
using RestChild.Comon.Enumeration;
using RestChild.Domain;
using RestChild.Web.Models.Monitoring;

namespace RestChild.Web.Controllers
{
    /// <summary>
    ///     загрузка из Excel
    /// </summary>
    public partial class MonitoringController
    {
        /// <summary>
        ///     Сведения о численности детей
        /// </summary>
        [HttpPost]
        [Route("Monitoring/SaveSmallLeisureInfoFiles")]
        public ActionResult SaveSmallLeisureInfoFiles(long yearOfRestId, int month, long organisationId)
        {
            if (!Security.HasAnyRightsForSomeOrganization(AccessRightEnum.Monitoring.SmallLeisureInfoData.View)
                || !Security.HasRight(AccessRightEnum.Monitoring.SmallLeisureInfoData.Edit))
            {
                return RedirectToAvalibleAction();
            }

            var result = SaveFile();

            var organizationIds = AccessRightEnum.Monitoring.ChildrenNumberInformation.View.GetSecurityOrganiztion();

            if (organizationIds != null && organizationIds.Any() && !organizationIds.Contains(organisationId))
            {
                foreach (var file in result.Where(r => r.Success))
                {
                    file.Success = false;
                    file.ResultLoad = "Нет прав для загрузки данных";
                }

                return Content(JsonConvert.SerializeObject(result), "application/json");
            }

            var info = UnitOfWork.GetSet<MonitoringSmallLeisureInfo>().FirstOrDefault(m =>
                m.YearOfRestId == yearOfRestId && m.Month == month &&
                m.OrganisationId == organisationId);

            var fullDiff = new StringBuilder();

            if (info == null)
            {
                foreach (var file in result.Where(r => r.Success))
                {
                    file.Success = false;
                    file.ResultLoad = "Мониторинг не сохранен";
                }

                return Content(JsonConvert.SerializeObject(result), "application/json");
            }

            var types = UnitOfWork.GetSet<SmallLeisureType>().ToList();
            var subTypes = UnitOfWork.GetSet<SmallLeisureSubtype>().ToList();

            foreach (var file in result.Where(f => f.Success).ToList())
            {
                var gbuIds = file.Data.Keys.ToArray();
                var monitoringGbus = UnitOfWork.GetSet<MonitoringGBU>().Where(c => gbuIds.Contains(c.Id)).ToList();

                if (monitoringGbus.Any(g => g.OrganisationId != organisationId))
                {
                    file.Success = false;
                    file.ResultLoad = "ГБУ в файле не соответствуют выбранному участнику мониторинга";
                    continue;
                }

                foreach (var key in file.Data.Keys)
                {
                    var gbuId = key;
                    var data = file.Data[gbuId];
                    if (!data.Any())
                    {
                        continue;
                    }

                    var gbu = UnitOfWork
                                  .GetSet<MonitoringSmallLeisureInfoGBU>()
                                  .FirstOrDefault(g => g.GBUId == key && g.MonitoringSmallLeisureInfoId == info.Id) ??
                              UnitOfWork.AddEntity(new MonitoringSmallLeisureInfoGBU
                              {
                                  MonitoringSmallLeisureInfoId = info.Id,
                                  GBUId = gbuId,
                                  LastUploadData = DateTime.Now,
                                  MonitoringSmallLeisureInfoDatas = new List<MonitoringSmallLeisureInfoData>()
                              });

                    gbu.LastUploadData = DateTime.Now;

                    var diff = new StringBuilder();

                    foreach (var target in data)
                    {
                        var source = gbu.MonitoringSmallLeisureInfoDatas.FirstOrDefault(f =>
                            target.SmallLeisureTypeId == f.SmallLeisureTypeId &&
                            target.SmallLeisureSubtypeId == f.SmallLeisureSubtypeId);

                        var indexName =
                            $"{types.FirstOrDefault(t => t.Id == target.SmallLeisureTypeId)?.Name} {subTypes.FirstOrDefault(t => t.Id == target.SmallLeisureSubtypeId)?.Name}"
                                .Trim();
                        if (source == null)
                        {
                            target.MonitoringSmallLeisureInfoGBUId = gbu.Id;
                            UnitOfWork.AddEntity(target, false);
                            diff.AppendLine(
                                indexName != "Примечание"
                                    ? $"<li>Добавлен показатель '{indexName}', количество {target.ChildrenCountPost}, численность {target.ChildrenCountPost}, объем {target.MoneyOutcome.FormatEx()}</li>"
                                    : $"<li>Добавлено '{indexName}', количество '{target.NoteOne}', численность '{target.NoteTwo}', объем '{target.NoteThree}'</li>");
                        }
                        else
                        {
                            var subDiff = new StringBuilder();
                            subDiff.Compare(source, target, "Изменено количество", v => v.ChildrenCountPost);
                            subDiff.Compare(source, target, "Изменено численность", v => v.ChildernCountCovered);
                            subDiff.Compare(source, target, "Изменено сумма", v => v.MoneyOutcome,
                                v => v.MoneyOutcome.FormatEx());
                            subDiff.Compare(source, target, "Изменено примечание количества", v => v.NoteOne);
                            subDiff.Compare(source, target, "Изменено примечание численность", v => v.NoteTwo);
                            subDiff.Compare(source, target, "Изменено примечание суммы", v => v.NoteThree);
                            var subDiffTxt = subDiff.ToString();
                            if (!string.IsNullOrWhiteSpace(subDiffTxt))
                            {
                                diff.AppendLine($"<li>Изменен показатель {indexName}<ul>{subDiffTxt}</ul></li>");
                                source.ChildernCountCovered = target.ChildernCountCovered;
                                source.ChildrenCountPost = target.ChildrenCountPost;
                                source.MoneyOutcome = target.MoneyOutcome;
                                source.NoteOne = target.NoteOne;
                                source.NoteTwo = target.NoteTwo;
                                source.NoteThree = target.NoteThree;
                            }
                        }
                    }

                    if (diff.Length > 0)
                    {
                        fullDiff.AppendLine(
                            $"<li>Изменены сведения по {gbu.GBU.ShortName ?? gbu.GBU.FullName}:<ul>{diff}<ul></li>");
                    }

                    file.Gbu.Add(gbu.GBU.ShortName ?? gbu.GBU.FullName ?? "Имя ГБУ не указано");
                }

                UnitOfWork.SaveChanges();

                if (!file.Gbu.Any())
                {
                    file.Success = false;
                    file.ResultLoad = "Не найдены сведения о ГБУ в файле";
                }
                else
                {
                    file.ResultLoad = "Файл успешно загружен";
                }
            }

            var link = UnitOfWork.WriteHistory(info.HistoryLink, "Загрузка данных из файлов", $"<ul>{fullDiff}<ul>",
                Security.GetCurrentAccountId());

            if (!info.HistoryLinkId.HasValue)
            {
                info.HistoryLinkId = link?.Id;
            }

            UnitOfWork.SaveChanges();

            return Content(JsonConvert.SerializeObject(result), "application/json");
        }

        /// <summary>
        ///     сохранение файлов
        /// </summary>
        private IList<ExcelLoadResult> SaveFile()
        {
            var result = new List<ExcelLoadResult>();

            for (var key =0; key < Request.Files.Count; key++)
            {
                var file = Request.Files[key];

                if (file == null)
                {
                    continue;
                }

                var inputStream = file.InputStream;
                var fileData = new ExcelLoadResult
                {
                    FileName = file.FileName,
                    Success = true
                };

                try
                {
                    using (var pkg = new ExcelPackage(inputStream))
                    {
                        foreach (var sheet in pkg.Workbook.Worksheets)
                        {
                            foreach (var range in sheet.Names)
                            {
                                fileData.AppendData(range.Name, range.Value ?? string.Empty);
                            }
                        }
                    }
                }
                catch
                {
                    fileData.Success = false;
                    fileData.ResultLoad = "Ошибка загрузки файла. Файл не формата XLSX";
                }

                fileData.ClearEmpty();

                result.Add(fileData);
            }

            return result;
        }
    }
}
