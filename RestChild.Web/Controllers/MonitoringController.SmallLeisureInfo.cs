using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Http;
using System.Web.Mvc;
using RestChild.Booking.Logic.Extensions;
using RestChild.Comon.Enumeration;
using RestChild.DAL.RepositoryExtensions;
using RestChild.Domain;
using RestChild.Extensions.Filter;
using RestChild.Web.Extensions;
using RestChild.Web.Models;
using RestChild.Web.Models.Monitoring;
using RestChild.Web.Properties;

namespace RestChild.Web.Controllers
{
    public partial class MonitoringController
    {
        /// <summary>
        ///     Сведения о малых формах досуга
        /// </summary>
        [System.Web.Mvc.HttpGet]
        [System.Web.Mvc.Route("Monitoring/SmallLeisureInfo/{organisationId}/{yearOfRestId}/{month}")]
        public ActionResult SmallLeisureInfoEdit(long? organisationId = null, long? yearOfRestId = null,
            int? month = null)
        {
            if (!Security.HasAnyRightsForSomeOrganization(AccessRightEnum.Monitoring.SmallLeisureInfoData.View))
            {
                return RedirectToAvalibleAction();
            }

            var orgs = AccessRightEnum.Monitoring.SmallLeisureInfoData.View.GetSecurityOrganiztion();

            if (orgs == null || !orgs.Any())
            {
                orgs = UnitOfWork.GetSet<Organization>().Where(ss => !ss.IsDeleted && ss.IsInMonitoring)
                    .Select(ss => (long?) ss.Id).ToArray();
            }

            var yor = UnitOfWork.GetById<YearOfRest>(yearOfRestId);

            if (!organisationId.HasValue || !orgs.Contains(organisationId) || yor == null || !month.HasValue ||
                month.Value < 1 || month.Value > 12)
            {
                var curentYear = UnitOfWork.GetSet<YearOfRest>().Where(ss => ss.Year == DateTime.Now.Year).First();
                var currentMonth = DateTime.Now.Month;

                return RedirectToAction(nameof(SmallLeisureInfoEdit),
                    new {organisationId = orgs.FirstOrDefault(), yearOfRestId = curentYear.Id, month = currentMonth});
            }

            var org = UnitOfWork.GetById<Organization>(organisationId);

            //Организация не является объектом мониторинга
            if (!org.IsInMonitoring)
            {
                return RedirectToAvalibleAction();
            }

            var cnim = UnitOfWork.GetSet<MonitoringSmallLeisureInfo>()
                           .FirstOrDefault(ss =>
                               ss.OrganisationId == organisationId && ss.Month == month &&
                               ss.YearOfRestId == yearOfRestId) ??
                       new MonitoringSmallLeisureInfo
                       {
                           Month = month.Value,
                           YearOfRest = yor,
                           YearOfRestId = yor.Id,
                           Organisation = org,
                           OrganisationId = org.Id
                       };


            var res = new SmallLeisureInfoModel(cnim);

            SmallLeisureInfoSetState(res);
            SmallLeisureInfoSetBasis(res, orgs);

            if (res.Data.Id < 1)
            {
                var cnimLast = UnitOfWork.GetSet<MonitoringSmallLeisureInfo>()
                    .Where(ss => ss.OrganisationId == organisationId)
                    .ToDictionary(sx => new DateTime(sx.YearOfRest.Year, sx.Month, 1), sy => sy)
                    .Where(ss => ss.Key < new DateTime(yor.Year, month.Value, 1))
                    .OrderByDescending(sx => sx.Key).Select(ss => ss.Value).FirstOrDefault();

                if (cnimLast != null)
                {
                    foreach (var gbu in cnimLast.SmallLeisureInfoGBUs?.ToList() ??
                                        new List<MonitoringSmallLeisureInfoGBU>())
                    {
                        res.SmallLeisureInfoGBUs.Add(Guid.NewGuid().ToString(), new SmallLeisureInfoGBUModel(new MonitoringSmallLeisureInfoGBU {
                            GBUId = gbu.GBUId,
                            GBU = gbu.GBU
                        }));
                    }
                }
            }

            return View("SmallLeisureInfo/Edit", res);
        }

