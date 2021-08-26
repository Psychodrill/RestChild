// File:    History.cs
// Purpose: Definition of Class History

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Mobile.Domain
{
    /// <summary>
    ///     История
    /// </summary>
    [Serializable]
    [DataContract(Name = "history")]
    public class History : IEntityBase
    {
        /// <summary>
        ///     Дата изменения
        /// </summary>
        [Display(Description = "Дата изменения")]
        [Required(ErrorMessage = "\"Дата изменения\" должно быть заполнено")]
        [DataMember(Name = "dateChange", EmitDefaultValue = false)]
        public virtual DateTime DateChange { get; set; }

        /// <summary>
        ///     Событие
        /// </summary>
        [Display(Description = "Событие")]
        [MaxLength(1000, ErrorMessage = "\"Событие\" не может быть больше 1000 символов")]
        [Required(ErrorMessage = "\"Событие\" не может быть пустым")]
        [DataMember(Name = "eventCode", EmitDefaultValue = false)]
        public virtual string EventCode { get; set; }

        /// <summary>
        ///     Комментарий
        /// </summary>
        [Display(Description = "Комментарий")]
        [DataMember(Name = "commentary", EmitDefaultValue = false)]
        public virtual string Commentary { get; set; }

        /// <summary>
        ///     Строка с автором изменений
        /// </summary>
        [Display(Description = "Строка с автором изменений")]
        [MaxLength(1000, ErrorMessage = "\"Строка с автором изменений\" не может быть больше 1000 символов")]
        [DataMember(Name = "authorString", EmitDefaultValue = false)]
        public virtual string AuthorString { get; set; }


        /// <summary>
        ///     В какой статус
        /// </summary>
        [ForeignKey("ToState")]
        [DataMember(Name = "toStateId")]
        [Display(Description = "В какой статус")]
        public virtual long? ToStateId { get; set; }


        /// <summary>
        ///     В какой статус
        /// </summary>
        [Display(Description = "В какой статус")]
        [DataMember(Name = "toState", EmitDefaultValue = false)]
        public virtual State ToState { get; set; }

        /// <summary>
        ///     Внешний пользователь
        /// </summary>
        [ForeignKey("AccountExternal")]
        [DataMember(Name = "accountExternalId")]
        [Display(Description = "Внешний пользователь")]
        public virtual long? AccountExternalId { get; set; }


        /// <summary>
        ///     Внешний пользователь
        /// </summary>
        [Display(Description = "Внешний пользователь")]
        [DataMember(Name = "accountExternal", EmitDefaultValue = false)]
        public virtual AccountExternal AccountExternal { get; set; }

        /// <summary>
        ///     Из какого статуса
        /// </summary>
        [ForeignKey("FromState")]
        [DataMember(Name = "fromStateId")]
        [Display(Description = "Из какого статуса")]
        public virtual long? FromStateId { get; set; }


        /// <summary>
        ///     Из какого статуса
        /// </summary>
        [Display(Description = "Из какого статуса")]
        [DataMember(Name = "fromState", EmitDefaultValue = false)]
        public virtual State FromState { get; set; }

        /// <summary>
        ///     История
        /// </summary>
        [ForeignKey("Link")]
        [DataMember(Name = "linkId")]
        [Display(Description = "История")]
        public virtual long? LinkId { get; set; }


        /// <summary>
        ///     История
        /// </summary>
        [InverseProperty("Historys")]
        [Display(Description = "История")]
        [DataMember(Name = "link", EmitDefaultValue = false)]
        public virtual Link Link { get; set; }

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
