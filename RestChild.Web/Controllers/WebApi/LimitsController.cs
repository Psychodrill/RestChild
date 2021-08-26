using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using RestChild.Booking.Logic.Extensions;
using RestChild.Comon;
using RestChild.Comon.Enumeration;
using RestChild.DAL.RepositoryExtensions;
using RestChild.Domain;
using RestChild.Web.Common;
using RestChild.Web.Extensions;
using RestChild.Web.Models;
using RestChild.Web.Models.Limits;
using Settings = RestChild.Web.Properties.Settings;

namespace RestChild.Web.Controllers.WebApi
{
	[Authorize]
	public class LimitsController : BaseController
	{
		public StateController StateController { get; set; }

		public WebHistoryController HistoryController { get; set; }

		public PdfController Pdf { get; set; }

		public WebCalculationController ApiCalculationController { get; set; }

		public CertificateController ApiCertificateController { get; set; }

		public override void SetUnitOfWorkInRefClass(IUnitOfWork unitOfWork)
		{
			base.SetUnitOfWorkInRefClass(unitOfWork);
			StateController.SetUnitOfWorkInRefClass(unitOfWork);
			HistoryController.SetUnitOfWorkInRefClass(unitOfWork);
			Pdf.SetUnitOfWorkInRefClass(unitOfWork);
			ApiCalculationController.SetUnitOfWorkInRefClass(unitOfWork);
			ApiCertificateController.SetUnitOfWorkInRefClass(unitOfWork);
		}

		/// <summary>
		///     получение организаций по правам
		/// </summary>
		internal IEnumerable<LimitOnVedomstvo> GetVedForRight(string query, long yearId)
		{
			var q = UnitOfWork.GetSet<LimitOnVedomstvo>()
				.Where(x => x.StateId != StateMachineStateEnum.Deleted && x.YearOfRestId == yearId);

			if (!string.IsNullOrEmpty(query))
			{
				q = q.Where(x =>
					x.Organization.Name.ToLower().Contains(query.ToLower()) ||
					x.Organization.ShortName.ToLower().Contains(query.ToLower()));
			}

			var secsTotal = Security.GetSecurity();

			var secs = secsTotal.Where(s => s.StartsWith(AccessRightEnum.Limits.LimitByOrganization)).ToList();

			if (!secs.Contains(AccessRightEnum.Limits.LimitByOrganization) &&
			    !secsTotal.Contains(AccessRightEnum.Limits.LimitToOiv))
			{
				var orgsId = secs.Select(
					s =>
						s.Replace(string.Format(Security.SecurityOrgTemplate, AccessRightEnum.Limits.LimitByOrganization, string.Empty),
							string.Empty).LongParse()).Where(l => l.HasValue).ToArray();

				q = q.Where(x => orgsId.Contains(x.Organization.EntityId ?? x.OrganizationId) || orgsId.Contains(x.OrganizationId));
			}

			return q.OrderBy(x => x.Organization.Name).ToList();
		}

		/// <summary>
		///     получение организаций по правам
		/// </summary>
		[HttpPost, HttpGet]
		public IList<Organization> GetOrganizationList(string query)
		{
			var general = Security.GetSecurity().ToList();

			var secsLimitChildInOrganization =
				general.Where(s => s.StartsWith(AccessRightEnum.Limits.LimitChildInOrganization)).ToList();
			var secsLimitByOrganization = general.Where(s => s.StartsWith(AccessRightEnum.Limits.LimitByOrganization)).ToList();

			var q = UnitOfWork.GetSet<Organization>().Where(x => !x.IsDeleted && x.IsLast);

			if (!string.IsNullOrEmpty(query))
			{
				var substrs = query.Split(' ').Where(s => !string.IsNullOrEmpty(s)).Select(s => s.ToLower()).ToList();
				foreach (var str in substrs)
				{
					q = q.Where(x => x.Name.ToLower().Contains(str) || x.Inn == str);
				}
			}

			if (!secsLimitByOrganization.Contains(AccessRightEnum.Limits.LimitChildInOrganization) &&
			    !general.Contains(AccessRightEnum.Limits.LimitToOiv) && secsLimitByOrganization.Any())
			{
				var orgsId = secsLimitByOrganization.Select(
					s =>
						s.Replace(
							string.Format(Security.SecurityOrgTemplate, AccessRightEnum.Limits.LimitByOrganization, string.Empty),
							string.Empty).LongParse()).Where(l => l.HasValue).ToArray();

				q = q.Where(x => orgsId.Contains(x.Id));
			}
			else if (!secsLimitChildInOrganization.Contains(AccessRightEnum.Limits.LimitChildInOrganization) &&
			         !general.Contains(AccessRightEnum.Limits.LimitToOiv))
			{
				var orgsId = secsLimitChildInOrganization.Select(
					s =>
						s.Replace(
							string.Format(Security.SecurityOrgTemplate, AccessRightEnum.Limits.LimitChildInOrganization, string.Empty),
							string.Empty).LongParse()).Where(l => l.HasValue).ToArray();

				q = q.Where(x => orgsId.Contains(x.ParentId) || orgsId.Contains(x.Id));
			}

			var res = q.OrderBy(x => x.Name.Length).ThenBy(x => x.Name)
				.Take(Settings.Default.WebBtiStreetsResponseCount)
				.ToList().Select(l => new Organization(l)).ToList();

			return res;
		}

		/// <summary>
		///     получение организаций по правам
		/// </summary>
		[HttpPost, HttpGet]
		public IList<LimitItemModel> GetLimitForChildList(string query, long? yearOfRestId, long? organizationId = null)
		{
			var q = UnitOfWork.GetSet<LimitOnOrganization>()
				.Where(
					x =>
						x.StateId.HasValue &&
						(x.StateId != StateMachineStateEnum.Limit.Organization.Formation && x.StateId != StateMachineStateEnum.Deleted));

			if (!string.IsNullOrEmpty(query))
			{
				q = q.Where(x => x.Organization.Name.ToLower().Contains(query.ToLower()));
			}

			if (yearOfRestId.HasValue)
			{
				q = q.Where(x => x.LimitOnVedomstvo.YearOfRestId == yearOfRestId);
			}

			if (organizationId.HasValue)
			{
				q = q.Where(x => x.OrganizationId == organizationId);
			}

			var general = Security.GetSecurity().ToList();

			var secsLimitChildInOrganization =
				general.Where(s => s.StartsWith(AccessRightEnum.Limits.LimitChildInOrganization)).ToList();
			var secsLimitByOrganization = general.Where(s => s.StartsWith(AccessRightEnum.Limits.LimitByOrganization)).ToList();

			if (!secsLimitByOrganization.Contains(AccessRightEnum.Limits.LimitChildInOrganization) &&
			    !general.Contains(AccessRightEnum.Limits.LimitToOiv) && secsLimitByOrganization.Any())
			{
				var orgsId = secsLimitByOrganization.Select(
					s =>
						s.Replace(
							string.Format(Security.SecurityOrgTemplate, AccessRightEnum.Limits.LimitByOrganization, string.Empty),
							string.Empty).LongParse()).Where(l => l.HasValue).ToArray();

				q = q.Where(x => orgsId.Contains(x.LimitOnVedomstvo.OrganizationId));
			}
			else if (!secsLimitChildInOrganization.Contains(AccessRightEnum.Limits.LimitChildInOrganization) &&
			         !general.Contains(AccessRightEnum.Limits.LimitToOiv))
			{
				var orgsId = secsLimitChildInOrganization.Select(
					s =>
						s.Replace(
							string.Format(Security.SecurityOrgTemplate, AccessRightEnum.Limits.LimitChildInOrganization, string.Empty),
							string.Empty).LongParse()).Where(l => l.HasValue).ToArray();

				q = q.Where(x => orgsId.Contains(x.OrganizationId));
			}

			q =
				q.Include(i => i.LimitOnVedomstvo)
					.Include(i => i.LimitOnVedomstvo.YearOfRest)
					.Include(i => i.LimitOnVedomstvo.Organization)
					.Include(i => i.Organization)
					.Include(i => i.Tour)
					.Include(i => i.Tour.TimeOfRest)
					.Include(i => i.Tour.Hotels)
					.Include(i => i.Tour.Hotels.PlaceOfRest)
					.Include(i => i.State);


			var res = q.OrderBy(x => x.Organization.Name)
				.Take(Settings.Default.WebBtiStreetsResponseCount)
				.ToList().Select(l => GetItemModelOnLimitOnOrganization(l, new Dictionary<long?, List<StateMachineAction>>()))
				.ToList();

			return res;
		}

		/// <summary>
		///     получить список годов отдыха.
		/// </summary>
		[HttpPost, HttpGet]
		public List<YearOfRest> GetYearsOfRest()
		{
			return UnitOfWork.GetSet<YearOfRest>().OrderBy(y => y.Year).ToList();
		}

		/// <summary>
		///     получить список квот по ОИВ для года
		/// </summary>
		[HttpPost, HttpGet]
		public List<LimitItemModel> GetLimitsOnYear(long? yearId)
		{
			SetUnitOfWorkInRefClass(UnitOfWork);
			if (!yearId.HasValue)
			{
				return new List<LimitItemModel>();
			}

			var tlids = GetAccesedTypeLimitList();

			var limitOnRequest =
				UnitOfWork.GetSet<LimitOnVedomstvo>()
					.Where(l => l.YearOfRestId == yearId && l.StateId.HasValue && l.StateId != StateMachineStateEnum.Deleted &&
					            tlids.Contains(l.TypeOfLimitListId ?? (long) TypeOfLimitListEnum.Profile))
					.SelectMany(
						l => l.LimitOnOrganizationRequests.Where(r => r.StateId == StateMachineStateEnum.LimitRequest.Approved))
					.GroupBy(g => g.LimitOnVedomstvoId)
					.Select(g => new {limitId = g.Key, volume = g.Sum(v => v.Volume)})
					.ToDictionary(d => d.limitId, d => d.volume);

			return
				UnitOfWork.GetSet<LimitOnVedomstvo>()
					.Where(
						l => l.YearOfRestId == yearId && l.StateId.HasValue && l.StateId != StateMachineStateEnum.Deleted &&
						     tlids.Contains(l.TypeOfLimitListId ?? (long) TypeOfLimitListEnum.Profile))
					.Where(l => l.Organization != null).Include(l => l.State).Include(l => l.Organization).ToList()
					.Select(l => new LimitItemModel
					{
						Id = l.Id,
						TypeOfListId = l.TypeOfLimitListId,
						TypeOfList = l.TypeOfLimitList?.Name ?? "-",
						OrganizationName = l.Organization.Name,
						OrganizationId = l.OrganizationId,
						RequestVolume = limitOnRequest.ContainsKey(l.Id) ? limitOnRequest[l.Id] : 0,
						Volume = l.Volume,
						State = new StateMachineState(l.State),
						Actions = StateController.GetActions(l.State, StateMachineEnum.LimitOivState)
							.Where(
								a =>
									new[]
									{
										AccessRightEnum.Limit.Oiv.Formation,
										AccessRightEnum.Limit.Oiv.Brought,
										AccessRightEnum.Limit.Oiv.GatheringRequirements
									}.Contains(a.ActionCode))
							.ToList()
					})
					.OrderBy(l => l.OrganizationName)
					.ToList();
		}

		public static List<long> GetAccesedTypeLimitList()
		{
			var tlids = new List<long> {(long) TypeOfLimitListEnum.Profile, (long) TypeOfLimitListEnum.Orphan};
			if (!Security.HasRight(AccessRightEnum.Limit.ViewOrphan))
			{
				tlids.Remove((long) TypeOfLimitListEnum.Orphan);
			}

			if (!Security.HasRight(AccessRightEnum.Limit.ViewProfile))
			{
				tlids.Remove((long) TypeOfLimitListEnum.Profile);
			}

			return tlids;
		}

		internal LimitOnVedomstvo GetLimitOnVedomstvo(long id)
		{
			return
				UnitOfWork.GetById<LimitOnVedomstvo>(id);
		}

		/// <summary>
		///     получить лимит на ОИВ
		/// </summary>
		internal LimitOnVedomstvo GetLimitOnVedomstvo(long yearId, long oivId)
		{
			return
				UnitOfWork.GetSet<LimitOnVedomstvo>()
					.FirstOrDefault(
						l => l.OrganizationId == oivId && l.YearOfRestId == yearId && l.StateId != StateMachineStateEnum.Deleted);
		}

