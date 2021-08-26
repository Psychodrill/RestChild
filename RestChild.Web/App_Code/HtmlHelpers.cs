using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using RestChild.Comon;
using RestChild.Comon.Enumeration;

namespace RestChild.Web.App_Code
{
	public static class HtmlHelpers
	{
		public static MvcHtmlString DisableIf(this MvcHtmlString htmlString, Func<bool> expression)
		{
			if (expression.Invoke())
			{
				var html = htmlString.ToString();
				const string disabled = "\"disabled\"";
				html = html.Insert(html.IndexOf(">", StringComparison.Ordinal), " disabled= " + disabled);
				return new MvcHtmlString(html);
			}
			return htmlString;
		}

		public static MvcHtmlString ReadonlyIf(this MvcHtmlString htmlString, Func<bool> expression)
		{
			if (expression.Invoke())
			{
				var html = htmlString.ToString();
				const string disabled = "\"readonly\"";
				html = html.Insert(html.IndexOf(">", StringComparison.Ordinal), " readonly= " + disabled);
				return new MvcHtmlString(html);
			}
			return htmlString;
		}


		public static IHtmlString DropDownListForEnum<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper,
			Expression<Func<TModel, TEnum>> expression, string optionLabel, object htmlAttributes)
		{
			var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
			var enumType = GetNonNullableModelType(metadata);
			var values = Enum.GetValues(enumType).Cast<TEnum>();

			var items =
				from value in values
				select new SelectListItem
				{
					Text = value.GetDisplayValue(),
					Value = value.ToString(),
					Selected = value.Equals(metadata.Model)
				};

			return htmlHelper.DropDownListFor(expression, items, optionLabel, htmlAttributes);
		}

		private static Type GetNonNullableModelType(ModelMetadata modelMetadata)
		{
			var modelType = modelMetadata.ModelType;

			var underlyingType = Nullable.GetUnderlyingType(modelType);
			if (underlyingType != null)
			{
				modelType = underlyingType;
			}
			return modelType;
		}
	}
}
