using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace RestChild.Domain
{
	public partial class RequestInformationVoucherAttendant
	{
		[NotMapped]
		[DataMember(Name = "AttendantGuid")]
		public Guid? AttendantGuid { get; set; }
	}
}
