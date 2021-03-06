using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Collections;
using OfficeOpenXml.Style;
using RestChild.Comon.Enumeration;
using RestChild.Comon.ToExcel;
using RestChild.Domain;
using RestChild.Extensions.Filter;
using RestChild.Web.Controllers.WebApi;
using RestChild.Web.Logic;
using RestChild.Web.Logic.AnalyticReport;
using System.Globalization;

namespace RestChild.Web.Controllers
{
    public class AnalyticReportController : BaseController
    {
        public AnalyticReportLogic Logic { get; set; }
        public WebRestTypeController ApiRestTypeController { get; set; }

        public static string ExportActionName => "Export";

        public static string ShowActionName => "Show";

        public AnalyticReportFilterRepository FilterRepository { get; set; }
        private BaseExcelTable Excel { get; set; }


        // GET: AnalyticReport
        [Route("AnalyticReport/{ReportType}")]
        public ActionResult BaseReport(string ReportType)
        {
            if (!ReportHasAccess(ReportType))
            {
                return RedirectToAvalibleAction();
            }

            ApiRestTypeController.SetUnitOfWorkInRefClass(UnitOfWork);
            var years = UnitOfWork.GetSet<YearOfRest>().ToArray().Select(i => new YearOfRest(i)).ToArray();

            var analyticReportFilter = new AnalyticReportFilter
            {
                ReportType = ReportType,
                ReportName =
                    AnalyticReportFilterRepository.GetReportName(ReportType),
                YearsOfRest = years,
                Cities =
                    UnitOfWork.GetSet<City>().Where(c => c.IsActive).OrderBy(c => c.Name).ToList()
                        .Select(c => new City(c)).ToList(),
                TypeOfTransports =
                    UnitOfWork.GetSet<TypeOfTransport>().OrderBy(c => c.Name).ToList()
                        .Select(c => new TypeOfTransport(c)).ToList(),
                TypeOfRests = ApiRestTypeController.GetForTour().ToList(),
                Statuses = UnitOfWork.GetSet<Status>().OrderBy(c => c.Id).ToList(),
                ExchangeBaseRegistryTypes = UnitOfWork.GetSet<ExchangeBaseRegistryType>().Where(row => //row.Id == -1 || //Запрос наличия заключения ЦПМПК
                                                                                                        row.Id != 10209 && //Запрос паспортного досье по СНИЛС
                                                                                                        row.Id != 8255 &&// Запрос СНИЛС по ФИО
                                                                                                                         //row.Id == 260 ||//Наличие льготной категории
                                                                                                                         //row.Id == 10211 ||//Получение регистрации по месту жительства
                                                                                                        row.Id != 3091 &&//Предоставление из ЕГР ЗАГС сведений об актах гражданского состояния
                                                                                                                         //row.Id == 10214 ||//Проверка адреса регистрации ребенка (МВД)
                                                                                                                         //row.Id == -2 ||//Проверка законного представительства внутри АИС ДО
                                                                                                        row.Id != 22 &&//Проверка родства
                                                                                                        row.Id != 10244 &&// Проверка СНИЛС
                                                                                                                          //row.Id == 11827 ||//Предоставление из ЕГР ЗАГС сведений об актах гражданского состояния (11827)
                                                                                                                          //row.Id == 12150 || //Предоставление из ФГИС ФРИ выписки сведений об инвалиде(12150)
                                                                                                        row.Id != 2043 &&//Предоставление из ФРИ сведений об инвалидности
                                                                                                        row.Id != 10062)
                                                                                                        /*row.Id == 2040)*/.ToList()// Проверка СНИЛС
            };

            IEnumerable<Status> statuses = analyticReportFilter.Statuses;

            if (ReportType== AccessRightEnum.AnalyticReports.RoomsFund)
            {
                ICollection<TypeOfRest> tor = analyticReportFilter.TypeOfRests;

                analyticReportFilter.TypeOfRests = tor.Where(t=>t.ParentId==null).ToList();
                analyticReportFilter.Statuses = statuses.Where(s => s.Id > 0 &&
                                                                   s.Id != (long)StatusEnum.RegistrationDecline &&
                                                                   s.Id != (long)StatusEnum.CancelByApplicant &&
                                                                   s.Id != (long)StatusEnum.Reject &&
                                                                   s.Id != (long)StatusEnum.WaitApplicantMoney &&
                                                                   s.Id != (long)StatusEnum.OperatorCheck &&
                                                                   s.Id != (long)StatusEnum.WaitForOrganizationSelection&&
                                                                   s.Id != (long)StatusEnum.RegistrationDeclineBecauseDuplicate
                                                                   ).ToList();

                //analyticReportFilter.TypeOfRests = ApiRestTypeController.GetGeneralTypes().ToList();

            }


            ViewBag.Statuses = analyticReportFilter.Statuses;//.Select(s => new SelectListItem
            //                                                            {
            //                                                                Text = s.Name,
            //                                                                Value = s.Id.ToString(CultureInfo.InvariantCulture)
            //                                                            });

            FilterVisibility(analyticReportFilter);

            return View("Index", analyticReportFilter);
        }

