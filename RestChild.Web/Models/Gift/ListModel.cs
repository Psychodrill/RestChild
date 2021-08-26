using System.Collections.Generic;
using MorpherDictionary;
using RestChild.Extensions.Filter;
using RestChild.Mobile.Domain;

namespace RestChild.Web.Models.Gift
{
    /// <summary>
    ///     список подарков
    /// </summary>
    public class ListModel
    {
        /// <summary>
        ///     номер страницы
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        ///     Название подарка
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Статус подарка
        /// </summary>
        public long? StateId { get; set; }

        /// <summary>
        ///     Статусы
        /// </summary>
        public State[] States { get; set; }

        /// <summary>
        ///     Стоимость от
        /// </summary>
        public decimal? PriceFrom { get; set; }

        /// <summary>
        ///     Стоимость до
        /// </summary>
        public decimal? PriceTo { get; set; }

        /// <summary>
        ///     Кол-во от
        /// </summary>
        public decimal? CountFrom { get; set; }

        /// <summary>
        ///     Кол-во до
        /// </summary>
        public decimal? CountTo { get; set; }

        /// <summary>
        /// количество остатков
        /// </summary>
        public Dictionary<long, long> Counts { get; set; }

        /// <summary>
        ///     результаты
        /// </summary>
        public CommonPagedList<Mobile.Domain.Gift> Result { get; set; }
    }
}
