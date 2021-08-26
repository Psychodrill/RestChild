using System.ComponentModel.DataAnnotations.Schema;

namespace RestChild.Domain
{
	public partial class Bout
	{
		[NotMapped]
		public int ChildrenCount { get; set; }

		[NotMapped]
		public int VacantChildrenCount { get; set; }

		/// <summary>
		///     Описание заезда
		/// </summary>
		public override string ToString()
		{
			return GroupedTimeOfRest == null ? $"{(Hotels != null ? Hotels.Name : string.Empty)} - с {DateIncome:dd.MM.yyyy} по {DateOutcome:dd.MM.yyyy}"
                : $"{(Hotels != null ? Hotels.Name : string.Empty)} - {GroupedTimeOfRest.Name} (с {DateIncome:dd.MM.yyyy} по {DateOutcome:dd.MM.yyyy})";
		}
	}
}
