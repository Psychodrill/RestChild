using System.Collections.Generic;
using System.Runtime.Serialization;

namespace RestChild.Comon.Dto.Booking
{
    /// <summary>
    ///     результат получения информации по месту
    /// </summary>
    [DataContract(Name = "bookingVariationPlacementResponse")]
    public class BookingVariationPlacementResponse
    {
        /// <summary>
        ///     информация о заезде
        /// </summary>
        [DataMember(Name = "tourId")]
        public long? TourId { get; set; }

        /// <summary>
        ///     варианты размещения.
        /// </summary>
        [DataMember(Name = "locations")]
        public List<Location> Locations { get; set; }
    }
}
