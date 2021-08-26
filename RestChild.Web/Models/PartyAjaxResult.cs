using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestChild.Web.Models
{
	public class PartyAjaxResult
	{
		public bool HasError { get; set; }
		public string ErrorMessage { get; set; }
		public long OpenedPartyId { get; set; }
		public string BoutLastUpdateTick { get; set; }
	}
}
