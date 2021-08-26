namespace RestChild.Web.Models
{
	public class OrganizationSearchModel
	{
		public string Name { get; set; }
		public string ParentId { get; set; }
		public int PageNumber { get; set; }
		public int OrganizationType { get; set; }

		public long? StateDistrictId { get; set; }
		public long? OivId { get; set; }

		/// <summary>
		/// тип организации изменился
		/// </summary>
		public bool ChangeOrgType { get; set; }
	}
}
