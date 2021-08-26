// File:    CamperTask.cs
// Purpose: Definition of Class CamperTask

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Mobile.Domain
{
    /// <summary>
    ///     Отдыхающий. Выполнение задания
    /// </summary>
    [Serializable]
    [DataContract(Name = "camperTask")]
    public class CamperTask : IEntityBase
    {
        /// <summary>
        ///     Комментарий
        /// </summary>
        [Display(Description = "Комментарий")]
        [DataMember(Name = "commentary", EmitDefaultValue = false)]
        public virtual string Commentary { get; set; }

        /// <summary>
        ///     Стоимость
        /// </summary>
        [Display(Description = "Стоимость")]
        [Required(ErrorMessage = "\"Стоимость\" должно быть заполнено")]
        [DataMember(Name = "price", EmitDefaultValue = false)]
        public virtual decimal Price { get; set; }

        /// <summary>
        ///     Ответ
        /// </summary>
        [Display(Description = "Ответ")]
        [DataMember(Name = "answer", EmitDefaultValue = false)]
        public virtual string Answer { get; set; }

        /// <summary>
        ///     Версия записи
        /// </summary>
        [Display(Description = "Версия записи")]
        [DataMember(Name = "rowVersion", EmitDefaultValue = false)]
        public virtual byte[] RowVersion { get; set; }

        /// <summary>
        ///     Дата начала доступности задания
        /// </summary>
        [Display(Description = "Дата начала доступности задания")]
        [DataMember(Name = "availabilityStart", EmitDefaultValue = false)]
        public virtual DateTime? AvailabilityStart { get; set; }

        /// <summary>
        ///     Дата окончания доступности задания
        /// </summary>
        [Display(Description = "Дата окончания доступности задания")]
        [DataMember(Name = "availabilityFinish", EmitDefaultValue = false)]
        public virtual DateTime? AvailabilityFinish { get; set; }

        /// <summary>
        ///     Срок для отказа
        /// </summary>
        [Display(Description = "Срок для отказа")]
        [DataMember(Name = "durationToDeclaine", EmitDefaultValue = false)]
        public virtual decimal? DurationToDeclaine { get; set; }

        /// <summary>
        ///     Дата задания
        /// </summary>
        [Display(Description = "Дата задания")]
        [DataMember(Name = "taskDate", EmitDefaultValue = false)]
        public virtual DateTime? TaskDate { get; set; }

        /// <summary>
        ///     Дата окончания задания
        /// </summary>
        [Display(Description = "Дата окончания задания")]
        [DataMember(Name = "taskFinishDate", EmitDefaultValue = false)]
        public virtual DateTime? TaskFinishDate { get; set; }

        /// <summary>
        ///     Дата принятия задания
        /// </summary>
        [Display(Description = "Дата принятия задания")]
        [DataMember(Name = "acceptDate", EmitDefaultValue = false)]
        public virtual DateTime? AcceptDate { get; set; }

        /// <summary>
        ///     Дата выполнения
        /// </summary>
        [Display(Description = "Дата выполнения")]
        [DataMember(Name = "compliteDate", EmitDefaultValue = false)]
        public virtual DateTime? CompliteDate { get; set; }

        /// <summary>
        ///     Дата подтверждения задания
        /// </summary>
        [Display(Description = "Дата подтверждения задания")]
        [DataMember(Name = "approveDate", EmitDefaultValue = false)]
        public virtual DateTime? ApproveDate { get; set; }

        /// <summary>
        ///     Дата отказа от выполнения
        /// </summary>
        [Display(Description = "Дата отказа от выполнения")]
        [DataMember(Name = "refuseDate", EmitDefaultValue = false)]
        public virtual DateTime? RefuseDate { get; set; }

        /// <summary>
        ///     Рейтинг
        /// </summary>
        [Display(Description = "Рейтинг")]
        [DataMember(Name = "rating", EmitDefaultValue = false)]
        public virtual decimal? Rating { get; set; }


        /// <summary>
        ///     Выполненные задания
        /// </summary>
        [ForeignKey("Camper")]
        [DataMember(Name = "camperId")]
        [Display(Description = "Выполненные задания")]
        public virtual long? CamperId { get; set; }


        /// <summary>
        ///     Выполненные задания
        /// </summary>
        [InverseProperty("Tasks")]
        [Display(Description = "Выполненные задания")]
        [DataMember(Name = "camper", EmitDefaultValue = false)]
        public virtual Camper Camper { get; set; }

        /// <summary>
        ///     История по заданию в лагере
        /// </summary>
        [ForeignKey("Link")]
        [DataMember(Name = "linkId")]
        [Display(Description = "История по заданию в лагере")]
        public virtual long? LinkId { get; set; }


        /// <summary>
        ///     История по заданию в лагере
        /// </summary>
        [Display(Description = "История по заданию в лагере")]
        [DataMember(Name = "link", EmitDefaultValue = false)]
        public virtual Link Link { get; set; }

        /// <summary>
        ///     Подтверждающий
        /// </summary>
        [ForeignKey("Counselor")]
        [DataMember(Name = "counselorId")]
        [Display(Description = "Подтверждающий")]
        public virtual long? CounselorId { get; set; }


        /// <summary>
        ///     Подтверждающий
        /// </summary>
        [Display(Description = "Подтверждающий")]
        [DataMember(Name = "counselor", EmitDefaultValue = false)]
        public virtual Personal Counselor { get; set; }

        /// <summary>
        ///     Связь задания с его выполнением
        /// </summary>
        [ForeignKey("BoutTask")]
        [DataMember(Name = "boutTaskId")]
        [Display(Description = "Связь задания с его выполнением")]
        public virtual long? BoutTaskId { get; set; }


        /// <summary>
        ///     Связь задания с его выполнением
        /// </summary>
        [InverseProperty("CamperTasks")]
        [Display(Description = "Связь задания с его выполнением")]
        [DataMember(Name = "boutTask", EmitDefaultValue = false)]
        public virtual BoutTask BoutTask { get; set; }

        /// <summary>
        ///     Статус выполнения задания
        /// </summary>
        [ForeignKey("State")]
        [DataMember(Name = "stateId")]
        [Display(Description = "Статус выполнения задания")]
        public virtual long? StateId { get; set; }


        /// <summary>
        ///     Статус выполнения задания
        /// </summary>
        [Display(Description = "Статус выполнения задания")]
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
