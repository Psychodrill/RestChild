using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using Newtonsoft.Json;
using RestChild.Booking.Logic.Extensions;
using RestChild.Comon;
using RestChild.Comon.Dto;
using RestChild.Comon.Enumeration;
using RestChild.DAL;
using RestChild.Domain;

namespace MailingDemon.Tasks
{
    /// <summary>
    ///     установка связи заявок на квоты в 2021 году
    /// </summary>
    [Task]
    public class MapRequestInList2021 : BaseTask
    {
        private readonly int _yearOfCompany = 2021;

        protected override void Execute()
        {
            //_yearOfCompany = ConfigurationManager.AppSettings["YearMultiCompany"].IntParse() ?? _yearOfCompany;

            // заливаем квоту если нужно
            InitialLoadList();

            // крепим заявки к спискам
            RequestAppendToLimit();

            // ранжируем заявки
            RequestRanking();
        }

        private string PrepareString(string s)
        {
            return s?.ToLower().Replace("ё", "е").Replace("й", "и").Replace(" ", "").Replace("-", "");
        }

        /// <summary>
        ///     формирование детализации по детям
        /// </summary>
        private DetailInfo[] RankChild(IUnitOfWork unitOfWork, Child child)
        {
            var childrenQueue =
                unitOfWork.GetSet<Child>()
                    .Where(
                        c =>
                            FirstRequestCompanyExtension.LiveRequestStatuses.Contains(c.Request.StatusId)
                            && !c.Request.IsDeleted
                            && c.Request.IsLast
                            && c.Request.TypeOfRestId != (long) TypeOfRestEnum.ChildRestFederalCamps).AsQueryable();

            var yearOfRest = child.YearOfCompany > 0 ? child.YearOfCompany : child.Request?.YearOfRest?.Year;
            childrenQueue =
                childrenQueue.Where(c => c.YearOfCompany <= yearOfRest && c.YearOfCompany + 3 >= yearOfRest);

            var entityId = child.EntityId ?? child.Id;
            if (entityId != 0)
            {
                childrenQueue = childrenQueue.Where(c => (c.EntityId ?? c.Id) != entityId);
            }

            var snils = child.Snils.Replace(" ", string.Empty).Replace("-", string.Empty);

            var items = new List<Child>();

            if (!string.IsNullOrWhiteSpace(snils))
            {
                items.AddRange(childrenQueue.Where(c =>
                    !string.IsNullOrEmpty(c.Snils) && c.Snils.Replace(" ", string.Empty).Replace("-", string.Empty) == snils).ToList());

                childrenQueue = childrenQueue.Where(c =>
                    string.IsNullOrEmpty(c.Snils) || c.Snils.Replace(" ", string.Empty).Replace("-", string.Empty) != snils);
            }

            var lastName = PrepareString(child.LastName);
            var firstName = PrepareString(child.FirstName);
            var middleName = PrepareString(child.MiddleName);
            var dateOfBirth = child.DateOfBirth;

            childrenQueue =
                childrenQueue.Where(c =>
                    c.LastName.ToLower().Replace("ё", "е").Replace("й", "и").Replace(" ", "").Replace("-", "") ==
                    lastName);
            childrenQueue =
                childrenQueue.Where(c =>
                    c.FirstName.ToLower().Replace("ё", "е").Replace("й", "и").Replace(" ", "").Replace("-", "") ==
                    firstName);

            if (!string.IsNullOrWhiteSpace(middleName))
            {
                childrenQueue =
                    childrenQueue.Where(c =>
                        c.MiddleName.ToLower().Replace("ё", "е").Replace("й", "и").Replace(" ", "").Replace("-", "") ==
                        middleName);
            }

            childrenQueue =
                childrenQueue.Where(c => c.DateOfBirth == dateOfBirth);


            items.AddRange(childrenQueue.ToList());
            var childName = $"{child.LastName} {child.FirstName} {child.MiddleName}".Trim();

            return items.Where(i => i.RequestId.HasValue)
                .Select(
                    i =>
                        new DetailInfo
                        {
                            ChildId = child.Id,
                            Child = childName,
                            Id = i.RequestId.Value,
                            RegistryNumber = i.Request.RequestNumber,
                            Year = i.YearOfCompany ??
                                   i.Request?.YearOfRest?.Year ?? i.Request?.Tour?.YearOfRest?.Year ?? 0
                        })
                .Where(i => i.Year > 2000)
                .ToArray();
        }

