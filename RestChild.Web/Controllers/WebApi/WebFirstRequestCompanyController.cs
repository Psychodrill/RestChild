using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Http;
using System.Web.Script.Serialization;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using MimeTypes;
using Org.BouncyCastle.Ocsp;
using RestChild.Booking.Logic.Extensions;
using RestChild.Comon;
using RestChild.Comon.Dto.Booking;
using RestChild.Comon.Enumeration;
using RestChild.DAL.RepositoryExtensions;
using RestChild.Domain;
using RestChild.Extensions.Filter;
using RestChild.Extensions.Services;
using RestChild.Web.Common;
using RestChild.Web.CshedService;
using RestChild.Web.Models;
using Slepov.Russian.Morpher;
using Paragraph = DocumentFormat.OpenXml.Drawing.Paragraph;
using Run = DocumentFormat.OpenXml.Drawing.Run;
using RunProperties = DocumentFormat.OpenXml.Drawing.RunProperties;
using Settings = RestChild.Web.Properties.Settings;
using Text = DocumentFormat.OpenXml.Drawing.Text;

namespace RestChild.Web.Controllers.WebApi
{
    /// <summary>
    ///     Контроллер для работы с заявлением.
    /// </summary>
    [Authorize]
    public class WebFirstRequestCompanyController : BaseController, IRequestSaver
    {
        public WebCalculationController ApiCalculationController { get; set; }

        public override void SetUnitOfWorkInRefClass(IUnitOfWork unitOfWork)
        {
            base.SetUnitOfWorkInRefClass(unitOfWork);
            ApiCalculationController.SetUnitOfWorkInRefClass(unitOfWork);
        }

        /// <summary>
        ///     изменение статуса заявления.
        /// </summary>
        public bool RequestChangeStatus(long requestId, string actionCode, long? declineReason = null)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);

            var request = UnitOfWork.GetById<Request>(requestId);
            if (request == null || request.IsDeleted || !request.IsLast)
            {
                return false;
            }

            var statusId = request.StatusId;

            request = UnitOfWork.RequestChangeStatusInternal(actionCode, request, declineReason, true,
                Security.GetCurrentAccountId());

