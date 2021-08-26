// File:    CertificateToApply.cs
// Purpose: Definition of Class CertificateToApply

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Применяемые сертификаты
   /// </summary>
   [Serializable]
   [DataContract(Name = "certificateToApply")]
   public partial class CertificateToApply : IEntityBase
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
      /// Ключ сертификата
      /// </summary>
      [Display(Description = "Ключ сертификата")]
      [MaxLength(1000, ErrorMessage = "\"Ключ сертификата\" не может быть больше 1000 символов")]
      [DataMember(Name = "certificateKey", EmitDefaultValue = false)]
      public virtual string CertificateKey { get; set; }
      
      /// <summary>
      /// Применение по умолчанию
      /// </summary>
      [Display(Description = "Применение по умолчанию")]
      [Required(ErrorMessage = "\"Применение по умолчанию\" должно быть заполнено")]
      [DataMember(Name = "byDefault", EmitDefaultValue = false)]
      public virtual bool ByDefault { get; set; }
      
      /// <summary>
      /// Вид уведомления
      /// </summary>
      [Display(Description = "Вид уведомления")]
      [MaxLength(1000, ErrorMessage = "\"Вид уведомления\" не может быть больше 1000 символов")]
      [DataMember(Name = "notificationType", EmitDefaultValue = false)]
      public virtual string NotificationType { get; set; }
      
      
      /// <summary>
      /// Правила
      /// </summary>
      [InverseProperty("CertificateToApply")]
      [DataMember(Name = "accounts", EmitDefaultValue = false)]
      public virtual ICollection<CertificateToApplyAccount> Accounts { get; set; }
      
      /// <summary>
      /// Пользователь который будет применяться в уведомлениях
      /// </summary>
      [ForeignKey("Account")]
      [DataMember(Name = "accountId")]
      [Display(Description = "Пользователь который будет применяться в уведомлениях")]
      public virtual long? AccountId { get; set; }
      
      
      /// <summary>
      /// Пользователь который будет применяться в уведомлениях
      /// </summary>
      [Display(Description = "Пользователь который будет применяться в уведомлениях")]
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