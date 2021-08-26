using System.Collections.Generic;
using System.Runtime.Serialization;
using RestChild.Extensions.Filter;
using RestChild.Domain;

namespace RestChild.Web.Models.Limits
{
	/// <summary>
	/// модель для списка организаций
	/// </summary>
	[DataContract]
	public class ListOfChildsListModel : CommonPagedList<ListOfChilds>
	{
		public ListOfChildsListModel()
		{			
		}

		public ListOfChildsListModel(IEnumerable<ListOfChilds> pageItems, int pageNumber, int pageSize, int totalItemCount)
			: base(pageItems, pageNumber, pageSize, totalItemCount)
		{			
		}

		/// <summary>
		/// года отдыха
		/// </summary>
		public List<YearOfRest> YearsOfRest { get; set; }

		/// <summary>
		/// год отдыха
		/// </summary>
		[DataMember]
		public long? YearOfRestId { get; set; }

		/// <summary>
		/// наименование квоты организации
		/// </summary>
		public string OrganizationName { get; set; }

		/// <summary>
		/// ИД организации
		/// </summary>
		[DataMember]
		public long? OrganizationId { get; set; }

		/// <summary>
		/// блокировка выбора организации
		/// </summary>
		public bool OnlyOneOrganization { get; set; }

		/// <summary>
		/// ИД квоты организации
		/// </summary>
		[DataMember]
		public long? LimitOnOrganizationId { get; set; }

		/// <summary>
		/// квота на организацию
		/// </summary>
		public LimitOnOrganization LimitOnOrganization { get; set; }

		/// <summary>
		/// статус
		/// </summary>
		public ViewModelState State { get; set; }

		/// <summary>
		/// Код действия.
		/// </summary>
		[DataMember]
		public string StringStateCode { get; set; }

		/// <summary>
		/// Код комментарий.
		/// </summary>
		[DataMember]
		public string StringCommentaryCode { get; set; }


		/// <summary>
		/// список ошибок
		/// </summary>
		public List<string> Errors { get; set; }

	}
}