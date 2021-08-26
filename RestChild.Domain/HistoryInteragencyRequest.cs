// File:    HistoryInteragencyRequest.cs
// Purpose: Definition of Class HistoryInteragencyRequest

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// История работы с межведомственными запросами
   /// </summary>
   [Serializable]
   [DataContract(Name = "historyInteragencyRequest")]
   public partial class HistoryInteragencyRequest : IEntityBase
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
      /// Операция
      /// </summary>
      [Display(Description = "Операция")]
      [DataMember(Name = "operation", EmitDefaultValue = false)]
      public virtual String Operation { get; set; }
      
      /// <summary>
      /// Дата операции
      /// </summary>
      [Display(Description = "Дата операции")]
      [Required(ErrorMessage = "\"Дата операции\" должно быть заполнено")]
      [DataMember(Name = "operationDate", EmitDefaultValue = false)]
      public virtual DateTime OperationDate { get; set; }
      
      /// <summary>
      /// Код операции
      /// </summary>
      [Display(Description = "Код операции")]
      [MaxLength(1000, ErrorMessage = "\"Код операции\" не может быть больше 1000 символов")]
      [DataMember(Name = "code", EmitDefaultValue = false)]
      public virtual string Code { get; set; }
      
      
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
      /// Межведомственный запрос
      /// </summary>
      [ForeignKey("InteragencyRequest")]
      [DataMember(Name = "interagencyRequestId")]
      [Display(Description = "Межведомственный запрос")]
      public virtual long? InteragencyRequestId { get; set; }
      
      
      /// <summary>
      /// Межведомственный запрос
      /// </summary>
      [Display(Description = "Межведомственный запрос")]
      [DataMember(Name = "interagencyRequest", EmitDefaultValue = false)]
      public virtual InteragencyRequest InteragencyRequest { get; set; }

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