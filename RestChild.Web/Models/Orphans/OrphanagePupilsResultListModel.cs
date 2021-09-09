using System;

namespace RestChild.Web.Models.Orphans
{
    /// <summary>
    ///     Краткая модель воспитанника учреждения социальной защиты
    /// </summary>
    public class OrphanagePupilsResultListModel
    {
        /// <summary>
        ///     Идентификатор
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        ///     ФИО
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Дата рождения
        /// </summary>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        ///     Удалён
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}
