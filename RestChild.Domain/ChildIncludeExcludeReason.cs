// File:    ChildIncludeExcludeReason.cs
// Purpose: Definition of Class ChildIncludeExcludeReason

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Причина добавления исключения
   /// </summary>
   [Serializable]
   [DataContract(Name = "childIncludeExcludeReason")]
   public partial class ChildIncludeExcludeReason : IEntityBase
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
      /// Причина исключения включеия
      /// </summary>
      [Display(Description = "Причина исключения включеия")]
      [DataMember(Name = "reason", EmitDefaultValue = false)]
      public virtual String Reason { get; set; }
      
      /// <summary>
      /// Дата исключения включения
      /// </summary>
      [Display(Description = "Дата исключения включения")]
      [Required(ErrorMessage = "\"Дата исключения включения\" должно быть заполнено")]
      [DataMember(Name = "operartionDate", EmitDefaultValue = false)]
      public virtual DateTime OperartionDate { get; set; }
      
      
      /// <summary>
      /// Ссылка на ЭП
      /// </summary>
      [ForeignKey("SignInfo")]
      [DataMember(Name = "signInfoId")]
      [Display(Description = "Ссылка на ЭП")]
      public virtual long? SignInfoId { get; set; }
      
      
      /// <summary>
      /// Ссылка на ЭП
      /// </summary>
      [Display(Description = "Ссылка на ЭП")]
      [DataMember(Name = "signInfo", EmitDefaultValue = false)]
      public virtual SignInfo SignInfo { get; set; }
      
      /// <summary>
      /// Связь с пользователем
      /// </summary>
      [ForeignKey("Account")]
      [DataMember(Name = "accountId")]
      [Display(Description = "Связь с пользователем")]
      public virtual long? AccountId { get; set; }
      
      
      /// <summary>
      /// Связь с пользователем
      /// </summary>
      [Display(Description = "Связь с пользователем")]
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