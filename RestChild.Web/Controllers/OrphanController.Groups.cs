using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using RestChild.Comon;
using RestChild.Comon.Enumeration;
using RestChild.DAL.RepositoryExtensions;
using RestChild.Domain;
using RestChild.Web.Extensions;
using RestChild.Web.Models;
using RestChild.Web.Models.Orphans;
using RestChild.Web.Properties;

namespace RestChild.Web.Controllers
{
    public partial class OrphanController
    {
        /// <summary>
        ///     Группа (потребность) -> поиск
        /// </summary>
        [Route("Orphanage/Groups/Search/")]
        public ActionResult OrphanageGroupsSearch(OrphanageGroupsFilterModel filter)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);
            if (!Security.HasRight(AccessRightEnum.Orphans.PupilGroup, filter.OrphanageId))
            {
                return RedirectToAvalibleAction();
            }

            ModelState.Clear();

            ApiController.FillGroups(filter, Settings.Default.TablePageSize);

            return PartialView("Partials/GroupList", filter);
        }

        /// <summary>
        ///     Группа (потребность) -> глобальный поиск
        /// </summary>
        [Route("Orphanage/Groups/List/")]
        public ActionResult OrphanageGroupsList(OrphanageGroupsFilterModel filter)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);
            if (!Security.HasRightForSomeOrganization(AccessRightEnum.Orphans.PupilGroup))
            {
                return RedirectToAvalibleAction();
            }

            ApiController.FillGroups(filter, Settings.Default.TablePageSize);

            return PartialView("OrphanageGroupSearch", filter);
        }

        /// <summary>
        ///     Группа (потребность) -> новый
        /// </summary>
        [Route("Orphanage/Groups/New/{orphanageId}")]
        public ActionResult OrphanageGroupNew(long orphanageId)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);
            if (!Security.HasRight(AccessRightEnum.Orphans.PupilGroup, orphanageId) && !Security.HasRight(AccessRightEnum.Orphans.PupilGroup))
            {
                return RedirectToAvalibleAction();
            }

            var result = TempData["OGE"] as OrphanageGroupModel;
            if (result == null)
            {
                var org = UnitOfWork.GetSet<Organization>()
                    .FirstOrDefault(ss => ss.Id == orphanageId && ss.Orphanage == true);
                if (org == null)
                {
                    throw new ArgumentNullException("Orphanage");
                }

                result = new OrphanageGroupModel(
                    new PupilGroup
                    {
                        Organization = org,
                        OrganizationId = orphanageId,
                        StateId = StateMachineStateEnum.PupilGroup.Formation
                    });
            }

            GroupSetState(result);

            ApiController.FillGroup(result);

            ViewbagTypeOfRestrictionsSet();

            return View("OrphanageGroupEdit", result);
        }

        /// <summary>
        ///     Группа (потребность) -> редактирование
        /// </summary>
        [Route("Orphanage/Groups/Edit/{groupId}")]
        public ActionResult OrphanageGroupEdit(long groupId, string ec)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);

            var pg = UnitOfWork.GetById<PupilGroup>(groupId);

            if (!Security.HasRight(AccessRightEnum.Orphans.PupilGroup, pg.OrganizationId))
            {
                return RedirectToAvalibleAction();
            }

            var result = PrepareModelForView(pg);

            var err = TempData[errorMessage] as string;

            if (!string.IsNullOrWhiteSpace(err))
            {
                result.ErrorMessage = $"<ul><li>{string.Join("</li><li>", err.Split(';').Where(s => !string.IsNullOrWhiteSpace(s)).ToArray())}</li></ul>";
                result.IsValid = false;
            }

            return View("OrphanageGroupEdit", result);
        }

        /// <summary>
        ///     подготовить модель
        /// </summary>
        private OrphanageGroupModel PrepareModelForView(PupilGroup pg)
        {
            var result = new OrphanageGroupModel(pg)
            {
                HasLimit = UnitOfWork.GetSet<LimitOnVedomstvo>().Any(ss =>
                    ss.OrganizationId == pg.Organization.ParentId && ss.YearOfRestId == pg.YearOfRestId &&
                    ss.StateId > 0 &&
                    ss.StateId != StateMachineStateEnum.Limit.Oiv.Formation)
            };


            GroupSetState(result);

            ApiController.FillGroup(result);

            ViewbagTypeOfRestrictionsSet();

            return result;
        }

        /// <summary>
        ///     Группа (потребность) -> Сохранить
        /// </summary>
        [Route("Orphanage/Groups/Save")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult OrphanageGroupSave(OrphanageGroupModel model)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);
            if (!Security.HasRight(AccessRightEnum.Orphans.PupilGroup, model.Data.OrganizationId))
            {
                return RedirectToAvalibleAction();
            }

            if (model.Data.StateId == StateMachineStateEnum.PupilGroup.Formation)
            {
                var err = ValidateModel(model);
                if (!string.IsNullOrWhiteSpace(err))
                {
                    if (model.Data.Id < 1)
                    {
                        model.ErrorMessage = $"<ul><li>{string.Join("</li><li>", err.Split(';').Where(s => !string.IsNullOrWhiteSpace(s)).ToArray())}</li></ul>";
                        model.IsValid = false;

                        TempData["OGE"] = model;

                        return RedirectToAction(nameof(OrphanageGroupNew), new { orphanageId = model.Data.OrganizationId });
                    }

                    TempData[errorMessage] = err;
                    return RedirectToAction(nameof(OrphanageGroupEdit), new { groupId = model.Data.Id });
                }
            }

            using (var tran = UnitOfWork.GetTransactionScope())
            {
                if (model.Data.Id < 1)
                {
                    model.Data.HistoryLink = ApiController.WriteHistory(model.Data.HistoryLink,
                        "Создание группы (потребности) учреждения социальной защиты", string.Empty);
                    model.Data.HistoryLinkId = model.Data.HistoryLink.Id;

                    var e = model.BuildData();

                    UnitOfWork.AddEntity(e);
                    UnitOfWork.SaveChanges();

                    model.Data.Id = e.Id;

                    WriteHistory(model.Data.Id, "создал группу (потребность) учреждения социальной защиты");

                    UnitOfWork.SaveChanges();

                    tran.Complete();
                }
                else
                {
                    if (UnitOfWork.GetLastUpdateTickById<PupilGroup>(model.Data.Id) != model.Data.LastUpdateTick)
                    {
                        SetRedicted();
                        return RedirectToAction(nameof(OrphanageGroupEdit), new { groupId = model.Data.Id});
                    }

                    var data = UnitOfWork.GetById<PupilGroup>(model.Data.Id);


                    //сохраняем только если группа в режиме редактирования и согласования (иначе только меняем статусы)
                    if (model.Data.StateId == StateMachineStateEnum.PupilGroup.Formation || model.Data.StateId == StateMachineStateEnum.PupilGroup.Agreed)
                    {
                        var md = model.BuildData();
                        md.HistoryLink = data.HistoryLink;
                        md.HistoryLinkId = data.HistoryLinkId;

                        var sb = new StringBuilder();

                        if (model.Data.StateId == StateMachineStateEnum.PupilGroup.Formation)
                        {
                            GetDiff(sb, md, data);

                            var hsd = model.HealthStatuses?.Values.Select(ss => ss.Id).ToList() ?? new List<long>();

                            var has = false;
                            foreach (var h in data.PupilsHealthStatuses?.Where(ss => !hsd.Contains(ss.Id)).ToList() ??
                                              new List<PupilsHealthStatus>())
                            {
                                UnitOfWork.Delete(h);
                                if (!has)
                                {
                                    has = true;
                                }
                            }

                            foreach (var h in model.HealthStatuses?.Where(ss => ss.Value.Id <= 0).Select(ss => ss.Value)
                                .ToList() ?? new List<PupilsHealthStatus>(0))
                            {
                                UnitOfWork.AddEntity(new PupilsHealthStatus
                                {
                                    PupilGroupId = data.Id,
                                    PupilsCount = h.PupilsCount,
                                    TypeOfRestrictionId = h.TypeOfRestrictionId,
                                    TypeOfSubRestrictionId = h.TypeOfSubRestrictionId
                                });
                                if (!has)
                                {
                                    has = true;
                                }
                            }

                            if (has)
                            {
                                sb.AppendLine("<li>Изменён состав информации о состоянии здоровья воспитанников</li>");
                            }

                            has = false;

                            var rsd = model.RequestsForPeriodOfRest?.Values.Select(ss => ss.Id).ToList() ??
                                      new List<long>();
                            foreach (var r in data.Requests?.Where(ss => !rsd.Contains(ss.Id)).ToList() ??
                                              new List<RequestForPeriodOfRest>())
                            {
                                UnitOfWork.Delete(r);
                                if (!has)
                                {
                                    has = true;
                                }
                            }

                            foreach (var r in model.RequestsForPeriodOfRest?.Where(ss => ss.Value.Id <= 0)
                                                  .Select(ss => ss.Value).ToList() ??
                                              new List<RequestForPeriodOfRest>(0))
                            {
                                UnitOfWork.AddEntity(new RequestForPeriodOfRest
                                {
                                    PupilGroupId = data.Id,
                                    TimeOfRestId = r.TimeOfRestId,
                                    PlaceOfRestId = r.PlaceOfRestId,
                                    TourId = r.TourId,
                                    PupilsCount = r.PupilsCount,
                                    MGTCollaboratorsCount = r.MGTCollaboratorsCount,
                                    CollaboratorsCount = r.CollaboratorsCount,
                                    VacationTo = r.VacationTo,
                                    VacationFrom = r.VacationFrom
                                });
                                if (!has)
                                {
                                    has = true;
                                }
                            }

                            if (has)
                            {
                                sb.AppendLine(
                                    "<li>Изменён состав информации о периодах отдыха воспитанников</li>");
                            }

                            var diff = sb.ToString();
                            if (!string.IsNullOrWhiteSpace(diff))
                            {
                                data.CopyEntity(md);
                                data.HistoryLink = ApiController.WriteHistory(data.HistoryLink,
                                    "Обновление сведений о группе (потребности) учреждения социальной защиты",
                                    $"<ul>{diff}</ul>");
                                data.HistoryLinkId = data.HistoryLink?.Id;

                                UnitOfWork.SaveChanges();
                            }
                        }
                        else if (model.Data.StateId == StateMachineStateEnum.PupilGroup.Agreed)
                        {
                            ModelState.Clear();

                            var br = new StringBuilder();

                            if (!string.IsNullOrEmpty(model.StateMachineActionString) && !string.Equals(model.StateMachineActionString, AccessRightEnum.Orphans.PupilGroupEdit, StringComparison.OrdinalIgnoreCase))
                            {
                                if (model.RequestsForPeriodOfRest?.Values.Any(ss => ss.TourId == null) ?? true)
                                {
                                    br.Append("Невозможно сохранить группу (потребность) без выбранного размещения;");
                                }

                                //#110435
                                /*if (!data.Pupils?.Any() ?? true)
                                {
                                    br.Append("Невозможно сохранить группу (потребность) без воспитанников;");
                                }*/

                                if (model.RequestsForPeriodOfRest?.Values.Any(x => x.PupilsCount < 1) ?? true)
                                {
                                    br.Append("Невозможно сохранить группу (потребность), в одном или нескольких периодах не верно задано число воспитанников;");
                                }
                            }

                            var err = br.ToString();
                            if (!string.IsNullOrWhiteSpace(err))
                            {
                                TempData[errorMessage] = err;
                                return RedirectToAction(nameof(OrphanageGroupEdit), new { @groupId = model.Data.Id });
                            }

                            var changed = false;

                            var periods = model.RequestsForPeriodOfRest?.Values.ToList() ??
                                          new List<RequestForPeriodOfRest>();

                            foreach (var r in periods)
                            {
                                var rr = UnitOfWork.GetById<RequestForPeriodOfRest>(r.Id);
                                if (rr != null)
                                {
                                    rr.TourId = r.TourId;
                                    UnitOfWork.SaveChanges();
                                    if (!changed)
                                    {
                                        changed = true;
                                        sb.Append("<li>Изменён состав информации о размещениях воспитанников</li>");
                                    }
                                }
                            }

                            var diff = sb.ToString();
                            if (!string.IsNullOrWhiteSpace(diff))
                            {
                                data.HistoryLink = ApiController.WriteHistory(data.HistoryLink,
                                    "Обновление сведений о группе (потребности) учреждения социальной защиты",
                                    $"<ul>{diff}</ul>");
                                data.HistoryLinkId = data.HistoryLink?.Id;

                                UnitOfWork.SaveChanges();
                            }
                        }
                    }

                    if (!string.IsNullOrEmpty(model.StateMachineActionString))
                    {
                        var errors = CheckOnChangeState(data, model.StateMachineActionString);

                        if (errors.Any())
                        {
                            UnitOfWork.SaveChanges();
                            tran.Complete();
                            return RedirectToAction(nameof(OrphanageGroupEdit),
                                new {groupId = model.Data.Id, ec = model.StateMachineActionString});
                        }

                        var action = ApiStateController.GetAction(
                            string.Equals(model.StateMachineActionString, "Delete", StringComparison.OrdinalIgnoreCase)
                                ? AccessRightEnum.Orphans.PupilGroupDelete
                                : model.StateMachineActionString);

                        data.HistoryLink = ApiController.WriteHistory(data.HistoryLink,
                            "Изменение статуса группы (потребности)",
                            $"Изменение статуса группы (потребности) с \"{data.NullSafe(c => c.State.Name)}\" на \"{action.NullSafe(a => a.ToState.Name)}\"",
                            action.ToStateId, data.StateId);
                        data.HistoryLinkId = data.HistoryLink?.Id;

                        data.StateId = action.ToStateId;

                        WriteHistory(model.Data.Id, "изменил данные группу (потребности) учреждения социальной защиты");

                        UnitOfWork.SaveChanges();
                    }

                    tran.Complete();
                }

                return RedirectToAction(nameof(OrphanageGroupEdit), new {groupId = model.Data.Id});
            }
        }

        /// <summary>
        ///     проверка на изменение статуса
        /// </summary>
        private List<string> CheckOnChangeState(PupilGroup entity, string action)
        {
            var res = new List<string>();

            if (action == AccessRightEnum.Orphans.PupilGroupForm)
            {
                if (!(entity?.Requests?.Any() ?? true))
                {
                    res.Add("Невозможно сохранить группу (потребность) без выбранного периода отдыха.");
                }
            }

            return res;
        }

        /// <summary>
        ///     Добавить в группу новые данные о состоянии здоровья воспитанников
        /// </summary>
        [Route("Orphanage/Groups/PupilHealthStatusAdd")]
        public ActionResult GroupPupilHealthStatusAdd(int index)
        {
            ViewData.TemplateInfo.HtmlFieldPrefix = $"HealthStatuses[{index}]";

            ViewbagTypeOfRestrictionsSet();

            return PartialView("EditorTemplates/GroupPupilHealthStatuses", new PupilsHealthStatus());
        }

        /// <summary>
        ///     Добавить в группу новые данные о периоде и направлении отдыха
        /// </summary>
        [Route("Orphanage/Groups/RequestsForPeriodOfRestAdd")]
        public ActionResult RequestsForPeriodOfRestAdd(int index, long? vacationPeriodId)
        {
            ViewData.TemplateInfo.HtmlFieldPrefix = $"RequestsForPeriodOfRest[{index}]";
            ViewData["vacationPeriod"] = vacationPeriodId;

            return PartialView("EditorTemplates/GroupRequestsForPeriodOfRest", new RequestForPeriodOfRest());
        }

        /// <summary>
        ///     Поиск воспитанников в группе
        /// </summary>
        [Route("Orphanage/Groups/PupilSearch")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult OrphanageGroupPupilSearch(OrphanagePupilsFilterModel filter)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);
            if (!Security.HasRight(AccessRightEnum.Orphans.PupilGroup, filter.OrphanageId))
            {
                return RedirectToAvalibleAction();
            }

            filter.Result = ApiController.GetPupils(filter);

            return PartialView("Partials/GroupPupilList", filter);
        }

        /// <summary>
        ///     Добавить воспитанника в группу
        /// </summary>
        [Route("Orphanage/Groups/PupilAdd")]
        [HttpPost]
        public ActionResult OrphanageGroupPupilAdd(long groupId, long pupilId)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);

            var g = UnitOfWork.GetById<PupilGroup>(groupId);

            if (!Security.HasRight(AccessRightEnum.Orphans.PupilGroup, g.OrganizationId))
            {
                return new HttpStatusCodeResult(403, "Unautorized");
            }

            var p = UnitOfWork.GetById<Pupil>(pupilId);

            if (p.OrphanageAddress.OrganisationId != g.OrganizationId)
            {
                return new HttpStatusCodeResult(403, "Wrong Orphanage");
            }

            if (g.Pupils.All(ss => ss.Id != p.Id))
            {
                g.Pupils.Add(p);
                UnitOfWork.SaveChanges();
            }

            return new HttpStatusCodeResult(200, "Success");
        }

        /// <summary>
        ///     Добавить воспитанника в группу
        /// </summary>
        [Route("Orphanage/Groups/PupilRemove")]
        [HttpPost]
        public ActionResult OrphanageGroupPupilRemove(long groupId, long pupilId)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);

            var g = UnitOfWork.GetById<PupilGroup>(groupId);

            if (!Security.HasRight(AccessRightEnum.Orphans.PupilGroup, g.OrganizationId))
            {
                return new HttpStatusCodeResult(403, "Unautorized");
            }

            var p = UnitOfWork.GetById<Pupil>(pupilId);

            if (p.OrphanageAddress.OrganisationId != g.OrganizationId)
            {
                return new HttpStatusCodeResult(403, "Wrong Orphanage");
            }

            if (g.Pupils.Any(ss => ss.Id == p.Id))
            {
                g.Pupils.Remove(p);
                UnitOfWork.SaveChanges();
            }

            return new HttpStatusCodeResult(200, "Success");
        }

        /// <summary>
        ///     Всплывающий список воспитанников для добавления в группу
        /// </summary>
        [Route("Orphanage/Groups/PupilsChoose")]
        [HttpPost]
        public ActionResult OrphanageGroupPupilsChoose(long orphanageId, long groupId)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);
            if (!Security.HasRight(AccessRightEnum.Orphans.PupilGroup, orphanageId))
            {
                return RedirectToAvalibleAction();
            }

            var pupils = UnitOfWork.GetSet<Pupil>().Where(ss =>
                ss.OrphanageAddress.OrganisationId == orphanageId && !ss.PupilGroups.Any(sx => sx.Id == groupId) &&
                ss.DateOut == null && (ss.Child.EntityId == null || ss.Child.EntityId == ss.ChildId)).ToList();

            var filter = new OrphanagePupilsFilterModel(pupils)
            {
                GroupId = groupId, ActionName = nameof(OrphanageGroupPupilsChooseSearch), IsInGroup = false
            };

            return PartialView("Partials/GroupPupilsToAdd", filter);
        }

        /// <summary>
        ///     Поиск во всплывающем списке воспитанников для добавления в группу
        /// </summary>
        [Route("Orphanage/Groups/PupilsChooseSearch")]
        [HttpPost]
        public ActionResult OrphanageGroupPupilsChooseSearch(OrphanagePupilsFilterModel filter)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);
            if (!Security.HasRight(AccessRightEnum.Orphans.PupilGroup, filter.OrphanageId))
            {
                return RedirectToAvalibleAction();
            }

            filter.IsFilled = true;
            filter.Result = ApiController.GetPupils(filter);

            return PartialView("Partials/GroupPupilsToAddForm", filter);
        }

        /// <summary>
        ///     проверка модели
        /// </summary>
        private string ValidateModel(OrphanageGroupModel model)
        {
            var sb = new StringBuilder();
            if (string.IsNullOrWhiteSpace(model.Data.Name))
            {
                sb.AppendLine("Порядковый номер группы не задан;");
            }

            if (model.HealthStatuses != null && model.HealthStatuses.Any(ss =>
                Convert.ToInt32(ss.Key) < 0 &&
                (!ss.Value.TypeOfRestrictionId.HasValue || !ss.Value.PupilsCount.HasValue)))
            {
                sb.AppendLine("Информация о состоянии здоровья воспитанников задана не верно;");
            }

            if (model.RequestsForPeriodOfRest != null && model.RequestsForPeriodOfRest.Any(ss =>
                Convert.ToInt32(ss.Key) < 0 &&
                (!ss.Value.TimeOfRestId.HasValue || !ss.Value.PlaceOfRestId.HasValue)))
            {
                sb.AppendLine("Информация о периодах и регионах отдыха задана не верно;");
            }

            return sb.ToString();
        }

        /// <summary>
        ///     Установка статусной модели
        /// </summary>
        private void GroupSetState(OrphanageGroupModel model)
        {
            if (model.Data.Id < 1)
            {
                var state = UnitOfWork.GetById<StateMachineState>(StateMachineStateEnum.PupilGroup.Formation);

                model.State = new ViewModelState
                {
                    Actions = new List<StateMachineAction>(),
                    NeedSaveButton = true,
                    CanReturn = false,
                    NeedRemoveButton = false,
                    State = state,
                    FormSelector = "#groupForm",
                    ActionSelector = "#StateMachineActionString"
                };
            }
            else
            {
                var isEditable = model.Data.StateId == StateMachineStateEnum.PupilGroup.Formation;

                var actions = ApiStateController.GetActions(model.Data.State, StateMachineEnum.PupilGroup);

                model.State = new ViewModelState
                {
                    State = model.Data.State,
                    Actions = actions,
                    NeedSaveButton = isEditable || model.Data.StateId == StateMachineStateEnum.PupilGroup.Agreed,
                    NeedRemoveButton = isEditable && Security.HasRight(AccessRightEnum.Orphans.PupilGroupDelete,
                        model.Data.OrganizationId),
                    CanReturn = false,
                    FormSelector = "#groupForm",
                    ActionSelector = "#StateMachineActionString"
                };

                if (model.Data?.HistoryLinkId.HasValue ?? false)
                {
                    model.State.PostNoStatusActions = model.State.PostNoStatusActions ?? new List<NoStatusAction>();
                    model.State.PostNoStatusActions.Add(new NoStatusAction
                    {
                        Name = "История",
                        ButtonClass = "btn btn-default btn-hystory-link",
                        SomeAddon = $"data-history-id=\"{model.Data.HistoryLinkId}\""
                    });
                }
            }
        }

        /// <summary>
        ///     Разница в группе (потребности)
        /// </summary>
        private void GetDiff(StringBuilder sb, PupilGroup entity, PupilGroup persisted)
        {
            if (persisted.Name != entity.Name)
            {
                sb.AppendLine(
                    $"<li>Изменён порядковый номер группы, старое значение:'{persisted.Name.FormatEx(string.Empty)}', новое значение:'{entity.Name.FormatEx(string.Empty)}'</li>");
            }

            if (persisted.VacationPeriodId != entity.VacationPeriodId)
            {
                sb.AppendLine(
                    $"<li>Изменён каникулярный период группы, старое значение:'{persisted.VacationPeriod?.Name.FormatEx(string.Empty)}', новое значение:'{UnitOfWork.GetById<PupilGroupVacationPeriod>(entity.VacationPeriodId)?.Name.FormatEx(string.Empty)}'</li>");
            }


            if (persisted.FormOfRestId != entity.FormOfRestId)
            {
                sb.AppendLine(
                    $"<li>Изменена форма отдыха и оздоровления, старое значение:'{persisted.FormOfRest?.Name.FormatEx(string.Empty)}', новое значение:'{entity.FormOfRest?.Name.FormatEx(string.Empty)}'</li>");
            }

            if (persisted.PupilsCount != entity.PupilsCount)
            {
                sb.AppendLine(
                    $"<li>Изменено кол-во воспитанников, старое значение:'{persisted.PupilsCount.FormatEx(string.Empty)}', новое значение:'{entity.PupilsCount.FormatEx(string.Empty)}'</li>");
            }

            if (persisted.CollaboratorsCount != entity.CollaboratorsCount)
            {
                sb.AppendLine(
                    $"<li>Изменено кол-во сопровождающих от учреждения, старое значение:'{persisted.CollaboratorsCount.FormatEx(string.Empty)}', новое значение:'{entity.CollaboratorsCount.FormatEx(string.Empty)}'</li>");
            }

            if (persisted.MGTCollaboratorsCount != entity.MGTCollaboratorsCount)
            {
                sb.AppendLine(
                    $"<li>Изменено кол-во сопровождающих от МГТ, старое значение:'{persisted.MGTCollaboratorsCount.FormatEx(string.Empty)}', новое значение:'{entity.MGTCollaboratorsCount.FormatEx(string.Empty)}'</li>");
            }
        }
    }
}
