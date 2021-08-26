using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using RestChild.DAL;
using RestChild.Domain;

namespace RestChild.Web.Controllers.WebApi
{
	public class WebTypesOfTransportsController : BaseController
    {
	    public ICollection<TypeOfTransport> Get()
	    {
		    return UnitOfWork.GetSet<TypeOfTransport>().ToList();
	    }
    }
}
