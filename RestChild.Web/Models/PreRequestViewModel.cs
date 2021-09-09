using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using RestChild.Comon;
using RestChild.Comon.Dto;
using RestChild.Comon.Dto.SearchRestChild;
using RestChild.Comon.Enumeration;
using RestChild.DAL.RepositoryExtensions;
using RestChild.Domain;
using RestChild.Extensions.Extensions;
using RestChild.Web.Common;
using RestChild.Web.Controllers.WebApi;
using RestChild.Web.Properties;

namespace RestChild.Web.Models
{
    public class PreRequestViewModel
    {
        /// <summary>
        ///     Номер заявления
        /// </summary>
        public string ReqNum { get; set; }

        /// <summary>
        ///     Номер заявления с МПГУ
        /// </summary>
        public string MpguNumber { get; set; }

        /// <summary>
        ///     Дата заявления
        /// </summary>
        public DateTime? RequestDate { get; set; }

        /// <summary>
        ///     Фамилия
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        ///     Имя
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        ///     Отчество
        /// </summary>
        public string MiddleName { get; set; }


        /// <summary>
        ///     Дата рождения
        /// </summary>
        public string DateOfBirth { get; set; }

        /// <summary>
        ///     Место рождения
        /// </summary>
        public string PlaceOfBirth { get; set; }

        /// <summary>
        ///     СНИЛС
        /// </summary>
        public string Snils { get; set; }

        /// <summary>
        ///     Тип документа
        /// </summary>
        public string DocumentType { get; set; }

        /// <summary>
        ///     Серия
        /// </summary>
        public string DocumentSeria { get; set; }

        /// <summary>
        ///     Номер
        /// </summary>
        public string DocumentNumber { get; set; }

        /// <summary>
        ///     Дата выдачи документа
        /// </summary>
        public string DocumentDate { get; set; }

        /// <summary>
        ///     Кем выдан документ
        /// </summary>
        public string DocumentIssure { get; set; }

        /// <summary>
        ///     Возраст
        /// </summary>
        public string Age { get; set; }

        /// <summary>
        ///     Пол
        /// </summary>
        public string Sex { get; set; }

        /// <summary>
        ///     Вид льготы
        /// </summary>
        public string BenefitType { get; set; }

        /// <summary>
        ///     адрес регистрации
        /// </summary>
        public string AddressRegistration { get; set; }

        /// <summary>
        ///     Доверенность
        /// </summary>
        public string Proxy { get; set; }

        /// <summary>
        ///     Вид отдыха
        /// </summary>
        public string TypeOfRest { get; set; }

        /// <summary>
        ///     Место отдыха
        /// </summary>
        public string HotelName { get; set; }

        /// <summary>
        ///     Время отдыха
        /// </summary>
        public string TimeOfRest { get; set; }

        /// <summary>
        ///     Вид ограничения
        /// </summary>
        public string RestrictionType { get; set; }

        /// <summary>
        ///     Подвид ограничения
        /// </summary>
        public string RestrictionSubType { get; set; }

        /// <summary>
        ///     Фамилия заявителя
        /// </summary>
        public string ApplicantLastName { get; set; }

        /// <summary>
        ///     Имя заявителя
        /// </summary>
        public string ApplicantFirstName { get; set; }

        /// <summary>
        ///     Отчество заявителя
        /// </summary>
        public string ApplicantMiddleName { get; set; }

        /// <summary>
        ///     Документ заявителя
        /// </summary>
        public string ApplicantDocType { get; set; }

        /// <summary>
        ///     Серия документа заявителя
        /// </summary>
        public string ApplicantDocSeries { get; set; }

        /// <summary>
        ///     Номер документа заявителя
        /// </summary>
        public string ApplicantDocNumber { get; set; }

        /// <summary>
        ///     Телефон заявителя
        /// </summary>
        public string ApplicantPhone { get; set; }

        /// <summary>
        ///     E-mail заявителя
        /// </summary>
        public string ApplicantEmail { get; set; }

