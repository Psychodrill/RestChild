using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestChild.Web.Models
{
	public class HouseViewModel
	{
		public long AddressId { get; set; }

		public string ShortAddress { get; set; }

		public string District { get; set; }

		public string Region { get; set; }
	}
}