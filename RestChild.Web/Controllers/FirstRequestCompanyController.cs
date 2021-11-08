using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.Serialization;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Mvc;
using RestChild.Booking.Logic.Extensions;
using RestChild.Booking.Logic.Services;
using RestChild.Comon;
using RestChild.Comon.Config;
using RestChild.Comon.Dto;
using RestChild.Comon.Dto.Booking;
using RestChild.Comon.Dto.SearchRestChild;
using RestChild.Comon.Enumeration;
using RestChild.DAL;
using RestChild.DAL.RepositoryExtensions;
using RestChild.Domain;
using RestChild.Extensions.Extensions;
using RestChild.Extensions.Filter;
using RestChild.Security.Logger;
using RestChild.Web.Controllers.WebApi;
using RestChild.Web.Extensions;
using RestChild.Web.Models;
using RestChild.Web.Models.Business.Export;
using RestChild.Web.Properties;
using TimeOfRest = RestChild.Domain.TimeOfRest;

namespace RestChild.Web.Controllers
{
    /// <summary>
    ///     управление заявочной кампанией
    /// </summary>
    [System.Web.Mvc.Authorize]
    public partial class FirstRequestCompanyController : BaseController
    {
        private readonly string DefaultOptionValue = "-- Не выбрано --";
        public WebFirstRequestCompanyController ApiController { get; set; }
        public WebExchangeController ExchangeController { get; set; }
        public WebVocabularyController VocController { get; set; }
        public WebBtiDistrictsController DistrictController { get; set; }
        public WebToursController TourController { get; set; }
        public WebRestTypeController RestTypeController { get; set; }
        public WebRestTimeController RestTimeController { get; set; }
        public WebRestPlaceController RestPlaceController { get; set; }
        public WebInteragencyRequestController InteragencyRequestController { get; set; }

        public override void SetUnitOfWorkInRefClass(IUnitOfWork unitOfWork)
        {
            base.SetUnitOfWorkInRefClass(unitOfWork);
            ApiController.SetUnitOfWorkInRefClass(UnitOfWork);
            ExchangeController.SetUnitOfWorkInRefClass(UnitOfWork);
            VocController.SetUnitOfWorkInRefClass(UnitOfWork);
            DistrictController.SetUnitOfWorkInRefClass(UnitOfWork);
            TourController.SetUnitOfWorkInRefClass(UnitOfWork);
            InteragencyRequestController.SetUnitOfWorkInRefClass(UnitOfWork);
            RestTypeController.SetUnitOfWorkInRefClass(UnitOfWork);
        }

        public ActionResult RequestPrint(long id)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);
            var request = ApiController.RequestEdit(id);

            if (request == null || request.IsDeleted)
            {
                return RedirectToAction("RequestList");
            }

