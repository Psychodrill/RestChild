using System;
using System.Collections.Generic;
using RestChild.Domain;

namespace RestChild.Extensions.Filter
{
    /// <summary>
    ///     тесты для проверки вожатых
    /// </summary>
    public class CounselorTestFilterModel : CommonPagedList<CounselorTest>
    {
        public CounselorTestFilterModel() : base(null, 1, 15, 0)
        {
        }

        public CounselorTestFilterModel(IEnumerable<CounselorTest> pageItems, int pageNumber, int pageSize,
            int totalItemCount) : base(pageItems, pageNumber, pageSize, totalItemCount)
        {
        }

        /// <summary>
        ///     номер страницы
        /// </summary>
        public int? PageNumberEx { get; set; }

        /// <summary>
        ///     Статус
        /// </summary>
        public long? StateId { get; set; }

        /// <summary>
        ///     дата начала создания.
        /// </summary>
        public DateTime? DateCreateStart { get; set; }

        /// <summary>
        ///     дата окончания создания
        /// </summary>
        public DateTime? DateCreateEnd { get; set; }

        /// <summary>
        ///     строка поиска по нименованию
        /// </summary>
        public string SearchString { get; set; }

        /// <summary>
        ///     статусы.
        /// </summary>
        public List<StateMachineState> States { get; set; }
    }
}
