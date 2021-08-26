using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using RestChild.Web.Controllers.WebApi;

namespace RestChild.Web.Controllers
{
	public class ReportController : BaseController
	{
		public WebReportTableController TableController { get; set; }

		public ActionResult GetSpecializedCampsReportByOiv(long oivId)
		{
			var table = TableController.GetReportTableForSpecializedCampsByOiv(oivId);
			if (table == null)
			{
				return RedirectToAvalibleAction();
			}

			return View(table);
		}
	}
}