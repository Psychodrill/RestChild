using System.Runtime.Serialization;

namespace RestChild.Comon.Dto.Booking
{
    [DataContract(Name = "roomShedule")]
    public class RoomShedule : Rooms
    {
        public RoomShedule()
        {
        }

        public RoomShedule(RoomShedule source) : base(source)
        {
            Left = source.Left;
        }

        /// <summary>
        ///     Количество осталось номеров
        /// </summary>
        [DataMember(Name = "left")]
        public int Left { get; set; }
    }
}
