// File:    Notification.cs
// Purpose: Definition of Class Notification

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Mobile.Domain
{
    /// <summary>
    ///     Уведомление
    /// </summary>
    [Serializable]
    [DataContract(Name = "notification")]
    public class Notification : IEntityBase
    {
        /// <summary>
        ///     Описание
        /// </summary>
        [Display(Description = "Описание")]
        [DataMember(Name = "description", EmitDefaultValue = false)]
        public virtual string Description { get; set; }

        /// <summary>
        ///     Ответ
        /// </summary>
        [Display(Description = "Ответ")]
        [DataMember(Name = "answer", EmitDefaultValue = false)]
        public virtual string Answer { get; set; }

        /// <summary>
        ///     Дата вопроса
        /// </summary>
        [Display(Description = "Дата вопроса")]
        [DataMember(Name = "dateWhenQuestionAsked", EmitDefaultValue = false)]
        public virtual DateTime? DateWhenQuestionAsked { get; set; }

        /// <summary>
        ///     Дата ответа
        /// </summary>
        [Display(Description = "Дата ответа")]
        [DataMember(Name = "dateWhenAnswerReceived", EmitDefaultValue = false)]
        public virtual DateTime? DateWhenAnswerReceived { get; set; }


        /// <summary>
        ///     Задание отдыхающего
        /// </summary>
        [ForeignKey("CamperTask")]
        [DataMember(Name = "camperTaskId")]
        [Display(Description = "Задание отдыхающего")]
        public virtual long? CamperTaskId { get; set; }


        /// <summary>
        ///     Задание отдыхающего
        /// </summary>
        [Display(Description = "Задание отдыхающего")]
        [DataMember(Name = "camperTask", EmitDefaultValue = false)]
        public virtual CamperTask CamperTask { get; set; }

        /// <summary>
        ///     История уведомления
        /// </summary>
        [ForeignKey("Link")]
        [DataMember(Name = "linkId")]
        [Display(Description = "История уведомления")]
        public virtual long? LinkId { get; set; }


        /// <summary>
        ///     История уведомления
        /// </summary>
        [Display(Description = "История уведомления")]
        [DataMember(Name = "link", EmitDefaultValue = false)]
        public virtual Link Link { get; set; }

        /// <summary>
        ///     Кто ответил
        /// </summary>
        [ForeignKey("WhoAnswered")]
        [DataMember(Name = "whoAnsweredId")]
        [Display(Description = "Кто ответил")]
        public virtual long? WhoAnsweredId { get; set; }


        /// <summary>
        ///     Кто ответил
        /// </summary>
        [Display(Description = "Кто ответил")]
        [DataMember(Name = "whoAnswered", EmitDefaultValue = false)]
        public virtual Account WhoAnswered { get; set; }

        /// <summary>
        ///     Кто спросил
        /// </summary>
        [ForeignKey("WhoAsk")]
        [DataMember(Name = "whoAskId")]
        [Display(Description = "Кто спросил")]
        public virtual long? WhoAskId { get; set; }


        /// <summary>
        ///     Кто спросил
        /// </summary>
        [Display(Description = "Кто спросил")]
        [DataMember(Name = "whoAsk", EmitDefaultValue = false)]
        public virtual Account WhoAsk { get; set; }

        /// <summary>
        ///     Статус уведомления
        /// </summary>
        [ForeignKey("State")]
        [DataMember(Name = "stateId")]
        [Display(Description = "Статус уведомления")]
        public virtual long? StateId { get; set; }


        /// <summary>
        ///     Статус уведомления
        /// </summary>
        [Display(Description = "Статус уведомления")]
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
