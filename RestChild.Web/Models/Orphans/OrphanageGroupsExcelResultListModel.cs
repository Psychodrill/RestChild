using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestChild.Web.Models.Orphans
{
    /// <summary>
    ///     Модель группы/потребности учреждения социальной защиты для Excel
    /// </summary>
    public class OrphanageGroupsExcelResultListModel : OrphanageGroupsResultListModel
    {
        /// <summary>
        ///     Оздоровительные организации
        /// </summary>
        public ICollection<string> Camps { get; set; }

        /// <summary>
        ///     Количество воспитанников
        /// </summary>
        public int PipilCount { get; set; }

        /// <summary>
        ///     Количество воспитанников передвигающихся с помощью кресла-коляски
        /// </summary>
        public int PupilРandicappedCount { get; set; }

        /// <summary>
        ///     Количество сопровождающих
        /// </summary>
        public int OverseerCount { get; set; }

        /// <summary>
        ///     Количество сопровождающих от ГАУК "МОСГОРТУР"
        /// </summary>
        public int MGTOverseerCount { get; set; }

        /// <summary>
        ///     Периоды отдыха
        /// </summary>
        public ICollection<string> PeriodsOfRest { get; set; }

        /// <summary>
        ///     Каникулы c
        /// </summary>
        public ICollection<string> VacationsFrom { get; set; }

        /// <summary>
        ///     Каникулы по
        /// </summary>
        public ICollection<string> VacationsTo { get; set; }

    }
}
