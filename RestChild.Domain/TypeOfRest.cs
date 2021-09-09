// File:    TypeOfRest.cs
// Purpose: Definition of Class TypeOfRest

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Цель обращения
   /// </summary>
   [Serializable]
   [DataContract(Name = "typeOfRest")]
   public partial class TypeOfRest : IEntityBase
   {
      
      /// <summary>
      /// Уникальный идентификатор
      /// </summary>
      [Display(Description = "Уникальный идентификатор")]
      [Required(ErrorMessage = "\"Уникальный идентификатор\" должно быть заполнено")]
      [Key]
      [DatabaseGenerated(DatabaseGeneratedOption.None)]
      [DataMember(Name = "id", EmitDefaultValue = false)]
      public virtual long Id { get; set; }
      
      /// <summary>
      /// Наименование
      /// </summary>
      [Display(Description = "Наименование")]
      [MaxLength(1000, ErrorMessage = "\"Наименование\" не может быть больше 1000 символов")]
      [Required(ErrorMessage = "\"Наименование\" не может быть пустым")]
      [DataMember(Name = "name", EmitDefaultValue = false)]
      public virtual string Name { get; set; }
      
      /// <summary>
      /// Активен
      /// </summary>
      [Display(Description = "Активен")]
      [Required(ErrorMessage = "\"Активен\" должно быть заполнено")]
      [DataMember(Name = "isActive", EmitDefaultValue = false)]
      public virtual bool IsActive { get; set; }
      
      /// <summary>
      /// Код услуги
      /// </summary>
      [Display(Description = "Код услуги")]
      [MaxLength(1000, ErrorMessage = "\"Код услуги\" не может быть больше 1000 символов")]
      [DataMember(Name = "serviceCode", EmitDefaultValue = false)]
      public virtual string ServiceCode { get; set; }
      
      /// <summary>
      /// Для МПГУ
      /// </summary>
      [Display(Description = "Для МПГУ")]
      [Required(ErrorMessage = "\"Для МПГУ\" должно быть заполнено")]
      [DataMember(Name = "forMPGU", EmitDefaultValue = false)]
      public virtual bool ForMPGU { get; set; }
      
      /// <summary>
      /// Нужно место отдыха
      /// </summary>
      [Display(Description = "Нужно место отдыха")]
      [Required(ErrorMessage = "\"Нужно место отдыха\" должно быть заполнено")]
      [DataMember(Name = "needPlace", EmitDefaultValue = false)]
      public virtual bool NeedPlace { get; set; }
      
      /// <summary>
      /// Нужно размещение
      /// </summary>
      [Display(Description = "Нужно размещение")]
      [Required(ErrorMessage = "\"Нужно размещение\" должно быть заполнено")]
      [DataMember(Name = "needPlacment", EmitDefaultValue = false)]
      public virtual bool NeedPlacment { get; set; }
      
      /// <summary>
      /// Нужна тематика
      /// </summary>
      [Display(Description = "Нужна тематика")]
      [Required(ErrorMessage = "\"Нужна тематика\" должно быть заполнено")]
      [DataMember(Name = "needSubject", EmitDefaultValue = false)]
      public virtual bool NeedSubject { get; set; }
      
      /// <summary>
      /// Нужен заявитель
      /// </summary>
      [Display(Description = "Нужен заявитель")]
      [Required(ErrorMessage = "\"Нужен заявитель\" должно быть заполнено")]
      [DataMember(Name = "needApplicant", EmitDefaultValue = false)]
      public virtual bool NeedApplicant { get; set; }
      
      /// <summary>
      /// Возможен сопровождающий
      /// </summary>
      [Display(Description = "Возможен сопровождающий")]
      [Required(ErrorMessage = "\"Возможен сопровождающий\" должно быть заполнено")]
      [DataMember(Name = "needAttendant", EmitDefaultValue = false)]
      public virtual bool NeedAttendant { get; set; }
      
      /// <summary>
      /// Нижняя граница возраста
      /// </summary>
      [Display(Description = "Нижняя граница возраста")]
      [Required(ErrorMessage = "\"Нижняя граница возраста\" должно быть заполнено")]
      [DataMember(Name = "minAge", EmitDefaultValue = false)]
      public virtual int MinAge { get; set; }
      
      /// <summary>
      /// Верхняя граница возраста
      /// </summary>
      [Display(Description = "Верхняя граница возраста")]
      [Required(ErrorMessage = "\"Верхняя граница возраста\" должно быть заполнено")]
      [DataMember(Name = "maxAge", EmitDefaultValue = false)]
      public virtual int MaxAge { get; set; }
      
      /// <summary>
      /// </summary>
      [Display(Description = "")]
      [DataMember(Name = "urlToRulesOfRest", EmitDefaultValue = false)]
      public virtual string UrlToRulesOfRest { get; set; }
      
      /// <summary>
      /// Для формирования заездов
      /// </summary>
      [Display(Description = "Для формирования заездов")]
      [Required(ErrorMessage = "\"Для формирования заездов\" должно быть заполнено")]
      [DataMember(Name = "forTour", EmitDefaultValue = false)]
      public virtual bool ForTour { get; set; }
      
      /// <summary>
      /// Комерческое
      /// </summary>
      [Display(Description = "Комерческое")]
      [Required(ErrorMessage = "\"Комерческое\" должно быть заполнено")]
      [DataMember(Name = "commercial", EmitDefaultValue = false)]
      public virtual bool Commercial { get; set; }
      
      /// <summary>
      /// Дополнительное (нет требуется блоки мест)
      /// </summary>
      [Display(Description = "Дополнительное (нет требуется блоки мест)")]
      [Required(ErrorMessage = "\"Дополнительное (нет требуется блоки мест)\" должно быть заполнено")]
      [DataMember(Name = "isAddon", EmitDefaultValue = false)]
      public virtual bool IsAddon { get; set; }
      
      /// <summary>
      /// Нужна цена отдыха
      /// </summary>
      [Display(Description = "Нужна цена отдыха")]
      [Required(ErrorMessage = "\"Нужна цена отдыха\" должно быть заполнено")]
      [DataMember(Name = "needPrice", EmitDefaultValue = false)]
      public virtual bool NeedPrice { get; set; }
      
      /// <summary>
      /// Нужны варианты размещения
      /// </summary>
      [Display(Description = "Нужны варианты размещения")]
      [Required(ErrorMessage = "\"Нужны варианты размещения\" должно быть заполнено")]
      [DataMember(Name = "needAccomodation", EmitDefaultValue = false)]
      public virtual bool NeedAccomodation { get; set; }
      
      /// <summary>
      /// Нужны даты бронивания
      /// </summary>
      [Display(Description = "Нужны даты бронивания")]
      [Required(ErrorMessage = "\"Нужны даты бронивания\" должно быть заполнено")]
      [DataMember(Name = "needBookingDate", EmitDefaultValue = false)]
      public virtual bool NeedBookingDate { get; set; }
      
      /// <summary>
      /// </summary>
      [Display(Description = "")]
      [DataMember(Name = "needTransport", EmitDefaultValue = false)]
      public virtual bool? NeedTransport { get; set; }
      
      /// <summary>
      /// Нужны даты записи
      /// </summary>
      [Display(Description = "Нужны даты записи")]
      [Required(ErrorMessage = "\"Нужны даты записи\" должно быть заполнено")]
      [DataMember(Name = "needRecordingDate", EmitDefaultValue = false)]
      public virtual bool NeedRecordingDate { get; set; }
      
      /// <summary>
      /// Есть главная услуга
      /// </summary>
      [Display(Description = "Есть главная услуга")]
      [Required(ErrorMessage = "\"Есть главная услуга\" должно быть заполнено")]
      [DataMember(Name = "haveMainService", EmitDefaultValue = false)]
      public virtual bool HaveMainService { get; set; }
      
      /// <summary>
      /// Могут быть доп услуги
      /// </summary>
      [Display(Description = "Могут быть доп услуги")]
      [Required(ErrorMessage = "\"Могут быть доп услуги\" должно быть заполнено")]
      [DataMember(Name = "haveAddonService", EmitDefaultValue = false)]
      public virtual bool HaveAddonService { get; set; }
      
      /// <summary>
      /// Главная услуга скрыта
      /// </summary>
      [Display(Description = "Главная услуга скрыта")]
      [Required(ErrorMessage = "\"Главная услуга скрыта\" должно быть заполнено")]
      [DataMember(Name = "hiddenMainService", EmitDefaultValue = false)]
      public virtual bool HiddenMainService { get; set; }
      
      /// <summary>
      /// Код номера
      /// </summary>
      [Display(Description = "Код номера")]
      [MaxLength(1000, ErrorMessage = "\"Код номера\" не может быть больше 1000 символов")]
      [DataMember(Name = "numberCode", EmitDefaultValue = false)]
      public virtual string NumberCode { get; set; }
      
      /// <summary>
      /// Ответсвенный текстом
      /// </summary>
      [Display(Description = "Ответсвенный текстом")]
      [MaxLength(1000, ErrorMessage = "\"Ответсвенный текстом\" не может быть больше 1000 символов")]
      [DataMember(Name = "responsibleText", EmitDefaultValue = false)]
      public virtual string ResponsibleText { get; set; }
      
      /// <summary>
      /// Нужно формировать путевку
      /// </summary>
      [Display(Description = "Нужно формировать путевку")]
      [Required(ErrorMessage = "\"Нужно формировать путевку\" должно быть заполнено")]
      [DataMember(Name = "needGeneratePermit", EmitDefaultValue = false)]
      public virtual bool NeedGeneratePermit { get; set; }
      
      /// <summary>
      /// Ссылка на перечень ограничений
      /// </summary>
      [Display(Description = "Ссылка на перечень ограничений")]
      [MaxLength(1000, ErrorMessage = "\"Ссылка на перечень ограничений\" не может быть больше 1000 символов")]
      [DataMember(Name = "urlToListRestriction", EmitDefaultValue = false)]
      public virtual string UrlToListRestriction { get; set; }
      
      /// <summary>
      /// Ссылка на правил сопровождения
      /// </summary>
      [Display(Description = "Ссылка на правил сопровождения")]
      [MaxLength(1000, ErrorMessage = "\"Ссылка на правил сопровождения\" не может быть больше 1000 символов")]
      [DataMember(Name = "urlToRoolAttendant", EmitDefaultValue = false)]
      public virtual string UrlToRoolAttendant { get; set; }
      
      /// <summary>
      /// Выбор в первую заявочную кампанию
      /// </summary>
      [Display(Description = "Выбор в первую заявочную кампанию")]
      [Required(ErrorMessage = "\"Выбор в первую заявочную кампанию\" должно быть заполнено")]
      [DataMember(Name = "firstRequestCompanySelect", EmitDefaultValue = false)]
      public virtual bool FirstRequestCompanySelect { get; set; }
      
      /// <summary>
      /// Код услуги первой заявочно кампании
      /// </summary>
      [Display(Description = "Код услуги первой заявочно кампании")]
      [MaxLength(1000, ErrorMessage = "\"Код услуги первой заявочно кампании\" не может быть больше 1000 символов")]
      [DataMember(Name = "serviceCodeFirstCompany", EmitDefaultValue = false)]
      public virtual string ServiceCodeFirstCompany { get; set; }
      
      /// <summary>
      /// Может быть денежная компенсация
      /// </summary>
      [Display(Description = "Может быть денежная компенсация")]
      [Required(ErrorMessage = "\"Может быть денежная компенсация\" должно быть заполнено")]
      [DataMember(Name = "mayBeMoney", EmitDefaultValue = false)]
      public virtual bool MayBeMoney { get; set; }
      
      /// <summary>
      /// Дети не нужны
      /// </summary>
      [Display(Description = "Дети не нужны")]
      [Required(ErrorMessage = "\"Дети не нужны\" должно быть заполнено")]
      [DataMember(Name = "notChildren", EmitDefaultValue = false)]
      public virtual bool NotChildren { get; set; }
      
      /// <summary>
      /// Необходимо указать тип транспорта
      /// </summary>
      [Display(Description = "Необходимо указать тип транспорта")]
      [Required(ErrorMessage = "\"Необходимо указать тип транспорта\" должно быть заполнено")]
      [DataMember(Name = "needTypeOfTransport", EmitDefaultValue = false)]
      public virtual bool NeedTypeOfTransport { get; set; }
      
      /// <summary>
      /// Ссылка на фотографию лагеря стационарного типа
      /// </summary>
      [Display(Description = "Ссылка на фотографию лагеря стационарного типа")]
      [MaxLength(1000, ErrorMessage = "\"Ссылка на фотографию лагеря стационарного типа\" не может быть больше 1000 символов")]
      [DataMember(Name = "urlToStationaryTypeOfCampPhoto", EmitDefaultValue = false)]
      public virtual string UrlToStationaryTypeOfCampPhoto { get; set; }
      
      /// <summary>
      /// Ссылка на фотографию лагеря палаточного типа
      /// </summary>
      [Display(Description = "Ссылка на фотографию лагеря палаточного типа")]
      [MaxLength(1000, ErrorMessage = "\"Ссылка на фотографию лагеря палаточного типа\" не может быть больше 1000 символов")]
      [DataMember(Name = "urlToCampTypeOfCampPhoto", EmitDefaultValue = false)]
      public virtual string UrlToCampTypeOfCampPhoto { get; set; }
      
      
      /// <summary>
      /// Вид отдыха
      /// </summary>
      [InverseProperty("TypeOfRest")]
      [DataMember(Name = "typeOfRestBenefitRestrictions", EmitDefaultValue = false)]
      public virtual ICollection<TypeOfRestBenefitRestriction> TypeOfRestBenefitRestrictions { get; set; }
      
      /// <summary>
      /// Связь с видом отдыха
      /// </summary>
      [InverseProperty("TypeOfRest")]
      [DataMember(Name = "benefitTypes", EmitDefaultValue = false)]
      public virtual ICollection<BenefitType> BenefitTypes { get; set; }
      
      /// <summary>
      /// Связь причин отказа с видом отдыха
      /// </summary>
      [InverseProperty("TypeOfRests")]
      [DataMember(Name = "declineReasons", EmitDefaultValue = false)]
      public virtual ICollection<DeclineReason> DeclineReasons { get; set; }
      
      /// <summary>
      /// Связь видов отдыха и типов файлов
      /// </summary>
      [InverseProperty("TypeOfRests")]
      [DataMember(Name = "requestFileTypes", EmitDefaultValue = false)]
      public virtual ICollection<RequestFileType> RequestFileTypes { get; set; }
      
      /// <summary>
      /// Цели обращения в АИС ДО -> МСП в ИС Социум
      /// </summary>
      [ForeignKey("TypeOfRestERL")]
      [DataMember(Name = "typeOfRestERLId")][Display(Description = "Цели обращения в АИС ДО -> МСП в ИС Социум")]
      public virtual long? TypeOfRestERLId { get; set; }
      /// <summary>
      /// Цели обращения в АИС ДО -> МСП в ИС Социум
      /// </summary>
      [InverseProperty("TypesOfRest")]
      [Display(Description = "Цели обращения в АИС ДО -> МСП в ИС Социум")]
      [DataMember(Name = "typeOfRestERL", EmitDefaultValue = false)]
      public virtual TypeOfRestERL TypeOfRestERL { get; set; }
      
      /// <summary>
      /// Цель обращения -> цена путёвки
      /// </summary>
      [InverseProperty("TypeOfRest")]
      [DataMember(Name = "prices", EmitDefaultValue = false)]
      public virtual ICollection<AverageRestPrice> Prices { get; set; }
      
      /// <summary>
      /// Ссылка на родителя
      /// </summary>
      [ForeignKey("Parent")]
      [DataMember(Name = "parentId")]
      [Display(Description = "Ссылка на родителя")]
      public virtual long? ParentId { get; set; }
      
      
      /// <summary>
      /// Ссылка на родителя
      /// </summary>
      [Display(Description = "Ссылка на родителя")]
      [DataMember(Name = "parent", EmitDefaultValue = false)]
      public virtual TypeOfRest Parent { get; set; }
      
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
      /// Ответсвенный
      /// </summary>
      [ForeignKey("Responsible")]
      [DataMember(Name = "responsibleId")]
      [Display(Description = "Ответсвенный")]
      public virtual long? ResponsibleId { get; set; }
      
      
      /// <summary>
      /// Ответсвенный
      /// </summary>
      [Display(Description = "Ответсвенный")]
      [DataMember(Name = "responsible", EmitDefaultValue = false)]
      public virtual Account Responsible { get; set; }
      
      /// <summary>
      /// Связь типа документа с целью обращения
      /// </summary>
      [InverseProperty("TypesOfRest")]
      [DataMember(Name = "documentTypes", EmitDefaultValue = false)]
      public virtual ICollection<DocumentType> DocumentTypes { get; set; }
      
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
      /// Вид отеля
      /// </summary>
      [ForeignKey("HotelType")]
      [DataMember(Name = "hotelTypeId")]
      [Display(Description = "Вид отеля")]
      public virtual long? HotelTypeId { get; set; }
      
      
      /// <summary>
      /// Вид отеля
      /// </summary>
      [Display(Description = "Вид отеля")]
      [DataMember(Name = "hotelType", EmitDefaultValue = false)]
      public virtual HotelType HotelType { get; set; }
      
      /// <summary>
      /// Вид применяемой услуги
      /// </summary>
      [ForeignKey("TypeOfService")]
      [DataMember(Name = "typeOfServiceId")]
      [Display(Description = "Вид применяемой услуги")]
      public virtual long? TypeOfServiceId { get; set; }
      
      
      /// <summary>
      /// Вид применяемой услуги
      /// </summary>
      [Display(Description = "Вид применяемой услуги")]
      [DataMember(Name = "typeOfService", EmitDefaultValue = false)]
      public virtual TypeOfService TypeOfService { get; set; }
      
      /// <summary>
      /// ЛК -> Цель обращения
      /// </summary>
      [InverseProperty("TypeOfRest")]
      [DataMember(Name = "benefitTypesERL", EmitDefaultValue = false)]
      public virtual ICollection<BenefitTypeERL> BenefitTypesERL { get; set; }

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