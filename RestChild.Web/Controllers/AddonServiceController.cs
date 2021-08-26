using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using RestChild.Comon;
using RestChild.Comon.Dto.Commercial;
using RestChild.Comon.Enumeration;
using RestChild.Extensions.Filter;
using RestChild.DAL.RepositoryExtensions;
using RestChild.Domain;
using RestChild.Web.Controllers.WebApi;
using RestChild.Web.Models;

namespace RestChild.Web.Controllers
{
	public class AddonServiceController : BaseController
	{
		public WebApi.AddonServiceController ApiController { get; set; }
		public WebHotelsController ApiHotelsController { get; set; }
		public StateController StateController { get; set; }

		public override void SetUnitOfWorkInRefClass(IUnitOfWork unitOfWork)
		{
			base.SetUnitOfWorkInRefClass(unitOfWork);
			ApiController.SetUnitOfWorkInRefClass(unitOfWork);
			StateController.SetUnitOfWorkInRefClass(unitOfWork);
			ApiHotelsController.SetUnitOfWorkInRefClass(unitOfWork);
		}

		public ActionResult Search(AddonServiceFilterModel filter)
		{
			SetUnitOfWorkInRefClass(UnitOfWork);
			if (!Security.HasRight(AccessRightEnum.AddonService.View))
			{
				return RedirectToAvalibleAction();
			}

			filter = filter ?? new AddonServiceFilterModel();
			filter.Result = ApiController.Search(filter);

			filter.States = ApiController.GetStates();
			filter.Types = UnitOfWork.GetSet<TypeOfService>().Where(t => t.IsActive).OrderBy(t => t.Name).ToArray();

			var hotel = filter.HotelId.HasValue
				? ApiHotelsController.Get(filter.HotelId.Value)
				: null;
			filter.HotelName = hotel != null ? hotel.Name : string.Empty;

			return View("List", filter);
		}

		[HttpGet]
		public ActionResult Save()
		{
			return RedirectToAction("Search");
		}

		[HttpPost]
		public ActionResult Save(AddonServicesModel model)
		{
			SetUnitOfWorkInRefClass(UnitOfWork);
			var entity = model.BuildData();
			if (!ModelState.IsValid && entity.StateId == StateMachineStateEnum.AddonService.Forming)
			{
				PrepareModelService(model, null);
				return View("Edit", model);
			}

			if (UnitOfWork.GetLastUpdateTickById<AddonServices>(entity.Id) != entity.LastUpdateTick)
			{
				SetRedicted();
				return RedirectToAction("Edit", new { id = entity.Id, activeTab = model.ActiveTab });
			}

			if (entity.StateId == StateMachineStateEnum.AddonService.Forming)
			{
				entity = ApiController.Save(entity);
				UnitOfWork.DetachEntity(entity);
			}

			if (!string.IsNullOrEmpty(model.StateMachineActionString))
			{
				ApiController.ChangeStatus(entity.Id, model.StateMachineActionString);
			}

			return RedirectToAction("Edit", new { id = entity.Id, activeTab = model.ActiveTab, smas = model.StateMachineActionString });
		}

		/// <summary>
		///     редактирование
		/// </summary>
		/// <param name="id"></param>
		/// <param name="activeTab"></param>
		/// <param name="smas"></param>
		/// <returns></returns>
		public ActionResult Edit(long? id, string activeTab, string smas)
		{
			SetUnitOfWorkInRefClass(UnitOfWork);
			var model = new AddonServicesModel(id.HasValue
				? UnitOfWork.GetById<AddonServices>(id.Value)
				: new AddonServices
				{
					IsActive = true,
					StateId = StateMachineStateEnum.AddonService.Forming,
					GeneralService = true
				}) {ActiveTab = activeTab};


			if (!model.Data.GeneralService)
			{
				return RedirectToAction("Search");
			}

			PrepareModelService(model, smas);

			return View("Edit", model);
		}

