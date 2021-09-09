using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Configuration;
using System.Web.Http;
using RestChild.Comon.Dto.Commercial;
using RestChild.Mobile.DAL;
using RestChild.Mobile.Domain;

namespace RestChild.Web.Controllers.WebApi
{
    /// <summary>
    ///     Справочники для мобилки
    /// </summary>
    [Authorize]
    public class WebMobileVocabularyController : BaseController
    {
        /// <summary>
        ///     Unit Of Work
        /// </summary>
        public IUnitOfWorkMobile MobileUw { get; set; }

        /// <summary>
        ///     Лагеря/Смены
        /// </summary>
        public IList<BaseResponse> GetBouts(string query)
        {
            var q = MobileUw.GetSet<Bout>().OrderByDescending(s => s.DateIncome).AsQueryable();
            if (!string.IsNullOrWhiteSpace(query))
            {
                q = q.Where(ss => ss.Name.ToLower().Contains(query.ToLower()));
            }

            return q.Take(15).Select(ss => new BaseResponse {Id = ss.Id, Name = ss.Name}).ToList();
        }
    }
}
