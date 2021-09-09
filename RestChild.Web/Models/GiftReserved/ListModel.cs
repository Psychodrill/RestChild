using System;
using System.Collections.Generic;
using RestChild.Extensions.Filter;
using RestChild.Mobile.Domain;

namespace RestChild.Web.Models.GiftReserved
{
    public class ListModel
    {
        /// <summary>
        ///     номер страницы
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        ///     ФИО ребёнка
        /// </summary>
        public string Child { get; set; }

        /// <summary>
        ///     Название подарка
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Параметр подарка
        /// </summary>
        public string Parameter { get; set; }

        /// <summary>
        ///     Статус подарка
        /// </summary>
        public long? StatusId { get; set; }

        /// <summary>
        ///     Статусы
        /// </summary>
        public State[] Statuses { get; set; }

        /// <summary>
        ///     Стоимость от
        /// </summary>
        public decimal? PriceFrom { get; set; }

        /// <summary>
        ///     Стоимость до
        /// </summary>
        public decimal? PriceTo { get; set; }

        /// <summary>
        ///     Зарезервировано с
        /// </summary>
        public DateTime? ReservedFrom { get; set; }

        /// <summary>
        ///     Зарезервировано по
        /// </summary>
        public DateTime? ReservedTo { get; set; }

        /// <summary>
        ///     результаты
        /// </summary>
        public CommonPagedList<Mobile.Domain.GiftReserved> Result { get; set; }

        /// <summary>
        ///     Лагерь/смена
        /// </summary>
        public long? BoutId { get; set; }

        /// <summary>
        ///     Лагерь/смена (наименование)
        /// </summary>
        public string BName { get; set; }

    }
}