		private LimitItemModel GetItemModelOnLimitOnOrganization(LimitOnOrganization item,
			Dictionary<long?, List<StateMachineAction>> states = null)
		{
			if (states == null)
			{
				states = new List<LimitOnOrganization> {item}.Where(s => s.StateId.HasValue)
					.GroupBy(s => s.StateId)
					.ToDictionary(s => s.Key, l => StateController.GetActions(l.First().State, StateMachineEnum.LimitOrganizationState)
						.Where(
							a =>
								(new[]
								 {
									 AccessRightEnum.Limit.Organization.Formation, AccessRightEnum.Limit.Organization.OnCompletion,
									 AccessRightEnum.Limit.Organization.Brought,
									 AccessRightEnum.Limit.Organization.Approved
								 }.Contains(a.ActionCode) &&
								 Security.HasRight(AccessRightEnum.Limits.LimitByOrganization, item.LimitOnVedomstvo.OrganizationId ?? 0)) ||
								(new[]
								{
									AccessRightEnum.Limit.Organization.ApprovedToCompetiotion, AccessRightEnum.Limit.Organization.Confirmed
								}.Contains(a.ActionCode)))
						.ToList());
			}

			return new LimitItemModel
			{
				Id = item.Id,
				Volume = item.Volume,
				OrganizationName = item.Organization?.Name ?? string.Empty,
				VedomstvoName = item.LimitOnVedomstvo?.Organization?.Name ?? string.Empty,
				State = new StateMachineState(item.State),
				OrganizationId = item.OrganizationId,
				YearOfRestId = item.LimitOnVedomstvo?.YearOfRestId,
				TourId = item.TourId,
				Tour = item.Tour,
				TourName = item.Tour.NullSafe(t => t.ToString()) ?? string.Empty,
				PlaceOfRest = item.PlaceOfRest,
				LimitOnOrganization = item,
				PlaceOfRestName = item.PlaceOfRest.NullSafe(p => p.Name) ?? string.Empty,
				PlaceOfRestId = item.PlaceOfRestId,
				TimeOfRestName = item.TimeOfRest.NullSafe(t => t.Name) ?? string.Empty,
				TimeOfRestId = item.TimeOfRestId,
				TimeOfRest = item.TimeOfRest,
				Actions =
					item.StateId.HasValue && states.ContainsKey(item.StateId) ? states[item.StateId] : new List<StateMachineAction>()
			};
		}

		/// <summary>
		/// получение сгруппированого результата.
		/// </summary>
		internal GroupedLimitItemModel GetTourOrganizationList(long yearId, long organizationId)
		{
			var res = new GroupedLimitItemModel();

			var q = UnitOfWork.GetSet<LimitOnOrganization>()
				.Where(
					l =>
						l.LimitOnVedomstvo.YearOfRestId == yearId &&
						l.StateId.HasValue && l.StateId != StateMachineStateEnum.Deleted && l.OrganizationId == organizationId);

			var subItems = q.Where(l => l.Organization != null).ToList();

			var ids = subItems.Select(s => s.Id).ToList();

			var listsCount =
				UnitOfWork.GetSet<ListOfChilds>()
					.Where(
						l =>
							l.LimitOnOrganizationId.HasValue && ids.Contains(l.LimitOnOrganizationId.Value) &&
							l.StateId != StateMachineStateEnum.Deleted && l.StateId.HasValue && l.IsLast && !l.IsDeleted &&
							l.StateId != StateMachineStateEnum.Limit.List.Formation)
					.GroupBy(l => l.LimitOnOrganizationId)
					.ToDictionary(l => l.Key ?? 0, v => v.Select(l => l.CountChild).Sum());

			var states = new Dictionary<long?, List<StateMachineAction>>();

			var items = subItems.Select(
					l =>
						GetItemModelOnLimitOnOrganization(l, states))
				.OrderBy(l => l.OrganizationName)
				.ToList();

			res.Items = items.Where(i => !i.TimeOfRestId.HasValue && !i.TourId.HasValue).ToList();

			res.SubGroups =
				items.Where(i => i.TimeOfRestId.HasValue).GroupBy(i => i.TimeOfRestId).Select(i => new GroupedLimitItemModel
				{
					TimeOfRestId = i.Key,
					Name = i.First().TimeOfRest.Name,
					Items = i.Where(j => !j.PlaceOfRestId.HasValue).ToList(),
					SubGroups =
						i.Where(k => k.PlaceOfRestId.HasValue).GroupBy(k => k.PlaceOfRestId).Select(k => new GroupedLimitItemModel
						{
							TimeOfRestId = i.Key,
							PlaceOfRestId = k.Key,
							Name = k.First().PlaceOfRestName,
							Items = k.Where(j => !j.TourId.HasValue).ToList(),
							SubGroups =
								k.Where(j => j.TourId.HasValue).GroupBy(j => j.TourId).Select(j => new GroupedLimitItemModel
								{
									TimeOfRestId = i.Key,
									PlaceOfRestId = k.Key,
									TourId = j.Key,
									OivId = j.First().Tour.LimitOnVedomstvoId,
									OivOrgId = j.First().Tour.LimitOnVedomstvo.OrganizationId,
									Name = j.First().Tour.ToString() + " " + j.First().Tour.LimitOnVedomstvo?.TypeOfLimitList?.Name,
									Volume = j.First().Tour.Volumes.FirstOrDefault()?.CountPlace ?? 0,
									FormedVolume = listsCount.ContainsKey(j.First().Id) ? listsCount[j.First().Id] : 0,
									Items = j.ToList(),
									SubGroups = new List<GroupedLimitItemModel>()
								}).ToList()
						}).ToList()
				}).ToList();

			return res;
		}


		/// <summary>
		/// получение сгруппированого результата.
		/// </summary>
		[HttpPost, HttpGet]
		public GroupedLimitItemModel GetOrganizationList(long yearId, long? oivId)
		{
			if (oivId == 0)
			{
				oivId = null;
			}

			var limit = UnitOfWork.GetById<LimitOnVedomstvo>(oivId);

			if (!Security.HasRight(AccessRightEnum.Limits.LimitByOrganization, limit?.OrganizationId ?? 0) &&
			    !Security.HasRight(AccessRightEnum.Limits.LimitToOiv))
			{
				return new GroupedLimitItemModel();
			}


			var res = new GroupedLimitItemModel();

			var tids = GetAccesedTypeLimitList();

			var q = UnitOfWork.GetSet<LimitOnOrganization>()
				.Where(
					l =>
						l.LimitOnVedomstvo.YearOfRestId == yearId &&
						l.StateId.HasValue && l.StateId != StateMachineStateEnum.Deleted &&
						tids.Contains(l.TypeOfLimitListId ?? (long) TypeOfLimitListEnum.Profile));

			if (oivId.HasValue || !Security.HasRight(AccessRightEnum.Limits.LimitToOiv))
			{
				q = q.Where(l => l.LimitOnVedomstvoId == oivId);
			}

			if (!oivId.HasValue && Security.HasRight(AccessRightEnum.Limits.LimitToOiv))
			{
				q = q.Where(l => l.StateId == StateMachineStateEnum.Limit.Organization.Approved);
			}

			var tours = new List<Tour>();

			if (oivId.HasValue)
			{
				tours =
					UnitOfWork.GetSet<Tour>()
						.Where(
							t =>
								t.LimitOnVedomstvoId == oivId && t.LimitOnVedomstvo.YearOfRestId == yearId && t.StateId.HasValue &&
								t.StateId != StateMachineStateEnum.Deleted && t.StateId != StateMachineStateEnum.Tour.Formation)
						.ToList();
			}

			var subItems = q.Where(l => l.Organization != null).ToList();

			var states = subItems.Where(s => s.StateId.HasValue)
				.GroupBy(s => s.StateId)
				.ToDictionary(s => s.Key, l => StateController.GetActions(l.First().State, StateMachineEnum.LimitOrganizationState)
					.Where(
						a =>
							(new[]
							{
								AccessRightEnum.Limit.Organization.Formation, AccessRightEnum.Limit.Organization.OnCompletion,
								AccessRightEnum.Limit.Organization.Brought,
								AccessRightEnum.Limit.Organization.Approved
							}.Contains(a.ActionCode) && Security.HasRight(AccessRightEnum.Limits.LimitByOrganization, oivId ?? 0)) || (new[]
							{
								AccessRightEnum.Limit.Organization.ApprovedToCompetiotion, AccessRightEnum.Limit.Organization.Confirmed
							}.Contains(a.ActionCode)))
					.ToList());

			var items = subItems.Select(
					l =>
						GetItemModelOnLimitOnOrganization(l, states))
				.OrderBy(l => l.OrganizationName)
				.ToList();

			res.Items = items.Where(i => !i.TimeOfRestId.HasValue && !i.TourId.HasValue).ToList();

			res.SubGroups =
				items.Where(i => i.TimeOfRestId.HasValue).GroupBy(i => i.TimeOfRestId).Select(i => new GroupedLimitItemModel
				{
					TimeOfRestId = i.Key,
					Name = i.First().TimeOfRest.Name,
					Items = i.Where(j => !j.PlaceOfRestId.HasValue).ToList(),
					SubGroups =
						i.Where(j => j.TourId.HasValue).GroupBy(j => j.TourId).Select(j => new GroupedLimitItemModel
						{
							TimeOfRestId = i.Key,
							PlaceOfRestId = j.Max(p => p.PlaceOfRestId),
							OivId = j.First().Tour.LimitOnVedomstvoId,
							OivOrgId = j.First().Tour.LimitOnVedomstvo.OrganizationId,
							TourId = j.Key,
							Name =
								$"{j.First().Tour} <br/><small><i>{j.First().Tour.NullSafe(tr => tr.Hotels.PlaceOfRest.Name)}</i></small>" +
								" " + j.First().Tour.LimitOnVedomstvo?.TypeOfLimitList?.Name,
							Volume = j.First().Tour.Volumes.FirstOrDefault().NullSafe(v => v.CountPlace) ?? 0,
							Items = j.ToList(),
							SubGroups = new List<GroupedLimitItemModel>()
						}).ToList()
				}).ToList();

			foreach (var tour in tours)
			{
				var subGroup = res.SubGroups.FirstOrDefault(s => s.TimeOfRestId == tour.TimeOfRestId);
				if (subGroup == null)
				{
					subGroup = new GroupedLimitItemModel
					{
						TimeOfRestId = tour.TimeOfRestId,
						Name = tour.TimeOfRest.Name,
						Items = new List<LimitItemModel>(),
						SubGroups = new List<GroupedLimitItemModel>()
					};
					res.SubGroups.Add(subGroup);
				}

				var t = subGroup.SubGroups.FirstOrDefault(sg => sg.TourId == tour.Id);
				if (t == null)
				{
					t = new GroupedLimitItemModel
					{
						TimeOfRestId = tour.TimeOfRestId,
						Name = $"{tour} <br/><small><i>{tour.NullSafe(tr => tr.Hotels.PlaceOfRest.Name)}</i></small>",
						PlaceOfRestId = tour.Hotels.PlaceOfRestId,
						OivId = tour.LimitOnVedomstvoId,
						OivOrgId = tour.LimitOnVedomstvo.OrganizationId,
						TourId = tour.Id,
						Volume = tour.Volumes.FirstOrDefault().NullSafe(v => v.CountPlace) ?? 0,
						Items = new List<LimitItemModel>(),
						OrderDate = tour.DateIncome,
						OrderString = tour.Hotels.NullSafe(h => h.Name),
						SubGroups = new List<GroupedLimitItemModel>()
					};

					subGroup.SubGroups.Add(t);
				}
			}

			if (res.SubGroups != null)
			{
				res.SubGroups = res.SubGroups.OrderBy(s => s.TimeOfRestId).ToList();
				foreach (var sg in res.SubGroups)
				{
					sg.SubGroups = sg.SubGroups.OrderBy(s => s.OrderString).ThenBy(s => s.OrderDate).ThenBy(s => s.Name).ToList();
					foreach (var inner in sg.SubGroups)
					{
						inner.SubGroups = inner.SubGroups.OrderBy(s => s.OrderString).ThenBy(s => s.OrderDate).ThenBy(s => s.Name)
							.ToList();
					}
				}
			}

			return res;
		}


		/// <summary>
		///     получить список квот по ОИВ для года
		/// </summary>
		[HttpPost]
		public List<LimitItemModel> GetVedLimitsOnYear(long yearId, long? oivId)
		{
			if (!Security.HasRight(AccessRightEnum.Limits.LimitByOrganization, oivId ?? 0) &&
			    !Security.HasRight(AccessRightEnum.Limits.LimitToOiv))
			{
				return new List<LimitItemModel>();
			}

			var q = UnitOfWork.GetSet<LimitOnOrganization>()
				.Where(
					l =>
						l.LimitOnVedomstvo.YearOfRestId == yearId &&
						l.StateId.HasValue && l.StateId != StateMachineStateEnum.Deleted);

			if (oivId.HasValue || !Security.HasRight(AccessRightEnum.Limits.LimitToOiv))
			{
				q = q.Where(l => l.LimitOnVedomstvo.OrganizationId == oivId);
			}

			if (!oivId.HasValue && Security.HasRight(AccessRightEnum.Limits.LimitToOiv))
			{
				q = q.Where(l => l.StateId == StateMachineStateEnum.Limit.Organization.Approved);
			}

			return q.Where(l => l.Organization != null).ToList()
				.Select(
					l =>
						new LimitItemModel
						{
							Id = l.Id,
							Volume = l.Volume,
							OrganizationName = l.Organization.Name,
							VedomstvoName = l.LimitOnVedomstvo.Organization.Name,
							State = l.State,
							OrganizationId = l.OrganizationId,
							YearOfRestId = l.LimitOnVedomstvo.YearOfRestId,
							Actions =
								StateController.GetActions(l.State, StateMachineEnum.LimitOrganizationState)
									.Where(
										a =>
											(new[]
											{
												AccessRightEnum.Limit.Organization.Formation, AccessRightEnum.Limit.Organization.OnCompletion,
												AccessRightEnum.Limit.Organization.Brought,
												AccessRightEnum.Limit.Organization.Approved
											}.Contains(a.ActionCode) && Security.HasRight(AccessRightEnum.Limits.LimitByOrganization, oivId ?? 0)) ||
											(new[]
											{
												AccessRightEnum.Limit.Organization.ApprovedToCompetiotion, AccessRightEnum.Limit.Organization.Confirmed
											}.Contains(a.ActionCode)))
									.ToList()
						})
				.OrderBy(l => l.OrganizationName)
				.ToList();
		}

