using RestChild.Extensions.Filter;
using RestChild.Mobile.Domain;

namespace RestChild.Web.Models.Task
{
    /// <summary>
    ///     список заданий
    /// </summary>
    public class ListModel
    {
        /// <summary>
        ///     номер страницы
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        ///     Название задания
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Статус
        /// </summary>
        public long? StateId { get; set; }

        /// <summary>
        ///     Статусы
        /// </summary>
        public State[] States { get; set; }

        /// <summary>
        ///     Стоимость от
        /// </summary>
        public decimal? PriceFrom { get; set; }

        /// <summary>
        ///     Стоимость до
        /// </summary>
        public decimal? PriceTo { get; set; }

        /// <summary>
        ///     Кол-во от
        /// </summary>
        public decimal? CountFrom { get; set; }

        /// <summary>
        ///     Кол-во до
        /// </summary>
        public decimal? CountTo { get; set; }

        /// <summary>
        ///     Объект отдыха
        /// </summary>
        public int? CampId { get; set; }

        /// <summary>
        ///     лагерь
        /// </summary>
        public Camp Camp { get; set; }

        /// <summary>
        ///     результаты
        /// </summary>
        public CommonPagedList<BoutTask> Result { get; set; }
    }
}
