using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using RestChild.Domain;

namespace RestChild.Web.Controllers.WebApi
{
    public class WebCountryController : WebGenericRestController<Country>
    {
	    public List<Country> Get(string query)
	    {
		    var q = UnitOfWork.GetSet<Country>().AsQueryable();

		    if (!string.IsNullOrWhiteSpace(query))
		    {
			    q = q.Where(c => c.FullName.ToLower().Contains(query.ToLower()));
		    }

			return q.OrderBy(c => c.FullName.Length).Take(150)
				    .ToList().Select(c=>new Country(c)).ToList();
	    }

	    public override Country Post(Country entity)
	    {
			throw new HttpResponseException(HttpStatusCode.NotImplemented);
	    }

	    public override Country Put(long id, Country entity)
	    {
			throw new HttpResponseException(HttpStatusCode.NotImplemented);
	    }
    }
}