		/// <summary>
		///     список списков детей организации
		/// </summary>
		public ListOfChildsListModel ListOfChildsList(ListOfChildsListModel model)
		{
			SetUnitOfWorkInRefClass(UnitOfWork);
			var secs = Security.GetSecurity().Where(s => s.StartsWith(AccessRightEnum.Limits.LimitChildInOrganization)).ToList();
			model.OnlyOneOrganization = false;

			// указываем что только одна организация если она одна
			if (!secs.Contains(AccessRightEnum.Limits.LimitChildInOrganization))
			{
				var orgsId = secs.Select(
					s =>
						s.Replace(
							string.Format(Security.SecurityOrgTemplate, AccessRightEnum.Limits.LimitChildInOrganization, string.Empty),
							string.Empty).LongParse()).Where(l => l.HasValue).ToArray();

				if (orgsId.Any() && orgsId.Length == 1)
				{
					var org = UnitOfWork.GetById<Organization>(orgsId.First() ?? 0);
					model.OrganizationId = org.Id;
					model.OrganizationName = org.Name;
					model.OnlyOneOrganization = true;
				}
			}

			// если есть квота то грузим ее и проверяем на не противоречивость модели
			if (model.LimitOnOrganizationId.HasValue)
			{
				model.LimitOnOrganization = UnitOfWork.GetById<LimitOnOrganization>(model.LimitOnOrganizationId.Value);

				// если есть год и он не равен модели то убераем моедль
				if (model.YearOfRestId.HasValue &&
				    model.YearOfRestId != model.NullSafe(m => m.LimitOnOrganization.LimitOnVedomstvo.YearOfRestId))
				{
					model.LimitOnOrganizationId = null;
					model.LimitOnOrganization = null;
				}
				else
				{
					model.YearOfRestId = model.NullSafe(m => m.LimitOnOrganization.LimitOnVedomstvo.YearOfRestId);
				}

				if (!model.OnlyOneOrganization || !model.OrganizationId.HasValue ||
				    model.OrganizationId == model.NullSafe(m => m.LimitOnOrganization.OrganizationId))
				{
					model.OrganizationId = model.NullSafe(m => m.LimitOnOrganization.OrganizationId);
					model.OrganizationName = model.NullSafe(m => m.LimitOnOrganization.Organization.Name);
				}
				else if (model.OnlyOneOrganization &&
				         model.OrganizationId != model.NullSafe(m => m.LimitOnOrganization.OrganizationId))
				{
					model.LimitOnOrganizationId = null;
					model.LimitOnOrganization = null;
				}
			}

			// если год не загрузился то
			if (!model.YearOfRestId.HasValue)
			{
				model.YearOfRestId =
					(model.YearsOfRest.FirstOrDefault(y => y.Year == DateTime.Now.Year) ?? model.YearsOfRest.LastOrDefault())
					.NullSafe(y => y.Id);
			}

			// если нет модели и есть организация то ищем подходящую.
			if (!model.LimitOnOrganizationId.HasValue && model.OrganizationId.HasValue)
			{
				var limits =
					UnitOfWork.GetSet<LimitOnOrganization>()
						.Where(
							l =>
								l.OrganizationId == model.OrganizationId && l.LimitOnVedomstvo.YearOfRestId == model.YearOfRestId &&
								l.StateId != StateMachineStateEnum.Deleted)
						.ToList();

				if (limits.Count == 1)
				{
					model.LimitOnOrganization = limits.First();
					model.LimitOnOrganizationId = model.LimitOnOrganization.Id;
				}
			}


			var query =
				UnitOfWork.GetSet<ListOfChilds>().Where(org =>
						org.IsLast && !org.IsDeleted && org.StateId.HasValue && org.StateId != StateMachineStateEnum.Deleted)
					.Where(x => x.LimitOnOrganizationId == model.LimitOnOrganizationId);

			var totalCount = query.Count();
			var entity = query.OrderBy(place => place.Name).ToList();

         //for(int i = 0; i <= entity)

			var result = new ListOfChildsListModel(entity, 1, 1, totalCount)
			{
				YearOfRestId = model.YearOfRestId,
				YearsOfRest = model.YearsOfRest,
				LimitOnOrganization = model.LimitOnOrganization,
				OrganizationId = model.OrganizationId,
				OrganizationName = model.OrganizationName,
				OnlyOneOrganization = model.OnlyOneOrganization,
				LimitOnOrganizationId = model.LimitOnOrganizationId
			};

			if (model.LimitOnOrganization != null)
			{
				result.State = new ViewModelState
				{
					Actions =
						StateController.GetActions(result.LimitOnOrganization.State, StateMachineEnum.LimitOrganizationState)
							.Where(
								a =>
									!new[]
									{
										AccessRightEnum.Limit.Organization.Brought, /*AccessRightEnum.Limit.Organization.OnCompletion,*/
										AccessRightEnum.Limit.Organization.Formation
									}.Contains(a.ActionCode))
							.ToList(),
					State = result.LimitOnOrganization.State,
					Title =
						$"{result.LimitOnOrganization.NullSafe(l => l.Organization.Name)} <h5>Год кампании: {result.LimitOnOrganization.NullSafe(l => l.LimitOnVedomstvo.YearOfRest.Name)}</h5>",
					ActionSelector = ".stringStateCode",
					FormSelector = ".postForm",
					CommentSelector = ".stringCommentaryCode",
					ActionWithComment = new List<string>
					{
						AccessRightEnum.Limit.Organization.ApprovedToCompetiotion,
						AccessRightEnum.Limit.Organization.OnCompletion
					},
					Sign = result.LimitOnOrganization.SignInfo
				};
			}

			return result;
		}

		/// <summary>
		///     сохранение списка ораганизации
		/// </summary>
		internal ListOfChilds SaveListOfChilds(ListOfChilds data)
		{
			if (data.StateId == StateMachineStateEnum.Limit.List.Formed &&
			    !Security.HasRight(AccessRightEnum.Limit.List.EditInAllStates))
			{
				return data;
			}

			//var childs = data.Childs ?? new List<Child>();
			//var newChilds = childs.Where(c => c.Id == 0).ToList();
			//var attendants = (data.Attendants ?? new List<Applicant>());
			//var newAttendants = attendants.Where(c => c.Id == 0).ToList();

			if (data.Childs != null)
			{
				foreach (var child in data.Childs)
				{
					if (child.Address != null)
					{
						child.Address.BtiAddress = null;
						child.Address.BtiDistrict = null;
						child.Address.BtiRegion = null;
					}
				}
			}

			if (data.Id > 0)
			{
				var newData = data;
				data = UnitOfWork.GetById<ListOfChilds>(data.Id);
				data.Name = newData.Name;
				data.Responsible = newData.Responsible;
				data.ResponsiblePhone = newData.ResponsiblePhone;
				data.LimitOnOrganizationId = newData.LimitOnOrganizationId;
				data.PlaceOfRestId = newData.PlaceOfRestId;
				data.TimeOfRestId = newData.TimeOfRestId;
				data.DateChange = DateTime.Now;
				data.ListOfChildsCategoryId = newData.ListOfChildsCategoryId;
				data.StateId = data.StateId ?? StateMachineStateEnum.Limit.List.Formation;
				// дети
				var childsId = newData.Childs.Where(c => c.Id > 0).Select(c => c.Id);
				var childsForAdd = newData.Childs.Where(c => c.Id == 0).ToList();
				foreach (var child in data.Childs.Where(c => !childsId.Contains(c.Id)).ToList())
				{
					if (child.Address != null)
					{
						UnitOfWork.GetSet<Address>().Remove(child.Address);
					}

					UnitOfWork.GetSet<Child>().Remove(child);
				}

				foreach (var child in childsForAdd)
				{
					if (data.TypeOfLimitListId == (long) TypeOfLimitListEnum.Orphan)
					{
						child.Payed = true;
					}

					if (child.TypeOfRestrictionId == 0)
					{
						child.TypeOfRestrictionId = null;
					}

					child.ChildListId = data.Id;
					UnitOfWork.AddEntity(child);
				}

				foreach (var child in data.Childs.Where(c => childsId.Contains(c.Id)).ToList())
				{
					var source = newData.Childs.FirstOrDefault(c => c.Id == child.Id);
					if (source != null)
					{
						child.LastName = source.LastName;
						child.FirstName = source.FirstName;
						child.MiddleName = source.MiddleName;
						child.HaveMiddleName = source.HaveMiddleName;
						child.Male = source.Male;
						child.DateOfBirth = source.DateOfBirth;
						child.PlaceOfBirth = source.PlaceOfBirth;
						child.DocumentTypeId = source.DocumentTypeId;
						child.DocumentSeria = source.DocumentSeria;
						child.DocumentNumber = source.DocumentNumber;
						child.DocumentDateOfIssue = source.DocumentDateOfIssue;
						child.DocumentSubjectIssue = source.DocumentSubjectIssue;
						child.SchoolId = source.SchoolId;
						child.SchoolNotPresent = source.SchoolNotPresent;
						child.ContactFirstName = source.ContactFirstName;
						child.ContactLastName = source.ContactLastName;
						child.ContactMiddleName = source.ContactMiddleName;
						child.ContactPhone = source.ContactPhone;

						if (source.TypeOfRestrictionId == 0)
						{
							child.TypeOfRestrictionId = null;
						}
						else
						{
							child.TypeOfRestrictionId = source.TypeOfRestrictionId;
						}

                  if (source.Address != null && !string.IsNullOrWhiteSpace(source.Address.Region))
                  {
                     source.Address.BtiDistrictId = UnitOfWork.GetSet<BtiDistrict>().Where(ss => ss.Name.ToLower() == source.Address.Region.ToLower()).Select(ss => ss.Id).FirstOrDefault();
                  }
                  if (source.Address != null && !string.IsNullOrWhiteSpace(source.Address.District))
                  {
                     source.Address.BtiRegionId = UnitOfWork.GetSet<BtiRegion>().Where(ss => ss.Name.ToLower() == source.Address.District.ToLower()).Select(ss => ss.Id).FirstOrDefault();
                  }

						if (child.Address == null && source.Address != null)
						{
							var a = UnitOfWork.AddEntity(source.Address);
							child.Address = a;
							child.AddressId = a.Id;
						}
						else if (child.Address != null && source.Address != null)
						{
							child.Address.BtiAddressId = source.Address.BtiAddressId;
							child.Address.Appartment = source.Address.Appartment;
							child.Address.BtiDistrictId = source.Address.BtiDistrictId;
							child.Address.BtiRegionId = source.Address.BtiRegionId;
							child.Address.Corpus = source.Address.Corpus;
							child.Address.House = source.Address.House;
							child.Address.Name = source.Address.Name;
							child.Address.Street = source.Address.Street;
							child.Address.Stroenie = source.Address.Stroenie;
							child.Address.Vladenie = source.Address.Vladenie;
                     child.Address.FiasId = source.Address.FiasId;
                     child.Address.District = source.Address.District;
                     child.Address.Region = source.Address.Region;
						}

						child.Key = StaticHelpers.GenerateKey(
							child.FirstName,
							child.LastName,
							child.MiddleName,
							child.DocumentSeria,
							child.DocumentNumber);
						child.KeySame = StaticHelpers.GenerateKeySame(
							child.FirstName,
							child.LastName,
							child.DateOfBirth);
						child.YearOfCompany = data?.Tour?.YearOfRest?.Year;
						child.TypeOfGroupCheckId = data?.Tour?.TypeOfRest?.TypeOfGroupCheckId;
						if (data.Tour != null)
						{
							child.IntervalStart = data?.Tour?.DateIncome?.Date.Ticks ?? 0;
							child.IntervalEnd = data?.Tour?.DateOutcome?.AddDays(1).Ticks ?? 0;
						}
						else
						{
							child.IntervalStart = null;
							child.IntervalEnd = null;
						}

						if (data.TypeOfLimitListId == (long) TypeOfLimitListEnum.Orphan)
						{
							child.Payed = true;
						}
					}
				}

				// сопровождающие
				var attendantsId = newData.Attendants.Where(c => c.Id > 0).Select(c => c.Id);
				foreach (var attendant in data.Attendants.Where(c => !attendantsId.Contains(c.Id)).ToList())
				{
					UnitOfWork.GetSet<Applicant>().Remove(attendant);
				}

				var attendantsForAdd = newData.Attendants.Where(c => c.Id == 0).ToList();
				foreach (var attendant in attendantsForAdd)
				{
					attendant.Country = null;
					if (attendant.CountryId <= 0)
					{
						attendant.CountryId = null;
					}

					if (data.TypeOfLimitListId == (long) TypeOfLimitListEnum.Orphan)
					{
						attendant.Payed = true;
					}

					attendant.ChildListId = data.Id;
					UnitOfWork.AddEntity(attendant);
				}

				foreach (var attendant in data.Attendants.Where(c => attendantsId.Contains(c.Id)).ToList())
				{
					var source = newData.Attendants.FirstOrDefault(c => c.Id == attendant.Id);
					if (source != null)
					{
						attendant.LastName = source.LastName;
						attendant.FirstName = source.FirstName;
						attendant.MiddleName = source.MiddleName;
						attendant.Male = source.Male;
						attendant.DateOfBirth = source.DateOfBirth;
						attendant.PlaceOfBirth = source.PlaceOfBirth;
						attendant.DocumentTypeId = source.DocumentTypeId;
						attendant.DocumentSeria = source.DocumentSeria;
						attendant.DocumentNumber = source.DocumentNumber;
						attendant.DocumentDateOfIssue = source.DocumentDateOfIssue;
						attendant.DocumentSubjectIssue = source.DocumentSubjectIssue;
						attendant.Position = source.Position;
						attendant.CountryId = source.CountryId;
						if (attendant.CountryId <= 0)
						{
							attendant.CountryId = null;
						}

						attendant.Key = StaticHelpers.GenerateKey(
							attendant.FirstName,
							attendant.LastName,
							attendant.MiddleName,
							attendant.DocumentSeria,
							attendant.DocumentNumber);
						if (data.Tour != null)
						{
							attendant.IntervalStart = data.Tour.DateIncome?.Date.Ticks ?? 0;
							attendant.IntervalEnd = data.Tour.DateOutcome?.Date.AddDays(1).Ticks ?? 0;
						}
						else
						{
							attendant.IntervalStart = null;
							attendant.IntervalEnd = null;
						}

						if (data.TypeOfLimitListId == (long) TypeOfLimitListEnum.Orphan)
						{
							attendant.Payed = true;
						}
					}
				}

				data = UnitOfWork.Update(data);
			}
			else
			{
				data.ResetZeroFk();
				data.DateChange = DateTime.Now;
				data.StateId = StateMachineStateEnum.Limit.List.Formation;
				if (data.Attendants != null)
				{
					foreach (var att in data.Attendants)
					{
						if (att.CountryId <= 0)
						{
							att.CountryId = null;
						}

						att.Country = null;

						if (data.TypeOfLimitListId == (long) TypeOfLimitListEnum.Orphan)
						{
							att.Payed = true;
						}
					}
				}

				if (data.Childs != null)
				{
					foreach (var child in data.Childs)
					{
						child.Key = StaticHelpers.GenerateKey(
							child.FirstName,
							child.LastName,
							child.MiddleName,
							child.DocumentSeria,
							child.DocumentNumber);
						child.KeySame = StaticHelpers.GenerateKeySame(
							child.FirstName,
							child.LastName,
							child.DateOfBirth);
						child.YearOfCompany = data.Tour?.YearOfRest?.Year;
						child.TypeOfGroupCheckId = data.Tour?.TypeOfRest?.TypeOfGroupCheckId;
						if (data.Tour != null)
						{
							child.IntervalStart = data.Tour.DateIncome?.Date.Ticks;
							child.IntervalEnd = data.Tour.DateOutcome?.AddDays(1).Ticks;
						}
						else
						{
							child.IntervalStart = null;
							child.IntervalEnd = null;
						}

						if (data.TypeOfLimitListId == (long) TypeOfLimitListEnum.Orphan)
						{
							child.Payed = true;
						}
					}
				}

				data = UnitOfWork.AddEntity(data);
			}

/*			var family = UnitOfWork.GetById<LimitOnOrganization>(data.LimitOnOrganizationId)?.Tour?.TypeOfRestId ==
			             (long) TypeOfRestEnum.SpecializedСampFamily;

			if (!family)
			{*/
			data.CountChild = data.Childs.Count;
			data.CountAttendants = data.Attendants.Count;
			/*}
			else
			{
				data.CountChild = data.Childs.Count + data.Attendants.Count;
				data.CountAttendants = 0;
			}*/

			UnitOfWork.SaveChanges();

			return data;
		}

