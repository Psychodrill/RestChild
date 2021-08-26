using RestChild.Domain;
using RestChild.Extensions.Filter;

namespace RestChild.Web.Models
{
	public class HotelPricesViewModel
	{
		public HotelPricesFilter Filter { get; set; }
		public Hotels Hotel { get; set; }
		public CommonPagedList<HotelPrice> Page { get; set; }
	}
}