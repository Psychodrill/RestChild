using System.Collections.Generic;

namespace RestChild.Web.Models.Orphans
{
    /// <summary>
    ///     Модель информации о состоянии здоровья воспитанников
    /// </summary>
    public class PupilsHealthStatusModel
    {
        /// <summary>
        ///     Кол-во воспитанников с указанными ограничениями
        /// </summary>
        public int? PupilsCount { get; set; }

        /// <summary>
        ///     Вид ограничения
        /// </summary>
        public long? TypeOfRestrictionId { get; set; }

        /// <summary>
        ///     Подвид ограничения
        /// </summary>
        public virtual long? TypeOfSubRestrictionId { get; set; }

        /// <summary>
        ///     Виды ограничения
        /// </summary>
        public IDictionary<long, string> TypesOfRestriction { get; set; }

        /// <summary>
        ///     Подвиды ограничения
        /// </summary>
        public IDictionary<long, string> TypesOfSubRestriction { get; set; }
    }
}
