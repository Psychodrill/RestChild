// File:    AddonServices.cs
// Purpose: Definition of Class AddonServices

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Услуга
   /// </summary>
   [Serializable]
   [DataContract(Name = "addonServices")]
   public partial class AddonServices : IEntityBase
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
      /// Код услуги
      /// </summary>
      [Display(Description = "Код услуги")]
      [MaxLength(1000, ErrorMessage = "\"Код услуги\" не может быть больше 1000 символов")]
      [DataMember(Name = "code", EmitDefaultValue = false)]
      public virtual string Code { get; set; }
      
      /// <summary>
      /// Наименование услуги
      /// </summary>
      [Display(Description = "Наименование услуги")]
      [MaxLength(1000, ErrorMessage = "\"Наименование услуги\" не может быть больше 1000 символов")]
      [Required(ErrorMessage = "\"Наименование услуги\" не может быть пустым")]
      [DataMember(Name = "name", EmitDefaultValue = false)]
      public virtual string Name { get; set; }
      
      /// <summary>
      /// Обязательность
      /// </summary>
      [Display(Description = "Обязательность")]
      [DataMember(Name = "requared", EmitDefaultValue = false)]
      public virtual bool? Requared { get; set; }
      
      /// <summary>
      /// Услуга реестра услуг (т.е. создана в реестре услуг, а не ее выбор в продукте или размещении)
      /// </summary>
      [Display(Description = "Услуга реестра услуг (т.е. создана в реестре услуг, а не ее выбор в продукте или размещении)")]
      [Required(ErrorMessage = "\"Услуга реестра услуг (т.е. создана в реестре услуг, а не ее выбор в продукте или размещении)\" должно быть заполнено")]
      [DataMember(Name = "generalService", EmitDefaultValue = false)]
      public virtual bool GeneralService { get; set; }
      
      /// <summary>
      /// Для зарубежных поездок
      /// </summary>
      [Display(Description = "Для зарубежных поездок")]
      [Required(ErrorMessage = "\"Для зарубежных поездок\" должно быть заполнено")]
      [DataMember(Name = "forForeign", EmitDefaultValue = false)]
      public virtual bool ForForeign { get; set; }
      
      /// <summary>
      /// Активно или нет
      /// </summary>
      [Display(Description = "Активно или нет")]
      [Required(ErrorMessage = "\"Активно или нет\" должно быть заполнено")]
      [DataMember(Name = "isActive", EmitDefaultValue = false)]
      public virtual bool IsActive { get; set; }
      
      /// <summary>
      /// Описание
      /// </summary>
      [Display(Description = "Описание")]
      [DataMember(Name = "description", EmitDefaultValue = false)]
      public virtual String Description { get; set; }
      
      /// <summary>
      /// По умолчанию
      /// </summary>
      [Display(Description = "По умолчанию")]
      [Required(ErrorMessage = "\"По умолчанию\" должно быть заполнено")]
      [DataMember(Name = "byDefault", EmitDefaultValue = false)]
      public virtual bool ByDefault { get; set; }
      
      /// <summary>
      /// Услуга только с заявлением
      /// </summary>
      [Display(Description = "Услуга только с заявлением")]
      [Required(ErrorMessage = "\"Услуга только с заявлением\" должно быть заполнено")]
      [DataMember(Name = "onlyWithRequest", EmitDefaultValue = false)]
      public virtual bool OnlyWithRequest { get; set; }
      
      /// <summary>
      /// Дата оказания услуги с
      /// </summary>
      [Display(Description = "Дата оказания услуги с")]
      [DataMember(Name = "dateFrom", EmitDefaultValue = false)]
      public virtual DateTime? DateFrom { get; set; }
      
      /// <summary>
      /// Дата оказания услуги по
      /// </summary>
      [Display(Description = "Дата оказания услуги по")]
      [DataMember(Name = "dateTo", EmitDefaultValue = false)]
      public virtual DateTime? DateTo { get; set; }
      
      /// <summary>
      /// Емкость
      /// </summary>
      [Display(Description = "Емкость")]
      [DataMember(Name = "volume", EmitDefaultValue = false)]
      public virtual int? Volume { get; set; }
      
      /// <summary>
      /// Требуется подтверждение
      /// </summary>
      [Display(Description = "Требуется подтверждение")]
      [Required(ErrorMessage = "\"Требуется подтверждение\" должно быть заполнено")]
      [DataMember(Name = "needApprove", EmitDefaultValue = false)]
      public virtual bool NeedApprove { get; set; }
      
      /// <summary>
      /// Дата покупки с
      /// </summary>
      [Display(Description = "Дата покупки с")]
      [DataMember(Name = "dateBookingFrom", EmitDefaultValue = false)]
      public virtual DateTime? DateBookingFrom { get; set; }
      
      /// <summary>
      /// Дата покупки по
      /// </summary>
      [Display(Description = "Дата покупки по")]
      [DataMember(Name = "dateBookingTo", EmitDefaultValue = false)]
      public virtual DateTime? DateBookingTo { get; set; }
      
      /// <summary>
      /// Анонс мероприятия
      /// </summary>
      [Display(Description = "Анонс мероприятия")]
      [DataMember(Name = "announcementEvent", EmitDefaultValue = false)]
      public virtual String AnnouncementEvent { get; set; }
      
      /// <summary>
      /// Условия для сопровождающих
      /// </summary>
      [Display(Description = "Условия для сопровождающих")]
      [DataMember(Name = "conditionsForAccompanying", EmitDefaultValue = false)]
      public virtual String ConditionsForAccompanying { get; set; }
      
      /// <summary>
      /// Минимальный размер группы
      /// </summary>
      [Display(Description = "Минимальный размер группы")]
      [DataMember(Name = "sizeMin", EmitDefaultValue = false)]
      public virtual int? SizeMin { get; set; }
      
      /// <summary>
      /// Максимальны размер группы
      /// </summary>
      [Display(Description = "Максимальны размер группы")]
      [DataMember(Name = "sizeMax", EmitDefaultValue = false)]
      public virtual int? SizeMax { get; set; }
      
      /// <summary>
      /// Продолжительность часы
      /// </summary>
      [Display(Description = "Продолжительность часы")]
      [DataMember(Name = "durationHour", EmitDefaultValue = false)]
      public virtual int? DurationHour { get; set; }
      
      /// <summary>
      /// Продолжительность дни
      /// </summary>
      [Display(Description = "Продолжительность дни")]
      [DataMember(Name = "durationDay", EmitDefaultValue = false)]
      public virtual int? DurationDay { get; set; }
      
      /// <summary>
      /// Продолжительность месяцы
      /// </summary>
      [Display(Description = "Продолжительность месяцы")]
      [DataMember(Name = "durationMonth", EmitDefaultValue = false)]
      public virtual int? DurationMonth { get; set; }
      
      /// <summary>
      /// Продолжительность годы
      /// </summary>
      [Display(Description = "Продолжительность годы")]
      [DataMember(Name = "durationYear", EmitDefaultValue = false)]
      public virtual int? DurationYear { get; set; }
      
      /// <summary>
      /// Групповая
      /// </summary>
      [Display(Description = "Групповая")]
      [Required(ErrorMessage = "\"Групповая\" должно быть заполнено")]
      [DataMember(Name = "isGroup", EmitDefaultValue = false)]
      public virtual bool IsGroup { get; set; }
      
      /// <summary>
      /// Скрытая услуга
      /// </summary>
      [Display(Description = "Скрытая услуга")]
      [Required(ErrorMessage = "\"Скрытая услуга\" должно быть заполнено")]
      [DataMember(Name = "hidden", EmitDefaultValue = false)]
      public virtual bool Hidden { get; set; }
      
      /// <summary>
      /// Произвольные даты оказания услуги
      /// </summary>
      [Display(Description = "Произвольные даты оказания услуги")]
      [Required(ErrorMessage = "\"Произвольные даты оказания услуги\" должно быть заполнено")]
      [DataMember(Name = "notFixedDate", EmitDefaultValue = false)]
      public virtual bool NotFixedDate { get; set; }
      
      /// <summary>
      /// Процент надбавки
      /// </summary>
      [Display(Description = "Процент надбавки")]
      [DataMember(Name = "procentOver", EmitDefaultValue = false)]
      public virtual decimal? ProcentOver { get; set; }
      
      /// <summary>
      /// Не для сайта
      /// </summary>
      [Display(Description = "Не для сайта")]
      [Required(ErrorMessage = "\"Не для сайта\" должно быть заполнено")]
      [DataMember(Name = "notForSite", EmitDefaultValue = false)]
      public virtual bool NotForSite { get; set; }
      
      
      /// <summary>
      /// Фотографии
      /// </summary>
      [InverseProperty("AddonServices")]
      [DataMember(Name = "photos", EmitDefaultValue = false)]
      public virtual ICollection<AddonServicesPhoto> Photos { get; set; }
      
      /// <summary>
      /// Родительская услуга
      /// </summary>
      [InverseProperty("Parent")]
      [DataMember(Name = "childs", EmitDefaultValue = false)]
      public virtual ICollection<AddonServices> Childs { get; set; }
      
      /// <summary>
      /// География мероприятия
      /// </summary>
      [InverseProperty("AddonServices")]
      [DataMember(Name = "eventGeographys", EmitDefaultValue = false)]
      public virtual ICollection<EventGeography> EventGeographys { get; set; }
      
      /// <summary>
      /// Стоимость услуг
      /// </summary>
      [InverseProperty("AddonServices")]
      [DataMember(Name = "prices", EmitDefaultValue = false)]
      public virtual ICollection<AddonServicesPrice> Prices { get; set; }
      
      /// <summary>
      /// Связанные услуги
      /// </summary>
      [InverseProperty("LinkService")]
      [DataMember(Name = "linkServices", EmitDefaultValue = false)]
      public virtual ICollection<AddonServices> LinkServices { get; set; }
      
      /// <summary>
      /// Блоки мест
      /// </summary>
      [InverseProperty("AddonServices")]
      [DataMember(Name = "serviceBlocks", EmitDefaultValue = false)]
      public virtual ICollection<ServiceBlock> ServiceBlocks { get; set; }
      
      /// <summary>
      /// Услуги
      /// </summary>
      [ForeignKey("Contract")]
      [DataMember(Name = "contractId")]
      [Display(Description = "Услуги")]
      public virtual long? ContractId { get; set; }
      
      
      /// <summary>
      /// Услуги
      /// </summary>
      [InverseProperty("Services")]
      [Display(Description = "Услуги")]
      [DataMember(Name = "contract", EmitDefaultValue = false)]
      public virtual Contract Contract { get; set; }
      
      /// <summary>
      /// Партнер
      /// </summary>
      [ForeignKey("Partner")]
      [DataMember(Name = "partnerId")]
      [Display(Description = "Партнер")]
      public virtual long? PartnerId { get; set; }
      
      
      /// <summary>
      /// Партнер
      /// </summary>
      [Display(Description = "Партнер")]
      [DataMember(Name = "partner", EmitDefaultValue = false)]
      public virtual Organization Partner { get; set; }
      
      /// <summary>
      /// Блок мест для которого применяется услуга
      /// </summary>
      [ForeignKey("Tour")]
      [DataMember(Name = "tourId")]
      [Display(Description = "Блок мест для которого применяется услуга")]
      public virtual long? TourId { get; set; }
      
      
      /// <summary>
      /// Блок мест для которого применяется услуга
      /// </summary>
      [Display(Description = "Блок мест для которого применяется услуга")]
      [DataMember(Name = "tour", EmitDefaultValue = false)]
      public virtual Tour Tour { get; set; }
      
      /// <summary>
      /// Отель / лагерь <-> Услуги
      /// </summary>
      [InverseProperty("Services")]
      [DataMember(Name = "hotels", EmitDefaultValue = false)]
      public virtual ICollection<Hotels> Hotels { get; set; }
      
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
      /// Пользователь
      /// </summary>
      [ForeignKey("Account")]
      [DataMember(Name = "accountId")]
      [Display(Description = "Пользователь")]
      public virtual long? AccountId { get; set; }
      
      
      /// <summary>
      /// Пользователь
      /// </summary>
      [Display(Description = "Пользователь")]
      [DataMember(Name = "account", EmitDefaultValue = false)]
      public virtual Account Account { get; set; }
      
      /// <summary>
      /// Родительская услуга
      /// </summary>
      [ForeignKey("Parent")]
      [DataMember(Name = "parentId")]
      [Display(Description = "Родительская услуга")]
      public virtual long? ParentId { get; set; }
      
      
      /// <summary>
      /// Родительская услуга
      /// </summary>
      [InverseProperty("Childs")]
      [Display(Description = "Родительская услуга")]
      [DataMember(Name = "parent", EmitDefaultValue = false)]
      public virtual AddonServices Parent { get; set; }
      
      /// <summary>
      /// Статус
      /// </summary>
      [ForeignKey("State")]
      [DataMember(Name = "stateId")]
      [Display(Description = "Статус")]
      public virtual long? StateId { get; set; }
      
      
      /// <summary>
      /// Статус
      /// </summary>
      [Display(Description = "Статус")]
      [DataMember(Name = "state", EmitDefaultValue = false)]
      public virtual StateMachineState State { get; set; }
      
      /// <summary>
      /// Вид услуги
      /// </summary>
      [ForeignKey("TypeOfService")]
      [DataMember(Name = "typeOfServiceId")]
      [Display(Description = "Вид услуги")]
      public virtual long? TypeOfServiceId { get; set; }
      
      
      /// <summary>
      /// Вид услуги
      /// </summary>
      [Display(Description = "Вид услуги")]
      [DataMember(Name = "typeOfService", EmitDefaultValue = false)]
      public virtual TypeOfService TypeOfService { get; set; }
      
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
      /// Вид стоимости оплаты услуги
      /// </summary>
      [ForeignKey("AddonServicesPaymentType")]
      [DataMember(Name = "addonServicesPaymentTypeId")]
      [Display(Description = "Вид стоимости оплаты услуги")]
      public virtual long? AddonServicesPaymentTypeId { get; set; }
      
      
      /// <summary>
      /// Вид стоимости оплаты услуги
      /// </summary>
      [Display(Description = "Вид стоимости оплаты услуги")]
      [DataMember(Name = "addonServicesPaymentType", EmitDefaultValue = false)]
      public virtual AddonServicesPaymentType AddonServicesPaymentType { get; set; }
      
      /// <summary>
      /// Вид номера
      /// </summary>
      [ForeignKey("TypeOfRooms")]
      [DataMember(Name = "typeOfRoomsId")]
      [Display(Description = "Вид номера")]
      public virtual long? TypeOfRoomsId { get; set; }
      
      
      /// <summary>
      /// Вид номера
      /// </summary>
      [Display(Description = "Вид номера")]
      [DataMember(Name = "typeOfRooms", EmitDefaultValue = false)]
      public virtual TypeOfRooms TypeOfRooms { get; set; }
      
      /// <summary>
      /// Тип мероприятия (однодневная, многодневная экскурсия, квест, интерактивное занятие, мастер-класс, корпоративное мероприятие
      /// </summary>
      [ForeignKey("TypeOfRestSubtype")]
      [DataMember(Name = "typeOfRestSubtypeId")]
      [Display(Description = "Тип мероприятия (однодневная, многодневная экскурсия, квест, интерактивное занятие, мастер-класс, корпоративное мероприятие")]
      public virtual long? TypeOfRestSubtypeId { get; set; }
      
      
      /// <summary>
      /// Тип мероприятия (однодневная, многодневная экскурсия, квест, интерактивное занятие, мастер-класс, корпоративное мероприятие
      /// </summary>
      [Display(Description = "Тип мероприятия (однодневная, многодневная экскурсия, квест, интерактивное занятие, мастер-класс, корпоративное мероприятие")]
      [DataMember(Name = "typeOfRestSubtype", EmitDefaultValue = false)]
      public virtual TypeOfRestSubtype TypeOfRestSubtype { get; set; }
      
      /// <summary>
      /// Тэги
      /// </summary>
      [InverseProperty("Services")]
      [DataMember(Name = "tags", EmitDefaultValue = false)]
      public virtual ICollection<Tag> Tags { get; set; }
      
      /// <summary>
      /// Тип подсчета стоимости
      /// </summary>
      [ForeignKey("TypePriceCalculation")]
      [DataMember(Name = "typePriceCalculationId")]
      [Display(Description = "Тип подсчета стоимости")]
      public virtual long? TypePriceCalculationId { get; set; }
      
      
      /// <summary>
      /// Тип подсчета стоимости
      /// </summary>
      [Display(Description = "Тип подсчета стоимости")]
      [DataMember(Name = "typePriceCalculation", EmitDefaultValue = false)]
      public virtual TypePriceCalculation TypePriceCalculation { get; set; }
      
      /// <summary>
      /// Транспорт
      /// </summary>
      [ForeignKey("TourTransport")]
      [DataMember(Name = "tourTransportId")]
      [Display(Description = "Транспорт")]
      public virtual long? TourTransportId { get; set; }
      
      
      /// <summary>
      /// Транспорт
      /// </summary>
      [Display(Description = "Транспорт")]
      [DataMember(Name = "tourTransport", EmitDefaultValue = false)]
      public virtual TourTransport TourTransport { get; set; }
      
      /// <summary>
      /// Регионы отдыха
      /// </summary>
      [InverseProperty("AddonServices")]
      [DataMember(Name = "placeOfRests", EmitDefaultValue = false)]
      public virtual ICollection<PlaceOfRest> PlaceOfRests { get; set; }
      
      /// <summary>
      /// Связанные услуги
      /// </summary>
      [ForeignKey("LinkService")]
      [DataMember(Name = "linkServiceId")]
      [Display(Description = "Связанные услуги")]
      public virtual long? LinkServiceId { get; set; }
      
      
      /// <summary>
      /// Связанные услуги
      /// </summary>
      [InverseProperty("LinkServices")]
      [Display(Description = "Связанные услуги")]
      [DataMember(Name = "linkService", EmitDefaultValue = false)]
      public virtual AddonServices LinkService { get; set; }

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