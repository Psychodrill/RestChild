using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using RestChild.Comon;
using RestChild.Comon.Enumeration;
using RestChild.Domain;
using RestChild.Extensions.Filter;
using RestChild.Web.Controllers.WebApi;
using RestChild.Web.Extensions;
using RestChild.Web.Models;

namespace RestChild.Web.Controllers
{
	/// <summary>
	///     реестор договоров контрактов
	/// </summary>
	[Authorize]
	public class ContractController : BaseController
	{
		public WebContractController ApiController { get; set; }
		public StateController ApiStateController { get; set; }
		public WebApi.OrganizationController ApiOrganizationController { get; set; }
		public WebRestYearController ApiYearController { get; set; }
		public WebDirectoryFlightsController WebDirectoryFlightsController { get; set; }
		public WebToursController ApiTourController { get; set; }

		public override void SetUnitOfWorkInRefClass(IUnitOfWork unitOfWork)
		{
			base.SetUnitOfWorkInRefClass(unitOfWork);
			ApiController.SetUnitOfWorkInRefClass(unitOfWork);
			ApiStateController.SetUnitOfWorkInRefClass(unitOfWork);
			ApiOrganizationController.SetUnitOfWorkInRefClass(unitOfWork);
			ApiYearController.SetUnitOfWorkInRefClass(unitOfWork);
			WebDirectoryFlightsController.SetUnitOfWorkInRefClass(unitOfWork);
			ApiTourController.SetUnitOfWorkInRefClass(unitOfWork);
		}

		/// <summary>
		///     поиск.
		/// </summary>
		/// <param name="filter"></param>
		/// <returns></returns>
		public ActionResult Search(ContractFilterModel filter)
		{
			SetUnitOfWorkInRefClass(UnitOfWork);
			if (!Security.HasRight(AccessRightEnum.Contract.View) && !Security.HasRight(AccessRightEnum.Contract.ViewCommercial))
			{
				return RedirectToAvalibleAction();
			}

			filter = filter ?? new ContractFilterModel();
			filter.Years = ApiYearController.Get().ToList();
			if (!filter.YearOfRestId.HasValue)
			{
				filter.YearOfRestId = filter.Years?.FirstOrDefault(y => y.Year == DateTime.Now.Year)?.Id ??
				                      filter.Years?.LastOrDefault()?.Id;
			}
			filter.Result = ApiController.Get(filter);
			filter.States = ApiController.GetStates();
			filter.Oivs = ApiOrganizationController.GetOivs(null).ToList();
			return View("ContractList", filter);
		}

		public ActionResult Insert()
		{
			SetUnitOfWorkInRefClass(UnitOfWork);
			if (!Security.HasRight(AccessRightEnum.Contract.Manage))
			{
				return RedirectToAction("Search");
			}

			var contract = new Contract
			{
				StateId = StateMachineStateEnum.Contract.New,
				State = ApiStateController.GetState(StateMachineStateEnum.Contract.New)
			};

			return GetContractViewResult(contract, null);
		}