        /// <summary>
        ///     Формирование таблицы
        /// </summary>
        [System.Web.Mvc.Route("Monitoring/SmallLeisureInfoBuildTable/{monitoringSmallLeisureInfoGBUId}")]
        public ActionResult SmallLeisureInfoBuildTable(long? monitoringSmallLeisureInfoGBUId = null)
        {
            if (!Security.HasAnyRightsForSomeOrganization(AccessRightEnum.Monitoring.SmallLeisureInfoData.View))
            {
                return RedirectToAvalibleAction();
            }

            var gbuInfo = UnitOfWork.GetById<MonitoringSmallLeisureInfoGBU>(monitoringSmallLeisureInfoGBUId);
            if(gbuInfo == null)
            {
                return RedirectToAvalibleAction();
            }

            var gbuName = gbuInfo.GBU.ShortName;

            var vm = new SmallLeisureInfoTableViewModel
            {
                ShortName = gbuName,
                MonitoringSmallLeisureInfoDatas = gbuInfo.MonitoringSmallLeisureInfoDatas.ToList()
            };

            return View("SmallLeisureInfo/Table", vm);
        }

        /// <summary>
        ///     Добавить ГБУ
        /// </summary>
        /// <returns></returns>
        [System.Web.Mvc.HttpGet]
        [System.Web.Mvc.Route("Monitoring/SmallLeisureInfo/GBUAdd")]
        public ActionResult SmallLeisureInfoGbuAdd(long gbuId)
        {
            var model = new SmallLeisureInfoGBUModel();
            var gbu = UnitOfWork.GetById<MonitoringGBU>(gbuId);
            if (gbu == null)
            {
                throw new ArgumentNullException(nameof(gbuId));
            }

            model.Data.GBU = gbu;
            model.Data.GBUId = gbuId;
            ViewData.TemplateInfo.HtmlFieldPrefix = $"SmallLeisureInfoGBUs[{Guid.NewGuid()}]";
            return PartialView("EditorTemplates/SmallLeisureInfoGBU", model);
        }

