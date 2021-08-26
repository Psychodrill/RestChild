using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using RestChild.Domain;

namespace RestChild.Web.Controllers.WebApi
{
	[Authorize]
	public class WebFileTypeController : WebGenericRestController<FileType>
	{
		public IList<FileType> Get()
		{
			IQueryable<FileType> query = UnitOfWork.GetSet<FileType>();
			var entities = query.OrderBy(place => place.Name).ToList();

			return entities;
		}
	}
}