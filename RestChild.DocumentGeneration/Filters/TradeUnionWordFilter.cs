using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestChild.DocumentGeneration.Filters
{
    /// <summary>
    ///     Фильтр для генерации Word в профсоюзных списках
    /// </summary>
    public struct TradeUnionWordFilter
    {
        /// <summary>
        ///     Идентификатор профсоюзного списка
        /// </summary>
        public long TradeUnionId { get; set; }

        /// <summary>
        ///     Только заехавшие дети
        /// </summary>
        public bool? CameChildren { get; set; }
    }
}
