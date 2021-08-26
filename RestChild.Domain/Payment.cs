// File:    Payment.cs
// Purpose: Definition of Class Payment

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Платеж
   /// </summary>
   [Serializable]
   [DataContract(Name = "payment")]
   public partial class Payment : IEntityBase
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
      /// Дата платежа
      /// </summary>
      [Display(Description = "Дата платежа")]
      [DataMember(Name = "paymentDate", EmitDefaultValue = false)]
      public virtual DateTime? PaymentDate { get; set; }
      
      /// <summary>
      /// Номер платежа
      /// </summary>
      [Display(Description = "Номер платежа")]
      [MaxLength(1000, ErrorMessage = "\"Номер платежа\" не может быть больше 1000 символов")]
      [DataMember(Name = "paymentNumber", EmitDefaultValue = false)]
      public virtual string PaymentNumber { get; set; }
      
      /// <summary>
      /// Сумма платежа
      /// </summary>
      [Display(Description = "Сумма платежа")]
      [DataMember(Name = "paymentSumm", EmitDefaultValue = false)]
      public virtual decimal? PaymentSumm { get; set; }
      
      /// <summary>
      /// Назначение платежа
      /// </summary>
      [Display(Description = "Назначение платежа")]
      [DataMember(Name = "purpose", EmitDefaultValue = false)]
      public virtual String Purpose { get; set; }
      
      /// <summary>
      /// Описание
      /// </summary>
      [Display(Description = "Описание")]
      [DataMember(Name = "description", EmitDefaultValue = false)]
      public virtual String Description { get; set; }
      
      /// <summary>
      /// Дата создания
      /// </summary>
      [Display(Description = "Дата создания")]
      [Required(ErrorMessage = "\"Дата создания\" должно быть заполнено")]
      [DataMember(Name = "dateCreate", EmitDefaultValue = false)]
      public virtual DateTime DateCreate { get; set; }
      
      /// <summary>
      /// Квитанция об оплате Url
      /// </summary>
      [Display(Description = "Квитанция об оплате Url")]
      [MaxLength(1000, ErrorMessage = "\"Квитанция об оплате Url\" не может быть больше 1000 символов")]
      [DataMember(Name = "paymentFileUrl", EmitDefaultValue = false)]
      public virtual string PaymentFileUrl { get; set; }
      
      /// <summary>
      /// Квитанция об оплате имя
      /// </summary>
      [Display(Description = "Квитанция об оплате имя")]
      [MaxLength(1000, ErrorMessage = "\"Квитанция об оплате имя\" не может быть больше 1000 символов")]
      [DataMember(Name = "paymentFileTitle", EmitDefaultValue = false)]
      public virtual string PaymentFileTitle { get; set; }
      
      /// <summary>
      /// Плательщик
      /// </summary>
      [Display(Description = "Плательщик")]
      [MaxLength(1000, ErrorMessage = "\"Плательщик\" не может быть больше 1000 символов")]
      [DataMember(Name = "payer", EmitDefaultValue = false)]
      public virtual string Payer { get; set; }
      
      /// <summary>
      /// Источник
      /// </summary>
      [Display(Description = "Источник")]
      [MaxLength(1000, ErrorMessage = "\"Источник\" не может быть больше 1000 символов")]
      [DataMember(Name = "source", EmitDefaultValue = false)]
      public virtual string Source { get; set; }
      
      
      /// <summary>
      /// Платеж - начисление
      /// </summary>
      [InverseProperty("Payments")]
      [DataMember(Name = "calculations", EmitDefaultValue = false)]
      public virtual ICollection<Calculation> Calculations { get; set; }
      
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
      /// История
      /// </summary>
      [ForeignKey("History")]
      [DataMember(Name = "historyId")]
      [Display(Description = "История")]
      public virtual long? HistoryId { get; set; }
      
      
      /// <summary>
      /// История
      /// </summary>
      [Display(Description = "История")]
      [DataMember(Name = "history", EmitDefaultValue = false)]
      public virtual HistoryLink History { get; set; }
      
      /// <summary>
      /// Ползователь
      /// </summary>
      [ForeignKey("Account")]
      [DataMember(Name = "accountId")]
      [Display(Description = "Ползователь")]
      public virtual long? AccountId { get; set; }
      
      
      /// <summary>
      /// Ползователь
      /// </summary>
      [Display(Description = "Ползователь")]
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