		/// <summary>
		/// Обновление списка доп. услуг для сопровождающего
		/// </summary>
		/// <param name="id">Id сопровождающего</param>
		/// <param name="addonServices">Id доп. услуг</param>
		/// <returns></returns>
		[HttpPost]
		public List<AddonServices> UpdateAddonServiceLinksForAttendant([FromUri] long id,
			[FromBody] ICollection<AddonServices> addonServices)
		{
			SetUnitOfWorkInRefClass(UnitOfWork);
			UnitOfWork.SaveChanges();
			return
				UnitOfWork.GetSet<AddonServicesLink>()
					.Where(l => l.AddonServices != null && l.ApplicantId == id)
					.Select(l => l.AddonServices)
					.Distinct()
					.ToList()
					.Select(s => new AddonServices(s))
					.ToList();
		}

		/// <summary>
		///     снять квоты ведомству
		/// </summary>
		[HttpPost]
		public GeneralResponse VedomstvoAction(long limitId, string actionCode)
		{
			using (var tran = UnitOfWork.GetTransactionScope())
			{
				if (!ChangeVedomstvoListState(limitId, actionCode))
				{
					return new GeneralResponse
					{
						IsError = true,
						Errors = CheckVedomstvoLimitStateChange(limitId, actionCode)
					};
				}

				tran.Complete();
				return new GeneralResponse();
			}
		}

		/// <summary>
		///     снять квоты ведомству
		/// </summary>
		[HttpPost]
		public GeneralResponse OrganizationAction(long limitId, string actionCode, string commentary)
		{
			SetUnitOfWorkInRefClass(UnitOfWork);
			var action = StateController.GetAction(actionCode);
			if (action != null && action.NeedSign)
			{
				var errors = CheckOrganizationLimitStateChange(limitId, actionCode);
				if (errors != null && errors.Any())
				{
					return new GeneralResponse
					{
						IsError = true,
						Errors = CheckOrganizationLimitStateChange(limitId, actionCode)
					};
				}

				var data = Pdf.GetDataForSign(limitId, SignTypeEnum.ListOfOrganization);
				if (data != null)
				{
					var limit = UnitOfWork.GetById<LimitOnOrganization>(limitId);

					data.ActionCode = actionCode;
					data.Commentary = commentary;
					var esep = new Esep();
					var upload = esep.UploadFilesToEsep(new List<DataForSign> {data});
					var url = esep.UrlToEsep(upload.Select(u => u.FileAccessCode).ToList(),
						Esep.FullReturnUrl(
							$"/Limits/OrganizationList?yearOfRestId={limit.LimitOnVedomstvo.YearOfRestId}&limitOnVedomstvoId={limit.LimitOnVedomstvoId}"),
						Guid.NewGuid().ToString());

					return new GeneralResponse
					{
						IsError = false,
						Url = url
					};
				}
			}


			if (!ChangeOrganizationListState(limitId, actionCode, null, commentary))
			{
				return new GeneralResponse
				{
					IsError = true,
					Errors = CheckOrganizationLimitStateChange(limitId, actionCode)
				};
			}

			return new GeneralResponse
			{
				Item = GetItemModelOnLimitOnOrganization(UnitOfWork.GetById<LimitOnOrganization>(limitId))
			};
		}

		/// <summary>
		///     добавить квоту
		/// </summary>
		[HttpPost, HttpGet]
		public GeneralResponse AddOrganizationLimit(long limitVedomstviId, long organizationId, long? timeOfRestId,
			long? placeOfRestId, long? tourId, int volume)
		{
			var limitOnVedomstvo = UnitOfWork.GetById<LimitOnVedomstvo>(limitVedomstviId);

			if (limitOnVedomstvo == null)
			{
				return new GeneralResponse("Нет организации для добавления квоты");
			}

			if (!Security.HasRight(AccessRightEnum.Limits.LimitByOrganization, limitOnVedomstvo.OrganizationId ?? 0))
			{
				return new GeneralResponse("Нет прав для добавления квот к указанной организации");
			}

			if (limitOnVedomstvo.StateId == StateMachineStateEnum.Deleted)
			{
				return new GeneralResponse("Для выбранного ведомства нет квот");
			}

			var limit =
				UnitOfWork.GetSet<LimitOnOrganization>()
					.FirstOrDefault(v =>
						v.LimitOnVedomstvoId == limitVedomstviId && tourId == v.TourId && v.PlaceOfRestId == placeOfRestId &&
						v.TimeOfRestId == timeOfRestId && v.OrganizationId == organizationId) ??
				new LimitOnOrganization
				{
					Approved = false,
					StateId = StateMachineStateEnum.Limit.Organization.Formation,
					OrganizationId = organizationId,
					LimitOnVedomstvoId = limitOnVedomstvo.Id,
					TimeOfRestId = timeOfRestId,
					PlaceOfRestId = placeOfRestId,
					TourId = tourId,
					TypeOfLimitListId = limitOnVedomstvo.TypeOfLimitListId
				};

			if (limit.StateId != StateMachineStateEnum.Deleted && limit.Id > 0)
			{
				return new GeneralResponse("Квота на организацию уже добавлена");
			}

			limit.Approved = false;
			limit.StateId = StateMachineStateEnum.Limit.Organization.Formation;
			limit.Volume = volume;
			limit = limit.Id == 0 ? UnitOfWork.AddEntity(limit) : UnitOfWork.Update(limit);
			limit.LimitOnVedomstvoId = limitOnVedomstvo.Id;
			limit = UnitOfWork.Update(limit);

			return new GeneralResponse {Item = GetItemModelOnLimitOnOrganization(limit)};
		}

		private IQueryable<Organization> GetOrganizationForRequest()
		{
			var curId = Security.GetCurrentAccountId() ?? 0;
			var forAll = Security.HasRight(AccessRightEnum.Limit.Request.View, null);
			var org = UnitOfWork.GetSet<Organization>().Where(o => !o.IsDeleted);

			if (!forAll)
			{
				var rolesId =
					UnitOfWork.GetSet<Role>()
						.Where(a => a.AccessRights.Any(r => r.Code == AccessRightEnum.Limit.Request.View))
						.Select(r => (long?) r.Id);

				var orgFirstIds =
					UnitOfWork.GetSet<Account>()
						.Where(a => a.Id == curId)
						.SelectMany(a => a.Rights)
						.Where(a => a.AccessRight.Code == AccessRightEnum.Limit.Request.View)
						.Select(a => a.OrganizationId);


				var orgSecondIds =
					UnitOfWork.GetSet<Account>()
						.Where(a => a.Id == curId)
						.SelectMany(a => a.Roles)
						.Where(a => rolesId.Contains(a.RoleId))
						.Select(r => r.OrganizationId);


				if (orgFirstIds.Count() > 50 || orgSecondIds.Count() > 50)
				{
					org = org.Where(l => orgFirstIds.Contains(l.Id) || orgSecondIds.Contains(l.Id) ||
					                     orgFirstIds.Contains(l.ParentId) || orgSecondIds.Contains(l.ParentId));
				}
				else
				{
					var orgFirstIdsArray = orgFirstIds.ToArray();
					var orgSecondIdsArray = orgSecondIds.ToArray();
					org = org.Where(l => orgFirstIdsArray.Contains(l.Id) || orgSecondIdsArray.Contains(l.Id) ||
					                     orgFirstIdsArray.Contains(l.ParentId) || orgSecondIdsArray.Contains(l.ParentId));
				}

				return org;
			}
			else
			{
				return org;
			}
		}

		[HttpPost, HttpGet]
		public List<Organization> GetOrganizationForRequest(long yearOfRestId, long? oivId, string name)
		{
			var q = GetOrganizationForRequest();

			if (oivId.HasValue)
			{
				var limit = UnitOfWork.GetById<LimitOnVedomstvo>(oivId);

				q = q.Where(o => o.ParentId == limit.OrganizationId || o.Id == limit.OrganizationId);
			}

			if (!string.IsNullOrEmpty(name))
			{
				var ss = name.ToLower();
				q = q.Where(o => o.Name.ToLower().Contains(ss));
			}

			return q.OrderBy(o => o.Name.Length).ThenBy(o => o.Name).Take(20).ToList().Select(o => new Organization(o)).ToList();
		}