		private ActionResult GetContractViewResult(Contract contract, string actionCode)
		{
			SetUnitOfWorkInRefClass(UnitOfWork);
			var state = contract.StateId.HasValue ? ApiStateController.GetState(contract.StateId.Value) : null;
			var actions = contract.Id != 0 && contract.StateId.HasValue
				? ApiStateController.GetActions(state, StateMachineEnum.ContractState)
				: new List<StateMachineAction>();
			var isEditable = contract.StateId == StateMachineStateEnum.Contract.New &&
			                 Security.HasRight(AccessRightEnum.Contract.Manage);
			var result = new ContractModel(new Contract(contract, 1) {AddonAgreements = contract.AddonAgreements})
			{
				IsEditable = isEditable,
				State =
					new ViewModelState
					{
						Actions = actions,
						State = state,
						FormSelector = "#contractForm",
						ActionSelector = "#StateMachineActionString",
						CanReturn = true,
						ReturnController = "Contract",
						ReturnAction = "Search",
						NeedSaveButton = isEditable,
						NeedRemoveButton =
							contract.Id != 0 && isEditable
					}
			};
			SetupVocabularies(result);

			if (!result.Data.YearOfRestId.HasValue)
			{
				result.Data.YearOfRestId = result.Years?.FirstOrDefault(y => y.Year == DateTime.Now.Year)?.Id ??
				                           result.Years?.LastOrDefault()?.Id;
			}

			if (contract.HistoryLinkId.HasValue)
			{
				result.State.PostNoStatusActions = result.State.PostNoStatusActions ?? new List<NoStatusAction>();
				result.State.PostNoStatusActions.Add(new NoStatusAction
				{
					Name = "История",
					ButtonClass = "btn btn-default btn-hystory-link",
					SomeAddon = $"data-history-id=\"{contract.HistoryLinkId}\""
				});
			}

			if (result.Data.OnTransport)
			{
				result.DirectoryFlightsFilterModel = new DirectoryFlightsFilterModel
				{
					ContractFiltered = true,
					ContractId = contract.Id,
					PageSize = 100
				};
				result.DirectoryFlightsFilterModel.Result = WebDirectoryFlightsController.Get(result.DirectoryFlightsFilterModel);
			}

			if (result.Data.OnRest)
			{
				result.ToursFilterModel = new ToursFilterModel
				{
					ContractId = contract.Id,
					ContractFiltered = true,
					PageSize = 100
				};
				result.ToursFilterModel.Result = ApiTourController.Get(result.ToursFilterModel);
			}

			return View("ContractEdit", result);
		}

		public ActionResult Update(long id)
		{
			SetUnitOfWorkInRefClass(UnitOfWork);
			if (!Security.HasRight(AccessRightEnum.Contract.View) && !Security.HasRight(AccessRightEnum.Contract.ViewCommercial))
			{
				return RedirectToAvalibleAction();
			}

			var contract = ApiController.Get(id);
			if (contract == null)
			{
				return RedirectToAction("Search");
			}

			return GetContractViewResult(contract, null);
		}

		[HttpGet]
		public ActionResult Save()
		{
			return RedirectToAction("Search");
		}

		[HttpPost]
		public ActionResult Save(ContractModel model)
		{
			SetUnitOfWorkInRefClass(UnitOfWork);
			ModelState.Remove("Data.Summa");
			var contract = model.BuildData();
			if (!ModelState.IsValid && contract.StateId == StateMachineStateEnum.Contract.New)
			{
				return GetContractViewResult(contract, model.StateMachineActionString);
			}

			if (contract.Id == 0)
			{
				contract.HistoryLink = ApiStateController.WriteHistory(contract.HistoryLink, "Добавление новой записи", string.Empty);
				contract.HistoryLinkId = contract.HistoryLink?.Id;
				ApiController.Post(contract);
			}
			else
			{
				if (contract.StateId == StateMachineStateEnum.Contract.New)
				{
					contract = ApiController.Put(contract.Id, contract);
					contract.HistoryLink = ApiStateController.WriteHistory(contract.HistoryLink, "Сохранение сведений", string.Empty);
					contract.HistoryLinkId = contract.HistoryLink?.Id;
				}

				if (!string.IsNullOrEmpty(model.StateMachineActionString))
				{
					var isOk = ApiController.ChangeState(contract.Id, model.StateMachineActionString);
					if (!isOk)
					{
						SetErrors(ApiController.GetErrorsOfChageStatus(contract.Id, model.StateMachineActionString));
					}
				}
			}

			UnitOfWork.SaveChanges();

			return RedirectToAction("Update", new {contract.Id});
		}

		private void SetupVocabularies(ContractModel model)
		{
			model.Years = ApiYearController.Get().ToList();
			model.Oivs = ApiOrganizationController.GetOivs(null).ToList();
		}
	}
}
