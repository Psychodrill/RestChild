using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RestChild.Extensions.Filter;

namespace RestChild.Web.Models.PedParty
{
	public class PedPartyFilterModel
	{
		public string Name { get; set; }
		public string City { get; set; }
		public int PageNumber { get; set; }
		public PedPartyPagedList Result { get; set; }
	}
}