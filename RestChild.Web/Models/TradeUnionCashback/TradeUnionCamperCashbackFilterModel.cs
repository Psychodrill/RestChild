using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RestChild.Domain;
using RestChild.Extensions.Filter;

namespace RestChild.Web.Models.TradeUnionCashback
{
    public class TradeUnionCamperCashbackFilterModel : BaseFilterModel<TradeUnionCamper>
    {
        /// <summary>
        ///     ФИО ребенка
        /// </summary>
        public string FIO { get; set; }

        /// <summary>
        ///     СНИЛС ребенка
        /// </summary>
        public string SNILS { get; set; }

        /// <summary>
        ///     Серия документа
        /// </summary>
        public string DocumentSeria { get; set; }

        /// <summary>
        ///     Номер документа
        /// </summary>
        public string DocumentNumber { get; set; }

        /// <summary>
        ///     Лагерь
        /// </summary>
        public long? CampId { get; set; }

        /// <summary>
        ///     Смена
        /// </summary>
        public long? ShiftId { get; set; }

        /// <summary>
        ///     ФИО законного представителя
        /// </summary>
        public string RepresentativeFIO { get; set; }

        /// <summary>
        ///     СНИЛС законного представителя
        /// </summary>
        public string RepresentativeSNILS { get; set; }

        /// <summary>
        ///     Условие того запрашивался ли кэшбэк
        /// </summary>
        public bool CashbackRequested { get; set; }

        /// <summary>
        ///     Лагери
        /// </summary>
        public IDictionary<long, string> Camps { get; set; }

        /// <summary>
        ///     Смены
        /// </summary>
        public IDictionary<long, string> Shifts { get; set; }
    }
}
