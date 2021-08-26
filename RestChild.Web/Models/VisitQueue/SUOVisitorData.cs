using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestChild.Web.Models.VisitQueue
{
    /// <summary>
    ///     Данные посетителя для СУО
    /// </summary>
    public class SUOVisitorData
    {
        /// <summary>
        ///     ФИО заявителя
        /// </summary>
        public string FIO { get; set; }

        /// <summary>
        ///     СНИЛС
        /// </summary>
        public string SNILS { get; set; }

        /// <summary>
        ///     Телефон
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        ///     E-mail
        /// </summary>
        public string EMail { get; set; }

        /// <summary>
        ///     Цель обращения
        /// </summary>
        public string VisitTarget { get; set; }

        /// <summary>
        ///     Идентификатор цели обращения
        /// </summary>
        public long? VisitTargetId { get; set; }

        /// <summary>
        ///     Кол-во слотов
        /// </summary>
        public int SlotCount { get; set; }

        /// <summary>
        ///     Дата и время приема
        /// </summary>
        public DateTime VisitTime { get; set; }
    }
}
