using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Web.Http;

using RestChild.Comon;
using RestChild.Comon.Enumeration;
using RestChild.DAL.RepositoryExtensions;
using RestChild.Domain;
using RestChild.Web.Models;
using RestChild.Web.Properties;
using Slepov.Russian.Morpher;

namespace RestChild.Web.Controllers.WebApi
{
	[Authorize]
	public class WebPartyController : WebGenericRestController<Party>
	{
		public StateController ApiStateController { get; set; }

		public override void SetUnitOfWorkInRefClass(IUnitOfWork unitOfWork)
		{
			base.SetUnitOfWorkInRefClass(unitOfWork);
			ApiStateController.SetUnitOfWorkInRefClass(unitOfWork);
		}

		internal PartyFilter GetChildsGrouped([FromUri] PartyFilter filter)
		{
			SetUnitOfWorkInRefClass(UnitOfWork);
			var bout = UnitOfWork.GetById<Bout>(filter.BoutsId ?? 0);
			filter.IsMale = null;
			filter.AgeFrom = null;
			filter.AgeTo = null;
			var query = FilterChilds(UnitOfWork.GetSet<Child>().AsQueryable(), filter);

			var childsByRequests = query
				.Where(c => c.Request != null)
				.GroupBy(c => c.Request, c => c,
					(r, c) =>
						new GroupedChilds
						{
							Childs = c.OrderBy(gc => gc.LastName).ThenBy(gc => gc.FirstName).ThenBy(gc => gc.MiddleName).ToList(),
							Name = "Льготные путевки"
						})
				.ToList();

			var childsByLists = query
				.Where(c => c.ChildList != null)
				.GroupBy(c => c.ChildList, c => c,
					(l, c) =>
						new GroupedChilds
						{
							Childs = c.OrderBy(oc => oc.LastName).ThenBy(oc => oc.FirstName).ThenBy(oc => oc.MiddleName).ToList(),
							Name = "Профильные путевки",
							Organization = l.LimitOnOrganization.Organization,
							Oiv = l.LimitOnOrganization.LimitOnVedomstvo.Organization
						})
				.ToList();

			filter.GroupedChilds = childsByRequests.Concat(childsByLists)
				.Select(
					g =>
						new GroupedChilds
						{
							Childs = g.Childs.Select(c => new Child(c, 2)).ToList(),
							Name = g.Name,
							Organization = new Organization(g.Organization),
							Oiv = new Organization(g.Oiv)
						}).ToList();
			CalculateChildsCounts(filter);
			filter.IsEditable = bout != null && bout.StateId == StateMachineStateEnum.Bout.Editing && Security.HasRight(AccessRightEnum.BoutManage);
			filter.BoutState = bout?.State;
			filter.Bout = bout;
			return filter;
		}

		internal PartyFilter GetChildsUngrouped([FromUri] PartyFilter filter)
		{
			SetUnitOfWorkInRefClass(UnitOfWork);
			var bout = UnitOfWork.GetById<Bout>(filter.BoutsId ?? 0);
			var query = FilterChilds(UnitOfWork.GetSet<Child>().AsQueryable(), filter);
			IOrderedQueryable<Child> orderedQuery = null;

			if (filter.OrderBy == "Age")
			{
				orderedQuery = query.OrderByDescending(c => c.DateOfBirth);
			}
			else if (filter.OrderBy == "Gender")
			{
				orderedQuery = query.OrderBy(c => c.Male);
			}
			else if (filter.OrderBy == "DateIncome")
			{
				orderedQuery = query.OrderBy(c => c.Request.Tour.DateIncome).ThenBy(c => c.ChildList.Tour.DateIncome);
			}
			else if (filter.OrderBy == "DateOutcome")
			{
				orderedQuery = query.OrderBy(c => c.Request.Tour.DateOutcome).ThenBy(c => c.ChildList.Tour.DateOutcome);
			}

			if (orderedQuery != null)
			{
				query = orderedQuery.ThenBy(c => c.LastName).ThenBy(c => c.FirstName).ThenBy(c => c.MiddleName);
			}
			else
			{
				query = query.OrderBy(c => c.LastName).ThenBy(c => c.FirstName).ThenBy(c => c.MiddleName);
			}


			filter.UngroupedChilds = query.ToList();
			filter.IsEditable = bout != null && bout.StateId == StateMachineStateEnum.Bout.Editing && Security.HasRight(AccessRightEnum.BoutManage);
			filter.BoutState = bout?.State;
			filter.Bout = bout;
			CalculateChildsCounts(filter);
			if (filter.SubjectsOfRest != null && filter.SubjectsOfRest.Any())
			{
				filter.SubjectsOfRest = filter.SubjectsOfRest.Where(s => BaseFilterChilds(UnitOfWork, UnitOfWork.GetSet<Child>().AsQueryable(), filter.BoutsId ?? 0).Any(c => c.Request.SubjectOfRestId == s.Id)).ToList();
			}
			return filter;
		}

