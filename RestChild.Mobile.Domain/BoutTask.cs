// File:    BoutTask.cs
// Purpose: Definition of Class BoutTask

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Mobile.Domain
{
    /// <summary>
    ///     Задание. В лагере. В заезде
    /// </summary>
    [Serializable]
    [DataContract(Name = "boutTask")]
    public class BoutTask : IEntityBase
    {
        /// <summary>
        ///     Наименование
        /// </summary>
        [Display(Description = "Наименование")]
        [MaxLength(1000, ErrorMessage = "\"Наименование\" не может быть больше 1000 символов")]
        [DataMember(Name = "name", EmitDefaultValue = false)]
        public virtual string Name { get; set; }

        /// <summary>
        ///     Описание
        /// </summary>
        [Display(Description = "Описание")]
        [DataMember(Name = "description", EmitDefaultValue = false)]
        public virtual string Description { get; set; }

        /// <summary>
        ///     Дата начала выполнения
        /// </summary>
        [Display(Description = "Дата начала выполнения")]
        [DataMember(Name = "startDate", EmitDefaultValue = false)]
        public virtual DateTime? StartDate { get; set; }

        /// <summary>
        ///     Дата окончания выполнения
        /// </summary>
        [Display(Description = "Дата окончания выполнения")]
        [DataMember(Name = "finishDate", EmitDefaultValue = false)]
        public virtual DateTime? FinishDate { get; set; }

        /// <summary>
        ///     Стоимость
        /// </summary>
        [Display(Description = "Стоимость")]
        [Required(ErrorMessage = "\"Стоимость\" должно быть заполнено")]
        [DataMember(Name = "price", EmitDefaultValue = false)]
        public virtual decimal Price { get; set; }

        /// <summary>
        ///     Количество на заезд
        /// </summary>
        [Display(Description = "Количество на заезд")]
        [Required(ErrorMessage = "\"Количество на заезд\" должно быть заполнено")]
        [DataMember(Name = "countOnBout", EmitDefaultValue = false)]
        public virtual int CountOnBout { get; set; }

        /// <summary>
        ///     Расписание
        /// </summary>
        [Display(Description = "Расписание")]
        [DataMember(Name = "timesheet", EmitDefaultValue = false)]
        public virtual string Timesheet { get; set; }


        /// <summary>
        ///     Связь задания с его выполнением
        /// </summary>
        [InverseProperty("BoutTask")]
        [DataMember(Name = "camperTasks", EmitDefaultValue = false)]
        public virtual ICollection<CamperTask> CamperTasks { get; set; }

        /// <summary>
        ///     История задания в лагере
        /// </summary>
        [ForeignKey("Link")]
        [DataMember(Name = "linkId")]
        [Display(Description = "История задания в лагере")]
        public virtual long? LinkId { get; set; }


        /// <summary>
        ///     История задания в лагере
        /// </summary>
        [Display(Description = "История задания в лагере")]
        [DataMember(Name = "link", EmitDefaultValue = false)]
        public virtual Link Link { get; set; }

        /// <summary>
        ///     Связь задания в лагере с заездом
        /// </summary>
        [ForeignKey("CampTask")]
        [DataMember(Name = "campTaskId")]
        [Display(Description = "Связь задания в лагере с заездом")]
        public virtual long? CampTaskId { get; set; }


        /// <summary>
        ///     Связь задания в лагере с заездом
        /// </summary>
        [InverseProperty("BoutTasks")]
        [Display(Description = "Связь задания в лагере с заездом")]
        [DataMember(Name = "campTask", EmitDefaultValue = false)]
        public virtual CampTask CampTask { get; set; }

        /// <summary>
        ///     Связь заезда и заданий
        /// </summary>
        [ForeignKey("Bout")]
        [DataMember(Name = "boutId")]
        [Display(Description = "Связь заезда и заданий")]
        public virtual long? BoutId { get; set; }


        /// <summary>
        ///     Связь заезда и заданий
        /// </summary>
        [InverseProperty("Tasks")]
        [Display(Description = "Связь заезда и заданий")]
        [DataMember(Name = "bout", EmitDefaultValue = false)]
        public virtual Bout Bout { get; set; }

        /// <summary>
        ///     Статус задания в заезде
        /// </summary>
        [ForeignKey("State")]
        [DataMember(Name = "stateId")]
        [Display(Description = "Статус задания в заезде")]
        public virtual long? StateId { get; set; }


        /// <summary>
        ///     Статус задания в заезде
        /// </summary>
        [Display(Description = "Статус задания в заезде")]
        [DataMember(Name = "state", EmitDefaultValue = false)]
        public virtual State State { get; set; }

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
