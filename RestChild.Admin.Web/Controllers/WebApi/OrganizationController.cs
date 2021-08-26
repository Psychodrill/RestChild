using RestChild.Admin.Web.Properties;
using RestChild.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace RestChild.Admin.Web.Controllers.WebApi
{
    /// <summary>
    ///     API для работы с организациями
    /// </summary>
    [Authorize]
    public class OrganizationController : BaseController
    {
        /// <summary>
        ///     поиск организации
        /// </summary>
        public IEnumerable<Organization> Get(string query)
        {
            var res =
                UnitOfWork.GetSet<Organization>()
                    .Where(x => !x.IsDeleted && x.IsLast && x.Name.ToLower().Contains(query.ToLower()))
                    .OrderBy(x => x.Name.Length)
                    .Take(Settings.Default.WebBtiStreetsResponseCount)
                    .ToList().Select(o => new Organization(o)).ToList();
            return res;
        }
    }
}
