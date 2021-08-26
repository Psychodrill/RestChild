using System.Collections.Generic;
using RestChild.Extensions.Filter;
using RestChild.Domain;

namespace RestChild.Web.Models.Limits
{
	/// <summary>
	///     модель для поиска детей.
	/// </summary>
	public class GiftedChildrenModel : CommonPagedList<Child>
	{
		public GiftedChildrenModel()
		{			
		}

		public GiftedChildrenModel(IEnumerable<Child> pageItems, int pageNumber, int pageSize, int totalItemCount)
			: base(pageItems, pageNumber, pageSize, totalItemCount)
		{			
		}

		/// <summary>
		///     Имя ребенка
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		///     Год
		/// </summary>
		public long? YearId { get; set; }

		/// <summary>
		///     Ведомство
		/// </summary>
		public long? VedomstvoId { get; set; }


		/// <summary>
		/// искать влюкченные 
		/// </summary>
		public bool Included { get; set; }

		/// <summary>
		/// искать исключенные
		/// </summary>
		public bool Excluded { get; set; }


		/// <summary>
		///     список возможных годов.
		/// </summary>
		public List<YearOfRest> ListOfYears { get; set; }

		/// <summary>
		///     список ведомств
		/// </summary>
		public List<LimitOnVedomstvo> Vedomstvos { get; set; }

		/// <summary>
		/// документы для детей
		/// </summary>
		public IList<DocumentType> DocumentTypesChild { get; set; }
	}
}