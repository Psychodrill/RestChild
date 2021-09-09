using System;
using System.Data.Entity;
using System.Linq;
using RestChild.Comon;
using RestChild.Comon.Enumeration;
using RestChild.DAL;
using RestChild.DAL.RepositoryExtensions;
using RestChild.Domain;
using System.Collections.Generic;

namespace MailingDemon.Tasks
{
	/// <summary>
	///     генерация заездов по блокам номеров.
	/// </summary>
	[Task]
	public class BoatCreatorTask : BaseTask
	{
		/// <summary>
		///     исполнение задачи
		/// </summary>
		protected override void Execute()
		{
			try
			{
				CreateBout();
			}
			catch (Exception ex)
			{
				Logger.Error("CreateBoat error", ex);
			}

			try
			{
				CreateTransportForBout();
			}
			catch (Exception ex)
			{
				Logger.Error("CreateTransportForBout error", ex);
			}

			try
			{
				LinkChildToBoat();
			}
			catch (Exception ex)
			{
				Logger.Error("LinkChildToBoat error", ex);
			}

			try
			{
				LinkApplicantToBoat();
			}
			catch (Exception ex)
			{
				Logger.Error("LinkApplicantToBoat error", ex);
			}

			try
			{
				UpdatePartyInChild();
			}
			catch (Exception ex)
			{
				Logger.Error("UpdatePartyInChild error", ex);
			}

			try
			{
				RefreshTransport();
			}
			catch (Exception ex)
			{
				Logger.Error("RefreshTransport error", ex);
			}

			try
			{
				DeleteBoats();
			}
			catch (Exception ex)
			{
				Logger.Error("DeleteBoats error", ex);
			}
		}

