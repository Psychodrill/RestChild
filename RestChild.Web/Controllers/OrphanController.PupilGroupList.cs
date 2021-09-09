using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using RestChild.Comon;
using RestChild.Comon.Enumeration;
using RestChild.DAL.RepositoryExtensions;
using RestChild.Domain;
using RestChild.Extensions.Filter;
using RestChild.Web.Extensions;
using RestChild.Web.Models;
using RestChild.Web.Models.Orphans;
using RestChild.Web.Properties;

namespace RestChild.Web.Controllers
{
    public partial class OrphanController
    {
        /// <summary>
        ///     Список/группа отправки -> поиск
        /// </summary>
        [Route("Orphanage/PupilGroupLists/Search/")]
        [HttpPost]
        public ActionResult OrphanagePupilGroupListsSearch(OrphanagePupilGroupListFilterModel filter)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);
            if (!Security.HasRight(AccessRightEnum.Orphans.PupilGroupList, filter.OrphanageId))
            {
                return RedirectToAvalibleAction();
            }

            ModelState.Clear();

            ApiController.FillPupilGroupList(filter, Settings.Default.TablePageSize);

            return PartialView("Partials/ListsList", filter);
        }

        /// <summary>
        ///     Список/группа отправки -> глобальный поиск
        /// </summary>
        [Route("Orphanage/PupilGroupLists/List/")]
        public ActionResult OrphanagePupilGroupListsList(OrphanagePupilGroupListFilterModel filter)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);
            if (!Security.HasRightForSomeOrganization(AccessRightEnum.Orphans.PupilGroupList))
            {
                return RedirectToAvalibleAction();
            }

            ApiController.FillPupilGroupList(filter, Settings.Default.TablePageSize);

            return PartialView("OrphanagePupilGroupListSearch", filter);
        }

        /// <summary>
        ///     Список/группа отправки -> редактирование
        /// </summary>
        [Route("Orphanage/PupilGroupLists/Edit/{listId}")]
        public ActionResult OrphanagePupilGroupListEdit(long? listId)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);
            var list = UnitOfWork.GetById<ListOfChilds>(listId);

            if (!Security.HasRight(AccessRightEnum.Orphans.PupilGroupList, list?.LimitOnOrganization?.OrganizationId))
            {
                return RedirectToAvalibleAction();
            }

            if (list == null)
            {
                // ReSharper disable once NotResolvedInText
                throw new ArgumentNullException("PupilGroupList");
            }

            var req = list.PupilGroupRequest.FirstOrDefault();
            if (req == null)
            {
                // ReSharper disable once NotResolvedInText
                throw new ArgumentNullException("PupilGroupList.RequestForPeriodOfRest");
            }

            var result = new OrphanagePupilGroupListModel(list);
            OrphanagePupilGroupListModelFill(result, req);

            var i = -1;

            foreach (var a in result.Data.PupilGroupRequest?.FirstOrDefault()?.PupilGroup?.Organization
                ?.OrphanageOrganizationAddresses ?? new List<OrphanageAddress>())
            {
                if (result.Transfers.Values.Any(x => x.AddressId == a.Id))
                {
                    continue;
                }

                if (result.Data.StateId == StateMachineStateEnum.PupilGroupList.Formation)
                {
                    result.Transfers.Add(i.ToString(), new PupilGroupListTransfer
                    {
                        Address = a,
                        AddressId = a.Id
                    });
                    i--;
                }
            }

            PupilGroupListSetState(result);

            var errMsg = TempData[errorMessage] as string;
            if (!string.IsNullOrWhiteSpace(errMsg))
            {
                result.ErrorMessage = errMsg;
            }

            return View("OrphanagePupilGroupListsEdit", result);
        }

        /// <summary>
        ///     Список/группа отправки -> создание
        /// </summary>
        [Route("Orphanage/PupilGroupLists/New/{requestForPeriodOfRestId}")]
        public ActionResult OrphanagePupilGroupListNew(long? requestForPeriodOfRestId)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);
            var req = UnitOfWork.GetById<RequestForPeriodOfRest>(requestForPeriodOfRestId);

            if (!Security.HasRight(AccessRightEnum.Orphans.PupilGroup, req.PupilGroup?.OrganizationId))
            {
                return RedirectToAvalibleAction();
            }

            if (req?.TourId == null || !req.TimeOfRestId.HasValue || !req.PupilGroupId.HasValue ||
                !req.PlaceOfRestId.HasValue || req.ListsId.HasValue)
            {
                return RedirectToAvalibleAction();
            }

            if (req.ListsId.HasValue && req.Lists?.StateId != StateMachineStateEnum.PupilGroupList.Deleted)
            {
                return RedirectToAction(nameof(OrphanagePupilGroupListEdit), new {listId = req.ListsId.Value});
            }

            var limit = UnitOfWork.GetSet<LimitOnVedomstvo>().Any(ss =>
                ss.OrganizationId == req.PupilGroup.Organization.ParentId &&
                ss.YearOfRestId == req.PupilGroup.YearOfRestId &&
                ss.StateId == StateMachineStateEnum.Limit.Oiv.Brought);
            if (!limit)
            {
                // ReSharper disable once NotResolvedInText
                throw new ArgumentNullException("LimitOnVedomstvo");
            }

            var result = new OrphanagePupilGroupListModel(
                new ListOfChilds
                {
                    StateId = StateMachineStateEnum.PupilGroupList.Formation
                });

            OrphanagePupilGroupListModelFill(result, req);

            var i = -1;

            foreach (var p in req.PupilGroup?.Pupils ?? new List<Pupil>())
            {
                result.Pupils.Add(i.ToString(), new OrphanagePupilGroupListMemberModel(new PupilGroupListMember
                {
                    Pupil = p,
                    PupilId = p.Id,
                    OrganisatonAddresId = p.OrphanageAddressId,
                    OrganisatonAddres = p.OrphanageAddress

                })
                {
                    DrugDoses = p.Drugs?.Where(ss => !ss.IsDeleted).ToDictionary(ss => Guid.NewGuid().ToString(), ss =>
                        new PupilGroupListMemberDrugDose
                        {
                            Dose = ss,
                            DoseId = ss.Id,
                            DrugQuantity = ss.Dose
                        }) ?? new Dictionary<string, PupilGroupListMemberDrugDose>(0)
                });
                i--;
            }

            i = -1;

            foreach (var a in req.PupilGroup?.Organization?.OrphanageOrganizationAddresses ??
                              new List<OrphanageAddress>())
            {
                result.Transfers.Add(i.ToString(), new PupilGroupListTransfer
                {
                    Address = a,
                    AddressId = a.Id,
                    CountPeople = req.PupilGroup?.Pupils.Count(sx => sx.OrphanageAddressId == a.Id) ?? 0
                });
                i--;
            }

            PupilGroupListSetState(result);

            return View("OrphanagePupilGroupListsEdit", result);
        }

        /// <summary>
        ///     Заполнение вспомогательных данных для списка/группы отправки
        /// </summary>
        private void OrphanagePupilGroupListModelFill(OrphanagePupilGroupListModel data,
            RequestForPeriodOfRest req)
        {
            data.PupilGroupRequestId = req.Id;
            data.OrganizationId = req.PupilGroup?.OrganizationId ?? 0;
            data.YearOfRestName = req.PupilGroup?.YearOfRest?.Name;
            data.PupilGroupName = req.PupilGroup?.Name;
            data.FormOfRestName = req.PupilGroup?.FormOfRest.Name;
            data.TimeOfRestName = $"{req.Tour.Hotels.Name} {req.Tour.Name}";
            data.CollaboratorsCount = req.CollaboratorsCount;
            data.MGTCollaboratorsCount = req.MGTCollaboratorsCount;
            data.PupilsCount = req.PupilsCount;

            ViewBag.PossibleTransferAdresses = UnitOfWork.GetSet<OrphanageAddress>().Where(oa => oa.OrganisationId == data.OrganizationId).ToList();
        }

        /// <summary>
        ///     Список/группа отправки -> создание
        /// </summary>
        [Route("Orphanage/PupilGroupLists/Save")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult OrphanagePupilGroupListSave(OrphanagePupilGroupListModel model)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);
            if (!Security.HasRight(AccessRightEnum.Orphans.PupilGroupList, model.OrganizationId))
            {
                return RedirectToAvalibleAction();
            }

            using (var tran = UnitOfWork.GetTransactionScope())
            {
                var e = model.BuildData();

                StateMachineAction action = null;

                if (!string.IsNullOrEmpty(model.StateMachineActionString))
                {
                    action = ApiStateController.GetAction(
                        string.Equals(model.StateMachineActionString, "Delete", StringComparison.OrdinalIgnoreCase)
                            ? AccessRightEnum.Orphans.PupilGroupListDelete
                            : model.StateMachineActionString);
                }

                if (e.Id < 1)
                {
                    var req = UnitOfWork.GetById<RequestForPeriodOfRest>(model.PupilGroupRequestId);

                    e.LimitOnOrganizationId = LimitOfOrganisationGetOrCreate(req);
                    e.DateChange = DateTime.Now;
                    e.PlaceOfRestId = req.PlaceOfRestId;
                    e.TimeOfRestId = req.TimeOfRestId;
                    e.TourId = req.TourId;
                    e.TypeOfLimitListId = (long) TypeOfLimitListEnum.Orphan;
                    e.IsLast = true;
                    e.Name = req.PupilGroup?.Name;

                    var rfr = req.PupilGroup?.Organization?.OrganisatonCollaborators.FirstOrDefault(ss => ss.PositionId == (long) OrphanageCollaboratorType.ResponsibleForRest && !ss.Applicant.IsDeleted);
                    if (rfr != null)
                    {
                        e.Responsible = rfr.Applicant.GetFio();
                        e.ResponsiblePhone = rfr.Applicant.Phone;
                    }

                    e.HistoryLink = ApiController.WriteHistory(e.HistoryLink, "Создание списка (группы отправки) учреждения социальной защиты", string.Empty);
                    e.HistoryLinkId = e.HistoryLink.Id;

                    UnitOfWork.AddEntity(e);
                    UnitOfWork.SaveChanges();

                    WriteHistory(e.Id, "создал список (группу отправки) учреждения социальной защиты");

                    req.ListsId = e.Id;

                    UnitOfWork.SaveChanges();

                    tran.Complete();
                }
                else
                {
                    if (UnitOfWork.GetLastUpdateTickById<ListOfChilds>(e.Id) != e.LastUpdateTick)
                    {
                        SetRedicted();
                        return RedirectToAction(nameof(OrphanagePupilGroupListEdit), new {listId = e.Id});
                    }

                    var data = UnitOfWork.GetById<ListOfChilds>(e.Id);

                    var md = model.BuildData();

                    if (e.StateId == StateMachineStateEnum.PupilGroupList.Formation)
                    {
                        var sb = new StringBuilder();

                        if (data.PupilsRulesAgreement != md.PupilsRulesAgreement)
                        {
                            data.PupilsRulesAgreement = md.PupilsRulesAgreement;
                            sb.AppendLine(
                                $"<li>Изменён состав информации о списке: Новые данные (\"Воспитанники с Правилами ознакомлены\": {(data.PupilsRulesAgreement ? "да" : "нет")})</li>");
                        }

                        if (data.RulesAgreement != md.RulesAgreement)
                        {
                            data.RulesAgreement = md.RulesAgreement;
                            sb.AppendLine(
                                $"<li>Изменён состав информации о списке: Новые данные (\"Подтверждаю, что ознакомлен и согласен с Правилами\": {(data.PupilsRulesAgreement ? "да" : "нет")})</li>");
                        }

                        {

                            //удаление воспитанников
                            var ps = md.GroupPupils.Select(ss => ss.Id).ToList();
                            foreach (var h in data.GroupPupils?.Where(ss => !ps.Contains(ss.Id)).ToList() ??
                                              new List<PupilGroupListMember>())
                            {
                                sb.AppendLine(
                                    $"<li>Изменён состав информации о воспитанниках: {h.Pupil.Child.GetFio()} удалён из списка</li>");
                                foreach (var pupilGroupListMemberDrugDose in UnitOfWork
                                    .GetSet<PupilGroupListMemberDrugDose>()
                                    .Where(ss => ss.GroupPupilId == h.Id).ToList())
                                {
                                    UnitOfWork.Delete(pupilGroupListMemberDrugDose);
                                }

                                UnitOfWork.Delete(h);
                            }

                            //удаление пустых связей воспитанник -> доза
                            foreach (var dd in UnitOfWork.GetSet<PupilGroupListMemberDrugDose>()
                                .Where(ss => ss.GroupPupilId == null))
                            {
                                UnitOfWork.Delete(dd);
                            }

                            //Добавление новых воспитанников
                            foreach (var h in md.GroupPupils?.Where(ss => Convert.ToInt32(ss.Id) <= 0).ToList() ??
                                              new List<PupilGroupListMember>(0))
                            {
                                var p = UnitOfWork.GetById<Pupil>(h.PupilId);

                                var np = new PupilGroupListMember
                                {
                                    PupilId = h.PupilId,
                                    GroupRequestListId = e.Id,
                                    TicketTo = h.TicketTo,
                                    TicketFrom = h.TicketFrom,
                                    OrganisatonAddresId = h.OrganisatonAddresId
                                };

                                UnitOfWork.AddEntity(np);

                                var drugs = new List<string>();
                                foreach (var dose in h.GroupPupilDoses?.ToList() ??
                                                     new List<PupilGroupListMemberDrugDose>(0))
                                {
                                    var npd = new PupilGroupListMemberDrugDose
                                    {
                                        DoseId = dose.DoseId,
                                        DrugQuantity = dose.DrugQuantity,
                                        GroupPupilId = np.Id
                                    };

                                    UnitOfWork.AddEntity(npd);

                                    drugs.Add($"{p.Drugs.Where(ss => ss.Id == dose.DoseId).Select(ss => ss.Drug.Name).FirstOrDefault()} дозировка: {dose.DrugQuantity}");
                                }

                                var drugInfo = string.Empty;
                                if (drugs.Count > 0)
                                {
                                    drugInfo = $"; Лекарственные препараты:{string.Join(" ,", drugs)}";
                                }

                                h.OrganisatonAddres = UnitOfWork.GetById<OrphanageAddress>(h.OrganisatonAddresId);

                                sb.AppendLine(
                                    $"<li>Изменён состав информации о воспитанниках: {p.Child.GetFio()} (Билет туда: {(h.TicketTo ? "да" : "нет")}; Билет обратно: {(h.TicketFrom ? "да" : "нет")}; Адрес подачи трансфера: {h.OrganisatonAddres?.Address?.Name}{drugInfo}) - добавлен в список</li>");
                            }

                            //изменение данных по связанным воспитанникам
                            foreach (var h in md.GroupPupils?.Where(ss => Convert.ToInt32(ss.Id) > 0).ToList() ??
                                              new List<PupilGroupListMember>(0))
                            {
                                var m = UnitOfWork.GetById<PupilGroupListMember>(h.Id);
                                var mainChanged = false;
                                if (m.TicketTo != h.TicketTo || m.TicketFrom != h.TicketFrom || m.OrganisatonAddresId != h.OrganisatonAddresId)
                                {
                                    m.TicketTo = h.TicketTo;
                                    m.TicketFrom = h.TicketFrom;
                                    m.OrganisatonAddresId = h.OrganisatonAddresId;

                                    UnitOfWork.SaveChanges();
                                    mainChanged = true;
                                }

                                var drugs = new List<string>();
                                foreach (var d in m.GroupPupilDoses)
                                {
                                    var dd = h.GroupPupilDoses.FirstOrDefault(ss => ss.Id == d.Id);
                                    if (dd != null)
                                    {
                                        if (dd.DrugQuantity != d.DrugQuantity)
                                        {
                                            d.DrugQuantity = dd.DrugQuantity;
                                            UnitOfWork.SaveChanges();
                                            drugs.Add(
                                                $"{m.GroupPupilDoses.Where(ss => ss.DoseId == d.DoseId).Select(ss => ss.Dose.Drug.Name).FirstOrDefault()} дозировка: {dd.DrugQuantity}");
                                        }
                                    }
                                }

                                var drugInfo = string.Empty;
                                if (drugs.Count > 0)
                                {
                                    drugInfo = $"; Лекарственные препараты:{string.Join(" ,", drugs)}";
                                    mainChanged = true;
                                }

                                if (mainChanged)
                                {
                                    h.OrganisatonAddres = UnitOfWork.GetById<OrphanageAddress>(h.OrganisatonAddresId);

                                    sb.AppendLine(
                                        $"<li>Изменён состав информации о воспитанниках: {m.Pupil.Child.GetFio()} Новые данные (Билет туда: {(h.TicketTo ? "да" : "нет")}; Билет обратно: {(h.TicketFrom ? "да" : "нет")}; Адрес подачи трансфера: {h.OrganisatonAddres?.Address?.Name}{drugInfo})</li>");
                                }
                            }
                        }

                        {
                            //удаление сопровождающих
                            var rsd = md.GroupCollaborators?.Select(ss => ss.Id).ToList();
                            foreach (var r in data.GroupCollaborators?.Where(ss => !rsd?.Contains(ss.Id) ?? false).ToList())
                            {
                                sb.AppendLine(
                                    $"<li>Изменён состав информации о сопровождающих: {r.OrganisatonCollaborator.Applicant.GetFio()} удалён из списка</li>");
                                UnitOfWork.Delete(r);
                            }

                            //добавление сопровождающих
                            foreach (var c in md.GroupCollaborators?.Where(ss => ss.Id <= 0).ToList() ??
                                              new List<PupilGroupListCollaborator>(0))
                            {
                                var r = UnitOfWork.GetById<OrganisatorCollaborator>(c.OrganisatonCollaboratorId);
                                UnitOfWork.AddEntity(new PupilGroupListCollaborator
                                {
                                    TicketFrom = c.TicketFrom,
                                    TicketTo = c.TicketTo,
                                    OrganisatonCollaboratorId = c.OrganisatonCollaboratorId,
                                    GroupRequestListId = e.Id,
                                    OrganisatonAddresId = c.OrganisatonAddresId

                                });

                                c.OrganisatonAddres = UnitOfWork.GetById<OrphanageAddress>(c.OrganisatonAddresId);

                                sb.AppendLine(
                                    $"<li>Изменён состав информации о сопровождающих: {r.Applicant.GetFio()} (Билет туда: {(c.TicketTo ? "да" : "нет")}; Билет обратно: {(c.TicketFrom ? "да" : "нет")}; Адрес подачи трансфера: {c.OrganisatonAddres?.Address?.Name}) добавлен в список</li>");
                            }

                            //изменение сопровождающих
                            foreach (var c in md.GroupCollaborators?.Where(ss => ss.Id > 0).ToList() ??
                                              new List<PupilGroupListCollaborator>(0))
                            {
                                var r = UnitOfWork.GetById<PupilGroupListCollaborator>(c.Id);
                                if (r.TicketTo != c.TicketTo || r.TicketFrom != c.TicketFrom || r.OrganisatonAddresId != c.OrganisatonAddresId)
                                {
                                    r.TicketTo = c.TicketTo;
                                    r.TicketFrom = c.TicketFrom;
                                    r.OrganisatonAddresId = c.OrganisatonAddresId;

                                    UnitOfWork.SaveChanges();

                                    r.OrganisatonAddres = UnitOfWork.GetById<OrphanageAddress>(r.OrganisatonAddresId);

                                    sb.AppendLine(
                                        $"<li>Изменён состав информации о сопровождающих: {r.OrganisatonCollaborator.Applicant.GetFio()} Новые данные (Билет туда: {(c.TicketTo ? "да" : "нет")}; Билет обратно: {(c.TicketFrom ? "да" : "нет")}; Адрес подачи трансфера: {c.OrganisatonAddres?.Address?.Name}) добавлен в список</li>");
                                }
                            }

                        }

                        //добавление трансферов
                        foreach (var t in md.GroupTransfers?.Where(ss => ss.Id <= 0 && ss.CountPeople > 0).ToList() ??
                                          new List<PupilGroupListTransfer>(0))
                        {
                            var a = UnitOfWork.GetById<OrphanageAddress>(t.AddressId);
                            UnitOfWork.AddEntity(new PupilGroupListTransfer
                            {
                                GroupRequestListId = e.Id,
                                AddressId = t.AddressId,
                                LargeParkingReAddress = t.LargeParkingReAddress,
                                Note = t.Note,
                                BoardingHelp = t.BoardingHelp,
                                CountPeople = t.CountPeople
                            });

                            sb.AppendLine(
                                $"<li>Изменён состав информации о трансферах: Трансфер на адрес {a.Address.Name} (Количество человек: {t.CountPeople}; Помощь при погрузке: {(t.BoardingHelp ? "да" : "нет")}; Адрес резервной парковки: {t.LargeParkingReAddress}; Примечания: {t.Note}) добавлен в список</li>");
                        }

                        //изменение трансферов
                        foreach (var t in md.GroupTransfers?.Where(ss => ss.Id > 0).ToList() ??
                                          new List<PupilGroupListTransfer>(0))
                        {
                            var tr = UnitOfWork.GetById<PupilGroupListTransfer>(t.Id);
                            if (!t.CountPeople.HasValue || t.CountPeople.Value < 1)
                            {
                                sb.AppendLine(
                                    $"<li>Изменён состав информации о трансферах: Трансфер на адрес {tr.Address.Address.Name} удалён из списка</li>");
                                UnitOfWork.Delete(tr);
                            }
                            else
                            {
                                if (tr.Note != t.Note || tr.LargeParkingReAddress != t.LargeParkingReAddress ||
                                    tr.BoardingHelp != t.BoardingHelp || tr.CountPeople != t.CountPeople)
                                {
                                    tr.LargeParkingReAddress = t.LargeParkingReAddress;
                                    tr.Note = t.Note;
                                    tr.BoardingHelp = t.BoardingHelp;
                                    tr.CountPeople = t.CountPeople;

                                    UnitOfWork.SaveChanges();

                                    sb.AppendLine(
                                        $"<li>Изменён состав информации о трансферах: Трансфер на адрес {tr.Address.Address.Name} Новые данные (Количество человек: {t.CountPeople}; Помощь при погрузке: {(t.BoardingHelp ? "да" : "нет")}; Адрес резервной парковки: {t.LargeParkingReAddress}; Примечания: {t.Note}) добавлен в список</li>");
                                }
                            }
                        }

                        var diffs = sb.ToString();
                        if (!string.IsNullOrWhiteSpace(diffs))
                        {
                            data.HistoryLink = ApiController.WriteHistory(data.HistoryLink,
                                "Обновление сведений о списке (группе отправки) учреждения социальной защиты",
                                $"<ul>{diffs}</ul>");
                            data.HistoryLinkId = data.HistoryLink?.Id;

                            UnitOfWork.SaveChanges();
                        }
                    }

                    if (action != null)
                    {
                        if (string.Equals(action.ActionCode, AccessRightEnum.Orphans.PupilGroupListApprove, StringComparison.OrdinalIgnoreCase))
                        {
                            //удаляем связи детей с данным списком (тех с кем нет связи (связь разорвана))
                            var ec = UnitOfWork.GetSet<PupilGroupListMember>()
                                .Where(ss => ss.GroupRequestListId == data.Id).Select(ss => ss.Pupil.ChildId)
                                .AsQueryable();
                            foreach (var child in UnitOfWork.GetSet<Child>()
                                .Where(ss => ss.ChildListId == data.Id && !ec.Contains(ss.Id)).ToList())
                            {
                                child.ChildListId = null;
                                child.ChildList = null;
                                child.IsDeleted = true;
                                UnitOfWork.SaveChanges();
                            }

                            //копируем детей
                            foreach (var gp in UnitOfWork.GetSet<PupilGroupListMember>()
                                .Where(ss => ss.GroupRequestListId == data.Id).ToList())
                            {
                                var child = UnitOfWork.GetById<Child>(gp.Pupil.ChildId);

                                if (child.ChildListId.HasValue)
                                {
                                    continue;
                                }

                                //копия ребёнка
                                var newChild = new Child(child, 0)
                                {
                                    AddressId = null,
                                    ChildListId = data.Id,
                                    Address = new Address(child.Address, 0),
                                    EntityId = child.Id,
                                    Payed = true
                                };
                                UnitOfWork.AddEntity(newChild);


                                //Для каждого ребёнка создаём воспитанника
                                var pupil = UnitOfWork.GetById<Pupil>(gp.PupilId);
                                var newPupil = new Pupil(pupil, 0) {ChildId = newChild.Id, EntityId = pupil.Id};
                                UnitOfWork.AddEntity(newPupil);

                                var drugDict = new Dictionary<long, long>();
                                //для каждого воспитанника создаём его наркотики
                                foreach (var drug in UnitOfWork.GetSet<PupilDose>().Where(ss => ss.PupilId == pupil.Id)
                                    .ToList())
                                {
                                    var newDrugDose = new PupilDose(drug, 0) {PupilId = newPupil.Id};
                                    UnitOfWork.AddEntity(newDrugDose);
                                    drugDict[drug.Id] = newDrugDose.Id;
                                }

                                //переписываем дозы на время отдыха
                                foreach (var pupilGroupListMemberDrugDose in UnitOfWork
                                    .GetSet<PupilGroupListMemberDrugDose>().Where(ss => ss.GroupPupilId == gp.Id)
                                    .ToList())
                                {
                                    pupilGroupListMemberDrugDose.DoseId =
                                        drugDict[pupilGroupListMemberDrugDose.DoseId.Value];
                                    UnitOfWork.SaveChanges();
                                }

                                //привязываем нового воспитанника к листу
                                gp.PupilId = newPupil.Id;
                                UnitOfWork.SaveChanges();
                            }

                            //удаляем связи сопровождающих с данным списком
                            var eco = UnitOfWork.GetSet<PupilGroupListCollaborator>()
                                .Where(ss => ss.GroupRequestListId == data.Id)
                                .Select(ss => ss.OrganisatonCollaborator.ApplicantId).AsQueryable();
                            foreach (var app in UnitOfWork.GetSet<Applicant>()
                                .Where(ss => ss.ChildListId == data.Id && !eco.Contains(ss.Id)).ToList())
                            {
                                app.ChildListId = null;
                                app.ChildList = null;
                                app.IsDeleted = true;
                                UnitOfWork.SaveChanges();
                            }

                            //копируем сопровождающих
                            foreach (var pupilGroupListCollaborator in UnitOfWork.GetSet<PupilGroupListCollaborator>()
                                .Where(ss => ss.GroupRequestListId == data.Id).ToList())
                            {
                                var app = UnitOfWork.GetById<Applicant>(pupilGroupListCollaborator
                                    .OrganisatonCollaborator.ApplicantId);

                                if (app.ChildListId.HasValue)
                                {
                                    continue;
                                }

                                //копия заявителя
                                var newApp = new Applicant(app, 0)
                                {
                                    AddressId = null,
                                    ChildListId = data.Id,
                                    Address = new Address(app.Address, 0),
                                    EntityId = app.Id,
                                    Payed = true
                                };
                                UnitOfWork.AddEntity(newApp);


                                //копия сотрудника
                                var coll = UnitOfWork.GetById<OrganisatorCollaborator>(pupilGroupListCollaborator
                                    .OrganisatonCollaboratorId);
                                var newColl = new OrganisatorCollaborator(coll, 0)
                                {
                                    OrganisatonId = null,
                                    OrganisatonAddressId = null
                                };
                                newColl.OrganisatonId = coll.OrganisatonId;
                                newColl.OrganisatonAddressId = coll.OrganisatonAddressId;
                                newColl.EntityId = coll.Id;
                                newColl.ApplicantId = newApp.Id;
                                UnitOfWork.AddEntity(newColl);

                                //привязываем нового сопровождающего к листу
                                pupilGroupListCollaborator.OrganisatonCollaboratorId = newColl.Id;
                                UnitOfWork.SaveChanges();
                            }
                        }

                        if (string.Equals(action.ActionCode, AccessRightEnum.Orphans.PupilGroupListForm, StringComparison.OrdinalIgnoreCase))
                        {
                            var req = data.PupilGroupRequest?.FirstOrDefault();
                            if (req == null)
                            {
                                SetRedicted();
                                return RedirectToAction(nameof(OrphanagePupilGroupListEdit), new {listId = e.Id});
                            }

                            var sb = new StringBuilder();
                            if (model.Pupils.Count < 1)
                            {
                                sb.Append("<li>Невозможно сформировать список без воспитанников</li>");
                            }

                            CheckPupilsAge(model, req, sb);

                            /* в соответствии с #135194 временно отключено
                            if (model.Pupils.Count > req.PupilsCount)
                            {
                                sb.Append("<li>Кол-во отдыхающих превышает максимальное число, указанное в потребности</li>");
                            }

                            if (model.Collaborators.Count > req.CollaboratorsCount)
                            {
                                sb.Append("<li>Кол-во сопровождающих превышает максимальное число, указанное в потребности</li>");
                            }
                            */

                            //Список не может быть направлен на согласование в связи с внесением в список воспитанников, нарушавших Правила
                            var rules = new long?[] {7, 8};
                            if (rules.Contains(data.PupilGroupRequest.Select(ss => ss.PupilGroup.FormOfRestId)
                                .FirstOrDefault()) && data.GroupPupils.Any(ss => ss.Pupil.Foul))
                            {
                                sb.Append("<li>Список не может быть направлен на согласование в связи с внесением в список воспитанников, нарушавших Правила</li>");
                            }

                            var moscowRegionId =
                                Convert.ToInt64(ConfigurationManager.AppSettings["MoscowRegionId"] ?? "23");
                            //Список не может быть направлен на согласование в связи с внесением в список воспитанников, нарушавших Правила с ограничением региона
                            if (data.PupilGroupRequest.Any(sx => sx.Tour?.Hotels?.PlaceOfRestId != moscowRegionId) &&
                                data.GroupPupils.Any(ss =>
                                    ss.Pupil.FoulRegionRestriction && (!ss.Pupil.FoulRegionRestrictionTo.HasValue ||
                                                                       ss.Pupil.FoulRegionRestrictionTo >
                                                                       data.PupilGroupRequest
                                                                           .Select(sx => sx.Tour.DateIncome)
                                                                           .FirstOrDefault())))
                            {
                                sb.Append(
                                    "<li>Список не может быть направлен на согласование в связи с внесением в список воспитанников, нарушавших Правила с ограничением региона</li>");
                            }

                            if (!data.RulesAgreement || !data.PupilsRulesAgreement)
                            {
                                sb.Append(
                                    "<li>Для формирования списка необходимо подтвердить согласие и ознакомление с правилами</li>");
                            }

                            var err = sb.ToString();
                            if (!string.IsNullOrWhiteSpace(err))
                            {
                                tran.Complete();
                                TempData[errorMessage] = $"<ul>{err}</ul>";
                                return RedirectToAction(nameof(OrphanagePupilGroupListEdit), new {listId = e.Id});
                            }
                        }


                        data.HistoryLink = ApiController.WriteHistory(data.HistoryLink,
                            "Изменение статуса Списка (группы отправки)",
                            $"Изменение статуса Списка (группы отправки) с \"{data.NullSafe(c => c.State.Name)}\" на \"{action.NullSafe(a => a.ToState.Name)}\"",
                            action.ToStateId, data.StateId);
                        data.HistoryLinkId = data.HistoryLink?.Id;

                        data.StateId = action.ToStateId;

                        WriteHistory(e.Id, "изменил список (группу отправку) учреждения социальной защиты");

                        UnitOfWork.SaveChanges();
                    }

                    tran.Complete();
                }
            }

            return RedirectToAction(nameof(OrphanagePupilGroupListEdit), new {listId = model.Data.Id});
        }

        /// <summary>
        ///     Проверка соответствия возраста детей форме отдыха
        /// </summary>
        public StringBuilder CheckPupilsAge(OrphanagePupilGroupListModel model, RequestForPeriodOfRest req, StringBuilder sb)
        {
            var pupilIds = model.Pupils.Values.Select(x => x.Data.PupilId).ToList();
            var childs = UnitOfWork.GetSet<Child>().Where(x => x.Pupils.Any(a => pupilIds.Contains(a.Id)));

            var dateIncome = req?.Tour?.DateIncome;
            var min = req.PupilGroup?.FormOfRest?.AgeFrom;
            var max = req.PupilGroup?.FormOfRest?.AgeTo;

            foreach (var child in childs)
            {
                if (child.DateOfBirth != null && dateIncome != null)
                {
                    var age = child.GetAgeInYears(dateIncome);
                    if (!(age >= min && age <= max))
                    {
                        sb.Append($"<li> Возраст воспитанника {child.LastName} {child.FirstName} не соответствует форме отдыха. </li>");
                    }
                }
            }

            return sb;
        }

        /// <summary>
        ///     всплывающий список воспитанников для добавления в список
        /// </summary>
        [Route("Orphanage/PupilGroupLists/PupilsChoose")]
        [HttpPost]
        public ActionResult OrphanagePupilGroupListsPupilsChoose(long orphanageId)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);
            if (!Security.HasRight(AccessRightEnum.Orphans.PupilGroupList, orphanageId))
            {
                return RedirectToAvalibleAction();
            }

            /*var pupils = UnitOfWork.GetSet<Pupil>().Where(ss =>
                ss.OrphanageAddress.OrganisationId == orphanageId && ss.DateOut == null &&
                (ss.Child.EntityId == null || ss.Child.EntityId == ss.Child.AddressId)).ToList();*/

            var filter = new OrphanagePupilsFilterModel()
            {
                ActionName = nameof(OrphanagePupilGroupListsPupilsChooseSearch),
                IsFilled = true,
                OrphanageId = orphanageId
            };

            filter.Results = ApiController.GetPupils(filter);

            return PartialView("Partials/GroupPupilsToAdd", filter);
        }

        /// <summary>
        ///     Поиск во всплывающем списке воспитанников для добавления в список
        /// </summary>
        [Route("Orphanage/PupilGroupLists/PupilsChooseSearch")]
        [HttpPost]
        public ActionResult OrphanagePupilGroupListsPupilsChooseSearch(OrphanagePupilsFilterModel filter)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);
            if (!Security.HasRight(AccessRightEnum.Orphans.PupilGroupList, filter.OrphanageId))
            {
                return RedirectToAvalibleAction();
            }

            filter.IsFilled = true;
            filter.Results = ApiController.GetPupils(filter);

            return PartialView("Partials/GroupPupilsToAddForm", filter);
        }

        /// <summary>
        ///     Поиск во всплывающем списке воспитанников для добавления в список
        /// </summary>
        [Route("Orphanage/PupilGroupLists/PupilAdd")]
        [HttpPost]
        public ActionResult OrphanagePupilGroupListsPupilAdd(long? pupilId, int dictKey)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);

            var p = UnitOfWork.GetById<Pupil>(pupilId);

            if (!Security.HasRight(AccessRightEnum.Orphans.PupilGroupList, p.OrphanageAddress.OrganisationId))
            {
                return RedirectToAvalibleAction();
            }

            var result = new OrphanagePupilGroupListMemberModel(new PupilGroupListMember
            {
                Pupil = p,
                PupilId = p.Id,
                OrganisatonAddresId = p.OrphanageAddressId
            })
            {
                DrugDoses = p.Drugs?.Where(ss => !ss.IsDeleted).ToDictionary(ss => Guid.NewGuid().ToString(), ss =>
                    new PupilGroupListMemberDrugDose
                    {
                        Dose = ss,
                        DoseId = ss.Id,
                        DrugQuantity = ss.Dose
                    }) ?? new Dictionary<string, PupilGroupListMemberDrugDose>(0)
            };

            ViewData.TemplateInfo.HtmlFieldPrefix = $"Pupils[{dictKey}]";

            ViewBag.PossibleTransferAdresses = UnitOfWork.GetSet<OrphanageAddress>().Where(oa => oa.OrganisationId == p.OrphanageAddress.OrganisationId).ToList();
            return PartialView("EditorTemplates/PupilGroupListPupil", result);
        }

        /// <summary>
        ///     всплывающий список сотрудников для добавления в список
        /// </summary>
        [Route("Orphanage/PupilGroupLists/CollaboratorsChoose")]
        [HttpPost]
        public ActionResult OrphanagePupilGroupListCollaboratorsChoose(long orphanageId)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);
            if (!Security.HasRight(AccessRightEnum.Orphans.PupilGroupList, orphanageId))
            {
                return RedirectToAvalibleAction();
            }

            var collaborators = UnitOfWork.GetSet<OrganisatorCollaborator>().Where(ss =>
                ss.OrganisatonId == orphanageId &&
                (ss.Applicant.EntityId == null || ss.Applicant.EntityId == ss.Applicant.AddressId)).ToList();

            var filter = new OrphanageCollaboratorsFilterModel(collaborators);
            filter.Collaborators = ApiController.GetOrphanageCollaborators(filter);
            return PartialView("Partials/GroupCollaboratorsToAdd", filter);
        }

        /// <summary>
        ///     Поиск во всплывающем списке сотрудников для добавления в список
        /// </summary>
        [Route("Orphanage/PupilGroupLists/CollaboratorsChooseSearch")]
        [HttpPost]
        public ActionResult OrphanagePupilGroupListCollaboratorsChooseSearch(OrphanageCollaboratorsFilterModel filter)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);
            if (!Security.HasRight(AccessRightEnum.Orphans.PupilGroupList, filter.OrphanageId))
            {
                return RedirectToAvalibleAction();
            }

            filter.Collaborators = ApiController.GetOrphanageCollaborators(filter);

            return PartialView("Partials/GroupCollaboratorsToAddForm", filter);
        }

        /// <summary>
        ///     Поиск во всплывающем списке сотрудников для добавления в список
        /// </summary>
        [Route("Orphanage/PupilGroupLists/CollaboratorAdd")]
        [HttpPost]
        public ActionResult OrphanagePupilGroupListsCollaboratorAdd(long? сollaboratorId, int dictKey)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);

            var p = UnitOfWork.GetById<OrganisatorCollaborator>(сollaboratorId);

            if (!Security.HasRight(AccessRightEnum.Orphans.PupilGroupList, p.OrganisatonId))
            {
                return RedirectToAvalibleAction();
            }

            var result = new PupilGroupListCollaborator
            {
                OrganisatonCollaborator = p,
                OrganisatonCollaboratorId = p.Id,
                OrganisatonAddresId = p.OrganisatonAddressId
            };

            ViewData.TemplateInfo.HtmlFieldPrefix = $"Collaborators[{dictKey}]";

            ViewBag.PossibleTransferAdresses = UnitOfWork.GetSet<OrphanageAddress>().Where(oa => oa.OrganisationId == p.OrganisatonAddress.OrganisationId).ToList();
            return PartialView("EditorTemplates/PupilGroupListCollaborator", result);
        }

        /// <summary>
        ///     Список для ГИБДД
        /// </summary>
        [Route("Orphanage/PupilGroupLists/GibddList/{listId}")]
        [HttpGet]
        public ActionResult OrphanagePupilGroupListsGibddList(long? listId)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);
            var list = UnitOfWork.GetById<ListOfChilds>(listId);

            if (!Security.HasRight(AccessRightEnum.Orphans.PupilGroupList, list?.LimitOnOrganization?.OrganizationId))
            {
                return RedirectToAvalibleAction();
            }

            var data = DocumentGeneration.WordProcessor.OrphanagePupilGroupListsGibddList(list);
            return File(data.FileBody, data.MimeType, data.FileName);

        }

        /// <summary>
        ///     Установка статусной модели
        /// </summary>
        /// <param name="model"></param>
        private void PupilGroupListSetState(OrphanagePupilGroupListModel model)
        {
            if (model.Data.Id < 1)
            {
                var state = UnitOfWork.GetById<StateMachineState>(StateMachineStateEnum.PupilGroupList.Formation);

                model.State = new ViewModelState
                {
                    Actions = new List<StateMachineAction>(),
                    NeedSaveButton = true,
                    CanReturn = false,
                    NeedRemoveButton = false,
                    State = state,
                    FormSelector = "#listForm",
                    ActionSelector = "#StateMachineActionString"
                };
            }
            else
            {
                var isEditable = model.Data.StateId == StateMachineStateEnum.PupilGroupList.Formation;

                var actions = ApiStateController.GetActions(model.Data.State, StateMachineEnum.LimitListState);

                model.State = new ViewModelState
                {
                    State = model.Data.State,
                    Actions = actions,
                    NeedSaveButton = isEditable,
                    //NeedRemoveButton = isEditable && Security.HasRight(AccessRightEnum.Orphans.PupilGroupListDelete, model.Data.PupilGroupRequest?.PupilGroup?.OrganizationId),
                    CanReturn = false,
                    FormSelector = "#listForm",
                    ActionSelector = "#StateMachineActionString"
                };

                if (model.Data.StateId == StateMachineStateEnum.PupilGroupList.Approved)
                {
                    model.State.PostNoStatusActions = model.State.PostNoStatusActions ?? new List<NoStatusAction>();
                    model.State.PostNoStatusActions.Add(new NoStatusAction {
                        Name = "Список для ГИБДД",
                        ButtonClass = "btn btn-default",
                        Action = nameof(OrphanagePupilGroupListsGibddList),
                        Controller = "Orphan",
                        ActionParameters = new { listId = model.Data.Id }
                    });
                }

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

        private long LimitOfOrganisationGetOrCreate(RequestForPeriodOfRest req)
        {
            if (req == null)
            {
                // ReSharper disable once NotResolvedInText
                throw new ArgumentNullException("RequestForPeriodOfRest");
            }

            var globalLimit = UnitOfWork.GetSet<LimitOnVedomstvo>().Where(ss =>
                ss.YearOfRestId == req.PupilGroup.YearOfRestId &&
                ss.StateId > 0 &&
                ss.OrganizationId == req.PupilGroup.Organization.ParentId
            ).Select(ss => (long?) ss.Id).FirstOrDefault();

            if (!globalLimit.HasValue)
            {
                // ReSharper disable once NotResolvedInText
                throw new ArgumentNullException("LimitOnVedomstvo");
            }

            var limit = UnitOfWork.GetSet<LimitOnOrganization>().FirstOrDefault(ss =>
                ss.StateId > 0 &&
                ss.OrganizationId == req.PupilGroup.OrganizationId &&
                ss.TimeOfRestId == req.TimeOfRestId &&
                ss.PlaceOfRestId == req.PlaceOfRestId &&
                ss.TourId == req.TourId &&
                ss.LimitOnVedomstvoId == globalLimit.Value);

            if (limit == null)
            {
                limit = new LimitOnOrganization
                {
                    Volume = req.PupilGroup?.PupilsCount ?? 0,
                    LimitOnVedomstvoId = globalLimit.Value,
                    OrganizationId = req.PupilGroup?.OrganizationId,
                    StateId = StateMachineStateEnum.Limit.Organization.Confirmed,
                    TimeOfRestId = req.TimeOfRestId,
                    PlaceOfRestId = req.PlaceOfRestId,
                    TourId = req.TourId,
                    TypeOfLimitListId = (long) TypeOfLimitListEnum.Orphan
                };

                limit.HistoryLink = ApiController.WriteHistory(limit.HistoryLink,
                    "Создание квоты на подведомственные учреждения социальной защиты из функционала создание списка (группы отправки) учреждения социальной защиты",
                    string.Empty);
                limit.HistoryLinkId = limit.HistoryLink.Id;

                UnitOfWork.AddEntity(limit);
                UnitOfWork.SaveChanges();
            }

            return limit.Id;
        }
    }
}
