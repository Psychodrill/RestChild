using System.ComponentModel.DataAnnotations.Schema;

namespace RestChild.Domain
{
	public partial class DirectoryFlights
	{
		[NotMapped]
		public int RestManCount { get; set; }
	}
}