using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using RestChild.Comon;
using RestChild.Comon.Enumeration;
using RestChild.Extensions.Filter;
using RestChild.DAL.RepositoryExtensions;
using RestChild.Domain;
using RestChild.Web.Controllers.WebApi;
using RestChild.Web.Models;

namespace RestChild.Web.Controllers
{
	[Authorize]
	public class HotelsController : BaseController
	{
		/// <summary>
		///     Gets or sets the api controller.
		/// </summary>
		public WebHotelsController ApiController { get; set; }

		public WebRestPlaceController RestPlaceApiController { get; set; }

		public WebFileTypeController ApiFileTypeController { get; set; }

		public WebHotelTypeController ApiHotelTypeController { get; set; }

		public StateController ApiStateController { get; set; }

		public WebCityController ApiCityController { get; set; }

		public WebFunctioningTypeController ApiFunctioningTypeController { get; set; }

		public WebHotelPlacementController ApiHotelPlacementController { get; set; }

		public override void SetUnitOfWorkInRefClass(IUnitOfWork unitOfWork)
		{
			base.SetUnitOfWorkInRefClass(unitOfWork);
			ApiController.SetUnitOfWorkInRefClass(unitOfWork);
			RestPlaceApiController.SetUnitOfWorkInRefClass(unitOfWork);
			ApiFileTypeController.SetUnitOfWorkInRefClass(unitOfWork);
			ApiHotelTypeController.SetUnitOfWorkInRefClass(unitOfWork);
			ApiStateController.SetUnitOfWorkInRefClass(unitOfWork);
			ApiCityController.SetUnitOfWorkInRefClass(unitOfWork);
			ApiFunctioningTypeController.SetUnitOfWorkInRefClass(unitOfWork);
			ApiHotelTypeController.SetUnitOfWorkInRefClass(unitOfWork);
		}

		// GET: Hotels
		public override ActionResult Index()
		{
			return RedirectToAction(nameof(Search));
		}

		public ActionResult Search(HotelsFilterModel filter)
		{
			if (!Security.HasRightForSomeOrganization(AccessRightEnum.Hotel.View))
			{
				return RedirectToAvalibleAction();
			}

			SetUnitOfWorkInRefClass(UnitOfWork);
			filter = filter ?? new HotelsFilterModel();
			filter.Result = ApiController.Get(filter);
			SetupVocabularies(filter);
			return View("HotelsList", filter);
		}

		public ActionResult Insert()
		{
			if (!Security.HasRight(AccessRightEnum.Hotel.Manage))
			{
				return RedirectToAction("Search");
			}

			SetUnitOfWorkInRefClass(UnitOfWork);
			var newRecord = new Hotels();
			return GetView(newRecord);
		}

		public ActionResult Update(long id, string actionCode, string activeTab)
		{
			SetUnitOfWorkInRefClass(UnitOfWork);
			if (!Security.HasRight(AccessRightEnum.Hotel.Manage))
			{
				return RedirectToAction("Search");
			}

			var entity = ApiController.Get(id);
			if (!string.IsNullOrEmpty(actionCode))
			{
				var errors = ApiController.GetErrorsOfChageStatus(id, actionCode);
				if (errors != null && errors.Any())
				{
					foreach (var error in errors)
					{
						ModelState.AddModelError(string.Empty, error);
					}
				}
			}

			ViewBag.TypesOfRooms = entity.TypeOfRooms.ToList()
				.Select(i => new SelectListItem { Value = i.Id.FormatEx(), Text = i.Name }).ToList();
			ViewBag.Accommodations = entity.Accommodation.ToList()
				.Select(i => new SelectListItem { Value = i.Id.FormatEx(), Text = i.Name }).ToList();
			ViewBag.DiningOptions = UnitOfWork.GetSet<DiningOptions>().Where(i => i.IsActive == true).ToList()
				.Select(i => new SelectListItem { Value = i.Id.FormatEx(), Text = i.Name }).ToList();

			return GetView(entity, activeTab);
		}

