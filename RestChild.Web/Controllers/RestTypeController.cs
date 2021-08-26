using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using RestChild.Comon;
using RestChild.Domain;
using RestChild.Web.Controllers.WebApi;

namespace RestChild.Web.Controllers
{
	[Authorize]
	public class RestTypeController : BaseController
	{
		/// <summary>
		///     Gets or sets the api controller.
		/// </summary>
		public WebRestTypeController ApiController { get; set; }

		public override void SetUnitOfWorkInRefClass(IUnitOfWork unitOfWork)
		{
			base.SetUnitOfWorkInRefClass(unitOfWork);
			ApiController.SetUnitOfWorkInRefClass(unitOfWork);
		}


		/// <summary>
		///     The index.
		/// </summary>
		/// <returns>
		///     The <see cref="ActionResult" />.
		/// </returns>
		public ActionResult Index()
		{
			return RedirectToAction("Search");
		}

		public ActionResult Search(string name = "", bool activeOnly = true, int pageNumber = 1)
		{
			SetUnitOfWorkInRefClass(UnitOfWork);
			var restTypes = ApiController.Get(name, activeOnly, pageNumber);
			ViewBag.name = name;
			ViewBag.activeOnly = activeOnly;
			return View("TypeOfRestList", restTypes);
		}

		public ActionResult Insert()
		{
			SetUnitOfWorkInRefClass(UnitOfWork);
			var newRecod = new TypeOfRest();
			newRecod.IsActive = true;
			ViewBag.RestTypes = GetSelectListOfParents(null);
			return View("TypeOfRestEdit", newRecod);
		}

		public ActionResult Update(long id)
		{
			var record = ApiController.Get(id);
			ViewBag.RestTypes = GetSelectListOfParents(record.ParentId);
			return View("TypeOfRestEdit", record);
		}

		[HttpPost]
		public ActionResult Save(TypeOfRest type)
		{
			SetUnitOfWorkInRefClass(UnitOfWork);
			if (!ModelState.IsValid)
			{
				ViewBag.RestTypes = GetSelectListOfParents(type.ParentId);
				return View("TypeOfRestEdit", type);
			}
			if (type.Id == 0)
			{
				// расчет максимального ИД
				var maxId = UnitOfWork.GetSet<TypeOfRest>().Select(t => t.Id).Max() + 1;
				type.Id = maxId;
				ApiController.Post(type);
			}
			else
			{
				ApiController.Put(type.Id, type);
			}
			return RedirectToAction("Update", new {id = type.Id});
		}

		private List<SelectListItem> GetSelectListOfParents(long? selectedId)
		{
			var parents =
				ApiController.Get()
					.Select(
						x =>
							new SelectListItem
							{
								Value = x.Id.ToString(CultureInfo.InvariantCulture),
								Text = x.Name,
								Selected = selectedId.HasValue && selectedId.Value == x.Id
							})
					.InsertAt(new SelectListItem
					{
						Value = string.Empty,
						Text = string.Empty,
						Selected = false
					});

			return parents.ToList();
		}
	}
}
