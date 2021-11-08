using System;
using System.Reflection;
using System.Security.Claims;
using System.Web;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.SessionState;
using log4net;
using RestChild.Comon;
using RestChild.DAL;
using RestChild.Web.Controllers;

namespace RestChild.Web
{
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
                var version = Assembly.GetExecutingAssembly().GetName().Version;
                var buildDate = new DateTime(2000, 1, 1).AddDays(version.Build).AddSeconds(version.Revision * 2);
                /*
                var gitVersion = Properties.Resources.CurrentCommit;
                
                if(string.IsNullOrWhiteSpace(gitVersion) || gitVersion.Length < 8)
                {
                    gitVersion = version.ToString();
                }
                else
                {
                    gitVersion = gitVersion.Substring(0, 8);
                }

                var displayableVersion = $"{gitVersion}";
                Version = displayableVersion;*/

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

                using (var uw = new UnitOfWork())
                {
                    Booking.Logic.Booking.FillDecodeTypeOfRest(uw);
                }
            }
            catch (Exception ex)
            {
                LogManager.GetLogger(typeof(MvcApplication)).Error("Ошибка запуска приложения", ex);
                throw;
            }
        }
    }
}
