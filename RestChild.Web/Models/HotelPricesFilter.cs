namespace RestChild.Web.Models
{
	/// <summary>
	/// Фильтр для поиска на вкладке "Матьрица цен" формы "Оздоровительная организация".
	/// </summary>
	public class HotelPricesFilter
	{
		public HotelPricesFilter()
		{
			PageNumber = 1;
		}

		public long HotelId { get; set; }
		public int PageNumber { get; set; }
		public int? Age { get; set; }
		public long? TypeOfRoomId { get; set; }
		public long? AccommodationId { get; set; }
		public long? DiningOptionsId { get; set; }
		public string Date { get; set; }
		public string Price { get; set; }
	}
}