using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Newtonsoft.Json;
using RestChild.Comon;
using RestChild.Comon.Enumeration;
using RestChild.Domain;
using RestChild.Web.Controllers.WebApi;
using RestChild.Web.Logic;
using RestChild.Web.Models;
using RestChild.Web.Models.PedParty;

namespace RestChild.Web.Controllers
{
	public class PedPartyController : BaseController
	{
		private readonly PedPartyLogic _pedPartyLogic;
		public StateController ApiStateController { get; set; }

		public PedPartyController(PedPartyLogic pedPartyLogic)
		{
			_pedPartyLogic = pedPartyLogic;
		}

		public override void SetUnitOfWorkInRefClass(IUnitOfWork unitOfWork)
		{
			base.SetUnitOfWorkInRefClass(unitOfWork);
			ApiStateController.SetUnitOfWorkInRefClass(unitOfWork);
			_pedPartyLogic.SetUnitOfWorkInRefClass(UnitOfWork);
		}

		/// <summary>
		///     Все сформированные педотряды
		/// <param name="query">Название отряда</param>
		/// </summary>
		public ContentResult AllFormedPedParties(string query)
		{
			SetUnitOfWorkInRefClass(UnitOfWork);

			var parties = _pedPartyLogic.GetAllFormedPedParties(query);

			var str = JsonConvert.SerializeObject(parties);
			return new ContentResult {Content = str, ContentType = "application/json"};
		}

		/// <summary>
		/// Поиск педотрядов
		/// </summary>
		/// <param name="filter">фильтр поиска</param>
		/// <returns></returns>
		public ActionResult Search(PedPartyFilterModel filter)
		{
			if (!Security.HasRight(AccessRightEnum.PedParty.PedPartyView))
			{
				return RedirectToAction("Index","Home");
			}
			SetUnitOfWorkInRefClass(UnitOfWork);

			if (filter.PageNumber == 0)
				filter.PageNumber = 1;

			filter.Result = _pedPartyLogic.GetPedParties(filter);
			return View("PedPartiesList", filter);
		}

		public ActionResult Insert()
		{
			if (!Security.HasRight(AccessRightEnum.PedParty.PedPartyManage))
			{
				return RedirectToAction("Search");
			}
			SetUnitOfWorkInRefClass(UnitOfWork);
			return View("PedPartyEdit", CreateModel(new PedParty()));
		}

		public ActionResult Update(long id)
		{
			SetUnitOfWorkInRefClass(UnitOfWork);
			var entity = UnitOfWork.GetSet<PedParty>().FirstOrDefault(i => i.Id == id);

			if (entity?.StateId == StateMachineStateEnum.Deleted)
			{
				return RedirectToAction("Search");
			}

			return View("PedPartyEdit", CreateModel(entity));
		}

		[HttpPost]
		public ActionResult Save(PedPartyModel pedPartyModel)
		{
			var currentAccountId = Security.GetCurrentAccountId();
			if (!Security.HasRight(AccessRightEnum.PedParty.PedPartyView) || !currentAccountId.HasValue)
			{
				return RedirectToAction("Search");
			}

			SetUnitOfWorkInRefClass(UnitOfWork);
			var data = pedPartyModel.BuildData();

			if ((!ModelState.IsValid || !ValidateModel(data)) && data.StateId == StateMachineStateEnum.PedParty.Editing)
			{
				data.State = ApiStateController.GetState(data.StateId ?? 0);

				return View("PedPartyEdit", CreateModel(pedPartyModel.Data));
			}

			if (Security.HasRight(AccessRightEnum.PedParty.PedPartyManage))
			{
				if (pedPartyModel.Data.StateId == StateMachineStateEnum.PedParty.Editing)
				{
					_pedPartyLogic.SavePedParty(pedPartyModel.Data, currentAccountId.Value);
				}

				if (pedPartyModel.StateMachineActionString == "Delete")
				{
					_pedPartyLogic.UpdateStatus(pedPartyModel.Data.Id, pedPartyModel.StateMachineActionString, currentAccountId.Value);
					return RedirectToAction("Search");
				}
			}



			if (!string.IsNullOrEmpty(pedPartyModel.StateMachineActionString) && Security.HasRight(pedPartyModel.StateMachineActionString))
			{
				var isOk = _pedPartyLogic.ChangeStatusPedParty(pedPartyModel.Data.Id, pedPartyModel.StateMachineActionString, currentAccountId.Value);
				return RedirectToAction("Update", new {id = data.Id, actionCode = isOk ? null : pedPartyModel.StateMachineActionString});
			}

			if (data.Id == 0)
			{
				return RedirectToAction("Search");
			}

			return RedirectToAction("Update", new {id = data.Id});
		}

		private PedPartyModel CreateModel(PedParty entity)
		{
			SetUnitOfWorkInRefClass(UnitOfWork);

			var model = new PedPartyModel(entity);

			model.State = new ViewModelState();

			if (entity.Id == 0)
			{
				model.IsEditable = true;
				var stateMachineState = ApiStateController.GetState((int) StateMachineStateEnum.PedParty.Editing);
				model.State.Actions = new List<StateMachineAction>();

				model.State.State = stateMachineState;
				model.Data.State = stateMachineState;
				model.Data.StateId = (int) StateMachineStateEnum.PedParty.Editing;

				model.State.NeedSaveButton = true;
			}
			else
			{
				model.State.State = entity.State;

				var stateMachineActions = ApiStateController.GetActions(entity.State, StateMachineEnum.PedParty);
				model.State.Actions = stateMachineActions;
				model.IsEditable = entity.StateId == StateMachineStateEnum.PedParty.Editing &&
				                   Security.HasRight(AccessRightEnum.PedParty.PedPartyManage);

				model.State.NeedSaveButton = model.IsEditable;
				model.State.NeedRemoveButton = model.IsEditable;

				model.State.CanReturn = true;
				model.State.ReturnController = "PedParty";
				model.State.ReturnAction = "Search";

				if (entity.HistoryLinkId.HasValue)
				{
					model.State.PostNoStatusActions = model.State.PostNoStatusActions ?? new List<NoStatusAction>();
					model.State.PostNoStatusActions.Add(new NoStatusAction
					{
						Name = "История",
						ButtonClass = "btn btn-default btn-hystory-link",
						SomeAddon = $"data-history-id=\"{entity.HistoryLinkId}\""
					});
				}
			}
			return model;
		}

		private bool ValidateModel(PedParty pedParty)
		{
			var isOk = true;
			if (pedParty == null)
			{
				ModelState.AddModelError(string.Empty, "Ошибка получения данных");
				return false;
			}
			if (string.IsNullOrWhiteSpace(pedParty.Name))
			{
				ModelState.AddModelError("Data.Name", "Необходимо указать название педотряда");
				isOk = false;
			}

			if (string.IsNullOrWhiteSpace(pedParty.City))
			{
				ModelState.AddModelError("Data.City", "Необходимо указать город");
				isOk = false;
			}
			return isOk;
		}
	}
}
