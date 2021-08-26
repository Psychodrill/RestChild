using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Castle.Core.Internal;
using RestChild.Comon;
using RestChild.Comon.Enumeration;
using RestChild.Extensions.Filter;
using RestChild.DAL.RepositoryExtensions;
using RestChild.Domain;
using RestChild.Web.Extensions;
using RestChild.Web.Properties;
using WebGrease.Css.Extensions;

namespace RestChild.Web.Controllers.WebApi
{
	[Authorize]
	public class WebHotelsController : WebGenericRestController<Hotels>
	{
		public StateController ApiStateController { get; set; }

		public override void SetUnitOfWorkInRefClass(IUnitOfWork unitOfWork)
		{
			base.SetUnitOfWorkInRefClass(unitOfWork);
			ApiStateController.SetUnitOfWorkInRefClass(unitOfWork);
		}

		[HttpPost, HttpGet]
		public IList<Organization> GetOrganizationList(string query)
		{
			var general = Security.GetSecurity().ToList();

			var secsHotelsView = general.Where(s => s.StartsWith(AccessRightEnum.Hotel.View)).ToList();

			var q = UnitOfWork.GetSet<Organization>().Where(x => !x.IsDeleted && x.IsLast);

			if (!string.IsNullOrEmpty(query))
			{
				var substrs = query.Split(' ').Where(s => !string.IsNullOrEmpty(s)).Select(s => s.ToLower()).ToList();
				foreach (var str in substrs)
				{
					q = q.Where(x => x.Name.ToLower().Contains(str) || x.Inn == str);
				}
			}

			if (!secsHotelsView.Contains(AccessRightEnum.Hotel.View))
			{
				var orgsId = secsHotelsView.Select(
					s =>
						s.Replace(
							string.Format(Security.SecurityOrgTemplate, AccessRightEnum.Hotel.View, string.Empty),
							string.Empty).LongParse()).Where(l => l.HasValue).ToArray();

				q = q.Where(x => orgsId.Contains(x.Id));
			}

			var res = q.OrderBy(x => x.Name.Length).ThenBy(x => x.Name)
				.Take(Settings.Default.WebBtiStreetsResponseCount)
				.ToList().Select(l => new Organization(l)).ToList();

			return res;
		}

