// File:    Hotels.cs
// Purpose: Definition of Class Hotels

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Отели/лагеря
   /// </summary>
   [Serializable]
   [DataContract(Name = "hotels")]
   public partial class Hotels : IEntityBase
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
      /// Название мета отдыха
      /// </summary>
      [Display(Description = "Название мета отдыха")]
      [MaxLength(1000, ErrorMessage = "\"Название мета отдыха\" не может быть больше 1000 символов")]
      [Required(ErrorMessage = "\"Название мета отдыха\" не может быть пустым")]
      [DataMember(Name = "name", EmitDefaultValue = false)]
      public virtual string Name { get; set; }
      
      /// <summary>
      /// Адрес места отдыха
      /// </summary>
      [Display(Description = "Адрес места отдыха")]
      [MaxLength(1000, ErrorMessage = "\"Адрес места отдыха\" не может быть больше 1000 символов")]
      [Required(ErrorMessage = "\"Адрес места отдыха\" не может быть пустым")]
      [DataMember(Name = "address", EmitDefaultValue = false)]
      public virtual string Address { get; set; }
      
      /// <summary>
      /// Наименование организации
      /// </summary>
      [Display(Description = "Наименование организации")]
      [MaxLength(1000, ErrorMessage = "\"Наименование организации\" не может быть больше 1000 символов")]
      [DataMember(Name = "nameOrganization", EmitDefaultValue = false)]
      public virtual string NameOrganization { get; set; }
      
      /// <summary>
      /// Телефон
      /// </summary>
      [Display(Description = "Телефон")]
      [MaxLength(1000, ErrorMessage = "\"Телефон\" не может быть больше 1000 символов")]
      [DataMember(Name = "phone", EmitDefaultValue = false)]
      public virtual string Phone { get; set; }
      
      /// <summary>
      /// Факс
      /// </summary>
      [Display(Description = "Факс")]
      [MaxLength(1000, ErrorMessage = "\"Факс\" не может быть больше 1000 символов")]
      [DataMember(Name = "fax", EmitDefaultValue = false)]
      public virtual string Fax { get; set; }
      
      /// <summary>
      /// </summary>
      [Display(Description = "")]
      [DataMember(Name = "email", EmitDefaultValue = false)]
      public virtual string Email { get; set; }
      
      /// <summary>
      /// </summary>
      [Display(Description = "")]
      [DataMember(Name = "url", EmitDefaultValue = false)]
      public virtual string Url { get; set; }
      
      /// <summary>
      /// </summary>
      [Display(Description = "")]
      [DataMember(Name = "head", EmitDefaultValue = false)]
      public virtual string Head { get; set; }
      
      /// <summary>
      /// </summary>
      [Display(Description = "")]
      [DataMember(Name = "headPosition", EmitDefaultValue = false)]
      public virtual string HeadPosition { get; set; }
      
      /// <summary>
      /// Маршрут проезда
      /// </summary>
      [Display(Description = "Маршрут проезда")]
      [DataMember(Name = "drivingDirections", EmitDefaultValue = false)]
      public virtual String DrivingDirections { get; set; }
      
      /// <summary>
      /// Количество корпусов
      /// </summary>
      [Display(Description = "Количество корпусов")]
      [MaxLength(1000, ErrorMessage = "\"Количество корпусов\" не может быть больше 1000 символов")]
      [DataMember(Name = "numberHousing", EmitDefaultValue = false)]
      public virtual string NumberHousing { get; set; }
      
      /// <summary>
      /// Площадь
      /// </summary>
      [Display(Description = "Площадь")]
      [DataMember(Name = "squere", EmitDefaultValue = false)]
      public virtual decimal? Squere { get; set; }
      
      /// <summary>
      /// </summary>
      [Display(Description = "")]
      [Required(ErrorMessage = "\"\" должно быть заполнено")]
      [DataMember(Name = "medicalOfficeAvailability", EmitDefaultValue = false)]
      public virtual bool MedicalOfficeAvailability { get; set; }
      
      /// <summary>
      /// </summary>
      [Display(Description = "")]
      [Required(ErrorMessage = "\"\" должно быть заполнено")]
      [DataMember(Name = "outdoorPondAvailability", EmitDefaultValue = false)]
      public virtual bool OutdoorPondAvailability { get; set; }
      
      /// <summary>
      /// </summary>
      [Display(Description = "")]
      [DataMember(Name = "outdoorPondName", EmitDefaultValue = false)]
      public virtual string OutdoorPondName { get; set; }
      
      /// <summary>
      /// </summary>
      [Display(Description = "")]
      [Required(ErrorMessage = "\"\" должно быть заполнено")]
      [DataMember(Name = "poolAvailability", EmitDefaultValue = false)]
      public virtual bool PoolAvailability { get; set; }
      
      /// <summary>
      /// </summary>
      [Display(Description = "")]
      [DataMember(Name = "securityInformation", EmitDefaultValue = false)]
      public virtual String SecurityInformation { get; set; }
      
      /// <summary>
      /// </summary>
      [Display(Description = "")]
      [Required(ErrorMessage = "\"\" должно быть заполнено")]
      [DataMember(Name = "computerClassAvailability", EmitDefaultValue = false)]
      public virtual bool ComputerClassAvailability { get; set; }
      
      /// <summary>
      /// </summary>
      [Display(Description = "")]
      [Required(ErrorMessage = "\"\" должно быть заполнено")]
      [DataMember(Name = "cinimaAvailability", EmitDefaultValue = false)]
      public virtual bool CinimaAvailability { get; set; }
      
      /// <summary>
      /// График работы кинозала
      /// </summary>
      [Display(Description = "График работы кинозала")]
      [DataMember(Name = "cinimaTimetable", EmitDefaultValue = false)]
      public virtual String CinimaTimetable { get; set; }
      
      /// <summary>
      /// </summary>
      [Display(Description = "")]
      [Required(ErrorMessage = "\"\" должно быть заполнено")]
      [DataMember(Name = "gymAvailability", EmitDefaultValue = false)]
      public virtual bool GymAvailability { get; set; }
      
      /// <summary>
      /// </summary>
      [Display(Description = "")]
      [Required(ErrorMessage = "\"\" должно быть заполнено")]
      [DataMember(Name = "libraryAvailability", EmitDefaultValue = false)]
      public virtual bool LibraryAvailability { get; set; }
      
      /// <summary>
      /// График работы библиотеки
      /// </summary>
      [Display(Description = "График работы библиотеки")]
      [DataMember(Name = "libraryTimetable", EmitDefaultValue = false)]
      public virtual String LibraryTimetable { get; set; }
      
      /// <summary>
      /// Доступная среда
      /// </summary>
      [Display(Description = "Доступная среда")]
      [Required(ErrorMessage = "\"Доступная среда\" должно быть заполнено")]
      [DataMember(Name = "accessibleEnvironment", EmitDefaultValue = false)]
      public virtual bool AccessibleEnvironment { get; set; }
      
      /// <summary>
      /// Принимает ребенка до 3-х лет
      /// </summary>
      [Display(Description = "Принимает ребенка до 3-х лет")]
      [Required(ErrorMessage = "\"Принимает ребенка до 3-х лет\" должно быть заполнено")]
      [DataMember(Name = "takeChildUp3Years", EmitDefaultValue = false)]
      public virtual bool TakeChildUp3Years { get; set; }
      
      /// <summary>
      /// Другой досуг
      /// </summary>
      [Display(Description = "Другой досуг")]
      [DataMember(Name = "otherLeisure", EmitDefaultValue = false)]
      public virtual String OtherLeisure { get; set; }
      
      /// <summary>
      /// Фио контактного лица
      /// </summary>
      [Display(Description = "Фио контактного лица")]
      [MaxLength(1000, ErrorMessage = "\"Фио контактного лица\" не может быть больше 1000 символов")]
      [DataMember(Name = "contactPerson", EmitDefaultValue = false)]
      public virtual string ContactPerson { get; set; }
      
      /// <summary>
      /// Контактный телефон
      /// </summary>
      [Display(Description = "Контактный телефон")]
      [MaxLength(1000, ErrorMessage = "\"Контактный телефон\" не может быть больше 1000 символов")]
      [DataMember(Name = "contactPhone", EmitDefaultValue = false)]
      public virtual string ContactPhone { get; set; }
      
      /// <summary>
      /// Описание места отдыха
      /// </summary>
      [Display(Description = "Описание места отдыха")]
      [DataMember(Name = "description", EmitDefaultValue = false)]
      public virtual String Description { get; set; }
      
      /// <summary>
      /// Широта
      /// </summary>
      [Display(Description = "Широта")]
      [DataMember(Name = "latitude", EmitDefaultValue = false)]
      public virtual decimal? Latitude { get; set; }
      
      /// <summary>
      /// Долгота
      /// </summary>
      [Display(Description = "Долгота")]
      [DataMember(Name = "longitude", EmitDefaultValue = false)]
      public virtual decimal? Longitude { get; set; }
      
      /// <summary>
      /// Возраст с
      /// </summary>
      [Display(Description = "Возраст с")]
      [DataMember(Name = "ageFrom", EmitDefaultValue = false)]
      public virtual int? AgeFrom { get; set; }
      
      /// <summary>
      /// Возраст по
      /// </summary>
      [Display(Description = "Возраст по")]
      [DataMember(Name = "ageTo", EmitDefaultValue = false)]
      public virtual int? AgeTo { get; set; }
      
      /// <summary>
      /// Удаленность от центра
      /// </summary>
      [Display(Description = "Удаленность от центра")]
      [DataMember(Name = "distanceFromCenter", EmitDefaultValue = false)]
      public virtual decimal? DistanceFromCenter { get; set; }
      
      /// <summary>
      /// Удаленность от пляжа
      /// </summary>
      [Display(Description = "Удаленность от пляжа")]
      [DataMember(Name = "distanceFromBeach", EmitDefaultValue = false)]
      public virtual decimal? DistanceFromBeach { get; set; }
      
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
      /// Не для сайта
      /// </summary>
      [Display(Description = "Не для сайта")]
      [Required(ErrorMessage = "\"Не для сайта\" должно быть заполнено")]
      [DataMember(Name = "notForSite", EmitDefaultValue = false)]
      public virtual bool NotForSite { get; set; }
      
      /// <summary>
      /// Участник ЛОК
      /// </summary>
      [Display(Description = "Участник ЛОК")]
      [Required(ErrorMessage = "\"Участник ЛОК\" должно быть заполнено")]
      [DataMember(Name = "isLok", EmitDefaultValue = false)]
      public virtual bool IsLok { get; set; }
      
      /// <summary>
      /// Описание для сайта
      /// </summary>
      [Display(Description = "Описание для сайта")]
      [DataMember(Name = "descriptionHtml", EmitDefaultValue = false)]
      public virtual String DescriptionHtml { get; set; }
      
      
      /// <summary>
      /// История
      /// </summary>
      [ForeignKey("HistoryLink")]
      [DataMember(Name = "historyLinkId")][Display(Description = "История")]
      public virtual long? HistoryLinkId { get; set; }
      /// <summary>
      /// История
      /// </summary>
      [Display(Description = "История")]
      [DataMember(Name = "historyLink", EmitDefaultValue = false)]
      public virtual HistoryLink HistoryLink { get; set; }
      
      /// <summary>
      /// Связь отелей и видов номеров
      /// </summary>
      [InverseProperty("Hotel")]
      [DataMember(Name = "typeOfRooms", EmitDefaultValue = false)]
      public virtual ICollection<TypeOfRooms> TypeOfRooms { get; set; }
      
      /// <summary>
      /// </summary>
      [InverseProperty("Hotel")]
      [DataMember(Name = "files", EmitDefaultValue = false)]
      public virtual ICollection<FileHotel> Files { get; set; }
      
      /// <summary>
      /// Контактные лица
      /// </summary>
      [InverseProperty("Hotel")]
      [DataMember(Name = "contacts", EmitDefaultValue = false)]
      public virtual ICollection<HotelContactPerson> Contacts { get; set; }
      
      /// <summary>
      /// Отель
      /// </summary>
      [InverseProperty("Hotel")]
      [DataMember(Name = "accommodation", EmitDefaultValue = false)]
      public virtual ICollection<Accommodation> Accommodation { get; set; }
      
      /// <summary>
      /// Отель
      /// </summary>
      [InverseProperty("Hotel")]
      [DataMember(Name = "diningOptions", EmitDefaultValue = false)]
      public virtual ICollection<DiningOptions> DiningOptions { get; set; }
      
      /// <summary>
      /// Отель
      /// </summary>
      [InverseProperty("Hotel")]
      [DataMember(Name = "prices", EmitDefaultValue = false)]
      public virtual ICollection<RoomRates> Prices { get; set; }
      
      /// <summary>
      /// Отель / лагерь <-> Услуги
      /// </summary>
      [InverseProperty("Hotels")]
      [DataMember(Name = "services", EmitDefaultValue = false)]
      public virtual ICollection<AddonServices> Services { get; set; }
      
      /// <summary>
      /// Цены
      /// </summary>
      [InverseProperty("Hotel")]
      [DataMember(Name = "matrixOfPrice", EmitDefaultValue = false)]
      public virtual ICollection<HotelPrice> MatrixOfPrice { get; set; }
      
      /// <summary>
      /// Отель
      /// </summary>
      [InverseProperty("Hotel")]
      [DataMember(Name = "placeBloks", EmitDefaultValue = false)]
      public virtual ICollection<HotelBlock> PlaceBloks { get; set; }
      
      /// <summary>
      /// Связь отеля и места отдыха
      /// </summary>
      [ForeignKey("PlaceOfRest")]
      [DataMember(Name = "placeOfRestId")]
      [Display(Description = "Связь отеля и места отдыха")]
      public virtual long? PlaceOfRestId { get; set; }
      
      
      /// <summary>
      /// Связь отеля и места отдыха
      /// </summary>
      [Display(Description = "Связь отеля и места отдыха")]
      [DataMember(Name = "placeOfRest", EmitDefaultValue = false)]
      public virtual PlaceOfRest PlaceOfRest { get; set; }
      
      /// <summary>
      /// Связь отеля и его вида
      /// </summary>
      [ForeignKey("HotelType")]
      [DataMember(Name = "hotelTypeId")]
      [Display(Description = "Связь отеля и его вида")]
      public virtual long? HotelTypeId { get; set; }
      
      
      /// <summary>
      /// Связь отеля и его вида
      /// </summary>
      [Display(Description = "Связь отеля и его вида")]
      [DataMember(Name = "hotelType", EmitDefaultValue = false)]
      public virtual HotelType HotelType { get; set; }
      
      /// <summary>
      /// Статус отеля
      /// </summary>
      [ForeignKey("State")]
      [DataMember(Name = "stateId")]
      [Display(Description = "Статус отеля")]
      public virtual long? StateId { get; set; }
      
      
      /// <summary>
      /// Статус отеля
      /// </summary>
      [Display(Description = "Статус отеля")]
      [DataMember(Name = "state", EmitDefaultValue = false)]
      public virtual StateMachineState State { get; set; }
      
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
      /// Победитель конкурса
      /// </summary>
      [ForeignKey("Organization")]
      [DataMember(Name = "organizationId")]
      [Display(Description = "Победитель конкурса")]
      public virtual long? OrganizationId { get; set; }
      
      
      /// <summary>
      /// Победитель конкурса
      /// </summary>
      [Display(Description = "Победитель конкурса")]
      [DataMember(Name = "organization", EmitDefaultValue = false)]
      public virtual Organization Organization { get; set; }
      
      /// <summary>
      /// Тип функционировани оздоровительной организации
      /// </summary>
      [ForeignKey("FunctioningType")]
      [DataMember(Name = "functioningTypeId")]
      [Display(Description = "Тип функционировани оздоровительной организации")]
      public virtual long? FunctioningTypeId { get; set; }
      
      
      /// <summary>
      /// Тип функционировани оздоровительной организации
      /// </summary>
      [Display(Description = "Тип функционировани оздоровительной организации")]
      [DataMember(Name = "functioningType", EmitDefaultValue = false)]
      public virtual FunctioningType FunctioningType { get; set; }
      
      /// <summary>
      /// Тип функционировани оздоровительной организации
      /// </summary>
      [ForeignKey("HotelPlacement")]
      [DataMember(Name = "hotelPlacementId")]
      [Display(Description = "Тип функционировани оздоровительной организации")]
      public virtual long? HotelPlacementId { get; set; }
      
      
      /// <summary>
      /// Тип функционировани оздоровительной организации
      /// </summary>
      [Display(Description = "Тип функционировани оздоровительной организации")]
      [DataMember(Name = "hotelPlacement", EmitDefaultValue = false)]
      public virtual HotelPlacement HotelPlacement { get; set; }
      
      /// <summary>
      /// Подтип вида Отеля
      /// </summary>
      [ForeignKey("SubHotelType")]
      [DataMember(Name = "subHotelTypeId")]
      [Display(Description = "Подтип вида Отеля")]
      public virtual long? SubHotelTypeId { get; set; }
      
      
      /// <summary>
      /// Подтип вида Отеля
      /// </summary>
      [Display(Description = "Подтип вида Отеля")]
      [DataMember(Name = "subHotelType", EmitDefaultValue = false)]
      public virtual HotelType SubHotelType { get; set; }

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