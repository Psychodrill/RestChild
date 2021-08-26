using System.Collections.Generic;
using RestChild.Extensions.Filter;
using RestChild.Domain;

namespace RestChild.Web.Models
{
	public class InteragencyRequestListViewModel : CommonPagedList<InteragencyRequest>
	{
		public InteragencyRequestListViewModel() : base(null, 1, 15, 0)
		{
		}

		public InteragencyRequestListViewModel(IEnumerable<InteragencyRequest> pageItems, int pageNumber, int pageSize,
			int totalItemCount) : base(pageItems, pageNumber, pageSize, totalItemCount)
		{
		}

		public string RequestNumber { get; set; }

		public int PageNumberEx
		{
			get { return PageNumber; }
			set { PageNumber = value; }
		}

		public int PageSizeEx
		{
			get { return PageSize; }
			set { PageSize = value; }			
		}
	}
}