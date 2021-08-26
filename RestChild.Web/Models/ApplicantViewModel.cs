using System;
using System.ComponentModel.DataAnnotations;
using RestChild.Comon.Enumeration;
using RestChild.Domain;
using RestChild.Web.Common;

namespace RestChild.Web.Models
{
   /// <summary>
   /// Модель заявителя
   /// </summary>
   public class ApplicantViewModel : ViewModelBase<Applicant>
   {
      public ApplicantViewModel() : base(new Applicant())
      {
         Address = new AddressViewModel(new Address());
         Guid = System.Guid.NewGuid();
      }

      public ApplicantViewModel(Applicant data) : base(data)
      {
         Address = new AddressViewModel(data.Address ?? new Address());
         Guid = System.Guid.NewGuid();
      }

      public AddressViewModel Address { get; set; }

      /// <summary>
      ///     Регион отдыха
      /// </summary>
      public PlaceOfRest MainPlace { get; set; }

      public PlaceOfRest SecondPlace { get; set; }

      /// <summary>
      ///     времена отдыха
      /// </summary>
      public TimeOfRest TimeOfRest { get; set; }


      public Guid? Guid { get; set; }

      /// <summary>
      ///     Нельзя удалить из заявления
      /// </summary>
      public bool? NotForDelete { get; set; }

      public bool HasNotMiddlename { get; set; }

      /// <summary>
      ///     модель заявления
      /// </summary>
      public RequestViewModel RequestModel { get; set; }

      public override Applicant BuildData()
      {
         Data.IsLast = true;
         Data.Address = Address?.BuildData();
         return base.BuildData();
      }

