using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using DocumentFormat.OpenXml.Office2010.Excel;
using RestChild.Comon.Dto;
using RestChild.Comon.Enumeration;
using RestChild.Domain;
using RestChild.Web.Common;

namespace RestChild.Web.Models
{
    /// <summary>
    ///     модель ребёнка
    /// </summary>
    public class ChildViewModel : ViewModelBase<Child>
    {
        /// <summary>
        ///     Id свидетельства о рождении в [dbo].[DocumentType]
        /// </summary>
        private readonly long BirthSertificateId = 22;

        /// <summary>
        /// конструктор
        /// </summary>
        /// <param name="data"></param>
        public ChildViewModel(Child data)
            : base(data)
        {
            Address = new AddressViewModel(data.Address ?? new Address());
            BenefitApproveHtml = string.IsNullOrEmpty(data.BenefitApproveHtml)
                ? string.Empty
                : Convert.ToBase64String(Encoding.UTF8.GetBytes(data.BenefitApproveHtml));
        }

        public ChildViewModel()
            : base(new Child())
        {
            Address = new AddressViewModel(new Address());
            BenefitApproveHtml = string.Empty;
        }

        public string BenefitApproveHtml { get; set; }

        /// <summary>
        ///     типы отдыха
        /// </summary>
        public IList<TypeOfRest> TypeOfRests { get; set; }

        /// <summary>
        ///     времена отдыха
        /// </summary>
        public IList<TimeOfRest> TimeOfRests { get; set; }

        /// <summary>
        ///     Выбранные времена
        /// </summary>
        public IList<DateTime> SelectedPeriods { get; set; }

        /// <summary>
        ///     времена отдыха
        /// </summary>
        public IList<PlaceOfRest> PlacesOfRest { get; set; }

        /// <summary>
        ///     текущие заявочные кампании.
        /// </summary>
        public IList<YearOfRest> RequestCurrentPeriod { get; set; }

        /// <summary>
        ///     виды ограничения
        /// </summary>
        public IList<TypeOfRestriction> TypeOfRestrictions { get; set; }

        /// <summary>
        ///     виды льготы
        /// </summary>
        public IList<BenefitType> BenefitTypes { get; set; }

        /// <summary>
        /// тип малообеспеченных
        /// </summary>
        public string[] LowIncomeTypes { get; set; }

        /// <summary>
        ///     ограничение на вид льготы и категорию
        /// </summary>
        public IList<TypeOfRestBenefitRestriction> TypeOfRestBenefitRestrictions { get; set; }

        /// <summary>
        ///     статус по отношению к ребенку
        /// </summary>
        public IList<StatusByChild> StatusByChild { get; set; }

        public AddressViewModel Address { get; set; }

        public Guid? AttendantGuid { get; set; }

        public IEnumerable<ResidentPreferentialCategories> ResidentPreferentialCategories { get; set; }

        public int? MinAge { get; set; }

        public int? MaxAge { get; set; }

        /// <summary>
        ///     Нельзя удалить из заявления
        /// </summary>
        public bool? NotForDelete { get; set; }

        public bool HasNotMiddlename { get; set; }

        /// <summary>
        ///     Межведомственные запросы.
        /// </summary>
        public IList<InteragencyRequestResultViewModel> Results { get; set; }

        public override Child BuildData()
        {
            Data.Address = Address.BuildData();
            Data.BenefitEndDate = Data.BenefitNeverEnd ? null : Data.BenefitEndDate;

            Data.BenefitApproveHtml = string.IsNullOrEmpty(BenefitApproveHtml)
                ? string.Empty
                : Encoding.UTF8.GetString(Convert.FromBase64String(BenefitApproveHtml));

            Data.IsLast = true;

            return base.BuildData();
        }

        private static int GetMonthsBetween(DateTime from, DateTime to)
        {
            if (from > to) return GetMonthsBetween(to, from);

            var monthDiff = Math.Abs(to.Year * 12 + (to.Month - 1) - (from.Year * 12 + (from.Month - 1)));

            if (from.AddMonths(monthDiff) > to || to.Day < from.Day)
            {
                return monthDiff - 1;
            }

            return monthDiff;
        }

