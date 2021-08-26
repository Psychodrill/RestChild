using System;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace RestChild.Web.Extensions
{
   public static class DisableHtmlControlExtension
   {
      public static MvcHtmlString DisableIf(this MvcHtmlString htmlString, Func<bool> expression)
      {
         if (expression.Invoke())
         {
            var html = htmlString.ToString();
            const string disabled = "\"disabled\"";
            html = html.Insert(html.IndexOf(">",
              StringComparison.Ordinal), " disabled= " + disabled);
            return new MvcHtmlString(html);
         }
         return htmlString;
      }

      public static MvcHtmlString ReadOnlyIf(this MvcHtmlString htmlString, Func<bool> expression)
      {
         if (expression.Invoke())
         {
            var html = htmlString.ToString();
            const string _readonly = "\"readonly\"";
            html = html.Insert(html.IndexOf(">",
              StringComparison.Ordinal), " readonly= " + _readonly);
            return new MvcHtmlString(html);
         }
         return htmlString;
      }

   }
}
