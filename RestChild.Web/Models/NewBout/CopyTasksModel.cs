using System.Collections.Generic;

namespace RestChild.Web.Models.NewBout
{
    /// <summary>
    ///     Модель для добавления скопированных заданий в заезд
    /// </summary>
    public class CopyTasksModel
    {
        /// <summary>
        ///     Идентификатор заезда из которого производится копирование
        /// </summary>
        public long SourceBoutId { get; set; }

        /// <summary>
        ///     Идентификатор заезда в который производится копирование
        /// </summary>
        public long TargetBoutId { get; set; }

        /// <summary>
        ///     Коллекция идентификатор заданий, которые будут добавлены
        /// </summary>
        public List<long> SourceTaskIds { get; set; }
    }
}
