using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestChild.Web.Models.VisitQueue
{
    /// <summary>
    ///     Данные для СУО
    /// </summary>
    public class VisitSUOResult
    {
        /// <summary>
        ///     Тип результата обращения от СУО
        /// </summary>
        public VisitSUOResultType ResultType { get; set; }

        /// <summary>
        ///     Данные посетителя
        /// </summary>
        public SUOVisitorData VisitorData { get; set; }
    }
}
