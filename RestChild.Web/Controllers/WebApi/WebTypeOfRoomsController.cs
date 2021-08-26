using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using RestChild.Comon;
using RestChild.Domain;
using RestChild.Web.Models;

namespace RestChild.Web.Controllers.WebApi
{
	[Authorize]
	public class WebTypeOfRoomsController : BaseController
	{
		public IEnumerable<TypeOfRoomsDto> Get(long hotelId)
		{
			return
				UnitOfWork.GetSet<TypeOfRooms>()
					.Where(t => t.HotelId == hotelId)
					.ToList()
					.Select(t => new TypeOfRoomsDto { entity = new TypeOfRooms(t), ConviencesString = t.GetConviencsString(), NameWithDescription = t.ToString() })
					.ToList();
		}
	}
}