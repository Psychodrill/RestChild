using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using RestChild.Comon;
using RestChild.Comon.Enumeration;
using RestChild.Domain;
using RestChild.Web.Controllers.WebApi;
using RestChild.Web.Models;

namespace RestChild.Web.Controllers
{
	using System.Globalization;

	[Authorize]
	public class RestTimeController : BaseController
    {
        /// <summary>
        /// Gets or sets the api controller.
        /// </summary>
		public WebRestTimeController ApiController { get; set; }

		public WebRestTypeController ApiRestTypeController { get; set; }

		public WebRestYearController ApiYearController { get; set; }

		public GroupedTimeOfRestController ApiGroupedTimeOfRest { get; set; }

		public override void SetUnitOfWorkInRefClass(IUnitOfWork unitOfWork)
		{
			base.SetUnitOfWorkInRefClass(unitOfWork);
			ApiController.SetUnitOfWorkInRefClass(unitOfWork);
			ApiRestTypeController.SetUnitOfWorkInRefClass(unitOfWork);
			ApiYearController.SetUnitOfWorkInRefClass(unitOfWork);
		}

        public ActionResult Index()
        {
			return RedirectToAction("Search");
        }

        public ActionResult Search(string name = "", long? tId = null, long? yId = null, int pageNumber = 1)
        {
	        SetUnitOfWorkInRefClass(UnitOfWork);
			var restTimes = this.ApiController.Get(name, tId, yId, pageNumber);
            ViewBag.name = name;
	        ViewBag.type = tId;
			ViewBag.year = yId;
	        ViewBag.TypesOfRest = UnitOfWork.GetSet<TypeOfRest>()
		        .Where(t => t.IsActive && !t.Commercial && t.HotelTypeId.HasValue).OrderBy(t=>t.Name).ToList();
	        ViewBag.Years = UnitOfWork.GetSet<YearOfRest>().OrderBy(y => y.Name).ToList();

			return View("TimeOfRestList", restTimes);
        }

		public ActionResult Insert()
		{
			if (!Security.HasRight(AccessRightEnum.VocabularyManage))
			{
				return RedirectToAvalibleAction();
			}

			SetUnitOfWorkInRefClass(UnitOfWork);
			var newRecod = new TimeOfRestModel();
			var years = ApiYearController.Get().ToList();
			newRecod.Data.YearOfRest = years.FirstOrDefault(y => y.Year == DateTime.Now.Year) ??
									   years.LastOrDefault();
			newRecod.Data.YearOfRestId = newRecod.Data.YearOfRest?.Id;
			newRecod.YearOfRestId = newRecod.Data.YearOfRestId;

			SetupVocabularies(newRecod);

			return View("TimeOfRestEdit", newRecod);
		}

		public ActionResult Update(long id)
        {
			if (!Security.HasRight(AccessRightEnum.VocabularyManage))
			{
				return RedirectToAvalibleAction();
			}

			SetUnitOfWorkInRefClass(UnitOfWork);
			var model = new TimeOfRestModel(ApiController.Get(id));
			SetupVocabularies(model);
			return View("TimeOfRestEdit", model);
        }

		private void SetupVocabularies(TimeOfRestModel model)
		{
			model.RestTypes = this.GetSelectListOfTypes(model?.TypeOfRestId);
			model.RestYears = this.GetSelectListOfYears(model?.YearOfRestId);
			model.GroupedTimeOfRest = this.GetGroupedTimeOfRest(model?.Data?.GroupedTimeOfRestId);
		}

		[HttpPost]
		public ActionResult Save(TimeOfRestModel model)
        {
			SetUnitOfWorkInRefClass(UnitOfWork);

			if (!Security.HasRight(AccessRightEnum.VocabularyManage))
			{
				return RedirectToAvalibleAction();
			}

			var time = model.BuildData();
			if (!ModelState.IsValid)
			{
				SetupVocabularies(model);
				return View("TimeOfRestEdit", model);
			}
	        if (time.Id == 0)
	        {
		        ApiController.Post(time);
	        }
	        else
	        {
		        ApiController.Put(time.Id, time);
	        }
	        return RedirectToAction("Update", new { id = time.Id });
        }

		private List<SelectListItem> GetSelectListOfTypes(long? selectedId)
		{
			var types = this.ApiRestTypeController.Get()
							.Select(
								x => new SelectListItem()
								{
									Value = x.Id.ToString(CultureInfo.InvariantCulture),
									Text = x.Name,
									Selected = selectedId.HasValue && selectedId.Value == x.Id
								})
							.InsertAt(new SelectListItem()
								{
									Value = string.Empty,
									Text = "-- Не выбрано --",
									Selected = false
								});

			return types.ToList();
		}

		private List<SelectListItem> GetSelectListOfYears(long? selectedId)
		{
			var years = this.ApiYearController.Get()
							.Select(
								x => new SelectListItem()
								{
									Value = x.Id.ToString(CultureInfo.InvariantCulture),
									Text = x.Name,
									Selected = selectedId.HasValue && selectedId.Value == x.Id
								})
							.InsertAt(new SelectListItem()
							{
								Value = string.Empty,
								Text = "-- Не выбрано --",
								Selected = false
							});

			return years.ToList();
		}

		private List<SelectListItem> GetGroupedTimeOfRest(long? selectedId)
		{
			var years = this.ApiGroupedTimeOfRest.Get()
							.Select(
								x => new SelectListItem()
								{
									Value = x.Id.ToString(CultureInfo.InvariantCulture),
									Text = x.Name,
									Selected = selectedId.HasValue && selectedId.Value == x.Id
								})
							.InsertAt(new SelectListItem()
							{
								Value = string.Empty,
								Text = "-- Не выбрано --",
								Selected = false
							});

			return years.ToList();
		}

    }
}
