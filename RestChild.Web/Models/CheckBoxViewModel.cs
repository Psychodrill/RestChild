using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestChild.Web.Models
{
	public class CheckBoxViewModel<TModel>
	{
		public CheckBoxViewModel()
		{
		}

		public CheckBoxViewModel(TModel data, bool isChecked)
		{
			Data = data;
			IsChecked = isChecked;
		}

		public TModel Data { get; set; }

		public bool IsChecked { get; set; }

		public string Description { get; set; }
	}
}