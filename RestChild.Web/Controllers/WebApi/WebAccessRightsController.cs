using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using RestChild.Comon;
using RestChild.Domain;

namespace RestChild.Web.Controllers.WebApi
{
	[Authorize]
	public class WebAccessRightsController : BaseController
	{
		public IEnumerable<AccessRight> Get()
		{
			return UnitOfWork.GetSet<AccessRight>().ToList();
		}
	}
}