using System.Runtime.Serialization;

namespace RestChild.Comon.Dto.Booking
{
    [DataContract(Name = "rooms")]
    public class Rooms
    {
        public Rooms()
        {
        }

        public Rooms(Rooms source)
        {
            Id = source.Id;
            Description = source.Description;
            Place = source.Place;
            PlaceAddon = source.PlaceAddon;
            CountRooms = source.CountRooms;
        }

        /// <summary>
        ///     ид вида номера
        /// </summary>
        [DataMember(Name = "id")]
        public long Id { get; set; }

        /// <summary>
        ///     описание номера.
        /// </summary>
        [DataMember(Name = "description")]
        public string Description { get; set; }

        /// <summary>
        ///     количество номеров
        /// </summary>
        [DataMember(Name = "countRooms")]
        public int? CountRooms { get; set; }

        /// <summary>
        ///     Количество мест
        /// </summary>
        [DataMember(Name = "place")]
        public int Place { get; set; }

        /// <summary>
        ///     Количество мест (дополнительных)
        /// </summary>
        [DataMember(Name = "placeAddon")]
        public int PlaceAddon { get; set; }
    }
}
