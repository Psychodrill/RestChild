using System.Collections.Generic;
using RestChild.Domain;
using RestChild.Extensions.Filter;

namespace RestChild.Web.Models
{
	/// <summary>
	/// модель поиска
	/// </summary>
	public class PlaceOfRestListModel : CommonPagedList<PlaceOfRest>
	{
		public PlaceOfRestListModel() 
		{
			ActiveOnly = true;
		}

		public PlaceOfRestListModel(IEnumerable<PlaceOfRest> pageItems, int pageNumber, int pageSize, int totalItemCount, PlaceOfRestListModel model) : base(pageItems, pageNumber, pageSize, totalItemCount)
		{
			Name = model?.Name;
			ActiveOnly = model?.ActiveOnly ?? true;
			GroupId = model?.GroupId;
			NotForSelect = model?.NotForSelect ?? false;
			NewPageNumber = model?.NewPageNumber ?? 1;
		}

		/// <summary>
		///     наименование
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// только активные
		/// </summary>
		public bool ActiveOnly { get; set; }

		/// <summary>
		/// группа
		/// </summary>
		public long? GroupId { get; set; }

		/// <summary>
		/// Групповой элемент
		/// </summary>
		public bool NotForSelect { get; set; }

		/// <summary>
		/// новый номер страницы
		/// </summary>
		public int NewPageNumber { get; set; }

		/// <summary>
		/// группы
		/// </summary>
		public List<PlaceOfRest> Groups { get; set; }

	}
}