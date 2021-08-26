using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using RestChild.Booking.Logic.Extensions;
using RestChild.Comon;
using RestChild.Comon.Enumeration;
using RestChild.Extensions.Filter;
using RestChild.DAL.RepositoryExtensions;
using RestChild.Domain;
using RestChild.Web.Models;
using RestChild.Web.Properties;

namespace RestChild.Web.Controllers.WebApi
{
	/// <summary>
	///     контроллер для доп услуг.
	/// </summary>
	public class AddonServiceController : BaseController
	{
		public WebHistoryController HistoryController { get; set; }

		public StateController StateController { get; set; }


		public override void SetUnitOfWorkInRefClass(IUnitOfWork unitOfWork)
		{
			base.SetUnitOfWorkInRefClass(unitOfWork);
			HistoryController.SetUnitOfWorkInRefClass(unitOfWork);
			StateController.SetUnitOfWorkInRefClass(unitOfWork);
		}

		/// <summary>
		/// получить услугу
		/// </summary>
		[HttpGet]
		public AddonServices GetSetService(long id)
		{
			var service = UnitOfWork.GetById<AddonServices>(id);
			return new AddonServices(service)
			{
				TypeOfService = service.TypeOfService,
				Prices = service.Prices.Select(p => new AddonServicesPrice(p)).ToList(),
				Parent = service.Parent == null
					? null
					: new AddonServices(service.Parent)
					{
						TypeOfService = service.Parent.TypeOfService,
						Prices = service.Parent.Prices.Select(p => new AddonServicesPrice(p)).ToList(),
					}
			};
		}

		/// <summary>
		/// получить услугу
		/// </summary>
		[HttpGet]
		public TypeOfService GetTypeService(long id)
		{
			return new TypeOfService(UnitOfWork.GetById<TypeOfService>(id));
		}

		/// <summary>
		/// изменить статус доп услуги.
		/// </summary>
		internal void ChangeStatus(long id, string actionCode)
		{
			var entity = UnitOfWork.GetById<AddonServices>(id);
			var state = StateController.GetNextState(entity.StateId, actionCode);
			if (state != null)
			{
				if (entity.StateId == StateMachineStateEnum.AddonService.Forming && state.Id == StateMachineStateEnum.AddonService.Formed)
				{
					var model = new AddonServicesModel(entity);
					if (!model.CheckModel())
					{
						return;
					}
				}
				HistoryController.InsertHistoryItem(entity.HistoryLink, new History
				{
					DateChange = DateTime.Now,
					EventCode = "Изменение статуса.",
					Commentary = string.Format("Статус изменен с {0} на {1}", entity.NullSafe(e=>e.State.Name), state.Name),
				});

				entity.StateId = state.Id;
				UnitOfWork.SaveChanges();
			}
		}

