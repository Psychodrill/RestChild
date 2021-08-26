using System;

namespace RestChild.Admin.Web
{
	using System.Reflection;
	using System.Security.Claims;
	using System.Web;
	using System.Web.Helpers;
	using System.Web.Http;
	using System.Web.Mvc;
	using System.Web.Optimization;
	using System.Web.Routing;
	using System.Web.SessionState;

	using Castle.Core.Internal;

	using RestChild.Booking.Logic;
	using RestChild.Comon;
	using RestChild.DAL;
	using RestChild.Admin.Web.Controllers;

	public class MvcApplication : HttpApplication
    {
		public static string Version { get; private set; }

		protected void Application_PostAuthorizeRequest()
		{
			HttpContext.Current.SetSessionStateBehavior(SessionStateBehavior.Required);
		}

        protected void Application_Start()
        {
	        try
	        {
		        var attr = typeof (MvcApplication).Assembly.GetAttribute<AssemblyInformationalVersionAttribute>();
		        Version = attr?.InformationalVersion;

		        AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.Sid;
		        var container = WindsorHolder.Instance.Container;

		        GlobalConfiguration.Configuration.DependencyResolver = new WindsorResolver(container);
		        var controllerFactory = new WindsorControllerFactory(container.Kernel);
		        ControllerBuilder.Current.SetControllerFactory(controllerFactory);

		        AreaRegistration.RegisterAllAreas();
		        GlobalConfiguration.Configure(WebApiConfig.Register);
		        FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
		        RouteConfig.RegisterRoutes(RouteTable.Routes);
		        BundleConfig.RegisterBundles(BundleTable.Bundles);
	        }
	        catch (Exception ex)
	        {
		        log4net.LogManager.GetLogger(typeof(MvcApplication)).Error("Ошибка запуска приложения", ex);
		        throw ex;
	        }
        }
    }
}