      public bool CheckModel(Request request, string action = null)
      {
         IsValid = true;
         //var startOfRest = TimeOfRest != null
         //	? new DateTime(TimeOfRest.Year, TimeOfRest.Month, TimeOfRest.DayOfMonth)
         //	: (DateTime?) null;

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

         if (string.IsNullOrWhiteSpace(Data.MiddleName) && !Data.HaveMiddleName)
         {
            IsValid = false;
            MiddleNameEm = RequaredField;
         }

         if (!Data.Male.HasValue)
         {
            IsValid = false;
            MaleEm = RequaredField;
         }

         if (Data.IsAccomp)
         {
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
         }

         if ((Data.Request == null || Data.Request.TypeOfRestId != (long) TypeOfRestEnum.CommercicalAddonRequest) &&
             string.IsNullOrWhiteSpace(Data.Phone))
         {
            IsValid = false;
            PhoneEm = RequaredField;
         }

         if (!Data.DocumentTypeId.HasValue)
         {
            IsValid = false;
            DocumentTypeEm = RequaredField;
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
               DocumentSeriaEm = "Введены некорректные данные";
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

         if (string.IsNullOrWhiteSpace(Data.Snils))
         {
         	IsValid = false;
         	SnilsEm = RequaredField;
         }

         //if (!Data.ApplicantTypeId.HasValue && Data.IsApplicant)
         //{
         //	IsValid = false;
         //	ApplicantTypeEm = RequaredField;
         //}

         //// документ для заграницы
         //if ((MainPlace?.IsForegin ?? false) && (bool) Data?.IsAccomp)
         //{
         //	if (!Data.ForeginDateEnd.HasValue)
         //	{
         //		IsValid = false;
         //		ForeginDateEndEm = RequaredField;
         //	}
         //	if (Data.ForeginDateOfIssue.HasValue &&
         //	    Data.ForeginDateOfIssue.Value > DateTime.Now)
         //	{
         //		IsValid = false;
         //		ForeginDateOfIssueEm = "Дата выдачи документа не может быть в будущем";
         //	}

         //	if (Data.ForeginDateEnd.HasValue && Data.ForeginDateOfIssue.HasValue &&
         //	    Data.ForeginDateOfIssue.Value > Data.ForeginDateEnd.Value)
         //	{
         //		IsValid = false;
         //		ForeginDateEndEm = "Дата окончания дейтсвия заграничного паспорта не может быть раньше даты выдачи";
         //	}

         //	if (Data.ForeginDateEnd.HasValue && startOfRest.HasValue &&
         //	    startOfRest.Value > Data.ForeginDateEnd.Value)
         //	{
         //		IsValid = false;
         //		ForeginDateEndEm =
         //			"Срок окочания действия загран паспорта не может быть меньше, чем дата заезда в организацию отдыха и оздоровления";
         //	}

         //	if (!Data.ForeginDateOfIssue.HasValue)
         //	{
         //		IsValid = false;
         //		ForeginDateOfIssueEm = RequaredField;
         //	}
         //	if (!Data.ForeginTypeId.HasValue)
         //	{
         //		IsValid = false;
         //		ForeginTypeEm = RequaredField;
         //	}
         //	if (string.IsNullOrWhiteSpace(Data.ForeginNumber))
         //	{
         //		IsValid = false;
         //		ForeginNumberEm = RequaredField;
         //	}
         //	if (string.IsNullOrWhiteSpace(Data.ForeginSeria))
         //	{
         //		IsValid = false;
         //		ForeginSeriaEm = RequaredField;
         //	}
         //	if (string.IsNullOrWhiteSpace(Data.ForeginSubjectIssue))
         //	{
         //		IsValid = false;
         //		ForeginSubjectIssueEm = RequaredField;
         //	}
         //}

         if (request != null && request.SourceId == (long) SourceEnum.Mpgu)
         {
            if (string.IsNullOrWhiteSpace(Data.Email))
            {
               IsValid = false;
               EmailEm = RequaredField;
            }
         }

         if (Data.DateOfBirth.HasValue)
         {
            var age = Data.GetAgeInYears(DateTime.Today);

            if (Data.GetAgeInYears(DateTime.Today.AddMonths(-1)) >= 14 && Data.DocumentTypeId.HasValue &&
                DocumentTypeHelper.IsBirthCert(Data.DocumentTypeId.Value))
            {
               if (Data.Request != null && Data.Request.TypeOfRestId == (long) TypeOfRestEnum.CommercicalAddonRequest)
               {
                  DocumentTypeEm = "Свидетельство о рождении не соответствует возрасту отдыхающего";
               }
               else
               {
                  DocumentTypeEm = "Свидетельство о рождении не соответствует возрасту сопровождающего";
               }

               IsValid = false;
            }

            if (age < 18 && !Data.RequestId.HasValue && !(RequestModel.Data.AgentApplicant ?? false))
            {
               DateOfBirthEm = "Заявителю не может быть меньше 18 лет";
               IsValid = false;
            }
            else if (age < 18 && Data.IsProxy)
            {
               DateOfBirthEm = "Сопровождающему с доверенностью не может быть меньше 18 лет";
               IsValid = false;
            }

            //if (Data.GetAgeInYears(DateTime.Today.AddMonths(1)) < 14 && Data.DocumentTypeId.HasValue && DocumentTypeHelper.IsPassport(Data.DocumentTypeId.Value))
            //{
            //	if (Data.Request != null && Data.Request.TypeOfRestId == (long)TypeOfRestEnum.CommercicalAddonRequest)
            //	{
            //		DocumentTypeEm = "Паспорт гражданина РФ не соответствует возрасту отдыхающего";
            //	}
            //	else
            //	{
            //		DocumentTypeEm = "Паспорт гражданина РФ не соответствует возрасту сопровождающего";
            //	}
            //	IsValid = false;
            //}
         }

         if (RequestModel != null && RequestModel.Data.TypeOfRestId == (long) TypeOfRestEnum.CommercicalAddonRequest &&
             !Data.OfferInRequestId.HasValue)
         {
            OfferInRequestEm = RequaredField;
            IsValid = false;
         }

         return IsValid.Value;
      }

      public override string ToString()
      {
         return $"{Data?.LastName} {Data?.FirstName} {Data?.MiddleName}";
      }

      #region Проверка формы

      [Display(Description = "Адрес регистрации")]
      public virtual string AddressEm { get; set; }

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
      ///     Пол
      /// </summary>
      [Display(Description = "Пол")]
      public virtual string MaleEm { get; set; }

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
      ///     Дата выдачи документа
      /// </summary>
      [Display(Description = "Когда выдан документ удостоверяющей личность")]
      public virtual string DocumentDateOfIssueEm { get; set; }

      /// <summary>
      ///     СНИЛС
      /// </summary>
      [Display(Description = "СНИЛС")]
      public virtual string SnilsEm { get; set; }

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
      public virtual string EmailEm { get; set; }

      /// <summary>
      ///     Вид документа представителя
      /// </summary>
      [Display(Description = "Вид документа удостоверяющего личность")]
      public virtual string DocumentTypeEm { get; set; }

      /// <summary>
      ///     Статус по отношению к ребенку
      /// </summary>
      [Display(Description = "Статус по отношению к ребёнку")]
      public virtual string ApplicantTypeEm { get; set; }


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

      [Display(Description = "Дата рождения")]
      public virtual string DateOfBirthEm { get; set; }

      [Display(Description = "Место рождения")]
      public virtual string PlaceOfBirthEm { get; set; }

      [Display(Description = "Размещение")] public string OfferInRequestEm { get; set; }

      /// <summary>
      /// Дата выдачи доверенности
      /// </summary>
      [Display(Description = "Дата выдачи доверенности")]
      public virtual string ProxyDateOfIssureEm { get; set; }

      /// <summary>
      /// Имя натариуса
      /// </summary>
      [Display(Description = "ФИО нотариуса")]
      public virtual string NotaryNameEm { get; set; }

      /// <summary>
      /// Дата окончания действия доверенности
      /// </summary>
      [Display(Description = "Дата окончания срока действия доверенности")]
      public virtual string ProxyEndDateEm { get; set; }

      /// <summary>
      /// Номер доверенности
      /// </summary>
      [Display(Description = "Номер доверенности")]
      public virtual string ProxyNumberEm { get; set; }

      #endregion
   }
}
