// File:    StateMachineFromStatus.cs
// Purpose: Definition of Class StateMachineFromStatus

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Источник перехода
   /// </summary>
   [Serializable]
   [DataContract(Name = "stateMachineFromStatus")]
   public partial class StateMachineFromStatus : IEntityBase
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
      /// Код службы
      /// </summary>
      [Display(Description = "Код службы")]
      [MaxLength(1000, ErrorMessage = "\"Код службы\" не может быть больше 1000 символов")]
      [DataMember(Name = "serviceCode", EmitDefaultValue = false)]
      public virtual string ServiceCode { get; set; }
      
      /// <summary>
      /// Код права
      /// </summary>
      [Display(Description = "Код права")]
      [MaxLength(1000, ErrorMessage = "\"Код права\" не может быть больше 1000 символов")]
      [DataMember(Name = "rightCode", EmitDefaultValue = false)]
      public virtual string RightCode { get; set; }
      
      
      /// <summary>
      /// </summary>
      [ForeignKey("StateMachineAction")]
      [DataMember(Name = "stateMachineActionId")][Display(Description = "")]
      public virtual long? StateMachineActionId { get; set; }
      /// <summary>
      /// </summary>
      [InverseProperty("FromStates")]
      [Display(Description = "")]
      [DataMember(Name = "stateMachineAction", EmitDefaultValue = false)]
      public virtual StateMachineAction StateMachineAction { get; set; }
      
      /// <summary>
      /// </summary>
      [ForeignKey("FromState")]
      [DataMember(Name = "fromStateId")]
      [Display(Description = "")]
      public virtual long? FromStateId { get; set; }
      
      
      /// <summary>
      /// </summary>
      [Display(Description = "")]
      [DataMember(Name = "fromState", EmitDefaultValue = false)]
      public virtual StateMachineState FromState { get; set; }
      
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