		internal PartyFilter GetParties([FromUri] PartyFilter filter)
		{
			SetUnitOfWorkInRefClass(UnitOfWork);
			var bout = UnitOfWork.GetById<Bout>(filter.BoutsId ?? 0);
			var query = UnitOfWork.GetSet<Party>()
				.Where(p => p.BoutsId == filter.BoutsId && p.StateId != StateMachineStateEnum.Deleted);
			if (!Security.HasRight(AccessRightEnum.BoutManage))
			{
				if (Security.HasRight(AccessRightEnum.Bout.AdministratorTour))
				{
					var accountId = Security.GetCurrentAccountId();
					var boutsForAccount =
						UnitOfWork.GetSet<AdministratorTour>().Where(a => a.LinkedAccountId == accountId).SelectMany(a => a.Bouts).Select(b => (long?)b.Id);
					query = query.Where(c => boutsForAccount.Contains(c.BoutsId));
				}
				else if (Security.HasRight(AccessRightEnum.Bout.Counselor))
				{
					var accountId = Security.GetCurrentAccountId();
					var counselorId =
						UnitOfWork.GetSet<Counselors>().Where(a => a.LinkedAccountId == accountId).Select(c => c.Id).FirstOrDefault();
					var boutIds =
						UnitOfWork.GetSet<LinkToPeople>()
							.Where(l => counselorId == l.CounselorsId && counselorId > 0)
							.Where(l => l.BoutId.HasValue)
							.Select(l => l.BoutId);
					query = query.Where(c => boutIds.Contains(c.BoutsId));
				}
				else
				{
					query = new List<Party>().AsQueryable();
				}
			}
			var parties = query
				.GroupJoin(UnitOfWork.GetSet<Child>(), p => p.Id, c => c.PartyId,
					(p, c) =>
						new
						{
							Party = p,
							Child = c.OrderBy(oc => oc.LastName).ThenBy(oc => oc.FirstName).ThenBy(oc => oc.MiddleName).ToList(),
							p.Counselors
						})
				.OrderByDescending(p => p.Party.PartyNumber)
				.ToList()
				.Select(
					g =>
					new PartyModel(
						new Party(g.Party, 2)
							{
								Childs = g.Child,
								Counselors = g.Counselors,
								Bouts =
									new Bout(g.Party.Bouts)
										{
											Tours =
												g.Party != null && g.Party.Bouts != null
												&& g.Party.Tours != null
													? g.Party.Bouts.Tours.Select(t => new Tour(t)).ToList()
													: null
										}
							})
						{
							State =
								new ViewModelState()
									{
										State = g.Party.State,
										Actions =
											ApiStateController
											.GetActions(
												g.Party.State, StateMachineEnum.PartyState)
									}
						})
				.ToList();
			filter.Parties = parties;
			filter.IsEditable = bout != null && bout.StateId == StateMachineStateEnum.Bout.Editing && Security.HasRight(AccessRightEnum.BoutManage);
			filter.BoutState = bout.NullSafe(b => b.State);
			if (filter.SubjectsOfRest != null && filter.SubjectsOfRest.Any())
			{
				filter.SubjectsOfRest = filter.SubjectsOfRest.Where(s => BaseFilterChilds(UnitOfWork, UnitOfWork.GetSet<Child>().AsQueryable(), filter.BoutsId ?? 0).Any(c => c.Request.SubjectOfRestId == s.Id)).ToList();
			}

			filter.Bout = bout;

			return filter;
		}

