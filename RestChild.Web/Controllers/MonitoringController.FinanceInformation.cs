using RestChild.Booking.Logic.Extensions;
using RestChild.Comon.Enumeration;
using RestChild.DAL.RepositoryExtensions;
using RestChild.Domain;
using RestChild.Web.Extensions;
using RestChild.Web.Models;
using RestChild.Web.Models.Monitoring;
using RestChild.Web.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace RestChild.Web.Controllers
{
    public partial class MonitoringController
    {
        /// <summary>
        ///     Реестр форм о финансировании
        /// </summary>
        [Route("Monitoring/FinanceInformationList")]
        public ActionResult FinanceInformationList(FinancialInformationFilterModel filter)
        {
            if (!Security.HasRight(AccessRightEnum.Monitoring.ReestrWork))
            {
                return RedirectToAvalibleAction();
            }

            var pageSize = Settings.Default.TablePageSize;

            if (filter == null)
            {
                filter = new FinancialInformationFilterModel();
            }

            var q = UnitOfWork.GetSet<MonitoringFinancialInformation>()
                .Where(x=>x.OrganisationId.HasValue);
            if (filter.OrganisationId.HasValue && filter.OrganisationId.Value > 0)
            {
                q = q.Where(ss => ss.OrganisationId == filter.OrganisationId);
            }
            if (filter.StateId.HasValue && filter.StateId.Value > 0)
            {
                q = q.Where(ss => ss.StateId == filter.StateId);
            }
            if (filter.YearOfRest.HasValue && filter.YearOfRest.Value > 0)
            {
                q = q.Where(ss => ss.YearOfRestId == filter.YearOfRest);
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

            filter.Result = new RestChild.Extensions.Filter.CommonPagedList<MonitoringFinancialInformation>(result, filter.PageNumber, pageSize > 0 ? pageSize : Math.Max(10, totalCount), totalCount);

            filter.States = UnitOfWork.GetSet<StateMachineState>().Where(ss => ss.StateMachineId == (long)StateMachineEnum.MonitoringFinanceInformation).ToDictionary(ss => ss.Id, sx => sx.Name);
            filter.YearsOfRest = UnitOfWork.GetSet<YearOfRest>().ToDictionary(ss => ss.Id, sx => sx.Name);
            filter.Organisations = UnitOfWork.GetSet<Organization>().Where(ss => !ss.IsDeleted && ss.IsInMonitoring).ToDictionary(ss => ss.Id, sx => sx.Name);

            return View("FinancialInformation/List", filter);
        }

        /// <summary>
        ///     Сведения о финансировании
        /// </summary>
        [HttpGet]
        [Route("Monitoring/FinanceInformation/{organisationId}/{yearOfRestId}")]
        public ActionResult FinanceInformationEdit(long? organisationId = null, long? yearOfRestId = null)
        {
            if (!Security.HasAnyRightsForSomeOrganization(AccessRightEnum.Monitoring.FinanceInformation.View))
            {
                return RedirectToAvalibleAction();
            }

            var organizationIds = AccessRightEnum.Monitoring.FinanceInformation.View.GetSecurityOrganiztion();

            if (organizationIds == null || !organizationIds.Any())
            {
                organizationIds = UnitOfWork.GetSet<Organization>().Where(ss => !ss.IsDeleted && ss.IsInMonitoring)
                    .Select(ss => (long?)ss.Id).ToArray();
            }

            var yor = UnitOfWork.GetById<YearOfRest>(yearOfRestId);

            if (!organisationId.HasValue || !organizationIds.Contains(organisationId) || yor == null)
            {
                var currentYear = UnitOfWork.GetSet<YearOfRest>().First(ss => ss.Year == DateTime.Now.Year);

                return RedirectToAction(nameof(FinanceInformationEdit),
                    new { organisationId = organizationIds.FirstOrDefault(), yearOfRestId = currentYear.Id });
            }

            var org = UnitOfWork.GetById<Organization>(organisationId);

            //Организация не является объектом мониторинга
            if (!org.IsInMonitoring)
            {
                return RedirectToAvalibleAction();
            }

            var monitoringFinancialInformation =
                UnitOfWork.GetSet<MonitoringFinancialInformation>().FirstOrDefault(ss =>
                    ss.OrganisationId == organisationId && ss.YearOfRestId == yearOfRestId) ??
                new MonitoringFinancialInformation
                {
                    Organisation = org,
                    OrganisationId = org.Id,
                    YearOfRest = yor,
                    YearOfRestId = yor.Id,

                };


            var res = new FinancialInformationModel(monitoringFinancialInformation);

            FinancialInformationSetState(res);
            FinancialInformationSetBasis(res, organizationIds);

            return View("FinancialInformation/Edit", res);
        }


        /// <summary>
        ///     Сохранение формы сведений о финансировании
        /// </summary>
        [ValidateAntiForgeryToken]
        [HttpPost]
        [Route("Monitoring/FinanceInformation/{organisationId}/{yearOfRestId}")]
        public ActionResult FinanceInformationSave(long organisationId, long yearOfRestId, [System.Web.Http.FromBody] FinancialInformationModel model)
        {
            if (!Security.HasAnyRightsForSomeOrganization(AccessRightEnum.Monitoring.FinanceInformation.View) || !Security.HasRight(AccessRightEnum.Monitoring.FinanceInformation.Edit))
            {
                return RedirectToAvalibleAction();
            }

            var organizationIds = AccessRightEnum.Monitoring.FinanceInformation.View.GetSecurityOrganiztion();

            if (organizationIds == null || !organizationIds.Any())
            {
                organizationIds = UnitOfWork.GetSet<Organization>().Where(ss => !ss.IsDeleted && ss.IsInMonitoring).Select(ss => (long?)ss.Id).ToArray();
            }

            if (!organizationIds.Any() || !organizationIds.Contains(organisationId) || organisationId != model.Data.OrganisationId || yearOfRestId != model.Data.YearOfRestId)
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
                var sbd = new StringBuilder();

                if(model.Data.StateId == StateMachineStateEnum.Monitoring.FinanceInformation.Formation)
                {
                    if (model.Data.Id < 1)
                    {
                        var monitoringFinancialInformation = model.BuildData();
                        var e = new MonitoringFinancialInformation()
                        {
                            YearOfRestId = monitoringFinancialInformation.YearOfRestId,
                            OrganisationId = monitoringFinancialInformation.OrganisationId,
                            StateId = StateMachineStateEnum.Monitoring.FinanceInformation.Formation,
                        };

                        e.HistoryLink = ApiController.WriteHistory(e.HistoryLink,
                            $"Создание сведений о финансировании организации \"{org.Name}\" за {yar.Year} г.", string.Empty);
                        e.HistoryLinkId = e.HistoryLink.Id;

                        UnitOfWork.AddEntity(e);

                        foreach (var fd in model.FinancialData?.Values ?? new List<MonitoringFinancialData>())
                        {
                            if (fd != null)
                            {
                                var finData = new MonitoringFinancialData()
                                {
                                    FinanceInformationId = e.Id,
                                    MonitoringFinancialSourceId = fd.MonitoringFinancialSourceId,
                                    Plan = fd.Plan,
                                    Jun = fd.Jun,
                                    Jul = fd.Jul,
                                    Aug = fd.Aug,
                                    Sep = fd.Sep,
                                    Oct = fd.Oct,
                                    Nov = fd.Nov,
                                    Dec = fd.Dec,
                                    Jan = fd.Jan,
                                    Feb = fd.Feb,
                                    Mar = fd.Mar,
                                    Apr = fd.Apr,
                                    May = fd.May
                                };
                                UnitOfWork.AddEntity(finData);
                            }
                        }

                        UnitOfWork.SaveChanges();

                        tran.Complete();
                        return RedirectToAction(nameof(FinanceInformationEdit), new { organisationId, yearOfRestId });
                    }

                    var mfi = UnitOfWork.GetById<MonitoringFinancialInformation>(model.Data.Id);

                    var entity = model.BuildData();

                    if (mfi.LastUpdateTick != model.Data.LastUpdateTick)
                    {
                        return RedirectToAvalibleAction();
                    }

                    var curDataFinanceInformationId = model.Data.Id;
                    var curFinanceData = UnitOfWork.GetSet<MonitoringFinancialData>().Where(x => x.FinanceInformationId == curDataFinanceInformationId).OrderBy(a => a.MonitoringFinancialSourceId).ToList();

                    foreach (var newFinanceData in model.FinancialData?.Values.OrderBy(a => a.MonitoringFinancialSourceId).ToList() ?? new List<MonitoringFinancialData>())
                    {

                        var curFinance = curFinanceData.FirstOrDefault(x => x.Id == newFinanceData.Id);
                        var diff = GetFinanceDataDiff(curFinance, newFinanceData);
                        //только если данные изменились
                        if (!string.IsNullOrWhiteSpace(diff))
                        {
                            sbd.AppendLine($"<li>В сведениях о финансировании по организации \"{org.Name}\" за  \"{yar.Name}\" г. изменились данные:<ul>{diff}</ul></li>");

                            if (curFinance == null)
                            {
                                newFinanceData.FinanceInformationId = curDataFinanceInformationId;
                                UnitOfWork.AddEntity(newFinanceData);
                            }
                            else
                            {
                                curFinance.Plan = newFinanceData.Plan;
                                curFinance.Jun = newFinanceData.Jun;
                                curFinance.Jul = newFinanceData.Jul;
                                curFinance.Aug = newFinanceData.Aug;
                                curFinance.Sep = newFinanceData.Sep;
                                curFinance.Oct = newFinanceData.Oct;
                                curFinance.Nov = newFinanceData.Nov;
                                curFinance.Dec = newFinanceData.Dec;
                                curFinance.Jan = newFinanceData.Jan;
                                curFinance.Feb = newFinanceData.Feb;
                                curFinance.Mar = newFinanceData.Mar;
                                curFinance.Apr = newFinanceData.Apr;
                                curFinance.May = newFinanceData.May;
                            }

                            UnitOfWork.SaveChanges();
                        }
                    }

                    var filediff = mfi.LinkToFiles.Diff(entity.LinkToFiles, UnitOfWork);
                    if (!string.IsNullOrWhiteSpace(filediff))
                    {
                        sbd.AppendLine($"<li>{filediff}</li>");
                        var link = mfi.LinkToFiles.SaveFiles(entity.LinkToFiles, UnitOfWork);
                        mfi.LinkToFilesId = link.Id;
                        entity.LinkToFilesId = mfi.LinkToFilesId;
                    }

                    var diffText = sbd.ToString();
                    if (!string.IsNullOrWhiteSpace(diffText))
                    {
                        WriteToHistory(model.Data.HistoryLinkId,
                            $"Обновление сведений о финансировании для \"{org.Name}\" за {yar.Year} г.",
                            $"<ul>{diffText}</ul>", null, null, null);
                    }
                }

                // обновление статуса
                if (action != null)
                {
                    var mfi = UnitOfWork.GetById<MonitoringFinancialInformation>(model.Data.Id);

                    WriteToHistory(mfi.HistoryLinkId.Value,
                        $"Изменение статуса сведений о финансировании для \"{org.Name}\" за {yar.Year} г.",
                        $"Изменение статуса с \"{mfi.State?.Name}\" на \"{action.ToState?.Name}\"", action.ToStateId,
                        mfi.StateId, null);
                    mfi.StateId = action.ToStateId;

                    UnitOfWork.SaveChanges();
                    tran.Complete();
                    return RedirectToAction(nameof(FinanceInformationEdit), new { organisationId, yearOfRestId });
                    
                }

                UnitOfWork.SaveChanges();
                tran.Complete();
            }
            return RedirectToAction(nameof(FinanceInformationEdit), new { organisationId, yearOfRestId});
        }


        /// <summary>
        ///     Разница в финансовых данных
        /// </summary>
        private string GetFinanceDataDiff(MonitoringFinancialData currentData, MonitoringFinancialData newData)
        {
            var sb = new StringBuilder();
            var finSource = UnitOfWork.GetSet<MonitoringFinancialSource>().FirstOrDefault(x => x.Id == newData.MonitoringFinancialSourceId);

            var categoryName = finSource?.Name;
            var categoryTypeName = finSource?.Parrent?.Name;

            if (currentData?.Plan != newData.Plan)
            {
                sb.AppendLine($"<li>В разделе: '{categoryTypeName}', в категории: '{categoryName}' изменилось поле \"План\", старое значение:'{currentData?.Plan}', новое значение:'{newData.Plan}'</li>");
            }

            if (currentData?.Jun != newData.Jun)
            {
                sb.AppendLine($"<li>В разделе: '{categoryTypeName}', в категории: '{categoryName}' изменилось поле \"Июнь\", старое значение:'{currentData?.Jun}', новое значение:'{newData.Jun}'</li>");
            }

            if (currentData?.Jul != newData.Jul)
            {
                sb.AppendLine($"<li>В разделе: '{categoryTypeName}', в категории: '{categoryName}' изменилось поле \"Июль\", старое значение:'{currentData?.Jul}', новое значение:'{newData.Jul}'</li>");
            }

            if (currentData?.Aug != newData.Aug)
            {
                sb.AppendLine( $"<li>В разделе: '{categoryTypeName}', в категории: '{categoryName}' изменилось поле \"Август\", старое значение:'{currentData?.Aug}', новое значение:'{newData.Aug}'</li>");
            }

            if (currentData?.Sep != newData.Sep)
            {
                sb.AppendLine($"<li>В разделе: '{categoryTypeName}', в категории: '{categoryName}' изменилось поле \"Сентябрь\", старое значение:'{currentData?.Sep}', новое значение:'{newData.Sep}'</li>");
            }

            if (currentData?.Oct != newData.Oct)
            {
                sb.AppendLine($"<li>В разделе: '{categoryTypeName}', в категории: '{categoryName}' изменилось поле \"Октябрь\", старое значение:'{currentData?.Oct}', новое значение:'{newData.Oct}'</li>");
            }

            if (currentData?.Nov != newData.Nov)
            {
                sb.AppendLine($"<li>В разделе: '{categoryTypeName}', в категории: '{categoryName}' изменилось поле \"Ноябрь\", старое значение:'{currentData?.Nov}', новое значение:'{newData.Nov}'</li>");
            }

            if (currentData?.Dec != newData.Dec)
            {
                sb.AppendLine($"<li>В разделе: '{categoryTypeName}', в категории: '{categoryName}' изменилось поле \"Декабрь\", старое значение:'{currentData?.Dec}', новое значение:'{newData.Dec}'</li>");
            }

            if (currentData?.Jan != newData.Jan)
            {
                sb.AppendLine($"<li>В разделе: '{categoryTypeName}', в категории: '{categoryName}' изменилось поле \"Январь\", старое значение:'{currentData?.Jan}', новое значение:'{newData.Jan}'</li>");
            }

            if (currentData?.Feb != newData.Feb)
            {
                sb.AppendLine($"<li>В разделе: '{categoryTypeName}', в категории: '{categoryName}' изменилось поле \"Февраль\", старое значение:'{currentData?.Feb}', новое значение:'{newData.Feb}'</li>");
            }

            if (currentData?.Mar != newData.Mar)
            {
                sb.AppendLine($"<li>В разделе: '{categoryTypeName}', в категории: '{categoryName}' изменилось поле \"Март\", старое значение:'{currentData?.Mar}', новое значение:'{newData.Mar}'</li>");
            }

            if (currentData?.Apr != newData.Apr)
            {
                sb.AppendLine($"<li>В разделе: '{categoryTypeName}', в категории: '{categoryName}' изменилось поле \"Апрель\", старое значение:'{currentData?.Apr}', новое значение:'{newData.Apr}'</li>");
            }

            if (currentData?.May != newData.May)
            {
                sb.AppendLine($"<li>В разделе: '{categoryTypeName}', в категории: '{categoryName}' изменилось поле \"Май\", старое значение:'{currentData?.May}', новое значение:'{newData.May}'</li>");
            }

            return sb.ToString();
        }


        /// <summary>
        ///     Установка статусной модели
        /// </summary>
        private void FinancialInformationSetState(FinancialInformationModel model)
        {
            if (model.Data.Id < 1)
            {
                var state = UnitOfWork.GetById<StateMachineState>(StateMachineStateEnum.Monitoring
                    .FinanceInformation.Formation);

                model.State = new ViewModelState
                {
                    Actions = new List<StateMachineAction>(),
                    NeedSaveButton = true,
                    CanReturn = false,
                    NeedRemoveButton = false,
                    State = state,
                    FormSelector = "#financialInformationForm",
                    ActionSelector = "#StateMachineActionString"
                };

                model.Data.StateId = StateMachineStateEnum.Monitoring
                    .FinanceInformation.Formation;
            }
            else
            {
                var isEditable =
                    model.Data.StateId == StateMachineStateEnum.Monitoring.FinanceInformation.Formation &&
                    Security.HasRight(AccessRightEnum.Monitoring.FinanceInformation.Edit);

                var actions = ApiStateController.GetActions(model.Data.State,
                    StateMachineEnum.MonitoringFinanceInformation);

                model.State = new ViewModelState
                {
                    State = model.Data.State,
                    Actions = actions,
                    NeedSaveButton = isEditable,
                    NeedRemoveButton = false,
                    CanReturn = false,
                    FormSelector = "#financialInformationForm",
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
                    Name = "Выгрузить данные",
                    ButtonClass = "btn btn-default",
                    Controller = "Monitoring",
                    Action = "FinancialInformationExcel",
                    ActionParameters = new
                    { organisationId = model.Data.OrganisationId, yearOfRestId = model.Data.YearOfRestId }
                });
            }
        }

        /// <summary>
        ///     Установка коллекций
        /// </summary>
        private void FinancialInformationSetBasis(FinancialInformationModel model,
            params long?[] organizationIds)
        {
            model.YearsOfRest = UnitOfWork.GetSet<YearOfRest>().OrderBy(ss => ss.Year).ToList();
            model.Organisations = UnitOfWork.GetSet<Organization>()
                .Where(ss => organizationIds.Contains(ss.Id) && ss.IsInMonitoring && !ss.IsDeleted)
                .OrderBy(ss => ss.Name).ToList();

            var financialSources = UnitOfWork.GetSet<MonitoringFinancialSource>().Where(ss => ss.IsActive).ToList();

            foreach(var mfs in financialSources)
            {
                if(!(model.FinancialData?.Values.Any(ss => ss.MonitoringFinancialSourceId == mfs.Id)) ?? false)
                {
                    model.FinancialData.Add(Guid.NewGuid().ToString(), new MonitoringFinancialData
                    {
                        MonitoringFinancialSource = mfs,
                        MonitoringFinancialSourceId = mfs.Id
                    });
                }
            }
        }

        /// <summary>
        ///     Отправить уведомление о необходимости заполнения формы
        /// </summary>
        [HttpPost]
        [Route("Monitoring/FinancialInformationSendEvent")]
        public ActionResult FinancialInformationSendEvent(DateTime? sendEventDate, string message)
        {
            if (!Security.HasRight(AccessRightEnum.Monitoring.EventSent))
            {
                return RedirectToAvalibleAction();
            }

            SendEmailSend(AccessRightEnum.Monitoring.FinanceInformation.EventRecive, sendEventDate, message, "Уведомление о необходимости заполнения формы сведений о финансировании");

            var mfis = UnitOfWork.GetSet<MonitoringFinancialInformation>().Where(x => x.StateId == StateMachineStateEnum.Monitoring.FinanceInformation.Agreed && x.YearOfRest.Year == DateTime.Now.Year).ToList();

            var sms = UnitOfWork.GetById<StateMachineState>(StateMachineStateEnum.Monitoring.FinanceInformation.Formation);

            foreach (var mfi in mfis)
            {
                WriteToHistory(mfi.HistoryLinkId.Value,
                            $"Изменение статуса сведений о финансировании для \"{mfi.Organisation.Name}\" за {mfi.YearOfRest.Year} г.",
                            $"Изменение статуса с \"{mfi.State?.Name}\" на \"{sms?.Name}\" на основании рассылки", sms?.Id,
                            mfi.StateId, null);

                mfi.StateId = StateMachineStateEnum.Monitoring.FinanceInformation.Formation;
                UnitOfWork.SaveChanges();
            }

            return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);
        }
    }
}