		/// <summary>
		///     сохранение дополнительных услуг.
		/// </summary>
		internal AddonServices Save(AddonServices model)
		{
			var typeOfService = UnitOfWork.GetById<TypeOfService>(model.TypeOfServiceId);
			var entity = UnitOfWork.GetById<AddonServices>(model.Id);

			var diff = entity != null ? entity.Compare(model, UnitOfWork) : "Первое сохранение услуги";

			if (typeOfService?.NeedTransport ?? false)
			{
				if (model.TourTransportId > 0)
				{
					var entityTt = model.TourTransport;
					model.TourTransport = UnitOfWork.GetById<TourTransport>(model.TourTransportId);
					model.TourTransport.CityOfArrivalId = entityTt.CityOfArrivalId >= 0 ? entityTt.CityOfArrivalId : null;
					model.TourTransport.CityOfDepartureId = entityTt.CityOfDepartureId >= 0 ? entityTt.CityOfDepartureId : null;
					model.TourTransport.PlaceOfDeparture = entityTt.PlaceOfDeparture;
					model.TourTransport.PlaceOfArrival = entityTt.PlaceOfArrival;
					model.TourTransport.DateOfArrival = entityTt.DateOfArrival;
					model.TourTransport.DateOfDeparture = entityTt.DateOfDeparture;
					model.TourTransport.LastUpdateTick = DateTime.Now.Ticks;
				}
				else
				{
					var tt = model.TourTransport ?? new TourTransport();
					tt.CityOfArrivalId = tt.CityOfArrivalId >= 0 ? tt.CityOfArrivalId : null;
					tt.CityOfDepartureId = tt.CityOfDepartureId >= 0 ? tt.CityOfDepartureId : null;
					model.TourTransport = UnitOfWork.AddEntity(tt);
					model.TourTransportId = model.TourTransport?.Id;
				}
			}
			else
			{
				var ttId = model.TourTransportId;
				if (entity != null)
				{
					ttId = entity.TourTransportId;
					entity.TourTransport = null;
					entity.TourTransportId = null;
				}

				model.TourTransport = null;
				model.TourTransportId = null;
				UnitOfWork.SaveChanges();
				if (ttId > 0)
				{
					UnitOfWork.Delete(UnitOfWork.GetById<TourTransport>(ttId));
				}
			}

			model.ResetZeroFk();
			var prices = model.Prices ?? new List<AddonServicesPrice>();
			model.Prices = null;
			if (model.Id == 0)
			{
				if (model.Hotels != null && model.Hotels.Any())
				{
					var idHotels = model.Hotels.Select(h => h.Id).Distinct().ToList();
					model.Hotels = UnitOfWork.GetSet<Hotels>().Where(h => idHotels.Contains(h.Id)).ToList();
				}
				if (model.Tags != null && model.Tags.Any())
				{
					var idTags = model.Tags.Select(h => h.Id).ToList();
					model.Tags = UnitOfWork.GetSet<Tag>().Where(h => idTags.Contains(h.Id)).ToList();
				}

				model = UnitOfWork.AddEntity(model);
			}

			entity = UnitOfWork.GetById<AddonServices>(model.Id);

			var currenPrices = entity.Prices ?? new List<AddonServicesPrice>();

			var pricesId = prices.Where(p => p.Id != 0).Select(p => p.Id).ToList();

			foreach (var forDel in currenPrices.Where(c => !pricesId.Contains(c.Id)).ToList())
			{
				UnitOfWork.Delete(forDel);
			}

			foreach (var forAdd in prices.Where(c => c.Id == 0).ToList())
			{
				forAdd.AddonServicesId = entity.Id;
				UnitOfWork.AddEntity(forAdd, false);
			}

			foreach (var fromUpdate in prices.Where(c => c.Id != 0).ToList())
			{
				var forUpdate = UnitOfWork.GetById<AddonServicesPrice>(fromUpdate.Id);
				if (forUpdate != null)
				{
					fromUpdate.AddonServicesId = entity.Id;
					fromUpdate.LastUpdateTick = DateTime.Now.Ticks;
					forUpdate.CopyEntity(fromUpdate);
				}
			}

			if (entity.StateId != model.StateId)
			{
				return entity;
			}

			model.HistoryLink = model.HistoryLinkId.HasValue && model.HistoryLinkId > 0
				? UnitOfWork.GetById<HistoryLink>(model.HistoryLinkId.Value)
				: new HistoryLink();

			if (model.HistoryLink.Id == 0)
			{
				model.HistoryLink = UnitOfWork.AddEntity(model.HistoryLink);
				model.HistoryLinkId = model.HistoryLink.Id;
			}

			HistoryController.InsertHistoryItem(model.HistoryLink, new History
			{
				DateChange = DateTime.Now,
				EventCode = "Сохранение услуги.",
				Commentary = diff
			});

			entity.Name = model.Name;
			entity.TourTransportId = model.TourTransportId;
			entity.Description = model.Description;
			entity.AnnouncementEvent = model.AnnouncementEvent;
			entity.ConditionsForAccompanying = model.ConditionsForAccompanying;
			entity.SizeMax = model.SizeMax;
			entity.SizeMin = model.SizeMin;
			entity.Requared = model.Requared;
			entity.OnlyWithRequest = model.OnlyWithRequest;
			entity.ByDefault = model.ByDefault;
			entity.IsActive = true;
			entity.GeneralService = true;
			entity.NeedApprove = model.NeedApprove;
			entity.PartnerId = model.PartnerId;
			entity.DurationDay = model.DurationDay;
			entity.DurationHour = model.DurationHour;
			entity.DurationMonth = model.DurationMonth;
			entity.DurationYear = model.DurationYear;
			entity.TypePriceCalculationId = model.TypePriceCalculationId;
			entity.DateFrom = model.DateFrom;
			entity.DateTo = model.DateTo;
			entity.IsGroup = model.IsGroup;
			entity.Hidden = model.Hidden;
			entity.Hotels.Clear();
			entity.NotForSite = model.NotForSite;

			if (model.Hotels != null && model.Hotels.Any())
			{
				var idHotels = model.Hotels.Select(h => h.Id).Distinct().ToList();
				entity.Hotels.AddRange(UnitOfWork.GetSet<Hotels>().Where(h => idHotels.Contains(h.Id)).ToList());
			}

			entity.Tags.Clear();

			if (model.Tags != null && model.Tags.Any())
			{
				var idTags = model.Tags.Select(h => h.Id).ToList();
				entity.Tags.AddRange(UnitOfWork.GetSet<Tag>().Where(h => idTags.Contains(h.Id)).ToList());
			}

			entity.TypeOfServiceId = model.TypeOfServiceId;

			if (model.Photos != null)
			{
				foreach (var photo in model.Photos.Where(p => p.Id == 0).ToList())
				{
					photo.AddonServicesId = entity.Id;
					photo.AccountId = Security.GetCurrentAccountId();
					UnitOfWork.AddEntity(photo);
				}
			}

			var ids = model.Photos != null ? model.Photos.Select(p => p.Id).ToList() : new List<long>();

			if (entity.Photos != null && entity.Photos.Any(p => !ids.Contains(p.Id)))
			{
				foreach (var photo in entity.Photos.Where(p => !ids.Contains(p.Id)).ToList())
				{
					UnitOfWork.Delete(photo);
				}
			}

			UnitOfWork.SaveChanges();

			return model;
		}

