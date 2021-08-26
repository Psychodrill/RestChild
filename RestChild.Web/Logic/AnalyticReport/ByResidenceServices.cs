using Security = RestChild.Web.Controllers.Security;
using System.Collections.Generic;
using System.Linq;
using RestChild.Comon;
using RestChild.Comon.Enumeration;
using RestChild.Comon.ToExcel;
using RestChild.Domain;
using RestChild.Extensions.Filter;
using RestChild.Web.Controllers;

namespace RestChild.Web.Logic.AnalyticReport
{
   using Security = RestChild.Web.Controllers.Security;

	public static class ByResidenceServices
	{
		/// <summary>
		///
		/// </summary>
		/// <param name="unitOfWork"></param>
		/// <param name="filter"></param>
		/// <returns></returns>
		public static BaseExcelTable GetByResidenceServices(this IUnitOfWork unitOfWork, AnalyticReportFilter filter)
		{
			var cnts = unitOfWork.GetSet<Contract>().Where(c => c.StateId == StateMachineStateEnum.Contract.Active);

			if (!Security.HasRight(AccessRightEnum.Contract.Manage) && !Security.HasRight(AccessRightEnum.Contract.View) && !Security.HasRight(AccessRightEnum.Contract.ViewCommercial))
			{
				var orgs = AccessRightEnum.Contract.View.GetSecurityOrganiztion();
				cnts = cnts.Where(c => orgs.Contains(c.SupplierId));
			}

			if (filter?.YearOfRestId.HasValue ?? false)
			{
				cnts = cnts.Where(c => c.YearOfRestId == filter.YearOfRestId);
			}

			if (filter?.YearOfRestId.HasValue ?? false)
			{
				cnts = cnts.Where(c => c.YearOfRestId == filter.YearOfRestId);
			}

			if (filter?.SupplierId.HasValue ?? false)
			{
				cnts = cnts.Where(c => c.SupplierId == filter.SupplierId);
			}

			var tours = cnts.SelectMany(c => c.Tour);

			if (filter?.HotelId.HasValue ?? false)
			{
				tours = tours.Where(t => t.HotelsId == filter.HotelId);
			}

			if (filter?.PlaceOfRestId.HasValue ?? false)
			{
				tours = tours.Where(t => t.Hotels.PlaceOfRestId == filter.PlaceOfRestId);
			}

			if (filter?.TimeOfRestId.HasValue ?? false)
			{
				tours = tours.Where(t => t.GroupedTimeOfRestId == filter.TimeOfRestId);
			}

			if (filter?.DateStartBegin.HasValue ?? false)
			{
				tours = tours.Where(t => t.DateIncome >= filter.DateStartBegin);
			}
			if (filter?.DateStartEnd.HasValue ?? false)
			{
				var d = filter.DateStartEnd?.AddDays(1);
				tours = tours.Where(t => t.DateIncome < d);
			}

			if (filter?.TypeOfRestId.HasValue ?? false)
			{
				tours = tours.Where(t => t.TypeOfRestId == filter.TypeOfRestId);
			}

			var links =
				unitOfWork.GetSet<LinkToPeople>()
					.Where(
						l => l.Bout.StateId.HasValue)
					.Where(lp => lp.Transport.BoutId == lp.BoutId && lp.TransportId == lp.Bout.TransportInfoFromId)
					.Where(lp => lp.NotNeedTicketReasonId != (long) NotNeedTicketReasonEnum.NotCome);

			var linkDict1 =
				links.Where(
					l =>
						tours.Select(f => (long?) f.Id).Contains(l.Request.TourId))
					.Where(l => l.Request.TourId.HasValue)
					.GroupBy(l => l.Request.TourId)
					.ToDictionary(y => y.Key, y => y.Count());
			var linkDict2 =
				links.Where(
					l =>
						tours.Select(f => (long?) f.Id).Contains(l.ListOfChilds.TourId))
					.Where(l => l.ListOfChilds.TourId.HasValue)
					.GroupBy(l => l.ListOfChilds.TourId)
					.ToDictionary(y => y.Key, y => y.Count());

			var data = tours.ToList().Select(t => new ReportRow

			{
				Id = t.Id,
				Number = t.Contract?.SignNumber,
				Organization = t.Contract?.Supplier?.Name,
				TypeOfRest = t.TypeOfRest?.Name,
				PlaceOfResst = t.Hotels?.PlaceOfRest?.Name,
				Hotel = t.Hotels?.Name,
				Period = t.TimeOfRest?.Name,
				Time = $"{t.DateIncome.FormatEx()}-{t.DateOutcome.FormatEx()}",
				Count =
					t.RequestsSingle.Where(r => r.StatusId == (long) StatusEnum.CertificateIssued && !r.IsDeleted)
						.SelectMany(r => r.Child.Where(c => !c.IsDeleted))
						.Count() +
					t.RequestsSingle.Where(r => r.StatusId == (long) StatusEnum.CertificateIssued && !r.IsDeleted)
						.SelectMany(r => r.Attendant.Where(c => !c.IsDeleted))
						.Count()
			}).ToList();

			var dict = data.ToDictionary(d => d.Id, d => d);
			foreach (var key in linkDict1.Keys)
			{
				if (dict.ContainsKey(key ?? 0))
				{
					dict[key ?? 0].Count += linkDict1[key ?? 0];
				}
			}

			foreach (var key in linkDict2.Keys)
			{
				if (dict.ContainsKey(key ?? 0))
				{
					dict[key ?? 0].Count += linkDict2[key ?? 0];
				}
			}

			return new ExcelTable<ReportRow>(new List<ExcelColumn<ReportRow>>
			{
				new ExcelColumn<ReportRow> {Title = "Исполнитель", Func = r => r.Organization},
				new ExcelColumn<ReportRow> {Title = "Номер контракта", Func = r => r.Number},
				new ExcelColumn<ReportRow> {Title = "Цель обращения", Func = r => r.TypeOfRest},
				new ExcelColumn<ReportRow> {Title = "Регион", Func = r => r.PlaceOfResst},
				new ExcelColumn<ReportRow> {Title = "Место отдыха", Func = r => r.Hotel},
				new ExcelColumn<ReportRow> {Title = "Время отдыха", Func = r => r.Period},
				new ExcelColumn<ReportRow> {Title = "Даты отдыха", Func = r => r.Time},
				new ExcelColumn<ReportRow> {Title = "Количество отдыхающих", Func = r => r.Count}
			}, data)
			{
				TableName = "Оказание услуг по проживанию",
			};
		}

		public class ReportRow
		{
			public long Id { get; set; }
			public string Organization { get; set; }
			public string Number { get; set; }
			public string TypeOfRest { get; set; }
			public string PlaceOfResst { get; set; }
			public string Hotel { get; set; }
			public string Period { get; set; }
			public string Time { get; set; }
			public int Count { get; set; }
		}
	}
}
