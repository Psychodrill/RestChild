using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Configuration;
using System.Web.Http;
using RestChild.Booking.Logic.Extensions;
using RestChild.Comon;
using RestChild.Comon.Dto.Commercial;
using RestChild.Comon.Enumeration;
using RestChild.DAL.RepositoryExtensions;
using RestChild.Domain;

namespace RestChild.Web.Controllers.WebApi
{
    [AllowAnonymous]
    public class WebVocabularyController : BaseController
    {
        /// <summary>
        ///     Получить регионы страны
        /// </summary>
        [HttpGet]
        [HttpPost]
        public IList<StateDistrict> GetStateDistricts(bool allItems = false)
        {
            var q = UnitOfWork.GetSet<StateDistrict>().AsQueryable();

            if(!allItems)
            {
                q = q.Where(p => p.IsActive);
            }

            q = q.OrderBy(p => p.Name);

            var res = q.ToList();

            return res;
        }

        /// <summary>
        ///     Получить регионы отдыха
        /// </summary>
        [HttpGet]
        [HttpPost]
        public IList<PlaceOfRest> GetPlacesOfRest(bool allItems = false)
        {
            return GetPlacesOfRestInternal(allItems);
        }

        /// <summary>
        ///     Получить периоды отдыха для группы (потребности) учреждения социальной защиты
        /// </summary>
        [HttpGet]
        [HttpPost]
        public IList<BaseResponse> GetPlacesOfRestForPupilsRequestOfPeriodRest(long groupId)
        {
            var g = UnitOfWork.GetById<PupilGroup>(groupId);

            return UnitOfWork.GetSet<PlaceOfRestTypeOfRest>()
                .Where(ss => ss.TypeOfRestId == g.FormOfRest.TypeOfRestId && ss.PlaceOfRest.IsActive)
                .Select(ss => new BaseResponse
                {
                    Id = ss.PlaceOfRest.Id,
                    Name = ss.PlaceOfRest.Name
                }).OrderBy(ss => ss.Name).ToList();
        }

        /// <summary>
        ///     Получить регионы отдыха
        /// </summary>
        internal IList<PlaceOfRest> GetPlacesOfRestInternal(bool allItems)
        {
            var res =
                UnitOfWork.GetSet<PlaceOfRest>()
                    .Where(p => p.IsActive)
                    .OrderBy(p => p.Name)
                    .ApplyOrder()
                    .ToList()
                    .Where(p => p.ForMpgu || allItems)
                    .Select(p =>
                        new PlaceOfRest(p)
                    {
                        TypeOfRestIds = p.TypeOfRests?.Select(t => t.TypeOfRestId).Where(t => t.HasValue).ToArray()
                    })
                    .ToList();

            foreach (var item in res)
            {
                if (!string.IsNullOrWhiteSpace(item.PhotoUrl))
                {
                    item.PhotoUrl = string.Format(WebConfigurationManager.AppSettings["UploadPhotoUrl"], item.PhotoUrl);
                }
            }

            return res;
        }

        /// <summary>
        ///     получить чьи интересы представляет
        /// </summary>
        [HttpGet]
        [HttpPost]
        public TypeOfSubRestriction[] GetTypeOfSubRestriction()
        {
            return GetTypeOfSubRestriction(null);
        }

        /// <summary>
        ///     получить чьи интересы представляет
        /// </summary>
        [HttpGet]
        [HttpPost]
        public TypeOfSubRestriction[] GetTypeOfSubRestriction(long? id)
        {
            var sub = UnitOfWork.GetSet<TypeOfSubRestriction>().Where(r => !r.IsDeleted);

            if (id.HasValue)
            {
                sub = sub.Where(r => r.TypeOfRestrictionId == id);
            }

            return sub.OrderBy(r => r.Id).ToArray().Select(r => new TypeOfSubRestriction(r)).ToArray();
        }

