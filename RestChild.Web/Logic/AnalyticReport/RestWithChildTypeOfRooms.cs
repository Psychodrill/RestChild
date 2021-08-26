using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OfficeOpenXml.Style;
using RestChild.Comon;
using RestChild.Comon.Enumeration;
using RestChild.Comon.ToExcel;
using RestChild.Domain;
using RestChild.Extensions.Filter;

namespace RestChild.Web.Logic.AnalyticReport
{
	public static class RestWithChildTypeOfRooms
	{
		private static Dictionary<string, int> GetRestWithChildTypeOfRooms(this Dictionary<string, int> dict, string key,
			int value)
		{
			if (dict.ContainsKey(key))
			{
				dict[key] = dict[key] + value;
			}
			else
			{
				dict.Add(key, value);
			}
			return dict;
		}

		/// <summary>
		///     Востребованность по ОИВ
		/// </summary>
		public static BaseExcelTable GetRestWithChildTypeOfRooms(this IUnitOfWork unitOfWork, AnalyticReportFilter filter)
		{
			var hotelId = filter?.HotelId;
			var yearId = filter?.YearOfRestId;

			var tour =
				unitOfWork.GetSet<Tour>().Where(o => o.StateId == StateMachineStateEnum.Tour.Formed || o.StateId == StateMachineStateEnum.Tour.ToFormationFromFormed || o.StateId == StateMachineStateEnum.Tour.ToFormed).
				Where(t=>t.HotelsId == hotelId && !t.TypeOfRest.Commercial && t.YearOfRestId == yearId).ToList();

			var inTour = unitOfWork.GetSet<Domain.Booking>()
				.Where(
					b =>
						b.Request.Child.Any(
							c =>
								c.LinkToPeoples.All(l => l.NotNeedTicketReasonId != (long) NotNeedTicketReasonEnum.NotCome) && c.BoutId.HasValue) &&
						!b.Canceled && b.Request.StatusId == (long) StatusEnum.CertificateIssued)
				.Where(b => b.TourVolume.Tour.HotelsId == hotelId && b.TourVolume.Tour.YearOfRestId == yearId)
				.GroupBy(b => b.TourVolumeId)
				.Select(t => new {TourVolumeId = t.Key, Count = t.Sum(v => v.CountRooms)})
				.ToDictionary(d => d.TourVolumeId, d => d.Count);

			var columnsNames =
				tour.OrderBy(s => s.DateIncome)
					.Select(s => $"{s.DateIncome.FormatEx()}-{s.DateOutcome.FormatEx()}")
					.Distinct()
					.ToList();

			var tvs =
				tour.SelectMany(t => t.Volumes).ToList().Select(v => new TourVolume(v) {TypeOfRooms = v.TypeOfRooms, Tour = v.Tour}).Where(t=>t.TypeOfRooms!=null).ToList();

			var dict = new Dictionary<long, List<RestWithChildTypeOfRoomsRow>>();

			foreach (var tv in tvs)
			{
				if (!dict.ContainsKey(tv.TypeOfRoomsId ?? 0))
				{
					dict.Add(tv.TypeOfRoomsId ?? 0, new List<RestWithChildTypeOfRoomsRow>
					{
						new RestWithChildTypeOfRoomsRow {Name = tv.TypeOfRooms.ToString(), Order = 0, TypeOfRoomId = tv.TypeOfRoomsId??0, Values = new Dictionary<string, int>()},
						new RestWithChildTypeOfRoomsRow {Name = "План", Order = 1, TypeOfRoomId = tv.TypeOfRoomsId??0, Values = new Dictionary<string, int>()},
						new RestWithChildTypeOfRoomsRow {Name = "Забронировано", Order = 2, TypeOfRoomId = tv.TypeOfRoomsId??0, Values = new Dictionary<string, int>()},
						new RestWithChildTypeOfRoomsRow {Name = "Фактически заехали", Order = 3, TypeOfRoomId = tv.TypeOfRoomsId??0, Values = new Dictionary<string, int>()}
					});
				}

				var list = dict[tv.TypeOfRoomsId ?? 0];
				var key = $"{tv.Tour.DateIncome.FormatEx()}-{tv.Tour.DateOutcome.FormatEx()}";
				list[1].Values.GetRestWithChildTypeOfRooms(key, tv.CountRooms ?? 0);
				list[2].Values.GetRestWithChildTypeOfRooms(key, tv.CountBusyRooms ?? 0);
				list[3].Values.GetRestWithChildTypeOfRooms(key, inTour.ContainsKey(tv.Id) ? inTour[tv.Id]??0 : 0);
			}

			var columns = new List<ExcelColumn<RestWithChildTypeOfRoomsRow>>
			{
				new ExcelColumn<RestWithChildTypeOfRoomsRow> {Title = "Вид номера", Func = r => r.Name},
			};

			foreach (var column in columnsNames)
			{
				columns.Add(new ExcelColumn<RestWithChildTypeOfRoomsRow>
				{
					Title = column,
					Func = r =>
					{
						if (r.Values == null)
						{
							return null;
						}

						if (r.Values.ContainsKey(column))
						{
							return r.Values[column];
						}

						return null;
					},
					Width = 20,
					HorizontalAlignment = ExcelHorizontalAlignment.Center
				});
			}

			return new ExcelTable<RestWithChildTypeOfRoomsRow>(columns,
				dict.Values.SelectMany(v => v)
					.Select(v => new ExcelRow<RestWithChildTypeOfRoomsRow> {Data = v, Bold = v.Order == 0})
					.ToList());
		}

