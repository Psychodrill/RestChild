using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Http;
using System.Web.Mvc;
using RestChild.Booking.Logic.Extensions;
using RestChild.Comon;
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
        ///     Сведения о численности детей
        /// </summary>
        [System.Web.Mvc.HttpGet]
        [System.Web.Mvc.Route("Monitoring/ChildrenNumberInformation/{organisationId}/{yearOfRestId}")]
        public ActionResult ChildrenNumberInformationEdit(long? organisationId = null, long? yearOfRestId = null)
        {
            if (!Security.HasAnyRightsForSomeOrganization(AccessRightEnum.Monitoring.ChildrenNumberInformation.View))
            {
                return RedirectToAvalibleAction();
            }

            var organizationIds = AccessRightEnum.Monitoring.ChildrenNumberInformation.View.GetSecurityOrganiztion();

            if (organizationIds == null || !organizationIds.Any())
            {
                organizationIds = UnitOfWork.GetSet<Organization>().Where(ss => !ss.IsDeleted && ss.IsInMonitoring)
                    .Select(ss => (long?) ss.Id).ToArray();
            }

            var yor = UnitOfWork.GetById<YearOfRest>(yearOfRestId);

            if (!organisationId.HasValue || !organizationIds.Contains(organisationId) || yor == null)
            {
                var currentYear = UnitOfWork.GetSet<YearOfRest>().First(ss => ss.Year == DateTime.Now.Year);

                return RedirectToAction(nameof(ChildrenNumberInformationEdit),
                    new {organisationId = organizationIds.FirstOrDefault(), yearOfRestId = currentYear.Id});
            }

            var org = UnitOfWork.GetById<Organization>(organisationId);

            //Организация не является объектом мониторинга
            if (!org.IsInMonitoring)
            {
                return RedirectToAvalibleAction();
            }

            var monitoringChildrenNumberInformation =
                UnitOfWork.GetSet<MonitoringChildrenNumberInformation>().FirstOrDefault(ss =>
                    ss.OrganisationId == organisationId && ss.YearOfRestId == yearOfRestId) ??
                new MonitoringChildrenNumberInformation
                {
                    Organisation = org,
                    OrganisationId = org.Id,
                    YearOfRest = yor,
                    YearOfRestId = yor.Id
                };

            var res = new ChildrenNumberInformationModel(monitoringChildrenNumberInformation);

            ChildrenNumberInformationSetState(res);
            ChildrenNumberInformationSetBasis(res, organizationIds);

            return View("ChildrenNumberInformation/Edit", res);
        }

        /// <summary>
        ///     Добавить организацию отдыха и оздоровления
        /// </summary>
        /// <returns></returns>
        [System.Web.Mvc.HttpGet]
        [System.Web.Mvc.Route("Monitoring/ChildrenNumberInformation/HotelAdd")]
        public ActionResult ChildrenNumberInformationHotelAdd()
        {
            var model = new HotelDataModel();
            var prefix = Guid.NewGuid().ToString();
            model.Prefix = prefix;
            model.Tours.Add(prefix, new TourDataModel());
            ViewData.TemplateInfo.HtmlFieldPrefix = $"HotelDatas[{prefix}]";
            return PartialView("EditorTemplates/ChildrenNumberInformationHotel", model);
        }

        /// <summary>
        ///     Добавить организацию отдыха и оздоровления
        /// </summary>
        /// <returns></returns>
        [System.Web.Mvc.HttpGet]
        [System.Web.Mvc.Route("Monitoring/ChildrenNumberInformation/TourAdd")]
        public ActionResult ChildrenNumberInformationTourAdd(string prefix)
        {
            var model = new TourDataModel();
            ViewData.TemplateInfo.HtmlFieldPrefix = $"HotelDatas[{prefix}].Tours[{Guid.NewGuid()}]";
            return PartialView("EditorTemplates/ChildrenNumberInformationTour", model);
        }

        /// <summary>
        ///     Сохранение сведений о численности детей
        /// </summary>
        [System.Web.Mvc.HttpPost]
        [ValidateAntiForgeryToken]
        [System.Web.Mvc.Route("Monitoring/ChildrenNumberInformation/{organisationId}/{yearOfRestId}")]
        public ActionResult ChildrenNumberInformationSave(long organisationId, long yearOfRestId,
            [FromBody] ChildrenNumberInformationModel model)
        {
            if (!Security.HasAnyRightsForSomeOrganization(AccessRightEnum.Monitoring.ChildrenNumberInformation.View) ||
                !Security.HasRight(AccessRightEnum.Monitoring.ChildrenNumberInformation.Edit))
            {
                return RedirectToAvalibleAction();
            }

            var organizationIds = AccessRightEnum.Monitoring.ChildrenNumberInformation.View.GetSecurityOrganiztion();

            if(organizationIds == null || !organizationIds.Any())
            {
                organizationIds = UnitOfWork.GetSet<Organization>().Where(ss => !ss.IsDeleted && ss.IsInMonitoring).Select(ss => (long?)ss.Id).ToArray();
            }

            if (organizationIds == null || !organizationIds.Any() || organisationId != model.Data.OrganisationId ||
                !organizationIds.Contains(organisationId) || yearOfRestId != model.Data.YearOfRestId)
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

                if(model.Data.StateId == StateMachineStateEnum.Monitoring.ChildrenNumberInformation.Formation)
                {
                    //создаём новый
                    if (model.Data.Id < 1)
                    {
                        var monitoringChildrenNumberInformation = model.BuildData();
                        var e = new MonitoringChildrenNumberInformation
                        {
                            OrganisationId = monitoringChildrenNumberInformation.OrganisationId,
                            YearOfRestId = monitoringChildrenNumberInformation.YearOfRestId,
                            StateId = StateMachineStateEnum.Monitoring.ChildrenNumberInformation.Formation
                        };

                        e.HistoryLink = ApiController.WriteHistory(e.HistoryLink,
                            $"Создание сведений о численности детей для \"{org.Name}\" за {yar.Year} г.", string.Empty);
                        e.HistoryLinkId = e.HistoryLink.Id;

                        UnitOfWork.AddEntity(e);

                        foreach (var h in model.HotelDatas?.Values ?? new List<HotelDataModel>())
                        {
                            if ((h.Data?.HotelId ?? 0) < 1)
                            {
                                continue;
                            }

                            var hotel = new MonitoringHotelData
                            {
                                HotelId = h.Data?.HotelId,
                                ChildrenNumberInformationId = e.Id
                            };

                            UnitOfWork.AddEntity(hotel);

                            foreach (var d in h.Tours?.Values ?? new List<TourDataModel>())
                            {
                                if ((d.Data?.DateIn.HasValue ?? false) || (d.Data?.DateOut.HasValue ?? false) ||
                                    d.Data?.PlanChildrenCount > 0 || d.Data?.FactChildrenCount > 0)
                                {
                                    var tour = new MonitoringTourData
                                    {
                                        HotelDataId = hotel.Id,
                                        DateIn = d.Data.DateIn,
                                        DateOut = d.Data.DateOut,
                                        FactChildrenCount = d.Data.FactChildrenCount,
                                        PlanChildrenCount = d.Data.PlanChildrenCount
                                    };

                                    UnitOfWork.AddEntity(tour);
                                }
                            }
                        }


                        tran.Complete();
                        return RedirectToAction(nameof(ChildrenNumberInformationEdit), new { organisationId, yearOfRestId });
                    }

                    var mci = UnitOfWork.GetById<MonitoringChildrenNumberInformation>(model.Data.Id);

                    if (mci.LastUpdateTick != model.Data.LastUpdateTick)
                    {
                        return RedirectToAvalibleAction();
                    }

                    var entity = model.BuildData();

                    var sbd = new StringBuilder();

                    //удаление данных об более не используемых турах организаций отдыха и оздоровления
                    var existTourData = mci.HotelDatas.SelectMany(ss => ss.TourDatas.Select(sx => sx.Id)).ToList();
                    var tourData =
                        model.HotelDatas?.Values.Where(ss => ss.Data.Id > 0).SelectMany(ss =>
                            ss.Tours?.Values.Where(sx => sx.Data?.Id > 0).Select(sx => sx.Data.Id)).ToList() ??
                        new List<long>();
                    foreach (var tdId in existTourData.Except(tourData))
                    {
                        var tours = UnitOfWork.GetSet<MonitoringTourData>().Where(ss => ss.Id == tdId).ToList();
                        foreach (var t in tours)
                        {
                            sbd.AppendLine(
                                $"<li>Для организации отдыха и оздоровления \"{t.HotelData?.Hotel?.ShortName}\" удален заезд c {t.DateIn.FormatEx()} по {t.DateOut.FormatEx()}</li>");
                            UnitOfWork.Delete(t);
                            UnitOfWork.SaveChanges();
                        }
                    }

                    //удаление данных об более не используемых организациях отдыха и оздоровления
                    var existHotelData = mci.HotelDatas.Select(ss => ss.Id).ToList();
                    var hotelData = model.HotelDatas?.Values.Select(ss => ss.Data.Id).Where(ss => ss > 0).ToList() ??
                                    new List<long>();
                    foreach (var hdId in existHotelData.Except(hotelData))
                    {
                        var tours = UnitOfWork.GetSet<MonitoringTourData>().Where(ss => ss.HotelDataId == hdId).ToList();
                        UnitOfWork.Delete<MonitoringTourData>(tours);
                        UnitOfWork.SaveChanges();
                        var hd = UnitOfWork.GetById<MonitoringHotelData>(hdId);
                        sbd.AppendLine(
                            $"<li>Для организации отдыха и оздоровления \"{hd.Hotel?.ShortName}\" удалены все заезды</li>");
                        UnitOfWork.Delete(hd);
                        UnitOfWork.SaveChanges();
                    }

                    foreach (var h in model.HotelDatas?.Values ?? new List<HotelDataModel>())
                    {
                        //данные у которых не указана организация отдыха и оздоровления игнорируются
                        if ((h.Data?.HotelId ?? 0) < 1)
                        {
                            continue;
                        }

                        MonitoringHotelData hd;
                        //новые организации отдыха и оздоровления
                        if (h.Data.Id < 1)
                        {
                            hd = UnitOfWork.GetSet<MonitoringHotelData>().FirstOrDefault(ss =>
                                ss.HotelId == h.Data.HotelId && ss.ChildrenNumberInformationId == mci.Id);
                            if (hd == null)
                            {
                                hd = new MonitoringHotelData
                                {
                                    ChildrenNumberInformationId = mci.Id,
                                    HotelId = h.Data.HotelId
                                };
                                UnitOfWork.AddEntity(hd);

                                hd = UnitOfWork.GetById<MonitoringHotelData>(hd.Id);

                                sbd.AppendLine(
                                    $"<li>Добавлена организации отдыха и оздоровления \"{hd.Hotel.ShortName}\"</li>");
                            }
                        }
                        else
                        {
                            hd = UnitOfWork.GetById<MonitoringHotelData>(h.Data.Id);
                        }

                        foreach (var d in h.Tours?.Values)
                        {
                            // новые данные о заездах
                            if (d.Data.Id < 1)
                            {
                                // не пустые данные
                                if ((d.Data?.DateIn.HasValue ?? false) || (d.Data?.DateOut.HasValue ?? false) ||
                                    d.Data?.PlanChildrenCount > 0 || d.Data?.FactChildrenCount > 0)
                                {
                                    var tour = new MonitoringTourData
                                    {
                                        HotelDataId = hd.Id,
                                        DateIn = d.Data.DateIn,
                                        DateOut = d.Data.DateOut,
                                        FactChildrenCount = d.Data.FactChildrenCount,
                                        PlanChildrenCount = d.Data.PlanChildrenCount
                                    };

                                    UnitOfWork.AddEntity(tour);

                                    sbd.AppendLine(
                                        $"<li>Для организации отдыха и оздоровления \"{hd.Hotel.ShortName}\" добавлен новый заезд: с {tour.DateIn.FormatEx()} по {tour.DateOut.FormatEx()}, плановое кол-во детей: {tour.PlanChildrenCount}, фактическое кол-во: {tour.FactChildrenCount}</li>");
                                }
                            }
                            else
                            {
                                var td = UnitOfWork.GetById<MonitoringTourData>(d.Data.Id);

                                var diff = GetMonitoringTourDataDiff(td, d.BuildData());
                                //только если данные изменились
                                if (!string.IsNullOrWhiteSpace(diff))
                                {
                                    sbd.AppendLine(
                                        $"<li>Для организации отдыха и оздоровления \"{td.HotelData.Hotel.ShortName}\" изменились данные заезда:<ul>{diff}</ul></li>");

                                    td.DateIn = d.Data?.DateIn;
                                    td.DateOut = d.Data?.DateOut;
                                    td.PlanChildrenCount = d.Data?.PlanChildrenCount;
                                    td.FactChildrenCount = d.Data?.FactChildrenCount;

                                    UnitOfWork.SaveChanges();
                                }
                            }
                        }
                    }

                    var filediff = mci.LinkToFiles.Diff(entity.LinkToFiles, UnitOfWork);
                    if (!string.IsNullOrWhiteSpace(filediff))
                    {
                        sbd.AppendLine($"<li>{filediff}</li>");
                        var link = mci.LinkToFiles.SaveFiles(entity.LinkToFiles, UnitOfWork);
                        mci.LinkToFilesId = link.Id;
                        entity.LinkToFilesId = mci.LinkToFilesId;
                    }

                    var diffText = sbd.ToString();
                    if (!string.IsNullOrWhiteSpace(diffText))
                    {
                        WriteToHistory(mci.HistoryLinkId.Value,
                            $"Обновление сведений о численности детей для \"{org.Name}\" за {yar.Year} г.",
                            $"<ul>{diffText}</ul>", null, null, null);
                    }
                }

                if (action != null)
                {
                    var mci = UnitOfWork.GetById<MonitoringChildrenNumberInformation>(model.Data.Id);
                    var sb = new StringBuilder();
                    if (string.Equals(action.ActionCode,
                        AccessRightEnum.Monitoring.ChildrenNumberInformation.OnAgreement,
                        StringComparison.OrdinalIgnoreCase))
                    {
                        if (mci.HotelDatas.Any(ss => ss.TourDatas.Any(sx =>
                            sx.DateIn == null || sx.DateOut == null || sx.PlanChildrenCount < 1 ||
                            sx.FactChildrenCount < 0)))
                        {
                            sb.Append(
                                "<li>Невозможно отправить на согласование сведения численности детей в организациях отдыха и оздоровления. В списке существуют заезды с некорректными данными.</li>");
                        }
                    }

                    var err = sb.ToString();
                    if (!string.IsNullOrWhiteSpace(err))
                    {
                        var res = new ChildrenNumberInformationModel(mci)
                        {
                            ErrorMessage = $"<ul>{err}</ul>", IsValid = false
                        };

                        ChildrenNumberInformationSetState(res);
                        ChildrenNumberInformationSetBasis(res, organizationIds);

                        tran.Complete();
                        return View("ChildrenNumberInformation/Edit", res);
                    }


                    WriteToHistory(mci.HistoryLinkId.Value,
                        $"Изменение статуса сведений о численности детей для \"{org.Name}\" за {yar.Year} г.",
                        $"Изменение статуса с \"{mci.State?.Name}\" на \"{action.ToState?.Name}\"", action.ToStateId,
                        mci.StateId, null);
                    mci.StateId = action.ToStateId;
                }

                UnitOfWork.SaveChanges();
                tran.Complete();
            }

            return RedirectToAction(nameof(ChildrenNumberInformationEdit), new {organisationId, yearOfRestId});
        }


        /// <summary>
        ///     Реестр форм о численности детей
        /// </summary>
        [System.Web.Mvc.Route("Monitoring/ChildrenNumberInformationList")]
        public ActionResult ChildrenNumberInformationList(ChildrenNumberInformationFilterModel filter)
        {
            if (!Security.HasRight(AccessRightEnum.Monitoring.ReestrWork))
            {
                return RedirectToAvalibleAction();
            }

            var pageSize = Settings.Default.TablePageSize;

            if (filter == null)
            {
                filter = new ChildrenNumberInformationFilterModel();
            }

            var q = UnitOfWork.GetSet<MonitoringChildrenNumberInformation>().AsQueryable();
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

            filter.Result = new CommonPagedList<MonitoringChildrenNumberInformation>(result, filter.PageNumber,
                pageSize > 0 ? pageSize : Math.Max(10, totalCount), totalCount);

            filter.States = UnitOfWork.GetSet<StateMachineState>()
                .Where(ss => ss.StateMachineId == (long) StateMachineEnum.MonitoringMonitoringChildrenNumberInformation)
                .ToDictionary(ss => ss.Id, sx => sx.Name);
            filter.YearsOfRest = UnitOfWork.GetSet<YearOfRest>().ToDictionary(ss => ss.Id, sx => sx.Name);
            filter.Organisations = UnitOfWork.GetSet<Organization>().Where(ss => !ss.IsDeleted && ss.IsInMonitoring)
                .ToDictionary(ss => ss.Id, sx => sx.Name);

            return View("ChildrenNumberInformation/List", filter);
        }

        /// <summary>
        ///     Отправить уведомление о необходимости заполнения формы
        /// </summary>
        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.Route("Monitoring/ChildrenNumberInformationSendEvent")]
        public ActionResult ChildrenNumberInformationSendEvent(DateTime? sendEventDate, string message)
        {
            if (!Security.HasRight(AccessRightEnum.Monitoring.EventSent))
            {
                return RedirectToAvalibleAction();
            }

            SendEmailSend(AccessRightEnum.Monitoring.ChildrenNumberInformation.EventRecive, sendEventDate, message, "Уведомление о необходимости заполнения формы сведений о численности детей");

            var mcis = UnitOfWork.GetSet<MonitoringChildrenNumberInformation>().Where(x=>x.StateId == StateMachineStateEnum.Monitoring.ChildrenNumberInformation.Agreed && x.YearOfRest.Year == DateTime.Now.Year).ToList();

            var sms = UnitOfWork.GetById<StateMachineState>(StateMachineStateEnum.Monitoring.ChildrenNumberInformation.Formation);

            foreach (var mci in mcis)
            {
                WriteToHistory(mci.HistoryLinkId.Value,
                            $"Изменение статуса сведений о численности детей для \"{mci.Organisation.Name}\" за {mci.YearOfRest.Year} г.",
                            $"Изменение статуса с \"{mci.State?.Name}\" на \"{sms?.Name}\" на основании рассылки", sms?.Id,
                            mci.StateId, null);

                mci.StateId = StateMachineStateEnum.Monitoring.ChildrenNumberInformation.Formation;
                UnitOfWork.SaveChanges();
            }

            return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);
        }

        /// <summary>
        ///     Установка статусной модели
        /// </summary>
        private void ChildrenNumberInformationSetState(ChildrenNumberInformationModel model)
        {
            if (model.Data.Id < 1)
            {
                var state = UnitOfWork.GetById<StateMachineState>(StateMachineStateEnum.Monitoring
                    .ChildrenNumberInformation.Formation);

                model.State = new ViewModelState
                {
                    Actions = new List<StateMachineAction>(),
                    NeedSaveButton = true,
                    CanReturn = false,
                    NeedRemoveButton = false,
                    State = state,
                    FormSelector = "#childrenNumberInformationForm",
                    ActionSelector = "#StateMachineActionString"
                };

                model.Data.StateId = StateMachineStateEnum.Monitoring.ChildrenNumberInformation.Formation;
            }
            else
            {
                var isEditable =
                    model.Data.StateId == StateMachineStateEnum.Monitoring.ChildrenNumberInformation.Formation &&
                    Security.HasRight(AccessRightEnum.Monitoring.ChildrenNumberInformation.Edit);

                var actions = ApiStateController.GetActions(model.Data.State,
                    StateMachineEnum.MonitoringMonitoringChildrenNumberInformation);

                model.State = new ViewModelState
                {
                    State = model.Data.State,
                    Actions = actions,
                    NeedSaveButton = isEditable,
                    NeedRemoveButton = false,
                    CanReturn = false,
                    FormSelector = "#childrenNumberInformationForm",
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
                    Action = "ChildrenNumberInformationExcel",
                    ActionParameters = new
                        {organisationId = model.Data.OrganisationId, yearOfRestId = model.Data.YearOfRestId}
                });
            }
        }

        /// <summary>
        ///     Установка коллекций
        /// </summary>
        private void ChildrenNumberInformationSetBasis(ChildrenNumberInformationModel model,
            params long?[] organizationIds)
        {
            model.YearsOfRest = UnitOfWork.GetSet<YearOfRest>().OrderBy(ss => ss.Year).ToList();
            model.Organisations = UnitOfWork.GetSet<Organization>()
                .Where(ss => organizationIds.Contains(ss.Id) && ss.IsInMonitoring && !ss.IsDeleted)
                .OrderBy(ss => ss.Name).ToList();
        }

        /// <summary>
        ///     Разница в турах
        /// </summary>
        private string GetMonitoringTourDataDiff(MonitoringTourData entity, MonitoringTourData persisted)
        {
            var sb = new StringBuilder();

            if (persisted.DateIn != entity.DateIn)
            {
                sb.AppendLine(
                    $"<li>Для заезда с {persisted.DateIn.FormatEx()} по {persisted.DateOut.FormatEx()} изменилось поле \"Дата заезда\", старое значение:'{persisted.DateIn.FormatEx(string.Empty)}', новое значение:'{entity.DateIn.FormatEx(string.Empty)}'</li>");
            }

            if (persisted.DateOut != entity.DateOut)
            {
                sb.AppendLine(
                    $"<li>Для заезда с {persisted.DateIn.FormatEx()} по {persisted.DateOut.FormatEx()} изменилось поле \"Дата выезда\", старое значение:'{persisted.DateOut.FormatEx(string.Empty)}', новое значение:'{entity.DateOut.FormatEx(string.Empty)}'</li>");
            }


            if (persisted.PlanChildrenCount != entity.PlanChildrenCount)
            {
                sb.AppendLine(
                    $"<li>Для заезда с {persisted.DateIn.FormatEx()} по {persisted.DateOut.FormatEx()} изменилось поле \"Плановое кол-во детей\", старое значение:'{persisted.PlanChildrenCount}', новое значение:'{entity.PlanChildrenCount}'</li>");
            }


            if (persisted.FactChildrenCount != entity.FactChildrenCount)
            {
                sb.AppendLine(
                    $"<li>Для заезда с {persisted.DateIn.FormatEx()} по {persisted.DateOut.FormatEx()} изменилось поле \"Дата Заезда\", старое значение:'{persisted.FactChildrenCount}', новое значение:'{entity.FactChildrenCount}'</li>");
            }


            return sb.ToString();
        }
    }
}
