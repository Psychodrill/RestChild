// File:    StateMachineAction.cs
// Purpose: Definition of Class StateMachineAction

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Переходы по статусам
   /// </summary>
   [Serializable]
   [DataContract(Name = "stateMachineAction")]
   public partial class StateMachineAction : IEntityBase
   {
      
      /// <summary>
      /// Уникальный идентификатор
      /// </summary>
      [Display(Description = "Уникальный идентификатор")]
      [Required(ErrorMessage = "\"Уникальный идентификатор\" должно быть заполнено")]
      [Key]
      [DatabaseGenerated(DatabaseGeneratedOption.None)]
      [DataMember(Name = "id", EmitDefaultValue = false)]
      public virtual long Id { get; set; }
      
      /// <summary>
      /// Действие
      /// </summary>
      [Display(Description = "Действие")]
      [MaxLength(1000, ErrorMessage = "\"Действие\" не может быть больше 1000 символов")]
      [Required(ErrorMessage = "\"Действие\" не может быть пустым")]
      [DataMember(Name = "actionName", EmitDefaultValue = false)]
      public virtual string ActionName { get; set; }
      
      /// <summary>
      /// Текст
      /// </summary>
      [Display(Description = "Текст")]
      [MaxLength(1000, ErrorMessage = "\"Текст\" не может быть больше 1000 символов")]
      [DataMember(Name = "description", EmitDefaultValue = false)]
      public virtual string Description { get; set; }
      
      /// <summary>
      /// Код действия
      /// </summary>
      [Display(Description = "Код действия")]
      [MaxLength(1000, ErrorMessage = "\"Код действия\" не может быть больше 1000 символов")]
      [Required(ErrorMessage = "\"Код действия\" не может быть пустым")]
      [DataMember(Name = "actionCode", EmitDefaultValue = false)]
      public virtual string ActionCode { get; set; }
      
      /// <summary>
      /// Системное действие
      /// </summary>
      [Display(Description = "Системное действие")]
      [Required(ErrorMessage = "\"Системное действие\" должно быть заполнено")]
      [DataMember(Name = "isSystemAction", EmitDefaultValue = false)]
      public virtual bool IsSystemAction { get; set; }
      
      /// <summary>
      /// Наложение ЭП
      /// </summary>
      [Display(Description = "Наложение ЭП")]
      [Required(ErrorMessage = "\"Наложение ЭП\" должно быть заполнено")]
      [DataMember(Name = "needSign", EmitDefaultValue = false)]
      public virtual bool NeedSign { get; set; }
      
      
      /// <summary>
      /// </summary>
      [ForeignKey("StateMachine")]
      [DataMember(Name = "stateMachineId")]
      [Display(Description = "")]
      public virtual long? StateMachineId { get; set; }
      
      
      /// <summary>
      /// </summary>
      [Display(Description = "")]
      [DataMember(Name = "stateMachine", EmitDefaultValue = false)]
      public virtual StateMachine StateMachine { get; set; }
      
      /// <summary>
      /// </summary>
      [ForeignKey("ToState")]
      [DataMember(Name = "toStateId")]
      [Display(Description = "")]
      public virtual long? ToStateId { get; set; }
      
      
      /// <summary>
      /// </summary>
      [InverseProperty("Actions")]
      [Display(Description = "")]
      [DataMember(Name = "toState", EmitDefaultValue = false)]
      public virtual StateMachineState ToState { get; set; }
      
      /// <summary>
      /// </summary>
      [InverseProperty("StateMachineAction")]
      [DataMember(Name = "fromStates", EmitDefaultValue = false)]
      public virtual ICollection<StateMachineFromStatus> FromStates { get; set; }

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