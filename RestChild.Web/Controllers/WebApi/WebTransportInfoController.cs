using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Omu.ValueInjecter;
using RestChild.Comon;
using RestChild.Comon.Enumeration;
using RestChild.Extensions.Filter;
using RestChild.DAL.RepositoryExtensions;
using RestChild.Domain;
using RestChild.Web.Models;
using RestChild.Web.Properties;

namespace RestChild.Web.Controllers.WebApi
{
    public class WebTransportInfoController : WebGenericRestController<TransportInfo>
    {
		public StateController ApiStateController { get; set; }

		public override void SetUnitOfWorkInRefClass(IUnitOfWork unitOfWork)
		{
			base.SetUnitOfWorkInRefClass(unitOfWork);
			ApiStateController.SetUnitOfWorkInRefClass(unitOfWork);
		}

		public CommonPagedList<TransportInfo> Get(TransportFilterModel filter)
		{
			if (!Security.HasRightForSomeOrganization(AccessRightEnum.Transport.View))
			{
				throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.Forbidden, "Forbidden"));
			}
			var pageSize = Settings.Default.TablePageSize;
			var pageNumber = filter?.PageNumber ?? 1;
			var startRecord = (pageNumber - 1) * pageSize;
			var query = UnitOfWork.GetSet<TransportInfo>().Where(c => c.StateId != null && c.StateId != StateMachineStateEnum.Deleted);

			if (!Security.HasRight(AccessRightEnum.Transport.View))
			{
				var orgs = AccessRightEnum.Transport.View.GetSecurityOrganiztion();
				query = query.Where(t => t.People.Any(l => orgs.Contains(l.DirectoryFlights.Contract.SupplierId)));
				query = query.Where(t => t.StateId == StateMachineStateEnum.Transport.Formed);
			}

			if (filter != null)
			{
				if (filter.DepartureCityId.HasValue)
				{
					query = query.Where(c => c.DepartureId == filter.DepartureCityId);
				}
				if (filter.ArrivalCityId.HasValue)
				{
					query = query.Where(c => c.ArrivalId == filter.ArrivalCityId);
				}
				if (filter.YearOfRestId.HasValue)
				{
					query = query.Where(c => c.YearOfRestId == filter.YearOfRestId);
				}
				if (filter.DateOfDepartureBegin.HasValue)
				{
					var dateOfDepartureBegin = filter.DateOfDepartureBegin.Value.Date;
					query = query.Where(c => c.DateOfDeparture  >= dateOfDepartureBegin);
				}
				if (filter.DateOfDepartureEnd.HasValue)
				{
					var dateOfDepartureEnd = filter.DateOfDepartureEnd.Value.Date.AddDays(1);
					query = query.Where(c => c.DateOfDeparture < dateOfDepartureEnd);
				}

				if (filter.StateId.HasValue)
				{
					query = query.Where(c => c.StateId == filter.StateId);
				}
			}

