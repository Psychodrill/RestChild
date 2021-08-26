// File:    RequestAccommodation.cs
// Purpose: Definition of Class RequestAccommodation

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Проживание в заявлении
   /// </summary>
   [Serializable]
   [DataContract(Name = "requestAccommodation")]
   public partial class RequestAccommodation : IEntityBase
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
      /// Дата с
      /// </summary>
      [Display(Description = "Дата с")]
      [DataMember(Name = "dateFrom", EmitDefaultValue = false)]
      public virtual DateTime? DateFrom { get; set; }
      
      /// <summary>
      /// Дата по
      /// </summary>
      [Display(Description = "Дата по")]
      [DataMember(Name = "dateTo", EmitDefaultValue = false)]
      public virtual DateTime? DateTo { get; set; }
      
      /// <summary>
      /// Цена
      /// </summary>
      [Display(Description = "Цена")]
      [DataMember(Name = "price", EmitDefaultValue = false)]
      public virtual decimal? Price { get; set; }
      
      /// <summary>
      /// Себестоимость
      /// </summary>
      [Display(Description = "Себестоимость")]
      [DataMember(Name = "priceInternal", EmitDefaultValue = false)]
      public virtual decimal? PriceInternal { get; set; }
      
      /// <summary>
      /// Подтверждено
      /// </summary>
      [Display(Description = "Подтверждено")]
      [Required(ErrorMessage = "\"Подтверждено\" должно быть заполнено")]
      [DataMember(Name = "approved", EmitDefaultValue = false)]
      public virtual bool Approved { get; set; }
      
      /// <summary>
      /// Для формирования начисления
      /// </summary>
      [Display(Description = "Для формирования начисления")]
      [Required(ErrorMessage = "\"Для формирования начисления\" должно быть заполнено")]
      [DataMember(Name = "forCalculation", EmitDefaultValue = false)]
      public virtual bool ForCalculation { get; set; }
      
      
      /// <summary>
      /// Отдыхающие
      /// </summary>
      [InverseProperty("RequestAccommodation")]
      [DataMember(Name = "requestAccommodationLinks", EmitDefaultValue = false)]
      public virtual ICollection<RequestAccommodationLink> RequestAccommodationLinks { get; set; }
      
      /// <summary>
      /// Проживание в заявлении - начисление
      /// </summary>
      [InverseProperty("RequestAccommodations")]
      [DataMember(Name = "calculations", EmitDefaultValue = false)]
      public virtual ICollection<Calculation> Calculations { get; set; }
      
      /// <summary>
      /// Проживания
      /// </summary>
      [ForeignKey("Request")]
      [DataMember(Name = "requestId")]
      [Display(Description = "Проживания")]
      public virtual long? RequestId { get; set; }
      
      
      /// <summary>
      /// Проживания
      /// </summary>
      [InverseProperty("RequestAccommodations")]
      [Display(Description = "Проживания")]
      [DataMember(Name = "request", EmitDefaultValue = false)]
      public virtual Request Request { get; set; }
      
      /// <summary>
      /// Объем фонда
      /// </summary>
      [ForeignKey("TourVolume")]
      [DataMember(Name = "tourVolumeId")]
      [Display(Description = "Объем фонда")]
      public virtual long? TourVolumeId { get; set; }
      
      
      /// <summary>
      /// Объем фонда
      /// </summary>
      [Display(Description = "Объем фонда")]
      [DataMember(Name = "tourVolume", EmitDefaultValue = false)]
      public virtual TourVolume TourVolume { get; set; }
      
      /// <summary>
      /// Стоимость номеров/проживания
      /// </summary>
      [ForeignKey("RoomRates")]
      [DataMember(Name = "roomRatesId")]
      [Display(Description = "Стоимость номеров/проживания")]
      public virtual long? RoomRatesId { get; set; }
      
      
      /// <summary>
      /// Стоимость номеров/проживания
      /// </summary>
      [Display(Description = "Стоимость номеров/проживания")]
      [DataMember(Name = "roomRates", EmitDefaultValue = false)]
      public virtual RoomRates RoomRates { get; set; }
      
      /// <summary>
      /// Виды номеров
      /// </summary>
      [ForeignKey("TypeOfRooms")]
      [DataMember(Name = "typeOfRoomsId")]
      [Display(Description = "Виды номеров")]
      public virtual long? TypeOfRoomsId { get; set; }
      
      
      /// <summary>
      /// Виды номеров
      /// </summary>
      [Display(Description = "Виды номеров")]
      [DataMember(Name = "typeOfRooms", EmitDefaultValue = false)]
      public virtual TypeOfRooms TypeOfRooms { get; set; }
      
      /// <summary>
      /// Варианты питания
      /// </summary>
      [ForeignKey("DiningOptions")]
      [DataMember(Name = "diningOptionsId")]
      [Display(Description = "Варианты питания")]
      public virtual long? DiningOptionsId { get; set; }
      
      
      /// <summary>
      /// Варианты питания
      /// </summary>
      [Display(Description = "Варианты питания")]
      [DataMember(Name = "diningOptions", EmitDefaultValue = false)]
      public virtual DiningOptions DiningOptions { get; set; }
      
      /// <summary>
      /// Отели/лагеря
      /// </summary>
      [ForeignKey("Hotels")]
      [DataMember(Name = "hotelsId")]
      [Display(Description = "Отели/лагеря")]
      public virtual long? HotelsId { get; set; }
      
      
      /// <summary>
      /// Отели/лагеря
      /// </summary>
      [Display(Description = "Отели/лагеря")]
      [DataMember(Name = "hotels", EmitDefaultValue = false)]
      public virtual Hotels Hotels { get; set; }
      
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
      /// Размещение
      /// </summary>
      [ForeignKey("Accommodation")]
      [DataMember(Name = "accommodationId")]
      [Display(Description = "Размещение")]
      public virtual long? AccommodationId { get; set; }
      
      
      /// <summary>
      /// Размещение
      /// </summary>
      [Display(Description = "Размещение")]
      [DataMember(Name = "accommodation", EmitDefaultValue = false)]
      public virtual Accommodation Accommodation { get; set; }
      
      /// <summary>
      /// Привязка к этапу мероприятия
      /// </summary>
      [ForeignKey("EventGeography")]
      [DataMember(Name = "eventGeographyId")]
      [Display(Description = "Привязка к этапу мероприятия")]
      public virtual long? EventGeographyId { get; set; }
      
      
      /// <summary>
      /// Привязка к этапу мероприятия
      /// </summary>
      [Display(Description = "Привязка к этапу мероприятия")]
      [DataMember(Name = "eventGeography", EmitDefaultValue = false)]
      public virtual EventGeography EventGeography { get; set; }

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