		/// <summary>
		///     Поиск мест отдыха
		/// </summary>
		/// <param name="filter">Настройки фильтра</param>
		/// <param name="pageNumber">Страница</param>
		/// <param name="typeOfRest">Тип отдыха</param>
		public CommonPagedList<Hotels> Get(HotelsFilterModel filter)
		{
			var pageSize = Settings.Default.TablePageSize;
			var pageNumber = filter != null ? filter.PageNumber : 1;
			var startRecord = filter != null ? (pageNumber - 1)*pageSize : 0;
			IQueryable<Hotels> query = UnitOfWork.GetSet<Hotels>().Where(h=>h.StateId.HasValue && h.StateId != StateMachineStateEnum.Deleted);
			var general = Security.GetSecurity().ToList();
			var secsHotelsView = general.Where(s => s.StartsWith(AccessRightEnum.Hotel.View)).ToList();
			if (!secsHotelsView.Contains(AccessRightEnum.Hotel.View))
			{
				var orgsId = secsHotelsView.Select(
					s =>
						s.Replace(
							string.Format(Security.SecurityOrgTemplate, AccessRightEnum.Hotel.View, string.Empty),
							string.Empty).LongParse()).Where(l => l.HasValue).ToArray();

				query = query.Where(x => orgsId.Contains(x.OrganizationId));
			}

			if (filter != null)
			{
				if (!string.IsNullOrEmpty(filter.Name))
				{
					query = query.Where(hotel => hotel.Name.ToLower().Contains(filter.Name.ToLower()));
				}
				if (!string.IsNullOrEmpty(filter.Address))
				{
					query = query.Where(hotel => hotel.Address.ToLower().Contains(filter.Address.ToLower()));
				}
				if (filter.Region.HasValue && filter.Region != 0)
				{
					query = query.Where(hotel => hotel.PlaceOfRestId == filter.Region);
				}
				if (filter.Tv == true)
				{
					query = query.Where(hotel => hotel.TypeOfRooms.Any(t => t.HaveTv));
				}
				if (filter.Fridge == true)
				{
					query = query.Where(hotel => hotel.TypeOfRooms.Any(t => t.HaveRefrigerator));
				}
				if (filter.Shower == true)
				{
					query = query.Where(hotel => hotel.TypeOfRooms.Any(t => t.HaveShower));
				}
				if (filter.StateId.HasValue && filter.StateId != 0)
				{
					query = query.Where(hotel => hotel.StateId == filter.StateId);
				}
				if (filter.TypeOfRest.HasValue)
				{
					var type = UnitOfWork.GetById<TypeOfRest>(filter.TypeOfRest);
					if (type?.HotelTypeId == (long)HotelTypeEnum.Camp)
					{
						query = query.Where(hotel => hotel.HotelTypeId == (long) HotelTypeEnum.Camp);
					}
					else if (type?.HotelTypeId == (long)HotelTypeEnum.Hotel)
					{
						query = query.Where(hotel => hotel.HotelTypeId == (long) HotelTypeEnum.Hotel);
					}
				}

				if (filter.HotelTypeId.HasValue)
				{
					query = query.Where(q => q.HotelTypeId == filter.HotelTypeId);
				}

				if (filter.CityId.HasValue && filter.CityId > 0)
				{
					query = query.Where(h => h.CityId == filter.CityId);
				}

            if(filter.Habitat == true)
            {
               query = query.Where(ss => ss.AccessibleEnvironment);
            }
			}

			var totalCount = query.Count();
			var entity =
				query.Include(h => h.Organization)
					.Include(h => h.HistoryLink)
					.Include(h => h.City)
					.Include(h => h.State)
					.Include(h => h.PlaceOfRest)
					.Include(h => h.HotelType)
					.Include(h => h.TypeOfRooms)
					.OrderBy(hotel => hotel.Name)
					.Skip(startRecord)
					.Take(pageSize)
					.ToList()
					.Select(h => new Hotels(h, 1) { TypeOfRooms = h.TypeOfRooms != null ? h.TypeOfRooms.Select(t => new TypeOfRooms(t)).ToList() : null })
					.ToList();

			return new CommonPagedList<Hotels>(entity, pageNumber, pageSize, totalCount);
		}

		[Route("api/WebHotels")]
		public IEnumerable<Hotels> Get(string name, long? typeOfRest = null, bool? onlyApproved = null, long? hotelType = null, long? cityId = null)
		{
			return
				Get(new HotelsFilterModel
				{
					Name = name,
					PageNumber = 1,
					TypeOfRest = typeOfRest,
					StateId = onlyApproved == true ? (long?) StateMachineStateEnum.Hotel.Approved : null,
					HotelTypeId = hotelType,
					CityId = cityId
				});
		}

		public List<Hotels> Get()
		{
			return UnitOfWork.GetSet<Hotels>().Where(h => h.StateId == StateMachineStateEnum.Hotel.Approved).ToList();
		}

		public override Hotels Post(Hotels entity)
		{
			if (entity == null)
			{
				return null;
			}

			using (var transaction = UnitOfWork.GetTransactionScope())
			{
				entity.HistoryLink = UnitOfWork
					.AddEntity(new HistoryLink
									{
										Historys =
											new List<History>
												{
													UnitOfWork
														.AddEntity(new History
																		{
																			AccountId = Security.GetCurrentAccountId(),
																			EventCode = "Создание места отдыха",
																			DateChange = DateTime.Now,
																			Commentary = string.Empty
																		})
												}
									});
				var result = base.Post(entity);
				transaction.Complete();
				return result;
			}
		}