		/// <summary>
		/// получить заявку по ИД.
		/// </summary>
		[HttpPost]
		public LimitOnOrganizationRequest GetOrganizationRequest(long reqId)
		{
			var r = UnitOfWork.GetById<LimitOnOrganizationRequest>(reqId);
			return new LimitOnOrganizationRequest(r, 2);
		}

		/// <summary>
		///     удалить квоту
		/// </summary>
		[HttpPost]
		public string RemoveOrganizationRequest(long reqId)
		{
			var entity = UnitOfWork.GetById<LimitOnOrganizationRequest>(reqId);

			if (entity == null)
			{
				return "Заявка на квоту не найдена";
			}


			if (
				!(Security.HasRight(AccessRightEnum.Limit.Request.View, entity.OrganizationId) ||
				  Security.HasRight(AccessRightEnum.Limit.Request.View, entity.Organization?.ParentId)) ||
				!Security.HasRight(AccessRightEnum.Limit.Request.Edit))
			{
				return "У вас нет прав для удаления заявки для организации";
			}


			if (entity.StateId != StateMachineStateEnum.LimitRequest.Editing)
			{
				return "В заявку были внесены изменения другим пользователем. Обновите страницу и внесите изменения повторно.";
			}

			entity.LastUpdateTick = DateTime.Now.Ticks;
			entity.HistoryLink = this.WriteHistory(entity.HistoryLink, "Удаление в заявки на профильный отдых",
				$"Наименование отделения {entity.Name}");
			entity.HistoryLinkId = entity.HistoryLink?.Id;
			entity.StateId = StateMachineStateEnum.Deleted;
			UnitOfWork.SaveChanges();
			return string.Empty;
		}

		/// <summary>
		///     добавить квоту
		/// </summary>
		[HttpPost]
		public string UpdateOrganizationRequest(LimitOnOrganizationRequest data)
		{
			var org = UnitOfWork.GetById<Organization>(data.OrganizationId);

			if (
				!(Security.HasRight(AccessRightEnum.Limit.Request.View, org.Id) ||
				  Security.HasRight(AccessRightEnum.Limit.Request.View, org.ParentId)) ||
				!Security.HasRight(AccessRightEnum.Limit.Request.Edit))
			{
				return "У вас нет прав для обновления заявки для организации";
			}

			var entity = UnitOfWork.GetById<LimitOnOrganizationRequest>(data.Id);

			if (entity.StateId != StateMachineStateEnum.LimitRequest.Editing ||
			    entity.LastUpdateTick / 10000 != data.LastUpdateTick / 10000)
			{
				return "В заявку были внесены изменения другим пользователем. Обновите страницу и внесите изменения повторно.";
			}

			if (data.GroupedTimeOfRestId <= 0)
			{
				data.GroupedTimeOfRestId = null;
			}

			if (data.ListOfChildsCategoryId <= 0)
			{
				data.ListOfChildsCategoryId = null;
			}

			entity.LastUpdateTick = DateTime.Now.Ticks;
			entity.HistoryLink = this.WriteHistory(data.HistoryLink, "Внесение изменение в заявку на профильный отдых",
				$"Наименование отделения {data.Name}");
			entity.HistoryLinkId = entity.HistoryLink?.Id;

			entity.ApprovedVolume = data.ApprovedVolume;
			entity.Volume = data.Volume;
			entity.PlaceOfRestId = data.PlaceOfRestId;
			entity.Comment = data.Comment;
			entity.Name = data.Name;
			entity.VolumeAttendant = data.VolumeAttendant;
			entity.VolumeCounselor = data.VolumeCounselor;
			entity.GroupedTimeOfRestId = data.GroupedTimeOfRestId;
			entity.ListOfChildsCategoryId = data.ListOfChildsCategoryId;
			UnitOfWork.SaveChanges();
			return string.Empty;
		}

		/// <summary>
		///     изменение статуса списка ОИВ
		/// </summary>
		internal bool ChangeRequestState(long id, string stateCode, long? signInfoId = null, string commentary = null)
		{
			var entity = UnitOfWork.GetById<LimitOnOrganizationRequest>(id);

			var state = StateController.GetNextState(entity.StateId, stateCode);

			if (state != null)
			{
				entity.HistoryLink = this.WriteHistory(entity.HistoryLink,
					$"Изменение статуса с '{entity.State.Name}' на '{state.Name}'", commentary, state.Id,
					entity.StateId);
				entity.HistoryLinkId = entity.HistoryLink?.Id;

				entity.StateId = state.Id;
				UnitOfWork.Update(entity);

				return true;
			}

			return false;
		}

		/// <summary>
		///     снять квоты ведомству
		/// </summary>
		[HttpPost]
		public GeneralResponse RequestAction(long reqId, string actionCode, string commentary)
		{
			using (var tran = UnitOfWork.GetTransactionScope())
			{
				var entity = UnitOfWork.GetById<LimitOnOrganizationRequest>(reqId);

				if (entity == null)
				{
					return new GeneralResponse("Заявка на квоту не найдена");
				}

				if (
					!Security.HasRight(actionCode) || !(Security.HasRight(AccessRightEnum.Limit.Request.View, entity.OrganizationId) ||
					                                    Security.HasRight(AccessRightEnum.Limit.Request.View,
						                                    entity.Organization?.ParentId)))
				{
					return new GeneralResponse("У вас нет прав для изменения статуса заявки для организации");
				}

				if (actionCode == AccessRightEnum.Limit.Request.ToApprove)
				{
					var errors = new List<string>();
					if (!entity.GroupedTimeOfRestId.HasValue)
					{
						errors.Add("Не указано желаемое время отдыха");
					}

					if (entity.Volume <= 0)
					{
						errors.Add("Не указано количество детей");
					}

					if (!entity.PlaceOfRestId.HasValue)
					{
						errors.Add("Не указан желаемый регион отдыха");
					}

					if (errors.Any())
					{
						return new GeneralResponse
						{
							IsError = true,
							Errors = errors
						};
					}
				}

				if (!ChangeRequestState(reqId, actionCode, null, commentary))
				{
					return new GeneralResponse("Ошибка при изменении статуса");
				}

				tran.Complete();
				return new GeneralResponse();
			}
		}

		/// <summary>
		///     добавить квоту
		/// </summary>
		[HttpPost]
		public string AddOrganizationRequest(LimitOnOrganizationRequest data)
		{
			var org = UnitOfWork.GetById<Organization>(data.OrganizationId);

			if (
				!(Security.HasRight(AccessRightEnum.Limit.Request.View, org.Id) ||
				  Security.HasRight(AccessRightEnum.Limit.Request.View, org.ParentId)) ||
				!Security.HasRight(AccessRightEnum.Limit.Request.Edit))
			{
				return "У вас нет прав для добавления заявки для организации";
			}

			var limit =
				UnitOfWork.GetSet<LimitOnVedomstvo>()
					.Where(v => v.StateId == StateMachineStateEnum.Limit.Oiv.GatheringRequirements)
					.FirstOrDefault(v => v.Id == data.LimitOnVedomstvoId && v.YearOfRestId == data.StateId);

			if (limit == null)
			{
				return "Кампания по сбору заявлений на профильный отдых от организаций для указанного ОИВ не идет";
			}

			data.LimitOnVedomstvoId = limit.Id;
			data.StateId = StateMachineStateEnum.LimitRequest.Editing;
			data.LastUpdateTick = DateTime.Now.Ticks;
			data.HistoryLink = this.WriteHistory(data.HistoryLink, "Добавление заявки на профильный отдых",
				$"Наименование отделения {data.Name}");
			if (data.GroupedTimeOfRestId <= 0)
			{
				data.GroupedTimeOfRestId = null;
			}

			if (data.ListOfChildsCategoryId <= 0)
			{
				data.ListOfChildsCategoryId = null;
			}

			data.HistoryLinkId = data.HistoryLink?.Id;

			UnitOfWork.AddEntity(data);
			UnitOfWork.SaveChanges();

			return string.Empty;
		}

		/// <summary>
		///     добавить квоту
		/// </summary>
		[HttpPost]
		public string AddOivLimit(long yearId, long organizationId, int? volume, long? tlid)
		{
			if (!Security.HasRight(AccessRightEnum.Limits.LimitToOiv))
			{
				return "У вас нет прав для добавления квоты для ОИВ";
			}

			tlid = tlid ?? (long) TypeOfLimitListEnum.Profile;

			var limit =
				UnitOfWork.GetSet<LimitOnVedomstvo>()
					.FirstOrDefault(v =>
						v.OrganizationId == organizationId && v.YearOfRestId == yearId &&
						(v.TypeOfLimitListId ?? (long) TypeOfLimitListEnum.Profile) == tlid) ?? new LimitOnVedomstvo
				{
					Approved = false,
					StateId = StateMachineStateEnum.Limit.Oiv.GatheringRequirements,
					OrganizationId = organizationId,
					YearOfRestId = yearId,
					TypeOfLimitListId = tlid
				};

			if (limit.Approved)
			{
				return "Квота по добавляемому ОИВ уже утверждена";
			}

			limit.Approved = false;
			limit.StateId = StateMachineStateEnum.Limit.Oiv.GatheringRequirements;
			limit.Volume = volume ?? 0;
			limit = limit.Id == 0 ? UnitOfWork.AddEntity(limit) : UnitOfWork.Update(limit);
			limit.LimitYear = limit.YearOfRest.Year;
			limit.LastUpdateTick = DateTime.Now.Ticks;
			UnitOfWork.Update(limit);
			return string.Empty;
		}

		/// <summary>
		///     обновить квоту
		/// </summary>
		[HttpPost]
		public string UpdateOivLimit(long limitId, int volume)
		{
			if (!Security.HasRight(AccessRightEnum.Limits.LimitToOiv))
			{
				return "У вас нет прав для редактирования квоты по ОИВ";
			}

			var limit = UnitOfWork.GetSet<LimitOnVedomstvo>().GetById(limitId);

			if (limit != null && (limit.StateId == StateMachineStateEnum.Limit.Oiv.Formation ||
			                      limit.StateId == StateMachineStateEnum.Limit.Oiv.GatheringRequirements))
			{
				limit.Volume = volume;
				UnitOfWork.Update(limit);
			}
			else
			{
				return "Квота по ОИВ уже доведена до ведомства.";
			}

			return string.Empty;
		}

		/// <summary>
		///     обновить квоту
		/// </summary>
		[HttpPost]
		public string UpdateOrganizationLimit(long limitId, int volume)
		{
			var limit = UnitOfWork.GetSet<LimitOnOrganization>().GetById(limitId);

			if (limit == null)
			{
				return "Нет указанной квоты.";
			}

			if (
				!Security.HasRight(AccessRightEnum.Limits.LimitByOrganization,
					limit.Organization.ParentId ?? limit.OrganizationId ?? 0))
			{
				return "Нет прав работой с квотами по организаций";
			}

			if (limit.StateId != StateMachineStateEnum.Limit.Organization.Formation)
			{
				return "Квота по организации утверждена";
			}

			limit.Volume = volume;
			UnitOfWork.Update(limit);

			return string.Empty;
		}

		public ListOfChilds GetListOfChild(long id)
		{
			return UnitOfWork.GetById<ListOfChilds>(id);
		}

		public ListOfChilds GetChildsAndAttendantsInList(long listId)
		{
			var res = UnitOfWork.GetSet<ListOfChilds>().Where(l => l.Id == listId)
				.Include(l => l.Childs)
				.Include(l => l.Attendants)
				.Include(l => l.Childs.Select(c => c.DocumentType))
				.Include(l => l.Attendants.Select(c => c.DocumentType))
				.FirstOrDefault();

			return new ListOfChilds(res)
			{
				Childs = res?.Childs.OrderBy(c => c.LastName).ThenBy(c => c.FirstName).ThenBy(c => c.MiddleName)
					         .Select(c => new Child(c) {DocumentType = new DocumentType(c.DocumentType)}).ToList() ??
				         new List<Child>(),
				Attendants =
					res?.Attendants.OrderBy(c => c.LastName).ThenBy(c => c.FirstName).ThenBy(c => c.MiddleName)
						.Select(c => new Applicant(c) {DocumentType = c.DocumentType}).ToList() ?? new List<Applicant>()
			};
		}

		public LimitOnOrganization GetLimitOnOrganization(long id)
		{
			return UnitOfWork.GetById<LimitOnOrganization>(id);
		}

		/// <summary>
		///     обновить квоту
		/// </summary>
		[HttpPost]
		public string DeleteOivLimit(long limitId)
		{
			if (!Security.HasRight(AccessRightEnum.Limits.LimitToOiv))
			{
				return "У вас нет прав для удаления квоты по ОИВ";
			}

			var limit = UnitOfWork.GetSet<LimitOnVedomstvo>().GetById(limitId);

			if (limit != null && !limit.Approved)
			{
				limit.StateId = StateMachineStateEnum.Deleted;
				UnitOfWork.Update(limit);
				return string.Empty;
			}

			return "Квота по ОИВ уже доведена до ОИВ и не может быть удалена.";
		}

