using System.Web.Mvc;
using RestChild.Comon;
using RestChild.Web.Controllers.WebApi;
using RestChild.Web.Models;

namespace RestChild.Web.Controllers
{
	[HandleError]
	[Authorize]
	public class HomeController : BaseController
	{
		public AnalyticController ApiController { get; set; }

		public override void SetUnitOfWorkInRefClass(IUnitOfWork unitOfWork)
		{
			base.SetUnitOfWorkInRefClass(unitOfWork);
			ApiController.SetUnitOfWorkInRefClass(unitOfWork);
		}

		public ActionResult Index()
		{
			SetUnitOfWorkInRefClass(UnitOfWork); 
			return View(new AnalyticsViewModel
			{
				Rows = ApiController.GetRowsForMainPage(),
				ReportSheet = ApiController.GetReportSheets(),
				ShowHiddenTables = false
			});
		}
	}
}