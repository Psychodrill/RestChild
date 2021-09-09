// File:    Applicant.cs
// Purpose: Definition of Class Applicant

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Заявитель/сопровождающий
   /// </summary>
   [Serializable]
   [DataContract(Name = "applicant")]
   public partial class Applicant : IEntityBase
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
      /// Телефон
      /// </summary>
      [Display(Description = "Телефон")]
      [MaxLength(1000, ErrorMessage = "\"Телефон\" не может быть больше 1000 символов")]
      [DataMember(Name = "phone", EmitDefaultValue = false)]
      public virtual string Phone { get; set; }
      
      /// <summary>
      /// Электронная почта
      /// </summary>
      [Display(Description = "Электронная почта")]
      [MaxLength(1000, ErrorMessage = "\"Электронная почта\" не может быть больше 1000 символов")]
      [DataMember(Name = "email", EmitDefaultValue = false)]
      public virtual string Email { get; set; }
      
      /// <summary>
      /// Снилс
      /// </summary>
      [Display(Description = "Снилс")]
      [MaxLength(1000, ErrorMessage = "\"Снилс\" не может быть больше 1000 символов")]
      [DataMember(Name = "snils", EmitDefaultValue = false)]
      public virtual string Snils { get; set; }
      
      /// <summary>
      /// Признак что заявитель сопровождающий
      /// </summary>
      [Display(Description = "Признак что заявитель сопровождающий")]
      [Required(ErrorMessage = "\"Признак что заявитель сопровождающий\" должно быть заполнено")]
      [DataMember(Name = "isAccomp", EmitDefaultValue = false)]
      public virtual bool IsAccomp { get; set; }
      
      /// <summary>
      /// Признак что заявитель
      /// </summary>
      [Display(Description = "Признак что заявитель")]
      [Required(ErrorMessage = "\"Признак что заявитель\" должно быть заполнено")]
      [DataMember(Name = "isApplicant", EmitDefaultValue = false)]
      public virtual bool IsApplicant { get; set; }
      
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
      /// Признак последней версии
      /// </summary>
      [Display(Description = "Признак последней версии")]
      [Required(ErrorMessage = "\"Признак последней версии\" должно быть заполнено")]
      [DataMember(Name = "isLast", EmitDefaultValue = false)]
      public virtual bool IsLast { get; set; }
      
      /// <summary>
      /// Мужской пол
      /// </summary>
      [Display(Description = "Мужской пол")]
      [DataMember(Name = "male", EmitDefaultValue = false)]
      public virtual bool? Male { get; set; }
      
      /// <summary>
      /// Дата рождения
      /// </summary>
      [Display(Description = "Дата рождения")]
      [DataMember(Name = "dateOfBirth", EmitDefaultValue = false)]
      public virtual DateTime? DateOfBirth { get; set; }
      
      /// <summary>
      /// Должность сопровождающего
      /// </summary>
      [Display(Description = "Должность сопровождающего")]
      [MaxLength(1000, ErrorMessage = "\"Должность сопровождающего\" не может быть больше 1000 символов")]
      [DataMember(Name = "position", EmitDefaultValue = false)]
      public virtual string Position { get; set; }
      
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
      /// Признак что удалено
      /// </summary>
      [Display(Description = "Признак что удалено")]
      [Required(ErrorMessage = "\"Признак что удалено\" должно быть заполнено")]
      [DataMember(Name = "isDeleted", EmitDefaultValue = false)]
      public virtual bool IsDeleted { get; set; }
      
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
      /// Дополнительный телефон
      /// </summary>
      [Display(Description = "Дополнительный телефон")]
      [MaxLength(1000, ErrorMessage = "\"Дополнительный телефон\" не может быть больше 1000 символов")]
      [DataMember(Name = "addonPhone", EmitDefaultValue = false)]
      public virtual string AddonPhone { get; set; }
      
      /// <summary>
      /// ИНН
      /// </summary>
      [Display(Description = "ИНН")]
      [MaxLength(1000, ErrorMessage = "\"ИНН\" не может быть больше 1000 символов")]
      [DataMember(Name = "inn", EmitDefaultValue = false)]
      public virtual string Inn { get; set; }
      
      /// <summary>
      /// Заявитель отдыхающий
      /// </summary>
      [Display(Description = "Заявитель отдыхающий")]
      [Required(ErrorMessage = "\"Заявитель отдыхающий\" должно быть заполнено")]
      [DataMember(Name = "isApplicantCamper", EmitDefaultValue = false)]
      public virtual bool IsApplicantCamper { get; set; }
      
      /// <summary>
      /// Представитель заявителя
      /// </summary>
      [Display(Description = "Представитель заявителя")]
      [Required(ErrorMessage = "\"Представитель заявителя\" должно быть заполнено")]
      [DataMember(Name = "isAgent", EmitDefaultValue = false)]
      public virtual bool IsAgent { get; set; }
      
      /// <summary>
      /// Дата выдачи доверенности
      /// </summary>
      [Display(Description = "Дата выдачи доверенности")]
      [DataMember(Name = "proxyDateOfIssure", EmitDefaultValue = false)]
      public virtual DateTime? ProxyDateOfIssure { get; set; }
      
      /// <summary>
      /// Имя натариуса
      /// </summary>
      [Display(Description = "Имя натариуса")]
      [MaxLength(1000, ErrorMessage = "\"Имя натариуса\" не может быть больше 1000 символов")]
      [DataMember(Name = "notaryName", EmitDefaultValue = false)]
      public virtual string NotaryName { get; set; }
      
      /// <summary>
      /// Дата окончания действия доверенности
      /// </summary>
      [Display(Description = "Дата окончания действия доверенности")]
      [DataMember(Name = "proxyEndDate", EmitDefaultValue = false)]
      public virtual DateTime? ProxyEndDate { get; set; }
      
      /// <summary>
      /// Номер доверенности
      /// </summary>
      [Display(Description = "Номер доверенности")]
      [MaxLength(1000, ErrorMessage = "\"Номер доверенности\" не может быть больше 1000 символов")]
      [DataMember(Name = "proxyNumber", EmitDefaultValue = false)]
      public virtual string ProxyNumber { get; set; }
      
      /// <summary>
      /// Доверенное лицо
      /// </summary>
      [Display(Description = "Доверенное лицо")]
      [Required(ErrorMessage = "\"Доверенное лицо\" должно быть заполнено")]
      [DataMember(Name = "isProxy", EmitDefaultValue = false)]
      public virtual bool IsProxy { get; set; }
      
      /// <summary>
      /// Признак что имеется заключение ЦПМПК города Москвы
      /// </summary>
      [Display(Description = "Признак что имеется заключение ЦПМПК города Москвы")]
      [Required(ErrorMessage = "\"Признак что имеется заключение ЦПМПК города Москвы\" должно быть заполнено")]
      [DataMember(Name = "isCPMPK", EmitDefaultValue = false)]
      public virtual bool IsCPMPK { get; set; }
      
      
      /// <summary>
      /// Сопровождающий ребенка
      /// </summary>
      [InverseProperty("Applicant")]
      [DataMember(Name = "childAttendant", EmitDefaultValue = false)]
      public virtual ICollection<Child> ChildAttendant { get; set; }
      
      /// <summary>
      /// Обмен с базовым регистром
      /// </summary>
      [InverseProperty("Applicant")]
      [DataMember(Name = "baseRegistryInfo", EmitDefaultValue = false)]
      public virtual ICollection<ExchangeBaseRegistry> BaseRegistryInfo { get; set; }
      
      /// <summary>
      /// Заявитель/сопровождающий
      /// </summary>
      [InverseProperty("Applicant")]
      [DataMember(Name = "linkToPeoples", EmitDefaultValue = false)]
      public virtual ICollection<LinkToPeople> LinkToPeoples { get; set; }
      
      /// <summary>
      /// Соповождающий
      /// </summary>
      [InverseProperty("Attendants")]
      [DataMember(Name = "calculations", EmitDefaultValue = false)]
      public virtual ICollection<Calculation> Calculations { get; set; }
      
      /// <summary>
      /// Связь заявителя со статусом в ЕРЛ
      /// </summary>
      [InverseProperty("Applicant")]
      [DataMember(Name = "eRLPersons", EmitDefaultValue = false)]
      public virtual ICollection<ERLPersonStatus> ERLPersons { get; set; }
      
      /// <summary>
      /// Сопровождающие лица
      /// </summary>
      [ForeignKey("Request")]
      [DataMember(Name = "requestId")]
      [Display(Description = "Сопровождающие лица")]
      public virtual long? RequestId { get; set; }
      
      
      /// <summary>
      /// Сопровождающие лица
      /// </summary>
      [InverseProperty("Attendant")]
      [Display(Description = "Сопровождающие лица")]
      [DataMember(Name = "request", EmitDefaultValue = false)]
      public virtual Request Request { get; set; }
      
      /// <summary>
      /// Вид документа заявителя
      /// </summary>
      [ForeignKey("DocumentType")]
      [DataMember(Name = "documentTypeId")]
      [Display(Description = "Вид документа заявителя")]
      public virtual long? DocumentTypeId { get; set; }
      
      
      /// <summary>
      /// Вид документа заявителя
      /// </summary>
      [Display(Description = "Вид документа заявителя")]
      [DataMember(Name = "documentType", EmitDefaultValue = false)]
      public virtual DocumentType DocumentType { get; set; }
      
      /// <summary>
      /// Вид представительства
      /// </summary>
      [ForeignKey("ApplicantType")]
      [DataMember(Name = "applicantTypeId")]
      [Display(Description = "Вид представительства")]
      public virtual long? ApplicantTypeId { get; set; }
      
      
      /// <summary>
      /// Вид представительства
      /// </summary>
      [Display(Description = "Вид представительства")]
      [DataMember(Name = "applicantType", EmitDefaultValue = false)]
      public virtual ApplicantType ApplicantType { get; set; }
      
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
      public virtual Applicant Entity { get; set; }
      
      /// <summary>
      /// Вид заграничного документа
      /// </summary>
      [ForeignKey("ForeginType")]
      [DataMember(Name = "foreginTypeId")]
      [Display(Description = "Вид заграничного документа")]
      public virtual long? ForeginTypeId { get; set; }
      
      
      /// <summary>
      /// Вид заграничного документа
      /// </summary>
      [Display(Description = "Вид заграничного документа")]
      [DataMember(Name = "foreginType", EmitDefaultValue = false)]
      public virtual DocumentType ForeginType { get; set; }
      
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
      /// Заявитель отдыхающий
      /// </summary>
      [ForeignKey("ApplicantCamper")]
      [DataMember(Name = "applicantCamperId")]
      [Display(Description = "Заявитель отдыхающий")]
      public virtual long? ApplicantCamperId { get; set; }
      
      
      /// <summary>
      /// Заявитель отдыхающий
      /// </summary>
      [Display(Description = "Заявитель отдыхающий")]
      [DataMember(Name = "applicantCamper", EmitDefaultValue = false)]
      public virtual Child ApplicantCamper { get; set; }
      
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
      /// Связь список детей и сопровождающий
      /// </summary>
      [ForeignKey("ChildList")]
      [DataMember(Name = "childListId")]
      [Display(Description = "Связь список детей и сопровождающий")]
      public virtual long? ChildListId { get; set; }
      
      
      /// <summary>
      /// Связь список детей и сопровождающий
      /// </summary>
      [InverseProperty("Attendants")]
      [Display(Description = "Связь список детей и сопровождающий")]
      [DataMember(Name = "childList", EmitDefaultValue = false)]
      public virtual ListOfChilds ChildList { get; set; }
      
      /// <summary>
      /// Признак что исключен из списка
      /// </summary>
      [ForeignKey("ExcludeReason")]
      [DataMember(Name = "excludeReasonId")]
      [Display(Description = "Признак что исключен из списка")]
      public virtual long? ExcludeReasonId { get; set; }
      
      
      /// <summary>
      /// Признак что исключен из списка
      /// </summary>
      [Display(Description = "Признак что исключен из списка")]
      [DataMember(Name = "excludeReason", EmitDefaultValue = false)]
      public virtual ChildIncludeExcludeReason ExcludeReason { get; set; }
      
      /// <summary>
      /// Причина включения в список
      /// </summary>
      [ForeignKey("IncludeReason")]
      [DataMember(Name = "includeReasonId")]
      [Display(Description = "Причина включения в список")]
      public virtual long? IncludeReasonId { get; set; }
      
      
      /// <summary>
      /// Причина включения в список
      /// </summary>
      [Display(Description = "Причина включения в список")]
      [DataMember(Name = "includeReason", EmitDefaultValue = false)]
      public virtual ChildIncludeExcludeReason IncludeReason { get; set; }
      
      /// <summary>
      /// Связь фонда и заявителей
      /// </summary>
      [ForeignKey("TourVolume")]
      [DataMember(Name = "tourVolumeId")]
      [Display(Description = "Связь фонда и заявителей")]
      public virtual long? TourVolumeId { get; set; }
      
      
      /// <summary>
      /// Связь фонда и заявителей
      /// </summary>
      [Display(Description = "Связь фонда и заявителей")]
      [DataMember(Name = "tourVolume", EmitDefaultValue = false)]
      public virtual TourVolume TourVolume { get; set; }
      
      /// <summary>
      /// Заезд
      /// </summary>
      [ForeignKey("Bout")]
      [DataMember(Name = "boutId")]
      [Display(Description = "Заезд")]
      public virtual long? BoutId { get; set; }
      
      
      /// <summary>
      /// Заезд
      /// </summary>
      [InverseProperty("Applicants")]
      [Display(Description = "Заезд")]
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
      [InverseProperty("Attendants")]
      [Display(Description = "Размещение")]
      [DataMember(Name = "offerInRequest", EmitDefaultValue = false)]
      public virtual OfferInRequest OfferInRequest { get; set; }
      
      /// <summary>
      /// Продукты <-> Сопровождающий
      /// </summary>
      [InverseProperty("Applicants")]
      [DataMember(Name = "tours", EmitDefaultValue = false)]
      public virtual ICollection<Tour> Tours { get; set; }
      
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
      /// Уникальный сопровождающий
      /// </summary>
      [ForeignKey("RelativeUniqe")]
      [DataMember(Name = "relativeUniqeId")]
      [Display(Description = "Уникальный сопровождающий")]
      public virtual long? RelativeUniqeId { get; set; }
      
      
      /// <summary>
      /// Уникальный сопровождающий
      /// </summary>
      [InverseProperty("Relatives")]
      [Display(Description = "Уникальный сопровождающий")]
      [DataMember(Name = "relativeUniqe", EmitDefaultValue = false)]
      public virtual RelativeUniqe RelativeUniqe { get; set; }

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