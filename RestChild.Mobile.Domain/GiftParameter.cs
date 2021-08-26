// File:    GiftParameter.cs
// Purpose: Definition of Class GiftParameter

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Mobile.Domain
{
    /// <summary>
    ///     Подарок. Параметр
    /// </summary>
    [Serializable]
    [DataContract(Name = "giftParameter")]
    public class GiftParameter : IEntityBase
    {
        /// <summary>
        ///     Наименование
        /// </summary>
        [Display(Description = "Наименование")]
        [MaxLength(1000, ErrorMessage = "\"Наименование\" не может быть больше 1000 символов")]
        [DataMember(Name = "name", EmitDefaultValue = false)]
        public virtual string Name { get; set; }

        /// <summary>
        ///     Количество
        /// </summary>
        [Display(Description = "Количество")]
        [Required(ErrorMessage = "\"Количество\" должно быть заполнено")]
        [DataMember(Name = "count", EmitDefaultValue = false)]
        public virtual long Count { get; set; }


        /// <summary>
        ///     Количество полученных подарков
        /// </summary>
        [InverseProperty("Gift")]
        [DataMember(Name = "reserved", EmitDefaultValue = false)]
        public virtual ICollection<GiftReserved> Reserved { get; set; }

        /// <summary>
        ///     Параметры подарка
        /// </summary>
        [ForeignKey("Gift")]
        [DataMember(Name = "giftId")]
        [Display(Description = "Параметры подарка")]
        public virtual long? GiftId { get; set; }


        /// <summary>
        ///     Параметры подарка
        /// </summary>
        [InverseProperty("GiftParameters")]
        [Display(Description = "Параметры подарка")]
        [DataMember(Name = "gift", EmitDefaultValue = false)]
        public virtual Gift Gift { get; set; }

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
