using System.Collections.Generic;
using RestChild.Extensions.Filter;
using RestChild.Web.Properties;

namespace RestChild.Web.Models.Orphans
{
    /// <summary>
    ///     Модель фильтра поиска группы/потребности
    /// </summary>
    public class OrphanageGroupsFilterModel
    {
        public OrphanageGroupsFilterModel()
        {
            PageNumber = 1;
            Result = new CommonPagedList<OrphanageGroupsResultListModel>(new List<OrphanageGroupsResultListModel>(0),
                PageNumber, Settings.Default.TablePageSize, 0);
        }

        public OrphanageGroupsFilterModel(long orphanageId) : this()
        {
            OrphanageId = orphanageId;
        }

        /// <summary>
        ///     Номер группы
        /// </summary>
        public string Name { get; set; }


        /// <summary>
        ///     Год потребности
        /// </summary>
        public long? YearOfRest { get; set; }

        /// <summary>
        ///     Форма отдыха и оздоровления
        /// </summary>
        public long? FormOfRest { get; set; }

        /// <summary>
        ///     Регион отдыха
        /// </summary>
        public long? RegionOfRest { get; set; }

        /// <summary>
        ///     Статус
        /// </summary>
        public long? StateId { get; set; }

        /// <summary>
        ///     Период
        /// </summary>
        public long? TimeOfRest { get; set; }

        /// <summary>
        ///     Каникулярный период
        /// </summary>
        public long? VacationPeriod { get; set; }

        /// <summary>
        ///     Группы/потребности
        /// </summary>
        public CommonPagedList<OrphanageGroupsResultListModel> Result { get; set; }

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
        ///     Список регионов
        /// </summary>
        public IDictionary<long, string> RegionsOfRest { get; set; }

        /// <summary>
        ///     Список стаусов
        /// </summary>
        public IDictionary<long, string> States { get; set; }

        /// <summary>
        ///     Периоды
        /// </summary>
        public IDictionary<long, string> TimesOfRest { get; set; }

        /// <summary>
        ///     Каникулярные периоды
        /// </summary>
        public IDictionary<long, string> VacationPeriods { get; set; }

        public int PageNumber { get; set; }
    }
}
