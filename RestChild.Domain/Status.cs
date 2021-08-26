// File:    Status.cs
// Purpose: Definition of Class Status

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Статус заявления
   /// </summary>
   [Serializable]
   [DataContract(Name = "status")]
   public partial class Status : IEntityBase
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
      /// Наименование
      /// </summary>
      [Display(Description = "Наименование")]
      [MaxLength(1000, ErrorMessage = "\"Наименование\" не может быть больше 1000 символов")]
      [Required(ErrorMessage = "\"Наименование\" не может быть пустым")]
      [DataMember(Name = "name", EmitDefaultValue = false)]
      public virtual string Name { get; set; }
      
      /// <summary>
      /// Внешний идентификатор
      /// </summary>
      [Display(Description = "Внешний идентификатор")]
      [MaxLength(1000, ErrorMessage = "\"Внешний идентификатор\" не может быть больше 1000 символов")]
      [DataMember(Name = "externalUid", EmitDefaultValue = false)]
      public virtual string ExternalUid { get; set; }
      
      /// <summary>
      /// Наименование статуса на МПГУ
      /// </summary>
      [Display(Description = "Наименование статуса на МПГУ")]
      [MaxLength(1000, ErrorMessage = "\"Наименование статуса на МПГУ\" не может быть больше 1000 символов")]
      [Required(ErrorMessage = "\"Наименование статуса на МПГУ\" не может быть пустым")]
      [DataMember(Name = "mpguName", EmitDefaultValue = false)]
      public virtual string MpguName { get; set; }
      
      /// <summary>
      /// Описание статуса
      /// </summary>
      [Display(Description = "Описание статуса")]
      [DataMember(Name = "mpguDescription", EmitDefaultValue = false)]
      public virtual String MpguDescription { get; set; }
      
      /// <summary>
      /// Комментарий для МПГУ
      /// </summary>
      [Display(Description = "Комментарий для МПГУ")]
      [DataMember(Name = "mpguComment", EmitDefaultValue = false)]
      public virtual String MpguComment { get; set; }
      
      /// <summary>
      /// Финальный статус
      /// </summary>
      [Display(Description = "Финальный статус")]
      [Required(ErrorMessage = "\"Финальный статус\" должно быть заполнено")]
      [DataMember(Name = "isFinal", EmitDefaultValue = false)]
      public virtual bool IsFinal { get; set; }
      
      /// <summary>
      /// Текст смс сообщений
      /// </summary>
      [Display(Description = "Текст смс сообщений")]
      [MaxLength(1000, ErrorMessage = "\"Текст смс сообщений\" не может быть больше 1000 символов")]
      [DataMember(Name = "smsMessage", EmitDefaultValue = false)]
      public virtual string SmsMessage { get; set; }
      
      /// <summary>
      /// Для льготных путевок
      /// </summary>
      [Display(Description = "Для льготных путевок")]
      [Required(ErrorMessage = "\"Для льготных путевок\" должно быть заполнено")]
      [DataMember(Name = "forPreferential", EmitDefaultValue = false)]
      public virtual bool ForPreferential { get; set; }
      
      /// <summary>
      /// Для коммерческих путевок
      /// </summary>
      [Display(Description = "Для коммерческих путевок")]
      [Required(ErrorMessage = "\"Для коммерческих путевок\" должно быть заполнено")]
      [DataMember(Name = "forCommerce", EmitDefaultValue = false)]
      public virtual bool ForCommerce { get; set; }
      
      /// <summary>
      /// Наименование для коммерции
      /// </summary>
      [Display(Description = "Наименование для коммерции")]
      [MaxLength(1000, ErrorMessage = "\"Наименование для коммерции\" не может быть больше 1000 символов")]
      [DataMember(Name = "commerceName", EmitDefaultValue = false)]
      public virtual string CommerceName { get; set; }
      
      
      /// <summary>
      /// Из какого статуса
      /// </summary>
      [InverseProperty("FromStatus")]
      [DataMember(Name = "action", EmitDefaultValue = false)]
      public virtual ICollection<StatusAction> Action { get; set; }
      
      /// <summary>
      /// Автор
      /// </summary>
      [ForeignKey("CreateUser")]
      [DataMember(Name = "createUserId")]
      [Display(Description = "Автор")]
      public virtual long? CreateUserId { get; set; }
      
      
      /// <summary>
      /// Автор
      /// </summary>
      [Display(Description = "Автор")]
      [DataMember(Name = "createUser", EmitDefaultValue = false)]
      public virtual Account CreateUser { get; set; }

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