		[HttpPost]
		public ActionResult Save(HotelsViewModel hotel)
		{
			if (!Security.HasRight(AccessRightEnum.Hotel.Manage))
			{
				return RedirectToAction("Search");
			}

			SetUnitOfWorkInRefClass(UnitOfWork);
			var data = hotel.BuildData();
			data.Files = LoadFileTypes(data.Files);
			if (data.Id > 0 && data.LastUpdateTick != UnitOfWork.GetLastUpdateTickById<Hotels>(data.Id))
			{
				SetRedicted();
				return RedirectToAction("Update", new { id = data.Id, activeTab = hotel.ActiveTab });
			}

			if ((!ModelState.IsValid || !ValidateModel(data)) && (data.StateId == StateMachineStateEnum.Hotel.Editing || data.StateId == StateMachineStateEnum.Hotel.OnReworking))
			{
				data.State = ApiStateController.GetState(data.StateId ?? 0);
				return GetView(data);
			}

			ClearFileTypes(data);

			if (data.TypeOfRooms?.Count > 0)
				FillPhotoFiles(data.TypeOfRooms, data.Id);

			if (data.Id == 0)
			{
				ApiController.Post(data);
			}
			else
			{
				if (data.StateId == StateMachineStateEnum.Hotel.Editing || data.StateId == StateMachineStateEnum.Hotel.OnReworking)
				{
					ApiController.Put(data.Id, data);
				}

				if (!string.IsNullOrEmpty(hotel.StateMachineActionString))
				{
					if (data.StateId == StateMachineStateEnum.Hotel.Editing || data.StateId == StateMachineStateEnum.Hotel.OnReworking)
					{
						var errors = new List<string>();
						var entity = UnitOfWork.GetById<Hotels>(data.Id);
						if (entity.AgeFrom > entity.AgeTo)
						{
							errors.Add("На закладке описание, возраст с должен быть не больше чем возраст по");
						}

						if (entity.Accommodation != null &&
							entity.Accommodation.Any(
							    a => a.AccommodationChildren != null && a.AccommodationChildren.Any(c => c.AgeFrom > c.AgeTo)))
						{
							errors.Add("На закладке номерное фонд, минимальный возраст должен быть не больше чем максимальный");
						}

						if (errors.Any())
						{
							SetErrors(errors);
							return RedirectToAction("Update", new { id = data.Id, activeTab = hotel.ActiveTab });
						}
					}

					var isOk = ApiController.ChangeStatus(data.Id, hotel.StateMachineActionString);
					return RedirectToAction("Update", new { id = data.Id, actionCode = isOk ? null : hotel.StateMachineActionString, activeTab = hotel.ActiveTab });
				}
			}

			return RedirectToAction("Update", new { id = data.Id, activeTab = hotel.ActiveTab });
		}

		private void FillPhotoFiles(ICollection<TypeOfRooms> typeOfRooms,long hotelId)
		{
			foreach (var typeOfRoom in typeOfRooms)
			{
				if (typeOfRoom.Files?.Count > 0)
				{
					foreach (var roomFile in typeOfRoom.Files)
					{
						if (typeOfRoom.Id != 0)
							roomFile.Id = typeOfRoom.Id;
						else
							roomFile.TypeOfRooms = typeOfRoom;

						roomFile.HotelId = hotelId;
						roomFile.FileName = $"Фотография {roomFile.FileUrl}";
					}
				}
			}
		}

		private ICollection<FileHotel> LoadFileTypes(ICollection<FileHotel> data)
		{
			if (data == null || !data.Any())
				return data;

			var fileTypes = ApiFileTypeController.Get().Where(t => t.ParentId.HasValue).ToList();

			return data.Select(
				f =>
				{
					if (f.FileTypeId.HasValue)
					{
						f.FileType = fileTypes.FirstOrDefault(t => t.Id == f.FileTypeId);
					}
					return f;
				}).ToArray();
		}

		private void ClearFileTypes(Hotels data)
		{
			if (data.Files != null && data.Files.Any())
			{
				var fileTypes =
					ApiFileTypeController.Get().Where(t => t.ParentId.HasValue).ToList();
				data.Files = data.Files.Select(
					f =>
					{
						if (f.FileTypeId.HasValue)
						{
							f.FileType = null;
						}
						return f;
					}).ToList();
			}
		}