        /// <summary>
        ///     ЕГИСО
        /// </summary>
        public static BaseExcelTable GetEGISO(this IUnitOfWork unitOfWork, AnalyticReportFilter filter)
        {
            var requsts = unitOfWork.GetSet<Request>().Where(r =>
                    r.StatusId == StateMachineStateEnum.Request.CertificateIssued && !r.IsDeleted && !r.IsDraft)
                .AsQueryable();

            if (filter?.YearOfRestId.HasValue ?? false)
            {
                requsts = requsts.Where(r => r.YearOfRestId == filter.YearOfRestId.Value);
            }
            if (filter?.DateFormingBegin.HasValue ?? false)
            {
                requsts = requsts.Where(r => r.DateRequest >= filter.DateFormingBegin.Value);
            }
            if (filter?.DateFormingEnd.HasValue ?? false)
            {
                requsts = requsts.Where(r => r.DateRequest <= filter.DateFormingEnd.Value);
            }

            var rest = unitOfWork.GetSet<TypeOfRest>()
                .Where(ss => ss.Id == (long) TypeOfRestEnum.ChildRest || ss.ParentId == (long) TypeOfRestEnum.ChildRest ||
                             ss.Id == (long)TypeOfRestEnum.RestWithParents || ss.ParentId == (long)TypeOfRestEnum.RestWithParents ||
                             ss.Id == (long)TypeOfRestEnum.RestWithParentsInvalidOrphanComplex || ss.ParentId == (long)TypeOfRestEnum.RestWithParentsInvalidOrphanComplex ||
                             ss.Id == (long)TypeOfRestEnum.ChildRestCamps || ss.ParentId == (long)TypeOfRestEnum.ChildRestCamps ||
                             ss.Id == (long)TypeOfRestEnum.TentChildrenCamp || ss.ParentId == (long)TypeOfRestEnum.TentChildrenCamp)
                .Select(ss => ss.Id).Distinct().ToList();

            var cert = unitOfWork.GetSet<TypeOfRest>()
                .Where(ss => ss.Id == (long)TypeOfRestEnum.Money || ss.ParentId == (long)TypeOfRestEnum.Money)
                .Select(ss => ss.Id).Distinct().ToList();

            var comps = new List<long>() { (long)TypeOfRestEnum.Compensation, (long)TypeOfRestEnum.CompensationYouthRest };

            var columns = new List<ExcelColumn<EGISORow>>
            {
                new ExcelColumn<EGISORow> {Title = "Номер заявления", Func = r => r.RequestNumber},
                new ExcelColumn<EGISORow> {Title = "Дата подачи заявления", Func = r => r.RequestDate},
                new ExcelColumn<EGISORow> {Title = "Статус заявления", Func = r => r.RequestStatus},
                new ExcelColumn<EGISORow> {Title = "Дата оказания услуги", Func = r => r.RequestDateIssured},
                new ExcelColumn<EGISORow> {Title = "Цель обращения", Func = r => r.TypeOfRest},
                new ExcelColumn<EGISORow> {Title = "Фамилия", Func = r => r.LastName},
                new ExcelColumn<EGISORow> {Title = "Имя", Func = r => r.FirstName},
                new ExcelColumn<EGISORow> {Title = "Отчество", Func = r => r.MiddleName},
                new ExcelColumn<EGISORow> {Title = "Пол", Func = r => r.Sex},
                new ExcelColumn<EGISORow> {Title = "Дата рождения", Func = r => r.DateOfBirth},
                new ExcelColumn<EGISORow> {Title = "СНИЛС", Func = r => r.SNILS},
                new ExcelColumn<EGISORow> {Title = "Документ удостоверяющий личность", Func = r => r.DocumentType},
                new ExcelColumn<EGISORow> {Title = "Серия", Func = r => r.DocumentSeria},
                new ExcelColumn<EGISORow> {Title = "Номер", Func = r => r.DocumentNumber},
                new ExcelColumn<EGISORow> {Title = "Дата выдачи", Func = r => r.DocumentDate},
                new ExcelColumn<EGISORow> {Title = "Кем выдан", Func = r => r.DocumentIssured},
                new ExcelColumn<EGISORow> {Title = "Вид ограничения", Func = r => r.TypeOfRestriction},
                new ExcelColumn<EGISORow> {Title = "Подвид ограничения", Func = r => r.TypeOfSubRestriction},
                new ExcelColumn<EGISORow> {Title = "Вид льготы", Func = r => r.BenefitType},
                new ExcelColumn<EGISORow> {Title = "Сумма (руб.)", Func = r => r.RequestSumm,  }
            };


            var results = new List<EGISORow>();
            foreach (var req in requsts.ToList())
            {
                if(req.Child?.Any() ?? false)
                {
                    foreach (var c in req.Child)
                    {
                        var summ = string.Empty;
                        if (comps.Contains(req.TypeOfRestId.Value))
                        {
                            summ = c.AmountOfCompensation?.ToString("C");
                        }
                        else
                        {
                            summ = unitOfWork.GetSet<AverageRestPrice>()
                                .Where(ss => ss.YearOfRestId == req.YearOfRestId && ss.TypeOfRestId == req.TypeOfRestId)
                                .Select(ss => ss.Price).FirstOrDefault().ToString("C");
                        }

                        results.Add(new EGISORow
                        {
                            RequestNumber = req.RequestNumber,
                            RequestDate = req.DateRequest,
                            RequestStatus = req.Status.Name,
                            RequestDateIssured = req.DateChangeStatus,
                            TypeOfRest = req.TypeOfRest.Name,
                            FirstName = c.FirstName,
                            LastName = c.LastName,
                            MiddleName = c.MiddleName,
                            Sex = c.Male ? "Мужской" : "Женский",
                            DateOfBirth = c.DateOfBirth,
                            SNILS = c.Snils,
                            DocumentType = c.DocumentType.Name,
                            DocumentSeria = c.DocumentSeria,
                            DocumentNumber = c.DocumentNumber,
                            DocumentDate = c.DocumentDateOfIssue,
                            DocumentIssured = c.DocumentSubjectIssue,
                            TypeOfRestriction = c.TypeOfRestriction?.Name,
                            TypeOfSubRestriction = c.TypeOfSubRestriction?.Name,
                            BenefitType = c.BenefitType?.Name,
                            RequestSumm = summ
                        });
                    }
                }

                if (req.InformationVouchers?.Any() ?? false)
                {
                    foreach (var iv in req.InformationVouchers)
                    {
                        foreach (var ap in iv.AttendantsPrice)
                        {
                            if (req.ApplicantId == ap.ApplicantId)
                            {
                                var summ = string.Empty;
                                if (comps.Contains(req.TypeOfRestId.Value))
                                {
                                    summ = ap.AmountOfCompensation?.ToString("C");
                                }

                                results.Add(new EGISORow
                                {
                                    RequestNumber = req.RequestNumber,
                                    RequestDate = req.DateRequest,
                                    RequestStatus = req.Status.Name,
                                    RequestDateIssured = req.DateChangeStatus,
                                    TypeOfRest = req.TypeOfRest.Name,
                                    FirstName = req.Applicant.FirstName,
                                    LastName = req.Applicant.LastName,
                                    MiddleName = req.Applicant.MiddleName,
                                    Sex = (req.Applicant.Male ?? true) ? "Мужской" : "Женский",
                                    DateOfBirth = req.Applicant.DateOfBirth,
                                    SNILS = req.Applicant.Snils,
                                    DocumentType = req.Applicant.DocumentType?.Name,
                                    DocumentSeria = req.Applicant.DocumentSeria,
                                    DocumentNumber = req.Applicant.DocumentNumber,
                                    DocumentDate = req.Applicant.DocumentDateOfIssue,
                                    DocumentIssured = req.Applicant.DocumentSubjectIssue,
                                    BenefitType = req.Applicant.BenefitType?.Name,
                                    RequestSumm = summ
                                });
                                break;
                            }
                        }
                    }
                }


                if (req.Attendant?.Any() ?? false)
                {
                    foreach (var a in req.Attendant)
                    {
                        var summ = string.Empty;
                        if (comps.Contains(req.TypeOfRestId.Value))
                        {
                            summ = req.InformationVouchers.SelectMany(sx => sx.AttendantsPrice)
                                .Where(sx => sx.ApplicantId == a.Id).Select(sx => sx.AmountOfCompensation)
                                .FirstOrDefault()?.ToString("C");
                        }

                        results.Add(new EGISORow
                        {
                            RequestNumber = req.RequestNumber,
                            RequestDate = req.DateRequest,
                            RequestStatus = req.Status.Name,
                            RequestDateIssured = req.DateChangeStatus,
                            TypeOfRest = req.TypeOfRest.Name,
                            FirstName = a.FirstName,
                            LastName = a.LastName,
                            MiddleName = a.MiddleName,
                            Sex = (a.Male ?? true) ? "Мужской" : "Женский",
                            DateOfBirth = a.DateOfBirth,
                            SNILS = a.Snils,
                            DocumentType = a.DocumentType?.Name,
                            DocumentSeria = a.DocumentSeria,
                            DocumentNumber = a.DocumentNumber,
                            DocumentDate = a.DocumentDateOfIssue,
                            DocumentIssured = a.DocumentSubjectIssue,
                            BenefitType = a.BenefitType?.Name,
                            RequestSumm = summ
                        });
                    }
                }
            }

            return new ExcelTable<EGISORow>(columns, results.OrderBy(ss => ss.RequestNumber));
        }


