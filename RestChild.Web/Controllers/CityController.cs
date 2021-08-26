using System.Web.Mvc;
using RestChild.Comon;
using RestChild.Comon.Enumeration;
using RestChild.Extensions.Filter;
using RestChild.Domain;
using RestChild.Web.Controllers.WebApi;
using RestChild.Web.Models;

namespace RestChild.Web.Controllers
{
	[Authorize]
	public class CityController : BaseController
	{
		public WebCityController ApiController { get; set; }

		public override void SetUnitOfWorkInRefClass(IUnitOfWork unitOfWork)
		{
			base.SetUnitOfWorkInRefClass(unitOfWork);
			ApiController.SetUnitOfWorkInRefClass(unitOfWork);
		}

		public ActionResult Search(CityFilterModel filter)
		{
         if (!Security.HasAnyRights(new[] { AccessRightEnum.CityView }))
         {
            return RedirectToAvalibleAction();
         }
         SetUnitOfWorkInRefClass(UnitOfWork);
			filter = filter ?? new CityFilterModel();
			if (!filter.IsActive.HasValue)
			{
				filter.IsActive = true;
			}
			filter.Result = ApiController.Get(filter);
			return View("CityList", filter);
		}

		public ActionResult Insert()
		{
			SetUnitOfWorkInRefClass(UnitOfWork);
			return View("CityEdit", new CityModel(new City{IsActive = true}));
		}

		public ActionResult Update(long id)
		{
			SetUnitOfWorkInRefClass(UnitOfWork);
			return View("CityEdit", new CityModel(ApiController.Get(id)));
		}

		public ActionResult Save(CityModel model)
		{
			SetUnitOfWorkInRefClass(UnitOfWork);
			var city = model.BuildData();
			if (!Security.HasRight(AccessRightEnum.CityManage))
			{
				return RedirectToAction("Search");
			}
			if (!ModelState.IsValid)
			{
				return View("CityEdit", model);
			}
			if (city.Id == 0)
			{
				ApiController.Post(city);
			}
			else
			{
				ApiController.Put(city.Id, city);
			}
			return RedirectToAction("Update", new {id = city.Id});
		}
	}
}