        public bool CheckModel(Request request, string action = null)
        {
            IsValid = true;

            if (Data == null)
            {
                return IsValid.Value;
            }

            if (string.IsNullOrWhiteSpace(Data.LastName))
            {
                IsValid = false;
                LastNameEm = RequaredField;
            }

            if (string.IsNullOrWhiteSpace(Data.FirstName))
            {
                IsValid = false;
                FirstNameEm = RequaredField;
            }


            if (string.IsNullOrWhiteSpace(Data.Snils))
            {
                IsValid = false;
                SnilsEm = RequaredField;
            }

            if (string.IsNullOrWhiteSpace(Data.MiddleName) && !Data.HaveMiddleName)
            {
                IsValid = false;
                MiddleNameEm = RequaredField;
            }


            if (!Data.DateOfBirth.HasValue)
            {
                IsValid = false;
                DateOfBirthEm = RequaredField;
            }

            if (string.IsNullOrWhiteSpace(Data.PlaceOfBirth))
            {
                IsValid = false;
                PlaceOfBirthEm = RequaredField;
            }

            if (!Data.DocumentTypeId.HasValue)
            {
                IsValid = false;
                DocumentTypeEm = RequaredField;
            }
            else
            {
                if (Data.DateOfBirth.HasValue && Data.DocumentTypeId == BirthSertificateId &&
                    GetMonthsBetween(DateTime.Now, Data.DateOfBirth.Value) >= 14 * 12 + 3)
                {
                    IsValid = false;
                    DocumentTypeEm = "Для ребенка старше 14 лет нельзя выбрать свидетельство о рождении";
                }
            }

            if (string.IsNullOrWhiteSpace(Data.DocumentSeria) && Data.DocumentTypeId != 23 &&
                !DocumentTypeHelper.IsPassportOfForeignCountry(Data.DocumentTypeId ?? 0))
            {
                IsValid = false;
                DocumentSeriaEm = RequaredField;
            }
            else
            {
                if (!DocumentTypeHelper.IsDocumentSeriaValid(Data.DocumentTypeId ?? 0, Data.DocumentSeria))
                {
                    IsValid = false;
                    DocumentSeriaEm = "Введены некорректные данные";
                }
            }

            if ((Data.DocumentType?.TypesOfRest?.Any() ?? false) &&
                (Data.DocumentType?.TypesOfRest?.All(ss => ss.Id != request.TypeOfRestId) ?? false))
            {
                IsValid = false;
                DocumentSeriaEm =
                    "В поле Вид документа удостоверяющего личность обнаружена ошибка: Указанный вид документа не может быть использован в данном виде обращения";
            }

            var age14 = DateTime.Now.Date.AddYears(-14);
            if (new long?[] {20009, 20010}.Contains(Data.DocumentTypeId) && Data.DateOfBirth > age14)
            {
                IsValid = false;
                DocumentSeriaEm =
                    "В поле Вид документа удостоверяющего личность обнаружена ошибка: Временное удостоверение личности гражданина РФ не соответствует возрасту ребенка";
            }

            if (string.IsNullOrWhiteSpace(Data.DocumentNumber))
            {
                IsValid = false;
                DocumentNumberEm = RequaredField;
            }
            else
            {
                if (!DocumentTypeHelper.IsDocumentNumberValid(Data.DocumentTypeId ?? 0, Data.DocumentNumber))
                {
                    IsValid = false;
                    DocumentNumberEm = "Введены некорректные данные";
                }
            }

            if (string.IsNullOrWhiteSpace(Data.DocumentSubjectIssue))
            {
                IsValid = false;
                DocumentSubjectIssueEm = RequaredField;
            }

            if (!Data.DocumentDateOfIssue.HasValue)
            {
                IsValid = false;
                DocumentDateOfIssueEm = RequaredField;
            }

            var startOfRest = request?.Tour?.DateIncome ??
                              (SelectedPeriods?.Any() ?? false ? SelectedPeriods.Min() : (DateTime?) null);
            var startLastOfRest = request?.Tour?.DateIncome ??
                                  (SelectedPeriods?.Any() ?? false ? SelectedPeriods.Max() : (DateTime?) null);

            if (!Data.DateOfBirth.HasValue)
            {
                IsValid = false;
                DateOfBirthEm = RequaredField;
            }
            else
            {
                if (TypeOfRests != null && TypeOfRestBenefitRestrictions != null && request.TypeOfRestId.HasValue
                    && (request.TimeOfRestId.HasValue ||
                        request.TypeOfRestId == (long)TypeOfRestEnum.Money || request.TypeOfRest.ParentId == (long)TypeOfRestEnum.Money))
                {
                    var typeOfRest = TypeOfRests.FirstOrDefault(t => t.Id == request.TypeOfRestId);
                    if (startOfRest.HasValue && typeOfRest != null)
                    {
                        var ageInStartDate = Data.GetAgeInYears(startOfRest.Value) ?? 0;
                        var ageInLastStartDate = Data.GetAgeInYears(startLastOfRest.Value) ?? 0;

                        if (ageInStartDate < typeOfRest.MinAge || ageInLastStartDate > typeOfRest.MaxAge)
                        {
                            DateOfBirthEm = "Возраст ребёнка не соответствует указанному виду отдыха";
                            IsValid = false;
                        }

                        var restriction =
                            TypeOfRestBenefitRestrictions.FirstOrDefault(
                                t => t.BenefitTypeId == Data.BenefitTypeId && t.TypeOfRestId == request.TypeOfRestId);
                        if (restriction != null &&
                            (ageInStartDate < restriction.MinAge || ageInLastStartDate > restriction.MaxAge))
                        {
                            DateOfBirthEm =
                                "Возраст ребёнка не соответствует указанному виду отдыха и выбранной льготе";
                            IsValid = false;
                        }
                    }
                    else if (request.TypeOfRestId == (long)TypeOfRestEnum.Money || request.TypeOfRest.ParentId == (long)TypeOfRestEnum.Money)
                    {
                        var ageInRegDate = Data.GetAgeInYears(request.DateRequest);
                        if (ageInRegDate < request.TypeOfRest.MinAge || ageInRegDate > request.TypeOfRest.MaxAge)
                        {
                            DateOfBirthEm = "Возраст ребёнка не соответствует указанному виду отдыха";
                            IsValid = false;
                        }
                    }
                }

                var dateTime = Data.Request?.DateRequest?.Date ?? DateTime.Today;
                if (Data.GetAgeInYears(dateTime.AddMonths(-3)).Value >= 14
                    && Data.DocumentTypeId.HasValue && DocumentTypeHelper.IsBirthCert(Data.DocumentTypeId.Value))
                {
                    DocumentTypeEm = "Свидетельство о рождении не соответствует возрасту ребенка";
                    IsValid = false;
                }

                if (Data.GetAgeInYears(dateTime.AddMonths(2)).Value < 14 && Data.DocumentTypeId.HasValue &&
                    DocumentTypeHelper.IsPassport(Data.DocumentTypeId.Value))
                {
                    DocumentTypeEm = "Паспорт гражданина РФ не соответствует возрасту ребенка";
                    IsValid = false;
                }
            }

            var benfit = BenefitTypes.FirstOrDefault(b => b.Id == Data.BenefitTypeId);
            if (benfit != null)
            {
                if (benfit.NeedTypeOfRestriction && !Data.TypeOfRestrictionId.HasValue)
                {
                    IsValid = false;
                    TypeOfRestrictionEm = RequaredField;
                }

                if (Data.TypeOfRestrictionId.HasValue && !Data.TypeOfSubRestrictionId.HasValue)
                {
                    var restriction = TypeOfRestrictions.FirstOrDefault(t => t.Id == Data.TypeOfRestrictionId);
                    if (restriction != null && restriction.Subs != null && restriction.Subs.Any())
                    {
                        IsValid = false;
                        TypeOfSubRestrictionEm = RequaredField;
                    }
                }


                if (benfit.NeedApproveDocument)
                {
                    if (!Data.BenefitDateOfIssure.HasValue)
                    {
                        IsValid = false;
                        BenefitDateOfIssureEm = RequaredField;
                    }

                    if (string.IsNullOrWhiteSpace(Data.BenefitNumber))
                    {
                        IsValid = false;
                        BenefitNumberEm = RequaredField;
                    }

                    if (string.IsNullOrWhiteSpace(Data.BenefitSubjectIssue))
                    {
                        IsValid = false;
                        BenefitSubjectIssueEm = RequaredField;
                    }
                }
            }

            if (!request.IsFirstCompany && !Data.SchoolNotPresent && !Data.SchoolId.HasValue)
            {
                IsValid = false;
                SchoolEm = RequaredField;
            }

            if (Data.RegisteredInMoscow)
            {
                var address = Data.Address;
                if (address == null
                    ||
                    (!address.BtiAddressId.HasValue && !address.BtiDistrictId.HasValue &&
                     !address.BtiRegionId.HasValue &&
                     string.IsNullOrEmpty(address.Street) && string.IsNullOrEmpty(address.House) &&
                     string.IsNullOrEmpty(address.Corpus) && string.IsNullOrEmpty(address.Appartment) &&
                     string.IsNullOrEmpty(address.Stroenie) &&
                     string.IsNullOrEmpty(address.Vladenie)))
                {
                    IsValid = false;
                    AddressEm = RequaredField;
                }
            }

            var tr = TypeOfRests.FirstOrDefault(t => t.Id == request.TypeOfRestId);

            if (tr?.Id == (long) TypeOfRestEnum.Compensation || tr?.Id == (long) TypeOfRestEnum.CompensationYouthRest)
            {
                if ((Data.CostOfTour ?? 0) <= 0)
                {
                    CostOfTourEm = RequaredField;
                    IsValid = false;
                }

                if ((Data.AmountOfCompensation ?? 0) <= 0)
                {
                    AmountOfCompensationEm = RequaredField;
                    IsValid = false;
                }

                if (!Data.RequestInformationVoucherId.HasValue)
                {
                    RequestInformationVoucherEm = RequaredField;
                    IsValid = false;
                }
            }

            return IsValid.Value;
        }

