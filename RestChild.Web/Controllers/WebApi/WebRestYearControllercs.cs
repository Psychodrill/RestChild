using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using RestChild.Domain;

namespace RestChild.Web.Controllers.WebApi
{
	/// <summary>
	///     WebApi-контроллер для поиска, получения, добавления и изменения данных реестра года отдыха
	/// </summary>
	[Authorize]
	public class WebRestYearController : BaseController
	{
		public IEnumerable<YearOfRest> Get()
		{
			return UnitOfWork.GetSet<YearOfRest>().OrderBy(y=>y.Year).ToList();
		}

		public YearOfRest GetCurrentYear()
		{
			return UnitOfWork.GetSet<YearOfRest>().FirstOrDefault(y => y.Year == DateTime.Today.Year);
		}

		public long? GetCurrentYearId()
		{
			var year = GetCurrentYear();
			return year != null ? (long?)year.Id : null;
		}
	}
}