            return View(new RequestViewModel(request));
        }


        /// <summary>
        ///     редактирование заявления
        /// </summary>
        [System.Web.Mvc.HttpGet]
        public ActionResult RequestEdit(long? id, bool needValidate = false, string saveaction = null,
            bool needCompleteionAlert = false, long? typeOfRestId = null, long? tourId = null, Guid? bookingGuid = null,
            string rateTypeString = null, bool reApply = false)
        {
            if (!Security.HasAnyRightsForSomeOrganization(new[] {AccessRightEnum.RequestView}))
            {
                return RedirectToAvalibleAction();
            }

            SetUnitOfWorkInRefClass(UnitOfWork);

            var request = ApiController.RequestEdit(id);
            var reApplyReq = ApiController.RequestEdit(null);
            if (request == null || request.IsDeleted || (request.TypeOfRest?.Commercial ?? false))
            {
                return RedirectToAction("RequestList");
            }

            if (!request.IsLast)
            {
                ViewBag.LastVersionRequest = ApiController.GetLastVersionRequest(request.EntityId ?? 0);
            }
            var model = new RequestViewModel(request);
            if (reApply)
            {
                reApplyReq.TypeOfRestId = request.TypeOfRestId;
                reApplyReq.AgentApplicant = request.AgentApplicant;
                var app = new Applicant();
                app.FirstName = request.Applicant.FirstName;
                app.LastName = request.Applicant.LastName;
                app.HaveMiddleName = request.Applicant.HaveMiddleName;
                app.MiddleName = request.Applicant.MiddleName;
                app.Male = (bool)request.Applicant.Male;
                app.Snils = request.Applicant.Snils;
                app.DateOfBirth = request.Applicant.DateOfBirth;
                app.PlaceOfBirth = request.Applicant.PlaceOfBirth;
                app.DocumentTypeId = request.Applicant.DocumentTypeId;
                app.DocumentSeria = request.Applicant.DocumentSeria;
                app.DocumentNumber = request.Applicant.DocumentNumber;
                app.DocumentDateOfIssue = request.Applicant.DocumentDateOfIssue;
                app.DocumentSubjectIssue = request.Applicant.DocumentSubjectIssue;
                app.BenefitTypeId = request.Applicant.BenefitTypeId;
                app.AddressId = request.Applicant.AddressId;
                app.Address = request.Applicant.Address;
                app.Phone = request.Applicant.Phone;
                app.AddonPhone = request.Applicant.AddonPhone;
                app.Email = request.Applicant.Email;
                app.IsAccomp = request.Applicant.IsAccomp;
                app.IsAgent = request.Applicant.IsAgent;
                app.IsApplicant = request.Applicant.IsApplicant;
                app.IsProxy = request.Applicant.IsProxy;

                if (!request.Attendant.IsNullOrEmpty())
                {
                    reApplyReq.CountAttendants = request.CountAttendants;
                    List<Applicant> attendan = new List<Applicant>();
                    foreach (Applicant attend in request.Attendant)
                    {
                        var att = new Applicant();
                        att.FirstName = attend.FirstName;
                        att.LastName = attend.LastName;
                        att.HaveMiddleName = attend.HaveMiddleName;
                        att.MiddleName = attend.MiddleName;
                        att.Male = (bool)attend.Male;
                        att.Snils = attend.Snils;
                        att.DateOfBirth = attend.DateOfBirth;
                        att.PlaceOfBirth = attend.PlaceOfBirth;
                        att.DocumentTypeId = attend.DocumentTypeId;
                        att.DocumentSeria = attend.DocumentSeria;
                        att.DocumentNumber = attend.DocumentNumber;
                        att.DocumentDateOfIssue = attend.DocumentDateOfIssue;
                        att.DocumentSubjectIssue = attend.DocumentSubjectIssue;
                        att.BenefitTypeId = attend.BenefitTypeId;
                        att.AddressId = attend.AddressId;
                        att.Address = attend.Address;
                        att.Phone = attend.Phone;
                        att.AddonPhone = attend.AddonPhone;
                        att.Email = attend.Email;
                        att.IsAccomp = attend.IsAccomp;
                        att.IsAgent = attend.IsAgent;
                        att.IsApplicant = attend.IsApplicant;
                        att.IsProxy = attend.IsProxy;
                        attendan.Add(att);
                    }
                    if (app.IsAccomp)
                        attendan.Add(app);
                    reApplyReq.Attendant = attendan;
                }
                reApplyReq.Attendant = request.Attendant;
               // reApplyReq.Applicant = app;
                if (!request.AgentId.IsNullOrEmpty())
                {
                    var agent = new Agent();
                    agent.FirstName = request.Agent.FirstName;
                    agent.LastName = request.Agent.LastName;
                    agent.HaveMiddleName = request.Agent.HaveMiddleName;
                    agent.MiddleName = request.Agent.MiddleName;
                   // agent.Male = (bool)request.Agent.Male;
                    agent.Snils = request.Agent.Snils;
                    agent.DateOfBirth = request.Agent.DateOfBirth;
                    agent.PlaceOfBirth = request.Agent.PlaceOfBirth;
                    agent.DocumentTypeId = request.Agent.DocumentTypeId;
                    agent.DocumentSeria = request.Agent.DocumentSeria;
                    agent.DocumentNumber = request.Agent.DocumentNumber;
                    agent.DocumentDateOfIssue = request.Agent.DocumentDateOfIssue;
                    agent.DocumentSubjectIssue = request.Agent.DocumentSubjectIssue;
                    agent.Phone = request.Agent.Phone;
                    agent.Email = request.Agent.Email;
                    reApplyReq.Agent = agent;
                }
                if (!request.Child.IsNullOrEmpty())
                    reApplyReq.Child.Clear();
                List<Child> childs = new List<Child>();
                foreach (var c in request.Child)
                {
                    var chil = new Child();
                    chil.FirstName = c.FirstName;
                    chil.LastName = c.LastName;
                    chil.HaveMiddleName = c.HaveMiddleName;
                    chil.MiddleName = c.MiddleName;
                    chil.Male = c.Male;
                    chil.Snils = c.Snils;
                    chil.DateOfBirth = c.DateOfBirth;
                    chil.PlaceOfBirth = c.PlaceOfBirth;
                    chil.DocumentTypeId = c.DocumentTypeId;
                    chil.DocumentSeria = c.DocumentSeria;
                    chil.DocumentNumber = c.DocumentNumber;
                    chil.DocumentDateOfIssue = c.DocumentDateOfIssue;
                    chil.DocumentSubjectIssue = c.DocumentSubjectIssue;
                    chil.BenefitTypeId = c.BenefitTypeId;
                    chil.AddressId = c.AddressId;
                    chil.Address = c.Address;
                    chil.SchoolId = c.SchoolId;
                    chil.School = c.School;
                    chil.ApplicantId = c.ApplicantId;
                    childs.Add(chil);
                }
                reApplyReq.Child = childs;
                if (!request.Attendant.IsNullOrEmpty())
                    reApplyReq.Attendant.Clear();
                foreach (var c in request.Attendant)
                {
                    var chil = new Applicant();
                    chil.FirstName = c.FirstName;
                    chil.LastName = c.LastName;
                    chil.HaveMiddleName = c.HaveMiddleName;
                    chil.MiddleName = c.MiddleName;
                    chil.Male = (bool)c.Male;
                    chil.Snils = c.Snils;
                    chil.DateOfBirth = c.DateOfBirth;
                    chil.PlaceOfBirth = c.PlaceOfBirth;
                    chil.DocumentTypeId = c.DocumentTypeId;
                    chil.DocumentSeria = c.DocumentSeria;
                    chil.DocumentNumber = c.DocumentNumber;
                    chil.DocumentDateOfIssue = c.DocumentDateOfIssue;
                    chil.DocumentSubjectIssue = c.DocumentSubjectIssue;
                    chil.BenefitTypeId = c.BenefitTypeId;
                    chil.AddressId = c.AddressId;
                    chil.Address = c.Address;
                    chil.Phone = c.Phone;
                    chil.AddonPhone = c.AddonPhone;
                    chil.Email = c.Email;
                    reApplyReq.Attendant.Add(chil);
                }
                reApplyReq.CountAttendants = request.CountAttendants;
                reApplyReq.CountPlace = request.CountPlace;
                model = new RequestViewModel(reApplyReq);
            }


            BookingRequest bookingRequest = null;
            if (!id.HasValue && typeOfRestId.HasValue && tourId.HasValue && bookingGuid.HasValue)
            {
                bookingRequest = new BookingRequest
                {
                    TypeOfRestId = typeOfRestId.Value,
                    TourId = tourId.Value,
                    BookingGuid = bookingGuid
                };

                var client = Booking.Logic.Booking.GetServiceClient(bookingRequest);
                try
                {
                    bookingRequest = client.GetPreBooking(bookingRequest);
                }
                finally
                {
                    Booking.Logic.Booking.CloseClient(client);
                }

                model.HasBooking = true;
            }

            model.HasBooking |= model.Data.BookingGuid.HasValue;

            if (bookingRequest != null)
            {
                FillBookingModel(model, bookingRequest);
            }
            else if (model.HasBooking)
            {
                FillLocationInModel(model);
            }
            else if (model.Data.Id == 0)
            {
                model.Data.IsFirstCompany = true;
            }

            FillRequestActionsAndEditMode(model);
            model.PrepareFiles(UnitOfWork.GetSet<RequestFileType>().ToList());
            PrepareVocabulary(request);
            FillVocsInVm(model);

            ApiController.CheckChildren(model);
            ApiController.CheckAttendants(model);
            ApiController.CheckApplicant(model);
            ApiController.ValidOnInSameTime(model);
            ApiController.BadPersonInRequest(model);

            if (needValidate)
            {
                model.CheckModel(saveaction);
                if (model.SameChildren.Any() || model.SameAttendantSnils.Any() || model.SameAttendants.Any())
                {
                    model.IsValid = false;
                }
            }

            if (model.Child != null)
            {
                foreach (var child in model.Child)
                {
                    child.Results = child.Data.Id > 0
                        ? InteragencyRequestController.GetRequestResultsForChild(child.Data.Id)
                        : null;
                    child.MinAge = model.NullSafe(m => m.Data.TypeOfRest.MinAge);
                    child.MaxAge = model.NullSafe(m => m.Data.TypeOfRest.MaxAge);
                    var xmlText = child.Data.BenefitApproveHtml;
                    if (!string.IsNullOrEmpty(xmlText))
                    {
                        try
                        {
                            child.ResidentPreferentialCategories =
                                Serialization.DeserializerDataContract<List<ResidentPreferentialCategories>>(xmlText);
                        }
                        catch (SerializationException e)
                        {
                            Logger.Error("Ошибка сериализации", e);
                            child.ResidentPreferentialCategories = new List<ResidentPreferentialCategories>();
                        }
                    }
                }
            }

            ViewBag.NeedCompletionAlert = needCompleteionAlert;

            ViewBag.BrPaymentDocument =
                (Settings.Default.BrPaymentDocument ?? string.Empty).Split(',').Select(s => s.Trim()).ToArray();

            if (!model.Data.TypeOfRestId.HasValue)
            {
                model.Data.TypeOfRest = model.TypeOfRests.FirstOrDefault();
                model.Data.TypeOfRestId = model.Data.TypeOfRest?.Id;
            }

            if (model.Data?.Id > 0)
            {
                SecurityLogger.AddToLogNamedProcess(UnitOfWork, "Просмотр заявления",
                    string.Concat("Пользователь {0} ({1}) просмотрел заявление ", model.Data.RequestNumber, "(",
                        model.Data.Id.ToString(), ")"), Security.GetCurrentAccountId().Value,
                    HttpContext.Request.UserAgent);
            }

            return View(model);
        }

        /// <summary>
        ///     заполнение размещений в модели.
        /// </summary>
        private void FillLocationInModel(RequestViewModel model)
        {
            var bookings =
                UnitOfWork.GetSet<Domain.Booking>()
                    .Where(b => b.Code == model.Data.BookingGuid && b.TourVolume.TypeOfRoomsId.HasValue)
                    .ToList();
            if (!bookings.Any())
            {
                return;
            }

            model.Location =
                bookings.Select(
                    b =>
                        new LocationRequest
                        {
                            Count = b.CountRooms,
                            Name = b.TourVolume.TypeOfRooms.ToString(),
                            RoomId = b.TourVolume.TypeOfRoomsId ?? 0
                        }).ToList();
        }

        /// <summary>
        ///     заполнение возможных действий и режима редактирования
        /// </summary>
        /// <param name="model"></param>
        private void FillRequestActionsAndEditMode(RequestViewModel model)
        {
            IList<StatusAction> actions = new List<StatusAction>();
            var checks = Security.GetSecurity().ToArray();

            actions = actions.Where(a => !a.RequestOnMoney.HasValue || a.RequestOnMoney == model.Data.RequestOnMoney)
                .ToList();

            if (model.Data?.StatusId != null && model.Data.Id > 0)
            {
                actions = ApiController.GetActions(model.Data.StatusId.Value, checks);
                actions = actions.Where(a => a.Code != AccessRightEnum.Status.ToReadyToPay).ToList();
            }

            // разводим статусную модель по многоэтапной и не многоэтапной кампаниям.
            actions =
                actions.Where(a =>
                        !a.IsFirstCompany.HasValue || a.IsFirstCompany == (model.Data?.IsFirstCompany ?? false))
                    .Where(a => !a.RequestOnMoney.HasValue || a.RequestOnMoney == (model.Data?.RequestOnMoney ?? false))
                    .ToList();

            model.IsEditable &= checks.Contains(AccessRightEnum.RequestManage) &&
                                checks.Contains(AccessRightEnum.RequestView);

            // нельзя отклонять регистрацию
            var action = actions.FirstOrDefault(a => a.Code == AccessRightEnum.Status.ToRegistrationDecline);
            if (action != null)
            {
                actions.Remove(action);
            }

            // нельзя отклонять регистрацию
            action = actions.FirstOrDefault(a => a.Code == AccessRightEnum.Status.ToRegistrationDeclineAttendant);
            if (action != null)
            {
                actions.Remove(action);
            }

            if (model.Data != null && model.Data.SourceId == (long) SourceEnum.Operator)
            {
                action = actions.FirstOrDefault(a => a.Code == AccessRightEnum.Status.ToWaitApplicant);
                if (action != null)
                {
                    actions.Remove(action);
                }
            }

            if (model.Data?.StatusId == (long) StatusEnum.ApplicantCome)
            {
                action = actions.FirstOrDefault(a => a.Code == AccessRightEnum.RetryRequestInBaseRegistry);
                if (action != null)
                {
                    actions.Remove(action);
                }
            }

            action = actions.FirstOrDefault(a => a.Code == AccessRightEnum.Status.FcToWaitApplicantMoney);
            if (action != null)
            {
                actions.Remove(action);
            }

            if (model.Data?.StatusId == (long) StatusEnum.CertificateIssued &&
                (!model.Data.IsFirstCompany || !model.Data.RequestOnMoney))
            {
                action = actions.FirstOrDefault(a => a.Code == AccessRightEnum.Status.FcToWaitApplicantMoney);
                if (action != null)
                {
                    actions.Remove(action);
                }
            }

            if (!Security.HasRight(AccessRightEnum.EditAfterRegistration))
            {
                action = actions.FirstOrDefault(a => a.Code == AccessRightEnum.Status.FcToDecisionMaking);
                if (action != null)
                {
                    actions.Remove(action);
                }

                action = actions.FirstOrDefault(a => a.Code == AccessRightEnum.Status.FcToDecisionMakingCovid);
                if (action != null)
                {
                    actions.Remove(action);
                }

                action = actions.FirstOrDefault(a => a.Code == AccessRightEnum.Status.FcToIncludedInList);
                if (action != null)
                {
                    actions.Remove(action);
                }

                action = actions.FirstOrDefault(a => a.Code == AccessRightEnum.Status.FcToRanging);
                if (action != null)
                {
                    actions.Remove(action);
                }
            }

            action = actions.FirstOrDefault(a => a.Code == AccessRightEnum.Status.FcFinishWorkWithRequest);
            if (action != null && !(model.Data?.MayFinalSend ?? false))
            {
                actions.Remove(action);
            }

            action = actions.FirstOrDefault(a => a.Code == AccessRightEnum.RequestTo10753);
            if (action != null)
            {
                actions.Remove(action);
            }

            if (model.Data?.TypeOfRestId == (long) TypeOfRestEnum.Compensation ||
                model.Data?.TypeOfRestId == (long) TypeOfRestEnum.CompensationYouthRest ||
                model.Data?.TypeOfRestId == (long) TypeOfRestEnum.CompensationGroup)
            {
                action = actions.FirstOrDefault(a => a.Code == AccessRightEnum.Status.ToCancelByApplicant);
                if (action != null)
                {
                    actions.Remove(action);
                }
            }



            /*if (model.Data?.StatusId == (long) StatusEnum.DecisionMakingCovid &&
                model.Data?.TypeOfRest?.NeedPlacment == false &&
                model.Data?.TypeOfRestId != (long)TypeOfRestEnum.YouthRestCamps &&
                model.Data?.TypeOfRestId != (long)TypeOfRestEnum.YouthRestOrphanCamps)
            {
                action = actions.FirstOrDefault(a => a.Code == AccessRightEnum.Status.FcToDecisionIsMade);
                if (action != null)
                {
                    actions.Remove(action);
                }
            }*/

            /*if (model.Data?.TypeOfRestId == (long)TypeOfRestEnum.Money ||
                 model.Data?.TypeOfRestId == (long)TypeOfRestEnum.MoneyOn7To15 ||
                 model.Data?.TypeOfRestId == (long)TypeOfRestEnum.MoneyOn18 ||
                 model.Data?.TypeOfRestId == (long)TypeOfRestEnum.MoneyOn3To7)
            {*/
                action = actions.FirstOrDefault(a => a.Code == AccessRightEnum.Status.FcToDecisionMakingCovid);
                if (action != null)
                {
                    actions.Remove(action);
                }
            /*}*/


            //в случае если сертификат по заявлению погашен или заявления в статусе "услуга оказана" - отменяем любые возможные действия
            if ((model.Data?.Certificates?.Any(ss => ss.StateMachineStateId != StateMachineStateEnum.Deleted) ?? false) ||
                (model.Data?.DeclineReasonId != null && model.Data?.StatusId == (long) StatusEnum.CertificateIssued))
            {
                foreach (var act in actions.ToList())
                {
                    //Услуга оказана "1075.3" приравнена к отказам (причина отказа - 201911) (КОВИД 2019)
                    //архимегакостыль-кровь-из-глаз
                    if (act.Id == ((long) StatusEnum.DecisionMaking + 200000) && model.Data?.DeclineReasonId == 201911)
                    {
                        continue;
                    }
                    actions.Remove(act);
                }
            }

            model.Actions = actions;
        }

        /// <summary>
        ///     заполнение модели по бронированию
        /// </summary>
        private void FillBookingModel(RequestViewModel model, BookingRequest bookingRequest)
        {
            var bookingRequestTour = UnitOfWork.GetById<Tour>(bookingRequest.TourId);
            var bookingRequestTypeOfRest = VocController.GetTypesOfRest(bookingRequest.TypeOfRestId ?? 0);
            model.BookingRequest = bookingRequest;
            var index = -1;
            model.Child =
                Enumerable.Repeat(0, bookingRequest.Places)
                    .Select(
                        c =>
                            new ChildViewModel(new Child
                            {
                                IsLast = true, IsDeleted = false, Request = model.Data, IndexField = index, Id = index--
                            })
                            {
                                NotForDelete = true,
                                HasNotMiddlename = false
                            })
                    .ToList();
            index = -1;
            model.Attendant =
                Enumerable.Repeat(0, bookingRequest.Attendants >= 1 ? bookingRequest.Attendants - 1 : 0)
                    .Select(
                        c =>
                            new ApplicantViewModel(new Applicant
                                {Request = model.Data, IndexField = index, Id = index--})
                            {
                                HasNotMiddlename = false,
                                RequestModel = model
                            })
                    .ToList();
            if (model.Applicant != null)
            {
                model.Applicant.HasNotMiddlename = false;
            }

            model.ForbidInsertAttendants = false;
            model.ForbidInsertChilds = true;
            model.Data.TourId = bookingRequest.TourId;
            model.Data.Tour = bookingRequestTour;
            model.Data.HotelsId = bookingRequestTour.HotelsId;
            model.Data.Hotels = bookingRequestTour.Hotels;
            model.Data.PlaceOfRest = bookingRequestTour.Hotels?.PlaceOfRest;
            model.Data.PlaceOfRestId = model.Data.PlaceOfRest?.Id;
            model.Data.TypeOfRestId = bookingRequest.TypeOfRestId;
            model.Data.TypeOfRest = bookingRequestTypeOfRest;
            model.Data.MainPlaces = bookingRequest.Places + bookingRequest.Attendants;
            model.Data.BookingGuid = bookingRequest.BookingGuid;
            model.Data.SubjectOfRestId = bookingRequestTour.SubjectOfRestId;
            model.Data.TimeOfRestId = bookingRequestTour.TimeOfRestId;
            model.Data.TimeOfRest = bookingRequestTour.TimeOfRest;
            model.Data.CountPlace = bookingRequest.Places;
            model.Data.CountAttendants = bookingRequest.Attendants;
            model.Data.YearOfRestId = bookingRequestTour.YearOfRestId;
            model.Location = bookingRequest.Rooms;
            if (model.Location != null && model.Location.Any())
            {
                var roomsId = model.Location.Select(l => l.RoomId).ToList();
                var rooms = UnitOfWork.GetSet<TourVolume>().Where(t => roomsId.Contains(t.Id)).ToList();

                foreach (var location in model.Location)
                {
                    var room = rooms.FirstOrDefault(r => r.Id == location.RoomId);
                    if (room != null)
                    {
                        location.Name = room.TypeOfRooms.ToString();
                    }
                }
            }

            if (!(model.Data.TypeOfRest?.NeedAttendant ?? true))
            {
                model.Attendant.Add(new ApplicantViewModel(new Applicant {Request = model.Data})
                {
                    HasNotMiddlename = false,
                    RequestModel = model
                });
            }
        }

        /// <summary>
        ///     заполнение справочников во ViewBag
        /// </summary>
        /// <param name="request"></param>
        /// <param name="filter"></param>
        private void PrepareVocabulary(Request request = null, RequestSearchFilterModel filter = null)
        {
            ViewBag.PlacesOfRest =
                VocController.GetPlacesOfRestInternal(true).OrderBy(p => p.Id)
                    .InsertAt(new PlaceOfRest {Id = 0, Name = DefaultOptionValue});

            #region DocumentsType set

            var documents = VocController.GetDocumentsType(request?.TypeOfRestId);
            ViewBag.DocumentsTypeChild =
                documents.Where(d => d.ForChild && !d.ForForeign)
                    .OrderBy(p => p.Id)
                    .ToList()
                    .InsertAt(new DocumentType {Id = 0, Name = DefaultOptionValue});
            ViewBag.DocumentsTypeForeign =
                documents.Where(d => d.ForForeign && d.ForChild)
                    .OrderBy(p => p.Id)
                    .ToList()
                    .InsertAt(new DocumentType {Id = 0, Name = DefaultOptionValue});

            ViewBag.DocumentsTypeForeignApplicant =
                documents.Where(d => d.ForForeign && d.ForApplicant)
                    .OrderBy(p => p.Id)
                    .ToList()
                    .InsertAt(new DocumentType {Id = 0, Name = DefaultOptionValue});

            ViewBag.DocumentsTypeForeignAttendant =
                documents.Where(d => d.ForForeign && d.ForOther)
                    .OrderBy(p => p.Id)
                    .ToList()
                    .InsertAt(new DocumentType {Id = 0, Name = DefaultOptionValue});

            ViewBag.DocumentsTypeApplicant =
                documents.Where(d => d.ForApplicant && !d.ForForeign)
                    .OrderBy(p => p.Id)
                    .ToList()
                    .InsertAt(new DocumentType {Id = 0, Name = DefaultOptionValue});

            ViewBag.DocumentsTypeAgent =
                documents.Where(d => d.ForAgent && !d.ForForeign)
                    .OrderBy(p => p.Id)
                    .ToList()
                    .InsertAt(new DocumentType {Id = 0, Name = DefaultOptionValue});

            ViewBag.DocumentsTypeOther =
                documents.Where(d => d.ForOther && !d.ForForeign)
                    .OrderBy(p => p.Id)
                    .ToList()
                    .InsertAt(new DocumentType {Id = 0, Name = DefaultOptionValue});

            #endregion

            ViewBag.ApplicantType = UnitOfWork.GetSet<ApplicantType>().Where(a => !a.IsDeleted)
                .OrderBy(p => p.Id)
                .ToList()
                .InsertAt(new ApplicantType {Id = 0, Name = DefaultOptionValue});

            ViewBag.StatusOfRest =
                VocController.GetStatusOfRest()
                    .Where(s => s.Id != (long) StatusEnum.ErrorRequest && s.ForPreferential)
                    .OrderBy(p => p.Id == (long) StatusEnum.Draft ? 0 : 1)
                    .ThenBy(p => p.Name)
                    .ToList();

            ViewBag.SubjectsOfRest =
                VocController.GetSubjectsOfRest()
                    .OrderBy(p => p.Id)
                    .ToList()
                    .InsertAt(new SubjectOfRest {Id = 0, Name = DefaultOptionValue});

            ViewBag.Transfers = UnitOfWork.GetSet<TypeOfTransfer>().ToList()
                .InsertAt(new TypeOfTransfer {Id = 0, Name = DefaultOptionValue});

            var typeOfRests =
                UnitOfWork.GetSet<TypeOfRest>()
                    .Where(p => (p.IsActive || p.Id == (long)TypeOfRestEnum.MoneyOn18) && !p.Commercial).ToList()
                    .OrderBy(OrderString)
                    .InsertAt(new TypeOfRest {Id = 0, Name = DefaultOptionValue}).ToList();

            HashSet<long> hs = typeOfRests.Select(t => t.ParentId).Where(p => p.HasValue).Select(p => p.Value).ToHashSet<long>();
            ViewBag.TypesOfRest = typeOfRests.Select(t => GetLeveled(hs, t)).ToList();

            // Цели обращений для которых возможна фильтрация по наличию сопровождающего.
            long parentGroupId = 16;
            ViewBag.TypesOfRestForAttendantFilter = typeOfRests
                .Where(x => x.ParentId == parentGroupId || x.Id == parentGroupId)
                .Select(x => x.Name)
                .ToList();

            ViewBag.TimesOfRest =
                VocController
                    .GetTimesOfRestWithoutFilter(request.NullSafe(r => r.TypeOfRestId) ??
                                                 filter.NullSafe(f => f.TypeOfRestId))
                    .OrderBy(p => p.Id)
                    .InsertAt(new TimeOfRest {Id = 0, Name = DefaultOptionValue});

            var benefitsQuery = UnitOfWork.GetAll<BenefitType>().Where(b => b.IsActive);
            if (request?.TypeOfRestId > 0 || request?.TypeOfRestId == (long) TypeOfRestEnum.Compensation ||
                request?.TypeOfRestId == (long) TypeOfRestEnum.CompensationYouthRest)
            {
                benefitsQuery = benefitsQuery.Where(t => t.TypeOfRestId == request.TypeOfRestId);
            }
            else
            {
                benefitsQuery = benefitsQuery.Where(b => !b.SameBenefitId.HasValue);
            }

            var benefits = benefitsQuery.OrderBy(p => p.Id).ToList()
                .InsertAt(new BenefitType {Id = 0, Name = DefaultOptionValue});

            var usedBenefitTypeIds =
                request?.Child?.Select(c => c.BenefitTypeId).Where(b => b.HasValue).Select(b => b.Value).Distinct()
                    .Where(b => benefits.All(t => t.Id != b)).ToList() ??
                new List<long>();

            if (usedBenefitTypeIds.Any())
            {
                benefits.AddRange(UnitOfWork.GetSet<BenefitType>().Where(b => usedBenefitTypeIds.Contains(b.Id))
                    .ToList());
            }

            ViewBag.BenefitType = benefits;


            ViewBag.BenefitApproveType =
                VocController.GetBenefitApproveType()
                    .OrderBy(p => p.Id)
                    .InsertAt(new BenefitApproveType {Id = 0, Name = DefaultOptionValue});

            var typeOfRestrictions = UnitOfWork.GetSet<TypeOfRestriction>().Where(t => t.IsActive).OrderBy(p => p.Name)
                .ToList();

            var trsId = typeOfRestrictions.Select(t => t.Id).ToHashSet();
            typeOfRestrictions.AddRange(request?.Child
                ?.Where(c =>
                    c.TypeOfRestriction != null && !trsId.Contains(c.TypeOfRestriction.Id))
                .GroupBy(g => g.TypeOfRestriction.Id).Select(g =>
                    g.Select(v => v.TypeOfRestriction).FirstOrDefault())
                .Where(t => t != null).ToList() ?? new List<TypeOfRestriction>());
            typeOfRestrictions = typeOfRestrictions.InsertAt(new TypeOfRestriction {Id = 0, Name = DefaultOptionValue})
                .ToList();

            ViewBag.TypeOfRestriction = typeOfRestrictions;

            ViewBag.TypeOfRestrictionSubs =
                typeOfRestrictions.Where(t => t.Subs != null && t.Subs.Any()).Select(t => t.Id).ToArray();

            ViewBag.ApplicantType =
                VocController.GetApplicantType()
                    .OrderBy(p => p.Id)
                    .InsertAt(new ApplicantType {Id = 0, Name = DefaultOptionValue});

            ViewBag.Districts =
                DistrictController.Get()
                    .InsertAt(new BtiDistrict
                    {
                        Id = 0,
                        Name = DefaultOptionValue
                    });

            ViewBag.Regions =
                VocController.GetRegions()
                    .OrderBy(p => p.Id)
                    .InsertAt(new BtiRegion
                    {
                        Id = 0,
                        Name = DefaultOptionValue
                    });

            ViewBag.Sources =
                VocController.GetSources()
                    .OrderBy(p => p.Id)
                    .InsertAt(new Source
                    {
                        Id = 0,
                        Name = DefaultOptionValue
                    });

            ViewBag.TypeViolations =
                UnitOfWork.GetSet<TypeViolation>().Where(t => !t.YearOfRestId.HasValue)
                    .InsertAt(new TypeViolation {Id = 0, Name = DefaultOptionValue}).ToList();

            ViewBag.DeclineReasons = UnitOfWork.GetSet<DeclineReason>()
                .Where(t => t.IsActive || t.Id == (long) DeclineReasonEnum.CertificateIssued)
                .InsertAt(new DeclineReason {Id = 0, Name = DefaultOptionValue}).ToList();

            if (request != null)
            {
                long requestId;
                if (request.Id == 0 && request.TypeOfRestId == (long) TypeOfRestEnum.CommercicalAddonRequest)
                {
                    requestId = request.ParentRequestId ?? 0;
                }
                else
                {
                    requestId = request.Id;
                }

                var addonServiceses = requestId != 0
                    ? ApiController.GetAddonServicesForRequest(requestId)
                    : new List<AddonServices>();
                ViewBag.AddonServices = addonServiceses;
                var addonServicesForDropDown = addonServiceses.Where(s => !(s.Requared ?? false));
                if (request.StatusId != (long) StatusEnum.Draft)
                {
                    addonServicesForDropDown = addonServicesForDropDown.Where(s => !s.OnlyWithRequest);
                }

                ViewBag.AddonServicesForDropDown = addonServicesForDropDown.ToList();
            }
        }

        /// <summary>
        ///     заполнение справочников в ViewModel
        /// </summary>
        /// <param name="model"></param>
        private void FillVocsInVm(RequestViewModel model)
        {
            if (model == null)
            {
                return;
            }

            model.BenefitTypes = VocController.GetBenefitTypeInternal();
            model.TypeOfRestrictions = UnitOfWork.GetSet<TypeOfRestriction>().ToList();

            FillTypeOfRestInModel(model);

            model.Beneficiaries = UnitOfWork.GetSet<Beneficiaries>().Where(b => b.Id != (long) BeneficiariesEnum.Child)
                .ToList();
            model.TypeInformationVoucher =
                UnitOfWork.GetSet<TypeRequestInformationVoucher>().OrderBy(t => t.Id).ToList();
            model.TimeOfRests = VocController.GetTimesOfRestWithoutFilter();

            model.TypeOfRestBenefitRestrictions = VocController.GetTypeOfRestBenefitRestrictions();
            model.RequestCurrentPeriod = VocController.GetRequestCurrentPeriods();
            model.PlacesOfRest = VocController.GetPlacesOfRestInternal(true);
            if (model.Data.TourId.HasValue && model.Data.Tour == null)
            {
                model.Data.Tour = UnitOfWork.GetById<Tour>(model.Data.TourId.Value);
            }

            model.StatusByChild = VocController.GetStatusByChild()
                .InsertAt(new StatusByChild {Id = 0, Name = DefaultOptionValue});

            model.DeclineReason =
                VocController.GetDeclineReason()
                    .Where(d => d.IsManual && d.FirstStage && d.IsActive)
                    .Where(d => d.TypeOfRests.Any(t => t.Id == model.Data.TypeOfRestId))
                    .OrderBy(d => d.Id)
                    .InsertAt(new DeclineReason {Id = 0, Name = DefaultOptionValue});

            model.TypesOfTransportInRequest =
                UnitOfWork.GetSet<TypeOfTransportInRequest>().Where(t => t.IsActive).ToList();

            model.TypesOfCamp =
                UnitOfWork.GetSet<TypeOfCamp>().Where(t => t.IsActive).ToList();

            model.TypesOfRestRequiringTransportSelection = UnitOfWork.GetSet<TypeOfRest>()
                .Where(t => t.IsActive && t.NeedTypeOfTransport).Select(t => t.Id).ToArray();

            model.PlacesOfRestRequiringTransportSelection = UnitOfWork.GetSet<PlaceOfRest>()
                .Where(t => t.IsActive && t.NeedTypeOfTransport).Select(t => t.Id).ToArray();

            // заполнение проверок по выплатам и льготам
            foreach (var child in model.Child)
            {
                child.StatusByChild = model.StatusByChild;
                child.LowIncomeTypes = WebExchangeController.NeedCheckPayment(child.Data)
                    ? new[] {Settings.Default.LowIncomeType}
                    : null;
            }

            if (model.Location != null)
            {
                model.AddonPlacesCount = 0;
                var typeOfRoomIds = model.Location.Select(l => l.RoomId);
                var typeOfRooms = UnitOfWork.GetSet<TypeOfRooms>().Where(r => typeOfRoomIds.Contains(r.Id)).ToList();
                foreach (var type in typeOfRooms)
                {
                    model.AddonPlacesCount += type.CountAddonPlace;
                }
            }
        }

        /// <summary>
        ///     заполнение целей обращения для заявления
        /// </summary>
        private void FillTypeOfRestInModel(RequestViewModel model)
        {
            model.TypeOfRestsAll = VocController.GetTypesOfRest(false);
            model.TypeOfRests = model.TypeOfRestsAll.Where(t => t.IsManualVisible()).ToList();
            var dict = UnitOfWork.GetSet<TypeOfRest>()
                .ToList()
                .ToDictionary(t => t.Id,
                    t => new TypeOfRest(t) {Parent = t.Parent != null ? new TypeOfRest(t.Parent) : null});
            var appayed = true;
            while (appayed)
            {
                appayed = false;
                foreach (var tr in model.TypeOfRests)
                {
                    if (tr.Parent?.ParentId.HasValue ?? false)
                    {
                        var it = dict.ContainsKey(tr.Parent?.ParentId ?? 0) ? dict[tr.Parent?.ParentId ?? 0] : null;
                        if (it != null)
                        {
                            appayed = true;
                            tr.Parent = it;
                            tr.ParentId = it.Id;
                        }
                    }
                }
            }

            if (model.TypeOfRests.All(t => t.Id != model.Data.TypeOfRestId) && model.Data.TypeOfRest != null)
            {
                model.TypeOfRests.Add(model.Data.TypeOfRest);
            }

            if (!model.Data.TypeOfRestId.HasValue)
            {
                model.Data.TypeOfRestId = model.TypeOfRests.FirstOrDefault()?.Id;
            }
        }

        /// <summary>
        ///     Сохранение заявки
        /// </summary>
        /// <param name="requestModel"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        [ValidateInput(false)]
        public ActionResult SaveRequest(RequestViewModel requestModel, string action)
        {
            try
            {
                SetUnitOfWorkInRefClass(UnitOfWork);

                if (requestModel == null)
                {
                    throw new ArgumentNullException(nameof(requestModel));
                }

                if (requestModel.Data == null)
                {
                    throw new ArgumentNullException(nameof(requestModel) + ".Data");
                }

                var checks = Security.GetSecurity().ToArray();

                if (!checks.Contains(AccessRightEnum.RequestManage))
                {
                    return RedirectToAction("RequestEdit",
                        new {id = requestModel.Data.Id, needValidate = !requestModel.IsValid, saveaction = action});
                }

                //RestChild.Security.Logger.SecurityLogger.AddToLogProcess(UnitOfWork, (requestModel.Data?.Id > 0 ? $"Изменение заявления ({requestModel.Data?.Id})" : "Создание нового заявляения"), Security.GetCurrentAccountId().Value);

                if (UnitOfWork.GetLastUpdateTickById<Request>(requestModel.Data?.Id) !=
                    requestModel.Data?.LastUpdateTick)
                {
                    SetRedicted();
                    return RedirectToAction("RequestEdit",
                        new {id = requestModel.Data.Id});
                }

                // изменил LUT
                var entityLut = UnitOfWork.GetById<Request>(requestModel.Data?.Id);

                // переход на форму выбора места (без сохранения)
                if (action == AccessRightEnum.Status.FcToDecisionIsMade && entityLut != null &&
                    entityLut.IsFirstCompany &&
                    (entityLut.StatusId == (long) StatusEnum.DecisionMaking ||
                     entityLut.StatusId == (long) StatusEnum.DecisionMakingCovid))
                {
                    entityLut.UpdateRequestInBooking();
                    return RedirectToAction("SelectPlace", "FirstRequestCompany", new {id = entityLut.Id});
                }

                if (entityLut != null)
                {
                    entityLut.LastUpdateTick = DateTime.Now.Ticks;
                    UnitOfWork.SaveChanges();
                    UnitOfWork.Context.Entry(entityLut).State = EntityState.Detached;
                }

                //адский костыль по добавлению БТИ адреса
                if (requestModel.Child != null)
                {
                    var chd = requestModel.Child.ToArray();
                    for (var i = 0; i < chd.Length; i++)
                    {
                        if (chd[i].Address != null && chd[i].Address.Data != null)
                        {
                            var adr = chd[i].Address;
                            if (!string.IsNullOrWhiteSpace(adr.Data.District))
                            {
                                chd[i].Address.Data.BtiRegionId = UnitOfWork.GetSet<BtiRegion>()
                                    .Where(ss => ss.Name.ToLower() == adr.Data.District.ToLower()).Select(ss => ss.Id)
                                    .FirstOrDefault();
                            }

                            if (!string.IsNullOrWhiteSpace(adr.Data.Region))
                            {
                                chd[i].Address.Data.BtiDistrictId = UnitOfWork.GetSet<BtiDistrict>()
                                    .Where(ss => ss.Name.ToLower() == adr.Data.Region.ToLower()).Select(ss => ss.Id)
                                    .FirstOrDefault();
                            }
                        }
                    }

                    requestModel.Child = chd.ToList();
                }

                if (requestModel.Applicant != null)
                {
                    if (!string.IsNullOrWhiteSpace(requestModel.Applicant?.Address?.Data?.District))
                    {
                        requestModel.Applicant.Address.Data.BtiRegionId = UnitOfWork.GetSet<BtiRegion>()
                            .Where(ss => ss.Name.ToLower() == requestModel.Applicant.Address.Data.District.ToLower()).Select(ss => ss.Id)
                            .FirstOrDefault();
                    }
                    if (!string.IsNullOrWhiteSpace(requestModel.Applicant?.Address?.Data?.Region))
                    {
                        requestModel.Applicant.Address.Data.BtiDistrictId = UnitOfWork.GetSet<BtiDistrict>()
                            .Where(ss => ss.Name.ToLower() == requestModel.Applicant.Address.Data.Region.ToLower()).Select(ss => ss.Id)
                            .FirstOrDefault();
                    }
                }


                var request = requestModel.BuildData();

                if (request.TypeOfRestId == (long) TypeOfRestEnum.YouthRestCamps ||
                    request.TypeOfRestId == (long) TypeOfRestEnum.YouthRestOrphanCamps)
                {
                    if (request.Applicant != null)
                    {
                        request.Applicant.IsAccomp = true;
                    }

                    if (request.CountAttendants == 0 && request.CountPlace == 0)
                    {
                        request.CountAttendants = 1;
                    }
                }

                request.LastUpdateTick = DateTime.Now.Ticks;
                if (action == RequestActionEnum.RemoveDraftVersion)
                {
                    var res = ApiController.RemoveDraftVersion(request.Id);

                    return res.HasValue
                        ? RedirectToAction("RequestEdit", new {id = res.Value})
                        : RedirectToAction("RequestList");
                }

                if (request.Id == 0 && requestModel.BookingRequest != null)
                {
                    try
                    {
                        var client = Booking.Logic.Booking.GetServiceClient(requestModel.BookingRequest);
                        try
                        {
                            var bookingResult = client.MakeBooking(requestModel.BookingRequest);
                            if (bookingResult.IsError)
                            {
                                return RedirectToAction("SelectPlace", new {errorMesage = bookingResult.ErrorMessage});
                            }
                        }
                        finally
                        {
                            Booking.Logic.Booking.CloseClient(client);
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Error("Ошибка бронирования", ex);
                        return RedirectToAction("SelectPlace", new {errorMesage = "Ошибка бронирования"});
                    }
                }
                else if (request.Id == 0 && requestModel.BookingRequest == null)
                {
                    // если нет бронирование и не компенсация то это двух эиапная кампания
                    request.IsFirstCompany = request.TypeOfRestId != (long) TypeOfRestEnum.Compensation &&
                                             request.TypeOfRestId != (long) TypeOfRestEnum.CompensationYouthRest;
                }

                bool processed;

                using (var tran = UnitOfWork.GetTransactionScope())
                {
                    request = request.ResetZeroFk();
                    request.Agent.ResetZeroFk();
                    if (request.Applicant.IndexField == 0)
                    {
                        request.Applicant.IndexField = request.Applicant.Id;
                    }

                    request.Applicant.ResetZeroFk();
                    request.Child = request.Child ?? new List<Child>();
                    request.Attendant = request.Attendant ?? new List<Applicant>();

                    foreach (var child in request.Child)
                    {
                        if (child.IndexField == 0)
                        {
                            child.IndexField = child.Id;
                        }

                        child.ResetZeroFk();
                    }

                    foreach (var attendant in request.Attendant)
                    {
                        if (attendant.IndexField == 0)
                        {
                            attendant.IndexField = attendant.Id;
                        }

                        attendant.ResetZeroFk();
                    }

                    long? declineReason = null;

                    if (request.DeclineReasonId.HasValue)
                    {
                        declineReason = request.DeclineReasonId;
                    }

                    SetupChilds(request);

                    SetupAttendants(request);


                    request = ApiController.SaveRequest(request, action == RequestActionEnum.EditAction,
                        requestModel.SaveFileOnly ?? false);

                    UnitOfWork.SaveChanges();

                    request = UnitOfWork.GetById<Request>(request.Id);

                    var model = new RequestViewModel(request);

                    if (string.IsNullOrWhiteSpace(action))
                    {
                        tran.Complete();
                        return RedirectToAction("RequestEdit", new { id = request.Id, needCompeteionAlert = true });
                    }

                    requestModel.Data.TypeOfRest = UnitOfWork.GetById<TypeOfRest>(requestModel.Data.TypeOfRestId ?? 0);

                    FillVocsInVm(model);

                    var requestDeclineNotApproved =
                        Settings.Default.RequestDeclineNotApproved?.Cast<string>()
                            .Select(v => v.LongParse())
                            .Where(l => l.HasValue)
                            .ToArray() ?? new long?[0];

                    if ((action == AccessRightEnum.Status.FcToCancelByRequest ||
                         action == AccessRightEnum.Status.ToCancelByApplicant) &&
                        requestDeclineNotApproved.Contains(declineReason))
                    {
                        UnitOfWork.SendChangeStatusByEvent(request, RequestEventEnum.RequestDeclineNotApproved);
                        UnitOfWork.WriteHistory(request.Id,
                            "Неуважительная причина отказа от путевки. Отказ не одобрен.",
                            Security.GetCurrentAccountId());
                        tran.Complete();
                        return RedirectToAction("RequestEdit", "FirstRequestCompany", new {id = request.Id});
                    }

                    if (action == AccessRightEnum.Status.FcFinishWorkWithRequest)
                    {
                        request.MayFinalSend = false;
                    }

                    if (action == AccessRightEnum.Status.FcRepareRequest)
                    {
                        request.Repared = true;
                        request.DeclineReasonId = null;
                        request.TourId = null;
                        request.HotelsId = null;
                        request.BookingGuid = null;
                        declineReason = null;
                        UnitOfWork.SaveChanges();
                    }

                    processed = ChangeStateOnSaveRequest(request, model, action, declineReason);

                    UnitOfWork.SaveChanges();
                    tran.Complete();
                }

                // обновление заявления
                request = UnitOfWork.GetById<Request>(request.Id);

                if (request.StatusId == (long)StatusEnum.RegistrationDecline)
                {
                    var model = new RequestViewModel(request);
                    ApiController.CheckApplicant(model);
                    ApiController.CheckChildren(model);
                    ApiController.CheckAttendants(model);
                    SetDeclineReason(model);
                    ApiController.SaveRequestDeclineStatus(model, request);

                    UnitOfWork.SaveChanges();
                }
                request.UpdateRequestInBooking();
                return RedirectToAction("RequestEdit",
                    new {id = request.Id, needValidate = !processed, saveaction = processed ? string.Empty : action});
            }
            catch (DbUpdateConcurrencyException)
            {
                SetRedicted();
                return RedirectToAction("RequestEdit", new {id = requestModel.Data.Id, needValidate = false});
            }
        }

        public void SetDeclineReason(RequestViewModel model)
        {

            if (model.SameChildren?.Any()??false)
            {
                model.Data.DeclineReason = UnitOfWork.GetById<DeclineReason>(202107);
            }
            if ((model.SameChildren?.Any()??false) && model.Data.TypeOfRestId == (long?)TypeOfRestEnum.YouthRestOrphanCamps)
            {
                model.Data.DeclineReason = UnitOfWork.GetById<DeclineReason>(202104);
            }
            if ((model.ApplicantDouble?.Any()??false) && model.Data.TypeOfRestId == (long?)TypeOfRestEnum.RestWithParentsPoor)
            {
                model.Data.DeclineReason = UnitOfWork.GetById<DeclineReason>(202105);
            }
            if ((model.ApplicantDouble?.Any() ?? false) && model.Data.TypeOfRestId == (long?)TypeOfRestEnum.MoneyOn3To7)
            {
                model.Data.DeclineReason = UnitOfWork.GetById<DeclineReason>(202106);
            }
            if ((model.SameAttendants?.Any() ?? false) && model.Data.TypeOfRestId == (long?)TypeOfRestEnum.RestWithParentsPoor)
            {
                model.Data.DeclineReason = UnitOfWork.GetById<DeclineReason>(202102);
            }
            if ((model.SameAttendants?.Any()??false) && model.Data.TypeOfRestId == (long?)TypeOfRestEnum.MoneyOn3To7)
            {
                model.Data.DeclineReason = UnitOfWork.GetById<DeclineReason>(202103);
            }

        }

        /// <summary>
        ///     изменение статуса при сохранении заявки
        /// </summary>
        private bool ChangeStateOnSaveRequest(Request request, RequestViewModel model, string action,
            long? declineReason)
        {
            var processed = false;
            switch (action)
            {
                // отправить запрос в БР повторно
                case AccessRightEnum.RetryRequestInBaseRegistry:
                    if (model.Data.StatusId != (long) StatusEnum.Draft && model.CheckModel(action))
                    {
                        ApiController.CheckChildren(model);
                        ApiController.CheckAttendants(model);
                        ApiController.ValidOnInSameTime(model);

                        if (
                            (model.SameAttendantSnils == null || !model.SameAttendantSnils.Any()) &&
                            (model.SameAttendants == null || !model.SameAttendants.Any()) &&
                            (model.SameChildren == null || !model.SameChildren.Any()) &&
                            (model.CrossTimeAttendants == null || !model.CrossTimeAttendants.Any()) &&
                            (model.CrossTimeChilds == null || !model.CrossTimeChilds.Any()))
                        {
                            request.NeedSendForBenefit = true;
                            request.NeedSendForSnils = true;
                            request.NeedSendForPassport = true;
                            request.NeedSendForRegistrationByPassport = true;
                            request.NeedSendForAisoLegalRepresentation = true;
                            request.NeedSendToRelative = true;
                            request.NeedSendForCPMPK = true;
                            request.NeedSendForFRI = ResolveNeedSendForFRI(request);
                            UnitOfWork.SaveChanges();
                        }
                    }

                    break;
                // зарегистрировать заявление
                case AccessRightEnum.Status.ToSend:
                    if (model.CheckModel(action))
                    {
                        model = new RequestViewModel(request);
                        ApiController.CheckApplicant(model);
                        ApiController.CheckChildren(model);
                        ApiController.CheckAttendants(model);

                        if (model.SameChildren.Any())
                        {
                            //ApiController.RequestChangeStatusInternal(AccessRightEnum.Status.ToReject, request,
                            //	request.TypeOfRestId == (long) TypeOfRestEnum.Compensation
                            //		? DeclineSectionProcess.GetDeclineReason("CompensationCrossChild", request.TypeOfRestId) ??
                            //		  Settings.Default.CompensationCrossChild
                            //		: DeclineSectionProcess.GetDeclineReason("CrossChild", request.TypeOfRestId) ?? Settings.Default.CrossChild,
                            //	false);

                            UnitOfWork.RequestChangeStatusInternal(AccessRightEnum.Status.ToRegistrationDecline,
                                request, 202107,
                                false);
                            break;
                        }

                        if (model.ApplicantDouble.Any())
                        {
                            if (model.Data.TypeOfRestId == (long)TypeOfRestEnum.RestWithParentsPoor)
                                UnitOfWork.RequestChangeStatusInternal(
                                    AccessRightEnum.Status.ToRegistrationDecline,
                                    request, 202105, false);
                            if (model.Data.TypeOfRestId == (long)TypeOfRestEnum.MoneyOn3To7)
                                UnitOfWork.RequestChangeStatusInternal(
                                    AccessRightEnum.Status.ToRegistrationDecline,
                                    request, 202106, false);
                            if (model.Data.TypeOfRestId == (long)TypeOfRestEnum.YouthRestCamps ||
                                model.Data.TypeOfRestId == (long)TypeOfRestEnum.YouthRestOrphanCamps)
                                UnitOfWork.RequestChangeStatusInternal(
                                    AccessRightEnum.Status.ToRegistrationDecline,
                                    request, 202104, false);
                            break;
                        }

                        if (model.SameAttendantSnils.Any() || model.SameAttendants.Any())
                        {
                            if (model.Data.TypeOfRestId == (long)TypeOfRestEnum.RestWithParentsPoor)
                                UnitOfWork.RequestChangeStatusInternal(
                                    AccessRightEnum.Status.ToRegistrationDeclineAttendant,
                                    request, 202102, false);
                            if (model.Data.TypeOfRestId == (long)TypeOfRestEnum.MoneyOn3To7)
                                UnitOfWork.RequestChangeStatusInternal(
                                    AccessRightEnum.Status.ToRegistrationDeclineAttendant,
                                    request, 202103, false);
                            break;
                        }

                        request = UnitOfWork.RequestChangeStatusInternal(action, request);

                        if (!ApiController.ValidOnInSameTime(model))
                        {
                            UnitOfWork.RequestChangeStatusInternal(AccessRightEnum.Status.ToReject, request,
                                DeclineSectionProcess.GetDeclineReason("CrossTime", request.TypeOfRestId) ??
                                Settings.Default.CrossTime, false);
                            break;
                        }

                        if (request.TypeOfRestId == (long) TypeOfRestEnum.Compensation ||
                            request.TypeOfRestId == (long) TypeOfRestEnum.CompensationYouthRest ||
                            request.TypeOfRestId == (long) TypeOfRestEnum.RestWithParentsOther)
                        {
                            request = UnitOfWork.RequestChangeStatusInternal(AccessRightEnum.Status.EditInWaitApplicant,
                                request, null,
                                false);
                        }

                        if (!request.IsFirstCompany)
                        {
                            request.NeedSendForBenefit = true;
                            request.NeedSendForSnils = true;
                            request.NeedSendForPassport = true;
                            request.NeedSendToRelative = true;
                            request.NeedSendForCPMPK = true;
                            request.NeedSendForRegistrationByPassport = true;
                            request.NeedSendForAisoLegalRepresentation = true;
                            request.NeedSendForFRI = ResolveNeedSendForFRI(request);
                        }
                        else
                        {
                            model.TypeOfRestsAll = VocController.GetTypesOfRest(false);
                            if (!ApiController.BadPersonInRequest(model))
                            {
                                ExchangeController.RejectRequestAsBadPerson(model, request);
                            }
                            else
                            {
                                request.NeedSendForBenefit = true;
                                request.NeedSendForSnils = true;
                                request.NeedSendForPassport = true;
                                request.NeedSendToRelative = true;
                                request.NeedSendForCPMPK = true;
                                request.NeedSendForRegistrationByPassport = true;
                                request.NeedSendForAisoLegalRepresentation = true;
                                request.NeedSendForFRI = ResolveNeedSendForFRI(request);
                            }
                        }

                        UnitOfWork.SaveChanges();
                        processed = true;
                    }

                    break;
                default:
                    processed = ApiController.RequestChangeStatus(request.Id, action, declineReason);
                    break;
            }

            return processed;
        }

        /// <summary>
        ///     Определить есть ли в заявлении персоны с инвалидностью
        /// </summary>
        private bool ResolveNeedSendForFRI(Request request)
        {
            return request.Applicant.IsInvalid || request.Child.Any(c => c.IsInvalid);
        }

        private static void SetupAttendants(Request request)
        {
            foreach (var applicant in request.Attendant)
            {
                if (applicant.IndexField == 0)
                {
                    applicant.IndexField = applicant.Id;
                }

                if (applicant.Id < 0)
                {
                    applicant.Id = 0;
                }
            }
        }

        private static void SetupChilds(Request request)
        {
            foreach (var child in request.Child)
            {
                if (child.Id != 0)
                {
                    child.IndexField = child.Id;
                }

                if (child.Id < 0)
                {
                    child.Id = 0;
                }

                child.Address?.ResetZeroFk();
            }
        }

        public ActionResult CheckRequestInBaseRegistry(long requestId)
        {
            var req = UnitOfWork.GetById<Request>(requestId);
            req.NeedSendForBenefit = true;
            req.NeedSendForSnils = true;
            req.NeedSendForPassport = true;
            UnitOfWork.SaveChanges();

            return RedirectToAction("RequestEdit", new {id = requestId});
        }

        /// <summary>
        ///     поиск заявлений
        /// </summary>
        public ActionResult RequestList(RequestFilterModel model)
        {
            if (!Security.HasAnyRightsForSomeOrganization(new[] {AccessRightEnum.RequestView}))
            {
                return RedirectToAvalibleAction();
            }

            SetUnitOfWorkInRefClass(UnitOfWork);

            PrepareVocabulary(null, model);
            model = model ?? new RequestFilterModel();

            if (model.PageSize == 0 || model.PageNumber == 0)
            {
                model.PageSize = 10;
                model.PageNumber = 1;
                model.MoreThenSelectedYear = true;
            }

            model.ListOfYears = UnitOfWork.GetSet<YearOfRest>().OrderBy(y => y.Year).ToList();

            if (model.YearOfRestId == 0)
            {
                var curYear =
                    model.ListOfYears.LastOrDefault(
                        y => y.DateFirstStage <= DateTime.Now && y.Year >= DateTime.Now.Year) ??
                    model.ListOfYears.FirstOrDefault(y => y.Year == DateTime.Now.Year) ??
                    model.ListOfYears.LastOrDefault();
                if (curYear != null)
                {
                    model.YearOfRestId = curYear.Id;
                }
            }

            if (model.HotelsId.HasValue)
            {
                model.Hotels = UnitOfWork.GetById<Hotels>(model.HotelsId.Value);
            }

            return View(ApiController.RequestList(model));
        }

        public ActionResult PreRegisterList(RequestFilterModel model)
        {
            if (!Security.HasAnyRightsForSomeOrganization(new[] {AccessRightEnum.RequestView}))
            {
                return RedirectToAvalibleAction();
            }

            SetUnitOfWorkInRefClass(UnitOfWork);

            PrepareVocabulary(null, model);
            model = model ?? new RequestFilterModel();

            if (model.PageSize == 0 || model.PageNumber == 0)
            {
                model.PageSize = 10;
                model.PageNumber = 1;
            }

            model.ListOfYears = UnitOfWork.GetSet<YearOfRest>().OrderBy(y => y.Year).ToList();

            if (model.YearOfRestId == 0)
            {
                var curYear = model.ListOfYears.FirstOrDefault(y => y.Year == DateTime.Now.Year) ??
                              model.ListOfYears.LastOrDefault();
                if (curYear != null)
                {
                    model.YearOfRestId = curYear.Id;
                }
            }

            model.SourceSetted = true;

            if (model.HotelsId.HasValue)
            {
                model.Hotels = UnitOfWork.GetById<Hotels>(model.HotelsId.Value);
            }

            model.AggregatedStatusOfRequest = (long) StatusEnum.CertificateIssued;

            return View(ApiController.RequestList(model));
        }

        public ActionResult PreRegisterList2(RestManFilterModel filterModel)
        {
            if (!Security.HasAnyRightsForSomeOrganization(new[] {AccessRightEnum.RequestView}))
            {
                return RedirectToAvalibleAction();
            }

            filterModel = filterModel ?? new RestManFilterModel();

            SetUnitOfWorkInRefClass(UnitOfWork);

            PrepareVocabulary(null, filterModel);

            if (filterModel.PageSize == 0 || filterModel.PageNumber == 0)
            {
                filterModel.PageSize = 10;
                filterModel.PageNumber = 1;
            }

            filterModel.ListOfYears = UnitOfWork.GetSet<YearOfRest>().OrderBy(y => y.Year).ToList();

            if (filterModel.YearOfRestId == 0)
            {
                var curYear = filterModel.ListOfYears.FirstOrDefault(y => y.Year == DateTime.Now.Year) ??
                              filterModel.ListOfYears.LastOrDefault();
                if (curYear != null)
                {
                    filterModel.YearOfRestId = curYear.Id;
                }

                filterModel.MoreThenSelectedYear = true;
            }

            filterModel.SourceSetted = true;

            if (filterModel.HotelsId.HasValue)
            {
                filterModel.Hotels = UnitOfWork.GetById<Hotels>(filterModel.HotelsId.Value);
            }

            filterModel.AggregatedStatusOfRequest = (long) StatusEnum.CertificateIssued;

            var searchIndexResult = SearchRestManByRequest(filterModel);

            var children = searchIndexResult.ResManPage
                .Select(i =>
                {
                    var typeOfRest = i.TypeOfRestId != 0
                        ? ((IList<TypeOfRest>) ViewBag.TypesOfRest)?.FirstOrDefault(r => r.Id == i.TypeOfRestId)
                        : null;
                    return new FullIndexRestChildDto
                    {
                        IndexRestChildDto = i,
                        TypeOfRest = typeOfRest != null ? typeOfRest.Name : string.Empty,
                        ParentTypeOfRest = typeOfRest?.Parent != null ? typeOfRest.Parent.Name : string.Empty,
                        BenefitType = i.BenefitTypeId != 0
                            ? ((IList<BenefitType>) ViewBag.BenefitType)?.FirstOrDefault(r => r.Id == i.BenefitTypeId)
                            ?.Name
                            : null,
                        PlaceOfRest =
                            i.PlaceOfRestId != 0
                                ? ((IList<PlaceOfRest>) ViewBag.PlacesOfRest)
                                ?.FirstOrDefault(r => r.Id == i.PlaceOfRestId)
                                ?.Name
                                : null,
                        TimeOfRest = i.TimeOfRestId != 0
                            ? ((IList<TimeOfRest>) ViewBag.TimesOfRest)?.FirstOrDefault(r => r.Id == i.TimeOfRestId)
                            ?.Name
                            : null
                    };
                })
                .ToArray();

            filterModel.Records = new CommonPagedList<FullIndexRestChildDto>(
                children,
                filterModel.PageNumber,
                filterModel.PageSize,
                searchIndexResult.TotalRestManCount);

            filterModel.TotalRecordsCount = searchIndexResult.TotalRestManCount;
            return View(filterModel);
        }

        private SearchIndexResult SearchRestManByRequest(RequestSearchFilterModel model)
        {
            var service = ServiceFactory.GetRestChildrenService();

            var years = new List<long> {model.YearOfRestId};

            if (model.MoreThenSelectedYear)
            {
                var year = UnitOfWork.GetById<YearOfRest>(model.YearOfRestId)?.Year;

                if (year.HasValue)
                {
                    years.AddRange(UnitOfWork.GetSet<YearOfRest>().Where(y => y.Year > year).Select(y => y.Id)
                        .ToList());
                }
            }

            var searchIndexResult = service.Execute(r => r.GetChildren(new RestChildFilterDto
            {
                PageSize = model.PageSize,
                PageNumber = model.PageNumber == 0 ? 1 : model.PageNumber,
                ChildFIO = model.ChildFio,
                TypeOfRest = model.TypeOfRestId,
                TimeOfRestId = model.TimeOfRestId,
                PlaceOfRest = model.PlaceOfRestId,
                Hotel = model.HotelsId.HasValue && model.HotelsId.Value != 0
                    ? model.Hotels?.Name ?? UnitOfWork.GetById<Hotels>(model.HotelsId)?.Name
                    : string.Empty,
                HotelId = model.HotelsId,
                DistrictId = model.DistrictId,
                RegionId = model.RegionId,
                SourceId = model.SourceId,
                OperatorId = model.CreateUserId,
                BenefitApprove = model.BaseRegisterBenefitApprove,
                IsApprovedInInteragency = model.InteragencyBenefitApprove,
                AgeStart = model.AgeStart,
                AgeEnd = model.AgeEnd,
                BenefitTypeId = model.BenefitTypeId,
                TypeOfRestriction = model.TypeOfRestrictionId != 0
                    ? ((IEnumerable<TypeOfRestriction>) ViewBag.TypeOfRestriction)
                    ?.FirstOrDefault(i => i.Id == model.TypeOfRestrictionId)?.Name ?? string.Empty
                    : string.Empty,
                RequestDateSupplyStart = model.StartRequestDate,
                RequestDateSupplyEnd = model.EndRequestDate,
                YearOfRestId = model.YearOfRestId,
                YearOfRests = years.ToArray(),
                OrganizationId = model.OrganizationId ?? 0,
                VedomstvoId = model.VedomstvoId ?? 0,
                RequestNumber = model.RequestNumber,
                RestCategory = model.RestCategory,
                PaymentStatus = !model.PaymentStatus.HasValue || model.PaymentStatus == -1
                    ? (bool?) null
                    : model.PaymentStatus == 1,
                ApplicantFIO = model.ApplicantFio,
                TypeOfDecision = model.TypeOfDecision
            }));

            return searchIndexResult;
        }

        public ActionResult ExportPreRequestToExcel2(RequestFilterModel requestFilterModel)
        {
            var account = Security.GetCurrentAccountId();
            if (!account.HasValue)
            {
                return HttpNotFound();
            }

            SecurityLogger.AddToLogProcess(UnitOfWork, "Реестр отдыхающих",
                account.Value, HttpContext.Request.UserAgent);

            var maxPageSize = 1000;

            requestFilterModel.PageSize = maxPageSize;

            PrepareVocabulary(null, requestFilterModel);
            var typesOfRest = (IList<TypeOfRest>) ViewBag.TypesOfRest;
            var placesOfRest = (IList<PlaceOfRest>) ViewBag.PlacesOfRest;
            var benefits = UnitOfWork.GetAll<BenefitType>();
            var restOfSubjects = (IList<SubjectOfRest>) ViewBag.SubjectsOfRest;
            var timeOfRest = (IList<TimeOfRest>) ViewBag.TimesOfRest;

            var list = new List<string>();
            var cnt = 0;
            int total;
            requestFilterModel.PageNumber = 1;
            do
            {
                var searchIndexResult = SearchRestManByRequest(requestFilterModel);
                cnt += searchIndexResult.ResManPage.Count;
                total = searchIndexResult.TotalRestManCount;

                var childIds = searchIndexResult.ResManPage.Select(c => (long?) c.ChildId).Where(c => c > 0).ToArray();
                var brs =
                    UnitOfWork.GetSet<ExchangeBaseRegistry>()
                        .Where(
                            e =>
                                childIds.Contains(e.ChildId) && !e.NotActual && e.IsProcessed && e.Success &&
                                e.ExchangeBaseRegistryTypeId == (long) ExchangeBaseRegistryTypeEnum.Relationship)
                        .ToList();

                var dict = new Dictionary<long?, RelationshipCheckResult>();
                foreach (var br in brs)
                {
                    if (!dict.ContainsKey(br.ChildId ?? 0))
                    {
                        dict.Add(br.ChildId ?? 0, br.Parse()?.RelationshipCheckResults?.FirstOrDefault());
                    }
                }

                var data = searchIndexResult.ResManPage
                    .Select(
                        p =>
                            PreRequestViewModel.CreateExcelModel(p, typesOfRest, placesOfRest, benefits, restOfSubjects,
                                timeOfRest,
                                p.ChildId > 0 && dict.ContainsKey(p.ChildId) ? dict[p.ChildId] : null))
                    .ToArray();

                var fileName = PreRequestExcelExport.GenerateFile(data);
                list.Add(fileName);
                requestFilterModel.PageNumber += 1;
            } while (cnt < total);

            var files = UnionFilesToZip(list);

            return FileAndDeleteOnClose(files, "application/zip", "Реестр отдыхающих.zip");
        }

        /// <summary>
        ///     выгрузка в excel
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public ActionResult ExportPreRequestToExcel(RequestFilterModel filter)
        {
            UnitOfWork.Context.Configuration.LazyLoadingEnabled = false;
            UnitOfWork.Context.Configuration.ProxyCreationEnabled = false;
            UnitOfWork.AutoDetectChangesDisable();
            SetUnitOfWorkInRefClass(UnitOfWork);

            SecurityLogger.AddToLogProcess(UnitOfWork, "Реестр заявлений -> Отдыхающие",
                Security.GetCurrentAccountId() ?? 0, HttpContext.Request.UserAgent);

            filter = filter ?? new RequestFilterModel();
            PrepareVocabulary(null, filter);
            var query = ApiController.RequestListQuery(filter);
            var files = new List<string>();
            var count = query.Count();

            if (count <= 1000)
            {
                var file = ExportPreRequestToExcelFile(query);
                if (!string.IsNullOrEmpty(file))
                {
                    return FileAndDeleteOnClose(file,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "Реестр отдыхающих.xlsx");
                }

                return null;
            }

            var index = 0;
            while (index < count)
            {
                using (var uw = new UnitOfWork())
                {
                    SetUnitOfWorkInRefClass(uw);
                    var squery = ApiController.RequestListQuery(filter);
                    var fn = ExportPreRequestToExcelFile(squery.OrderBy(r => r.Id).Skip(index).Take(5000));
                    if (!string.IsNullOrWhiteSpace(fn))
                    {
                        files.Add(fn);
                    }
                }

                index = index + 5000;
            }

            var tempFile = UnionFilesToZip(files);

            return FileAndDeleteOnClose(tempFile, "application/zip", "Реестр отдыхающих.zip");
        }

        /// <summary>
        ///     свернуть все в zip
        /// </summary>
        public static string UnionFilesToZip(List<string> files, string name = "Реестр отдыхающих")
        {
            var tempFile = GetTempFileName();
            var date = DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss");
            using (var zipToOpen = new FileStream(tempFile, FileMode.Create))
            {
                using (var archive = new ZipArchive(zipToOpen, ZipArchiveMode.Create))
                {
                    var fileIndex = 0;
                    foreach (var file in files)
                    {
                        fileIndex++;
                        var readmeEntry = archive.CreateEntry($"{name}.{date}.{fileIndex}.xlsx");
                        var buffer = new byte[10 * 1024 * 1024];
                        using (var stream = readmeEntry.Open())
                        {
                            using (
                                var fileStream = new FileStream(file, FileMode.Open, FileAccess.ReadWrite,
                                    FileShare.ReadWrite, 512, FileOptions.DeleteOnClose))
                            {
                                var readed = 1;
                                while (readed > 0)
                                {
                                    readed = fileStream.Read(buffer, 0, buffer.Length);
                                    if (readed > 0)
                                    {
                                        stream.Write(buffer, 0, readed);
                                    }
                                }
                            }

                            try
                            {
                                if (System.IO.File.Exists(file))
                                {
                                    System.IO.File.Delete(file);
                                }
                            }
                            catch
                            {
                                // ignored
                            }
                        }
                    }
                }
            }

            return tempFile;
        }

        public string ExportPreRequestToExcelFile(IQueryable<Request> query)
        {
            var data = PreRequestViewModel.CreatePreRequests(query, UnitOfWork);
            foreach (var entry in UnitOfWork.Context.ChangeTracker.Entries())
            {
                entry.State = EntityState.Detached;
            }

            return PreRequestExcelExport.GenerateFile(data);
        }

        /// <summary>
        ///     обработка выбора
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private ActionResult ProcessActionInSelectPlace(SelectPlaceModel model)
        {
            if (model.ActionStep == "nextstep")
            {
                if (model.SelectPlaceStep == SelectPlaceStepEnum.ThridSelectTimeAndPlacment)
                {
                    var res = MakePreBooking(model);
                    if (res != null)
                    {
                        return res;
                    }
                }
                else if (model.SelectPlaceStep == SelectPlaceStepEnum.ThridEnterMoneyAccount)
                {
                    var request = model.Request;
                    if ((request?.TypeOfRest?.MayBeMoney ?? false) &&
                        !(request?.Child?.Any(c =>
                            Booking.Logic.Booking.BenefitNotForPayment.Contains(
                                c.BenefitType?.SameBenefitId ?? c.BenefitTypeId)) ?? false) &&
                        (request.StatusId == (long) StatusEnum.DecisionMaking ||
                         request.StatusId == (long) StatusEnum.DecisionMakingCovid) &&
                        !request.IsDeleted)
                    {
                        request.RequestOnMoney = true;
                        request.BankName = model.BankName;
                        request.BankAccount = model.BankAccount;
                        request.BankBik = model.BankBik;
                        request.BankCardNumber = model.BankCardNumber;
                        request.BankKpp = model.BankKpp;
                        request.BankLastName = model.BankLastName;
                        request.BankMiddleName = model.BankMiddleName;
                        request.BankFirstName = model.BankFirstName;
                        request.BankCorr = model.BankCorr;
                        request.BankInn = model.BankInn;
                        UnitOfWork.SaveChanges();

                        UnitOfWork.RequestChangeStatusInternal(AccessRightEnum.Status.FcToDecisionIsMade, request, null,
                            false,
                            Security.GetCurrentAccountId());

                        return RedirectToAction("RequestEdit", new {id = model.Request.Id});
                    }

                    model.SelectPlaceStep = SelectPlaceStepEnum.SecondSelectOrganization;
                }
                else if (model.SelectPlaceStep == SelectPlaceStepEnum.SecondSelectOrganization)
                {
                    if (model.CheckModel())
                    {
                        model.SelectPlaceStep = SelectPlaceStepEnum.ThridSelectTimeAndPlacment;
                    }
                }
                else if (model.SelectPlaceStep == SelectPlaceStepEnum.FirstSelectTypeTimeAndPlace)
                {
                    if (model.CheckModel())
                    {
                        model.SelectPlaceStep = SelectPlaceStepEnum.SecondSelectOrganization;
                    }
                }
            }

            if (model.ActionStep == "bank" && model.Request != null &&
                (model.Request.TypeOfRest?.MayBeMoney ?? false) &&
                !model.Request.Child.Any(c =>
                    Booking.Logic.Booking.BenefitNotForPayment.Contains(
                        c.BenefitType?.SameBenefitId ?? c.BenefitTypeId)))
            {
                model.SelectPlaceStep = SelectPlaceStepEnum.ThridEnterMoneyAccount;
                if (model.Request != null)
                {
                    if (model.Request.Agent != null && (model.Request.AgentApplicant ?? false))
                    {
                        model.BankFirstName = model.Request.Agent.FirstName;
                        model.BankLastName = model.Request.Agent.LastName;
                        model.BankMiddleName = model.Request.Agent.MiddleName;
                    }
                    else if (model.Request.Applicant != null)
                    {
                        model.BankFirstName = model.Request.Applicant.FirstName;
                        model.BankLastName = model.Request.Applicant.LastName;
                        model.BankMiddleName = model.Request.Applicant.MiddleName;
                    }
                }
            }

            if (model.ActionStep == "prevstep")
            {
                if (model.SelectPlaceStep == SelectPlaceStepEnum.ThridEnterMoneyAccount)
                {
                    model.SelectPlaceStep = SelectPlaceStepEnum.SecondSelectOrganization;
                }
                else if (model.SelectPlaceStep == SelectPlaceStepEnum.ThridSelectTimeAndPlacment)
                {
                    model.SelectPlaceStep = SelectPlaceStepEnum.SecondSelectOrganization;
                }
                else if (model.SelectPlaceStep == SelectPlaceStepEnum.SecondSelectOrganization)
                {
                    model.SelectPlaceStep = SelectPlaceStepEnum.FirstSelectTypeTimeAndPlace;
                }
            }

            if (model.ActionStep == "initial")
            {
                model.SelectPlaceStep = SelectPlaceStepEnum.FirstSelectTypeTimeAndPlace;
            }

            model.ActionStep = string.Empty;

            if (model.SelectPlaceStep == SelectPlaceStepEnum.FirstSelectTypeTimeAndPlace && model.Request != null)
            {
                model.SelectPlaceStep = SelectPlaceStepEnum.SecondSelectOrganization;
            }

            return null;
        }

        /// <summary>
        ///     форма поиска места.
        /// </summary>
        public ActionResult SelectPlace(SelectPlaceModel model, long? id = null)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);

            ModelState.Clear();

            var requestId = model?.RequestId ?? id;
            var request = UnitOfWork.GetById<Request>(requestId);

            model = model ?? new SelectPlaceModel();

            if (request != null)
            {
                model.Request = request;
                model.TypeOfRestId = request.TypeOfRestId ?? model.TypeOfRestId;
                // если решение уже принято оп заявлению то все переходим на список
                if (!request.IsFirstCompany || request.IsDeleted ||
                    request.StatusId != (long) StatusEnum.DecisionMaking
                    && request.StatusId != (long) StatusEnum.DecisionMakingCovid)
                {
                    return RedirectToAction("RequestEdit", new {id = requestId});
                }
            }

            SelectPlaceFillVocs(model);

            var action = ProcessActionInSelectPlace(model);
            if (action != null)
            {
                return action;
            }

            if (model.SelectPlaceStep == SelectPlaceStepEnum.ThridSelectTimeAndPlacment)
            {
                SelectPlaceFillThrid(model);
            }

            if (model.SelectPlaceStep == SelectPlaceStepEnum.SecondSelectOrganization)
            {
                SelectPlaceFillSecond(model);
            }

            if (model.SelectPlaceStep == SelectPlaceStepEnum.FirstSelectTypeTimeAndPlace)
            {
                SelectPlaceFillFirst(model);
            }

            return View(model);
        }

        /// <summary>
        ///     выполнить бронирование внутри системы
        /// </summary>
        private ActionResult MakePreBooking(SelectPlaceModel model)
        {
            var item = model.SearchResult.Hotels.FirstOrDefault(h => h.Key == model.HotelKey);
            if (item == null || !item.TimeOfRests.Any() || !model.IndexPlacement.HasValue || !model.TourId.HasValue)
            {
                model.SelectPlaceStep = SelectPlaceStepEnum.SecondSelectOrganization;
                model.IsError = true;
                model.ErrorMessage += "Не выбрано место отдыха\n";
                return null;
            }

            var typeOfRest = model.TypeOfRests.FirstOrDefault(t => t.Id == model.TypeOfRestId) ??
                             model.Request?.TypeOfRest;
            if (typeOfRest == null)
            {
                model.SelectPlaceStep = SelectPlaceStepEnum.FirstSelectTypeTimeAndPlace;
                model.IsError = true;
                model.ErrorMessage += "Не найден вид отдыха\n";
                return null;
            }

            var booking = new BookingRequest
            {
                Attendants = model.CountAttendant,
                TypeOfRestId = model.TypeOfRestId,
                TourId = model.TourId.Value,
                Places = model.CountChildren,
                DocumentNumber = model.Request?.RequestNumber
            };

            if (typeOfRest.NeedAccomodation)
            {
                var place = model.Placement.Locations[model.IndexPlacement.Value];
                booking.Rooms = place.Places.Select(p => new LocationRequest {Count = p.CountRooms, RoomId = p.Id})
                    .ToList();
            }

            var client = Booking.Logic.Booking.GetServiceClient(booking);
            try
            {
                var result = client.MakePreBooking(booking);
                if (result.BookingGuid.HasValue)
                {
                    if (model.Request == null)
                    {
                        return RedirectToAction("RequestEdit",
                            new
                            {
                                id = (int?) null,
                                typeOfRestId = booking.TypeOfRestId,
                                tourId = booking.TourId,
                                bookingGuid = result.BookingGuid
                            });
                    }
                    else
                    {
                        // бронирование и переход на заявление с выбранным местом отдыха
                        booking.BookingGuid = result.BookingGuid;
                        var lastRest = client.MakeBookingWithAccount(booking, Security.GetCurrentAccountId());
                        if (!lastRest.IsError)
                        {
                            return RedirectToAction("RequestEdit", new {id = model.Request.Id});
                        }
                        else
                        {
                            model.ErrorMessage += $"{lastRest.ErrorMessage}\n";
                            model.IsError = true;
                        }
                    }
                }
                else
                {
                    model.ErrorMessage += $"{result.ErrorMessage}\n";
                    model.IsError = true;
                }
            }
            finally
            {
                Booking.Logic.Booking.CloseClient(client);
            }

            model.IsError = true;
            model.ErrorMessage += "Закончились места в выбранное место отдыха, на выбранный период\n";
            return null;
        }

        private static void SelectPlaceFillThrid(SelectPlaceModel model)
        {
            var item = model.SearchResult.Hotels.FirstOrDefault(h => h.Key == model.HotelKey);
            if (item == null || !item.TimeOfRests.Any())
            {
                model.SelectPlaceStep = SelectPlaceStepEnum.SecondSelectOrganization;
                model.IsError = true;
                model.ErrorMessage += "Не выбрано место отдыха\n";
                return;
            }

            var typeOfRest = model.TypeOfRests.FirstOrDefault(t => t.Id == model.TypeOfRestId);
            if (typeOfRest == null)
            {
                model.SelectPlaceStep = SelectPlaceStepEnum.FirstSelectTypeTimeAndPlace;
                model.IsError = true;
                model.ErrorMessage += "Не найден вид отдыха\n";
                return;
            }

            if (typeOfRest.NeedAccomodation)
            {
                model.TourId = model.TourId ?? item.TimeOfRests.First().Id;
                var bs = new BookingVariationPlacementRequest
                {
                    TypeOfRestId = model.TypeOfRestId,
                    Attendants = model.CountAttendant,
                    Places = model.CountChildren,
                    HotelKey = item.Key,
                    TourId = model.TourId ?? 0,
                    WithBookingDate = !Security.HasRight(AccessRightEnum.RequestWithoutBookingDate),
                    DocumentNumber = model.Request?.RequestNumber
                };

                var bookingClient = Booking.Logic.Booking.GetServiceClient(bs);
                try
                {
                    model.Placement = bookingClient.VariationPlacement(bs);
                    if (model.Placement == null || !model.Placement.Locations.Any())
                    {
                        model.SelectPlaceStep = SelectPlaceStepEnum.SecondSelectOrganization;
                    }
                    else
                    {
                        var i = 0;
                        foreach (var location in model.Placement.Locations)
                        {
                            location.Index = i;
                            i++;
                        }
                    }
                }
                finally
                {
                    Booking.Logic.Booking.CloseClient(bookingClient);
                }
            }
            else
            {
                model.Placement = null;
            }

            model.SelectedItem = item;
        }

        private static void SelectPlaceFillFirst(SelectPlaceModel model)
        {
            model.SearchResult = null;
            model.Placement = null;
            model.HotelKey = null;
            model.SelectedItem = null;
            model.IndexPlacement = null;
            model.ActionStep = string.Empty;
            model.BookingRequest = null;
            model.TourId = null;
        }

        private static void SelectPlaceFillSecond(SelectPlaceModel model)
        {
            model.Placement = null;
            model.HotelKey = null;
            model.SelectedItem = null;
            model.IndexPlacement = null;
            model.ActionStep = string.Empty;
            model.BookingRequest = null;
            model.TourId = null;

            var bs = new BookingSearchRequest
            {
                TypeOfRestId = model.TypeOfRestId,
                Attendants = model.CountAttendant,
                CountRows = Settings.Default.TablePageSize,
                FirstRow = model.PageNumber * Settings.Default.TablePageSize,
                Places = model.CountChildren,
                PlaceOfRestId = model.PlaceOfRestId <= 0 ? null : model.PlaceOfRestId,
                TimeOfRestId = model.TimeOfRestId <= 0 ? null : model.TimeOfRestId,
                WithBookingDate = !Security.HasRight(AccessRightEnum.RequestWithoutBookingDate),
                DocumentNumber = model.Request?.RequestNumber
            };

            var bookingClient = Booking.Logic.Booking.GetServiceClient(bs);
            try
            {
                model.SearchResult = bookingClient.GetHotels(bs);
            }
            finally
            {
                Booking.Logic.Booking.CloseClient(bookingClient);
            }

            model.PageLast = model.SearchResult.Count / Settings.Default.TablePageSize +
                (model.SearchResult.Count % Settings.Default.TablePageSize > 0 ? 1 : 0) - 1;

            model.PageStart = model.PageNumber - 5;
            if (model.PageStart < 0)
            {
                model.PageStart = 0;
            }

            model.PageEnd = model.PageStart + Settings.Default.TablePageSize;
            if (model.PageEnd > model.PageLast)
            {
                model.PageEnd = model.PageLast;
            }
        }

        private string OrderString(TypeOfRest t)
        {
            var res = string.Empty;
            while (t != null)
            {
                res = t.Name + "\\" + res;
                t = t.Parent;
            }

            return res;
        }

        private TypeOfRest GetLeveled(HashSet<long> ids, TypeOfRest t)
        {
            var item = t;
            var level = 0;
            while (item != null)
            {
                level++;
                item = item.Parent;
            }

            return t != null ? new TypeOfRest(t) {MaxAge = level, IsActive = ids.Contains(t.Id)} : null;
        }

        private void SelectPlaceFillVocs(SelectPlaceModel model)
        {
            model.PlaceOfRests =
                VocController.GetPlacesOfRestInternal(true)
                    .OrderBy(p => p.Id)
                    .InsertAt(new PlaceOfRest {Id = 0, Name = DefaultOptionValue})
                    .ToList();

            model.TypeOfRests =
                UnitOfWork.GetSet<TypeOfRest>()
                    .Where(p => p.IsActive && !p.Commercial)
                    .Where(p => p.ForMPGU || p.Id == (long) TypeOfRestEnum.RestWithParentsOther).ToList()
                    .OrderBy(OrderString)
                    .InsertAt(new TypeOfRest {Id = 0, Name = DefaultOptionValue}).ToList();

            var hs = model.TypeOfRests.Select(t => t.ParentId).Where(p => p.HasValue).Select(p => p.Value).ToHashSet();
            model.TypeOfRests = model.TypeOfRests.Select(t => GetLeveled(hs, t)).ToList();

            model.YearOfRests = VocController.GetRequestCurrentPeriods();

            if (model.YearOfRestId == 0)
            {
                var year = model.YearOfRests.FirstOrDefault(y => y.Year == DateTime.Today.Year) ??
                           model.YearOfRests.LastOrDefault();
                model.YearOfRestId = year?.Id ?? 0;
            }

            model.TimeOfRests = VocController.GetTimesOfRest(model.TypeOfRestId, model.YearOfRestId)
                .OrderBy(p => p.Id)
                .InsertAt(new TimeOfRest {Id = 0, Name = DefaultOptionValue}).ToList();


            model.SubjectOfRests = VocController.GetSubjectsOfRest()
                .OrderBy(p => p.Id)
                .ToList();
        }

        /// <summary>
        ///     Замена сопровождающего
        /// </summary>
        [System.Web.Http.HttpPost]
        public ActionResult RequestReplacingAccompanying([FromBody] ApplicantViewModel model, [FromUri]long RequestId, [FromUri]long? ReplacingAccompanyId = null)
        {
            if (!Security.HasRight(AccessRightEnum.ReplacingAccompanying))
            {
                return Json(new BaseAsyncAnswer { ErrorText = "Access denied" });
            }

            SetUnitOfWorkInRefClass(UnitOfWork);

            var r = UnitOfWork.GetById<Request>(RequestId);

            model.Data.HaveMiddleName = model.HasNotMiddlename;

            if (model.CheckModel(r))
            {
                using (var scope = UnitOfWork.GetTransactionScope())
                {
                    if (ReplacingAccompanyId.HasValue && ReplacingAccompanyId.Value > 0)
                    {
                        var acc = UnitOfWork.GetById<Applicant>(ReplacingAccompanyId.Value);
                        if (acc.IsAgent)
                        {
                            acc.IsDeleted = true;
                            acc.IsAccomp = false;
                            UnitOfWork.WriteHistory(r.Id, $"Представитель заявителя {acc.GetFio()} исключен из заявления", Security.GetCurrentAccountId());
                        }
                        else
                        {
                            acc.IsDeleted = true;
                            r.CountAttendants++;
                            UnitOfWork.WriteHistory(r.Id, $"Сопровождающий {acc.GetFio()} исключен из заявления", Security.GetCurrentAccountId());
                        }
                    }
                    else
                    {
                        var acc = UnitOfWork.GetById<Applicant>(r.ApplicantId);
                        acc.IsAccomp = false;

                        UnitOfWork.WriteHistory(r.Id, $"Заявитель более не является сопровождающим", Security.GetCurrentAccountId());
                    }
                    UnitOfWork.SaveChanges();

                    var a = model.BuildData();
                    a.IsAccomp = true;
                    a.Payed = true;
                    a.RequestId = r.Id;
                    a.ForeginTypeId = a.ForeginTypeId > 0 ? a.ForeginTypeId : null;
                    a.IsProxy = a.ApplicantTypeId == (long)ApplicantTypeEnum.Confidant;
                    a.IsAgent = false;

                    UnitOfWork.AddEntity(a);

                    UnitOfWork.WriteHistory(r.Id, $"Сопровождающий {a.GetFio()} включен в заявление", Security.GetCurrentAccountId());

                    scope.Complete();
                }

                return Json(new BaseAsyncAnswer());
            }

            return Json(new BaseAsyncAnswer
            {
                ErrorText = model.GetErrorDescription()
            });
        }

        /// <summary>
        /// Функция для стресс теста 25.10.2021, если найдено в коде после 10.11.2021 - УДАЛИТЬ
        /// </summary>
        ///
        public ActionResult StressTest()
        {
            DateTime sdate = new DateTime();
            DateTime edate = new DateTime();
            var tp = DateTime.TryParse("26/10/2021 13:30:00", out sdate);
            tp = DateTime.TryParse("27/10/2021 13:30:00", out edate);
            var filter = new RequestFilterModel();
            filter.StartRequestDate = sdate;
            filter.EndRequestDate = edate;
            filter.YearOfRestId = 5;
            filter.SourceId = (long)SourceEnum.Mpgu;
            filter.ApplicantFio = "фам";
            var query = ApiController.RequestListQuery(filter);
            foreach (Request r in query)
            {
                using (var uw = new UnitOfWork())
                {
                    SetUnitOfWorkInRefClass(uw);
                    ApiController.RequestChangeStatus(r.Id, "33039E99-9360-47B2-AF29-6707EC0AFF01");
                    ApiController.RequestChangeStatus(r.Id, "4407104C-99FD-4667-A4EB-D654C264F34E");
                    ApiController.RequestChangeStatus(r.Id, "27D4DBFA-0B91-4D94-A2CE-A5F973414DA4");
                }
            }
            return RedirectToAction("RequestList");
        }

        public ActionResult StressTestDeleteAll()
        {
            DateTime sdate = new DateTime();
            DateTime edate = new DateTime();
            var tp = DateTime.TryParse("26/10/2021 13:30:00", out sdate);
            tp = DateTime.TryParse("27/10/2021 13:30:00", out edate);
            var filter = new RequestFilterModel();
            filter.StartRequestDate = sdate;
            filter.EndRequestDate = edate;
            filter.YearOfRestId = 5;
            filter.SourceId = (long)SourceEnum.Mpgu;
            filter.ApplicantFio = "фам";
            //filter.ApplicantFio = "Удаление";
            var query = ApiController.RequestListQuery(filter);
            foreach (Request r in query)
            {
                using (var uw = new UnitOfWork())
                {
                    SetUnitOfWorkInRefClass(uw);
                    var res = ApiController.RemoveStressTestVersion(r.Id);
                }
            }
            return RedirectToAction("RequestList");
        }

    }


}
