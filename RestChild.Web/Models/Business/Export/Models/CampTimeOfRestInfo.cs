using System.Collections.Generic;

namespace RestChild.Web.Models.Business.Export.Models
{
	public class CampTimeOfRestInfo
	{
		public string TimeOfRestName { get; set; }
		public string HotelName { get; set; }
		public IDictionary<long, int> MaleCount { get; set; }
		public IDictionary<long, int> FemaleCount { get; set; }
		public int TotalTimeOfRestMaleCount { get; set; }
		public int TotalTimeOfRestFemaleCount { get; set; }
	}

	public class FamilyTimeOfRestInfo
	{
		public string TimeOfRestName { get; set; }
		public string HotelName { get; set; }
		public IDictionary<long, int> MaleCount { get; set; }
		public IDictionary<long, int> FemaleCount { get; set; }
		public int AttendantsMaleCount { get; set; }
		public int AttendantsFemaleCount { get; set; }
	}
}