		private void RefreshTransportOnBout(IUnitOfWork unitOfWork, Bout bout, TransportInfo transport)
		{
			var peopls = transport.People;
			// добавление администраторов смены.
			foreach (var admins in bout.AdministratorTours)
			{
				if (peopls.All(p => p.AdministratorTourId != admins.Id))
				{
					unitOfWork.Context.Set<LinkToPeople>().Add(new LinkToPeople
					{
						AdministratorTourId = admins.Id,
						BoutId = bout.Id,
						TypeOfLinkPeopleId = (long)TypeOfLinkPeopleEnum.Administrator,
						TransportId = transport.Id,
						NeedTicket = true
					});
				}
			}

			var peoplesToExplude =
				peopls.Where(
					p =>
						p.TypeOfLinkPeopleId == (long)TypeOfLinkPeopleEnum.Administrator && p.BoutId == bout.Id &&
						p.AdministratorTourId.HasValue &&
						!bout.AdministratorTours.Select(a => a.Id).Contains(p.AdministratorTourId.Value)).ToList();

			foreach (var people in peoplesToExplude)
			{
				unitOfWork.Context.Entry(people).State = EntityState.Deleted;
			}

			// старшие вожатые
			foreach (var seniorCounselor in bout.SeniorCounselors)
			{
				if (peopls.All(p => p.CounselorsId != seniorCounselor.Id))
				{
					unitOfWork.Context.Set<LinkToPeople>().Add(new LinkToPeople
					{
						CounselorsId = seniorCounselor.Id,
						BoutId = bout.Id,
						TypeOfLinkPeopleId = (long)TypeOfLinkPeopleEnum.SeniorCounselor,
						TransportId = transport.Id,
						NeedTicket = true
					});
				}
			}

			peoplesToExplude =
				peopls.Where(
					p =>
						p.TypeOfLinkPeopleId == (long)TypeOfLinkPeopleEnum.SeniorCounselor && p.BoutId == bout.Id &&
						p.CounselorsId.HasValue &&
						!bout.SeniorCounselors.Select(a => a.Id).Contains(p.CounselorsId.Value)).ToList();

			foreach (var people in peoplesToExplude)
			{
				unitOfWork.Context.Entry(people).State = EntityState.Deleted;
			}


			// сменынне вожатые
			foreach (var swingCounselor in bout.SwingCounselors)
			{
				if (peopls.All(p => p.CounselorsId != swingCounselor.Id))
				{
					unitOfWork.Context.Set<LinkToPeople>().Add(new LinkToPeople
					{
						CounselorsId = swingCounselor.Id,
						BoutId = bout.Id,
						TypeOfLinkPeopleId = (long)TypeOfLinkPeopleEnum.SwingCounselor,
						TransportId = transport.Id,
						NeedTicket = true
					});
				}
			}

			peoplesToExplude =
				peopls.Where(
					p =>
						p.TypeOfLinkPeopleId == (long)TypeOfLinkPeopleEnum.SwingCounselor && p.BoutId == bout.Id &&
						p.CounselorsId.HasValue &&
						!bout.SwingCounselors.Select(a => a.Id).Contains(p.CounselorsId.Value)).ToList();

			foreach (var people in peoplesToExplude)
			{
				unitOfWork.Context.Entry(people).State = EntityState.Deleted;
			}

			// дети
			foreach (var child in bout.Chidren)
			{
				if (peopls.All(p => p.ChildId != child.Id))
				{
					unitOfWork.Context.Set<LinkToPeople>().Add(new LinkToPeople
					{
						ChildId = child.Id,
						RequestId = child.RequestId,
						ListOfChildsId = child.ChildListId,
						BoutId = bout.Id,
						TypeOfLinkPeopleId = (long)TypeOfLinkPeopleEnum.Child,
						TransportId = transport.Id,
						PartyId = child.PartyId,
						NeedTicket = true,
						NotComeInPlaceOfRest = true,
						NotNeedTicketReasonId = bout.TransportInfoFromId == transport.Id ? (long)NotNeedTicketReasonEnum.NotCome : (long?)null
					});
				}
				else
				{
					var people = peopls.FirstOrDefault(p => p.ChildId == child.Id);
					if (people != null && people.PartyId != child.PartyId)
					{
						people.PartyId = child.PartyId;
					}
				}
			}

			peoplesToExplude =
				peopls.Where(
					p =>
						p.TypeOfLinkPeopleId == (long)TypeOfLinkPeopleEnum.Child && p.BoutId == bout.Id &&
						p.ChildId.HasValue &&
						!bout.Chidren.Select(a => a.Id).Contains(p.ChildId.Value)).ToList();

			foreach (var people in peoplesToExplude)
			{
				unitOfWork.Context.Entry(people).State = EntityState.Deleted;
			}

			foreach (var party in bout.Partys)
			{
				foreach (var counselor in party.Counselors)
				{
					if (!peopls.Any(p => p.CounselorsId == counselor.Id && p.PartyId == party.Id))
					{
						unitOfWork.Context.Set<LinkToPeople>().Add(new LinkToPeople
						{
							CounselorsId = counselor.Id,
							BoutId = bout.Id,
							TypeOfLinkPeopleId = (long)TypeOfLinkPeopleEnum.Counselor,
							TransportId = transport.Id,
							PartyId = party.Id,
							NeedTicket = true,
							NotComeInPlaceOfRest = true,
							NotNeedTicketReasonId = bout.TransportInfoFromId == transport.Id ? (long)NotNeedTicketReasonEnum.NotCome : (long?)null
						});
					}
				}

				peoplesToExplude =
					peopls.Where(
						p =>
							p.TypeOfLinkPeopleId == (long)TypeOfLinkPeopleEnum.Counselor && p.BoutId == bout.Id &&
							p.CounselorsId.HasValue &&
							p.PartyId == party.Id &&
							!party.Counselors.Select(a => a.Id).Contains(p.CounselorsId.Value)).ToList();

				foreach (var people in peoplesToExplude)
				{
					unitOfWork.Context.Entry(people).State = EntityState.Deleted;
				}
			}

			foreach (var applicant in bout.Applicants)
			{
				if (peopls.All(p => p.ApplicantId != applicant.Id))
				{
					var lp = new LinkToPeople
					{
						ApplicantId = applicant.Id,
						RequestId = applicant.RequestId,
						ListOfChildsId = applicant.ChildListId,
						BoutId = bout.Id,
						TypeOfLinkPeopleId = (long)TypeOfLinkPeopleEnum.Attendant,
						TransportId = transport.Id,
						NeedTicket = true,
						NotComeInPlaceOfRest = true,
						NotNeedTicketReasonId = bout.TransportInfoFromId == transport.Id ? (long)NotNeedTicketReasonEnum.NotCome : (long?)null
					};

					if (!lp.ListOfChildsId.HasValue && !lp.RequestId.HasValue)
					{
						var requestId =
							unitOfWork.GetSet<Request>().Where(a => a.ApplicantId == lp.ApplicantId).Select(r => r.Id).FirstOrDefault();
						if (requestId > 0)
						{
							lp.RequestId = requestId;
						}
					}

					unitOfWork.Context.Set<LinkToPeople>().Add(lp);
				}
			}

			peoplesToExplude =
				peopls.Where(
					p =>
						p.TypeOfLinkPeopleId == (long)TypeOfLinkPeopleEnum.Attendant && p.BoutId == bout.Id &&
						p.ApplicantId.HasValue &&
						!bout.Applicants.Select(a => a.Id).Contains(p.ApplicantId.Value)).ToList();

			foreach (var people in peoplesToExplude)
			{
				unitOfWork.Context.Entry(people).State = EntityState.Deleted;
			}

			unitOfWork.SaveChanges();
		}

		private void RefreshTransport()
		{
			using (var unitOfWork = new UnitOfWork())
			{
				Logger.Info("Обновление связей для транспорта");

				var bouts = unitOfWork.GetSet<Bout>().Where(b => !b.IncludedInTransport && b.TransportInfoFrom != null && b.TransportInfoTo != null).ToList();
				foreach (var b in bouts)
				{
					var bout = b;
					RefreshTransportOnBout(unitOfWork, bout, bout.TransportInfoFrom);
					RefreshTransportOnBout(unitOfWork, bout, bout.TransportInfoTo);
					bout.IncludedInTransport = true;
					unitOfWork.Update(bout);
				}

				unitOfWork.SaveChanges();
				Logger.Info("Обновление связей для транспорта конец");
			}
		}