		public override Hotels Put(long id, Hotels entity)
		{
			if (entity == null)
			{
				return null;
			}
			var lastUpdateTick = DateTime.Now.Ticks;

			var files = entity.Files ?? new List<FileHotel>();
			var typeOfRooms = entity.TypeOfRooms ?? new List<TypeOfRooms>();
			var contacts = entity.Contacts ?? new List<HotelContactPerson>();
			var accomodations = entity.Accommodation ?? new List<Accommodation>();
			var diningOptions = entity.DiningOptions ?? new List<DiningOptions>();
			entity.Files = null;
			entity.TypeOfRooms = null;
			entity.Contacts = null;
			entity.Accommodation = null;
			entity.DiningOptions = null;
			entity.EkisNeedSend = true;
			entity.LastUpdateTick = lastUpdateTick;

			foreach (var typeOfRoom in typeOfRooms)
			{
				typeOfRoom.LastUpdateTick = lastUpdateTick;
				if (typeOfRoom.Files?.Count > 0)
				{
					foreach (var fileHotel in typeOfRoom.Files)
					{
						fileHotel.LastUpdateTick = lastUpdateTick;
					}
				}
			}

			foreach (var fileHotel in files)
			{
				fileHotel.LastUpdateTick = lastUpdateTick;
			}

			using (var transaction = UnitOfWork.GetTransactionScope())
			{
				var ekisId = UnitOfWork.GetSet<Hotels>().Where(h => h.Id == id).Select(h => h.EkisId).FirstOrDefault();
				entity.EkisId = ekisId;
				base.Put(id, entity);
				var persisted = UnitOfWork.GetSet<Hotels>().FirstOrDefault(h => h.Id == id);
				if (persisted != null)
				{
					UnitOfWork.MergeCollection(
						typeOfRooms,
						persisted.TypeOfRooms,
						(s, d) =>
						{
							d.Name = s.Name;
							d.CountAddonPlace = s.CountAddonPlace;
							d.CountBasePlace = s.CountBasePlace;
							d.HaveAirConditioning = s.HaveAirConditioning;
							d.HaveBalcony = s.HaveBalcony;
							d.HaveBar = s.HaveBar;
							d.HaveBath = s.HaveBath;
							d.HaveBidet = s.HaveBidet;
							d.HaveFurniture = s.HaveFurniture;
							d.HaveHairDryer = s.HaveHairDryer;
							d.HaveKitchen = s.HaveKitchen;
							d.HaveLocalTv = s.HaveLocalTv;
							d.HavePhone = s.HavePhone;
							d.HaveRadio = s.HaveRadio;
							d.HaveRefrigerator = s.HaveRefrigerator;
							d.HaveSafe = s.HaveSafe;
							d.HaveSatelliteTv = s.HaveSatelliteTv;
							d.HaveShower = s.HaveShower;
							d.HaveTv = s.HaveTv;
							d.HaveWc = s.HaveWc;
							d.MaximumCount = s.MaximumCount;
							d.RoomSize = s.RoomSize;
							d.RoomSizePerPerson = s.RoomSizePerPerson;
							d.LastUpdateTick = s.LastUpdateTick;
							UnitOfWork.MergeCollection(
								s.Files,
								d.Files,
								(sa, da) =>
								{
									da.IsMainPhoto = sa.IsMainPhoto;
									da.LastUpdateTick = sa.LastUpdateTick;
								});
						});

					var idSource = files.Select(s => s.Id).ToList();
					idSource.AddRange(
						persisted.TypeOfRooms?.SelectMany(t => t.Files?.Select(f => f.Id).ToList() ?? new List<long>()) ??
						new List<long>());

					var destDelete = persisted.Files.Where(d => !idSource.Contains(d.Id)).ToList();
					foreach (var item in destDelete)
					{
						if (item.Id != 0)
						{
							UnitOfWork.Delete(item);
							persisted.Files.Remove(item);
						}
					}

					UnitOfWork.MergeCollection(files, persisted.Files, (s, d) =>
					{
						d.IsMainPhoto = s.IsMainPhoto;
					});

					UnitOfWork.MergeCollection(
						contacts,
						persisted.Contacts,
						(s, d) =>
						{
							d.LastName = s.LastName;
							d.FirstName = s.FirstName;
							d.MiddleName = s.MiddleName;
							d.Phone = s.Phone;
							d.Position = s.Position;
						});

					UnitOfWork.MergeCollection(accomodations, persisted.Accommodation,
						(s, d) =>
							{
								d.Adult = s.Adult;
								d.Name = s.Name;
								UnitOfWork.MergeCollection(
									s.AccommodationChildren,
									d.AccommodationChildren,
									(sa, da) =>
										{
											da.AgeFrom = sa.AgeFrom;
											da.AgeTo = sa.AgeTo;
											da.CountChildren = sa.CountChildren;
										});
							});
					UnitOfWork.MergeCollection(diningOptions, persisted.DiningOptions, (s, d) => { d.Name = s.Name; });
					persisted.HistoryLink = persisted.HistoryLink ?? UnitOfWork.AddEntity(new HistoryLink());
					persisted.HistoryLink.Historys = persisted.HistoryLink.Historys ?? new List<History>();
					persisted.HistoryLink.Historys.Add(UnitOfWork.AddEntity(new History
																				{
																					AccountId = Security.GetCurrentAccountId(),
																					EventCode = "Изменение места отдыха",
																					DateChange = DateTime.Now,
																					Commentary = string.Empty
																				}));
					UnitOfWork.SaveChanges();

					transaction.Complete();
					return persisted;
				}
				throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not found"));
			}
		}

