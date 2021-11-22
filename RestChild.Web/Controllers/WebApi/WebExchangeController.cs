using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Http;
using MimeTypes;
using RestChild.Booking.Logic.Extensions;
using RestChild.Comon;
using RestChild.Comon.Config;
using RestChild.Comon.Dto;
using RestChild.Comon.Enumeration;
using RestChild.Comon.Exchange;
using RestChild.DAL;
using RestChild.DAL.RepositoryExtensions;
using RestChild.DocumentGeneration;
using RestChild.Domain;
using RestChild.Extensions.Extensions;
using RestChild.Web.CshedService;
using RestChild.Web.Models;
using RestChild.Web.Properties;

namespace RestChild.Web.Controllers.WebApi
{
    /// <summary>
    ///     интеграционный сервис
    /// </summary>
    [AllowAnonymous]
    public partial class WebExchangeController : BaseController
    {
        private static readonly long?[] BenefitNotForPaymentCheck =
            Settings.Default.BenefitNotForCheckPayment?.Cast<string>().Select(v => v.LongParse()).Where(l => l.HasValue)
                .ToArray();

        private static readonly long?[] BenefitNotDecline =
            Settings.Default.BenefitNotDecline.Cast<string>().Select(s => s.LongParse()).Where(s => s.HasValue)
                .ToArray();

        private readonly Dictionary<long?, long?> violationToDecline =
            Settings.Default.ViolationToDecline?.Cast<string>()
                .Select(v => v.Split(';'))
                .Where(v => v.Length == 2)
                .Select(v => new {id = v[0].LongParse(), dec = v[1].LongParse()})
                .ToDictionary(v => v.id, v => v.dec) ?? new Dictionary<long?, long?>();

        /// <summary>
        ///     работа с заявлениями
        /// </summary>
        public WebFirstRequestCompanyController ApiRequest { get; set; }

        /// <summary>
        ///     контроллер для работы со справочниками
        /// </summary>
        public WebVocabularyController VocController { get; set; }

        /// <summary>
        ///     нужно проверять по мало обеспеченности
        /// </summary>
        internal static bool NeedCheckPayment(Child childForCurUpdate)
        {
            var bt = childForCurUpdate?.BenefitType?.SameBenefitId ?? childForCurUpdate?.BenefitTypeId ?? 0;
            return !BenefitNotForPaymentCheck.Contains(bt) && (childForCurUpdate?.Request?.IsFirstCompany ?? false);
        }

        public override void SetUnitOfWorkInRefClass(IUnitOfWork unitOfWork)
        {
            base.SetUnitOfWorkInRefClass(unitOfWork);
            ApiRequest.SetUnitOfWorkInRefClass(unitOfWork);
            VocController.SetUnitOfWorkInRefClass(unitOfWork);
        }