		[HttpPost]
		public PartyAjaxResult AddParty(PartyFilter filter)
		{
			SetUnitOfWorkInRefClass(UnitOfWork);
			if (!Security.HasRight(AccessRightEnum.BoutManage))
			{
				return new PartyAjaxResult { HasError = true, ErrorMessage = "Нет прав для добавления отряда"};
			}

			var partyNumber = 0;
			var tours =
				UnitOfWork.GetSet<Tour>()
					.Where(t => t.BoutId == filter.BoutsId)
					.ToList();
			var partiesInTours =
				tours.SelectMany(t => t.Partys)
					.Where(p => p.PartyNumber.HasValue && p.PartyNumber > 0 && p.StateId != StateMachineStateEnum.Deleted && p.BoutsId == filter.BoutsId)
					.OrderBy(p => p.PartyNumber)
					.ToList();

			if (partiesInTours.Any() && partiesInTours.First().PartyNumber == 1)
			{
				if (partiesInTours.Count >= 2)
				{
					for (var i = 1; i < partiesInTours.Count; i++)
					{
						partyNumber = (partiesInTours[i].PartyNumber ?? 0);
						if (partiesInTours[i].PartyNumber - partiesInTours[i - 1].PartyNumber > 1)
						{
							partyNumber = (partiesInTours[i - 1].PartyNumber ?? 0);
							break;
						}
					}
				}
				else
				{
					partyNumber = (partiesInTours[0].PartyNumber ?? 0);
				}
			}

			var newParty = new Party
			{
				HotelsId = filter.HotelsId,
				BoutsId = filter.BoutsId,
				Tours = new List<Tour> {tours.FirstOrDefault()},
				Name = "Отряд",
				StateId = StateMachineStateEnum.Party.Forming,
				PartyNumber = partyNumber + 1
			};

			UnitOfWork.GetSet<Party>().Add(newParty);
			UnitOfWork.SaveChanges();
			return new PartyAjaxResult {OpenedPartyId = newParty.Id};
		}

		[HttpPost]
		public PartyAjaxResult AddToParty(long? partyId, [FromBody] ICollection<long> childs)
		{
			SetUnitOfWorkInRefClass(UnitOfWork);
			if (!Security.HasRight(AccessRightEnum.BoutManage))
			{
				return new PartyAjaxResult { HasError = true, ErrorMessage = "Нет прав для добавления отряда" };
			}

			if (!partyId.HasValue)
			{
				return new PartyAjaxResult();
			}
			var party = UnitOfWork.GetById<Party>(partyId.Value);
			if (party == null)
			{
				return new PartyAjaxResult();
			}
			if (party.StateId != StateMachineStateEnum.Party.Forming)
			{
				return new PartyAjaxResult {HasError = true, ErrorMessage = "Отряд должен быть в статусе \"Формирование\""};
			}
			var persistedChilds = UnitOfWork.GetSet<Child>().Where(c => childs.Contains(c.Id)).ToList();
			var linkToPeoples = UnitOfWork.GetSet<LinkToPeople>().Where(c => c.ChildId.HasValue && childs.Contains(c.ChildId.Value)).ToList();
			foreach (var child in persistedChilds)
			{
				child.PartyId = partyId;
				UnitOfWork.Context.Entry(child).State = EntityState.Modified;

				if (party?.Bouts?.TransportInfoToId.HasValue ?? false)
				{
					var lpTo =
						linkToPeoples.FirstOrDefault(l => l.ChildId == child.Id && l.TransportId == party.Bouts.TransportInfoToId);
					AddLinkToChildTo(partyId, lpTo, child, party);
					if (lpTo != null)
					{
						var lpToDel =
							linkToPeoples.Where(
								l => l.ChildId == child.Id && l.TransportId == party.Bouts.TransportInfoToId && l.Id != lpTo.Id).ToList();
						foreach (var l in lpToDel)
						{
							UnitOfWork.Context.Entry(l).State = EntityState.Deleted;
						}
					}
				}
				if (party?.Bouts?.TransportInfoFromId.HasValue ?? false)
				{
					var lpFrom =
						linkToPeoples.FirstOrDefault(l => l.ChildId == child.Id && l.TransportId == party.Bouts.TransportInfoFromId);
					AddLinkToChildFrom(partyId, lpFrom, child, party);
					if (lpFrom != null)
					{
						var lpToDel =
							linkToPeoples.Where(
								l => l.ChildId == child.Id && l.TransportId == party.Bouts.TransportInfoFromId && l.Id != lpFrom.Id).ToList();
						foreach (var l in lpToDel)
						{
							UnitOfWork.Context.Entry(l).State = EntityState.Deleted;
						}
					}
				}
			}

			UnitOfWork.SaveChanges();
			return new PartyAjaxResult();
		}

