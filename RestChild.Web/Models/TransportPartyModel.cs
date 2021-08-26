using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using RestChild.Domain;

namespace RestChild.Web.Models
{
	public class TransportPartyModel
	{
		public long Id { get; set; }
		public int? Number { get; set; }
		public List<LinkToPeopleModel> Counselors { get; set; }
		public List<LinkToPeopleModel> Children { get; set; }
	}
}