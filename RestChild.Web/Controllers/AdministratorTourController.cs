using System;
using System.Collections.Generic;
using System.Linq;
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
	public class AdministratorTourController : BaseController
	{
		public WebAdministratorTourController ApiController { get; set; }

		public StateController ApiStateController { get; set; }

		public WebAccountController ApiAccountController { get; set; }

		public override void SetUnitOfWorkInRefClass(IUnitOfWork unitOfWork)
		{
			base.SetUnitOfWorkInRefClass(unitOfWork);
			ApiController.SetUnitOfWorkInRefClass(unitOfWork);
			ApiStateController.SetUnitOfWorkInRefClass(unitOfWork);
			ApiAccountController.SetUnitOfWorkInRefClass(unitOfWork);
		}

		public ActionResult Search(AdministratorTourFilterModel filter)
		{
         if (!Security.HasAnyRights(new[] { AccessRightEnum.AdministratorTour.View }))
         {
            return RedirectToAvalibleAction();
         }
         SetUnitOfWorkInRefClass(UnitOfWork);
			filter = filter ?? new AdministratorTourFilterModel();
			filter.Result = ApiController.Get(filter);
			filter.States = ApiController.GetStates();
			return View("AdministratorTourList", filter);
		}

		public ActionResult Insert()
		{
			SetUnitOfWorkInRefClass(UnitOfWork);
			var administratorTour = new AdministratorTour
			{
				StateId = StateMachineStateEnum.AdministratorTour.Editing,
				State = ApiStateController.GetState(StateMachineStateEnum.AdministratorTour.Editing)
			};

			return GetAdministratorTourViewResult(administratorTour, null);
		}

		public ActionResult Update(long id)
		{
			SetUnitOfWorkInRefClass(UnitOfWork);
			var administratorTour = ApiController.Get(id);
			if (administratorTour == null)
			{
				return RedirectToAction("Search");
			}

			return GetAdministratorTourViewResult(administratorTour, null);
		}

		public ActionResult Save(AdministratorTourModel model)
		{
			SetUnitOfWorkInRefClass(UnitOfWork);

			var administratorTour = model.BuildData();

			if (administratorTour.StateId == StateMachineStateEnum.AdministratorTour.Editing)
			{

				if (!ModelState.IsValid || !ValidateModel(model))
				{
					if (administratorTour.LinkedAccountId.HasValue)
					{
						administratorTour.LinkedAccount = ApiAccountController.GetAccount(administratorTour.LinkedAccountId.Value);
					}

					return GetAdministratorTourViewResult(administratorTour, model.StateMachineActionString);
				}
			}

			if (administratorTour.Id == 0)
			{
				administratorTour.DateCreate = DateTime.Now;
				administratorTour.DateUpdate = administratorTour.DateCreate;
				ApiController.Post(administratorTour);
			}
			else
			{
				if (administratorTour.StateId != model.Data.StateId)
				{
					return RedirectToAction("Update", new { Id = administratorTour.Id });
				}

				administratorTour.DateUpdate = DateTime.Now;
				if (administratorTour.StateId == StateMachineStateEnum.AdministratorTour.Editing)
				{
					ApiController.Put(administratorTour.Id, administratorTour);
				}

				if (model.StateMachineActionString == AccessRightEnum.AdministratorTour.CreateAccount)
				{
					if (Security.HasRight(AccessRightEnum.AdministratorTour.CreateAccount))
					{
						ApiController.CreateAccount(model.Data.Id);
					}

				} else if (!string.IsNullOrEmpty(model.StateMachineActionString))
				{
					var isOk = ApiController.ChangeStatus(administratorTour.Id, model.StateMachineActionString);

					if (Security.HasRight(AccessRightEnum.AdministratorTour.CreateAccount) && model.StateMachineActionString == AccessRightEnum.AdministratorTour.ToFormed)
					{
						ApiController.CreateAccount(model.Data.Id);
					}

					return RedirectToAction("Update", new { id = administratorTour.Id, actionCode = isOk ? null : model.StateMachineActionString });
				}

			}

			return RedirectToAction("Update", new { Id = administratorTour.Id });
		}

		[HttpGet]
		public ActionResult AdministratorsForAdd(AdministratorTourFilterModel filter)
		{
			SetUnitOfWorkInRefClass(UnitOfWork);
			filter = filter ?? new AdministratorTourFilterModel();
			filter.StateId = StateMachineStateEnum.AdministratorTour.Formed;
			filter.Result = ApiController.Get(filter);
			return View("Assets/AdministratorList", filter);
		}

		private ActionResult GetAdministratorTourViewResult(AdministratorTour administratorTour, string actionCode)
		{
			var isEditable = (administratorTour.StateId ?? StateMachineStateEnum.AdministratorTour.Editing) == StateMachineStateEnum.AdministratorTour.Editing && Security.HasRight(AccessRightEnum.AdministratorTour.Manage);
			var state = ApiStateController.GetState(administratorTour.StateId ?? StateMachineStateEnum.AdministratorTour.Editing);
			var result = new AdministratorTourModel(administratorTour)
								{
									IsEditable = isEditable,
									State =
										new ViewModelState
											{
												Actions = ApiStateController.GetActions(state, StateMachineEnum.AdministratorTour),
												State = state,
												FormSelector = "#administratorForm",
												ActionSelector = "#StateMachineActionString",
												CanReturn = true,
												ReturnController = "AdministratorTour",
												ReturnAction = "Search",
												NeedSaveButton = isEditable,
												NeedRemoveButton = false
											}
								};

			if (state.Id == StateMachineStateEnum.AdministratorTour.Formed && Security.HasRight(AccessRightEnum.AdministratorTour.CreateAccount))
			{
				result.State.Actions.Add(new StateMachineAction
				{
					ActionCode = AccessRightEnum.AdministratorTour.CreateAccount,
					ActionName = "Создать пользователя",
					Description = "Создать пользователя?"
				});
			}

			SetupVocabularies(result);
			if (!string.IsNullOrEmpty(actionCode))
			{
				var errors = ApiController.GetErrorsOfChageStatus(administratorTour.Id, actionCode);
				if (errors != null && errors.Any())
				{
					foreach (var error in errors)
					{
						ModelState.AddModelError(string.Empty, error);
					}
				}
			}
			return View("AdministratorTourEdit", result);
		}

		private void SetupVocabularies(AdministratorTourModel model)
		{
			model.DocumentTypes = ApiController.GetAvailableDocumentTypes();
		}

		private bool ValidateModel(AdministratorTourModel model)
		{
			if (model == null || model.Data == null)
			{
				ModelState.AddModelError(string.Empty, "Ошибка передачи данных");
				return false;
			}

			var AdministratorTour = model.BuildData();
			bool isOk = true;

			if (string.IsNullOrEmpty(AdministratorTour.FirstName))
			{
				isOk = false;
				ModelState.AddModelError("Data.FirstName", "Необходимо указать имя");
			}

			if (string.IsNullOrEmpty(AdministratorTour.MiddleName) && !AdministratorTour.HaveMiddleName)
			{
				isOk = false;
				ModelState.AddModelError("Data.MiddleName", "Необходимо указать отчество");
			}

			if (string.IsNullOrEmpty(AdministratorTour.LastName))
			{
				isOk = false;
				ModelState.AddModelError("Data.LastName", "Необходимо указать фамилию");
			}

			if (!model.IsMale.HasValue)
			{
				isOk = false;
				ModelState.AddModelError("IsMale", "Необходимо указать пол");
			}

			if (!AdministratorTour.DateOfBirth.HasValue)
			{
				isOk = false;
				ModelState.AddModelError("Data.DateOfBirth", "Необходимо указать дату рождения");
			}

			if (string.IsNullOrEmpty(AdministratorTour.PlaceOfBirth))
			{
				isOk = false;
				ModelState.AddModelError("Data.PlaceOfBirth", "Необходимо указать место рождения");
			}

			if (AdministratorTour.DocumentTypeId == null)
			{
				isOk = false;
				ModelState.AddModelError("Data.DocumentTypeId", "Необходимо указать документ, подтверждающий личность");
			}

			if (string.IsNullOrEmpty(AdministratorTour.DocumentSeria))
			{
				isOk = false;
				ModelState.AddModelError("Data.DocumentSeria", "Необходимо указать серию документа");
			}

			if (string.IsNullOrEmpty(AdministratorTour.DocumentNumber))
			{
				isOk = false;
				ModelState.AddModelError("Data.DocumentNumber", "Необходимо указать номер документа");
			}

			if (!AdministratorTour.DocumentDateOfIssue.HasValue)
			{
				isOk = false;
				ModelState.AddModelError("Data.DocumentDateOfIssue", "Необходимо указать дату выдачи документа");
			}

			if (string.IsNullOrEmpty(AdministratorTour.DocumentSubjectIssue))
			{
				isOk = false;
				ModelState.AddModelError("Data.DocumentSubjectIssue", "Необходимо указать, кем выдан документ");
			}

			return isOk;
		}
	}
}
