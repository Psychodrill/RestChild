// File:    Task.cs
// Purpose: Definition of Class Task

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Mobile.Domain
{
    /// <summary>
    ///     Задание
    /// </summary>
    [Serializable]
    [DataContract(Name = "task")]
    public class Task : IEntityBase
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
        ///     Стоимость
        /// </summary>
        [Display(Description = "Стоимость")]
        [Required(ErrorMessage = "\"Стоимость\" должно быть заполнено")]
        [DataMember(Name = "price", EmitDefaultValue = false)]
        public virtual decimal Price { get; set; }

        /// <summary>
        ///     Не для всех лагерей
        /// </summary>
        [Display(Description = "Не для всех лагерей")]
        [Required(ErrorMessage = "\"Не для всех лагерей\" должно быть заполнено")]
        [DataMember(Name = "notForAll", EmitDefaultValue = false)]
        public virtual bool NotForAll { get; set; }

        /// <summary>
        ///     Количество на заезд
        /// </summary>
        [Display(Description = "Количество на заезд")]
        [Required(ErrorMessage = "\"Количество на заезд\" должно быть заполнено")]
        [DataMember(Name = "countOnBout", EmitDefaultValue = false)]
        public virtual int CountOnBout { get; set; }

        /// <summary>
        ///     Время провердения задания начало
        /// </summary>
        [Display(Description = "Время провердения задания начало")]
        [DataMember(Name = "startTime", EmitDefaultValue = false)]
        public virtual DateTime? StartTime { get; set; }

        /// <summary>
        ///     Время провердения задания конец
        /// </summary>
        [Display(Description = "Время провердения задания конец")]
        [DataMember(Name = "finishTime", EmitDefaultValue = false)]
        public virtual DateTime? FinishTime { get; set; }

        /// <summary>
        ///     Дата начала использования
        /// </summary>
        [Display(Description = "Дата начала использования")]
        [DataMember(Name = "dateStartUsing", EmitDefaultValue = false)]
        public virtual DateTime? DateStartUsing { get; set; }

        /// <summary>
        ///     Расписание
        /// </summary>
        [Display(Description = "Расписание")]
        [DataMember(Name = "timesheet", EmitDefaultValue = false)]
        public virtual string Timesheet { get; set; }


        /// <summary>
        ///     Задания в лагере
        /// </summary>
        [InverseProperty("Task")]
        [DataMember(Name = "camps", EmitDefaultValue = false)]
        public virtual ICollection<CampTask> Camps { get; set; }

        /// <summary>
        ///     История по заданию
        /// </summary>
        [ForeignKey("Link")]
        [DataMember(Name = "linkId")]
        [Display(Description = "История по заданию")]
        public virtual long? LinkId { get; set; }


        /// <summary>
        ///     История по заданию
        /// </summary>
        [Display(Description = "История по заданию")]
        [DataMember(Name = "link", EmitDefaultValue = false)]
        public virtual Link Link { get; set; }

        /// <summary>
        ///     Период выполнения задания
        /// </summary>
        [ForeignKey("TaskPeriod")]
        [DataMember(Name = "taskPeriodId")]
        [Display(Description = "Период выполнения задания")]
        public virtual long? TaskPeriodId { get; set; }


        /// <summary>
        ///     Период выполнения задания
        /// </summary>
        [Display(Description = "Период выполнения задания")]
        [DataMember(Name = "taskPeriod", EmitDefaultValue = false)]
        public virtual TaskPeriod TaskPeriod { get; set; }

        /// <summary>
        ///     Статус задания
        /// </summary>
        [ForeignKey("State")]
        [DataMember(Name = "stateId")]
        [Display(Description = "Статус задания")]
        public virtual long? StateId { get; set; }


        /// <summary>
        ///     Статус задания
        /// </summary>
        [Display(Description = "Статус задания")]
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
