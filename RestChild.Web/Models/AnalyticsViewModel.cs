using System.Collections.Generic;
using RestChild.Domain;

namespace RestChild.Web.Models
{
	public class AnalyticsViewModel
	{
		/// <summary>
		///     общая инфомация по системе.
		/// </summary>
		public IList<AnalyticsViewRow> Rows { get; set; }

		/// <summary>
		/// Отчеты
		/// </summary>
		public IList<ReportSheet> ReportSheet { get; set; }

		/// <summary>
		/// Показывать скрытые таблицы
		/// </summary>
		public bool ShowHiddenTables { get; set; }
	}
}