        [HttpPost]
        [HttpGet]
        public void RequestRejectByTime(long requestId, string action, long rejectionCode)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);
            using (var tran = UnitOfWork.GetTransactionScope())
            {
                var reason = rejectionCode == 0 ? (long?) null : rejectionCode;
                ApiRequest.RequestChangeStatusWithCreateVersion(requestId, action, reason);
                tran.Complete();
            }
        }

        /// <summary>
        ///     смена статуса заявки
        /// </summary>
        [HttpPost]
        [HttpGet]
        // ReSharper disable once IdentifierTypo
        public void SendEventToRequest(long requestId, string action, long? accountId, string plandate)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);

            var planDate = plandate.XmlToDateTime();

            var request = UnitOfWork.GetById<Request>(requestId);
            long? declineReason = null;
            if (AccessRightEnum.Status.FcToIncludedInList == action)
            {
                if (!UnitOfWork.GetSet<ListTravelersRequest>().Any(r => r.RequestId == request.Id && r.IsIncluded))
                {
                    action = AccessRightEnum.Status.FcToReject;
                    declineReason = Settings.Default.ReasonNotIncludedInList;
                }
            }

            UnitOfWork.RequestChangeStatusInternal(action, request, declineReason, false, accountId, planDate);
        }

        /// <summary>
        ///     обработка результата
        /// </summary>
        /// <param name="exhcangeId"></param>
        [HttpPost]
        [HttpGet]
        public void SendAcknowledgement(long exhcangeId)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);

            var exchangeBaseRegistry = UnitOfWork.GetById<ExchangeBaseRegistry>(exhcangeId);
            if (!exchangeBaseRegistry.IsProcessed && !string.IsNullOrWhiteSpace(exchangeBaseRegistry.ResponseGuid) &&
                !string.IsNullOrWhiteSpace(exchangeBaseRegistry.ServiceNumber))
            {
                if (exchangeBaseRegistry.IsIncoming || exchangeBaseRegistry.IsAddonRequest ||
                    exchangeBaseRegistry.OperationType == "SetFilesAndStatusGuidProblem")
                {
                    exchangeBaseRegistry.IsProcessed = true;
                    UnitOfWork.SaveChanges();
                }
                else
                {
                    ProcessResultOfCheck(exchangeBaseRegistry);
                }
            }
        }

        /// <summary>
        ///     обработка результата проверки БР
        /// </summary>
        private void ProcessResultOfCheck(ExchangeBaseRegistry exchangeBaseRegistry)
        {
            try
            {
                using (var tran = UnitOfWork.GetTransactionScope())
                {
                    var request = exchangeBaseRegistry.Child?.Request
                                  ?? exchangeBaseRegistry.Applicant?.Request
                                  ?? UnitOfWork.GetSet<Request>().FirstOrDefault(r => r.ApplicantId == exchangeBaseRegistry.ApplicantId);

                    if (request != null)
                    {
                        if (request.NeedSendForBenefit || request.NeedSendToRelative || request.NeedSendForParent ||
                            request.NeedSendForPassport ||
                            request.NeedSendForSnils || request.NeedSendForCPMPK ||
                            request.NeedSendForAisoLegalRepresentation ||
                            request.NeedSendForRegistrationByPassport)
                        {
                            return;
                        }
                    }

                    if (request == null)
                    {
                        Logger.Info($"Ошибка в SendAcknowledgement (Заявитель или сопровождающий по которому сформирован запрос не связан ни с каким заявлением) = {exchangeBaseRegistry?.Id}");
                        return;
                    }

                    

                    var comment = string.Empty;
                    exchangeBaseRegistry.IsProcessed = true;

                    var childForUpdate = exchangeBaseRegistry.Child;
                    Child childForCurUpdate = null;
                    if (childForUpdate != null)
                    {
                        childForCurUpdate = UnitOfWork.GetSet<Child>()
                            .FirstOrDefault(
                                c =>
                                    (c.EntityId ?? c.Id) == (childForUpdate.EntityId ?? childForUpdate.Id) &&
                                    c.Request.IsLast &&
                                    !c.Request.IsDeleted && c.Id != childForUpdate.Id);
                    }

                    var codes = childForCurUpdate?.BenefitType?.ExnternalUid.Split(',').ToArray() ??
                                childForUpdate?.BenefitType?.ExnternalUid.Split(',').ToArray() ?? new string[0];

                    var result = exchangeBaseRegistry.Parse(codes,
                        (Settings.Default.BrPaymentDocument ?? string.Empty).Split(',').Select(s => s.Trim()).ToArray(),
                        NeedCheckPayment(childForCurUpdate) ? new[] {Settings.Default.LowIncomeType} : null);
                    exchangeBaseRegistry.Success = result?.Approved ?? false;

                    var resendStatus = new int?[]
                    {
                        ExchangeBaseRegistryResponseStatusEnum.NoData,
                        ExchangeBaseRegistryResponseStatusEnum.ReceivedResponse,
                        ExchangeBaseRegistryResponseStatusEnum.InvalidParameters
                    };

                    // обработка ответа по запросу
                    if (!resendStatus.Contains(result?.Status))
                    {
                        var req = exchangeBaseRegistry.Child?.Request ?? exchangeBaseRegistry.Applicant?.Request;

                        if (req == null && exchangeBaseRegistry.ApplicantId != null)
                        {
                            req = UnitOfWork.GetSet<Request>()
                                .FirstOrDefault(r => r.ApplicantId == exchangeBaseRegistry.ApplicantId);
                        }

                        var copy = RefreshExchangeBaseRegistry(new ExchangeBaseRegistry(exchangeBaseRegistry)
                        {
                            IsProcessed = false,
                            SendDate = null,
                            ResponseGuid = null,
                            ResponseText = null,
                            ResponseDate = null
                        }, req);

                        if (copy != null)
                        {
                            exchangeBaseRegistry.NotActual = true;
                            UnitOfWork.AddEntity(copy);
                            UnitOfWork.SaveChanges();
                            tran.Complete();
                            return;
                        }
                    }

                    if (exchangeBaseRegistry.ExchangeBaseRegistryTypeId == (long) ExchangeBaseRegistryTypeEnum.Benefit)
                    {
                        if (result?.ResultAbsent == true)
                        {
                            comment = string.Format("По запросу {1} {0}", comment, exchangeBaseRegistry.ServiceNumber);
                        }
                        else if (result?.Approved == true && (result.ApprovedLowIncome ?? true))
                        {
                            comment =
                                $"По запросу {exchangeBaseRegistry.ServiceNumber} из Базового регистра получено подтверждение:";
                        }
                        else
                        {
                            comment =
                                $"По запросу {exchangeBaseRegistry.ServiceNumber} из Базового регистра не получено подтверждение";
                        }

                        UpdateInfoInChild(childForUpdate, comment);
                        UpdateInfoInChild(childForCurUpdate, comment);
                    }

                    if (string.IsNullOrWhiteSpace(exchangeBaseRegistry.ResponseText))
                    {
                        UnitOfWork.SaveChanges();
                        tran.Complete();
                        return;
                    }

                    var requestForUpdate = childForCurUpdate?.Request ??
                                           childForUpdate?.Request ?? exchangeBaseRegistry.Applicant?.Request ??
                                           UnitOfWork.GetSet<Request>()
                                               .FirstOrDefault(r => r.ApplicantId == exchangeBaseRegistry.ApplicantId);


                    if (requestForUpdate != null)
                    {
                        var resCheck = CheckStateCheckRequest(requestForUpdate);

                        requestForUpdate = UnitOfWork.GetById<Request>(requestForUpdate.Id);

                        if (requestForUpdate.StatusId == (long) StatusEnum.Send ||
                            requestForUpdate.StatusId == (long) StatusEnum.OperatorCheck ||
                            requestForUpdate.StatusId == (long) StatusEnum.DecisionIsMade)
                        {
                            if (!resCheck.NotFinished)
                            {
                                if (request.IsFirstCompany)
                                {
                                    UnitOfWork.SendChangeStatusByEvent(request, RequestEventEnum.GetResponseBase);
                                }
                                if (requestForUpdate.IsFirstCompany)
                                {
                                    // отправка статусов по заявке
                                    if (requestForUpdate.StatusId == (long) StatusEnum.Send)
                                    {
                                        var requestResultCheck =
                                            resCheck.BenefitApprove && resCheck.SnilsApprove &&
                                            /*resCheck.PassportApprove &&*/ resCheck.LowIncomeApprove; //???

                                        var declined = false;

                                        if (!requestResultCheck && requestForUpdate.StatusId == (long) StatusEnum.Send)
                                        {
                                            if (!resCheck.SnilsApprove)
                                            {
                                                UnitOfWork.RequestChangeStatusInternal(
                                                    AccessRightEnum.Status.FcToReject,
                                                    requestForUpdate,
                                                    DeclineSectionProcess.GetDeclineReason("BadDocuments",
                                                        requestForUpdate.TypeOfRestId) ??
                                                    Settings.Default.BadDocuments, false);
                                                declined = true;
                                            }
                                            else if (!resCheck.LowIncomeApprove)
                                            {
                                                UnitOfWork.RequestChangeStatusInternal(
                                                    AccessRightEnum.Status.FcToReject,
                                                    requestForUpdate, Settings.Default.ReasonNotHavePayment, false);
                                                declined = true;
                                            }
                                            else if (!resCheck.BenefitApprove)
                                            {
                                                UnitOfWork.RequestChangeStatusInternal(
                                                    AccessRightEnum.Status.FcToReject,
                                                    requestForUpdate, Settings.Default.ReasonNotHaveBenefit, false);
                                                declined = true;
                                            }
                                            else
                                            {
                                                UnitOfWork.RequestChangeStatusInternal(
                                                    AccessRightEnum.Status.FcToReject,
                                                    requestForUpdate,
                                                    DeclineSectionProcess.GetDeclineReason("BadDocuments",
                                                        requestForUpdate.TypeOfRestId) ??
                                                    Settings.Default.BadDocuments, false);
                                                declined = true;
                                            }
                                        }

                                        if (!declined && requestForUpdate.SourceId == (long) SourceEnum.Mpgu)
                                        {
                                            if (
                                                (requestForUpdate.Attendant?.Any(a => a.IsAccomp && a.IsProxy) ??
                                                 false) ||
                                                resCheck.CallOfApplicant ||
                                                (requestForUpdate.AgentApplicant ?? false) ||
                                                requestForUpdate.TypeOfRestId ==
                                                (long) TypeOfRestEnum.YouthRestOrphanCamps)
                                            {
                                                UnitOfWork.RequestChangeStatusInternal(
                                                    AccessRightEnum.Status.FcToWaitApplicant, requestForUpdate, null,
                                                    false);
                                                Logger.InfoFormat("FcToWaitApplicant - {0}", requestForUpdate.Id);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    var requestResultCheck =
                                        resCheck.SnilsApprove &&
                                        resCheck.LowIncomeApprove &&
                                        resCheck.BenefitApprove &&
                                        !resCheck.CallOfApplicant;

                                    if (requestResultCheck)
                                    {
                                        var targetStatus = !(requestForUpdate.AgentApplicant ?? false) &&
                                                           resCheck.BenefitApprove
                                            ? AccessRightEnum.Status.CertificateIssued
                                            : requestForUpdate.SourceId == (long) SourceEnum.Operator
                                                ? AccessRightEnum.Status.EditInWaitApplicant
                                                : AccessRightEnum.Status.ToOperatorCheck;

                                        var model = new RequestViewModel(requestForUpdate)
                                        {
                                            TypeOfRestsAll = VocController.GetTypesOfRest(false)
                                        };
                                        if (!ApiRequest.BadPersonInRequest(model))
                                        {
                                            if (requestForUpdate.SourceId == (long) SourceEnum.Mpgu)
                                            {
                                                targetStatus = AccessRightEnum.Status.ToWaitApplicant;
                                            }
                                            else if (requestForUpdate.SourceId == (long) SourceEnum.Operator)
                                            {
                                                targetStatus = AccessRightEnum.Status.ToOperatorCheck;
                                            }
                                        }
                                        Logger.InfoFormat("requestForUpdate.Id={0}, targetStatus={1}", requestForUpdate.Id, targetStatus);
                                        UnitOfWork.RequestChangeStatusInternal(targetStatus, requestForUpdate, null,
                                            false);
                                    }
                                    else
                                    {
                                        UnitOfWork.RequestChangeStatusInternal(
                                            requestForUpdate.SourceId == (long) SourceEnum.Operator
                                                ? AccessRightEnum.Status.EditInWaitApplicant
                                                : AccessRightEnum.Status.ToOperatorCheck, requestForUpdate, null,
                                            false);
                                    }
                                }
                            }
                        }
                    }

                    UnitOfWork.SaveChanges();
                    tran.Complete();
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"Ошибка в SendAcknowledgement = {exchangeBaseRegistry?.Id}", ex);
            }
        }

        internal static void UpdateInfoInChild(Child child, string comment)
        {
            if (child == null)
            {
                return;
            }

            child.BenefitApproveComment = comment;
        }


        /// <summary>
        ///     Запрос явки заявителя
        /// </summary>
        /// <param name="requestId"></param>
        /// <param name="plandate"></param>
        [HttpPost]
        [HttpGet]
        public void SendRequestToWaitApplicant(long requestId, string plandate = null)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);

            var planDate = plandate?.XmlToDateTime();
            var requestForUpdate = UnitOfWork.GetById<Request>(requestId);

            if (requestForUpdate.StatusId == 1050 && !requestForUpdate.IsDeleted)
            {
                UnitOfWork.RequestChangeStatusInternal(AccessRightEnum.Status.FcToWaitApplicant, requestForUpdate, null, false, null, planDate);
            }
        }
        /// <summary>
        ///     Проверить заявление на вызов
        /// </summary>
        [Route("api/CheckRequestForCallOfApplicant")]
        [HttpGet]
        public CheckResultRequest CheckRequestForCallOfApplicant(long requestId)
        {
            var requestForCheck = UnitOfWork.GetById<Request>(requestId);

            if (requestForCheck != null)
            {
                return CheckStateCheckRequest(requestForCheck);
            }
            return null;
        }

        /// <summary>
        ///     получение результатов проверки в БР
        /// </summary>
        private CheckResultRequest CheckStateCheckRequest(Request req)
        {
            var result = new CheckResultRequest
            {
                BenefitApprove = true,
                PassportApprove = true,
                SnilsApprove = true,
                LowIncomeApprove = true,
                CallOfApplicant = false
            };

            if (req.NeedSendForBenefit || req.NeedSendToRelative || req.NeedSendForParent || req.NeedSendForPassport ||
                req.NeedSendForSnils || req.NeedSendForCPMPK || req.NeedSendForAisoLegalRepresentation ||
                req.NeedSendForRegistrationByPassport)
            {
                result.NotFinished = true;
                return result;
            }

            var children = req.Child.ToList();

            // проверка результата по сопровождающим
            var applicants = req.Attendant?.ToList() ?? new List<Applicant>();
            if (req.Applicant != null)
            {
                applicants.Add(req.Applicant);
            }

            // если есть не завершенные проверки в БР то дальше нечего делать
            if (children.Any(c => c.BaseRegistryInfo.Any(b => !b.IsProcessed && !b.NotActual))
                || applicants.Any(c => c.BaseRegistryInfo.Any(b => !b.IsProcessed && !b.NotActual)))
            {
                result.NotFinished = true;
                return result;
            }

            foreach (var child in children)
            {
                var relativeChild = true;
                var relativeChildChecked = false;

                var relativeSmevChild = true;
                var relativeSmevChildChecked = false;

                var aisoChild = true;
                var aisoChildChecked = false;

                var FGISFRIChild = true;
                var FGISFRIChildChecked = false;

                var benefitApprove = true;
                var benefitChildChecked = false;

                var callOfApplicant = false;
                var callOfApplicantBenefit = false; //вызов если нет льготы
                var lowIncomeApprove = true;

                var cpmpkChildCheked = false;
                var cpmpkChild = true;

                var PassportChildCheked = false;
                var PassportChild = true;


                foreach (var bri in child.BaseRegistryInfo.Where(b => !b.NotActual).ToArray())
                {
                    if (bri.ExchangeBaseRegistryTypeId == (long) ExchangeBaseRegistryTypeEnum.Benefit)
                    {
                        var codes = child?.BenefitType?.ExnternalUid.Split(',').ToArray() ??
                                    child?.BenefitType?.ExnternalUid.Split(',').ToArray() ?? new string[0];

                        var needCheckPayment = NeedCheckPayment(child);

                        var benefitCheck = bri.Parse(codes,
                            (Settings.Default.BrPaymentDocument ?? string.Empty).Split(',').Select(s => s.Trim())
                            .ToArray(),
                            needCheckPayment ? new[] {Settings.Default.LowIncomeType} : null);

                        var benefitApproveAdditional = true;
                        var callOfApplicantAdditional = false;
                        var lowIncomeApproveAdditional = true;

                        if (benefitCheck.Approved == false)
                        {
                            var benefitTypeId = child?.BenefitType?.SameBenefitId ?? child?.BenefitTypeId ?? 0;
                            var benefitType = child?.BenefitType?.SameBenefit?.ExnternalUid
                                              ?? child?.BenefitType?.ExnternalUid ?? string.Empty;

                            if (!BenefitNotDecline.Contains(child.BenefitTypeId) &&
                                !BenefitNotDecline.Contains(benefitTypeId) &&
                                (!needCheckPayment || Settings.Default.LowIncomeType == benefitType))
                            {
                                benefitApproveAdditional = false;
                            }
                            else
                            {
                                callOfApplicantAdditional = true;
                            }
                        }
                        else if (!benefitCheck.Approved.HasValue)
                        {
                            result.CallOfApplicant = true;
                        }

                        if (benefitCheck.ApprovedLowIncome == false)
                        {
                            lowIncomeApproveAdditional = false;
                        }

                        if (!benefitChildChecked || benefitChildChecked &&
                            (!benefitApprove && benefitApproveAdditional ||     //??? выражение никогда не выполняется
                             !lowIncomeApprove && lowIncomeApproveAdditional))  //??? выражение никогда не выполняется
                        {
                            benefitApprove = benefitApproveAdditional;
                            lowIncomeApprove = lowIncomeApproveAdditional;
                        }

                        callOfApplicantBenefit |= callOfApplicantAdditional;
                        benefitChildChecked = true;
                    }

                    if ((bri.ExchangeBaseRegistryTypeId == (long) ExchangeBaseRegistryTypeEnum.Snils ||
                         bri.ExchangeBaseRegistryTypeId == (long) ExchangeBaseRegistryTypeEnum.Snils2040) &&
                        !bri.Success)
                    {
                        result.SnilsApprove = false;
                    }

                    //не используется
                    if (bri.ExchangeBaseRegistryTypeId == (long) ExchangeBaseRegistryTypeEnum.Payments &&
                        !bri.Success)
                    {
                        result.LowIncomeApprove = false;
                    }
                    //не используется
                    if (bri.ExchangeBaseRegistryTypeId == (long) ExchangeBaseRegistryTypeEnum.Relationship)
                    {
                        relativeChildChecked = true;
                        if (!bri.Success)
                        {
                            relativeChild = false;
                        }
                    }

                    if (bri.ExchangeBaseRegistryTypeId == (long) ExchangeBaseRegistryTypeEnum.GetEGRZAGS)
                    {
                        relativeSmevChildChecked = true;
                        
                        var benefitCheck = bri.Parse();
                        // если проверка родства между ребенком и заявителем/сопровождающими и прверка документа не прошла то родство по сведрожду не подтвердилось
                        if (!(benefitCheck.Approved.HasValue && benefitCheck.Approved == true))
                        {
                            relativeSmevChild = false;
                        }
                        //if (!bri.Success)
                        //{
                        //    relativeSmevChild = false;
                        //}
                    }

                    if (bri.ExchangeBaseRegistryTypeId == (long)ExchangeBaseRegistryTypeEnum.GetFGISFRI)
                    {
                        FGISFRIChildChecked = true;
                        if (!bri.Success)
                        {
                            FGISFRIChild = false;
                        }
                    }

                    if (bri.ExchangeBaseRegistryTypeId == (long)ExchangeBaseRegistryTypeEnum.PassportDataBySNILS)
                    {
                        PassportChildCheked = true; //Костыль для подтверждения родства ребенка если указан паспорт чтоб проверка на родство не вызывала заявителя
                        if (!bri.Success)
                        {
                            result.PassportApprove = false;
                        }
                    }

                    if (bri.ExchangeBaseRegistryTypeId ==
                        (long) ExchangeBaseRegistryTypeEnum.AisoLegalRepresentationCheck)
                    {
                        aisoChildChecked = true;
                        if (!bri.Success)
                        {
                            aisoChild = false;
                        }
                    }

                    /*if (bri.ExchangeBaseRegistryTypeId == (long) ExchangeBaseRegistryTypeEnum.PassportRegistration && !bri.Success)
                    {
                        result.CallOfApplicant = true;
                    }

                    if (bri.ExchangeBaseRegistryTypeId == (long) ExchangeBaseRegistryTypeEnum.GetPassportRegistration && !bri.Success)
                    {
                        result.CallOfApplicant = true;
                    }*/

                    if (bri.ExchangeBaseRegistryTypeId == (long) ExchangeBaseRegistryTypeEnum.CpmpkExchange) //??? 
                    {
                        cpmpkChildCheked = true;
                        if (!bri.Success)
                        {
                            cpmpkChild = false;
                        }
                    }


                    //Если ребенок сирота то вызов
                    if (child.BenefitType.ExnternalUid == "52,69")
                    {
                        result.CallOfApplicant = true;
                    }
                    // В связи замечаниями проверки паспортов у детей и количества неподтвержденных решено подтверждать родство в заявлениях, в которых у детей паспорт
                    if (child.DocumentTypeId == 50001)
                    {
                        PassportChild = true;
                    }


                    // Повторяется
                    //if (bri.ExchangeBaseRegistryTypeId == (long) ExchangeBaseRegistryTypeEnum.PassportDataBySNILS &&
                    //    !bri.Success)
                    //{
                    //    result.PassportApprove = false;
                    //}
                }
                Logger.InfoFormat("Start ReqId={0}, CallOfApplicant={1}, BenefitApprove={2}", req.Id, result.CallOfApplicant, result.BenefitApprove);
                result.BenefitApprove &= benefitApprove;
                result.CallOfApplicant |= callOfApplicant; //
                Logger.InfoFormat("BenefitApprove &= benefitApprove={0}, CallOfApplicant |= callOfApplicant={1}", result.BenefitApprove, result.CallOfApplicant);

                //result.CallOfApplicant |= callOfApplicantBenefit && !cpmpkApproved;

                result.CallOfApplicant |= (callOfApplicantBenefit && !(cpmpkChild && cpmpkChildCheked)) && (callOfApplicantBenefit && !(FGISFRIChild && FGISFRIChildChecked));

                 result.CallOfApplicant |= (!((aisoChild && aisoChildChecked) || (relativeSmevChild && relativeSmevChildChecked)) // || (PassportChild && PassportChildCheked))
                        || !((cpmpkChild && cpmpkChildCheked) || (FGISFRIChild && FGISFRIChildChecked) || (benefitApprove && benefitChildChecked) ));

                Logger.InfoFormat("result.CallOfApplicant={0}, callOfApplicantBenefit={1}, ", result.CallOfApplicant, callOfApplicantBenefit);
                Logger.InfoFormat("ChildId={0}, aisoChild={1},aisoChildChecked={2}, relativeSmevChild={3},relativeSmevChildChecked={4},PassportChild={5},PassportChildCheked={6}", child.Id, aisoChild, aisoChildChecked, relativeSmevChild, relativeSmevChildChecked, PassportChild, PassportChildCheked);
                Logger.InfoFormat("ChildId={0}, cpmpkChild={1}, cpmpkChildCheked={2}, FGISFRIChild={3},FGISFRIChildChecked={4}, benefitApprove={5},benefitChildChecked={6}, lowIncomeApprove={7}", child.Id, cpmpkChild, cpmpkChildCheked, FGISFRIChild, FGISFRIChildChecked, benefitApprove, benefitChildChecked, lowIncomeApprove);
                Logger.InfoFormat("ChildEnd result.CallOfApplicant={0}", result.CallOfApplicant);
                result.LowIncomeApprove &= lowIncomeApprove;

                //if (!(relativeChild && relativeChildChecked)
                //    && !(relativeSmevChild && relativeSmevChildChecked)
                //    && !(aisoChild && aisoChildChecked))
                //{
                //    result.CallOfApplicant = true;
                //}
            }

            foreach (var applicant in applicants)
            {
                var applicantCheck = new CheckStateCheckRequestResult();
                foreach (var bri in applicant.BaseRegistryInfo.Where(b => !b.NotActual).ToArray())
                {
                    applicantCheck.NotFinished |= !bri.IsProcessed;

                    if ((bri.ExchangeBaseRegistryTypeId == (long) ExchangeBaseRegistryTypeEnum.Snils ||
                         bri.ExchangeBaseRegistryTypeId == (long) ExchangeBaseRegistryTypeEnum.Snils2040) &&
                        !bri.Success)
                    {
                        result.SnilsApprove = false;
                    }

                    if (bri.ExchangeBaseRegistryTypeId == (long) ExchangeBaseRegistryTypeEnum.PassportDataBySNILS &&
                        !bri.Success)
                    {
                        result.PassportApprove = false;
                    }

                    //if (bri.ExchangeBaseRegistryTypeId == (long)ExchangeBaseRegistryTypeEnum.PassportRegistration && // ??? не нужно вызывать
                    //    !bri.Success)
                    //{
                    //    result.CallOfApplicant = true;
                    //}

                    //if (bri.ExchangeBaseRegistryTypeId == (long)ExchangeBaseRegistryTypeEnum.GetPassportRegistration && // ??? не нужно вызывать
                    //    !bri.Success)
                    //{
                    //    result.CallOfApplicant = true;
                    //}
                }
            }

            Logger.InfoFormat("End result.CallOfApplicant={0}", result.CallOfApplicant);

            return result;
        }

        /// <summary>
        ///     проверка всего заявления
        /// </summary>
        [HttpPost]
        [HttpGet]
        public void CheckRequestInBaseRegistry(long requestId)
        {
            var req = UnitOfWork.GetById<Request>(requestId);

            CheckRequestInBaseRegistryStatusSet(req);

            CheckRequestInBaseRegistryBenefitReq(req);
            CheckRequestInBaseRegistrySnilsReq(req);
            CheckRequestInBaseRegistryPassportReq(req);
            CheckRequestInBaseRegistryRelativesReq(req);
            CheckRequestInBaseRegistryRegistrationByPassportReq(req);
            CheckBaseRegistryExtractFromFGISFRIReq(req);
        }

        /// <summary>
        ///     разбор заявления
        /// </summary>
        private void ParseRequestItems(request source, Request target)
        {
            if (source?.Items == null || source.ItemsElementName == null ||
                source.Items.Length != source.ItemsElementName.Length)
            {
                return;
            }

            var placeOrder = 1;
            var timeOrder = 1;

            for (var i = 0; i < source.ItemsElementName.Length; i++)
            {
                switch (source.ItemsElementName[i])
                {
                    case ItemsChoiceType.additionalPlaces:
                        target.AdditionalPlaces = Convert.ToInt32(source.Items[i]);
                        break;
                    case ItemsChoiceType.bookingGuid:
                        var bookingGuid = new Guid((string) source.Items[i]);
                        var booking = UnitOfWork.GetSet<Domain.Booking>()
                            .FirstOrDefault(b => b.Code == bookingGuid);
                        if (booking != null)
                        {
                            target.BookingGuid = bookingGuid;
                            target.TypeOfRestId = target.TypeOfRestId ?? booking.TypeOfRestId;
                            target.TypeOfRest = target.TypeOfRest ?? booking.TypeOfRest;
                            target.YearOfRestId = booking?.TourVolume?.Tour?.YearOfRestId;
                            target.SubjectOfRestId = booking?.TourVolume?.Tour?.SubjectOfRestId;
                            target.MainPlaces = booking.CountPlace;
                            target.HotelsId = booking?.TourVolume?.Tour?.HotelsId;
                            target.TourId = booking?.TourVolume?.TourId;
                            target.Tour = booking?.TourVolume?.Tour;
                            target.PlaceOfRestId = booking?.TourVolume?.Tour?.Hotels?.PlaceOfRestId;
                            target.TimeOfRestId = booking?.TourVolume?.Tour?.TimeOfRestId;
                            target.CountPlace = booking.CountPlace ?? 0;
                            target.CountAttendants = booking.CountAttendants ?? 0;
                            target.DateRequest = target.DateRequest ?? booking.BookingDate;
                        }

                        break;
                    case ItemsChoiceType.mainPlaces:
                        target.CountPlace = Convert.ToInt32(source.Items[i]);
                        break;
                    case ItemsChoiceType.attendantCount:
                        target.CountAttendants = Convert.ToInt32(source.Items[i]);
                        break;
                    case ItemsChoiceType.placeOfRest:
                        target.PlaceOfRestId = Convert.ToInt32(source.Items[i]);
                        break;
                    case ItemsChoiceType.subjectOfRest:
                        target.SubjectOfRestId = Convert.ToInt32(source.Items[i]);
                        break;
                    case ItemsChoiceType.timeOfRest:
                        target.TimeOfRestId = Convert.ToInt32(source.Items[i]);
                        break;
                    case ItemsChoiceType.typeOfCamp:
                        target.TypeOfCampId = Convert.ToInt32(source.Items[i]);
                        break;
                    case ItemsChoiceType.typeOfCampAddon:
                        target.TypeOfCampAddonId = Convert.ToInt32(source.Items[i]);
                        break;
                    case ItemsChoiceType.placeOfRestAddon:
                        target.PlacesOfRest = target.PlacesOfRest ?? new List<RequestPlaceOfRest>();
                        target.PlacesOfRest.Add(new RequestPlaceOfRest
                        {
                            PlaceOfRestId = Convert.ToInt64(source.Items[i]),
                            RequestId = target.Id,
                            Order = placeOrder++
                        });
                        break;
                    case ItemsChoiceType.timeOfRestAddon:
                        target.TimesOfRest = target.TimesOfRest ?? new List<RequestsTimeOfRest>();
                        target.TimesOfRest.Add(new RequestsTimeOfRest
                        {
                            TimeOfRestId = Convert.ToInt32(source.Items[i]),
                            RequestId = target.Id,
                            Order = timeOrder++
                        });
                        break;
                    case ItemsChoiceType.typeOfRest:
                        target.TypeOfRestId = Convert.ToInt32(source.Items[i]);
                        target.RequestOnMoney = target.TypeOfRestId == (long) TypeOfRestEnum.MoneyOn3To7 ||
                                                target.TypeOfRestId == (long) TypeOfRestEnum.MoneyOn7To15 ||
                                                target.TypeOfRestId == (long) TypeOfRestEnum.MoneyOn18 ||
                                                target.TypeOfRestId == (long) TypeOfRestEnum.MoneyOnInvalidOn4To17 ||
                                                target.TypeOfRestId == (long) TypeOfRestEnum.Money;
                        break;
                }
            }
        }

        /// <summary>
        ///     сохранить уведомление об отказе
        /// </summary>
        /// <param name="id"></param>
        [HttpPost]
        [HttpGet]
        public string SaveNotificationRefuse(long id)
        {
            var request = UnitOfWork.GetById<Request>(id);

            if (request == null || request.IsDeleted || request.StatusId != (long) StatusEnum.Reject)
            {
                return string.Empty;
            }

            var doc = WordProcessor.NotificationRefuseContentEx(request);
            using (var csClient = new CustomWebServiceImplClient())
            {
                if (csClient.ClientCredentials != null)
                {
                    csClient.ClientCredentials.UserName.UserName =
                        ConfigurationManager.AppSettings["CshedLogin"];
                    csClient.ClientCredentials.UserName.Password =
                        ConfigurationManager.AppSettings["CshedPass"];
                }

                try
                {
                    var fileType =
                        UnitOfWork.GetById<RequestFileType>((long) RequestFileTypeEnum.NotificationRefuse);

                    var mimeType = MimeTypeMap.GetMimeType(".docx");
                    var docId =
                        csClient.CreateDocument(
                            new CreateDocumentRequest
                            {
                                Document = doc.FileBody,
                                DocumentClass = fileType.CodeChed,
                                SSOID = string.IsNullOrWhiteSpace(request.SsoId) ? "0" : request.SsoId,
                                FromSystemCode = ConfigurationManager.AppSettings["CshedLogin"],
                                ServerStore = ConfigurationManager.AppSettings["CshedServerStore"],
                                properties =
                                    new[]
                                    {
                                        new Property {Name = "ASGUF_Code", Value = fileType.CodeAsGuf},
                                        new Property {Name = "MimeType", Value = mimeType},
                                        new Property {Name = "DocumentTitle", Value = "Уведомление.docx"}
                                    }
                            });

                    Logger.Info($"SaveNotificationRefuse - document saved in CHED(ReqId={id}, CHED={docId})");

                    return docId;
                }
                catch (Exception ex)
                {
                    // ignored
                    Logger.Error("SaveNotificationRefuse - Ошибка загрузки файла в РЦХЭД ReqId={id}", ex);
                }
            }

            return string.Empty;
        }

        /// <summary>
        ///     сохранить сертификат в заявление
        /// </summary>
        /// <param name="id"></param>
        [HttpPost]
        [HttpGet]
        public void SaveCertificateToRequest(long id)
        {
            var request = UnitOfWork.GetById<Request>(id);

            if (request == null || request.IsDeleted ||
                request.StatusId != (long) StatusEnum.CertificateIssued)
            {
                return;
            }

            var file = PdfController.CertificateForRequestTemporyFile(UnitOfWork, id);

            if (string.IsNullOrWhiteSpace(file))
            {
                return;
            }

            var bytes = File.ReadAllBytes(file);
            using (var csClient = new CustomWebServiceImplClient())
            {
                if (csClient.ClientCredentials != null)
                {
                    csClient.ClientCredentials.UserName.UserName =
                        ConfigurationManager.AppSettings["CshedLogin"];
                    csClient.ClientCredentials.UserName.Password =
                        ConfigurationManager.AppSettings["CshedPass"];
                }

                try
                {
                    var fileType =
                        UnitOfWork.GetById<RequestFileType>(request.RequestOnMoney
                            ? (long) RequestFileTypeEnum.CertificateOnPayment
                            : (long) RequestFileTypeEnum.CertificateOnRest);

                    var mimeType = MimeTypeMap.GetMimeType(".pdf");
                    var docId =
                        csClient.CreateDocument(
                            new CreateDocumentRequest
                            {
                                Document = bytes,
                                DocumentClass = fileType.CodeChed,
                                SSOID = string.IsNullOrWhiteSpace(request.SsoId) ? "0" : request.SsoId,
                                FromSystemCode = ConfigurationManager.AppSettings["CshedLogin"],
                                ServerStore = ConfigurationManager.AppSettings["CshedServerStore"],
                                properties =
                                    new[]
                                    {
                                        new Property {Name = "ASGUF_Code", Value = fileType.CodeAsGuf},
                                        new Property {Name = "MimeType", Value = mimeType},
                                        new Property {Name = "DocumentTitle", Value = "Сертификат.pdf"}
                                    }
                            });
                    Logger.Info($"SaveCertificateToRequest - document saved to ReqId={id}, CHED={docId}");

                    var files =
                        request.Files.Where(
                            f =>
                                f.RequestFileTypeId == (long) RequestFileTypeEnum.CertificateOnPayment ||
                                f.RequestFileTypeId == (long) RequestFileTypeEnum.CertificateOnRest).ToList();

                    foreach (var f in files)
                    {
                        request.Files.Remove(f);
                        UnitOfWork.Delete(f);
                    }

                    request.Files.Add(UnitOfWork.AddEntity(new RequestFile
                    {
                        RequestId = request.Id,
                        FileName = docId,
                        RemoteSave = true,
                        FileTitle = "Сертификат.pdf",
                        RequestFileTypeId = fileType.Id,
                        DataCreate = DateTime.Now,
                        LastUpdateTick = DateTime.Now.Ticks
                    }));

                    UnitOfWork.SaveChanges();
                    File.Delete(file);
                }
                catch (Exception ex)
                {
                    // ignored
                    Logger.Error("SaveCertificateToRequest - Error load document CHED, ReqId={id}", ex);
                }
            }
        }

        /// <summary>
        ///     разбор статусов
        /// </summary>
        /// <param name="exchangeUtsId"></param>
        [HttpPost]
        [HttpGet]
        public void ProcessStatus(long exchangeUtsId)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);

            var uts = UnitOfWork.GetById<ExchangeUTS>(exchangeUtsId);
            if (uts == null)
            {
                Logger.InfoFormat("Нет статусов для разбора {0}", exchangeUtsId);
                return;
            }

            StatusMessage data = null;

            try
            {
                data = Serialization.Deserialize<StatusMessage>(uts.Message);
            }
            catch (Exception ex)
            {
                Logger.Error("ProcessStatus error", ex);
            }

            bool isError;
            uts.ServiceNumber = data?.ServiceNumber;

            if (data != null && data.StatusCode == 1069)
            {
                isError = !ProcessCancel(uts);
            }
            else if (data != null && (data.StatusCode == (long) StatusEnum.DecisionIsMade &&
                                      (data.ReasonCode == "1" || data.ReasonCode == "101") ||
                                      data.StatusCode == 80111 ||
                                      data.StatusCode == 8011101))
            {
                //ничего не делать
                isError = false;
            }
            else if (data != null && (data.StatusCode == (long) StatusEnum.DecisionIsMade &&
                                      (data.ReasonCode == "2" || data.ReasonCode == "102") ||
                                      data.StatusCode == 80112 ||
                                      data.StatusCode == 8011102))
            {
                isError = !ProcessRejectOptOut(uts);
            }
            else
            {
                isError = true;
            }

            uts.IsError = isError;
            uts.Processed = true;
            UnitOfWork.SaveChanges();
        }

        /// <summary>
        ///     обработка отзыва
        /// </summary>
        /// <param name="uts"></param>
        /// <returns></returns>
        private bool ProcessCancel(ExchangeUTS uts)
        {
            var request =
                UnitOfWork
                    .GetSet<Request>()
                    .FirstOrDefault(r => r.RequestNumber == uts.ServiceNumber && !r.IsDeleted);

            if (request != null)
            {
                uts.RequestId = request.Id;

                if (request.StatusId == (long) StatusEnum.Send ||
                    request.StatusId == (long) StatusEnum.WaitApplicant ||
                    request.StatusId == (long) StatusEnum.ApplicantCome ||
                    request.StatusId == (long) StatusEnum.Ranging ||
                    request.StatusId == (long) StatusEnum.IncludedInList ||
                    request.StatusId == (long) StatusEnum.DecisionMaking ||
                    request.StatusId == (long) StatusEnum.DecisionMakingCovid)
                {
                    UnitOfWork.RequestChangeStatusInternal(AccessRightEnum.Status.FcToCancelByRequest, request,
                        null,
                        false, isFromMpgu: true);
                }
                else
                {
                    UnitOfWork.SendChangeStatusByEvent(request, RequestEventEnum.DeclineCancel);
                }

                return true;
            }

            return false;
        }

        /// <summary>
        ///     обработка отзыва (отказа заявителем от предложенных вариантов)
        /// </summary>
        private bool ProcessRejectOptOut(ExchangeUTS uts)
        {
            var request = UnitOfWork.GetSet<Request>()
                .FirstOrDefault(r => r.RequestNumber == uts.ServiceNumber && !r.IsDeleted);

            if (request != null)
            {
                uts.RequestId = request.Id;

                if (request.StatusId == (long) StatusEnum.DecisionMaking
                    || request.StatusId == (long) StatusEnum.DecisionMakingCovid)
                {
                    UnitOfWork.RequestChangeStatusInternal(AccessRightEnum.Status.FcToReject, request,
                        Settings.Default.ReasonOptOut,
                        false, isFromMpgu: true);
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        ///     разбор заявки которая пришла от ЕТП МВ
        /// </summary>
        /// <param name="exchangeUtsId"></param>
        [HttpPost]
        [HttpGet]
        public void ParseRequest(long exchangeUtsId)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);

            var serviceNumber = string.Empty;

            try
            {
                var uts = UnitOfWork.GetById<ExchangeUTS>(exchangeUtsId);
                if (uts == null)
                {
                    Logger.InfoFormat("Нет заявления для разбора {0}", exchangeUtsId);
                    return;
                }

                var data = Serialization.Deserialize<Message>(uts.Message);

                using (var tran = UnitOfWork.GetTransactionScope())
                {
                    if (data == null)
                    {
                        uts.IsError = true;
                        UnitOfWork.SaveChanges();
                        tran.Complete();
                        Logger.InfoFormat("Нет содержимого заявления для разбора {0}", exchangeUtsId);
                        return;
                    }

                    uts.ServiceNumber =
                        uts.ServiceNumber ?? data.Service.NullSafe(s => s.ServiceNumber) ?? data.Service.RegNum;
                    serviceNumber = uts.ServiceNumber;
                    UnitOfWork.SaveChanges();

                    if (
                        UnitOfWork.GetSet<Request>()
                            .Any(r => r.RequestNumber == uts.ServiceNumber && !r.IsDeleted &&
                                      r.StatusId != (long) StatusEnum.ErrorRequest))
                    {
                        Logger.InfoFormat("Заявление с номером {0} уже есть ({1})", uts.ServiceNumber,
                            exchangeUtsId);
                        uts.IsError = true;
                        uts.ErrorText = $"Заявление с номером {uts.ServiceNumber} уже есть ({exchangeUtsId})";
                        UnitOfWork.SaveChanges();
                        tran.Complete();
                        return;
                    }

                    var xmlElement = data.CustomAttributes;

                    var requestData = Serialization.Deserialize<request>(xmlElement.OuterXml);

                    if (requestData == null)
                    {
                        Logger.InfoFormat("Нет информации о заявлении для разбора {0}", exchangeUtsId);
                        uts.IsError = true;
                        uts.ErrorText = $"Нет информации о заявлении для разбора {exchangeUtsId}";
                        UnitOfWork.SaveChanges();
                        tran.Complete();
                        return;
                    }

                    /*
                    var assembly = Assembly.Load("RestChild.Comon");
                    using (var streamXsd =
                        assembly.GetManifestResourceStream("RestChild.Comon.Exchange.camps.xsd"))
                    {
                        using (var xmlReader = XmlReader.Create(streamXsd))
                        {
                            XmlSchemaSet schemas = new XmlSchemaSet();
                            schemas.Add("http://camps.mos.ru/kultura_gu/", xmlReader);
                            XDocument documentToCheck = XDocument.Parse(xmlElement.OuterXml);
                            string msg = "";
                            documentToCheck.Validate(schemas, (o, e) => {
                                msg += e.Message + Environment.NewLine;
                            });

                            if (!string.IsNullOrWhiteSpace(msg))
                            {
                                Logger.InfoFormat("Ошибка проверки сообщения по XSD схеме {0}", exchangeUtsId);
                                uts.IsError = true;
                                uts.ErrorText = $"Ошибка проверки сообщения по XSD схеме {exchangeUtsId}, {msg}";
                                UnitOfWork.SaveChanges();
                                tran.Complete();
                                return;
                            }
                        }
                    }
                    */

                    var entity = new Request
                    {
                        RequestNumber = uts.ServiceNumber,
                        TransferFromId = requestData.transferFromSpecified
                            ? requestData.transferFrom
                            : (long?) null,
                        TransferToId = requestData.transferToSpecified ? requestData.transferTo : (long?) null,
                        DateRequest = requestData.dateRequest.XmlToDateTime(),
                        IsLast = true,
                        IsDeleted = false,
                        ExternalSystem = "ETPMV",
                        StatusId = (long) StatusEnum.Send,
                        SourceId = (long) SourceEnum.Mpgu,
                        RequestNumberMpgu = data.NullSafe(s => s.Service.RegNum),
                        NeedEmail = requestData.needEmailSpecified && requestData.needEmail,
                        NeedSms = requestData.needSmsSpecified && requestData.needSms,
                        BeneficiariesId = requestData.isDisabledSpecified
                            ? requestData.isDisabled
                            : (long?) BeneficiariesEnum.Child,
                        ChangeByScan = requestData.changeByScanSpecified && requestData.changeByScan,
                        SsoId = requestData.ssoid,
                        StatusApplicant = requestData.statusByChildSpecified
                            ? requestData.statusByChild.ToString()
                            : null,
                        BankName = requestData.bankProperty?.name,
                        BankAccount = requestData.bankProperty?.account,
                        BankBik = requestData?.bankProperty?.bik,
                        BankCardNumber = requestData?.bankProperty?.cardNumber,
                        BankKpp = requestData?.bankProperty?.kpp,
                        BankInn = requestData?.bankProperty?.inn,
                        BankCorr = requestData?.bankProperty?.korr,
                        BankLastName = requestData?.bankProperty?.lastName,
                        BankFirstName = requestData?.bankProperty?.firstName,
                        BankMiddleName = requestData?.bankProperty?.middleName,
                        PriorityTypeOfTransportInRequestId = requestData.typeOfTransportSpecified ? requestData.typeOfTransport : (int?)null,
                        AdditionalTypeOfTransportInRequestId = requestData.typeOfTransportAddonSpecified ? requestData.typeOfTransportAddon : (int?)null
                    };

                    //foreach (var child in requestData.childs.child)
                    //{

                    //}
                        

                    if (entity.DateRequest.HasValue && entity.DateRequest.Value.Year < 2000)
                    {
                        entity.DateRequest = null;
                    }

                    ParseRequestItems(requestData, entity);

                    if (entity.BookingGuid.HasValue)
                    {
                        if (
                            UnitOfWork.GetSet<Request>()
                                .Any(r => r.BookingGuid == entity.BookingGuid && !r.IsDeleted &&
                                          r.StatusId != (long) StatusEnum.ErrorRequest))
                        {
                            Logger.InfoFormat("На бронирование уже есть заявление {0}, exchangeUtsId={1}",
                                entity.BookingGuid,
                                exchangeUtsId);
                            uts.IsError = true;
                            uts.ErrorText =
                                $"На бронирование с номером {entity.BookingGuid} уже есть заявление. (exchangeUtsId={exchangeUtsId})";
                            UnitOfWork.SaveChanges();
                            tran.Complete();
                            return;
                        }

                        if (UnitOfWork.GetSet<Domain.Booking>()
                            .Any(b => b.Code == entity.BookingGuid && b.Canceled))
                        {
                            Logger.InfoFormat("Бронирование {0} уже отменено. exchangeUtsId={1}",
                                entity.BookingGuid,
                                exchangeUtsId);
                            uts.IsError = true;
                            uts.ErrorText =
                                $"Бронирование  {entity.BookingGuid} уже отменено. (exchangeUtsId={exchangeUtsId})";
                            UnitOfWork.SaveChanges();
                            tran.Complete();
                            return;
                        }
                    }
                    else
                    {
                        var years = UnitOfWork.GetYearsForTypeOfRest(entity.TypeOfRestId, entity.DateRequest);
                        var year = years.FirstOrDefault(y => y.Year == DateTime.Now.Year) ??
                                   years.FirstOrDefault();
                        if (year == null)
                        {
                            Logger.InfoFormat(
                                "Кампания по приему заявлений не открыта ({0}, exchangeUtsId={1})",
                                uts.ServiceNumber, exchangeUtsId);
                            uts.IsError = true;
                            uts.ErrorText =
                                $"Кампания по приему заявлений не открыта ({uts.ServiceNumber}, exchangeUtsId={exchangeUtsId})";
                            UnitOfWork.SaveChanges();
                            tran.Complete();
                            return;
                        }

                        if (entity.TypeOfRestId != (long) TypeOfRestEnum.Compensation &&
                            entity.TypeOfRestId != (long) TypeOfRestEnum.CompensationYouthRest)
                        {
                            entity.IsFirstCompany = true;
                        }

                        entity.YearOfRestId = year.Id;
                    }

                    if (requestData.childs?.child != null &&
                        entity.CountPlace != requestData.childs.child.Length)
                    {
                        Logger.InfoFormat(
                            "Количество детей в резервировании не соответствует количеству детей в заявлении с номером {0} уже есть ({1})",
                            uts.ServiceNumber, exchangeUtsId);
                        uts.IsError = true;
                        uts.ErrorText =
                            $"Количество детей в резервировании не соответствует количеству детей в заявлении с номером {uts.ServiceNumber} уже есть ({exchangeUtsId})";
                        UnitOfWork.SaveChanges();
                        tran.Complete();
                        return;
                    }

                    var attendantsArray =
                        requestData.attendants?.attendant?.Where(
                                a => !string.IsNullOrWhiteSpace(a.lastName) ||
                                     !string.IsNullOrWhiteSpace(a.firstName))
                            .ToList();

                    if (requestData.isDisabled == (long) BeneficiariesEnum.SecondParent &&
                        (attendantsArray == null || attendantsArray.Count != 1))
                    {
                        Logger.InfoFormat(
                            "Количество сопровождающих в резервировании не соответствует количеству сопровождающих в заявлении с номером {0} уже есть ({1}) второй родитель",
                            uts.ServiceNumber, exchangeUtsId);
                        uts.IsError = true;
                        uts.ErrorText =
                            $"Количество сопровождающих в резервировании не соответствует количеству сопровождающих в заявлении с номером {uts.ServiceNumber} уже есть ({exchangeUtsId}) второй родитель";
                        UnitOfWork.SaveChanges();
                        tran.Complete();
                        return;
                    }

                    if (requestData.isDisabled != (long) BeneficiariesEnum.SecondParent &&
                        entity.CountAttendants !=
                        (attendantsArray?.Count ?? 0) + (requestData?.declarant?.isAccomp ?? false ? 1 : 0) +
                        (requestData?.head?.isAccomp ?? false ? 1 : 0))
                    {
                        Logger.InfoFormat(
                            "Количество сопровождающих в резервировании не соответствует количеству сопровождающих в заявлении с номером {0} ({1})",
                            uts.ServiceNumber, exchangeUtsId);
                        uts.IsError = true;
                        uts.ErrorText =
                            $"Количество сопровождающих в резервировании не соответствует количеству сопровождающих в заявлении с номером {uts.ServiceNumber} уже есть ({exchangeUtsId})";
                        UnitOfWork.SaveChanges();
                        tran.Complete();
                        return;
                    }

                    entity.DateRequest = entity.DateRequest ?? data.Service?.RegDate ?? uts.DateCreate;

                    if (requestData.declarant != null)
                    {
                        var applicant = requestData.declarant;
                        entity.Applicant = new Applicant
                        {
                            LastName = applicant.lastName,
                            FirstName = applicant.firstName,
                            MiddleName = applicant.middleName,
                            HaveMiddleName = applicant.middleNameAbsent ||
                                             string.IsNullOrEmpty(applicant.middleName),
                            ApplicantTypeId = applicant.applicantType == 0
                                ? (long?) null
                                : applicant.applicantType,
                            DocumentTypeId = applicant.documentType,
                            DocumentSeria = applicant.documentSeria,
                            DocumentNumber = applicant.documentNumber,
                            DocumentDateOfIssue = applicant.documentDateOfIssue.XmlToDateTime(),
                            DocumentSubjectIssue = applicant.documentSubjectIssue,
                            DocumentCode = applicant.documentCode,
                            Phone = applicant.phone,
                            Email = applicant.email,
                            IsAccomp = applicant.isAccomp,
                            PlaceOfBirth = applicant.placeOfBirth,
                            DateOfBirth = applicant.dateOfBirth.XmlToDateTime(),
                            Payed = true,
                            IndexField = -1,
                            Male = applicant.sex == sexType.Item1,
                            AddonPhone = applicant.phoneAddon,
                            Snils = applicant.snils
                        };

                        if (applicant.foreginDocument != null)
                        {
                            var item = entity.Applicant;
                            var doc = applicant.foreginDocument;
                            item.ForeginDateOfIssue = doc.documentDateOfIssue.XmlToDateTime();
                            item.ForeginNumber = doc.documentNumber;
                            item.ForeginSeria = doc.documentSeria;
                            item.ForeginSubjectIssue = doc.documentSubjectIssue;
                            item.ForeginTypeId = doc.documentType;
                            item.ForeginDateEnd = doc.documentEndDate.XmlToDateTime();
                        }

                        if (applicant.addressRegistration != null)
                        {
                            var address = applicant.addressRegistration;

                            var district = address.districtSpecified ? address.district : (long?) null;
                            var region = address.regionSpecified ? address.region : (long?) null;

                            if (district >= 100)
                            {
                                district = district / 100;
                                district = UnitOfWork.GetSet<BtiDistrict>()
                                    .FirstOrDefault(d => d.Givz == district)?.Id;
                            }

                            district = UnitOfWork.GetSet<BtiDistrict>().FirstOrDefault(r => r.Id == district)
                                ?.Id;

                            region = UnitOfWork.GetSet<BtiRegion>().FirstOrDefault(r => r.Givz == region)?.Id ??
                                     UnitOfWork.GetSet<BtiRegion>().FirstOrDefault(r => r.Id == region)?.Id;

                            var item = entity.Applicant;
                            item.Address = new Address
                            {
                                Appartment = address.appartment,
                                Corpus = address.corpus,
                                House = address.house,
                                Street = address.street,
                                Stroenie = address.stroenie,
                                Vladenie = address.vladenie,
                                BtiDistrictId = district,
                                BtiRegionId = region,
                                FiasId = address.fiasid,
                                Name = address.addressName
                            };
                            if (address.addressUnomSpecified)
                            {
                                var statuses = new long[] {1, 2};
                                var addr = UnitOfWork.GetSet<BtiAddress>()
                                    .FirstOrDefault(a =>
                                        a.Unom == address.addressUnom && statuses.Contains(a.Status));

                                if (addr != null)
                                {
                                    //item.Address.BtiAddressId = addr.Id;
                                    //item.Address.Name = addr.FullAddress;
                                    item.Address.BtiRegionId = addr.BtiRegionId;
                                    item.Address.BtiDistrictId = addr.BtiDistrictId;
                                }
                            }

                            if (!string.IsNullOrWhiteSpace(address.fiasid))
                            {
                                item.Address.Street = null;
                                item.Address.House = null;
                                item.Address.Stroenie = null;
                                item.Address.Corpus = null;
                                item.Address.Vladenie = null;
                            }
                        }
                    }

                    entity.AgentApplicant = requestData.headDeclarant;
                    var attendants = new List<Applicant>();

                    if (requestData.head != null && requestData.headDeclarant)
                    {
                        var agent = requestData.head;
                        entity.Agent = new Agent
                        {
                            LastName = agent.lastName,
                            FirstName = agent.firstName,
                            MiddleName = agent.middleName,
                            HaveMiddleName = agent.middleNameAbsent || string.IsNullOrEmpty(agent.middleName),
                            DocumentTypeId = agent.documentType,
                            DocumentSeria = agent.documentSeria,
                            DocumentNumber = agent.documentNumber,
                            DocumentDateOfIssue = agent.documentDateOfIssue.XmlToDateTime(),
                            DocumentSubjectIssue = agent.documentSubjectIssue,
                            Phone = agent.phone,
                            Email = agent.email,
                            ProxyDateOfIssure = agent.proxyDateOfIssure.XmlToDateTime(),
                            ProxyEndDate = agent.proxyEndDate.XmlToDateTime(),
                            NotaryName = agent.notaryName,
                            ProxyNumber = agent.proxyNumber,
                            Snils = agent.snils,
                            DocumentCode = agent.documentCode,
                            PlaceOfBirth = agent.placeOfBirth,
                            DateOfBirth = agent.dateOfBirth.XmlToDateTime(),
                            Male = agent.sex == sexType.Item1,
                            IsLast = true
                        };

                        entity.RepresentInterestId = agent.statusByApplicant?.IntParse();

                        if (agent.isAccompSpecified && agent.isAccomp && agent.attendant != null)
                        {
                            var item = new Applicant
                            {
                                LastName = agent.lastName,
                                FirstName = agent.firstName,
                                MiddleName = agent.middleName,
                                HaveMiddleName =
                                    agent.middleNameAbsent || string.IsNullOrEmpty(agent.middleName),
                                DocumentTypeId = agent.documentType,
                                DocumentSeria = agent.documentSeria,
                                DocumentNumber = agent.documentNumber,
                                DocumentDateOfIssue = agent.documentDateOfIssue.XmlToDateTime(),
                                DocumentSubjectIssue = agent.documentSubjectIssue,
                                Phone = agent.phone,
                                Email = agent.email,
                                PlaceOfBirth = agent.placeOfBirth,
                                DateOfBirth = agent.dateOfBirth.XmlToDateTime(),
                                IsAccomp = true,
                                Payed = true,
                                Id = 0,
                                IndexField = -2,
                                Male = agent.sex == sexType.Item1,
                                Snils = agent.snils,
                                DocumentCode = agent.documentCode,
                                IsAgent = true,
                                IsProxy = true,
                                ProxyDateOfIssure = agent.attendant.proxyDateOfIssure.XmlToDateTime(),
                                ProxyEndDate = agent.attendant.proxyEndDate.XmlToDateTime(),
                                NotaryName = agent.attendant.notaryName,
                                ProxyNumber = agent.attendant.proxyNumber
                            };

                            attendants.Add(item);
                        }
                    }

                    entity.Attendant = new List<Applicant>();
                    var keyRef = new Dictionary<int, Applicant>();

                    if (attendantsArray != null)
                    {
                        var applicantId = -2;
                        foreach (var attendant in attendantsArray)
                        {
                            var item = new Applicant
                            {
                                LastName = attendant.lastName,
                                FirstName = attendant.firstName,
                                MiddleName = attendant.middleName,
                                HaveMiddleName =
                                    attendant.middleNameAbsent || string.IsNullOrEmpty(attendant.middleName),
                                DocumentTypeId = attendant.documentType,
                                DocumentSeria = attendant.documentSeria,
                                DocumentNumber = attendant.documentNumber,
                                DocumentDateOfIssue = attendant.documentDateOfIssue.XmlToDateTime(),
                                DocumentSubjectIssue = attendant.documentSubjectIssue,
                                DocumentCode = attendant.documentCode,
                                Inn = attendant.inn,
                                Phone = attendant.phone,
                                Email = attendant.email,
                                PlaceOfBirth = attendant.placeOfBirth,
                                DateOfBirth = attendant.dateOfBirth.XmlToDateTime(),
                                IsAccomp = true,
                                Payed = true,
                                Id = 0,
                                IndexField = applicantId,
                                Male = attendant.sex == sexType.Item1,
                                Snils = attendant.snils,
                                IsProxy = attendant.proxy != null,
                                ProxyDateOfIssure = attendant.proxy?.proxyDateOfIssure?.XmlToDateTime(),
                                ProxyEndDate = attendant.proxy?.proxyEndDate.XmlToDateTime(),
                                NotaryName = attendant.proxy?.notaryName,
                                ProxyNumber = attendant.proxy?.proxyNumber,
                                ApplicantTypeId = attendant.statusByChildSpecified
                                    ? attendant.statusByChild
                                    : (long?) null
                            };

                            applicantId--;

                            if (attendant.foreginDocument != null)
                            {
                                var doc = attendant.foreginDocument;
                                item.ForeginDateOfIssue = doc.documentDateOfIssue.XmlToDateTime();
                                item.ForeginNumber = doc.documentNumber;
                                item.ForeginSeria = doc.documentSeria;
                                item.ForeginSubjectIssue = doc.documentSubjectIssue;
                                item.ForeginTypeId = doc.documentType;
                                item.ForeginDateEnd = doc.documentEndDate.XmlToDateTime();
                            }

                            keyRef.Add(attendant.number, item);
                            attendants.Add(item);
                        }
                    }

                    entity.Attendant = attendants;
                    entity.Child = new List<Child>();
                    
                    if (requestData.childs?.child != null)
                    {
                        var children = new List<Child>();
                        var indexChild = -1;
                        foreach (var child in requestData.childs.child)
                        {
                            
                            var item = new Child
                            {
                                Id = indexChild,
                                LastName = child.lastName,
                                FirstName = child.firstName,
                                MiddleName = child.middleName,
                                HaveMiddleName =
                                    child.middleNameAbsent || string.IsNullOrEmpty(child.middleName),
                                DocumentTypeId = child.documentType,
                                DocumentSeria = child.documentSeria,
                                DocumentNumber = child.documentNumber,
                                DocumentDateOfIssue = child.documentDateOfIssue.XmlToDateTime(),
                                DocumentSubjectIssue = child.documentSubjectIssue,
                                DocumentCode = child.documentCode,
                                RegisteredInMoscow = child.registeredInMoscow,
                                SchoolId = !child.schoolNotPresent && child.schoolSpecified
                                    ? child.school
                                    : (long?) null,
                                SchoolNotPresent = child.schoolNotPresent,
                                Male = child.sex == sexType.Item1,
                                PlaceOfBirth = child.placeOfBirth,
                                DateOfBirth = child.dateOfBirth.XmlToDateTime(),
                                Payed = true,
                                IndexField = indexChild,
                                Snils = child.snils
                                
                            };
                            indexChild--;
                            if (child.birthCertDocument != null)
                            {
                                var cd = child.birthCertDocument;
                                item.DocumentNumberCertOfBirth = cd.svidNumber;
                                item.DocumentSeriaCertOfBirth = cd.svidSeria;
                                item.DocumentTypeCertOfBirthId = cd.svidType;
                            }

                            if (child.benefit != null)
                            {
                                var b = child.benefit;
                                entity.NeedSendForCPMPK = item.IsCPMPK = b.cpmpcConclusion;
                                item.BenefitDate = b.benefitDate.XmlToDateTime();
                                item.BenefitEndDate = b.benefitEndDate.XmlToDateTime();
                                item.BenefitNeverEnd = b.benefitNeverEnd;
                                item.BenefitTypeId = b.benefitType;
                                item.TypeOfRestrictionId =
                                    b.typeOfRestrictionSpecified ? b.typeOfRestriction : (long?) null;
                                item.IsInvalid = b.isInvalid;
                                item.BenefitGroupInvalidId =
                                    b.groupInvalidSpecified ? b.groupInvalid : (long?) null;
                                item.TypeOfSubRestrictionId =
                                    b.typeOfSubRestrictionIdSpecified ? b.typeOfSubRestrictionId : (long?) null;

                                //вид льготы для коммерции
                                if (entity.TypeOfRestId == (long) TypeOfRestEnum.ChildRestFederalCamps)
                                {
                                    var benefitTypeId =
                                        UnitOfWork.GetSet<BenefitType>()
                                            .Where(
                                                bt => bt.SameBenefitId == item.BenefitTypeId &&
                                                      bt.TypeOfRestId ==
                                                      (long) TypeOfRestEnum.ChildRestFederalCamps)
                                            .Select(bt => bt.Id)
                                            .FirstOrDefault();

                                    item.BenefitTypeId =
                                        benefitTypeId == 0 ? item.BenefitTypeId : benefitTypeId;
                                }
                            }

                            if (child.foreginDocument != null)
                            {
                                var doc = child.foreginDocument;
                                item.ForeginDateOfIssue = doc.documentDateOfIssue.XmlToDateTime();
                                item.ForeginNumber = doc.documentNumber;
                                item.ForeginSeria = doc.documentSeria;
                                item.ForeginSubjectIssue = doc.documentSubjectIssue;
                                item.ForeginTypeId = doc.documentType;
                                item.ForeginDateEnd = doc.documentEndDate.XmlToDateTime();
                            }

                            if (child.addressRegistration != null)
                            {
                                var address = child.addressRegistration;

                                var district = address.districtSpecified ? address.district : (long?) null;
                                var region = address.regionSpecified ? address.region : (long?) null;

                                if (district >= 100)
                                {
                                    district = district / 100;
                                    district = UnitOfWork.GetSet<BtiDistrict>()
                                        .FirstOrDefault(d => d.Givz == district)
                                        ?.Id;
                                }

                                district = UnitOfWork.GetSet<BtiDistrict>().FirstOrDefault(r => r.Id == district)?.Id;

                                region = UnitOfWork.GetSet<BtiRegion>().FirstOrDefault(r => r.Givz == region)
                                             ?.Id ??
                                         UnitOfWork.GetSet<BtiRegion>().FirstOrDefault(r => r.Id == region)?.Id;

                                item.Address = new Address
                                {
                                    Appartment = address.appartment,
                                    Corpus = address.corpus,
                                    House = address.house,
                                    Street = address.street,
                                    Stroenie = address.stroenie,
                                    Vladenie = address.vladenie,
                                    BtiDistrictId = district,
                                    BtiRegionId = region,
                                    FiasId = address.fiasid,
                                    Name = address.addressName
                                };
                                if (address.addressUnomSpecified)
                                {
                                    var statuses = new long[] {1, 2};
                                    var addr = UnitOfWork.GetSet<BtiAddress>().FirstOrDefault(a =>
                                        a.Unom == address.addressUnom && statuses.Contains(a.Status));

                                    if (addr != null)
                                    {
                                        //item.Address.BtiAddressId = addr.Id;
                                        //item.Address.Name = addr.FullAddress;
                                        item.Address.BtiRegionId = addr.BtiRegionId;
                                        item.Address.BtiDistrictId = addr.BtiDistrictId;
                                    }
                                }

                                if (!string.IsNullOrWhiteSpace(address.fiasid))
                                {
                                    item.Address.Street = null;
                                    item.Address.House = null;
                                    item.Address.Stroenie = null;
                                    item.Address.Corpus = null;
                                    item.Address.Vladenie = null;
                                }
                            }

                            if (child.attendantChildNumberSpecified)
                            {
                                if (child.attendantChildNumber > 0)
                                {
                                    item.Applicant = keyRef[child.attendantChildNumber];
                                    item.ApplicantId = item.Applicant?.IndexField;
                                }
                                else if (entity.Applicant != null)
                                {
                                    item.ApplicantId = entity.Applicant?.IndexField;
                                    entity.Applicant.IsAccomp = true;
                                }

                                item.StatusByChildId = child.statusByChildSpecified
                                    ? child.statusByChild
                                    : (int?) null;
                            }

                            if (entity.IsFirstCompany && !item.SchoolId.HasValue && !item.SchoolNotPresent)
                            {
                                item.SchoolNotPresent = true;
                            }

                            children.Add(item);
                        }

                        entity.Child = children;
                    }

                    if (requestData.files != null && requestData.files.Length > 0)
                    {
                        entity.Files = new List<RequestFile>();
                        foreach (var file in requestData.files)
                        {
                            entity.Files.Add(new RequestFile
                            {
                                RemoteSave = true,
                                FileTitle = file.fileName,
                                RequestFileTypeId = file.documentType,
                                FileName = file.uid,
                                LastUpdateTick = DateTime.Now.Ticks,
                                DataCreate = DateTime.Now,
                                Description = file.description
                            });
                        }
                    }

                    entity = ApiRequest.SaveRequest(entity);

                    uts.Processed = true;
                    uts.RequestId = entity.Id;
                    UnitOfWork.SaveChanges();

                    var vm = new RequestViewModel(entity)
                    {
                        BenefitTypes = VocController.GetBenefitTypeInternal(),
                        TypeOfRestrictions = UnitOfWork.GetSet<TypeOfRestriction>().ToList(),
                        TypeOfRestsAll = VocController.GetTypesOfRest(false),
                        TimeOfRests = VocController.GetTimesOfRestWithoutFilter(),
                        TypeOfRestBenefitRestrictions = VocController.GetTypeOfRestBenefitRestrictions(),
                        RequestCurrentPeriod = VocController.GetRequestCurrentPeriods(),
                        PlacesOfRest = VocController.GetPlacesOfRestInternal(true),
                        StatusByChild = VocController.GetStatusByChild()
                            .InsertAt(new StatusByChild {Id = 0, Name = "-- Не выбрано --"}),
                        DeclineReason = VocController.GetDeclineReason()
                            .Where(d => d.IsManual && d.FirstStage && d.IsActive)
                            .OrderBy(d => d.Id)
                            .InsertAt(new DeclineReason {Id = 0, Name = "-- Не выбрано --"})
                    };

                    foreach (var child in vm.Child)
                    {
                        child.StatusByChild = vm.StatusByChild;
                    }

                    var status = StatusEnum.Send;
                    long? decline = null;
                    var isDeleted = false;

                    string action = null;

                    if (!vm.CheckModel())
                    {
                        uts.ErrorText = vm.GetErrorDescription(string.Empty, false);
                        if (Settings.Default.SendErrorStatus)
                        {
                            status = StatusEnum.ErrorRequest;
                        }
                        else
                        {
                            uts.IsError = true;
                            uts.Processed = false;
                            isDeleted = true;
                        }
                    }

                    if (status == StatusEnum.Send && !isDeleted)
                    {
                        ApiRequest.CheckApplicant(vm);
                        if (vm.ApplicantDouble.Any())
                        {
                            status = StatusEnum.RegistrationDecline;
                            var sb = new StringBuilder();                            
                            sb.Append($"{vm.Applicant.Data.LastName} {vm.Applicant.Data.FirstName} {vm.Applicant.Data.MiddleName},");                            
                            var msg = sb.ToString();
                            msg = msg.Substring(0, msg.Length - 1);
                            action = AccessRightEnum.Status.ToRegistrationDeclineAttendant;
                            if (entity.TypeOfRestId == (long)TypeOfRestEnum.RestWithParentsPoor)
                            {
                                decline = 202105;
                                uts.ErrorText = $@"Заявление является повторным. Заявление о предоставлении бесплатной путёвки для отдыха и оздоровления от имени родителя (законного представителя) ({msg}) уже подано. По указанной цели обращения не допускается подача отдельных заявлений о предоставлении бесплатной путевки для отдыха и оздоровления в отношении каждого ребенка.";
                            }
                            if (entity.TypeOfRestId == (long)TypeOfRestEnum.MoneyOn3To7)
                            {
                                decline = 202106;
                                uts.ErrorText = $@"Заявление является повторным. Заявление о предоставлении сертификата на отдых и оздоровления от имени родителя (законного представителя) ({msg}) уже подано. По указанной цели обращения не допускается подача отдельных заявлений о предоставлении сертификата на отдых и оздоровление в отношении каждого ребенка.";
                            }
                            if (entity.TypeOfRestId == (long)TypeOfRestEnum.YouthRestCamps || entity.TypeOfRestId == (long)TypeOfRestEnum.YouthRestOrphanCamps)
                            {
                                decline = 202104;
                                //uts.ErrorText = $@"Заявление является повторным. На указанное в заявлении лицо из числа детей-сирот и детей, оставшихся без попечения родителей ({msg}) уже подано заявление о предоставлении бесплатной путевки для отдыха и оздоровления.";
                                uts.ErrorText = $@"Заявление является повторным. На указанное в заявлении лицо из числа детей-сирот и детей, оставшихся без попечения родителей ({msg}) уже подано заявление о предоставлении бесплатной путевки для отдыха и оздоровления.";

                            }
                        }

                        ApiRequest.CheckChildren(vm, true);
                        if (vm.SameChildren.Any())
                        {
                            bool plural = vm.SameChildren.Count > 1;
                            status = StatusEnum.RegistrationDecline;
                            action = AccessRightEnum.Status.ToRegistrationDeclineAttendant;
                            if (vm.SameChildren.Select(ss => ss.Request.SsoId).Any(ss => entity.SsoId != ss))
                            {
                                action = AccessRightEnum.Status.ToRegistrationDeclineChildDiffSSOId;
                            }
                            var sb = new StringBuilder();
                            foreach (var child in vm.SameChildren)
                            {
                                sb.Append($"{child.LastName} {child.FirstName} {child.MiddleName},");
                            }
                            var msg = sb.ToString();
                            msg = msg.Substring(0, msg.Length - 1);
                            decline = 202107;
                            //uts.ErrorText = $@"Заявление является повторным. Вы уже подали заявление о предоставлении услуг отдыха и оздоровления на {(plural ? "детей" : "ребёнка")} ({msg})";
                            //uts.ErrorText = $@"Заявление является повторным. На указанн{(plural ? "ых" : "ого")} в заявлении {(plural ? "детей" : "ребёнка")} ({msg}) уже подано заявление о предоставлении услуг отдыха и оздоровления.";
                            uts.ErrorText = $@"Заявление является повторным. На указанного(ых) в заявлении ребёнка(детей) ({msg}) уже подано заявление о предоставлении услуг отдыха и оздоровления.";
                        }

                        ApiRequest.CheckAttendants(vm);
                        // чудовищный костыль, связаннаый с косячным отправлением из МПГУ в случае с наличием доверенного лица, им достаточно проставить у себя галочку isagent 
                        if (!vm.Data.Agent.IsNullOrEmpty())
                        {
                            vm.SameAttendants.Clear();
                            vm.SameAttendantSnils.Clear();
                            //if (vm.Data.TypeOfRestId != (long)TypeOfRestEnum.YouthRestCamps && vm.Data.TypeOfRestId != (long)TypeOfRestEnum.YouthRestCamps)
                            vm.ApplicantDouble.Clear();
                        }
                        if (vm.SameAttendantSnils.Any() || vm.SameAttendants.Any())
                        {
                            status = StatusEnum.RegistrationDecline;
                            action = AccessRightEnum.Status.ToRegistrationDeclineAttendant;
                            if (vm.SameAttendants.Any())
                            {
                                bool plural = vm.SameAttendants.Count > 1;
                                var sb = new StringBuilder();
                                foreach (var req in vm.SameAttendants)
                                {
                                    sb.Append($"{req.LastName} {req.FirstName} {req.MiddleName},");
                                }
                                var msg = sb.ToString();
                                msg = msg.Substring(0, msg.Length - 1);
                                if (entity.TypeOfRestId == (long)TypeOfRestEnum.RestWithParentsPoor)
                                    //uts.ErrorText = $@"Заявление является повторным. На указанн{(plural ? "ых" : "ое")} в заявлении сопровождающ{(plural ? "их лиц" : "ее лицо")} ({msg}) уже подано заявление о предоставлении бесплатной путевки для отдыха и оздоровления.";
                                    uts.ErrorText = $@"Заявление является повторным. На указанное(ых) в заявлении сопровождающее(их) лицо(лиц) ({msg}) уже подано заявление о предоставлении бесплатной путевки для отдыха и оздоровления.";
                                if (entity.TypeOfRestId == (long)TypeOfRestEnum.MoneyOn3To7)
                                    //uts.ErrorText = $@"Заявление является повторным. На указанн{(plural ? "ых" : "ое")} в заявлении сопровождающ{(plural ? "их лиц" : "ее лицо")} ({msg}) уже подано заявление о предоставлении сертификата на отдых и оздоровление.";
                                    uts.ErrorText = $@"Заявление является повторным. На указанное(ых) в заявлении сопровождающее(их) лицо(лиц) ({msg}) уже подано заявление о предоставлении сертификата на отдых и оздоровление.";
                            }
                            if (entity.TypeOfRestId == (long)TypeOfRestEnum.RestWithParentsPoor) decline = 202102;
                            if (entity.TypeOfRestId == (long)TypeOfRestEnum.MoneyOn3To7) decline = 202103;
                        }
                    }

                    UnitOfWork.SaveChanges();

                    entity = UnitOfWork.GetById<Request>(entity.Id);
                    if (status == StatusEnum.Send && !isDeleted)
                    {
                        if (!ApiRequest.ValidOnInSameTime(new RequestViewModel(entity)))
                        {
                            status = StatusEnum.Reject;
                            decline =
                                DeclineSectionProcess.GetDeclineReason("CrossTime", entity.TypeOfRestId) ??
                                Settings.Default.CrossTime;
                        }
                    }

                    entity.StatusId = (long) status;
                    entity.DateChangeStatus = DateTime.Now;
                    entity.DeclineReasonId = decline;
                    entity.IsDeleted = isDeleted;
                    entity = UnitOfWork.Update(entity);
                    entity.UpdateIntervalDates();

                    if (!entity.IsDeleted)
                    {
                        UnitOfWork.SendChangeStatus(entity, entity.StatusId ?? (long) status, uts.ErrorText, action);
                    }

                    if (status == StatusEnum.Send && !entity.IsDeleted)
                    {
                        var needSendForBenefit = true;
                        if (entity.IsFirstCompany)
                        {
                            var model = new RequestViewModel(entity)
                                {TypeOfRestsAll = VocController.GetTypesOfRest(false)};
                            if (!ApiRequest.BadPersonInRequest(model))
                            {
                                RejectRequestAsBadPerson(model, entity);
                                needSendForBenefit = false;
                            }
                        }

                        entity.NeedSendForBenefit = needSendForBenefit;
                        entity.NeedSendToRelative = needSendForBenefit;
                        entity.NeedSendForCPMPK = needSendForBenefit;
                        entity.NeedSendForSnils = needSendForBenefit;
                        entity.NeedSendForPassport = needSendForBenefit;
                        entity.NeedSendForRegistrationByPassport = needSendForBenefit;
                        entity.NeedSendForAisoLegalRepresentation = needSendForBenefit;
                    }

                    UnitOfWork.SaveChanges();
                    tran.Complete();
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Ошибка разбора заявления", ex);
                using (var uw = new UnitOfWork())
                {
                    var exchangeUts = uw.GetById<ExchangeUTS>(exchangeUtsId);
                    exchangeUts.IsError = true;
                    exchangeUts.ErrorText = ex.Message;
                    exchangeUts.ErrorDescription = ex.ToString();
                    exchangeUts.ServiceNumber = string.IsNullOrEmpty(exchangeUts.ServiceNumber)
                        ? serviceNumber
                        : exchangeUts.ServiceNumber;
                    uw.SaveChanges();
                }

                throw;
            }
        }

        /// <summary>
        ///     отказать так как есть нарушения в предыдущей кампании
        /// </summary>
        internal void RejectRequestAsBadPerson(RequestViewModel model, Request entity)
        {
            var violations =
                model.BadAttendants?.Where(a => a.TypeViolationId.HasValue)
                    .Select(a => a.TypeViolationId.Value)
                    .ToList() ?? new List<long>();

            violations.AddRange(model.BadChildren?.Where(a => a.TypeViolationId.HasValue)
                .Select(a => a.TypeViolationId.Value)
                .ToList() ?? new List<long>());

            violations = violations.Distinct().ToList();

            var statusChanged = false;
            foreach (var v in violations)
            {
                if (violationToDecline.ContainsKey(v))
                {
                    UnitOfWork.RequestChangeStatusInternal(AccessRightEnum.Status.FcToReject, entity,
                        violationToDecline[v],
                        false);
                    statusChanged = true;
                    break;
                }
            }

            if (!statusChanged)
            {
                UnitOfWork.RequestChangeStatusInternal(AccessRightEnum.Status.FcToReject, entity,
                    violationToDecline.Values.FirstOrDefault(), false);
            }
        }

        private void ResetCheckApplicantInBaseRegistry(long applicantId, ExchangeBaseRegistryTypeEnum type)
        {
            var exchangeBaseRegistries =
                UnitOfWork.GetSet<ExchangeBaseRegistry>()
                    .Where(e => !e.NotActual && e.ApplicantId == applicantId &&
                                e.ExchangeBaseRegistryTypeId == (long) type)
                    .ToList();
            foreach (var ebr in exchangeBaseRegistries)
            {
                ebr.NotActual = true;
            }

            UnitOfWork.SaveChanges();
        }

        private void ResetCheckChildInBaseRegistry(long childId, long applicantId,
            ExchangeBaseRegistryTypeEnum type)
        {
            var exchangeBaseRegistries =
                UnitOfWork.GetSet<ExchangeBaseRegistry>()
                    .Where(e => !e.NotActual && e.ChildId == childId && e.ApplicantId == applicantId &&
                                e.ExchangeBaseRegistryTypeId == (long) type)
                    .ToList();

            foreach (var ebr in exchangeBaseRegistries)
            {
                ebr.NotActual = true;
            }

            UnitOfWork.SaveChanges();
        }


        /// <summary>
        ///     Сбросить значения для проверки
        /// </summary>
        private void ResetCheckChildInBaseRegistry(long childId, ExchangeBaseRegistryTypeEnum type)
        {
            var exchangeBaseRegistries =
                UnitOfWork.GetSet<ExchangeBaseRegistry>()
                    .Where(e => !e.NotActual && e.ChildId == childId &&
                                (e.ExchangeBaseRegistryTypeId == (long) type ||
                                 !e.ExchangeBaseRegistryTypeId.HasValue))
                    .ToList();
            foreach (var exchangeBaseRegistry in exchangeBaseRegistries)
            {
                exchangeBaseRegistry.NotActual = true;
            }

            UnitOfWork.SaveChanges();
        }

        [HttpPost]
        [HttpGet]
        public void RequestToRejectNoBenefit(long requestId)
        {
            var requestToReject = UnitOfWork.GetById<Request>(requestId);
            UnitOfWork.RequestChangeStatusInternal(AccessRightEnum.Status.FcToReject,
                                                    requestToReject, Settings.Default.ReasonNotHaveBenefit, false);
        }

        [HttpPost]
        [HttpGet]
        public void RequestToRejectWithReason(long requestId, long declineReason)
        {
            var requestToReject = UnitOfWork.GetById<Request>(requestId);
            UnitOfWork.RequestChangeStatusInternal(AccessRightEnum.Status.FcToReject,
                                                    requestToReject, declineReason, false);
        }
    }
}
