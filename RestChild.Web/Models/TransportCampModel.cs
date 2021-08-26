using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using RestChild.Domain;

namespace RestChild.Web.Models
{
	public class TransportCampModel
	{
		public long Id { get; set; }
		public string Name { get; set; }
		public List<LinkToPeopleModel> SeniorCounselors { get; set; }
		public List<LinkToPeopleModel> SwingCounselors { get; set; }
		public List<LinkToPeopleModel> Administrators { get; set; }

		public List<LinkToPeopleModel> Attendants { get; set; }
		public List<TransportPartyModel> Parties { get; set; }
	}
}