		/// <summary>
		/// генерация изменений
		/// </summary>
		internal string GenerateDiff(AddonServices source, AddonServices target)
		{
			var sb = new StringBuilder();
			if (source == null && target != null)
			{
				sb.AppendLineLi("Первое сохранение.");
			} else if (target != null)
			{
				if (source.Name != target.Name)
				{
					sb.AppendLineLi(string.Format("Изменено наименование услуги с '{0}' на '{1}'", source.Name.FormatEx(), target.Name.FormatEx()));
				}

				if (source.Description != target.Description)
				{
					sb.AppendLineLi(string.Format("Изменено описание услуги с '{0}' на '{1}'", source.Description.FormatEx(), target.Description.FormatEx()));
				}

				if (source.Requared != target.Requared)
				{
					sb.AppendLineLi(string.Format("Изменено 'Обязательность' с '{0}' на '{1}'", source.Requared.FormatEx(), target.Requared.FormatEx()));
				}

				if (source.OnlyWithRequest != target.OnlyWithRequest)
				{
					sb.AppendLineLi(string.Format("Изменено 'Только для нового заявления' с '{0}' на '{1}'", source.OnlyWithRequest.FormatEx(), target.OnlyWithRequest.FormatEx()));
				}

				if (source.ByDefault != target.ByDefault)
				{
					sb.AppendLineLi(string.Format("Изменено 'По умолчанию' с '{0}' на '{1}'", source.ByDefault.FormatEx(), target.ByDefault.FormatEx()));
				}

				//if (source.HotelsId != target.HotelsId)
				//{
				//	if (!target.HotelsId.HasValue)
				//	{
				//		sb.AppendLineLi(string.Format("Сброшено значение места отдыха"));
				//	}
				//	else if (!source.HotelsId.HasValue)
				//	{
				//		var hotel = UnitOfWork.GetById<Hotels>(target.HotelsId.Value);
				//		sb.AppendLineLi(string.Format("Установлено место отдыха - '{0}'", hotel.Name.FormatEx()));
				//	}
				//	else
				//	{
				//		var hotelTarget = UnitOfWork.GetById<Hotels>(target.HotelsId.Value);
				//		var hotelSource = UnitOfWork.GetById<Hotels>(source.HotelsId.Value);
				//		sb.AppendLineLi(string.Format("Изменено место отдыха - с '{0}' на '{1}'", hotelSource.Name.FormatEx(), hotelTarget.Name.FormatEx()));
				//	}
				//}

				if (source.TypeOfServiceId != target.TypeOfServiceId)
				{
					if (!target.TypeOfServiceId.HasValue)
					{
						sb.AppendLineLi(string.Format("Сброшено значение вид услуги"));
					}
					else if (!source.TypeOfServiceId.HasValue)
					{
						var typeOfService = UnitOfWork.GetById<TypeOfService>(target.TypeOfServiceId.Value);
						sb.AppendLineLi(string.Format("Установлено вид услуги - '{0}'", typeOfService.Name.FormatEx()));
					}
					else
					{
						var typeOfServiceTarget = UnitOfWork.GetById<TypeOfService>(target.TypeOfServiceId.Value);
						var typeOfServiceSource = UnitOfWork.GetById<TypeOfService>(source.TypeOfServiceId.Value);
						sb.AppendLineLi(string.Format("Изменено вид услуги - с '{0}' на '{1}'", typeOfServiceSource.Name.FormatEx(), typeOfServiceTarget.Name.FormatEx()));
					}
				}

				if (target.Photos != null && target.Photos.Any(p => p.Id == 0))
				{
					sb.AppendLineLi(string.Format("Добавлены фотографии: {0}",
						string.Join(", ", target.Photos.Where(p => p.Id == 0).Select(p => p.FileTitle.FormatEx()))));
				}

				var ids = target.Photos != null ? target.Photos.Select(p => p.Id).ToList() : new List<long>();

				if (source.Photos != null && target.Photos != null && source.Photos.Any(p => !ids.Contains(p.Id)))
				{
					sb.AppendLineLi(string.Format("Удалены фотографии: {0}",
						string.Join(", ", source.Photos.Where(p => !ids.Contains(p.Id)).Select(p => p.FileTitle.FormatEx()))));
				}
			}

			return string.Format("<ul>{0}</ul>", sb);
		}

