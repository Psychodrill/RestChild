// File:    BoutPersonal.cs
// Purpose: Definition of Class BoutPersonal

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Mobile.Domain
{
    /// <summary>
    ///     Сотрудник. В заезде
    /// </summary>
    [Serializable]
    [DataContract(Name = "boutPersonal")]
    public class BoutPersonal : IEntityBase
    {
        /// <summary>
        ///     Отряд
        /// </summary>
        [ForeignKey("Party")]
        [DataMember(Name = "partyId")]
        [Display(Description = "Отряд")]
        public virtual long? PartyId { get; set; }


        /// <summary>
        ///     Отряд
        /// </summary>
        [InverseProperty("Peronals")]
        [Display(Description = "Отряд")]
        [DataMember(Name = "party", EmitDefaultValue = false)]
        public virtual Party Party { get; set; }

        /// <summary>
        ///     Персонал
        /// </summary>
        [ForeignKey("Personal")]
        [DataMember(Name = "personalId")]
        [Display(Description = "Персонал")]
        public virtual long? PersonalId { get; set; }


        /// <summary>
        ///     Персонал
        /// </summary>
        [InverseProperty("Bouts")]
        [Display(Description = "Персонал")]
        [DataMember(Name = "personal", EmitDefaultValue = false)]
        public virtual Personal Personal { get; set; }

        /// <summary>
        ///     Сотрудники
        /// </summary>
        [ForeignKey("Bout")]
        [DataMember(Name = "boutId")]
        [Display(Description = "Сотрудники")]
        public virtual long? BoutId { get; set; }


        /// <summary>
        ///     Сотрудники
        /// </summary>
        [InverseProperty("Personals")]
        [Display(Description = "Сотрудники")]
        [DataMember(Name = "bout", EmitDefaultValue = false)]
        public virtual Bout Bout { get; set; }

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
