using System.Web.Mvc;
using System.Web.Routing;

namespace RestChild.Admin.Web
{
   public class RouteConfig
   {
      public static void RegisterRoutes(RouteCollection routes)
      {
         routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
         routes.IgnoreRoute("{resource}.ashx/{*pathInfo}");
         routes.IgnoreRoute("exchange/{*pathInfo}");

         routes.MapRoute(
            name: "Default",
            url: "{controller}/{action}/{id}",
            defaults: new {controller = "Home", action = "Index", id = UrlParameter.Optional}
         );
      }
   }
}
