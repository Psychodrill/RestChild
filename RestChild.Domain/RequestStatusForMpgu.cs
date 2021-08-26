// File:    RequestStatusForMpgu.cs
// Purpose: Definition of Class RequestStatusForMpgu

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Заявление. Цепочка статусов для ЕЛК и МПГУ. Статус
   /// </summary>
   [Serializable]
   [DataContract(Name = "requestStatusForMpgu")]
   public partial class RequestStatusForMpgu : IEntityBase
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
      /// Код статуса
      /// </summary>
      [Display(Description = "Код статуса")]
      [Required(ErrorMessage = "\"Код статуса\" должно быть заполнено")]
      [DataMember(Name = "status", EmitDefaultValue = false)]
      public virtual long Status { get; set; }
      
      /// <summary>
      /// Код причины
      /// </summary>
      [Display(Description = "Код причины")]
      [MaxLength(1000, ErrorMessage = "\"Код причины\" не может быть больше 1000 символов")]
      [DataMember(Name = "reasonCode", EmitDefaultValue = false)]
      public virtual string ReasonCode { get; set; }
      
      /// <summary>
      /// Комментарий
      /// </summary>
      [Display(Description = "Комментарий")]
      [DataMember(Name = "commentary", EmitDefaultValue = false)]
      public virtual String Commentary { get; set; }
      
      /// <summary>
      /// Наименование статуса
      /// </summary>
      [Display(Description = "Наименование статуса")]
      [DataMember(Name = "name", EmitDefaultValue = false)]
      public virtual String Name { get; set; }
      
      /// <summary>
      /// Сортировка статусов
      /// </summary>
      [Display(Description = "Сортировка статусов")]
      [Required(ErrorMessage = "\"Сортировка статусов\" должно быть заполнено")]
      [DataMember(Name = "orderField", EmitDefaultValue = false)]
      public virtual int OrderField { get; set; }
      
      /// <summary>
      /// Отправлять Email
      /// </summary>
      [Display(Description = "Отправлять Email")]
      [Required(ErrorMessage = "\"Отправлять Email\" должно быть заполнено")]
      [DataMember(Name = "sendEmail", EmitDefaultValue = false)]
      public virtual bool SendEmail { get; set; }
      
      /// <summary>
      /// Уведомление для отправки
      /// </summary>
      [Display(Description = "Уведомление для отправки")]
      [MaxLength(1000, ErrorMessage = "\"Уведомление для отправки\" не может быть больше 1000 символов")]
      [DataMember(Name = "notificationToSend", EmitDefaultValue = false)]
      public virtual string NotificationToSend { get; set; }
      
      
      /// <summary>
      /// Статусы для отправки в МПГУ
      /// </summary>
      [ForeignKey("Chain")]
      [DataMember(Name = "chainId")]
      [Display(Description = "Статусы для отправки в МПГУ")]
      public virtual long? ChainId { get; set; }
      
      
      /// <summary>
      /// Статусы для отправки в МПГУ
      /// </summary>
      [InverseProperty("Statuses")]
      [Display(Description = "Статусы для отправки в МПГУ")]
      [DataMember(Name = "chain", EmitDefaultValue = false)]
      public virtual RequestStatusChainForMpgu Chain { get; set; }
      
      /// <summary>
      /// Связь в рамках отправки документов в РЦХЭД
      /// </summary>
      [InverseProperty("MpguStatus")]
      [DataMember(Name = "cshedDocAndSign", EmitDefaultValue = false)]
      public virtual ICollection<RequestStatusCshedSendAndSignDocument> CshedDocAndSign { get; set; }

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