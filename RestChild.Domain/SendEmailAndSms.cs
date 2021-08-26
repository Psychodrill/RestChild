// File:    SendEmailAndSms.cs
// Purpose: Definition of Class SendEmailAndSms

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Отправка уведомлений по электронной почте и СМС
   /// </summary>
   [Serializable]
   [DataContract(Name = "sendEmailAndSms")]
   public partial class SendEmailAndSms : IEntityBase
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
      /// Электронная почта
      /// </summary>
      [Display(Description = "Электронная почта")]
      [MaxLength(1000, ErrorMessage = "\"Электронная почта\" не может быть больше 1000 символов")]
      [DataMember(Name = "email", EmitDefaultValue = false)]
      public virtual string Email { get; set; }
      
      /// <summary>
      /// Телефон
      /// </summary>
      [Display(Description = "Телефон")]
      [MaxLength(1000, ErrorMessage = "\"Телефон\" не может быть больше 1000 символов")]
      [DataMember(Name = "phone", EmitDefaultValue = false)]
      public virtual string Phone { get; set; }
      
      /// <summary>
      /// Текст сообщения по почте
      /// </summary>
      [Display(Description = "Текст сообщения по почте")]
      [DataMember(Name = "emailMessage", EmitDefaultValue = false)]
      public virtual String EmailMessage { get; set; }
      
      /// <summary>
      /// Заголовок
      /// </summary>
      [Display(Description = "Заголовок")]
      [MaxLength(1000, ErrorMessage = "\"Заголовок\" не может быть больше 1000 символов")]
      [DataMember(Name = "emailTitle", EmitDefaultValue = false)]
      public virtual string EmailTitle { get; set; }
      
      /// <summary>
      /// Текст по телефон
      /// </summary>
      [Display(Description = "Текст по телефон")]
      [MaxLength(1000, ErrorMessage = "\"Текст по телефон\" не может быть больше 1000 символов")]
      [DataMember(Name = "smsMessage", EmitDefaultValue = false)]
      public virtual string SmsMessage { get; set; }
      
      /// <summary>
      /// Отправлена почта
      /// </summary>
      [Display(Description = "Отправлена почта")]
      [Required(ErrorMessage = "\"Отправлена почта\" должно быть заполнено")]
      [DataMember(Name = "isEmailSended", EmitDefaultValue = false)]
      public virtual bool IsEmailSended { get; set; }
      
      /// <summary>
      /// Отправлена СМС
      /// </summary>
      [Display(Description = "Отправлена СМС")]
      [Required(ErrorMessage = "\"Отправлена СМС\" должно быть заполнено")]
      [DataMember(Name = "isSmsSended", EmitDefaultValue = false)]
      public virtual bool IsSmsSended { get; set; }
      
      /// <summary>
      /// Дата создания
      /// </summary>
      [Display(Description = "Дата создания")]
      [Required(ErrorMessage = "\"Дата создания\" должно быть заполнено")]
      [DataMember(Name = "dateCreate", EmitDefaultValue = false)]
      public virtual DateTime DateCreate { get; set; }
      
      /// <summary>
      /// Дата отправкт почты
      /// </summary>
      [Display(Description = "Дата отправкт почты")]
      [DataMember(Name = "dateEmail", EmitDefaultValue = false)]
      public virtual DateTime? DateEmail { get; set; }
      
      /// <summary>
      /// Дата отправик СМС
      /// </summary>
      [Display(Description = "Дата отправик СМС")]
      [DataMember(Name = "dateSms", EmitDefaultValue = false)]
      public virtual DateTime? DateSms { get; set; }
      
      /// <summary>
      /// ПланируемаяДатаОтправки
      /// </summary>
      [Display(Description = "ПланируемаяДатаОтправки")]
      [DataMember(Name = "dateToSend", EmitDefaultValue = false)]
      public virtual DateTime? DateToSend { get; set; }
      
      
      /// <summary>
      /// Вложения
      /// </summary>
      [InverseProperty("SendEmailAndSms")]
      [DataMember(Name = "attachments", EmitDefaultValue = false)]
      public virtual ICollection<SendEmailAndSmsAttachment> Attachments { get; set; }
      
      /// <summary>
      /// Заявление
      /// </summary>
      [ForeignKey("Request")]
      [DataMember(Name = "requestId")]
      [Display(Description = "Заявление")]
      public virtual long? RequestId { get; set; }
      
      
      /// <summary>
      /// Заявление
      /// </summary>
      [Display(Description = "Заявление")]
      [DataMember(Name = "request", EmitDefaultValue = false)]
      public virtual Request Request { get; set; }
      
      /// <summary>
      /// Статус заявления
      /// </summary>
      [ForeignKey("StatusRequest")]
      [DataMember(Name = "statusRequestId")]
      [Display(Description = "Статус заявления")]
      public virtual long? StatusRequestId { get; set; }
      
      
      /// <summary>
      /// Статус заявления
      /// </summary>
      [Display(Description = "Статус заявления")]
      [DataMember(Name = "statusRequest", EmitDefaultValue = false)]
      public virtual Status StatusRequest { get; set; }

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