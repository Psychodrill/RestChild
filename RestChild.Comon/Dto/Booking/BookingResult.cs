using System;
using System.Runtime.Serialization;

namespace RestChild.Comon.Dto.Booking
{
    /// <summary>
    ///     результат бронирования
    /// </summary>
    [DataContract(Name = "bookingResult")]
    public class BookingResult
    {
        /// <summary>
        ///     Идентификатор бронирования
        /// </summary>
        [DataMember(Name = "bookingGuid")]
        public Guid? BookingGuid { get; set; }

        /// <summary>
        ///     ошибка в бронировании
        /// </summary>
        [DataMember(Name = "errorMessage")]
        public string ErrorMessage { get; set; }

        /// <summary>
        ///     есть ли ошибка
        /// </summary>
        [DataMember(Name = "isError")]
        public bool IsError { get; set; }
    }
}
