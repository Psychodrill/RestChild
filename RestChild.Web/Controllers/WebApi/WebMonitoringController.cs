using RestChild.Comon.Dto.Commercial;
using RestChild.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RestChild.Web.Controllers.WebApi
{
    /// <summary>
    ///     API блока мониторинга
    /// </summary>
    public class WebMonitoringController : BaseController
    {
        /// <summary>
        ///     Мониторинг. Организации отдыха и оздаровления
        /// </summary>
        [HttpGet]
        [Authorize]
        public List<BaseResponse> GetMonitoringHotels(long? regionId = null)
        {
            var res = UnitOfWork.GetSet<MonitoringHotel>().AsQueryable();
            if (regionId.HasValue && regionId.Value > 0)
            {
                res = res.Where(ss => ss.RegionId == regionId);
            }
            res = res.OrderBy(ss => ss.ShortName);

            var list = res.ToList();

            return list?.Select(ss => new BaseResponse { Id = ss.Id, Name = ss.ShortName }).ToList() ?? new List<BaseResponse>();
        }
    }
}
