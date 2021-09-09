using System.Collections.Generic;
using RestChild.Comon;
using RestChild.Comon.Enumeration;
using static RestChild.Comon.Enumeration.AccessRightEnum.AnalyticReports;

namespace RestChild.Web.Logic
{
	public class AnalyticReportFilterRepository
	{
		private static Dictionary<string, string> Names { get; set; }

		static AnalyticReportFilterRepository()
		{
			Names = GeneralExtensions.GetDisplayAttributesByValue<string>(typeof(AccessRightEnum.AnalyticReports));
		}

		public static string GetReportName(string reportType)
		{
			string value;

			if (Names == null || string.IsNullOrWhiteSpace(reportType) || !Names.TryGetValue(reportType, out value))
				return "Отчет";

			return  value;
		}

		public HashSet<string> DateStartFilter => new HashSet<string>
		{
			BenefitFamilyRestByAgeAndSex,
			BenefitFamilyRestByBoutCompleteness,
			ByResidenceServices,
			ByTransportServices
		};

		public HashSet<string> DistrictFilter => new HashSet<string>
		{
			BenefitRestChildByCategoryAndDistrict,
			BenefitFamilyRestByCategoryAndDistrict
		};

		public HashSet<string> BenefitFilter => new HashSet<string>
		{
			BenefitRestChildByCategoryAndDistrict,
			BenefitFamilyRestByCategoryAndDistrict
		};

		public HashSet<string> YearOfBirthFilter => new HashSet<string>
		{
			BenefitRestChildByAgeAndSex,
			BenefitFamilyRestByAgeAndSex,

			SpecializedCampsByAgeAndRegions
		};

		public HashSet<string> HotelFilter => new HashSet<string> {
			BenefitRestChildByAgeAndSex,
			BenefitFamilyRestByAgeAndSex,
			BenefitRestChildByBoutCompleteness,
			BenefitFamilyRestByBoutCompleteness,
			ByResidenceServices,
			SpecializedCampsByAgeAndRegions,
			RestWithChildTypeOfRooms
		};

		public HashSet<string> SupplierFilter => new HashSet<string>()
		{
			ByResidenceServices,
			ByTransportServices
		};

		public HashSet<string> VedomstvoFilter => new HashSet<string>()
		{
			SpecializedCampsByOrganizations,
			SpecializedCampsByVedomstvo,
			SpecializedCampsByAgeAndRegions
		};


		public HashSet<string> AgencyFilter => new HashSet<string>()
		{
			SpecializedCampsByOrganizations,
			SpecializedCampsByAgeAndRegions
		};

		public HashSet<string> TimeOfRestFilter => new HashSet<string>
		{
			BenefitRestChildByAgeAndSex,
			BenefitRestChildByCategoryAndDistrict,
			BenefitRestChildByBoutCompleteness,
			ByResidenceServices,
			SpecializedCampsByOrganizations,
			SpecializedCampsByAgeAndRegions,
		};

		public HashSet<string> TypeOfRestFilter => new HashSet<string>
		{
			ByResidenceServices,
		};


		public HashSet<string> PlaceOfRestFilter => new HashSet<string>
		{
			ByResidenceServices,
			SpecializedCampsByAgeAndRegions
		};

		public HashSet<string> ArrivalFilter => new HashSet<string>
		{
			ByTransportServices
		};

		public HashSet<string> DepartureFilter => new HashSet<string>
		{
			ByTransportServices
		};

		public HashSet<string> FlightNumberFilter => new HashSet<string>
		{
			ByTransportServices
		};

		public HashSet<string> TypeOfTransportFilter => new HashSet<string>
		{
			ByTransportServices
		};

        public HashSet<string> DateFormingFilter => new HashSet<string>
        {
            EGISO
        };

        public HashSet<string> NextYearsIncludedFilter => new HashSet<string>
        {
            RoomsFund
        };
    }
}
