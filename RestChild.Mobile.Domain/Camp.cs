// File:    Camp.cs
// Purpose: Definition of Class Camp

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Mobile.Domain
{
    /// <summary>
    ///     Лагерь
    /// </summary>
    [Serializable]
    [DataContract(Name = "camp")]
    public class Camp : IEntityBase
    {
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
        ///     Наименование
        /// </summary>
        [Display(Description = "Наименование")]
        [MaxLength(1000, ErrorMessage = "\"Наименование\" не может быть больше 1000 символов")]
        [DataMember(Name = "name", EmitDefaultValue = false)]
        public virtual string Name { get; set; }

        /// <summary>
        ///     Адрес
        /// </summary>
        [Display(Description = "Адрес")]
        [MaxLength(1000, ErrorMessage = "\"Адрес\" не может быть больше 1000 символов")]
        [DataMember(Name = "address", EmitDefaultValue = false)]
        public virtual string Address { get; set; }

        /// <summary>
        ///     Ближайший город
        /// </summary>
        [Display(Description = "Ближайший город")]
        [MaxLength(1000, ErrorMessage = "\"Ближайший город\" не может быть больше 1000 символов")]
        [DataMember(Name = "nearestCity", EmitDefaultValue = false)]
        public virtual string NearestCity { get; set; }

        /// <summary>
        ///     Идентификатор вида отеля/лагеря (из МГТ)
        /// </summary>
        [Display(Description = "Идентификатор вида отеля/лагеря (из МГТ)")]
        [DataMember(Name = "hotelTypeId", EmitDefaultValue = false)]
        public virtual long? HotelTypeId { get; set; }


        /// <summary>
        ///     Задания для лагеря
        /// </summary>
        [InverseProperty("Camp")]
        [DataMember(Name = "tasks", EmitDefaultValue = false)]
        public virtual ICollection<CampTask> Tasks { get; set; }

        /// <summary>
        ///     Заезды
        /// </summary>
        [InverseProperty("Camp")]
        [DataMember(Name = "bouts", EmitDefaultValue = false)]
        public virtual ICollection<Bout> Bouts { get; set; }

        /// <summary>
        ///     Статус лагеря
        /// </summary>
        [ForeignKey("State")]
        [DataMember(Name = "stateId")]
        [Display(Description = "Статус лагеря")]
        public virtual long? StateId { get; set; }


        /// <summary>
        ///     Статус лагеря
        /// </summary>
        [Display(Description = "Статус лагеря")]
        [DataMember(Name = "state", EmitDefaultValue = false)]
        public virtual State State { get; set; }

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
