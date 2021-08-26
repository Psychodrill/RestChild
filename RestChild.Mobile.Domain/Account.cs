// File:    Account.cs
// Purpose: Definition of Class Account

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Mobile.Domain
{
    /// <summary>
    ///     Пользователь
    /// </summary>
    [Serializable]
    [DataContract(Name = "account")]
    public class Account : IEntityBase
    {
        /// <summary>
        ///     Код пользователя
        /// </summary>
        [Display(Description = "Код пользователя")]
        [MaxLength(1000, ErrorMessage = "\"Код пользователя\" не может быть больше 1000 символов")]
        [DataMember(Name = "accountKey", EmitDefaultValue = false)]
        public virtual string AccountKey { get; set; }

        /// <summary>
        ///     Наименование
        /// </summary>
        [Display(Description = "Наименование")]
        [MaxLength(1000, ErrorMessage = "\"Наименование\" не может быть больше 1000 символов")]
        [DataMember(Name = "name", EmitDefaultValue = false)]
        public virtual string Name { get; set; }

        /// <summary>
        ///     Дата рождения
        /// </summary>
        [Display(Description = "Дата рождения")]
        [DataMember(Name = "dateOfBirth", EmitDefaultValue = false)]
        public virtual DateTime? DateOfBirth { get; set; }

        /// <summary>
        ///     Пол мужской
        /// </summary>
        [Display(Description = "Пол мужской")]
        [Required(ErrorMessage = "\"Пол мужской\" должно быть заполнено")]
        [DataMember(Name = "male", EmitDefaultValue = false)]
        public virtual bool Male { get; set; }

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
        ///     Пароль
        /// </summary>
        [Display(Description = "Пароль")]
        [MaxLength(1000, ErrorMessage = "\"Пароль\" не может быть больше 1000 символов")]
        [DataMember(Name = "passwordHash", EmitDefaultValue = false)]
        public virtual string PasswordHash { get; set; }

        /// <summary>
        ///     Соль
        /// </summary>
        [Display(Description = "Соль")]
        [MaxLength(1000, ErrorMessage = "\"Соль\" не может быть больше 1000 символов")]
        [DataMember(Name = "salt", EmitDefaultValue = false)]
        public virtual string Salt { get; set; }

        /// <summary>
        ///     Количество баллов
        /// </summary>
        [Display(Description = "Количество баллов")]
        [Required(ErrorMessage = "\"Количество баллов\" должно быть заполнено")]
        [DataMember(Name = "points", EmitDefaultValue = false)]
        public virtual decimal Points { get; set; }

        /// <summary>
        ///     Количество заданий
        /// </summary>
        [Display(Description = "Количество заданий")]
        [Required(ErrorMessage = "\"Количество заданий\" должно быть заполнено")]
        [DataMember(Name = "taskCount", EmitDefaultValue = false)]
        public virtual int TaskCount { get; set; }

        /// <summary>
        ///     Количество баллов на счете
        /// </summary>
        [Display(Description = "Количество баллов на счете")]
        [Required(ErrorMessage = "\"Количество баллов на счете\" должно быть заполнено")]
        [DataMember(Name = "pointsOnAccount", EmitDefaultValue = false)]
        public virtual decimal PointsOnAccount { get; set; }

        /// <summary>
        ///     Заблокирован
        /// </summary>
        [Display(Description = "Заблокирован")]
        [DataMember(Name = "isBlocked", EmitDefaultValue = false)]
        public virtual bool? IsBlocked { get; set; }

        /// <summary>
        ///     Удалено
        /// </summary>
        [Display(Description = "Удалено")]
        [DataMember(Name = "isDeleted", EmitDefaultValue = false)]
        public virtual bool? IsDeleted { get; set; }

        /// <summary>
        ///     Пароль
        /// </summary>
        [Display(Description = "Пароль")]
        [MaxLength(1000, ErrorMessage = "\"Пароль\" не может быть больше 1000 символов")]
        [DataMember(Name = "passwordHashNew", EmitDefaultValue = false)]
        public virtual string PasswordHashNew { get; set; }

        /// <summary>
        ///     Соль
        /// </summary>
        [Display(Description = "Соль")]
        [MaxLength(1000, ErrorMessage = "\"Соль\" не может быть больше 1000 символов")]
        [DataMember(Name = "saltNew", EmitDefaultValue = false)]
        public virtual string SaltNew { get; set; }


        /// <summary>
        ///     Подарки
        /// </summary>
        [InverseProperty("Owner")]
        [DataMember(Name = "gifts", EmitDefaultValue = false)]
        public virtual ICollection<GiftReserved> Gifts { get; set; }

        /// <summary>
        ///     Связка с вожатым
        /// </summary>
        [InverseProperty("Account")]
        [DataMember(Name = "counselors", EmitDefaultValue = false)]
        public virtual ICollection<Personal> Counselors { get; set; }

        /// <summary>
        ///     Указание на конкретного ребёнка в заезде
        /// </summary>
        [InverseProperty("Account")]
        [DataMember(Name = "campers", EmitDefaultValue = false)]
        public virtual ICollection<Camper> Campers { get; set; }

        /// <summary>
        ///     История действий по пользователю
        /// </summary>
        [ForeignKey("Link")]
        [DataMember(Name = "linkId")]
        [Display(Description = "История действий по пользователю")]
        public virtual long? LinkId { get; set; }


        /// <summary>
        ///     История действий по пользователю
        /// </summary>
        [Display(Description = "История действий по пользователю")]
        [DataMember(Name = "link", EmitDefaultValue = false)]
        public virtual Link Link { get; set; }

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
