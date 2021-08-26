// File:    Child.cs
// Purpose: Definition of Class Child

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Ребёнок
   /// </summary>
   [Serializable]
   [DataContract(Name = "child")]
   public partial class Child : IEntityBase
   {
      
      /// <summary>
      /// Уникальный идентификатор
      /// </summary>
      [Display(Description = "Уникальный идентификатор")]
      [Required(ErrorMessage = "\"Уникальный идентификатор\" должно быть заполнено")]
      [Key]
      [DataMember(Name = "id", EmitDefaultValue = false)]
      public virtual long Id { get; set; }
      
      /// <summary>
      /// Фамилия
      /// </summary>
      [Display(Description = "Фамилия")]
      [MaxLength(1000, ErrorMessage = "\"Фамилия\" не может быть больше 1000 символов")]
      [DataMember(Name = "lastName", EmitDefaultValue = false)]
      public virtual string LastName { get; set; }
      
      /// <summary>
      /// Имя
      /// </summary>
      [Display(Description = "Имя")]
      [MaxLength(1000, ErrorMessage = "\"Имя\" не может быть больше 1000 символов")]
      [DataMember(Name = "firstName", EmitDefaultValue = false)]
      public virtual string FirstName { get; set; }
      
      /// <summary>
      /// Отчество
      /// </summary>
      [Display(Description = "Отчество")]
      [MaxLength(1000, ErrorMessage = "\"Отчество\" не может быть больше 1000 символов")]
      [DataMember(Name = "middleName", EmitDefaultValue = false)]
      public virtual string MiddleName { get; set; }
      
      /// <summary>
      /// Признак что есть отчество
      /// </summary>
      [Display(Description = "Признак что есть отчество")]
      [Required(ErrorMessage = "\"Признак что есть отчество\" должно быть заполнено")]
      [DataMember(Name = "haveMiddleName", EmitDefaultValue = false)]
      public virtual bool HaveMiddleName { get; set; }
      
      /// <summary>
      /// Серия документа
      /// </summary>
      [Display(Description = "Серия документа")]
      [MaxLength(1000, ErrorMessage = "\"Серия документа\" не может быть больше 1000 символов")]
      [DataMember(Name = "documentSeria", EmitDefaultValue = false)]
      public virtual string DocumentSeria { get; set; }
      
      /// <summary>
      /// Номер документа
      /// </summary>
      [Display(Description = "Номер документа")]
      [MaxLength(1000, ErrorMessage = "\"Номер документа\" не может быть больше 1000 символов")]
      [DataMember(Name = "documentNumber", EmitDefaultValue = false)]
      public virtual string DocumentNumber { get; set; }
      
      /// <summary>
      /// Дата выдачи документа
      /// </summary>
      [Display(Description = "Дата выдачи документа")]
      [DataMember(Name = "documentDateOfIssue", EmitDefaultValue = false)]
      public virtual DateTime? DocumentDateOfIssue { get; set; }
      
      /// <summary>
      /// Кем выдан документ
      /// </summary>
      [Display(Description = "Кем выдан документ")]
      [MaxLength(1000, ErrorMessage = "\"Кем выдан документ\" не может быть больше 1000 символов")]
      [DataMember(Name = "documentSubjectIssue", EmitDefaultValue = false)]
      public virtual string DocumentSubjectIssue { get; set; }
      
      /// <summary>
      /// Документ код подразделения
      /// </summary>
      [Display(Description = "Документ код подразделения")]
      [MaxLength(1000, ErrorMessage = "\"Документ код подразделения\" не может быть больше 1000 символов")]
      [DataMember(Name = "documentCode", EmitDefaultValue = false)]
      public virtual string DocumentCode { get; set; }
      
      /// <summary>
      /// Дата рождения
      /// </summary>
      [Display(Description = "Дата рождения")]
      [DataMember(Name = "dateOfBirth", EmitDefaultValue = false)]
      public virtual DateTime? DateOfBirth { get; set; }
      
      /// <summary>
      /// Дата возникновления льготы
      /// </summary>
      [Display(Description = "Дата возникновления льготы")]
      [DataMember(Name = "benefitDate", EmitDefaultValue = false)]
      public virtual DateTime? BenefitDate { get; set; }
      
      /// <summary>
      /// Признак что льгота бессрочная
      /// </summary>
      [Display(Description = "Признак что льгота бессрочная")]
      [Required(ErrorMessage = "\"Признак что льгота бессрочная\" должно быть заполнено")]
      [DataMember(Name = "benefitNeverEnd", EmitDefaultValue = false)]
      public virtual bool BenefitNeverEnd { get; set; }
      
      /// <summary>
      /// Дата окончания льготы
      /// </summary>
      [Display(Description = "Дата окончания льготы")]
      [DataMember(Name = "benefitEndDate", EmitDefaultValue = false)]
      public virtual DateTime? BenefitEndDate { get; set; }
      
      /// <summary>
      /// Номер документа льготы
      /// </summary>
      [Display(Description = "Номер документа льготы")]
      [MaxLength(1000, ErrorMessage = "\"Номер документа льготы\" не может быть больше 1000 символов")]
      [DataMember(Name = "benefitNumber", EmitDefaultValue = false)]
      public virtual string BenefitNumber { get; set; }
      
      /// <summary>
      /// Дата документа льготы
      /// </summary>
      [Display(Description = "Дата документа льготы")]
      [MaxLength(1000, ErrorMessage = "\"Дата документа льготы\" не может быть больше 1000 символов")]
      [DataMember(Name = "benefitSubjectIssue", EmitDefaultValue = false)]
      public virtual string BenefitSubjectIssue { get; set; }
      
      /// <summary>
      /// Дата выдачи документа льготы
      /// </summary>
      [Display(Description = "Дата выдачи документа льготы")]
      [DataMember(Name = "benefitDateOfIssure", EmitDefaultValue = false)]
      public virtual DateTime? BenefitDateOfIssure { get; set; }
      
      /// <summary>
      /// Серия документа з/п
      /// </summary>
      [Display(Description = "Серия документа з/п")]
      [MaxLength(1000, ErrorMessage = "\"Серия документа з/п\" не может быть больше 1000 символов")]
      [DataMember(Name = "foreginSeria", EmitDefaultValue = false)]
      public virtual string ForeginSeria { get; set; }
      
      /// <summary>
      /// Номер документа з/п
      /// </summary>
      [Display(Description = "Номер документа з/п")]
      [MaxLength(1000, ErrorMessage = "\"Номер документа з/п\" не может быть больше 1000 символов")]
      [DataMember(Name = "foreginNumber", EmitDefaultValue = false)]
      public virtual string ForeginNumber { get; set; }
      
      /// <summary>
      /// </summary>
      [Display(Description = "")]
      [DataMember(Name = "foreginSubjectIssue", EmitDefaultValue = false)]
      public virtual string ForeginSubjectIssue { get; set; }
      
      /// <summary>
      /// Дата выдачи документа з/п
      /// </summary>
      [Display(Description = "Дата выдачи документа з/п")]
      [DataMember(Name = "foreginDateOfIssue", EmitDefaultValue = false)]
      public virtual DateTime? ForeginDateOfIssue { get; set; }
      
      /// <summary>
      /// Срок действия загран паспорта
      /// </summary>
      [Display(Description = "Срок действия загран паспорта")]
      [DataMember(Name = "foreginDateEnd", EmitDefaultValue = false)]
      public virtual DateTime? ForeginDateEnd { get; set; }
      
      /// <summary>
      /// Школы нет в списке
      /// </summary>
      [Display(Description = "Школы нет в списке")]
      [Required(ErrorMessage = "\"Школы нет в списке\" должно быть заполнено")]
      [DataMember(Name = "schoolNotPresent", EmitDefaultValue = false)]
      public virtual bool SchoolNotPresent { get; set; }
      
      /// <summary>
      /// Ребенок зарегистрирован по месту жительства в Москве
      /// </summary>
      [Display(Description = "Ребенок зарегистрирован по месту жительства в Москве")]
      [Required(ErrorMessage = "\"Ребенок зарегистрирован по месту жительства в Москве\" должно быть заполнено")]
      [DataMember(Name = "registeredInMoscow", EmitDefaultValue = false)]
      public virtual bool RegisteredInMoscow { get; set; }
      
      /// <summary>
      /// Мужской пол
      /// </summary>
      [Display(Description = "Мужской пол")]
      [Required(ErrorMessage = "\"Мужской пол\" должно быть заполнено")]
      [DataMember(Name = "male", EmitDefaultValue = false)]
      public virtual bool Male { get; set; }
      
      /// <summary>
      /// Сведения о льготе подтверждены в БР
      /// </summary>
      [Display(Description = "Сведения о льготе подтверждены в БР")]
      [Required(ErrorMessage = "\"Сведения о льготе подтверждены в БР\" должно быть заполнено")]
      [DataMember(Name = "benefitApprove", EmitDefaultValue = false)]
      public virtual bool BenefitApprove { get; set; }
      
      /// <summary>
      /// Дата запроса по льготе в БР
      /// </summary>
      [Display(Description = "Дата запроса по льготе в БР")]
      [DataMember(Name = "benefitApproveRequestDate", EmitDefaultValue = false)]
      public virtual DateTime? BenefitApproveRequestDate { get; set; }
      
      /// <summary>
      /// Комментарий по подтверждению БР
      /// </summary>
      [Display(Description = "Комментарий по подтверждению БР")]
      [DataMember(Name = "benefitApproveComment", EmitDefaultValue = false)]
      public virtual String BenefitApproveComment { get; set; }
      
      /// <summary>
      /// Описание подтверждения БР
      /// </summary>
      [Display(Description = "Описание подтверждения БР")]
      [DataMember(Name = "benefitApproveHtml", EmitDefaultValue = false)]
      public virtual String BenefitApproveHtml { get; set; }
      
      /// <summary>
      /// Номер межвед запроса по льготе
      /// </summary>
      [Display(Description = "Номер межвед запроса по льготе")]
      [MaxLength(1000, ErrorMessage = "\"Номер межвед запроса по льготе\" не может быть больше 1000 символов")]
      [DataMember(Name = "benefitRequestNumber", EmitDefaultValue = false)]
      public virtual string BenefitRequestNumber { get; set; }
      
      /// <summary>
      /// Дата межвед запроса по льготе
      /// </summary>
      [Display(Description = "Дата межвед запроса по льготе")]
      [DataMember(Name = "benefitRequestDate", EmitDefaultValue = false)]
      public virtual DateTime? BenefitRequestDate { get; set; }
      
      /// <summary>
      /// Номер ответа по льготе
      /// </summary>
      [Display(Description = "Номер ответа по льготе")]
      [MaxLength(1000, ErrorMessage = "\"Номер ответа по льготе\" не может быть больше 1000 символов")]
      [DataMember(Name = "benefitAnswerNumber", EmitDefaultValue = false)]
      public virtual string BenefitAnswerNumber { get; set; }
      
      /// <summary>
      /// Дата ответа по льготе
      /// </summary>
      [Display(Description = "Дата ответа по льготе")]
      [DataMember(Name = "benefitAnswerDate", EmitDefaultValue = false)]
      public virtual DateTime? BenefitAnswerDate { get; set; }
      
      /// <summary>
      /// Комментарий по ответу по льготе
      /// </summary>
      [Display(Description = "Комментарий по ответу по льготе")]
      [DataMember(Name = "benefitAnswerComment", EmitDefaultValue = false)]
      public virtual String BenefitAnswerComment { get; set; }
      
      /// <summary>
      /// Снилс
      /// </summary>
      [Display(Description = "Снилс")]
      [MaxLength(1000, ErrorMessage = "\"Снилс\" не может быть больше 1000 символов")]
      [DataMember(Name = "snils", EmitDefaultValue = false)]
      public virtual string Snils { get; set; }
      
      /// <summary>
      /// Комментарий по запросу по льготе
      /// </summary>
      [Display(Description = "Комментарий по запросу по льготе")]
      [DataMember(Name = "benefitRequestComment", EmitDefaultValue = false)]
      public virtual String BenefitRequestComment { get; set; }
      
      /// <summary>
      /// Включен в межвед запрос
      /// </summary>
      [Display(Description = "Включен в межвед запрос")]
      [Required(ErrorMessage = "\"Включен в межвед запрос\" должно быть заполнено")]
      [DataMember(Name = "isIncludeInInteragency", EmitDefaultValue = false)]
      public virtual bool IsIncludeInInteragency { get; set; }
      
      /// <summary>
      /// Согласован в межвед запросе
      /// </summary>
      [Display(Description = "Согласован в межвед запросе")]
      [DataMember(Name = "isApprovedInInteragency", EmitDefaultValue = false)]
      public virtual bool? IsApprovedInInteragency { get; set; }
      
      /// <summary>
      /// </summary>
      [Display(Description = "")]
      [Required(ErrorMessage = "\"\" должно быть заполнено")]
      [DataMember(Name = "isIncludeInInteragencySecondary", EmitDefaultValue = false)]
      public virtual bool IsIncludeInInteragencySecondary { get; set; }
      
      /// <summary>
      /// </summary>
      [Display(Description = "")]
      [DataMember(Name = "isApprovedInInteragencySecondary", EmitDefaultValue = false)]
      public virtual bool? IsApprovedInInteragencySecondary { get; set; }
      
      /// <summary>
      /// </summary>
      [Display(Description = "")]
      [Required(ErrorMessage = "\"\" должно быть заполнено")]
      [DataMember(Name = "isInvalid", EmitDefaultValue = false)]
      public virtual bool IsInvalid { get; set; }
      
      /// <summary>
      /// Ключ по ребенку
      /// </summary>
      [Display(Description = "Ключ по ребенку")]
      [MaxLength(1000, ErrorMessage = "\"Ключ по ребенку\" не может быть больше 1000 символов")]
      [DataMember(Name = "key", EmitDefaultValue = false)]
      public virtual string Key { get; set; }
      
      /// <summary>
      /// Начало интервала
      /// </summary>
      [Display(Description = "Начало интервала")]
      [DataMember(Name = "intervalStart", EmitDefaultValue = false)]
      public virtual long? IntervalStart { get; set; }
      
      /// <summary>
      /// Окончание интервала
      /// </summary>
      [Display(Description = "Окончание интервала")]
      [DataMember(Name = "intervalEnd", EmitDefaultValue = false)]
      public virtual long? IntervalEnd { get; set; }
      
      /// <summary>
      /// Ключ похожие
      /// </summary>
      [Display(Description = "Ключ похожие")]
      [MaxLength(1000, ErrorMessage = "\"Ключ похожие\" не может быть больше 1000 символов")]
      [DataMember(Name = "keySame", EmitDefaultValue = false)]
      public virtual string KeySame { get; set; }
      
      /// <summary>
      /// Последний
      /// </summary>
      [Display(Description = "Последний")]
      [Required(ErrorMessage = "\"Последний\" должно быть заполнено")]
      [DataMember(Name = "isLast", EmitDefaultValue = false)]
      public virtual bool IsLast { get; set; }
      
      /// <summary>
      /// Квитанция об оплате Url
      /// </summary>
      [Display(Description = "Квитанция об оплате Url")]
      [MaxLength(1000, ErrorMessage = "\"Квитанция об оплате Url\" не может быть больше 1000 символов")]
      [DataMember(Name = "paymentFileUrl", EmitDefaultValue = false)]
      public virtual string PaymentFileUrl { get; set; }
      
      /// <summary>
      /// Квитанция об оплате имя
      /// </summary>
      [Display(Description = "Квитанция об оплате имя")]
      [MaxLength(1000, ErrorMessage = "\"Квитанция об оплате имя\" не может быть больше 1000 символов")]
      [DataMember(Name = "paymentFileTitle", EmitDefaultValue = false)]
      public virtual string PaymentFileTitle { get; set; }
      
      /// <summary>
      /// Документ подтверждающий льготу Url
      /// </summary>
      [Display(Description = "Документ подтверждающий льготу Url")]
      [MaxLength(1000, ErrorMessage = "\"Документ подтверждающий льготу Url\" не может быть больше 1000 символов")]
      [DataMember(Name = "documentFileUrl", EmitDefaultValue = false)]
      public virtual string DocumentFileUrl { get; set; }
      
      /// <summary>
      /// Документ подтверждающий льготу имя
      /// </summary>
      [Display(Description = "Документ подтверждающий льготу имя")]
      [DataMember(Name = "documentFileTitle", EmitDefaultValue = false)]
      public virtual String DocumentFileTitle { get; set; }
      
      /// <summary>
      /// Признак что удален
      /// </summary>
      [Display(Description = "Признак что удален")]
      [Required(ErrorMessage = "\"Признак что удален\" должно быть заполнено")]
      [DataMember(Name = "isDeleted", EmitDefaultValue = false)]
      public virtual bool IsDeleted { get; set; }
      
      /// <summary>
      /// Контактный телефон
      /// </summary>
      [Display(Description = "Контактный телефон")]
      [MaxLength(1000, ErrorMessage = "\"Контактный телефон\" не может быть больше 1000 символов")]
      [DataMember(Name = "contactPhone", EmitDefaultValue = false)]
      public virtual string ContactPhone { get; set; }
      
      /// <summary>
      /// Фамилия представителя
      /// </summary>
      [Display(Description = "Фамилия представителя")]
      [MaxLength(1000, ErrorMessage = "\"Фамилия представителя\" не может быть больше 1000 символов")]
      [DataMember(Name = "contactLastName", EmitDefaultValue = false)]
      public virtual string ContactLastName { get; set; }
      
      /// <summary>
      /// Имя представителя
      /// </summary>
      [Display(Description = "Имя представителя")]
      [MaxLength(1000, ErrorMessage = "\"Имя представителя\" не может быть больше 1000 символов")]
      [DataMember(Name = "contactFirstName", EmitDefaultValue = false)]
      public virtual string ContactFirstName { get; set; }
      
      /// <summary>
      /// Отчество представителя
      /// </summary>
      [Display(Description = "Отчество представителя")]
      [MaxLength(1000, ErrorMessage = "\"Отчество представителя\" не может быть больше 1000 символов")]
      [DataMember(Name = "contactMiddleName", EmitDefaultValue = false)]
      public virtual string ContactMiddleName { get; set; }
      
      /// <summary>
      /// Признак что есть отчество у представителя
      /// </summary>
      [Display(Description = "Признак что есть отчество у представителя")]
      [Required(ErrorMessage = "\"Признак что есть отчество у представителя\" должно быть заполнено")]
      [DataMember(Name = "contactHaveMiddleName", EmitDefaultValue = false)]
      public virtual bool ContactHaveMiddleName { get; set; }
      
      /// <summary>
      /// Место рождения
      /// </summary>
      [Display(Description = "Место рождения")]
      [MaxLength(1000, ErrorMessage = "\"Место рождения\" не может быть больше 1000 символов")]
      [DataMember(Name = "placeOfBirth", EmitDefaultValue = false)]
      public virtual string PlaceOfBirth { get; set; }
      
      /// <summary>
      /// Не нужен билет туда
      /// </summary>
      [Display(Description = "Не нужен билет туда")]
      [Required(ErrorMessage = "\"Не нужен билет туда\" должно быть заполнено")]
      [DataMember(Name = "notNeedTicketForward", EmitDefaultValue = false)]
      public virtual bool NotNeedTicketForward { get; set; }
      
      /// <summary>
      /// Не нужен билет обратно
      /// </summary>
      [Display(Description = "Не нужен билет обратно")]
      [Required(ErrorMessage = "\"Не нужен билет обратно\" должно быть заполнено")]
      [DataMember(Name = "notNeedTicketBackward", EmitDefaultValue = false)]
      public virtual bool NotNeedTicketBackward { get; set; }
      
      /// <summary>
      /// Оплачено
      /// </summary>
      [Display(Description = "Оплачено")]
      [Required(ErrorMessage = "\"Оплачено\" должно быть заполнено")]
      [DataMember(Name = "payed", EmitDefaultValue = false)]
      public virtual bool Payed { get; set; }
      
      /// <summary>
      /// Год компании
      /// </summary>
      [Display(Description = "Год компании")]
      [DataMember(Name = "yearOfCompany", EmitDefaultValue = false)]
      public virtual int? YearOfCompany { get; set; }
      
      /// <summary>
      /// Не явился в место отдыха
      /// </summary>
      [Display(Description = "Не явился в место отдыха")]
      [Required(ErrorMessage = "\"Не явился в место отдыха\" должно быть заполнено")]
      [DataMember(Name = "notComeInPlaceOfRest", EmitDefaultValue = false)]
      public virtual bool NotComeInPlaceOfRest { get; set; }
      
      /// <summary>
      /// Серия свидетельства о рождении
      /// </summary>
      [Display(Description = "Серия свидетельства о рождении")]
      [MaxLength(1000, ErrorMessage = "\"Серия свидетельства о рождении\" не может быть больше 1000 символов")]
      [DataMember(Name = "documentSeriaCertOfBirth", EmitDefaultValue = false)]
      public virtual string DocumentSeriaCertOfBirth { get; set; }
      
      /// <summary>
      /// Номер свидетельтсва о рождении
      /// </summary>
      [Display(Description = "Номер свидетельтсва о рождении")]
      [MaxLength(1000, ErrorMessage = "\"Номер свидетельтсва о рождении\" не может быть больше 1000 символов")]
      [DataMember(Name = "documentNumberCertOfBirth", EmitDefaultValue = false)]
      public virtual string DocumentNumberCertOfBirth { get; set; }
      
      /// <summary>
      /// Ключ для поиска похожих с свидетельством о рождении
      /// </summary>
      [Display(Description = "Ключ для поиска похожих с свидетельством о рождении")]
      [MaxLength(1000, ErrorMessage = "\"Ключ для поиска похожих с свидетельством о рождении\" не может быть больше 1000 символов")]
      [DataMember(Name = "keyOther", EmitDefaultValue = false)]
      public virtual string KeyOther { get; set; }
      
      /// <summary>
      /// Загран паспорт имя
      /// </summary>
      [Display(Description = "Загран паспорт имя")]
      [MaxLength(1000, ErrorMessage = "\"Загран паспорт имя\" не может быть больше 1000 символов")]
      [DataMember(Name = "foreginName", EmitDefaultValue = false)]
      public virtual string ForeginName { get; set; }
      
      /// <summary>
      /// Загран паспрт фамилия
      /// </summary>
      [Display(Description = "Загран паспрт фамилия")]
      [MaxLength(1000, ErrorMessage = "\"Загран паспрт фамилия\" не может быть больше 1000 символов")]
      [DataMember(Name = "foreginLastName", EmitDefaultValue = false)]
      public virtual string ForeginLastName { get; set; }
      
      /// <summary>
      /// Размер компенсации
      /// </summary>
      [Display(Description = "Размер компенсации")]
      [DataMember(Name = "amountOfCompensation", EmitDefaultValue = false)]
      public virtual decimal? AmountOfCompensation { get; set; }
      
      /// <summary>
      /// Стоимость проезда ребенка
      /// </summary>
      [Display(Description = "Стоимость проезда ребенка")]
      [DataMember(Name = "costOfRide", EmitDefaultValue = false)]
      public virtual decimal? CostOfRide { get; set; }
      
      /// <summary>
      /// Стоимость путевки
      /// </summary>
      [Display(Description = "Стоимость путевки")]
      [DataMember(Name = "costOfTour", EmitDefaultValue = false)]
      public virtual decimal? CostOfTour { get; set; }
      
      /// <summary>
      /// Иднетификатор ребёнка в контингенте
      /// </summary>
      [Display(Description = "Иднетификатор ребёнка в контингенте")]
      [DataMember(Name = "contingentGuid", EmitDefaultValue = false)]
      public virtual Guid? ContingentGuid { get; set; }
      
      /// <summary>
      /// Идентификатор ЕКИС
      /// </summary>
      [Display(Description = "Идентификатор ЕКИС")]
      [DataMember(Name = "ekisId", EmitDefaultValue = false)]
      public virtual long? EkisId { get; set; }
      
      /// <summary>
      /// Нужно выгрузить ЕКИС
      /// </summary>
      [Display(Description = "Нужно выгрузить ЕКИС")]
      [Required(ErrorMessage = "\"Нужно выгрузить ЕКИС\" должно быть заполнено")]
      [DataMember(Name = "ekisNeedSend", EmitDefaultValue = false)]
      public virtual bool EkisNeedSend { get; set; }
      
      /// <summary>
      /// ИНН
      /// </summary>
      [Display(Description = "ИНН")]
      [MaxLength(1000, ErrorMessage = "\"ИНН\" не может быть больше 1000 символов")]
      [DataMember(Name = "inn", EmitDefaultValue = false)]
      public virtual string Inn { get; set; }
      
      /// <summary>
      /// Младенец
      /// </summary>
      [Display(Description = "Младенец")]
      [Required(ErrorMessage = "\"Младенец\" должно быть заполнено")]
      [DataMember(Name = "infant", EmitDefaultValue = false)]
      public virtual bool Infant { get; set; }
      
      
      /// <summary>
      /// Ребёнок <-> Родитель
      /// </summary>
      [InverseProperty("Child")]
      [DataMember(Name = "relatives", EmitDefaultValue = false)]
      public virtual ICollection<Relative> Relatives { get; set; }
      
      /// <summary>
      /// Обмен с базовым регистром
      /// </summary>
      [InverseProperty("Child")]
      [DataMember(Name = "baseRegistryInfo", EmitDefaultValue = false)]
      public virtual ICollection<ExchangeBaseRegistry> BaseRegistryInfo { get; set; }
      
      /// <summary>
      /// Ребёнок
      /// </summary>
      [InverseProperty("Child")]
      [DataMember(Name = "linkToPeoples", EmitDefaultValue = false)]
      public virtual ICollection<LinkToPeople> LinkToPeoples { get; set; }
      
      /// <summary>
      /// Ребёнок
      /// </summary>
      [InverseProperty("Children")]
      [DataMember(Name = "calculations", EmitDefaultValue = false)]
      public virtual ICollection<Calculation> Calculations { get; set; }
      
      /// <summary>
      /// Связь ребенка со статусом ЕРЛ
      /// </summary>
      [InverseProperty("Child")]
      [DataMember(Name = "eRLPersons", EmitDefaultValue = false)]
      public virtual ICollection<ERLPersonStatus> ERLPersons { get; set; }
      
      /// <summary>
      /// Ребенок -> Воспитанник
      /// </summary>
      [InverseProperty("Child")]
      [DataMember(Name = "pupils", EmitDefaultValue = false)]
      public virtual ICollection<Pupil> Pupils { get; set; }
      
      /// <summary>
      /// Дети
      /// </summary>
      [ForeignKey("Request")]
      [DataMember(Name = "requestId")]
      [Display(Description = "Дети")]
      public virtual long? RequestId { get; set; }
      
      
      /// <summary>
      /// Дети
      /// </summary>
      [InverseProperty("Child")]
      [Display(Description = "Дети")]
      [DataMember(Name = "request", EmitDefaultValue = false)]
      public virtual Request Request { get; set; }
      
      /// <summary>
      /// Ссылка на образовательное учреждение
      /// </summary>
      [ForeignKey("School")]
      [DataMember(Name = "schoolId")]
      [Display(Description = "Ссылка на образовательное учреждение")]
      public virtual long? SchoolId { get; set; }
      
      
      /// <summary>
      /// Ссылка на образовательное учреждение
      /// </summary>
      [Display(Description = "Ссылка на образовательное учреждение")]
      [DataMember(Name = "school", EmitDefaultValue = false)]
      public virtual School School { get; set; }
      
      /// <summary>
      /// Вид документа ребенка
      /// </summary>
      [ForeignKey("DocumentType")]
      [DataMember(Name = "documentTypeId")]
      [Display(Description = "Вид документа ребенка")]
      public virtual long? DocumentTypeId { get; set; }
      
      
      /// <summary>
      /// Вид документа ребенка
      /// </summary>
      [Display(Description = "Вид документа ребенка")]
      [DataMember(Name = "documentType", EmitDefaultValue = false)]
      public virtual DocumentType DocumentType { get; set; }
      
      /// <summary>
      /// Вид льготы
      /// </summary>
      [ForeignKey("BenefitType")]
      [DataMember(Name = "benefitTypeId")]
      [Display(Description = "Вид льготы")]
      public virtual long? BenefitTypeId { get; set; }
      
      
      /// <summary>
      /// Вид льготы
      /// </summary>
      [Display(Description = "Вид льготы")]
      [DataMember(Name = "benefitType", EmitDefaultValue = false)]
      public virtual BenefitType BenefitType { get; set; }
      
      /// <summary>
      /// Адрес регистрации
      /// </summary>
      [ForeignKey("Address")]
      [DataMember(Name = "addressId")]
      [Display(Description = "Адрес регистрации")]
      public virtual long? AddressId { get; set; }
      
      
      /// <summary>
      /// Адрес регистрации
      /// </summary>
      [Display(Description = "Адрес регистрации")]
      [DataMember(Name = "address", EmitDefaultValue = false)]
      public virtual Address Address { get; set; }
      
      /// <summary>
      /// Вид документа подтверждающего льготу
      /// </summary>
      [ForeignKey("BenefitDocType")]
      [DataMember(Name = "benefitDocTypeId")]
      [Display(Description = "Вид документа подтверждающего льготу")]
      public virtual long? BenefitDocTypeId { get; set; }
      
      
      /// <summary>
      /// Вид документа подтверждающего льготу
      /// </summary>
      [Display(Description = "Вид документа подтверждающего льготу")]
      [DataMember(Name = "benefitDocType", EmitDefaultValue = false)]
      public virtual DocumentType BenefitDocType { get; set; }
      
      /// <summary>
      /// Вид документ удостоверяющего личность за рубежом
      /// </summary>
      [ForeignKey("ForeginType")]
      [DataMember(Name = "foreginTypeId")]
      [Display(Description = "Вид документ удостоверяющего личность за рубежом")]
      public virtual long? ForeginTypeId { get; set; }
      
      
      /// <summary>
      /// Вид документ удостоверяющего личность за рубежом
      /// </summary>
      [Display(Description = "Вид документ удостоверяющего личность за рубежом")]
      [DataMember(Name = "foreginType", EmitDefaultValue = false)]
      public virtual DocumentType ForeginType { get; set; }
      
      /// <summary>
      /// Статус по отношению к ребенку
      /// </summary>
      [ForeignKey("StatusByChild")]
      [DataMember(Name = "statusByChildId")]
      [Display(Description = "Статус по отношению к ребенку")]
      public virtual long? StatusByChildId { get; set; }
      
      
      /// <summary>
      /// Статус по отношению к ребенку
      /// </summary>
      [Display(Description = "Статус по отношению к ребенку")]
      [DataMember(Name = "statusByChild", EmitDefaultValue = false)]
      public virtual StatusByChild StatusByChild { get; set; }
      
      /// <summary>
      /// Сопровождающий ребенка
      /// </summary>
      [ForeignKey("Applicant")]
      [DataMember(Name = "applicantId")]
      [Display(Description = "Сопровождающий ребенка")]
      public virtual long? ApplicantId { get; set; }
      
      
      /// <summary>
      /// Сопровождающий ребенка
      /// </summary>
      [InverseProperty("ChildAttendant")]
      [Display(Description = "Сопровождающий ребенка")]
      [DataMember(Name = "applicant", EmitDefaultValue = false)]
      public virtual Applicant Applicant { get; set; }
      
      /// <summary>
      /// Связь версий
      /// </summary>
      [ForeignKey("Entity")]
      [DataMember(Name = "entityId")]
      [Display(Description = "Связь версий")]
      public virtual long? EntityId { get; set; }
      
      
      /// <summary>
      /// Связь версий
      /// </summary>
      [Display(Description = "Связь версий")]
      [DataMember(Name = "entity", EmitDefaultValue = false)]
      public virtual Child Entity { get; set; }
      
      /// <summary>
      /// Тип подтверждения льготы
      /// </summary>
      [ForeignKey("BenefitApproveType")]
      [DataMember(Name = "benefitApproveTypeId")]
      [Display(Description = "Тип подтверждения льготы")]
      public virtual long? BenefitApproveTypeId { get; set; }
      
      
      /// <summary>
      /// Тип подтверждения льготы
      /// </summary>
      [Display(Description = "Тип подтверждения льготы")]
      [DataMember(Name = "benefitApproveType", EmitDefaultValue = false)]
      public virtual BenefitApproveType BenefitApproveType { get; set; }
      
      /// <summary>
      /// Вид ограничения
      /// </summary>
      [ForeignKey("TypeOfRestriction")]
      [DataMember(Name = "typeOfRestrictionId")]
      [Display(Description = "Вид ограничения")]
      public virtual long? TypeOfRestrictionId { get; set; }
      
      
      /// <summary>
      /// Вид ограничения
      /// </summary>
      [Display(Description = "Вид ограничения")]
      [DataMember(Name = "typeOfRestriction", EmitDefaultValue = false)]
      public virtual TypeOfRestriction TypeOfRestriction { get; set; }
      
      /// <summary>
      /// Группа инвалидности
      /// </summary>
      [ForeignKey("BenefitGroupInvalid")]
      [DataMember(Name = "benefitGroupInvalidId")]
      [Display(Description = "Группа инвалидности")]
      public virtual long? BenefitGroupInvalidId { get; set; }
      
      
      /// <summary>
      /// Группа инвалидности
      /// </summary>
      [Display(Description = "Группа инвалидности")]
      [DataMember(Name = "benefitGroupInvalid", EmitDefaultValue = false)]
      public virtual BenefitGroupInvalid BenefitGroupInvalid { get; set; }
      
      /// <summary>
      /// Вид группы проверки
      /// </summary>
      [ForeignKey("TypeOfGroupCheck")]
      [DataMember(Name = "typeOfGroupCheckId")]
      [Display(Description = "Вид группы проверки")]
      public virtual long? TypeOfGroupCheckId { get; set; }
      
      
      /// <summary>
      /// Вид группы проверки
      /// </summary>
      [Display(Description = "Вид группы проверки")]
      [DataMember(Name = "typeOfGroupCheck", EmitDefaultValue = false)]
      public virtual TypeOfGroupCheck TypeOfGroupCheck { get; set; }
      
      /// <summary>
      /// Вид свидетельства о рождении
      /// </summary>
      [ForeignKey("DocumentTypeCertOfBirth")]
      [DataMember(Name = "documentTypeCertOfBirthId")]
      [Display(Description = "Вид свидетельства о рождении")]
      public virtual long? DocumentTypeCertOfBirthId { get; set; }
      
      
      /// <summary>
      /// Вид свидетельства о рождении
      /// </summary>
      [Display(Description = "Вид свидетельства о рождении")]
      [DataMember(Name = "documentTypeCertOfBirth", EmitDefaultValue = false)]
      public virtual DocumentType DocumentTypeCertOfBirth { get; set; }
      
      /// <summary>
      /// Вид нарушения
      /// </summary>
      [ForeignKey("TypeViolation")]
      [DataMember(Name = "typeViolationId")]
      [Display(Description = "Вид нарушения")]
      public virtual long? TypeViolationId { get; set; }
      
      
      /// <summary>
      /// Вид нарушения
      /// </summary>
      [Display(Description = "Вид нарушения")]
      [DataMember(Name = "typeViolation", EmitDefaultValue = false)]
      public virtual TypeViolation TypeViolation { get; set; }
      
      /// <summary>
      /// Подвид ограничения
      /// </summary>
      [ForeignKey("TypeOfSubRestriction")]
      [DataMember(Name = "typeOfSubRestrictionId")]
      [Display(Description = "Подвид ограничения ")]
      public virtual long? TypeOfSubRestrictionId { get; set; }
      
      
      /// <summary>
      /// Подвид ограничения
      /// </summary>
      [Display(Description = "Подвид ограничения ")]
      [DataMember(Name = "typeOfSubRestriction", EmitDefaultValue = false)]
      public virtual TypeOfSubRestriction TypeOfSubRestriction { get; set; }
      
      /// <summary>
      /// Связь списка и детей
      /// </summary>
      [ForeignKey("ChildList")]
      [DataMember(Name = "childListId")]
      [Display(Description = "Связь списка и детей")]
      public virtual long? ChildListId { get; set; }
      
      
      /// <summary>
      /// Связь списка и детей
      /// </summary>
      [InverseProperty("Childs")]
      [Display(Description = "Связь списка и детей")]
      [DataMember(Name = "childList", EmitDefaultValue = false)]
      public virtual ListOfChilds ChildList { get; set; }
      
      /// <summary>
      /// Причина исключения
      /// </summary>
      [ForeignKey("ExcludeReason")]
      [DataMember(Name = "excludeReasonId")]
      [Display(Description = "Причина исключения")]
      public virtual long? ExcludeReasonId { get; set; }
      
      
      /// <summary>
      /// Причина исключения
      /// </summary>
      [Display(Description = "Причина исключения")]
      [DataMember(Name = "excludeReason", EmitDefaultValue = false)]
      public virtual ChildIncludeExcludeReason ExcludeReason { get; set; }
      
      /// <summary>
      /// Причина добавления
      /// </summary>
      [ForeignKey("IncludeReason")]
      [DataMember(Name = "includeReasonId")]
      [Display(Description = "Причина добавления")]
      public virtual long? IncludeReasonId { get; set; }
      
      
      /// <summary>
      /// Причина добавления
      /// </summary>
      [Display(Description = "Причина добавления")]
      [DataMember(Name = "includeReason", EmitDefaultValue = false)]
      public virtual ChildIncludeExcludeReason IncludeReason { get; set; }
      
      /// <summary>
      /// Расположение по номерам
      /// </summary>
      [ForeignKey("TourVolume")]
      [DataMember(Name = "tourVolumeId")]
      [Display(Description = "Расположение по номерам")]
      public virtual long? TourVolumeId { get; set; }
      
      
      /// <summary>
      /// Расположение по номерам
      /// </summary>
      [Display(Description = "Расположение по номерам")]
      [DataMember(Name = "tourVolume", EmitDefaultValue = false)]
      public virtual TourVolume TourVolume { get; set; }
      
      /// <summary>
      /// Дети из отряда
      /// </summary>
      [ForeignKey("Party")]
      [DataMember(Name = "partyId")]
      [Display(Description = "Дети из отряда")]
      public virtual long? PartyId { get; set; }
      
      
      /// <summary>
      /// Дети из отряда
      /// </summary>
      [Display(Description = "Дети из отряда")]
      [DataMember(Name = "party", EmitDefaultValue = false)]
      public virtual Party Party { get; set; }
      
      /// <summary>
      /// Дети
      /// </summary>
      [ForeignKey("Bout")]
      [DataMember(Name = "boutId")]
      [Display(Description = "Дети")]
      public virtual long? BoutId { get; set; }
      
      
      /// <summary>
      /// Дети
      /// </summary>
      [InverseProperty("Chidren")]
      [Display(Description = "Дети")]
      [DataMember(Name = "bout", EmitDefaultValue = false)]
      public virtual Bout Bout { get; set; }
      
      /// <summary>
      /// Размещение
      /// </summary>
      [ForeignKey("OfferInRequest")]
      [DataMember(Name = "offerInRequestId")]
      [Display(Description = "Размещение")]
      public virtual long? OfferInRequestId { get; set; }
      
      
      /// <summary>
      /// Размещение
      /// </summary>
      [InverseProperty("Children")]
      [Display(Description = "Размещение")]
      [DataMember(Name = "offerInRequest", EmitDefaultValue = false)]
      public virtual OfferInRequest OfferInRequest { get; set; }
      
      /// <summary>
      /// Продукты <-> Дети
      /// </summary>
      [ForeignKey("Tours")]
      [DataMember(Name = "toursId")]
      [Display(Description = "Продукты <-> Дети")]
      public virtual long? ToursId { get; set; }
      
      
      /// <summary>
      /// Продукты <-> Дети
      /// </summary>
      [InverseProperty("TourChilds")]
      [Display(Description = "Продукты <-> Дети")]
      [DataMember(Name = "tours", EmitDefaultValue = false)]
      public virtual Tour Tours { get; set; }
      
      /// <summary>
      /// Гражданство
      /// </summary>
      [ForeignKey("Country")]
      [DataMember(Name = "countryId")]
      [Display(Description = "Гражданство")]
      public virtual long? CountryId { get; set; }
      
      
      /// <summary>
      /// Гражданство
      /// </summary>
      [Display(Description = "Гражданство")]
      [DataMember(Name = "country", EmitDefaultValue = false)]
      public virtual Country Country { get; set; }
      
      /// <summary>
      /// Сведения о путевках для компенсации
      /// </summary>
      [ForeignKey("RequestInformationVoucher")]
      [DataMember(Name = "requestInformationVoucherId")]
      [Display(Description = "Сведения о путевках для компенсации")]
      public virtual long? RequestInformationVoucherId { get; set; }
      
      
      /// <summary>
      /// Сведения о путевках для компенсации
      /// </summary>
      [Display(Description = "Сведения о путевках для компенсации")]
      [DataMember(Name = "requestInformationVoucher", EmitDefaultValue = false)]
      public virtual RequestInformationVoucher RequestInformationVoucher { get; set; }
      
      /// <summary>
      /// Уникальный ребёнок
      /// </summary>
      [ForeignKey("ChildUniqe")]
      [DataMember(Name = "childUniqeId")]
      [Display(Description = "Уникальный ребёнок")]
      public virtual long? ChildUniqeId { get; set; }
      
      
      /// <summary>
      /// Уникальный ребёнок
      /// </summary>
      [InverseProperty("Children")]
      [Display(Description = "Уникальный ребёнок")]
      [DataMember(Name = "childUniqe", EmitDefaultValue = false)]
      public virtual ChildUniqe ChildUniqe { get; set; }

      /// <summary>
      /// Последнее сохранение
      /// </summary>
      [Display(Description = "Последнее сохранение")]
      [DataMember(Name = "lastUpdateTick", EmitDefaultValue = false)]
      public virtual long LastUpdateTick { get; set; }
      
      /// <summary>
      /// Внешний ключ
      /// </summary>
      [Display(Description = "Внешний ключ")]
      [DataMember(Name = "eid", EmitDefaultValue = false)]
      [Index]
      public virtual long? Eid { get; set; }      

      /// <summary>
      /// Статус обмена по сущности
      /// </summary>
      [Display(Description = "Статус обмена по сущности")]
      [DataMember(Name = "eidSendStatus", EmitDefaultValue = false)]
      [Index]
      public virtual long? EidSendStatus { get; set; }      

      /// <summary>
      /// Дата синхронизации
      /// </summary>
      [Display(Description = "Дата синхронизации")]
      [DataMember(Name = "eidSyncDate", EmitDefaultValue = false)]
      public virtual DateTime? EidSyncDate { get; set; }      
   }
}