		private void AddLinkToChildTo(long? partyId, LinkToPeople lpTo, Child child, Party party)
		{
			if (lpTo == null)
			{
				UnitOfWork.Context.Entry(new LinkToPeople
				{
					ChildId = child.Id,
					PartyId = partyId,
					BoutId = party?.BoutsId,
					NeedTicket = true,
					RequestId = child.RequestId,
					ListOfChildsId = child.ChildListId,
					TypeOfLinkPeopleId = (long) TypeOfLinkPeopleEnum.Child,
					LastUpdateTick = DateTime.Now.Ticks,
					TransportId = party?.Bouts?.TransportInfoToId,
					NotComeInPlaceOfRest = true,
				}).State = EntityState.Added;
			}
			else
			{
				lpTo.PartyId = partyId;
				UnitOfWork.Context.Entry(lpTo).Property(c => c.PartyId).IsModified = true;
			}

			if (child?.Request != null)
			{
				child.Request.ForIndex = true;
			}
			if (child?.ChildList != null && child.ChildList.ForIndex == false)
			{
				child.ChildList.ForIndex = true;
				UnitOfWork.SaveChanges();
			}
		}

		private void AddLinkToChildFrom(long? partyId, LinkToPeople lpTo, Child child, Party party)
		{
			if (lpTo == null)
			{
				UnitOfWork.Context.Entry(new LinkToPeople
				{
					ChildId = child.Id,
					PartyId = partyId,
					BoutId = party?.BoutsId,
					NeedTicket = true,
					RequestId = child.RequestId,
					ListOfChildsId = child.ChildListId,
					TypeOfLinkPeopleId = (long)TypeOfLinkPeopleEnum.Child,
					LastUpdateTick = DateTime.Now.Ticks,
					TransportId = party?.Bouts?.TransportInfoFromId,
					NotComeInPlaceOfRest = true,
					NotNeedTicketReasonId = (long)NotNeedTicketReasonEnum.NotCome
				}).State = EntityState.Added;
			}
			else
			{
				lpTo.PartyId = partyId;
				UnitOfWork.Context.Entry(lpTo).Property(c => c.PartyId).IsModified = true;
			}

			if (child?.Request != null)
			{
				child.Request.ForIndex = true;
			}
			if (child?.ChildList != null && child.ChildList.ForIndex == false)
			{
				child.ChildList.ForIndex = true;
				UnitOfWork.SaveChanges();
			}
		}

		[HttpGet]
		public PartyAjaxResult ExcludeFromParty([FromUri] ICollection<long> childs)
		{
			SetUnitOfWorkInRefClass(UnitOfWork);
			if (!Security.HasRight(AccessRightEnum.BoutManage))
			{
				return new PartyAjaxResult { HasError = true, ErrorMessage = "Нет прав для исключения из отряда" };
			}

			var persistedChilds = UnitOfWork.GetSet<Child>().Where(c => childs.Contains(c.Id)).ToList();
			if (persistedChilds.Any(c => c.Party.StateId != StateMachineStateEnum.Party.Forming))
			{
				return new PartyAjaxResult {HasError = true, ErrorMessage = "Отряд должен быть в статусе \"Формирование\""};
			}

			var linkToPeoples = UnitOfWork.GetSet<LinkToPeople>().Where(c => c.ChildId.HasValue && childs.Contains(c.ChildId.Value)).ToList();

			using (var tran = UnitOfWork.GetTransactionScope())
			{
				foreach (var ltp in linkToPeoples)
				{
					ltp.PartyId = null;
				}

				foreach (var child in persistedChilds)
				{
					child.PartyId = null;
					UnitOfWork.Context.Entry(child).Property(c => c.PartyId).IsModified = true;
				}

				UnitOfWork.SaveChanges();
				tran.Complete();
			}

			return new PartyAjaxResult();
		}

