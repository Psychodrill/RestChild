using System.Runtime.Serialization;

namespace RestChild.Comon.Dto.Booking
{
    [DataContract(Name = "baseRequest")]
    public class BaseRequest
    {
        /// <summary>
        ///     вид отдыха
        /// </summary>
        [DataMember(Name = "typeOfRestId", EmitDefaultValue = false)]
        public long? TypeOfRestId { get; set; }

        /// <summary>
        ///     номер заявления
        /// </summary>
        [DataMember(Name = "dn", EmitDefaultValue = false)]
        public string DocumentNumber { get; set; }

        /// <summary>
        ///     заявка
        /// </summary>
        [IgnoreDataMember]
        public RequestDto Request { get; set; }
    }
}
