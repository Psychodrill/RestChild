// File:    Bout.cs
// Purpose: Definition of Class Bout

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Заезд
   /// </summary>
   [Serializable]
   [DataContract(Name = "bout")]
   public partial class Bout : IEntityBase
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
      /// Дата заезда
      /// </summary>
      [Display(Description = "Дата заезда")]
      [DataMember(Name = "dateIncome", EmitDefaultValue = false)]
      public virtual DateTime? DateIncome { get; set; }
      
      /// <summary>
      /// Дата выезда
      /// </summary>
      [Display(Description = "Дата выезда")]
      [DataMember(Name = "dateOutcome", EmitDefaultValue = false)]
      public virtual DateTime? DateOutcome { get; set; }
      
      /// <summary>
      /// Включен в транспорт
      /// </summary>
      [Display(Description = "Включен в транспорт")]
      [Required(ErrorMessage = "\"Включен в транспорт\" должно быть заполнено")]
      [DataMember(Name = "includedInTransport", EmitDefaultValue = false)]
      public virtual bool IncludedInTransport { get; set; }
      
      /// <summary>
      /// Результат заезда(комментарий)
      /// </summary>
      [Display(Description = "Результат заезда(комментарий)")]
      [DataMember(Name = "comment", EmitDefaultValue = false)]
      public virtual String Comment { get; set; }
      
      /// <summary>
      /// Опубликовано на сайте
      /// </summary>
      [Display(Description = "Опубликовано на сайте")]
      [Required(ErrorMessage = "\"Опубликовано на сайте\" должно быть заполнено")]
      [DataMember(Name = "isPublishOnSite", EmitDefaultValue = false)]
      public virtual bool IsPublishOnSite { get; set; }
      
      
      /// <summary>
      /// Связь с заездами отряда
      /// </summary>
      [InverseProperty("Bouts")]
      [DataMember(Name = "partys", EmitDefaultValue = false)]
      public virtual ICollection<Party> Partys { get; set; }
      
      /// <summary>
      /// Связь с блоком мест
      /// </summary>
      [InverseProperty("Bout")]
      [DataMember(Name = "tours", EmitDefaultValue = false)]
      public virtual ICollection<Tour> Tours { get; set; }
      
      /// <summary>
      /// Заезд
      /// </summary>
      [InverseProperty("Bout")]
      [DataMember(Name = "applicants", EmitDefaultValue = false)]
      public virtual ICollection<Applicant> Applicants { get; set; }
      
      /// <summary>
      /// Дети
      /// </summary>
      [InverseProperty("Bout")]
      [DataMember(Name = "chidren", EmitDefaultValue = false)]
      public virtual ICollection<Child> Chidren { get; set; }
      
      /// <summary>
      /// Дневник заезда
      /// </summary>
      [InverseProperty("Bout")]
      [DataMember(Name = "boutJournal", EmitDefaultValue = false)]
      public virtual ICollection<BoutJournal> BoutJournal { get; set; }
      
      /// <summary>
      /// Сопровождающие
      /// </summary>
      [InverseProperty("Bout")]
      [DataMember(Name = "attendants", EmitDefaultValue = false)]
      public virtual ICollection<BoutAttendant> Attendants { get; set; }
      
      /// <summary>
      /// Старший вожатый
      /// </summary>
      [InverseProperty("Bouts")]
      [DataMember(Name = "seniorCounselors", EmitDefaultValue = false)]
      public virtual ICollection<Counselors> SeniorCounselors { get; set; }
      
      /// <summary>
      /// Отель
      /// </summary>
      [ForeignKey("Hotels")]
      [DataMember(Name = "hotelsId")]
      [Display(Description = "Отель")]
      public virtual long? HotelsId { get; set; }
      
      
      /// <summary>
      /// Отель
      /// </summary>
      [Display(Description = "Отель")]
      [DataMember(Name = "hotels", EmitDefaultValue = false)]
      public virtual Hotels Hotels { get; set; }
      
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
      /// Смена в заезде
      /// </summary>
      [ForeignKey("GroupedTimeOfRest")]
      [DataMember(Name = "groupedTimeOfRestId")]
      [Display(Description = "Смена в заезде")]
      public virtual long? GroupedTimeOfRestId { get; set; }
      
      
      /// <summary>
      /// Смена в заезде
      /// </summary>
      [Display(Description = "Смена в заезде")]
      [DataMember(Name = "groupedTimeOfRest", EmitDefaultValue = false)]
      public virtual GroupedTimeOfRest GroupedTimeOfRest { get; set; }
      
      /// <summary>
      /// Администраторы смены
      /// </summary>
      [InverseProperty("Bouts")]
      [DataMember(Name = "administratorTours", EmitDefaultValue = false)]
      public virtual ICollection<AdministratorTour> AdministratorTours { get; set; }
      
      /// <summary>
      /// Подменные вожатые
      /// </summary>
      [InverseProperty("SwingBoats")]
      [DataMember(Name = "swingCounselors", EmitDefaultValue = false)]
      public virtual ICollection<Counselors> SwingCounselors { get; set; }
      
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
      /// Год компании
      /// </summary>
      [ForeignKey("YearOfRest")]
      [DataMember(Name = "yearOfRestId")]
      [Display(Description = "Год компании")]
      public virtual long? YearOfRestId { get; set; }
      
      
      /// <summary>
      /// Год компании
      /// </summary>
      [Display(Description = "Год компании")]
      [DataMember(Name = "yearOfRest", EmitDefaultValue = false)]
      public virtual YearOfRest YearOfRest { get; set; }
      
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
      /// Транспорт из места отдыха
      /// </summary>
      [ForeignKey("TransportInfoFrom")]
      [DataMember(Name = "transportInfoFromId")]
      [Display(Description = "Транспорт из места отдыха")]
      public virtual long? TransportInfoFromId { get; set; }
      
      
      /// <summary>
      /// Транспорт из места отдыха
      /// </summary>
      [Display(Description = "Транспорт из места отдыха")]
      [DataMember(Name = "transportInfoFrom", EmitDefaultValue = false)]
      public virtual TransportInfo TransportInfoFrom { get; set; }
      
      /// <summary>
      /// Траснпорт в место отдыха
      /// </summary>
      [ForeignKey("TransportInfoTo")]
      [DataMember(Name = "transportInfoToId")]
      [Display(Description = "Траснпорт в место отдыха")]
      public virtual long? TransportInfoToId { get; set; }
      
      
      /// <summary>
      /// Траснпорт в место отдыха
      /// </summary>
      [Display(Description = "Траснпорт в место отдыха")]
      [DataMember(Name = "transportInfoTo", EmitDefaultValue = false)]
      public virtual TransportInfo TransportInfoTo { get; set; }

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