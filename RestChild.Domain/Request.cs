// File:    Request.cs
// Purpose: Definition of Class Request

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Заявление
   /// </summary>
   [Serializable]
   [DataContract(Name = "request")]
   public partial class Request : IEntityBase
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
      /// Кем подается заявление
      /// </summary>
      [Display(Description = "Кем подается заявление")]
      [DataMember(Name = "agentApplicant", EmitDefaultValue = false)]
      public virtual bool? AgentApplicant { get; set; }
      
      /// <summary>
      /// Номер заявления
      /// </summary>
      [Display(Description = "Номер заявления")]
      [MaxLength(1000, ErrorMessage = "\"Номер заявления\" не может быть больше 1000 символов")]
      [DataMember(Name = "requestNumber", EmitDefaultValue = false)]
      public virtual string RequestNumber { get; set; }
      
      /// <summary>
      /// Дата заявления
      /// </summary>
      [Display(Description = "Дата заявления")]
      [DataMember(Name = "dateRequest", EmitDefaultValue = false)]
      public virtual DateTime? DateRequest { get; set; }
      
      /// <summary>
      /// Дата обновления заявления
      /// </summary>
      [Display(Description = "Дата обновления заявления")]
      [DataMember(Name = "updateDate", EmitDefaultValue = false)]
      public virtual DateTime? UpdateDate { get; set; }
      
      /// <summary>
      /// Признак последней версии
      /// </summary>
      [Display(Description = "Признак последней версии")]
      [Required(ErrorMessage = "\"Признак последней версии\" должно быть заполнено")]
      [DataMember(Name = "isLast", EmitDefaultValue = false)]
      public virtual bool IsLast { get; set; }
      
      /// <summary>
      /// Признак что удален
      /// </summary>
      [Display(Description = "Признак что удален")]
      [Required(ErrorMessage = "\"Признак что удален\" должно быть заполнено")]
      [DataMember(Name = "isDeleted", EmitDefaultValue = false)]
      public virtual bool IsDeleted { get; set; }
      
      /// <summary>
      /// Номер версии
      /// </summary>
      [Display(Description = "Номер версии")]
      [DataMember(Name = "version", EmitDefaultValue = false)]
      public virtual int? Version { get; set; }
      
      /// <summary>
      /// Внешний ИД
      /// </summary>
      [Display(Description = "Внешний ИД")]
      [MaxLength(1000, ErrorMessage = "\"Внешний ИД\" не может быть больше 1000 символов")]
      [DataMember(Name = "externalUid", EmitDefaultValue = false)]
      public virtual string ExternalUid { get; set; }
      
      /// <summary>
      /// Внешняя система
      /// </summary>
      [Display(Description = "Внешняя система")]
      [MaxLength(1000, ErrorMessage = "\"Внешняя система\" не может быть больше 1000 символов")]
      [DataMember(Name = "externalSystem", EmitDefaultValue = false)]
      public virtual string ExternalSystem { get; set; }
      
      /// <summary>
      /// Количество основных мест
      /// </summary>
      [Display(Description = "Количество основных мест")]
      [DataMember(Name = "mainPlaces", EmitDefaultValue = false)]
      public virtual int? MainPlaces { get; set; }
      
      /// <summary>
      /// Количество дополнительных мест
      /// </summary>
      [Display(Description = "Количество дополнительных мест")]
      [DataMember(Name = "additionalPlaces", EmitDefaultValue = false)]
      public virtual int? AdditionalPlaces { get; set; }
      
      /// <summary>
      /// Черновик
      /// </summary>
      [Display(Description = "Черновик")]
      [Required(ErrorMessage = "\"Черновик\" должно быть заполнено")]
      [DataMember(Name = "isDraft", EmitDefaultValue = false)]
      public virtual bool IsDraft { get; set; }
      
      /// <summary>
      /// Номер заявления МПГУ
      /// </summary>
      [Display(Description = "Номер заявления МПГУ")]
      [MaxLength(1000, ErrorMessage = "\"Номер заявления МПГУ\" не может быть больше 1000 символов")]
      [DataMember(Name = "requestNumberMpgu", EmitDefaultValue = false)]
      public virtual string RequestNumberMpgu { get; set; }
      
      /// <summary>
      /// необходимо отправлять уведомления по Email
      /// </summary>
      [Display(Description = "необходимо отправлять уведомления по Email")]
      [Required(ErrorMessage = "\"необходимо отправлять уведомления по Email\" должно быть заполнено")]
      [DataMember(Name = "needEmail", EmitDefaultValue = false)]
      public virtual bool NeedEmail { get; set; }
      
      /// <summary>
      /// необходимо отправлять уведомления по Sms
      /// </summary>
      [Display(Description = "необходимо отправлять уведомления по Sms")]
      [Required(ErrorMessage = "\"необходимо отправлять уведомления по Sms\" должно быть заполнено")]
      [DataMember(Name = "needSms", EmitDefaultValue = false)]
      public virtual bool NeedSms { get; set; }
      
      /// <summary>
      /// Дата изменения статуса заявления
      /// </summary>
      [Display(Description = "Дата изменения статуса заявления")]
      [DataMember(Name = "dateChangeStatus", EmitDefaultValue = false)]
      public virtual DateTime? DateChangeStatus { get; set; }
      
      /// <summary>
      /// Идентификатор бронирования
      /// </summary>
      [Display(Description = "Идентификатор бронирования")]
      [DataMember(Name = "bookingGuid", EmitDefaultValue = false)]
      public virtual Guid? BookingGuid { get; set; }
      
      /// <summary>
      /// Количество детей
      /// </summary>
      [Display(Description = "Количество детей")]
      [Required(ErrorMessage = "\"Количество детей\" должно быть заполнено")]
      [DataMember(Name = "countPlace", EmitDefaultValue = false)]
      public virtual int CountPlace { get; set; }
      
      /// <summary>
      /// Количество сопровождающих
      /// </summary>
      [Display(Description = "Количество сопровождающих")]
      [Required(ErrorMessage = "\"Количество сопровождающих\" должно быть заполнено")]
      [DataMember(Name = "countAttendants", EmitDefaultValue = false)]
      public virtual int CountAttendants { get; set; }
      
      /// <summary>
      /// Номер сертификата
      /// </summary>
      [Display(Description = "Номер сертификата")]
      [MaxLength(1000, ErrorMessage = "\"Номер сертификата\" не может быть больше 1000 символов")]
      [DataMember(Name = "certificateNumber", EmitDefaultValue = false)]
      public virtual string CertificateNumber { get; set; }
      
      /// <summary>
      /// Дата генерации сертификата
      /// </summary>
      [Display(Description = "Дата генерации сертификата")]
      [DataMember(Name = "certificateDate", EmitDefaultValue = false)]
      public virtual DateTime? CertificateDate { get; set; }
      
      /// <summary>
      /// Стоимость
      /// </summary>
      [Display(Description = "Стоимость")]
      [DataMember(Name = "price", EmitDefaultValue = false)]
      public virtual decimal? Price { get; set; }
      
      /// <summary>
      /// Заезд с
      /// </summary>
      [Display(Description = "Заезд с")]
      [DataMember(Name = "dateIncome", EmitDefaultValue = false)]
      public virtual DateTime? DateIncome { get; set; }
      
      /// <summary>
      /// Заезд по
      /// </summary>
      [Display(Description = "Заезд по")]
      [DataMember(Name = "dateOutcome", EmitDefaultValue = false)]
      public virtual DateTime? DateOutcome { get; set; }
      
      /// <summary>
      /// Комментарий
      /// </summary>
      [Display(Description = "Комментарий")]
      [DataMember(Name = "commentary", EmitDefaultValue = false)]
      public virtual String Commentary { get; set; }
      
      /// <summary>
      /// Наименование банка
      /// </summary>
      [Display(Description = "Наименование банка")]
      [MaxLength(1000, ErrorMessage = "\"Наименование банка\" не может быть больше 1000 символов")]
      [DataMember(Name = "bankName", EmitDefaultValue = false)]
      public virtual string BankName { get; set; }
      
      /// <summary>
      /// БИК банка
      /// </summary>
      [Display(Description = "БИК банка")]
      [MaxLength(1000, ErrorMessage = "\"БИК банка\" не может быть больше 1000 символов")]
      [DataMember(Name = "bankBik", EmitDefaultValue = false)]
      public virtual string BankBik { get; set; }
      
      /// <summary>
      /// ИНН банка
      /// </summary>
      [Display(Description = "ИНН банка")]
      [MaxLength(1000, ErrorMessage = "\"ИНН банка\" не может быть больше 1000 символов")]
      [DataMember(Name = "bankInn", EmitDefaultValue = false)]
      public virtual string BankInn { get; set; }
      
      /// <summary>
      /// КПП банка
      /// </summary>
      [Display(Description = "КПП банка")]
      [MaxLength(1000, ErrorMessage = "\"КПП банка\" не может быть больше 1000 символов")]
      [DataMember(Name = "bankKpp", EmitDefaultValue = false)]
      public virtual string BankKpp { get; set; }
      
      /// <summary>
      /// Номер карты
      /// </summary>
      [Display(Description = "Номер карты")]
      [MaxLength(1000, ErrorMessage = "\"Номер карты\" не может быть больше 1000 символов")]
      [DataMember(Name = "bankCardNumber", EmitDefaultValue = false)]
      public virtual string BankCardNumber { get; set; }
      
      /// <summary>
      /// Корр счет
      /// </summary>
      [Display(Description = "Корр счет")]
      [MaxLength(1000, ErrorMessage = "\"Корр счет\" не может быть больше 1000 символов")]
      [DataMember(Name = "bankCorr", EmitDefaultValue = false)]
      public virtual string BankCorr { get; set; }
      
      /// <summary>
      /// Получатель платежа фамилия
      /// </summary>
      [Display(Description = "Получатель платежа фамилия")]
      [MaxLength(1000, ErrorMessage = "\"Получатель платежа фамилия\" не может быть больше 1000 символов")]
      [DataMember(Name = "bankLastName", EmitDefaultValue = false)]
      public virtual string BankLastName { get; set; }
      
      /// <summary>
      /// Получатель платежа имя
      /// </summary>
      [Display(Description = "Получатель платежа имя")]
      [MaxLength(1000, ErrorMessage = "\"Получатель платежа имя\" не может быть больше 1000 символов")]
      [DataMember(Name = "bankFirstName", EmitDefaultValue = false)]
      public virtual string BankFirstName { get; set; }
      
      /// <summary>
      /// Получатель платежа отчество
      /// </summary>
      [Display(Description = "Получатель платежа отчество")]
      [MaxLength(1000, ErrorMessage = "\"Получатель платежа отчество\" не может быть больше 1000 символов")]
      [DataMember(Name = "bankMiddleName", EmitDefaultValue = false)]
      public virtual string BankMiddleName { get; set; }
      
      /// <summary>
      /// Лицевой счет
      /// </summary>
      [Display(Description = "Лицевой счет")]
      [MaxLength(1000, ErrorMessage = "\"Лицевой счет\" не может быть больше 1000 символов")]
      [DataMember(Name = "bankAccount", EmitDefaultValue = false)]
      public virtual string BankAccount { get; set; }
      
      /// <summary>
      /// Стоимость внутренняя
      /// </summary>
      [Display(Description = "Стоимость внутренняя")]
      [DataMember(Name = "priceInternal", EmitDefaultValue = false)]
      public virtual decimal? PriceInternal { get; set; }
      
      /// <summary>
      /// Номер дисконтной карты
      /// </summary>
      [Display(Description = "Номер дисконтной карты")]
      [MaxLength(1000, ErrorMessage = "\"Номер дисконтной карты\" не может быть больше 1000 символов")]
      [DataMember(Name = "discountCardNumber", EmitDefaultValue = false)]
      public virtual string DiscountCardNumber { get; set; }
      
      /// <summary>
      /// Процент скидки
      /// </summary>
      [Display(Description = "Процент скидки")]
      [DataMember(Name = "discountProcent", EmitDefaultValue = false)]
      public virtual decimal? DiscountProcent { get; set; }
      
      /// <summary>
      /// Для индексации
      /// </summary>
      [Display(Description = "Для индексации")]
      [Required(ErrorMessage = "\"Для индексации\" должно быть заполнено")]
      [DataMember(Name = "forIndex", EmitDefaultValue = false)]
      public virtual bool ForIndex { get; set; }
      
      /// <summary>
      /// Пользователь сайта
      /// </summary>
      [Display(Description = "Пользователь сайта")]
      [MaxLength(1000, ErrorMessage = "\"Пользователь сайта\" не может быть больше 1000 символов")]
      [DataMember(Name = "siteUser", EmitDefaultValue = false)]
      public virtual string SiteUser { get; set; }
      
      /// <summary>
      /// Согласие на изменение заявления по сканам
      /// </summary>
      [Display(Description = "Согласие на изменение заявления по сканам")]
      [Required(ErrorMessage = "\"Согласие на изменение заявления по сканам\" должно быть заполнено")]
      [DataMember(Name = "changeByScan", EmitDefaultValue = false)]
      public virtual bool ChangeByScan { get; set; }
      
      /// <summary>
      /// Процент предоплаты
      /// </summary>
      [Display(Description = "Процент предоплаты")]
      [DataMember(Name = "procentPrepaid", EmitDefaultValue = false)]
      public virtual decimal? ProcentPrepaid { get; set; }
      
      /// <summary>
      /// Процент надбавки
      /// </summary>
      [Display(Description = "Процент надбавки")]
      [DataMember(Name = "procentOver", EmitDefaultValue = false)]
      public virtual decimal? ProcentOver { get; set; }
      
      /// <summary>
      /// Начисление на каждого человека
      /// </summary>
      [Display(Description = "Начисление на каждого человека")]
      [Required(ErrorMessage = "\"Начисление на каждого человека\" должно быть заполнено")]
      [DataMember(Name = "calculationOnPerson", EmitDefaultValue = false)]
      public virtual bool CalculationOnPerson { get; set; }
      
      /// <summary>
      /// Внутренний комментарий
      /// </summary>
      [Display(Description = "Внутренний комментарий")]
      [DataMember(Name = "internalCommentary", EmitDefaultValue = false)]
      public virtual String InternalCommentary { get; set; }
      
      /// <summary>
      /// Заявитель организация
      /// </summary>
      [Display(Description = "Заявитель организация")]
      [Required(ErrorMessage = "\"Заявитель организация\" должно быть заполнено")]
      [DataMember(Name = "isApplicantOrganization", EmitDefaultValue = false)]
      public virtual bool IsApplicantOrganization { get; set; }
      
      /// <summary>
      /// Заявление в первую заявочную
      /// </summary>
      [Display(Description = "Заявление в первую заявочную")]
      [Required(ErrorMessage = "\"Заявление в первую заявочную\" должно быть заполнено")]
      [DataMember(Name = "isFirstCompany", EmitDefaultValue = false)]
      public virtual bool IsFirstCompany { get; set; }
      
      /// <summary>
      /// Заявление на деньги
      /// </summary>
      [Display(Description = "Заявление на деньги")]
      [Required(ErrorMessage = "\"Заявление на деньги\" должно быть заполнено")]
      [DataMember(Name = "requestOnMoney", EmitDefaultValue = false)]
      public virtual bool RequestOnMoney { get; set; }
      
      /// <summary>
      /// Нужно отправить запрос на льготу
      /// </summary>
      [Display(Description = "Нужно отправить запрос на льготу")]
      [Required(ErrorMessage = "\"Нужно отправить запрос на льготу\" должно быть заполнено")]
      [DataMember(Name = "needSendForBenefit", EmitDefaultValue = false)]
      public virtual bool NeedSendForBenefit { get; set; }
      
      /// <summary>
      /// Нужно отправить запрос на родство
      /// </summary>
      [Display(Description = "Нужно отправить запрос на родство")]
      [Required(ErrorMessage = "\"Нужно отправить запрос на родство\" должно быть заполнено")]
      [DataMember(Name = "needSendToRelative", EmitDefaultValue = false)]
      public virtual bool NeedSendToRelative { get; set; }
      
      /// <summary>
      /// SsoId МПГУ
      /// </summary>
      [Display(Description = "SsoId МПГУ")]
      [MaxLength(1000, ErrorMessage = "\"SsoId МПГУ\" не может быть больше 1000 символов")]
      [DataMember(Name = "ssoId", EmitDefaultValue = false)]
      public virtual string SsoId { get; set; }
      
      /// <summary>
      /// Статус того кто подает заявлени
      /// </summary>
      [Display(Description = "Статус того кто подает заявлени")]
      [MaxLength(1000, ErrorMessage = "\"Статус того кто подает заявлени\" не может быть больше 1000 символов")]
      [DataMember(Name = "statusApplicant", EmitDefaultValue = false)]
      public virtual string StatusApplicant { get; set; }
      
      /// <summary>
      /// Можно отправить финальный статус
      /// </summary>
      [Display(Description = "Можно отправить финальный статус")]
      [Required(ErrorMessage = "\"Можно отправить финальный статус\" должно быть заполнено")]
      [DataMember(Name = "mayFinalSend", EmitDefaultValue = false)]
      public virtual bool MayFinalSend { get; set; }
      
      /// <summary>
      /// Восcтановлено
      /// </summary>
      [Display(Description = "Восcтановлено")]
      [Required(ErrorMessage = "\"Восcтановлено\" должно быть заполнено")]
      [DataMember(Name = "repared", EmitDefaultValue = false)]
      public virtual bool Repared { get; set; }
      
      /// <summary>
      /// Заявление на отказ в приеме
      /// </summary>
      [Display(Description = "Заявление на отказ в приеме")]
      [Required(ErrorMessage = "\"Заявление на отказ в приеме\" должно быть заполнено")]
      [DataMember(Name = "refusalOfAdmission", EmitDefaultValue = false)]
      public virtual bool RefusalOfAdmission { get; set; }
      
      /// <summary>
      /// Нужно отправить запрос на СНИЛС
      /// </summary>
      [Display(Description = "Нужно отправить запрос на СНИЛС")]
      [Required(ErrorMessage = "\"Нужно отправить запрос на СНИЛС\" должно быть заполнено")]
      [DataMember(Name = "needSendForSnils", EmitDefaultValue = false)]
      public virtual bool NeedSendForSnils { get; set; }
      
      /// <summary>
      /// Нужно отправить запрос на ЦПМПК
      /// </summary>
      [Display(Description = "Нужно отправить запрос на ЦПМПК")]
      [Required(ErrorMessage = "\"Нужно отправить запрос на ЦПМПК\" должно быть заполнено")]
      [DataMember(Name = "needSendForCPMPK", EmitDefaultValue = false)]
      public virtual bool NeedSendForCPMPK { get; set; }
      
      /// <summary>
      /// Нужно отправить запрос на законного представителя
      /// </summary>
      [Display(Description = "Нужно отправить запрос на законного представителя")]
      [Required(ErrorMessage = "\"Нужно отправить запрос на законного представителя\" должно быть заполнено")]
      [DataMember(Name = "needSendForParent", EmitDefaultValue = false)]
      public virtual bool NeedSendForParent { get; set; }
      
      /// <summary>
      /// Нужно отправить запрос на паспорт
      /// </summary>
      [Display(Description = "Нужно отправить запрос на паспорт")]
      [Required(ErrorMessage = "\"Нужно отправить запрос на паспорт\" должно быть заполнено")]
      [DataMember(Name = "needSendForPassport", EmitDefaultValue = false)]
      public virtual bool NeedSendForPassport { get; set; }
      
      /// <summary>
      /// Нужно отправить проверку регистрации по паспорту
      /// </summary>
      [Display(Description = "Нужно отправить проверку регистрации по паспорту")]
      [Required(ErrorMessage = "\"Нужно отправить проверку регистрации по паспорту\" должно быть заполнено")]
      [DataMember(Name = "needSendForRegistrationByPassport", EmitDefaultValue = false)]
      public virtual bool NeedSendForRegistrationByPassport { get; set; }
      
      /// <summary>
      /// Нужно отправить на проверку законного представительства внутри АИС ДО
      /// </summary>
      [Display(Description = "Нужно отправить на проверку законного представительства внутри АИС ДО")]
      [Required(ErrorMessage = "\"Нужно отправить на проверку законного представительства внутри АИС ДО\" должно быть заполнено")]
      [DataMember(Name = "needSendForAisoLegalRepresentation", EmitDefaultValue = false)]
      public virtual bool NeedSendForAisoLegalRepresentation { get; set; }
      
      /// <summary>
      /// Нужно отправить на проверку сведений об инвалидности в ФРИ
      /// </summary>
      [Display(Description = "Нужно отправить на проверку сведений об инвалидности в ФРИ")]
      [Required(ErrorMessage = "\"Нужно отправить на проверку сведений об инвалидности в ФРИ\" должно быть заполнено")]
      [DataMember(Name = "needSendForFRI", EmitDefaultValue = false)]
      public virtual bool NeedSendForFRI { get; set; }
      
      
      /// <summary>
      /// Дети
      /// </summary>
      [InverseProperty("Request")]
      [DataMember(Name = "child", EmitDefaultValue = false)]
      public virtual ICollection<Child> Child { get; set; }
      
      /// <summary>
      /// Сопровождающие лица
      /// </summary>
      [InverseProperty("Request")]
      [DataMember(Name = "attendant", EmitDefaultValue = false)]
      public virtual ICollection<Applicant> Attendant { get; set; }
      
      /// <summary>
      /// Файлы
      /// </summary>
      [InverseProperty("Request")]
      [DataMember(Name = "files", EmitDefaultValue = false)]
      public virtual ICollection<RequestFile> Files { get; set; }
      
      /// <summary>
      /// связь погашения сертификата и заявления
      /// </summary>
      [InverseProperty("Request")]
      [DataMember(Name = "certificates", EmitDefaultValue = false)]
      public virtual ICollection<Certificate> Certificates { get; set; }
      
      /// <summary>
      /// Связь заявления с местом отдыха
      /// </summary>
      [InverseProperty("Request")]
      [DataMember(Name = "placesOfRest", EmitDefaultValue = false)]
      public virtual ICollection<RequestPlaceOfRest> PlacesOfRest { get; set; }
      
      /// <summary>
      /// Связь заявления с временем отдыха
      /// </summary>
      [InverseProperty("Request")]
      [DataMember(Name = "timesOfRest", EmitDefaultValue = false)]
      public virtual ICollection<RequestsTimeOfRest> TimesOfRest { get; set; }
      
      /// <summary>
      /// Бронирования
      /// </summary>
      [InverseProperty("Request")]
      [DataMember(Name = "bookingsCom", EmitDefaultValue = false)]
      public virtual ICollection<BookingCommercial> BookingsCom { get; set; }
      
      /// <summary>
      /// Заявление на дополнительное место
      /// </summary>
      [InverseProperty("ParentRequest")]
      [DataMember(Name = "addonRequests", EmitDefaultValue = false)]
      public virtual ICollection<Request> AddonRequests { get; set; }
      
      /// <summary>
      /// Заявление связь с услугами
      /// </summary>
      [InverseProperty("Request")]
      [DataMember(Name = "addonServicesLinks", EmitDefaultValue = false)]
      public virtual ICollection<AddonServicesLink> AddonServicesLinks { get; set; }
      
      /// <summary>
      /// Проживания
      /// </summary>
      [InverseProperty("Request")]
      [DataMember(Name = "requestAccommodations", EmitDefaultValue = false)]
      public virtual ICollection<RequestAccommodation> RequestAccommodations { get; set; }
      
      /// <summary>
      /// Заявление
      /// </summary>
      [InverseProperty("Request")]
      [DataMember(Name = "calculations", EmitDefaultValue = false)]
      public virtual ICollection<Calculation> Calculations { get; set; }
      
      /// <summary>
      /// Билет
      /// </summary>
      [InverseProperty("Request")]
      [DataMember(Name = "tickets", EmitDefaultValue = false)]
      public virtual ICollection<Ticket> Tickets { get; set; }
      
      /// <summary>
      /// Сведения о путевках для компенсации
      /// </summary>
      [InverseProperty("Request")]
      [DataMember(Name = "informationVouchers", EmitDefaultValue = false)]
      public virtual ICollection<RequestInformationVoucher> InformationVouchers { get; set; }
      
      /// <summary>
      /// Выбранные услуги в заявке
      /// </summary>
      [InverseProperty("Request")]
      [DataMember(Name = "requestServices", EmitDefaultValue = false)]
      public virtual ICollection<RequestService> RequestServices { get; set; }
      
      /// <summary>
      /// Статус
      /// </summary>
      [ForeignKey("Status")]
      [DataMember(Name = "statusId")]
      [Display(Description = "Статус")]
      public virtual long? StatusId { get; set; }
      
      
      /// <summary>
      /// Статус
      /// </summary>
      [Display(Description = "Статус")]
      [DataMember(Name = "status", EmitDefaultValue = false)]
      public virtual Status Status { get; set; }
      
      /// <summary>
      /// Заявитель
      /// </summary>
      [ForeignKey("Applicant")]
      [DataMember(Name = "applicantId")]
      [Display(Description = "Заявитель")]
      public virtual long? ApplicantId { get; set; }
      
      
      /// <summary>
      /// Заявитель
      /// </summary>
      [Display(Description = "Заявитель")]
      [DataMember(Name = "applicant", EmitDefaultValue = false)]
      public virtual Applicant Applicant { get; set; }
      
      /// <summary>
      /// Вид отдыха
      /// </summary>
      [ForeignKey("TypeOfRest")]
      [DataMember(Name = "typeOfRestId")]
      [Display(Description = "Вид отдыха")]
      public virtual long? TypeOfRestId { get; set; }
      
      
      /// <summary>
      /// Вид отдыха
      /// </summary>
      [Display(Description = "Вид отдыха")]
      [DataMember(Name = "typeOfRest", EmitDefaultValue = false)]
      public virtual TypeOfRest TypeOfRest { get; set; }
      
      /// <summary>
      /// Время отдыха
      /// </summary>
      [ForeignKey("TimeOfRest")]
      [DataMember(Name = "timeOfRestId")]
      [Display(Description = "Время отдыха")]
      public virtual long? TimeOfRestId { get; set; }
      
      
      /// <summary>
      /// Время отдыха
      /// </summary>
      [Display(Description = "Время отдыха")]
      [DataMember(Name = "timeOfRest", EmitDefaultValue = false)]
      public virtual TimeOfRest TimeOfRest { get; set; }
      
      /// <summary>
      /// Тематика смены
      /// </summary>
      [ForeignKey("SubjectOfRest")]
      [DataMember(Name = "subjectOfRestId")]
      [Display(Description = "Тематика смены")]
      public virtual long? SubjectOfRestId { get; set; }
      
      
      /// <summary>
      /// Тематика смены
      /// </summary>
      [Display(Description = "Тематика смены")]
      [DataMember(Name = "subjectOfRest", EmitDefaultValue = false)]
      public virtual SubjectOfRest SubjectOfRest { get; set; }
      
      /// <summary>
      /// Тип сопровождения ребенка
      /// </summary>
      [ForeignKey("AttendantType")]
      [DataMember(Name = "attendantTypeId")]
      [Display(Description = "Тип сопровождения ребенка")]
      public virtual long? AttendantTypeId { get; set; }
      
      
      /// <summary>
      /// Тип сопровождения ребенка
      /// </summary>
      [Display(Description = "Тип сопровождения ребенка")]
      [DataMember(Name = "attendantType", EmitDefaultValue = false)]
      public virtual AttendantType AttendantType { get; set; }
      
      /// <summary>
      /// Место отдыха
      /// </summary>
      [ForeignKey("PlaceOfRest")]
      [DataMember(Name = "placeOfRestId")]
      [Display(Description = "Место отдыха")]
      public virtual long? PlaceOfRestId { get; set; }
      
      
      /// <summary>
      /// Место отдыха
      /// </summary>
      [Display(Description = "Место отдыха")]
      [DataMember(Name = "placeOfRest", EmitDefaultValue = false)]
      public virtual PlaceOfRest PlaceOfRest { get; set; }
      
      /// <summary>
      /// Представитель
      /// </summary>
      [ForeignKey("Agent")]
      [DataMember(Name = "agentId")]
      [Display(Description = "Представитель")]
      public virtual long? AgentId { get; set; }
      
      
      /// <summary>
      /// Представитель
      /// </summary>
      [Display(Description = "Представитель")]
      [DataMember(Name = "agent", EmitDefaultValue = false)]
      public virtual Agent Agent { get; set; }
      
      /// <summary>
      /// Ссылка на первую версию заявления
      /// </summary>
      [ForeignKey("Entity")]
      [DataMember(Name = "entityId")]
      [Display(Description = "Ссылка на первую версию заявления")]
      public virtual long? EntityId { get; set; }
      
      
      /// <summary>
      /// Ссылка на первую версию заявления
      /// </summary>
      [Display(Description = "Ссылка на первую версию заявления")]
      [DataMember(Name = "entity", EmitDefaultValue = false)]
      public virtual Request Entity { get; set; }
      
      /// <summary>
      /// Источник заявления
      /// </summary>
      [ForeignKey("Source")]
      [DataMember(Name = "sourceId")]
      [Display(Description = "Источник заявления")]
      public virtual long? SourceId { get; set; }
      
      
      /// <summary>
      /// Источник заявления
      /// </summary>
      [Display(Description = "Источник заявления")]
      [DataMember(Name = "source", EmitDefaultValue = false)]
      public virtual Source Source { get; set; }
      
      /// <summary>
      /// Причина отказа
      /// </summary>
      [ForeignKey("DeclineReason")]
      [DataMember(Name = "declineReasonId")]
      [Display(Description = "Причина отказа")]
      public virtual long? DeclineReasonId { get; set; }
      
      
      /// <summary>
      /// Причина отказа
      /// </summary>
      [Display(Description = "Причина отказа")]
      [DataMember(Name = "declineReason", EmitDefaultValue = false)]
      public virtual DeclineReason DeclineReason { get; set; }
      
      /// <summary>
      /// Куратор
      /// </summary>
      [ForeignKey("Curator")]
      [DataMember(Name = "curatorId")]
      [Display(Description = "Куратор")]
      public virtual long? CuratorId { get; set; }
      
      
      /// <summary>
      /// Куратор
      /// </summary>
      [Display(Description = "Куратор")]
      [DataMember(Name = "curator", EmitDefaultValue = false)]
      public virtual Account Curator { get; set; }
      
      /// <summary>
      /// Кто получил льготу
      /// </summary>
      [ForeignKey("Beneficiaries")]
      [DataMember(Name = "beneficiariesId")]
      [Display(Description = "Кто получил льготу")]
      public virtual long? BeneficiariesId { get; set; }
      
      
      /// <summary>
      /// Кто получил льготу
      /// </summary>
      [Display(Description = "Кто получил льготу")]
      [DataMember(Name = "beneficiaries", EmitDefaultValue = false)]
      public virtual Beneficiaries Beneficiaries { get; set; }
      
      /// <summary>
      /// История
      /// </summary>
      [ForeignKey("HistoryLink")]
      [DataMember(Name = "historyLinkId")]
      [Display(Description = "История")]
      public virtual long? HistoryLinkId { get; set; }
      
      
      /// <summary>
      /// История
      /// </summary>
      [Display(Description = "История")]
      [DataMember(Name = "historyLink", EmitDefaultValue = false)]
      public virtual HistoryLink HistoryLink { get; set; }
      
      /// <summary>
      /// Представляет интересы
      /// </summary>
      [ForeignKey("RepresentInterest")]
      [DataMember(Name = "representInterestId")]
      [Display(Description = "Представляет интересы")]
      public virtual long? RepresentInterestId { get; set; }
      
      
      /// <summary>
      /// Представляет интересы
      /// </summary>
      [Display(Description = "Представляет интересы")]
      [DataMember(Name = "representInterest", EmitDefaultValue = false)]
      public virtual RepresentInterest RepresentInterest { get; set; }
      
      /// <summary>
      /// Трансфер из места отдыха
      /// </summary>
      [ForeignKey("TransferFrom")]
      [DataMember(Name = "transferFromId")]
      [Display(Description = "Трансфер из места отдыха")]
      public virtual long? TransferFromId { get; set; }
      
      
      /// <summary>
      /// Трансфер из места отдыха
      /// </summary>
      [Display(Description = "Трансфер из места отдыха")]
      [DataMember(Name = "transferFrom", EmitDefaultValue = false)]
      public virtual TypeOfTransfer TransferFrom { get; set; }
      
      /// <summary>
      /// В место отдыха
      /// </summary>
      [ForeignKey("TransferTo")]
      [DataMember(Name = "transferToId")]
      [Display(Description = "В место отдыха")]
      public virtual long? TransferToId { get; set; }
      
      
      /// <summary>
      /// В место отдыха
      /// </summary>
      [Display(Description = "В место отдыха")]
      [DataMember(Name = "transferTo", EmitDefaultValue = false)]
      public virtual TypeOfTransfer TransferTo { get; set; }
      
      /// <summary>
      /// Приоритетный вид транспорта
      /// </summary>
      [ForeignKey("PriorityTypeOfTransportInRequest")]
      [DataMember(Name = "priorityTypeOfTransportInRequestId")]
      [Display(Description = "Приоритетный вид транспорта")]
      public virtual long? PriorityTypeOfTransportInRequestId { get; set; }
      
      
      /// <summary>
      /// Приоритетный вид транспорта
      /// </summary>
      [Display(Description = "Приоритетный вид транспорта")]
      [DataMember(Name = "priorityTypeOfTransportInRequest", EmitDefaultValue = false)]
      public virtual TypeOfTransportInRequest PriorityTypeOfTransportInRequest { get; set; }
      
      /// <summary>
      /// Дополнительный вид транспорта
      /// </summary>
      [ForeignKey("AdditionalTypeOfTransportInRequest")]
      [DataMember(Name = "additionalTypeOfTransportInRequestId")]
      [Display(Description = "Дополнительный вид транспорта")]
      public virtual long? AdditionalTypeOfTransportInRequestId { get; set; }
      
      
      /// <summary>
      /// Дополнительный вид транспорта
      /// </summary>
      [Display(Description = "Дополнительный вид транспорта")]
      [DataMember(Name = "additionalTypeOfTransportInRequest", EmitDefaultValue = false)]
      public virtual TypeOfTransportInRequest AdditionalTypeOfTransportInRequest { get; set; }
      
      /// <summary>
      /// Приоритетный тип лагеря
      /// </summary>
      [ForeignKey("TypeOfCamp")]
      [DataMember(Name = "typeOfCampId")]
      [Display(Description = "Приоритетный тип лагеря")]
      public virtual long? TypeOfCampId { get; set; }
      
      
      /// <summary>
      /// Приоритетный тип лагеря
      /// </summary>
      [Display(Description = "Приоритетный тип лагеря")]
      [DataMember(Name = "typeOfCamp", EmitDefaultValue = false)]
      public virtual TypeOfCamp TypeOfCamp { get; set; }
      
      /// <summary>
      /// Дополнительный тип лагеря
      /// </summary>
      [ForeignKey("TypeOfCampAddon")]
      [DataMember(Name = "typeOfCampAddonId")]
      [Display(Description = "Дополнительный тип лагеря")]
      public virtual long? TypeOfCampAddonId { get; set; }
      
      
      /// <summary>
      /// Дополнительный тип лагеря
      /// </summary>
      [Display(Description = "Дополнительный тип лагеря")]
      [DataMember(Name = "typeOfCampAddon", EmitDefaultValue = false)]
      public virtual TypeOfCamp TypeOfCampAddon { get; set; }
      
      /// <summary>
      /// Автор
      /// </summary>
      [ForeignKey("CreateUser")]
      [DataMember(Name = "createUserId")]
      [Display(Description = "Автор")]
      public virtual long? CreateUserId { get; set; }
      
      
      /// <summary>
      /// Автор
      /// </summary>
      [Display(Description = "Автор")]
      [DataMember(Name = "createUser", EmitDefaultValue = false)]
      public virtual Account CreateUser { get; set; }
      
      /// <summary>
      /// Ссылка организацию
      /// </summary>
      [ForeignKey("Organization")]
      [DataMember(Name = "organizationId")]
      [Display(Description = "Ссылка организацию")]
      public virtual long? OrganizationId { get; set; }
      
      
      /// <summary>
      /// Ссылка организацию
      /// </summary>
      [Display(Description = "Ссылка организацию")]
      [DataMember(Name = "organization", EmitDefaultValue = false)]
      public virtual Organization Organization { get; set; }
      
      /// <summary>
      /// Связь заявления и отеля/лагеря
      /// </summary>
      [ForeignKey("Hotels")]
      [DataMember(Name = "hotelsId")]
      [Display(Description = "Связь заявления и отеля/лагеря")]
      public virtual long? HotelsId { get; set; }
      
      
      /// <summary>
      /// Связь заявления и отеля/лагеря
      /// </summary>
      [Display(Description = "Связь заявления и отеля/лагеря")]
      [DataMember(Name = "hotels", EmitDefaultValue = false)]
      public virtual Hotels Hotels { get; set; }
      
      /// <summary>
      /// </summary>
      [ForeignKey("YearOfRest")]
      [DataMember(Name = "yearOfRestId")]
      [Display(Description = "")]
      public virtual long? YearOfRestId { get; set; }
      
      
      /// <summary>
      /// </summary>
      [Display(Description = "")]
      [DataMember(Name = "yearOfRest", EmitDefaultValue = false)]
      public virtual YearOfRest YearOfRest { get; set; }
      
      /// <summary>
      /// Связь заезда и заявления
      /// </summary>
      [ForeignKey("Tour")]
      [DataMember(Name = "tourId")]
      [Display(Description = "Связь заезда и заявления")]
      public virtual long? TourId { get; set; }
      
      
      /// <summary>
      /// Связь заезда и заявления
      /// </summary>
      [InverseProperty("RequestsSingle")]
      [Display(Description = "Связь заезда и заявления")]
      [DataMember(Name = "tour", EmitDefaultValue = false)]
      public virtual Tour Tour { get; set; }
      
      /// <summary>
      /// Смена
      /// </summary>
      [ForeignKey("GroupedTimeOfRest")]
      [DataMember(Name = "groupedTimeOfRestId")]
      [Display(Description = "Смена")]
      public virtual long? GroupedTimeOfRestId { get; set; }
      
      
      /// <summary>
      /// Смена
      /// </summary>
      [Display(Description = "Смена")]
      [DataMember(Name = "groupedTimeOfRest", EmitDefaultValue = false)]
      public virtual GroupedTimeOfRest GroupedTimeOfRest { get; set; }
      
      /// <summary>
      /// Заявление на дополнительное место
      /// </summary>
      [ForeignKey("ParentRequest")]
      [DataMember(Name = "parentRequestId")]
      [Display(Description = "Заявление на дополнительное место")]
      public virtual long? ParentRequestId { get; set; }
      
      
      /// <summary>
      /// Заявление на дополнительное место
      /// </summary>
      [InverseProperty("AddonRequests")]
      [Display(Description = "Заявление на дополнительное место")]
      [DataMember(Name = "parentRequest", EmitDefaultValue = false)]
      public virtual Request ParentRequest { get; set; }
      
      /// <summary>
      /// Использованиые варианты размещений
      /// </summary>
      [InverseProperty("Request")]
      [DataMember(Name = "offerInRequest", EmitDefaultValue = false)]
      public virtual ICollection<OfferInRequest> OfferInRequest { get; set; }
      
      /// <summary>
      /// Связь продуктов и заявлений
      /// </summary>
      [InverseProperty("Requests")]
      [DataMember(Name = "tours", EmitDefaultValue = false)]
      public virtual ICollection<Tour> Tours { get; set; }
      
      /// <summary>
      /// Город
      /// </summary>
      [ForeignKey("City")]
      [DataMember(Name = "cityId")]
      [Display(Description = "Город")]
      public virtual long? CityId { get; set; }
      
      
      /// <summary>
      /// Город
      /// </summary>
      [Display(Description = "Город")]
      [DataMember(Name = "city", EmitDefaultValue = false)]
      public virtual City City { get; set; }
      
      /// <summary>
      /// Подтип вида отдыха
      /// </summary>
      [ForeignKey("TypeOfRestSubtype")]
      [DataMember(Name = "typeOfRestSubtypeId")]
      [Display(Description = "Подтип вида отдыха")]
      public virtual long? TypeOfRestSubtypeId { get; set; }
      
      
      /// <summary>
      /// Подтип вида отдыха
      /// </summary>
      [Display(Description = "Подтип вида отдыха")]
      [DataMember(Name = "typeOfRestSubtype", EmitDefaultValue = false)]
      public virtual TypeOfRestSubtype TypeOfRestSubtype { get; set; }
      
      /// <summary>
      /// Базовая услуга
      /// </summary>
      [ForeignKey("BaseService")]
      [DataMember(Name = "baseServiceId")]
      [Display(Description = "Базовая услуга")]
      public virtual long? BaseServiceId { get; set; }
      
      
      /// <summary>
      /// Базовая услуга
      /// </summary>
      [Display(Description = "Базовая услуга")]
      [DataMember(Name = "baseService", EmitDefaultValue = false)]
      public virtual AddonServices BaseService { get; set; }
      
      /// <summary>
      /// Список детей на который коммерческая заявка
      /// </summary>
      [ForeignKey("ParentListOfChild")]
      [DataMember(Name = "parentListOfChildId")]
      [Display(Description = "Список детей на который коммерческая заявка")]
      public virtual long? ParentListOfChildId { get; set; }
      
      
      /// <summary>
      /// Список детей на который коммерческая заявка
      /// </summary>
      [InverseProperty("Requests")]
      [Display(Description = "Список детей на который коммерческая заявка")]
      [DataMember(Name = "parentListOfChild", EmitDefaultValue = false)]
      public virtual ListOfChilds ParentListOfChild { get; set; }
      
      /// <summary>
      /// Заявитель юридическое лицо
      /// </summary>
      [ForeignKey("ApplicantOrganization")]
      [DataMember(Name = "applicantOrganizationId")]
      [Display(Description = "Заявитель юридическое лицо")]
      public virtual long? ApplicantOrganizationId { get; set; }
      
      
      /// <summary>
      /// Заявитель юридическое лицо
      /// </summary>
      [Display(Description = "Заявитель юридическое лицо")]
      [DataMember(Name = "applicantOrganization", EmitDefaultValue = false)]
      public virtual Organization ApplicantOrganization { get; set; }
      
      /// <summary>
      /// Акция <-> Заявки
      /// </summary>
      [InverseProperty("Requests")]
      [DataMember(Name = "discounts", EmitDefaultValue = false)]
      public virtual ICollection<Discount> Discounts { get; set; }
      
      /// <summary>
      /// Номер дисконтной карты
      /// </summary>
      [ForeignKey("DiscountCard")]
      [DataMember(Name = "discountCardId")]
      [Display(Description = "Номер дисконтной карты")]
      public virtual long? DiscountCardId { get; set; }
      
      
      /// <summary>
      /// Номер дисконтной карты
      /// </summary>
      [Display(Description = "Номер дисконтной карты")]
      [DataMember(Name = "discountCard", EmitDefaultValue = false)]
      public virtual DiscountCard DiscountCard { get; set; }

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