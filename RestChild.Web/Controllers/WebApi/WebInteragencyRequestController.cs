using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using RestChild.Comon;
using RestChild.Comon.Config;
using RestChild.Comon.Enumeration;
using RestChild.DAL.RepositoryExtensions;
using RestChild.Domain;
using RestChild.Web.Models;

namespace RestChild.Web.Controllers.WebApi
{
    /// <summary>
    /// межведомственные запросы
    /// </summary>
    [Authorize]
    public class WebInteragencyRequestController : BaseController
    {
        public WebFirstRequestCompanyController RequestController { get; set; }

        public override void SetUnitOfWorkInRefClass(IUnitOfWork unitOfWork)
        {
            base.SetUnitOfWorkInRefClass(unitOfWork);
            RequestController.SetUnitOfWorkInRefClass(unitOfWork);
        }

        public InteragencyRequest Get(long id)
        {
            return UnitOfWork.GetById<InteragencyRequest>(id);
        }


        /// <summary>
        ///     статусы запроса
        /// </summary>
        public IList<StatusInteragencyRequest> GetStatuses()
        {
            return UnitOfWork.GetSet<StatusInteragencyRequest>().OrderBy(s => s.Id).ToList();
        }

        /// <summary>
        ///     статусы результата
        /// </summary>
        public IList<StatusResult> GetStatusResults()
        {
            return UnitOfWork.GetSet<StatusResult>().OrderBy(s => s.Id).ToList();
        }


        public IList<InteragencyRequestResultViewModel> GetRequestResults(long id)
        {
            IQueryable<InteragencyRequestResult> q =
                UnitOfWork.GetSet<InteragencyRequestResult>().Where(r => r.InteragencyRequestId == id);
            Dictionary<long?, Child> children =
                UnitOfWork.GetSet<Child>()
                    .Where(c => q.Select(r => r.ChildId).Contains(c.Id) && c.Request.IsLast)
                    .ToDictionary(c => (long?) (c.Id), c => c);
            children.Add(0, null);

            return q.ToList().Select(c => new InteragencyRequestResultViewModel(c)
            {
                Child = children[c.ChildId ?? 0]
            }).ToList();
        }

        public IList<InteragencyRequestResultViewModel> GetRequestResultsForChild(long id)
        {
            var child = UnitOfWork.GetById<Child>(id);
            return
                UnitOfWork.GetSet<InteragencyRequestResult>()
                    .Where(r => r.ChildId == child.EntityId)
                    .ToList()
                    .Select(s => new InteragencyRequestResultViewModel(s)
                    {
                        Child = child
                    }).ToList();
        }

