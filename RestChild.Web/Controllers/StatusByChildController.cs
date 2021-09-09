using System.Linq;
using System.Web.Mvc;
using RestChild.Comon;
using RestChild.Domain;
using RestChild.Web.Controllers.WebApi;

namespace RestChild.Web.Controllers
{
	[Authorize]
	public class StatusByChildController : BaseController
	{
		public WebStatusByChildController ApiController { get; set; }

		public override void SetUnitOfWorkInRefClass(IUnitOfWork unitOfWork)
		{
			base.SetUnitOfWorkInRefClass(unitOfWork);
			ApiController.SetUnitOfWorkInRefClass(unitOfWork);
		}

		public override ActionResult Index()
		{
			return RedirectToAction(nameof(Search));
		}

		public ActionResult Search(string name = "", int pageNumber = 1)
		{
			SetUnitOfWorkInRefClass(UnitOfWork);
			var statusByChild = ApiController.Get(name, pageNumber);
			ViewBag.name = name;
			return View("StatusByChildList", statusByChild);
		}

		public ActionResult Insert()
		{
			SetUnitOfWorkInRefClass(UnitOfWork);
			return View("StatusByChildEdit", new StatusByChild());
		}

		public ActionResult Update(long id)
		{
			SetUnitOfWorkInRefClass(UnitOfWork);
			return View("StatusByChildEdit", ApiController.Get(id));
		}

		[HttpPost]
		public ActionResult Save(StatusByChild statusByChild)
		{
			SetUnitOfWorkInRefClass(UnitOfWork);

			if (!ModelState.IsValid)
			{
				return View("StatusByChildEdit", statusByChild);
			}
			if (statusByChild.Id == 0)
			{
				statusByChild.Id = UnitOfWork.GetSet<StatusByChild>().Select(s => s.Id).DefaultIfEmpty().Max() + 1;
				ApiController.Post(statusByChild);
			}
			else
			{
				ApiController.Put(statusByChild.Id, statusByChild);
			}

			return RedirectToAction("Update", new {id = statusByChild.Id});
		}
	}
}
