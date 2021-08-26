using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using RestChild.Comon;
using RestChild.Comon.Dto.Commercial;
using RestChild.Comon.Enumeration;
using RestChild.Domain;
using RestChild.Web.Properties;

namespace RestChild.Web.Controllers.WebApi
{
	[Authorize]
	public class WebSchoolsController : BaseController
	{
		[Route("api/WebSchools")]
		public IEnumerable<School> Get(string query)
		{
			var q = UnitOfWork.GetSet<School>().Where(x => !x.Status.HasValue);

			if (!string.IsNullOrWhiteSpace(query))
			{
				q = q.Where(x => x.Name.ToLower().Contains(query.ToLower()));
			}

			return q.OrderBy(x => x.Name)
				.Take(Settings.Default.WebSchoolsResponseCount)
				.ToList();
		}

		[Route("api/WebEkisSchools")]
        [HttpGet, HttpPost]
        public IList<BaseResponse> GetSchools(string query)
		{
			var q = UnitOfWork.GetSet<School>().Where(x => x.Status != (long)OrganizationStatusEnum.StatusDeleted && x.Name != null && x.Name.Trim() != string.Empty && x.Name != "NULL").AsQueryable();

			if (!string.IsNullOrWhiteSpace(query))
			{
				q = q.Where(x => x.Name.ToLower().Contains(query.ToLower()));
			}

            var res = q.OrderBy(x => x.Name)
                .Take(Settings.Default.WebSchoolsResponseCount)
                .Select(s => new BaseResponse { Id =  s.Id, Name = s.Name })
                .ToList();

            return res;
        }
	}
}
