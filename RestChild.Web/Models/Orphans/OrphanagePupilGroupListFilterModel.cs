using RestChild.Extensions.Filter;
using RestChild.Web.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestChild.Web.Models.Orphans
{
    /// <summary>
    ///     Модель фильтра списков/групп отправки
    /// </summary>
    public class OrphanagePupilGroupListFilterModel
    {
        public OrphanagePupilGroupListFilterModel()
        {
            PageNumber = 1;
            Result = new CommonPagedList<OrphanagePupilGroupListResultListModel>(new List<OrphanagePupilGroupListResultListModel>(0), PageNumber, Settings.Default.TablePageSize, 0);
        }

        public OrphanagePupilGroupListFilterModel(long orphanageId) : this()
        {
            OrphanageId = orphanageId;
        }

        /// <summary>
        ///     Списки/Группы отправки
        /// </summary>
        public CommonPagedList<OrphanagePupilGroupListResultListModel> Result { get; set; }

        public int PageNumber { get; set; }

        /// <summary>
        ///     Порядковый номер группы
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Год потребности
        /// </summary>
        public long? YearOfRest { get; set; }

        /// <summary>
        ///     Форма отдыха и оздоровления
        /// </summary>
        public long? FormOfRestId { get; set; }

        /// <summary>
        ///     Статус
        /// </summary>
        public long? StateId { get; set; }

        /// <summary>
        ///     Идентификатор детского дома
        /// </summary>
        public long? OrphanageId { get; set; }

        /// <summary>
        ///     Название детского дома
        /// </summary>
        public string OrphanageName { get; set; }

        /// <summary>
        ///     Список годов потребности
        /// </summary>
        public IDictionary<long, string> YearsOfRest { get; set; }

        /// <summary>
        ///     Список форм отдыха и оздоровления
        /// </summary>
        public IDictionary<long, string> FormsOfRest { get; set; }

        /// <summary>
        ///     Список стаусов
        /// </summary>
        public IDictionary<long, string> States { get; set; }
    }
}
