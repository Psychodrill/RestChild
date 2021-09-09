using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using RestChild.Comon;
using RestChild.DAL.RepositoryExtensions;
using RestChild.Domain;
using RestChild.Admin.Web.Controllers.WebApi;
using RestChild.Admin.Web.Models;
using RestChild.Extensions.Filter;
using RestChild.Mobile.DAL;
using RestChild.Mobile.DAL.Enum;
using RestChild.Mobile.Domain;
using History = RestChild.Mobile.Domain.History;

namespace RestChild.Admin.Web.Controllers
{
    [Authorize]
    public class GiftCancelController : BaseMobileController
    {
        public ActionResult History(GiftCancelHistoryFilter filter)
        {
            filter = filter ?? new GiftCancelHistoryFilter();

            var qweqwe = MobileUw.GetSet<GiftReserved>()
                .Where(g => g.StateId == StateEnum.GiftReserved.Refusal).Select(a => a.Link).ToList();

            var links = MobileUw.GetSet<GiftReserved>()
                .Where(g => g.StateId == StateEnum.GiftReserved.Refusal).Select(x => x.LinkId).ToList();

            var query = MobileUw.GetSet<History>().Where(h => links.Contains(h.LinkId));

            var tc = query.Count();
            var pageSize = 10;
            var pageNumber = filter.PageNumber;
            var startRecord = (pageNumber - 1) * pageSize;

            query = query.OrderByDescending(t => t.Id).Skip(startRecord).Take(pageSize);

            filter.Result = new CommonPagedList<History>(query.ToList(), pageNumber, pageSize, tc);

            return View(filter);
        }
    }
}