        public class RestWithChildTypeOfRoomsRow
		{
			/// <summary>
			/// вид комнаты
			/// </summary>
			public long TypeOfRoomId { get; set; }

			/// <summary>
			/// наименование
			/// </summary>
			public string Name { get; set; }

			/// <summary>
			/// порядок
			/// </summary>
			public long Order { get; set; }

			/// <summary>
			/// значение
			/// </summary>
			public Dictionary<string, int> Values { get; set; }
		}

        /// <summary>
        ///     Строка для отчета в ЕГИСО
        /// </summary>
        public struct EGISORow
        {
            /// <summary>
            ///     Номер заявления
            /// </summary>
            public string RequestNumber { get; set; }

            /// <summary>
            ///     Дата подачи заявления
            /// </summary>
            public DateTime? RequestDate { get; set; }

            /// <summary>
            ///     Статус заявления
            /// </summary>
            public string RequestStatus { get; set; }

            /// <summary>
            ///     Дата оказания услуги
            /// </summary>
            public DateTime? RequestDateIssured { get; set; }

            /// <summary>
            ///     Цель обращения
            /// </summary>
            public string TypeOfRest { get; set; }

            /// <summary>
            ///     Фамилия
            /// </summary>
            public string LastName { get; set; }

            /// <summary>
            ///     Имя
            /// </summary>
            public string FirstName { get; set; }

