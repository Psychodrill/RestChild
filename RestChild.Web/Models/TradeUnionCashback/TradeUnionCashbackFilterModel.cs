using System.Collections.Generic;
using RestChild.Domain;

namespace RestChild.Web.Models.TradeUnionCashback
{
    /// <summary>
    ///     Модель для поиска списков претендентов на кэшбек
    /// </summary>
    public class TradeUnionCashbackFilterModel : BaseFilterModel<TradeUnionList>
    {
        /// <summary>
        ///     Идентификатор организации
        /// </summary>
        public long? OrganizationId { get; set; }

        /// <summary>
        ///     Организации
        /// </summary>
        public IDictionary<long, string> Organizations { get; set; }

        /// <summary>
        ///     Года компании
        /// </summary>
        public IDictionary<long, string> YearOfRests { get; set; }

        /// <summary>
        ///     Год компании
        /// </summary>

        public long? YearOfRestId { get; set; }

        /// <summary>
        ///     Смены
        /// </summary>
        public IDictionary<long, string> TimeOfRests { get; set; }

        /// <summary>
        ///     Смена
        /// </summary>
        public long? TimeOfRestId { get; set; }
    }
}