        private bool ReportHasAccess(string reportType)
        {
            if (string.Equals(reportType, AccessRightEnum.AnalyticReports.BenefitRestChildByAgeAndSex,
                System.StringComparison.OrdinalIgnoreCase))
            {
                return Security.HasAnyRightsForSomeOrganization(new[]
                    {AccessRightEnum.AnalyticReports.BenefitRestChildByAgeAndSex});
            }
            else if (string.Equals(reportType, AccessRightEnum.AnalyticReports.BenefitFamilyRestByAgeAndSex,
                System.StringComparison.OrdinalIgnoreCase))
            {
                return Security.HasAnyRightsForSomeOrganization(new[]
                    {AccessRightEnum.AnalyticReports.BenefitFamilyRestByAgeAndSex});
            }
            else if (string.Equals(reportType, AccessRightEnum.AnalyticReports.BenefitRestChildByCategoryAndDistrict,
                System.StringComparison.OrdinalIgnoreCase))
            {
                return Security.HasAnyRightsForSomeOrganization(new[]
                    {AccessRightEnum.AnalyticReports.BenefitRestChildByCategoryAndDistrict});
            }
            else if (string.Equals(reportType, AccessRightEnum.AnalyticReports.BenefitFamilyRestByCategoryAndDistrict,
                System.StringComparison.OrdinalIgnoreCase))
            {
                return Security.HasAnyRightsForSomeOrganization(new[]
                    {AccessRightEnum.AnalyticReports.BenefitFamilyRestByCategoryAndDistrict});
            }
            else if (string.Equals(reportType, AccessRightEnum.AnalyticReports.BenefitRestChildByBoutCompleteness,
                System.StringComparison.OrdinalIgnoreCase))
            {
                return Security.HasAnyRightsForSomeOrganization(new[]
                    {AccessRightEnum.AnalyticReports.BenefitRestChildByBoutCompleteness});
            }
            else if (string.Equals(reportType, AccessRightEnum.AnalyticReports.BenefitFamilyRestByBoutCompleteness,
                System.StringComparison.OrdinalIgnoreCase))
            {
                return Security.HasAnyRightsForSomeOrganization(new[]
                    {AccessRightEnum.AnalyticReports.BenefitFamilyRestByBoutCompleteness});
            }
            else if (string.Equals(reportType, AccessRightEnum.AnalyticReports.ByTransportServices,
                System.StringComparison.OrdinalIgnoreCase))
            {
                return Security.HasAnyRightsForSomeOrganization(new[]
                    {AccessRightEnum.AnalyticReports.ByTransportServices});
            }
            else if (string.Equals(reportType, AccessRightEnum.AnalyticReports.ByResidenceServices,
                System.StringComparison.OrdinalIgnoreCase))
            {
                return Security.HasAnyRightsForSomeOrganization(new[]
                    {AccessRightEnum.AnalyticReports.ByResidenceServices});
            }
            else if (string.Equals(reportType, AccessRightEnum.AnalyticReports.SpecializedCampsByVedomstvo,
                System.StringComparison.OrdinalIgnoreCase))
            {
                return Security.HasAnyRightsForSomeOrganization(new[]
                    {AccessRightEnum.AnalyticReports.SpecializedCampsByVedomstvo});
            }
            else if (string.Equals(reportType, AccessRightEnum.AnalyticReports.SpecializedCampsByOrganizations,
                System.StringComparison.OrdinalIgnoreCase))
            {
                return Security.HasAnyRightsForSomeOrganization(new[]
                    {AccessRightEnum.AnalyticReports.SpecializedCampsByOrganizations});
            }
            else if (string.Equals(reportType, AccessRightEnum.AnalyticReports.SpecializedCampsByAgeAndRegions,
                System.StringComparison.OrdinalIgnoreCase))
            {
                return Security.HasAnyRightsForSomeOrganization(new[]
                    {AccessRightEnum.AnalyticReports.SpecializedCampsByAgeAndRegions});
            }
            else if (string.Equals(reportType, AccessRightEnum.AnalyticReports.RestWithChildTypeOfRooms,
                System.StringComparison.OrdinalIgnoreCase))
            {
                return Security.HasAnyRightsForSomeOrganization(new[]
                    {AccessRightEnum.AnalyticReports.RestWithChildTypeOfRooms});
            }
            else if (string.Equals(reportType, AccessRightEnum.AnalyticReports.EGISO,
                System.StringComparison.OrdinalIgnoreCase))
            {
                return Security.HasAnyRightsForSomeOrganization(new[] {AccessRightEnum.AnalyticReports.EGISO});
            }
            else if (string.Equals(reportType, AccessRightEnum.AnalyticReports.RoomsFund,
                System.StringComparison.OrdinalIgnoreCase))
            {
                return Security.HasAnyRightsForSomeOrganization(new[] {AccessRightEnum.AnalyticReports.RoomsFund});
            }
            else if (string.Equals(reportType, AccessRightEnum.AnalyticReports.NotRespondedRequests,
                System.StringComparison.OrdinalIgnoreCase))
            {
                return Security.HasAnyRightsForSomeOrganization(new[] { AccessRightEnum.AnalyticReports.NotRespondedRequests });
            }

            return true;
        }