        /// <summary>
        ///     получить чьи интересы представляет
        /// </summary>
        [HttpGet]
        [HttpPost]
        public RepresentInterest[] GetRepresentInterest(long? id)
        {
            var representInterests = UnitOfWork.GetSet<RepresentInterest>().Where(r => !r.IsDeleted);

            // очистка типа
            var excludeFatherMother = new[]
            {
                (long?) TypeOfRestEnum.ChildRestOrphanCamps, (long?) TypeOfRestEnum.RestWithParentsOrphan,
                (long?) TypeOfRestEnum.YouthRestOrphanCamps, (long?) TypeOfRestEnum.CompensationYouthRest,
                (long?) TypeOfRestEnum.MoneyOn18
            };

            if (excludeFatherMother.Contains(id))
            {
                representInterests = representInterests.Where(r => r.Id != 1 && r.Id != 2);
            }

            if (id == (long) TypeOfRestEnum.YouthRestCamps ||
                id == (long) TypeOfRestEnum.YouthRestOrphanCamps ||
                id == (long) TypeOfRestEnum.MoneyOn18 ||
                id == (long) TypeOfRestEnum.CompensationYouthRest)
            {
                representInterests = representInterests.Where(r => r.Id == 4);
            }
            else
            {
                representInterests = representInterests.Where(r => r.Id != 4);
            }

            return representInterests.OrderBy(r => r.Id).ToArray().Select(r => new RepresentInterest(r)).ToArray();
        }

        [HttpGet]
        [HttpPost]
        [Authorize]
        public List<Status> GetStatusOfRest()
        {
            return UnitOfWork.GetSet<Status>().OrderBy(p => p.Id).ToList();
        }

        [HttpGet]
        [HttpPost]
        public List<Beneficiaries> GetBeneficiarieses()
        {
            return UnitOfWork.GetSet<Beneficiaries>().Where(p => p.Id != (long) BeneficiariesEnum.Child)
                .OrderBy(p => p.Id)
                .ToList().Select(b => new Beneficiaries(b)).ToList();
        }

        [HttpGet]
        [HttpPost]
        public List<RequestFileType> GetRequestFileType()
        {
            return UnitOfWork.GetSet<RequestFileType>().Where(t => t.ForMpgu && t.IsActive && !t.TypeOfRests.Any())
                .OrderBy(p => p.Id).ToList().Select(b => new RequestFileType(b)).ToList();
        }

        [HttpGet]
        [HttpPost]
        public List<Beneficiaries> GetAbsentOtherParentReason()
        {
            return new List<Beneficiaries>();
        }