        /// <summary>
        ///     Сохранение сведений о малых формах досуга
        /// </summary>
        [System.Web.Mvc.HttpPost]
        [ValidateAntiForgeryToken]
        [System.Web.Mvc.Route("Monitoring/SmallLeisureInfo/{organisationId}/{yearOfRestId}/{month}")]
        public ActionResult SmallLeisureInfoSave(long organisationId, long yearOfRestId, int month,
            [FromBody] SmallLeisureInfoModel model)
        {
            if (!Security.HasAnyRightsForSomeOrganization(AccessRightEnum.Monitoring.SmallLeisureInfoData.View) ||
                !Security.HasRight(AccessRightEnum.Monitoring.SmallLeisureInfoData.Edit))
            {
                return RedirectToAvalibleAction();
            }

            var organizationIds = AccessRightEnum.Monitoring.SmallLeisureInfoData.View.GetSecurityOrganiztion();

            if (organizationIds == null || !organizationIds.Any())
            {
                organizationIds = UnitOfWork.GetSet<Organization>().Where(ss => !ss.IsDeleted && ss.IsInMonitoring)
                    .Select(ss => (long?) ss.Id).ToArray();
            }

            if (organizationIds == null || !organizationIds.Any() || organisationId != model.Data.OrganisationId ||
                !organizationIds.Contains(organisationId) ||
                yearOfRestId != model.Data.YearOfRestId || !model.Months.ContainsKey(model.Data.Month))
            {
                return RedirectToAvalibleAction();
            }

            using (var tran = UnitOfWork.GetTransactionScope())
            {
                StateMachineAction action = null;

                if (!string.IsNullOrEmpty(model.StateMachineActionString))
                {
                    action = ApiStateController.GetAction(model.StateMachineActionString);
                }

                var org = UnitOfWork.GetById<Organization>(organisationId);
                var yar = UnitOfWork.GetById<YearOfRest>(yearOfRestId);


                if (model.Data.StateId == StateMachineStateEnum.Monitoring.SmallLeisureInfoData.Formation)
                {
                    if (model.Data.Id < 1)
                    {
                        var slid = model.BuildData();
                        var e = new MonitoringSmallLeisureInfo
                        {
                            Month = slid.Month,
                            YearOfRestId = slid.YearOfRestId,
                            OrganisationId = slid.OrganisationId,
                            StateId = StateMachineStateEnum.Monitoring.SmallLeisureInfoData.Formation
                        };

                        e.HistoryLink = ApiController.WriteHistory(e.HistoryLink,
                            $"Создание сведений о малых формах досуга \"{org.Name}\" за {model.Months[slid.Month]} {yar.Year} г.",
                            string.Empty);
                        e.HistoryLinkId = e.HistoryLink.Id;

                        UnitOfWork.AddEntity(e);

                        foreach (var s in model.SmallLeisureInfoGBUs.Values)
                        {
                            var sdb = new MonitoringSmallLeisureInfoGBU
                            {
                                MonitoringSmallLeisureInfoId = e.Id,
                                GBUId = s.Data.GBUId
                            };

                            UnitOfWork.AddEntity(sdb);
                        }

                        tran.Complete();
                        return RedirectToAction(nameof(SmallLeisureInfoEdit), new { organisationId, yearOfRestId, month });
                    }

                    var msli = UnitOfWork.GetById<MonitoringSmallLeisureInfo>(model.Data.Id);
                    if (msli.LastUpdateTick != model.Data.LastUpdateTick)
                    {
                        return RedirectToAvalibleAction();
                    }

                    var entity = model.BuildData();

                    var sbd = new StringBuilder();

                    var existSmallLeisureInfoGbu = msli.SmallLeisureInfoGBUs.Select(sx => sx.Id).ToList();
                    var smallLeisureInfoGbu =
                        model.SmallLeisureInfoGBUs?.Values.Select(ss => ss.Data.Id).Where(ss => ss > 0).ToList() ??
                        new List<long>();

                    foreach (var sligId in existSmallLeisureInfoGbu.Except(smallLeisureInfoGbu))
                    {
                        var slig = UnitOfWork.GetById<MonitoringSmallLeisureInfoGBU>(sligId);
                        sbd.AppendLine(
                            $"<li>Для ГБУ \"{slig.GBU?.ShortName}\" удалены все данные о малых формах отдыха</li>");

                        slig.MonitoringSmallLeisureInfoId = null;

                        UnitOfWork.SaveChanges();
                    }

                    foreach (var sligm in model.SmallLeisureInfoGBUs?.Values ?? new List<SmallLeisureInfoGBUModel>())
                    {
                        if ((sligm.Data?.Id ?? 0) > 0)
                        {
                            continue;
                        }

                        if ((sligm.Data?.GBUId ?? 0) < 1)
                        {
                            continue;
                        }

                        var sdb = new MonitoringSmallLeisureInfoGBU
                        {
                            MonitoringSmallLeisureInfoId = msli.Id,
                            GBUId = sligm.Data?.GBUId
                        };

                        UnitOfWork.AddEntity(sdb);

                        sdb = UnitOfWork.GetById<MonitoringSmallLeisureInfoGBU>(sdb.Id);

                        sbd.AppendLine($"<li>ГБУ \"{sdb.GBU.ShortName}\" добавлено</li>");
                    }

                    var filediff = msli.LinkToFiles.Diff(entity.LinkToFiles, UnitOfWork);
                    if (!string.IsNullOrWhiteSpace(filediff))
                    {
                        sbd.AppendLine($"<li>{filediff}</li>");
                        var link = msli.LinkToFiles.SaveFiles(entity.LinkToFiles, UnitOfWork);
                        msli.LinkToFilesId = link.Id;
                        entity.LinkToFilesId = msli.LinkToFilesId;
                    }

                    var diffText = sbd.ToString();
                    if (!string.IsNullOrWhiteSpace(diffText))
                    {
                        WriteToHistory(msli.HistoryLinkId,
                            $"Обновление сведений о малых формах досуга для \"{org.Name}\" за {model.Months[msli.Month]} {yar.Year} г.",
                            $"<ul>{diffText}</ul>", null, null, null);
                    }
                }

                if (action != null)
                {
                    var msli = UnitOfWork.GetById<MonitoringSmallLeisureInfo>(model.Data.Id);
                    var sb = new StringBuilder();

                    if (string.Equals(action.ActionCode, AccessRightEnum.Monitoring.SmallLeisureInfoData.OnApproving, StringComparison.OrdinalIgnoreCase))
                    {
                        if(msli.SmallLeisureInfoGBUs == null || !msli.SmallLeisureInfoGBUs.Any())
                        {
                            sb.Append("<li>Невозможно отправить на утверждение форму без введенных ГБУ</li>");
                        }
                        else if(msli.SmallLeisureInfoGBUs.Any(sx => !sx.LastUploadData.HasValue))
                        {
                            sb.Append("<li>Невозможно отправить на утверждение форму без привязанного к ГБУ файла</li>");
                        }

                    }

                    var err = sb.ToString();
                    if (!string.IsNullOrWhiteSpace(err))
                    {
                        var res = new SmallLeisureInfoModel(msli)
                        {
                            ErrorMessage = $"<ul>{err}</ul>",
                            IsValid = false
                        };

                        SmallLeisureInfoSetState(res);
                        SmallLeisureInfoSetBasis(res, organizationIds);

                        tran.Complete();
                        return View("SmallLeisureInfo/Edit", res);
                    }


                    WriteToHistory(msli.HistoryLinkId,
                        $"Изменение статуса сведений о малых формах досуга для \"{org.Name}\" за {model.Months[msli.Month]} {yar.Year} г.",
                        $"Изменение статуса с \"{msli.State?.Name}\" на \"{action.ToState?.Name}\"", action.ToStateId,
                        msli.StateId, null);

                    msli.StateId = action.ToStateId;
                }

                UnitOfWork.SaveChanges();
                tran.Complete();
            }


            return RedirectToAction(nameof(SmallLeisureInfoEdit), new {organisationId, yearOfRestId, month});
        }

