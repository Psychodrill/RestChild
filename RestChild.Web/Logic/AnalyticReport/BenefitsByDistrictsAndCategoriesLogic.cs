using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using RestChild.Comon;
using RestChild.Comon.Enumeration;
using RestChild.Comon.ToExcel;
using RestChild.Domain;
using RestChild.Extensions.Filter;
using RestChild.Web.Models.Business.Export.AnalyticReports;
using RestChild.Web.Models.Business.Export.Models;

namespace RestChild.Web.Logic.AnalyticReport
{
	public class BenefitsByDistrictsAndCategoriesLogic : BenefitsLogic
	{
		private static int _unknownDistrictKey = Int32.MaxValue;

		private AnalyticReportInfo<BenefitCategoryAndDistrict> GetReportInfo(AnalyticReportFilter filter, Child[] data,bool withParents)
		{
			var btiDistricts = UnitOfWork.GetSet<BtiDistrict>().ToArray()
				.Select(i => new BtiDistrict(i))
				.OrderBy(i => i.Id)
				.ToArray();

			var result = new AnalyticReportInfo<BenefitCategoryAndDistrict>
			{
				SubHeaders = CreateSubHeaders(btiDistricts)
			};

			if (!string.IsNullOrWhiteSpace(filter.YearOfRest))
				result.ParametersCount++;

			if (!string.IsNullOrWhiteSpace(filter.BenefitType))
				result.ParametersCount++;

			if (!string.IsNullOrWhiteSpace(filter.District))
				result.ParametersCount++;

			if (!string.IsNullOrWhiteSpace(filter.TimeOfRest))
				result.ParametersCount++;

			var rowsList = CreateData(data, btiDistricts, withParents);

			result.Data = rowsList;

			return result;
		}

		private static Dictionary<long,string> CreateSubHeaders(BtiDistrict[] btiDistricts)
		{
			var subHeaders = new Dictionary<long, string>();

			subHeaders.AddRange(btiDistricts.Select(i => new KeyValuePair<long, string>(i.Id, i.Name)).ToArray());
			subHeaders.Add(_unknownDistrictKey, "Не определен");

			return subHeaders;
		}

		private static List<BenefitCategoryAndDistrict> CreateData(Child[] data, BtiDistrict[] btiDistricts, bool withParents)
		{
			var rowsList = new List<BenefitCategoryAndDistrict>();
			var firstTotalRow = new BenefitCategoryAndDistrict();
			rowsList.Add(firstTotalRow); //добавляем сразу, чтобы потом не двигать массив, заполняем в конце
			foreach (var timeOfRestGr in data.GroupBy(i => i.Request.TimeOfRest).Where(i => i.Key != null))
			{
				var benefitTotalRow = new BenefitCategoryAndDistrict
				{
					IsTimeOfRestTotal = true,
					BenefitCategory = timeOfRestGr.Key.Name,
					CountByDistrict = new Dictionary<long, int>()
				};

				var timeOfRestRowsList = new List<BenefitCategoryAndDistrict>();
				foreach (var benefitType in timeOfRestGr.GroupBy(i => i.BenefitType).OrderBy(i => i.Key.Id))
				{
					var row = new BenefitCategoryAndDistrict
					{
						BenefitCategory = benefitType.Key.Name,
						CountByDistrict = new Dictionary<long, int>()
					};

					var rowDistrict = benefitType.ToLookup(i => i.Address.BtiDistrictId ?? _unknownDistrictKey);
					foreach (var btiDistrict in btiDistricts)
					{
						var restManCount = 0;
						if (rowDistrict.Contains(btiDistrict.Id))
						{
							var children = rowDistrict[btiDistrict.Id];

							if (withParents)
							{
								restManCount = children.GroupBy(i => i.Request).Sum(i => i.Key.Child.Count + i.Key.Attendant.Count);
							}
							else
							{
								restManCount = children.Count();
							}
						}

						row.CountByDistrict.Add(new KeyValuePair<long, int>(btiDistrict.Id, restManCount));
					}
					row.CountByDistrict.Add(new KeyValuePair<long, int>(_unknownDistrictKey,
						rowDistrict.Contains(_unknownDistrictKey) ? rowDistrict[_unknownDistrictKey].Count() : 0));

					row.TotalCount = row.CountByDistrict.Values.Sum();

					timeOfRestRowsList.Add(row);
				}

				FillBtiDistricts(btiDistricts, timeOfRestRowsList.SelectMany(i => i.CountByDistrict).ToLookup(i => i.Key, i=> i.Value), benefitTotalRow);
				benefitTotalRow.TotalCount = benefitTotalRow.CountByDistrict.Values.Sum();

				rowsList.Add(benefitTotalRow);
				rowsList.AddRange(timeOfRestRowsList);
			}

			firstTotalRow.BenefitCategory = "Всего";
			firstTotalRow.CountByDistrict = new Dictionary<long, int>();
			FillBtiDistricts(btiDistricts, rowsList.Where(i => i.IsTimeOfRestTotal).SelectMany(i => i.CountByDistrict).ToLookup(i => i.Key,i => i.Value), firstTotalRow);
			firstTotalRow.TotalCount = firstTotalRow.CountByDistrict.Values.Sum();
			return rowsList;
		}