		private void SetupVocabularies(HotelsFilterModel model)
		{
			model.Regions = RestPlaceApiController.GetActive();
			model.States = ApiController.GetStates();
			model.HotelTypes = ApiHotelTypeController.Get();
		}

		private void SetupVocabularies(HotelsViewModel model)
		{
			model.Regions = RestPlaceApiController.GetActive();
			model.FileTypes = ApiFileTypeController.Get().Where(t => t.ParentId.HasValue).ToList();
			model.HotelTypes = ApiHotelTypeController.Get();
			model.States = ApiController.GetStates();
			model.Cities = ApiCityController.GetActive();
			model.FunctioningType = ApiFunctioningTypeController.Get();
			model.HotelPlacements = ApiHotelPlacementController.Get();
		}

		private ActionResult GetView(Hotels entity, string activeTab = null)
		{
			List<StateMachineAction> stateActions = null;
			if (entity.Id == 0)
			{
				entity.StateId = (int)StateMachineStateEnum.Hotel.Editing;
				entity.State = ApiStateController.GetState((int)StateMachineStateEnum.Hotel.Editing);
				stateActions = new List<StateMachineAction>();
			}
			else
			{
				stateActions = ApiStateController.GetActions(entity.State, StateMachineEnum.HotelState);
			}
			var canEdit = (entity.StateId == StateMachineStateEnum.Hotel.Editing
			               || entity.StateId == StateMachineStateEnum.Hotel.OnReworking)
			              && Security.HasRight(AccessRightEnum.Hotel.Manage);

			if (entity.OrganizationId.HasValue && entity.Organization == null)
			{
				entity.Organization = UnitOfWork.GetById<Organization>(entity.OrganizationId.Value);
			}

			if (entity.CityId.HasValue && entity.City == null)
			{
				entity.City = UnitOfWork.GetById<City>(entity.CityId.Value);
			}

			var place = new HotelsViewModel(entity)
							{
								State =
									new ViewModelState
										{
											Actions = stateActions,
											State = entity.State,
											FormSelector = "#hotelsForm",
											ActionSelector = "#StateMachineActionString",
											CanReturn = true,
											ReturnController = "Hotels",
											ReturnAction = "Search",
											NeedSaveButton = canEdit
										},
								ActiveTab = activeTab,
								CanEdit = canEdit,
								// Наименование редактируемо до первого сохранения либо при наличии права
								CanEditName = canEdit && (entity.Id == 0 || Security.HasRight(AccessRightEnum.Hotel.EditName))
			};
			if (entity.HistoryLinkId.HasValue)
			{
				place.State.PostNoStatusActions = place.State.PostNoStatusActions ?? new List<NoStatusAction>();
				place.State.PostNoStatusActions.Add(new NoStatusAction
				{
					Name = "История",
					ButtonClass = "btn btn-default btn-hystory-link",
					SomeAddon = string.Format("data-history-id=\"{0}\"", entity.HistoryLinkId)
				});
			}

			ViewBag.TypesOfRooms = entity.TypeOfRooms?.ToList()
				.Select(i => new SelectListItem { Value = i.Id.FormatEx(), Text = i.Name }).ToList() ?? new List<SelectListItem>();
			ViewBag.Accommodations = entity.Accommodation?.ToList()
				.Select(i => new SelectListItem { Value = i.Id.FormatEx(), Text = i.Name }).ToList() ?? new List<SelectListItem>();
			ViewBag.DiningOptions = UnitOfWork.GetSet<DiningOptions>().Where(i => i.IsActive == true).ToList()
				.Select(i => new SelectListItem { Value = i.Id.FormatEx(), Text = i.Name }).ToList();

			SetupVocabularies(place);
			return View("HotelsEdit", place);
		}

