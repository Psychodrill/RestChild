using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using RestChild.Comon;

namespace RestChild.Web.App_Code
{
	public static class SelectListHelper
	{
		public static IList<SelectListItem> Create<T>(IEnumerable<T> collection, Func<T, SelectListItem> func, string defaultText = "-- Не выбрано --")
		{
			collection = collection ?? new List<T>();
			var selectList = collection.Select(func).InsertAt(new SelectListItem() { Value = string.Empty, Text = defaultText });
			return selectList;
		}
	}
}
