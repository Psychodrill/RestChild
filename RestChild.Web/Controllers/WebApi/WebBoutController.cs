using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Castle.Core.Logging;
using RestChild.Comon;
using RestChild.Comon.Enumeration;
using RestChild.Extensions.Filter;
using RestChild.DAL.RepositoryExtensions;
using RestChild.Domain;
using RestChild.Web.Extensions;
using RestChild.Web.Models;
using RestChild.Web.Properties;

namespace RestChild.Web.Controllers.WebApi
{
	[Authorize]
	public class WebBoutController : WebGenericRestController<Bout>
	{
		public StateController ApiStateController { get; set; }

		public WebAdministratorTourController ApiAdministratorTourController { get; set; }

		public override void SetUnitOfWorkInRefClass(IUnitOfWork unitOfWork)
		{
			base.SetUnitOfWorkInRefClass(unitOfWork);
			ApiStateController.SetUnitOfWorkInRefClass(unitOfWork);
			ApiAdministratorTourController.SetUnitOfWorkInRefClass(unitOfWork);
		}

		/// <summary>
		///     Поиск заезда
		/// </summary>
		/// <param name="filter">Фильтр</param>
		public CommonPagedList<Bout> Get(BoutFilterModel filter)
		{
			SetUnitOfWorkInRefClass(UnitOfWork);

			int pageSize = Settings.Default.TablePageSize;
			var pageNumber = filter != null ? filter.PageNumber : 1;
			int startRecord = (pageNumber - 1) * pageSize;

			IQueryable<Bout> query;
			if (Security.HasRight(AccessRightEnum.BoutManage))
			{
				query = UnitOfWork.GetSet<Bout>().Where(t => t.StateId != null && t.StateId != StateMachineStateEnum.Deleted);
			}
			else if (Security.HasRight(AccessRightEnum.Bout.AdministratorTour))
			{
				var accountId = Security.GetCurrentAccountId();
				var boutsForAccount = UnitOfWork.GetSet<AdministratorTour>().Where(a => a.LinkedAccountId == accountId).SelectMany(a => a.Bouts);
				query = boutsForAccount.Where(t => t.StateId != null && t.StateId != StateMachineStateEnum.Deleted);
			}
			else if (Security.HasRight(AccessRightEnum.Bout.Counselor))
			{
				var accountId = Security.GetCurrentAccountId();
				var counselorId = UnitOfWork.GetSet<Counselors>().Where(a => a.LinkedAccountId == accountId).Select(c=>c.Id).FirstOrDefault();
				var boutIds =
					UnitOfWork.GetSet<LinkToPeople>()
						.Where(l => counselorId == l.CounselorsId && counselorId > 0)
						.Where(l => l.BoutId.HasValue)
						.Select(l => l.BoutId);
				query = UnitOfWork.GetSet<Bout>().Where(t => t.StateId != null && t.StateId != StateMachineStateEnum.Deleted && t.StateId != StateMachineStateEnum.Bout.Editing)
					.Where(b=>boutIds.Contains(b.Id));
			}
			else
			{
				return new CommonPagedList<Bout>(new List<Bout>(), 1, 1, 0);
			}

			query = Filter(query, filter);
			int totalCount = query.Count();
			List<Bout> entity = query.OrderBy(b => b.HotelsId).ThenBy(b => b.GroupedTimeOfRestId).Skip(startRecord).Take(pageSize).ToList();
			return new CommonPagedList<Bout>(entity, pageNumber, pageSize, totalCount);
		}

		private IQueryable<Bout> Filter(IQueryable<Bout> query, BoutFilterModel filter)
		{
			SetUnitOfWorkInRefClass(UnitOfWork);
			if (query != null && filter != null)
			{
				if (filter.HotelsId.HasValue && filter.HotelsId != 0)
				{
					query = query.Where(b => b.HotelsId == filter.HotelsId);
				}
				if (filter.YearOfRestId.HasValue && filter.YearOfRestId != 0)
				{
					query = query.Where(b => b.YearOfRestId == filter.YearOfRestId);
				}
				if (filter.StateId.HasValue && filter.StateId > 0)
				{
					query = query.Where(b => b.StateId == filter.StateId);
				}
				if (filter.GroupedTimeOfRestId.HasValue && filter.GroupedTimeOfRestId != 0)
				{
					query = query.Where(b => b.GroupedTimeOfRestId == filter.GroupedTimeOfRestId);
				}
				if (filter.HotelTypeId.HasValue)
				{
					query = query.Where(q => q.Hotels.HotelTypeId == filter.HotelTypeId);
				}
			}

			return query;
		}