        public override string ToString()
        {
            return $"{Data?.LastName} {Data?.FirstName} {Data?.MiddleName}";
        }

        #region Проверка формы

        /// <summary>
        ///     Фамилия
        /// </summary>
        [Display(Description = "Фамилия")]
        public virtual string LastNameEm { get; set; }

        /// <summary>
        ///     Имя
        /// </summary>
        [Display(Description = "Имя")]
        public virtual string FirstNameEm { get; set; }

        /// <summary>
        ///     Отчество
        /// </summary>
        [Display(Description = "Отчество")]
        public virtual string MiddleNameEm { get; set; }


        /// <summary>
        ///     СНИЛС
        /// </summary>
        [Display(Description = "СНИЛС")]
        public virtual string SnilsEm { get; set; }

        /// <summary>
        ///     Серия документа
        /// </summary>
        [Display(Description = "Серия документа удостоверяющего личность")]
        public virtual string DocumentSeriaEm { get; set; }

        /// <summary>
        ///     Номер документа
        /// </summary>
        [Display(Description = "Номер документа удостоверяющего личность")]
        public virtual string DocumentNumberEm { get; set; }

        /// <summary>
        ///     Информация о льготе
        /// </summary>
        [Display(Description = "Информация о льготе")]
        public virtual string BenefitTypeEm { get; set; }

