using System;
using System.Collections.Generic;
using RestChild.Comon;
using RestChild.Comon.Enumeration;
using RestChild.Comon.ToExcel;
using RestChild.DAL.RepositoryExtensions;
using RestChild.Domain;
using RestChild.Extensions.ExcelExport;
using RestChild.Extensions.Filter;
using static RestChild.Comon.Enumeration.AccessRightEnum.AnalyticReports;

namespace RestChild.Web.Logic.AnalyticReport
{

	public class AnalyticReportLogic : ILogic
	{
		public BenefitsByDistrictsAndCategoriesLogic BenefitsByDistrictsAndCategoriesLogic { get; set; }
		public BenefitsByAgeAndSex BenefitsByAgeAndSex { get; set; }
		public BenefitsByBoatCompleteness BenefitsByBoatCompleteness { get; set; }

		protected List<Tuple<string, string>> GetParameters(AnalyticReportFilter filter)
		{
			var parameters = new List<Tuple<string, string>>();
			if (filter.AgencyId.HasValue && string.IsNullOrWhiteSpace(filter.Agency))
			{
				filter.Agency = UnitOfWork.GetById<Organization>(filter.AgencyId)?.Name;
			}

			if (filter.OrganizationId.HasValue && string.IsNullOrWhiteSpace(filter.Organization))
			{
				filter.Organization = UnitOfWork.GetById<Organization>(filter.OrganizationId)?.Name;
			}

			if (filter.SupplierId.HasValue && string.IsNullOrWhiteSpace(filter.Supplier))
			{
				filter.Supplier = UnitOfWork.GetById<Organization>(filter.SupplierId)?.Name;
			}

			if (filter.PlaceOfRestId.HasValue && string.IsNullOrWhiteSpace(filter.PlaceOfRest))
			{
				filter.PlaceOfRest = UnitOfWork.GetById<PlaceOfRest>(filter.PlaceOfRestId)?.Name;
			}

			if (filter.HotelId.HasValue && string.IsNullOrWhiteSpace(filter.HotelName))
			{
				filter.HotelName = UnitOfWork.GetById<Hotels>(filter.HotelId)?.Name;
			}

			if (filter.ArrivalId.HasValue && string.IsNullOrWhiteSpace(filter.Arrival))
			{
				filter.Arrival = UnitOfWork.GetById<City>(filter.ArrivalId)?.Name;
			}

			if (filter.DepartureId.HasValue && string.IsNullOrWhiteSpace(filter.Departure))
			{
				filter.Departure = UnitOfWork.GetById<City>(filter.DepartureId)?.Name;
			}

			if (filter.TypeOfTransportId.HasValue && string.IsNullOrWhiteSpace(filter.TypeOfTransport))
			{
				filter.TypeOfTransport = UnitOfWork.GetById<TypeOfTransport>(filter.TypeOfTransportId)?.Name;
			}

			if (filter.TypeOfRestId.HasValue && string.IsNullOrWhiteSpace(filter.TypeOfRest))
			{
				filter.TypeOfRest = UnitOfWork.GetById<TypeOfRest>(filter.TypeOfRestId)?.Name;
			}

			if (filter.DistrictId.HasValue && string.IsNullOrWhiteSpace(filter.District))
			{
				filter.District = UnitOfWork.GetById<BtiDistrict>(filter.DistrictId)?.Name;
			}

			parameters.TryAddParameter("Год кампании", filter.YearOfRest);
			parameters.TryAddParameter("Цель обращения", filter.TypeOfRest);
			parameters.TryAddParameter("Период", filter.TimeOfRest);
			parameters.TryAddParameter("ОИВ", filter.Organization);
			parameters.TryAddParameter("Учреждение",filter.Agency);
			parameters.TryAddParameter("Исполнитель",filter.Supplier);
			parameters.TryAddParameter("Регион",filter.PlaceOfRest);
			parameters.TryAddParameter("Место отдыха", filter.HotelName);
			parameters.TryAddParameter("Дата с", filter.DateStartBegin?.FormatEx(string.Empty));
			parameters.TryAddParameter("Дата по", filter.DateStartEnd?.FormatEx(string.Empty));
			parameters.TryAddParameter("Категория льготы", filter.BenefitType);
			parameters.TryAddParameter("Административный округ", filter.District);
			parameters.TryAddParameter("Номер рейса", filter.FlightNumber);
			parameters.TryAddParameter("Место отбытия", filter.Departure);
			parameters.TryAddParameter("Место прибытия", filter.Arrival);
			parameters.TryAddParameter("Вид транспорта", filter.TypeOfTransport);
			parameters.TryAddParameter("Год рождения с", filter.YearOfBirthDateBegin?.FormatEx(string.Empty));
			parameters.TryAddParameter("Год рождения по", filter.YearOfBirthDateEnd?.FormatEx(string.Empty));

			return parameters;
		}

		public BaseExcelTable GetExcelTable(AnalyticReportFilter filter)
		{
			BaseExcelTable excelTable = null;
			switch (filter.ReportType)
			{
				case BenefitRestChildByAgeAndSex:
					excelTable = BenefitsByAgeAndSex.GenerateCampsByAgeAndSex(filter);
					break;
				case BenefitFamilyRestByAgeAndSex:
					excelTable = BenefitsByAgeAndSex.GenerateFamilyByAgeAndSex(filter);
					break;
				case BenefitRestChildByCategoryAndDistrict:
					excelTable = BenefitsByDistrictsAndCategoriesLogic.GetRestChildExcel(filter);
					break;
				case BenefitFamilyRestByCategoryAndDistrict:
					excelTable = BenefitsByDistrictsAndCategoriesLogic.GetRestWithParentsExcel(filter);
					break;
				case BenefitRestChildByBoutCompleteness:
					excelTable = BenefitsByBoatCompleteness.GetRestChildExcel(filter);
					break;
				case BenefitFamilyRestByBoutCompleteness:
					excelTable = BenefitsByBoatCompleteness.GetRestWithParentsExcel(filter);
					break;
				case AccessRightEnum.AnalyticReports.ByTransportServices:
					excelTable = UnitOfWork.GetByTransportServices(filter);
					break;
				case AccessRightEnum.AnalyticReports.ByResidenceServices:
					excelTable = UnitOfWork.GetByResidenceServices(filter);
					break;
				case AccessRightEnum.AnalyticReports.SpecializedCampsByOrganizations:
					excelTable = UnitOfWork.GetSpecializedCampsByOrganizations(filter);
					break;
				case AccessRightEnum.AnalyticReports.SpecializedCampsByVedomstvo:
					excelTable = UnitOfWork.GetSpecializedCampsByVedomstvo(filter);
					break;
				case AccessRightEnum.AnalyticReports.SpecializedCampsByAgeAndRegions:
					excelTable = UnitOfWork.GetSpecializedCampsByAgeAndRegions(filter);
					break;
				case AccessRightEnum.AnalyticReports.RestWithChildTypeOfRooms:
					excelTable = UnitOfWork.GetRestWithChildTypeOfRooms(filter);
					break;
                case EGISO:
                    excelTable = UnitOfWork.GetEGISO(filter);
                    break;
                default:
					throw new ArgumentOutOfRangeException(nameof(filter.ReportType));
			}
			excelTable.Parameters = GetParameters(filter);
			excelTable.TableName = AnalyticReportFilterRepository.GetReportName(filter.ReportType);
			return excelTable;
		}

		public IUnitOfWork UnitOfWork { get; set; }
	}
}
