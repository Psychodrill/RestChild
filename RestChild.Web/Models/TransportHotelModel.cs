using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using RestChild.Domain;

namespace RestChild.Web.Models
{
	public class TransportHotelModel
	{
		public long Id { get; set; }
		public string Name { get; set; }

		public List<LinkToPeopleModel> Administrators { get; set; }
		public List<TransportRequestModel> Requests { get; set; }
	}
}