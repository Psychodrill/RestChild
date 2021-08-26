// File:    ExchangeUTS.cs
// Purpose: Definition of Class ExchangeUTS

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Domain
{


   /// <summary>
   /// Обмен с ЕТП
   /// </summary>
   [Serializable]
   [DataContract(Name = "exchangeUTS")]
   public partial class ExchangeUTS : IEntityBase
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
      /// Сообщение
      /// </summary>
      [Display(Description = "Сообщение")]
      [DataMember(Name = "message", EmitDefaultValue = false)]
      public virtual String Message { get; set; }
      
      /// <summary>
      /// Отправлено/Обработано
      /// </summary>
      [Display(Description = "Отправлено/Обработано")]
      [Required(ErrorMessage = "\"Отправлено/Обработано\" должно быть заполнено")]
      [DataMember(Name = "processed", EmitDefaultValue = false)]
      public virtual bool Processed { get; set; }
      
      /// <summary>
      /// Имя очереди
      /// </summary>
      [Display(Description = "Имя очереди")]
      [MaxLength(1000, ErrorMessage = "\"Имя очереди\" не может быть больше 1000 символов")]
      [DataMember(Name = "queueName", EmitDefaultValue = false)]
      public virtual string QueueName { get; set; }
      
      /// <summary>
      /// Дата запроса
      /// </summary>
      [Display(Description = "Дата запроса")]
      [Required(ErrorMessage = "\"Дата запроса\" должно быть заполнено")]
      [DataMember(Name = "dateCreate", EmitDefaultValue = false)]
      public virtual DateTime DateCreate { get; set; }
      
      /// <summary>
      /// Входящее
      /// </summary>
      [Display(Description = "Входящее")]
      [Required(ErrorMessage = "\"Входящее\" должно быть заполнено")]
      [DataMember(Name = "incoming", EmitDefaultValue = false)]
      public virtual bool Incoming { get; set; }
      
      /// <summary>
      /// От организации
      /// </summary>
      [Display(Description = "От организации")]
      [MaxLength(1000, ErrorMessage = "\"От организации\" не может быть больше 1000 символов")]
      [DataMember(Name = "fromOrgCode", EmitDefaultValue = false)]
      public virtual string FromOrgCode { get; set; }
      
      /// <summary>
      /// К организации
      /// </summary>
      [Display(Description = "К организации")]
      [MaxLength(1000, ErrorMessage = "\"К организации\" не может быть больше 1000 символов")]
      [DataMember(Name = "toOrgCode", EmitDefaultValue = false)]
      public virtual string ToOrgCode { get; set; }
      
      /// <summary>
      /// Ид сообщения
      /// </summary>
      [Display(Description = "Ид сообщения")]
      [MaxLength(1000, ErrorMessage = "\"Ид сообщения\" не может быть больше 1000 символов")]
      [DataMember(Name = "messageId", EmitDefaultValue = false)]
      public virtual string MessageId { get; set; }
      
      /// <summary>
      /// Ид заявления
      /// </summary>
      [Display(Description = "Ид заявления")]
      [MaxLength(1000, ErrorMessage = "\"Ид заявления\" не может быть больше 1000 символов")]
      [DataMember(Name = "serviceNumber", EmitDefaultValue = false)]
      public virtual string ServiceNumber { get; set; }
      
      /// <summary>
      /// Ошибка обработки
      /// </summary>
      [Display(Description = "Ошибка обработки")]
      [Required(ErrorMessage = "\"Ошибка обработки\" должно быть заполнено")]
      [DataMember(Name = "isError", EmitDefaultValue = false)]
      public virtual bool IsError { get; set; }
      
      /// <summary>
      /// Текст ошибки
      /// </summary>
      [Display(Description = "Текст ошибки")]
      [DataMember(Name = "errorText", EmitDefaultValue = false)]
      public virtual String ErrorText { get; set; }
      
      /// <summary>
      /// Служебная информация по ошибке
      /// </summary>
      [Display(Description = "Служебная информация по ошибке")]
      [DataMember(Name = "errorDescription", EmitDefaultValue = false)]
      public virtual String ErrorDescription { get; set; }
      
      /// <summary>
      /// Вид отдыха
      /// </summary>
      [Display(Description = "Вид отдыха")]
      [DataMember(Name = "typeOfRestId", EmitDefaultValue = false)]
      public virtual long? TypeOfRestId { get; set; }
      
      /// <summary>
      /// ИД бронирования
      /// </summary>
      [Display(Description = "ИД бронирования")]
      [DataMember(Name = "bookingGuid", EmitDefaultValue = false)]
      public virtual Guid? BookingGuid { get; set; }
      
      /// <summary>
      /// Установленный статус
      /// </summary>
      [Display(Description = "Установленный статус")]
      [DataMember(Name = "toState", EmitDefaultValue = false)]
      public virtual long? ToState { get; set; }
      
      /// <summary>
      /// Ошибка снятия брони
      /// </summary>
      [Display(Description = "Ошибка снятия брони")]
      [Required(ErrorMessage = "\"Ошибка снятия брони\" должно быть заполнено")]
      [DataMember(Name = "isErrorOnReleaseBooking", EmitDefaultValue = false)]
      public virtual bool IsErrorOnReleaseBooking { get; set; }
      
      /// <summary>
      /// Дата отправки запроса
      /// </summary>
      [Display(Description = "Дата отправки запроса")]
      [DataMember(Name = "dateToSend", EmitDefaultValue = false)]
      public virtual DateTime? DateToSend { get; set; }
      
      /// <summary>
      /// Уведомление подписано
      /// </summary>
      [Display(Description = "Уведомление подписано")]
      [Required(ErrorMessage = "\"Уведомление подписано\" должно быть заполнено")]
      [DataMember(Name = "isSigned", EmitDefaultValue = false)]
      public virtual bool IsSigned { get; set; }
      
      
      /// <summary>
      /// Обмен с ЕТП
      /// </summary>
      [ForeignKey("Request")]
      [DataMember(Name = "requestId")]
      [Display(Description = "Обмен с ЕТП")]
      public virtual long? RequestId { get; set; }
      
      
      /// <summary>
      /// Обмен с ЕТП
      /// </summary>
      [Display(Description = "Обмен с ЕТП")]
      [DataMember(Name = "request", EmitDefaultValue = false)]
      public virtual Request Request { get; set; }
      
      /// <summary>
      /// Пользователь выполнивший действие
      /// </summary>
      [ForeignKey("Account")]
      [DataMember(Name = "accountId")]
      [Display(Description = "Пользователь выполнивший действие")]
      public virtual long? AccountId { get; set; }
      
      
      /// <summary>
      /// Пользователь выполнивший действие
      /// </summary>
      [Display(Description = "Пользователь выполнивший действие")]
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