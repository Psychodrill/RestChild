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
	///     WebApi-контроллер для поиска, получения, добавления и изменения данных реестра типов отдыха
	/// </summary>
	[Authorize]
	public class WebRestTypeController : WebGenericRestController<TypeOfRest>
	{
		/// <summary>
		///     Поиск типов отдыха
		/// </summary>
		/// <param name="name">Название</param>
		/// <param name="activeOnly">Искать только активные</param>
		/// <param name="pageNumber">Номер страницы (начиная с 1)</param>
		/// <returns>Список найденных видов отдыха</returns>
		public CommonPagedList<TypeOfRest> Get(string name, bool activeOnly, int pageNumber)
		{
			name = (name ?? string.Empty).ToLower();
			IQueryable<TypeOfRest> query = UnitOfWork.GetSet<TypeOfRest>().Where(place => place.Name.ToLower().Contains(name));
			if (activeOnly)
			{
				query = query.Where(place => place.IsActive);
			}

			int pageSize = Settings.Default.TablePageSize;
			int startRecord = (pageNumber - 1)*pageSize;
			int totalCount = query.Count();
			List<TypeOfRest> entity = query.OrderBy(place => place.Name).Skip(startRecord).Take(pageSize).ToList();

			return new CommonPagedList<TypeOfRest>(entity, pageNumber, pageSize, totalCount);
		}

		public IEnumerable<TypeOfRest> Get()
		{
			return UnitOfWork.GetSet<TypeOfRest>().Where(t=>t.IsActive && !t.Commercial).ToList();
		}

		public TypeOfRest GetById(long id)
		{
			return new TypeOfRest(UnitOfWork.GetById<TypeOfRest>(id), 1);
		}

		public IEnumerable<TypeOfRest> GetForTour()
		{
			return UnitOfWork.GetSet<TypeOfRest>().Where(t => t.IsActive && t.ForTour && !t.Commercial).ToList();
		}

		public IEnumerable<TypeOfRest> GetForProduct()
		{
			return UnitOfWork.GetSet<TypeOfRest>().Where(t => t.IsActive && t.ForTour && t.Commercial).OrderBy(t=>t.Name).ToList();
		}
	}
}
