using System.Web.Mvc;
using RestChild.Comon;
using RestChild.Domain;
using RestChild.Web.Controllers.WebApi;
using RestChild.Web.Properties;

namespace RestChild.Web.Controllers
{
	[Authorize]
	public class HotelPlacementController : BaseController
	{
		public WebHotelPlacementController ApiController { get; set; }

		public override void SetUnitOfWorkInRefClass(IUnitOfWork unitOfWork)
		{
			base.SetUnitOfWorkInRefClass(unitOfWork);
			ApiController.SetUnitOfWorkInRefClass(unitOfWork);
		}

		public ActionResult Index()
		{
			return RedirectToAction("Search");
		}

		public ActionResult Search(string name = "", int pageNumber = 1)
		{
			SetUnitOfWorkInRefClass(UnitOfWork);
			var entities = ApiController.Get(name, pageNumber);
			ViewBag.name = name;
			return View("List", entities);
		}

		public ActionResult Insert()
		{
			SetUnitOfWorkInRefClass(UnitOfWork);

			var entity = new HotelPlacement();
			return View("Edit", entity);
		}

		public ActionResult Update(long id)
		{
			SetUnitOfWorkInRefClass(UnitOfWork);

			var entity = ApiController.Get(id);

			return View("Edit", entity);
		}

		[HttpPost]
		public ActionResult Save(HotelPlacement hotelPlacement)
		{
			SetUnitOfWorkInRefClass(UnitOfWork);

			if (!ModelState.IsValid)
			{
				return View("Edit", hotelPlacement);
			}

			if (hotelPlacement.Id == 0)
			{
				ApiController.Post(hotelPlacement);
			}
			else
			{
				ApiController.Put(hotelPlacement.Id, hotelPlacement);
			}

			return RedirectToAction("Update", new {id = hotelPlacement.Id});
		}
	}
}