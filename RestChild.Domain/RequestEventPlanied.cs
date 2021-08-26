// File:    RequestEventPlanied.cs
// Purpose: Definition of Class RequestEventPlanied

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Запланированнное событие
   /// </summary>
   [Serializable]
   [DataContract(Name = "requestEventPlanied")]
   public partial class RequestEventPlanied : IEntityBase
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
      /// Код события
      /// </summary>
      [Display(Description = "Код события")]
      [Required(ErrorMessage = "\"Код события\" должно быть заполнено")]
      [DataMember(Name = "eventCode", EmitDefaultValue = false)]
      public virtual Guid EventCode { get; set; }
      
      /// <summary>
      /// Дата события
      /// </summary>
      [Display(Description = "Дата события")]
      [DataMember(Name = "dateEvent", EmitDefaultValue = false)]
      public virtual DateTime? DateEvent { get; set; }
      
      /// <summary>
      /// Произошло
      /// </summary>
      [Display(Description = "Произошло")]
      [Required(ErrorMessage = "\"Произошло\" должно быть заполнено")]
      [DataMember(Name = "processed", EmitDefaultValue = false)]
      public virtual bool Processed { get; set; }
      
      /// <summary>
      /// Дата создания
      /// </summary>
      [Display(Description = "Дата создания")]
      [Required(ErrorMessage = "\"Дата создания\" должно быть заполнено")]
      [DataMember(Name = "dateCreate", EmitDefaultValue = false)]
      public virtual DateTime DateCreate { get; set; }
      
      /// <summary>
      /// Плановая дата
      /// </summary>
      [Display(Description = "Плановая дата")]
      [DataMember(Name = "planDate", EmitDefaultValue = false)]
      public virtual DateTime? PlanDate { get; set; }
      
      
      /// <summary>
      /// Пользователь
      /// </summary>
      [ForeignKey("Account")]
      [DataMember(Name = "accountId")]
      [Display(Description = "Пользователь")]
      public virtual long? AccountId { get; set; }
      
      
      /// <summary>
      /// Пользователь
      /// </summary>
      [Display(Description = "Пользователь")]
      [DataMember(Name = "account", EmitDefaultValue = false)]
      public virtual Account Account { get; set; }

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