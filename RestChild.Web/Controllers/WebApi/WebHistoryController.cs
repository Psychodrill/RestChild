using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using RestChild.Domain;

namespace RestChild.Web.Controllers.WebApi
{
	[Authorize]
	public class WebHistoryController : WebGenericRestController<History>
	{
		public ICollection<History> GetByHistoryLink(long? historyLinkId)
		{
			return
				UnitOfWork.GetSet<History>()
					.Where(h => h.LinkId == historyLinkId)
					.ToList()
					.Select(h => new History(h) {Account = new Account(h.Account)})
					.ToList();
		}

		internal HistoryLink CreateHistoryLink()
		{
			return new HistoryLink();
		}

		internal void InsertHistoryItem(HistoryLink historyLink, History history)
		{
			if (historyLink != null)
			{
				if (historyLink.Historys == null)
				{
					historyLink.Historys = new List<History>();
				}

				history.Id = 0;
				history.LinkId = 0;
				history.Link = null;
				history.AccountId = history.AccountId ?? Security.GetCurrentAccountId();
				historyLink.Historys.Add(history);
			}
		}
	}
}