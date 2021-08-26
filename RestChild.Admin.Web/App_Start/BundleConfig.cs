using System.Web.Configuration;
using System.Web.Optimization;

namespace RestChild.Admin.Web
{
	public class BundleConfig
	{
		// For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
		public static void RegisterBundles(BundleCollection bundles)
		{
			bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
				"~/Scripts/jquery-{version}.js"));

			bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
				"~/Scripts/jquery-ui-1.11.2.js"));

			bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
				"~/Scripts/jquery.validate.js",
				"~/Scripts/jquery.validate.globalize.js",
				"~/Scripts/jquery.validate.unobtrusive.js",
				"~/Scripts/localization/messages_ru.js"));

			bundles.Add(new ScriptBundle("~/bundles/jqueryinputmask").Include(
				"~/Scripts/jquery.inputmask/jquery.inputmask.js",
				"~/Scripts/jquery.inputmask/jquery.inputmask.extensions.js",
				"~/Scripts/jquery.inputmask/jquery.inputmask.date.extensions.js",
				"~/Scripts/jquery.inputmask/jquery.inputmask.numeric.extensions.js",
				"~/Scripts/jquery.inputmask/jquery.inputmask.phone.extensions.js",
				"~/Scripts/jquery.inputmask/jquery.inputmask.regex.extensions.js",
				"~/Scripts/InputMaskConfig.js"));


			bundles.Add(new ScriptBundle("~/bundles/global").Include(
				"~/Scripts/globalize/globalize.js",
				"~/Scripts/globalize/cultures/globalize.culture.ru-RU.js"));

			bundles.Add(new ScriptBundle("~/bundles/globalLastFile").Include(
				"~/Scripts/jquery.validate.globalize*"));

			// Use the development version of Modernizr to develop with and learn from. Then, when you're
			// ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
			bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
				"~/Scripts/modernizr-*"));

			bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
				"~/Scripts/bootstrap.js",
				"~/Scripts/bootstrap-datepicker.js",
				"~/Scripts/bootstrap-datepicker-globalize.js",
				"~/Scripts/respond.js",
				"~/Scripts/bootstrap-dialog.js"));

			bundles.Add(new StyleBundle("~/Content/css-general").Include(
				"~/Content/bootstrap.css",
				"~/Content/bootstrap-datepicker3.css",
				"~/Content/voffset.css",
				"~/Content/site.css",
				"~/Content/VerticalTextArea.css",
				"~/Content/bootstrap-dialog.css"));

			bundles.Add(new ScriptBundle("~/bundles/moment").Include(
				"~/Scripts/moment.js",
				"~/Scripts/moment-with-locales.js"));

			bundles.Add(new ScriptBundle("~/bundles/layoutScripts").Include(
				"~/Scripts/assets/InfoPanel.js",
				"~/Scripts/AlertPanel.js",
				"~/Scripts/Shared.js",
				"~/Scripts/cycle.js"
				));
		}
	}
}