		private bool ValidateModel(Hotels model)
		{
			var isOk = true;
			if (model == null)
			{
				ModelState.AddModelError(string.Empty, "Ошибка получения данных");
				return false;
			}

			if (string.IsNullOrEmpty(model.Name))
			{
				ModelState.AddModelError("Data.Name", "Необходимо указать название места отдыха");
				isOk = false;
			}

			if (!model.OrganizationId.HasValue)
			{
				ModelState.AddModelError("Data.OrganizationId", "Необходимо указать наименование организации места отдыха");
				isOk = false;
			}

			if (string.IsNullOrEmpty(model.Phone))
			{
				ModelState.AddModelError("Data.Phone", "Необходимо указать телефон");
				isOk = false;
			}

			if (!model.HotelTypeId.HasValue)
			{
				ModelState.AddModelError("Data.HotelTypeId", "Необходимо указать вид места отдыха");
				isOk = false;
			}

			if (!model.PlaceOfRestId.HasValue)
			{
				ModelState.AddModelError("Data.PlaceOfRestId", "Необходимо указать регион");
				isOk = false;
			}

			if (model.HotelTypeId == (long?)HotelTypeEnum.Hotel)
			{
				if (model.TypeOfRooms == null || !model.TypeOfRooms.Any())
				{
					ModelState.AddModelError("Data.TypeOfRooms", "Необходимо добавить номерной фонд");
					isOk = false;
				}
			}

			if (model.Files == null || model.Files.All(f => f.NullSafe(ff => ff.FileType.ParentId) != (long)FileTypeEnum.Interior && f.NullSafe(ff => ff.FileType.ParentId) != (long)FileTypeEnum.Exterior))
			{
				ModelState.AddModelError("Data.Files", "Необходимо добавить фотографию интерьера или экстерьера");
				isOk = false;
			}

			return isOk;
		}

		[HttpGet]
		public ActionResult HotelPriceTable(HotelPricesFilter filter)
		{
			var pageSize = 20;
			var model = new HotelPricesViewModel();
			model.Filter = filter;
			model.Hotel = UnitOfWork.GetSet<Hotels>().FirstOrDefault(i => i.Id == filter.HotelId);

			var query = UnitOfWork.GetSet<HotelPrice>().Where(i => i.HotelId == filter.HotelId);

			if (filter.Age.HasValue)
			{
				query = query.Where(i => i.AgeFrom <= filter.Age.Value && i.AgtTo >= filter.Age.Value);
			}

			if (filter.TypeOfRoomId.HasValue)
			{
				query = query.Where(i => i.TypeOfRoomsId == filter.TypeOfRoomId.Value);
			}

			if (filter.AccommodationId.HasValue)
			{
				query = query.Where(i => i.AccommodationId == filter.AccommodationId.Value);
			}

			if (filter.DiningOptionsId.HasValue)
			{
				query = query.Where(i => i.DiningOptionsId == filter.DiningOptionsId.Value);
			}

			if (!string.IsNullOrWhiteSpace(filter.Date))
			{
				var date = DateTime.Parse(filter.Date);
				query = query.Where(i => i.DateFrom <= date && i.DateTo >= date);
			}

			if (!string.IsNullOrWhiteSpace(filter.Price))
			{
				var p = decimal.Parse(filter.Price.Trim().Replace(",", "."));
				var pfrom = p - p / 10M;
				var pto = p + p / 10M;
				query = query.Where(i => i.Price > pfrom && i.Price < pto);
			}

			var totalFound = query.Count();
			model.Page = new CommonPagedList<HotelPrice>(
				query.OrderByDescending(i => i.Id).Skip((filter.PageNumber - 1) * pageSize).Take(pageSize).ToList(),
				filter.PageNumber, pageSize, totalFound);

			return View(model);
		}

		[HttpPost]
		public ActionResult SavePrice(HotelPrice dto)
		{
			if (dto == null)
				return Json(new { error = "Не переданы данные" });

			dto.LastUpdateTick = DateTime.Now.Ticks;
			if (dto.Id == 0)
			{
				UnitOfWork.AddEntity(dto);
			}
			else
			{
				UnitOfWork.Update(dto);
			}

			return Json(new { result = "ok" });
		}

		[HttpPost]
		public ActionResult DeletePrice(long id)
		{
			if (id == 0)
				return Json(new { error = "Не переданы данные" });

			var entity = UnitOfWork.GetSet<HotelPrice>().FirstOrDefault(i => i.Id == id);
			UnitOfWork.Delete(entity);
			return Json(new { result = "ok" });
		}
	}
}
