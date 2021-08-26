using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestChild.Web.Models.Orphans
{
    /// <summary>
    ///     Краткая модель списка/группы отправки
    /// </summary>
    public class OrphanagePupilGroupListResultListModel
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
        ///     Порядковый номер группы
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        ///     Год потребности
        /// </summary>
        public string YearOfRest { get; set; }

        /// <summary>
        ///     Форма отдыха и оздоровления
        /// </summary>
        public string FormOfRest { get; set; }

        /// <summary>
        ///     Размещение
        /// </summary>
        public string TourName { get; set; }

        /// <summary>
        ///     Статус
        /// </summary>
        public string StateName { get; set; }
    }
}
