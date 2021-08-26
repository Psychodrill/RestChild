using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using RestChild.Comon;
using RestChild.Comon.Enumeration;
using RestChild.Comon.ERL;
using RestChild.DAL.RepositoryExtensions;
using RestChild.Domain;
using RestChild.Extensions.Filter;
using RestChild.Web.Controllers.WebApi;
using RestChild.Web.Models;
using RestChild.Web.Properties;

namespace RestChild.Web.Controllers
{
    [Authorize]
    public partial class RequestCurrentPeriodController : BaseController
    {
        public WebRequestCurrentPeriodController ApiController { get; set; }
        public WebVocabularyController VocController { get; set; }

        public override void SetUnitOfWorkInRefClass(IUnitOfWork unitOfWork)
        {
            base.SetUnitOfWorkInRefClass(unitOfWork);
            ApiController.SetUnitOfWorkInRefClass(unitOfWork);
            VocController.SetUnitOfWorkInRefClass(unitOfWork);
        }

        public ActionResult Index()
        {
            return RedirectToAction("Search");
        }

        public ActionResult Search(string name = "", int pageNumber = 1)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);
            var requestCurrentPeriod = ApiController.Get(name, pageNumber);
            ViewBag.name = name;
            return View("RequestCurrentPeriodList", requestCurrentPeriod);
        }

        /// <summary>
        ///     Новая заявочная кампания
        /// </summary>
        public ActionResult Insert()
        {
            SetUnitOfWorkInRefClass(UnitOfWork);
            var newRecod = new YearOfRest {Year = DateTime.Now.Year + 1, Name = (DateTime.Now.Year + 1).ToString()};
            var model = new YearOfRestModel(newRecod);

            return View("RequestCurrentPeriodEdit", model);
        }

        /// <summary>
        ///     управление списком заявлений для включения в квоты.
        /// </summary>
        public ActionResult LimitEdit(long? id, LimitEditModel model)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);
            var entity = UnitOfWork.GetById<ListTravelers>(model?.Data?.Id ?? id);
            model = model ?? new LimitEditModel();
            model.Data = entity;

            var query = FilterListTravelersRequests(model, entity);

            model.TimeOfRests =
                VocController.GetTimesOfRestWithoutFilter(model.Data.TypeOfRestId, model.Data.YearOfRestId);
            model.BenefitTypes = VocController.GetBenefitTypeInternal(model.Data.TypeOfRestId);
            model.PlaceOfRests = VocController.GetPlacesOfRest();

            if (model.Action == "Excel")
            {
                return ExcelList(model);
            }

            if (entity?.YearOfRest?.ListComplited == false)
            {
                if (model.Action == "IncludeRequest" && model.CountToInclude > 0)
                {
                    var items = query.OrderByDescending(r => r.Rank).ThenBy(r => r.DateRequest)
                        .Take(model.CountToInclude.Value).ToArray();
                    ApiController.IncludeRequest(items);
                }
                else if (model.Action == "ExcludeAll")
                {
                    ApiController.ExcludeAll(model.Data.Id);
                }
            }

            UnitOfWork.SaveChanges();

            model.FactCount = UnitOfWork.GetSet<ListTravelersRequest>().Count(l => l.ListTravelersId == model.Data.Id);
            model.FactIncludedCount =
                UnitOfWork.GetSet<ListTravelersRequest>()
                    .Where(l => l.ListTravelersId == model.Data.Id && l.IsIncluded)
                    .Sum(l => (int?) (l.Request.TypeOfRestId == (long) TypeOfRestEnum.YouthRestOrphanCamps
                        ? 1
                        : l.Request.CountPlace)) ?? 0;
            model.FactAttendantCount =
                UnitOfWork.GetSet<ListTravelersRequest>()
                    .Where(l => l.ListTravelersId == model.Data.Id && l.IsIncluded)
                    .Sum(l => (int?) (l.Request.TypeOfRestId == (long) TypeOfRestEnum.YouthRestOrphanCamps
                        ? 0
                        : l.Request.CountAttendants)) ?? 0;

            model.Action = string.Empty;
            model.CountToInclude = null;

            var pageNumber = model.PageNumber ?? 1;
            var pageSize = Settings.Default.TablePageSize;
            var startRecord = (pageNumber - 1) * pageSize;
            var totalCount = query.Count();
            var entitys = query.OrderByDescending(r => r.Rank).ThenBy(r => r.DateRequest).Skip(startRecord)
                .Take(pageSize).ToList();
            model.Requests = new CommonPagedList<ListTravelersRequest>(entitys, pageNumber, pageSize, totalCount);

            ModelState.Clear();
            return View("LimitEdit", model);
        }

        private IQueryable<ListTravelersRequest> FilterListTravelersRequests(LimitEditModel model, ListTravelers entity)
        {
            var query = UnitOfWork.GetSet<ListTravelersRequest>().Where(l => l.ListTravelersId == entity.Id);

            if (model.BenefitTypeId > 0)
            {
                query = query.Where(q => q.Request.Child.Any(c => c.BenefitTypeId == model.BenefitTypeId));
            }

            if (model.DateRequestFrom.HasValue)
            {
                query = query.Where(q => q.Request.DateRequest >= model.DateRequestFrom);
            }

            if (model.DateRequestTo.HasValue)
            {
                var date = model.DateRequestTo.Value.Date.AddDays(1);
                query = query.Where(q => q.Request.DateRequest < date);
            }

            if (model.TimeOfRestId > 0)
            {
                query =
                    query.Where(
                        q => q.Request.TimeOfRestId == model.TimeOfRestId ||
                             q.Request.TimesOfRest.Any(t => t.TimeOfRestId == model.TimeOfRestId));
            }

            if (model.PlaceOfRestId > 0)
            {
                query =
                    query.Where(
                        q =>
                            q.Request.PlaceOfRestId == model.PlaceOfRestId ||
                            q.Request.PlacesOfRest.Any(t => t.PlaceOfRestId == model.PlaceOfRestId));
            }

            if (!string.IsNullOrWhiteSpace(model.Fio))
            {
                var items = model.Fio.ToLower().Split(' ').Where(s => !string.IsNullOrWhiteSpace(s)).ToArray();

                query = items.Aggregate(query,
                    (current, item) =>
                        current.Where(
                            c =>
                                c.Request.Applicant.IsAccomp &&
                                (c.Request.Applicant.LastName.ToLower().Contains(item) ||
                                 c.Request.Applicant.FirstName.ToLower().Contains(item) ||
                                 c.Request.Applicant.MiddleName.ToLower().Contains(item)) ||
                                c.Request.Child.Any(
                                    a =>
                                        !a.IsDeleted &&
                                        (a.LastName.ToLower().Contains(item) || a.FirstName.ToLower().Contains(item) ||
                                         a.MiddleName.ToLower().Contains(item))) ||
                                c.Request.Attendant.Any(
                                    a =>
                                        !a.IsDeleted &&
                                        (a.LastName.ToLower().Contains(item) || a.FirstName.ToLower().Contains(item) ||
                                         a.MiddleName.ToLower().Contains(item)))));
            }

            if (!string.IsNullOrWhiteSpace(model.RequestNumber))
            {
                query =
                    query.Where(
                        q =>
                            q.Request.RequestNumber.Contains(model.RequestNumber) ||
                            q.Request.RequestNumberMpgu != null &&
                            q.Request.RequestNumberMpgu.Contains(model.RequestNumber));
            }

            if (model.RankFrom.HasValue)
            {
                if (model.ExcludeRankFrom)
                {
                    query =
                        query.Where(
                            q => q.Rank > model.RankFrom * 10000);
                }
                else
                {
                    query =
                        query.Where(
                            q => q.Rank >= model.RankFrom * 10000);
                }
            }

            if (model.RankTo.HasValue)
            {
                if (model.ExcludeRankTo)
                {
                    query =
                        query.Where(
                            q => q.Rank < model.RankTo * 10000);
                }
                else
                {
                    query =
                        query.Where(
                            q => q.Rank <= model.RankTo * 10000);
                }
            }

            if (model.ViewRequestType > 0)
            {
                if (model.ViewRequestType == 1)
                {
                    query = query.Where(q => q.IsIncluded);
                }
                else if (model.ViewRequestType == 2)
                {
                    query = query.Where(q => !q.IsIncluded);
                }
            }

            if (model.StatusOfCheck > 0)
            {
                if (model.StatusOfCheck == 1)
                {
                    query =
                        query.Where(
                            q => !q.Request.Child.Any(c =>
                                c.BaseRegistryInfo.Any(b => !b.NotActual && (!b.IsProcessed || !b.Success))));
                }
                else if (model.StatusOfCheck == 2)
                {
                    query =
                        query.Where(
                            q => q.Request.Child.Any(c =>
                                c.BaseRegistryInfo.Any(b => !b.NotActual && (!b.IsProcessed || !b.Success))));
                }
            }

            return query;
        }

        public ActionResult Update(long id)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);
            var model = PrepareYearsModel(id);

            return View("RequestCurrentPeriodEdit", model);
        }

        private YearOfRestModel PrepareYearsModel(long id)
        {
            var record = ApiController.Get(id);
            var items =
                UnitOfWork.GetSet<ListTravelers>()
                    .Where(y => y.YearOfRestId == id)
                    .OrderBy(y => y.Point)
                    .ToArray()
                    .Select(s => new LimitEditModel {Data = s})
                    .ToArray();


            var model = new YearOfRestModel(record)
            {
                Limits = items.ToDictionary(i => i.Data.Id.ToString(), i => i)
            };

            SetPrices(model);

            var filedItems = items.Where(d => d.Data.TypeOfRestId.HasValue).ToArray();

            var dictionary = items.ToDictionary(i => i.Data.Id, i => i);

            foreach (var limit in filedItems)
            {
                limit.FactCount = UnitOfWork.GetSet<ListTravelersRequest>()
                    .Count(l => l.ListTravelersId == limit.Data.Id);
                limit.FactIncludedCount =
                    UnitOfWork.GetSet<ListTravelersRequest>()
                        .Where(l => l.ListTravelersId == limit.Data.Id && l.IsIncluded)
                        .Sum(
                            l =>
                                (int?)
                                (l.Request.TypeOfRestId == (long) TypeOfRestEnum.YouthRestOrphanCamps
                                    ? 1
                                    : l.Request.CountAttendants + l.Request.CountPlace)) ?? 0;

                var parentId = limit.Data.ParentId;
                while (parentId.HasValue)
                {
                    var d = dictionary.ContainsKey(parentId ?? 0) ? dictionary[parentId ?? 0] : null;
                    if (d != null)
                    {
                        d.PlanCount += limit.Data.Limit ?? 0;
                        d.FactCount += limit.FactCount;
                        d.FactIncludedCount += limit.FactIncludedCount;
                        parentId = d.Data?.ParentId;
                    }
                    else
                    {
                        parentId = null;
                    }
                }
            }

            return model;
        }

        /// <summary>
        ///     Установка средней цены на отдых (для ЕРЛ)
        /// </summary>
        private void SetPrices(YearOfRestModel record)
        {
            var typesOfRest = UnitOfWork.GetSet<TypeOfRest>()
                .Where(y => (y.IsActive || y.Prices.Any(s => s.YearOfRestId == record.Data.Id)) &&
                            y.Id != (long) TypeOfRestEnum.Compensation &&
                            y.Id != (long) TypeOfRestEnum.CompensationYouthRest)
                .Where(y => y.TypeOfRestERLId != null)
                .OrderBy(y => y.Name)
                .ToArray();

            var prices = new Dictionary<string, AverageRestPrice>();

            var j = 0;
            foreach (var type in typesOfRest)
            {
                var av = type.Prices.FirstOrDefault(s => s.YearOfRestId == record.Data.Id) ?? new AverageRestPrice
                {
                    YearOfRestId = record.Data.Id, TypeOfRestId = type.Id, TypeOfRest = type, Price = 0, Id = j--
                };
                prices[av.Id.ToString()] = av;
            }

            record.AveragePrices = prices;
        }

        /// <summary>
        ///     Сохранить заявочную кампанию
        /// </summary>
        [HttpPost]
        public ActionResult Save(YearOfRestModel requestCurrentPeriod)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);
            foreach (var key in ModelState.Keys.ToList()
                .Where(key => key.StartsWith(nameof(requestCurrentPeriod.AveragePrices))))
            {
                ModelState[key].Errors.Clear();
            }

            if (!ModelState.IsValid)
            {
                return View("RequestCurrentPeriodEdit", requestCurrentPeriod);
            }

            var entity = requestCurrentPeriod.BuildData();

            if (entity.Id == 0)
            {
                ApiController.Post(entity);
            }
            else
            {
                var domain = UnitOfWork.GetById<YearOfRest>(entity.Id);
                entity.ReceptionOfApplicationsCompleted = domain.ReceptionOfApplicationsCompleted;
                entity.ListComplited = domain.ListComplited;
                entity.TourOpened = domain.TourOpened;
                UnitOfWork.Context.Entry(domain).State = EntityState.Detached;

                ApiController.Put(entity.Id, entity);
                if (requestCurrentPeriod.Limits != null && requestCurrentPeriod.Limits.Any())
                {
                    var items =
                        UnitOfWork.GetSet<ListTravelers>()
                            .Where(y => y.YearOfRestId == entity.Id)
                            .OrderBy(y => y.Point)
                            .ToDictionary(d => d.Id, d => d);

                    var ids = requestCurrentPeriod.Limits.Values?.Select(l => l.Data?.Id ?? 0).ToArray();
                    var toUpdate = items.Values.Where(v => !ids.Contains(v.Id)).ToArray();
                    foreach (var upd in toUpdate)
                    {
                        upd.Limit = 0;
                    }

                    foreach (var limit in requestCurrentPeriod.Limits.Values)
                    {
                        if (items.ContainsKey(limit.Data?.Id ?? 0))
                        {
                            var item = items[limit.Data?.Id ?? 0];
                            item.Limit = limit.Data?.Limit ?? 0;

                            var parentId = item.ParentId;
                            while (parentId.HasValue)
                            {
                                var d = items.ContainsKey(parentId ?? 0) ? items[parentId ?? 0] : null;
                                if (d != null)
                                {
                                    d.Limit += limit.Data?.Limit ?? 0;
                                    parentId = d?.ParentId;
                                }
                                else
                                {
                                    parentId = null;
                                }
                            }
                        }
                    }

                    UnitOfWork.SaveChanges();
                }
            }

            if (requestCurrentPeriod.AveragePrices != null)
            {
                foreach (var price in requestCurrentPeriod.AveragePrices.Values)
                {
                    var av = UnitOfWork.GetById<AverageRestPrice>(price.Id) ?? new AverageRestPrice
                                 {TypeOfRestId = price.TypeOfRestId, YearOfRestId = price.YearOfRestId};
                    av.Price = price.Price;
                    if (av.Id < 1)
                    {
                        UnitOfWork.AddEntity(av);
                    }

                    UnitOfWork.SaveChanges();
                }
            }

            return RedirectToAction("Update", new {id = entity.Id});
        }

        /// <summary>
        ///     Постановка задачи на отправку файла (поток 2.4) В ЕРЛ
        /// </summary>
        [HttpPost]
        [HttpGet]
        public bool Send24ToERL(long? CurrentPeriodId)
        {
            try
            {
                SetUnitOfWorkInRefClass(UnitOfWork);
                var task = new ExchangeUTS
                {
                    DateCreate = DateTime.Now,
                    Incoming = false,
                    Message = CurrentPeriodId.ToString(),
                    Processed = false,
                    FromOrgCode = 796.ToString(),
                    ToOrgCode = string.Empty,
                    MessageId = Guid.NewGuid().ToString(),
                    QueueName = ERLConstants.ERLQueue24ExchangeUTS,
                    ServiceNumber = string.Empty
                };

                UnitOfWork.AddEntity(task);
                UnitOfWork.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