		public override Bout Get(long id)
		{
			SetUnitOfWorkInRefClass(UnitOfWork);
			var bout = base.Get(id);
			if (bout == null)
			{
				throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not found"));
			}
			if (!CheckRights(bout))
			{
				throw new HttpResponseException(HttpStatusCode.Forbidden);
			}

			UpdateTransportArrivalDeparture(bout);

			return bout;
		}

		public Bout GetWithChilds(long id)
		{
			SetUnitOfWorkInRefClass(UnitOfWork);
			var bout = Get(id);
			if (bout.Partys != null && bout.Partys.Any() && bout.Chidren != null && bout.Chidren.Any())
			{
				var childsByParties = bout.Chidren.Where(c => c.BoutId == id && c.PartyId.HasValue).GroupBy(c => c.PartyId).ToDictionary(c => c.Key, c => c.ToList());
				foreach (var party in bout.Partys)
				{
					List<Child> childs;
					if (childsByParties.TryGetValue(party.Id, out childs))
					{
						party.Childs = childs;
					}

				}
			}

			return bout;
		}

		public List<string> GetErrorsOfChageStatus(long boutId, string actionCode)
		{
			SetUnitOfWorkInRefClass(UnitOfWork);
			var errors = new List<string>();
			var bout = UnitOfWork.GetById<Bout>(boutId);
			if (bout == null)
			{
				throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not found"));
			}
			var boutModel = new BoutModel(bout);
			if (!CheckRights(bout))
			{
				throw new HttpResponseException(HttpStatusCode.Forbidden);
			}

			if (actionCode == AccessRightEnum.Bout.Edit)
			{
				return errors;
			}

			var childs = WebPartyController.BaseFilterChilds(UnitOfWork, UnitOfWork.GetSet<Child>(), bout.Id);
			if (childs == null || !childs.Any())
			{
				errors.Add("В заезде отсутствуют дети");
				return errors;
			}
			if (bout.Hotels.HotelTypeId == (long)HotelTypeEnum.Camp)
			{
				if (childs.Any(c => !c.PartyId.HasValue))
				{
					errors.Add("Не все дети распределены по отрядам");
					return errors;
				}
				if (bout.Partys != null
					&& bout.Partys.Any(
						p => p.StateId != StateMachineStateEnum.Deleted && p.StateId != StateMachineStateEnum.Party.Formed))
				{
					errors.Add("Не все отряды сформированы");
					return errors;
				}
			}
			if (actionCode == AccessRightEnum.Bout.Confirmed && !boutModel.CanTransferToConfirmedState())
			{
				errors.Add(string.Format("Переход в указанный статус возможен только через три дня после даты начала заезда ({0})", boutModel.GetDateOfBoutStart().FormatEx()));
			}

			return errors;
		}

		internal List<string> ChangeStatus(long id, string actionStateCode)
		{
			SetUnitOfWorkInRefClass(UnitOfWork);
			var bout = UnitOfWork.GetById<Bout>(id);

			if (bout == null)
			{
				throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not found"));
			}

			if (!CheckRights(bout))
			{
				throw new HttpResponseException(HttpStatusCode.Forbidden);
			}

			if (actionStateCode == "Delete")
			{
				bout.HistoryLink = this.WriteHistory(bout.HistoryLink, "Удаление заезда", string.Empty,
					StateMachineStateEnum.Deleted, bout.StateId);
				bout.HistoryLinkId = bout.HistoryLink?.Id;
				bout.StateId = StateMachineStateEnum.Deleted;
			}
			else
			{
				var errors = GetErrorsOfChageStatus(id, actionStateCode);
				if (errors.Any())
				{
					return errors;
				}

				var action = ApiStateController.GetAction(actionStateCode);
				if (action != null && action.ToStateId.HasValue)
				{
					bout.HistoryLink = this.WriteHistory(bout.HistoryLink, "Изменение статуса", $"C {bout?.State?.Name} на {action?.ToState?.Name}",
						action.ToStateId, bout.StateId);
					bout.HistoryLinkId = bout.HistoryLink?.Id;
					bout.StateId = action.ToStateId;
				}
			}

			if (bout.StateId != StateMachineStateEnum.Bout.Editing && bout.StateId != StateMachineStateEnum.Deleted)
			{
				bout.IsPublishOnSite = true;
			}

			bout.IncludedInTransport = false;

			UnitOfWork.SaveChanges();
			return new List<string>();
		}

