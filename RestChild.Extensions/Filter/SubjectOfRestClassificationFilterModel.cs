using System.Collections.Generic;
using RestChild.Domain;

namespace RestChild.Extensions.Filter
{
    public class SubjectOfRestClassificationFilterModel : CommonPagedList<SubjectOfRestClassification>
    {
        public SubjectOfRestClassificationFilterModel() : base(null, 1, 15, 0)
        {
        }

        public SubjectOfRestClassificationFilterModel(IEnumerable<SubjectOfRestClassification> pageItems,
            int pageNumber,
            int pageSize,
            int totalItemCount) : base(pageItems, pageNumber, pageSize, totalItemCount)
        {
        }

        /// <summary>
        ///     строка поиска по наименованию
        /// </summary>
        public string SearchString { get; set; }

        /// <summary>
        ///     номер страницы
        /// </summary>
        public int? PageNumberEx { get; set; }

        public bool ViewArchive { get; set; }
    }
}
