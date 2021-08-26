using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace RestChild.Comon.Dto.Booking
{
    /// <summary>
    ///     запрос на бронирование
    /// </summary>
    [DataContract(Name = "bookingRequest")]
    public class BookingRequest : BaseRequest
    {
        /// <summary>
        ///     GUID
        /// </summary>
        [DataMember(Name = "bookingGuid")]
        public Guid? BookingGuid { get; set; }

        /// <summary>
        ///     Дата бронирования
        /// </summary>
        [IgnoreDataMember]
        public DateTime? BookingDate { get; set; }

        /// <summary>
        ///     забронировано (сохранено в БД).
        /// </summary>
        [IgnoreDataMember]
        public bool Booked { get; set; }

        /// <summary>
        ///     освобождение (сохранено в БД).
        /// </summary>
        [IgnoreDataMember]
        public bool Released { get; set; }

        /// <summary>
        ///     место отдыха
        /// </summary>
        [DataMember(Name = "tourId")]
        public long TourId { get; set; }

        /// <summary>
        ///     количество детей
        /// </summary>
        [DataMember(Name = "places")]
        public int Places { get; set; }

        /// <summary>
        ///     количество сопровождающих
        /// </summary>
        [DataMember(Name = "attendants")]
        public int Attendants { get; set; }

        /// <summary>
        ///     места бронирования
        /// </summary>
        [DataMember(Name = "rooms")]
        public List<LocationRequest> Rooms { get; set; }

        /// <summary>
        ///     запрос пришел из МПГУ
        /// </summary>
        [DataMember(Name = "fromMPGU")]
        public bool IsFromMPGU { get; set; }
    }
}
