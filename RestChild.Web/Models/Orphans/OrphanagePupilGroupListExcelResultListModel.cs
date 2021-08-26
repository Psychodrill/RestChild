using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestChild.Web.Models.Orphans
{
    /// <summary>
    ///     Расширенная для Excel модель списка/группы отправки
    /// </summary>
    public class OrphanagePupilGroupListExcelResultListModel : OrphanagePupilGroupListResultListModel
    {
        /// <summary>
        ///     Регион отдыха
        /// </summary>
        public string RegionOfRest { get; set; }

        /// <summary>
        ///     Период отдыха
        /// </summary>
        public string TimeOfRest { get; set; }

        /// <summary>
        ///     Оздоровительная организация
        /// </summary>
        public string HotelName { get; set; }

        /// <summary>
        ///     Воспитанники
        /// </summary>
        public OrphanagePupilGroupListExcelResultPeopleModel[] Pupils { get; set; }

        /// <summary>
        ///     Сопровождающие
        /// </summary>
        public OrphanagePupilGroupListExcelResultPeopleModel[] Collaborators { get; set; }
    }
}
