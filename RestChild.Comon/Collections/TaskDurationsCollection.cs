using System.Web.Mvc;

namespace RestChild.Comon.Collections
{
    /// <summary>
    ///     Класс для определения продолжительности задания
    /// </summary>
    public static class TaskDurationsCollection
    {
        /// <summary>
        ///     Коллекция для сопоставления значения времени в минутах с текстовой интерпретации этого времени в нужном склонении
        /// </summary>
        public static SelectListItem[] Durations { get; }

        /// <summary>
        ///     Конструктор класса Durations
        /// </summary>
        static TaskDurationsCollection()
        {
            Durations = new[]
            {
                new SelectListItem {Value = "15", Text = "15 минут"},
                new SelectListItem {Value = "30", Text = "30 минут"},
                new SelectListItem {Value = "45", Text = "45 минут"},
                new SelectListItem {Value = "60", Text = "1 час"},
                new SelectListItem {Value = "120", Text = "2 часа"},
                new SelectListItem {Value = "180", Text = "3 часа"},
                new SelectListItem {Value = "240", Text = "4 часа"},
                new SelectListItem {Value = "300", Text = "5 часов"},
                new SelectListItem {Value = "360", Text = "6 часов"},
                new SelectListItem {Value = "420", Text = "7 часов"},
                new SelectListItem {Value = "480", Text = "8 часов"},
                new SelectListItem {Value = "540", Text = "9 часов"},
                new SelectListItem {Value = "600", Text = "10 часов"},
                new SelectListItem {Value = "660", Text = "11 часов"},
                new SelectListItem {Value = "720", Text = "12 часов"},
                new SelectListItem {Value = "1440", Text = "1 день"},
                new SelectListItem {Value = "2880", Text = "2 дня"},
                new SelectListItem {Value = "4320", Text = "3 дня"},
                new SelectListItem {Value = "5760", Text = "4 дня"},
                new SelectListItem {Value = "7200", Text = "5 дней"},
                new SelectListItem {Value = "8640", Text = "6 дней"},
                new SelectListItem {Value = "10080", Text = "7 дней"},
                new SelectListItem {Value = "11520", Text = "8 дней"},
                new SelectListItem {Value = "12960", Text = "9 дней"},
                new SelectListItem {Value = "14400", Text = "10 дней"},
                new SelectListItem {Value = "15840", Text = "11 дней"},
                new SelectListItem {Value = "17280", Text = "12 дней"},
                new SelectListItem {Value = "18720", Text = "13 дней"},
                new SelectListItem {Value = "20160", Text = "14 дней"},
                new SelectListItem {Value = "21600", Text = "15 дней"},
                new SelectListItem {Value = "23040", Text = "16 дней"},
                new SelectListItem {Value = "24480", Text = "17 дней"},
                new SelectListItem {Value = "25920", Text = "18 дней"},
                new SelectListItem {Value = "27360", Text = "19 дней"},
                new SelectListItem {Value = "28800", Text = "20 дней"},
                new SelectListItem {Value = "30240", Text = "21 день"}
            };
        }
    }
}
