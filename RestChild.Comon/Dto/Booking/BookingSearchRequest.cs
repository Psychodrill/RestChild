using System.Runtime.Serialization;

namespace RestChild.Comon.Dto.Booking
{
    /// <summary>
    ///     поисковый запрос для мест отдыха
    /// </summary>
    [DataContract(Name = "bookingSearchRequest")]
    public class BookingSearchRequest : BaseRequest
    {
        /// <summary>
        ///     время отдыха.
        /// </summary>
        [DataMember(Name = "timeOfRestId")]
        public long? TimeOfRestId { get; set; }

        /// <summary>
        ///     Направление отдыха
        /// </summary>
        [DataMember(Name = "placeOfRestId")]
        public long? PlaceOfRestId { get; set; }

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
        ///     первая запись в результате
        /// </summary>
        [DataMember(Name = "firstRow")]
        public int FirstRow { get; set; }

        /// <summary>
        ///     количество записей в запросе
        /// </summary>
        [DataMember(Name = "countRows")]
        public int CountRows { get; set; }

        /// <summary>
        ///     использовать даты бронирования
        /// </summary>
        [DataMember(Name = "wbr", EmitDefaultValue = false)]
        public bool? WithBookingDate { get; set; }
    }
}
