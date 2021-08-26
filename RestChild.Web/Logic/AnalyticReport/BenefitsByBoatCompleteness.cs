using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using RestChild.Comon.Enumeration;
using RestChild.Comon.ToExcel;
using RestChild.Domain;
using RestChild.Extensions.Filter;
using RestChild.Web.Models.Business.Export.AnalyticReports;
using RestChild.Web.Models.Business.Export.Models;

namespace RestChild.Web.Logic.AnalyticReport
{
	public class BenefitsByBoatCompleteness : BenefitsLogic
	{
		public BaseExcelTable GetRestChildExcel(AnalyticReportFilter filter)
		{
			var childs = UnitOfWork.GetSet<Child>().AsQueryable();
			var requests = UnitOfWork.GetSet<Request>().AsQueryable();
			var timeOfRests = UnitOfWork.GetSet<TimeOfRest>().AsQueryable();
			var tours = UnitOfWork.GetSet<Tour>().AsQueryable();
			var hotels = UnitOfWork.GetSet<Hotels>().AsQueryable();
			var volumes = UnitOfWork.GetSet<TourVolume>();
			var boutes = UnitOfWork.GetSet<Bout>().AsQueryable();
			var linkToPeoples = UnitOfWork.GetSet<LinkToPeople>().AsQueryable();

			var toursQueyr = from tour in tours
							 join volume in volumes on tour.Id equals volume.TourId
							 join timeOfRest in timeOfRests on tour.TimeOfRestId equals timeOfRest.Id
							 join hotel in hotels on tour.HotelsId equals hotel.Id
							 where tour.TypeOfRestId == (long)TypeOfRestEnum.ChildRest
                                   || tour.TypeOfRestId == (long)TypeOfRestEnum.ChildRestOrphanCamps || tour.TypeOfRestId == (long)TypeOfRestEnum.ChildRestCamps
                                   || tour.TypeOfRestId == (long)TypeOfRestEnum.TentChildrenCamp || tour.TypeOfRestId == (long)TypeOfRestEnum.TentChildrenCampOrphan
                                   || tour.TypeOfRestId == (long)TypeOfRestEnum.ChildRestFederalCamps
							 select new { tour, volume, timeOfRest, hotel };

			var lpQuery = from lp in linkToPeoples
				join boute in boutes on new {lp.TransportId, lp.BoutId} equals
					new {TransportId = boute.TransportInfoToId, BoutId = (long?) boute.Id}
				join request in requests on lp.RequestId equals request.Id
				join tour in tours on
					new {BoutId = (long?) boute.Id, boute.HotelsId, TourId = request.TourId}
					equals new {BoutId = tour.BoutId, tour.HotelsId, TourId = (long?)tour.Id}
				join child in childs on lp.ChildId equals child.Id
				join timeOfRest in timeOfRests on request.TimeOfRestId equals timeOfRest.Id
				where lp.TypeOfLinkPeopleId == (long) TypeOfLinkPeopleEnum.Child
				where !request.IsDeleted && request.StatusId == (int) StatusEnum.CertificateIssued
				where !child.IsDeleted
				select new {lp, boute, tour, request, timeOfRest };

			if (filter.YearOfRestId.HasValue)
			{
				lpQuery = lpQuery.Where(i => i.request.YearOfRestId == filter.YearOfRestId.Value);
				toursQueyr = toursQueyr.Where(i => i.tour.YearOfRestId == filter.YearOfRestId);
			}

			if (filter.HotelId.HasValue)
			{
				toursQueyr = toursQueyr.Where(i => i.hotel.Id == filter.HotelId.Value);
				lpQuery = lpQuery.Where(i => i.request.HotelsId == filter.HotelId.Value);
			}

			if (filter.TimeOfRestId.HasValue)
			{
				toursQueyr = toursQueyr.Where(i => i.timeOfRest.GroupedTimeOfRestId == filter.TimeOfRestId);
				lpQuery = lpQuery.Where(i => i.timeOfRest.GroupedTimeOfRestId == filter.TimeOfRestId);
			}

			var lpByTourCount = from lp in lpQuery
							   group lp.lp by lp.tour.Id
				into tourGr
							   select new
							   {
								   TourId = tourGr.Key,
								   childCount = tourGr.Count(i => i.TypeOfLinkPeopleId == (long)TypeOfLinkPeopleEnum.Child
																  && !i.NotComeInPlaceOfRest),
							   };

			var lpByTour = lpByTourCount.ToDictionary(i => i.TourId,j => new { j.childCount});
			var result = new AnalyticReportInfo<BenefitsByBoatCompletenessInfo>();
			var rows = new List<BenefitsByBoatCompletenessInfo>();
			foreach (var hotel in toursQueyr.ToArray().GroupBy(i => i.hotel).OrderBy(i => i.Key.Id))
			{
				foreach (var timeOfRest in hotel.GroupBy(i => i.timeOfRest).OrderBy(i => i.Key.Id))
				{
					var benefitsByBoatCompletenessInfo = new BenefitsByBoatCompletenessInfo
					{
						HotelName = hotel.Key.Name,
						TimeOfRest = timeOfRest.Key.Name,
						Planned = timeOfRest.Sum(i => i.volume.CountPlace) ?? 0,
						Booked = timeOfRest.Sum(i => i.volume.CountBusyPlace) ?? 0
					};

					foreach (var lpsByTour in from tour in timeOfRest.GroupBy(i => i.tour).Where(i => i.Key != null) where lpByTour.ContainsKey(tour.Key.Id) select lpByTour[tour.Key.Id])
					{
						benefitsByBoatCompletenessInfo.Fact += lpsByTour.childCount;
					}

					benefitsByBoatCompletenessInfo.PlannedPercent = benefitsByBoatCompletenessInfo.Planned != 0
						? (float)benefitsByBoatCompletenessInfo.Booked / benefitsByBoatCompletenessInfo.Planned
						: 0;

					benefitsByBoatCompletenessInfo.BookedPercent = benefitsByBoatCompletenessInfo.Booked != 0
						? (float)benefitsByBoatCompletenessInfo.Fact / benefitsByBoatCompletenessInfo.Booked
						: 0;

					rows.Add(benefitsByBoatCompletenessInfo);
				}
			}

			result.Data = rows;

			var excelTable = BenefitsBoatCompletenessExcelExport.GenerateChildRestExcel(result,
				"Индивидуальный отдых. Недозаезды", "Лагеря");

			return excelTable;
		}

