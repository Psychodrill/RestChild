// File:    AccountHistoryLogin.cs
// Purpose: Definition of Class AccountHistoryLogin

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Mobile.Domain
{
    /// <summary>
    ///     История входов в систему
    /// </summary>
    [Serializable]
    [DataContract(Name = "accountHistoryLogin")]
    public class AccountHistoryLogin : IEntityBase
    {
        /// <summary>
        ///     Дата входа
        /// </summary>
        [Display(Description = "Дата входа")]
        [Required(ErrorMessage = "\"Дата входа\" должно быть заполнено")]
        [DataMember(Name = "dateEnter", EmitDefaultValue = false)]
        public virtual DateTime DateEnter { get; set; }

        /// <summary>
        ///     Дата активности
        /// </summary>
        [Display(Description = "Дата активности")]
        [Required(ErrorMessage = "\"Дата активности\" должно быть заполнено")]
        [DataMember(Name = "dateLastActivity", EmitDefaultValue = false)]
        public virtual DateTime DateLastActivity { get; set; }

        /// <summary>
        ///     Дата завершения сессии
        /// </summary>
        [Display(Description = "Дата завершения сессии")]
        [DataMember(Name = "dateExit", EmitDefaultValue = false)]
        public virtual DateTime? DateExit { get; set; }

        /// <summary>
        ///     Ид сессии
        /// </summary>
        [Display(Description = "Ид сессии")]
        [MaxLength(1000, ErrorMessage = "\"Ид сессии\" не может быть больше 1000 символов")]
        [DataMember(Name = "sessionUid", EmitDefaultValue = false)]
        public virtual string SessionUid { get; set; }

        /// <summary>
        ///     Адрес клиента
        /// </summary>
        [Display(Description = "Адрес клиента")]
        [MaxLength(1000, ErrorMessage = "\"Адрес клиента\" не может быть больше 1000 символов")]
        [DataMember(Name = "remoteAddr", EmitDefaultValue = false)]
        public virtual string RemoteAddr { get; set; }

        /// <summary>
        ///     Данные о браузере
        /// </summary>
        [Display(Description = "Данные о браузере")]
        [DataMember(Name = "userAgent", EmitDefaultValue = false)]
        public virtual string UserAgent { get; set; }

        /// <summary>
        ///     Сессия остановлена
        /// </summary>
        [Display(Description = "Сессия остановлена")]
        [Required(ErrorMessage = "\"Сессия остановлена\" должно быть заполнено")]
        [DataMember(Name = "stopSession", EmitDefaultValue = false)]
        public virtual bool StopSession { get; set; }

        /// <summary>
        ///     Имя пользователя
        /// </summary>
        [Display(Description = "Имя пользователя")]
        [MaxLength(1000, ErrorMessage = "\"Имя пользователя\" не может быть больше 1000 символов")]
        [DataMember(Name = "login", EmitDefaultValue = false)]
        public virtual string Login { get; set; }

        /// <summary>
        ///     Успешная авторизация
        /// </summary>
        [Display(Description = "Успешная авторизация")]
        [Required(ErrorMessage = "\"Успешная авторизация\" должно быть заполнено")]
        [DataMember(Name = "isAuthorized", EmitDefaultValue = false)]
        public virtual bool IsAuthorized { get; set; }


        /// <summary>
        ///     Пользователь
        /// </summary>
        [ForeignKey("Account")]
        [DataMember(Name = "accountId")]
        [Display(Description = "Пользователь")]
        public virtual long? AccountId { get; set; }


        /// <summary>
        ///     Пользователь
        /// </summary>
        [Display(Description = "Пользователь")]
        [DataMember(Name = "account", EmitDefaultValue = false)]
        public virtual Account Account { get; set; }

        /// <summary>
        ///     Пользователь (внешний)
        /// </summary>
        [ForeignKey("AccountExternal")]
        [DataMember(Name = "accountExternalId")]
        [Display(Description = "Пользователь (внешний)")]
        public virtual long? AccountExternalId { get; set; }


        /// <summary>
        ///     Пользователь (внешний)
        /// </summary>
        [Display(Description = "Пользователь (внешний)")]
        [DataMember(Name = "accountExternal", EmitDefaultValue = false)]
        public virtual AccountExternal AccountExternal { get; set; }

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
