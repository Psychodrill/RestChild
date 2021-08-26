using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestChild.Web.Models.Business.Export.Models
{
	public class BenefitsByBoatCompletenessInfo
	{
		public string HotelName { get; set; }
		public string TimeOfRest { get; set; }
		public int Planned { get; set; }
		public int Booked { get; set; }
		public int Fact { get; set; }
		public float PlannedPercent { get; set; }
		public float BookedPercent { get; set; }
	}

	public class BenefitsFamilyRestByBoatCompletenessInfo
	{
		public string HotelName { get; set; }
		public string TimeOfRest { get; set; }
		public int Planned { get; set; }
		public int BookedTotal { get; set; }
		public float PlannedPercent { get; set; }
		public int BookedChild { get; set; }
		public int BookedAttendant { get; set; }
		public int FactChild { get; set; }
		public int FactAttendant { get; set; }
		
	}
}