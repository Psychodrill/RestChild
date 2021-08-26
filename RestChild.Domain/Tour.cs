// File:    Tour.cs
// Purpose: Definition of Class Tour

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Реестр размещений
   /// </summary>
   [Serializable]
   [DataContract(Name = "tour")]
   public partial class Tour : IEntityBase
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
      /// Наименование
      /// </summary>
      [Display(Description = "Наименование")]
      [MaxLength(1000, ErrorMessage = "\"Наименование\" не может быть больше 1000 символов")]
      [DataMember(Name = "name", EmitDefaultValue = false)]
      public virtual string Name { get; set; }
      
      /// <summary>
      /// Описание
      /// </summary>
      [Display(Description = "Описание")]
      [DataMember(Name = "descr", EmitDefaultValue = false)]
      public virtual String Descr { get; set; }
      
      /// <summary>
      /// Активно
      /// </summary>
      [Display(Description = "Активно")]
      [Required(ErrorMessage = "\"Активно\" должно быть заполнено")]
      [DataMember(Name = "isActive", EmitDefaultValue = false)]
      public virtual bool IsActive { get; set; }
      
      /// <summary>
      /// Дата начала
      /// </summary>
      [Display(Description = "Дата начала")]
      [DataMember(Name = "dateIncome", EmitDefaultValue = false)]
      public virtual DateTime? DateIncome { get; set; }
      
      /// <summary>
      /// Дата окончания
      /// </summary>
      [Display(Description = "Дата окончания")]
      [DataMember(Name = "dateOutcome", EmitDefaultValue = false)]
      public virtual DateTime? DateOutcome { get; set; }
      
      /// <summary>
      /// Заезд для профильников
      /// </summary>
      [Display(Description = "Заезд для профильников")]
      [Required(ErrorMessage = "\"Заезд для профильников\" должно быть заполнено")]
      [DataMember(Name = "forList", EmitDefaultValue = false)]
      public virtual bool ForList { get; set; }
      
      /// <summary>
      /// Цена заезда
      /// </summary>
      [Display(Description = "Цена заезда")]
      [DataMember(Name = "tourPrice", EmitDefaultValue = false)]
      public virtual decimal? TourPrice { get; set; }
      
      /// <summary>
      /// Цена заезда для сопровождающего
      /// </summary>
      [Display(Description = "Цена заезда для сопровождающего")]
      [DataMember(Name = "tourPriceAttendant", EmitDefaultValue = false)]
      public virtual decimal? TourPriceAttendant { get; set; }
      
      /// <summary>
      /// Дата начала записи
      /// </summary>
      [Display(Description = "Дата начала записи")]
      [DataMember(Name = "startBooking", EmitDefaultValue = false)]
      public virtual DateTime? StartBooking { get; set; }
      
      /// <summary>
      /// Дата окончания записи
      /// </summary>
      [Display(Description = "Дата окончания записи")]
      [DataMember(Name = "endBooking", EmitDefaultValue = false)]
      public virtual DateTime? EndBooking { get; set; }
      
      /// <summary>
      /// Ключ для формирования отрядов (Год, Дата с, Дата по, место отдыха)
      /// </summary>
      [Display(Description = "Ключ для формирования отрядов (Год, Дата с, Дата по, место отдыха)")]
      [MaxLength(1000, ErrorMessage = "\"Ключ для формирования отрядов (Год, Дата с, Дата по, место отдыха)\" не может быть больше 1000 символов")]
      [DataMember(Name = "surrogateKey", EmitDefaultValue = false)]
      public virtual string SurrogateKey { get; set; }
      
      /// <summary>
      /// Номера корпусов
      /// </summary>
      [Display(Description = "Номера корпусов")]
      [MaxLength(1000, ErrorMessage = "\"Номера корпусов\" не может быть больше 1000 символов")]
      [DataMember(Name = "corpusNumber", EmitDefaultValue = false)]
      public virtual string CorpusNumber { get; set; }
      
      /// <summary>
      /// Стоимость дополнительного места взрослого
      /// </summary>
      [Display(Description = "Стоимость дополнительного места взрослого")]
      [DataMember(Name = "paymentForAdult", EmitDefaultValue = false)]
      public virtual decimal? PaymentForAdult { get; set; }
      
      /// <summary>
      /// Стоимость дополнительного места ребенка
      /// </summary>
      [Display(Description = "Стоимость дополнительного места ребенка")]
      [DataMember(Name = "paymentForChild", EmitDefaultValue = false)]
      public virtual decimal? PaymentForChild { get; set; }
      
      /// <summary>
      /// Возраст ребенка с
      /// </summary>
      [Display(Description = "Возраст ребенка с")]
      [DataMember(Name = "childAgeFrom", EmitDefaultValue = false)]
      public virtual int? ChildAgeFrom { get; set; }
      
      /// <summary>
      /// Возраст ребенка по
      /// </summary>
      [Display(Description = "Возраст ребенка по")]
      [DataMember(Name = "childAgeTo", EmitDefaultValue = false)]
      public virtual int? ChildAgeTo { get; set; }
      
      /// <summary>
      /// Не самостоятельное размещение
      /// </summary>
      [Display(Description = "Не самостоятельное размещение")]
      [Required(ErrorMessage = "\"Не самостоятельное размещение\" должно быть заполнено")]
      [DataMember(Name = "notSelf", EmitDefaultValue = false)]
      public virtual bool NotSelf { get; set; }
      
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
      /// Требуется подтверждение
      /// </summary>
      [Display(Description = "Требуется подтверждение")]
      [Required(ErrorMessage = "\"Требуется подтверждение\" должно быть заполнено")]
      [DataMember(Name = "needApprove", EmitDefaultValue = false)]
      public virtual bool NeedApprove { get; set; }
      
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
      /// Множественные географические объекты
      /// </summary>
      [Display(Description = "Множественные географические объекты")]
      [Required(ErrorMessage = "\"Множественные географические объекты\" должно быть заполнено")]
      [DataMember(Name = "multiEventGeography", EmitDefaultValue = false)]
      public virtual bool MultiEventGeography { get; set; }
      
      /// <summary>
      /// Число заявок ограничено
      /// </summary>
      [Display(Description = "Число заявок ограничено")]
      [Required(ErrorMessage = "\"Число заявок ограничено\" должно быть заполнено")]
      [DataMember(Name = "countRequestLimited", EmitDefaultValue = false)]
      public virtual bool CountRequestLimited { get; set; }
      
      /// <summary>
      /// Минимальный размер предоплаты
      /// </summary>
      [Display(Description = "Минимальный размер предоплаты")]
      [DataMember(Name = "minPrepaymentAmount", EmitDefaultValue = false)]
      public virtual decimal? MinPrepaymentAmount { get; set; }
      
      /// <summary>
      /// Произвольные даты оказания услуги
      /// </summary>
      [Display(Description = "Произвольные даты оказания услуги")]
      [Required(ErrorMessage = "\"Произвольные даты оказания услуги\" должно быть заполнено")]
      [DataMember(Name = "notFixedDate", EmitDefaultValue = false)]
      public virtual bool NotFixedDate { get; set; }
      
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
      /// Процент надбавки
      /// </summary>
      [Display(Description = "Процент надбавки")]
      [DataMember(Name = "procentOver", EmitDefaultValue = false)]
      public virtual decimal? ProcentOver { get; set; }
      
      /// <summary>
      /// Можно платить аванс
      /// </summary>
      [Display(Description = "Можно платить аванс")]
      [Required(ErrorMessage = "\"Можно платить аванс\" должно быть заполнено")]
      [DataMember(Name = "prepaymentMayBe", EmitDefaultValue = false)]
      public virtual bool PrepaymentMayBe { get; set; }
      
      /// <summary>
      /// Процент аванса
      /// </summary>
      [Display(Description = "Процент аванса")]
      [DataMember(Name = "prepaymentProcent", EmitDefaultValue = false)]
      public virtual decimal? PrepaymentProcent { get; set; }
      
      /// <summary>
      /// Не для сайта
      /// </summary>
      [Display(Description = "Не для сайта")]
      [Required(ErrorMessage = "\"Не для сайта\" должно быть заполнено")]
      [DataMember(Name = "notForSite", EmitDefaultValue = false)]
      public virtual bool NotForSite { get; set; }
      
      /// <summary>
      /// Размещение для многоэтапной кампании
      /// </summary>
      [Display(Description = "Размещение для многоэтапной кампании")]
      [Required(ErrorMessage = "\"Размещение для многоэтапной кампании\" должно быть заполнено")]
      [DataMember(Name = "forMultipleStageCompany", EmitDefaultValue = false)]
      public virtual bool ForMultipleStageCompany { get; set; }
      
      /// <summary>
      /// Размещение для колясочников
      /// </summary>
      [Display(Description = "Размещение для колясочников")]
      [Required(ErrorMessage = "\"Размещение для колясочников\" должно быть заполнено")]
      [DataMember(Name = "forInvalid", EmitDefaultValue = false)]
      public virtual bool ForInvalid { get; set; }
      
      
      /// <summary>
      /// Связь заездов и спика детей
      /// </summary>
      [InverseProperty("Tour")]
      [DataMember(Name = "listOfChilds", EmitDefaultValue = false)]
      public virtual ICollection<ListOfChilds> ListOfChilds { get; set; }
      
      /// <summary>
      /// Ссылка на квоту
      /// </summary>
      [InverseProperty("Tour")]
      [DataMember(Name = "limitOnOrganizations", EmitDefaultValue = false)]
      public virtual ICollection<LimitOnOrganization> LimitOnOrganizations { get; set; }
      
      /// <summary>
      /// Связь заезда и номерного фонда
      /// </summary>
      [InverseProperty("Tour")]
      [DataMember(Name = "volumes", EmitDefaultValue = false)]
      public virtual ICollection<TourVolume> Volumes { get; set; }
      
      /// <summary>
      /// Связь заезда и фотографий
      /// </summary>
      [InverseProperty("Tour")]
      [DataMember(Name = "files", EmitDefaultValue = false)]
      public virtual ICollection<FileOfTour> Files { get; set; }
      
      /// <summary>
      /// Связь заезда и заявления
      /// </summary>
      [InverseProperty("Tour")]
      [DataMember(Name = "requestsSingle", EmitDefaultValue = false)]
      public virtual ICollection<Request> RequestsSingle { get; set; }
      
      /// <summary>
      /// Стоимость места
      /// </summary>
      [InverseProperty("Tour")]
      [DataMember(Name = "prices", EmitDefaultValue = false)]
      public virtual ICollection<TourPrice> Prices { get; set; }
      
      /// <summary>
      /// Связь размещений для комплексного размещения
      /// </summary>
      [InverseProperty("ComplexItem")]
      [DataMember(Name = "parts", EmitDefaultValue = false)]
      public virtual ICollection<Tour> Parts { get; set; }
      
      /// <summary>
      /// Стоимость номеров
      /// </summary>
      [InverseProperty("Tour")]
      [DataMember(Name = "roomRates", EmitDefaultValue = false)]
      public virtual ICollection<RoomRates> RoomRates { get; set; }
      
      /// <summary>
      /// Связь продуктов и заявлений
      /// </summary>
      [InverseProperty("Tours")]
      [DataMember(Name = "requests", EmitDefaultValue = false)]
      public virtual ICollection<Request> Requests { get; set; }
      
      /// <summary>
      /// Продукты <-> Дети
      /// </summary>
      [InverseProperty("Tours")]
      [DataMember(Name = "tourChilds", EmitDefaultValue = false)]
      public virtual ICollection<Child> TourChilds { get; set; }
      
      /// <summary>
      /// Продукты <-> Сопровождающий
      /// </summary>
      [InverseProperty("Tours")]
      [DataMember(Name = "applicants", EmitDefaultValue = false)]
      public virtual ICollection<Applicant> Applicants { get; set; }
      
      /// <summary>
      /// Проживания
      /// </summary>
      [InverseProperty("Tour")]
      [DataMember(Name = "tourAccommodations", EmitDefaultValue = false)]
      public virtual ICollection<TourAccommodation> TourAccommodations { get; set; }
      
      /// <summary>
      /// Транспорт
      /// </summary>
      [InverseProperty("Tour")]
      [DataMember(Name = "tourTransport", EmitDefaultValue = false)]
      public virtual ICollection<TourTransport> TourTransport { get; set; }
      
      /// <summary>
      /// География мероприятия
      /// </summary>
      [InverseProperty("Tour")]
      [DataMember(Name = "eventGeographys", EmitDefaultValue = false)]
      public virtual ICollection<EventGeography> EventGeographys { get; set; }
      
      /// <summary>
      /// Связь страны и размещения
      /// </summary>
      [InverseProperty("Tour")]
      [DataMember(Name = "countrys", EmitDefaultValue = false)]
      public virtual ICollection<TourCountry> Countrys { get; set; }
      
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
      /// Ссылка на заезд
      /// </summary>
      [ForeignKey("LimitOnVedomstvo")]
      [DataMember(Name = "limitOnVedomstvoId")]
      [Display(Description = "Ссылка на заезд")]
      public virtual long? LimitOnVedomstvoId { get; set; }
      
      
      /// <summary>
      /// Ссылка на заезд
      /// </summary>
      [Display(Description = "Ссылка на заезд")]
      [DataMember(Name = "limitOnVedomstvo", EmitDefaultValue = false)]
      public virtual LimitOnVedomstvo LimitOnVedomstvo { get; set; }
      
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
      /// </summary>
      [ForeignKey("TypeOfRest")]
      [DataMember(Name = "typeOfRestId")]
      [Display(Description = "")]
      public virtual long? TypeOfRestId { get; set; }
      
      
      /// <summary>
      /// </summary>
      [Display(Description = "")]
      [DataMember(Name = "typeOfRest", EmitDefaultValue = false)]
      public virtual TypeOfRest TypeOfRest { get; set; }
      
      /// <summary>
      /// </summary>
      [ForeignKey("SubjectOfRest")]
      [DataMember(Name = "subjectOfRestId")]
      [Display(Description = "")]
      public virtual long? SubjectOfRestId { get; set; }
      
      
      /// <summary>
      /// </summary>
      [Display(Description = "")]
      [DataMember(Name = "subjectOfRest", EmitDefaultValue = false)]
      public virtual SubjectOfRest SubjectOfRest { get; set; }
      
      /// <summary>
      /// </summary>
      [ForeignKey("TimeOfRest")]
      [DataMember(Name = "timeOfRestId")]
      [Display(Description = "")]
      public virtual long? TimeOfRestId { get; set; }
      
      
      /// <summary>
      /// </summary>
      [Display(Description = "")]
      [DataMember(Name = "timeOfRest", EmitDefaultValue = false)]
      public virtual TimeOfRest TimeOfRest { get; set; }
      
      /// <summary>
      /// Связь заезда и отеля
      /// </summary>
      [ForeignKey("Hotels")]
      [DataMember(Name = "hotelsId")]
      [Display(Description = "Связь заезда и отеля")]
      public virtual long? HotelsId { get; set; }
      
      
      /// <summary>
      /// Связь заезда и отеля
      /// </summary>
      [Display(Description = "Связь заезда и отеля")]
      [DataMember(Name = "hotels", EmitDefaultValue = false)]
      public virtual Hotels Hotels { get; set; }
      
      /// <summary>
      /// Статус заезда
      /// </summary>
      [ForeignKey("State")]
      [DataMember(Name = "stateId")]
      [Display(Description = "Статус заезда")]
      public virtual long? StateId { get; set; }
      
      
      /// <summary>
      /// Статус заезда
      /// </summary>
      [Display(Description = "Статус заезда")]
      [DataMember(Name = "state", EmitDefaultValue = false)]
      public virtual StateMachineState State { get; set; }
      
      /// <summary>
      /// Связь с блоком мест
      /// </summary>
      [ForeignKey("Bout")]
      [DataMember(Name = "boutId")]
      [Display(Description = "Связь с блоком мест")]
      public virtual long? BoutId { get; set; }
      
      
      /// <summary>
      /// Связь с блоком мест
      /// </summary>
      [InverseProperty("Tours")]
      [Display(Description = "Связь с блоком мест")]
      [DataMember(Name = "bout", EmitDefaultValue = false)]
      public virtual Bout Bout { get; set; }
      
      /// <summary>
      /// Отряд и блок мест
      /// </summary>
      [InverseProperty("Tours")]
      [DataMember(Name = "partys", EmitDefaultValue = false)]
      public virtual ICollection<Party> Partys { get; set; }
      
      /// <summary>
      /// Связь размещений для комплексного размещения
      /// </summary>
      [ForeignKey("ComplexItem")]
      [DataMember(Name = "complexItemId")]
      [Display(Description = "Связь размещений для комплексного размещения")]
      public virtual long? ComplexItemId { get; set; }
      
      
      /// <summary>
      /// Связь размещений для комплексного размещения
      /// </summary>
      [InverseProperty("Parts")]
      [Display(Description = "Связь размещений для комплексного размещения")]
      [DataMember(Name = "complexItem", EmitDefaultValue = false)]
      public virtual Tour ComplexItem { get; set; }
      
      /// <summary>
      /// Ссылка на файлы
      /// </summary>
      [ForeignKey("LinkToFile")]
      [DataMember(Name = "linkToFileId")]
      [Display(Description = "Ссылка на файлы")]
      public virtual long? LinkToFileId { get; set; }
      
      
      /// <summary>
      /// Ссылка на файлы
      /// </summary>
      [Display(Description = "Ссылка на файлы")]
      [DataMember(Name = "linkToFile", EmitDefaultValue = false)]
      public virtual LinkToFile LinkToFile { get; set; }
      
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
      /// Группа ограничений
      /// </summary>
      [ForeignKey("RestrictionGroup")]
      [DataMember(Name = "restrictionGroupId")]
      [Display(Description = "Группа ограничений")]
      public virtual long? RestrictionGroupId { get; set; }
      
      
      /// <summary>
      /// Группа ограничений
      /// </summary>
      [Display(Description = "Группа ограничений")]
      [DataMember(Name = "restrictionGroup", EmitDefaultValue = false)]
      public virtual RestrictionGroup RestrictionGroup { get; set; }
      
      /// <summary>
      /// </summary>
      [ForeignKey("SignInfo")]
      [DataMember(Name = "signInfoId")]
      [Display(Description = "")]
      public virtual long? SignInfoId { get; set; }
      
      
      /// <summary>
      /// </summary>
      [Display(Description = "")]
      [DataMember(Name = "signInfo", EmitDefaultValue = false)]
      public virtual SignInfo SignInfo { get; set; }
      
      /// <summary>
      /// Договор
      /// </summary>
      [ForeignKey("Contract")]
      [DataMember(Name = "contractId")]
      [Display(Description = "Договор")]
      public virtual long? ContractId { get; set; }
      
      
      /// <summary>
      /// Договор
      /// </summary>
      [InverseProperty("Tour")]
      [Display(Description = "Договор")]
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
      /// Город отбытия
      /// </summary>
      [ForeignKey("City")]
      [DataMember(Name = "cityId")]
      [Display(Description = "Город отбытия")]
      public virtual long? CityId { get; set; }
      
      
      /// <summary>
      /// Город отбытия
      /// </summary>
      [Display(Description = "Город отбытия")]
      [DataMember(Name = "city", EmitDefaultValue = false)]
      public virtual City City { get; set; }
      
      /// <summary>
      /// Основная услуга
      /// </summary>
      [ForeignKey("BaseService")]
      [DataMember(Name = "baseServiceId")]
      [Display(Description = "Основная услуга")]
      public virtual long? BaseServiceId { get; set; }
      
      
      /// <summary>
      /// Основная услуга
      /// </summary>
      [Display(Description = "Основная услуга")]
      [DataMember(Name = "baseService", EmitDefaultValue = false)]
      public virtual AddonServices BaseService { get; set; }
      
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
      /// Список продуктов
      /// </summary>
      [ForeignKey("Product")]
      [DataMember(Name = "productId")]
      [Display(Description = "Список продуктов")]
      public virtual long? ProductId { get; set; }
      
      
      /// <summary>
      /// Список продуктов
      /// </summary>
      [InverseProperty("Tours")]
      [Display(Description = "Список продуктов")]
      [DataMember(Name = "product", EmitDefaultValue = false)]
      public virtual Product Product { get; set; }
      
      /// <summary>
      /// Тэги
      /// </summary>
      [InverseProperty("Tours")]
      [DataMember(Name = "tags", EmitDefaultValue = false)]
      public virtual ICollection<Tag> Tags { get; set; }
      
      /// <summary>
      /// Акции <-> Реестр размещения
      /// </summary>
      [InverseProperty("Tours")]
      [DataMember(Name = "discounts", EmitDefaultValue = false)]
      public virtual ICollection<Discount> Discounts { get; set; }

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