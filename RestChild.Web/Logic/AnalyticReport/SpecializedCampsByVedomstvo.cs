using System.Collections.Generic;
using System.Linq;
using RestChild.Comon;
using RestChild.Comon.Enumeration;
using RestChild.Comon.ToExcel;
using RestChild.Domain;
using RestChild.Extensions.Filter;

namespace RestChild.Web.Logic.AnalyticReport
{
	public static class SpecializedCampsByVedomstvo
	{
		/// <summary>
		///     Востребованность по ОИВ
		/// </summary>
		public static BaseExcelTable GetSpecializedCampsByVedomstvo(this IUnitOfWork unitOfWork, AnalyticReportFilter filter)
		{
			var oivId = filter?.OrganizationId;
			var yearId = filter?.YearOfRestId;

			var q =
				unitOfWork.GetSet<Organization>()
					.Where(o => !o.IsDeleted && o.IsVedomstvo);

			if (oivId.HasValue)
			{
				q = q.Where(o => o.Id == oivId);
			}
			var items = q.OrderBy(o => o.Name).ToList().Select(o => new ReportRow {OivId = o.Id, Oiv = o.Name}).ToList();

			var dict = items.ToDictionary(t => t.OivId, t => t);

			var oivs =
				unitOfWork.GetSet<LimitOnVedomstvo>()
					.Where(o => o.YearOfRestId == yearId && o.StateId == StateMachineStateEnum.Limit.Oiv.Brought);

			if (oivId.HasValue)
			{
				oivs = oivs.Where(o => o.OrganizationId == oivId);
			}

			foreach (var oiv in oivs.ToList())
			{
				if (dict.ContainsKey(oiv.OrganizationId ?? 0))
				{
					dict[oiv.OrganizationId ?? 0].Approved = oiv.Volume;
				}
			}

			var tours =
				unitOfWork.GetSet<Tour>()
					.Where(
						o =>
							o.YearOfRestId == yearId &&
							(o.StateId == StateMachineStateEnum.Tour.Formed || o.StateId == StateMachineStateEnum.Tour.ToFormed ||
							 o.StateId == StateMachineStateEnum.Tour.ToFormationFromFormed))
					.Where(t => t.LimitOnVedomstvo != null && t.LimitOnVedomstvo.Organization != null);

			if (oivId.HasValue)
			{
				tours = tours.Where(o => o.LimitOnVedomstvo.OrganizationId == oivId);
			}

			var dictTour = tours.GroupBy(t => t.LimitOnVedomstvo.OrganizationId).ToList()
				.ToDictionary(t => t.Key, v => v.Select(i => i.Volumes.Select(tv => tv.CountPlace ?? 0).Sum()).Sum());

			foreach (var key in dictTour.Keys)
			{
				if (dict.ContainsKey(key ?? 0))
				{
					dict[key ?? 0].Tour = dictTour[key ?? 0];
				}
			}

			var lo = unitOfWork.GetSet<LimitOnOrganization>().Where(l =>
				(l.StateId == StateMachineStateEnum.Limit.Organization.Confirmed ||
				 l.StateId == StateMachineStateEnum.Limit.Organization.Approved ||
				 l.StateId == StateMachineStateEnum.Limit.Organization.Brought ||
				 l.StateId == StateMachineStateEnum.Limit.Organization.OnCompletion ||
				 l.StateId == StateMachineStateEnum.Limit.Organization.ToApprove
					) &&
				l.LimitOnVedomstvo.YearOfRestId == yearId);
			if (oivId.HasValue)
			{
				lo = lo.Where(o => o.LimitOnVedomstvo.OrganizationId == oivId);
			}


			var doctLo = lo.GroupBy(t => t.LimitOnVedomstvo.OrganizationId)
				.ToDictionary(t => t.Key, v => v.Select(i => i.Volume).Sum());

			foreach (var key in doctLo.Keys)
			{
				if (dict.ContainsKey(key ?? 0))
				{
					dict[key ?? 0].ToOrg = doctLo[key ?? 0];
				}
			}

			var loToApprove = unitOfWork.GetSet<LimitOnOrganization>().Where(l =>
				(
					l.StateId == StateMachineStateEnum.Limit.Organization.ToApprove
					) &&
				l.LimitOnVedomstvo.YearOfRestId == yearId);
			if (oivId.HasValue)
			{
				loToApprove = loToApprove.Where(o => o.LimitOnVedomstvo.OrganizationId == oivId);
			}

			var doctLoToApprove = loToApprove.GroupBy(t => t.LimitOnVedomstvo.OrganizationId)
				.ToDictionary(t => t.Key, v => v.Select(i => i.Volume).Sum());

			foreach (var key in doctLoToApprove.Keys)
			{
				if (dict.ContainsKey(key ?? 0))
				{
					dict[key ?? 0].OnApprove = doctLoToApprove[key ?? 0];
				}
			}

			var columns = new List<ExcelColumn<ReportRow>>
			{
				new ExcelColumn<ReportRow> {Title = "ОИВ", Func = r => r.Oiv},
				new ExcelColumn<ReportRow> {Title = "Размер утвержденной для ОИВ квоты", Func = r => r.Approved},
				new ExcelColumn<ReportRow> {Title = "Размер размещения ", Func = r => r.Tour},
				new ExcelColumn<ReportRow> {Title = "Распределено из утвержденной для ОИВ квоты", Func = r => r.ToOrg},
				new ExcelColumn<ReportRow> {Title = "Не распределено из утвержденной для ОИВ квоты", Func = r => r.Tour - r.ToOrg},
				new ExcelColumn<ReportRow> {Title = "На согласовании в ОИВ", Func = r => r.OnApprove}
			};

			return new ExcelTable<ReportRow>(columns, items)
			{
				TableName = "Востребованность по ОИВ",
			};
		}

		public class ReportRow
		{
			public long OivId { get; set; }
			public string Oiv { get; set; }
			public int Approved { get; set; }
			public int Tour { get; set; }
			public int ToOrg { get; set; }
			public int NotToOrg { get; set; }
			public int OnApprove { get; set; }
		}
	}
}
