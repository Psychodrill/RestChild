using System.Web.Mvc;
using RestChild.Comon;
using RestChild.Domain;
using RestChild.Web.Controllers.WebApi;
using RestChild.Web.Properties;

namespace RestChild.Web.Controllers
{
	[Authorize]
	public class FunctioningTypeController : BaseController
	{
		public WebFunctioningTypeController ApiController { get; set; }

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

			var entity = new FunctioningType();
			return View("Edit", entity);
		}

		public ActionResult Update(long id)
		{
			SetUnitOfWorkInRefClass(UnitOfWork);

			var entity = ApiController.Get(id);

			return View("Edit", entity);
		}

		[HttpPost]
		public ActionResult Save(FunctioningType functioningType)
		{
			SetUnitOfWorkInRefClass(UnitOfWork);

			if (!ModelState.IsValid)
			{
				return View("Edit", functioningType);
			}

			if (functioningType.Id == 0)
			{
				ApiController.Post(functioningType);
			}
			else
			{
				ApiController.Put(functioningType.Id, functioningType);
			}

			return RedirectToAction("Update", new {id = functioningType.Id});
		}
	}
}