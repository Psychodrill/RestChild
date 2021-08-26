using System.Collections.Generic;
using System.Linq;
using RestChild.Mobile.Domain;

namespace RestChild.Web.Controllers.WebApi
{
    /// <summary>
    ///     контроллер для истории
    /// </summary>
    public class HistoryMobileController : BaseMobileController
    {
        /// <summary>
        ///     получить историю
        /// </summary>
        public ICollection<History> GetByHistoryLink(long? historyLinkId)
        {
            return
                MobileUw.GetSet<History>()
                    .Where(h => h.LinkId == historyLinkId)
                    .OrderByDescending(h=>h.DateChange)
                    .ToList()
                    .Select(h => new History
                    {
                        Commentary = h.Commentary,
                        Id = h.Id,
                        DateChange = h.DateChange,
                        EventCode = h.EventCode,
                        AccountExternalId = h.AccountExternalId,
                        AccountId = h.AccountId,
                        AuthorString = h.AccountExternal?.Name ?? h.Account?.Name ?? "-"
                    })
                    .ToList();
        }
    }
}