			var totalCount = query.Count();
			var entity =
				query.OrderBy(t=>t.DateOfDeparture).ThenBy(t=>t.Departure.Name).ThenByDescending(t => t.Id)
					.Skip(startRecord).Take(pageSize)
					.ToList()
					.Select(c => new TransportInfo(c, 2))
					.ToList();
			return new CommonPagedList<TransportInfo>(entity, pageNumber, pageSize, totalCount);
		}

	    public ICollection<string> GetErrors(long id, string actionCode)
	    {
			if (!Security.HasRightForSomeOrganization(AccessRightEnum.Transport.View))
			{
				throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.Forbidden, "Forbidden"));
			}
			SetUnitOfWorkInRefClass(UnitOfWork);
			var errors = new List<string>();
			var linksToPeoples = UnitOfWork.GetSet<LinkToPeople>().Where(l => l.TransportId == id);
			var notapprovedBouts = linksToPeoples
				.Where(l => l.Bout.StateId != StateMachineStateEnum.Bout.Confirmed && l.Bout.StateId != StateMachineStateEnum.Bout.Formed && l.Bout.StateId != StateMachineStateEnum.Deleted)
				.Select(l => l.Bout)
				.Distinct()
				.Include(b => b.GroupedTimeOfRest)
				.ToList();

		    foreach (var bout in notapprovedBouts)
		    {
			    var hotelName = bout.Hotels != null ? bout.Hotels.Name : string.Empty;
			    var groupedTimeName = bout.GroupedTimeOfRest != null ? bout.GroupedTimeOfRest.Name : string.Empty;
				var boutName = string.Format("{0} {1}", hotelName, !string.IsNullOrEmpty(groupedTimeName) ? groupedTimeName : bout.DateIncome.FormatEx() + " - " + bout.DateOutcome.FormatEx());
				errors.Add("Заезд не утвержден: " + boutName);
		    }

			return errors;
	    }

		public bool ChangeStatus(long id, string stateMachineActionString)
		{
			if (!Security.HasRightForSomeOrganization(AccessRightEnum.TransportInfoManage) || !Security.HasRight(stateMachineActionString))
			{
				throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.Forbidden, "Forbidden"));
			}
			SetUnitOfWorkInRefClass(UnitOfWork);

			using (var transaction = UnitOfWork.GetTransactionScope())
			{
				var transportInfo = UnitOfWork.GetById<TransportInfo>(id);
				if (transportInfo == null)
				{
					throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not found"));
				}

				transportInfo.HistoryLink = transportInfo.HistoryLink ?? new HistoryLink();
				transportInfo.HistoryLink.Historys = transportInfo.HistoryLink.Historys ?? new List<History>();
				if (GetErrors(id, stateMachineActionString).Any())
				{
					return false;
				}

				var action = ApiStateController.GetAction(stateMachineActionString);
				if (action != null && action.ToStateId.HasValue)
				{
					transportInfo.HistoryLink.Historys.Add(new History
					{
						AccountId = Security.GetCurrentAccountId(),
						EventCode = "Изменение статуса транспорта",
						DateChange = DateTime.Now,
						Commentary =
							string.Format("Изменение статуса транспорта с \"{0}\" на \"{1}\"", transportInfo.NullSafe(c => c.State.Name),
								action.NullSafe(a => a.ToState.Name))
					});
					transportInfo.StateId = action.NullSafe(a => a.ToStateId.Value);
				}

				UnitOfWork.SaveChanges();
				transaction.Complete();
			}
			return true;
		}

	    public ICollection<StateMachineState> GetStates()
	    {
			if (!Security.HasRightForSomeOrganization(AccessRightEnum.Transport.View))
			{
				throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.Forbidden, "Forbidden"));
			}
		    return UnitOfWork.GetSet<StateMachineState>().Where(s => s.StateMachineId == (long)StateMachineEnum.TransportState).ToList();
	    }

		/// <summary>
		/// Установить причину отказа от обратного билета
		/// </summary>
		/// <param name="id">Id связи с транспортом</param>
		/// <param name="reasonId">Id причины отказа</param>
		[System.Web.Http.ActionName("SetNotNeedTicketReason")]
		[System.Web.Http.HttpGet]
		public long SetNotNeedTicketReason(long id, long? reasonId)
		{
			SetUnitOfWorkInRefClass(UnitOfWork);

			if (!Security.HasRight(AccessRightEnum.Transport.SetNotNeedTicketReason))
			{
				throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.Forbidden, "Forbidden"));
			}

			var link = UnitOfWork.GetById<LinkToPeople>(id);

			if (link == null)
			{
				throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, "Транспорт не найден"));
			}

			if (link.Bout != null && link.Bout.StateId == StateMachineStateEnum.Bout.Confirmed && (link.NotNeedTicketReasonId == (long)NotNeedTicketReasonEnum.NotCome || reasonId == (long)NotNeedTicketReasonEnum.NotCome))
			{
				throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Нельзя изменить признак прибытия в место отдыха"));
			}

			var oldValue = link.NotNeedTicketReason;
			var newValue = UnitOfWork.GetById<NotNeedTicketReason>(reasonId ?? 0);

			link.NotNeedTicketReasonId = reasonId;
			var linkTo =
				link.Bout?.TransportInfoTo?.People?.FirstOrDefault(l => l.ChildId == link.ChildId && l.ApplicantId == link.ApplicantId && l.CounselorsId == link.CounselorsId && l.AdministratorTourId == link.AdministratorTourId);

			if (reasonId == (long) NotNeedTicketReasonEnum.NotCome)
			{
				link.NotComeInPlaceOfRest = true;
				if (linkTo != null)
				{
					linkTo.NotComeInPlaceOfRest = true;
				}
			} else if (reasonId == null && oldValue?.Id == (long) NotNeedTicketReasonEnum.NotCome)
			{
				link.NotComeInPlaceOfRest = false;
				if (linkTo != null)
				{
					linkTo.NotComeInPlaceOfRest = false;
				}
			}

			if (link.ListOfChilds != null && link.ListOfChilds.ForIndex == false)
			{
				link.ListOfChilds.ForIndex = true;
				UnitOfWork.SaveChanges();
			}

			if (link.Request != null)
			{
				link.Request.ForIndex = true;
			}

			var h = new History
			{
				AccountId = Security.GetCurrentAccountId(),
				EventCode = "Изменение причины по которой не нужен билет " + (link?.TransportId == link?.Transport?.Bout.TransportInfoToId ? "в место отдыха" : "из места отдыха"),
				DateChange = DateTime.Now,
				Commentary =
					$"Для {TransportInfoController.GetLinkPeopleName(link)} было изменено значение \"Причина по которой не нужен билет\" с '{(oldValue?.Name).FormatEx()}' на '{(newValue?.Name).FormatEx()}'",
				LinkId = link.Transport?.HistoryLinkId
			};

			if (link.Transport != null)
			{
				link.Transport.HistoryLink = link.Transport.HistoryLink ?? UnitOfWork.AddEntity(new HistoryLink());
				link.Transport.HistoryLinkId = link.Transport.HistoryLink.Id;
				UnitOfWork.Update(link.Transport);
				UnitOfWork.AddEntity(new History(h) {LinkId = link.Transport.HistoryLinkId});
			}

			if (link.Bout != null)
			{
				link.Bout.HistoryLink = link.Bout.HistoryLink ?? UnitOfWork.AddEntity(new HistoryLink());
				link.Bout.HistoryLinkId = link.Bout.HistoryLink.Id;
				UnitOfWork.Update(link.Bout);
				UnitOfWork.AddEntity(new History(h) {LinkId = link.Bout.HistoryLinkId});
			}

			UnitOfWork.SaveChanges();

			return link.NotNeedTicketReasonId ?? 0;
		}

		/// <summary>
		/// Установить признак "Нужен билет"
		/// </summary>
		/// <param name="id"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		[System.Web.Http.ActionName("SetNeedTicket")]
		[System.Web.Http.HttpGet]
	    public bool SetNeedTicket(long id, bool value)
	    {
			SetUnitOfWorkInRefClass(UnitOfWork);

			if (!Security.HasRight(AccessRightEnum.Transport.SetNotNeedTicket))
			{
				throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.Forbidden, "Forbidden"));
			}

			var link = UnitOfWork.GetById<LinkToPeople>(id);

			if (link == null)
			{
				throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, "Транспорт не найден"));
			}

			var oldValue = link.NeedTicket;
			link.NeedTicket = value;

			var h = new History
			{
				AccountId = Security.GetCurrentAccountId(),
				EventCode = "Изменение потребности в билете",
				DateChange = DateTime.Now,
				Commentary =
					$"Для {TransportInfoController.GetLinkPeopleName(link)} было изменено значение \"Нужен билет\" с '{oldValue.FormatEx()}' на '{value.FormatEx()}'",
				LinkId = link.Transport.HistoryLinkId
			};

			if (link.Transport != null)
			{
				link.Transport.HistoryLink = link.Transport.HistoryLink ?? UnitOfWork.AddEntity(new HistoryLink());
				link.Transport.HistoryLinkId = link.Transport.HistoryLink.Id;
				UnitOfWork.Update(link.Transport);
				UnitOfWork.AddEntity(new History(h) { LinkId = link.Transport.HistoryLinkId });
			}

			if (link.Bout != null)
			{
				link.Bout.HistoryLink = link.Bout.HistoryLink ?? UnitOfWork.AddEntity(new HistoryLink());
				link.Bout.HistoryLinkId = link.Bout.HistoryLink.Id;
				UnitOfWork.Update(link.Bout);
				UnitOfWork.AddEntity(new History(h) { LinkId = link.Bout.HistoryLinkId });
			}

			if (link.ListOfChilds != null && link.ListOfChilds.ForIndex == false)
			{
				link.ListOfChilds.ForIndex = true;
			}

			if (link.Request != null)
			{
				link.Request.ForIndex = true;
			}


			UnitOfWork.SaveChanges();

			return link.NeedTicket;
	    }

		internal IQueryable<LinkToPeople> GetLinksToPeople()
		{
			return UnitOfWork.GetSet<LinkToPeople>().AsQueryable();
		}

		//public override TransportInfo Put(long id, TransportInfo entity)
		//{
		//	var persisted = UnitOfWork.GetById<TransportInfo>(id);
		//	if (persisted == null)
		//	{
		//		throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
		//	}

		//	persisted.Venue = entity.Venue;
		//	persisted.DateCollection = entity.DateCollection;
		//	UnitOfWork.SaveChanges();
		//	return persisted;
		//}
    }

}
