using System;
using System.Collections.Generic;

namespace RestChild.Booking.Logic.Logic
{
    /// <summary>
    ///     логика работы с интервалами
    /// </summary>
    public static class MGTWorkingDayLogic
    {
        /// <summary>
        ///     Временной интервал приёма
        /// </summary>
        public static short TimeIntervalOn(DateTime onDate)
        {
            //до 31 октября (включительно) 2020 старая сетка должна оставаться без изменения с шагом 45 минут;
            if (onDate <= new DateTime(2020, 10, 31))
            {
                return 45;
            }

            //начиная с 1 ноября 2020 сетка должна формироваться с шагом 30 минут;
            return 30;
        }

        /// <summary>
        ///     интервалы
        /// </summary>
        public static string[] GetIntervals(DateTime onDate)
        {
            var timeSpans = new List<string>();

            var start = new TimeSpan(8, 0, 0);
            var interval = TimeSpan.FromMinutes(TimeIntervalOn(onDate));
            var end = new TimeSpan(23, 0, 0);

            while (start.Add(interval) <= end)
            {
                timeSpans.Add(start.ToString(@"hh\:mm"));
                start = start.Add(interval);
            }

            return timeSpans.ToArray();
        }
    }
}