		/// <summary>
		/// обновление отряда в тарнспорте.
		/// </summary>
		private void UpdatePartyInChild()
		{
			using (var unitOfWork = new UnitOfWork())
			{
				Logger.Info("Обновление детей для формирования заездов");


				var childsForUpdate =
					unitOfWork.GetSet<LinkToPeople>()
						.Where(l => l.TypeOfLinkPeopleId == (long)TypeOfLinkPeopleEnum.Child && l.PartyId != l.Child.PartyId)
						.Take(1000)
						.Include(c => c.Child)
						.ToList();
				foreach (var child in childsForUpdate)
				{
					child.PartyId = child.Child.PartyId;
				}

				unitOfWork.SaveChanges();
				Logger.Info("Обновление детей для формирования заездов окончено");
			}
		}

		private void DeleteBoats()
		{
			using (var unitOfWork = new UnitOfWork())
			{
				Logger.Info("Определяем размещения для разрыва связей");
				var tours =
					unitOfWork.GetSet<Tour>().Where(x => x.StateId == StateMachineStateEnum.Deleted && x.BoutId.HasValue)
						.Take(500)
						.ToList();
				Logger.Info("Разрываем связь размещений с заявлениями и размещениями");
				foreach (var tour in tours)
				{
					var t = tour;
					var childs =
						unitOfWork.GetSet<Child>().Where(c => c.BoutId == t.BoutId && (c.Request.TourId == t.Id || c.ChildList.TourId == t.Id)).ToList();

					RemoveBoutFromChild(unitOfWork, childs);

					var applicants =
						unitOfWork.GetSet<Applicant>().Where(c => c.BoutId == t.BoutId && (c.Request.TourId == t.Id || c.ChildList.TourId == t.Id)).ToList();
					foreach (var applicant in applicants)
					{
						applicant.Bout = null;
						applicant.BoutId = null;
						ClearAttendantLinkToTransport(unitOfWork, applicant);
					}

					applicants =
						unitOfWork.GetSet<Request>()
							.Where(c => c.Applicant.BoutId == t.BoutId && c.TourId == t.Id)
							.Where(r => r.Applicant != null)
							.Select(r => r.Applicant)
							.ToList();

					foreach (var applicant in applicants)
					{
						applicant.Bout = null;
						applicant.BoutId = null;
						ClearAttendantLinkToTransport(unitOfWork, applicant);
					}

					tour.BoutId = null;
					tour.Bout = null;

					unitOfWork.SaveChanges();
				}

				Logger.Info("Ищем заезды для удаления");
				var toursWithBout = unitOfWork.GetSet<Tour>().Where(b => b.BoutId.HasValue).Select(b => b.BoutId);

				var bouts = unitOfWork.GetSet<Bout>().Where(b => !toursWithBout.Contains(b.Id)).Take(500).ToList();
				Logger.Info("Удаляем заезды");
				foreach (var b in bouts)
				{
					var bout = b;
					if (bout.TransportInfoFrom != null && bout.Id == bout.TransportInfoFrom.BoutId)
					{
						var transport = bout.TransportInfoFrom;
						transport = ClearTransport(unitOfWork, transport);

						bout.TransportInfoFrom = null;
						bout.TransportInfoFromId = null;
						bout = unitOfWork.Update(bout);
						unitOfWork.Delete(transport);
					}

					if (bout.TransportInfoTo != null && bout.Id == bout.TransportInfoTo.BoutId)
					{
						var transport = bout.TransportInfoTo;
						transport = ClearTransport(unitOfWork, transport);

						bout.TransportInfoTo = null;
						bout.TransportInfoToId = null;
						bout = unitOfWork.Update(bout);

						unitOfWork.Delete(transport);
					}

					var trs = unitOfWork.GetSet<TransportInfo>().Where(t => t.BoutId == bout.Id).ToList();
					foreach (var tr in trs)
					{
						var t = tr;
						t = ClearTransport(unitOfWork, t);
						unitOfWork.Delete(t);
					}

					var childs = unitOfWork.GetSet<Child>().Where(t => t.BoutId == bout.Id).ToList();
					foreach (var child in childs)
					{
						child.BoutId = null;
						child.EkisNeedSend = true;
						child.Bout = null;
						child.PartyId = null;
						child.Party = null;
						unitOfWork.SaveChanges();
					}

					var applicants = unitOfWork.GetSet<Applicant>().Where(t => t.BoutId == bout.Id).ToList();
					foreach (var applicant in applicants)
					{
						applicant.BoutId = null;
						applicant.Bout = null;
						unitOfWork.SaveChanges();
					}

					var ltps = unitOfWork.GetSet<LinkToPeople>().Where(t => t.BoutId == bout.Id).ToList();
					foreach (var ltp in ltps)
					{
						unitOfWork.Delete(ltp);
					}

					var bjs = unitOfWork.GetSet<BoutJournal>().Where(t => t.BoutId == bout.Id).ToList();
					foreach (var bj in bjs)
					{
						foreach (var bjf in bj.Files.ToList())
						{
							bj.Files.Remove(bjf);
							unitOfWork.Delete(bjf);
						}

						unitOfWork.Delete(bj);
					}

					var ctsq = unitOfWork.GetSet<CounselorTask>().Where(t => t.BoutId == bout.Id);
					var files =
						unitOfWork.GetSet<CounselorTaskFile>()
							.Where(t => ctsq.Select(c => (long?)c.Id).Contains(t.CounselorTaskId))
							.ToList();
					foreach (var ct in files)
					{
						unitOfWork.Delete(ct);
					}
					var commentarys =
						unitOfWork.GetSet<CounselorTaskCommentary>()
							.Where(t => ctsq.Select(c => (long?)c.Id).Contains(t.CounselorTaskId))
							.ToList();
					foreach (var ct in commentarys)
					{
						unitOfWork.Delete(ct);
					}

					var rfs =
						unitOfWork.GetSet<CounselorTaskReportFile>()
							.Where(t => ctsq.Select(c => (long?)c.Id).Contains(t.CounselorTaskId))
							.ToList();

					foreach (var ct in rfs)
					{
						unitOfWork.Delete(ct);
					}

					var cts = ctsq.ToList();
					foreach (var ct in cts)
					{
						unitOfWork.Delete(ct);
					}

					var rfts = unitOfWork.GetSet<ResponsibilityForTask>().Where(t => t.BoutId == bout.Id).ToList();
					foreach (var rft in rfts)
					{
						unitOfWork.Delete(rft);
					}

					var boutsId = unitOfWork.GetSet<Party>().Where(p => p.BoutsId == bout.Id).Select(p => (long?)p.Id);

					var childParty = unitOfWork.GetSet<Child>().Where(t => boutsId.Contains(t.PartyId)).ToList();
					foreach (var child in childParty)
					{
						child.PartyId = null;
						child.Party = null;
						unitOfWork.SaveChanges();
					}
					var partys = unitOfWork.GetSet<Party>().Where(t => t.BoutsId == bout.Id).ToList();
					foreach (var party in partys)
					{
						var p = party;
						p.Counselors.Clear();
						foreach (var lp in unitOfWork.GetSet<LinkToPeople>().Where(l => l.PartyId == p.Id).ToList())
						{
							lp.PartyId = null;
							lp.Party = null;
						}

						unitOfWork.SaveChanges();
						unitOfWork.Delete(p);
					}

					bout.TransportInfoToId = null;
					bout.TransportInfoTo = null;
					bout.TransportInfoFromId = null;
					bout.TransportInfoFrom = null;
					unitOfWork.SaveChanges();

					var tis = unitOfWork.GetSet<TransportInfo>().Where(t => t.BoutId == bout.Id).ToList();
					foreach (var party in tis)
					{
						var p = party;
						unitOfWork.Delete(p);
					}

					unitOfWork.Delete(bout);
				}

				unitOfWork.SaveChanges();
				Logger.Info("Удалили заезды");
			}
		}

