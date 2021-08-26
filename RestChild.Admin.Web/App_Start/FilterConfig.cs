using System.Web.Mvc;
using RestChild.Admin.Web.Common;

namespace RestChild.Admin.Web
{
   public class FilterConfig
   {
      public static void RegisterGlobalFilters(GlobalFilterCollection filters)
      {
         filters.Add(new HandleErrorAttribute());
         //filters.Add(new AuthorizeAttribute());
         filters.Add(new Security.AuthorizeWithAnonTimeoutAttribute());
         filters.Add(new AntiForgeryTokenFilter());
      }
   }
}
