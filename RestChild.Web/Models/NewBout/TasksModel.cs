using System;

namespace RestChild.Web.Models.NewBout
{
    /// <summary>
    ///     Модель для скопированного задания
    /// </summary>
    public class TasksModel
    {
        /// <summary>
        ///     Идентификатор заезда из которого происходит копирование
        /// </summary>
        public long? SourceBoutId { get; set; }

        /// <summary>
        ///     Идентификатор задания
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        ///     Наименование задания
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Рейтинг задания
        /// </summary>
        public decimal? Rating { get; set; }

        /// <summary>
        ///     Дата начала задания
        /// </summary>
        public DateTime? StartDate { get; set; }
    }
}
