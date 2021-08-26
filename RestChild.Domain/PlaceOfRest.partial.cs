using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace RestChild.Domain
{
    public partial class PlaceOfRest
    {
        /// <summary>
        ///     цели обращения
        /// </summary>
        [NotMapped]
        [DataMember(Name = "typeOfRestIds")]
        public long?[] TypeOfRestIds { get; set; }
    }
}
