// File:    SendEmailAndSmsAttachment.cs
// Purpose: Definition of Class SendEmailAndSmsAttachment

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Отправка уведомлений (Вложения)
   /// </summary>
   [Serializable]
   [DataContract(Name = "sendEmailAndSmsAttachment")]
   public partial class SendEmailAndSmsAttachment : IEntityBase
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
      /// Имя файла
      /// </summary>
      [Display(Description = "Имя файла")]
      [MaxLength(1000, ErrorMessage = "\"Имя файла\" не может быть больше 1000 символов")]
      [Required(ErrorMessage = "\"Имя файла\" не может быть пустым")]
      [DataMember(Name = "name", EmitDefaultValue = false)]
      public virtual string Name { get; set; }
      
      /// <summary>
      /// Адрес для загрузки
      /// </summary>
      [Display(Description = "Адрес для загрузки")]
      [MaxLength(1000, ErrorMessage = "\"Адрес для загрузки\" не может быть больше 1000 символов")]
      [Required(ErrorMessage = "\"Адрес для загрузки\" не может быть пустым")]
      [DataMember(Name = "urlToDownload", EmitDefaultValue = false)]
      public virtual string UrlToDownload { get; set; }
      
      
      /// <summary>
      /// Вложения
      /// </summary>
      [ForeignKey("SendEmailAndSms")]
      [DataMember(Name = "sendEmailAndSmsId")]
      [Display(Description = "Вложения")]
      public virtual long? SendEmailAndSmsId { get; set; }
      
      
      /// <summary>
      /// Вложения
      /// </summary>
      [InverseProperty("Attachments")]
      [Display(Description = "Вложения")]
      [DataMember(Name = "sendEmailAndSms", EmitDefaultValue = false)]
      public virtual SendEmailAndSms SendEmailAndSms { get; set; }

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