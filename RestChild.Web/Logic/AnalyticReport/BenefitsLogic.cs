using System.Linq;
using RestChild.Comon;
using RestChild.Domain;
using RestChild.Extensions.Filter;

namespace RestChild.Web.Logic.AnalyticReport
{
	public abstract class BenefitsLogic : ILogic
	{
		public IUnitOfWork UnitOfWork { get; set; }
		public AnalyticReportFilterRepository RightsRepository { get; set; }

		protected IQueryable<Child> DateStartRangeFilter(AnalyticReportFilter filter, IQueryable<Child> query)
		{
			if (filter.DateStartBegin != null && filter.DateStartEnd != null)
			{
				var beginMonth = filter.DateStartBegin.Value.Month;
				var beginDay = filter.DateStartBegin.Value.Day;
				var endMonth = filter.DateStartEnd.Value.Month;
				var endDay = filter.DateStartEnd.Value.Day;

				query = query.Where(i => i.Request.TimeOfRest.Month >= beginMonth
					                        && i.Request.TimeOfRest.DayOfMonth >= beginDay
					                        && i.Request.TimeOfRest.Month <= endMonth
					                        && i.Request.TimeOfRest.DayOfMonth <= endDay);
			}
			return query;
		}

		protected IQueryable<Child> AddFilters(IQueryable<Child> query, AnalyticReportFilter filter)
		{
			if (filter.YearOfRestId.HasValue)
				query = query.Where(i => i.Request.YearOfRestId == filter.YearOfRestId.Value);

			if (filter.BenefitTypeId.HasValue)
				query = query.Where(i => i.BenefitTypeId == filter.BenefitTypeId.Value);

			if (filter.DistrictId.HasValue)
				query = query.Where(i => i.Address.BtiDistrictId == filter.DistrictId);

			if (filter.HotelId.HasValue)
				query = query.Where(i => i.Request.Tour.HotelsId == filter.HotelId);

			if (filter.YearOfBirthDateBegin.HasValue && filter.YearOfBirthDateEnd.HasValue)
				query =
					query.Where(
						i =>
							i.DateOfBirth.Value.Year >= filter.YearOfBirthDateBegin.Value &&
							i.DateOfBirth.Value.Year <= filter.YearOfBirthDateEnd.Value);

			if (filter.TimeOfRestId.HasValue)
				query = query.Where(i => i.Request.TimeOfRest.GroupedTimeOfRestId == filter.TimeOfRestId.Value);

			if (filter.DateStartBegin.HasValue && filter.DateStartEnd.HasValue)
			{
				query = DateStartRangeFilter(filter, query);
			}

			return query;
		}

	}
}
