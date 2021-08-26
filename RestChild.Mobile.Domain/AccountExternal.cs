// File:    AccountExternal.cs
// Purpose: Definition of Class AccountExternal

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Mobile.Domain
{
    /// <summary>
    ///     Пользователь внешний
    /// </summary>
    [Serializable]
    [DataContract(Name = "accountExternal")]
    public class AccountExternal : IEntityBase
    {
        /// <summary>
        ///     Наименование
        /// </summary>
        [Display(Description = "Наименование")]
        [MaxLength(1000, ErrorMessage = "\"Наименование\" не может быть больше 1000 символов")]
        [DataMember(Name = "name", EmitDefaultValue = false)]
        public virtual string Name { get; set; }

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
        ///     Внешний ИД
        /// </summary>
        [Display(Description = "Внешний ИД")]
        [DataMember(Name = "externalUid", EmitDefaultValue = false)]
        public virtual long? ExternalUid { get; set; }

        /// <summary>
        ///     Заблокирован
        /// </summary>
        [Display(Description = "Заблокирован")]
        [Required(ErrorMessage = "\"Заблокирован\" должно быть заполнено")]
        [DataMember(Name = "isBlocked", EmitDefaultValue = false)]
        public virtual bool IsBlocked { get; set; }

        /// <summary>
        ///     Уникальный идентификатор (не генерируемый)
        /// </summary>
        [Display(Description = "Уникальный идентификатор (не генерируемый)")]
        [Required(ErrorMessage = "\"Уникальный идентификатор (не генерируемый)\" должно быть заполнено")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
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