        [HttpGet]
        [HttpPost]
        public List<TypeOfRest> GetTypesOfRequest()
        {
            var q =
                UnitOfWork.GetSet<TypeOfRest>()
                    .Where(p => p.IsActive && !p.Commercial)
                    .Where(p => p.ForMPGU)
                    .Where(p => p.FirstRequestCompanySelect);

            var res = q.ToList();
            // убираем промежуточные уровни видов отдыха
            var data = res;
            res = new List<TypeOfRest>();
            var hashSet = data.Select(d => d.ParentId).Where(p => p.HasValue).ToHashSet();
            var exists = data.Select(d => d.Id).ToHashSet();

            foreach (var d in data)
            {
                if (d.Parent == null)
                {
                    res.Add(new TypeOfRest(d));
                }
                else if (!hashSet.Contains(d.Id))
                {
                    var parent = d.Parent;
                    while (parent.Parent != null)
                    {
                        parent = parent.Parent;
                    }

                    if (!exists.Contains(parent.Id))
                    {
                        res.Add(new TypeOfRest(parent));
                        exists.Add(parent.Id);
                    }

                    res.Add(new TypeOfRest(d) {Parent = new TypeOfRest(parent), ParentId = parent.Id});
                }
            }

            foreach (var v in res)
            {
                if (!string.IsNullOrEmpty(v.UrlToRulesOfRest) && !v.UrlToRulesOfRest.StartsWith("http:\\") &&
                    !string.IsNullOrEmpty(WebConfigurationManager.AppSettings["DownloadRuleUrl"]))
                {
                    v.UrlToRulesOfRest = string.Format(WebConfigurationManager.AppSettings["DownloadRuleUrl"],
                        v.UrlToRulesOfRest);
                }

                if (!string.IsNullOrEmpty(v.UrlToRoolAttendant) && !v.UrlToRoolAttendant.StartsWith("http:\\") &&
                    !string.IsNullOrEmpty(WebConfigurationManager.AppSettings["DownloadRuleUrl"]))
                {
                    v.UrlToRoolAttendant = string.Format(WebConfigurationManager.AppSettings["DownloadRuleUrl"],
                        v.UrlToRoolAttendant);
                }

                if (!string.IsNullOrEmpty(v.UrlToListRestriction) && !v.UrlToListRestriction.StartsWith("http:\\") &&
                    !string.IsNullOrEmpty(WebConfigurationManager.AppSettings["DownloadRuleUrl"]))
                {
                    v.UrlToListRestriction = string.Format(WebConfigurationManager.AppSettings["DownloadRuleUrl"],
                        v.UrlToListRestriction);
                }

                if (!string.IsNullOrEmpty(v.UrlToCampTypeOfCampPhoto) && !v.UrlToCampTypeOfCampPhoto.StartsWith("http:\\") &&
                    !string.IsNullOrEmpty(WebConfigurationManager.AppSettings["DownloadRuleUrl"]))
                {
                    v.UrlToCampTypeOfCampPhoto = string.Format(WebConfigurationManager.AppSettings["DownloadRuleUrl"],
                        v.UrlToCampTypeOfCampPhoto);
                }

                if (!string.IsNullOrEmpty(v.UrlToStationaryTypeOfCampPhoto) && !v.UrlToStationaryTypeOfCampPhoto.StartsWith("http:\\") &&
                    !string.IsNullOrEmpty(WebConfigurationManager.AppSettings["DownloadRuleUrl"]))
                {
                    v.UrlToStationaryTypeOfCampPhoto = string.Format(WebConfigurationManager.AppSettings["DownloadRuleUrl"],
                        v.UrlToStationaryTypeOfCampPhoto);
                }
            }

            return res.OrderBy(p => p.Id == (long) TypeOfRestEnum.Money ? 0 : 1).ThenBy(p => p.Id).ToList();
        }

        [HttpGet]
        [HttpPost]
        public List<TypeOfRest> GetTypesOfRest(bool onlyMpgu = true)
        {
            var q = UnitOfWork.GetSet<TypeOfRest>().Where(p => p.IsActive && !p.Commercial);
            if (onlyMpgu)
            {
                q = q.Where(p => p.ForMPGU);
            }

            var res =
                !onlyMpgu
                    ? q.ToList().Select(v => new TypeOfRest(v)
                            {Parent = v.Parent == null ? null : new TypeOfRest(v.Parent)})
                        .ToList()
                    : q.ToList();

            if (onlyMpgu)
            {
                // убираем промежуточные уровни видов отдыха
                var data = res;
                res = new List<TypeOfRest>();
                var hashSet = data.Select(d => d.ParentId).Where(p => p.HasValue).ToHashSet();
                var date = DateTime.Now;
                var typeOfRestId = UnitOfWork.GetSet<Tour>()
                    .Where(t => !t.TypeOfRest.Commercial && t.EndBooking >= date && t.DateOutcome >= date &&
                                !t.ForMultipleStageCompany && t.StateId.HasValue &&
                                t.StateId == StateMachineStateEnum.Tour.Formed)
                    .Select(t => t.TypeOfRestId)
                    .Where(t => t.HasValue)
                    .Distinct()
                    .ToArray();

                foreach (var d in data)
                {
                    if (d.Parent == null)
                    {
                        if (typeOfRestId.Contains(d.Id))
                        {
                            res.Add(new TypeOfRest(d));
                        }
                    }
                    else if (!hashSet.Contains(d.Id))
                    {
                        var parent = d.Parent;
                        var present = typeOfRestId.Contains(d.Id) || typeOfRestId.Contains(parent?.Id);
                        while (parent.Parent != null)
                        {
                            parent = parent.Parent;
                            present |= typeOfRestId.Contains(parent?.Id);
                        }

                        if (present)
                        {
                            res.Add(new TypeOfRest(d) {Parent = new TypeOfRest(parent), ParentId = parent.Id});
                        }
                    }
                }

                var resIds = res.Select(s => s.Id).ToArray();

                var ids = res.Select(s => s.ParentId ?? 0).Where(i => i != 0 && !resIds.Contains(i)).ToArray();

                foreach (var d in data.Where(d => ids.Contains(d.Id)).ToArray())
                {
                    if (d.Parent == null)
                    {
                        res.Add(new TypeOfRest(d));
                    }
                }
            }

            foreach (var v in res)
            {
                if (!string.IsNullOrEmpty(v.UrlToRulesOfRest) && !v.UrlToRulesOfRest.StartsWith("http:\\") &&
                    !string.IsNullOrEmpty(WebConfigurationManager.AppSettings["DownloadRuleUrl"]))
                {
                    v.UrlToRulesOfRest = string.Format(WebConfigurationManager.AppSettings["DownloadRuleUrl"],
                        v.UrlToRulesOfRest);
                }

                if (!string.IsNullOrEmpty(v.UrlToRoolAttendant) && !v.UrlToRoolAttendant.StartsWith("http:\\") &&
                    !string.IsNullOrEmpty(WebConfigurationManager.AppSettings["DownloadRuleUrl"]))
                {
                    v.UrlToRoolAttendant = string.Format(WebConfigurationManager.AppSettings["DownloadRuleUrl"],
                        v.UrlToRoolAttendant);
                }

                if (!string.IsNullOrEmpty(v.UrlToListRestriction) && !v.UrlToListRestriction.StartsWith("http:\\") &&
                    !string.IsNullOrEmpty(WebConfigurationManager.AppSettings["DownloadRuleUrl"]))
                {
                    v.UrlToListRestriction = string.Format(WebConfigurationManager.AppSettings["DownloadRuleUrl"],
                        v.UrlToListRestriction);
                }
            }

            return res.OrderBy(p => p.Id).ToList();
        }

