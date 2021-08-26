using System.Collections.Generic;
using RestChild.Domain;

namespace RestChild.Web.Models.Limits
{
	/// <summary>
	///     Модель по списку лимитов организации
	/// </summary>
	public class OrganizationModel
	{
		public long? OrganizationId { get; set; }
		public string OrganizationName { get; set; }
		public bool OnlyOneOrganization { get; set; }
		public long? YearOfRestId { get; set; }

		/// <summary>
		///     список возможных годов.
		/// </summary>
		public List<YearOfRest> ListOfYears { get; set; }

		/// <summary>
		///     список
		/// </summary>
		public GroupedLimitItemModel Items { get; set; }
	}
}