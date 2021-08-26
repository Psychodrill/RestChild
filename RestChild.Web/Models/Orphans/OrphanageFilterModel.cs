using RestChild.Extensions.Filter;

namespace RestChild.Web.Models.Orphans
{
    /// <summary>
    ///     Фильтр поиска детских домов
    /// </summary>
    public class OrphanageFilterModel
    {
        public OrphanageFilterModel()
        {
            PageNumber = 1;
        }

        /// <summary>
        ///     Наименование
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Краткое наименование
        /// </summary>
        public string ShortName { get; set; }

        /// <summary>
        ///     Адрес
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        ///     Директор
        /// </summary>
        public string DirectorName { get; set; }

        /// <summary>
        ///     Результат поиска
        /// </summary>
        public CommonPagedList<OrphanageResultListModel> Result { get; set; }

        public int PageNumber { get; set; }
    }
}