		[HttpGet]
		public PartyAjaxResult AddCounselorToParty(long partyId, long counselorId)
		{
			if (!Security.HasRight(AccessRightEnum.BoutManage))
			{
				return new PartyAjaxResult { HasError = true, ErrorMessage = "Нет прав для добавления вожатого" };
			}

			SetUnitOfWorkInRefClass(UnitOfWork);
			using (var transaction = UnitOfWork.GetTransactionScope())
			{
				var party = UnitOfWork.GetById<Party>(partyId);
				var counselor = UnitOfWork.GetById<Counselors>(counselorId);

				if (party != null && counselor != null)
				{
					if (counselor.Partys != null && counselor.Partys.Any(p => p.HotelsId == party.HotelsId && p.TimeOfRest != null && p.TimeOfRest.GroupedTimeOfRestId == party.NullSafe(pp => pp.TimeOfRest.GroupedTimeOfRestId))
						|| (counselor.Bouts != null && counselor.Bouts.Any(b => b.HotelsId == party.HotelsId && b.GroupedTimeOfRestId == party.NullSafe(pp => pp.TimeOfRest.GroupedTimeOfRestId))
						|| (counselor.SwingBoats != null && counselor.SwingBoats.Any(b => b.HotelsId == party.HotelsId && b.GroupedTimeOfRestId == party.NullSafe(pp => pp.TimeOfRest.GroupedTimeOfRestId)))))
					{
						return new PartyAjaxResult {HasError = true, ErrorMessage = "Вожатый уже находится в другом отряде"};
					}

					if (party.StateId != StateMachineStateEnum.Party.Forming)
					{
						return new PartyAjaxResult {HasError = true, ErrorMessage = "Отряд должен быть в статусе \"Формирование\""};
					}
					party.Counselors = party.Counselors ?? new List<Counselors>();
					party.Counselors.Add(counselor);

					if (party?.Bouts?.TransportInfoToId.HasValue ?? false)
					{
						var linkToPepopleTo = new LinkToPeople
						{
							CounselorsId = counselorId,
							PartyId = partyId,
							BoutId = party.BoutsId,
							LastUpdateTick = DateTime.Now.Ticks,
							TypeOfLinkPeopleId = (long) TypeOfLinkPeopleEnum.Counselor,
							TransportId = party?.Bouts?.TransportInfoToId,
							NeedTicket = true
						};

						UnitOfWork.Context.Entry(linkToPepopleTo).State = EntityState.Added;
					}
					if (party?.Bouts?.TransportInfoFromId.HasValue ?? false)
					{
						var linkToPepopleFrom = new LinkToPeople
						{
							CounselorsId = counselorId,
							PartyId = partyId,
							BoutId = party.BoutsId,
							LastUpdateTick = DateTime.Now.Ticks,
							TypeOfLinkPeopleId = (long) TypeOfLinkPeopleEnum.Counselor,
							TransportId = party?.Bouts?.TransportInfoFromId,
							NeedTicket = true
						};

						UnitOfWork.Context.Entry(linkToPepopleFrom).State = EntityState.Added;
					}
					UnitOfWork.SaveChanges();
					transaction.Complete();
				}
			}

			return new PartyAjaxResult();
		}

		[HttpGet]
		public PartyAjaxResult RemoveCounselorFromParty(long partyId, long counselorId)
		{
			if (!Security.HasRight(AccessRightEnum.BoutManage))
			{
				return new PartyAjaxResult { HasError = true, ErrorMessage = "Нет прав для удаления вожатого" };
			}

			SetUnitOfWorkInRefClass(UnitOfWork);
			var party = UnitOfWork.GetById<Party>(partyId);
			var counselor = UnitOfWork.GetById<Counselors>(counselorId);
			if (party != null && counselor != null)
			{
				if (party.StateId != StateMachineStateEnum.Party.Forming)
				{
					return new PartyAjaxResult {HasError = true, ErrorMessage = "Отряд должен быть в статусе \"Формирование\""};
				}

				var lps =
					UnitOfWork.GetSet<LinkToPeople>().Where(l => l.CounselorsId == counselorId && l.PartyId == partyId).ToList();

				using (var transaction = UnitOfWork.GetTransactionScope())
				{
					foreach (var lp in lps)
					{
						UnitOfWork.Delete(lp);
					}

					party.Counselors.Remove(counselor);
					UnitOfWork.SaveChanges();
					transaction.Complete();
				}
				return new PartyAjaxResult();
			}
			return new PartyAjaxResult {HasError = true, ErrorMessage = "Отряд не найден"};
		}

