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

	public static class ByTransportServices
	{
		public static BaseExcelTable GetByTransportServices(this IUnitOfWork unitOfWork, AnalyticReportFilter filter)
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

			var flights = cnts.SelectMany(c => c.DirectoryFlights);

			if (filter?.ArrivalId.HasValue ?? false)
			{
				flights = flights.Where(f => f.ArrivalId == filter.ArrivalId);
			}

			if (filter?.DepartureId.HasValue ?? false)
			{
				flights = flights.Where(f => f.DepartureId == filter.DepartureId);
			}

			if (filter?.TypeOfTransportId.HasValue ?? false)
			{
				flights = flights.Where(f => f.TypeOfTransportId == filter.TypeOfTransportId);
			}

			if (!string.IsNullOrWhiteSpace(filter?.FlightNumber))
			{
				var s = filter.FlightNumber.ToLower();
				flights = flights.Where(f => f.FilightNumber.ToLower().Contains(s));
			}



			var data = flights.ToList().Select(f => new ReportRow
			{
				Id = f.Id,
				Organization = f.Contract?.Supplier?.Name,
				Number = f.Contract?.SignNumber,
				Transport = f.TypeOfTransport?.Name,
				Departure = f.Departure?.Name,
				Arrival = f.Arrival?.Name
			}).ToList();

			var links =
				unitOfWork.GetSet<LinkToPeople>()
					.Where(
						l => l.Transport.StateId == StateMachineStateEnum.Transport.Formed && l.Bout.StateId.HasValue &&
						     flights.Select(f => (long?) f.Id).Contains(l.DirectoryFlightsId))
					.Where(lp => lp.Transport.BoutId == lp.BoutId)
					.Where(lp => lp.NotNeedTicketReasonId != (long) NotNeedTicketReasonEnum.NotCome);

			if (filter!=null && filter.DateStartBegin.HasValue)
			{
				links = links.Where(t => t.DateDeparture >= filter.DateStartBegin);
			}
			if (filter != null && filter.DateStartEnd.HasValue)
			{
				var d = filter.DateStartEnd?.AddDays(1);
				links = links.Where(t => t.DateDeparture < d);
			}

			var dict = data.ToDictionary(d => d.Id, d => d);

			var individual =
				links.Where(
					l =>
						l.ChildId.HasValue && (l.Request.TypeOfRestId == (long) TypeOfRestEnum.ChildRest ||
						                       l.Request.TypeOfRest.ParentId == (long) TypeOfRestEnum.ChildRest ||
											   l.Request.TypeOfRestId == (long)TypeOfRestEnum.ChildRestCamps ||
											   l.Request.TypeOfRest.ParentId == (long)TypeOfRestEnum.ChildRestCamps ||
                                               l.Request.TypeOfRestId == (long)TypeOfRestEnum.TentChildrenCamp ||
                                               l.Request.TypeOfRest.ParentId == (long)TypeOfRestEnum.TentChildrenCamp ||
                                               l.Request.TypeOfRestId == (long)TypeOfRestEnum.TentChildrenCampOrphan ||
                                               l.Request.TypeOfRest.ParentId == (long)TypeOfRestEnum.TentChildrenCampOrphan ||
											   l.Request.TypeOfRestId == (long)TypeOfRestEnum.ChildRestFederalCamps ||
											   l.Request.TypeOfRest.ParentId == (long)TypeOfRestEnum.ChildRestFederalCamps ||
												l.Request.TypeOfRestId == (long)TypeOfRestEnum.ChildRestOrphanCamps ||
												l.Request.TypeOfRest.ParentId == (long)TypeOfRestEnum.ChildRestOrphanCamps
											   ))
					.GroupBy(t => t.DirectoryFlightsId)
					.ToDictionary(t => t.Key, s => s.Count());

			foreach (var key in individual.Keys)
			{
				if (dict.ContainsKey(key ?? 0))
				{
					dict[key ?? 0].Rest = individual[key ?? 0];
				}
			}

			var child =
				links.Where(
					l =>
						l.ChildId.HasValue && (l.Request.TypeOfRestId == (long) TypeOfRestEnum.RestWithParents ||
						                       l.Request.TypeOfRest.ParentId == (long) TypeOfRestEnum.RestWithParents))
					.GroupBy(t => t.DirectoryFlightsId)
					.ToDictionary(t => t.Key, s => s.Count());

			foreach (var key in child.Keys)
			{
				if (dict.ContainsKey(key ?? 0))
				{
					dict[key ?? 0].Child = child[key ?? 0];
				}
			}

			var attendants =
				links.Where(
					l =>
						l.ApplicantId.HasValue && (l.Request.TypeOfRestId == (long) TypeOfRestEnum.RestWithParents ||
						                           l.Request.TypeOfRest.ParentId == (long) TypeOfRestEnum.RestWithParents))
					.GroupBy(t => t.DirectoryFlightsId)
					.ToDictionary(t => t.Key, s => s.Count());

			foreach (var key in attendants.Keys)
			{
				if (dict.ContainsKey(key ?? 0))
				{
					dict[key ?? 0].Attendant = attendants[key ?? 0];
				}
			}

			var specialized =
				links.Where(
					l =>
						l.ChildId.HasValue && (l.ListOfChildsId.HasValue))
					.GroupBy(t => t.DirectoryFlightsId)
					.ToDictionary(t => t.Key, s => s.Count());

			foreach (var key in specialized.Keys)
			{
				if (dict.ContainsKey(key ?? 0))
				{
					dict[key ?? 0].Specialized = specialized[key ?? 0];
				}
			}

			var personal =
				links.Where(
					l =>
						l.CounselorsId.HasValue || l.AdministratorTourId.HasValue)
					.GroupBy(t => t.DirectoryFlightsId)
					.ToDictionary(t => t.Key, s => s.Count());

			foreach (var key in personal.Keys)
			{
				if (dict.ContainsKey(key ?? 0))
				{
					dict[key ?? 0].Personal = personal[key ?? 0];
				}
			}

			return new ExcelTable<ReportRow>(new List<ExcelColumn<ReportRow>>
			{
				new ExcelColumn<ReportRow> {Func = r => r.Organization},
				new ExcelColumn<ReportRow> {Func = r => r.Number},
				new ExcelColumn<ReportRow> {Func = r => r.Transport},
				new ExcelColumn<ReportRow> {Func = r => r.FilightNumber},
				new ExcelColumn<ReportRow> {Func = r => r.Departure},
				new ExcelColumn<ReportRow> {Func = r => r.Arrival},
				new ExcelColumn<ReportRow> {Func = r => r.Rest},
				new ExcelColumn<ReportRow> {Func = r => r.Child},
				new ExcelColumn<ReportRow> {Func = r => r.Attendant},
				new ExcelColumn<ReportRow> {Func = r => r.Attendant + r.Child},
				new ExcelColumn<ReportRow> {Func = r => r.Specialized},
				new ExcelColumn<ReportRow> {Func = r => r.Personal},
				new ExcelColumn<ReportRow> {Func = r => r.Rest + r.Specialized + r.Attendant + r.Child + r.Personal}
			}, new List<List<ExcelHeader<ReportRow>>>
			{
				new List<ExcelHeader<ReportRow>>
				{
					new ExcelHeader<ReportRow> {RowSpan = 3, Column = 1, Title = "Транспортная компания"},
					new ExcelHeader<ReportRow> {RowSpan = 3, Column = 2, Title = "Номер контракта"},
					new ExcelHeader<ReportRow> {RowSpan = 3, Column = 3, Title = "Вид транспорта"},
					new ExcelHeader<ReportRow> {RowSpan = 3, Column = 4, Title = "Номер рейса"},
					new ExcelHeader<ReportRow> {RowSpan = 3, Column = 5, Title = "Место отправления"},
					new ExcelHeader<ReportRow> {RowSpan = 3, Column = 6, Title = "Место прибытия"},
					new ExcelHeader<ReportRow> {ColSpan = 7, Column = 7, Title = "Перевезено"}
				},
				new List<ExcelHeader<ReportRow>>
				{
					new ExcelHeader<ReportRow> {RowSpan = 2, Column = 7, Title = "Индивидуальный отдых"},
					new ExcelHeader<ReportRow> {ColSpan = 3, Column = 8, Title = "Совместных отдых"},
					new ExcelHeader<ReportRow> {RowSpan = 2, Column = 11, Title = "Профильные лагеря"},
					new ExcelHeader<ReportRow> {RowSpan = 2, Column = 12, Title = "Персонал"},
					new ExcelHeader<ReportRow> {RowSpan = 2, Column = 13, Title = "Всего"}
				},
				new List<ExcelHeader<ReportRow>>
				{
					new ExcelHeader<ReportRow> {ColSpan = 1, Column = 8, Title = "Детей"},
					new ExcelHeader<ReportRow> {ColSpan = 1, Column = 9, Title = "Сопровождающих"},
					new ExcelHeader<ReportRow> {ColSpan = 1, Column = 10, Title = "Всего"}
				}
			}, data)
			{
				TableName = "Оказание транспортных услуг"
			};
		}

		public class ReportRow
		{
			public long Id { get; set; }
			public string Organization { get; set; }
			public string Number { get; set; }
			public string Transport { get; set; }
			public string FilightNumber { get; set; }
			public string Departure { get; set; }
			public string Arrival { get; set; }
			public int Rest { get; set; }
			public int Child { get; set; }
			public int Attendant { get; set; }
			public int Specialized { get; set; }
			public int Personal { get; set; }
		}
	}
}