            /// <summary>
            ///     Отчество
            /// </summary>
            public string MiddleName { get; set; }

            /// <summary>
            ///     Пол
            /// </summary>
            public string Sex { get; set; }

            /// <summary>
            ///     Дата рождения
            /// </summary>
            public DateTime? DateOfBirth { get; set; }

            /// <summary>
            ///     СНИЛС
            /// </summary>
            public string SNILS { get; set; }

            /// <summary>
            ///     Документ удостоверяющий личность
            /// </summary>
            public string DocumentType { get; set; }

            /// <summary>
            ///    Серия
            /// </summary>
            public string DocumentSeria { get; set; }

            /// <summary>
            ///     Номер
            /// </summary>
            public string DocumentNumber { get; set; }

            /// <summary>
            ///     Дата выдачи
            /// </summary>
            public DateTime? DocumentDate { get; set; }

            /// <summary>
            ///    Кем выдан
            /// </summary>
            public string DocumentIssured { get; set; }

            /// <summary>
            ///     Вид ограничения (при наличии)
            /// </summary>
            public string TypeOfRestriction { get; set; }

            /// <summary>
            ///     Подвид ограничения (при наличии)
            /// </summary>
            public string TypeOfSubRestriction { get; set; }

            /// <summary>
            ///     Вид льготы
            /// </summary>
            public string BenefitType { get; set; }

            /// <summary>
            ///     Сумма (Указывается в рублях только в случае компенсации)
            /// </summary>
            public string RequestSumm { get; set; }
        }
	}
}
