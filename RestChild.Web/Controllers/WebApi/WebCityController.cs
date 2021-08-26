using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using RestChild.Extensions.Filter;
using RestChild.Domain;
using RestChild.Web.Properties;

namespace RestChild.Web.Controllers.WebApi
{
	[Authorize]
	public class WebCityController : WebGenericRestController<City>
	{
		public CommonPagedList<City> Get(CityFilterModel filter)
		{
			var pageSize = Settings.Default.TablePageSize;
			var pageNumber = filter != null ? filter.PageNumber : 1;
			var startRecord = (pageNumber - 1)*pageSize;
			IQueryable<City> query = UnitOfWork.GetSet<City>();
			if (filter != null)
			{
				if (!string.IsNullOrEmpty(filter.Name))
				{
					query = query.Where(c => c.Name.ToLower().Contains(filter.Name.ToLower()));
				}
				if (filter.IsActive.HasValue && (filter.IsActive ?? false))
				{
					query = query.Where(c => c.IsActive);
				}
				if (filter.IsActive.HasValue && (filter.HaveAero ?? false))
				{
					query = query.Where(c => c.HaveAero);
				}
				if (filter.HaveRailway.HasValue && (filter.HaveRailway ?? false))
				{
					query = query.Where(c => c.HaveRailway);
				}
			}

			var totalCount = query.Count();
			var entity = query.OrderBy(c => c.Name).Skip(startRecord).Take(pageSize).ToList().Select(c => new City(c)).ToList();
			return new CommonPagedList<City>(entity, pageNumber, pageSize, totalCount);
		}

		public List<City> GetActive()
		{
			return UnitOfWork.GetSet<City>().Where(c => c.IsActive).OrderBy(c=>c.Name).ToList().Select(c => new City(c)).ToList();
		}
	}
}
