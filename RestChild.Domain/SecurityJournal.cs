// File:    SecurityJournal.cs
// Purpose: Definition of Class SecurityJournal

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Журнал программ и процессов
   /// </summary>
   [Serializable]
   [DataContract(Name = "securityJournal")]
   public partial class SecurityJournal : IEntityBase
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
      /// Дата/время
      /// </summary>
      [Display(Description = "Дата/время")]
      [DataMember(Name = "dateEvent", EmitDefaultValue = false)]
      public virtual DateTime? DateEvent { get; set; }
      
      /// <summary>
      /// Событие
      /// </summary>
      [Display(Description = "Событие")]
      [MaxLength(1000, ErrorMessage = "\"Событие\" не может быть больше 1000 символов")]
      [DataMember(Name = "eventName", EmitDefaultValue = false)]
      public virtual string EventName { get; set; }
      
      /// <summary>
      /// Описание
      /// </summary>
      [Display(Description = "Описание")]
      [DataMember(Name = "description", EmitDefaultValue = false)]
      public virtual String Description { get; set; }
      
      /// <summary>
      /// Браузер
      /// </summary>
      [Display(Description = "Браузер")]
      [MaxLength(1000, ErrorMessage = "\"Браузер\" не может быть больше 1000 символов")]
      [DataMember(Name = "brouser", EmitDefaultValue = false)]
      public virtual string Brouser { get; set; }
      
      /// <summary>
      /// Пользователь
      /// </summary>
      [Display(Description = "Пользователь")]
      [MaxLength(1000, ErrorMessage = "\"Пользователь\" не может быть больше 1000 символов")]
      [DataMember(Name = "userName", EmitDefaultValue = false)]
      public virtual string UserName { get; set; }
      
      
      /// <summary>
      /// Тип
      /// </summary>
      [ForeignKey("SecurityJournalType")]
      [DataMember(Name = "securityJournalTypeId")]
      [Display(Description = "Тип")]
      public virtual long? SecurityJournalTypeId { get; set; }
      
      
      /// <summary>
      /// Тип
      /// </summary>
      [Display(Description = "Тип")]
      [DataMember(Name = "securityJournalType", EmitDefaultValue = false)]
      public virtual SecurityJournalType SecurityJournalType { get; set; }

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