		internal bool ChangeStatus(long hotelId, string stateMachineActionString)
		{
			SetUnitOfWorkInRefClass(UnitOfWork);

			var hotel = UnitOfWork.GetById<Hotels>(hotelId);
			if (hotel == null)
			{
				throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not found"));
			}
			hotel.EkisNeedSend = true;
			if (stateMachineActionString == "Delete")
			{
				hotel.StateId = StateMachineStateEnum.Deleted;
				hotel.HistoryLink = this.WriteHistory(hotel.HistoryLink, "Удаление места отдыха",
					string.Empty, StateMachineStateEnum.Deleted,
					hotel.StateId);
				hotel.HistoryLinkId = hotel.HistoryLink?.Id;
			}
			else
			{
				if (GetErrorsOfChageStatus(hotelId, stateMachineActionString).Any())
				{
					return false;
				}

				var action = ApiStateController.GetAction(stateMachineActionString);
				if (action != null && action.ToStateId.HasValue)
				{
					hotel.HistoryLink = this.WriteHistory(hotel.HistoryLink, "Изменение статуса места отдыха",
						$"Изменение статуса места отдыха с \"{hotel.State?.Name}\" на \"{action?.ToState?.Name}\"", action.ToStateId,
						hotel.StateId);
					hotel.HistoryLinkId = hotel.HistoryLink?.Id;
					hotel.StateId = action?.ToStateId ?? hotel.StateId;
				}
			}

			UnitOfWork.SaveChanges();
			return true;
		}

		public IEnumerable<String> GetErrorsOfChageStatus(long hotelId, string stateMachineActionString)
		{
			SetUnitOfWorkInRefClass(UnitOfWork);

			var errors = new List<string>();
			var hotel = UnitOfWork.GetById<Hotels>(hotelId);
			if (hotel == null)
			{
				errors.Add("Не найдено место отдыха");
				return errors;
			}
			var action = ApiStateController.GetAction(stateMachineActionString);
			if (action == null)
			{
				errors.Add("Не найден переход в следующий статус места отдыха");
			}
			else if (!action.ToStateId.HasValue)
			{
				errors.Add("Не найден статус места отдыха");
			}

			return errors;
		}

		public List<StateMachineState> GetStates()
		{
			return
				UnitOfWork.GetSet<StateMachineState>().Where(s => s.StateMachineId == (long) StateMachineEnum.HotelState).ToList();
		}
	}
}
