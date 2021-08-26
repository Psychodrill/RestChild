using System;

namespace RestChild.Extensions.Filter
{
    /// <summary>
    ///     Модель данных брони для отоброжения
    /// </summary>
    public sealed class BookingMosgorturReestrFilterResultModel
    {
        /// <summary>
        ///     Идентификатор брони
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        ///     Дата визита
        /// </summary>
        public DateTime DateShedule { get; set; }

        /// <summary>
        ///     Цель посещения
        /// </summary>
        public string Target { get; set; }

        /// <summary>
        ///     Статус брони
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        ///     ФИО заявителя
        /// </summary>
        public string AplicantFIO { get; set; }

        /// <summary>
        ///     СНИЛС посетителя
        /// </summary>
        public string AplicantSNILS { get; set; }

        /// <summary>
        ///     Дата рождения заявителя
        /// </summary>
        public DateTime? AplicantDateBirth { get; set; }

        /// <summary>
        ///     Пол заявителя
        /// </summary>
        public bool AplicantMale { get; set; }

        /// <summary>
        ///     Телефон заявителя
        /// </summary>
        public string AplicantTel { get; set; }

        /// <summary>
        ///     Email заявителя
        /// </summary>
        public string AplicantEmail { get; set; }

        /// <summary>
        ///     Идентификатор заявления
        /// </summary>
        public long? StatementId { get; set; }

        /// <summary>
        ///     Номер брони
        /// </summary>
        public string BookingNumber { get; set; }

        /// <summary>
        ///     Количество слотов
        /// </summary>
        public int SlotsCount { get; set; }

        /// <summary>
        ///     Номер брони в МПГУ
        /// </summary>
        public string MPGUNumber { get; set; }

        /// <summary>
        ///     Пин код
        /// </summary>
        public string PINCode { get; set; }

        /// <summary>
        ///     Источник брони (МПГУ/Оператор)
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        ///     Дата регистрации брони
        /// </summary>
        public DateTime? RegDate { get; set; }
    }
}
