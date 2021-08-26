using System.Collections.Generic;
using System.Linq;
using RestChild.Domain;

namespace RestChild.Web.Models
{
	public class RequestInformationVoucherModel : ViewModelBase<RequestInformationVoucher>
	{
		public RequestInformationVoucherModel() : base(new RequestInformationVoucher())
		{
		}

		public RequestInformationVoucherModel(RequestInformationVoucher data) : base(data)
		{
			AttendantsPrice = data?.AttendantsPrice?.ToList() ?? new List<RequestInformationVoucherAttendant>();

		}

		public List<RequestInformationVoucherAttendant> AttendantsPrice { get; set; }

		public override RequestInformationVoucher BuildData()
		{
			Data.AttendantsPrice = AttendantsPrice;
			return base.BuildData();
		}
	}
}
