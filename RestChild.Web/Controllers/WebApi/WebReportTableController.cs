using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RestChild.Comon;
using RestChild.Comon.Enumeration;
using RestChild.Extensions.Filter;
using RestChild.DAL.RepositoryExtensions;
using RestChild.Domain;
using RestChild.Web.Models;
using RestChild.Web.Properties;

namespace RestChild.Web.Controllers.WebApi
{
	[Authorize]
	public class WebReportTableController : WebGenericRestController<ReportTable>
	{
		/// <summary>
		/// Получение таблицы отчета по профильным лагерям для ОИВ
		/// </summary>
		/// <param name="organisationId"></param>
		/// <returns></returns>
		internal SpecializedCampsReportModel GetReportTableForSpecializedCampsByOiv(long organisationId)
		{
			var organisation = UnitOfWork.GetById<Organization>(organisationId);
			var table = UnitOfWork.GetSet<ReportTable>().FirstOrDefault(t => t.Name == "specialized-camps-report-organisation-" + organisationId);
			return new SpecializedCampsReportModel()
						{
							Organization = organisation,
							ReportTable = table
						};
		}
	}
}