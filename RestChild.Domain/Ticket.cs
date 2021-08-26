// File:    Ticket.cs
// Purpose: Definition of Class Ticket

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Билет
   /// </summary>
   [Serializable]
   [DataContract(Name = "ticket")]
   public partial class Ticket : IEntityBase
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
      /// Рейс
      /// </summary>
      [Display(Description = "Рейс")]
      [MaxLength(1000, ErrorMessage = "\"Рейс\" не может быть больше 1000 символов")]
      [DataMember(Name = "flightNumber", EmitDefaultValue = false)]
      public virtual string FlightNumber { get; set; }
      
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
      /// Билет
      /// </summary>
      [InverseProperty("Tickets")]
      [DataMember(Name = "calculations", EmitDefaultValue = false)]
      public virtual ICollection<Calculation> Calculations { get; set; }
      
      /// <summary>
      /// Отдыхающие
      /// </summary>
      [InverseProperty("Ticket")]
      [DataMember(Name = "persons", EmitDefaultValue = false)]
      public virtual ICollection<TicketLink> Persons { get; set; }
      
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
      [InverseProperty("Tickets")]
      [Display(Description = "Рейс")]
      [DataMember(Name = "directoryFlights", EmitDefaultValue = false)]
      public virtual DirectoryFlights DirectoryFlights { get; set; }
      
      /// <summary>
      /// Услуга
      /// </summary>
      [ForeignKey("AddonServices")]
      [DataMember(Name = "addonServicesId")]
      [Display(Description = "Услуга")]
      public virtual long? AddonServicesId { get; set; }
      
      
      /// <summary>
      /// Услуга
      /// </summary>
      [Display(Description = "Услуга")]
      [DataMember(Name = "addonServices", EmitDefaultValue = false)]
      public virtual AddonServices AddonServices { get; set; }
      
      /// <summary>
      /// Транспортная организация
      /// </summary>
      [ForeignKey("Organization")]
      [DataMember(Name = "organizationId")]
      [Display(Description = "Транспортная организация")]
      public virtual long? OrganizationId { get; set; }
      
      
      /// <summary>
      /// Транспортная организация
      /// </summary>
      [Display(Description = "Транспортная организация")]
      [DataMember(Name = "organization", EmitDefaultValue = false)]
      public virtual Organization Organization { get; set; }
      
      /// <summary>
      /// Билет
      /// </summary>
      [ForeignKey("Request")]
      [DataMember(Name = "requestId")]
      [Display(Description = "Билет")]
      public virtual long? RequestId { get; set; }
      
      
      /// <summary>
      /// Билет
      /// </summary>
      [InverseProperty("Tickets")]
      [Display(Description = "Билет")]
      [DataMember(Name = "request", EmitDefaultValue = false)]
      public virtual Request Request { get; set; }
      
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