		[HttpGet]
		public PartyAjaxResult RemoveParty(long partyId)
		{
			if (!Security.HasRight(AccessRightEnum.BoutManage))
			{
				return new PartyAjaxResult { HasError = true, ErrorMessage = "Нет прав для удаления отряда" };
			}
			SetUnitOfWorkInRefClass(UnitOfWork);
			var party = UnitOfWork.GetById<Party>(partyId);
			if (party != null)
			{
				if (party.StateId != StateMachineStateEnum.Party.Forming)
				{
					return new PartyAjaxResult {HasError = true, ErrorMessage = "Отряд должен быть в статусе \"Формирование\""};
				}
				party.StateId = StateMachineStateEnum.Deleted;
				var childs = UnitOfWork.GetSet<Child>().Where(c => c.PartyId == partyId).ToList();
				var partys = UnitOfWork.GetSet<Party>().Where(c => c.BoutsId == party.BoutsId && c.StateId.HasValue && c.StateId != StateMachineStateEnum.Deleted && c.Id != partyId).OrderBy(p=>p.PartyNumber).ToList();
				var linkToPeoples = UnitOfWork.GetSet<LinkToPeople>().Where(c => c.PartyId == partyId).ToList();

				foreach (var child in childs)
				{
					child.PartyId = null;
				}

				foreach (var ltp in linkToPeoples)
				{
					ltp.PartyId = null;
				}

				var partyNumber = 1;

				foreach (var p in partys)
				{
					p.Name = partyNumber.ToString();
					p.PartyNumber = partyNumber++;
				}

				party.PartyNumber = null;
				party.Name = "-";

				UnitOfWork.SaveChanges();
				return new PartyAjaxResult();
			}
			return new PartyAjaxResult {HasError = true, ErrorMessage = "Отряд не найден"};
		}

		[HttpGet]
		public PartyAjaxResult ChangeState(long partyId, string actionCode)
		{
			SetUnitOfWorkInRefClass(UnitOfWork);
			if (!Security.HasRight(actionCode))
			{
				return new PartyAjaxResult { HasError = true, ErrorMessage = "Недостаточно прав для изменения статуса отряда" };
			}

			var party = UnitOfWork.GetById<Party>(partyId);
			if (party != null)
			{
				var state = ApiStateController.GetNextState(party.StateId, actionCode);
				var errors = ValidatePartyBeforeChangeState(party, actionCode);

				if (state != null && string.IsNullOrEmpty(errors))
				{
					party.StateId = state.Id;
					UnitOfWork.SaveChanges();
					return new PartyAjaxResult { HasError = false };
				}
				else
				{
					return new PartyAjaxResult { HasError = true, ErrorMessage = errors };
				}


			}
			return new PartyAjaxResult { HasError = true, ErrorMessage = "Отряд не найден" };
		}

		[HttpGet]
		public PartyAjaxResult NeedTicket(long childId, bool forward, bool need)
		{
			if (!Security.HasRight(AccessRightEnum.BoutManage))
			{
				return new PartyAjaxResult { HasError = true, ErrorMessage = "Нет прав для изменения признака" };
			}
			SetUnitOfWorkInRefClass(UnitOfWork);
			var child = UnitOfWork.GetById<Child>(childId);
			if (child == null)
			{
				return new PartyAjaxResult {HasError = true, ErrorMessage = "Ребенок не найден"};
			}
			if (forward)
			{
				child.NotNeedTicketForward = !need;
			}
			else
			{
				child.NotNeedTicketBackward = !need;
			}
			UnitOfWork.SaveChanges();
			return new PartyAjaxResult();
		}