		private static void RemoveBoutFromChild(UnitOfWork unitOfWork, IEnumerable<Child> children)
		{
			foreach (var child in children)
			{
				child.Bout = null;
				child.BoutId = null;
				child.PartyId = null;
				child.Party = null;
				child.EkisNeedSend = true;
				unitOfWork.Context.Entry(child).State = EntityState.Modified;
				ClearChildLinkToTransport(unitOfWork, child);
			}
		}

		private static TransportInfo ClearTransport(UnitOfWork unitOfWork, TransportInfo transport)
		{
			transport.BoutId = null;
			transport.Bout = null;
			transport = unitOfWork.Update(transport);
			var links = unitOfWork.GetSet<LinkToPeople>().Where(l => l.TransportId == transport.Id).ToList();
			foreach (var link in links)
			{
				unitOfWork.Delete(link);
			}

			return transport;
		}

		private void CreateTransportForBout()
		{
			using (var unitOfWork = new UnitOfWork())
			{
				Logger.Info("Создаем транспорт к заездам там где его нет started");
				var bouts =
					unitOfWork.GetSet<Bout>().Where(x => (!x.TransportInfoFromId.HasValue || !x.TransportInfoToId.HasValue) && x.DateIncome.HasValue && x.DateOutcome.HasValue)
						.ToList();

				foreach (var bout in bouts)
				{
					AppendTransportToBout(unitOfWork, bout);
				}

				unitOfWork.SaveChanges();
				Logger.Info("Создаем транспорт к заездам там где его нет end");
			}
		}

