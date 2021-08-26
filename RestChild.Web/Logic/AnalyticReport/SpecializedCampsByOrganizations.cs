using System.Collections.Generic;
using System.Linq;
using RestChild.Comon;
using RestChild.Comon.Enumeration;
using RestChild.Comon.ToExcel;
using RestChild.Domain;
using RestChild.Extensions.Filter;

namespace RestChild.Web.Logic.AnalyticReport
{
	/// <summary>
	/// Востребованность по учреждениям
	/// </summary>
	public static class SpecializedCampsByOrganizations
	{
		/// <summary>
		/// получение данных
		/// </summary>
		public static BaseExcelTable GetSpecializedCampsByOrganizations(this IUnitOfWork unitOfWork, AnalyticReportFilter filter)
		{
			var los =
				unitOfWork.GetSet<LimitOnOrganization>()
					.Where(
						l =>
							l.StateId.HasValue && l.StateId != StateMachineStateEnum.Deleted &&
							l.StateId != StateMachineStateEnum.Limit.Organization.Formation &&
							l.LimitOnVedomstvo.YearOfRestId == filter.YearOfRestId);

			if (filter.OrganizationId.HasValue)
			{
				los = los.Where(l => l.LimitOnVedomstvo.OrganizationId == filter.OrganizationId);
			}

			if (filter.AgencyId.HasValue)
			{
				los = los.Where(l => l.OrganizationId == filter.AgencyId);
			}

			if (filter.TimeOfRestId.HasValue)
			{
				los = los.Where(l => l.TimeOfRest.GroupedTimeOfRestId == filter.TimeOfRestId);
			}

			if (filter.HotelId.HasValue)
			{
				los = los.Where(l => l.Tour.HotelsId == filter.HotelId);
			}

			var data = los.ToList().Select(l => new ReportRow
			{
				Id = l.Id,
				Oiv = l.LimitOnVedomstvo?.Organization?.Name,
				Organization = l.Organization?.Name,
				Period = l.Tour?.TimeOfRest?.Name,
				Place = l.Tour?.Hotels?.Name,
				Time = $"{l.Tour?.DateIncome.FormatEx()}-{l.Tour?.DateOutcome.FormatEx()}",
				Volume = l.Volume,
			}).ToList();

			var dict = data.ToDictionary(d => d.Id, d=>d);

			var ids = data.Select(d => (long?)d.Id).ToList();

			var childQuery =
				unitOfWork.GetSet<Child>()
					.Where(
						c =>
							!c.IsDeleted && ids.Contains(c.ChildList.LimitOnOrganizationId)
							&& c.ChildList != null
							&& !c.ChildList.IsDeleted && c.ChildList.StateId != StateMachineStateEnum.Limit.List.Formed &&
							c.ChildList.StateId != StateMachineStateEnum.Deleted && c.ChildList.StateId.HasValue);

			var dictEntered = childQuery.GroupBy(t => t.ChildList.LimitOnOrganizationId).ToDictionary(t => t.Key, v => v.Count());

			foreach (var key in dictEntered.Keys)
			{
				if (dict.ContainsKey(key ?? 0))
				{
					dict[key ?? 0].Entered = dictEntered[key ?? 0];
				}
			}

			var dictPayed = childQuery.Where(c=>c.Payed).GroupBy(t => t.ChildList.LimitOnOrganizationId).ToDictionary(t => t.Key, v => v.Count());

			foreach (var key in dictPayed.Keys)
			{
				if (dict.ContainsKey(key ?? 0))
				{
					dict[key ?? 0].Payed = dictPayed[key ?? 0];
				}
			}

			var dictInHotel =
				unitOfWork.GetSet<LinkToPeople>()
					.Where(lp => lp.ChildId.HasValue && ids.Contains(lp.ListOfChilds.LimitOnOrganizationId))
					.Where(lp => lp.Transport.BoutId == lp.BoutId && lp.TransportId == lp.Bout.TransportInfoFromId)
					.Where(lp=> lp.NotNeedTicketReasonId != (long)NotNeedTicketReasonEnum.NotCome)
					.GroupBy(lp=>lp.ListOfChilds.LimitOnOrganizationId).ToDictionary(lp=>lp.Key, lp=>lp.Count());

			foreach (var key in dictInHotel.Keys)
			{
				if (dict.ContainsKey(key ?? 0))
				{
					dict[key ?? 0].InHotel = dictInHotel[key ?? 0];
				}
			}

			var columns = new List<ExcelColumn<ReportRow>>
			{
				new ExcelColumn<ReportRow> { Title = "ОИВ", Func = (r)=> r.Oiv},
				new ExcelColumn<ReportRow> { Title = "Наименование учреждения", Func = (r)=> r.Organization},
				new ExcelColumn<ReportRow> { Title = "Период", Func = (r)=> r.Period},
				new ExcelColumn<ReportRow> { Title = "Место отдыха", Func = (r)=> r.Place},
				new ExcelColumn<ReportRow> { Title = "Время отдыха", Func = (r)=> r.Time},
				new ExcelColumn<ReportRow> { Title = "Размер утвержденной квоты", Func = (r)=> r.Volume},
				new ExcelColumn<ReportRow> { Title = "Распределено", Func = (r)=> r.Entered},
				new ExcelColumn<ReportRow> { Title = "Не распределено", Func = (r)=> r.Volume - r.Entered},
				new ExcelColumn<ReportRow> { Title = "Оплачено", Func = (r)=> r.Payed},
				new ExcelColumn<ReportRow> { Title = "Заехало", Func = (r)=> r.InHotel},
			};

			return new ExcelTable<ReportRow>(columns, data.OrderBy(d => d.Oiv).ThenBy(d => d.Organization).ThenBy(d => d.Period).ThenBy(d => d.Place).ToList())
			{
				TableName = "Востребованность по учреждениям",
			};
		}

		/// <summary>
		/// класс для строки
		/// </summary>
		public class ReportRow
		{
			public long Id { get; set; }
			public string Oiv { get; set; }
			public string Organization { get; set; }
			public string Period { get; set; }
			public string Place { get; set; }
			public string Time { get; set; }
			public int Volume { get; set; }
			public int Entered { get; set; }
			public int Payed { get; set; }
			public int InHotel { get; set; }
		}
	}
}