		private void PrepareModelService(AddonServicesModel model, string smas)
		{
			var typeOfServices = UnitOfWork.GetSet<TypeOfService>().Where(t => t.IsActive).OrderBy(t => t.Name).ToList();
			typeOfServices.Insert(0, new TypeOfService {Id = 0, Name = "-- Не выбрано --"});
			var availableTypes = AvailableServiceTypesBySecurity();
			availableTypes.Add(0); // для значения "-- Не выбрано --"

			// Есть право на общее редактирование (любой тип(вид) доп.услуги).
			var accessAllTypes = Security.HasRight(AccessRightEnum.AddonService.Edit);
			// Есть право на редактирование указанного в модели типа(вида) доп.услуги.
			var accessCurrentType = model.Data.Id == 0 || !model.Data.TypeOfServiceId.HasValue || availableTypes.Contains((ServiceEnum) model.Data.TypeOfServiceId);

			// Проверка доступа к редактированию доп. услуги в зависимости от типа(вида) услуги и соответствующих прав.
			// При наличии права на абстрактное редактирование доступ не зависит от типа услуги.
			model.IsEditable = model.Data.StateId == StateMachineStateEnum.AddonService.Forming && (accessAllTypes || accessCurrentType);

			model.TypeOfServices = typeOfServices
				.Select(i => new SelectListItem { Value = i.Id.FormatEx(), Text = i.Name, Disabled = !accessAllTypes && !availableTypes.Select(e => (long)e).Contains(i.Id) })
				.ToList();

			model.Data.State = model.Data.State ?? StateController.GetState(StateMachineStateEnum.AddonService.Forming);
			model.TypePriceCalculations = UnitOfWork.GetSet<TypePriceCalculation>().OrderBy(t => t.Name).ToList();
			model.Hotels = model.Hotels ?? new List<BaseResponse>();
			model.Citys = UnitOfWork.GetSet<City>().Where(t => t.IsActive).OrderBy(t => t.Name).ToList();
			model.Citys.Insert(0, new City { Id = -1, Name = "-- Не выбрано --" });

			if (model.Hotels.Any(h => string.IsNullOrEmpty(h.Name)))
			{
				var idHotels = model.Hotels.Select(h => h.Id).ToList();
				model.Hotels =
					UnitOfWork.GetSet<Hotels>()
						.Where(h => idHotels.Contains(h.Id))
						.ToList()
						.Select(h => new BaseResponse {Id = h.Id, Name = h.Name})
						.ToList();
			}

			model.State =
				new ViewModelState
				{
					Actions = (accessAllTypes || accessCurrentType) ? StateController.GetActions(model.Data.State, StateMachineEnum.AddonServiceState) : new List<StateMachineAction>(),
					State = model.Data.State,
					FormSelector = "#addonServiceForm",
					ActionSelector = "#StateMachineActionString",
					CanReturn = true,
					ReturnController = "AddonService",
					ReturnAction = "Search",
					JsFunctionToAction = "confirmStateButtonAddonService",
					NeedSaveButton = model.IsEditable
				};

			if (model.Data.HistoryLinkId.HasValue)
			{
				model.State.PostNoStatusActions = model.State.PostNoStatusActions ?? new List<NoStatusAction>();
				model.State.PostNoStatusActions.Add(new NoStatusAction
				{
					Name = "История",
					ButtonClass = "btn btn-default btn-hystory-link",
					SomeAddon = string.Format("data-history-id=\"{0}\"", model.Data.HistoryLinkId)
				});
			}

			if (!string.IsNullOrEmpty(smas))
			{
				model.CheckModel();
			}

			if (!model.Data.TypePriceCalculationId.HasValue)
			{
				model.Data.TypePriceCalculationId = (long) TypePriceCalculationEnum.Once;
			}
		}

		/// <summary>
		/// Получение списка типов(видов) доп.услуг по наличию соответствующих прав.
		/// </summary>
		private static List<ServiceEnum> AvailableServiceTypesBySecurity()
		{
			var types = new List<ServiceEnum>();

			if (Security.HasRight(AccessRightEnum.TypeOfServiceExcursion))     types.Add(ServiceEnum.Excursion);
			if (Security.HasRight(AccessRightEnum.TypeOfServiceVisa))          types.Add(ServiceEnum.Visa);
			if (Security.HasRight(AccessRightEnum.TypeOfServiceTransferAero))  types.Add(ServiceEnum.TransferAero);
			if (Security.HasRight(AccessRightEnum.TypeOfServiceInsurance))     types.Add(ServiceEnum.Insurance);
			if (Security.HasRight(AccessRightEnum.TypeOfServiceAddonPlace))    types.Add(ServiceEnum.AddonPlace);
			if (Security.HasRight(AccessRightEnum.TypeOfServiceTransferTrain)) types.Add(ServiceEnum.TransferTrain);
			if (Security.HasRight(AccessRightEnum.TypeOfServiceTransferAuto))  types.Add(ServiceEnum.TransferAuto);
			if (Security.HasRight(AccessRightEnum.TypeOfServiceOther))         types.Add(ServiceEnum.Other);

			return types;
		}
	}
}