		public List<StateMachineState> GetStates()
		{
			SetUnitOfWorkInRefClass(UnitOfWork);
			return
				UnitOfWork.GetSet<StateMachineState>()
					.Where(s => s.StateMachineId == (long?)StateMachineEnum.BoutState)
					.ToList()
					.Select(s => new StateMachineState(s))
					.ToList();
		}

		public override Bout Post(Bout entity)
		{
			SetUnitOfWorkInRefClass(UnitOfWork);
			if (entity == null)
			{
				throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bad request"));
			}
			if (!CheckRights(entity))
			{
				throw new HttpResponseException(HttpStatusCode.Forbidden);
			}
			using (var transaction = UnitOfWork.GetTransactionScope())
			{
				var persisted = base.Post(entity);
				var tours =
					UnitOfWork.GetSet<Tour>()
						.Where(
							t =>
							t.StateId == StateMachineStateEnum.Tour.Formed && !t.BoutId.HasValue && t.HotelsId == entity.HotelsId
							&& t.TimeOfRest.GroupedTimeOfRestId == entity.GroupedTimeOfRestId)
						.ToList();
				foreach (var tour in tours)
				{
					tour.BoutId = entity.Id;
				}

				persisted.IncludedInTransport = false;
				persisted.LastUpdateTick = DateTime.Now.Ticks;

				UnitOfWork.SaveChanges();
				transaction.Complete();
				return persisted;
			}
		}

		[HttpGet]
		public PartyAjaxResult AddSeniorCounselor(long boutId, long counselorId)
		{
			SetUnitOfWorkInRefClass(UnitOfWork);
			return AddCounselorToBout(boutId, counselorId, CounselorTypeEnum.Senior);
		}

		[HttpGet]
		public PartyAjaxResult AddSwingCounselor(long boutId, long counselorId)
		{
			SetUnitOfWorkInRefClass(UnitOfWork);
			return AddCounselorToBout(boutId, counselorId, CounselorTypeEnum.Swing);
		}

		private void AddLinkToCounselor(Counselors counselor, Bout bout, TypeOfLinkPeopleEnum type)
		{
			if (bout == null)
			{
				return;
			}

			if (bout.TransportInfoToId.HasValue)
				UnitOfWork.Context.Entry(new LinkToPeople
				{
					CounselorsId = counselor.Id,
					BoutId = bout?.Id,
					NeedTicket = true,
					TypeOfLinkPeopleId = (long)type,
					LastUpdateTick = DateTime.Now.Ticks,
					TransportId = bout.TransportInfoToId
				}).State = EntityState.Added;

			if (bout.TransportInfoFromId.HasValue)
				UnitOfWork.Context.Entry(new LinkToPeople
				{
					CounselorsId = counselor.Id,
					BoutId = bout?.Id,
					NeedTicket = true,
					TypeOfLinkPeopleId = (long)type,
					LastUpdateTick = DateTime.Now.Ticks,
					TransportId = bout.TransportInfoFromId
				}).State = EntityState.Added;
		}

		private void AddLinkToAdministartor(AdministratorTour administrator, Bout bout)
		{
			if (bout == null)
			{
				return;
			}

			if (bout.TransportInfoToId.HasValue)
				UnitOfWork.Context.Entry(new LinkToPeople
				{
					AdministratorTourId = administrator.Id,
					BoutId = bout?.Id,
					NeedTicket = true,
					TypeOfLinkPeopleId = (long)TypeOfLinkPeopleEnum.Administrator,
					LastUpdateTick = DateTime.Now.Ticks,
					TransportId = bout.TransportInfoToId
				}).State = EntityState.Added;

			if (bout.TransportInfoFromId.HasValue)
				UnitOfWork.Context.Entry(new LinkToPeople
				{
					AdministratorTourId = administrator.Id,
					BoutId = bout?.Id,
					NeedTicket = true,
					TypeOfLinkPeopleId = (long) TypeOfLinkPeopleEnum.Administrator,
					LastUpdateTick = DateTime.Now.Ticks,
					TransportId = bout.TransportInfoFromId
				}).State = EntityState.Added;
		}

