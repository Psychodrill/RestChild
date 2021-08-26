namespace RestChild.Web.Models
{
	public class CounselorTaskPerformerModel
	{
		/// <summary>
		///     исполнитель
		/// </summary>
		public long PerformerId { get; set; }

		/// <summary>
		///     Тип исполнителя
		/// </summary>
		public long CoworkerType { get; set; }

		/// <summary>
		///     Заезд
		/// </summary>
		public long? BoutId { get; set; }

		/// <summary>
		///     Отряд
		/// </summary>
		public long? PartyId { get; set; }
	}
}