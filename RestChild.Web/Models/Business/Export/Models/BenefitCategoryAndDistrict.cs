using System.Collections.Generic;

namespace RestChild.Web.Models.Business.Export.Models
{
	public class BenefitCategoryAndDistrict
	{
		public string BenefitCategory { get; set; }
		public int TotalCount { get; set; }
		public bool IsTimeOfRestTotal { get; set; }
		public IDictionary<long,int> CountByDistrict { get; set; }
	}
}