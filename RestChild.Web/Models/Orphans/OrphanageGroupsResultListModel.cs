using System.Collections.Generic;

namespace RestChild.Web.Models.Orphans
{
    /// <summary>
    ///     Краткая модель группы/потребности учреждения социальной защиты
    /// </summary>
    public class OrphanageGroupsResultListModel
    {
        /// <summary>
        ///     Идентификатор
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        ///     Наименование учреждения
        /// </summary>
        public string OrphanageName { get; set; }

        /// <summary>
        ///     Наименование (номер группы)
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Год потребности
        /// </summary>
        public string Year { get; set; }

        /// <summary>
        ///     Каникулярный период
        /// </summary>
        public string VacationPeriod { get; set; }

        /// <summary>
        ///     Форма отдыха и оздоровления
        /// </summary>
        public string FormOfRest { get; set; }

        /// <summary>
        ///     Регион и период отдыха
        /// </summary>
        public ICollection<string> RegionsOfRest { get; set; }

        /// <summary>
        ///     Статус
        /// </summary>
        public string Status { get; set; }
    }
}
