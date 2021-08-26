// File:    DirectoryFlights.cs
// Purpose: Definition of Class DirectoryFlights

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Рейс
   /// </summary>
   [Serializable]
   [DataContract(Name = "directoryFlights")]
   public partial class DirectoryFlights : IEntityBase
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
      /// Код рейса
      /// </summary>
      [Display(Description = "Код рейса")]
      [MaxLength(1000, ErrorMessage = "\"Код рейса\" не может быть больше 1000 символов")]
      [DataMember(Name = "code", EmitDefaultValue = false)]
      public virtual string Code { get; set; }
      
      /// <summary>
      /// Номер рейса
      /// </summary>
      [Display(Description = "Номер рейса")]
      [MaxLength(1000, ErrorMessage = "\"Номер рейса\" не может быть больше 1000 символов")]
      [Required(ErrorMessage = "\"Номер рейса\" не может быть пустым")]
      [DataMember(Name = "filightNumber", EmitDefaultValue = false)]
      public virtual string FilightNumber { get; set; }
      
      /// <summary>
      /// Время вылета
      /// </summary>
      [Display(Description = "Время вылета")]
      [DataMember(Name = "timeOfDeparture", EmitDefaultValue = false)]
      public virtual DateTime? TimeOfDeparture { get; set; }
      
      /// <summary>
      /// Время прилета
      /// </summary>
      [Display(Description = "Время прилета")]
      [DataMember(Name = "timeOfArrival", EmitDefaultValue = false)]
      public virtual DateTime? TimeOfArrival { get; set; }
      
      /// <summary>
      /// Код места отбытия
      /// </summary>
      [Display(Description = "Код места отбытия")]
      [MaxLength(1000, ErrorMessage = "\"Код места отбытия\" не может быть больше 1000 символов")]
      [DataMember(Name = "codeDeparture", EmitDefaultValue = false)]
      public virtual string CodeDeparture { get; set; }
      
      /// <summary>
      /// Код места прибытия
      /// </summary>
      [Display(Description = "Код места прибытия")]
      [MaxLength(1000, ErrorMessage = "\"Код места прибытия\" не может быть больше 1000 символов")]
      [DataMember(Name = "codeArrival", EmitDefaultValue = false)]
      public virtual string CodeArrival { get; set; }
      
      
      /// <summary>
      /// Рейс
      /// </summary>
      [InverseProperty("DirectoryFlights")]
      [DataMember(Name = "linkToPeoples", EmitDefaultValue = false)]
      public virtual ICollection<LinkToPeople> LinkToPeoples { get; set; }
      
      /// <summary>
      /// Рейс
      /// </summary>
      [InverseProperty("DirectoryFlights")]
      [DataMember(Name = "tickets", EmitDefaultValue = false)]
      public virtual ICollection<Ticket> Tickets { get; set; }
      
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
      [InverseProperty("DirectoryFlights")]
      [Display(Description = "Договор")]
      [DataMember(Name = "contract", EmitDefaultValue = false)]
      public virtual Contract Contract { get; set; }
      
      /// <summary>
      /// Вид транспорта
      /// </summary>
      [ForeignKey("TypeOfTransport")]
      [DataMember(Name = "typeOfTransportId")]
      [Display(Description = "Вид транспорта")]
      public virtual long? TypeOfTransportId { get; set; }
      
      
      /// <summary>
      /// Вид транспорта
      /// </summary>
      [Display(Description = "Вид транспорта")]
      [DataMember(Name = "typeOfTransport", EmitDefaultValue = false)]
      public virtual TypeOfTransport TypeOfTransport { get; set; }
      
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
      /// вылет
      /// </summary>
      [ForeignKey("Departure")]
      [DataMember(Name = "departureId")]
      [Display(Description = "вылет")]
      public virtual long? DepartureId { get; set; }
      
      
      /// <summary>
      /// вылет
      /// </summary>
      [Display(Description = "вылет")]
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