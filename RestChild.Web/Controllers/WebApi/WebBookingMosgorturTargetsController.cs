using RestChild.Domain;
using RestChild.Extensions.Filter;
using RestChild.Web.Models;
using RestChild.Web.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RestChild.Web.Controllers.WebApi
{
    /// <summary>
    ///     API Контроллер работы с целями обращения визитов в МГТ
    /// </summary>
    public class WebBookingMosgorturTargetsController : BaseController
    {
        /// <summary>
        ///     Поиск целей обращения
        /// </summary>
        internal CommonPagedList<MGTVisitTarget> Get(BookingMosgorturTargetsFilterModel filter)
        {
            var pageSize = Settings.Default.TablePageSize;
            var pageNumber = filter?.PageNumber ?? 1;
            var startRecord = (pageNumber - 1) * pageSize;

            var query = UnitOfWork.GetSet<MGTVisitTarget>().AsQueryable();

            if (filter != null)
            {
                if (!string.IsNullOrWhiteSpace(filter.Name))
                {
                    query = query.Where(sx => sx.Name.ToLower().Contains(filter.Name.ToLower()));
                }
                if (filter.DepartmentId>1)
                {
                    query = query.Where(sx => sx.DepartmentId == filter.DepartmentId);
                }
            }

            var totalCount = query.Count();

            query = query.OrderBy(sx => sx.Id);

            var entity = query.Skip(startRecord).Take(pageSize).ToList();

            return new CommonPagedList<MGTVisitTarget>(entity, pageNumber, pageSize, totalCount);
        }
        /// <summary>
        ///     Извлечь список отделов
        /// </summary>
        internal ICollection<MGTDepartmentModel> GetDepartments()
        {
            return UnitOfWork.GetSet<MGTDepartment>().Where(sx => !sx.IsDeleted).Select(sx =>
                  new MGTDepartmentModel
                  {
                      Id = sx.Id,
                      Name = sx.Name,
                      Description = sx.Description
                  }).ToList();
        }
    }
}