		/// <summary>
		///     обновить квоту
		/// </summary>
		[HttpPost]
		public string DeleteOrganizationLimit(long limitId)
		{
			var limit = UnitOfWork.GetSet<LimitOnOrganization>().GetById(limitId);

			if (limit == null)
			{
				return "Нет указанной квоты.";
			}

			if (
				!Security.HasRight(AccessRightEnum.Limits.LimitByOrganization,
					limit.Organization.ParentId ?? limit.OrganizationId ?? 0))
			{
				return "Нет прав работой с квотами по организаций";
			}

			if (limit.StateId == StateMachineStateEnum.Limit.Organization.Brought)
			{
				return "Квота по организации утверждена";
			}

			if (
				UnitOfWork.GetSet<ListOfChilds>()
					.Any(l => l.LimitOnOrganizationId == limit.Id && !l.IsDeleted && l.StateId != StateMachineStateEnum.Deleted))
			{
				return "Нельзя удалять квоту организации к которой привязаны не удаленные списки.";
			}

			limit.StateId = StateMachineStateEnum.Deleted;
			limit.Approved = false;
			UnitOfWork.Update(limit);
			return string.Empty;
		}

		#region Работа с списками организации

		internal List<string> CheckListChildStateChange(long id, string stateCode)
		{
			var res = new List<string>();
			var data = UnitOfWork.GetById<ListOfChilds>(id);


			if (data.LimitOnOrganization.StateId != StateMachineStateEnum.Limit.Organization.Brought &&
			    data.LimitOnOrganization.StateId != StateMachineStateEnum.Limit.Organization.Confirmed)
			{
				res.Add("Нельзя изменять статус если ОИВ не довел размер квот.");
			}

			if (AccessRightEnum.Limit.List.Formed == stateCode)
			{
				if (new[]
				{
					StateMachineStateEnum.Limit.Organization.Formation, (long?) StateMachineStateEnum.Limit.Organization.OnCompletion
				}.Contains(data.LimitOnOrganization.StateId))
				{
					res.Add("Размер квоты учреждения не утвержден.");
				}

				if (new[]
				{
					(long?) StateMachineStateEnum.Limit.Oiv.Formation
				}.Contains(data.LimitOnOrganization.LimitOnVedomstvo.StateId))
				{
					res.Add("Размер квоты ОИВ не утвержден.");
				}

				if (data.CountChild <= 0 || data.Childs.Count <= 0)
				{
					res.Add("Нельзя утвердить список без отдыхающих.");
				}

				if (data.TypeOfLimitListId == (long) TypeOfLimitListEnum.Orphan)
				{
					if (data.Attendants.Count == 0)
					{
						if (data.Childs.Any(c => c.GetAgeInYears(data.LimitOnOrganization?.Tour?.DateIncome) < 7))
						{
							res.Add("Если есть ребенок от 2 до 6 лет, то должен быть хотя бы один сопровождающий");
						}
					}
				}

				var countApproveChild =
					UnitOfWork.GetSet<ListOfChilds>()
						.Where(
							c =>
								c.LimitOnOrganizationId == data.LimitOnOrganizationId && !c.IsDeleted && c.IsLast &&
								c.StateId == StateMachineStateEnum.Limit.List.Formed)
						.Select(c => c.CountChild).DefaultIfEmpty()
						.Sum();

				if (countApproveChild + data.CountChild > data.LimitOnOrganization.Volume)
				{
					res.Add("Превышен размер квоты");
				}

				if (data.Childs != null)
				{
					foreach (var child in data.Childs.Where(c => c.IsLast && !c.IsDeleted).ToList())
					{
						if (!DocumentTypeHelper.IsDocumentSeriaValid(child.DocumentTypeId ?? 0, child.DocumentSeria))
						{
							res.Add(
								$"Указано некорректное значение серии документа ребенка ({string.Join(" ", new List<string>() {child.LastName, child.FirstName, child.MiddleName}.Where(s => !string.IsNullOrEmpty(s)))})");
						}

						if (!DocumentTypeHelper.IsDocumentNumberValid(child.DocumentTypeId ?? 0, child.DocumentNumber))
						{
							res.Add(
								$"Указано некорректное значение номера документа ребенка ({string.Join(" ", new List<string>() {child.LastName, child.FirstName, child.MiddleName}.Where(s => !string.IsNullOrEmpty(s)))})");
						}
					}
				}

				if (data.Attendants != null)
				{
					foreach (var attendant in data.Attendants.Where(c => c.IsLast).ToList())
					{
						if (!DocumentTypeHelper.IsDocumentSeriaValid(attendant.DocumentTypeId ?? 0, attendant.DocumentSeria))
						{
							res.Add(
								$"Указано некорректное значение серии документа сопровождающего ({string.Join(" ", new List<string>() {attendant.LastName, attendant.FirstName, attendant.MiddleName}.Where(s => !string.IsNullOrEmpty(s)))})");
						}

						if (!DocumentTypeHelper.IsDocumentNumberValid(attendant.DocumentTypeId ?? 0, attendant.DocumentNumber))
						{
							res.Add(
								$"Указано некорректное значение номера документа сопровождающего ({string.Join(" ", new List<string>() {attendant.LastName, attendant.FirstName, attendant.MiddleName}.Where(s => !string.IsNullOrEmpty(s)))})");
						}
					}
				}

				if (string.IsNullOrWhiteSpace(data.Responsible))
				{
					res.Add("ФИО ответственного за выезд не может быть пустым");
				}

				if (string.IsNullOrWhiteSpace(data.ResponsiblePhone))
				{
					res.Add("Телефон ответственного за выезд не может быть пустым");
				}

				var doubles = FindDoubles(id);
				var requestsAtSameTime = FindSameTimeRequests(id);
				var attendantsAtSameTime = FindSameTimeAttendants(id);
            var attendantsSame = FindApplicantDoubles(id);


            if (doubles != null && doubles.Any())
				{
					res.Add("Каждый ребенок может быть только в одном списке");
				}

				if (requestsAtSameTime != null && requestsAtSameTime.Any())
				{
					res.Add("Каждый ребенок может быть только в одном списке или в заявлении в одно время заезда");
				}

				if (attendantsAtSameTime != null && attendantsAtSameTime.Any())
				{
					res.Add("Каждый сопровождающий может быть только в списке или в заявлении в одно время заезда");
				}

            if (attendantsSame != null && attendantsSame.Any())
            {
               res.Add("Сопровождающий в списке не может повторяться");
            }

         }

         if (stateCode == AccessRightEnum.Limit.List.IncludedPayment)
			{
				if (data.NullSafe(d => d.LimitOnOrganization.StateId) != StateMachineStateEnum.Limit.Organization.Confirmed)
				{
					res.Add("Квота организации должна быть утверждена ОИВ");
				}
			}

			if (stateCode == AccessRightEnum.Limit.List.Formation)
			{
				if (data.NullSafe(d => d.LimitOnOrganization.StateId) != StateMachineStateEnum.Limit.Organization.Brought)
				{
					res.Add("Нельзя редактировать список если статус квоты не равен 'Доведены до организации'");
				}
			}

			return res;
		}


		/// <summary>
		///     удалить список детей.
		/// </summary>
		/// <param name="id"></param>
		internal object DeleteListChild(long id)
		{
			SetUnitOfWorkInRefClass(UnitOfWork);
			using (var tran = UnitOfWork.GetTransactionScope())
			{
				var entity = UnitOfWork.GetById<ListOfChilds>(id);
				var p = new
				{
					yearOfRestId = entity.LimitOnOrganization.LimitOnVedomstvo.YearOfRestId,
					limitOfOrganizationId = entity.LimitOnOrganizationId
				};

				entity.StateId = StateMachineStateEnum.Deleted;
				entity.IsDeleted = true;
				foreach (var child in entity.Childs)
				{
					child.IsLast = false;
				}

				foreach (var attendant in entity.Attendants)
				{
					attendant.IsLast = false;
				}

				if (entity.HistoryLink == null)
				{
					entity.HistoryLink = HistoryController.CreateHistoryLink();
				}

				HistoryController.InsertHistoryItem(entity.HistoryLink,
					new History() {EventCode = "Удаление", DateChange = DateTime.Now});

				UnitOfWork.SaveChanges();

				tran.Complete();
				return p;
			}
		}

		/// <summary>
		///     изменение статуса списка детей организации
		/// </summary>
		internal bool ChangeListChildState(long id, string stateCode)
		{
			var errors = CheckListChildStateChange(id, stateCode);

			var doubles = FindDoubles(id);
			var requestsAtSameTime = FindSameTimeRequests(id);
			var sameChildren = (doubles != null && doubles.Any()) || (requestsAtSameTime != null && requestsAtSameTime.Any());

			if (errors.Any())
			{
				return false;
			}

			if (sameChildren && stateCode != AccessRightEnum.Limit.List.Formation)
			{
				return false;
			}

			var entity = UnitOfWork.GetById<ListOfChilds>(id);

			var state = StateController.GetNextState(entity.StateId, stateCode);

			if (state != null && !CheckListChildStateChange(id, stateCode).Any())
			{
				var link = entity.HistoryLink ?? new HistoryLink();
				if (link.Id == 0)
				{
					link = UnitOfWork.AddEntity(link);
					entity.HistoryLinkId = link.Id;
				}

				UnitOfWork.AddEntity(new History
				{
					AccountId = Security.GetCurrentAccountId(),
					DateChange = DateTime.Now,
					LinkId = link.Id,
					EventCode = $"Изменение статуса с '{entity.State.Name}' на '{state.Name}'"
				});


				if (AccessRightEnum.Limit.List.Formation == stateCode &&
				    entity.LimitOnOrganization.StateId == StateMachineStateEnum.Limit.Organization.ToApprove)
				{
					ChangeOrganizationListState(entity.LimitOnOrganizationId ?? 0, AccessRightEnum.Limit.Organization.Brought);
				}

				entity.StateId = state.Id;
				ApiCertificateController.GenerateCertificateNumber(entity);

				entity.ForIndex = true;
				UnitOfWork.Update(entity);

				return true;
			}

			return false;
		}

		#endregion

		#region Работа с квотами организации

		internal List<string> CheckOrganizationLimitStateChange(long id, string stateCode)
		{
			var res = new List<string>();
			var data = UnitOfWork.GetById<LimitOnOrganization>(id);

			if (
				!new[] {AccessRightEnum.Limit.Organization.Formation, AccessRightEnum.Limit.Organization.OnCompletion}.Contains(
					stateCode))
			{
				if (new[]
				{
					(long?) StateMachineStateEnum.Limit.Oiv.Formation
				}.Contains(data.LimitOnVedomstvo.StateId))
				{
					res.Add("Размер квоты ОИВ не утвержден.");
				}
			}

			if (AccessRightEnum.Limit.Organization.Brought == stateCode)
			{
				if (data.Volume <= 0)
				{
					res.Add("Нельзя утвердить квоту нулевого размера.");
				}

				var limitApproved =
					UnitOfWork.GetSet<LimitOnOrganization>()
						.Where(
							l =>
								l.LimitOnVedomstvoId == data.LimitOnVedomstvoId && l.StateId != StateMachineStateEnum.Deleted &&
								l.StateId != StateMachineStateEnum.Limit.Organization.Formation)
						.Select(l => l.Volume).DefaultIfEmpty()
						.Sum();

				if (data.Volume + limitApproved > data.LimitOnVedomstvo.Volume)
				{
					res.Add("Нельзя утвердить квоты организации больше квоты ведомства");
				}
			}

			if (AccessRightEnum.Limit.Organization.ToApprove == stateCode)
			{
				var countApproveChild =
					UnitOfWork.GetSet<ListOfChilds>()
						.Where(
							c =>
								c.LimitOnOrganizationId == id && !c.IsDeleted && c.IsLast &&
								(c.StateId == StateMachineStateEnum.Limit.List.Formed ||
								 c.StateId == StateMachineStateEnum.Limit.List.IncludedPayment))
						.Select(c => c.CountChild).DefaultIfEmpty()
						.Sum();
				if (countApproveChild != data.Volume)
				{
					res.Add("Размер квоты должен быть равен общему размеру списков");
				}

				if (UnitOfWork.GetSet<ListOfChilds>().Any(c =>
					c.LimitOnOrganizationId == id && !c.IsDeleted && c.IsLast &&
					c.StateId == StateMachineStateEnum.Limit.List.Formation))
				{
					res.Add("Нельзя отправить на утверждение в ОИВ квоту с неутвержденными списками");
				}
			}

			return res;
		}

