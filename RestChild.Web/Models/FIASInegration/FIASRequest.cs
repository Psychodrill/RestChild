using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace RestChild.Web.Models.FIASInegration
{
    /// <summary>
    /// класс запроса в ФИАС
    /// </summary>
    [DataContract]
    internal struct FIASRequest
    {
        /// <summary>
        /// текст запроса
        /// </summary>
        [DataMember(Name = "query")]
        public string Query { get; set; }

        /// <summary>
        /// кол-во возвращаемых результатов
        /// </summary>
        [DataMember(Name = "count")]
        public int Count { get; set; }

        /// <summary>
        /// Список ограничений
        /// </summary>
        [DataMember(Name = "locations")]
        public FIASRequestLocation[] Locations { get; set; }

        /// <summary>
        /// Приоритетный город поиска
        /// </summary>
        [DataMember(Name = "locations_boost")]
        public FIASRequestLocation[] LocationsBoost { get; set; }
    }
}
