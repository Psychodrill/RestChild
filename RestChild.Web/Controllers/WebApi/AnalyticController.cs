using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Web.Http;
using RestChild.Comon;
using RestChild.DAL.RepositoryExtensions;
using RestChild.Domain;

namespace RestChild.Web.Controllers.WebApi
{
	[Authorize]
	public class AnalyticController : BaseController
	{
		public IList<AnalyticsViewRow> GetRowsForMainPage()
		{
			return UnitOfWork.GetSet<AnalyticsViewRow>().OrderBy(a => a.Id).ToList();
		}

		public IList<ReportSheet> GetReportSheets()
		{
			var codes =
				UnitOfWork.GetSet<ReportSheet>()
					.Select(r => r.CodeAccess.ToString().ToLower()).ToList()
					.Intersect(Security.GetSecurity().Select(s => s.ToLower()));

			var reports =
				UnitOfWork.GetSet<ReportSheet>()
					.Where(s => codes.Contains(s.CodeAccess.ToString()));

			return reports.ToList();
		}
	}
}