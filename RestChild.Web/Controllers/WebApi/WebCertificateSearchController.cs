using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using RestChild.Comon;
using RestChild.Comon.Enumeration;
using RestChild.DAL.RepositoryExtensions;
using RestChild.Domain;
using RestChild.Extensions.Filter;

namespace RestChild.Web.Controllers.WebApi
{
    /// <summary>
    ///     Контроллер для работы с заявлением.
    /// </summary>
    [Authorize]
    public class WebCertificateSearchController : BaseController
    {
        public WebCalculationController ApiCalculationController { get; set; }

        public override void SetUnitOfWorkInRefClass(IUnitOfWork unitOfWork)
        {
            base.SetUnitOfWorkInRefClass(unitOfWork);
            ApiCalculationController.SetUnitOfWorkInRefClass(unitOfWork);
        }

        [ActionName("CertificateList")]
        public CertificateFilterModel CertificateList(CertificateFilterModel search, bool needPaging = true)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);

            var pager = new PagerState(search.PageNumber, search.PageSize);
            var query = CertificateListQuery(search);

            query = query.Include(r => r.Request.Applicant)
                .Include(r => r.Request.TimeOfRest)
                .Include(r => r.Request.TypeOfRest)
                .Include(r => r.Request.Status)
                .Include(r => r.Request.Tour)
                .Include(r => r.Request.PlaceOfRest)
                .Include(r => r.Request.Tour.Hotels)
                .Include(r => r.Request.TypeOfRest.Parent)
                .Include(r => r.Request.Attendant);
            if (needPaging)
            {
                search.Certificates = new CommonPagedList<Certificate>(
                    query.GetPage(pager),
                    pager.CurrentPage,
                    pager.PerPage,
                    query.Count());
            }
            else
            {
                search.Certificates = new CommonPagedList<Certificate>(
                    query.ToList(),
                    1,
                    1,
                    query.Count());
            }

            search.TotalRecordsCount =
                query.Select(x => x.Request.Child.Count(c => !c.IsDeleted) + x.Request.Attendant.Count + (x.Request.Applicant.IsAccomp ? 1 : 0))
                    .DefaultIfEmpty().Sum();

            return search;
        }
        internal IQueryable<Certificate> CertificateListQuery(CertificateFilterModel search)
        {
            if (search.WillRest == null)
                search.WillRest = false;
            if (search.Resting == null)
                search.Resting = false;
            if (search.Rested == null)
                search.Rested = false;
            if (search.AttendantWith == null)
                search.AttendantWith = false;
            if (search.AttendantWO == null)
                search.AttendantWO = false;
            var query =
                UnitOfWork.GetSet<Certificate>().Where(r =>r.Request.StatusId==(long)StatusEnum.CertificateIssued);

            if (!string.IsNullOrWhiteSpace(search.ApplicantFio))
            {
                var item = search.ApplicantFio.ToLower().Trim().Replace(' ', '|').Replace("ё", "е");

                query = query.Where(r => r.Request.Applicant.Key.Contains(item));
            }

            if (!string.IsNullOrWhiteSpace(search.ChildFio))
            {
                var item = search.ChildFio.ToLower().Trim().Replace(' ', '|').Replace("ё", "е");

                query = query.Where(r => r.Request.Child.Any(c => c.Key.Contains(item)));
            }

            if (!string.IsNullOrWhiteSpace(search.AttendantFio))
            {
                var item = search.AttendantFio.ToLower().Trim().Replace(' ', '|').Replace("ё", "е");

                query = query.Where(r => r.Request.Attendant.Any(c => c.Key.Contains(item)));
            }

            if (!string.IsNullOrWhiteSpace(search.ContractNumber))
            {
                query = query.Where(r => r.ContractNumber == search.ContractNumber);
            }

            if (!string.IsNullOrWhiteSpace(search.CertificateNumber))
            {
                query = query.Where(r => r.Request.CertificateNumber == search.CertificateNumber);
            }

            if (!search.HotelName.IsNullOrEmpty() && search.HotelName != "-- Не выбрано --")
            {
                query = query.Where(r => r.Place == search.HotelName);
            }

            if (search.PlaceOfRestId > 0)
            {
                query = query.Where(q => q.Request.PlaceOfRestId == search.PlaceOfRestId);
            }

            if (search.ContractDate.HasValue)
            {
                query = query.Where(q => q.ContractDate == DbFunctions.TruncateTime(search.ContractDate.Value));
            }
            if (!search.Request.IsNullOrEmpty())
                if (search.Request.CertificateDate.HasValue)
            {
                 //   var test = DbFunctions.TruncateTime(search.Request.CertificateDate);
                query = query.Where(q => DbFunctions.TruncateTime(q.Request.CertificateDate.Value).ToString() == DbFunctions.TruncateTime(search.Request.CertificateDate.Value).ToString());
            }
            if (search.RestDateFromFrom.HasValue)
            {
                query = query.Where(q => DbFunctions.TruncateTime(q.RestDateFrom) >= DbFunctions.TruncateTime(search.RestDateFromFrom.Value));
            }
            if (search.RestDateFromTo.HasValue)
            {
                query = query.Where(q => DbFunctions.TruncateTime(q.RestDateFrom) <= DbFunctions.TruncateTime(search.RestDateFromTo.Value));
            }
            if (search.RestDateToFrom.HasValue)
            {
                query = query.Where(q => DbFunctions.TruncateTime(q.RestDateTo) >= DbFunctions.TruncateTime(search.RestDateToFrom.Value));
            }
            if (search.RestDateToTo.HasValue)
            {
                query = query.Where(q => DbFunctions.TruncateTime(q.RestDateTo) <= DbFunctions.TruncateTime(search.RestDateToTo.Value));
            }
            if ((bool)search.WillRest)
            {
                query = query.Where(q => DbFunctions.TruncateTime(q.RestDateFrom) >= DbFunctions.TruncateTime(DateTime.Now));
            }
            if ((bool)search.Resting)
            {
                query = query.Where(q => DbFunctions.TruncateTime(q.RestDateFrom) <= DbFunctions.TruncateTime(DateTime.Now) && DbFunctions.TruncateTime(q.RestDateTo) >= DbFunctions.TruncateTime(DateTime.Now));
            }
            if ((bool)search.Rested)
            {
                query = query.Where(q => DbFunctions.TruncateTime(q.RestDateTo) <= DbFunctions.TruncateTime(DateTime.Now));
            }
            if ((bool)search.AttendantWith)
            {
                query = query.Where(q => q.Request.Attendant.Any());
            }
            if (search.ContractId>0)
            {
                var contract = UnitOfWork.GetById<Contract>(search.ContractId);
                query = query.Where(q => q.Request.OrganizationId == contract.OrganizationId);
            }
            if (!string.IsNullOrWhiteSpace(search.SNILS))
            {
                query = query.Where(r =>
                    r.Request.Applicant.Snils.Contains(search.SNILS)
                    || r.Request.Child.Any(c => c.Snils.Contains(search.SNILS))
                    || r.Request.Attendant.Any(c => c.Snils.Contains(search.SNILS))
                    || r.Request.Agent.Snils.Contains(search.SNILS)
                );
            }
            return query;
        }

    }
}