        /// <summary>
        ///     Адрес оздоровительной организации
        /// </summary>
        public string HotelAddress { get; set; }

        /// <summary>
        ///     Тематика смены
        /// </summary>
        public string RestSubject { get; set; }

        /// <summary>
        ///     Статус заявления
        /// </summary>
        public string RequestState { get; set; }

        /// <summary>
        ///     Причина отказа
        /// </summary>
        public string DeclineReason { get; set; }

        public string CertificateNumber { get; set; }

        /// <summary>
        ///     Категория отдыхающего
        /// </summary>
        public string RestCaregory { get; set; }

        /// <summary>
        ///     Статус оплаты
        /// </summary>
        public string PaymentStatus { get; set; }

        /// <summary>
        ///     Имя списка детей
        /// </summary>
        public string ListOfChildrenName { get; set; }

        /// <summary>
        ///     отказ от билета обратно
        /// </summary>
        public string FromNotNeedTicketReason { get; set; }

        /// <summary>
        ///     отказ от билета туда
        /// </summary>
        public string ToNotNeedTicketReason { get; set; }

        /// <summary>
        ///     Информация об ограничениях из базового регистра
        /// </summary>
        public string BaseRegistryRestrictionTypeInfo { get; set; }

        /// <summary>
        ///     получение информации о родстве
        /// </summary>
        public string FatherFio { get; set; }

        public string FatherBirthDate { get; set; }
        public string MotherFio { get; set; }
        public string MotherBirthDate { get; set; }

        public string OrganizationName { get; set; }

        public string VedomstvoShortName { get; set; }

