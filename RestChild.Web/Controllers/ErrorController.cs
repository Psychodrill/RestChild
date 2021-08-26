using System.Web.Mvc;

namespace RestChild.Web.Controllers
{
	[HandleError]
	public class ErrorController : Controller
	{
		[AllowAnonymous]
		public ActionResult Error()
		{
			return View();
		}
	}
}