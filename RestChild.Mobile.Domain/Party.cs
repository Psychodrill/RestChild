// File:    Party.cs
// Purpose: Definition of Class Party

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Mobile.Domain
{
    /// <summary>
    ///     Отряд
    /// </summary>
    [Serializable]
    [DataContract(Name = "party")]
    public class Party : IEntityBase
    {
        /// <summary>
        ///     Наименование
        /// </summary>
        [Display(Description = "Наименование")]
        [MaxLength(1000, ErrorMessage = "\"Наименование\" не может быть больше 1000 символов")]
        [DataMember(Name = "name", EmitDefaultValue = false)]
        public virtual string Name { get; set; }

        /// <summary>
        ///     Сортировка
        /// </summary>
        [Display(Description = "Сортировка")]
        [DataMember(Name = "sortOrder", EmitDefaultValue = false)]
        public virtual int? SortOrder { get; set; }


        /// <summary>
        ///     Отдыхающие в отряде
        /// </summary>
        [InverseProperty("Party")]
        [DataMember(Name = "campers", EmitDefaultValue = false)]
        public virtual ICollection<Camper> Campers { get; set; }

        /// <summary>
        ///     Отряд
        /// </summary>
        [InverseProperty("Party")]
        [DataMember(Name = "peronals", EmitDefaultValue = false)]
        public virtual ICollection<BoutPersonal> Peronals { get; set; }

        /// <summary>
        ///     Отряды в заезде
        /// </summary>
        [ForeignKey("Bout")]
        [DataMember(Name = "boutId")]
        [Display(Description = "Отряды в заезде")]
        public virtual long? BoutId { get; set; }


        /// <summary>
        ///     Отряды в заезде
        /// </summary>
        [InverseProperty("Partys")]
        [Display(Description = "Отряды в заезде")]
        [DataMember(Name = "bout", EmitDefaultValue = false)]
        public virtual Bout Bout { get; set; }

        /// <summary>
        ///     Статус
        /// </summary>
        [ForeignKey("State")]
        [DataMember(Name = "stateId")]
        [Display(Description = "Статус")]
        public virtual long? StateId { get; set; }


        /// <summary>
        ///     Статус
        /// </summary>
        [Display(Description = "Статус")]
        [DataMember(Name = "state", EmitDefaultValue = false)]
        public virtual State State { get; set; }

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
