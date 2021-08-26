// File:    TransportInfo.cs
// Purpose: Definition of Class TransportInfo

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Транспорт
   /// </summary>
   [Serializable]
   [DataContract(Name = "transportInfo")]
   public partial class TransportInfo : IEntityBase
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
      /// Дата вылета
      /// </summary>
      [Display(Description = "Дата вылета")]
      [Required(ErrorMessage = "\"Дата вылета\" должно быть заполнено")]
      [DataMember(Name = "dateOfDeparture", EmitDefaultValue = false)]
      public virtual DateTime DateOfDeparture { get; set; }
      
      /// <summary>
      /// Место сбора
      /// </summary>
      [Display(Description = "Место сбора")]
      [DataMember(Name = "venue", EmitDefaultValue = false)]
      public virtual String Venue { get; set; }
      
      /// <summary>
      /// Дата сбора
      /// </summary>
      [Display(Description = "Дата сбора")]
      [DataMember(Name = "dateCollection", EmitDefaultValue = false)]
      public virtual DateTime? DateCollection { get; set; }
      
      /// <summary>
      /// Дата прибытия
      /// </summary>
      [Display(Description = "Дата прибытия")]
      [DataMember(Name = "dateArrival", EmitDefaultValue = false)]
      public virtual DateTime? DateArrival { get; set; }
      
      /// <summary>
      /// Памятка
      /// </summary>
      [Display(Description = "Памятка")]
      [DataMember(Name = "memo", EmitDefaultValue = false)]
      public virtual String Memo { get; set; }
      
      /// <summary>
      /// Файл памятки
      /// </summary>
      [Display(Description = "Файл памятки")]
      [MaxLength(1000, ErrorMessage = "\"Файл памятки\" не может быть больше 1000 символов")]
      [DataMember(Name = "memoFile", EmitDefaultValue = false)]
      public virtual string MemoFile { get; set; }
      
      /// <summary>
      /// Ссылка на файл памятки
      /// </summary>
      [Display(Description = "Ссылка на файл памятки")]
      [MaxLength(1000, ErrorMessage = "\"Ссылка на файл памятки\" не может быть больше 1000 символов")]
      [DataMember(Name = "memoLink", EmitDefaultValue = false)]
      public virtual string MemoLink { get; set; }
      
      
      /// <summary>
      /// Траснпорт
      /// </summary>
      [InverseProperty("Transport")]
      [DataMember(Name = "people", EmitDefaultValue = false)]
      public virtual ICollection<LinkToPeople> People { get; set; }
      
      /// <summary>
      /// Место прибытия
      /// </summary>
      [ForeignKey("Arrival")]
      [DataMember(Name = "arrivalId")]
      [Display(Description = "Место прибытия")]
      public virtual long? ArrivalId { get; set; }
      
      
      /// <summary>
      /// Место прибытия
      /// </summary>
      [Display(Description = "Место прибытия")]
      [DataMember(Name = "arrival", EmitDefaultValue = false)]
      public virtual City Arrival { get; set; }
      
      /// <summary>
      /// Вылет
      /// </summary>
      [ForeignKey("Departure")]
      [DataMember(Name = "departureId")]
      [Display(Description = "Вылет")]
      public virtual long? DepartureId { get; set; }
      
      
      /// <summary>
      /// Вылет
      /// </summary>
      [Display(Description = "Вылет")]
      [DataMember(Name = "departure", EmitDefaultValue = false)]
      public virtual City Departure { get; set; }
      
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
      /// Год кампании
      /// </summary>
      [ForeignKey("YearOfRest")]
      [DataMember(Name = "yearOfRestId")]
      [Display(Description = "Год кампании")]
      public virtual long? YearOfRestId { get; set; }
      
      
      /// <summary>
      /// Год кампании
      /// </summary>
      [Display(Description = "Год кампании")]
      [DataMember(Name = "yearOfRest", EmitDefaultValue = false)]
      public virtual YearOfRest YearOfRest { get; set; }
      
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
      [Display(Description = "Заезд")]
      [DataMember(Name = "bout", EmitDefaultValue = false)]
      public virtual Bout Bout { get; set; }

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