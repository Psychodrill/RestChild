using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace RestChild.Domain
{
	public partial class OfferInRequest
	{
		[IgnoreDataMember]
		[NotMapped]
		public long Index { get; set; }
	}
}