        public static ICollection<PreRequestViewModel> CreatePreRequests(IQueryable<Request> requestsQueryable,
            IUnitOfWork unitOfWork)
        {
            var result = new List<PreRequestViewModel>();
            var requests = requestsQueryable
                .Include(r => r.Applicant)
                .Include(r => r.Applicant.DocumentType)
                .Include(r => r.TransferFrom)
                .Include(r => r.TransferTo)
                .Include(r => r.TimesOfRest)
                .Include(r => r.PlacesOfRest)
                .ToArray()
                .Select(
                    r =>
                        new Request(r, 0)
                        {
                            TimesOfRest = r.TimesOfRest?.Select(t => new RequestsTimeOfRest(t)
                            {
                                TimeOfRest = t.TimeOfRest != null ? new TimeOfRest(t.TimeOfRest) : null
                            }).ToArray(),
                            PlacesOfRest = r.PlacesOfRest?.Select(t => new RequestPlaceOfRest(t)
                            {
                                PlaceOfRest = t.PlaceOfRest != null ? new PlaceOfRest(t.PlaceOfRest) : null
                            }).ToArray(),
                            TransferFrom = r.TransferFrom == null ? null : new TypeOfTransfer(r.TransferFrom),
                            TransferTo = r.TransferTo == null ? null : new TypeOfTransfer(r.TransferTo),
                            Applicant =
                                r.Applicant != null
                                    ? new Applicant(r.Applicant)
                                    {
                                        DocumentType = r.Applicant.DocumentType != null
                                            ? new DocumentType(r.Applicant.DocumentType)
                                            : null
                                    }
                                    : null
                        })
                .ToArray();

            var brPaymentDocument = (Settings.Default.BrPaymentDocument ?? string.Empty).Split(',').Select(s => s.Trim()).ToArray();

            foreach (var entry in unitOfWork.Context.ChangeTracker.Entries())
            {
                entry.State = EntityState.Detached;
            }

            var childrenList =
                unitOfWork.GetSet<Child>()
                    .Where(c => !c.IsDeleted && requestsQueryable.Select(r => r.Id).Contains(c.RequestId.Value))
                    .Include(c => c.DocumentType)
                    .Include(c => c.BenefitType)
                    .Include(c => c.Address)
                    .Include(c => c.Address.BtiAddress)
                    .Include(c => c.TypeOfRestriction)
                    .Include(c => c.TypeOfSubRestriction)
                    .Include(c => c.Address.BtiAddress.BtiStreet)
                    .Include(c => c.Address.BtiRegion)
                    .Include(c => c.BaseRegistryInfo)
                    .ToList().Select(r => new Child(r)
                    {
                        DocumentType = r.DocumentType != null ? new DocumentType(r.DocumentType) : null,
                        BenefitType = r.BenefitType != null ? new BenefitType(r.BenefitType) : null,
                        TypeOfSubRestriction = r.TypeOfSubRestriction != null
                            ? new TypeOfSubRestriction(r.TypeOfSubRestriction)
                            : null,
                        Address = r.Address != null
                            ? new Address(r.Address)
                            {
                                BtiAddress = r.Address.BtiAddress != null
                                    ? new BtiAddress(r.Address.BtiAddress)
                                    {
                                        BtiStreet = r.Address.BtiAddress.BtiStreet != null
                                            ? new BtiStreet(r.Address.BtiAddress.BtiStreet)
                                            : null,
                                        BtiRegion = r.Address.BtiAddress.BtiRegion != null
                                            ? new BtiRegion(r.Address.BtiAddress.BtiRegion)
                                            : null
                                    }
                                    : null,
                                BtiRegion = r.Address.BtiRegion != null
                                    ? new BtiRegion(r.Address.BtiRegion)
                                    : null
                            }
                            : null,
                        TypeOfRestriction = r.TypeOfRestriction != null
                            ? new TypeOfRestriction(r.TypeOfRestriction)
                            : null,
                        BaseRegistryInfo = r.BaseRegistryInfo?.Select(ss => new ExchangeBaseRegistry(ss)).ToList()
                    }).ToList();

            foreach (var entry in unitOfWork.Context.ChangeTracker.Entries())
            {
                entry.State = EntityState.Detached;
            }

            var childs = childrenList.GroupedDictionary(k => k.RequestId, v => v);

            var applicants =
                unitOfWork.GetSet<Applicant>()
                    .Where(
                        c =>
                            requestsQueryable.Where(r => r.TypeOfRest.NeedAttendant).Select(r => r.Id)
                                .Contains(c.RequestId.Value))
                    .Include(c => c.DocumentType)
                    .ToList()
                    .Select(r => new Applicant(r)
                    {
                        DocumentType = r.DocumentType != null ? new DocumentType(r.DocumentType) : null
                    })
                    .ToList()
                    .GroupedDictionary(k => k.RequestId, v => v);

            foreach (var entry in unitOfWork.Context.ChangeTracker.Entries())
            {
                entry.State = EntityState.Detached;
            }

            var tours =
                unitOfWork.GetSet<Tour>()
                    .Where(t => requestsQueryable.Select(r => r.TourId).Contains(t.Id))
                    .Include(t => t.Hotels)
                    .ToDictionary(t => t.Id, t => t);

            var hotels =
                unitOfWork.GetSet<Hotels>()
                    .Where(t => requestsQueryable.Select(r => r.Tour.HotelsId ?? r.HotelsId).Contains(t.Id))
                    .ToDictionary(t => t.Id, t => t);

            var placeOfRests =
                unitOfWork.GetSet<PlaceOfRest>()
                    .ToDictionary(t => t.Id, t => t);

            var timeOfRests =
                unitOfWork.GetSet<TimeOfRest>()
                    .ToDictionary(t => t.Id, t => t);

            var documentTypes =
                unitOfWork.GetSet<DocumentType>()
                    .ToDictionary(t => t.Id, t => t);

            var typeOfRests =
                unitOfWork.GetSet<TypeOfRest>()
                    .Include(t => t.Parent)
                    .ToDictionary(t => t.Id, t => t);

            var statuses =
                unitOfWork.GetSet<Status>()
                    .ToDictionary(t => t.Id, t => t);

            var declineReasons =
                unitOfWork.GetSet<DeclineReason>()
                    .ToDictionary(t => t.Id, t => t);


            var subjectOfRest =
                unitOfWork.GetSet<SubjectOfRest>()
                    .ToDictionary(t => t.Id, t => t);

            foreach (var request in requests)
            {
                var applicant = request.Applicant ?? new Applicant();
                var tour = tours.ContainsKey(request.TourId ?? 0) ? tours[request.TourId ?? 0] : null;
                var hotelsId = tour != null ? tour.HotelsId : request.HotelsId;
                var hotel = hotels.ContainsKey(hotelsId ?? 0) ? hotels[hotelsId ?? 0] : null;
                var por = placeOfRests.ContainsKey(request.PlaceOfRestId ?? 0)
                    ? placeOfRests[request.PlaceOfRestId ?? 0]
                    : null;
                var tr = typeOfRests.ContainsKey(request.TypeOfRestId ?? 0)
                    ? typeOfRests[request.TypeOfRestId ?? 0]
                    : null;

                var timeRest = timeOfRests.ContainsKey(request.TimeOfRestId ?? 0)
                    ? timeOfRests[request.TimeOfRestId ?? 0]
                    : null;
                var dateIncome = tour?.DateIncome;
                var timeOfRest = FormatTimeOfRest(timeRest?.Name, tour?.DateIncome, tour?.DateOutcome);

                var placeOfRest =
                    $"{(hotel != null ? hotel.Name : "-")}\n{(por != null ? por.Name.FormatEx(false) : "-")}";

                if (request.IsFirstCompany && !request.BookingGuid.HasValue)
                {
                    placeOfRest =
                        $"{por?.Name}, {string.Join(", ", request?.PlacesOfRest?.Select(p => p.PlaceOfRest?.Name ?? String.Empty)?.ToArray() ?? new string[0])}";
                    timeOfRest =
                        $"{timeRest?.Name}, {string.Join(", ", request?.TimesOfRest?.Select(p => p.TimeOfRest?.Name)?.ToArray() ?? new string[0])}";
                }

                var typeOfRest = FormatTypeOfRest(tr);

                var hotelAddress = hotel != null ? hotel.Address : "-";

                var restSubject = subjectOfRest.ContainsKey(request.SubjectOfRestId ?? 0)
                    ? subjectOfRest[request.SubjectOfRestId ?? 0].Name
                    : "-";

                var requestState = statuses.ContainsKey(request.StatusId ?? 0)
                    ? statuses[request.StatusId ?? 0].Name
                    : "-";

                var declineReason = declineReasons.ContainsKey(request.DeclineReasonId ?? 0)
                    ? declineReasons[request.DeclineReasonId ?? 0].Name
                    : "-";

                var applicantDocType = documentTypes.ContainsKey(applicant.DocumentTypeId ?? 0)
                    ? documentTypes[applicant.DocumentTypeId ?? 0].Name
                    : "-";

                if (applicant.IsAccomp)
                {
                    var to = applicant.LinkToPeoples?.FirstOrDefault(l =>
                        l.TransportId == applicant.Bout?.TransportInfoToId);
                    var from = applicant.LinkToPeoples?.FirstOrDefault(l =>
                        l.TransportId == applicant.Bout?.TransportInfoFromId);
                    if (applicant.Address == null && applicant.AddressId != null)
                        applicant.Address = unitOfWork.GetById<Address>(applicant.AddressId);

                    result.Add(new PreRequestViewModel
                    {
                        ReqNum = request.RequestNumber,
                        LastName = applicant.LastName,
                        FirstName = applicant.FirstName,
                        MiddleName = applicant.MiddleName,
                        Proxy = applicant.IsProxy ? $"{applicant.ProxyNumber ?? "б/н"} от {applicant.ProxyDateOfIssure.FormatEx()}": string.Empty,
                        DocumentType = applicantDocType,
                        DocumentSeria = applicant.DocumentSeria,
                        DocumentNumber = applicant.DocumentNumber,
                        DocumentDate = applicant.DocumentDateOfIssue.FormatEx(),
                        DocumentIssure = applicant.DocumentSubjectIssue,
                        Sex = FormatSexForExcel(applicant.Male),
                        BenefitType = null,
                        DateOfBirth = applicant.DateOfBirth.FormatEx(),
                        Age = StaticHelpers.GetAgeInYears(applicant.DateOfBirth, dateIncome).FormatEx(),
                        PlaceOfBirth = applicant.PlaceOfBirth,
                        HotelName = placeOfRest,
                        TimeOfRest = timeOfRest,
                        TypeOfRest = typeOfRest,
                        RestrictionType = null,
                        Snils = applicant.Snils,
                        ApplicantLastName = applicant.LastName,
                        ApplicantFirstName = applicant.FirstName,
                        ApplicantMiddleName = applicant.MiddleName,
                        ApplicantDocType = applicantDocType,
                        ApplicantDocSeries = applicant.DocumentSeria,
                        ApplicantDocNumber = applicant.DocumentNumber,
                        ApplicantPhone = applicant.Phone,
                        ApplicantEmail = applicant.Email,
                        AddressRegistration = applicant.Address?.ToString(),
                        MpguNumber = request.RequestNumberMpgu,
                        RequestDate = request.DateRequest,
                        HotelAddress = hotelAddress,
                        RestSubject = restSubject,
                        RequestState = requestState,
                        DeclineReason = declineReason,
                        CertificateNumber = request.CertificateNumber,
                        FromNotNeedTicketReason = request.TransferFrom?.Name ?? from?.NotNeedTicketReason?.Name,
                        ToNotNeedTicketReason = request.TransferTo?.Name ?? to?.NotNeedTicketReason?.Name
                    });
                }

                var attendants = applicants.ContainsKey(request.Id) ? applicants[request.Id] : null;
                if (attendants != null && attendants.Any())
                {
                    foreach (var attendant in attendants)
                    {
                        var to = attendant.LinkToPeoples?.FirstOrDefault(l =>
                            l.TransportId == attendant.Bout?.TransportInfoToId);
                        var from = attendant.LinkToPeoples?.FirstOrDefault(l =>
                            l.TransportId == attendant.Bout?.TransportInfoFromId);

                        result.Add(new PreRequestViewModel
                        {
                            ReqNum = request.RequestNumber,
                            LastName = attendant.LastName,
                            FirstName = attendant.FirstName,
                            MiddleName = attendant.MiddleName,
                            Proxy = attendant.IsProxy ? $"{attendant.ProxyNumber ?? "б/н"} от {attendant.ProxyDateOfIssure.FormatEx()}": string.Empty,
                            DocumentType = attendant.DocumentType?.Name,
                            DocumentSeria = attendant.DocumentSeria,
                            DocumentNumber = attendant.DocumentNumber,
                            DateOfBirth = attendant.DateOfBirth.FormatEx(),
                            PlaceOfBirth = attendant.PlaceOfBirth,
                            DocumentDate = attendant.DocumentDateOfIssue.FormatEx(),
                            DocumentIssure = attendant.DocumentSubjectIssue,
                            Sex = FormatSexForExcel(attendant.Male),
                            Age = StaticHelpers.GetAgeInYears(attendant.DateOfBirth, dateIncome).FormatEx(),
                            BenefitType = null,
                            HotelName = placeOfRest,
                            TimeOfRest = timeOfRest,
                            TypeOfRest = typeOfRest,
                            Snils = attendant.Snils,
                            RestrictionType = null,
                            ApplicantLastName = applicant.LastName,
                            ApplicantFirstName = applicant.FirstName,
                            ApplicantMiddleName = applicant.MiddleName,
                            ApplicantDocType = applicantDocType,
                            ApplicantDocSeries = applicant.DocumentSeria,
                            ApplicantDocNumber = applicant.DocumentNumber,
                            ApplicantPhone = applicant.Phone,
                            ApplicantEmail = applicant.Email,
                            MpguNumber = request.RequestNumberMpgu,
                            RequestDate = request.DateRequest,
                            HotelAddress = hotelAddress,
                            RestSubject = restSubject,
                            RequestState = requestState,
                            DeclineReason = declineReason,
                            CertificateNumber = request.CertificateNumber,
                            FromNotNeedTicketReason = request.TransferFrom?.Name ?? from?.NotNeedTicketReason?.Name,
                            ToNotNeedTicketReason = request.TransferTo?.Name ?? to?.NotNeedTicketReason?.Name
                        });
                    }
                }

                var curchilds = childs.ContainsKey(request.Id) ? childs[request.Id] : null;

                if (curchilds != null && curchilds.Any())
                {
                    foreach (var child in curchilds)
                    {
                        if (child == null)
                        {
                            continue;
                        }

                        var to = child.LinkToPeoples?.FirstOrDefault(l =>
                            l.TransportId == child?.Bout?.TransportInfoToId);
                        var from = child.LinkToPeoples?.FirstOrDefault(l =>
                            l.TransportId == child?.Bout?.TransportInfoFromId);

                        var baseRegistryRestrictionTypeInfo = string.Empty;
                        var codes = child.BenefitType?.ExnternalUid.Split(',').ToArray();
                        var lowincom = WebExchangeController.NeedCheckPayment(child) ? new[] { Settings.Default.LowIncomeType } : null;

                        if (child.BaseRegistryInfo?.Any() ?? false)
                        {
                            var briresult = new StringBuilder();
                            foreach (var bri in child.BaseRegistryInfo.Where(ss => !ss.NotActual))
                            {
                                var ss = bri.Parse(codes, brPaymentDocument, lowincom);
                                foreach (var category in ss.BenefitCheckResult?.ToList() ?? new List<ResidentPreferentialCategories>())
                                {
                                    briresult.Append(category.Preferentical);

                                    if (isDateValid(category.StartDate) || isDateValid(category.EndDate))
                                    {
                                        briresult.Append(", срок действия:");
                                        briresult.Append(isDateValid(category.StartDate) ? " с " + category.StartDate.FormatEx() : string.Empty);
                                        briresult.Append(isDateValid(category.EndDate) ? " по " + category.EndDate.FormatEx() : string.Empty);
                                    }
                                    briresult.Append(".\n\r");
                                }
                            }
                            baseRegistryRestrictionTypeInfo = briresult.ToString();
                        }

                        result.Add(new PreRequestViewModel
                        {
                            ReqNum = request.RequestNumber,
                            LastName = child.LastName,
                            FirstName = child.FirstName,
                            MiddleName = child.MiddleName,
                            DocumentType = child.DocumentType?.Name,
                            DocumentSeria = child.DocumentSeria,
                            DocumentNumber = child.DocumentNumber,
                            DateOfBirth = child.DateOfBirth.FormatEx(),
                            PlaceOfBirth = child.PlaceOfBirth,
                            Age = StaticHelpers.GetAgeInYears(child.DateOfBirth, dateIncome).FormatEx(),
                            DocumentDate = child.DocumentDateOfIssue.FormatEx(),
                            DocumentIssure = child.DocumentSubjectIssue,
                            Sex = FormatSexForExcel(child.Male),
                            BenefitType = child.BenefitType?.Name,
                            HotelName = placeOfRest,
                            TimeOfRest = timeOfRest,
                            TypeOfRest = typeOfRest,
                            Snils = child.Snils,
                            RestrictionSubType = child.TypeOfSubRestriction?.Name,
                            RestrictionType = child.TypeOfRestriction?.Name,
                            ApplicantLastName = applicant.LastName,
                            ApplicantFirstName = applicant.FirstName,
                            ApplicantMiddleName = applicant.MiddleName,
                            ApplicantDocType = applicantDocType,
                            ApplicantDocSeries = applicant.DocumentSeria,
                            ApplicantDocNumber = applicant.DocumentNumber,
                            ApplicantPhone = applicant.Phone,
                            ApplicantEmail = applicant.Email,
                            MpguNumber = request.RequestNumberMpgu,
                            RequestDate = request.DateRequest,
                            HotelAddress = hotelAddress,
                            RestSubject = restSubject,
                            RequestState = requestState,
                            DeclineReason = declineReason,
                            AddressRegistration = child.Address?.ToString(),
                            CertificateNumber = request.CertificateNumber,
                            FromNotNeedTicketReason = request.TransferFrom?.Name ?? from?.NotNeedTicketReason?.Name,
                            ToNotNeedTicketReason = request.TransferTo?.Name ?? to?.NotNeedTicketReason?.Name,
                            BaseRegistryRestrictionTypeInfo = baseRegistryRestrictionTypeInfo
                        });
                    }
                }
            }

            return result;
        }