        /// <summary>
        ///     Дата выдачи документа
        /// </summary>
        [Display(Description = "Когда выдан документ удостоверяющей личность")]
        public virtual string DocumentDateOfIssueEm { get; set; }

        /// <summary>
        ///     Кем выдан документ
        /// </summary>
        [Display(Description = "Кем выдан документ удостоверяющий личность")]
        public virtual string DocumentSubjectIssueEm { get; set; }

        /// <summary>
        ///     Телефон
        /// </summary>
        [Display(Description = "Телефон")]
        public virtual string PhoneEm { get; set; }

        /// <summary>
        ///     Электронная почта
        /// </summary>
        [Display(Description = "Электронная почта")]
        public virtual string EmailEm { get; set; }

        /// <summary>
        ///     Вид документа представителя
        /// </summary>
        [Display(Description = "Вид документа удостоверяющего личность")]
        public virtual string DocumentTypeEm { get; set; }


        /// <summary>
        ///     Дата рождения ребенка.
        /// </summary>
        [Display(Description = "Дата рождения")]
        public virtual string DateOfBirthEm { get; set; }

        /// <summary>
        ///     Тип ограничения
        /// </summary>
        [Display(Description =
            "Дополнительные сведения о ребенке, находящемся в трудной жизненной ситуации, вид ограничения")]
        public virtual string TypeOfRestrictionEm { get; set; }

