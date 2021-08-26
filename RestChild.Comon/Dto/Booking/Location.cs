using System.Collections.Generic;
using System.Runtime.Serialization;

namespace RestChild.Comon.Dto.Booking
{
    [DataContract(Name = "location")]
    public class Location
    {
        [IgnoreDataMember] public int Index { get; set; }

        /// <summary>
        ///     размещения.
        /// </summary>
        [DataMember(Name = "places")]
        public List<Rooms> Places { get; set; }
    }
}
