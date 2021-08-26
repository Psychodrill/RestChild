// File:    Gift.cs
// Purpose: Definition of Class Gift

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using RestChild.Comon;

namespace RestChild.Mobile.Domain
{
    /// <summary>
    ///     Подарок
    /// </summary>
    [Serializable]
    [DataContract(Name = "gift")]
    public class Gift : IEntityBase
    {
        /// <summary>
        ///     Уникальный идентификатор
        /// </summary>
        [Display(Description = "Уникальный идентификатор")]
        [Required(ErrorMessage = "\"Уникальный идентификатор\" должно быть заполнено")]
        [Key]
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public virtual long Id { get; set; }

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
        ///     Дата начала использования
        /// </summary>
        [Display(Description = "Дата начала использования")]
        [DataMember(Name = "dateStartUsing", EmitDefaultValue = false)]
        public virtual DateTime? DateStartUsing { get; set; }

        /// <summary>
        ///     Стоимость в рублях
        /// </summary>
        [Display(Description = "Стоимость в рублях")]
        [DataMember(Name = "priceRub", EmitDefaultValue = false)]
        public virtual decimal? PriceRub { get; set; }


        /// <summary>
        ///     Параметры подарка
        /// </summary>
        [InverseProperty("Gift")]
        [DataMember(Name = "giftParameters", EmitDefaultValue = false)]
        public virtual ICollection<GiftParameter> GiftParameters { get; set; }

        /// <summary>
        ///     История по подарку общая
        /// </summary>
        [ForeignKey("Link")]
        [DataMember(Name = "linkId")]
        [Display(Description = "История по подарку общая")]
        public virtual long? LinkId { get; set; }


        /// <summary>
        ///     История по подарку общая
        /// </summary>
        [Display(Description = "История по подарку общая")]
        [DataMember(Name = "link", EmitDefaultValue = false)]
        public virtual Link Link { get; set; }

        /// <summary>
        ///     Статус подарка
        /// </summary>
        [ForeignKey("State")]
        [DataMember(Name = "stateId")]
        [Display(Description = "Статус подарка")]
        public virtual long? StateId { get; set; }


        /// <summary>
        ///     Статус подарка
        /// </summary>
        [Display(Description = "Статус подарка")]
        [DataMember(Name = "state", EmitDefaultValue = false)]
        public virtual State State { get; set; }

        /// <summary>
        ///     Название для магазина
        /// </summary>
        [Display(Description = "Название для магазина")]
        [MaxLength(1000, ErrorMessage = "\"Название для магазина\" не может быть больше 1000 символов")]
        [DataMember(Name = "nameForShop", EmitDefaultValue = false)]
        public virtual string NameForShop { get; set; }

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
