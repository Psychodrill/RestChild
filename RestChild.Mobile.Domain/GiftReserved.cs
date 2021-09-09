// File:    GiftReserved.cs
// Purpose: Definition of Class GiftReserved

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Mobile.Domain
{
    /// <summary>
    ///     Подарок. Параметр. Резерв
    /// </summary>
    [Serializable]
    [DataContract(Name = "giftReserved")]
    public class GiftReserved : IEntityBase
    {
        /// <summary>
        ///     Количество
        /// </summary>
        [Display(Description = "Количество")]
        [Required(ErrorMessage = "\"Количество\" должно быть заполнено")]
        [DataMember(Name = "count", EmitDefaultValue = false)]
        public virtual long Count { get; set; }

        /// <summary>
        ///     Стоимость
        /// </summary>
        [Display(Description = "Стоимость")]
        [Required(ErrorMessage = "\"Стоимость\" должно быть заполнено")]
        [DataMember(Name = "price", EmitDefaultValue = false)]
        public virtual decimal Price { get; set; }

        /// <summary>
        ///     Код подтверждения
        /// </summary>
        [Display(Description = "Код подтверждения")]
        [MaxLength(1000, ErrorMessage = "\"Код подтверждения\" не может быть больше 1000 символов")]
        [DataMember(Name = "aprovalCode", EmitDefaultValue = false)]
        public virtual string AprovalCode { get; set; }

        /// <summary>
        ///     Дата отправки кода
        /// </summary>
        [Display(Description = "Дата отправки кода")]
        [DataMember(Name = "codeSendDate", EmitDefaultValue = false)]
        public virtual DateTime? CodeSendDate { get; set; }

        /// <summary>
        ///     Телефон
        /// </summary>
        [Display(Description = "Телефон")]
        [MaxLength(1000, ErrorMessage = "\"Телефон\" не может быть больше 1000 символов")]
        [DataMember(Name = "phone", EmitDefaultValue = false)]
        public virtual string Phone { get; set; }

        /// <summary>
        ///     Рейтинг
        /// </summary>
        [Display(Description = "Рейтинг")]
        [DataMember(Name = "rating", EmitDefaultValue = false)]
        public virtual decimal? Rating { get; set; }


        /// <summary>
        ///     История по подарку в резерв
        /// </summary>
        [ForeignKey("Link")]
        [DataMember(Name = "linkId")]
        [Display(Description = "История по подарку в резерв")]
        public virtual long? LinkId { get; set; }


        /// <summary>
        ///     История по подарку в резерв
        /// </summary>
        [Display(Description = "История по подарку в резерв")]
        [DataMember(Name = "link", EmitDefaultValue = false)]
        public virtual Link Link { get; set; }

        /// <summary>
        ///     Количество полученных подарков
        /// </summary>
        [ForeignKey("Gift")]
        [DataMember(Name = "giftId")]
        [Display(Description = "Количество полученных подарков")]
        public virtual long? GiftId { get; set; }


        /// <summary>
        ///     Количество полученных подарков
        /// </summary>
        [InverseProperty("Reserved")]
        [Display(Description = "Количество полученных подарков")]
        [DataMember(Name = "gift", EmitDefaultValue = false)]
        public virtual GiftParameter Gift { get; set; }

        /// <summary>
        ///     Подарки
        /// </summary>
        [ForeignKey("Owner")]
        [DataMember(Name = "ownerId")]
        [Display(Description = "Подарки")]
        public virtual long? OwnerId { get; set; }


        /// <summary>
        ///     Подарки
        /// </summary>
        [InverseProperty("Gifts")]
        [Display(Description = "Подарки")]
        [DataMember(Name = "owner", EmitDefaultValue = false)]
        public virtual Account Owner { get; set; }

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

        /// <summary>
        /// Дата резервирования подарка
        /// </summary>
        [Display(Description = "Дата резервирования подарка")]
        [DataMember(Name = "dateReserved", EmitDefaultValue = false)]
        public virtual DateTime? DateReserved { get; set; }
    }
}
