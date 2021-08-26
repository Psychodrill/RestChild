using System.Runtime.Serialization;

namespace RestChild.Comon.Dto.Booking
{
    /// <summary>
    ///     запрос на бронирование номера
    /// </summary>
    [DataContract(Name = "locationRequest")]
    public class LocationRequest
    {
        /// <summary>
        ///     ИД номера
        /// </summary>
        [DataMember(Name = "roomId")]
        public long RoomId { get; set; }

        /// <summary>
        ///     наименование размещения
        /// </summary>
        [IgnoreDataMember]
        public string Name { get; set; }

        /// <summary>
        ///     количество номеров
        /// </summary>
        [DataMember(Name = "count")]
        public int? Count { get; set; }
    }
}
