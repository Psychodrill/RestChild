using System;
using System.Collections.Generic;

namespace RestChild.Web.Models.Business.Export.Models
{
	public class AnalyticReportInfo<T>
	{
		//нужен для отступа при работе с таблицей
		public int ParametersCount { get; set; }
		public IDictionary<long,string> SubHeaders { get; set; }
		public IEnumerable<T> Data { get; set; }
	}
}