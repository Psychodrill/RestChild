using System.ComponentModel.DataAnnotations.Schema;

namespace RestChild.Domain
{
	public partial class ReportTableHead
	{
		[NotMapped]
		public string Key { get; set; }
	}
}