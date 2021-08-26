using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using RestChild.Comon;
using RestChild.Comon.Enumeration;
using RestChild.Extensions.Filter;
using RestChild.DAL;
using RestChild.Domain;
using RestChild.Web.Controllers.WebApi;
using RestChild.Web.Models;

namespace RestChild.Web.Controllers
{
	[Authorize]
	public class DirectoryFlightsController : BaseController
	{
		public WebDirectoryFlightsController ApiController { get; set; }
		public StateController ApiStateController { get; set; }
		public WebCityController ApiCityController { get; set; }
		public WebRestYearController ApiRestYearController { get; set; }
		public WebTypesOfTransportsController ApiTypesOfTransportsController { get; set; }

		public override void SetUnitOfWorkInRefClass(IUnitOfWork unitOfWork)
		{
			base.SetUnitOfWorkInRefClass(unitOfWork);
			ApiController.SetUnitOfWorkInRefClass(unitOfWork);
			ApiStateController.SetUnitOfWorkInRefClass(unitOfWork);
			ApiCityController.SetUnitOfWorkInRefClass(unitOfWork);
			ApiRestYearController.SetUnitOfWorkInRefClass(unitOfWork);
		}

		// GET: DirectoryFlights
		public ActionResult Insert()
		{
			SetUnitOfWorkInRefClass(UnitOfWork);
			if (!Security.HasRightForSomeOrganization(AccessRightEnum.DirectoryFlightsManage))
			{
				return RedirectToAvalibleAction();
			}

			var entity = new DirectoryFlights()
							{
								StateId = StateMachineStateEnum.DirectoryFlights.Forming,
								YearOfRestId = ApiRestYearController.GetCurrentYearId(),
								YearOfRest = ApiRestYearController.GetCurrentYear()
							};
			return GetViewResult(entity, null);
		}

		public ActionResult Search(DirectoryFlightsFilterModel filter)
		{
			SetUnitOfWorkInRefClass(UnitOfWork);
			if (!Security.HasRightForSomeOrganization(AccessRightEnum.DirectoryFlightsManage))
			{
				return RedirectToAvalibleAction();
			}

			filter = filter ?? new DirectoryFlightsFilterModel();
			filter.YearOfRestId = filter.YearOfRestId ?? ApiRestYearController.GetCurrentYearId();
			filter.Result = ApiController.Get(filter);
			filter.TypesOfTransports = ApiTypesOfTransportsController.Get();
			filter.Cities = ApiCityController.GetActive();
			filter.Years = ApiRestYearController.Get().ToList();
			filter.States = ApiController.GetStates();
			return View("DirectoryFlightsList", filter);
		}

		public ActionResult Update(long id, string actionCode)
		{
			SetUnitOfWorkInRefClass(UnitOfWork);
			if (!Security.HasRightForSomeOrganization(AccessRightEnum.DirectoryFlightsManage))
			{
				return RedirectToAvalibleAction();
			}

			var directoryFlight = ApiController.Get(id);
			if (directoryFlight == null)
			{
				return RedirectToAction("Search");
			}

			return GetViewResult(directoryFlight, actionCode);
		}

		public ActionResult Save(DirectoryFlightsModel model)
		{
			SetUnitOfWorkInRefClass(UnitOfWork);
			if (!Security.HasRightForSomeOrganization(AccessRightEnum.DirectoryFlightsManage))
			{
				return RedirectToAvalibleAction();
			}

			var directoryFlight = model.BuildData();
			if (!ModelState.IsValid && directoryFlight.StateId == StateMachineStateEnum.DirectoryFlights.Forming)
			{
				return GetViewResult(directoryFlight, model.StateMachineActionString);
			}

			if (directoryFlight.Id == 0)
			{
				directoryFlight.HistoryLink = new HistoryLink();
				ApiController.Post(directoryFlight);
			}
			else
			{
				if (directoryFlight.StateId == StateMachineStateEnum.DirectoryFlights.Forming)
				{
					ApiController.Put(directoryFlight.Id, directoryFlight);
				}

				if (!string.IsNullOrEmpty(model.StateMachineActionString))
				{
					var isOk = ApiController.ChangeState(directoryFlight.Id, model.StateMachineActionString);
					return RedirectToAction("Update", new { id = directoryFlight.Id, actionCode = isOk ? null : model.StateMachineActionString });
				}
			}
			return RedirectToAction("Update", new { directoryFlight.Id });
		}

		private ActionResult GetViewResult(DirectoryFlights directoryFlight, string actionCode)
		{
			var state = directoryFlight.StateId.HasValue ? ApiStateController.GetState(directoryFlight.StateId.Value) : null;
			var actions = directoryFlight.Id != 0 && directoryFlight.StateId.HasValue
				? ApiStateController.GetActions(state, StateMachineEnum.DirectoryFlightsState)
				: new List<StateMachineAction>();
			var isEditable = directoryFlight.StateId == StateMachineStateEnum.DirectoryFlights.Forming;
			var result = new DirectoryFlightsModel(new DirectoryFlights(directoryFlight, 2))
			{
				IsEditable = isEditable,
				State =
					new ViewModelState
					{
						Actions = actions,
						State = state,
						FormSelector = "#directoryFlightForm",
						ActionSelector = "#StateMachineActionString",
						CanReturn = true,
						ReturnController = "DirectoryFlight",
						ReturnAction = "Search",
						NeedSaveButton = isEditable,
						NeedRemoveButton =
							directoryFlight.Id != 0 && isEditable
					}
			};

			SetupVocabularies(result);
			if (directoryFlight.HistoryLinkId.HasValue)
			{
				result.State.PostNoStatusActions = result.State.PostNoStatusActions ?? new List<NoStatusAction>();
				result.State.PostNoStatusActions.Add(new NoStatusAction
				{
					Name = "История",
					ButtonClass = "btn btn-default btn-hystory-link",
					SomeAddon = string.Format("data-history-id=\"{0}\"", directoryFlight.HistoryLinkId)
				});
			}
			if (!string.IsNullOrEmpty(actionCode))
			{
				var errors = ApiController.GetErrorsOfChageStatus(directoryFlight.Id, actionCode);
				if (errors != null && errors.Any())
				{
					foreach (var error in errors)
					{
						ModelState.AddModelError(string.Empty, error);
					}
				}
			}
			return View("DirectoryFlightsEdit", result);
		}

		private void SetupVocabularies(DirectoryFlightsModel result)
		{
			result.Cities = ApiCityController.GetActive();
			result.Years = ApiRestYearController.Get().ToList();
			result.TypesOfTransports = ApiTypesOfTransportsController.Get();
		}
	}
}
