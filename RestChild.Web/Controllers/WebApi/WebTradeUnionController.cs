using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;
using Newtonsoft.Json;
using RestChild.Domain;
using RestChild.Web.Models.TradeUnion;

namespace RestChild.Web.Controllers.WebApi
{
    [Authorize]
    public class WebTradeUnionController : BaseController
    {
        /// <summary>
        ///     Сервис поиска данных о ребенке в профсоюзном списке
        /// </summary>
        [HttpGet]
        [HttpPost]
        public System.Web.Http.IHttpActionResult GetChildBySNILS(string snils)
        {
            if (string.IsNullOrWhiteSpace(snils))
                throw new ArgumentNullException(nameof(snils));

            if (snils.Length != 14 || snils.Replace(" ", string.Empty).Replace("-", string.Empty).Length != 11)
                throw new Exception($"Ошибка формата СНИЛС: {snils}");

            var currentYear = DateTime.Now.Year;

            var years = UnitOfWork.GetSet<YearOfRest>()
                .Where(ss => ss.Year == currentYear || ss.Year == currentYear - 1 || ss.Year == currentYear - 2)
                .Select(ss => (long?)ss.Id).ToList();


            var item = UnitOfWork.GetSet<TradeUnionList>().Where(ss => years.Contains(ss.YearOfRestId))
                .SelectMany(ss => ss.Campers).Where(ss => ss.Child.Snils.ToLower() == snils.ToLower())
                .OrderByDescending(ss => ss.Child.LastUpdateTick).FirstOrDefault();

            if (item == null)
                return NotFound();

            if (item.Child?.Address != null)
            {
                if (string.IsNullOrWhiteSpace(item.Child?.Address?.FiasId))
                {
                    item.Child.Address = null;
                    item.Child.AddressId = null;
                }
            }

            var res = new TradeUnionCamperModel(item)
            {
                Id = 0,
                ChildId = 0,
                Summa = null,
                SummaBudget = null,
                SummaOrganization = null,
                SummaParent = null,
                SummaTradeUnion = null,
                IsChecked = false
            };

            return Json(res);
        }
    }
}
