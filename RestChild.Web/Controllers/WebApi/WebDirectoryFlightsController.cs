using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

using RestChild.Comon;
using RestChild.Comon.Dto.Commercial;
using RestChild.Comon.Enumeration;
using RestChild.Extensions.Extensions;
using RestChild.Extensions.Filter;
using RestChild.DAL.RepositoryExtensions;
using RestChild.Domain;
using RestChild.Web.Properties;

namespace RestChild.Web.Controllers.WebApi
{
	public class WebDirectoryFlightsController : WebGenericRestController<DirectoryFlights>
	{
		public StateController ApiStateController { get; set; }

		public override void SetUnitOfWorkInRefClass(IUnitOfWork unitOfWork)
		{
			base.SetUnitOfWorkInRefClass(unitOfWork);
			ApiStateController.SetUnitOfWorkInRefClass(unitOfWork);
		}

		/// <summary>
		/// получение информации о рейсах
		/// </summary>
		/// <param name="filter"></param>
		/// <returns></returns>
		public CommonPagedList<DirectoryFlights> Get(DirectoryFlightsFilterModel filter)
		{
			if (!(filter?.ContractFiltered??false) && !Security.HasRightForSomeOrganization(AccessRightEnum.DirectoryFlightsManage))
			{
				throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.Forbidden, "Forbidden"));
			}

			var pageSize = filter?.PageSize ?? Settings.Default.TablePageSize;
			var pageNumber = filter?.PageNumber ?? 1;
			var startRecord = (pageNumber - 1) * pageSize;
			var query = UnitOfWork.GetSet<DirectoryFlights>().Where(c => c.StateId != null && c.StateId != StateMachineStateEnum.Deleted);

			if (!Security.HasRight(AccessRightEnum.DirectoryFlightsManage))
			{
				var orgs = AccessRightEnum.DirectoryFlightsManage.GetSecurityOrganiztion();
				query = query.Where(c => orgs.Contains(c.Contract.SupplierId));
			}

			if (filter != null)
			{
				if (filter.YearOfRestId.HasValue)
				{
					query = query.Where(f => f.YearOfRestId == filter.YearOfRestId);
				}
				if (filter.TypeOfTransportId.HasValue)
				{
					query = query.Where(f => f.TypeOfTransportId == filter.TypeOfTransportId);
				}
				if (filter.DepartureId.HasValue)
				{
					query = query.Where(f => f.DepartureId == filter.DepartureId);
				}
				if (filter.ArrivalId.HasValue)
				{
					query = query.Where(f => f.ArrivalId == filter.ArrivalId);
				}
				if (filter.StateId.HasValue)
				{
					query = query.Where(f => f.StateId == filter.StateId);
				}
				if (!string.IsNullOrEmpty(filter.Code))
				{
					query = query.Where(f => f.Code.ToLower().Contains(filter.Code.ToLower()));
				}
				if (!string.IsNullOrEmpty(filter.DepartureCode))
				{
					query = query.Where(f => f.CodeDeparture.ToLower().Contains(filter.DepartureCode.ToLower()));
				}
				if (!string.IsNullOrEmpty(filter.ArrivalCode))
				{
					query = query.Where(f => f.CodeArrival.ToLower().Contains(filter.ArrivalCode.ToLower()));
				}
				if (!string.IsNullOrEmpty(filter.FilightNumber))
				{
					query = query.Where(f => f.FilightNumber.ToLower().Contains(filter.FilightNumber.ToLower()));
				}
				if (filter.ContractId.HasValue && filter.ContractId > 0)
				{
					query = query.Where(f => f.ContractId == filter.ContractId.Value);
				}
			}

			var totalCount = query.Count();
			var entities = query.OrderBy(f => f.Id)
								.Skip(startRecord)
								.Take(pageSize)
								.ToList();

			//признак того, что нужно кол-во детей
			if (filter?.ContractId != null)
			{
				foreach (var entity in entities)
				{
					entity.RestManCount = entity.GetRestManCount();
				}
			}

			var result = entities.Select(c =>
										{
											var item = new DirectoryFlights(c, 2) {RestManCount = c.RestManCount};
											return item;
										})
								.ToList();

