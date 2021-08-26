using System.Collections.Generic;
using RestChild.Extensions.Filter;

namespace RestChild.Web.Models.PedParty
{
	public class PedPartyPagedList : CommonPagedList<Domain.PedParty>
	{
		public PedPartyPagedList(IEnumerable<Domain.PedParty> pageItems, int pageNumber, int pageSize, int totalItemCount)
			: base(pageItems, pageNumber, pageSize, totalItemCount)
		{
		}
	}
}