        [HttpGet]
        [HttpPost]
        [Authorize]
        public TypeOfRest GetTypesOfRest(long id)
        {
            return UnitOfWork.GetById<TypeOfRest>(id);
        }

        [AllowAnonymous]
        [HttpPost]
        [HttpGet]
        public List<TypeOfRestBenefitRestriction> GetTypeOfRestBenefitRestrictions()
        {
            return UnitOfWork.GetSet<TypeOfRestBenefitRestriction>().ToList()
                .Select(s => new TypeOfRestBenefitRestriction(s)).ToList();
        }

        [Authorize]
        public List<YearOfRest> GetRequestCurrentPeriods()
        {
            return UnitOfWork.GetSet<YearOfRest>().ToList().OrderBy(y => y.Name).Select(y => new YearOfRest(y))
                .ToList();
        }

        /// <summary>
        ///     года кампании для заявления по виду обращения
        /// </summary>
        [Authorize]
        public List<YearOfRest> GetYearsForTypeOfRest(long? typeOfRestId = null)
        {
            return UnitOfWork.GetYearsForTypeOfRest(typeOfRestId);
        }

        [Authorize]
        public List<BenefitGroupInvalid> GetBenefitGroupInvalid()
        {
            return UnitOfWork.GetSet<BenefitGroupInvalid>().ToList();
        }

