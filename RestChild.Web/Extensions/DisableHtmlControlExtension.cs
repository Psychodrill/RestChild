using System;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using System.Web.UI.WebControls;

namespace RestChild.Web.Extensions
{
    public static class DisableHtmlControlExtension
    {
        /// <summary>
        ///     поле заблокировано
        /// </summary>
        public static MvcHtmlString DisableIf(this MvcHtmlString htmlString, bool disable)
        {
            return htmlString.DisableIf(() => disable);
        }

        /// <summary>
        ///     поле заблокировано
        /// </summary>
        public static MvcHtmlString DisableIf(this MvcHtmlString htmlString, Func<bool> expression)
        {
            if (expression.Invoke())
            {
                var html = htmlString.ToString();
                const string disabled = " disabled=\"disabled\"";
                html = html.Insert(html.IndexOf(">", StringComparison.Ordinal), disabled);
                return new MvcHtmlString(html);
            }

            return htmlString;
        }

        /// <summary>
        ///     поле только для чтения
        /// </summary>
        public static MvcHtmlString ReadOnlyIf(this MvcHtmlString htmlString, bool enable)
        {
            return htmlString.ReadOnlyIf(() => enable);
        }

        /// <summary>
        ///     поле только для чтения
        /// </summary>
        public static MvcHtmlString ReadOnlyIf(this MvcHtmlString htmlString, Func<bool> expression)
        {
            if (expression.Invoke())
            {
                var html = htmlString.ToString();
                const string readOnly = " readonly=\"readonly\"";
                html = html.Insert(html.IndexOf(">", StringComparison.Ordinal), readOnly);
                return new MvcHtmlString(html);
            }

            return htmlString;
        }
    }
}
