using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestChild.Web.Models.VisitQueue
{
    /// <summary>
    ///     фильтр сетки записи
    /// </summary>
    public class BookingVisitGridViewFilter
    {
        /// <summary>
        ///     цель визита
        /// </summary>
        public long SelectedTarget { get; set; }

        /// <summary>
        ///     кол-во слотов
        /// </summary>
        public int SlotsCount { get; set; }

        /// <summary>
        ///     дата визита
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        ///     коллекция возможный времён визита
        /// </summary>
        public IDictionary<TimeSpan, string> Times { get; set; }

        /// <summary>
        ///     Время
        /// </summary>
        public TimeSpan Time { get; set; }

        /// <summary>
        ///     Выбранное время
        /// </summary>
        public string VisitTime { get; set; }
    }
}