		[HttpGet]
		public PartyAjaxResult AddAdministartorTour(long boutId, long administratorId)
		{
			SetUnitOfWorkInRefClass(UnitOfWork);
			string lastUpdateTick = null;
			using (var transaction = UnitOfWork.GetTransactionScope())
			{
				var bout = UnitOfWork.GetById<Bout>(boutId);
				var administrator = UnitOfWork.GetById<AdministratorTour>(administratorId);

				if (bout != null && administrator != null)
				{
					if (!CheckRights(bout))
					{
						throw new HttpResponseException(HttpStatusCode.Forbidden);
					}

					//if (bout.GroupedTimeOfRestId.HasValue && administrator.Bouts.Any(b => b.GroupedTimeOfRestId == bout.GroupedTimeOfRestId))
					//{
					//	return new PartyAjaxResult { HasError = true, ErrorMessage = "Администратор уже назначен" };
					//}

					//if (bout.DateIncome.HasValue && bout.DateOutcome.HasValue && administrator.Bouts.Any(p => p.DateIncome <= bout.DateOutcome && p.DateOutcome >= bout.DateIncome ))
					//{
					//	return new PartyAjaxResult { HasError = true, ErrorMessage = "Администратор уже назначен" };
					//}

					if (bout.StateId != StateMachineStateEnum.Bout.Editing)
					{
						return new PartyAjaxResult { HasError = true, ErrorMessage = "Заезд должен быть в статусе \"Редактирование\"" };
					}
					bout.AdministratorTours = bout.AdministratorTours ?? new List<AdministratorTour>();
					bout.AdministratorTours.Add(administrator);
					AddLinkToAdministartor(administrator, bout);
					bout.IncludedInTransport = false;

					UnitOfWork.SaveChanges();
					lastUpdateTick = bout.LastUpdateTick.ToString();
					transaction.Complete();
				}
			}

			return new PartyAjaxResult {BoutLastUpdateTick = lastUpdateTick};
		}

		[HttpGet]
		public PartyAjaxResult RemoveSeniorCounselor(long boutId, long counselorId)
		{
			SetUnitOfWorkInRefClass(UnitOfWork);
			string lastUpdateTick = null;
			var bout = UnitOfWork.GetById<Bout>(boutId);
			if (bout != null)
			{
				if (!CheckRights(bout))
				{
					throw new HttpResponseException(HttpStatusCode.Forbidden);
				}

				var counselor = bout.SeniorCounselors.FirstOrDefault(c => c.Id == counselorId);
				var ltps =
					UnitOfWork.GetSet<LinkToPeople>().Where(l => l.CounselorsId == counselorId && l.BoutId == boutId).ToList();
				if (counselor != null)
				{
					bout.SeniorCounselors.Remove(counselor);
					foreach (var lt in ltps)
					{
						UnitOfWork.Delete(lt);
					}
					bout.IncludedInTransport = false;
					lastUpdateTick = bout.LastUpdateTick.ToString();
					UnitOfWork.SaveChanges();
				}
			}
			return new PartyAjaxResult { BoutLastUpdateTick = lastUpdateTick };
		}

		[HttpGet]
		public PartyAjaxResult RemoveSwingCounselor(long boutId, long counselorId)
		{
			SetUnitOfWorkInRefClass(UnitOfWork);
			string lastUpdateTick = null;

			var bout = UnitOfWork.GetById<Bout>(boutId);
			if (bout != null)
			{
				if (!CheckRights(bout))
				{
					throw new HttpResponseException(HttpStatusCode.Forbidden);
				}

				var counselor = bout.SwingCounselors.FirstOrDefault(c => c.Id == counselorId);
				var ltps =
					UnitOfWork.GetSet<LinkToPeople>().Where(l => l.CounselorsId == counselorId && l.BoutId == boutId).ToList();

				if (counselor != null)
				{
					bout.SwingCounselors.Remove(counselor);
					foreach (var lt in ltps)
					{
						UnitOfWork.Delete(lt);
					}
					bout.IncludedInTransport = false;
					lastUpdateTick = bout.LastUpdateTick.ToString();
					UnitOfWork.SaveChanges();
				}
			}
			return new PartyAjaxResult { BoutLastUpdateTick = lastUpdateTick };
		}

