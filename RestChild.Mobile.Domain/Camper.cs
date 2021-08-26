// File:    Camper.cs
// Purpose: Definition of Class Camper

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Mobile.Domain
{
    /// <summary>
    ///     Отдыхающий
    /// </summary>
    [Serializable]
    [DataContract(Name = "camper")]
    public class Camper : IEntityBase
    {
        /// <summary>
        ///     Внешний ИД ребёнка АИС ДО
        /// </summary>
        [Display(Description = "Внешний ИД ребёнка АИС ДО")]
        [DataMember(Name = "childExtCampUid", EmitDefaultValue = false)]
        public virtual long? ChildExtCampUid { get; set; }

        /// <summary>
        ///     Внешний ИД ребёнка АИС МГТ
        /// </summary>
        [Display(Description = "Внешний ИД ребёнка АИС МГТ")]
        [DataMember(Name = "childExtMgtUid", EmitDefaultValue = false)]
        public virtual long? ChildExtMgtUid { get; set; }

        /// <summary>
        ///     Внешний ИД сопровождающего АИС МГТ
        /// </summary>
        [Display(Description = "Внешний ИД сопровождающего АИС МГТ")]
        [DataMember(Name = "attendantExtCampUid", EmitDefaultValue = false)]
        public virtual long? AttendantExtCampUid { get; set; }

        /// <summary>
        ///     Внешний ИД сопровождающего АИС ДО
        /// </summary>
        [Display(Description = "Внешний ИД сопровождающего АИС ДО")]
        [DataMember(Name = "attendantExtMgrUid", EmitDefaultValue = false)]
        public virtual long? AttendantExtMgrUid { get; set; }

        /// <summary>
        ///     Наименование
        /// </summary>
        [Display(Description = "Наименование")]
        [MaxLength(1000, ErrorMessage = "\"Наименование\" не может быть больше 1000 символов")]
        [DataMember(Name = "name", EmitDefaultValue = false)]
        public virtual string Name { get; set; }

        /// <summary>
        ///     Пол мужской
        /// </summary>
        [Display(Description = "Пол мужской")]
        [DataMember(Name = "male", EmitDefaultValue = false)]
        public virtual bool? Male { get; set; }

        /// <summary>
        ///     Дата рождения
        /// </summary>
        [Display(Description = "Дата рождения")]
        [DataMember(Name = "dateOfBirth", EmitDefaultValue = false)]
        public virtual DateTime? DateOfBirth { get; set; }

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
        ///     Количество заданий
        /// </summary>
        [Display(Description = "Количество заданий")]
        [Required(ErrorMessage = "\"Количество заданий\" должно быть заполнено")]
        [DataMember(Name = "taskCount", EmitDefaultValue = false)]
        public virtual int TaskCount { get; set; }

        /// <summary>
        ///     Количество баллов
        /// </summary>
        [Display(Description = "Количество баллов")]
        [Required(ErrorMessage = "\"Количество баллов\" должно быть заполнено")]
        [DataMember(Name = "points", EmitDefaultValue = false)]
        public virtual decimal Points { get; set; }


        /// <summary>
        ///     Выполненные задания
        /// </summary>
        [InverseProperty("Camper")]
        [DataMember(Name = "tasks", EmitDefaultValue = false)]
        public virtual ICollection<CamperTask> Tasks { get; set; }

        /// <summary>
        ///     Отдыхающие в заезде
        /// </summary>
        [ForeignKey("Bout")]
        [DataMember(Name = "boutId")]
        [Display(Description = "Отдыхающие в заезде")]
        public virtual long? BoutId { get; set; }


        /// <summary>
        ///     Отдыхающие в заезде
        /// </summary>
        [InverseProperty("Campers")]
        [Display(Description = "Отдыхающие в заезде")]
        [DataMember(Name = "bout", EmitDefaultValue = false)]
        public virtual Bout Bout { get; set; }

        /// <summary>
        ///     Отдыхающие в отряде
        /// </summary>
        [ForeignKey("Party")]
        [DataMember(Name = "partyId")]
        [Display(Description = "Отдыхающие в отряде")]
        public virtual long? PartyId { get; set; }


        /// <summary>
        ///     Отдыхающие в отряде
        /// </summary>
        [InverseProperty("Campers")]
        [Display(Description = "Отдыхающие в отряде")]
        [DataMember(Name = "party", EmitDefaultValue = false)]
        public virtual Party Party { get; set; }

        /// <summary>
        ///     Указание на конкретного ребёнка в заезде
        /// </summary>
        [ForeignKey("Account")]
        [DataMember(Name = "accountId")]
        [Display(Description = "Указание на конкретного ребёнка в заезде")]
        public virtual long? AccountId { get; set; }


        /// <summary>
        ///     Указание на конкретного ребёнка в заезде
        /// </summary>
        [InverseProperty("Campers")]
        [Display(Description = "Указание на конкретного ребёнка в заезде")]
        [DataMember(Name = "account", EmitDefaultValue = false)]
        public virtual Account Account { get; set; }

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
