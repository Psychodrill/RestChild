// File:    PersonalType.cs
// Purpose: Definition of Class PersonalType

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Mobile.Domain
{
    /// <summary>
    ///     Персонал. Вид
    /// </summary>
    [Serializable]
    [DataContract(Name = "personalType")]
    public class PersonalType : IEntityBase
    {
        /// <summary>
        ///     Наименование
        /// </summary>
        [Display(Description = "Наименование")]
        [MaxLength(1000, ErrorMessage = "\"Наименование\" не может быть больше 1000 символов")]
        [DataMember(Name = "name", EmitDefaultValue = false)]
        public virtual string Name { get; set; }

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