			return new CommonPagedList<DirectoryFlights>(result, pageNumber, pageSize, totalCount);
		}

		/// <summary>
		/// получить список рейсов по услуге
		/// </summary>
		public List<BaseResponse> GetTourDirectoryFlights(long addonServiceId, long yearOfRestId)
		{
			var ads = UnitOfWork.GetById<AddonServices>(addonServiceId);
			if (ads?.TourTransport == null)
			{
				return new List<BaseResponse>();
			}

			return
				UnitOfWork.GetSet<DirectoryFlights>()
					.Where(
						f =>
							f.DepartureId == ads.TourTransport.CityOfDepartureId && f.ArrivalId == ads.TourTransport.CityOfArrivalId &&
							f.YearOfRestId == yearOfRestId && f.StateId == StateMachineStateEnum.DirectoryFlights.Formed)
					.ToList()
					.Select(f => new BaseResponse {Id = f.Id, Name = f.GetShortName()})
					.ToList();
		}

		public List<DirectoryFlights> Get(long cityDepartureId, long cityArrivalId, long yearOfRestId)
		{
			return
				UnitOfWork.GetSet<DirectoryFlights>()
					.Where(f => f.DepartureId == cityDepartureId && f.ArrivalId == cityArrivalId && f.YearOfRestId == yearOfRestId && f.StateId == StateMachineStateEnum.DirectoryFlights.Formed)
					.ToList();
		}

		public override DirectoryFlights Post(DirectoryFlights entity)
		{
			if (!Security.HasRightForSomeOrganization(AccessRightEnum.DirectoryFlightsManage))
			{
				throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.Forbidden, "Forbidden"));
			}

			if (entity == null)
			{
				return null;
			}
			using (var transaction = UnitOfWork.GetTransactionScope())
			{
				entity.HistoryLink = UnitOfWork.AddEntity(new HistoryLink
															{
																Historys =
																	new List<History>
																		{
																			UnitOfWork.AddEntity(new History
																									{
																										AccountId = Security.GetCurrentAccountId(),
																										EventCode = "Создание рейса",
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

		public override DirectoryFlights Put(long id, DirectoryFlights entity)
		{
			if (!Security.HasRightForSomeOrganization(AccessRightEnum.DirectoryFlightsManage))
			{
				throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.Forbidden, "Forbidden"));
			}

			if (entity == null)
			{
				return null;
			}

			using (var transaction = UnitOfWork.GetTransactionScope())
			{
				base.Put(id, entity);
				var persisted = UnitOfWork.GetById<DirectoryFlights>(id);
				persisted.HistoryLink = persisted.HistoryLink ?? UnitOfWork.AddEntity(new HistoryLink());
				persisted.HistoryLink.Historys = persisted.HistoryLink.Historys ?? new List<History>();
				persisted.HistoryLink.Historys.Add(UnitOfWork.AddEntity(new History
																			{
																				AccountId = Security.GetCurrentAccountId(),
																				EventCode = "Изменение рейса",
																				DateChange = DateTime.Now,
																				Commentary = string.Empty
																			}));
				UnitOfWork.SaveChanges();
				transaction.Complete();
				return persisted;
			}
		}

		public bool ChangeState(long id, string stateMachineActionString)
		{
			if (!Security.HasRightForSomeOrganization(AccessRightEnum.DirectoryFlightsManage))
			{
				throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.Forbidden, "Forbidden"));
			}

			var result = false;
			var directoryFlight = UnitOfWork.GetById<DirectoryFlights>(id);
			if (directoryFlight == null)
			{
				throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not found"));
			}

			using (var transaction = UnitOfWork.GetTransactionScope())
			{
				directoryFlight.HistoryLink = directoryFlight.HistoryLink ?? new HistoryLink();
				directoryFlight.HistoryLink.Historys = directoryFlight.HistoryLink.Historys ?? new List<History>();

				if (stateMachineActionString == "Delete")
				{
					directoryFlight.StateId = StateMachineStateEnum.Deleted;
					directoryFlight.HistoryLink.Historys.Add(new History
					{
						AccountId = Security.GetCurrentAccountId(),
						EventCode = "Удаление рейса",
						DateChange = DateTime.Now
					});
				}
				else
				{
					if (GetErrorsOfChageStatus(id, stateMachineActionString).Any())
					{
						return false;
					}

					var action = ApiStateController.GetAction(stateMachineActionString);
					if (action != null && action.ToStateId.HasValue)
					{
						directoryFlight.HistoryLink.Historys.Add(new History
						{
							AccountId = Security.GetCurrentAccountId(),
							EventCode = "Изменение статуса рейса",
							DateChange = DateTime.Now,
							Commentary =
								string.Format("Изменение статуса рейса с \"{0}\" на \"{1}\"", directoryFlight.NullSafe(c => c.State.Name),
									action.NullSafe(a => a.ToState.Name))
						});
						directoryFlight.StateId = action.ToStateId;
						result = true;
					}
				}

				UnitOfWork.SaveChanges();

				transaction.Complete();
				return result;
			}
		}

		public List<string> GetErrorsOfChageStatus(long id, string actionCode)
		{
			if (!Security.HasRightForSomeOrganization(AccessRightEnum.DirectoryFlightsManage))
			{
				throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.Forbidden, "Forbidden"));
			}

			var errors = new List<string>();
			var directoryFlight = UnitOfWork.GetById<DirectoryFlights>(id);
			if (directoryFlight == null)
			{
				throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not found"));
			}

			if (!directoryFlight.YearOfRestId.HasValue)
			{
				errors.Add("Поле \"Год кампании\" должно быть заполнено");
			}
			if (!directoryFlight.DepartureId.HasValue)
			{
				errors.Add("Поле \"Место отбытия\" должно быть заполнено");
			}
			if (!directoryFlight.ArrivalId.HasValue)
			{
				errors.Add("Поле \"Место прибытия\" должно быть заполнено");
			}
			if (!directoryFlight.TypeOfTransportId.HasValue)
			{
				errors.Add("Поле \"Вид транспорта\" должно быть заполнено");
			}
			if (directoryFlight.DepartureId.HasValue && directoryFlight.ArrivalId.HasValue
			    && directoryFlight.DepartureId == directoryFlight.ArrivalId)
			{
				errors.Add("Место отбытия не должно совпадать с местом прибытия");
			}
			return errors;
		}

		public List<StateMachineState> GetStates()
		{
			return
				UnitOfWork.GetSet<StateMachineState>()
					.Where(s => s.StateMachineId == (long)StateMachineEnum.DirectoryFlightsState)
					.ToList()
					.Select(s => new StateMachineState(s))
					.ToList();
		}
	}
}