		[HttpGet]
		public PartyAjaxResult RemoveAdministratotTour(long boutId, long administratorId)
		{
			SetUnitOfWorkInRefClass(UnitOfWork);
			string lastUpdateTick = null;

			var bout = UnitOfWork.GetById<Bout>(boutId);
			if (bout != null)
			{
				if (!CheckRights(bout))
				{
					throw new HttpResponseException(HttpStatusCode.Forbidden);
				}

				var administrator = bout.AdministratorTours.FirstOrDefault(a => a.Id == administratorId);
				var ltps =
					UnitOfWork.GetSet<LinkToPeople>().Where(l => l.AdministratorTourId == administratorId && l.BoutId == boutId).ToList();
				if (administrator != null)
				{
					bout.AdministratorTours.Remove(administrator);
					foreach (var lt in ltps)
					{
						UnitOfWork.Delete(lt);
					}
					bout.IncludedInTransport = false;
					lastUpdateTick = bout.LastUpdateTick.ToString();
					UnitOfWork.SaveChanges();
				}
			}
			return new PartyAjaxResult { BoutLastUpdateTick = lastUpdateTick };
		}

		private PartyAjaxResult AddCounselorToBout(long boutId, long counselorId, CounselorTypeEnum counselorType)
		{
			SetUnitOfWorkInRefClass(UnitOfWork);
			var bout = UnitOfWork.GetById<Bout>(boutId);
			var counselor = UnitOfWork.GetById<Counselors>(counselorId);
			string lastUpdateTick = null;

			if (bout != null && counselor != null)
			{
				if (!CheckRights(bout))
				{
					throw new HttpResponseException(HttpStatusCode.Forbidden);
				}

				if (bout.StateId != StateMachineStateEnum.Bout.Editing)
				{
					return new PartyAjaxResult { HasError = true, ErrorMessage = "Заезд должен быть в статусе \"Редактирование\"" };
				}
				if (counselorType == CounselorTypeEnum.Senior)
				{
					bout.SeniorCounselors = bout.SeniorCounselors ?? new List<Counselors>();
					AddLinkToCounselor(counselor, bout, TypeOfLinkPeopleEnum.SeniorCounselor);
					bout.SeniorCounselors.Add(counselor);
				}
				else if (counselorType == CounselorTypeEnum.Swing)
				{
					bout.SwingCounselors = bout.SwingCounselors ?? new List<Counselors>();
					AddLinkToCounselor(counselor, bout, TypeOfLinkPeopleEnum.SwingCounselor);
					bout.SwingCounselors.Add(counselor);
				}

				bout.IncludedInTransport = false;
				lastUpdateTick = bout.LastUpdateTick.ToString();
				UnitOfWork.SaveChanges();
			}

			return new PartyAjaxResult { BoutLastUpdateTick = lastUpdateTick };
		}

		/// <summary>
		/// Установить отметку "Не явился в место отдыха" для ребенка
		/// </summary>
		[ActionName("SetNotComeInPlaceOfRest")]
		[HttpGet]
		public bool SetNotComeInPlaceOfRest(long linkToPeopleId, long linkToPeopleFromId, bool notCome)
		{
			SetUnitOfWorkInRefClass(UnitOfWork);
			var link = UnitOfWork.GetById<LinkToPeople>(linkToPeopleId);
			var linkFrom = UnitOfWork.GetById<LinkToPeople>(linkToPeopleFromId);

			if (link == null || linkFrom == null)
			{
				throw new HttpResponseException(HttpStatusCode.NotFound);
			}

			if (CheckRightsForNotComeInPlaceOfRest(link.BoutId))
			{
				link.NotComeInPlaceOfRest = notCome;
				linkFrom.NotComeInPlaceOfRest = notCome;
				linkFrom.NotNeedTicketReasonId = notCome && !linkFrom.NotNeedTicketReasonId.HasValue
					? (long?) NotNeedTicketReasonEnum.NotCome
					: linkFrom.NotNeedTicketReasonId == (long?) NotNeedTicketReasonEnum.NotCome
						? (long?) null
						: linkFrom.NotNeedTicketReasonId;

				var h = new History
				{
					AccountId = Security.GetCurrentAccountId(),
					EventCode = "Изменение признака прибытия в место отдыха",
					DateChange = DateTime.Now,
					Commentary =
						$"Для {TransportInfoController.GetLinkPeopleName(link)} было изменено значение \"Прибыл\" на '{(!notCome).FormatEx()}'",
					LinkId = link.Transport?.HistoryLinkId
				};

				if (link.Transport != null)
				{
					link.Transport.HistoryLink = link.Transport.HistoryLink ?? UnitOfWork.AddEntity(new HistoryLink());
					link.Transport.HistoryLinkId = link.Transport.HistoryLink.Id;
					UnitOfWork.Update(link.Transport);
					UnitOfWork.AddEntity(new History(h) { LinkId = link.Transport.HistoryLinkId });
				}

				if (linkFrom?.Transport != null)
				{
					linkFrom.Transport.HistoryLink = linkFrom.Transport.HistoryLink ?? UnitOfWork.AddEntity(new HistoryLink());
					linkFrom.Transport.HistoryLinkId = linkFrom.Transport.HistoryLink.Id;
					UnitOfWork.Update(linkFrom.Transport);
					UnitOfWork.AddEntity(new History(h) { LinkId = linkFrom.Transport.HistoryLinkId });
				}

				if (link.Bout != null)
				{
					link.Bout.HistoryLink = link.Bout.HistoryLink ?? UnitOfWork.AddEntity(new HistoryLink());
					link.Bout.HistoryLinkId = link.Bout.HistoryLink.Id;
					UnitOfWork.Update(link.Bout);
					UnitOfWork.AddEntity(new History(h) { LinkId = link.Bout.HistoryLinkId });
				}

				if (linkFrom?.Request != null)
				{
					linkFrom.Request.ForIndex = true;
				}

				if (linkFrom?.ListOfChilds != null && linkFrom.ListOfChilds.ForIndex == false)
				{
					linkFrom.ListOfChilds.ForIndex = true;
					UnitOfWork.SaveChanges();
				}
			}
			else
			{
				throw new HttpResponseException(HttpStatusCode.Forbidden);
			}

			UnitOfWork.SaveChanges();

			return link.NotComeInPlaceOfRest;
		}

