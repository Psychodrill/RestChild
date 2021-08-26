using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

using RestChild.Comon;
using RestChild.DAL;
using RestChild.DAL.RepositoryExtensions;
using RestChild.Domain;

namespace RestChild.Web.Controllers.WebApi
{
	public class GroupedTimeOfRestController : BaseController
	{

		public List<GroupedTimeOfRest> Get()
		{
			return UnitOfWork.GetSet<GroupedTimeOfRest>().ToList();
		}

		public GroupedTimeOfRest Get(long id)
		{
			return UnitOfWork.GetById<GroupedTimeOfRest>(id);
		}
	}
}