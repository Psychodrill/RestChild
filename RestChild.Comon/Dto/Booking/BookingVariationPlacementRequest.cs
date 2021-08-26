using System.Runtime.Serialization;

namespace RestChild.Comon.Dto.Booking
{
    /// <summary>
    ///     запрос на варианты размещения
    /// </summary>
    [DataContract(Name = "bookingVariationPlacementRequest")]
    public class BookingVariationPlacementRequest : BaseRequest
    {
        /// <summary>
        ///     место отдыха
        /// </summary>
        [DataMember(Name = "key")]
        public string HotelKey { get; set; }

        /// <summary>
        ///     время отдыха
        /// </summary>
        [DataMember(Name = "tourId")]
        public long TourId { get; set; }

        /// <summary>
        ///     Количество детей
        /// </summary>
        [DataMember(Name = "places")]
        public int Places { get; set; }

        /// <summary>
        ///     сопровождающие
        /// </summary>
        [DataMember(Name = "attendants")]
        public int Attendants { get; set; }

        /// <summary>
        ///     использовать даты бронирования
        /// </summary>
        [DataMember(Name = "wbr", EmitDefaultValue = false)]
        public bool? WithBookingDate { get; set; }
    }
}
