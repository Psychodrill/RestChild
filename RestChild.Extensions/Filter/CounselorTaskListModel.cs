using System;
using System.Collections.Generic;
using RestChild.Domain;

namespace RestChild.Extensions.Filter
{
    /// <summary>
    ///     модель для просмотра
    /// </summary>
    public class CounselorTaskListModel : CommonPagedList<CounselorTask>
    {
        public CounselorTaskListModel() : base(null, 1, 15, 0)
        {
        }

        public CounselorTaskListModel(IEnumerable<CounselorTask> pageItems, int pageNumber, int pageSize,
            int totalItemCount) : base(pageItems, pageNumber, pageSize, totalItemCount)
        {
        }

        /// <summary>
        ///     номер страницы
        /// </summary>
        public int? PageNumberEx { get; set; }

        /// <summary>
        ///     год кампании
        /// </summary>
        public long? YearOfRestId { get; set; }

        /// <summary>
        ///     Тип обращения
        /// </summary>
        public long? NotNessesary { get; set; }

        /// <summary>
        ///     Статус
        /// </summary>
        public long? StateId { get; set; }

        /// <summary>
        ///     отель
        /// </summary>
        public Hotels Hotels { get; set; }

        /// <summary>
        ///     ИД отеля
        /// </summary>
        public long? HotelsId { get; set; }

        /// <summary>
        ///     смена
        /// </summary>
        public long? GroupedTimeOfRestId { get; set; }

        /// <summary>
        ///     тема
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        ///     Содержание
        /// </summary>
        public string Body { get; set; }

        public DateTime? TaskStartDate { get; set; }
        public DateTime? TaskEndDate { get; set; }

        public DateTime? ExecutionStartDate { get; set; }
        public DateTime? ExecutionEndDate { get; set; }

        /// <summary>
        ///     годы кампаний
        /// </summary>
        public List<YearOfRest> Years { get; set; }

        /// <summary>
        ///     смены
        /// </summary>
        public ICollection<GroupedTimeOfRest> GroupedTimesOfRest { get; set; }

        /// <summary>
        ///     годы кампаний
        /// </summary>
        public List<StateMachineState> States { get; set; }
    }
}
