using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using RestChild.DAL.RepositoryExtensions;
using RestChild.Extensions.Filter;
using RestChild.Domain;
using RestChild.Web.Properties;

namespace RestChild.Web.Controllers.WebApi
{
	/// <summary>
	///     WebApi-контроллер для поиска, получения, добавления и изменения данных реестра времени отдыха
	/// </summary>
	[Authorize]
	public class WebRestTimeController : WebGenericRestController<TimeOfRest>
	{
		/// <summary>
		///     Поиск времени отдыха
		/// </summary>
		/// <returns>Список найденных времен отдыха</returns>
		public CommonPagedList<TimeOfRest> Get(string name, long? tId, long? yId, int pageNumber)
		{
			var query = UnitOfWork.GetSet<TimeOfRest>().Where(t=>t.IsActive);

			if (!string.IsNullOrEmpty(name))
			{
				name = name.Trim().ToLower();
				query = query.Where(place => place.Name.ToLower().Contains(name));
			}

			if (tId.HasValue)
			{
				query = query.Where(place => place.TypeOfRestId == tId);
			}

			if (yId.HasValue)
			{
				query = query.Where(place => place.YearOfRestId == yId);
			}

			var pageSize = Settings.Default.TablePageSize;
			var startRecord = (pageNumber - 1)*pageSize;
			var totalCount = query.Count();
			var entity = query.OrderBy(place => place.Name).Skip(startRecord).Take(pageSize).ToList();

			return new CommonPagedList<TimeOfRest>(entity, pageNumber, pageSize, totalCount);
		}

		public IEnumerable<TimeOfRest> Get()
		{
			return UnitOfWork.GetSet<TimeOfRest>().ToList().Select(i => new TimeOfRest(i));
		}

		public IEnumerable<TimeOfRest> GetByTypeAndYear(long? typeOfRestId, long? yearOfRestId, string name)
		{
			var tr = UnitOfWork.GetById<TypeOfRest>(typeOfRestId);
			while (tr != null)
			{
				typeOfRestId = tr.ParentId ?? typeOfRestId;
				tr = tr.Parent;
			}

			var query =
				UnitOfWork.GetSet<TimeOfRest>().Where(t => t.TypeOfRestId == typeOfRestId && t.YearOfRestId == yearOfRestId && t.IsActive);
			if (!string.IsNullOrEmpty(name))
			{
				query = query.Where(t => t.Name.ToLower().Contains(name.ToLower()));
			}
			return query.ToList().Select(t => new TimeOfRest(t));
		}
	}
}
