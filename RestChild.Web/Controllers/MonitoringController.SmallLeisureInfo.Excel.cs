using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Web.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using RestChild.Comon.Enumeration;
using RestChild.DAL.RepositoryExtensions;
using RestChild.Domain;

namespace RestChild.Web.Controllers
{
    /// <summary>
    ///     выгрузка в Excel
    /// </summary>
    public partial class MonitoringController
    {
        /// <summary>
        ///     Сведения о численности детей
        /// </summary>
        [HttpGet]
        [Route("Monitoring/SmallLeisureInfoEditExcel")]
        public ActionResult SmallLeisureInfoEditExcel(long? yearOfRestId = null, int? month = null,
            long? organisationId = null)
        {
            if (!Security.HasAnyRightsForSomeOrganization(AccessRightEnum.Monitoring.SmallLeisureInfoData.View)
                && !Security.HasRight(AccessRightEnum.Monitoring.CompleteFormDownload))
            {
                return RedirectToAvalibleAction();
            }

            var organizationIds = AccessRightEnum.Monitoring.SmallLeisureInfoData.View.GetSecurityOrganiztion();

            var yor = UnitOfWork.GetById<YearOfRest>(yearOfRestId);

            if (!Security.HasRight(AccessRightEnum.Monitoring.CompleteFormDownload) &&
                (organisationId == null || !organizationIds.Contains(organisationId)) || yor == null)
            {
                return RedirectToAvalibleAction();
            }

            var info = UnitOfWork.GetSet<MonitoringSmallLeisureInfoGBU>()
                .Where(m => m.MonitoringSmallLeisureInfo.YearOfRestId == yearOfRestId);

            if (organisationId.HasValue)
            {
                info = info.Where(m => m.GBU.OrganisationId == organisationId);
                info = info.Where(m => m.MonitoringSmallLeisureInfo.Month == month);
            }
            else
            {
                var ids = UnitOfWork.GetSet<MonitoringSmallLeisureInfoGBU>()
                    .Where(g => g.MonitoringSmallLeisureInfo.YearOfRestId == yearOfRestId)
                    .Where(g => g.MonitoringSmallLeisureInfo.StateId ==
                                StateMachineStateEnum.Monitoring.SmallLeisureInfoData.Approved)
                    .GroupBy(g => new {g.GBUId})
                    .Select(g =>
                        g.OrderByDescending(v => v.MonitoringSmallLeisureInfo.Month).Select(v => v.Id).FirstOrDefault())
                    .ToArray();

                info = info.Where(i => ids.Contains(i.Id));
            }


            var data = info.OrderBy(g => g.GBU.ShortName).ToList();

            string tempFile;

            using (var pkg = new ExcelPackage())
            {
                PrepareSmallInfoBook(pkg, data);

                tempFile = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
                pkg.SaveAs(new FileInfo(tempFile));
            }

            return FileAndDeleteOnClose(tempFile, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "Малые формы.xlsx");
        }