		private void CreateBout()
		{
			using (var unitOfWork = new UnitOfWork())
			{
				Logger.Info("Цепляем блоки мест к заездам started");
				var tours =
					unitOfWork.GetSet<Tour>().Where(x => x.StateId == StateMachineStateEnum.Tour.Formed && !x.BoutId.HasValue
														 && x.Hotels.StateId == StateMachineStateEnum.Hotel.Approved && x.HotelsId.HasValue)
						.Take(500)
						.ToList();
				// проход по заездам и добавление в них списков
				foreach (var tour in tours)
				{
					var gtrId = tour.GroupedTimeOfRestId ?? tour.TimeOfRest?.GroupedTimeOfRestId;

					var bout =
						unitOfWork.GetSet<Bout>()
							.FirstOrDefault(
								b =>
									b.YearOfRestId == tour.YearOfRestId && b.HotelsId == tour.HotelsId &&
									((b.GroupedTimeOfRestId == gtrId ||
									  (!b.GroupedTimeOfRestId.HasValue && !gtrId.HasValue)) &&
									 (b.DateIncome == tour.DateIncome && b.DateOutcome == tour.DateOutcome))) ??
						unitOfWork.AddEntity(new Bout
						{
							YearOfRestId = tour.YearOfRestId,
							HotelsId = tour.HotelsId,
							GroupedTimeOfRestId = gtrId,
							DateIncome = tour.DateIncome,
							DateOutcome = tour.DateOutcome,
							StateId = StateMachineStateEnum.Bout.Editing
						});

					AppendTransportToBout(unitOfWork, bout);
					tour.BoutId = bout.Id;
				}

				unitOfWork.SaveChanges();
				Logger.Info("Цепляем блоки мест к заездам finished");
			}
		}

		private static void AppendTransportToBout(UnitOfWork unitOfWork, Bout bout)
		{
			if (!bout.TransportInfoFromId.HasValue && bout.TransportInfoFrom == null)
			{
				bout.TransportInfoFrom = unitOfWork.AddEntity(new TransportInfo
				{
					DateOfDeparture = bout.DateOutcome ?? DateTime.Now,
					DepartureId = bout.Hotels.CityId,
					ArrivalId = (long)CityEnum.Moscow,
					YearOfRestId = bout.YearOfRestId,
					StateId = StateMachineStateEnum.Transport.Forming,
					BoutId = bout.Id
				});
				bout.TransportInfoFromId = bout.TransportInfoFrom.Id;
			}

			if (!bout.TransportInfoToId.HasValue && bout.TransportInfoTo == null)
			{
				bout.TransportInfoTo = unitOfWork.AddEntity(new TransportInfo
				{
					DateOfDeparture = bout.DateIncome ?? DateTime.Now,
					ArrivalId = bout.Hotels.CityId,
					DepartureId = (long)CityEnum.Moscow,
					YearOfRestId = bout.YearOfRestId,
					StateId = StateMachineStateEnum.Transport.Forming,
					BoutId = bout.Id
				});

				bout.TransportInfoToId = bout.TransportInfoTo.Id;
			}
		}


		/// <summary>
		/// сброс статуса в редактирование
		/// </summary>
		/// <param name="unitOfWork"></param>
		/// <param name="boatId"></param>
		private void BoatToEditState(UnitOfWork unitOfWork, long? boatId)
		{
			if (!boatId.HasValue)
			{
				return;
			}

			var b = unitOfWork.GetById<Bout>(boatId.Value);
			if (b.StateId != StateMachineStateEnum.Bout.Editing)
			{
				b.StateId = StateMachineStateEnum.Bout.Editing;
				b.IncludedInTransport = false;
				unitOfWork.SaveChanges();
			}
		}

		/// <summary>
		///     связь детей.
		/// </summary>
		private void LinkChildToBoat()
		{
			using (var unitOfWork = new UnitOfWork())
			{
				Logger.Info("Цепляем детей к заездам started");
				var childSet =
					unitOfWork.GetSet<Child>()
							.Where(c => !c.IsDeleted && c.Payed)
							.Where(
								c =>
								!c.BoutId.HasValue || (c.Request != null && c.Request.Tour != null && c.BoutId != c.Request.Tour.BoutId)
								|| (c.ChildList != null && c.ChildList.Tour != null && c.BoutId != c.ChildList.Tour.BoutId));

				var childArr1 = childSet
					.Where(c => c.Request.Tour.BoutId.HasValue && c.Request.StatusId == (long)StatusEnum.CertificateIssued && c.Request.IsLast && !c.Request.IsDeleted)
					.Where(c => !c.BoutId.HasValue || c.BoutId != c.Request.Tour.BoutId)
					.Take(3000).ToArray();
				var childArr2 =
					childSet.Where(
						c =>
						c.ChildList.Tour.BoutId.HasValue && !c.ChildList.IsDeleted && c.ChildList.IsLast
						&& (c.ChildList.StateId == StateMachineStateEnum.Limit.List.Formed
							|| c.ChildList.StateId == StateMachineStateEnum.Limit.List.IncludedPayment)
						&& c.ChildList.LimitOnOrganization.StateId == StateMachineStateEnum.Limit.Organization.Confirmed)
						.Where(c => !c.BoutId.HasValue || c.BoutId != c.ChildList.Tour.BoutId)
						.Take(3000).ToArray();

				// проход по заездам и добавление в них списков
				SetBoutInChildren(childArr1, unitOfWork);
				SetBoutInChildren(childArr2, unitOfWork);

				var childsToExclude =
					unitOfWork.GetSet<Child>()
							.Where(
								c =>
								c.BoutId.HasValue
								&& (c.IsDeleted
									|| (c.Request != null &&
											(
												c.Request.StatusId == (long)StatusEnum.Reject || !c.Request.IsLast || c.Request.IsDeleted
												|| (c.Request.TypeOfRest.Commercial && !c.Payed)
											)
										)
									|| (c.ChildList != null
										&& (c.ChildList.IsDeleted
											|| (c.ChildList.StateId != StateMachineStateEnum.Limit.List.Formed && c.ChildList.StateId != StateMachineStateEnum.Limit.List.IncludedPayment)
											|| c.ChildList.LimitOnOrganization.StateId != StateMachineStateEnum.Limit.Organization.Confirmed
											|| !c.Payed))))
							.Take(3000).ToArray();

				RemoveBoutFromChild(unitOfWork, childsToExclude);

				var linksToRemove = unitOfWork.GetSet<LinkToPeople>().Where(l => l.Child != null && !l.Child.BoutId.HasValue).Take(600).ToList();

				foreach (var link in linksToRemove)
				{
					unitOfWork.Delete(link);
				}


				unitOfWork.SaveChanges();
				Logger.Info("Цепляем детей к заездам finished");
			}
		}

