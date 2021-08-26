using System.Collections.Generic;
using System.Runtime.Serialization;

namespace RestChild.Comon.Dto.Booking
{
    [DataContract(Name = "resultSearch")]
    public class ResultSearch
    {
        /// <summary>
        ///     количество детей
        /// </summary>
        [DataMember(Name = "places", EmitDefaultValue = false)]
        public int Places { get; set; }

        /// <summary>
        ///     количество сопровождающих
        /// </summary>
        [DataMember(Name = "attendants", EmitDefaultValue = false)]
        public int Attendants { get; set; }

        /// <summary>
        ///     всего мест.
        /// </summary>
        [DataMember(Name = "count")]
        public int Count { get; set; }

        /// <summary>
        ///     места отдыха
        /// </summary>
        [DataMember(Name = "hotels")]
        public List<Hotel> Hotels { get; set; }

        /// <summary>
        ///     могут быть деньги
        /// </summary>
        [DataMember(Name = "mayBeMoney")]
        public bool MayBeMoney { get; set; }
    }
}
