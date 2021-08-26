using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using RestChild.Domain;

namespace RestChild.Web.Models
{
	/// <summary>
	/// Модель для таблицы отчета по отдыхающим в профильных лагерях
	/// </summary>
	public class SpecializedCampsReportModel
	{
		public ReportTable ReportTable { get; set; }

		public Organization Organization { get; set; }
	}
}