using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RestChild.Comon;
using RestChild.Comon.Dto.Booking;
using RestChild.Comon.Enumeration;
using RestChild.Extensions.Extensions;
using RestChild.Extensions.Filter;
using RestChild.DAL.RepositoryExtensions;
using RestChild.Domain;
using RestChild.Web.Extensions;
using RestChild.Web.Properties;
using RestChild.Booking.Logic.Extensions;

namespace RestChild.Web.Controllers.WebApi
{
    [Authorize]
    public class WebToursController : WebGenericRestController<Tour>
    {
        public StateController ApiStateController { get; set; }
        public WebCalculationController ApiCalculationController { get; set; }

        public override void SetUnitOfWorkInRefClass(IUnitOfWork unitOfWork)
        {
            base.SetUnitOfWorkInRefClass(unitOfWork);
            ApiStateController.SetUnitOfWorkInRefClass(unitOfWork);
            ApiCalculationController.SetUnitOfWorkInRefClass(unitOfWork);
        }

        [HttpPost]
        public void ChangePaidChild(long id, long childId, bool value)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);
            if (!Security.HasRight(AccessRightEnum.ToursManage))
            {
                return;
            }

            var child = UnitOfWork.GetById<Child>(childId);
            if (child == null)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, "Ребенок не найден"));
            }

            child.Payed = value;
            if (child.ChildList != null && child.ChildList.ForIndex == false)
            {
                child.ChildList.ForIndex = true;
            }

            //var calculations = UnitOfWork.GetSet<Calculation>().Where(c => c.ChildId == childId).ToList();
            //foreach (var calculation in calculations)
            //{
            //	ApiCalculationController.SetCalculationPaid(calculation, value);
            //}

            var tour = UnitOfWork.GetById<Tour>(id);
            tour.HistoryLink = this.WriteHistory(tour.HistoryLink, "Изменение статуса оплаты ребёнка",
               $"{(value ? "Установка" : "Снятие")} отметки об оплате для ребёнка {child.LastName} {child.FirstName} {child.MiddleName}");
            tour.HistoryLinkId = tour.HistoryLink?.Id;
            UnitOfWork.SaveChanges();
        }

        [HttpGet]
        public void ChangePaidChildList(long id, bool paid)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);
            if (!Security.HasRight(AccessRightEnum.ToursManage))
            {
                return;
            }

            var list = UnitOfWork.GetById<ListOfChilds>(id);
            if (list == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            if (list.Childs != null && list.Childs.Any())
            {
                foreach (var child in list.Childs)
                {
                    child.Payed = paid;
                    //var calculations = UnitOfWork.GetSet<Calculation>().Where(c => c.ChildId == child.Id).ToList();
                    //foreach (var calculation in calculations)
                    //{
                    //	ApiCalculationController.SetCalculationPaid(calculation, paid);
                    //}
                }
            }

            if (list.Attendants != null && list.Attendants.Any())
            {
                foreach (var attendant in list.Attendants)
                {
                    attendant.Payed = paid;
                    //var calculations = UnitOfWork.GetSet<Calculation>().Where(c => c.ApplicantId == attendant.Id).ToList();
                    //foreach (var calculation in calculations)
                    //{
                    //	ApiCalculationController.SetCalculationPaid(calculation, paid);
                    //}
                }
            }

            if (list.ForIndex == false)
            {
                list.ForIndex = true;
            }

            UnitOfWork.SaveChanges();
        }

        [HttpPost]
        public void ChangePaidAttendant(long id, long attendantId, bool value)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);
            if (!Security.HasRight(AccessRightEnum.ToursManage))
            {
                return;
            }

            var attendant = UnitOfWork.GetById<Applicant>(attendantId);
            if (attendant == null)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound,
                   "Сопровождающий не найден"));
            }

            attendant.Payed = value;
            if (attendant.ChildList != null && attendant.ChildList.ForIndex == false)
            {
                attendant.ChildList.ForIndex = true;
            }

            //var calculations = UnitOfWork.GetSet<Calculation>().Where(c => c.ApplicantId == attendant.Id).ToList();
            //foreach (var calculation in calculations)
            //{
            //	ApiCalculationController.SetCalculationPaid(calculation, value);
            //}
            UnitOfWork.SaveChanges();
            var tour = UnitOfWork.GetById<Tour>(id);
            tour.HistoryLink = this.WriteHistory(tour.HistoryLink, "Изменение статуса оплаты педагога/тренира",
               $"{(value ? "Установка" : "Снятие")} отметки об оплате для педагога/тренира {attendant.LastName} {attendant.FirstName} {attendant.MiddleName}");
            tour.HistoryLinkId = tour.HistoryLink?.Id;
            UnitOfWork.SaveChanges();
        }

        /// <summary>
        ///     Поиск блока мест
        /// </summary>
        /// <param name="filter">Фильтр</param>
        public CommonPagedList<Tour> Get(ToursFilterModel filter)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);
            if (!filter.ContractFiltered && !Security.HasRight(AccessRightEnum.ToursView) &&
                !Security.HasRight(AccessRightEnum.Tour.WorkWithServices))
            {
                return new CommonPagedList<Tour>();
            }

            int pageSize = filter.PageSize ?? Settings.Default.TablePageSize;
            var pageNumber = filter.PageNumber;
            int startRecord = (pageNumber - 1) * pageSize;
            IQueryable<Tour> query =
               UnitOfWork.GetSet<Tour>().Where(t =>
                  t.StateId != null && t.StateId != StateMachineStateEnum.Deleted && !t.TypeOfRest.Commercial);

            if (!string.IsNullOrEmpty(filter.Name))
            {
                query = query.Where(t => t.Hotels.Name.ToLower().Contains(filter.Name.ToLower()));
            }

            if (filter.YearOfRestId.HasValue && filter.YearOfRestId != 0)
            {
                if (filter.MoreThenSelectedYear)
                {
                    var year = UnitOfWork.GetById<YearOfRest>(filter.YearOfRestId);
                    query = year != null
                       ? query.Where(q => q.YearOfRest.Year >= year.Year)
                       : query.Where(t => t.YearOfRestId == filter.YearOfRestId);
                }
                else
                {
                    query = query.Where(t => t.YearOfRestId == filter.YearOfRestId);
                }
            }

            if (filter.HotelId.HasValue && filter.HotelId != 0)
            {
                query = query.Where(t => t.HotelsId == filter.HotelId);
            }

            if (filter.PlaceOfRestId.HasValue && filter.PlaceOfRestId != 0)
            {
                query = query.Where(t => t.Hotels.PlaceOfRestId == filter.PlaceOfRestId);
            }

            if (filter.TimeOfRestId.HasValue && filter.TimeOfRestId != 0)
            {
                query = query.Where(t => t.TimeOfRestId == filter.TimeOfRestId);
            }

            if (filter.StateId.HasValue && filter.StateId != 0)
            {
                query = query.Where(t => t.StateId == filter.StateId);
            }

            if (filter.TypeOfRestId.HasValue && filter.TypeOfRestId != 0)
            {
                query = query.Where(t => t.TypeOfRestId == filter.TypeOfRestId);
            }

            if (filter.ContractId.HasValue && filter.ContractId != 0)
            {
                query = query.Where(t => t.ContractId == filter.ContractId);
            }

            if (filter.TypeOfServiceId.HasValue && filter.TypeOfServiceId > 0)
            {
                if (filter.TypeOfServiceInclude)
                    query = query.Where(t => t.Services.Any(s => s.IsActive && s.TypeOfServiceId == filter.TypeOfServiceId));
                else
                    query = query.Where(t =>
                       !t.Services.Any(s => s.IsActive && s.TypeOfServiceId == filter.TypeOfServiceId));
            }

            if (filter.RestrictionGroupId.HasValue && filter.RestrictionGroupId.Value > 0)
            {
                query = query.Where(t => t.RestrictionGroupId == (long)filter.RestrictionGroupId);
            }

            int totalCount = query.Count();
            List<Tour> entity =
               query.OrderByDescending(t => t.DateIncome)
                  .ThenBy(t => t.Hotels.Name)
                  .ThenBy(t => t.Id)
                  .Skip(startRecord)
                  .Take(pageSize)
                  .ToList();

            //признак того, что нужно кол-во детей
            if (filter.ContractId.HasValue)
            {
                foreach (var tour in entity)
                {
                    tour.ChildrenCount = tour.GetRestManCount();
                }
            }

            return new CommonPagedList<Tour>(entity, pageNumber, pageSize, totalCount);
        }

        public override Tour Get(long id)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);
            if (!Security.HasRight(AccessRightEnum.ToursView) && !Security.HasRight(AccessRightEnum.Tour.WorkWithServices))
            {
                return null;
            }

            var tour = base.Get(id);
            if (tour != null)
            {
                tour.Childs = UnitOfWork.GetSet<Child>().Where(c => c.Request.TourId == id).ToList();
            }
            return tour;
        }

        public virtual Tour Post(Tour entity, IList<AddonServices> services)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);
            if (!Security.HasRight(AccessRightEnum.ToursManage))
            {
                return null;
            }

            if (entity == null)
            {
                return null;
            }

            entity.HistoryLink = UnitOfWork.AddEntity(new HistoryLink
            {
                Historys =
                  new List<History>
                  {
                  UnitOfWork.AddEntity(new History
                  {
                     AccountId = Security.GetCurrentAccountId(),
                     EventCode = "Создание размещения",
                     DateChange = DateTime.Now,
                     Commentary = string.Empty
                  })
                  }
            });
            entity = base.Post(entity);

            return entity;
        }

        public virtual Tour Put(long id, Tour entity, IList<AddonServices> services)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);
            if (!Security.HasRight(AccessRightEnum.ToursManage))
            {
                return null;
            }

            if (entity == null)
            {
                return null;
            }

            var source = UnitOfWork.GetById<Tour>(id);
            var diff = source.Compare(entity, UnitOfWork);
            UnitOfWork.DetachAllEntitys();

            var volumes = entity.Volumes ?? new List<TourVolume>();
            var roomRates = (entity.RoomRates ?? new List<RoomRates>()).ToList();
            entity.RoomRates = null;
            entity.Volumes = null;
            entity.Services = null;
            using (var transaction = UnitOfWork.GetTransactionScope())
            {
                entity.MinPrepaymentAmount = UnitOfWork.GetSet<Tour>().Where(t => t.Id == id)
                   .Select(t => t.MinPrepaymentAmount).FirstOrDefault();
                base.Put(id, entity);
                var persisted = UnitOfWork.GetById<Tour>(id);
                if (persisted != null)
                {
                    UnitOfWork.MergeCollection(
                       volumes,
                       persisted.Volumes,
                       (s, d) =>
                       {
                           d.CountPlace = s.CountPlace ?? 0;
                           d.CountRooms = s.CountRooms ?? 0;
                           d.CountBusyPlace = s.CountBusyPlace ?? 0;
                           d.CountBusyRooms = s.CountBusyRooms ?? 0;
                           d.TypeOfRoomsId = s.TypeOfRoomsId;
                           d.HotelsId = s.HotelsId;
                       });

                    var persistedLists = UnitOfWork.GetSet<ListOfChilds>().Where(l => l.TourId == entity.Id).ToList();
                    if (entity.ChildLists != null && entity.ChildLists.Any())
                    {
                        var listsToDelete = persistedLists.Where(l => entity.ChildLists.All(cl => cl.Id != l.Id)).ToList();
                        var listsToInsertIds =
                           entity.ChildLists.Where(cl => persistedLists.All(l => cl.Id != l.Id)).Select(cl => cl.Id).ToList();
                        var listsToinsert = UnitOfWork.GetSet<ListOfChilds>().Where(l => listsToInsertIds.Contains(l.Id))
                           .ToList();
                        foreach (var list in listsToinsert)
                        {
                            list.TourId = id;
                        }

                        foreach (var list in listsToDelete)
                        {
                            list.TourId = null;
                        }
                    }
                    else
                    {
                        foreach (var list in persistedLists)
                        {
                            list.TourId = null;
                        }
                    }

                    // добавляем
                    foreach (var roomRate in roomRates.Where(rr => rr.Id == 0).ToList())
                    {
                        roomRate.TourId = entity.Id;
                        roomRate.HotelId = entity.HotelsId;
                        roomRate.YearOfRestId = entity.YearOfRestId;

                        if (roomRate.AccommodationId <= 0)
                        {
                            roomRate.AccommodationId = null;
                        }

                        if (roomRate.DiningOptionsId <= 0)
                        {
                            roomRate.DiningOptionsId = null;
                        }

                        if (roomRate.TypeOfRoomsId <= 0)
                        {
                            roomRate.TypeOfRoomsId = null;
                        }

                        UnitOfWork.AddEntity(roomRate);
                    }

                    var roomRatesIds = roomRates.Select(rr => rr.Id).ToList();

                    var dictRoomRatesPrice = roomRates.Where(rr => rr.Id > 0).ToDictionary(rr => rr.Id, rr => rr);

                    // исправляем
                    foreach (var roomRate in persisted.RoomRates.Where(rr => roomRatesIds.Contains(rr.Id)).ToList())
                    {
                        if (dictRoomRatesPrice.ContainsKey(roomRate.Id))
                        {
                            roomRate.Price = dictRoomRatesPrice[roomRate.Id].Price;
                            UnitOfWork.Update(roomRate);
                        }
                    }

                    // удяляем
                    foreach (var roomRate in persisted.RoomRates.Where(rr => !roomRatesIds.Contains(rr.Id)).ToList())
                    {
                        UnitOfWork.Delete(roomRate);
                    }

                    persisted.HistoryLink = persisted.HistoryLink ?? UnitOfWork.AddEntity(new HistoryLink());
                    persisted.HistoryLink.Historys = persisted.HistoryLink.Historys ?? new List<History>();

                    persisted.HistoryLink.Historys.Add(UnitOfWork.AddEntity(new History
                    {
                        AccountId = Security.GetCurrentAccountId(),
                        EventCode = "Обновление размещения",
                        DateChange = DateTime.Now,
                        Commentary = diff
                    }));

                    UnitOfWork.SaveChanges();
                    transaction.Complete();
                    return persisted;
                }

                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not found"));
            }
        }

        /// <summary>
        /// получение списка доп услуг.
        /// </summary>
        internal IList<AddonServices> GetAddonServices(long tourId)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);
            var itemTour = UnitOfWork.GetSet<Tour>().FirstOrDefault(t => t.Id == tourId);
            var hotelsId = itemTour?.HotelsId;
            var stateId = itemTour?.StateId;

            var hIds = itemTour?.RoomRates.Where(r => r.HotelId.HasValue).Select(r => r.HotelId).ToList() ??
                       new List<long?>();
            if (hotelsId.HasValue)
            {
                hIds.Add(hotelsId);
            }

            var hasIds = hIds.Any();

            var res =
               UnitOfWork.GetSet<AddonServices>()
                  .Include(a => a.Parent)
                  .Where(
                     a =>
                        (a.Parent == null || (a.Parent.StateId == StateMachineStateEnum.AddonService.Formed &&
                                              a.Parent.IsActive)) &&
                        a.StateId == StateMachineStateEnum.AddonService.Formed && a.IsActive &&
                        (a.TourId == tourId || (a.Hotels.Any(h => hIds.Contains(h.Id)) && hasIds) ||
                         (!a.TourId.HasValue && !a.Hotels.Any() && !a.ParentId.HasValue)))
                  .ToList();

            var idsExclude = res.Where(r => r.ParentId.HasValue).Select(r => r.ParentId).ToList();
            res.RemoveAll(a => idsExclude.Contains(a.Id));
            res =
               res.Select(a => new AddonServices(a)
               {
                   Parent =
                     a.Parent != null
                        ? new AddonServices(a.Parent)
                        {
                            Hotels = a.Parent.Hotels != null && a.Parent.Hotels.Any()
                              ? a.Parent.Hotels.Select(h => new Hotels(h)).ToList()
                              : null
                        }
                        : null,
                   Hotels = a.Hotels != null && a.Hotels.Any() ? a.Hotels.Select(h => new Hotels(h)).ToList() : null
               }).ToList();

            if (stateId != StateMachineStateEnum.Tour.Formation)
            {
                res.RemoveAll(a => !a.TourId.HasValue);
            }

            foreach (var item in res)
            {
                if (item.Parent != null)
                {
                    item.Name = item.Parent.Name;
                    item.Description = item.Parent.Description;
                    item.Hotels = item.Parent.Hotels;
                    item.Requared = item.Parent.Requared;
                    item.OnlyWithRequest = item.Parent.OnlyWithRequest;
                    item.ByDefault = item.Parent.ByDefault;
                }

                item.IsActive = item.TourId.HasValue || (item.Requared ?? false);
            }

            return res.OrderBy(s => s.Name).ToList();
        }

        internal ICollection<ListOfChilds> GetChildLists(long tourId)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);
            var res =
               UnitOfWork.GetSet<ListOfChilds>()
                  .Where(l => l.TourId == tourId)
                  .Union(UnitOfWork.GetSet<ListOfChilds>().Where(l => l.TourId == tourId))
                  .ToList();

            return res;
        }

        [HttpGet]
        public IEnumerable<ListOfChilds> GetAvailableChildLists(long yearId, long? vedomstvoId = null,
           int? childsCountFrom = null, int? childsCountTo = null)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);
            var query =
               UnitOfWork.GetSet<ListOfChilds>()
                  .Where(e =>
                     e.StateId == StateMachineStateEnum.Limit.List.Formed
                     && e.LimitOnOrganizationId.HasValue
                     && e.LimitOnOrganization.StateId != StateMachineStateEnum.Deleted
                     && e.LimitOnOrganization.LimitOnVedomstvoId.HasValue
                     && e.LimitOnOrganization.LimitOnVedomstvo.StateId != StateMachineStateEnum.Deleted
                     && e.LimitOnOrganization.LimitOnVedomstvo.YearOfRestId == yearId
                     && !e.TourId.HasValue
                     && e.LimitOnOrganization.StateId == StateMachineStateEnum.Limit.Organization.Confirmed
                  );

            if (vedomstvoId.HasValue)
            {
                query =
                   query.Where(l => l.LimitOnOrganization.LimitOnVedomstvo.OrganizationId == vedomstvoId.Value);
            }

            if (childsCountFrom.HasValue)
            {
                query =
                   query.Where(l => l.CountChild >= childsCountFrom.Value);
            }

            if (childsCountTo.HasValue)
            {
                query =
                   query.Where(l => l.CountChild <= childsCountTo.Value);
            }

            query =
               query.Include(q => q.LimitOnOrganization)
                  .Include(q => q.LimitOnOrganization.Organization)
                  .Include(q => q.LimitOnOrganization.LimitOnVedomstvo)
                  .Include(q => q.LimitOnOrganization.LimitOnVedomstvo.Organization)
                  .Include(q => q.State);

            var res = query.OrderBy(l => l.Name).ToList().Select(i => new ListOfChilds
            {
                Id = i.Id,
                Name = i.Name,
                CountChild = i.CountChild,
                CountAttendants = i.CountAttendants,
                State = i.State,
                StateId = i.StateId,
                ListOfChildsCategory = i.ListOfChildsCategory,
                ListOfChildsCategoryId = i.ListOfChildsCategoryId,
                LimitOnOrganization = new LimitOnOrganization
                {
                    Organization =
                     new Organization
                     { Id = i.LimitOnOrganization.Organization.Id, Name = i.LimitOnOrganization.Organization.Name },
                    LimitOnVedomstvo =
                     new LimitOnVedomstvo
                     {
                         Id = i.LimitOnOrganization.LimitOnVedomstvo.Id,
                         Organization =
                           new Organization
                           {
                               Id = i.LimitOnOrganization.LimitOnVedomstvo.Organization.Id,
                               Name = i.LimitOnOrganization.LimitOnVedomstvo.Organization.Name
                           }
                     }
                }
            }).ToList();
            return res;
        }

        internal List<string> ChangeStatus(long tourId, string stateMachineActionString, long? signInfoId = null)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);
            var result = new List<string> { "Ошибка изменения статуса" };
            var tour = UnitOfWork.GetById<Tour>(tourId);
            if (tour == null)
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not found"));
            }

            using (var transaction = UnitOfWork.GetTransactionScope())
            {
                tour.SignInfoId = signInfoId;
                if (tour.TypeOfRestId == (long)TypeOfRestEnum.RestWithParents ||
                    tour.TypeOfRestId == (long)TypeOfRestEnum.ChildRest ||
                    tour.TypeOfRestId == (long)TypeOfRestEnum.ChildRestFederalCamps ||
                    tour.TypeOfRestId == (long)TypeOfRestEnum.ChildRestCamps ||
                    tour.TypeOfRestId == (long)TypeOfRestEnum.SpecializedСamp)
                {
                    tour.EkisNeedSend = true;
                }

                if (stateMachineActionString == "Delete")
                {
                    tour.StateId = StateMachineStateEnum.Deleted;
                    var childLists = UnitOfWork.GetSet<ListOfChilds>().Where(l => l.TourId == tour.Id);
                    foreach (var list in childLists)
                    {
                        list.TourId = null;
                    }

                    tour.HistoryLink = this.WriteHistory(tour.HistoryLink, "Удаление размещения",
                       string.Empty, StateMachineStateEnum.Deleted,
                       tour.StateId, signInfoId);
                    tour.HistoryLinkId = tour.HistoryLink?.Id;
                }
                else
                {
                    var errors = GetErrorsOfChageStatus(tourId, stateMachineActionString);
                    if (errors.Any())
                    {
                        return errors.ToList();
                    }

                    var stateName = tour.NullSafe(t => t.State.Name);
                    var action = ApiStateController.GetAction(stateMachineActionString);
                    if (action?.ToStateId != null)
                    {
                        tour.ChildLists = GetChildLists(tour.Id);
                        tour.HistoryLink = this.WriteHistory(tour.HistoryLink, "Изменение статуса размещения",
                           $"Изменение статуса размещения с {stateName} на {action.NullSafe(a => a.ToState.Name)}",
                           action.ToStateId,
                           tour.StateId, signInfoId);
                        tour.HistoryLinkId = tour.HistoryLink?.Id;

                        tour.StateId = action.ToStateId;
                        result = new List<string>();
                    }
                }

                UnitOfWork.SaveChanges();
                transaction.Complete();
            }

            if (tour.TypeOfRest != null &&
                (tour.TypeOfRestId == (long)TypeOfRestEnum.RestWithParents ||
                 tour.TypeOfRestId == (long)TypeOfRestEnum.ChildRest ||
                 tour.TypeOfRestId == (long)TypeOfRestEnum.ChildRestCamps ||
                 tour.TypeOfRestId == (long)TypeOfRestEnum.TentChildrenCamp ||
                 tour.TypeOfRestId == (long)TypeOfRestEnum.TentChildrenCampOrphan ||
                 tour.TypeOfRestId == (long)TypeOfRestEnum.ChildRestFederalCamps ||
                 tour.TypeOfRestId == (long)TypeOfRestEnum.ChildRestOrphanCamps ||
                 tour.TypeOfRestId == (long)TypeOfRestEnum.YouthRestCamps ||
                 tour.TypeOfRest?.ParentId == (long)TypeOfRestEnum.RestWithParents ||
                 tour.TypeOfRest?.Parent?.ParentId == (long)TypeOfRestEnum.RestWithParents ||
                 tour.TypeOfRest?.Parent?.Parent?.ParentId == (long)TypeOfRestEnum.RestWithParents ||
                 tour.TypeOfRest?.ParentId == (long)TypeOfRestEnum.ChildRest ||
                 tour.TypeOfRest?.ParentId == (long)TypeOfRestEnum.TentChildrenCamp ||
                 tour.TypeOfRest?.ParentId == (long)TypeOfRestEnum.TentChildrenCampOrphan ||
                 tour.TypeOfRest?.ParentId == (long)TypeOfRestEnum.ChildRestCamps ||
                 tour.TypeOfRest?.ParentId == (long)TypeOfRestEnum.ChildRestOrphanCamps ||
                 tour.TypeOfRest?.ParentId == (long)TypeOfRestEnum.ChildRestFederalCamps ||
                 tour.TypeOfRest?.ParentId == (long)TypeOfRestEnum.YouthRestOrphanCamps ||
                 tour.TypeOfRest?.ParentId == (long)TypeOfRestEnum.YouthRestCamps
                ))
            {
                var client =
                   Booking.Logic.Booking.GetServiceClient(new BaseRequest { TypeOfRestId = tour.TypeOfRestId ?? 0 });
                try
                {
                    client.UpdateTour(tourId);
                }
                finally
                {
                    Booking.Logic.Booking.CloseClient(client);
                }
            }

            return result;
        }

        internal ICollection<string> GetErrorsOfChageStatus(long tourId, string stateMachineActionString)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);
            var result = new List<string>();
            var tour = UnitOfWork.GetById<Tour>(tourId);
            if (tour == null)
            {
                result.Add("Размещение не найдено");
                return result;
            }

            if (stateMachineActionString != "Delete")
            {
                var action = ApiStateController.GetAction(stateMachineActionString);
                if (action?.ToStateId != null)
                {
                    tour.ChildLists = GetChildLists(tour.Id);
                    //if (action.ToStateId == StateMachineStateEnum.Tour.Paid &&
                    //	tour.TypeOfRestId == (long) TypeOfRestEnum.SpecializedСamp)
                    //{
                    //	if (tour.ChildLists != null
                    //		&& tour.ChildLists.Any(
                    //			c =>
                    //				c.StateId != StateMachineStateEnum.Limit.List.IncludedPayment && c.StateId != StateMachineStateEnum.Deleted))
                    //	{
                    //		result.Add("Не все включенные списки находятся в статусе \"Сведения об оплате внесены\"");
                    //	}
                    //}

                    if (action.ToStateId == StateMachineStateEnum.Tour.ToFormed &&
                        tour.TypeOfRestId == (long)TypeOfRestEnum.SpecializedСamp)
                    {
                        if (!tour.LimitOnVedomstvoId.HasValue || tour.LimitOnVedomstvo == null ||
                            tour.LimitOnVedomstvo.YearOfRestId != tour.YearOfRestId
                            || !tour.LimitOnVedomstvo.StateId.HasValue ||
                            tour.LimitOnVedomstvo.StateId == StateMachineStateEnum.Deleted)
                        {
                            result.Add("Нельзя сформировать размещение без указания ОИВ");
                        }


                        if (tour.LimitOnVedomstvo != null &&
                            tour.LimitOnVedomstvo.StateId != StateMachineStateEnum.Limit.Oiv.Brought)
                        {
                            result.Add("Нельзя сформировать размещение с недоведенными квотами");
                        }
                    }

                    if (action.ToStateId == StateMachineStateEnum.Tour.ToFormed &&
                        (tour.TypeOfRestId == (long)TypeOfRestEnum.SpecializedСamp ||
                         tour.TypeOfRestId == (long)TypeOfRestEnum.ChildRest ||
                         tour.TypeOfRestId == (long)TypeOfRestEnum.ChildRestFederalCamps ||
                         tour.TypeOfRestId == (long)TypeOfRestEnum.ChildRestCamps
                        ))
                    {
                        if (tour.Volumes == null || !tour.Volumes.Any() || !tour.Volumes.Any(v => v.CountPlace > 0))
                        {
                            result.Add("Нельзя сформировать размещение без мест");
                        }
                    }

                    if (tour.Volumes != null && tour.LimitOnVedomstvo != null &&
                        action.ToStateId == StateMachineStateEnum.Tour.ToFormed &&
                        tour.TypeOfRestId == (long)TypeOfRestEnum.SpecializedСamp)
                    {
                        var totalSum = UnitOfWork.GetSet<Tour>()
                                          .Where(
                                             t =>
                                                t.StateId != StateMachineStateEnum.Tour.Formation && t.StateId.HasValue &&
                                                t.StateId != StateMachineStateEnum.Deleted &&
                                                t.LimitOnVedomstvoId == tour.LimitOnVedomstvoId &&
                                                t.TypeOfRestId == (long)TypeOfRestEnum.SpecializedСamp)
                                          .SelectMany(t => t.Volumes.Select(v => v.CountPlace)).Sum() ?? 0;
                        var current = tour.Volumes.Select(v => v.CountPlace).Sum() ?? 0;
                        if (tour.NullSafe(t => t.LimitOnVedomstvo.Volume) < totalSum + current)
                        {
                            result.Add("Сумма размещений превышает квоту ОИВ.");
                        }
                    }

                    if (action.ActionCode == AccessRightEnum.Tour.ToTourForm)
                    {
                        if (tour.DateIncome > tour.DateOutcome)
                        {
                            result.Add("Дата начала должна быть меньше или равна дате окончания размещения");
                        }

                        if (!tour.DateIncome.HasValue)
                        {
                            result.Add("Не указана дата начала размещения");
                        }

                        if (!tour.DateOutcome.HasValue)
                        {
                            result.Add("Не указана дата окончания размещения");
                        }

                        if (tour.StartBooking > tour.EndBooking)
                        {
                            result.Add("Дата начала записи должна быть меньше или равна дате окончания записи");
                        }
                    }
                }
                else
                {
                    result.Add("Не найден статус размещения");
                }
            }

            return result;
        }

        internal ICollection<Domain.Booking> GetBookings(long? tourId)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);
            var bookings =
               UnitOfWork.GetSet<Domain.Booking>()
                  .Where(b => b.TourVolume.TourId == tourId && !b.Canceled && b.Request != null &&
                              b.Request.StatusId == (long)StatusEnum.CertificateIssued)
                  .ToList();

            return bookings.ToList();
        }

        internal ICollection<Child> GetChilds(long? tourId)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);
            return UnitOfWork.GetSet<Request>().Where(r => r.TourId == tourId).SelectMany(r => r.Child).ToList();
        }

        internal ICollection<StateMachineState> GetStates()
        {
            SetUnitOfWorkInRefClass(UnitOfWork);
            return
               UnitOfWork.GetSet<StateMachineState>().Where(s => s.StateMachineId == (long)StateMachineEnum.TourState)
                  .ToList();
        }

        public ArrayList GetGroupByDates(long? hotelId)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);
            return new ArrayList(
               UnitOfWork.GetSet<Tour>()
                  .Where(t => t.StateId == StateMachineStateEnum.Tour.Formed && t.HotelsId == hotelId)
                  .GroupBy(t => new { t.DateIncome, t.DateOutcome }, t => t,
                     (key, tours) => new { key.DateIncome, key.DateOutcome })
                  .ToList()
                  .Select(
                     t =>
                        new
                        {
                            t.DateIncome,
                            t.DateOutcome,
                            Text = $"{t.DateIncome.FormatEx()} - {t.DateOutcome.FormatEx()}"
                        })
                  .ToList());
        }
    }
}
