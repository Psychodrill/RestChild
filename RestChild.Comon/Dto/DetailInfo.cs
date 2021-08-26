using System.Runtime.Serialization;

namespace RestChild.Comon.Dto
{
    /// <summary>
    ///     детализация по рейтингу
    /// </summary>
    [DataContract(Name = "di")]
    public class DetailInfo
    {
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public long Id { get; set; }

        [DataMember(Name = "cid", EmitDefaultValue = false)]
        public long ChildId { get; set; }

        [DataMember(Name = "c", EmitDefaultValue = false)]
        public string Child { get; set; }

        [DataMember(Name = "rn", EmitDefaultValue = false)]
        public string RegistryNumber { get; set; }

        [DataMember(Name = "y", EmitDefaultValue = false)]
        public long Year { get; set; }
    }
}
