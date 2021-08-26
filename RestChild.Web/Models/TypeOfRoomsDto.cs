using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using RestChild.Domain;

namespace RestChild.Web.Models
{
	public class TypeOfRoomsDto
	{
		public TypeOfRooms entity { get; set; }
		public string ConviencesString { get; set; }
		public string NameWithDescription { get; set; }
	}
}