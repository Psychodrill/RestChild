// File:    Party.cs
// Purpose: Definition of Class Party

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Отряд
   /// </summary>
   [Serializable]
   [DataContract(Name = "party")]
   public partial class Party : IEntityBase
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
      /// Наименование отряда
      /// </summary>
      [Display(Description = "Наименование отряда")]
      [MaxLength(1000, ErrorMessage = "\"Наименование отряда\" не может быть больше 1000 символов")]
      [Required(ErrorMessage = "\"Наименование отряда\" не может быть пустым")]
      [DataMember(Name = "name", EmitDefaultValue = false)]
      public virtual string Name { get; set; }
      
      /// <summary>
      /// Номер отряда
      /// </summary>
      [Display(Description = "Номер отряда")]
      [DataMember(Name = "partyNumber", EmitDefaultValue = false)]
      public virtual int? PartyNumber { get; set; }
      
      
      /// <summary>
      /// Отряд и блок мест
      /// </summary>
      [InverseProperty("Partys")]
      [DataMember(Name = "tours", EmitDefaultValue = false)]
      public virtual ICollection<Tour> Tours { get; set; }
      
      /// <summary>
      /// Связь с заездами отряда
      /// </summary>
      [ForeignKey("Bouts")]
      [DataMember(Name = "boutsId")]
      [Display(Description = "Связь с заездами отряда")]
      public virtual long? BoutsId { get; set; }
      
      
      /// <summary>
      /// Связь с заездами отряда
      /// </summary>
      [InverseProperty("Partys")]
      [Display(Description = "Связь с заездами отряда")]
      [DataMember(Name = "bouts", EmitDefaultValue = false)]
      public virtual Bout Bouts { get; set; }
      
      /// <summary>
      /// Вожатые отряд
      /// </summary>
      [InverseProperty("Partys")]
      [DataMember(Name = "counselors", EmitDefaultValue = false)]
      public virtual ICollection<Counselors> Counselors { get; set; }
      
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
      /// Отель/Лагерь
      /// </summary>
      [ForeignKey("Hotels")]
      [DataMember(Name = "hotelsId")]
      [Display(Description = "Отель/Лагерь")]
      public virtual long? HotelsId { get; set; }
      
      
      /// <summary>
      /// Отель/Лагерь
      /// </summary>
      [Display(Description = "Отель/Лагерь")]
      [DataMember(Name = "hotels", EmitDefaultValue = false)]
      public virtual Hotels Hotels { get; set; }
      
      /// <summary>
      /// Время отдыха
      /// </summary>
      [ForeignKey("TimeOfRest")]
      [DataMember(Name = "timeOfRestId")]
      [Display(Description = "Время отдыха")]
      public virtual long? TimeOfRestId { get; set; }
      
      
      /// <summary>
      /// Время отдыха
      /// </summary>
      [Display(Description = "Время отдыха")]
      [DataMember(Name = "timeOfRest", EmitDefaultValue = false)]
      public virtual TimeOfRest TimeOfRest { get; set; }
      
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