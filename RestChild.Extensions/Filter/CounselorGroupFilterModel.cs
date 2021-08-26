using System;
using System.Collections.Generic;
using RestChild.Domain;

namespace RestChild.Extensions.Filter
{
    /// <summary>
    ///     поиск групп обучения вожатых
    /// </summary>
    public class CounselorGroupFilterModel : CommonPagedList<TrainingCounselors>
    {
        public CounselorGroupFilterModel() : base(null, 1, 15, 0)
        {
        }

        public CounselorGroupFilterModel(IEnumerable<TrainingCounselors> pageItems, int pageNumber, int pageSize,
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
        ///     дата начала обучения с.
        /// </summary>
        public DateTime? DateCreateStart { get; set; }

        /// <summary>
        ///     дата начала обучения по.
        /// </summary>
        public DateTime? DateCreateEnd { get; set; }

        /// <summary>
        ///     строка поиска по наименованию
        /// </summary>
        public string SearchString { get; set; }

        /// <summary>
        ///     статусы
        /// </summary>
        public List<StateMachineState> States { get; set; }
    }
}