        /// <summary>
        ///     Подвид ограничения
        /// </summary>
        [Display(Description =
            "Дополнительные сведения о ребенке, находящемся в трудной жизненной ситуации, подвид ограничения")]
        public virtual string TypeOfSubRestrictionEm { get; set; }

        /// <summary>
        ///     дата льготы
        /// </summary>
        [Display(Description = "Дата возникновения льготы")]
        public virtual string BenefitDateEm { get; set; }

        /// <summary>
        ///     Дата окончания действия.
        /// </summary>
        [Display(Description = "Дата окончания льготы")]
        public virtual string BenefitEndDateEm { get; set; }

        [Display(Description = "Кто выдал документ подтверждающего льготу")]
        public virtual string BenefitSubjectIssueEm { get; set; }

        [Display(Description = "Номер документа подтверждающего льготу")]
        public virtual string BenefitNumberEm { get; set; }

        [Display(Description = "Дата выдачи документа подтверждающего льготу")]
        public virtual string BenefitDateOfIssureEm { get; set; }

        [Display(Description = "Сопровождающий")]
        public virtual string ApplicantEm { get; set; }

        [Display(Description = "Сопровождающий, cтатус по отношению к ребёнку")]
        public virtual string StatusByChildEm { get; set; }

        [Display(Description = "Образовательное учреждение")]
        public virtual string SchoolEm { get; set; }

        [Display(Description = "Срок действия документа, удостоверяющего личность за рубежом")]
        public virtual string ForeginDateEndEm { get; set; }

        [Display(Description = "Серия документа, удостоверяющего личность за рубежом")]
        public virtual string ForeginSeriaEm { get; set; }

        [Display(Description = "Дата выдачи документа, удостоверяющего личность за рубежом")]
        public virtual string ForeginDateOfIssueEm { get; set; }

        [Display(Description = "Кем выдан документ, удостоверяющий личность за рубежом")]
        public virtual string ForeginSubjectIssueEm { get; set; }

        [Display(Description = "Тип документа, удостоверяющего личность за рубежом")]
        public virtual string ForeginTypeEm { get; set; }

        [Display(Description = "Номер документа, удостоверяющего личность за рубежом")]
        public virtual string ForeginNumberEm { get; set; }

        [Display(Description = "Адрес регистрации")]
        public virtual string AddressEm { get; set; }

        [Display(Description = "Место рождения")]
        public virtual string PlaceOfBirthEm { get; set; }

        [Display(Description = "Сопровождающий ребёнка")]
        public virtual string AttendantGuidEm { get; set; }

        [Display(Description = "Стоимость путевки (руб)")]
        public virtual string CostOfTourEm { get; set; }

        [Display(Description = "Сумма компенсации (руб)")]
        public virtual string AmountOfCompensationEm { get; set; }

        [Display(Description = "Стоимость проезда (руб)")]
        public virtual string CostOfRideEm { get; set; }

        [Display(Description = "Путевка")] public virtual string RequestInformationVoucherEm { get; set; }

        #endregion
    }
}
