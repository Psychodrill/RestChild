using RestChild.Extensions.Filter;
using RestChild.Mobile.Domain;

namespace RestChild.Web.Models.NewBout
{
    /// <summary>
    ///     модель для списка заездов
    /// </summary>
    public class ListModel
    {
        /// <summary>
        ///     номер страницы
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        ///     год компании
        /// </summary>
        public int? Year { get; set; }

        /// <summary>
        ///     года
        /// </summary>
        public int[] Years { get; set; }

        /// <summary>
        ///     период
        /// </summary>
        public int? GroupedTime { get; set; }

        /// <summary>
        ///     смены
        /// </summary>
        public GroupedTime[] Times { get; set; }

        /// <summary>
        ///     Объект отдыха
        /// </summary>
        public int? CampId { get; set; }

        /// <summary>
        ///     лагерь
        /// </summary>
        public Camp Camp { get; set; }

        /// <summary>
        ///     город
        /// </summary>
        public string City { get; set; }

        /// <summary>
        ///     результаты
        /// </summary>
        public CommonPagedList<Bout> Result { get; set; }
    }
}