		private static void FillBtiDistricts(BtiDistrict[] btiDistricts, ILookup<long,int> rows, BenefitCategoryAndDistrict firstTotalRow)
		{
			foreach (var btiDistrict in btiDistricts)
			{
				var totalByDistrict = rows[btiDistrict.Id].Sum();

				firstTotalRow.CountByDistrict.Add(new KeyValuePair<long, int>(btiDistrict.Id, totalByDistrict));
			}

			firstTotalRow.CountByDistrict.Add(new KeyValuePair<long, int>(_unknownDistrictKey, rows[_unknownDistrictKey].Sum()));
		}

		public BaseExcelTable GetRestChildExcel(AnalyticReportFilter filter)
		{
			var query = UnitOfWork.GetSet<Child>().AsQueryable()
				.Include(i => i.Address)
				.Include(i => i.Request.TypeOfRest)
				.Include(i => i.Request.TimeOfRest.GroupedTimeOfRest)
				.Include(i => i.Request.Tour)
				.Where(i => !i.IsDeleted)
				.Where(i => i.Request != null && !i.Request.IsDeleted)
				.Where(i => i.Request.StatusId == (int)StatusEnum.CertificateIssued)
				.Where(i => i.Request.TypeOfRest.ParentId == (long)TypeOfRestEnum.ChildRest
                            || i.Request.TypeOfRest.ParentId == (long)TypeOfRestEnum.ChildRestCamps
                            || i.Request.TypeOfRest.ParentId == (long)TypeOfRestEnum.ChildRestOrphanCamps
                            || i.Request.TypeOfRest.ParentId == (long)TypeOfRestEnum.TentChildrenCamp
                            || i.Request.TypeOfRest.ParentId == (long)TypeOfRestEnum.TentChildrenCampOrphan
                            || i.Request.TypeOfRest.ParentId == (long)TypeOfRestEnum.ChildRestFederalCamps);

			query = AddFilters(query,filter);

			var data = query.ToArray();

			var report = GetReportInfo(filter, data,false);

			return BenefitsCampsByCategoryAndDistrictExport.GetExcel(report,
				AnalyticReportFilterRepository.GetReportName(filter.ReportType),
				"Лагеря");
		}

		public BaseExcelTable GetRestWithParentsExcel(AnalyticReportFilter filter)
		{
			var query = UnitOfWork.GetSet<Child>().AsQueryable()
				.Include(i => i.Address)
				.Include(i => i.Request.TypeOfRest)
				.Include(i => i.Request.TimeOfRest)
				.Include(i => i.Request.Tour)
				.Where(i => !i.IsDeleted)
				.Where(i => i.Request != null && !i.Request.IsDeleted)
				.Where(i => i.Request.StatusId == (int)StatusEnum.CertificateIssued)
				.Where(i => i.Request.TypeOfRest.ParentId == (long)TypeOfRestEnum.RestWithParents);

			query = AddFilters(query, filter);

			var data = query.ToArray();

			var report = GetReportInfo(filter, data, true);

			return BenefitsCampsByCategoryAndDistrictExport.GetExcel(report,
				AnalyticReportFilterRepository.GetReportName(filter.ReportType),
				"Семейный отдых");
		}
	}
}
