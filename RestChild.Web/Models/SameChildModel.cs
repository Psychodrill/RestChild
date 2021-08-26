using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using RestChild.Domain;

namespace RestChild.Web.Models
{
	public class SameChildModel
	{
		public IEnumerable<Child> Children { get; set; }

		public Organization Organization { get; set; }
	}
}