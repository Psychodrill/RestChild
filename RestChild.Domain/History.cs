// File:    History.cs
// Purpose: Definition of Class History

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// История
   /// </summary>
   [Serializable]
   [DataContract(Name = "history")]
   public partial class History : IEntityBase
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
      /// Дата изменения
      /// </summary>
      [Display(Description = "Дата изменения")]
      [Required(ErrorMessage = "\"Дата изменения\" должно быть заполнено")]
      [DataMember(Name = "dateChange", EmitDefaultValue = false)]
      public virtual DateTime DateChange { get; set; }
      
      /// <summary>
      /// Событие
      /// </summary>
      [Display(Description = "Событие")]
      [MaxLength(1000, ErrorMessage = "\"Событие\" не может быть больше 1000 символов")]
      [Required(ErrorMessage = "\"Событие\" не может быть пустым")]
      [DataMember(Name = "eventCode", EmitDefaultValue = false)]
      public virtual string EventCode { get; set; }
      
      /// <summary>
      /// Комментарий
      /// </summary>
      [Display(Description = "Комментарий")]
      [DataMember(Name = "commentary", EmitDefaultValue = false)]
      public virtual String Commentary { get; set; }
      
      /// <summary>
      /// Строка с автором изменений
      /// </summary>
      [Display(Description = "Строка с автором изменений")]
      [MaxLength(1000, ErrorMessage = "\"Строка с автором изменений\" не может быть больше 1000 символов")]
      [DataMember(Name = "authorString", EmitDefaultValue = false)]
      public virtual string AuthorString { get; set; }
      
      
      /// <summary>
      /// Ссылка на оператора
      /// </summary>
      [ForeignKey("Account")]
      [DataMember(Name = "accountId")]
      [Display(Description = "Ссылка на оператора")]
      public virtual long? AccountId { get; set; }
      
      
      /// <summary>
      /// Ссылка на оператора
      /// </summary>
      [Display(Description = "Ссылка на оператора")]
      [DataMember(Name = "account", EmitDefaultValue = false)]
      public virtual Account Account { get; set; }
      
      /// <summary>
      /// Ссылка на историю
      /// </summary>
      [ForeignKey("Link")]
      [DataMember(Name = "linkId")]
      [Display(Description = "Ссылка на историю")]
      public virtual long? LinkId { get; set; }
      
      
      /// <summary>
      /// Ссылка на историю
      /// </summary>
      [InverseProperty("Historys")]
      [Display(Description = "Ссылка на историю")]
      [DataMember(Name = "link", EmitDefaultValue = false)]
      public virtual HistoryLink Link { get; set; }
      
      /// <summary>
      /// Ссылка на подпись
      /// </summary>
      [ForeignKey("SignInfo")]
      [DataMember(Name = "signInfoId")]
      [Display(Description = "Ссылка на подпись")]
      public virtual long? SignInfoId { get; set; }
      
      
      /// <summary>
      /// Ссылка на подпись
      /// </summary>
      [Display(Description = "Ссылка на подпись")]
      [DataMember(Name = "signInfo", EmitDefaultValue = false)]
      public virtual SignInfo SignInfo { get; set; }
      
      /// <summary>
      /// Из статуса
      /// </summary>
      [ForeignKey("FromState")]
      [DataMember(Name = "fromStateId")]
      [Display(Description = "Из статуса")]
      public virtual long? FromStateId { get; set; }
      
      
      /// <summary>
      /// Из статуса
      /// </summary>
      [Display(Description = "Из статуса")]
      [DataMember(Name = "fromState", EmitDefaultValue = false)]
      public virtual StateMachineState FromState { get; set; }
      
      /// <summary>
      /// В статус
      /// </summary>
      [ForeignKey("ToState")]
      [DataMember(Name = "toStateId")]
      [Display(Description = "В статус")]
      public virtual long? ToStateId { get; set; }
      
      
      /// <summary>
      /// В статус
      /// </summary>
      [Display(Description = "В статус")]
      [DataMember(Name = "toState", EmitDefaultValue = false)]
      public virtual StateMachineState ToState { get; set; }

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