		private void SetBoutInChildren(IEnumerable<Child> childSet, UnitOfWork unitOfWork)
		{
			foreach (var child in childSet)
			{
				if (child.Request?.Tour != null)
				{
					child.BoutId = child.Request.Tour.BoutId;
					child.EkisNeedSend = true;
					unitOfWork.Context.Entry(child).State = EntityState.Modified;
					BoatToEditState(unitOfWork, child.BoutId);

					var tFromId = child.Request?.Tour?.Bout?.TransportInfoFromId;
					var tToId = child.Request?.Tour?.Bout?.TransportInfoToId;

					AppendLinkToChild(unitOfWork, child, tFromId, tToId, CheckNeedReason(child, null));
				}

				if (child.ChildList?.Tour != null)
				{
					child.BoutId = child.ChildList.Tour.BoutId;
					child.EkisNeedSend = true;
					unitOfWork.Context.Entry(child).State = EntityState.Modified;
					BoatToEditState(unitOfWork, child.BoutId);

					var tFromId = child.ChildList?.Tour?.Bout?.TransportInfoFromId;
					var tToId = child.ChildList?.Tour?.Bout?.TransportInfoToId;

					AppendLinkToChild(unitOfWork, child, tFromId, tToId, CheckNeedReason(child, null));
				}
			}
		}

		private static void ClearChildLinkToTransport(UnitOfWork unitOfWork, Child child)
		{
			var links = unitOfWork.GetSet<LinkToPeople>().Where(l => l.ChildId == child.Id).ToList();
			foreach (var link in links)
			{
				unitOfWork.Delete(link);
			}
		}

		private static void AppendLinkToChild(UnitOfWork unitOfWork, Child child, long? tFromId, long? tToId, bool setComeSingly)
		{
			if (!unitOfWork.GetSet<LinkToPeople>().Any(l => l.TransportId == tFromId && l.ChildId == child.Id))
			{
				unitOfWork.AddEntity(new LinkToPeople
				{
					ChildId = child.Id,
					BoutId = child.BoutId,
					RequestId = child.RequestId,
					ListOfChildsId = child.ChildListId,
					TransportId = tFromId,
					TypeOfLinkPeopleId = (long)TypeOfLinkPeopleEnum.Child,
					NotComeInPlaceOfRest = true,
					// Если заявка коммерч. и нет услуг трансп. с этим реб.
					NotNeedTicketReasonId = setComeSingly ? (long)NotNeedTicketReasonEnum.ComeSingly : (long)NotNeedTicketReasonEnum.NotCome,
					PartyId = child.PartyId
				});
			}

			if (!unitOfWork.GetSet<LinkToPeople>().Any(l => l.TransportId == tToId && l.ChildId == child.Id))
			{
				unitOfWork.AddEntity(new LinkToPeople
				{
					ChildId = child.Id,
					BoutId = child.BoutId,
					RequestId = child.RequestId,
					ListOfChildsId = child.ChildListId,
					TransportId = tToId,
					TypeOfLinkPeopleId = (long)TypeOfLinkPeopleEnum.Child,
					PartyId = child.PartyId,
					NotComeInPlaceOfRest = true,
					// Если заявка коммерч. и нет услуг трансп. с этим реб.
					NotNeedTicketReasonId = setComeSingly ? (long?)NotNeedTicketReasonEnum.ComeSingly : null
				});
			}
		}