        /// <summary>
        ///     присвоение ранга заявлению.
        /// </summary>
        protected void RequestRanking()
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var year = unitOfWork.GetSet<YearOfRest>().FirstOrDefault(y => y.Year == _yearOfCompany);

                if (year == null)
                {
                    return;
                }

                if (year.ListComplited || year.TourOpened || year.IsClosed)
                {
                    Logger.Info($"MapRequestInList2021 - RequestRanking not started {_yearOfCompany} company finish");
                    return;
                }

                Logger.Info($"MapRequestInList2021 - RequestRanking start {_yearOfCompany} - company");

                var ranking =
                    unitOfWork.GetSet<ListTravelersRequest>()
                        .Where(l => !l.Rank.HasValue && l.ListTravelers.YearOfRestId == year.Id)
                        .Include(r => r.Request)
                        .Include(r => r.Request.Child)
                        .Take(5000)
                        .ToList();

                var orphanBenefitTypeIds = new long?[] {1, 2, 12, 13, 18, 21, 22, 23, 24, 34, 38, 39, 40, 55, 74};

                foreach (var rank in ranking)
                {
                    var details = new List<DetailInfo>();

                    if ((
                        rank.Request.TypeOfRestId == (long) TypeOfRestEnum.YouthRestOrphanCamps ||
                        rank.Request.TypeOfRestId == (long) TypeOfRestEnum.YouthRestCamps)&&
                        rank.Request.Applicant != null)
                    {
                        rank.Request.Applicant.IsAccomp = true;
                        unitOfWork.SaveChanges();
                    }

                    if (rank.Request.TypeOfRestId == (long) TypeOfRestEnum.ChildRestOrphanCamps ||
                        rank.Request.TypeOfRestId == (long) TypeOfRestEnum.YouthRestOrphanCamps ||
                        rank.Request.TypeOfRestId == (long) TypeOfRestEnum.RestWithParentsOrphan ||
                        rank.Request.TypeOfRestId == (long) TypeOfRestEnum.TentChildrenCampOrphan ||
                        (rank.Request.Child.Any(c => orphanBenefitTypeIds.Contains(c.BenefitTypeId)) &&
                         rank.Request.TypeOfRestId == (long) TypeOfRestEnum.RestWithParentsComplex))
                    {
                        rank.Rank = 100000;
                    }
                    else
                    {
                        var count = rank.Request.Child.Count;
                        var r = 0;

                        var additionalRank = 0;
                        var maximumRank = 0;
                        var minimumRank = 4;

                        foreach (var child in rank.Request.Child)
                        {
                            var n = RankChild(unitOfWork, child);
                            details.AddRange(n);
                            var rChild = n.GetRank();

                            if (maximumRank < rChild)
                            {
                                maximumRank = rChild;
                            }

                            if (minimumRank > rChild)
                            {
                                minimumRank = rChild;
                            }

                            r += rChild;

                            if (rChild == 3)
                            {
                                additionalRank = 7;
                            }
                        }

                        if (count == 0)
                        {
                            rank.Rank = 100000;
                        }
                        else if (maximumRank > 0 && minimumRank < 4)
                        {
                            // новое ранжирование
                            rank.Rank = maximumRank == 3 ? 100000 :
                                minimumRank == 2 ? 50000 :
                                10000;
                        }
                        else
                        {
                            // старое ранжирование
                            rank.Rank = r * 10000 / count + additionalRank * 10000;
                        }

                        var item = rank.Details.FirstOrDefault() ?? new ListTravelersRequestDetail();
                        item.Detail = JsonConvert.SerializeObject(details);
                        if (item.Id == 0)
                        {
                            item.ListTravelersRequestId = rank.Id;
                            rank.Details.Add(unitOfWork.AddEntity(item, false));
                        }
                    }

                    unitOfWork.SaveChanges();
                }

