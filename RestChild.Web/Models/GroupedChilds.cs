using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using RestChild.Domain;

namespace RestChild.Web.Models
{
	public class GroupedChilds
	{
		public string Name { get; set; }

		public List<Child> Childs { get; set; }

		public Organization Organization { get; set; }

		public Organization Oiv { get; set; }
	}
}