        private static string FormatTimeOfRest(string timeRestName, DateTime? dateIncome, DateTime? dateOutcome)
        {
            return
                $"{(string.IsNullOrWhiteSpace(timeRestName) ? "-" : timeRestName)}\n{(!dateIncome.HasValue ? "-" : dateIncome.FormatEx())}-{(!dateOutcome.HasValue ? "-" : dateOutcome.FormatEx())}";
        }

        private static string FormatTypeOfRest(TypeOfRest tr)
        {
            return tr?.Parent != null
                ? $"{tr.Parent.Name} / {tr.Name}"
                : tr != null
                    ? tr.Name
                    : "-";
        }

        private static void FormatForExcel(PreRequestViewModel preRequestViewModel)
        {
            preRequestViewModel.DocumentType = preRequestViewModel.DocumentType == null
                ? "-"
                : preRequestViewModel.DocumentType.FormatEx(false);
            preRequestViewModel.DocumentSeria = preRequestViewModel.DocumentSeria.FormatEx(false);
            preRequestViewModel.DocumentNumber = preRequestViewModel.DocumentNumber.FormatEx(false);
            preRequestViewModel.PlaceOfBirth = preRequestViewModel.PlaceOfBirth.FormatEx(false);
            preRequestViewModel.DocumentDate = preRequestViewModel.DocumentDate.FormatEx();
            preRequestViewModel.DocumentIssure = preRequestViewModel.DocumentIssure.FormatEx();
            preRequestViewModel.BenefitType = string.IsNullOrWhiteSpace(preRequestViewModel.BenefitType)
                ? "-"
                : preRequestViewModel.BenefitType;
            preRequestViewModel.RestrictionType = string.IsNullOrWhiteSpace(preRequestViewModel.RestrictionType)
                ? "-"
                : preRequestViewModel.RestrictionType;
            preRequestViewModel.AddressRegistration = string.IsNullOrWhiteSpace(preRequestViewModel.AddressRegistration)
                ? "-"
                : preRequestViewModel.AddressRegistration;
        }