        [HttpGet]
        [HttpPost]
        public List<TimeOfRest> GetTimesOfRest(long? typeOfRestId = null, long? yearOfRestId = null)
        {
            var today = DateTime.Today;
            var year = today.Year;
            var month = today.Month;
            var day = today.Day;

            var query =
                UnitOfWork.GetSet<TimeOfRest>()
                    .Where(t => t.IsActive)
                    .Where(
                        t =>
                            t.Year == 0 || t.Year > year ||
                            t.Year == year && (t.Month > month || t.Month == month && t.DayOfMonth >= day));

            if (typeOfRestId.HasValue && typeOfRestId.Value != 0 &&
                typeOfRestId.Value != (long) TypeOfRestEnum.CommercicalAddonRequest)
            {
                var typeOfRest = UnitOfWork.GetById<TypeOfRest>(typeOfRestId.Value);

                var ids = new List<long?>();
                while (typeOfRest != null)
                {
                    ids.Add(typeOfRest.Id);
                    typeOfRest = typeOfRest.Parent;
                }

                query = query.Where(t => ids.Contains(t.TypeOfRestId));
            }

            if (yearOfRestId.HasValue && yearOfRestId.Value != 0)
            {
                query = query.Where(t => t.YearOfRestId == yearOfRestId);
            }
            else
            {
                var curYear = DateTime.Now.Year;
                query = query.Where(t =>
                    t.YearOfRest.DateFirstStage <= today && today <= t.YearOfRest.DateFirstStageClose ||
                    t.YearOfRest.Year >= curYear &&
                    t.YearOfRest.Year <= curYear + 1 && typeOfRestId == (long) TypeOfRestEnum.ChildRestFederalCamps);
            }

            return query.OrderBy(t => t.YearOfRest.Year).ThenBy(t => t.Month).ThenBy(t => t.DayOfMonth).ToList()
                .Select(t => new TimeOfRest(t, 1)).ToList();
        }


        [HttpGet]
        [HttpPost]
        public List<TimeOfRest> GetTimesOfRestWithoutFilter(long? typeOfRestId = null, long? yearOfRestId = null)
        {
            var query =
                UnitOfWork.GetSet<TimeOfRest>()
                    .Where(t => t.IsActive);

            if (typeOfRestId.HasValue && typeOfRestId.Value != 0 &&
                typeOfRestId.Value != (long) TypeOfRestEnum.CommercicalAddonRequest)
            {
                var typeOfRest = UnitOfWork.GetById<TypeOfRest>(typeOfRestId.Value);

                var ids = new List<long?>();
                while (typeOfRest != null)
                {
                    ids.Add(typeOfRest.Id);
                    typeOfRest = typeOfRest.Parent;
                }

                query = query.Where(t => ids.Contains(t.TypeOfRestId));
            }

            if (yearOfRestId.HasValue && yearOfRestId.Value != 0)
            {
                query = query.Where(t => t.YearOfRestId == yearOfRestId);
            }

            return query.OrderBy(t => t.YearOfRest.Year).ThenBy(t => t.Month).ThenBy(t => t.DayOfMonth)
                .ThenBy(t => t.Id)
                .ToList().Select(t => new TimeOfRest(t, 1)).ToList();
        }

        [HttpGet]
        [HttpPost]
        [Authorize]
        public List<BenefitApproveType> GetBenefitApproveType()
        {
            return UnitOfWork.GetAll<BenefitApproveType>().OrderBy(t => t.Id).ToList();
        }

        [HttpGet]
        [HttpPost]
        public List<SubjectOfRest> GetSubjectsOfRest()
        {
            var res = UnitOfWork.GetAll<SubjectOfRest>().Where(s => s.IsActive).OrderBy(t => t.Id).ToList()
                .Select(s => new SubjectOfRest(s)).ToList();
            foreach (var item in res)
            {
                if (!string.IsNullOrWhiteSpace(item.PhotoUrl))
                {
                    item.PhotoUrl = string.Format(WebConfigurationManager.AppSettings["UploadPhotoUrl"], item.PhotoUrl);
                }
            }

            return res;
        }

        [HttpGet]
        [HttpPost]
        [Authorize]
        public List<DocumentType> GetDocumentsType(long? typeOfRestId = null)
        {
            var items = UnitOfWork.GetAll<DocumentType>();
            var query = items
                .Where(b => !(b.TypesOfRest?.Any() ?? false) || b.TypesOfRest.Any(ss => ss.Id == typeOfRestId))
                .ToList();

            return query.OrderBy(t => t.Id).ToList();
        }

        [HttpGet]
        [HttpPost]
        [Authorize]
        public List<BenefitType> GetBenefitTypeInternal(long? typeOfRestId = null)
        {
            var query = UnitOfWork.GetAll<BenefitType>().Where(b => b.IsActive);
            if (typeOfRestId.HasValue && typeOfRestId.Value > 0 ||
                typeOfRestId == (long) TypeOfRestEnum.Compensation ||
                typeOfRestId == (long) TypeOfRestEnum.CompensationYouthRest)
            {
                query = query.Where(t => t.TypeOfRestId == typeOfRestId);
            }

            return query.Select(q => new BenefitType(q)).OrderBy(q => q.Name).ToList();
        }