		/// <summary>
		/// Установить отметку "сдан родителям" для ребенка
		/// </summary>
		[ActionName("SetDeliveredParents")]
		[HttpGet]
		public bool SetDeliveredParents(long linkToPeopleId, bool notCome)
		{
			SetUnitOfWorkInRefClass(UnitOfWork);
			var link = UnitOfWork.GetById<LinkToPeople>(linkToPeopleId);

			if (link == null)
			{
				throw new HttpResponseException(HttpStatusCode.NotFound);
			}

			if (CheckRightsForNotComeInPlaceOfRest(link.BoutId))
			{
				link.DeliveredParents = notCome;
			}
			else
			{
				throw new HttpResponseException(HttpStatusCode.Forbidden);
			}

			UnitOfWork.SaveChanges();

			return link.DeliveredParents;
		}

		/// <summary>
		/// Установить отметку "Не явился в место отдыха" для ребенка
		/// </summary>
		/// <param name="childId">Id ребенка</param>
		/// <param name="notCome">Отметка</param>
		/// <returns>Значение отметки "Не явился в место отдыха"</returns>
		[ActionName("SetChildNotComeInPlaceOfRest")]
		[HttpGet]
		public bool SetChildNotComeInPlaceOfRest(long childId, bool notCome)
		{
			SetUnitOfWorkInRefClass(UnitOfWork);
			var child = UnitOfWork.GetById<Child>(childId);

			if (child == null)
			{
				throw new HttpResponseException(HttpStatusCode.NotFound);
			}

			if (CheckRightsForNotComeInPlaceOfRest(child.BoutId))
			{
				child.NotComeInPlaceOfRest = notCome;
			}
			else
			{
				throw new HttpResponseException(HttpStatusCode.Forbidden);
			}

			UnitOfWork.SaveChanges();

			return child.NotComeInPlaceOfRest;
		}


		/// <summary>
		/// Установить отметку "Не явился в место отдыха" для ребенка
		/// </summary>
		/// <param name="applicantId">Id сопровождающего</param>
		/// <param name="notCome">Отметка</param>
		/// <returns>Значение отметки "Не явился в место отдыха"</returns>
		[ActionName("SetApplicantNotComeInPlaceOfRest")]
		[HttpGet]
		public bool SetApplicantNotComeInPlaceOfRest(long applicantId, bool notCome)
		{
			SetUnitOfWorkInRefClass(UnitOfWork);
			var applicant = UnitOfWork.GetById<Applicant>(applicantId);
			if (applicant == null)
			{
				throw new HttpResponseException(HttpStatusCode.NotFound);
			}

			if (CheckRightsForNotComeInPlaceOfRest(applicant.BoutId))
			{
				applicant.NotComeInPlaceOfRest = notCome;
			}
			else
			{
				throw new HttpResponseException(HttpStatusCode.Forbidden);
			}

			UnitOfWork.SaveChanges();
			return applicant.NotComeInPlaceOfRest;
		}