        [HttpPost]
        public InteragencyRequest Save(InteragencyRequestViewModel requestVm)
        {
            bool newRequest = false;
            SetUnitOfWorkInRefClass(UnitOfWork);

            if (!Security.HasRight(AccessRightEnum.InteragencyRequestManage))
            {
                return null;
            }

            InteragencyRequest request = requestVm.BuildData();
            try
            {
                using (var tran = UnitOfWork.GetTransactionScope())
                {
                    request.CreateDate = DateTime.Now;
                    request.AccountId = Security.GetCurrentAccountId();
                    requestVm.CheckModel();

                    if (request.BtiRegionId <= 0)
                    {
                        request.ForAllRegion = request.BtiRegionId < 0;
                        request.BtiRegionId = null;
                    }
                    else
                    {
                        request.ForAllRegion = false;
                    }

                    InteragencyRequest model;
                    if (request.Id > 0)
                    {
                        model = UnitOfWork.GetById<InteragencyRequest>(request.Id);

                        model.RequestNumber = request.RequestNumber;
                        model.RequsetDate = request.RequsetDate;
                        model.RequestComment = request.RequestComment;
                        model.RequestFileUrl = request.RequestFileUrl;

                        model.AnswerNumber = request.AnswerNumber;
                        model.AnswerDate = request.AnswerDate;
                        model.AnswerComment = request.AnswerComment;
                        model.AnswerFileUrl = request.AnswerFileUrl;

                        model.CreateDate = request.CreateDate;
                        model.AccountId = request.AccountId;

                        model.IsSecondaryRequest = request.IsSecondaryRequest;

                        model.OrganizationId = request.OrganizationId;


                        List<BenefitType> unselectedBenefits =
                            requestVm.BenefitTypes.Where(x => !x.IsChecked).Select(x => x.Data).ToList();
                        List<InteragencyRequestBenefitType> linksForDelete =
                            UnitOfWork.GetSet<InteragencyRequestBenefitType>()
                                .Where(x => x.InteragencyRequestId == request.Id).ToList();
                        foreach (
                            InteragencyRequestBenefitType link in
                            linksForDelete.Where(x => unselectedBenefits.Any(y => y.Id == x.BenefitTypeId)))
                        {
                            UnitOfWork.Delete(link);
                        }

                        foreach (var link in requestVm.BenefitTypes.Where(x => x.IsChecked))
                        {
                            UnitOfWork.AddEntity(new InteragencyRequestBenefitType
                            {
                                InteragencyRequestId = request.Id,
                                BenefitTypeId = link.Data.Id
                            });
                        }


                        UnitOfWork.AddEntity(new HistoryInteragencyRequest
                        {
                            AccountId = Security.GetCurrentAccountId(),
                            Operation = "Сохранение межведомственного запроса",
                            OperationDate = DateTime.Now,
                            InteragencyRequestId = model.Id
                        });
                    }
                    else
                    {
                        newRequest = true;
                        var benefitTypesIds = requestVm.BenefitTypes.Where(t => t.IsChecked).Select(t => t.Data.Id);
                        var persistedBenefitTypes = UnitOfWork.GetSet<BenefitType>()
                            .Where(t => benefitTypesIds.Contains(t.Id))
                            .Join(UnitOfWork.GetSet<BenefitType>(), b => b.Name, b => b.Name, (o, i) => i);

                        foreach (var link in persistedBenefitTypes)
                        {
                            UnitOfWork.GetSet<InteragencyRequestBenefitType>()
                                .Add(new InteragencyRequestBenefitType
                                    {InteragencyRequestId = request.Id, BenefitTypeId = link.Id});
                        }

                        model = UnitOfWork.AddEntity(request);
                        model.StatusInteragencyRequestId = (long) StatusInteragencyRequestEnum.Draft;
                        UnitOfWork.AddEntity(new HistoryInteragencyRequest
                        {
                            AccountId = Security.GetCurrentAccountId(),
                            Operation = "Добавление нового межведомственного запроса",
                            OperationDate = DateTime.Now,
                            InteragencyRequestId = model.Id
                        });
                    }

                    model.CreateDate = DateTime.Now;
                    model.AccountId = Security.GetCurrentAccountId();

                    IEnumerable<InteragencyRequestResult> results = requestVm.Results.Select(r => r.BuildData());

                    IList<InteragencyRequestResult> interagencyRequestResults =
                        results as IList<InteragencyRequestResult> ??
                        results.ToList();
                    long?[] childId = interagencyRequestResults.Select(c => c.ChildId).ToArray();
                    Dictionary<long, Child> children = UnitOfWork.GetSet<Child>()
                        .Where(c => childId.Contains(c.Id))
                        .ToDictionary(c => c.Id, c => c);

                    List<long?> entityIds = children.Values.Select(c => c.EntityId).ToList();

                    children.Add(0, null);

                    var benefitTypesFromRequest =
                        requestVm.BenefitTypes.Where(bt => bt.IsChecked).Select(bt => bt.NullSafe(x => x.Data.Id))
                            .ToList();
                    var allBenefitTypesQuery = UnitOfWork.GetSet<BenefitType>();
                    var benefitTypes =
                        UnitOfWork.GetSet<BenefitType>()
                            .Where(b => benefitTypesFromRequest.Contains(b.Id))
                            .Join(allBenefitTypesQuery, b => b.Name, b => b.Name, (o, i) => i.Id).ToList();

                    // Последние версии данных о детях
                    Dictionary<long, Child> lastVersionsOfChildren = UnitOfWork.GetSet<Child>()
                        .Where(c => entityIds.Contains(c.Id))
                        .ToDictionary(c => c.Id, c => c);
                    lastVersionsOfChildren.Add(0, null);

                    if (request.StatusInteragencyRequestId != (long) StatusInteragencyRequestEnum.Draft &&
                        (requestVm.IsValid ?? false))
                    {
                        model.StatusInteragencyRequestId = request.StatusInteragencyRequestId;
                    }

                    foreach (InteragencyRequestResult interagencyRequestResult in interagencyRequestResults)
                    {
                        Child child = children[interagencyRequestResult.ChildId ?? 0];

                        if (newRequest)
                        {
                            if (!benefitTypes.Contains(child.NullSafe(c => c.BenefitTypeId ?? 0)))
                            {
                                continue;
                            }

                            if (!request.ForAllRegion)
                            {
                                if (request.BtiRegionId.HasValue &&
                                    child.NullSafe(c => c.Address.BtiRegionId) != request.BtiRegionId &&
                                    child.NullSafe(c => c.Address.BtiAddress.BtiRegionId) != request.BtiRegionId)
                                {
                                    continue;
                                }

                                if (!request.BtiRegionId.HasValue &&
                                    (child.NullSafe(c => c.Address.BtiRegionId) != null ||
                                     child.NullSafe(c => c.Address.BtiAddress.BtiRegionId) != null))
                                {
                                    continue;
                                }
                            }
                        }


                        interagencyRequestResult.ChildId = child.EntityId;
                        Child childFirst = lastVersionsOfChildren[child.EntityId ?? child.Id];
                        childFirst.IsIncludeInInteragency =
                            childFirst.IsIncludeInInteragency || !requestVm.Data.IsSecondaryRequest ||
                            childFirst.IsIncludeInInteragency;
                        child.IsIncludeInInteragency = child.IsIncludeInInteragency ||
                                                       !requestVm.Data.IsSecondaryRequest ||
                                                       childFirst.IsIncludeInInteragency;
                        childFirst.IsIncludeInInteragencySecondary =
                            childFirst.IsIncludeInInteragencySecondary || requestVm.Data.IsSecondaryRequest;
                        child.IsIncludeInInteragencySecondary =
                            child.IsIncludeInInteragencySecondary || requestVm.Data.IsSecondaryRequest;

                        if (interagencyRequestResult.Id > 0)
                        {
                            var data = UnitOfWork.GetById<InteragencyRequestResult>(interagencyRequestResult.Id);
                            data.InteragencyRequestId = model.Id;
                            data.StatusResultId = (interagencyRequestResult.StatusResultId ?? 0) > 0
                                ? interagencyRequestResult.StatusResultId
                                : null;

                            childFirst.IsApprovedInInteragency = !requestVm.Data.IsSecondaryRequest &&
                                                                 model.StatusInteragencyRequestId ==
                                                                 (long) StatusInteragencyRequestEnum.Answered
                                ? interagencyRequestResult.StatusResultId == (long) StatusResultEnum.Approved
                                : childFirst.IsApprovedInInteragency;

                            childFirst.IsApprovedInInteragencySecondary = requestVm.Data.IsSecondaryRequest &&
                                                                          model.StatusInteragencyRequestId ==
                                                                          (long) StatusInteragencyRequestEnum.Answered
                                ? interagencyRequestResult.StatusResultId == (long) StatusResultEnum.Approved
                                : childFirst.IsApprovedInInteragencySecondary;

                            data.Comment = interagencyRequestResult.Comment;
                            data.ChildId = interagencyRequestResult.ChildId;
                        }
                        else
                        {
                            interagencyRequestResult.InteragencyRequestId = model.Id;
                            UnitOfWork.AddEntity(interagencyRequestResult);
                        }
                    }

                    UnitOfWork.SaveChanges();

                    if (request.StatusInteragencyRequestId != (long) StatusInteragencyRequestEnum.Draft &&
                        (requestVm.IsValid ?? false))
                    {
                        if (model.StatusInteragencyRequestId == (long) StatusInteragencyRequestEnum.Answered)
                        {
                            List<long?> list =
                                interagencyRequestResults
                                    .Where(r => r.StatusResultId == (long) StatusResultEnum.Rejected)
                                    .Select(c => c.ChildId)
                                    .ToList();
                            var requests =
                                UnitOfWork.GetSet<Request>()
                                    .Where(
                                        r =>
                                            r.IsLast && !r.IsDeleted && r.Child.Any(c => list.Contains(c.EntityId)) &&
                                            new[]
                                                    {(long?) StatusEnum.WaitApplicant, (long?) StatusEnum.ApplicantCome}
                                                .Contains(r.StatusId)).Select(r =>
                                        new {RequestId = r.Id, TypeOfRest = r.Id})
                                    .ToList();
                            foreach (var res in requests)
                            {
                                RequestController.RequestChangeStatusWithCreateVersion(res.RequestId,
                                    AccessRightEnum.Status.ToReject,
                                    DeclineSectionProcess.GetDeclineReason("BadDocuments", res.TypeOfRest) ??
                                    Properties.Settings.Default.BadDocuments);
                            }

                            list =
                                interagencyRequestResults
                                    .Where(r => r.StatusResultId == (long) StatusResultEnum.Approved)
                                    .Select(c => c.ChildId)
                                    .ToList();
                            List<Request> requestsModel =
                                UnitOfWork.GetSet<Request>()
                                    .Where(r => r.IsLast && !r.IsDeleted &&
                                                r.Child.Any(c => list.Contains(c.EntityId)) &&
                                                new[]
                                                {
                                                    (long?) StatusEnum.WaitApplicant, (long?) StatusEnum.ApplicantCome
                                                }.Contains(r.StatusId)
                                    ).ToList();

                            foreach (Request r in requestsModel)
                            {
                                if (
                                    !r.Child.Any(
                                        c =>
                                            c.BenefitTypeId.HasValue &&
                                            (c.BenefitApproveTypeId != (long) BenefitApproveTypeEnum.ApprovedByBr &&
                                             !((c.Entity.IsApprovedInInteragency ?? false) ||
                                               (c.Entity.IsApprovedInInteragencySecondary ?? false)))) &&
                                    (
                                        r.StatusId == (long?) StatusEnum.ApplicantCome ||
                                        (r.AgentApplicant ?? false) ||
                                        !r.Child.Any(c =>
                                            c.BenefitTypeId.HasValue && c.BenefitApproveTypeId ==
                                            (long) BenefitApproveTypeEnum.ApprovedByBr)
                                    ))
                                {
                                    RequestController.RequestChangeStatusWithCreateVersion(r.Id,
                                        AccessRightEnum.Status.CertificateIssued);
                                }
                            }
                        }

                        UnitOfWork.SaveChanges();
                    }

                    tran.Complete();
                    return model;
                }
            }
            catch
            {
                UnitOfWork.RollBack();
                return null;
            }
        }