        /// <summary>
        ///     Реестр форм о малых формах досуга
        /// </summary>
        [System.Web.Mvc.Route("Monitoring/SmallLeisureInfoList")]
        public ActionResult SmallLeisureInfoList(SmallLeisureInfoFilterModel filter)
        {
            if (!Security.HasRight(AccessRightEnum.Monitoring.ReestrWork))
            {
                return RedirectToAvalibleAction();
            }

            var pageSize = Settings.Default.TablePageSize;

            if (filter == null)
            {
                filter = new SmallLeisureInfoFilterModel();
            }

            var q = UnitOfWork.GetSet<MonitoringSmallLeisureInfo>()
                .Where(x => x.OrganisationId.HasValue);
            if (filter.OrganisationId.HasValue && filter.OrganisationId.Value > 0)
            {
                q = q.Where(aa => aa.OrganisationId == filter.OrganisationId);
            }

            if (filter.StateId.HasValue && filter.StateId.Value > 0)
            {
                q = q.Where(ss => ss.StateId == filter.StateId);
            }

            if (filter.YearOfRest.HasValue && filter.YearOfRest.Value > 0)
            {
                q = q.Where(ss => ss.YearOfRestId == filter.YearOfRest);
            }

            if (filter.Month.HasValue && filter.Month.Value > 0)
            {
                q = q.Where(ss => ss.Month == filter.Month);
            }

            var totalCount = q.Count();

            q = q.OrderBy(ss => ss.Id);

            if (pageSize > 0)
            {
                var pageNumber = filter.PageNumber;
                var startRecord = (pageNumber - 1) * pageSize;

                q = q.Skip(startRecord).Take(pageSize);
            }

            var result = q.ToList();

            filter.Result = new CommonPagedList<MonitoringSmallLeisureInfo>(result, filter.PageNumber,
                pageSize > 0 ? pageSize : Math.Max(10, totalCount), totalCount);

            filter.States = UnitOfWork.GetSet<StateMachineState>()
                .Where(ss => ss.StateMachineId == (long) StateMachineEnum.MonitoringSmallLeisureInfoData)
                .ToDictionary(ss => ss.Id, sx => sx.Name);
            filter.YearsOfRest = UnitOfWork.GetSet<YearOfRest>().ToDictionary(ss => ss.Id, sx => sx.Name);
            filter.Organisations = UnitOfWork.GetSet<Organization>().Where(ss => !ss.IsDeleted && ss.IsInMonitoring)
                .ToDictionary(ss => ss.Id, sx => sx.Name);

            return View("SmallLeisureInfo/List", filter);
        }

