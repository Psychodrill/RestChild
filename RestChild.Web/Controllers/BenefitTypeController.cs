using System.Web.Mvc;
using RestChild.Comon;
using RestChild.Domain;
using RestChild.Web.Controllers.WebApi;

namespace RestChild.Web.Controllers
{
	[Authorize]
	public class BenefitTypeController : BaseController
	{
		public WebBenefitTypeController ApiController { get; set; }

		public WebVocabularyController ApiVocabularyController { get; set; }

		public override void SetUnitOfWorkInRefClass(IUnitOfWork unitOfWork)
		{
			base.SetUnitOfWorkInRefClass(unitOfWork);
			ApiController.SetUnitOfWorkInRefClass(unitOfWork);
			ApiVocabularyController.SetUnitOfWorkInRefClass(unitOfWork);
		}

		public ActionResult Index()
		{
			return RedirectToAction("Search");
		}

		public ActionResult Search(string name = "", int pageNumber = 1)
		{
			SetUnitOfWorkInRefClass(UnitOfWork);
			var benefitType = ApiController.Get(name, pageNumber);
			ViewBag.name = name;
			return View("BenefitTypeList", benefitType);
		}

		public ActionResult Insert()
		{
			SetUnitOfWorkInRefClass(UnitOfWork);
			SetupVocabularies();
			return View("BenefitTypeEdit", new BenefitType());
		}

		public ActionResult Update(long id)
		{
			SetUnitOfWorkInRefClass(UnitOfWork);
			SetupVocabularies();
			return View("BenefitTypeEdit", ApiController.Get(id));
		}

		[HttpPost]
		public ActionResult Save(BenefitType benefitType)
		{
			SetUnitOfWorkInRefClass(UnitOfWork);
			if (!ModelState.IsValid)
			{
				SetupVocabularies();
				return View("BenefitTypeEdit", benefitType);
			}
			if (benefitType.Id == 0)
			{
				ApiController.Post(benefitType);
			}
			else
			{
				ApiController.Put(benefitType.Id, benefitType);
			}
			return RedirectToAction("Update", new {id = benefitType.Id});
		}

		private void SetupVocabularies()
		{
			ViewBag.TypesOfRest = ApiVocabularyController.GetTypesOfRest(false);
		}
	}
}