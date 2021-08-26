using System;
using System.Collections.Generic;
using System.Linq;
using RestChild.Comon;
using RestChild.Comon.Enumeration;
using RestChild.Comon.ToExcel;
using RestChild.Domain;
using RestChild.Extensions.Filter;

namespace RestChild.Web.Logic.AnalyticReport
{
	public static class SpecializedCampsByAgeAndRegions
	{
		/// <summary>
		///     формирование данных для отчета Профильные лагеря. Распределение по году рождения и регионам
		/// </summary>
		public static BaseExcelTable GetSpecializedCampsByAgeAndRegions(this IUnitOfWork unitOfWork,
			AnalyticReportFilter filter)
		{
			var childrens = unitOfWork.GetSet<Child>()
				.Where(
					i =>
						i.ChildList != null && !i.IsDeleted && !i.ChildList.IsDeleted
						&& (i.ChildList.StateId == StateMachineStateEnum.Limit.List.Formed ||
						    i.ChildList.StateId == StateMachineStateEnum.Limit.List.IncludedInTour ||
						    i.ChildList.StateId == StateMachineStateEnum.Limit.List.IncludedPayment) &&
						i.ChildList.LimitOnOrganization.StateId == StateMachineStateEnum.Limit.Organization.Confirmed &&
						i.ChildList.LimitOnOrganization.LimitOnVedomstvo.StateId == StateMachineStateEnum.Limit.Oiv.Brought &&
						new long?[] {StateMachineStateEnum.Tour.Formed, StateMachineStateEnum.Tour.ToFormationFromFormed}.Contains(
							i.ChildList.LimitOnOrganization.Tour.StateId));

			if (filter != null)
			{
				if (filter.YearOfRestId.HasValue)
				{
					childrens = childrens.Where(c => c.ChildList.Tour.YearOfRestId == filter.YearOfRestId);
				}

				if (filter.YearOfBirthDateBegin.HasValue)
				{
					childrens = childrens.Where(c => c.DateOfBirth.Value.Year >= filter.YearOfBirthDateBegin);
				}

				if (filter.YearOfBirthDateEnd.HasValue)
				{
					childrens = childrens.Where(c => c.DateOfBirth.Value.Year <= filter.YearOfBirthDateEnd);
				}
				if (filter.OrganizationId.HasValue)
				{
					childrens =
						childrens.Where(c => c.ChildList.LimitOnOrganization.LimitOnVedomstvo.OrganizationId == filter.OrganizationId);
				}
				if (filter.AgencyId.HasValue)
				{
					childrens = childrens.Where(c => c.ChildList.LimitOnOrganization.OrganizationId == filter.AgencyId);
				}
				if (filter.HotelId.HasValue)
				{
					childrens = childrens.Where(c => c.ChildList.Tour.HotelsId == filter.HotelId);
				}
				if (filter.PlaceOfRestId.HasValue)
				{
					childrens = childrens.Where(c => c.ChildList.Tour.Hotels.PlaceOfRestId == filter.PlaceOfRestId);
				}

				if (filter.TimeOfRestId.HasValue)
				{
					childrens = childrens.Where(c => c.ChildList.Tour.TimeOfRest.GroupedTimeOfRestId == filter.TimeOfRestId);
				}
			}


			var minYear = childrens.Any() ? childrens.Select(c => c.DateOfBirth.Value.Year).Min() : 0;
			var maxYear = childrens.Any() ? childrens.Select(c => c.DateOfBirth.Value.Year).Max() : -1;

			var data = childrens.Where(c => c.DateOfBirth.HasValue).GroupBy(c => new ReportRow
			{
				OrganizationId = c.ChildList.LimitOnOrganization.OrganizationId,
				Organization = c.ChildList.LimitOnOrganization.Organization.Name,
				VedomstvoId = c.ChildList.LimitOnOrganization.LimitOnVedomstvo.OrganizationId,
				Vedomstvo = c.ChildList.LimitOnOrganization.LimitOnVedomstvo.Organization.Name,
				PeriodId = c.ChildList.Tour.TimeOfRestId,
				Period = c.ChildList.Tour.TimeOfRest.Name,
				PlaceOfRest = c.ChildList.Tour.Hotels.Name,
				PlaceOfRestId = c.ChildList.Tour.HotelsId,
				Region = c.ChildList.Tour.Hotels.PlaceOfRest.Name
			}).ToList().Select(
				k =>
					new ReportRow(k.Key)
					{
						Count = k.GroupBy(db => db.DateOfBirth.Value.Year).ToDictionary(y => y.Key, d => d.Count()),
						Total = k.Count()
					})
				.ToList()
				.OrderBy(i => i.Vedomstvo)
				.ThenBy(i => i.Organization)
				.ThenBy(i => i.PlaceOfRest)
				.ThenBy(i => i.Period)
				.ToList();

			var firstHeaderRow = new List<ExcelHeader<ReportRow>>
			{
				new ExcelHeader<ReportRow> {Title = "ОИВ", Column = 1, RowSpan = 2},
				new ExcelHeader<ReportRow> {Title = "Учреждение", Column = 2, RowSpan = 2},
				new ExcelHeader<ReportRow> {Title = "Регион", Column = 3, RowSpan = 2},
				new ExcelHeader<ReportRow> {Title = "Место отдыха", Column = 4, RowSpan = 2},
				new ExcelHeader<ReportRow> {Title = "Период", Column = 5, RowSpan = 2},
				new ExcelHeader<ReportRow> {Title = "Всего", Column = 6, RowSpan = 2}
			};
			var secondHeaderRow = new List<ExcelHeader<ReportRow>>();

			var columns = new List<ExcelColumn<ReportRow>>
			{
				new ExcelColumn<ReportRow> {Title = "ОИВ", Func = r => r.Vedomstvo},
				new ExcelColumn<ReportRow> {Title = "Учреждение", Func = r => r.Organization},
				new ExcelColumn<ReportRow> {Title = "Регион", Func = r => r.Region},
				new ExcelColumn<ReportRow> {Title = "Место отдыха", Func = r => r.PlaceOfRest},
				new ExcelColumn<ReportRow> {Title = "Период", Func = r => r.Period},
				new ExcelColumn<ReportRow> {Title = "Всего", Func = r => r.Total}
			};

			var startColumn = 7;

			for (var year = minYear; year <= maxYear; year++)
			{
				var yearInternal = year;
				columns.Add(new ExcelColumn<ReportRow>
				{
					Title = "Кол-во",
					Func = r => r.Count.ContainsKey(yearInternal) ? r.Count[yearInternal] : 0
				});
				columns.Add(new ExcelColumn<ReportRow>
				{
					Title = "%",
					Func =
						r =>
							r.Total > 0
								? ((Convert.ToDecimal(r.Count.ContainsKey(yearInternal) ? r.Count[yearInternal] : 0)/Convert.ToDecimal(r.Total))*
								   100).FormatEx()
								: "-"
				});

				firstHeaderRow.Add(new ExcelHeader<ReportRow> {Title = yearInternal.ToString(), Column = startColumn, ColSpan = 2});
				secondHeaderRow.Add(new ExcelHeader<ReportRow> {Title = "Кол-во", Column = startColumn});
				startColumn++;
				secondHeaderRow.Add(new ExcelHeader<ReportRow> {Title = "%", Column = startColumn});
				startColumn++;
			}

			return new ExcelTable<ReportRow>(columns, new List<List<ExcelHeader<ReportRow>>>
				{
					firstHeaderRow,
					secondHeaderRow
				}, data)
			{
				TableName = "Профильные лагеря. Распределение по году рождения и регионам",
			};
		}


		public class ReportRow
		{
			public ReportRow(ReportRow item)
			{
				Count = new Dictionary<int, int>();
				VedomstvoId = item.VedomstvoId;
				Vedomstvo = item.Vedomstvo;
				OrganizationId = item.OrganizationId;
				Organization = item.Organization;
				Region = item.Region;
				PlaceOfRestId = item.PlaceOfRestId;
				PlaceOfRest = item.PlaceOfRest;
				PeriodId = item.PeriodId;
				Period = item.Period;
				Total = item.Total;
			}

			public ReportRow()
			{
				Count = new Dictionary<int, int>();
			}

			public long? VedomstvoId { get; set; }
			public string Vedomstvo { get; set; }
			public long? OrganizationId { get; set; }
			public string Organization { get; set; }
			public string Region { get; set; }
			public long? PlaceOfRestId { get; set; }
			public string PlaceOfRest { get; set; }
			public long? PeriodId { get; set; }
			public string Period { get; set; }
			public int Total { get; set; }
			public Dictionary<int, int> Count { get; set; }
			public List<Child> Children { get; set; }
		}
	}
}