            return statusId != request.StatusId;
        }

        /// <summary>
        ///     сохранение заявления.
        /// </summary>
        [ActionName("SaveRequest")]
        [HttpPost]
        public Request SaveRequest(Request data, bool needCreateVersion = false, bool saveFileOnly = false)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);

            SetupOfferInRequest(data);
            var vouchers = data.InformationVouchers ?? new List<RequestInformationVoucher>();
            var vouchersPrice = vouchers.Where(v => v.AttendantsPrice != null).SelectMany(v => v.AttendantsPrice)
                .Where(a => a != null && a.ApplicantId <= 0).ToList();

            var voucherToChild = data.Child.Where(c => c.RequestInformationVoucherId.HasValue)
                .GroupedDictionary(c => c.RequestInformationVoucherId, c => c.IndexField);


            if (data.Id == 0)
            {
                var timesOfRest = data.TimesOfRest ?? new List<RequestsTimeOfRest>();
                var placesOfRest = data.PlacesOfRest ?? new List<RequestPlaceOfRest>();

                data.TimesOfRest = null;
                data.PlacesOfRest = null;

                data.InformationVouchers = new List<RequestInformationVoucher>();

                if (data.TypeOfRestId == (long) TypeOfRestEnum.MoneyOn3To7 ||
                    data.TypeOfRestId == (long) TypeOfRestEnum.MoneyOn7To15 ||
                    data.TypeOfRestId == (long) TypeOfRestEnum.MoneyOn18 ||
                    data.TypeOfRestId == (long) TypeOfRestEnum.MoneyOnInvalidOn4To17)
                {
                    data.RequestOnMoney = true;
                }

                if (data.Applicant != null)
                {
                    data.ApplicantId = null;
                    data.Applicant.IsLast = true;
                    data.Applicant.Payed = true;
                    if (data.TypeOfRestId == (long) TypeOfRestEnum.YouthRestCamps ||
                        data.TypeOfRestId == (long) TypeOfRestEnum.YouthRestOrphanCamps)
                    {
                        data.Applicant.IsAccomp = true;
                    }
                }

                if (data.Agent != null)
                {
                    data.Agent.Id = 0;
                    data.AgentId = 0;
                }

                data.IsLast = true;
                if (string.IsNullOrEmpty(data.RequestNumber))
                {
                    data.Version = (data.Version ?? 0) + 1;
                    data.IsDraft = false;
                    data.SourceId = data.SourceId ?? (long) SourceEnum.Operator;
                }

                foreach (var child in data.Child)
                {
                    child.RequestId = 0;
                    child.IsLast = true;
                    child.Payed = true;
                }

                foreach (var attendant in data.Attendant)
                {
                    attendant.RequestId = 0;
                    attendant.IsLast = true;
                    attendant.Payed = true;
                    attendant.Key = StaticHelpers.GenerateKey(
                        attendant.FirstName,
                        attendant.LastName,
                        attendant.MiddleName,
                        attendant.DocumentSeria,
                        attendant.DocumentNumber);
                    if (data.Tour != null && (data.TypeOfRest?.NeedAttendant ?? false))
                    {
                        attendant.IntervalStart = data.Tour.DateIncome?.Date.Ticks ?? 0;
                        attendant.IntervalEnd = data.Tour.DateOutcome?.Date.AddDays(1).Ticks ?? 0;
                    }
                    else
                    {
                        attendant.IntervalStart = null;
                        attendant.IntervalEnd = null;
                        if (!data.IsFirstCompany)
                        {
                            attendant.IsAccomp = false;
                        }

                        attendant.BoutId = null;
                    }
                }

                data.UpdateDate = DateTime.Now;
                data.CreateUserId = data.CreateUserId ?? Security.GetCurrentAccountId();
                data.StatusId = data.StatusId ?? (long) StatusEnum.Draft;

                var addonServiceLinks = data.AddonServicesLinks ?? new List<AddonServicesLink>();
                data.AddonServicesLinks = new List<AddonServicesLink>();
                var offers = data.OfferInRequest?.ToList() ?? new List<OfferInRequest>();
                data.OfferInRequest = null;

                var childs = data.Child;
                data.Child = new List<Child>();

                var attendants = data.Attendant;
                data.Attendant = new List<Applicant>();

                var applicant = data.Applicant;
                applicant.Request = null;
                applicant.RequestId = null;

                var applicantId = applicant.IndexField;
                var currentApplicant = applicant;
                applicant.Id = 0;
                if (data.TypeOfRestId != (long) TypeOfRestEnum.YouthRestOrphanCamps &&
                    data.TypeOfRestId != (long) TypeOfRestEnum.YouthRestCamps &&
                    data.TypeOfRestId != (long) TypeOfRestEnum.MoneyOn18)
                {
                    applicant.Address = null;
                    applicant.AddressId = null;
                }

                UnitOfWork.Context.Set<Applicant>().Add(applicant);
                UnitOfWork.SaveChanges();

                data.Applicant = applicant;
                data.ApplicantId = applicant.Id;

                data = UnitOfWork.AddEntity(data, false);
                UnitOfWork.SaveChanges();

                foreach (var asl in addonServiceLinks.Where(a => a.ChildId > 0 || a.ApplicantId > 0).ToList())
                {
                    asl.RequestId = data.Id;
                    var aslEntity = UnitOfWork.AddEntity(asl);
                    data.AddonServicesLinks.Add(aslEntity);
                }

                foreach (var child in childs.Where(vp => vp.ApplicantId == applicantId))
                {
                    child.ApplicantId = applicant.Id;
                }

                foreach (var voucher in vouchersPrice.Where(vp => vp.ApplicantId == applicantId))
                {
                    voucher.Applicant = applicant;
                    voucher.ApplicantId = applicant.Id;
                }

                foreach (var asl in addonServiceLinks.Where(o =>
                    o.Applicant?.Id == applicantId || o.ApplicantId == applicantId ||
                    o.Applicant == currentApplicant).ToList())
                {
                    asl.ApplicantId = applicant.Id;
                    asl.Applicant = null;
                    asl.RequestId = data.Id;
                    var aslEntity = UnitOfWork.AddEntity(asl);
                    data.AddonServicesLinks.Add(aslEntity);
                }

                foreach (var attendant in attendants)
                {
                    var attendantId = attendant.IndexField;
                    attendant.Id = 0;
                    attendant.RequestId = data.Id;

                    UnitOfWork.Context.Set<Applicant>().Add(attendant);
                    UnitOfWork.SaveChanges();
                    var model = attendant;

                    foreach (var child in childs.Where(vp => vp.ApplicantId == attendantId))
                    {
                        child.ApplicantId = model.Id;
                    }

                    foreach (var voucher in vouchersPrice.Where(vp => vp.ApplicantId == attendantId))
                    {
                        voucher.Applicant = model;
                        voucher.ApplicantId = model.Id;
                    }

                    foreach (var asl in addonServiceLinks.Where(o =>
                        o.Applicant?.Id == attendantId || o.ApplicantId == attendantId ||
                        o.Applicant == attendant).ToList())
                    {
                        asl.ApplicantId = model.Id;
                        asl.Applicant = null;
                        asl.RequestId = data.Id;
                        UnitOfWork.AddEntity(asl, false);
                        UnitOfWork.SaveChanges();
                        data.AddonServicesLinks.Add(asl);
                    }
                }

                foreach (var child in childs)
                {
                    var childId = child.Id;
                    child.RequestId = data.Id;
                    child.Id = 0;
                    child.RequestInformationVoucherId = null;
                    UnitOfWork.Context.Entry(child).State = EntityState.Added;
                    UnitOfWork.SaveChanges();
                    foreach (var asl in addonServiceLinks
                        .Where(o => o.Child?.Id == childId || o.ChildId == childId || o.Child == child).ToList())
                    {
                        asl.ChildId = child.Id;
                        asl.Child = null;
                        asl.RequestId = data.Id;
                        UnitOfWork.AddEntity(asl, false);
                        UnitOfWork.SaveChanges();
                        data.AddonServicesLinks.Add(asl);
                    }
                }

                foreach (var offer in offers)
                {
                    var offerLocal = UnitOfWork.GetById<OfferInRequest>(offer.Id);
                    if (offerLocal.RequestId != data.Id)
                    {
                        offerLocal.RequestId = data.Id;
                        UnitOfWork.Context.Entry(offerLocal).State = EntityState.Modified;
                    }
                }

                var keys = childs.ToDictionary(c => c.IndexField, c => c.Id);

                VoucherUpdate(data, vouchers, voucherToChild, keys);

                UnitOfWork.SaveChanges();

                foreach (var tor in timesOfRest)
                {
                    tor.RequestId = data.Id;
                    UnitOfWork.AddEntity(tor, false);
                }

                foreach (var por in placesOfRest)
                {
                    por.RequestId = data.Id;
                    UnitOfWork.AddEntity(por, false);
                }

                UnitOfWork.WriteHistory(data.Id, "Первое сохранение заявления", Security.GetCurrentAccountId());
                UnitOfWork.SaveChanges();
            }
            else
            {
                var entity = UnitOfWork.GetById<Request>(data.Id);

                if (entity.TypeOfRestId == (long) TypeOfRestEnum.MoneyOn3To7 ||
                    entity.TypeOfRestId == (long) TypeOfRestEnum.MoneyOn7To15 ||
                    data.TypeOfRestId == (long) TypeOfRestEnum.MoneyOn18 ||
                    data.TypeOfRestId == (long) TypeOfRestEnum.MoneyOnInvalidOn4To17)
                {
                    entity.RequestOnMoney = true;
                    data.RequestOnMoney = true;
                }

                if (entity.TypeOfRestId == (long) TypeOfRestEnum.YouthRestCamps ||
                    entity.TypeOfRestId == (long) TypeOfRestEnum.YouthRestOrphanCamps)
                {
                    if (entity.Applicant != null)
                    {
                        entity.Applicant.IsAccomp = true;
                    }

                    if (entity.Applicant != null)
                    {
                        data.Applicant.IsAccomp = true;
                    }
                }

                var oldRequest = new Request(entity, 3)
                {
                    Child = entity.Child?.Select(c => new Child(c, 1)).ToList(),
                    Attendant = entity.Attendant?.Select(a => new Applicant(a)).ToList(),
                    Files = entity.Files?.Select(f => new RequestFile(f)).ToList(),
                    AddonServicesLinks = entity.AddonServicesLinks?.Select(l => new AddonServicesLink(l, 1)).ToList(),
                    InformationVouchers = entity.InformationVouchers?.Select(v => new RequestInformationVoucher(v, 1)
                    {
                        AttendantsPrice = v.AttendantsPrice?.Select(a => new RequestInformationVoucherAttendant(a, 1))
                            .ToList()
                    }).ToList(),
                    PlacesOfRest = entity.PlacesOfRest?.Select(p => new RequestPlaceOfRest(p)).ToList(),
                    TimesOfRest = entity.TimesOfRest?.Select(t => new RequestsTimeOfRest(t)).ToList(),
                };

                data.StatusId = entity.StatusId;
                data.RequestNumber = entity.RequestNumber;
                data.RequestNumberMpgu = entity.RequestNumberMpgu;
                data.DateRequest = entity.DateRequest;
                data.SourceId = entity.SourceId;
                data.LastUpdateTick = entity.LastUpdateTick;

                data.TourId = entity.TourId ?? data.TourId;
                data.HotelsId = entity.HotelsId ?? data.HotelsId;
                data.BookingGuid = entity.BookingGuid ?? data.BookingGuid;

                if (!entity.IsLast || entity.IsDeleted)
                {
                    return entity;
                }

                // Сохраняем "Вид нарушения" только в статусе "Услуга оказана". Остальная логика не затрагивается.
                if (data.StatusId == (long) StatusEnum.CertificateIssued &&
                    Security.HasRight(AccessRightEnum.RequestEditTypeViolation))
                {
                    CopyTypeViolationFromModelToEntity(entity, data);
                }

                if (saveFileOnly)
                {
                    if (entity.ParentRequestId != null && entity.StatusId == (long) StatusEnum.IncludedInList)
                    {
                        entity.TimeOfRestId = data.TimeOfRestId;
                        entity.PlaceOfRestId = data.PlaceOfRestId;

                        DAL.UnitOfWork.MergeCollectionStatic(data.TimesOfRest, entity.TimesOfRest,
                            (s, t) => { t.TimeOfRestId = s.TimeOfRestId; }, (s) =>
                            {
                                if (s.TimeOfRestId.HasValue)
                                {
                                    s.RequestId = entity.Id;
                                    UnitOfWork.AddEntity(s);
                                }
                            }, (s) => { UnitOfWork.Delete(s); });

                        DAL.UnitOfWork.MergeCollectionStatic(data.PlacesOfRest, entity.PlacesOfRest,
                            (s, t) => { t.PlaceOfRestId = s.PlaceOfRestId; }, (s) =>
                            {
                                if (s.PlaceOfRestId.HasValue)
                                {
                                    s.RequestId = entity.Id;
                                    UnitOfWork.AddEntity(s);
                                }
                            },
                            (s) => { UnitOfWork.Delete(s); });
                    }

                    SaveFiles(data, entity);
                }
                else
                {
                    CopyRequest(data, entity);

                    // работа с файлами
                    SaveFiles(data, entity);

                    var applicant = entity.Applicant;
                    if (applicant.Id <= 0)
                    {
                        var appicantId = applicant.Id;
                        applicant = UnitOfWork.AddEntity(applicant);
                        entity.Applicant = applicant;
                        entity.ApplicantId = applicant.Id;

                        foreach (var voucher in vouchersPrice.Where(vp => vp.ApplicantId == appicantId))
                        {
                            voucher.Applicant = applicant;
                            voucher.ApplicantId = applicant.Id;
                        }
                    }

                    foreach (var child in entity.Child.Where(c => c.RequestInformationVoucherId <= 0).ToList())
                    {
                        child.RequestInformationVoucherId = null;
                    }

                    UnitOfWork.Context.Entry(entity).State = EntityState.Modified;

                    UnitOfWork.SaveChanges();

                    var keysAttendant = entity.Attendant.Where(a => !a.IsDeleted).ToDictionary(c => c.IndexField, c => c.Id);

                    foreach (var voucher in vouchersPrice.Where(vp => vp.ApplicantId < 0).ToList())
                    {
                        if (keysAttendant.ContainsKey(voucher.ApplicantId ?? 0))
                        {
                            voucher.ApplicantId = keysAttendant[(int) voucher.ApplicantId];
                        }
                    }

                    var keys = entity.Child.Where(c => !c.IsDeleted).ToDictionary(c => c.IndexField, c => c.Id);

                    data = UnitOfWork.Update(entity);

                    VoucherUpdate(data, vouchers, voucherToChild, keys);
                }

                List<RequestDiff> diff;

                // Это же условие в RestChild.Web\Views\FirstRequestCompany\RequestEdit.cshtml, все поля дизаблятся, сравнивать не нужно!
                if (data.StatusId != (long) StatusEnum.Draft &&
                    !Security.HasRight(AccessRightEnum.EditAfterRegistration))
                {
                    diff = RequestComparer.GetDiffForRequest(oldRequest, entity, UnitOfWork);
                }
                else
                {
                    diff = RequestComparer.GetDiffForRequest(oldRequest, data, UnitOfWork);
                }

                WriteHistory(data.Id, diff);
            }

            if (!data.EntityId.HasValue)
            {
                data.EntityId = data.Id;
            }
            else
            {
                var reqs =
                    UnitOfWork.GetSet<Request>()
                        .Where(r => r.IsLast && !r.IsDeleted && r.EntityId == data.EntityId && r.Id != data.Id)
                        .ToList();
                foreach (var req in reqs)
                {
                    req.IsLast = false;
                    if (req.Agent != null)
                    {
                        req.Agent.IsLast = false;
                    }

                    if (req.Applicant != null)
                    {
                        req.Applicant.IsLast = false;
                    }

                    if (req.Attendant != null && req.Attendant.Any())
                    {
                        foreach (var attendant in req.Attendant)
                        {
                            attendant.IsLast = false;
                        }
                    }
                }

                UnitOfWork.SaveChanges();

                var childsId = data.Child.Select(c => (long?) c.Id).ToArray();
                var childsEntityId = data.Child.Where(c => c.EntityId.HasValue).Select(c => c.EntityId).ToArray();

                var childs =
                    UnitOfWork.GetSet<Child>()
                        .Where(r => r.IsLast && childsEntityId.Contains(r.EntityId) && !childsId.Contains(r.Id))
                        .ToList();

                foreach (var child in childs)
                {
                    child.IsLast = false;
                }

                var attendantId = data.Attendant.Select(c => (long?) c.Id).ToList();
                var attendantEntityId = data.Attendant.Where(c => c.EntityId.HasValue).Select(c => c.EntityId).ToList();

                if (data.Applicant != null)
                {
                    attendantId.Add(data.Applicant.Id);
                    attendantEntityId.Add(data.Applicant.EntityId);
                }

                var attendants =
                    UnitOfWork.GetSet<Applicant>()
                        .Where(r => r.IsLast && attendantEntityId.Contains(r.EntityId) && !attendantId.Contains(r.Id))
                        .ToList();

                foreach (var attendant in attendants)
                {
                    attendant.IsLast = false;
                }
            }

            foreach (var attendant in data.Attendant)
            {
                if (!attendant.EntityId.HasValue)
                {
                    attendant.EntityId = attendant.Id;
                }
            }

            if (data.Applicant != null && !data.Applicant.EntityId.HasValue)
            {
                data.Applicant.EntityId = data.Applicant.Id;
            }

            foreach (var child in data.Child)
            {
                if (!child.EntityId.HasValue)
                {
                    child.EntityId = child.Id;
                }

                if (child.Applicant != null)
                {
                    var real = data.Applicant != null && data.Applicant.EntityId == child.Applicant.EntityId
                        ? data.Applicant
                        : data.Attendant.FirstOrDefault(a => a.EntityId == child.Applicant.EntityId);
                    if (real != null)
                    {
                        child.Applicant = real;
                        child.ApplicantId = real.Id;
                    }
                }
            }

            data.UpdateDate = DateTime.Now;
            data.CreateUserId = data.CreateUserId ?? Security.GetCurrentAccountId();
            data.StatusId = data.StatusId ?? (long) StatusEnum.Draft;

            UnitOfWork.SaveChanges();

            if (data.OfferInRequest != null)
            {
                foreach (var offer in data.OfferInRequest)
                {
                    offer.RoomRates = offer.RoomRatesId.HasValue
                        ? UnitOfWork.GetById<RoomRates>(offer.RoomRatesId.Value)
                        : null;
                }
            }

            data.UpdateIntervalDates();
            UpdateKey(data);

            // сохранение связи с бронированием.
            if (data.BookingGuid.HasValue)
            {
                var bookings = UnitOfWork.GetSet<Domain.Booking>().Where(b => b.Code == data.BookingGuid).ToList();
                foreach (var booking in bookings)
                {
                    booking.RequestId = data.Id;
                }
            }

            UnitOfWork.SaveChanges();

            return data;
        }

        /// <summary>
        ///     проверка на нарушителей
        /// </summary>
        internal bool BadPersonInRequest(RequestViewModel model)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);

            if (model == null)
            {
                throw new ArgumentNullException("model");
            }

            if (model.Data == null)
            {
                throw new ArgumentNullException("model.Data");
            }

            if (model.BadAttendants == null)
            {
                model.BadAttendants = new List<Applicant>();
            }

            if (model.BadChildren == null)
            {
                model.BadChildren = new List<Child>();
            }

            if (model.Data.ParentRequestId.HasValue)
            {
                return true;
            }

            var requestYearOfRest = model.Data?.YearOfRest?.Year ??
                                    UnitOfWork.GetById<YearOfRest>(model.Data?.YearOfRestId)?.Year ?? DateTime.Now.Year;

            var needAttendant = (model.Data?.TypeOfRest ??
                                 model.TypeOfRestsAll?.FirstOrDefault(t => t.Id == model.Data?.TypeOfRestId))
                                ?.NeedAttendant ??
                                false;

            if (needAttendant || model.Data.TypeOfRestId == (long) TypeOfRestEnum.YouthRestOrphanCamps)
            {
                if (model.Applicant != null && ((model.Applicant?.Data?.IsAccomp ?? false) ||
                                                model.Data.TypeOfRestId == (long) TypeOfRestEnum.YouthRestOrphanCamps))
                {
                    var applicants =
                        UnitOfWork.GetSet<Applicant>()
                            .Where(
                                a =>
                                    model.Applicant.Data.EntityId != a.EntityId && a.IsLast &&
                                    (model.Applicant.Data.Key == a.Key ||
                                     model.Applicant.Data.Snils == a.Snils && !string.IsNullOrEmpty(a.Snils))
                                    && a.TypeViolationId.HasValue
                                    && (
                                        a.Request == null && a.ChildList == null ||
                                        a.Request.IsLast && !a.Request.IsDeleted &&
                                        FirstRequestCompanyExtension.LiveRequestStatuses.Contains(a.Request.StatusId)
                                        && (a.Request.YearOfRest.Year == requestYearOfRest ||
                                            a.Request.YearOfRest.Year == requestYearOfRest - 1)
                                        || !a.ChildList.IsDeleted && a.ChildList.IsLast &&
                                        a.ChildList.StateId != StateMachineStateEnum.Deleted
                                        && a.ChildList.StateId != StateMachineStateEnum.Limit.List.Formation))
                            .ToList();

                    model.BadAttendants.AddRange(applicants.Where(a => a.RequestId.HasValue || a.ChildListId.HasValue)
                        .ToList());
                    var addonCheck = applicants.Where(a => !a.RequestId.HasValue && !a.ChildListId.HasValue).ToList();
                    if (addonCheck.Any())
                    {
                        foreach (var applicant in addonCheck)
                        {
                            var request = UnitOfWork.GetSet<Request>()
                                .FirstOrDefault(
                                    r =>
                                        r.ApplicantId == applicant.Id && r.IsLast && !r.IsDeleted &&
                                        FirstRequestCompanyExtension.LiveRequestStatuses.Contains(r.StatusId) &&
                                        (r.YearOfRest.Year == requestYearOfRest ||
                                         r.YearOfRest.Year == requestYearOfRest - 1));
                            if (request != null)
                            {
                                model.BadAttendants.Add(new Applicant(applicant)
                                {
                                    Request = request,
                                    RequestId = request.Id,
                                    DocumentType = applicant.DocumentType,
                                    TypeViolation = applicant.TypeViolation
                                });
                            }
                        }
                    }
                }

                if (model.Attendant != null)
                {
                    var attendants =
                        model.Attendant.Where(a => a.Data?.IsAccomp ?? false).SelectMany(
                                attendant =>
                                    UnitOfWork.GetSet<Applicant>()
                                        .Where(
                                            a =>
                                                attendant.Data.EntityId != a.EntityId && a.IsLast &&
                                                (attendant.Data.Key == a.Key &&
                                                 attendant.Data.Key != null ||
                                                 attendant.Data.Snils ==
                                                 a.Snils &&
                                                 !string.IsNullOrEmpty(a.Snils))
                                                && a.TypeViolationId.HasValue
                                                && (
                                                    a.Request == null && a.ChildList == null ||
                                                    a.Request.IsLast && !a.Request.IsDeleted &&
                                                    FirstRequestCompanyExtension.LiveRequestStatuses.Contains(
                                                        a.Request.StatusId)
                                                    && (a.Request.YearOfRest.Year == requestYearOfRest ||
                                                        a.Request.YearOfRest.Year == requestYearOfRest - 1)
                                                    || !a.ChildList.IsDeleted && a.ChildList.IsLast &&
                                                    a.ChildList.StateId != StateMachineStateEnum.Deleted
                                                    && a.ChildList.StateId !=
                                                    StateMachineStateEnum.Limit.List.Formation)))
                            .ToList();

                    model.BadAttendants.AddRange(attendants.Where(a => a.RequestId.HasValue || a.ChildListId.HasValue)
                        .ToList());
                    var addonCheck = attendants.Where(a => !a.RequestId.HasValue && !a.ChildListId.HasValue).ToList();
                    if (addonCheck.Any())
                    {
                        foreach (var applicant in addonCheck)
                        {
                            var request = UnitOfWork.GetSet<Request>()
                                .FirstOrDefault(
                                    r => r.ApplicantId == applicant.Id && r.IsLast && !r.IsDeleted &&
                                         FirstRequestCompanyExtension.LiveRequestStatuses.Contains(r.StatusId) &&
                                         (r.YearOfRest.Year == requestYearOfRest ||
                                          r.YearOfRest.Year == requestYearOfRest - 1));
                            if (request != null)
                            {
                                model.BadAttendants.Add(new Applicant(applicant)
                                {
                                    Request = request,
                                    RequestId = request.Id,
                                    DocumentType = applicant.DocumentType,
                                    TypeViolation = applicant.TypeViolation
                                });
                            }
                        }
                    }
                }
            }

            if (model.Child != null)
            {
                var childQueue =
                    UnitOfWork.GetSet<Child>()
                        .Where(
                            c =>
                                FirstRequestCompanyExtension.LiveRequestStatuses.Contains(c.Request.StatusId) &&
                                !c.Request.IsDeleted &&
                                c.Request.IsLast && c.TypeViolationId.HasValue);

                foreach (var child in model.Child)
                {
                    var documentSeria = (child.Data.DocumentSeria ?? string.Empty).ToLower();
                    var documentNumber = (child.Data.DocumentNumber ?? string.Empty).ToLower();
                    var documentSeriaCb = (child.Data.DocumentSeriaCertOfBirth ?? string.Empty).ToLower();
                    var documentNumberCb = (child.Data.DocumentNumberCertOfBirth ?? string.Empty).ToLower();

                    var yearOfRest = child.Data.YearOfCompany.HasValue && child.Data.YearOfCompany != 0
                        ? child.Data.YearOfCompany
                        : model.Data?.YearOfRest?.Year;

                    var q = childQueue;

                    var entityId = child.Data?.EntityId ?? child.Data?.Id;
                    if (entityId != 0)
                    {
                        q = q.Where(c => (c.EntityId ?? c.Id) != entityId);
                    }

                    if (string.IsNullOrWhiteSpace(documentSeria) && string.IsNullOrWhiteSpace(documentNumber))
                    {
                        q = q.Where(c => c.Snils == child.Data.Snils && !string.IsNullOrEmpty(c.Snils));
                    }
                    else if (string.IsNullOrWhiteSpace(documentSeriaCb) && string.IsNullOrWhiteSpace(documentNumberCb))
                    {
                        q =
                            q.Where(
                                c =>
                                    (c.DocumentSeria ?? string.Empty).ToLower() == documentSeria &&
                                    c.DocumentNumber.ToLower() == documentNumber ||
                                    (c.DocumentSeriaCertOfBirth ?? string.Empty).ToLower() == documentSeria &&
                                    (c.DocumentNumberCertOfBirth ?? string.Empty).ToLower() == documentNumber &&
                                    !string.IsNullOrEmpty(c.DocumentNumberCertOfBirth) ||
                                    c.Snils == child.Data.Snils && !string.IsNullOrEmpty(c.Snils));
                    }
                    else
                    {
                        q = q.Where(
                            c =>
                                (c.DocumentSeria ?? string.Empty).ToLower() == documentSeria &&
                                c.DocumentNumber.ToLower() == documentNumber ||
                                (c.DocumentSeriaCertOfBirth ?? string.Empty).ToLower() == documentSeria &&
                                (c.DocumentNumberCertOfBirth ?? string.Empty).ToLower() == documentNumber ||
                                (c.DocumentSeria ?? string.Empty).ToLower() == documentSeriaCb &&
                                c.DocumentNumber.ToLower() == documentNumberCb ||
                                (c.DocumentSeriaCertOfBirth ?? string.Empty).ToLower() == documentSeriaCb &&
                                (c.DocumentNumberCertOfBirth ?? string.Empty).ToLower() == documentNumberCb ||
                                c.Snils == child.Data.Snils && !string.IsNullOrEmpty(c.Snils));
                    }

                    var children = q.Where(c => c.YearOfCompany <= yearOfRest && c.YearOfCompany + 1 >= yearOfRest)
                        .ToList();

                    model.BadChildren.AddRange(children);
                }
            }

            return !(model.BadAttendants.Any() || model.BadChildren.Any());
        }

        internal bool ValidOnInSameTime(RequestViewModel model)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);

            if (model == null)
            {
                throw new ArgumentNullException("model");
            }

            if (model.Data == null)
            {
                throw new ArgumentNullException("model.Data");
            }

            if (model.CrossTimeAttendants == null)
            {
                model.CrossTimeAttendants = new List<Applicant>();
            }

            if (model.CrossTimeChilds == null)
            {
                model.CrossTimeChilds = new List<Child>();
            }

            var tour = model.Data.Tour ?? UnitOfWork.GetById<Tour>(model.Data.TourId);

            if (tour == null)
            {
                return true;
            }

            if (model.Applicant != null && model.Applicant.Data.IsAccomp)
            {
                var intervalStart = model.Applicant.Data.IntervalStart ?? tour.DateIncome?.Ticks;
                var intervalEnd = model.Applicant.Data.IntervalEnd ?? tour.DateOutcome?.AddDays(1).Ticks;
                var key = model.Applicant.Data.Key ?? StaticHelpers.GenerateKey(
                    model.Applicant.Data.FirstName,
                    model.Applicant.Data.LastName,
                    model.Applicant.Data.MiddleName,
                    model.Applicant.Data.DocumentSeria,
                    model.Applicant.Data.DocumentNumber);

                if (!string.IsNullOrWhiteSpace(key))
                {
                    var applicants =
                        UnitOfWork.GetSet<Applicant>()
                            .Where(
                                a =>
                                    model.Applicant.Data.EntityId != a.EntityId && a.IsLast && a.IsAccomp &&
                                    key == a.Key
                                    && a.IntervalStart < intervalEnd && a.IntervalEnd > intervalStart
                                    && (
                                        a.Request == null && a.ChildList == null ||
                                        a.Request.IsLast && !a.Request.IsDeleted &&
                                        a.Request.ParentRequestId == null &&
                                        a.Request.StatusId != (long) StatusEnum.Draft
                                        && a.Request.StatusId != (long) StatusEnum.CancelByApplicant
                                        && a.Request.StatusId != (long) StatusEnum.ErrorRequest &&
                                        a.Request.StatusId != (long) StatusEnum.Reject
                                        || !a.ChildList.IsDeleted && a.ChildList.IsLast &&
                                        a.ChildList.StateId != StateMachineStateEnum.Deleted
                                        && a.ChildList.StateId != StateMachineStateEnum.Limit.List.Formation))
                            .ToList();

                    model.CrossTimeAttendants.AddRange(applicants
                        .Where(a => a.RequestId.HasValue || a.ChildListId.HasValue)
                        .ToList());
                    var addonCheck = applicants.Where(a => !a.RequestId.HasValue && !a.ChildListId.HasValue).ToList();
                    if (addonCheck.Any())
                    {
                        foreach (var applicant in addonCheck)
                        {
                            var requestApplicant = UnitOfWork.GetSet<Request>()
                                .FirstOrDefault(
                                    r => r.ApplicantId == applicant.Id && r.IsLast && !r.IsDeleted &&
                                         r.StatusId != (long) StatusEnum.Draft &&
                                         r.StatusId != (long) StatusEnum.CancelByApplicant &&
                                         r.StatusId != (long) StatusEnum.ErrorRequest &&
                                         r.StatusId != (long) StatusEnum.Reject);

                            if (requestApplicant != null)
                            {
                                model.CrossTimeAttendants.Add(new Applicant(applicant)
                                {
                                    DocumentType = applicant.DocumentType,
                                    Request = requestApplicant,
                                    RequestId = requestApplicant.Id
                                });
                            }
                        }
                    }
                }
            }

            if (model.Attendant != null)
            {
                var attendants = new List<Applicant>();
                foreach (var attendant in model.Attendant)
                {
                    var intervalStart = attendant.Data.IntervalStart ?? tour?.DateIncome?.Ticks;
                    var intervalEnd = attendant.Data.IntervalEnd ?? tour?.DateOutcome?.AddDays(1).Ticks;
                    var key = attendant.Data.Key ?? StaticHelpers.GenerateKey(
                        attendant.Data.FirstName,
                        attendant.Data.LastName,
                        attendant.Data.MiddleName,
                        attendant.Data.DocumentSeria,
                        attendant.Data.DocumentNumber);

                    attendants.AddRange(UnitOfWork.GetSet<Applicant>()
                        .Where(
                            a =>
                                attendant.Data.EntityId != a.EntityId && a.IsLast && a.IsAccomp && key == a.Key &&
                                key != null
                                && a.IntervalStart < intervalEnd && a.IntervalEnd > intervalStart
                                && (
                                    a.Request == null && a.ChildList == null ||
                                    a.Request.IsLast && !a.Request.IsDeleted &&
                                    a.Request.ParentRequestId == null &&
                                    a.Request.StatusId != (long) StatusEnum.Draft
                                    && a.Request.StatusId != (long) StatusEnum.CancelByApplicant
                                    && a.Request.StatusId != (long) StatusEnum.ErrorRequest &&
                                    a.Request.StatusId != (long) StatusEnum.Reject
                                    || !a.ChildList.IsDeleted && a.ChildList.IsLast &&
                                    a.ChildList.StateId != StateMachineStateEnum.Deleted
                                    && a.ChildList.StateId != StateMachineStateEnum.Limit.List.Formation)).ToList());
                }

                model.CrossTimeAttendants.AddRange(attendants.Where(a => a.RequestId.HasValue || a.ChildListId.HasValue)
                    .ToList());
                var addonCheck = attendants.Where(a => !a.RequestId.HasValue && !a.ChildListId.HasValue).ToList();
                if (addonCheck.Any())
                {
                    foreach (var applicant in addonCheck)
                    {
                        var requestApplicant = UnitOfWork.GetSet<Request>()
                            .FirstOrDefault(
                                r => r.ApplicantId == applicant.Id && r.IsLast && !r.IsDeleted &&
                                     r.StatusId != (long) StatusEnum.Draft &&
                                     r.StatusId != (long) StatusEnum.CancelByApplicant &&
                                     r.StatusId != (long) StatusEnum.ErrorRequest &&
                                     r.StatusId != (long) StatusEnum.Reject);
                        if (requestApplicant != null)
                        {
                            model.CrossTimeAttendants.Add(new Applicant(applicant)
                            {
                                DocumentType = applicant.DocumentType,
                                Request = requestApplicant,
                                RequestId = requestApplicant.Id
                            });
                        }
                    }
                }
            }

            if (model.Child != null)
            {
                var childs =
                    model.Child.SelectMany(
                        child =>
                        {
                            var intervalStart = child.Data.IntervalStart ?? tour.DateIncome?.Ticks;
                            var intervalEnd = child.Data.IntervalEnd ?? tour?.DateOutcome?.AddDays(1).Ticks;
                            return UnitOfWork.GetSet<Child>()
                                .Where(
                                    c =>
                                        child.Data.EntityId != c.EntityId && c.IsLast &&
                                        child.Data.DocumentSeria == c.DocumentSeria &&
                                        child.Data.DocumentNumber == c.DocumentNumber
                                        && c.IntervalStart < intervalEnd && c.IntervalEnd > intervalStart &&
                                        !c.IsDeleted
                                        && (c.Request.IsLast && !c.Request.IsDeleted &&
                                            c.Request.ParentRequestId == null &&
                                            c.Request.StatusId != (long) StatusEnum.Draft
                                            && c.Request.StatusId != (long) StatusEnum.CancelByApplicant
                                            && c.Request.StatusId != (long) StatusEnum.ErrorRequest &&
                                            c.Request.StatusId != (long) StatusEnum.Reject
                                            || !c.ChildList.IsDeleted && c.ChildList.IsLast &&
                                            c.ChildList.StateId != StateMachineStateEnum.Deleted
                                            && c.ChildList.StateId != StateMachineStateEnum.Limit.List.Formation));
                        }).ToList();

                model.CrossTimeChilds.AddRange(childs);
            }

            if (model.CrossTimeAttendants.Any() || model.CrossTimeChilds.Any())
            {
                return false;
            }

            return true;
        }

        /// <summary>
        ///     проверить заявителя на дубль по семейному отдыху
        /// </summary>
        public void CheckApplicant(RequestViewModel model)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);

            if (model.Data.ParentRequestId != null)
            {
                return;
            }

            model.ApplicantDouble = new List<Request>();

            if (model.Data.TypeOfRestId == (long) TypeOfRestEnum.YouthRestCamps ||
                model.Data.TypeOfRestId == (long) TypeOfRestEnum.YouthRestOrphanCamps)
            {
                var snils = model.Applicant?.Data?.Snils;
                model.ApplicantDouble = UnitOfWork.GetSet<Request>().Where(r =>
                        !r.IsDeleted && r.Applicant.Snils == snils
                                     && r.Id != model.Data.Id
                                     && r.StatusId != (long) StatusEnum.Draft
                                     && r.StatusId != (long) StatusEnum.RegistrationDecline
                                     && r.StatusId != (long) StatusEnum.Reject
                                     && r.StatusId != (long) StatusEnum.CancelByApplicant
                                     && r.StatusId != (long) StatusEnum.Denial
                                     && r.YearOfRestId == model.Data.YearOfRestId
                                     && r.ParentRequestId == null
                                     && (r.TypeOfRestId == (long) TypeOfRestEnum.YouthRestCamps ||
                                         r.TypeOfRestId == (long) TypeOfRestEnum.YouthRestOrphanCamps))
                    .ToList();
            }

            /*
            // По решению сотрудников МГТ (Иванов и Сердешнова) откатить данный функционал - в связи с опасениями судебных исков.
            var type = UnitOfWork.GetById<TypeOfRest>(model.Data.TypeOfRestId);

            while (type != null && type.Id != (long) TypeOfRestEnum.RestWithParents)
            {
                type = type.Parent;
            }

            var snils = model.Applicant?.Data?.Snils;

            if (type == null || string.IsNullOrWhiteSpace(snils))
            {
                return;
            }

            model.ApplicantDouble = UnitOfWork.GetSet<Request>().Where(r => !r.IsDeleted && r.Applicant.Snils == snils
                                                                                         && r.Id != model.Data.Id
                                                                                         && r.StatusId !=
                                                                                         (long) StatusEnum.Draft
                                                                                         && r.StatusId !=
                                                                                         (long) StatusEnum
                                                                                             .RegistrationDecline
                                                                                         && r.StatusId !=
                                                                                         (long) StatusEnum.Reject
                                                                                         && r.StatusId !=
                                                                                         (long) StatusEnum
                                                                                             .CancelByApplicant
                                                                                         && r.StatusId !=
                                                                                         (long) StatusEnum.Denial
                                                                                         && r.YearOfRestId ==
                                                                                         model.Data.YearOfRestId
                                                                                         && r.TypeOfRest
                                                                                             .NeedAttendant &&
                                                                                         r.TypeOfRest
                                                                                             .FirstRequestCompanySelect)
                .ToList();
                */
        }

        /// <summary>
        ///     проверка на дубли в заявлении
        /// </summary>
        public void CheckAttendants(RequestViewModel model, bool onlySame = false)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);

            if (model.Data.ParentRequestId != null)
            {
                return;
            }

            model.SameAttendants = new List<Applicant>();
            model.SameAttendantSnils = new List<string>();

            if (model.ParentInvalid != null)
            {
                return;
            }

            var attendants = new List<Applicant>();
            if (model.Applicant.Data.IsAccomp)
            {
                attendants.Add(model.Data.Applicant);
            }

            if (model.Agent?.DataApplicant?.Id > 0)
            {
                attendants.Add(model.Agent.DataApplicant);
            }

            attendants.AddRange(model.Attendant.Select(a => a.Data));

            model.SameAttendantSnils = attendants.Where(a => !string.IsNullOrWhiteSpace(a.Snils)).GroupBy(g => g.Snils)
                .Where(g => g.Count() > 1).Select(g => g.Key)
                .ToList();
            if (attendants.Any(a => string.IsNullOrWhiteSpace(a.Snils)))
            {
                model.SameAttendants = attendants
                    .Where(a => !model.SameAttendantSnils.Contains(a.Snils ?? string.Empty))
                    .GroupBy(g => (g.DocumentSeria ?? string.Empty) + "---" + (g.DocumentNumber ?? string.Empty))
                    .Where(g => g.Count() > 1).SelectMany(g => g.Select(f => f).ToList()).ToList();
            }
        }

        /// <summary>
        ///     проверка на дубли детей.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="onlySame">только такие же.</param>
        public void CheckChildren(RequestViewModel model, bool onlySame = false)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);

            if (model.Data?.ParentRequestId != null)
            {
                return;
            }

            model.SameChildren = new List<Child>();
            model.SimilarChildren = new List<Child>();

            var childQueue =
                UnitOfWork.GetSet<Child>()
                    .Where(
                        c =>
                            FirstRequestCompanyExtension.LiveRequestStatuses.Contains(c.Request.StatusId) &&
                            !c.Request.IsDeleted &&
                            c.Request.IsLast && c.Request.ParentRequestId == null);

            var sameChildrenIds = new List<long>();

            var typeOfRest = UnitOfWork.GetById<TypeOfRest>(model.Data.TypeOfRestId);
            while (typeOfRest?.Parent != null)
            {
                typeOfRest = typeOfRest.Parent;
            }

            foreach (var child in model.Child)
            {
                var snils = child.Data.Snils ?? string.Empty;
                var documentSeria = (child.Data.DocumentSeria ?? string.Empty).ToLower();
                var documentNumber = (child.Data.DocumentNumber ?? string.Empty).ToLower();
                var typeOfGroupCheckId = child.Data.TypeOfGroupCheckId ??
                                         child.Data.BenefitType?.TypeOfGroupCheckId ??
                                         model.Data?.TypeOfRest?.TypeOfGroupCheckId;
                var yearOfRest = child.Data?.YearOfCompany > 0
                    ? child.Data.YearOfCompany
                    : model.Data?.YearOfRest?.Year ?? UnitOfWork.GetById<YearOfRest>(model.Data?.YearOfRestId)?.Year;

                var q = childQueue;

                if (string.IsNullOrWhiteSpace(documentSeria) && string.IsNullOrWhiteSpace(documentNumber) &&
                    string.IsNullOrWhiteSpace(snils))
                {
                    q = q.Where(c => false);
                }
                else if (string.IsNullOrWhiteSpace(documentSeria) && string.IsNullOrWhiteSpace(documentNumber))
                {
                    q = q.Where(c => c.Snils == snils);
                }
                else if (string.IsNullOrWhiteSpace(snils))
                {
                    q = q.Where(
                        c =>
                            (c.DocumentSeria ?? string.Empty).ToLower() == documentSeria &&
                            c.DocumentNumber.ToLower() == documentNumber
                    );
                }
                else
                {
                    q = q.Where(
                        c =>
                            c.Snils == snils ||
                            (c.DocumentSeria ?? string.Empty).ToLower() == documentSeria &&
                            c.DocumentNumber.ToLower() == documentNumber
                    );
                }

                var entityId = child.Data?.EntityId ?? child?.Data?.Id;
                if ((entityId ?? 0) != 0)
                {
                    q = q.Where(c => (c.EntityId ?? c.Id) != entityId);
                }

                var childs =
                    q.Where(c =>
                        (typeOfGroupCheckId != null
                            ? c.TypeOfGroupCheckId == typeOfGroupCheckId && c.YearOfCompany <= yearOfRest &&
                              c.YearOfCompany + c.TypeOfGroupCheck.Period - 1 >= yearOfRest
                            : !c.TypeOfGroupCheckId.HasValue && c.YearOfCompany == yearOfRest) &&
                        !sameChildrenIds.Contains(c.Id)).ToList();

                model.SameChildren.AddRange(childs);
                sameChildrenIds.AddRange(childs.Select(s => s.Id).ToList());

                // проверка на совместный отдых
                if ((long) TypeOfRestEnum.RestWithParents == typeOfRest?.Id)
                {
                    childs = q.Where(c =>
                            c.YearOfCompany == yearOfRest &&
                            !sameChildrenIds.Contains(c.Id)
                        )
                        .Where(
                            c =>
                                c.Request.TypeOfRestId == (long) TypeOfRestEnum.RestWithParents ||
                                c.Request.TypeOfRest.ParentId == (long) TypeOfRestEnum.RestWithParents ||
                                c.Request.TypeOfRest.Parent.ParentId == (long) TypeOfRestEnum.RestWithParents ||
                                c.Request.TypeOfRest.Parent.Parent.ParentId == (long) TypeOfRestEnum.RestWithParents)
                        .Where(c => c.Request.StatusId != (long) StatusEnum.RegistrationDecline
                                    && c.Request.StatusId != (long) StatusEnum.CancelByApplicant
                                    && c.Request.StatusId != (long) StatusEnum.Reject
                                    && c.Request.StatusId != (long) StatusEnum.ErrorRequest
                                    && !c.Request.IsDeleted)
                        .ToList();

                    model.SameChildren.AddRange(childs);
                    sameChildrenIds.AddRange(childs.Select(s => s.Id).ToList());
                }
            }

            if (onlySame)
            {
                return;
            }

            foreach (
                var child in
                model.Child.Where(
                    c =>
                        !string.IsNullOrEmpty(c.Data.LastName) && !string.IsNullOrEmpty(c.Data.FirstName) &&
                        c.Data.DateOfBirth.HasValue)
            )
            {
                var keySame =
                    StaticHelpers.GenerateKeySame(child.Data.FirstName, child.Data.LastName, child.Data.DateOfBirth);
                var typeOfGroupCheckId = child.Data.TypeOfGroupCheckId;
                var yearOfRest = child.Data.YearOfCompany.HasValue && child.Data.YearOfCompany != 0
                    ? child.Data.YearOfCompany
                    : model.NullSafe(m => m.Data.YearOfRest.Year);

                var entityId = child.Data?.EntityId ?? child?.Data?.Id;
                var q = childQueue;
                if ((entityId ?? 0) != 0)
                {
                    q = childQueue.Where(c => (c.EntityId ?? c.Id) != entityId);
                }


                var childs = q.Where(c => c.KeySame == keySame &&
                                          (typeOfGroupCheckId != null
                                              ? c.TypeOfGroupCheckId == typeOfGroupCheckId &&
                                                c.YearOfCompany <= yearOfRest &&
                                                c.YearOfCompany + c.TypeOfGroupCheck.Period - 1 >= yearOfRest
                                              : !c.TypeOfGroupCheckId.HasValue && c.YearOfCompany == yearOfRest) &&
                                          !sameChildrenIds.Contains(c.Id)
                ).ToList();

                sameChildrenIds.AddRange(childs.Select(s => s.Id).ToList());
                model.SimilarChildren.AddRange(childs);

                // проверка на совместный отдых
                if ((long) TypeOfRestEnum.RestWithParents == typeOfRest?.Id)
                {
                    childs = q.Where(c =>
                            c.YearOfCompany == yearOfRest &&
                            c.KeySame == keySame &&
                            !sameChildrenIds.Contains(c.Id)
                        )
                        .Where(
                            c =>
                                c.Request.TypeOfRestId == (long) TypeOfRestEnum.RestWithParents ||
                                c.Request.TypeOfRest.ParentId == (long) TypeOfRestEnum.RestWithParents ||
                                c.Request.TypeOfRest.Parent.ParentId == (long) TypeOfRestEnum.RestWithParents ||
                                c.Request.TypeOfRest.Parent.Parent.ParentId == (long) TypeOfRestEnum.RestWithParents)
                        .Where(c => c.Request.StatusId != (long) StatusEnum.CancelByApplicant &&
                                    c.Request.StatusId != (long) StatusEnum.Reject &&
                                    c.Request.StatusId != (long) StatusEnum.ErrorRequest)
                        .ToList();

                    sameChildrenIds.AddRange(childs.Select(s => s.Id).ToList());
                    model.SimilarChildren.AddRange(childs);
                }
            }
        }

        /// <summary>
        ///     изменение статуса заявления c созданием версии если нужно.
        /// </summary>
        internal void RequestChangeStatusWithCreateVersion(long requestId, string actionCode, long? decline = null)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);

            var request = UnitOfWork.GetById<Request>(requestId);

            if (request == null || !request.IsLast || request.IsDeleted)
            {
                return;
            }

            UnitOfWork.RequestChangeStatusInternal(actionCode, request, decline, false);
        }

        /// <summary>
        ///     Запись в историю изменений по заявлению
        /// </summary>
        private void WriteHistory(long id, string operation)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);

            var history = new HistoryRequest
            {
                AccountId = Security.GetCurrentAccountId(),
                OperationDate = DateTime.Now,
                RequestId = id,
                LastUpdateTick = DateTime.Now.Ticks,
                Operation = operation
            };

            UnitOfWork.AddEntity(history);
        }

        /// <summary>
        ///     Запись в историю изменений по заявлению
        /// </summary>
        private void WriteHistory(long id, ICollection<RequestDiff> diffCollection)
        {
            var diffHtml = new StringBuilder();
            if (diffCollection != null && diffCollection.Any())
            {
                foreach (var diff in diffCollection)
                {
                    var skl = new Склонятель();
                    var objName = skl.Проанализировать(diff.ObjectName ?? string.Empty);
                    diffHtml.AppendFormat(
                        "<p>{0} {1}</p>",
                        diff.Action,
                        ((objName != null ? objName.Родительный : diff.ObjectName) ?? string.Empty).ToLower());
                    diffHtml.Append("<ul>");
                    if (diff.Items != null)
                    {
                        foreach (var diffItem in diff.Items)
                        {
                            if (!string.IsNullOrEmpty(diffItem.OldFieldValue) ||
                                !string.IsNullOrEmpty(diffItem.NewFieldValue))
                            {
                                var oldField = string.IsNullOrEmpty(diffItem.OldFieldValue)
                                    ? "старое значение: сброшено,"
                                    : "старое значение: &quot;" + diffItem.OldFieldValue + "&quot;,";
                                var newField = string.IsNullOrEmpty(diffItem.NewFieldValue)
                                    ? "сброшено"
                                    : "&quot;" + diffItem.NewFieldValue + "&quot;";
                                diffHtml.AppendFormat(
                                    "<li>Изменено поле &quot;{0}&quot; {1} новое значение: {2}</li>",
                                    diffItem.FieldName,
                                    oldField,
                                    newField);
                            }
                        }
                    }

                    if (diff.Messages != null)
                    {
                        foreach (var message in diff.Messages)
                        {
                            diffHtml.AppendFormat("<li>{0}</li>", message);
                        }
                    }

                    diffHtml.Append("</ul>");
                }
            }
            else
            {
                diffHtml.Append("Изменений нет");
            }

            WriteHistory(id, diffHtml.ToString());
        }

        [ActionName("RequestEdit")]
        [HttpGet]
        public Request RequestEdit(long? id)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);

            var res = id.HasValue && id > 0
                ? UnitOfWork.GetById<Request>(id.Value)
                : new Request
                {
                    Child = new List<Child> {new Child {IsDeleted = false, Payed = true, IsLast = true}},
                    Attendant = new List<Applicant>(),
                    Applicant = new Applicant {IsApplicant = true, Id = -1},
                    Agent = new Agent(),
                    IsLast = true,
                    IsDeleted = false,
                    IsDraft = false,
                    Status = UnitOfWork.GetById<Status>((long) StatusEnum.Draft),
                    StatusId = (long) StatusEnum.Draft,
                    Version = 0,
                    UpdateDate = DateTime.Now,
                    SourceId = (long) SourceEnum.Operator,
                    Source = UnitOfWork.GetById<Source>((long) SourceEnum.Operator)
                };

            return res;
        }

        [ActionName("RequestList")]
        public RequestFilterModel RequestList(RequestFilterModel search, bool needPaging = true)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);

            var pager = new PagerState(search.PageNumber, search.PageSize);
            var query = RequestListQuery(search);

            query = query.Include(r => r.Applicant)
                .Include(r => r.TimeOfRest)
                .Include(r => r.TypeOfRest)
                .Include(r => r.Status)
                .Include(r => r.Tour)
                .Include(r => r.PlaceOfRest)
                .Include(r => r.Tour.Hotels)
                .Include(r => r.TypeOfRest.Parent);
            if (needPaging)
            {
                search.Requests = new CommonPagedList<Request>(
                    query.GetPage(pager),
                    pager.CurrentPage,
                    pager.PerPage,
                    query.Count());
            }
            else
            {
                search.Requests = new CommonPagedList<Request>(
                    query.ToList(),
                    1,
                    1,
                    query.Count());
            }

            search.TotalRecordsCount =
                query.Select(x => x.Child.Count(c => !c.IsDeleted) + x.Attendant.Count + (x.Applicant.IsAccomp ? 1 : 0))
                    .DefaultIfEmpty().Sum();

            return search;
        }

        internal IQueryable<Request> RequestListQuery(RequestFilterModel search)
        {
            var query =
                UnitOfWork.GetSet<Request>().Where(r =>
                    r.IsLast && !r.IsDeleted && r.StatusId != (long) StatusEnum.ErrorRequest &&
                    !r.TypeOfRest.Commercial);

            if (!string.IsNullOrWhiteSpace(search.ApplicantFio))
            {
                var item = search.ApplicantFio.ToLower().Trim().Replace(' ', '|').Replace("ё", "е");

                query = query.Where(r => r.Applicant.Key.Contains(item));
            }

            if (!string.IsNullOrWhiteSpace(search.ChildFio))
            {
                var item = search.ChildFio.ToLower().Trim().Replace(' ', '|').Replace("ё", "е");

                query = query.Where(r => r.Child.Any(c => c.Key.Contains(item)));
            }

            if (!string.IsNullOrWhiteSpace(search.RequestNumber))
            {
                var requestNumber = search.RequestNumber.ToLower();
                query =
                    query.Where(
                        q => q.RequestNumber.ToLower().Contains(requestNumber) ||
                             q.RequestNumberMpgu.ToLower().Contains(requestNumber));
            }

            if (search.PlaceOfRestId > 0)
            {
                query = query.Where(q => q.Tour.Hotels.PlaceOfRestId == search.PlaceOfRestId);
            }

            if (search.TypeOfRestId != 0)
            {
                var res = new List<long?> {search.TypeOfRestId};
                bool added;
                do
                {
                    added = false;
                    var typeOfRestsId =
                        UnitOfWork.GetSet<TypeOfRest>()
                            .Where(t => res.Contains(t.ParentId) && !res.Contains(t.Id))
                            .Select(a => (long?) a.Id)
                            .ToList();
                    if (typeOfRestsId.Any())
                    {
                        res.AddRange(typeOfRestsId);
                        added = true;
                    }
                } while (added);

                query = query.Where(q => res.Contains(q.TypeOfRestId));
            }

            if (search.TimeOfRestId > 0)
            {
                query = query.Where(q => q.TimeOfRestId == search.TimeOfRestId);
            }

            if (search.AggregatedStatusOfRequest == 0)
            {
                search.AggregatedStatusOfRequest = (long) AggregatedStatusEnum.All;
            }

            if (search.AggregatedStatusOfRequest == (int) AggregatedStatusEnum.InProgress)
            {
                query = query.Where(q => q.Status.IsFinal == false);
            }
            else if (search.AggregatedStatusOfRequest == (int) AggregatedStatusEnum.Complete)
            {
                query = query.Where(q => q.Status.IsFinal);
            }
            else if (search.AggregatedStatusOfRequest != (int) AggregatedStatusEnum.All)
            {
                query = query.Where(q => q.StatusId == search.AggregatedStatusOfRequest);
            }

            if (search.YearOfRestId != 0)
            {
                if (search.MoreThenSelectedYear)
                {
                    var year = UnitOfWork.GetById<YearOfRest>(search.YearOfRestId);
                    if (year != null)
                    {
                        query = query.Where(q => q.YearOfRest.Year >= year.Year);
                    }
                    else
                    {
                        query = query.Where(q => q.YearOfRestId == search.YearOfRestId);
                    }
                }
                else
                {
                    query = query.Where(q => q.YearOfRestId == search.YearOfRestId);
                }
            }

            if (search.DistrictId != 0)
            {
                query =
                    query.Where(
                        q =>
                            q.Child.Any(
                                c => !c.IsDeleted && (c.Address.BtiDistrictId == search.DistrictId ||
                                                      c.Address.BtiAddress.BtiDistrictId == search.DistrictId)));
            }

            if (search.RegionId != 0)
            {
                query =
                    query.Where(
                        q =>
                            q.Child.Any(c => !c.IsDeleted && (c.Address.BtiRegionId == search.RegionId ||
                                                              c.Address.BtiAddress.BtiRegionId == search.RegionId)));
            }

            if (search.StartRequestDate.HasValue)
            {
                query = query.Where(q => q.DateRequest >= DbFunctions.TruncateTime(search.StartRequestDate.Value));
            }

            if (search.EndRequestDate.HasValue)
            {
                query =
                    query.Where(q =>
                        q.DateRequest < DbFunctions.AddDays(DbFunctions.TruncateTime(search.EndRequestDate.Value), 1));
            }

            if (search.StartChangeStatusDate.HasValue)
            {
                query = query.Where(q =>
                    q.DateChangeStatus >= DbFunctions.TruncateTime(search.StartChangeStatusDate.Value));
            }

            if (search.EndChangeStatustDate.HasValue)
            {
                query =
                    query.Where(q => q.DateChangeStatus <
                                     DbFunctions.AddDays(DbFunctions.TruncateTime(search.EndChangeStatustDate.Value),
                                         1));
            }

            if (search.BenefitTypeId != 0)
            {
                var bIds =
                    UnitOfWork.GetSet<BenefitType>()
                        .Where(b => (b.Id == search.BenefitTypeId || b.SameBenefitId == search.BenefitTypeId) &&
                                    b.IsActive)
                        .Select(b => b.Id)
                        .ToList().Select(c => (long?) c).ToList();

                query = query.Where(q => q.Child.Any(c => !c.IsDeleted && bIds.Contains(c.BenefitTypeId)));
            }

            if (search.SourceId != 0)
            {
                query = query.Where(q => q.SourceId == search.SourceId);
            }

            if (search.CreateUserId != 0)
            {
                query = query.Where(q => q.CreateUserId == search.CreateUserId);
                search.CreateUser = UnitOfWork.GetById<Account>(search.CreateUserId);
            }

            if (search.BaseRegisterBenefitApprove.HasValue && !search.InteragencyBenefitApprove.HasValue)
            {
                query = search.BaseRegisterBenefitApprove == true
                    ? query.Where(q => q.Child.Any(c => !c.IsDeleted && c.BaseRegistryInfo.Any(b => !b.NotActual) &&
                                                        !c.BaseRegistryInfo.Any(b =>
                                                            !b.Success && b.IsProcessed && !b.NotActual)))
                    : query.Where(q => q.Child.Any(c => !c.IsDeleted &&
                                                        c.BaseRegistryInfo.Any(b =>
                                                            !b.Success && b.IsProcessed && !b.NotActual)));
            }
            else if (!search.BaseRegisterBenefitApprove.HasValue && search.InteragencyBenefitApprove.HasValue)
            {
                query = query.Where(q => q.Child.Any(c => !c.IsDeleted &&
                                                          c.IsApprovedInInteragency ==
                                                          search.InteragencyBenefitApprove));
            }
            else if (search.BaseRegisterBenefitApprove.HasValue && search.InteragencyBenefitApprove.HasValue)
            {
                query = search.BaseRegisterBenefitApprove == true
                    ? query.Where(q => q.Child.Any(c => !c.IsDeleted && c.BaseRegistryInfo.Any(b => !b.NotActual) &&
                                                        !(c.BaseRegistryInfo.Any(b =>
                                                              !b.Success && b.IsProcessed && !b.NotActual) ||
                                                          c.IsApprovedInInteragency ==
                                                          search.InteragencyBenefitApprove)))
                    : query.Where(q => q.Child.Any(c => !c.IsDeleted &&
                                                        (c.BaseRegistryInfo.Any(b =>
                                                             !b.Success && b.IsProcessed && !b.NotActual) ||
                                                         c.IsApprovedInInteragency ==
                                                         search.InteragencyBenefitApprove)));
            }

            if (search.HotelsId.HasValue)
            {
                query = query.Where(q => q.Tour.HotelsId == search.HotelsId);
            }

            if (search.SourceSetted)
            {
                query = query.Where(r => r.SourceId.HasValue);
            }

            if (!string.IsNullOrEmpty(search.CertificateNumber))
            {
                query = query.Where(r => r.CertificateNumber.ToLower().Contains(search.CertificateNumber.ToLower()));
            }

            if (search.TypeOfDecision == 1)
            {
                query = query.Where(r => !r.RequestOnMoney);
            }
            else if (search.TypeOfDecision == 2)
            {
                query = query.Where(r => r.RequestOnMoney);
            }

            var now = DateTime.Now.Date;
            if (search.AgeStart.HasValue)
            {
                var startDate = now.AddYears(-search.AgeStart.Value);
                query = query.Where(r => r.Child.Any(i => i.DateOfBirth.Value <= startDate));
            }

            if (search.AgeEnd.HasValue)
            {
                var endDate = now.AddYears(-(search.AgeEnd.Value + 1));
                query = query.Where(r => r.Child.Any(i => i.DateOfBirth.Value > endDate));
            }

            if (search.TypeOfRestrictionId != 0)
            {
                query = query.Where(i => i.Child.Any(j => j.TypeOfRestrictionId == search.TypeOfRestrictionId));
            }

            if (search.BaseRegistryPreferentialCategoryCheck.HasValue)
            {
                query = query.Where(r => r.Child.Any() && r.Child.All(c => c.BaseRegistryInfo.Any(bri =>
                    bri.ExchangeBaseRegistryTypeId ==
                    (long) ExchangeBaseRegistryTypeEnum.Benefit
                    && bri.IsProcessed
                    && !bri.NotActual
                    && bri.Success == search.BaseRegistryPreferentialCategoryCheck.Value)));
            }

            if (search.BaseRegistryPassportCheck.HasValue)
            {
                if (search.BaseRegistryPassportCheck.Value)
                {
                    query = query.Where(r => r.Child.Any() && r.Child.All(c => c.BaseRegistryInfo.Any(bri =>
                                                               bri.ExchangeBaseRegistryTypeId ==
                                                               (long) ExchangeBaseRegistryTypeEnum.PassportDataBySNILS
                                                               && bri.IsProcessed
                                                               && !bri.NotActual
                                                               && bri.Success))
                                                           && r.Applicant.BaseRegistryInfo.Any(bri =>
                                                               bri.ExchangeBaseRegistryTypeId ==
                                                               (long) ExchangeBaseRegistryTypeEnum.PassportDataBySNILS
                                                               && bri.IsProcessed
                                                               && !bri.NotActual
                                                               && bri.Success));
                }
                else
                {
                    query = query.Where(r => r.Child.Any() && r.Child.All(c => c.BaseRegistryInfo.Any(bri =>
                                                 bri.ExchangeBaseRegistryTypeId ==
                                                 (long) ExchangeBaseRegistryTypeEnum.PassportDataBySNILS
                                                 && bri.IsProcessed
                                                 && !bri.NotActual
                                                 && !bri.Success))
                                             || r.Applicant.BaseRegistryInfo.Any(bri =>
                                                 bri.ExchangeBaseRegistryTypeId ==
                                                 (long) ExchangeBaseRegistryTypeEnum.PassportDataBySNILS
                                                 && bri.IsProcessed
                                                 && !bri.NotActual
                                                 && !bri.Success));
                }
            }

            if (search.BaseRegistryRelationshipCheck.HasValue)
            {
                query = query.Where(r => r.Child.Any() && r.Child.All(c => c.BaseRegistryInfo.Any(bri =>
                    bri.ExchangeBaseRegistryTypeId ==
                    (long) ExchangeBaseRegistryTypeEnum.Relationship
                    && bri.IsProcessed
                    && !bri.NotActual
                    && bri.Success == search.BaseRegistryRelationshipCheck.Value)));
            }

            if (search.BaseRegistrySNILSCheck.HasValue)
            {
                if (search.BaseRegistrySNILSCheck.Value)
                {
                    query = query.Where(r => r.Child.Any() && r.Child.All(c => c.BaseRegistryInfo.Any(bri =>
                                                               bri.ExchangeBaseRegistryTypeId ==
                                                               (long) ExchangeBaseRegistryTypeEnum.Snils2040
                                                               && bri.IsProcessed
                                                               && !bri.NotActual
                                                               && bri.Success))
                                                           && r.Applicant.BaseRegistryInfo.Any(bri =>
                                                               bri.ExchangeBaseRegistryTypeId ==
                                                               (long) ExchangeBaseRegistryTypeEnum.Snils2040
                                                               && bri.IsProcessed
                                                               && !bri.NotActual
                                                               && bri.Success));
                }
                else
                {
                    query = query.Where(r => r.Child.Any() && r.Child.All(c => c.BaseRegistryInfo.Any(bri =>
                                                 bri.ExchangeBaseRegistryTypeId ==
                                                 (long) ExchangeBaseRegistryTypeEnum.Snils2040
                                                 && bri.IsProcessed
                                                 && !bri.NotActual
                                                 && !bri.Success))
                                             || r.Applicant.BaseRegistryInfo.Any(bri =>
                                                 bri.ExchangeBaseRegistryTypeId ==
                                                 (long) ExchangeBaseRegistryTypeEnum.Snils2040
                                                 && bri.IsProcessed
                                                 && !bri.NotActual
                                                 && !bri.Success));
                }
            }

            if (!string.IsNullOrWhiteSpace(search.SNILS))
            {
                query = query.Where(r =>
                    r.Applicant.Snils.Contains(search.SNILS)
                    || r.Child.Any(c => c.Snils.Contains(search.SNILS))
                    || r.Attendant.Any(c => c.Snils.Contains(search.SNILS))
                    || r.Agent.Snils.Contains(search.SNILS)
                );
            }

            if (search.TransferFromId.HasValue && search.TransferFromId.Value > 0)
            {
                query = query.Where(r => r.TransferFromId == search.TransferFromId.Value);
            }

            if (search.TransferToId.HasValue && search.TransferToId.Value > 0)
            {
                query = query.Where(r => r.TransferToId == search.TransferToId.Value);
            }

            if (search.CertificateRepaid)
            {
                query = query.Where(ss =>
                    ss.Certificates.Any(c => c.StateMachineStateId != StateMachineStateEnum.Deleted));
            }

            if (search.DeclineReasonId.HasValue && search.DeclineReasonId.Value > 0)
            {
                query = query.Where(ss => ss.DeclineReasonId == search.DeclineReasonId);
            }

            if (search.WithKC.HasValue)
            {
                query = query.Where(ss =>
                    (ss.InternalCommentary != null && ss.InternalCommentary != "") == search.WithKC.Value);
            }

            return query;
        }

        /// <summary>
        ///     получить список допустимых операций для статусов
        /// </summary>
        [HttpPost]
        public IList<StatusAction> GetActions(long statusId, IEnumerable<string> checks)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);

            var list =
                UnitOfWork.GetSet<StatusAction>()
                    .Where(sa => sa.FromStatus.Any(s => s.Id == statusId))
                    .ToList()
                    .Where(sa => checks.Contains(sa.Code))
                    .ToList();
            return list;
        }

        /// <summary>
        ///     получить последнюю версию заявления
        /// </summary>
        [HttpPost]
        public Request GetLastVersionRequest(long entityId)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);
            return UnitOfWork.GetSet<Request>().FirstOrDefault(r => r.EntityId == entityId && !r.IsDeleted && r.IsLast);
        }

        /// <summary>
        ///     Удалить черновик.
        /// </summary>
        internal long? RemoveDraftVersion(long requestId)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);

            if (!Security.HasRight(AccessRightEnum.RemoveDraft))
            {
                return requestId;
            }

            var current = UnitOfWork.GetById<Request>(requestId);

            if (current == null || !current.IsLast || current.StatusId != (long) StatusEnum.Draft || current.IsDeleted)
            {
                return null;
            }

            current.IsDeleted = true;

            foreach (var child in current.Child)
            {
                child.IsDeleted = true;
                child.Key = null;
                child.KeySame = null;
                child.YearOfCompany = null;
            }

            foreach (var attendant in current.Attendant)
            {
                attendant.IsLast = false;
                attendant.Key = string.Empty;
            }

            if (current.Applicant != null)
            {
                current.Applicant.IsLast = false;
                current.Applicant.Key = string.Empty;
            }

            UnitOfWork.WriteHistory(requestId, "Удаление заявления", Security.GetCurrentAccountId());

            UnitOfWork.SaveChanges();

            if (current.BookingGuid.HasValue)
            {
                var booking = UnitOfWork.GetSet<Domain.Booking>().FirstOrDefault(b => b.Code == current.BookingGuid);
                if (booking != null)
                {
                    var request = new BookingRequest
                    {
                        TypeOfRestId = current.TypeOfRestId ?? booking.TypeOfRestId ?? 0,
                        BookingGuid = booking.Code,
                        Places = booking.CountPlace ?? 0,
                        Attendants = booking.CountAttendants ?? 0
                    };

                    var client = Booking.Logic.Booking.GetServiceClient(request);
                    try
                    {
                        var res = client.ReleaseBooking(request);
                        if (res.IsError)
                        {
                            Logger.ErrorFormat(
                                "Не произошло снятие бронирования. BookingGuid={0}, requestId={1}, Error={2}",
                                booking.Code,
                                requestId, res.ErrorMessage);
                        }
                    }
                    finally
                    {
                        Booking.Logic.Booking.CloseClient(client);
                    }
                }
            }

            return null;
        }

        /// <summary>
        ///     Загрузка истории заявления
        /// </summary>
        /// <returns></returns>
        [ActionName("LoadRequestHistory")]
        [HttpGet]
        public IEnumerable<RequestVersionDto> LoadRequestHistory(long id)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);

            var currentRequest = UnitOfWork.GetById<Request>(id);
            var versions =
                UnitOfWork.GetSet<Request>()
                    .Where(x => x.EntityId == currentRequest.EntityId && !x.IsDeleted)
                    .OrderBy(x => x.UpdateDate);
            var history =
                versions.GroupJoin(UnitOfWork.GetSet<HistoryRequest>(), version => version.Id, hist => hist.RequestId,
                    (version, hist) => new {Version = version, History = hist.OrderBy(x => x.OperationDate)}).ToList();
            return
                history.Select(
                    x =>
                        new RequestVersionDto
                        {
                            RequestId = x.Version.Id,
                            History = x.History.Select(y => new RequestHistoryDto(y))
                        });
        }

        /// <summary>
        ///     В статус принятие решения (дополнительная кампания 2020, администратор).
        /// </summary>
        [HttpPost, HttpGet]
        public bool Lok2020DecisionMake(long currentPeriodId)
        {
            if (!Security.HasRight(AccessRightEnum.Status.FcToDecisionMakingCovid))
            {
                return false;
            }

            var reqs = UnitOfWork.GetSet<Request>().Where(ss =>
                ss.YearOfRestId == currentPeriodId && !ss.IsDraft &&
                ss.StatusId == (long) StatusEnum.DecisionMakingCovid).ToList();

            foreach (var req in reqs)
            {
                UnitOfWork.RequestChangeStatusInternal(AccessRightEnum.Status.ToReject, req,
                    (long) DeclineReasonEnum.RefuseAllVariantsCovid2020, true, Security.GetCurrentAccountId());
            }

            return true;
        }

        /// <summary>
        ///     Загрузка истории межведомственных запросов
        /// </summary>
        /// <returns></returns>
        [ActionName("LoadInteragencyRequestHistory")]
        [HttpGet]
        public IEnumerable<InteragencyRequestHistoryDto> LoadInteragencyRequestHistory(long id)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);

            var history =
                UnitOfWork.GetSet<HistoryInteragencyRequest>()
                    .Where(x => x.InteragencyRequestId == id)
                    .OrderBy(x => x.OperationDate)
                    .ToList();
            return
                history.Select(
                    x =>
                        new InteragencyRequestHistoryDto(x));
        }

        private void VoucherUpdate(Request request, ICollection<RequestInformationVoucher> vouchers,
            Dictionary<long?, List<long>> childVouchers, Dictionary<long, long> childKey)
        {
            var vourcherForDelete =
                request.InformationVouchers.Where(iv => !vouchers.Select(v => v.Id).Contains(iv.Id)).ToList();

            var vourcherForUpdate =
                vouchers.Where(iv => request.InformationVouchers.Select(v => v.Id).Contains(iv.Id)).ToList();


            foreach (var voucher in vourcherForDelete)
            {
                foreach (var ap in voucher.AttendantsPrice.ToList())
                {
                    UnitOfWork.Delete(ap);
                }

                var updates = UnitOfWork.GetSet<Child>().Where(c => c.RequestInformationVoucherId == voucher.Id)
                    .ToList();
                foreach (var child in updates)
                {
                    child.RequestInformationVoucherId = null;
                    child.RequestInformationVoucher = null;
                    UnitOfWork.Update(child);
                }

                UnitOfWork.Delete(voucher);
            }

            foreach (var voucher in vouchers.Where(v => v.Id <= 0).ToList())
            {
                var childKeys = childVouchers.ContainsKey(voucher.Id) ? childVouchers[voucher.Id] : null;

                voucher.RequestId = request.Id;
                var v = UnitOfWork.AddEntity(voucher);

                if (!childKeys.IsNullOrEmpty() && childKey != null)
                {
                    var ids = childKeys?.Where(childKey.ContainsKey).Select(c => childKey[c]).ToList();
                    var childs = UnitOfWork.GetSet<Child>().Where(c => ids.Contains(c.Id)).ToList();
                    foreach (var child in childs)
                    {
                        child.RequestInformationVoucherId = v.Id;
                        UnitOfWork.Update(child);
                    }
                }
            }

            foreach (var voucher in vourcherForUpdate)
            {
                var entity = UnitOfWork.GetById<RequestInformationVoucher>(voucher.Id);
                entity.TypeId = voucher.TypeId;
                entity.CostOfRide = voucher.CostOfRide;
                entity.DateFrom = voucher.DateFrom;
                entity.DateTo = voucher.DateTo;
                entity.CountPeople = voucher.CountPeople;
                entity.OrganizationName = voucher.OrganizationName;
                entity.Price = voucher.Price;

                entity = UnitOfWork.Update(entity);
                var apForUpdate =
                    voucher.AttendantsPrice?.Where(a => entity.AttendantsPrice.Select(p => p.Id).Contains(a.Id))
                        .ToList() ??
                    new List<RequestInformationVoucherAttendant>();
                var apForDelete =
                    entity.AttendantsPrice.Where(a => !voucher.AttendantsPrice.Select(p => p.Id).Contains(a.Id))
                        .ToList();
                var apForInsert = voucher.AttendantsPrice?.Where(a => a.Id <= 0).ToList() ??
                                  new List<RequestInformationVoucherAttendant>();

                foreach (var attendantsPrice in apForDelete)
                {
                    UnitOfWork.Delete(attendantsPrice);
                }

                foreach (var attendantsPrice in apForUpdate)
                {
                    var entityattendantsPrice =
                        UnitOfWork.GetById<RequestInformationVoucherAttendant>(attendantsPrice.Id);
                    entityattendantsPrice.ApplicantId = attendantsPrice.ApplicantId;
                    entityattendantsPrice.CostOfRide = attendantsPrice.CostOfRide;
                    entityattendantsPrice.AmountOfCompensation = attendantsPrice.AmountOfCompensation;
                    entityattendantsPrice.Price = attendantsPrice.Price;
                    UnitOfWork.Update(entityattendantsPrice);
                }

                foreach (var attendantsPrice in apForInsert)
                {
                    attendantsPrice.RequestInformationVoucherId = entity.Id;
                    UnitOfWork.AddEntity(attendantsPrice);
                }
            }
        }

        private void SetupOfferInRequest(Request request)
        {
            if (request.OfferInRequest != null)
            {
                foreach (var offer in request.OfferInRequest)
                {
                    if (offer.Id == 0)
                    {
                        offer.Id = 0;
                        var tempOffer = UnitOfWork.AddEntity(offer);
                        offer.Id = tempOffer.Id;
                    }
                }

                if (request.Attendant != null)
                {
                    foreach (var attendant in request.Attendant)
                    {
                        attendant.OfferInRequest = null;
                        attendant.OfferInRequestId = attendant.OfferInRequestId > 0
                            ? attendant.OfferInRequestId
                            : request.OfferInRequest.Any(oi => oi.Index == attendant.OfferInRequestId)
                                ? request.OfferInRequest.First(oi => oi.Index == attendant.OfferInRequestId).Id
                                : (long?) null;
                    }
                }

                if (request.Applicant?.OfferInRequestId != null)
                {
                    request.Applicant.OfferInRequest = null;
                    request.Applicant.OfferInRequestId = request.Applicant.OfferInRequestId > 0
                        ? request.Applicant.OfferInRequestId
                        : request.OfferInRequest.Any(oi => oi.Index == request.Applicant.OfferInRequestId)
                            ? request.OfferInRequest.First(oi => oi.Index == request.Applicant.OfferInRequestId).Id
                            : (long?) null;
                }
            }
        }

        /// <summary>
        ///     проверка на заказ одного и того же места.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool CheckOnSameOfferInRequest(RequestViewModel model)
        {
            if (!model.Data.ParentRequestId.HasValue)
            {
                return true;
            }

            var res =
                !UnitOfWork.GetSet<Request>()
                    .Where(
                        r =>
                            r.Id != model.Data.Id && r.ParentRequestId == model.Data.ParentRequestId &&
                            r.TypeOfRestId == (long) TypeOfRestEnum.CommercicalAddonRequest && !r.IsDeleted &&
                            r.StatusId.HasValue && r.StatusId != (long) StatusEnum.Reject &&
                            r.StatusId != (long) StatusEnum.Draft)
                    .SelectMany(r => r.OfferInRequest
                        .Where(o => o.RoomRates != null && o.RoomRates.TypeOfRoomsId.HasValue)
                        .Select(o => o.RoomRates.TypeOfRoomsId)).Any();

            if (!res)
            {
                model.AttendantEm = "Уже есть другие заявления на эти же дополнительные места";
            }

            return res;
        }

        /// <summary>
        ///     сохранение файлов в том числе и в ЦХЭД
        /// </summary>
        /// <param name="request"></param>
        /// <param name="model"></param>
        private void SaveFiles(Request request, Request model)
        {
            var filesToAdd = request.Files.Where(f => f.Id == 0).ToList();
            using (var csClient = new CustomWebServiceImplClient())
            {
                if (csClient.ClientCredentials != null)
                {
                    csClient.ClientCredentials.UserName.UserName = ConfigurationManager.AppSettings["CshedLogin"];
                    csClient.ClientCredentials.UserName.Password = ConfigurationManager.AppSettings["CshedPass"];
                }

                var fileTypes = UnitOfWork.GetSet<RequestFileType>().ToDictionary(f => f.Id, f => f);
                foreach (var file in filesToAdd)
                {
                    if (fileTypes.ContainsKey(file.RequestFileTypeId ?? 0) && !file.RemoteSave)
                    {
                        var fileType = fileTypes[file.RequestFileTypeId ?? 0];
                        if (!string.IsNullOrWhiteSpace(fileType.CodeAsGuf) &&
                            !string.IsNullOrWhiteSpace(fileType.CodeChed))
                        {
                            var fileName = Path.Combine(Settings.Default.StorageRootPath, file.FileName);
                            if (!File.Exists(fileName))
                            {
                                continue;
                            }

                            var bytes = File.ReadAllBytes(fileName);
                            try
                            {
                                var mimeType =
                                    MimeTypeMap.GetMimeType(Path.GetExtension(file.FileTitle ?? file.FileName));
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
                                                    new Property
                                                    {
                                                        Name = "DocumentTitle", Value = file.FileTitle ?? file.FileName
                                                    }
                                                }
                                        });

                                file.FileName = docId;
                                file.RemoteSave = true;
                                File.Delete(fileName);
                            }
                            catch (Exception ex)
                            {
                                // ignored
                                Logger.Error("Ошибка загрузки файла в РЦХЭД", ex);
                            }
                        }
                    }

                    file.RequestId = request.Id;
                    file.CreateUserId = Security.GetCurrentAccountId();
                    file.DataCreate = DateTime.Now;
                    UnitOfWork.AddEntity(file);
                }
            }

            // для удаления.
            var fids = request.Files.Where(f => f.Id > 0).Select(f => f.Id).ToList();
            var fileToRemove = model.Files.Where(f =>
                !fids.Contains(f.Id) && f.RequestFileTypeId != (long) RequestFileTypeEnum.CertificateOnPayment
                                     && f.RequestFileTypeId != (long) RequestFileTypeEnum.NotificationRefuse
                                     && f.RequestFileTypeId != (long) RequestFileTypeEnum.CertificateOnRest
                                     && f.RequestFileTypeId != (long) RequestFileTypeEnum.Notifications).ToList();
            foreach (var file in fileToRemove)
            {
                var filePath = Path.Combine(Settings.Default.StorageRootPath, file.FileName);
                if (File.Exists(filePath))
                {
                    try
                    {
                        //System.IO.File.Delete(filePath);
                    }
                    catch (Exception ex)
                    {
                        Logger.Error($"Ошибка удаления файла '{file.FileName}' '{file.FileTitle}'", ex);
                    }
                }

                model.Files.Remove(file);

                UnitOfWork.Delete(file);
            }
        }

        public void UpdateKey(Request request)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);

            if (request.Applicant != null)
            {
                request.Applicant.Key = StaticHelpers.GenerateKey(
                    request.Applicant.FirstName,
                    request.Applicant.LastName,
                    request.Applicant.MiddleName,
                    request.Applicant.DocumentSeria,
                    request.Applicant.DocumentNumber);
                if (request.Tour != null)
                {
                    request.Applicant.IntervalStart = request.Tour.DateIncome?.Date.Ticks;
                    request.Applicant.IntervalEnd = request.Tour.DateOutcome?.AddDays(1).Ticks;
                }
                else
                {
                    request.Applicant.IntervalStart = null;
                    request.Applicant.IntervalEnd = null;
                }
            }

            int year;
            if (request.YearOfRest != null)
            {
                year = request.YearOfRest.Year;
            }
            else
            {
                year = UnitOfWork.GetById<YearOfRest>(request.YearOfRestId)?.Year ?? 0;
            }

            var tour = request.Tour ?? UnitOfWork.GetById<Tour>(request.TourId);

            if (request.TourId.HasValue && tour == null)
            {
                tour = UnitOfWork.GetById<Tour>(request.TourId);
            }

            var typeOfRest = request.TypeOfRest;
            if (typeOfRest == null && request.TypeOfRestId.HasValue)
            {
                typeOfRest = UnitOfWork.GetById<TypeOfRest>(request.TypeOfRestId);
            }

            foreach (var child in request.Child)
            {
                if (child.IsDeleted)
                {
                    continue;
                }

                child.Key = StaticHelpers.GenerateKey(
                    child.FirstName,
                    child.LastName,
                    child.MiddleName,
                    child.DocumentSeria,
                    child.DocumentNumber);

                child.KeySame = StaticHelpers.GenerateKeySame(child.FirstName, child.LastName, child.DateOfBirth);
                child.YearOfCompany = year;

                child.TypeOfGroupCheckId = child?.BenefitType?.TypeOfGroupCheckId ??
                                           typeOfRest?.TypeOfGroupCheckId ?? tour?.TypeOfRest?.TypeOfGroupCheckId;
                if (tour != null)
                {
                    child.IntervalStart = tour.DateIncome?.Date.Ticks;
                    child.IntervalEnd = tour.DateOutcome?.AddDays(1).Ticks;
                }
                else
                {
                    child.IntervalStart = null;
                    child.IntervalEnd = null;
                }
            }

            foreach (var attendant in request.Attendant)
            {
                attendant.Key = StaticHelpers.GenerateKey(
                    attendant.FirstName,
                    attendant.LastName,
                    attendant.MiddleName,
                    attendant.DocumentSeria,
                    attendant.DocumentNumber);

                if (tour != null && (typeOfRest?.NeedAttendant ?? false))
                {
                    attendant.IntervalStart = tour?.DateIncome?.Date.Ticks;
                    attendant.IntervalEnd = tour?.DateOutcome?.Date.AddDays(1).Ticks;
                }
                else
                {
                    attendant.IntervalStart = null;
                    attendant.IntervalEnd = null;

                    if (!request.IsFirstCompany)
                    {
                        attendant.IsAccomp = false;
                    }

                    attendant.BoutId = null;
                }
            }
        }


        internal void SetInteragencyRequestDocumentStyles(WordprocessingDocument wordprocessingDocument)
        {
            var styleDefinitionsPart = wordprocessingDocument.MainDocumentPart.StyleDefinitionsPart;
            var styles = new Styles();
            if (styleDefinitionsPart == null)
            {
                styleDefinitionsPart = wordprocessingDocument.MainDocumentPart.AddNewPart<StyleDefinitionsPart>();
                styles.Save(styleDefinitionsPart);
            }
        }

        /// <summary>
        ///     Получить печатную форму для межведомственного запроса
        /// </summary>
        internal void GetInteragencyRequestDocument(long childId, Document doc)
        {
            var mainRunProperties = new RunProperties();
            mainRunProperties.AppendChild(new RunFonts
            {
                Ascii = "Times New Roman",
                HighAnsi = "Times New Roman",
                ComplexScript = "Times New Roman"
            });
            mainRunProperties.AppendChild(new FontSize {Val = "24"});

            var mainTitleRunProperties = mainRunProperties.CloneNode(true);
            mainTitleRunProperties.AppendChild(new Bold());

            var mainSmallRunProperties = mainRunProperties.CloneNode(true);
            mainSmallRunProperties.RemoveAllChildren<FontSize>();
            mainSmallRunProperties.AppendChild(new FontSize {Val = "20"});

            var mainUnderlineRunProperties = mainRunProperties.CloneNode(true);

            var bottomBorders = new ParagraphBorders
            {
                BottomBorder =
                    new BottomBorder
                    {
                        Val = new EnumValue<BorderValues>(BorderValues.Single),
                        Size = 6,
                        Space = 1
                    }
            };

            doc.AppendChild(new Paragraph(
                new ParagraphProperties(new Justification {Val = JustificationValues.Center},
                    new SpacingBetweenLines {After = "20"}),
                new Run(mainTitleRunProperties.CloneNode(true),
                    new Text("МЕЖВЕДОМСТВЕННЫЙ ЗАПРОС"))));
            doc.AppendChild(new Paragraph(
                new ParagraphProperties(new Justification {Val = JustificationValues.Center},
                    new SpacingBetweenLines {After = "20"}),
                new Run(mainTitleRunProperties.CloneNode(true),
                    new Text("О ПРЕДСТАВЛЕНИИ ИНФОРМАЦИИ, "))));
            doc.AppendChild(new Paragraph(
                new ParagraphProperties(new Justification {Val = JustificationValues.Center},
                    new SpacingBetweenLines {After = "20"}),
                new Run(mainTitleRunProperties.CloneNode(true),
                    new Text("НЕОБХОДИМОЙ ДЛЯ ПРЕДОСТАВЛЕНИЯ ГОСУДАРСТВЕННОЙ УСЛУГИ"))));

            doc.AppendChild(new Paragraph(new ParagraphProperties(new SpacingBetweenLines {After = "20"})));
            doc.AppendChild(new Paragraph(new ParagraphProperties(new SpacingBetweenLines {After = "20"})));

            doc.AppendChild(new Paragraph(
                new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                    bottomBorders.CloneNode(true)),
                new Run(mainRunProperties.CloneNode(true),
                    new Text(
                        "В целях предоставления государственной услуги по организации отдыха и оздоровления детей в соответствии с постановлением Правительства Москвы от 15 февраля 2011 г. № 29-ПП \"Об организации отдыха и оздоровления детей города Москвы в 2011 году и последующие годы\" просим представить в отношении"))));

            doc.AppendChild(new Paragraph(
                new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                    bottomBorders.CloneNode(true)),
                new Run(mainUnderlineRunProperties.CloneNode(true), new Text(""))));

            doc.AppendChild(new Paragraph(
                new ParagraphProperties(new Justification {Val = JustificationValues.Center},
                    new SpacingBetweenLines {After = "20"}),
                new Run(mainSmallRunProperties.CloneNode(true),
                    new Text("(Ф.И.О. лица, подающего заявление, его местонахождение"))));

            doc.AppendChild(new Paragraph(
                new ParagraphProperties(new Justification {Val = JustificationValues.Center},
                    new SpacingBetweenLines {After = "20"}),
                new Run(mainSmallRunProperties.CloneNode(true),
                    new Text(
                        "(для юридического лица)/место жительства (для физического лица) либо иные сведения, необходимые "))));

            doc.AppendChild(new Paragraph(new ParagraphProperties(new Justification {Val = JustificationValues.Center}),
                new Run(mainSmallRunProperties.CloneNode(true),
                    new Text("для представления информации)"))));

            doc.AppendChild(new Paragraph(
                new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                    bottomBorders.CloneNode(true)),
                new Run(mainRunProperties.CloneNode(true),
                    new Text(
                        "следующую информацию об отнесении к льготной категории \"дети из малообеспеченной семьи\" ребенка заявителя"))));

            doc.AppendChild(new Paragraph(
                new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                    bottomBorders.CloneNode(true)),
                new Run(mainUnderlineRunProperties.CloneNode(true), new Text(""))));

            doc.AppendChild(new Paragraph(
                new ParagraphProperties(new Justification {Val = JustificationValues.Center},
                    new SpacingBetweenLines {After = "20"}),
                new Run(mainSmallRunProperties.CloneNode(true),
                    new Text("(Ф.И.О. лица, подающего заявление, его местонахождение"))));

            doc.AppendChild(new Paragraph(
                new ParagraphProperties(new Justification {Val = JustificationValues.Center},
                    new SpacingBetweenLines {After = "20"}),
                new Run(mainSmallRunProperties.CloneNode(true),
                    new Text(
                        "(для юридического лица)/место жительства (для физического лица) либо иные сведения, необходимые "))));

            doc.AppendChild(new Paragraph(new ParagraphProperties(new Justification {Val = JustificationValues.Center}),
                new Run(mainSmallRunProperties.CloneNode(true),
                    new Text("для представления информации)"))));

            doc.AppendChild(new Paragraph(
                new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                    bottomBorders.CloneNode(true)),
                new Run(mainRunProperties.CloneNode(true),
                    new Text("Ответ на межведомственный запрос просим направить по"))));

            doc.AppendChild(new Paragraph(new ParagraphProperties(new Justification {Val = JustificationValues.Center}),
                new Run(mainSmallRunProperties.CloneNode(true),
                    new Text(
                        "(указать адрес электронной почты Исполнителя, номер факса, по которым надлежит направить ответ)"))));

            doc.AppendChild(new Paragraph(
                new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                    bottomBorders.CloneNode(true)),
                new Run(mainRunProperties.CloneNode(true),
                    new Text("в срок до ______________________________."))));

            doc.AppendChild(new Paragraph(new ParagraphProperties(new Justification {Val = JustificationValues.Center}),
                new Run(mainSmallRunProperties.CloneNode(true),
                    new Text("(указать срок ожидаемого ответа)"))));
        }

        private void CopyRequest(Request source, Request target)
        {
            target.EntityId = source.EntityId;
            target.AdditionalPlaces = source.AdditionalPlaces;
            target.AgentApplicant = source.AgentApplicant;
            target.AttendantTypeId = source.AttendantTypeId;
            target.DateRequest = source.DateRequest;
            target.MainPlaces = source.MainPlaces;
            target.OrganizationId = source.OrganizationId;
            target.PlaceOfRestId = source.PlaceOfRestId;
            target.YearOfRestId = source.YearOfRestId;
            target.RequestNumber = source.RequestNumber;
            target.StatusId = source.StatusId;
            target.SubjectOfRestId = source.SubjectOfRestId;
            target.TimeOfRestId = source.TimeOfRestId;
            target.TypeOfRestId = source.TypeOfRestId;
            target.UpdateDate = DateTime.Now;
            target.DateChangeStatus = source.DateChangeStatus;
            target.Version = source.Version;
            target.SourceId = source.SourceId;
            target.ExternalUid = source.ExternalUid;
            target.ExternalSystem = source.ExternalSystem;
            target.RequestNumberMpgu = source.RequestNumberMpgu;
            target.NeedEmail = source.NeedEmail;
            target.NeedSms = source.NeedSms;
            target.BookingGuid = source.BookingGuid;
            target.TourId = source.TourId;
            target.OrganizationId = source.OrganizationId;
            target.HotelsId = source.HotelsId;
            target.CountAttendants = source.CountAttendants;
            target.CountPlace = source.CountPlace;
            target.BankAccount = source.BankAccount;
            target.BankBik = source.BankBik;
            target.BankInn = source.BankInn;
            target.BankName = source.BankName;
            target.BankKpp = source.BankKpp;
            target.BankCardNumber = source.BankCardNumber;
            target.BankCorr = source.BankCorr;
            target.BankFirstName = source.BankFirstName;
            target.BankMiddleName = source.BankMiddleName;
            target.BankLastName = source.BankLastName;
            target.CuratorId = source.CuratorId;
            target.Commentary = source.Commentary;
            target.DateIncome = source.DateIncome;
            target.Price = source.Price;
            target.DateOutcome = source.DateOutcome;
            target.ParentRequestId = source.ParentRequestId;
            target.ChangeByScan = source.ChangeByScan;
            target.LastUpdateTick = DateTime.Now.Ticks;
            target.BeneficiariesId = source.BeneficiariesId;
            target.IsFirstCompany = source.IsFirstCompany;
            target.StatusApplicant = source.StatusApplicant;
            target.RepresentInterestId = source.RepresentInterestId;
            target.TransferFromId = source.TransferFromId;
            target.TransferToId = source.TransferToId;
            target.InternalCommentary = source.InternalCommentary;
            target.PriorityTypeOfTransportInRequestId = source.PriorityTypeOfTransportInRequestId == -1 ? null : source.PriorityTypeOfTransportInRequestId;
            target.AdditionalTypeOfTransportInRequestId = source.AdditionalTypeOfTransportInRequestId == -1 ? null : source.AdditionalTypeOfTransportInRequestId;
            target.TypeOfCampId = source.TypeOfCampId == -1 ? null : source.TypeOfCampId;
            target.TypeOfCampAddonId = source.TypeOfCampAddonId == -1 ? null : source.TypeOfCampAddonId;

            if (target.IsFirstCompany)
            {
                target.CountPlace = source.CountPlace;
                target.CountAttendants = source.CountAttendants;
                target.MainPlaces = source.MainPlaces;
            }

            if ((target.Agent != null) ^ (source.Agent != null))
            {
                target.Agent = source.Agent;
            }
            else if (target.Agent != null && source.Agent != null)
            {
                var s = source.Agent;
                var t = target.Agent;
                t.DocumentDateOfIssue = s.DocumentDateOfIssue;
                t.DocumentNumber = s.DocumentNumber;
                t.DocumentSeria = s.DocumentSeria;
                t.DocumentSubjectIssue = s.DocumentSubjectIssue;
                t.DocumentTypeId = s.DocumentTypeId;
                t.Email = s.Email;
                t.FirstName = s.FirstName;
                t.LastName = s.LastName;
                t.MiddleName = s.MiddleName;
                t.HaveMiddleName = s.HaveMiddleName;
                t.NotaryName = s.NotaryName;
                t.Phone = s.Phone;
                t.ProxyDateOfIssure = s.ProxyDateOfIssure;
                t.ProxyEndDate = s.ProxyEndDate;
                t.ProxyNumber = s.ProxyNumber;
                t.Snils = s.Snils;
                t.IsLast = s.IsLast;
                t.DocumentCode = s.DocumentCode;
                t.Male = s.Male;
                t.PlaceOfBirth = s.PlaceOfBirth;
                t.DateOfBirth = s.DateOfBirth;
                //t.StatusByChildId = s.StatusByChildId;
            }

            if ((target.Applicant != null) ^ (source.Applicant != null))
            {
                target.Applicant = source.Applicant;
            }
            else if (target.Applicant != null && source.Applicant != null)
            {
                var s = source.Applicant;
                var t = target.Applicant;

                if (!t.IsDeleted && s.IsDeleted)
                {
                    ExcludeAttendant(t);
                }

                t.DocumentDateOfIssue = s.DocumentDateOfIssue;
                t.DocumentNumber = s.DocumentNumber;
                t.DocumentSeria = s.DocumentSeria;
                t.DocumentSubjectIssue = s.DocumentSubjectIssue;
                t.DocumentTypeId = s.DocumentTypeId;
                t.Email = s.Email;
                t.FirstName = s.FirstName;
                t.LastName = s.LastName;
                t.MiddleName = s.MiddleName;
                t.HaveMiddleName = s.HaveMiddleName;
                t.Phone = s.Phone;
                t.ApplicantTypeId = s.ApplicantTypeId;
                if (t.Snils != s.Snils)
                {
                    t.RelativeUniqeId = null;
                }

                t.Snils = s.Snils;
                t.IsAccomp = s.IsAccomp;
                t.ForeginDateOfIssue = s.ForeginDateOfIssue;
                t.ForeginDateEnd = s.ForeginDateEnd;
                t.ForeginNumber = s.ForeginNumber;
                t.ForeginSeria = s.ForeginSeria;
                t.ForeginSubjectIssue = s.ForeginSubjectIssue;
                t.ForeginTypeId = s.ForeginTypeId;
                t.EntityId = s.EntityId;
                t.IsLast = s.IsLast;
                t.DateOfBirth = s.DateOfBirth;
                t.PlaceOfBirth = s.PlaceOfBirth;
                t.OfferInRequestId = s.OfferInRequestId;
                t.ForeginLastName = s.ForeginLastName;
                t.ForeginName = s.ForeginName;
                t.Male = s.Male;
                t.AddonPhone = s.AddonPhone;
                if (Security.HasRight(AccessRightEnum.RequestEditTypeViolation))
                {
                    t.TypeViolationId = s.TypeViolationId;
                }

                t.DocumentCode = s.DocumentCode;
                t.IsProxy = s.IsProxy;
                t.ProxyDateOfIssure = s.ProxyDateOfIssure;
                t.ProxyEndDate = s.ProxyEndDate;
                t.NotaryName = s.NotaryName;
                t.ProxyNumber = s.ProxyNumber;
                t.IsCPMPK = s.IsCPMPK;

                if (source.TypeOfRestId == (long) TypeOfRestEnum.YouthRestOrphanCamps ||
                    source.TypeOfRestId == (long) TypeOfRestEnum.YouthRestOrphanCamps ||
                    target.TypeOfRestId == (long) TypeOfRestEnum.YouthRestOrphanCamps ||
                    target.TypeOfRestId == (long) TypeOfRestEnum.YouthRestOrphanCamps)
                {
                    if (s.Address != null)
                    {
                        if (t.Address == null)
                        {
                            var jss = new JavaScriptSerializer();
                            var clone = jss.Deserialize<Address>(jss.Serialize(s.Address));
                            clone.BtiAddress = null;
                            clone.BtiDistrict = null;
                            clone.BtiRegion = null;
                            t.AddressId = null;
                            UnitOfWork.AddEntity(clone);
                            t.Address = clone;
                        }
                        else
                        {
                            t.Address.Appartment = s.Address.Appartment;

                            if (s.Address.BtiAddressId == 0)
                            {
                                s.Address.BtiAddressId = null;
                            }

                            if (s.Address.BtiDistrictId == 0)
                            {
                                s.Address.BtiDistrictId = null;
                            }

                            if (s.Address.BtiRegionId == 0)
                            {
                                s.Address.BtiRegionId = null;
                            }

                            t.Address.BtiAddressId = s.Address.BtiAddressId;
                            t.Address.BtiDistrictId = s.Address.BtiDistrictId;
                            t.Address.BtiRegionId = s.Address.BtiRegionId;
                            t.Address.Corpus = s.Address.Corpus;
                            t.Address.House = s.Address.House;
                            t.Address.Name = s.Address.Name;
                            t.Address.Street = s.Address.Street;
                            t.Address.Stroenie = s.Address.Stroenie;
                            t.Address.Vladenie = s.Address.Vladenie;
                            t.Address.FiasId = s.Address.FiasId;
                        }
                    }
                }
            }

            foreach (var a in target.Attendant ?? new List<Applicant>())
            {
                a.Address = null;
                a.AddressId = null;
            }

            UnitOfWork.MergeCollection(source.Attendant, target.Attendant, (s, t) =>
            {
                if (!s.IsDeleted && !t.IsDeleted)
                {
                    t.DocumentDateOfIssue = s.DocumentDateOfIssue;
                    t.DocumentNumber = s.DocumentNumber;
                    t.DocumentSeria = s.DocumentSeria;
                    t.DocumentSubjectIssue = s.DocumentSubjectIssue;
                    t.DocumentTypeId = s.DocumentTypeId;
                    t.FirstName = s.FirstName;
                    t.LastName = s.LastName;
                    t.MiddleName = s.MiddleName;
                    t.HaveMiddleName = s.HaveMiddleName;
                    t.Email = s.Email;
                    t.Phone = s.Phone;
                    t.EntityId = s.EntityId;
                    t.ForeginDateOfIssue = s.ForeginDateOfIssue;
                    t.ForeginDateEnd = s.ForeginDateEnd;
                    t.ForeginNumber = s.ForeginNumber;
                    t.ForeginSeria = s.ForeginSeria;
                    t.ForeginSubjectIssue = s.ForeginSubjectIssue;
                    t.ForeginTypeId = s.ForeginTypeId;
                    t.IsLast = s.IsLast;
                    t.EntityId = s.EntityId;
                    t.DateOfBirth = s.DateOfBirth;
                    t.PlaceOfBirth = s.PlaceOfBirth;
                    t.OfferInRequestId = s.OfferInRequestId;
                    t.IndexField = s.Id;
                    t.ForeginLastName = s.ForeginLastName;
                    t.ForeginName = s.ForeginName;
                    t.Male = s.Male;
                    t.AddonPhone = s.AddonPhone;
                    if (Security.HasRight(AccessRightEnum.RequestEditTypeViolation))
                    {
                        t.TypeViolationId = s.TypeViolationId;
                    }

                    t.IsAgent = s.IsAgent;
                    t.IsAccomp = s.IsAccomp;
                    t.DocumentCode = s.DocumentCode;
                    if (t.Snils != s.Snils)
                    {
                        t.RelativeUniqeId = null;
                    }

                    t.Snils = s.Snils;
                    t.IsProxy = s.IsProxy;
                    t.ProxyDateOfIssure = s.ProxyDateOfIssure;
                    t.ProxyEndDate = s.ProxyEndDate;
                    t.NotaryName = s.NotaryName;
                    t.ProxyNumber = s.ProxyNumber;
                    t.ApplicantTypeId = s.ApplicantTypeId;
                }
                else if (!t.IsDeleted && s.IsDeleted)
                {
                    ExcludeAttendant(t);
                }
            });

            UnitOfWork.MergeCollection(source.Child, target.Child, (s, t) =>
            {
                if (!s.IsDeleted && !t.IsDeleted)
                {
                    t.DocumentDateOfIssue = s.DocumentDateOfIssue;
                    t.DocumentNumber = s.DocumentNumber;
                    t.DocumentSeria = s.DocumentSeria;
                    t.DocumentSubjectIssue = s.DocumentSubjectIssue;
                    t.DocumentTypeId = s.DocumentTypeId;
                    t.FirstName = s.FirstName;
                    t.LastName = s.LastName;
                    t.MiddleName = s.MiddleName;
                    t.HaveMiddleName = s.HaveMiddleName;
                    //t.IsApprovedInInteragency = s.IsApprovedInInteragency;
                    //t.IsIncludeInInteragency = s.IsIncludeInInteragency;
                    //t.IsApprovedInInteragencySecondary = s.IsApprovedInInteragencySecondary;
                    //t.IsIncludeInInteragencySecondary = s.IsIncludeInInteragencySecondary;
                    t.IsInvalid = s.IsInvalid;
                    t.EntityId = s.EntityId;
                    t.IsLast = s.IsLast;
                    t.IndexField = s.IndexField;
                    if (t.Snils != s.Snils)
                    {
                        t.ChildUniqeId = null;
                    }

                    t.Snils = s.Snils;
                    t.DocumentCode = s.DocumentCode;

                    if (s.ApplicantId > 0)
                    {
                        t.ApplicantId = s.ApplicantId;
                    }
                    else
                    {
                        t.Applicant = s.Applicant;
                    }

                    t.BenefitAnswerComment = s.BenefitAnswerComment;
                    t.BenefitAnswerDate = s.BenefitAnswerDate;
                    t.BenefitAnswerNumber = s.BenefitAnswerNumber;
                    t.BenefitApprove = s.BenefitApprove;
                    t.BenefitApproveComment = s.BenefitApproveComment;
                    t.BenefitApproveTypeId = s.BenefitApproveTypeId;
                    t.BenefitDate = s.BenefitDate;
                    t.BenefitDateOfIssure = s.BenefitDateOfIssure;
                    t.BenefitDocTypeId = s.BenefitDocTypeId;
                    t.BenefitEndDate = s.BenefitEndDate;
                    t.BenefitNeverEnd = s.BenefitNeverEnd;
                    t.BenefitNumber = s.BenefitNumber;
                    t.BenefitRequestDate = s.BenefitRequestDate;
                    t.BenefitRequestNumber = s.BenefitRequestNumber;
                    t.BenefitSubjectIssue = s.BenefitSubjectIssue;
                    t.BenefitTypeId = s.BenefitTypeId;
                    t.BenefitGroupInvalidId = s.BenefitGroupInvalidId;
                    t.TypeOfRestrictionId = s.TypeOfRestrictionId;
                    t.TypeOfSubRestrictionId = s.TypeOfSubRestrictionId;
                    t.BenefitGroupInvalidId = s.BenefitGroupInvalidId;
                    t.DateOfBirth = s.DateOfBirth;
                    t.PlaceOfBirth = s.PlaceOfBirth;
                    t.IsInvalid = s.IsInvalid;
                    t.ForeginDateOfIssue = s.ForeginDateOfIssue;
                    t.ForeginDateEnd = s.ForeginDateEnd;
                    t.ForeginNumber = s.ForeginNumber;
                    t.ForeginSeria = s.ForeginSeria;
                    t.ForeginSubjectIssue = s.ForeginSubjectIssue;
                    t.ForeginTypeId = s.ForeginTypeId;
                    t.Male = s.Male;
                    t.RegisteredInMoscow = s.RegisteredInMoscow;
                    t.SchoolId = s.SchoolId;
                    t.SchoolNotPresent = s.SchoolNotPresent;
                    t.StatusByChildId = s.StatusByChildId;
                    t.EntityId = s.EntityId;
                    t.DocumentFileTitle = s.DocumentFileTitle;
                    t.DocumentFileUrl = s.DocumentFileUrl;
                    t.BenefitRequestComment = s.BenefitRequestComment;
                    t.BenefitApproveHtml = s.BenefitApproveHtml;
                    t.Snils = s.Snils;
                    t.ForeginName = s.ForeginName;
                    t.ForeginLastName = s.ForeginLastName;
                    t.AmountOfCompensation = s.AmountOfCompensation;
                    t.CostOfTour = s.CostOfTour;
                    t.CostOfRide = s.CostOfRide;
                    t.DocumentSeriaCertOfBirth = s.DocumentSeriaCertOfBirth;
                    t.DocumentNumberCertOfBirth = s.DocumentNumberCertOfBirth;
                    t.ForeginLastName = s.ForeginLastName;
                    t.ForeginName = s.ForeginName;
                    t.DocumentTypeCertOfBirthId = s.DocumentTypeCertOfBirthId;
                    t.IsCPMPK = s.IsCPMPK;

                    if (Security.HasRight(AccessRightEnum.RequestEditTypeViolation))
                    {
                        t.TypeViolationId = s.TypeViolationId;
                    }

                    if (s.RequestInformationVoucherId > 0 || !s.RequestInformationVoucherId.HasValue)
                    {
                        t.RequestInformationVoucherId = s.RequestInformationVoucherId;
                    }

                    if (s.Address != null)
                    {
                        if (t.Address == null)
                        {
                            var jss = new JavaScriptSerializer();
                            var clone = jss.Deserialize<Address>(jss.Serialize(s.Address));
                            clone.BtiAddress = null;
                            clone.BtiDistrict = null;
                            clone.BtiRegion = null;
                            t.AddressId = null;
                            UnitOfWork.AddEntity(clone);
                            t.Address = clone;
                        }
                        else
                        {
                            t.Address.Appartment = s.Address.Appartment;
                            t.Address.BtiAddressId = s.Address.BtiAddressId;
                            t.Address.BtiDistrictId = s.Address.BtiDistrictId;
                            t.Address.BtiRegionId = s.Address.BtiRegionId;
                            t.Address.Corpus = s.Address.Corpus;
                            t.Address.House = s.Address.House;
                            t.Address.Name = s.Address.Name;
                            t.Address.Street = s.Address.Street;
                            t.Address.Stroenie = s.Address.Stroenie;
                            t.Address.Vladenie = s.Address.Vladenie;
                            t.Address.FiasId = s.Address.FiasId;
                        }
                    }
                }
                else if (!t.IsDeleted && s.IsDeleted)
                {
                    ExcludeChild(t);
                }
            });

            if (!target.BookingGuid.HasValue)
            {
                DAL.UnitOfWork.MergeCollectionStatic(source.TimesOfRest, target.TimesOfRest,
                    (s, t) => { t.TimeOfRestId = s.TimeOfRestId; }, (s) =>
                    {
                        if (s.TimeOfRestId.HasValue)
                        {
                            s.RequestId = target.Id;
                            UnitOfWork.AddEntity(s);
                        }
                    }, (s) => { UnitOfWork.Delete(s); });

                DAL.UnitOfWork.MergeCollectionStatic(source.PlacesOfRest, target.PlacesOfRest,
                    (s, t) => { t.PlaceOfRestId = s.PlaceOfRestId; }, (s) =>
                    {
                        if (s.PlaceOfRestId.HasValue)
                        {
                            s.RequestId = target.Id;
                            UnitOfWork.AddEntity(s);
                        }
                    }, (s) => { UnitOfWork.Delete(s); });
            }
        }

        private void ExcludeChild(Child child)
        {
            if (!Security.HasRight(AccessRightEnum.ExcludeChild))
            {
                return;
            }

            child.IsDeleted = true;
            if (child.ContingentGuid.HasValue)
            {
                child.EkisNeedSend = true;
            }

            child.BoutId = null;
            child.Bout = null;
            child.LinkToPeoples?.ToList().ForEach(r => UnitOfWork.Context.Entry(r).State = EntityState.Deleted);
            child.PartyId = null;
            child.Party = null;
            child.TourVolumeId = null;
            child.TourVolume = null;
            child.Key = string.Empty;
            child.KeySame = string.Empty;
            child.IntervalStart = null;
            child.IntervalEnd = null;
            child.TypeOfGroupCheck = null;
            child.TypeOfGroupCheckId = null;
            child.YearOfCompany = null;

            if (child.Request.StatusId == (long) StatusEnum.DecisionMaking ||
                child.Request.StatusId == (long) StatusEnum.DecisionMakingCovid)
            {
                child.Request.CountPlace--;
            }

            UnitOfWork.Context.Entry(child).State = EntityState.Modified;

            var dateIncome = child.Request?.Tour?.DateIncome;
            if (dateIncome.HasValue && dateIncome.Value > DateTime.Today)
            {
                var booking = UnitOfWork.GetSet<Domain.Booking>()
                    .FirstOrDefault(b => b.Code == child.Request.BookingGuid);
                if (booking != null)
                {
                    booking.CountPlace--;
                }
            }
        }

        private void ExcludeAttendant(Applicant applicant)
        {
            if (!Security.HasRight(AccessRightEnum.ExcludeChild))
            {
                return;
            }

            applicant.IsDeleted = true;

            applicant.BoutId = null;
            applicant.Bout = null;
            applicant.LinkToPeoples?.ToList().ForEach(r => UnitOfWork.Context.Entry(r).State = EntityState.Deleted);
            applicant.TourVolumeId = null;
            applicant.TourVolume = null;
            applicant.Key = string.Empty;
            applicant.IntervalStart = null;
            applicant.IntervalEnd = null;

            UnitOfWork.Context.Entry(applicant).State = EntityState.Modified;

            var request = applicant.Request ??
                          UnitOfWork.GetSet<Request>().FirstOrDefault(r => r.ApplicantId == applicant.Id);

            if (request?.StatusId == (long) StatusEnum.DecisionMaking ||
                request?.StatusId == (long) StatusEnum.DecisionMakingCovid)
            {
                request.CountAttendants--;
            }

            var dateIncome = request?.Tour?.DateIncome;
            if (dateIncome.HasValue && dateIncome.Value > DateTime.Today)
            {
                var booking = UnitOfWork.GetSet<Domain.Booking>().FirstOrDefault(b => b.Code == request.BookingGuid);
                if (booking != null)
                {
                    booking.CountPlace--;
                }
            }
        }

        public List<AddonServices> GetAddonServicesForRequest(long? requestId)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);

            var requestTourId = UnitOfWork.GetSet<Request>().Where(r => r.Id == requestId).Select(r => r.TourId)
                .FirstOrDefault();
            return
                UnitOfWork.GetSet<AddonServices>()
                    .Where(
                        s =>
                            s.TourId == requestTourId &&
//(s.DateFrom <= DateTime.Now || !s.DateFrom.HasValue) &&
//(s.DateTo >= DateTime.Now || !s.DateTo.HasValue) &&
                            s.IsActive && s.StateId == StateMachineStateEnum.AddonService.Formed &&
                            (s.Parent == null || s.Parent.StateId == StateMachineStateEnum.AddonService.Formed))
                    .ToList();
        }

        private void CopyTypeViolationFromModelToEntity(Request entity, Request model)
        {
            entity.Applicant.TypeViolationId = model.Applicant.TypeViolationId;

            if (model.Attendant == null)
            {
                return;
            }

            foreach (var modelAttendant in model.Attendant)
            {
                var entityAttendant = entity.Attendant?.FirstOrDefault(i => i.Id == modelAttendant.Id);
                if (entityAttendant != null)
                {
                    entityAttendant.TypeViolationId = modelAttendant.TypeViolationId;
                }
            }

            if (model.Child == null)
            {
                return;
            }

            foreach (var modelChild in model.Child)
            {
                var entityChild = entity.Child?.FirstOrDefault(i => i.Id == modelChild.Id);
                if (entityChild != null)
                {
                    entityChild.TypeViolationId = modelChild.TypeViolationId;
                }
            }
        }
    }
}