		/// <summary>
		///     изменение статуса списка организации
		/// </summary>
		internal bool ChangeOrganizationListState(long id, string stateCode, long? signInfoId = null,
			string commentary = null)
		{
			var errors = CheckOrganizationLimitStateChange(id, stateCode);

			if (errors.Any() || !Security.HasRight(stateCode))
			{
				return false;
			}

			var entity = UnitOfWork.GetById<LimitOnOrganization>(id);
			var state = StateController.GetNextState(entity.StateId, stateCode);

			if (state != null)
			{
				//if (state.Id == StateMachineStateEnum.Limit.Organization.Confirmed)
				//{
				//	ApiCalculationController.UpdateCalculation(entity);
				//}

				var childLists =
					UnitOfWork.GetSet<ListOfChilds>()
						.Where(l => l.StateId.HasValue && !l.IsDeleted && l.StateId != StateMachineStateEnum.Deleted &&
						            l.LimitOnOrganizationId == id)
						.ToList();
				foreach (var cl in childLists)
				{
					if (!cl.ForIndex)
					{
						cl.ForIndex = true;
						UnitOfWork.SaveChanges();
					}
				}

				entity.HistoryLink = this.WriteHistory(entity.HistoryLink,
					$"Изменение статуса с '{entity.State.Name}' на '{state.Name}'", commentary, state.Id, entity.StateId, signInfoId);
				entity.HistoryLinkId = entity.HistoryLink?.Id;
				entity.LastUpdateTick = DateTime.Now.Ticks;
				entity.StateId = state.Id;
				entity.SignInfoId = signInfoId;
				UnitOfWork.Update(entity);
				UnitOfWork.SaveChanges();

				return true;
			}

			return false;
		}

		#endregion

		#region Работа с квотами ведомства

		internal List<string> CheckVedomstvoLimitStateChange(long id, string stateCode)
		{
			var res = new List<string>();
			var data = UnitOfWork.GetById<LimitOnVedomstvo>(id);

			if (AccessRightEnum.Limit.Oiv.Brought == stateCode)
			{
				if (data.Volume <= 0)
				{
					res.Add("Нельзя утвердить квоту нулевого размера.");
				}
			}

			if (AccessRightEnum.Limit.Oiv.BroughtToOrganization == stateCode)
			{
				var limitVedomstvo =
					UnitOfWork.GetSet<LimitOnOrganization>()
						.Where(l => l.LimitOnVedomstvoId == id && l.StateId.HasValue && l.StateId != StateMachineStateEnum.Deleted)
						.Select(l => l.Volume)
						.DefaultIfEmpty()
						.Sum();

				if (data.Volume != limitVedomstvo)
				{
					res.Add("Размер квоты по организациям не соответствует общему размеру квоты по ведомству");
				}
			}

			return res;
		}


		/// <summary>
		///     изменение статуса списка ОИВ
		/// </summary>
		internal bool ChangeVedomstvoListState(long id, string stateCode, long? signInfoId = null, string commentary = null)
		{
			var errors = CheckVedomstvoLimitStateChange(id, stateCode);

			if (errors.Any())
			{
				return false;
			}

			var entity = UnitOfWork.GetById<LimitOnVedomstvo>(id);

			var state = StateController.GetNextState(entity.StateId, stateCode);

			if (state != null)
			{
				entity.HistoryLink = this.WriteHistory(entity.HistoryLink,
					$"Изменение статуса с '{entity.State.Name}' на '{state.Name}'", commentary, state.Id,
					entity.StateId);
				entity.HistoryLinkId = entity.HistoryLink?.Id;
				UnitOfWork.SaveChanges();

				if (stateCode == AccessRightEnum.Limit.Oiv.BroughtToOrganization)
				{
					var los = UnitOfWork.GetSet<LimitOnOrganization>()
						.Where(l => l.LimitOnVedomstvoId == id && l.StateId.HasValue && l.StateId != StateMachineStateEnum.Deleted)
						.Select(l => l.Id).ToList();

					foreach (var loid in los)
					{
						ChangeOrganizationListState(loid, AccessRightEnum.Limit.Organization.Brought);
					}
				}

				if (stateCode == AccessRightEnum.Limit.Oiv.OnCompletion)
				{
					var los = UnitOfWork.GetSet<LimitOnOrganization>()
						.Where(l => l.LimitOnVedomstvoId == id && l.StateId.HasValue && l.StateId != StateMachineStateEnum.Deleted)
						.Select(l => l.Id).ToList();

					foreach (var loid in los)
					{
						ChangeOrganizationListState(loid, AccessRightEnum.Limit.Organization.Formation);
					}
				}

				entity = UnitOfWork.GetById<LimitOnVedomstvo>(id);

				entity.StateId = state.Id;
				entity.SignInfoId = signInfoId;
				UnitOfWork.Update(entity);

				return true;
			}

			return false;
		}

		#endregion

		/// <summary>
		/// список одарённых детей.
		/// </summary>
		internal GiftedChildrenModel GiftedChildren(GiftedChildrenModel model)
		{
			int pageSize = Settings.Default.TablePageSize;
			int startRecord = (model.PageNumber - 1) * pageSize;
			var name = (model.Name ?? string.Empty).ToLower();

			IQueryable<Child> query =
				UnitOfWork.GetSet<Child>()
					.Include(e => e.ChildList)
					.Include(e => e.ChildList.LimitOnOrganization)
					.Include(e => e.ChildList.LimitOnOrganization.Organization)
					.Include(e => e.ChildList.LimitOnOrganization.LimitOnVedomstvo)
					.Include(e => e.ChildList.LimitOnOrganization.LimitOnVedomstvo.Organization)
					.Include(e => e.ChildList.LimitOnOrganization.LimitOnVedomstvo.YearOfRest)
					.Include(e => e.ChildList.Tour)
					.Include(e => e.ChildList.Tour.State)
					.Where(e => e.IsLast && !e.ChildList.IsDeleted
					                     && e.ChildListId.HasValue
					                     && e.ChildList.StateId != StateMachineStateEnum.Deleted
					                     && e.ChildList.LimitOnOrganizationId.HasValue
					                     && e.ChildList.LimitOnOrganization.StateId != StateMachineStateEnum.Deleted
					                     && e.ChildList.LimitOnOrganization.LimitOnVedomstvoId.HasValue
					                     && e.ChildList.LimitOnOrganization.LimitOnVedomstvo.StateId != StateMachineStateEnum.Deleted
					                     && e.ChildList.LimitOnOrganization.LimitOnVedomstvo.YearOfRestId.HasValue
					);

			if (!string.IsNullOrEmpty(name))
			{
				query = query.Where(e =>
					e.LastName.ToLower().Contains(name) || e.MiddleName.ToLower().Contains(name) ||
					e.FirstName.ToLower().Contains(name));
			}

			if (model.YearId.HasValue && model.YearId != 0)
			{
				query = query.Where(e => e.ChildList.LimitOnOrganization.LimitOnVedomstvo.YearOfRestId == model.YearId);
			}

			if (model.VedomstvoId.HasValue && model.VedomstvoId != 0)
			{
				query = query.Where(e => e.ChildList.LimitOnOrganization.LimitOnVedomstvo.OrganizationId == model.VedomstvoId);
			}

			if (!model.Included)
			{
				query = query.Where(e => !e.IncludeReasonId.HasValue);
			}

			if (!model.Excluded)
			{
				query = query.Where(e => !e.ExcludeReasonId.HasValue);
			}

			int totalCount = query.Count();

			List<Child> entity = query.OrderBy(e => e.Id).Skip(startRecord).Take(pageSize).ToList();

			return new GiftedChildrenModel(entity, model.PageNumber, pageSize, totalCount)
			{
				Name = model.Name,
				VedomstvoId = model.VedomstvoId,
				YearId = model.YearId,
				Included = model.Included,
				Excluded = model.Excluded
			};
		}

		#region Работа с общим списком

		/// <summary>
		/// исключить ребенка из списка
		/// </summary>
		[HttpPost]
		public GeneralResponse ExcludeChildFromList([FromBody] ChildIncludeExcludeReason reason)
		{
			var ie = new ChildIncludeExcludeReason
			{
				OperartionDate = DateTime.Now,
				Reason = reason.Reason,
				AccountId = Security.GetCurrentAccountId()
			};

			using (var tran = UnitOfWork.GetTransactionScope())
			{
				ie = UnitOfWork.AddEntity(ie);
				var child = UnitOfWork.GetById<Child>(reason.Id);
				child.ExcludeReasonId = ie.Id;
				child.ExcludeReason = ie;
				UnitOfWork.SaveChanges();
				tran.Complete();
			}

			return new GeneralResponse();
		}

		/// <summary>
		/// включить ребенка в списка
		/// </summary>
		public GeneralResponse IncludeChildToList(Child model)
		{
			using (var tran = UnitOfWork.GetTransactionScope())
			{
				var ie = model.IncludeReason ?? new ChildIncludeExcludeReason {Reason = "Не указана"};
				ie.AccountId = Security.GetCurrentAccountId();
				ie.OperartionDate = DateTime.Now;
				ie = UnitOfWork.AddEntity(ie);

				model.IncludeReason = null;
				model.IncludeReasonId = ie.Id;
				model.IsDeleted = false;
				model.IsLast = true;

				UnitOfWork.AddEntity(model);
				tran.Complete();
			}

			return new GeneralResponse();
		}

		#endregion

		[HttpPost]
		public void SavePaymentLinkToFile(long childId, string url, string title)
		{
			var child = UnitOfWork.GetById<Child>(childId);
			child.PaymentFileTitle = title;
			child.PaymentFileUrl = url;
			UnitOfWork.SaveChanges();
		}

		[HttpPost]
		public void SavePaymentLinkAttendantToFile(long attendantId, string url, string title)
		{
			var child = UnitOfWork.GetById<Applicant>(attendantId);
			child.PaymentFileTitle = title;
			child.PaymentFileUrl = url;
			UnitOfWork.SaveChanges();
		}

      /// <summary>
      /// Поиск заявок для копирования отдыхающих
      /// </summary>
      /// <param name="id"></param>

      internal List<ListofLimitsToCopy> FindLimitsToCopy(long id)
      {
         if(id < 1)
         {
            return new List<ListofLimitsToCopy>(0);
         }

         var baselist = UnitOfWork.GetSet<ListOfChilds>().Where(ss => ss.Id == id).Select(ss => new { ss.LimitOnOrganization.OrganizationId, ss.TimeOfRest.Year }).First();
         IQueryable<ListOfChilds> listOfChilds = UnitOfWork.GetSet<ListOfChilds>();
         listOfChilds = listOfChilds.Where(ss => ss.Id != id && !ss.IsDeleted);
         listOfChilds = listOfChilds.Where(ss => ss.LimitOnOrganization.OrganizationId == baselist.OrganizationId);
         listOfChilds = listOfChilds.Where(ss => ss.TimeOfRest.Year == baselist.Year);

         var result = listOfChilds.ToList();

         return result.Select(ss => new ListofLimitsToCopy()
         {
            ListId = ss.Id,
            Childreninlist = ss.Childs.Count(),
            DateIncome = (ss.Tour ?? ss.LimitOnOrganization.Tour).DateIncome,
            DateOutcome = (ss.Tour ?? ss.LimitOnOrganization.Tour).DateOutcome,
            LimitType = ss.TypeOfLimitList.Name,
            LimitVolume = ss.LimitOnOrganization.Volume,
            PlaceOfRest = (ss.Tour ?? ss.LimitOnOrganization.Tour).Hotels.Name,
            PlaceOfRestAdress = (ss.Tour ?? ss.LimitOnOrganization.Tour).Hotels.Address
         }).ToList();
      }

      internal void MergeChildren(long From, long To)
      {
         IQueryable<ListOfChilds> listOfChilds = UnitOfWork.GetSet<ListOfChilds>();
         var listTo = listOfChilds.GetById(To).Childs.ToList();
         var listFrom = listOfChilds.GetById(From).Childs.ToList();

         foreach (var child in listFrom)
         {
            if(!listTo.Any(ss =>
            //doc num + seria
            (string.Equals(ss.DocumentSeria, child.DocumentSeria, StringComparison.OrdinalIgnoreCase) && string.Equals(ss.DocumentNumber, child.DocumentNumber, StringComparison.OrdinalIgnoreCase))
            ||
            // fio + date birth
            (
            ss.DateOfBirth == child.DateOfBirth
            && string.Equals(ss.FirstName, child.FirstName, StringComparison.OrdinalIgnoreCase)
            && string.Equals(ss.LastName, child.LastName, StringComparison.OrdinalIgnoreCase)
            && string.Equals(ss.MiddleName, child.MiddleName, StringComparison.OrdinalIgnoreCase))
            ))
            {
               var _newChild = new Child(child, 0);
               _newChild.AddressId = null;
               _newChild.ChildListId = To;
               _newChild.Address = new Address(child.Address, 0);
               UnitOfWork.AddEntity(_newChild);
            }
         }

         UnitOfWork.SaveChanges();
      }

