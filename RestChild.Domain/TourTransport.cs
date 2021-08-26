// File:    TourTransport.cs
// Purpose: Definition of Class TourTransport

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Транспортные услуги по продукту
   /// </summary>
   [Serializable]
   [DataContract(Name = "tourTransport")]
   public partial class TourTransport : IEntityBase
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
      [DataMember(Name = "name", EmitDefaultValue = false)]
      public virtual String Name { get; set; }
      
      /// <summary>
      /// Дата вылета
      /// </summary>
      [Display(Description = "Дата вылета")]
      [DataMember(Name = "dateOfDeparture", EmitDefaultValue = false)]
      public virtual DateTime? DateOfDeparture { get; set; }
      
      /// <summary>
      /// Дата прибытия
      /// </summary>
      [Display(Description = "Дата прибытия")]
      [DataMember(Name = "dateOfArrival", EmitDefaultValue = false)]
      public virtual DateTime? DateOfArrival { get; set; }
      
      /// <summary>
      /// Место отправления
      /// </summary>
      [Display(Description = "Место отправления")]
      [DataMember(Name = "placeOfDeparture", EmitDefaultValue = false)]
      public virtual String PlaceOfDeparture { get; set; }
      
      /// <summary>
      /// Место прибытия
      /// </summary>
      [Display(Description = "Место прибытия")]
      [DataMember(Name = "placeOfArrival", EmitDefaultValue = false)]
      public virtual String PlaceOfArrival { get; set; }
      
      /// <summary>
      /// Требуется подтверждение
      /// </summary>
      [Display(Description = "Требуется подтверждение")]
      [Required(ErrorMessage = "\"Требуется подтверждение\" должно быть заполнено")]
      [DataMember(Name = "needApprove", EmitDefaultValue = false)]
      public virtual bool NeedApprove { get; set; }
      
      
      /// <summary>
      /// Стоимость
      /// </summary>
      [InverseProperty("TourTransport")]
      [DataMember(Name = "prices", EmitDefaultValue = false)]
      public virtual ICollection<TourTransportPrice> Prices { get; set; }
      
      /// <summary>
      /// Транспорт
      /// </summary>
      [ForeignKey("Tour")]
      [DataMember(Name = "tourId")]
      [Display(Description = "Транспорт")]
      public virtual long? TourId { get; set; }
      
      
      /// <summary>
      /// Транспорт
      /// </summary>
      [InverseProperty("TourTransport")]
      [Display(Description = "Транспорт")]
      [DataMember(Name = "tour", EmitDefaultValue = false)]
      public virtual Tour Tour { get; set; }
      
      /// <summary>
      /// Город прибытия
      /// </summary>
      [ForeignKey("CityOfArrival")]
      [DataMember(Name = "cityOfArrivalId")]
      [Display(Description = "Город прибытия")]
      public virtual long? CityOfArrivalId { get; set; }
      
      
      /// <summary>
      /// Город прибытия
      /// </summary>
      [Display(Description = "Город прибытия")]
      [DataMember(Name = "cityOfArrival", EmitDefaultValue = false)]
      public virtual City CityOfArrival { get; set; }
      
      /// <summary>
      /// Город отправления
      /// </summary>
      [ForeignKey("CityOfDeparture")]
      [DataMember(Name = "cityOfDepartureId")]
      [Display(Description = "Город отправления")]
      public virtual long? CityOfDepartureId { get; set; }
      
      
      /// <summary>
      /// Город отправления
      /// </summary>
      [Display(Description = "Город отправления")]
      [DataMember(Name = "cityOfDeparture", EmitDefaultValue = false)]
      public virtual City CityOfDeparture { get; set; }
      
      /// <summary>
      /// Рейс
      /// </summary>
      [ForeignKey("DirectoryFlights")]
      [DataMember(Name = "directoryFlightsId")]
      [Display(Description = "Рейс")]
      public virtual long? DirectoryFlightsId { get; set; }
      
      
      /// <summary>
      /// Рейс
      /// </summary>
      [Display(Description = "Рейс")]
      [DataMember(Name = "directoryFlights", EmitDefaultValue = false)]
      public virtual DirectoryFlights DirectoryFlights { get; set; }

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