		private IQueryable<Child> FilterChilds(IQueryable<Child> query, PartyFilter filter)
		{
			SetUnitOfWorkInRefClass(UnitOfWork);
			if (filter == null)
			{
				return query;
			}

			query = BaseFilterChilds(UnitOfWork, query, filter.BoutsId ?? 0);

			if (filter.AgeFrom.HasValue)
			{
				query = query.Where(c => DbFunctions.AddSeconds(DbFunctions.AddYears(DbFunctions.TruncateTime(c.Request.Tour.Bout.DateIncome ?? c.ChildList.Tour.Bout.DateIncome ?? DateTime.Today), -filter.AgeFrom), 1) >= c.DateOfBirth);
			}

			if (filter.AgeTo.HasValue)
			{
				query = query.Where(c => DbFunctions.AddSeconds(DbFunctions.AddYears(DbFunctions.TruncateTime(c.Request.Tour.Bout.DateIncome ?? c.ChildList.Tour.Bout.DateIncome ?? DateTime.Today), -filter.AgeTo - 1), 1) <= c.DateOfBirth);
			}

			if (filter.IsMale.HasValue)
			{
				query = query.Where(c => c.Male == filter.IsMale.Value);
			}

			if (!string.IsNullOrEmpty(filter.Name))
			{
				var s = filter.Name.ToLower();
				query = query.Where(c => ((c.LastName ?? string.Empty) + " " + (c.FirstName ?? string.Empty) + " " + (c.MiddleName ?? string.Empty)).ToLower().Contains(s));
			}

			if (filter.OnlySpecilized)
			{
				query = query.Where(c => c.ChildListId.HasValue);
			}

			if (filter.OnlyNotAdded ?? true)
			{
				query = query.Where(c => !c.PartyId.HasValue);
			}

			if (filter.OnlyBenefits)
			{
				query = query.Where(c => !c.Request.TypeOfRest.Commercial);
			}

			if (filter.SubjectOfRestid.HasValue)
			{
				query = query.Where(c => c.Request.SubjectOfRestId == filter.SubjectOfRestid);
			}

			return query;
		}

		internal static IQueryable<Child> BaseFilterChilds(IUnitOfWork unitOfWork, IQueryable<Child> query, long boutsId)
		{
			query =
				query.Where(c => c.BoutId == boutsId && c.IsLast && !c.IsDeleted && c.Bout.StateId.HasValue && c.Bout.StateId != StateMachineStateEnum.Deleted);
			if (!Security.HasRight(AccessRightEnum.BoutManage))
			{
				if (Security.HasRight(AccessRightEnum.Bout.AdministratorTour))
				{
					var accountId = Security.GetCurrentAccountId();
					var boutsForAccount =
						unitOfWork.GetSet<AdministratorTour>().Where(a => a.LinkedAccountId == accountId).SelectMany(a => a.Bouts).Select(b=>(long?)b.Id);
					query = query.Where(c=> boutsForAccount.Contains(c.BoutId));
				}
				else if (Security.HasRight(AccessRightEnum.Bout.Counselor))
				{
					var accountId = Security.GetCurrentAccountId();
					var counselorId =
						unitOfWork.GetSet<Counselors>().Where(a => a.LinkedAccountId == accountId).Select(c => c.Id).FirstOrDefault();
					var boutIds =
						unitOfWork.GetSet<LinkToPeople>()
							.Where(l => counselorId == l.CounselorsId && counselorId > 0)
							.Where(l => l.BoutId.HasValue)
							.Select(l => l.BoutId);
					query = query.Where(c => boutIds.Contains(c.BoutId));
				}
				else
				{
					query = new List<Child>().AsQueryable();
				}
			}

			return query;
		}

		private void CalculateChildsCounts(PartyFilter filter)
		{
			var query = BaseFilterChilds(UnitOfWork, UnitOfWork.GetSet<Child>().AsQueryable(), filter.BoutsId ?? 0);
			filter.AllChildrenCount = query.Count();
			filter.VacantChildrenCount = query.Count(c => !c.PartyId.HasValue);
		}

		private string ValidatePartyBeforeChangeState(Party party, string actionCode)
		{
			var builder = new StringBuilder();
			var childs = UnitOfWork.GetSet<Child>().Where(c => c.Payed && !c.IsDeleted && c.PartyId == party.Id).ToList();

			if (!childs.Any())
			{
				builder.Append("В отряде должен быть хотя бы один ребёнок");
			}

			return builder.ToString();
		}
	}
}