      /// <summary>
      /// Поиск дублей
      /// </summary>
      /// <param name="id"></param>
      internal List<Child> FindDoubles(long id)
		{
			IQueryable<ListOfChilds> listOfChilds = UnitOfWork.GetSet<ListOfChilds>();
			var list = listOfChilds.GetById(id);

         if (list == null)
         {
         	return new List<Child>();
         }

         bool isOrphan = list.TypeOfLimitListId == (long)TypeOfLimitListEnum.Orphan;


         var yearOfRest = list.NullSafe(l => l.Tour.YearOfRest.Year);
			var typeOfGroupCheckId = list.NullSafe(l => l.Tour.TypeOfRest.TypeOfGroupCheckId);
         var childrenInLists =
            listOfChilds.Where(
                  l => (l.Id == id || (l.StateId != StateMachineStateEnum.Limit.List.Formation && l.StateId.HasValue &&
                                       l.StateId != StateMachineStateEnum.Deleted)) && l.IsLast && !l.IsDeleted)
               .SelectMany(l => l.Childs);

         if (!isOrphan)
         {
            childrenInLists = childrenInLists.Where(
                  c => typeOfGroupCheckId != null ? (c.TypeOfGroupCheckId == typeOfGroupCheckId && c.YearOfCompany <= yearOfRest && c.YearOfCompany + c.TypeOfGroupCheck.Period - 1 >= yearOfRest)
                        : (!c.TypeOfGroupCheckId.HasValue && c.YearOfCompany == yearOfRest));
         }
         else
         {
            childrenInLists = childrenInLists.Where(c => c.ChildListId == id);
         }


         var lists = listOfChilds;

         var docDubles = childrenInLists.GroupBy(c => new { DocumentSeria = c.DocumentSeria.ToLower(), DocumentNumber = c.DocumentNumber.ToLower() })
               .Where(c => c.Count() > 1)
               .Where(
                  d =>
                     lists.Where(l => l.Id == id)
                        .SelectMany(l => l.Childs)
                        .Any(c => c.DocumentSeria.ToLower() == d.Key.DocumentSeria && c.DocumentNumber.ToLower() == d.Key.DocumentNumber))
               .SelectMany(g => g)
               .Where(c => !c.IsDeleted && c.IsLast)
               .ToList();

         var fioDubls = childrenInLists.GroupBy(c => new {
            Name = (c.LastName + c.FirstName + c.MiddleName).ToLower().Replace("-", string.Empty).Replace(" ", string.Empty).Replace("_", string.Empty),
            BirthDate = c.DateOfBirth,
            Organisation = listOfChilds.Where(sx => sx.Id == id & sx.Childs.Any(sy => sy.Id == c.Id)).Select(ss => ss.LimitOnOrganization.OrganizationId).FirstOrDefault()})
            .Where(c => c.Count() > 1)
            .Where(
               d =>
                  lists.Where(l => l.Id == id).Any(c =>
                     c.LimitOnOrganization.OrganizationId == d.Key.Organisation
                     && c.Childs.Any(cc => (cc.LastName + cc.FirstName + cc.MiddleName).ToLower().Replace("-", string.Empty).Replace(" ", string.Empty).Replace("_", string.Empty) == d.Key.Name && cc.DateOfBirth == d.Key.BirthDate)
                  )
            )
            .SelectMany(g => g)
            .Where(c => !c.IsDeleted && c.IsLast)
            .ToList();


         return docDubles.Union(fioDubls).ToList();
      }

      /// <summary>
      /// Поиск дублей среди сопровождающих
      /// </summary>
      /// <param name="id"></param>
      internal List<Applicant> FindApplicantDoubles(long id)
      {
         IQueryable<ListOfChilds> listOfChilds = UnitOfWork.GetSet<ListOfChilds>();
         var list = listOfChilds.GetById(id);

         if (list == null)
         {
            return new List<Applicant>();
         }

         var applicantsInLists =
            listOfChilds.Where(
                  l => (l.Id == id || (l.StateId != StateMachineStateEnum.Limit.List.Formation && l.StateId.HasValue &&
                                       l.StateId != StateMachineStateEnum.Deleted)) && l.IsLast && !l.IsDeleted)
               .SelectMany(l => l.Attendants);

         applicantsInLists = applicantsInLists.Where(c => c.ChildListId == id);

         var lists = listOfChilds;

         var docDubles = applicantsInLists.GroupBy(c => new { c.DocumentSeria, c.DocumentNumber })
               .Where(c => c.Count() > 1)
               .Where(
                  d =>
                     lists.Where(l => l.Id == id)
                        .SelectMany(l => l.Attendants)
                        .Any(c => c.DocumentSeria == d.Key.DocumentSeria && c.DocumentNumber == d.Key.DocumentNumber))
               .SelectMany(g => g)
               .Where(c => !c.IsDeleted && c.IsLast)
               .ToList();

         var fioDubls = applicantsInLists.GroupBy(c => new {
            Name = (c.LastName + c.FirstName + c.MiddleName).ToLower().Replace("-", string.Empty).Replace(" ", string.Empty).Replace("_", string.Empty),
            BirthDate = c.DateOfBirth,
            Organisation = listOfChilds.Where(sx => sx.Id == id & sx.Attendants.Any(sy => sy.Id == c.Id)).Select(ss => ss.LimitOnOrganization.OrganizationId).FirstOrDefault()
         })
            .Where(c => c.Count() > 1)
            .Where(
               d =>
                  lists.Where(l => l.Id == id).Any(c =>
                     c.LimitOnOrganization.OrganizationId == d.Key.Organisation
                     && c.Attendants.Any(cc => (cc.LastName + cc.FirstName + cc.MiddleName).ToLower().Replace("-", string.Empty).Replace(" ", string.Empty).Replace("_", string.Empty) == d.Key.Name && cc.DateOfBirth == d.Key.BirthDate)
                  )
            )
            .SelectMany(g => g)
            .Where(c => !c.IsDeleted && c.IsLast)
            .ToList();


         return docDubles.Union(fioDubls).ToList();
      }


      /// <summary>
      /// Поиск похожих детей
      /// </summary>
      /// <param name="id"></param>
      /// <returns></returns>
      internal List<Child> FindSimilar(long id)
		{
			IQueryable<ListOfChilds> listOfChilds = UnitOfWork.GetSet<ListOfChilds>();
			var list = listOfChilds.GetById(id);
			var yearOfRest = list.NullSafe(l => l.Tour.YearOfRest.Year);
			var typeOfGroupCheckId = list.NullSafe(l => l.Tour.TypeOfRestId);
			var childrenInLists =
				listOfChilds.Where(
						l => l.Id == id || l.StateId != StateMachineStateEnum.Limit.List.Formation && l.IsLast && !l.IsDeleted)
					.SelectMany(l => l.Childs)
					.Where(
						c =>
							typeOfGroupCheckId != null
								? (c.TypeOfGroupCheckId == typeOfGroupCheckId && c.YearOfCompany <= yearOfRest &&
								   c.YearOfCompany + c.TypeOfGroupCheck.Period - 1 >= yearOfRest)
								: (!c.TypeOfGroupCheckId.HasValue && c.YearOfCompany == yearOfRest));
			var lists = listOfChilds;
			return
				childrenInLists.GroupBy(c => c.KeySame)
					.Where(c => c.Count() > 1)
					.Where(d => lists.Where(l => l.Id == id).SelectMany(l => l.Childs).Any(c => c.KeySame == d.Key))
					.SelectMany(g => g)
					.Where(c => !c.IsDeleted && c.IsLast)
					.ToList();
		}

      /// <summary>
      /// Поиск пересечений по времени с заявлениями (старая версия им. А.Н. Шинкевича)
      /// </summary>
      /// <param name="id"></param>
      /// <returns></returns>
      internal List<Child> FindSameTimeRequests_OLD(long id)
		{
			IQueryable<Child> children = UnitOfWork.GetSet<Child>();
			var q = children.Where(
				c =>
					c.IsLast && !c.IsDeleted &&
					children.Where(cc => cc.ChildListId == id)
						.Any(
							cc =>
								cc.EntityId != c.EntityId && c.DocumentNumber == cc.DocumentNumber && c.DocumentSeria == cc.DocumentSeria
								&& !FirstRequestCompanyExtension.RequestDeclineStatuses.Contains(c.Request.StatusId)
								&& !c.Request.IsDeleted
								&& c.IsLast
								&& c.IntervalStart < cc.IntervalEnd
								&& c.IntervalEnd > cc.IntervalStart));
			return q.ToList();
		}

      /// <summary>
      /// Поиск пересечений по времени с заявлениями
      /// </summary>
      /// <param name="id"></param>
      /// <returns></returns>
      internal List<Child> FindSameTimeRequests(long id)
      {
         /*
         var current_child_list_children = UnitOfWork.GetSet<ListOfChilds>().Where(l => l.Id == id).SelectMany(l => l.Childs).AsQueryable();

         var all_other_children = UnitOfWork.GetSet<ListOfChilds>().Where(l => l.Id != id).SelectMany(l => l.Childs)
            .Where(a => current_child_list_children.Any(cl =>
               (a.IntervalStart >= cl.IntervalStart && a.IntervalStart < cl.IntervalEnd) ||
               (a.IntervalEnd > cl.IntervalStart && a.IntervalEnd <= cl.IntervalEnd)));

         var all_children = current_child_list_children.Concat(all_other_children);

         var doc_doubles = all_children.GroupBy(a => new { DocumentSeria = a.DocumentSeria.ToLower(), DocumentNumber = a.DocumentNumber.ToLower() })
            .Where(g => g.Count() > 1)
            .SelectMany(g => g)
            .Where(c => !c.IsDeleted && c.IsLast)
            .ToList();

         var fio_doubles = all_children.GroupBy(a => new { Name = (a.LastName + a.FirstName + a.MiddleName).ToLower().Replace("-", string.Empty).Replace(" ", string.Empty).Replace("_", string.Empty).Replace("ё", "е"), BirthDate = a.DateOfBirth })
            .Where(g => g.Count() > 1)
            .SelectMany(g => g)
            .Where(c => !c.IsDeleted && c.IsLast)
            .ToList();


         return doc_doubles.Union(fio_doubles).ToList();
         */
         return new List<Child>();
      }

      /// <summary>
      /// Поиск пересечений для заявителей (старая версия им. А.Н. Шинкевича)
      /// </summary>
      /// <param name="id"></param>
      /// <returns></returns>
      internal List<Applicant> FindSameTimeAttendants_OLD(long id)
		{
			IQueryable<Request> requests =
				UnitOfWork.GetSet<Request>()
					.Where(r => !r.IsDeleted && !FirstRequestCompanyExtension.RequestDeclineStatuses.Contains(r.StatusId)
					                         && r.IsLast);

			var attendantsFromRequests = requests.SelectMany(r => r.Attendant);
			var applicantsFromRequests = requests.Where(a => a.Applicant.IsAccomp).Select(r => r.Applicant);
			var attendantsFromLists =
				UnitOfWork.GetSet<ListOfChilds>()
					.Where(l => !l.IsDeleted && l.IsLast && l.StateId != StateMachineStateEnum.Limit.List.Formation)
					.SelectMany(l => l.Attendants);
			var attendants = attendantsFromRequests.Concat(applicantsFromRequests).Concat(attendantsFromLists);
			var allAttendants = UnitOfWork.GetSet<Applicant>();

			return
				attendants.Where(
					a =>
						allAttendants.Where(ar => ar.ChildListId == id)
							.Any(
								ar => ar.Key == a.Key && ar.EntityId != a.EntityId && ar.IsLast && ar.IntervalStart <= a.IntervalEnd &&
								      ar.IntervalEnd >= a.IntervalStart)).ToList();
		}

      /// <summary>
      /// Поиск пересечений для заявителей
      /// </summary>
      /// <param name="id"></param>
      /// <returns></returns>
      internal List<Applicant> FindSameTimeAttendants(long id)
      {
         return new List<Applicant>();
         /*
         var current_child_list_applicants = UnitOfWork.GetSet<ListOfChilds>().Where(l => l.Id == id).SelectMany(l => l.Attendants).AsQueryable();

         var all_other_applicants = UnitOfWork.GetSet<ListOfChilds>().Where(l => l.Id != id).SelectMany(l => l.Attendants).Where(a => current_child_list_applicants.Any(cl => (a.IntervalStart >= cl.IntervalStart && a.IntervalStart < cl.IntervalEnd) || (a.IntervalEnd > cl.IntervalStart && a.IntervalEnd <= cl.IntervalEnd)));

         var all_applicants = current_child_list_applicants.Concat(all_other_applicants);

         var doc_doubles = all_applicants.GroupBy(a => new { DocumentSeria = a.DocumentSeria.ToLower(), DocumentNumber = a.DocumentNumber.ToLower() })
            .Where(g => g.Count() > 1)
            .SelectMany(g => g)
            .Where(c => !c.IsDeleted && c.IsLast)
            .ToList();

         var fio_doubles = all_applicants.GroupBy(a => new { Name = (a.LastName + a.FirstName + a.MiddleName).ToLower().Replace("-", string.Empty).Replace(" ", string.Empty).Replace("_", string.Empty).Replace("ё", "е"), BirthDate = a.DateOfBirth })
            .Where(g => g.Count() > 1)
            .SelectMany(g => g)
            .Where(c => !c.IsDeleted && c.IsLast)
            .ToList();


         return doc_doubles.Union(fio_doubles).ToList();
         */
      }




   }
}