		internal List<StateMachineState> GetStates()
		{
			SetUnitOfWorkInRefClass(UnitOfWork);
			return
				UnitOfWork.GetSet<StateMachineState>()
					.Where(s => s.StateMachineId == (long?)StateMachineEnum.AddonServiceState)
					.ToList()
					.Select(s => new StateMachineState(s))
					.ToList();
		}

		/// <summary>
		///     поиск дополнительных услуг.
		/// </summary>
		internal CommonPagedList<AddonServices> Search(AddonServiceFilterModel filter)
		{
			if (!Security.HasRight(AccessRightEnum.AddonService.View))
			{
				return new CommonPagedList<AddonServices>();
			}

			var pageSize = Settings.Default.TablePageSize;
			var pageNumber = filter != null ? filter.PageNumber : 1;
			var startRecord = (pageNumber - 1)*pageSize;
			var query =
				UnitOfWork.GetSet<AddonServices>()
					.Where(t => t.IsActive && !t.TourId.HasValue && t.StateId != StateMachineStateEnum.Deleted && t.GeneralService);
			if (filter != null)
			{
				if (!string.IsNullOrEmpty(filter.Name))
				{
					query = query.Where(t => t.Name.ToLower().Contains(filter.Name.ToLower()));
				}
				if (filter.HotelId.HasValue && filter.HotelId != 0)
				{
					query = query.Where(t => t.Hotels.Any(h => h.Id == filter.HotelId));
				}

				if (filter.TypeId.HasValue && filter.TypeId != 0)
				{
					query = query.Where(t => t.TypeOfServiceId == filter.TypeId);
				}

				query = filter.StateId.HasValue
					? query.Where(t => t.StateId == filter.StateId)
					: query.Where(t => t.StateId != StateMachineStateEnum.AddonService.Archive && t.StateId.HasValue);
			}

			var totalCount = query.Count();
			var entity =
				query.Include(t => t.Hotels).Include(y => y.Parent).OrderBy(t => t.Name).Skip(startRecord).Take(pageSize).ToList();
			return new CommonPagedList<AddonServices>(entity, pageNumber, pageSize, totalCount);
		}
	}
}
