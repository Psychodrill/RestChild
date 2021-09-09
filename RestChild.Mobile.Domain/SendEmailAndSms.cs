// File:    SendEmailAndSms.cs
// Purpose: Definition of Class SendEmailAndSms

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Mobile.Domain
{
    /// <summary>
    ///     Отправка уведомлений по электронной почте и СМС
    /// </summary>
    [Serializable]
    [DataContract(Name = "sendEmailAndSms")]
    public partial class SendEmailAndSms : IEntityBase
    {
        /// <summary>
        ///     Электронная почта организации
        /// </summary>
        [Display(Description = "Электронная почта организации")]
        [DataMember(Name = "email", EmitDefaultValue = false)]
        public virtual string Email { get; set; }

        /// <summary>
        ///     Телефон
        /// </summary>
        [Display(Description = "Телефон")]
        [MaxLength(1000, ErrorMessage = "\"Телефон\" не может быть больше 1000 символов")]
        [DataMember(Name = "phone", EmitDefaultValue = false)]
        public virtual string Phone { get; set; }

        /// <summary>
        ///     Текст сообщения по почте
        /// </summary>
        [Display(Description = "Текст сообщения по почте")]
        [DataMember(Name = "emailMessage", EmitDefaultValue = false)]
        public virtual string EmailMessage { get; set; }

        /// <summary>
        ///     Заголовок
        /// </summary>
        [Display(Description = "Заголовок")]
        [MaxLength(1000, ErrorMessage = "\"Заголовок\" не может быть больше 1000 символов")]
        [DataMember(Name = "emailTitle", EmitDefaultValue = false)]
        public virtual string EmailTitle { get; set; }

        /// <summary>
        ///     Текст по телефон
        /// </summary>
        [Display(Description = "Текст по телефон")]
        [MaxLength(1000, ErrorMessage = "\"Текст по телефон\" не может быть больше 1000 символов")]
        [DataMember(Name = "smsMessage", EmitDefaultValue = false)]
        public virtual string SmsMessage { get; set; }

        /// <summary>
        ///     Отправлена почта
        /// </summary>
        [Display(Description = "Отправлена почта")]
        [Required(ErrorMessage = "\"Отправлена почта\" должно быть заполнено")]
        [DataMember(Name = "isEmailSended", EmitDefaultValue = false)]
        public virtual bool IsEmailSended { get; set; }

        /// <summary>
        ///     Отправлена СМС
        /// </summary>
        [Display(Description = "Отправлена СМС")]
        [Required(ErrorMessage = "\"Отправлена СМС\" должно быть заполнено")]
        [DataMember(Name = "isSmsSended", EmitDefaultValue = false)]
        public virtual bool IsSmsSended { get; set; }

        /// <summary>
        ///     Дата действия
        /// </summary>
        [Display(Description = "Дата действия")]
        [Required(ErrorMessage = "\"Дата действия\" должно быть заполнено")]
        [DataMember(Name = "dateCreate", EmitDefaultValue = false)]
        public virtual DateTime DateCreate { get; set; }

        /// <summary>
        ///     Дата отправкт почты
        /// </summary>
        [Display(Description = "Дата отправкт почты")]
        [DataMember(Name = "dateEmail", EmitDefaultValue = false)]
        public virtual DateTime? DateEmail { get; set; }

        /// <summary>
        ///     Дата отправик СМС
        /// </summary>
        [Display(Description = "Дата отправик СМС")]
        [DataMember(Name = "dateSms", EmitDefaultValue = false)]
        public virtual DateTime? DateSms { get; set; }

        /// <summary>
        ///     Уникальный идентификатор
        /// </summary>
        [Display(Description = "Уникальный идентификатор")]
        [Required(ErrorMessage = "\"Уникальный идентификатор\" должно быть заполнено")]
        [Key]
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public virtual long Id { get; set; }

        /// <summary>
        ///     Последнее сохранение
        /// </summary>
        [Display(Description = "Последнее сохранение")]
        [DataMember(Name = "lastUpdateTick", EmitDefaultValue = false)]
        public virtual long LastUpdateTick { get; set; }

        /// <summary>
        ///     Внешний ключ
        /// </summary>
        [Display(Description = "Внешний ключ")]
        [DataMember(Name = "eid", EmitDefaultValue = false)]
        [Index]
        public virtual long? Eid { get; set; }

        /// <summary>
        ///     Статус обмена по сущности
        /// </summary>
        [Display(Description = "Статус обмена по сущности")]
        [DataMember(Name = "eidSendStatus", EmitDefaultValue = false)]
        [Index]
        public virtual long? EidSendStatus { get; set; }

        /// <summary>
        ///     Дата синхронизации
        /// </summary>
        [Display(Description = "Дата синхронизации")]
        [DataMember(Name = "eidSyncDate", EmitDefaultValue = false)]
        public virtual DateTime? EidSyncDate { get; set; }
    }
}