        private static string FormatSexForExcel(bool? sex)
        {
            return sex.HasValue
                ? sex.Value ? "Мужской" : "Женский"
                : "";
        }

        public static PreRequestViewModel CreateExcelModel(IndexRestChildDto restRecord,
            IEnumerable<TypeOfRest> typesOfRest,
            IEnumerable<PlaceOfRest> placesOfRest,
            IEnumerable<BenefitType> benefitTypes,
            IEnumerable<SubjectOfRest> subjectsOfRest,
            IEnumerable<TimeOfRest> timeOfRest,
            RelationshipCheckResult relationship)
        {
            var preRequestViewModel = new PreRequestViewModel
            {
                ReqNum = restRecord.RequestNumber,
                MpguNumber = restRecord.RequestNumberFromMpgu,
                CertificateNumber = restRecord.CertificateNumber,
                RequestDate = restRecord.RequestSupplyDate,
                RequestState = restRecord.Status,
                TypeOfRest = restRecord.TypeOfRestId != 0
                    ? FormatTypeOfRest(typesOfRest.FirstOrDefault(r => r.Id == restRecord.TypeOfRestId))
                    : string.Empty,
                HotelName = restRecord.HotelName,
                HotelAddress = restRecord.HotelAddress,
                TimeOfRest = FormatTimeOfRest(timeOfRest.FirstOrDefault(i => i.Id == restRecord.TimeOfRestId)?.Name,
                    restRecord.DateIncome, restRecord.DateOutcome),
                RestSubject = restRecord.SubjectOfRestId != 0
                    ? subjectsOfRest.FirstOrDefault(r => r.Id == restRecord.SubjectOfRestId)?.Name
                    : string.Empty,
                LastName = restRecord.LastName,
                FirstName = restRecord.FirstName,
                MiddleName = restRecord.MiddleName,
                Sex = FormatSexForExcel(restRecord.Male),
                DateOfBirth = restRecord.BirthDate.FormatEx(),
                Age = StaticHelpers.GetAgeInYears(restRecord.BirthDate, restRecord.DateIncome).FormatEx(),
                PlaceOfBirth = restRecord.PlaceOfBirth,
                DocumentType = restRecord.DocumentType,
                DocumentSeria = restRecord.DocumentSeria,
                DocumentNumber = restRecord.DocumentNumber,
                DocumentDate = restRecord.DocumentIssueDate.FormatEx(),
                DocumentIssure = restRecord.DocumentSubjectIssue,
                RestrictionType = restRecord.TypeOfRestriction,
                BenefitType = restRecord.BenefitTypeId != 0
                    ? benefitTypes.FirstOrDefault(r => r.Id == restRecord.BenefitTypeId)?.Name
                    : string.Empty,
                AddressRegistration = restRecord.Address,
                ApplicantLastName = restRecord.ApplicantLastName,
                ApplicantFirstName = restRecord.ApplicantFirstName,
                ApplicantMiddleName = restRecord.ApplicantMiddleName,
                ApplicantDocType = restRecord.ApplicantDocumentType,
                ApplicantDocSeries = restRecord.ApplicantDocumentSeria,
                ApplicantDocNumber = restRecord.ApplicantDocumentNumber,
                ApplicantPhone = restRecord.ApplicantPhone,
                ApplicantEmail = restRecord.ApplicantEmail,
                RestCaregory = FormatRestCategory(restRecord.RestCategory),
                PaymentStatus = FormatPaymentStatus(restRecord.PaymentStatus),
                ListOfChildrenName = restRecord.ListOfChildrenName,
                VedomstvoShortName = restRecord.VedomstvoShortName,
                OrganizationName = restRecord.OrganizationName,
                FromNotNeedTicketReason = restRecord.FromNotNeedTicketReason.FormatEx(),
                ToNotNeedTicketReason = restRecord.ToNotNeedTicketReason.FormatEx(),
                FatherFio =
                    $"{relationship?.FatherLastName} {relationship?.FatherFirstName} {relationship?.FatherPatronymic}",
                FatherBirthDate = $"{relationship?.FatherBirthDate.FormatEx()}",
                MotherFio =
                    $"{relationship?.MotherLastName} {relationship?.MotherFirstName} {relationship?.MotherPatronymic}",
                MotherBirthDate = $"{relationship?.MotherBirthDate.FormatEx()}"
            };

            FormatForExcel(preRequestViewModel);
            return preRequestViewModel;
        }

        private static string FormatPaymentStatus(bool status)
        {
            return status ? "Оплачено" : "Не оплачено";
        }

        private static string FormatRestCategory(RestCategoryEnum restCategory)
        {
            return restCategory.GetDisplayValue();
        }

        /// <summary>
        ///     Проверка валидности времени действия льготы
        /// </summary>
        private static bool isDateValid(DateTime? dateTime)
        {
            if (dateTime.HasValue)
            {
                if (dateTime != new DateTime(0))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