		private bool CheckRightsForNotComeInPlaceOfRest(long? boutId)
		{
			if (!Security.HasRight(AccessRightEnum.Bout.NotComeInPlaceOfRestForAllBouts))
			{
				var administratorToursForAccount = ApiAdministratorTourController.GetAdministratorToursForAccount();
				if (administratorToursForAccount.Any(a => a.Bouts != null && !a.Bouts.Any(b => b.Id == boutId)))
				{
					return false;
				}
			}

			return true;
		}

		internal bool CheckRights(Bout bout)
		{
			SetUnitOfWorkInRefClass(UnitOfWork);
			if (Security.HasRight(AccessRightEnum.BoutManage))
			{
				return true;
			}
			if (Security.HasRight(AccessRightEnum.Bout.AdministratorTour))
			{
				var accountId = Security.GetCurrentAccountId();
				return UnitOfWork.GetSet<AdministratorTour>().Any(a => a.LinkedAccountId == accountId && a.Bouts.Select(b => b.Id).Contains(bout.Id));
			}
			if (Security.HasRight(AccessRightEnum.Bout.Counselor))
			{
				var accountId = Security.GetCurrentAccountId();

				var counselorsId =
					UnitOfWork.GetSet<Counselors>().Where(c => c.LinkedAccountId == accountId).Select(c => (long?)c.Id).ToList();
				return UnitOfWork.GetSet<LinkToPeople>().Any(a => a.BoutId == bout.Id && counselorsId.Contains(a.CounselorsId));
			}

			return false;
		}

		public override Bout Put(long id, Bout entity)
		{
			SetUnitOfWorkInRefClass(UnitOfWork);
			var bout = UnitOfWork.GetById<Bout>(id);
			if (bout == null)
			{
				throw new HttpResponseException(new HttpResponseMessage { StatusCode = HttpStatusCode.NotFound });
			}

			CopyEntity(entity, bout);
			if (Security.HasRight(AccessRightEnum.Bout.Edit) && (bout.StateId == StateMachineStateEnum.Bout.Editing || bout.StateId == StateMachineStateEnum.Bout.Formed))
			{
				bout.IsPublishOnSite = entity.IsPublishOnSite;
			}

			bout.IncludedInTransport = false;
			bout.HistoryLink = this.WriteHistory(bout.HistoryLink, "Сохранение заезда", string.Empty);
			bout.HistoryLinkId = bout.HistoryLink?.Id;
			bout.LastUpdateTick = DateTime.Now.Ticks;

			UpdateTransportArrivalDeparture(bout);
			UnitOfWork.SaveChanges();
			return bout;
		}

		private void UpdateTransportArrivalDeparture(Bout bout)
		{
			var needSave = false;

			var canTransportTo = Security.HasRight(AccessRightEnum.BoutManage) && (bout.StateId == StateMachineStateEnum.Bout.Editing || bout.StateId == StateMachineStateEnum.Bout.Formed);
			var canTransportFrom = Security.HasRight(AccessRightEnum.BoutManage) && (bout.StateId == StateMachineStateEnum.Bout.Editing || bout.StateId == StateMachineStateEnum.Bout.Formed || bout.StateId == StateMachineStateEnum.Bout.Confirmed);

			if (canTransportFrom && bout.TransportInfoFrom != null && bout.Hotels != null && bout.TransportInfoFrom?.DepartureId != bout.Hotels?.CityId)
			{
				bout.TransportInfoFrom.DepartureId = bout.Hotels.CityId;
				bout.TransportInfoFrom.Departure = bout.Hotels.City;
				bout.TransportInfoFrom.ArrivalId = (long) CityEnum.Moscow;
				needSave = true;
			}

			if (canTransportTo && bout.TransportInfoTo != null && bout.Hotels != null && bout.TransportInfoTo?.ArrivalId != bout.Hotels?.CityId)
			{
				bout.TransportInfoTo.ArrivalId = bout.Hotels.CityId;
				bout.TransportInfoTo.Arrival = bout.Hotels.City;
				bout.TransportInfoTo.DepartureId = (long) CityEnum.Moscow;
				needSave = true;
			}

			if (needSave)
			{
				UnitOfWork.SaveChanges();
			}
		}

