// File:    RoomRates.cs
// Purpose: Definition of Class RoomRates

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Стоимость номеров/проживания
   /// </summary>
   [Serializable]
   [DataContract(Name = "roomRates")]
   public partial class RoomRates : IEntityBase
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
      /// Цена
      /// </summary>
      [Display(Description = "Цена")]
      [DataMember(Name = "price", EmitDefaultValue = false)]
      public virtual decimal? Price { get; set; }
      
      /// <summary>
      /// Дата с (действие)
      /// </summary>
      [Display(Description = "Дата с (действие)")]
      [DataMember(Name = "dateFrom", EmitDefaultValue = false)]
      public virtual DateTime? DateFrom { get; set; }
      
      /// <summary>
      /// Дата по (действие)
      /// </summary>
      [Display(Description = "Дата по (действие)")]
      [DataMember(Name = "dateTo", EmitDefaultValue = false)]
      public virtual DateTime? DateTo { get; set; }
      
      /// <summary>
      /// Дополнительное место
      /// </summary>
      [Display(Description = "Дополнительное место")]
      [Required(ErrorMessage = "\"Дополнительное место\" должно быть заполнено")]
      [DataMember(Name = "isAddonPlace", EmitDefaultValue = false)]
      public virtual bool IsAddonPlace { get; set; }
      
      /// <summary>
      /// Цена внутренняя
      /// </summary>
      [Display(Description = "Цена внутренняя")]
      [DataMember(Name = "priceInternal", EmitDefaultValue = false)]
      public virtual decimal? PriceInternal { get; set; }
      
      /// <summary>
      /// Общая цена
      /// </summary>
      [Display(Description = "Общая цена")]
      [DataMember(Name = "priceTotal", EmitDefaultValue = false)]
      public virtual decimal? PriceTotal { get; set; }
      
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
      /// Отель
      /// </summary>
      [ForeignKey("Hotel")]
      [DataMember(Name = "hotelId")]
      [Display(Description = "Отель")]
      public virtual long? HotelId { get; set; }
      
      
      /// <summary>
      /// Отель
      /// </summary>
      [InverseProperty("Prices")]
      [Display(Description = "Отель")]
      [DataMember(Name = "hotel", EmitDefaultValue = false)]
      public virtual Hotels Hotel { get; set; }
      
      /// <summary>
      /// Вариант питания
      /// </summary>
      [ForeignKey("DiningOptions")]
      [DataMember(Name = "diningOptionsId")]
      [Display(Description = "Вариант питания")]
      public virtual long? DiningOptionsId { get; set; }
      
      
      /// <summary>
      /// Вариант питания
      /// </summary>
      [Display(Description = "Вариант питания")]
      [DataMember(Name = "diningOptions", EmitDefaultValue = false)]
      public virtual DiningOptions DiningOptions { get; set; }
      
      /// <summary>
      /// Вариант размещения
      /// </summary>
      [ForeignKey("Accommodation")]
      [DataMember(Name = "accommodationId")]
      [Display(Description = "Вариант размещения")]
      public virtual long? AccommodationId { get; set; }
      
      
      /// <summary>
      /// Вариант размещения
      /// </summary>
      [Display(Description = "Вариант размещения")]
      [DataMember(Name = "accommodation", EmitDefaultValue = false)]
      public virtual Accommodation Accommodation { get; set; }
      
      /// <summary>
      /// Вариант комнаты
      /// </summary>
      [ForeignKey("TypeOfRooms")]
      [DataMember(Name = "typeOfRoomsId")]
      [Display(Description = "Вариант комнаты")]
      public virtual long? TypeOfRoomsId { get; set; }
      
      
      /// <summary>
      /// Вариант комнаты
      /// </summary>
      [Display(Description = "Вариант комнаты")]
      [DataMember(Name = "typeOfRooms", EmitDefaultValue = false)]
      public virtual TypeOfRooms TypeOfRooms { get; set; }
      
      /// <summary>
      /// Год отдыха
      /// </summary>
      [ForeignKey("YearOfRest")]
      [DataMember(Name = "yearOfRestId")]
      [Display(Description = "Год отдыха")]
      public virtual long? YearOfRestId { get; set; }
      
      
      /// <summary>
      /// Год отдыха
      /// </summary>
      [Display(Description = "Год отдыха")]
      [DataMember(Name = "yearOfRest", EmitDefaultValue = false)]
      public virtual YearOfRest YearOfRest { get; set; }
      
      /// <summary>
      /// Стоимость номеров
      /// </summary>
      [ForeignKey("Tour")]
      [DataMember(Name = "tourId")]
      [Display(Description = "Стоимость номеров")]
      public virtual long? TourId { get; set; }
      
      
      /// <summary>
      /// Стоимость номеров
      /// </summary>
      [InverseProperty("RoomRates")]
      [Display(Description = "Стоимость номеров")]
      [DataMember(Name = "tour", EmitDefaultValue = false)]
      public virtual Tour Tour { get; set; }
      
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
      /// Варианты размещения
      /// </summary>
      [ForeignKey("TourAccommodation")]
      [DataMember(Name = "tourAccommodationId")]
      [Display(Description = "Варианты размещения")]
      public virtual long? TourAccommodationId { get; set; }
      
      
      /// <summary>
      /// Варианты размещения
      /// </summary>
      [InverseProperty("RoomRates")]
      [Display(Description = "Варианты размещения")]
      [DataMember(Name = "tourAccommodation", EmitDefaultValue = false)]
      public virtual TourAccommodation TourAccommodation { get; set; }

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