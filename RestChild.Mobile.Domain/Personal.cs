// File:    Personal.cs
// Purpose: Definition of Class Personal

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Mobile.Domain
{
    /// <summary>
    ///     Персонал
    /// </summary>
    [Serializable]
    [DataContract(Name = "personal")]
    public class Personal : IEntityBase
    {
        /// <summary>
        ///     Внешний ИД Вожатого
        /// </summary>
        [Display(Description = "Внешний ИД Вожатого")]
        [DataMember(Name = "externalUidCounselor", EmitDefaultValue = false)]
        public virtual long? ExternalUidCounselor { get; set; }

        /// <summary>
        ///     Внешний ИД администратора
        /// </summary>
        [Display(Description = "Внешний ИД администратора")]
        [DataMember(Name = "externalUidAdministrator", EmitDefaultValue = false)]
        public virtual long? ExternalUidAdministrator { get; set; }

        /// <summary>
        ///     Внешний ИД
        /// </summary>
        [Display(Description = "Внешний ИД")]
        [DataMember(Name = "externalUid", EmitDefaultValue = false)]
        public virtual long? ExternalUid { get; set; }

        /// <summary>
        ///     Наименование
        /// </summary>
        [Display(Description = "Наименование")]
        [MaxLength(1000, ErrorMessage = "\"Наименование\" не может быть больше 1000 символов")]
        [DataMember(Name = "name", EmitDefaultValue = false)]
        public virtual string Name { get; set; }

        /// <summary>
        ///     Электронная почта организации
        /// </summary>
        [Display(Description = "Электронная почта организации")]
        [DataMember(Name = "email", EmitDefaultValue = false)]
        public virtual string Email { get; set; }

        /// <summary>
        ///     Пол мужской
        /// </summary>
        [Display(Description = "Пол мужской")]
        [Required(ErrorMessage = "\"Пол мужской\" должно быть заполнено")]
        [DataMember(Name = "male", EmitDefaultValue = false)]
        public virtual bool Male { get; set; }

        /// <summary>
        ///     Телефон
        /// </summary>
        [Display(Description = "Телефон")]
        [MaxLength(1000, ErrorMessage = "\"Телефон\" не может быть больше 1000 символов")]
        [DataMember(Name = "phone", EmitDefaultValue = false)]
        public virtual string Phone { get; set; }


        /// <summary>
        ///     Персонал
        /// </summary>
        [InverseProperty("Personal")]
        [DataMember(Name = "bouts", EmitDefaultValue = false)]
        public virtual ICollection<BoutPersonal> Bouts { get; set; }

        /// <summary>
        ///     Вид персонала
        /// </summary>
        [ForeignKey("PersonalType")]
        [DataMember(Name = "personalTypeId")]
        [Display(Description = "Вид персонала")]
        public virtual long? PersonalTypeId { get; set; }


        /// <summary>
        ///     Вид персонала
        /// </summary>
        [Display(Description = "Вид персонала")]
        [DataMember(Name = "personalType", EmitDefaultValue = false)]
        public virtual PersonalType PersonalType { get; set; }

        /// <summary>
        ///     Отряд для персонала
        /// </summary>
        [ForeignKey("Party")]
        [DataMember(Name = "partyId")]
        [Display(Description = "Отряд для персонала")]
        public virtual long? PartyId { get; set; }


        /// <summary>
        ///     Отряд для персонала
        /// </summary>
        [Display(Description = "Отряд для персонала")]
        [DataMember(Name = "party", EmitDefaultValue = false)]
        public virtual Party Party { get; set; }

        /// <summary>
        ///     Связка с вожатым
        /// </summary>
        [ForeignKey("Account")]
        [DataMember(Name = "accountId")]
        [Display(Description = "Связка с вожатым")]
        public virtual long? AccountId { get; set; }


        /// <summary>
        ///     Связка с вожатым
        /// </summary>
        [InverseProperty("Counselors")]
        [Display(Description = "Связка с вожатым")]
        [DataMember(Name = "account", EmitDefaultValue = false)]
        public virtual Account Account { get; set; }

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