		private static void AppendLinkToApplicant(UnitOfWork unitOfWork, Applicant applicant, long? requestId, long? tFromId, long? tToId, bool setComeSingly)
		{
			if (!unitOfWork.GetSet<LinkToPeople>().Any(l => l.TransportId == tFromId && l.ApplicantId == applicant.Id))
			{
				unitOfWork.AddEntity(new LinkToPeople
				{
					ApplicantId = applicant.Id,
					BoutId = applicant.BoutId,
					RequestId = requestId,
					ListOfChildsId = applicant.ChildListId,
					TransportId = tFromId,
					TypeOfLinkPeopleId = (long)TypeOfLinkPeopleEnum.Attendant,
					NotComeInPlaceOfRest = true,
					// Если заявка коммерч. и нет услуг трансп. с этим чел.
					NotNeedTicketReasonId = setComeSingly ? (long)NotNeedTicketReasonEnum.ComeSingly : (long)NotNeedTicketReasonEnum.NotCome,
				});
			}

			if (!unitOfWork.GetSet<LinkToPeople>().Any(l => l.TransportId == tToId && l.ApplicantId == applicant.Id))
			{
				unitOfWork.AddEntity(new LinkToPeople
				{
					ApplicantId = applicant.Id,
					BoutId = applicant.BoutId,
					RequestId = requestId,
					ListOfChildsId = applicant.ChildListId,
					TransportId = tToId,
					TypeOfLinkPeopleId = (long)TypeOfLinkPeopleEnum.Attendant,
					NotComeInPlaceOfRest = true,
					// Если заявка коммерч. и нет услуг трансп. с этим чел.
					NotNeedTicketReasonId = setComeSingly ? (long?)NotNeedTicketReasonEnum.ComeSingly : null
				});
			}
		}

		/// <summary>
		///     связь детей.
		/// </summary>
		private void LinkApplicantToBoat()
		{
			using (var unitOfWork = new UnitOfWork())
			{
				Logger.Info("Цепляем сопровождающих к заездам started");
				var requests =
					unitOfWork.GetSet<Request>().Where(
						c =>
							c.Tour.BoutId.HasValue && c.StatusId == (long)StatusEnum.CertificateIssued
							&& !c.IsDeleted &&
							c.Tour != null && c.Applicant != null && c.Applicant.IsAccomp && c.Applicant.BoutId != c.Tour.BoutId).Take(1000).ToList();

				var exTypeOfRest = new[] { (long?)TypeOfRestEnum.ChildRestCamps, (long)TypeOfRestEnum.ChildRestFederalCamps, (long)TypeOfRestEnum.ChildRest };

				var applicants =
					unitOfWork.GetSet<Applicant>()
						.Where(c => c.Payed)
						.Where(c => c.ChildListId.HasValue || !c.Request.Tour.TypeOfRest.Commercial || c.IsAccomp)
						.Where(
							c =>
								(!c.BoutId.HasValue && (!exTypeOfRest.Contains(c.Request.TypeOfRestId) || c.ChildList != null)) ||
								(c.Request != null && c.Request.Tour != null && c.BoutId != c.Request.Tour.BoutId && !exTypeOfRest.Contains(c.Request.TypeOfRestId)) ||
								(c.Request != null && exTypeOfRest.Contains(c.Request.TypeOfRestId) && c.BoutId.HasValue) ||
								(c.ChildList != null && c.ChildList.Tour != null && c.BoutId != c.ChildList.Tour.BoutId))
						.Where(
							c =>

								(c.Request.Tour.BoutId.HasValue && c.Request.StatusId == (long)StatusEnum.CertificateIssued &&
								c.Request.IsLast
								&& !c.Request.IsDeleted)
								|| (c.ChildList.Tour.BoutId.HasValue && !c.ChildList.IsDeleted && c.ChildList.IsLast
									&& c.ChildList.StateId == StateMachineStateEnum.Limit.List.Formed &&
									c.ChildList.LimitOnOrganization.StateId == StateMachineStateEnum.Limit.Organization.Confirmed))
						.ToList();
				// проход по заездам и добавление в них списков
				foreach (var applicant in applicants.ToArray())
				{
					if (applicant.Request?.Tour != null
						&& !exTypeOfRest.Contains(applicant.Request?.Tour.TypeOfRestId))
					{
						applicant.BoutId = applicant.Request.Tour.BoutId;
						unitOfWork.Context.Entry(applicant).State = EntityState.Modified;
						BoatToEditState(unitOfWork, applicant.BoutId);

						var tFromId = applicant.Request?.Tour?.Bout?.TransportInfoFromId;
						var tToId = applicant.Request?.Tour?.Bout?.TransportInfoToId;
						AppendLinkToApplicant(unitOfWork, applicant, applicant.RequestId, tFromId, tToId, CheckNeedReason(null, applicant));
					}
					else if (applicant.ChildList?.Tour != null)
					{
						applicant.BoutId = applicant.ChildList.Tour.BoutId;
						unitOfWork.Context.Entry(applicant).State = EntityState.Modified;
						BoatToEditState(unitOfWork, applicant.BoutId);

						var tFromId = applicant.ChildList?.Tour?.Bout?.TransportInfoFromId;
						var tToId = applicant.ChildList?.Tour?.Bout?.TransportInfoToId;
						AppendLinkToApplicant(unitOfWork, applicant, null, tFromId, tToId, CheckNeedReason(null, applicant));
					}
					else if (exTypeOfRest.Contains(applicant?.Request?.TypeOfRestId))
					{
						applicant.BoutId = null;
						applicant.IsAccomp = false;

						unitOfWork.Context.Entry(applicant).Property(m => m.BoutId).IsModified = true;
						unitOfWork.Context.Entry(applicant).Property(m => m.IsAccomp).IsModified = true;
					}
				}

				// проход по заездам и добавление в них списков
				foreach (var request in requests)
				{
					request.Applicant.BoutId = request.Tour.BoutId;
					unitOfWork.Context.Entry(request.Applicant).State = EntityState.Modified;
					BoatToEditState(unitOfWork, request.Applicant.BoutId);

					var tFromId = request.Tour?.Bout?.TransportInfoFromId;
					var tToId = request.Tour?.Bout?.TransportInfoToId;

					AppendLinkToApplicant(unitOfWork, request.Applicant, request.Id, tFromId, tToId, CheckNeedReason(null, request.Applicant));
				}

				unitOfWork.SaveChanges();

				var attendantToExclude =
					unitOfWork.GetSet<Applicant>()
							.Where(
								c =>
								c.BoutId.HasValue
								&& ((c.Request != null && (
																c.Request.StatusId == (long)StatusEnum.Reject || !c.Request.IsLast || c.Request.IsDeleted
																|| (c.Request.TypeOfRest.Commercial && !c.Payed)
															))
									|| (c.ChildList != null
										&& (c.ChildList.IsDeleted || c.ChildList.StateId != StateMachineStateEnum.Limit.List.Formed
											|| c.ChildList.LimitOnOrganization.StateId != StateMachineStateEnum.Limit.Organization.Confirmed || !c.Payed))))
							.Take(5000)
							.ToList();

				foreach (var attendant in attendantToExclude)
				{
					attendant.BoutId = null;
					attendant.Bout = null;
					unitOfWork.Context.Entry(attendant).State = EntityState.Modified;

					ClearAttendantLinkToTransport(unitOfWork, attendant);
				}

				var attendantToExcludeAddon =
					unitOfWork.GetSet<Request>().Where(
						r =>
							r.Applicant.BoutId.HasValue &&
							(!r.Applicant.IsAccomp || r.StatusId != (long)StatusEnum.CertificateIssued || !r.IsLast || r.IsDeleted)).Select(r => r.Applicant)
						.Take(1000)
						.ToList();

				foreach (var attendant in attendantToExcludeAddon)
				{
					attendant.BoutId = null;
					attendant.Bout = null;
					unitOfWork.Context.Entry(attendant).State = EntityState.Modified;

					var links = unitOfWork.GetSet<LinkToPeople>().Where(l => l.ApplicantId == attendant.Id).ToList();
					foreach (var link in links)
					{
						unitOfWork.Delete(link);
					}
				}

				var linksToRemove = unitOfWork.GetSet<LinkToPeople>().Where(l => l.Applicant != null && !l.Applicant.BoutId.HasValue).Take(600).ToList();

				foreach (var link in linksToRemove)
				{
					unitOfWork.Delete(link);
				}

				unitOfWork.SaveChanges();
				Logger.Info("Цепляем сопровождающих к заездам finished");
			}
		}

