using System.Web.Mvc;

namespace RestChild.Admin.Web.Common
{
	public class AntiForgeryTokenFilter : FilterAttribute, IExceptionFilter
	{
		public void OnException(ExceptionContext filterContext)
		{
			if (filterContext.Exception is HttpAntiForgeryException)
			{
				filterContext.Result = new RedirectResult("~/");
				filterContext.ExceptionHandled = true;
			}
		}
	}
}