                foreach (var entry in unitOfWork.Context.ChangeTracker.Entries())
                {
                    entry.State = EntityState.Detached;
                }

                Logger.Info("MapRequestInList2021 - RequestRanking finish");
            }
        }

        /// <summary>
        ///     прикрепление заявки к квоте
        /// </summary>
        protected void RequestAppendToLimit()
        {
            //льготы детей инвалидов
            var invalidBenefitIds = new long?[] {41, 42, 43, 75, 80, 81};

            using (var unitOfWork = new UnitOfWork())
            {
                var year = unitOfWork.GetSet<YearOfRest>().FirstOrDefault(y => y.Year == _yearOfCompany);

                if (year == null)
                {
                    return;
                }

                if (year.ListComplited || year.TourOpened || year.IsClosed)
                {
                    Logger.Info(
                        $"MapRequestInList2021 - RequestAppendToLimit not started {_yearOfCompany} company finish");
                    return;
                }

                Logger.Info($"MapRequestInList2021 - RequestAppendToLimit started - {_yearOfCompany} company");

                var items =
                    unitOfWork.GetSet<ListTravelers>()
                        .Where(y => y.YearOfRestId == year.Id && y.TypeOfRestId.HasValue)
                        .GroupedDictionary(t => t.TypeOfRestId, t => t);

                var requests = unitOfWork.GetSet<ListTravelersRequest>().AsQueryable();

                var requestForInsert = unitOfWork.GetSet<Request>()
                    .Where(r => r.IsFirstCompany && !r.IsDeleted && !requests.Any(l => l.RequestId == r.Id) &&
                                r.YearOfRestId == year.Id)
                    .Where(r => r.StatusId == (long) StatusEnum.Ranging /* ||
                                r.StatusId == (long) StatusEnum.DecisionMaking ||
                                r.StatusId == (long) StatusEnum.DecisionMakingCovid ||
                                r.StatusId == (long) StatusEnum.IncludedInList*/).Take(1000).ToList();

                foreach (var request in requestForInsert)
                {
                    var typeOfRestId = request.TypeOfRestId ?? 0;

                    if (typeOfRestId == (long) TypeOfRestEnum.TentChildrenCampOrphan)
                    {
                        typeOfRestId = (long) TypeOfRestEnum.ChildRestOrphanCamps;
                    }

                    if (typeOfRestId == (long) TypeOfRestEnum.TentChildrenCamp)
                    {
                        typeOfRestId = (long) TypeOfRestEnum.ChildRestCamps;
                    }

                    if (!items.ContainsKey(typeOfRestId))
                    {
                        continue;
                    }

                    var limits = items[typeOfRestId];
                    if (limits.Count == 1)
                    {
                        unitOfWork.AddEntity(
                            new ListTravelersRequest
                            {
                                ListTravelersId = limits[0].Id,
                                DateRequest = request.DateRequest,
                                RequestId = request.Id
                            }, false);
                    }
                    else
                    {
                        if (typeOfRestId == (long) TypeOfRestEnum.MoneyOn7To15)
                        {
                            unitOfWork.AddEntity(
                                new ListTravelersRequest
                                {
                                    ListTravelersId = request.Child.Any(c =>
                                        invalidBenefitIds.Contains(c.BenefitTypeId))
                                        ? limits.Where(l => l.Point == "7.2.").Select(l => l.Id).Min()
                                        : limits.Where(l => l.Point == "9.2.").Select(l => l.Id).Max(),
                                    DateRequest = request.DateRequest,
                                    RequestId = request.Id
                                }, false);
                        }

                        if (typeOfRestId == (long) TypeOfRestEnum.ChildRestCamps)
                        {
                            unitOfWork.AddEntity(
                                new ListTravelersRequest
                                {
                                    ListTravelersId = request.Child.Any(c =>
                                        invalidBenefitIds.Contains(c.BenefitTypeId))
                                        ? limits.Where(l => l.Point == "7.1.").Select(l => l.Id).Min()
                                        : limits.Where(l => l.Point == "9.1.").Select(l => l.Id).Max(),
                                    DateRequest = request.DateRequest,
                                    RequestId = request.Id
                                }, false);
                        }
                    }
                }

                unitOfWork.SaveChanges();

                foreach (var entry in unitOfWork.Context.ChangeTracker.Entries())
                {
                    entry.State = EntityState.Detached;
                }

                // удаление то что отказали
                Logger.Info("MapRequestInList2021 - RequestAppendToLimit delete declined request");
                var requestForExclude = unitOfWork.GetSet<Request>()
                    .Where(r => r.IsFirstCompany && !r.IsDeleted && r.YearOfRestId == year.Id)
                    .Where(
                        r =>
                            r.StatusId == (long) StatusEnum.CancelByApplicant ||
                            r.StatusId == (long) StatusEnum.RegistrationDecline ||
                            r.StatusId == (long) StatusEnum.Reject);

                var excludeItems = requests
                    .Where(l => requestForExclude.Any(r => l.RequestId == r.Id) && !l.IsIncluded)
                    .Take(30000)
                    .ToArray();

                foreach (var ei in excludeItems)
                {
                    unitOfWork.Delete<ListTravelersRequestDetail>(ei.Details.ToArray());
                    unitOfWork.Delete(ei);
                }

                unitOfWork.SaveChanges();

                Logger.Info("MapRequestInList2021 - RequestAppendToLimit delete declined request finished");
                foreach (var entry in unitOfWork.Context.ChangeTracker.Entries())
                {
                    entry.State = EntityState.Detached;
                }

                Logger.Info($"MapRequestInList2021 - RequestAppendToLimit finish {_yearOfCompany} company");
            }
        }


        /// <summary>
        ///     заливка квот на 2021 год
        /// </summary>
        protected void InitialLoadList()
        {
            using (var unitOfWork = new UnitOfWork())
            {
                Logger.Info($"MapRequestInList2021 - InitialLoadList started (company {_yearOfCompany})");

                var year = unitOfWork.GetSet<YearOfRest>().FirstOrDefault(y => y.Year == _yearOfCompany);

                if (year == null)
                {
                    return;
                }

                if (year.ListComplited || year.TourOpened || year.IsClosed)
                {
                    Logger.Info(
                        $"MapRequestInList2020 - InitialLoadList not started company finish (company {_yearOfCompany})");
                    return;
                }

                if (unitOfWork.GetSet<ListTravelers>().Any(a => a.YearOfRestId == year.Id))
                {
                    return;
                }

                var set = unitOfWork.GetSet<ListTravelers>();

                set.AddOrUpdate(a => a.Id, new ListTravelers
                {
                    Point = "1.",
                    Name =
                        "Дети-сироты и дети, оставшиеся без попечения родителей, являющиеся воспитанниками учреждений социальной защиты (путевки)",
                    Limit = 6763,
                    YearOfRestId = year.Id,
                    TypeOfRestId = (long) TypeOfRestEnum.SpecializedСamp
                }, new ListTravelers
                {
                    Point = "2.",
                    Name =
                        "Дети-сироты и дети, оставшиеся без попечения родителей, находящиеся под опекой попечительством, в том числе в приемной или патронатной семье, в возрасте от 3 до 17 лет включительно (совместный отдых, путевки)",
                    Limit = 2434,
                    YearOfRestId = year.Id,
                    TypeOfRestId = (long) TypeOfRestEnum.RestWithParentsOrphan
                }, new ListTravelers
                {
                    Point = "3.",
                    Name =
                        "Дети-сироты и дети, оставшиеся без попечения родителей, находящиеся под опекой попечительством, в том числе в приемной или патронатной семье, в возрасте от 7 до 17 лет включительно (индивидуальный отдых, путевки)",
                    Limit = 395,
                    YearOfRestId = year.Id,
                    TypeOfRestId = (long) TypeOfRestEnum.ChildRestOrphanCamps
                }, new ListTravelers
                {
                    Point = "4.",
                    Name =
                        "Лица из числа детей-сирот и детей, оставшихся без попечения родителей (молодежный отдых, путевки)",
                    Limit = 89,
                    YearOfRestId = year.Id,
                    TypeOfRestId = (long) TypeOfRestEnum.YouthRestOrphanCamps
                }, new ListTravelers
                {
                    Point = "5.",
                    Name =
                        "Дети разных льготных категорий, воспитывающихся в одной семье, в возрасте от 3 до 17 лет включительно (совместный отдых, путевки)",
                    Limit = 1013,
                    YearOfRestId = year.Id,
                    TypeOfRestId = (long) TypeOfRestEnum.RestWithParentsComplex
                }, new ListTravelers
                {
                    Point = "6.1.",
                    Name =
                        "Дети-инвалиды, дети с ограниченными возможностями здоровья в возрасте от 4 до 17 лет включительно (совместный отдых, путевки)",
                    Limit = 2975,
                    YearOfRestId = year.Id,
                    TypeOfRestId = (long) TypeOfRestEnum.RestWithParentsInvalid
                }, new ListTravelers
                {
                    Point = "6.2.",
                    Name =
                        "Дети-инвалиды, дети с ограниченными возможностями здоровья в возрасте от 4 до 17 лет включительно (сертификаты)",
                    Limit = 2992,
                    YearOfRestId = year.Id,
                    TypeOfRestId = (long) TypeOfRestEnum.MoneyOnInvalidOn4To17
                }, new ListTravelers
                {
                    Point = "7.1.",
                    Name =
                        "Дети-инвалиды, дети с ограниченными возможностями здоровья в возрасте от 7 до 15 лет включительно (индивидуальный отдых, путевки)",
                    Limit = 23,
                    YearOfRestId = year.Id,
                    TypeOfRestId = (long) TypeOfRestEnum.ChildRestCamps
                }, new ListTravelers
                {
                    Point = "7.2.",
                    Name =
                        "Дети-инвалиды, дети с ограниченными возможностями здоровья в возрасте от 7 до 15 лет включительно (сертификаты)",
                    Limit = 38,
                    YearOfRestId = year.Id,
                    TypeOfRestId = (long) TypeOfRestEnum.MoneyOn7To15
                }, new ListTravelers
                {
                    Point = "8.1.",
                    Name =
                        @"Дети из малообеспеченных семей в возрасте от 3 до 7 лет включительно (совместный отдых, путевки)",
                    Limit = 9417,
                    YearOfRestId = year.Id,
                    TypeOfRestId = (long) TypeOfRestEnum.RestWithParentsPoor
                }, new ListTravelers
                {
                    Point = "8.2.",
                    Name =
                        "Дети из малообеспеченных семей в возрасте от 3 до 7 лет включительно (сертификаты)",
                    Limit = 10980,
                    YearOfRestId = year.Id,
                    TypeOfRestId = (long) TypeOfRestEnum.MoneyOn3To7
                }, new ListTravelers
                {
                    Point = "9.1.",
                    Name =
                        @"Дети из малообеспеченных семей и 9 льготных категорий в возрасте от 7 до 15 лет включительно (индивидуальный отдых, путевки)",
                    Limit = 1389,
                    YearOfRestId = year.Id,
                    TypeOfRestId = (long) TypeOfRestEnum.ChildRestCamps
                }, new ListTravelers
                {
                    Point = "9.2.",
                    Name =
                        "Дети из малообеспеченных семей и 9 льготных категорий в возрасте от 7 до 15 лет включительно (сертификаты)",
                    Limit = 6765,
                    YearOfRestId = year.Id,
                    TypeOfRestId = (long) TypeOfRestEnum.MoneyOn7To15
                });

                unitOfWork.SaveChanges();
                Logger.Info("MapRequestInList2021 - InitialLoadList finish");
            }
        }
    }
}
