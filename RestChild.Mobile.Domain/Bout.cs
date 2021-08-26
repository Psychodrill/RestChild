// File:    Bout.cs
// Purpose: Definition of Class Bout

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Mobile.Domain
{
    /// <summary>
    ///     Заезд
    /// </summary>
    [Serializable]
    [DataContract(Name = "bout")]
    public class Bout : IEntityBase
    {
        /// <summary>
        ///     Дата заезда
        /// </summary>
        [Display(Description = "Дата заезда")]
        [Required(ErrorMessage = "\"Дата заезда\" должно быть заполнено")]
        [DataMember(Name = "dateIncome", EmitDefaultValue = false)]
        public virtual DateTime DateIncome { get; set; }

        /// <summary>
        ///     Дата отъезда
        /// </summary>
        [Display(Description = "Дата отъезда")]
        [Required(ErrorMessage = "\"Дата отъезда\" должно быть заполнено")]
        [DataMember(Name = "dateOutcome", EmitDefaultValue = false)]
        public virtual DateTime DateOutcome { get; set; }

        /// <summary>
        ///     Наименование
        /// </summary>
        [Display(Description = "Наименование")]
        [MaxLength(1000, ErrorMessage = "\"Наименование\" не может быть больше 1000 символов")]
        [DataMember(Name = "name", EmitDefaultValue = false)]
        public virtual string Name { get; set; }

        /// <summary>
        ///     Год кампании
        /// </summary>
        [Display(Description = "Год кампании")]
        [Required(ErrorMessage = "\"Год кампании\" должно быть заполнено")]
        [DataMember(Name = "yearOfCompany", EmitDefaultValue = false)]
        public virtual int YearOfCompany { get; set; }

        /// <summary>
        ///     Смена
        /// </summary>
        [Display(Description = "Смена")]
        [MaxLength(1000, ErrorMessage = "\"Смена\" не может быть больше 1000 символов")]
        [DataMember(Name = "change", EmitDefaultValue = false)]
        public virtual string Change { get; set; }


        /// <summary>
        ///     Отдыхающие в заезде
        /// </summary>
        [InverseProperty("Bout")]
        [DataMember(Name = "campers", EmitDefaultValue = false)]
        public virtual ICollection<Camper> Campers { get; set; }

        /// <summary>
        ///     Отряды в заезде
        /// </summary>
        [InverseProperty("Bout")]
        [DataMember(Name = "partys", EmitDefaultValue = false)]
        public virtual ICollection<Party> Partys { get; set; }

        /// <summary>
        ///     Связь заезда и заданий
        /// </summary>
        [InverseProperty("Bout")]
        [DataMember(Name = "tasks", EmitDefaultValue = false)]
        public virtual ICollection<BoutTask> Tasks { get; set; }

        /// <summary>
        ///     Сотрудники
        /// </summary>
        [InverseProperty("Bout")]
        [DataMember(Name = "personals", EmitDefaultValue = false)]
        public virtual ICollection<BoutPersonal> Personals { get; set; }

        /// <summary>
        ///     Заезды
        /// </summary>
        [ForeignKey("Camp")]
        [DataMember(Name = "campId")]
        [Display(Description = "Заезды")]
        public virtual long? CampId { get; set; }


        /// <summary>
        ///     Заезды
        /// </summary>
        [InverseProperty("Bouts")]
        [Display(Description = "Заезды")]
        [DataMember(Name = "camp", EmitDefaultValue = false)]
        public virtual Camp Camp { get; set; }

        /// <summary>
        ///     Смена
        /// </summary>
        [ForeignKey("GroupedTime")]
        [DataMember(Name = "groupedTimeId")]
        [Display(Description = "Смена")]
        public virtual long? GroupedTimeId { get; set; }


        /// <summary>
        ///     Смена
        /// </summary>
        [Display(Description = "Смена")]
        [DataMember(Name = "groupedTime", EmitDefaultValue = false)]
        public virtual GroupedTime GroupedTime { get; set; }

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
