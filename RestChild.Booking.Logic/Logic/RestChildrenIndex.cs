using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Common.Logging;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Linq;
using Lucene.Net.Linq.Fluent;
using Lucene.Net.Linq.Mapping;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using PredicateExtensions;
using RestChild.Booking.Logic.Indexing;
using RestChild.Booking.Logic.LuceneHelpers;
using RestChild.Comon;
using RestChild.Comon.Dto.SearchRestChild;
using RestChild.Comon.Enumeration;
using RestChild.DAL;
using RestChild.Domain;
using Version = Lucene.Net.Util.Version;

namespace RestChild.Booking.Logic.Logic
{
    /// <summary>
    ///     работа с индексами поиска по заявлениям
    /// </summary>
    public class RestChildrenIndex
    {
        private readonly IDocumentMapper<IndexRestChildDto> _documentMapper;
        private readonly ILog _logger;
        private readonly string IndexName = "RestChild";

        public RestChildrenIndex(ILog logger)
        {
            _documentMapper = GetDocumentMapper();
            _logger = logger;
        }

        /// <summary>
        ///     поиск детей.
        /// </summary>
        public SearchIndexResult SearchChildren(RestChildFilterDto restChildFilterDto)
        {
            LuceneConnectionFactory.RestChildWriteLock.AcquireReaderLock(5000);
            try
            {
                using (var session = LuceneConnectionFactory.GetLuceneConnection(IndexName, _documentMapper))
                {
                    var query = session.Query();
                    LuceneQueryStatistics stats = null;

                    Expression<Func<IndexRestChildDto, bool>> res = a => true;

                    if (!string.IsNullOrWhiteSpace(restChildFilterDto.RequestNumber))
                    {
                        res =
                            res.And(
                                m =>
                                    m.RequestNumber == restChildFilterDto.RequestNumber ||
                                    m.RequestNumberFromMpgu == restChildFilterDto.RequestNumber);
                    }

                    if (restChildFilterDto.YearOfRests?.Any() ?? false)
                    {
                        Expression<Func<IndexRestChildDto, bool>> resSub = a =>
                            a.YearOfRest == restChildFilterDto.YearOfRestId;
                        // ReSharper disable once LoopCanBeConvertedToQuery
                        foreach (var year in restChildFilterDto.YearOfRests
                            .Where(i => i != restChildFilterDto.YearOfRestId).ToArray())
                        {
                            resSub = resSub.Or(a => a.YearOfRest == year);
                        }

                        res = res.And(resSub);
                    }
                    else if (restChildFilterDto.YearOfRestId != 0)
                    {
                        res = res.And(m => m.YearOfRest == restChildFilterDto.YearOfRestId);
                    }

                    if (!string.IsNullOrWhiteSpace(restChildFilterDto.ApplicantFIO))
                    {
                        var items = restChildFilterDto.ApplicantFIO.Split(new[] {" "},
                            StringSplitOptions.RemoveEmptyEntries);
                        foreach (var str in items)
                        {
                            res = res.And(m => m.ApplicantFirstName.Contains(str)
                                               || m.ApplicantLastName.Contains(str)
                                               || m.ApplicantMiddleName.Contains(str));
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(restChildFilterDto.ChildFIO))
                    {
                        var items = restChildFilterDto.ChildFIO.Split(new[] {" "},
                            StringSplitOptions.RemoveEmptyEntries);
                        foreach (var str in items)
                        {
                            res = res.And(m => m.FirstName.Contains(str)
                                               || m.LastName.Contains(str)
                                               || m.MiddleName.Contains(str));
                        }
                    }

                    if (restChildFilterDto.TypeOfRest != 0)
                    {
                        var typeOfRestIds = new List<long?> {restChildFilterDto.TypeOfRest};
                        using (var unitOfWork = new UnitOfWork())
                        {
                            bool added;
                            do
                            {
                                added = false;
                                var typeOfRestsId =
                                    unitOfWork.GetSet<TypeOfRest>()
                                        .Where(t => typeOfRestIds.Contains(t.ParentId) && !typeOfRestIds.Contains(t.Id))
                                        .Select(a => (long?) a.Id)
                                        .ToList();
                                if (typeOfRestsId.Any())
                                {
                                    typeOfRestIds.AddRange(typeOfRestsId);
                                    added = true;
                                }
                            } while (added);
                        }

                        Expression<Func<IndexRestChildDto, bool>> resSub = a =>
                            a.TypeOfRestId == restChildFilterDto.TypeOfRest;
                        // ReSharper disable once LoopCanBeConvertedToQuery
                        foreach (var typeOfRest in typeOfRestIds.Where(i => i != restChildFilterDto.TypeOfRest)
                            .Where(t => t.HasValue).Select(t => t.Value).ToArray())
                        {
                            resSub = resSub.Or(a => a.TypeOfRestId == typeOfRest);
                        }

                        res = res.And(resSub);
                    }

                    if (restChildFilterDto.TimeOfRestId != 0)
                    {
                        res = res.And(m => m.TimeOfRestId == restChildFilterDto.TimeOfRestId);
                    }

                    if (restChildFilterDto.PlaceOfRest != 0)
                    {
                        res = res.And(m => m.PlaceOfRestId == restChildFilterDto.PlaceOfRest);
                    }

                    //HotelOfRest

                    if (restChildFilterDto.DistrictId != 0)
                    {
                        res.And(m => m.DistrictId == restChildFilterDto.DistrictId);
                    }

                    if (restChildFilterDto.RegionId != 0)
                    {
                        res.And(m => m.RegionId == restChildFilterDto.RegionId);
                    }

                    if (restChildFilterDto.DistrictId != 0)
                    {
                        res = restChildFilterDto.RegionId != 0
                            ? res.And(m => m.RegionId == restChildFilterDto.RegionId)
                            : res.And(m => m.DistrictId == restChildFilterDto.DistrictId);
                    }

                    if (restChildFilterDto.SourceId != 0)
                    {
                        res = res.And(m => m.SourceId == restChildFilterDto.SourceId);
                    }

                    if (restChildFilterDto.HotelId > 0)
                    {
                        res = res.And(m => m.HotelId == restChildFilterDto.HotelId);
                    }

                    if (restChildFilterDto.OperatorId != 0)
                    {
                        res = res.And(m => m.OperatorId == restChildFilterDto.OperatorId);
                    }

                    if (restChildFilterDto.BenefitApprove.HasValue)
                    {
                        res =
                            res.And(m => m.BenefitApprove == restChildFilterDto.BenefitApprove);
                    }

                    if (restChildFilterDto.TypeOfDecision != 0)
                    {
                        res =
                            res.And(m => m.TypeOfDecision == restChildFilterDto.TypeOfDecision);
                    }

                    if (restChildFilterDto.IsApprovedInInteragency.HasValue)
                    {
                        res =
                            res.And(
                                m =>
                                    m.IsApprovedInInteragency == restChildFilterDto.IsApprovedInInteragency);
                    }

                    var now = DateTime.Now.Date;
                    if (restChildFilterDto.AgeStart.HasValue && restChildFilterDto.AgeStart.Value > 0)
                    {
                        var startDate = now.AddYears(-restChildFilterDto.AgeStart.Value);
                        res = res.And(m => m.BirthDate <= startDate);
                    }

                    if (restChildFilterDto.AgeEnd.HasValue && restChildFilterDto.AgeEnd.Value > 0)
                    {
                        var endDate = now.AddYears(-(restChildFilterDto.AgeEnd.Value + 1));
                        res = res.And(m => m.BirthDate > endDate);
                    }

                    if (restChildFilterDto.BenefitTypeId.HasValue && restChildFilterDto.BenefitTypeId.Value != 0)
                    {
                        var ids = new List<long?> {restChildFilterDto.BenefitTypeId};

                        using (var uw = new UnitOfWork())
                        {
                            ids.AddRange(
                                uw.GetSet<BenefitType>()
                                    .Where(b => b.SameBenefitId == restChildFilterDto.BenefitTypeId)
                                    .Select(b => b.Id)
                                    .ToList().Select(s => (long?) s).ToList());
                        }

                        Expression<Func<IndexRestChildDto, bool>> resSub = a =>
                            a.BenefitTypeId == restChildFilterDto.BenefitTypeId;
                        // ReSharper disable once LoopCanBeConvertedToQuery
                        foreach (var typeOfRest in ids.Where(i => i != restChildFilterDto.BenefitTypeId)
                            .Where(t => t.HasValue).Select(t => t.Value).ToArray())
                        {
                            resSub = resSub.Or(a => a.BenefitTypeId == typeOfRest);
                        }

                        res = res.And(resSub);
                    }

                    if (!string.IsNullOrWhiteSpace(restChildFilterDto.TypeOfRestriction))
                    {
                        res = res.And(m => m.TypeOfRestriction.Contains(restChildFilterDto.TypeOfRestriction));
                    }

                    if (restChildFilterDto.RequestDateSupplyStart.HasValue)
                    {
                        res = res.And(m => m.RequestSupplyDate > restChildFilterDto.RequestDateSupplyStart);
                    }

                    if (restChildFilterDto.RequestDateSupplyEnd.HasValue)
                    {
                        res = res.And(m => m.RequestSupplyDate < restChildFilterDto.RequestDateSupplyEnd);
                    }

                    if (restChildFilterDto.OrganizationId != 0)
                    {
                        res = res.And(m => m.Organization == restChildFilterDto.OrganizationId);
                    }

                    if (restChildFilterDto.VedomstvoId != 0)
                    {
                        res = res.And(m => m.VedomstvoId == restChildFilterDto.VedomstvoId);
                    }

                    if (restChildFilterDto.RestCategory.HasValue)
                    {
                        query = query.Where(i => i.RestCategory == restChildFilterDto.RestCategory.Value);
                    }

                    if (restChildFilterDto.PaymentStatus.HasValue)
                    {
                        query = query.Where(i => i.PaymentStatus == restChildFilterDto.PaymentStatus);
                    }

                    var pageQuery = query.Where(res).OrderBy(r => r.ChildId)
                        .CaptureStatistics(s => { stats = s; })
                        .Skip((restChildFilterDto.PageNumber - 1) * restChildFilterDto.PageSize)
                        .Take(restChildFilterDto.PageSize);

                    var childrenPage = pageQuery.ToArray();

                    return new SearchIndexResult
                    {
                        ResManPage = childrenPage,
                        TotalRestManCount = stats.TotalHits
                    };
                }
            }
            catch (Exception e)
            {
                _logger.Error("Error during rest search", e);
                return new SearchIndexResult
                {
                    ResManPage = new List<IndexRestChildDto>(),
                    TotalRestManCount = 0
                };
            }
            finally
            {
                LuceneConnectionFactory.RestChildWriteLock.ReleaseReaderLock();
            }
        }

        public void UpdateRequestIndex(IUnitOfWork unitOfWork, long requestId)
        {
            var updateResultInfo = GetRequestsUpdateInfo(unitOfWork, requestId);
            LuceneConnectionFactory.RestChildWriteLock.AcquireWriterLock(5000);
            try
            {
                using (var session = LuceneConnectionFactory.GetLuceneConnection(IndexName, _documentMapper))
                {
                    if (updateResultInfo.RequestDeleted)
                    {
                        DeleteFromIndex(session, new[] {requestId}, "RequestId", string.Empty);
                    }
                    else
                    {
                        DeleteFromIndex(session, updateResultInfo.RestManDelete, "Key", "c");
                        DeleteFromIndex(session, updateResultInfo.RestApplicantDelete, "Key", "a");
                        AddOrUpdateIndex(session, updateResultInfo.RestManAdd);
                    }

                    session.Commit();
                }
            }
            catch (Exception e)
            {
                _logger.Error("Error during delete rest index", e);
                throw;
            }
            finally
            {
                LuceneConnectionFactory.RestChildWriteLock.ReleaseWriterLock();
            }
        }

        private static UpdateRequestInfo GetRequestsUpdateInfo(IUnitOfWork unitOfWork, long requestId)
        {
            var children = unitOfWork.GetSet<Child>()
                .Where(
                    i =>
                        i.Request != null && i.ChildList == null &&
                        i.Request.StatusId == (int) StatusEnum.CertificateIssued
                        && !i.Request.IsDeleted && !i.Request.TypeOfRest.Commercial)
                .Where(i => i.RequestId == requestId)
                .Include(t => t.Address)
                .Include(t => t.BenefitType)
                .Include(t => t.DocumentType)
                .Include(t => t.TypeOfRestriction)
                .Include(t => t.Request.Applicant)
                .Include(t => t.Request.Applicant.DocumentType)
                .Include(t => t.Request)
                .Include(t => t.Request.Status)
                .Include(t => t.Request.TypeOfRest)
                .Include(t => t.Request.Hotels)
                .Include(t => t.Request.Tour)
                .Include(t => t.Request.TimeOfRest)
                .OrderBy(i => i.Id)
                .ToArray();

            if (children.Length == 0)
            {
                return new UpdateRequestInfo
                {
                    RequestDeleted = true
                };
            }

            var result = new UpdateRequestInfo
            {
                RestManAdd = new List<IndexRestChildDto>(),
                RestManDelete = new List<long>(),
                RestApplicantDelete = new List<long>()
            };

            foreach (var child in children)
            {
                if (child.IsDeleted)
                {
                    result.RestManDelete.Add(child.Id);
                }
                else
                {
                    result.RestManAdd.Add(IndexRestChildCreator.CreateIndexRestManDto(child, child.Request));
                }
            }

            var types = new[]
            {
                (long?) TypeOfRestEnum.RestWithParents, (long?) TypeOfRestEnum.RestWithParentsPoor,
                (long?) TypeOfRestEnum.RestWithParentsComplex, (long?) TypeOfRestEnum.RestWithParentsInvalid,
                (long?) TypeOfRestEnum.RestWithParentsInvalidOrphanComplex,
                (long?) TypeOfRestEnum.RestWithParentsOrphan,
                (long?) TypeOfRestEnum.RestWithParentsOther
            };

            var attendants = unitOfWork.GetSet<Request>()
                .Where(i => i.StatusId == (int) StatusEnum.CertificateIssued && !i.IsDeleted &&
                            !i.TypeOfRest.Commercial && types.Contains(i.TypeOfRestId) && !i.RequestOnMoney)
                .Where(i => i.Id == requestId)
                .SelectMany(r => r.Attendant)
                .Where(a => !a.IsDeleted)
                .Include(t => t.Request)
                .Include(t => t.Request.Applicant)
                .Include(t => t.Request.Applicant.DocumentType)
                .Include(t => t.Request.Status)
                .Include(t => t.Request.TypeOfRest)
                .Include(t => t.Request.Hotels)
                .Include(t => t.Request.Tour)
                .Include(t => t.Request.TimeOfRest)
                .OrderBy(r => r.Id)
                .ToArray();

            foreach (var attendant in attendants)
            {
                result.RestManAdd.Add(IndexRestChildCreator.CreateIndexRestManDto(attendant, attendant.Request));
            }

            var applicantRequest = unitOfWork.GetSet<Request>()
                .Where(i => i.StatusId == (int) StatusEnum.CertificateIssued && !i.IsDeleted &&
                            !i.TypeOfRest.Commercial)
                .Where(i => i.Id == requestId)
                .Include(t => t.Applicant)
                .Include(t => t.Applicant.DocumentType)
                .Include(t => t.Status)
                .Include(t => t.TypeOfRest)
                .Include(t => t.Hotels)
                .Include(t => t.Tour)
                .Include(t => t.TimeOfRest).OrderBy(r => r.Id).ToArray();

            foreach (var request in applicantRequest)
            {
                if (request.Applicant.IsAccomp && !request.RequestOnMoney)
                {
                    result.RestManAdd.Add(IndexRestChildCreator.CreateIndexRestManDto(request.Applicant, request));
                }
                else
                {
                    result.RestApplicantDelete.Add(request.Applicant.Id);
                }
            }

            return result;
        }

        private void AddOrUpdateIndex(ISession<IndexRestChildDto> session, IEnumerable<IndexRestChildDto> records)
        {
            if (records == null)
            {
                return;
            }

            try
            {
                foreach (var indexRestChildDto in records)
                {
                    session.Add(indexRestChildDto);
                }
            }
            catch (Exception e)
            {
                _logger.Error("Error during update rest index", e);
                throw;
            }
        }

        private void DeleteFromIndex(ISession<IndexRestChildDto> session, IEnumerable<long> idsToDelete, string field,
            string keyPart)
        {
            foreach (var indexRestChildDto in idsToDelete)
            {
                var booleanQuery = new BooleanQuery();

                var query =
                    new QueryParser(Version.LUCENE_30, field, new StandardAnalyzer(LuceneConnection.LuceneVersion))
                        .Parse(
                            $"{keyPart}{indexRestChildDto}");

                booleanQuery.Add(query, Occur.MUST);
                session.Delete(query);
            }
        }

        public static IDocumentMapper<IndexRestChildDto> GetDocumentMapper()
        {
            var map = new ClassMap<IndexRestChildDto>(LuceneConnection.LuceneVersion);

            map.Key(p => p.Key);
            map.Property(p => p.AttendantId).NotAnalyzed().Stored();
            map.Property(p => p.ChildId).NotAnalyzed().Stored();
            map.Property(p => p.ApplicantId).NotAnalyzed().Stored();
            map.Property(p => p.ListOfChildrenId).Stored().NotAnalyzed();
            map.Property(p => p.ListOfChildrenName).Stored().NotAnalyzed();
            map.Property(p => p.DateIncome).CaseSensitive().AsNumericField().Stored();
            map.Property(p => p.DateOutcome).CaseSensitive().AsNumericField().Stored();

            //filter
            map.Property(p => p.DistrictId).NotAnalyzed().Stored();
            map.Property(p => p.RegionId).NotAnalyzed().Stored();
            map.Property(p => p.SourceId).NotAnalyzed().Stored();
            map.Property(p => p.OperatorId).NotAnalyzed().Stored();
            map.Property(p => p.FromNotNeedTicketReasonId).NotAnalyzed().Stored();
            map.Property(p => p.ToNotNeedTicketReasonId).NotAnalyzed().Stored();
            map.Property(p => p.FromNotNeedTicketReason).NotAnalyzed().Stored();
            map.Property(p => p.ToNotNeedTicketReason).NotAnalyzed().Stored();
            map.Property(p => p.BenefitApprove).NotAnalyzed().Stored();
            map.Property(p => p.IsApprovedInInteragency).NotAnalyzed().Stored();
            map.Property(p => p.RestCategory).NotAnalyzed().Stored();
            map.Property(p => p.PaymentStatus).NotAnalyzed().Stored();

            map.Property(p => p.Organization).NotAnalyzed().Stored();
            map.Property(p => p.OrganizationName).Stored().NotAnalyzed();
            map.Property(p => p.OrganizationShortName).Stored().NotAnalyzed();
            map.Property(p => p.VedomstvoId).NotAnalyzed().Stored();
            map.Property(p => p.VedomstvoName).Stored().NotAnalyzed();
            map.Property(p => p.VedomstvoShortName).Stored().NotAnalyzed();

            map.Property(p => p.RequestId).NotAnalyzed().Stored();
            map.Property(p => p.RequestNumber).NotAnalyzed().Stored();
            map.Property(p => p.RequestNumberFromMpgu).Stored().NotAnalyzed();
            map.Property(p => p.YearOfRest).NotAnalyzed().Stored();
            map.Property(p => p.TypeOfDecision).NotAnalyzed().Stored();

            map.Property(p => p.CertificateNumber).Stored().NotAnalyzed();
            map.Property(p => p.RequestSupplyDate).CaseSensitive().AsNumericField().Stored();
            map.Property(p => p.Status).NotAnalyzed().Stored();
            map.Property(p => p.TypeOfRestId).NotAnalyzed().Stored();
            map.Property(p => p.PlaceOfRestId).NotAnalyzed().Stored();
            map.Property(p => p.HotelId).NotAnalyzed().Stored();
            map.Property(p => p.HotelName).NotAnalyzed().Stored();
            map.Property(p => p.HotelAddress).NotAnalyzed().Stored();
            map.Property(p => p.TimeOfRestId).NotAnalyzed().Stored();
            map.Property(p => p.SubjectOfRestId).Stored().NotAnalyzed();
            //
            //Child
            map.Property(p => p.FirstName).Analyzed().Stored();
            map.Property(p => p.LastName).Analyzed().Stored();
            map.Property(p => p.MiddleName).Analyzed().Stored();
            map.Property(p => p.Male).Stored().NotAnalyzed();
            map.Property(p => p.BirthDate).CaseSensitive().AsNumericField().Stored();
            map.Property(p => p.Age).Stored().AsNumericField();
            map.Property(p => p.PlaceOfBirth).Stored().NotAnalyzed();
            map.Property(p => p.DocumentType).Stored().NotAnalyzed();
            map.Property(p => p.DocumentSeria).Stored().NotAnalyzed();
            map.Property(p => p.DocumentNumber).Stored().NotAnalyzed();
            map.Property(p => p.DocumentIssueDate).Stored().NotAnalyzed();
            map.Property(p => p.DocumentSubjectIssue).Stored().NotAnalyzed();
            map.Property(p => p.TypeOfRestriction).NotAnalyzed().Stored(); //категория здоровья
            map.Property(p => p.BenefitTypeId).NotAnalyzed().Stored(); //льготная категория
            map.Property(p => p.Address).Stored().Analyzed();
            //
            //Applicant
            map.Property(p => p.ApplicantId).Stored().NotAnalyzed();
            map.Property(p => p.ApplicantLastName).Stored().Analyzed();
            map.Property(p => p.ApplicantFirstName).Stored().Analyzed();
            map.Property(p => p.ApplicantMiddleName).Stored().Analyzed();
            map.Property(p => p.ApplicantDocumentTypeId).Stored().NotAnalyzed();
            map.Property(p => p.ApplicantDocumentType).Stored().NotAnalyzed();
            map.Property(p => p.ApplicantDocumentSeria).Stored().NotAnalyzed();
            map.Property(p => p.ApplicantDocumentNumber).Stored().NotAnalyzed();
            map.Property(p => p.ApplicantPhone).Stored().NotAnalyzed();
            map.Property(p => p.ApplicantEmail).Stored().NotAnalyzed();
            map.Property(p => p.ApplicantBirthDate).CaseSensitive().AsNumericField().Stored();
            //
            return map.ToDocumentMapper();
        }

        public void UpdateListChildIndex(UnitOfWork unitOfWork, long childListId)
        {
            var updateInfo = GetChildListUpdateInfo(unitOfWork, childListId);
            LuceneConnectionFactory.RestChildWriteLock.AcquireWriterLock(5000);
            try
            {
                using (var session = LuceneConnectionFactory.GetLuceneConnection(IndexName, _documentMapper))
                {
                    if (updateInfo.ChildListDelete)
                    {
                        DeleteFromIndex(session, new[] {childListId}, "ListOfChildId", string.Empty);
                    }

                    AddOrUpdateIndex(session, updateInfo.RestManAdd);

                    session.Commit();
                }
            }
            catch (Exception e)
            {
                _logger.Error("Error during delete rest index", e);
                throw;
            }
            finally
            {
                LuceneConnectionFactory.RestChildWriteLock.ReleaseWriterLock();
            }
        }

        private UpdateChildListInfo GetChildListUpdateInfo(UnitOfWork unitOfWork, long childListId)
        {
            var children = unitOfWork.GetSet<Child>()
                .Where(
                    i =>
                        i.ChildList != null &&
                        (i.ChildList.StateId == StateMachineStateEnum.Limit.List.Formed ||
                         i.ChildList.StateId == StateMachineStateEnum.Limit.List.IncludedInTour ||
                         i.ChildList.StateId == StateMachineStateEnum.Limit.List.IncludedPayment) &&
                        i.ChildList.LimitOnOrganization.StateId == StateMachineStateEnum.Limit.Organization.Confirmed &&
                        i.ChildList.LimitOnOrganization.LimitOnVedomstvo.StateId ==
                        StateMachineStateEnum.Limit.Oiv.Brought &&
                        new long?[]
                        {
                            StateMachineStateEnum.Tour.Formed, StateMachineStateEnum.Tour.ToFormationFromFormed
                        }.Contains(
                            i.ChildList.LimitOnOrganization.Tour.StateId))
                .Where(i => i.ChildListId == childListId)
                .Include(t => t.Address)
                .Include(t => t.BenefitType)
                .Include(t => t.DocumentType)
                .Include(t => t.TypeOfRestriction)
                .Include(t => t.Applicant)
                .Include(t => t.Applicant.DocumentType)
                .Include(t => t.ChildList.LimitOnOrganization.Organization.Vedomstvo)
                .Include(t => t.ChildList.LimitOnOrganization.Tour.Hotels.PlaceOfRest)
                .Include(t => t.ChildList.LimitOnOrganization.Tour.TimeOfRest)
                .ToArray();

            var departments = unitOfWork.GetSet<Organization>()
                .Where(i => !i.ParentId.HasValue).ToArray().ToDictionary(i => i.Id);
            LuceneConnectionFactory.RestChildWriteLock.AcquireWriterLock(5000);
            try
            {
                using (var session = LuceneConnectionFactory.GetLuceneConnection(IndexName, _documentMapper))
                {
                    var query = session.Query();
                    Expression<Func<IndexRestChildDto, bool>> res = a => true;

                    res = res.And(t => t.ListOfChildrenId == childListId);

                    var indexListOfChildren = query.Where(res).ToArray()
                        .GroupBy(i => i.ListOfChildrenId, i => i)
                        .ToDictionary(i => i.Key, i => i);

                    var result = new UpdateChildListInfo
                    {
                        RestManAdd = new List<IndexRestChildDto>()
                    };

                    if (indexListOfChildren.Count != 0)
                    {
                        //удаляем весь лист и добавляем всех его детей заново, можно переделать, если будут проблемы
                        result.ChildListDelete = true;
                    }

                    foreach (var child in children)
                    {
                        result.RestManAdd.Add(IndexRestChildCreator.CreateIndexRestManDto(child,
                            child.ChildList,
                            departments));
                    }

                    return result;
                }
            }
            finally
            {
                LuceneConnectionFactory.RestChildWriteLock.ReleaseWriterLock();
            }
        }

        public void RebuildIndex()
        {
            using (var uw = new UnitOfWork())
            {
                LuceneConnectionFactory.RestChildWriteLock.AcquireWriterLock(50000);
                try
                {
                    using (var session = LuceneConnectionFactory.GetLuceneConnection("RestChild", _documentMapper))
                    {
                        session.DeleteAll();
                        session.Commit();
                    }
                }
                finally
                {
                    LuceneConnectionFactory.RestChildWriteLock.ReleaseWriterLock();
                }

                uw.Context.Database.CommandTimeout = 1800;

                var count = 500;

                var children = uw.GetSet<Child>()
                    .Where(
                        i =>
                            i.Request != null && i.ChildList == null &&
                            i.Request.StatusId == (int) StatusEnum.CertificateIssued &&
                            !i.Request.IsDeleted && !i.IsDeleted && !i.Request.TypeOfRest.Commercial)
                    .Include(t => t.Address)
                    .Include(t => t.BenefitType)
                    .Include(t => t.DocumentType)
                    .Include(t => t.TypeOfRestriction)
                    .Include(t => t.Request.Applicant)
                    .Include(t => t.Request.Applicant.DocumentType)
                    .Include(t => t.Request)
                    .Include(t => t.Request.Status)
                    .Include(t => t.Request.TypeOfRest)
                    .Include(t => t.Request.Hotels)
                    .Include(t => t.Request.Tour)
                    .Include(t => t.Request.TimeOfRest).OrderBy(i => i.Id);

                var departments = uw.GetSet<Organization>()
                    .Where(i => !i.ParentId.HasValue).ToArray().ToDictionary(i => i.Id);

                var j = 0;
                var sum = 0;
                var cnt = children.Count();
                _logger.Info("Started requests");
                while (sum < cnt)
                {
                    var data = children.Skip(j++ * count).Take(count).ToArray();
                    var indexRestChildDto = data.Select(i => IndexRestChildCreator.CreateIndexRestManDto(i, i.Request))
                        .ToArray();

                    foreach (var entry in uw.Context.ChangeTracker.Entries())
                    {
                        entry.State = EntityState.Detached;
                    }

                    LuceneConnectionFactory.RestChildWriteLock.AcquireWriterLock(50000);
                    try
                    {
                        using (var session = LuceneConnectionFactory.GetLuceneConnection("RestChild", _documentMapper))
                        {
                            session.Add(indexRestChildDto);
                            session.Commit();
                        }
                    }
                    finally
                    {
                        LuceneConnectionFactory.RestChildWriteLock.ReleaseWriterLock();
                    }

                    sum += count;
                }

                _logger.Info($"Requests commit finished {sum}");

                var applicantGoing = uw.GetSet<Request>()
                    .Where(
                        i =>
                            i.StatusId == (int) StatusEnum.CertificateIssued &&
                            !i.IsDeleted && i.Applicant.IsAccomp && !i.TypeOfRest.Commercial &&
                            !i.RequestOnMoney)
                    .Include(t => t.Applicant)
                    .Include(t => t.Applicant.DocumentType)
                    .Include(t => t.Status)
                    .Include(t => t.TypeOfRest)
                    .Include(t => t.Hotels)
                    .Include(t => t.Tour)
                    .Include(t => t.TimeOfRest).OrderBy(r => r.Id);

                j = 0;
                sum = 0;
                cnt = applicantGoing.Count();

                _logger.Info("Started applicants");
                while (sum < cnt)
                {
                    var data = applicantGoing.Skip(j++ * count).Take(count).ToArray();
                    var indexRestChildDto =
                        data.Select(i => IndexRestChildCreator.CreateIndexRestManDto(i.Applicant, i)).ToArray();

                    foreach (var entry in uw.Context.ChangeTracker.Entries())
                    {
                        entry.State = EntityState.Detached;
                    }

                    LuceneConnectionFactory.RestChildWriteLock.AcquireWriterLock(50000);
                    try
                    {
                        using (var session = LuceneConnectionFactory.GetLuceneConnection("RestChild", _documentMapper))
                        {
                            session.Add(indexRestChildDto);
                            session.Commit();
                        }
                    }
                    finally
                    {
                        LuceneConnectionFactory.RestChildWriteLock.ReleaseWriterLock();
                    }

                    sum += count;
                }

                _logger.Info($"Commit finished {sum}");
                var types = new[]
                {
                    (long?) TypeOfRestEnum.RestWithParents, (long?) TypeOfRestEnum.RestWithParentsPoor,
                    (long?) TypeOfRestEnum.RestWithParentsComplex, (long?) TypeOfRestEnum.RestWithParentsInvalid,
                    (long?) TypeOfRestEnum.RestWithParentsInvalidOrphanComplex,
                    (long?) TypeOfRestEnum.RestWithParentsOrphan,
                    (long?) TypeOfRestEnum.RestWithParentsOther
                };
                var attendants = uw.GetSet<Request>()
                    .Where(
                        i =>
                            i.StatusId == (int) StatusEnum.CertificateIssued && !i.RequestOnMoney &&
                            !i.IsDeleted && !i.TypeOfRest.Commercial && types.Contains(i.TypeOfRestId))
                    .SelectMany(r => r.Attendant)
                    .Where(a => !a.IsDeleted)
                    .Include(t => t.Request)
                    .Include(t => t.Request.Applicant)
                    .Include(t => t.Request.Applicant.DocumentType)
                    .Include(t => t.Request.Status)
                    .Include(t => t.Request.TypeOfRest)
                    .Include(t => t.Request.Hotels)
                    .Include(t => t.Request.Tour)
                    .Include(t => t.Request.TimeOfRest).OrderBy(r => r.Id);

                j = 0;
                sum = 0;
                cnt = attendants.Count();
                _logger.Info("Started Attendant");
                while (sum < cnt)
                {
                    var data = attendants.Skip(j++ * count).Take(count).ToArray();
                    var indexRestChildDto = data.Select(i => IndexRestChildCreator.CreateIndexRestManDto(i, i.Request))
                        .ToArray();

                    foreach (var entry in uw.Context.ChangeTracker.Entries())
                    {
                        entry.State = EntityState.Detached;
                    }

                    LuceneConnectionFactory.RestChildWriteLock.AcquireWriterLock(50000);
                    try
                    {
                        using (var session = LuceneConnectionFactory.GetLuceneConnection("RestChild", _documentMapper))
                        {
                            session.Add(indexRestChildDto);
                            session.Commit();
                        }
                    }
                    finally
                    {
                        LuceneConnectionFactory.RestChildWriteLock.ReleaseWriterLock();
                    }

                    sum += count;
                }

                _logger.Info($"CommitFinished {sum}");

                children = uw.GetSet<Child>()
                    .Where(
                        i =>
                            i.ChildList != null && !i.IsDeleted && !i.ChildList.IsDeleted
                            && (i.ChildList.StateId == StateMachineStateEnum.Limit.List.Formed ||
                                i.ChildList.StateId == StateMachineStateEnum.Limit.List.IncludedInTour ||
                                i.ChildList.StateId == StateMachineStateEnum.Limit.List.IncludedPayment) &&
                            i.ChildList.LimitOnOrganization.StateId ==
                            StateMachineStateEnum.Limit.Organization.Confirmed &&
                            i.ChildList.LimitOnOrganization.LimitOnVedomstvo.StateId ==
                            StateMachineStateEnum.Limit.Oiv.Brought &&
                            new long?[]
                            {
                                StateMachineStateEnum.Tour.Formed, StateMachineStateEnum.Tour.ToFormationFromFormed
                            }.Contains(
                                i.ChildList.LimitOnOrganization.Tour.StateId))
                    .Include(t => t.Address)
                    .Include(t => t.BenefitType)
                    .Include(t => t.DocumentType)
                    .Include(t => t.TypeOfRestriction)
                    .Include(t => t.Applicant)
                    .Include(t => t.Applicant.DocumentType)
                    .Include(t => t.ChildList.LimitOnOrganization.Organization.Vedomstvo)
                    .Include(t => t.ChildList.LimitOnOrganization.Tour.Hotels.PlaceOfRest)
                    .Include(t => t.ChildList.LimitOnOrganization.Tour.TimeOfRest)
                    .OrderBy(c => c.Id);

                _logger.Info("Started Lists");
                j = 0;
                sum = 0;
                cnt = children.Count();
                while (sum < cnt)
                {
                    var indexRestChildDto =
                        children.Skip(j++ * count)
                            .Take(count)
                            .ToArray()
                            .Select(i => IndexRestChildCreator.CreateIndexRestManDto(i, i.ChildList, departments))
                            .ToArray();

                    foreach (var entry in uw.Context.ChangeTracker.Entries())
                    {
                        entry.State = EntityState.Detached;
                    }

                    LuceneConnectionFactory.RestChildWriteLock.AcquireWriterLock(50000);
                    try
                    {
                        using (var session = LuceneConnectionFactory.GetLuceneConnection("RestChild", _documentMapper))
                        {
                            session.Add(indexRestChildDto);
                            session.Commit();
                        }
                    }
                    finally
                    {
                        LuceneConnectionFactory.RestChildWriteLock.ReleaseWriterLock();
                    }

                    sum += count;
                }

                _logger.Info($"CommitFinished {sum}");

                var teachers = uw.GetSet<Applicant>()
                    .Where(
                        i =>
                            i.ChildList != null && !i.IsDeleted && !i.ChildList.IsDeleted
                            && (i.ChildList.StateId == StateMachineStateEnum.Limit.List.Formed ||
                                i.ChildList.StateId == StateMachineStateEnum.Limit.List.IncludedInTour ||
                                i.ChildList.StateId == StateMachineStateEnum.Limit.List.IncludedPayment) &&
                            i.ChildList.LimitOnOrganization.StateId ==
                            StateMachineStateEnum.Limit.Organization.Confirmed &&
                            i.ChildList.LimitOnOrganization.LimitOnVedomstvo.StateId ==
                            StateMachineStateEnum.Limit.Oiv.Brought &&
                            new long?[]
                            {
                                StateMachineStateEnum.Tour.Formed, StateMachineStateEnum.Tour.ToFormationFromFormed
                            }.Contains(
                                i.ChildList.LimitOnOrganization.Tour.StateId))
                    .Include(t => t.DocumentType)
                    .Include(t => t.ChildList.LimitOnOrganization.Organization.Vedomstvo)
                    .Include(t => t.ChildList.LimitOnOrganization.Tour.Hotels.PlaceOfRest)
                    .Include(t => t.ChildList.LimitOnOrganization.Tour.TimeOfRest)
                    .OrderBy(c => c.Id);

                _logger.Info("Started teachers");
                j = 0;
                sum = 0;
                cnt = teachers.Count();
                while (sum < cnt)
                {
                    var indexRestChildDto =
                        teachers.Skip(j++ * count)
                            .Take(count)
                            .ToArray()
                            .Select(i => IndexRestChildCreator.CreateIndexRestManDto(i, i.ChildList, departments))
                            .ToArray();

                    foreach (var entry in uw.Context.ChangeTracker.Entries())
                    {
                        entry.State = EntityState.Detached;
                    }

                    LuceneConnectionFactory.RestChildWriteLock.AcquireWriterLock(50000);
                    try
                    {
                        using (var session = LuceneConnectionFactory.GetLuceneConnection("RestChild", _documentMapper))
                        {
                            session.Add(indexRestChildDto);
                            session.Commit();
                        }
                    }
                    finally
                    {
                        LuceneConnectionFactory.RestChildWriteLock.ReleaseWriterLock();
                    }

                    sum += count;
                }

                _logger.Info($"Teachers commit finished {sum}");
            }
        }


        public class UpdateRequestInfo
        {
            public IList<IndexRestChildDto> RestManAdd { get; set; }
            public IList<long> RestManDelete { get; set; }
            public IList<long> RestApplicantDelete { get; set; }
            public bool RequestDeleted { get; set; }
        }

        public class UpdateChildListInfo
        {
            public bool ChildListDelete { get; set; }
            public IList<IndexRestChildDto> RestManAdd { get; set; }
        }
    }
}