        /// <summary>
        ///     для МПГУ. не использовать внутри.
        /// </summary>
        [HttpGet]
        [HttpPost]
        public List<BenefitType> GetBenefitType(long? typeOfRestId = null)
        {
            var query = UnitOfWork.GetSet<BenefitType>().Where(b => b.IsActive);
            if (typeOfRestId.HasValue)
            {
                query = query.Where(t => t.TypeOfRestId == typeOfRestId || t.TypeOfRest.ParentId == typeOfRestId);
            }

            query = query.Where(b => b.TypeOfRest.ForMPGU);

            return query.Include(b => b.TypeOfRest).ToList()
                .Select(
                    b =>
                        new BenefitType
                        {
                            Id = b.TypeOfRestId == (long) TypeOfRestEnum.ChildRestFederalCamps &&
                                 b.SameBenefitId.HasValue
                                ? b.SameBenefitId ?? b.Id
                                : b.Id,
                            Name = b.Name,
                            TypeOfRestId = b.TypeOfRestId,
                            NeedTypeOfRestriction = b.NeedTypeOfRestriction
                        })
                .ToList();
        }

        [HttpGet]
        [HttpPost]
        public List<StatusByChild> GetStatusByChild()
        {
            return UnitOfWork.GetAll<StatusByChild>().Where(s => s.IsActive).OrderBy(t => t.Id).ToList();
        }

        [HttpGet]
        [HttpPost]
        [Authorize]
        public List<DeclineReason> GetDeclineReason()
        {
            return UnitOfWork.GetAll<DeclineReason>().OrderBy(t => t.Id).ToList();
        }

        [HttpGet]
        [HttpPost]
        [Authorize]
        public List<ApplicantType> GetApplicantType()
        {
            return UnitOfWork.GetAll<ApplicantType>().Where(a => !a.IsDeleted).OrderBy(t => t.Id).ToList();
        }

        [HttpGet]
        [HttpPost]
        public List<TypeOfRestriction> GetTypeOfRestriction()
        {
            return UnitOfWork.GetAll<TypeOfRestriction>().Where(t => t.IsActive).OrderBy(t => t.Id).ToList()
                .Select(s => new TypeOfRestriction(s)).ToList();
        }

        [HttpGet]
        [HttpPost]
        [Authorize]
        public List<BtiRegion> GetRegions()
        {
            return UnitOfWork.GetAll<BtiRegion>().OrderBy(t => t.Id).ToList();
        }

        [HttpGet]
        [HttpPost]
        [Authorize]
        public List<Source> GetSources()
        {
            return UnitOfWork.GetAll<Source>().Where(s => !s.Commercial).OrderBy(t => t.Id).ToList();
        }

        [HttpGet]
        [Authorize]
        public List<NotNeedTicketReason> GetActiveNotNeedTicketReasons()
        {
            return UnitOfWork.GetAll<NotNeedTicketReason>().Where(r => r.IsActive).ToList();
        }

        [HttpGet]
        [Authorize]
        public List<MatrialStatus> GetMatrialStatuses()
        {
            return UnitOfWork.GetAll<MatrialStatus>().Where(r => r.IsActive).ToList();
        }

        [HttpGet]
        [Authorize]
        public List<MilitaryDuty> GetMilitaryDuties()
        {
            return UnitOfWork.GetAll<MilitaryDuty>().Where(r => r.IsActive).ToList();
        }

        [HttpGet]
        [Authorize]
        public List<TypeOfEducation> GetTypesOfEducation()
        {
            return UnitOfWork.GetAll<TypeOfEducation>().Where(r => r.IsActive).ToList();
        }

        [HttpGet]
        [Authorize]
        public List<Accommodation> GetAccommodation(long hotelId)
        {
            return UnitOfWork.GetAll<Accommodation>().Where(r => r.HotelId == hotelId).Select(a => new Accommodation(a))
                .ToList();
        }
    }
}
