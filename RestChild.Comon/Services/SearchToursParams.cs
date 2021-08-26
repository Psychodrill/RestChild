using System.Runtime.Serialization;

namespace RestChild.Comon.Services
{
    [DataContract(Name = "searchToursParams")]
    public class SearchToursParams
    {
        [DataMember] public int TypeId { get; set; }

        [DataMember] public string StartDate { get; set; }

        [DataMember] public string EndDate { get; set; }

        [DataMember] public int? AdultCount { get; set; }

        [DataMember] public int[] ChildrenAges { get; set; }

        [DataMember] public int[] DiningOptions { get; set; }

        [DataMember] public int? PriceFrom { get; set; }

        [DataMember] public int? PriceTo { get; set; }

        [DataMember] public int? DurationFrom { get; set; }

        [DataMember] public int? DurationTo { get; set; }

        [DataMember] public bool IncludeNoTrasport { get; set; }

        [DataMember] public long[] Regions { get; set; }

        [DataMember] public bool AccessibleEnvironment { get; set; }

        [DataMember] public long[] Countries { get; set; }

        [DataMember] public long[] Programs { get; set; }

        /// <summary>
        ///     наименование продукта
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        ///     наименование отеля
        /// </summary>
        [DataMember]
        public string HotelName { get; set; }

        /// <summary>
        ///     номер смены
        /// </summary>
        [DataMember]
        public int? Smena { get; set; }

        /// <summary>
        ///     зона у моря
        /// </summary>
        [DataMember]
        public bool ZoneOfSea { get; set; }

        [DataMember] public long Index { get; set; }

        [DataMember] public bool HaveMore { get; set; }

        [DataMember] public long? Id { get; set; }

        /// <summary>
        ///     групповые
        /// </summary>
        [DataMember(EmitDefaultValue = false)]
        public bool? IsGroup { get; set; }
    }
}
