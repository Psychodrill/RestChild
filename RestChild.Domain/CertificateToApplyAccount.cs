// File:    CertificateToApplyAccount.cs
// Purpose: Definition of Class CertificateToApplyAccount

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Применяемые сертификаты. Пользователи
   /// </summary>
   [Serializable]
   [DataContract(Name = "certificateToApplyAccount")]
   public partial class CertificateToApplyAccount : IEntityBase
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
      /// За исключением
      /// </summary>
      [Display(Description = "За исключением")]
      [Required(ErrorMessage = "\"За исключением\" должно быть заполнено")]
      [DataMember(Name = "forExcept", EmitDefaultValue = false)]
      public virtual bool ForExcept { get; set; }
      
      /// <summary>
      /// Системный пользователь
      /// </summary>
      [Display(Description = "Системный пользователь")]
      [Required(ErrorMessage = "\"Системный пользователь\" должно быть заполнено")]
      [DataMember(Name = "forSystemAccount", EmitDefaultValue = false)]
      public virtual bool ForSystemAccount { get; set; }
      
      
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
      /// Правила
      /// </summary>
      [ForeignKey("CertificateToApply")]
      [DataMember(Name = "certificateToApplyId")]
      [Display(Description = "Правила")]
      public virtual long? CertificateToApplyId { get; set; }
      
      
      /// <summary>
      /// Правила
      /// </summary>
      [InverseProperty("Accounts")]
      [Display(Description = "Правила")]
      [DataMember(Name = "certificateToApply", EmitDefaultValue = false)]
      public virtual CertificateToApply CertificateToApply { get; set; }

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