        private bool ReportHasAccessSmart(string reportType)
        {
            return Security.HasAnyRightsForSomeOrganization(new[] {reportType});
        }

        private void FilterVisibility(AnalyticReportFilter filter)
        {
            if (FilterRepository.TimeOfRestFilter.Contains(filter.ReportType))
                filter.TimeOfRestVisibility = true;

            if (FilterRepository.AgencyFilter.Contains(filter.ReportType))
                filter.AgencyVisibility = true;

            if (FilterRepository.VedomstvoFilter.Contains(filter.ReportType))
                filter.VedomstvoVisibility = true;

            if (FilterRepository.SupplierFilter.Contains(filter.ReportType))
                filter.SupplierVisibility = true;

            if (FilterRepository.PlaceOfRestFilter.Contains(filter.ReportType))
                filter.PlaceOfRestVisibility = true;

            if (FilterRepository.HotelFilter.Contains(filter.ReportType))
                filter.HotelVisibility = true;

            if (FilterRepository.DateStartFilter.Contains(filter.ReportType))
                filter.DateStartVisibility = true;

            if (FilterRepository.YearOfBirthFilter.Contains(filter.ReportType))
                filter.YearOfBirthVisibility = true;

            if (FilterRepository.BenefitFilter.Contains(filter.ReportType))
                filter.BenefitVisibility = true;

            if (FilterRepository.DistrictFilter.Contains(filter.ReportType))
                filter.DistrictVisibility = true;

            if (FilterRepository.NextYearsIncludedFilter.Contains(filter.ReportType))
                filter.NextYearsIncludedVisibility = true;

            if (FilterRepository.ExchangeRequestsFilter.Contains(filter.ReportType))
                filter.ExchangeRequestVisibility = true;

            filter.TypeOfRestVisibility = FilterRepository.TypeOfRestFilter.Contains(filter.ReportType);
            filter.ArrivalVisibility = FilterRepository.ArrivalFilter.Contains(filter.ReportType);
            filter.DepartureVisibility = FilterRepository.DepartureFilter.Contains(filter.ReportType);
            filter.FlightNumberVisibility = FilterRepository.FlightNumberFilter.Contains(filter.ReportType);
            filter.TypeOfTransportVisibility = FilterRepository.TypeOfTransportFilter.Contains(filter.ReportType);
            filter.DateFormingVisibility = FilterRepository.DateFormingFilter.Contains(filter.ReportType);
        }

        [HttpPost]
        [Route("AnalyticReport/Generate")]
        public ActionResult Generate(AnalyticReportFilter filter/*, IEnumerable<int>chosenStatuses*/)
        {


            filter.StatusIds =string.Join(",", filter.SelectedStatuses??new List<long>());
            
            if (filter.ActionName == ExportActionName)
            {
                return ExportToExcel(filter);
            }

            return RedirectToAction("GenerateReport",filter);
        }


        [HttpGet]
        [Route("AnalyticReport/GenerateReport")]
        public ActionResult GenerateReport(AnalyticReportFilter filter)
        {
            Excel = Logic.GetExcelTable(filter);

            return View("BaseReport", Excel);
        }

        public ActionResult ExportToExcel(AnalyticReportFilter filter)
        {
            Excel = Logic.GetExcelTable(filter);
            Excel.DataBind("Отчет", ExcelBorderStyle.Thin);
            var fileStream = Excel.CreateExcel();

            return File(fileStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                $"{AnalyticReportFilterRepository.GetReportName(filter.ReportType)}.xlsx");
        }
    }
}