		private void CopyEntity(Bout source, Bout destination)
		{
			var canTransportTo = Security.HasRight(AccessRightEnum.BoutManage) && (destination.StateId == StateMachineStateEnum.Bout.Editing || destination.StateId == StateMachineStateEnum.Bout.Formed);
			var canTransportFrom = Security.HasRight(AccessRightEnum.BoutManage) && (destination.StateId == StateMachineStateEnum.Bout.Editing || destination.StateId == StateMachineStateEnum.Bout.Formed || destination.StateId == StateMachineStateEnum.Bout.Confirmed);
			if (canTransportTo)
			{
				if (destination.TransportInfoTo != null && source.TransportInfoTo != null)
				{
					destination.TransportInfoTo.Venue = source.TransportInfoTo.Venue;
					destination.TransportInfoTo.DateCollection = source.TransportInfoTo.DateCollection;
					destination.TransportInfoTo.Memo = source.TransportInfoTo.Memo;
					destination.TransportInfoTo.MemoFile = source.TransportInfoTo.MemoFile;
					destination.TransportInfoTo.MemoLink = source.TransportInfoTo.MemoLink;
					destination.TransportInfoTo.DateOfDeparture = source.TransportInfoTo.DateOfDeparture;
					destination.TransportInfoTo.DateArrival = source.TransportInfoTo.DateArrival;
				}
			}

			if (canTransportFrom)
			{
				if (destination.TransportInfoFrom != null && source.TransportInfoFrom != null)
				{
					destination.TransportInfoFrom.Venue = source.TransportInfoFrom.Venue;
					destination.TransportInfoFrom.DateCollection = source.TransportInfoFrom.DateCollection;
					destination.TransportInfoFrom.Memo = source.TransportInfoFrom.Memo;
					destination.TransportInfoFrom.MemoFile = source.TransportInfoFrom.MemoFile;
					destination.TransportInfoFrom.MemoLink = source.TransportInfoFrom.MemoLink;
					destination.TransportInfoFrom.DateOfDeparture = source.TransportInfoFrom.DateOfDeparture;
					destination.TransportInfoFrom.DateArrival = source.TransportInfoFrom.DateArrival;
				}
			}

			if (destination.StateId == StateMachineStateEnum.Bout.Confirmed)
			{
				destination.Comment = source.Comment;
			}
		}

		/// <summary>
		/// получить журнал
		/// </summary>
		[HttpPost]
		public void ChangeJournalActive(long id, bool state)
		{
			var journal = UnitOfWork.GetById<BoutJournal>(id);
			if (journal != null)
			{
				journal.ForSite = state;
				UnitOfWork.SaveChanges();
			}
		}

		/// <summary>
		/// получить журнал
		/// </summary>
		[HttpGet]
		public List<BoutJournalDto> GetBoutJournal(long boutId, bool? viewOnSite, bool? forEvent, bool? forReport, bool? forIncident)
		{
			SetUnitOfWorkInRefClass(UnitOfWork);
			var journal = UnitOfWork.GetSet<BoutJournal>().Where(b => b.BoutId == boutId && !b.IsArchive && b.BoutJournalTypeId.HasValue);

			if (!(forEvent ?? true))
			{
				journal = journal.Where(j => j.BoutJournalTypeId != (long) BoutJournalTypeEnum.Event);
			}

			if (!(forReport ?? true))
			{
				journal = journal.Where(j => j.BoutJournalTypeId != (long)BoutJournalTypeEnum.Report);
			}

			if (!(forIncident ?? true))
			{
				journal = journal.Where(j => j.BoutJournalTypeId != (long)BoutJournalTypeEnum.Incident);
			}

			if (viewOnSite.HasValue && viewOnSite.Value)
				journal = journal.Where(i => i.ForSite);

			return
				journal.OrderByDescending(b => b.EventDate).ThenByDescending(b => b.Id).ToList().Select(b => new BoutJournalDto
				{
					Id = b.Id,
					DateCreate = b.DateCreate.FormatEx(),
					BoutJournalTypeName = b.BoutJournalType?.Name,
					Incident = (b?.CategoryIncident?.Parent != null ? $"{b?.CategoryIncident?.Parent?.Name}/":string.Empty) + b?.CategoryIncident?.Name,
					IsArchive = b.IsArchive,
					Title = b.Title,
					DateEvent = b.EventDate.FormatEx(),
					PartyNumber = (b.Party?.PartyNumber).FormatEx(),
					ForSite = b.ForSite,
					BoutJournalType = b.BoutJournalTypeId ?? 0,
                    Author = b.Counselors != null
						? $"{b.Counselors.LastName} {b.Counselors.FirstName} {b.Counselors.MiddleName}"
						: (b.AdministratorTour != null
							? $"{b.AdministratorTour.LastName} {b.AdministratorTour.FirstName} {b.AdministratorTour.MiddleName}"
							: "-")
				}).ToList();
		}
	}
}