        /// <summary>
        ///     Установка статусной модели
        /// </summary>
        private void SmallLeisureInfoSetState(SmallLeisureInfoModel model)
        {
            if (model.Data.Id < 1)
            {
                var state = UnitOfWork.GetById<StateMachineState>(StateMachineStateEnum.Monitoring.SmallLeisureInfoData
                    .Formation);

                model.State = new ViewModelState
                {
                    Actions = new List<StateMachineAction>(),
                    NeedSaveButton = true,
                    CanReturn = false,
                    NeedRemoveButton = false,
                    State = state,
                    FormSelector = "#smallLeisureInfoForm",
                    ActionSelector = "#StateMachineActionString"
                };

                model.Data.StateId = StateMachineStateEnum.Monitoring.SmallLeisureInfoData.Formation;
            }
            else
            {
                var isEditable =
                    model.Data.StateId == StateMachineStateEnum.Monitoring.SmallLeisureInfoData.Formation &&
                    Security.HasRight(AccessRightEnum.Monitoring.SmallLeisureInfoData.Edit);

                var actions =
                    ApiStateController.GetActions(model.Data.State, StateMachineEnum.MonitoringSmallLeisureInfoData);

                model.State = new ViewModelState
                {
                    State = model.Data.State,
                    Actions = actions,
                    NeedSaveButton = isEditable,
                    NeedRemoveButton = false,
                    CanReturn = false,
                    FormSelector = "#smallLeisureInfoForm",
                    ActionSelector = "#StateMachineActionString"
                };

                model.State.PostNoStatusActions = model.State.PostNoStatusActions ?? new List<NoStatusAction>();

                if (model.Data?.HistoryLinkId.HasValue ?? false)
                {
                    model.State.PostNoStatusActions.Add(new NoStatusAction
                    {
                        Name = "История",
                        ButtonClass = "btn btn-default btn-hystory-link",
                        SomeAddon = $"data-history-id=\"{model.Data.HistoryLinkId}\""
                    });
                }

                model.State.PostNoStatusActions.Add(new NoStatusAction
                {
                    Name = "Выгрузить форму",
                    ButtonClass = "btn btn-default",
                    Controller = "Monitoring",
                    Action = "SmallLeisureInfoEditExcel",
                    ActionParameters = new
                    {
                        organisationId = model.Data.OrganisationId, yearOfRestId = model.Data.YearOfRestId,
                        month = model.Data.Month
                    }
                });

                model.State.PostNoStatusActions.Add(new NoStatusAction
                {
                    Name = "Выгрузить шаблоны для ГБУ",
                    ButtonClass = "btn btn-default",
                    Controller = "Monitoring",
                    Action = "SmallLeisureInfoEditZipExcel",
                    ActionParameters = new
                    {
                        organisationId = model.Data.OrganisationId, yearOfRestId = model.Data.YearOfRestId,
                        month = model.Data.Month
                    }
                });

                model.State.PostNoStatusActions.Add(new NoStatusAction
                {
                    Name = "Загрузить данные от ГБУ",
                    ButtonClass = "btn btn-info",
                    SomeAddon = "id='fileToUpload'"
                });
            }
        }

        /// <summary>
        ///     Установка коллекций
        /// </summary>
        private void SmallLeisureInfoSetBasis(SmallLeisureInfoModel model, params long?[] organizationIds)
        {
            model.YearsOfRest = UnitOfWork.GetSet<YearOfRest>().OrderBy(ss => ss.Year).ToList();
            model.Organisations = UnitOfWork.GetSet<Organization>()
                .Where(ss => organizationIds.Contains(ss.Id) && ss.IsInMonitoring && !ss.IsDeleted)
                .OrderBy(ss => ss.Name)
                .ToList();
            model.GBUs = UnitOfWork.GetSet<MonitoringGBU>().Where(ss => ss.OrganisationId == model.Data.OrganisationId)
                .OrderBy(ss => ss.ShortName).ToList();
        }

        /// <summary>
        ///     Отправить уведомление о необходимости заполнения формы
        /// </summary>
        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.Route("Monitoring/SmallLeisureInfoSendEvent")]
        public ActionResult SmallLeisureInfoSendEvent(DateTime? sendEventDate, string message)
        {
            if (!Security.HasRight(AccessRightEnum.Monitoring.EventSent))
            {
                return RedirectToAvalibleAction();
            }

            SendEmailSend(AccessRightEnum.Monitoring.SmallLeisureInfoData.EventRecive, sendEventDate, message,
                "Уведомление о необходимости заполнения формы сведений о малых формах досуга (занятости)");

            var mslis = UnitOfWork.GetSet<MonitoringSmallLeisureInfo>().Where(x => x.StateId == StateMachineStateEnum.Monitoring.SmallLeisureInfoData.Approved && x.YearOfRest.Year == DateTime.Now.Year).ToList();

            var sms = UnitOfWork.GetById<StateMachineState>(StateMachineStateEnum.Monitoring.SmallLeisureInfoData.Formation);

            foreach (var msli in mslis)
            {
                WriteToHistory(msli.HistoryLinkId.Value,
                            $"Изменение статуса сведений о малых формах досуга (занятости) для \"{msli.Organisation.Name}\" за {msli.YearOfRest.Year} г.",
                            $"Изменение статуса с \"{msli.State?.Name}\" на \"{sms?.Name}\" на основании рассылки", sms?.Id,
                            msli.StateId, null);

                msli.StateId = StateMachineStateEnum.Monitoring.SmallLeisureInfoData.Formation;
                UnitOfWork.SaveChanges();
            }

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
    }
}
