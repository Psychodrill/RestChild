// File:    CampTask.cs
// Purpose: Definition of Class CampTask

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Mobile.Domain
{
    /// <summary>
    ///     Задание. В лагере
    /// </summary>
    [Serializable]
    [DataContract(Name = "campTask")]
    public class CampTask : IEntityBase
    {
        /// <summary>
        ///     Описание
        /// </summary>
        [Display(Description = "Описание")]
        [DataMember(Name = "description", EmitDefaultValue = false)]
        public virtual string Description { get; set; }


        /// <summary>
        ///     Связь задания в лагере с заездом
        /// </summary>
        [InverseProperty("CampTask")]
        [DataMember(Name = "boutTasks", EmitDefaultValue = false)]
        public virtual ICollection<BoutTask> BoutTasks { get; set; }

        /// <summary>
        ///     Задания в лагере
        /// </summary>
        [ForeignKey("Task")]
        [DataMember(Name = "taskId")]
        [Display(Description = "Задания в лагере")]
        public virtual long? TaskId { get; set; }


        /// <summary>
        ///     Задания в лагере
        /// </summary>
        [InverseProperty("Camps")]
        [Display(Description = "Задания в лагере")]
        [DataMember(Name = "task", EmitDefaultValue = false)]
        public virtual Task Task { get; set; }

        /// <summary>
        ///     Задания для лагеря
        /// </summary>
        [ForeignKey("Camp")]
        [DataMember(Name = "campId")]
        [Display(Description = "Задания для лагеря")]
        public virtual long? CampId { get; set; }


        /// <summary>
        ///     Задания для лагеря
        /// </summary>
        [InverseProperty("Tasks")]
        [Display(Description = "Задания для лагеря")]
        [DataMember(Name = "camp", EmitDefaultValue = false)]
        public virtual Camp Camp { get; set; }

        /// <summary>
        ///     Статус задания в лагере
        /// </summary>
        [ForeignKey("State")]
        [DataMember(Name = "stateId")]
        [Display(Description = "Статус задания в лагере")]
        public virtual long? StateId { get; set; }


        /// <summary>
        ///     Статус задания в лагере
        /// </summary>
        [Display(Description = "Статус задания в лагере")]
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