		private static void ClearAttendantLinkToTransport(UnitOfWork unitOfWork, Applicant attendant)
		{
			var links = unitOfWork.GetSet<LinkToPeople>().Where(l => l.ApplicantId == attendant.Id).ToList();
			foreach (var link in links)
			{
				unitOfWork.Delete(link);
			}
		}

		/// <summary>
		/// Определение необходимости указания причины отказа от билета "добирается самостоятельно".
		/// Истина, если заявка коммерч. и есть транспортные доп. услуги с этим ребенком.
		/// </summary>
		private static bool CheckNeedReason(Child child, Applicant applicant)
		{
			var request = child?.Request ?? applicant?.Request;

			// Если заявка коммерческая
			if (request?.TypeOfRest?.Commercial ?? false)
			{
				// Если есть доп. услуги
				if (request.AddonServicesLinks != null)
				{
					var types = new List<ServiceEnum>
					{
						ServiceEnum.TransferAero, ServiceEnum.TransferAuto, ServiceEnum.TransferTrain,
						ServiceEnum.TransportFrom, ServiceEnum.TransportTo // Последние два неактивны, но для истории оставим
					}.Select(t => (long?)t).ToList();

					// Среди доп. услуг есть с типом "транспорт" для этого ребенка/взрослого
					var query = request.AddonServicesLinks
						.Where(s => s.AddonServices != null && types.Contains(s.AddonServices.TypeOfServiceId));
					query = child != null ? query.Where(s => s.ChildId == child.Id) : query.Where(s => s.ApplicantId == applicant.Id);

					// Значит ему нужен билет, причину отказа не указываем (оставляем как раньше)
					return !query.Any();
					// В противном случае он "Добирается самостоятельно" (проставляем такую причину отказа от билета)
				}
			}

			// Для некоммерческих причину не указываем (оставляем как раньше)
			return false;
		}
	}
}