		public BaseExcelTable GetRestWithParentsExcel(AnalyticReportFilter filter)
		{
			var childs = UnitOfWork.GetSet<Child>().AsQueryable();
			var requests = UnitOfWork.GetSet<Request>().AsQueryable();
			var timeOfRests = UnitOfWork.GetSet<TimeOfRest>().AsQueryable();
			var typeOfRests = UnitOfWork.GetSet<TypeOfRest>().AsQueryable();
			var tours = UnitOfWork.GetSet<Tour>().AsQueryable();
			var attendants = UnitOfWork.GetSet<Applicant>().AsQueryable();
			var hotels = UnitOfWork.GetSet<Hotels>().AsQueryable();
			var applicants = UnitOfWork.GetSet<Applicant>().AsQueryable().Where(i => i.IsAccomp);
			var typeOfRooms = UnitOfWork.GetSet<TypeOfRooms>();
			var volumes = UnitOfWork.GetSet<TourVolume>();
			var boutes = UnitOfWork.GetSet<Bout>().AsQueryable();
			var linkToPeoples = UnitOfWork.GetSet<LinkToPeople>().AsQueryable();

			var toursQueyr = from tour in tours
				join volume in volumes on tour.Id equals volume.TourId
				join typeOfRest in typeOfRests on tour.TypeOfRestId equals typeOfRest.Id
				join timeOfRest in timeOfRests on tour.TimeOfRestId equals timeOfRest.Id
				join typeOfRoom in typeOfRooms on volume.TypeOfRoomsId equals typeOfRoom.Id
				join hotel in hotels on tour.HotelsId equals hotel.Id
				where
					new[]
					{
						(long) TypeOfRestEnum.RestWithParents,
						(long) TypeOfRestEnum.RestWithParentsComplex,
						(long) TypeOfRestEnum.RestWithParentsInvalidOrphanComplex,
						(long) TypeOfRestEnum.RestWithParentsInvalid,
						(long) TypeOfRestEnum.RestWithParentsComplex,
						(long) TypeOfRestEnum.RestWithParentsOrphan,
						(long) TypeOfRestEnum.RestWithParentsOther,
						(long) TypeOfRestEnum.RestWithParentsPoor
					}.Contains(typeOfRest.Id)
				select new {tour, volume, typeOfRest, timeOfRest, typeOfRoom, hotel};

			var requestChildrenQuery = from request in requests
				join child in childs on request.Id equals child.RequestId
				where !request.IsDeleted
				where !child.IsDeleted
				where request.StatusId == (int) StatusEnum.CertificateIssued
				where request.TourId.HasValue
				select new {request, child};

			var applicantsQuery = from request in requests
				join attendant in attendants on request.Id equals attendant.RequestId into attendantGr
				from attendantQuery in attendantGr.DefaultIfEmpty()
				join applicant in applicants on new {request.ApplicantId, IsAccomp = true} equals
					new {ApplicantId = (long?) applicant.Id, applicant.IsAccomp} into applicantGr
				from applicantQuery in applicantGr.DefaultIfEmpty()
				where !request.IsDeleted && request.TourId.HasValue
				where (applicantQuery == null || !applicantQuery.IsDeleted)
				where (attendantQuery == null || !attendantQuery.IsDeleted)
				where request.StatusId == (int) StatusEnum.CertificateIssued
				select new {request, attendantQuery, applicantQuery};

			var lpQuery = from lp in linkToPeoples
						  join boute in boutes on new { lp.TransportId, lp.BoutId } equals
							  new { TransportId = boute.TransportInfoToId, BoutId = (long?)boute.Id }
						  join tour in tours on new { BoutId = (long?)boute.Id, boute.HotelsId } equals new { tour.BoutId, tour.HotelsId }
						  join request in requests on lp.RequestId equals request.Id
						  where lp.TypeOfLinkPeopleId == (long)TypeOfLinkPeopleEnum.Child ||
								lp.TypeOfLinkPeopleId == (long)TypeOfLinkPeopleEnum.Attendant
						  where !request.IsDeleted
						  where request.StatusId == (int)StatusEnum.CertificateIssued
						  select new { lp, boute, tour, request };

			if (filter.YearOfRestId.HasValue)
			{
				requestChildrenQuery = requestChildrenQuery.Where(i => i.request.YearOfRestId == filter.YearOfRestId.Value);
				lpQuery = lpQuery.Where(i => i.request.YearOfRestId == filter.YearOfRestId.Value);
				applicantsQuery = applicantsQuery.Where(i => i.request.YearOfRestId == filter.YearOfRestId.Value);
				toursQueyr = toursQueyr.Where(i => i.tour.YearOfRestId == filter.YearOfRestId);
			}

			if (filter.HotelId.HasValue)
			{
				toursQueyr = toursQueyr.Where(i => i.hotel.Id == filter.HotelId.Value);
				applicantsQuery = applicantsQuery.Where(i => i.request.HotelsId == filter.HotelId.Value);
				lpQuery = lpQuery.Where(i => i.request.HotelsId == filter.HotelId.Value);
				requestChildrenQuery = requestChildrenQuery.Where(i => i.request.HotelsId == filter.HotelId.Value);
			}

			if (filter.DateStartBegin.HasValue && filter.DateStartEnd.HasValue)
			{
				var beginMonth = filter.DateStartBegin.Value.Month;
				var beginDay = filter.DateStartBegin.Value.Day;
				var endMonth = filter.DateStartEnd.Value.Month;
				var endDay = filter.DateStartEnd.Value.Day;

				toursQueyr = toursQueyr.Where(i => i.timeOfRest.Month >= beginMonth
												   && i.timeOfRest.DayOfMonth >= beginDay
												   && i.timeOfRest.Month <= endMonth
													&& i.timeOfRest.DayOfMonth <= endDay);
			}

			var childrenByTourCountQuery = from requestChild in requestChildrenQuery
									  group requestChild.child by requestChild.request.TourId
									  into tourGr
									  select new { TourId = tourGr.Key.Value, ChildrenCount = tourGr.Count() };
			var bookedChildrenByTour = childrenByTourCountQuery.ToDictionary(i => i.TourId, j => j.ChildrenCount);

			var applicantByTourCountQuery = from applicant in applicantsQuery
											group new { applicant.applicantQuery, applicant.attendantQuery} by applicant.request.TourId into requestGr
											select new {TourId = requestGr.Key.Value, attendantCount = requestGr.Count(i => i.applicantQuery != null || i.attendantQuery != null)};

			var bookedApplicantsByTour = applicantByTourCountQuery.ToDictionary(i => i.TourId,j => j.attendantCount);

			var lpByTouCount = from lp in lpQuery
				group lp.lp by lp.tour.Id
				into tourGr
				select new
				{
					TourId = tourGr.Key,
					childCount = tourGr.Count(i => i.TypeOfLinkPeopleId == (long) TypeOfLinkPeopleEnum.Child
					                              &&
					                               (!i.NotComeInPlaceOfRest)),
					attendantCount = tourGr.Count(i => i.TypeOfLinkPeopleId == (long) TypeOfLinkPeopleEnum.Attendant
					                                  && (!i.NotComeInPlaceOfRest))
				};

			var lpsByTours = lpByTouCount.ToDictionary(i => i.TourId);

			var rows = new List<BenefitsFamilyRestByBoatCompletenessInfo>();
			foreach (var hotel in toursQueyr.ToArray().GroupBy(i => i.hotel).OrderBy(i => i.Key.Id))
			{
				foreach (var timeOfRest in hotel.GroupBy(i => i.timeOfRest).OrderBy(i => i.Key.Id))
				{
					var benefitsByBoatCompletenessInfo = new BenefitsFamilyRestByBoatCompletenessInfo();

					benefitsByBoatCompletenessInfo.HotelName = hotel.Key.Name;
					benefitsByBoatCompletenessInfo.TimeOfRest = timeOfRest.Key.Name;

					benefitsByBoatCompletenessInfo.Planned = timeOfRest.Sum(i => i.volume.CountRooms * i.volume.TypeOfRooms.CountBasePlace ?? 0);

					benefitsByBoatCompletenessInfo.BookedTotal =
						timeOfRest.Sum(i => i.volume.CountBusyRooms*i.volume.TypeOfRooms.CountBasePlace ?? 0);

					foreach (var tour in timeOfRest.GroupBy(i => i.tour))
					{
						int bookedChild;
						bookedChildrenByTour.TryGetValue(tour.Key.Id,out bookedChild);
						benefitsByBoatCompletenessInfo.BookedChild += bookedChild;

						int bookedAttendant;
						bookedApplicantsByTour.TryGetValue(tour.Key.Id, out bookedAttendant);
						benefitsByBoatCompletenessInfo.BookedAttendant += bookedAttendant;

						if (lpsByTours.ContainsKey(tour.Key.Id))
						{
							var lpsByTour = lpsByTours[tour.Key.Id];
							benefitsByBoatCompletenessInfo.FactChild += lpsByTour.childCount;
							benefitsByBoatCompletenessInfo.FactAttendant += lpsByTour.attendantCount;
						}
					};

					benefitsByBoatCompletenessInfo.PlannedPercent = benefitsByBoatCompletenessInfo.Planned != 0
						? (float) benefitsByBoatCompletenessInfo.BookedTotal/
						  benefitsByBoatCompletenessInfo.Planned
						: 0;

					rows.Add(benefitsByBoatCompletenessInfo);
				}
			}

			var report = new AnalyticReportInfo<BenefitsFamilyRestByBoatCompletenessInfo>()
			{
				Data = rows,
			};
			var excelTable = BenefitsBoatCompletenessExcelExport.GenerateFamilyRestExcel(report, "Совместный отдых. Недозаезды",
				"Совместный отдых");

			return excelTable;
		}
	}
}
