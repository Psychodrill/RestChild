using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using RestChild.Extensions.Filter;
using RestChild.Domain;
using RestChild.Web.Properties;

namespace RestChild.Web.Controllers.WebApi
{
	/// <summary>
	///     WebApi-контроллер для поиска, получения, добавления и изменения данных реестра видов льгот
	/// </summary>
	[Authorize]
	public class WebBenefitTypeController : WebGenericRestController<BenefitType>
	{
		/// <summary>
		///     Поиск вида льготы
		/// </summary>
		/// <param name="name">Название</param>
		/// <param name="pageNumber">Номер страницы (начиная с 1)</param>
		/// <returns>Список найденных регионов отдыха</returns>
		public CommonPagedList<BenefitType> Get(string name, int pageNumber)
		{
			IQueryable<BenefitType> query = UnitOfWork.GetSet<BenefitType>().Where(place => place.Name.Contains(name));

			int pageSize = Settings.Default.TablePageSize;
			int startRecord = (pageNumber - 1)*pageSize;
			int totalCount = query.Count();
			List<BenefitType> entity = query.OrderBy(place => place.Name).Skip(startRecord).Take(pageSize).ToList();

			return new CommonPagedList<BenefitType>(entity, pageNumber, pageSize, totalCount);
		}

		public IEnumerable<BenefitType> Get()
		{
			return UnitOfWork.GetSet<BenefitType>().ToArray().Select(i => new BenefitType(i)).ToList();
		}
	}
}