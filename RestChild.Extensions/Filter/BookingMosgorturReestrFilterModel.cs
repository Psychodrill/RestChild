using System;

namespace RestChild.Extensions.Filter
{
    /// <summary>
    ///     Фильтр списка броней на визит в МГТ
    /// </summary>
    public class BookingMosgorturReestrFilterModel
    {
        public BookingMosgorturReestrFilterModel()
        {
            PageNumber = 1;
            ShowPebookings = false;
        }

        /// <summary>
        ///     ФИО заявителя
        /// </summary>
        public string FIO { get; set; }

        /// <summary>
        ///     Идентификатор статуса
        /// </summary>
        public long? Status { get; set; }

        /// <summary>
        ///     Дата визита с
        /// </summary>
        public DateTime? DateFrom { get; set; }

        /// <summary>
        ///     Дата визита по
        /// </summary>
        public DateTime? DateTo { get; set; }

        /// <summary>
        ///     Идентификатор цели обращения
        /// </summary>
        public long? Target { get; set; }

        /// <summary>
        ///     Номер заявления
        /// </summary>
        public string ServiceNumber { get; set; }

        /// <summary>
        ///     Дата подачи заявления с
        /// </summary>
        public DateTime? DateRegFrom { get; set; }

        /// <summary>
        ///     Дата подачи заявления по
        /// </summary>
        public DateTime? DateRegTo { get; set; }

        /// <summary>
        ///     Идентификатор источника (Не указан(null)/МПГУ(1)/Оператор(2))
        /// </summary>
        public int? Source { get; set; }

        /// <summary>
        ///     Результирующая выборка
        /// </summary>
        public CommonPagedList<BookingMosgorturReestrFilterResultModel> Result { get; set; }

        /// <summary>
        ///     Показывать преброни
        /// </summary>
        public bool ShowPebookings { get; set; }

        /// <summary>
        ///     Номер страницы
        /// </summary>
        public int PageNumber { get; set; }
    }
}
