using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using RestChild.Extensions.Filter;
using RestChild.Domain;
using RestChild.Web.Properties;

namespace RestChild.Web.Controllers.WebApi
{
	/// <summary>
	///     WebApi-контроллер для поиска, получения, добавления и изменения данных реестра статусов отношения к ребенку
	/// </summary>
	[Authorize]
	public class WebStatusByChildController : WebGenericRestController<StatusByChild>
	{
		/// <summary>
		///     Поиск статусов отношения к ребенку
		/// </summary>
		/// <param name="name">Название</param>
		/// <param name="pageNumber">Номер страницы (начиная с 1)</param>
		/// <returns>Список найденных статусов по отношению к ребенку</returns>
		public CommonPagedList<StatusByChild> Get(string name, int pageNumber)
		{
			var query = UnitOfWork.GetSet<StatusByChild>().Where(place => place.Name.Contains(name));

			var pageSize = Settings.Default.TablePageSize;
			var startRecord = (pageNumber - 1)*pageSize;
			var totalCount = query.Count();
			var entity = query.OrderBy(place => place.Name).Skip(startRecord).Take(pageSize).ToList();

			return new CommonPagedList<StatusByChild>(entity, pageNumber, pageSize, totalCount);
		}

		public IEnumerable<StatusByChild> Get()
		{
			return UnitOfWork.GetSet<StatusByChild>().ToList();
		}
	}
}