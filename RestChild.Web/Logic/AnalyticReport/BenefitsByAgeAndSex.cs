using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using RestChild.Comon.Enumeration;
using RestChild.Comon.ToExcel;
using RestChild.Domain;
using RestChild.Extensions.Filter;
using RestChild.Web.Models.Business.Export.AnalyticReports;
using RestChild.Web.Models.Business.Export.Models;

namespace RestChild.Web.Logic.AnalyticReport
{
	public class BenefitsByAgeAndSex : BenefitsLogic
	{
		public BaseExcelTable GenerateFamilyByAgeAndSex(AnalyticReportFilter filter)
		{
			var data = GetFamilyRestData(filter);

			return BenefitsFamilyRestBySexAndAgeExcelExport.GetExcelData(data);
		}
		private AnalyticReportInfo<FamilyTimeOfRestInfo> GetFamilyRestData(AnalyticReportFilter filter)
		{
			var requests = UnitOfWork.GetSet<Request>().AsQueryable();
			var attendants = UnitOfWork.GetSet<Applicant>().AsQueryable();
			var applicants = UnitOfWork.GetSet<Applicant>().AsQueryable().Where(i => i.IsAccomp);
			var typeOfRests = UnitOfWork.GetSet<TypeOfRest>().AsQueryable();

			var query = UnitOfWork.GetSet<Child>().AsQueryable()
				.Include(i => i.Request.Hotels)
				.Include(i => i.Request.TimeOfRest)
				.Include(i => i.Request.TypeOfRest)
				.Where(i => !i.IsDeleted)
				.Where(i => i.Request != null && !i.Request.IsDeleted)
				.Where(i => i.Request.Tour != null)
				.Where(i => i.Request.StatusId == (int)StatusEnum.CertificateIssued)
				.Where(i => i.Request.Tour.TypeOfRest.ParentId == (long)TypeOfRestEnum.RestWithParents || i.Request.Tour.TypeOfRestId == (long)TypeOfRestEnum.RestWithParents);

			query = AddFilters(query, filter);

			var result = query.Select(i => new
			{
				hotel = i.Request.Tour.Hotels,
				timeOfRest = i.Request.Tour.TimeOfRest,
				i.Male,
				request = i.Request,
				DateOfBirth = i.DateOfBirth.Value.Year
			}).ToArray();

			var attendantsQuery = from request in requests
								  join typeOfRest in typeOfRests on request.TypeOfRestId equals typeOfRest.Id
								  join attendant in attendants on request.Id equals attendant.RequestId into attendantGr
								  from attendantQuery in attendantGr.DefaultIfEmpty()
								  join applicant in applicants on new { request.ApplicantId, IsAccomp = true } equals
									  new { ApplicantId = (long?)applicant.Id, applicant.IsAccomp } into applicantGr
								  from applicantQuery in applicantGr.DefaultIfEmpty()
								  where !request.IsDeleted && request.TourId.HasValue
								  where (applicantQuery == null || !applicantQuery.IsDeleted)
								  where (attendantQuery == null || !attendantQuery.IsDeleted)
								  where request.StatusId == (int)StatusEnum.CertificateIssued
								  where typeOfRest.ParentId == (long)TypeOfRestEnum.RestWithParents
								  select new { request, attendantQuery, applicantQuery };

			var attendantsByTourQuery = from attendant in attendantsQuery
				group new {attendant.applicantQuery, attendant.attendantQuery} by attendant.request.Id
				into requestGr
				select new
				{
					RequestId = requestGr.Key,
					attendantMaleCount = requestGr.Count(i => (i.applicantQuery != null && i.applicantQuery.Male.HasValue && i.applicantQuery.Male.Value)
					|| (i.attendantQuery != null && i.attendantQuery.Male.HasValue && i.attendantQuery.Male.Value)
					|| ((i.attendantQuery != null && !i.attendantQuery.Male.HasValue) || i.applicantQuery != null && !i.applicantQuery.Male.HasValue)),
					attendantFemaleCount = requestGr.Count(i => (i.applicantQuery != null && i.applicantQuery.Male.HasValue && !i.applicantQuery.Male.Value)
					|| (i.attendantQuery != null && i.attendantQuery.Male.HasValue && !i.attendantQuery.Male.Value))
				};

			var bookedApplicantsByTour = attendantsByTourQuery.ToDictionary(i => i.RequestId, j => new { j.attendantMaleCount, j.attendantFemaleCount});

			var data = new AnalyticReportInfo<FamilyTimeOfRestInfo>
			{
				SubHeaders = new Dictionary<long, string>(),
			};

			var timeOfRestInfos = new List<FamilyTimeOfRestInfo>();
			foreach (var hotel in result.GroupBy(i => i.hotel).OrderBy(i => i.Key.Id))
			{
				foreach (var timeOfRest in hotel.GroupBy(i => i.timeOfRest).OrderBy(i => i.Key.Id))
				{
					var campTimeORestInfo = new FamilyTimeOfRestInfo();

					campTimeORestInfo.TimeOfRestName = timeOfRest.Key.Name;

					campTimeORestInfo.MaleCount = new Dictionary<long, int>();
					campTimeORestInfo.FemaleCount = new Dictionary<long, int>();

					campTimeORestInfo.HotelName = hotel.Key.Name;

					foreach (var tourKey in timeOfRest.GroupBy(i => i.request.Id))
					{
						foreach (var birthYearGr in tourKey.GroupBy(i => i.DateOfBirth))
						{
							if (campTimeORestInfo.MaleCount.ContainsKey(birthYearGr.Key))
								campTimeORestInfo.MaleCount[birthYearGr.Key] += birthYearGr.Count(i => i.Male);
							else
							{
								campTimeORestInfo.MaleCount.Add(birthYearGr.Key, birthYearGr.Count(i => i.Male));
							}


							if (campTimeORestInfo.FemaleCount.ContainsKey(birthYearGr.Key))
							{
								campTimeORestInfo.FemaleCount[birthYearGr.Key] += birthYearGr.Count(i => !i.Male);
							}
							else
							{
								campTimeORestInfo.FemaleCount.Add(birthYearGr.Key, birthYearGr.Count(i => !i.Male));
							}

							if (!data.SubHeaders.ContainsKey(birthYearGr.Key))
								data.SubHeaders.Add(new KeyValuePair<long, string>(birthYearGr.Key, birthYearGr.Key.ToString()));
						}

						if (bookedApplicantsByTour.ContainsKey(tourKey.Key))
						{
							campTimeORestInfo.AttendantsMaleCount += bookedApplicantsByTour[tourKey.Key].attendantMaleCount;
							campTimeORestInfo.AttendantsFemaleCount += bookedApplicantsByTour[tourKey.Key].attendantFemaleCount;
						}
					}

					timeOfRestInfos.Add(campTimeORestInfo);
				}
			}

			data.Data = timeOfRestInfos;

			return data;
		}
		public BaseExcelTable GenerateCampsByAgeAndSex(AnalyticReportFilter filter)
		{
			var data = GetChildRestData(filter);

			return BenefitsCampsBySexAndAgeExcelExport.GetExcelData(data);
		}
		private AnalyticReportInfo<CampTimeOfRestInfo> GetChildRestData(AnalyticReportFilter filter)
		{
			var query = UnitOfWork.GetSet<Child>().AsQueryable()
				.Include(i => i.Address)
				.Include(i => i.Request.TypeOfRest)
				.Include(i => i.Request.TimeOfRest.GroupedTimeOfRest)
				.Include(i => i.Request.Tour)
				.Where(i => !i.IsDeleted)
				.Where(i => i.Request != null && !i.Request.IsDeleted)
				.Where(i => i.Request.StatusId == (int)StatusEnum.CertificateIssued)
				.Where(i =>
							i.Request.Tour.TypeOfRest.ParentId == (long)TypeOfRestEnum.ChildRest
							|| i.Request.Tour.TypeOfRestId == (long)TypeOfRestEnum.ChildRest
							|| i.Request.Tour.TypeOfRest.ParentId == (long)TypeOfRestEnum.ChildRestCamps
							|| i.Request.Tour.TypeOfRestId == (long)TypeOfRestEnum.ChildRestCamps
                            || i.Request.Tour.TypeOfRest.ParentId == (long)TypeOfRestEnum.TentChildrenCamp
                            || i.Request.Tour.TypeOfRestId == (long)TypeOfRestEnum.TentChildrenCamp
                            || i.Request.Tour.TypeOfRest.ParentId == (long)TypeOfRestEnum.TentChildrenCampOrphan
                            || i.Request.Tour.TypeOfRestId == (long)TypeOfRestEnum.TentChildrenCampOrphan
							|| i.Request.Tour.TypeOfRest.ParentId == (long)TypeOfRestEnum.ChildRestFederalCamps
							|| i.Request.Tour.TypeOfRestId == (long)TypeOfRestEnum.ChildRestFederalCamps
							|| i.Request.Tour.TypeOfRest.ParentId == (long)TypeOfRestEnum.ChildRestOrphanCamps
							|| i.Request.Tour.TypeOfRestId == (long)TypeOfRestEnum.ChildRestOrphanCamps
					);

			query = AddFilters(query, filter);

			var result = query.ToArray();

			var data = new AnalyticReportInfo<CampTimeOfRestInfo>
			{
				SubHeaders = new Dictionary<long, string>()
			};

			var timeOfRestInfos = new List<CampTimeOfRestInfo>();
			foreach (var hotel in result.GroupBy(i => i.Request.Hotels).Where(i =>i.Key != null).OrderBy(i => i.Key.Id))
			{
				foreach (var timeOfRest in hotel.GroupBy(i => i.Request.TimeOfRest).Where(i => i.Key != null).OrderBy(i => i.Key.Id))
				{

					var campTimeORestInfo = new CampTimeOfRestInfo();
					campTimeORestInfo.TimeOfRestName = timeOfRest.Key.Name;

					campTimeORestInfo.MaleCount = new Dictionary<long, int>();
					campTimeORestInfo.FemaleCount = new Dictionary<long, int>();

					campTimeORestInfo.HotelName = hotel.Key.Name;

					foreach (var birthYearGr in timeOfRest.GroupBy(i => i.DateOfBirth.Value.Year))
					{
						campTimeORestInfo.MaleCount.Add(birthYearGr.Key, birthYearGr.Count(i => i.Male));
						campTimeORestInfo.FemaleCount.Add(birthYearGr.Key, birthYearGr.Count(i => !i.Male));

						if (!data.SubHeaders.ContainsKey(birthYearGr.Key))
							data.SubHeaders.Add(new KeyValuePair<long, string>(birthYearGr.Key, birthYearGr.Key.ToString()));
					}

					campTimeORestInfo.TotalTimeOfRestMaleCount = campTimeORestInfo.MaleCount.Sum(i => i.Value);
					campTimeORestInfo.TotalTimeOfRestFemaleCount = campTimeORestInfo.FemaleCount.Sum(i => i.Value);

					timeOfRestInfos.Add(campTimeORestInfo);
				}
			}

			data.Data = timeOfRestInfos;
			return data;
		}
	}
}