        /// <summary>
        ///     Сведения о численности детей
        /// </summary>
        [HttpGet]
        [Route("Monitoring/SmallLeisureInfoEditZipExcel")]
        public ActionResult SmallLeisureInfoEditZipExcel(long? yearOfRestId = null, int? month = null,
            long? organisationId = null)
        {
            if (!Security.HasAnyRightsForSomeOrganization(AccessRightEnum.Monitoring.SmallLeisureInfoData.View)
                && !Security.HasRight(AccessRightEnum.Monitoring.CompleteFormDownload))
            {
                return RedirectToAvalibleAction();
            }

            var organizationIds = AccessRightEnum.Monitoring.SmallLeisureInfoData.View.GetSecurityOrganiztion();

            var yor = UnitOfWork.GetById<YearOfRest>(yearOfRestId);

            if (!Security.HasRight(AccessRightEnum.Monitoring.CompleteFormDownload) &&
                (organisationId == null || !organizationIds.Contains(organisationId)) || yor == null)
            {
                return RedirectToAvalibleAction();
            }

            var info = UnitOfWork.GetSet<MonitoringSmallLeisureInfoGBU>()
                .Where(m => m.MonitoringSmallLeisureInfo.YearOfRestId == yearOfRestId);

            if (organisationId.HasValue)
            {
                info = info.Where(m =>
                    m.GBU.OrganisationId == organisationId &&
                    m.MonitoringSmallLeisureInfo.OrganisationId == organisationId);
                info = info.Where(m => m.MonitoringSmallLeisureInfo.Month == month);
            }
            else
            {
                var ids = UnitOfWork.GetSet<MonitoringSmallLeisureInfoGBU>()
                    .Where(g => g.MonitoringSmallLeisureInfo.YearOfRestId == yearOfRestId)
                    .Where(g => g.MonitoringSmallLeisureInfo.StateId ==
                                StateMachineStateEnum.Monitoring.SmallLeisureInfoData.Approved)
                    .GroupBy(g => new {g.GBUId})
                    .Select(g =>
                        g.OrderByDescending(v => v.MonitoringSmallLeisureInfo.Month).Select(v => v.Id).FirstOrDefault())
                    .ToArray();

                info = info.Where(i => ids.Contains(i.Id) && i.MonitoringSmallLeisureInfo.OrganisationId.HasValue);
            }

            var data = info.OrderBy(g => g.GBU.ShortName).ToList();
            var tempFile = GetTempFileName();
            var date = DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss");
            using (var zipToOpen = new FileStream(tempFile, FileMode.Create))
            {
                using (var archive = new ZipArchive(zipToOpen, ZipArchiveMode.Create))
                {
                    foreach (var item in data)
                    {
                        using (var pkg = new ExcelPackage())
                        {
                            PrepareSmallInfoBook(pkg, new List<MonitoringSmallLeisureInfoGBU> {item});
                            using (var ms = new MemoryStream())
                            {
                                pkg.SaveAs(ms);
                                ms.Seek(0, SeekOrigin.Begin);
                                var readmeEntry =
                                    archive.CreateEntry(GetValidFileName($"{item.GBU.ShortName}.{date}.xlsx"));
                                var buffer = new byte[10 * 1024 * 1024];
                                using (var stream = readmeEntry.Open())
                                {
                                    var readed = 1;
                                    while (readed > 0)
                                    {
                                        readed = ms.Read(buffer, 0, buffer.Length);
                                        if (readed > 0)
                                        {
                                            stream.Write(buffer, 0, readed);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return FileAndDeleteOnClose(tempFile, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "Малые формы.zip");
        }

        /// <summary>
        /// получить валидное имя файла
        /// </summary>
        private string GetValidFileName(string name)
        {
            foreach (char c in Path.GetInvalidFileNameChars())
            {
                name = name.Replace(c, '_');
            }

            return name;
        }

        /// <summary>
        ///     подготовить книгу для выгрузки
        /// </summary>
        private void PrepareSmallInfoBook(ExcelPackage pkg, List<MonitoringSmallLeisureInfoGBU> data)
        {
            var sheet = pkg.Workbook.Worksheets.Add("Малые формы");
            var gbuIndexer = 1;
            var types = UnitOfWork.GetSet<SmallLeisureType>().Where(t => t.IsActive).OrderBy(t => t.Id).ToList();

            var columns = 2 + types.Count(c => !c.SmallLeisureSubtypes.Any()) +
                          types.SelectMany(c => c.SmallLeisureSubtypes).Count();


            using (var excelRange = sheet.Cells[2, 1, 2, columns])
            {
                excelRange.Style.WrapText = true;
                excelRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                excelRange.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                excelRange.Style.Font.SetFromFont(new Font("Times New Roman", 11));
                excelRange.Style.Font.Bold = true;
                excelRange.Merge = true;
                excelRange.Value =
                    "Сведения о малых формах досуга (занятости) детей";
            }

            FillHeaderCellThin(sheet, 1, 4,
                "№ п/п", 2);
            FillHeaderCellThin(sheet, 2, 4,
                "Наименование показателей", 2);
            FillHeaderCellThin(sheet, 3, 4,
                "Перечень малых форм досуга (занятости) детей", 0, columns - 3);

            sheet.Column(1).Width = 5;
            sheet.Column(2).Width = 23;
            sheet.Column(columns).Width = 28;

            sheet.Row(6).Height = 120;

            var column = 3;
            foreach (var type in types)
            {
                FillHeaderCellThin(sheet, column, 5,
                    type.Name, type.SmallLeisureSubtypes.Any() ? 0 : 1,
                    !type.SmallLeisureSubtypes.Any() ? 0 : type.SmallLeisureSubtypes.Count - 1);

                foreach (var subtype in type.SmallLeisureSubtypes)
                {
                    FillHeaderCellThin(sheet, column, 6,
                        subtype.Name, 0,
                        0, 90);
                    column++;
                }

                if (!type.SmallLeisureSubtypes.Any())
                {
                    column++;
                }
            }

            var row = 7;

            foreach (var gbu in data)
            {
                FillHeaderCellThin(sheet, 1, row,
                    (gbuIndexer++).ToString());
                FillHeaderCellThin(sheet, 2, row,
                    gbu.GBU?.ShortName ?? gbu.GBU?.FullName ?? "-", 0, columns - 2, 0, $"GBU_{gbu.Id}_{gbu.GBUId}");

                row++;

                FillCellSmallForm(sheet, 1, row, 1,
                    "Количество проведенных малых формах досуга (занятости) детей*");
                FillCellSmallForm(sheet, 1, row + 1, 1,
                    "Численность детей, охваченных малыми формами досуга (занятости)**");
                FillCellSmallForm(sheet, 1, row + 2, 1,
                    "Объем финансовых средств, затраченных на организацию малых форм досуга (занятости) (тыс.руб.)");

                column = 3;
                foreach (var type in types)
                {
                    foreach (var subtype in type.SmallLeisureSubtypes)
                    {
                        var item = gbu.MonitoringSmallLeisureInfoDatas.FirstOrDefault(f =>
                            f.SmallLeisureSubtypeId == subtype.Id) ?? new MonitoringSmallLeisureInfoData();

                        FillCellSmallForm(sheet, column, row, 0,
                            item.ChildrenCountPost ?? 0,
                            $"ChildrenCountPost_{type.Id}_{subtype.Id}_{gbu.Id}_{gbu.GBUId}");
                        FillCellSmallForm(sheet, column, row + 1, 0,
                            item.ChildernCountCovered ?? 0,
                            $"ChildernCountCovered_{type.Id}_{subtype.Id}_{gbu.Id}_{gbu.GBUId}");
                        FillCellSmallForm(sheet, column, row + 2, 0,
                            item.MoneyOutcome ?? 0, $"MoneyOutcome_{type.Id}_{subtype.Id}_{gbu.Id}_{gbu.GBUId}");

                        column++;
                    }

                    if (!type.SmallLeisureSubtypes.Any())
                    {
                        var item = gbu.MonitoringSmallLeisureInfoDatas.FirstOrDefault(f =>
                            f.SmallLeisureTypeId == type.Id) ?? new MonitoringSmallLeisureInfoData();

                        if (type.IsTextData)
                        {
                            FillCellSmallForm(sheet, column, row, 0,
                                item.NoteOne ?? string.Empty, $"NoteOne_{type.Id}__{gbu.Id}_{gbu.GBUId}");
                            FillCellSmallForm(sheet, column, row + 1, 0,
                                item.NoteTwo ?? string.Empty, $"NoteTwo_{type.Id}__{gbu.Id}_{gbu.GBUId}");
                            FillCellSmallForm(sheet, column, row + 2, 0,
                                item.NoteThree ?? string.Empty, $"NoteThree_{type.Id}__{gbu.Id}_{gbu.GBUId}");
                        }
                        else if (!string.IsNullOrWhiteSpace(type.Formula))
                        {
                            FillCellSmallForm(sheet, column, row, 0,
                                item.ChildrenCountPost ?? 0, $"ChildrenCountPost_{type.Id}__{gbu.Id}_{gbu.GBUId}",
                                string.Format(type.Formula, row));
                            FillCellSmallForm(sheet, column, row + 1, 0,
                                item.ChildernCountCovered ?? 0, $"ChildernCountCovered_{type.Id}__{gbu.Id}_{gbu.GBUId}",
                                string.Format(type.Formula, row + 1));
                            FillCellSmallForm(sheet, column, row + 2, 0,
                                item.MoneyOutcome ?? 0, $"MoneyOutcome_{type.Id}__{gbu.Id}_{gbu.GBUId}",
                                string.Format(type.Formula, row + 2));
                        }
                        else
                        {
                            FillCellSmallForm(sheet, column, row, 0,
                                item.ChildrenCountPost ?? 0, $"ChildrenCountPost_{type.Id}__{gbu.Id}_{gbu.GBUId}");
                            FillCellSmallForm(sheet, column, row + 1, 0,
                                item.ChildernCountCovered ?? 0,
                                $"ChildernCountCovered_{type.Id}__{gbu.Id}_{gbu.GBUId}");
                            FillCellSmallForm(sheet, column, row + 2, 0,
                                item.MoneyOutcome ?? 0, $"MoneyOutcome_{type.Id}__{gbu.Id}_{gbu.GBUId}");
                        }

                        column++;
                    }
                }

                sheet.Row(row).Height = 30;
                sheet.Row(row + 1).Height = 40;
                sheet.Row(row + 2).Height = 40;
                row = row + 3;
            }
        }

        /// <summary>
        ///     заполнить ячейку
        /// </summary>
        private static void FillCellSmallForm(ExcelWorksheet sheet, int column, int row, int columns, object index,
            string nameCell = null,
            string formula = null, ExcelHorizontalAlignment align = ExcelHorizontalAlignment.Center)
        {
            using (var excelRange = sheet.Cells[row, column, row, column + columns])
            {
                if (string.IsNullOrWhiteSpace(formula))
                {
                    excelRange.Value = index;
                }
                else
                {
                    excelRange.Formula = formula;
                }

                excelRange.Style.Border.BorderAround(ExcelBorderStyle.Thin);
                excelRange.Style.Font.SetFromFont(new Font("Times New Roman", 9));
                excelRange.Style.WrapText = true;
                excelRange.Style.HorizontalAlignment = align;
                excelRange.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                if (columns > 0)
                {
                    excelRange.Merge = true;
                }

                if (!string.IsNullOrWhiteSpace(nameCell))
                {
                    sheet.Names.Add(nameCell, excelRange);
                }
            }
        }

        /// <summary>
        ///     заполнить ячейку заголовка
        /// </summary>
        private static void FillHeaderCellThin(ExcelWorksheet sheet, int column, int row, string v, int rows = 0,
            int cols = 0, int rotate = 0, string nameCell = null)
        {
            using (var excelRange = sheet.Cells[row, column, row + rows, column + cols])
            {
                excelRange.Value = v;
                excelRange.Style.Border.BorderAround(ExcelBorderStyle.Thin);
                excelRange.Style.Font.SetFromFont(new Font("Times New Roman", 9));
                excelRange.Style.Font.Bold = true;
                excelRange.Style.WrapText = true;
                excelRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                excelRange.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                excelRange.Style.TextRotation = rotate;

                if (rows > 0 || cols > 0)
                {
                    excelRange.Merge = true;
                }

                if (!string.IsNullOrWhiteSpace(nameCell))
                {
                    sheet.Names.Add(nameCell, excelRange);
                }
            }
        }
    }
}