        [HttpGet]
        public ICollection<CheckBoxViewModel<BenefitType>> GetBenefitTypesForRegion(long? regionId, bool allRegions)
        {
            var allChildren = GetAllChildren(false);
            if (!allRegions)
            {
                if (regionId == 0)
                {
                    regionId = null;
                }

                if (regionId.HasValue)
                {
                    allChildren = allChildren.Where(a =>
                        a.Address.BtiRegionId == regionId || a.Address.BtiAddress.BtiRegionId == regionId);
                }
                else
                {
                    allChildren = allChildren.Where(a =>
                        a.Address.BtiRegionId == null && a.Address.BtiAddress.BtiRegionId == null);
                }
            }

            return allChildren.Select(c => c.BenefitType).ToList().Select(c => new BenefitType(c)).GroupBy(c => c.Name)
                .Select(c => new CheckBoxViewModel<BenefitType>()
                    {Data = c.FirstOrDefault(), Description = "(детей: " + c.Count() + ")"}).ToList();
        }

        internal IQueryable<Child> GetAllChildren(bool createSecondaryRequest)
        {
            IQueryable<Child> allChildren =
                UnitOfWork.GetSet<Child>()
                    .Where(
                        c =>
                            c.Request.IsLast && !c.Request.IsDeleted && c.BenefitTypeId.HasValue
                            && !c.BaseRegistryInfo.Any(a =>
                                a.IsProcessed && !a.NotActual && a.Success && a.ExchangeBaseRegistryTypeId ==
                                (long) ExchangeBaseRegistryTypeEnum.Benefit)
                            && c.BaseRegistryInfo.Any(a =>
                                a.IsProcessed && !a.NotActual && a.ExchangeBaseRegistryTypeId ==
                                (long) ExchangeBaseRegistryTypeEnum.Benefit)
                            && !c.BaseRegistryInfo.Any(a =>
                                a.IsProcessed && !a.NotActual && a.Success && a.ExchangeBaseRegistryTypeId ==
                                (long) ExchangeBaseRegistryTypeEnum.CpmpkExchange));

            if (createSecondaryRequest)
            {
                allChildren =
                    allChildren.Where(c =>
                            new[] {(long) StatusEnum.ApplicantCome, (long) StatusEnum.Ranging}.Contains(
                                c.Request.StatusId.Value) ||
                            (c.Request.StatusId == (long) StatusEnum.Send &&
                             c.Request.SourceId == (long) SourceEnum.Operator))
                        .Where(c => !c.Entity.IsIncludeInInteragencySecondary);
            }
            else
            {
                allChildren =
                    allChildren.Where(c =>
                            new[] {(long) StatusEnum.ApplicantCome, (long) StatusEnum.Ranging}.Contains(
                                c.Request.StatusId.Value) ||
                            (c.Request.StatusId == (long) StatusEnum.Send &&
                             c.Request.SourceId == (long) SourceEnum.Operator))
                        .Where(c => !c.Entity.IsIncludeInInteragency);
            }

            return allChildren;
        }

        internal InteragencyRequestListViewModel List(InteragencyRequestListViewModel model)
        {
            IQueryable<InteragencyRequest> query = UnitOfWork.GetSet<InteragencyRequest>().AsQueryable();
            var pager = new PagerState(model.PageNumber, model.PageSize);

            if (!string.IsNullOrWhiteSpace(model.RequestNumber))
            {
                string number = model.RequestNumber.ToLower();
                query = query.Where(i => i.RequestNumber.ToLower().Contains(number));
            }

            var res = new InteragencyRequestListViewModel(query.GetPage(pager), model.PageNumber, model.PageSize,
                query.Count())
            {
                RequestNumber = model.RequestNumber
            };

            return res;
        }
    }
}
