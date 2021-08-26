using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using RestChild.Comon;
using RestChild.Domain;

namespace RestChild.Web.Controllers.WebApi
{
	[Authorize]
	public class WebHotelTypeController : BaseController
	{		
		public IEnumerable<HotelType> Get()
		{
			return UnitOfWork.GetSet<HotelType>().ToList();
		}
	}
}