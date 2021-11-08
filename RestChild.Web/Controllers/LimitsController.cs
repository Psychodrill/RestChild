using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using RestChild.Comon;
using RestChild.Comon.Enumeration;
using RestChild.DAL.RepositoryExtensions;
using RestChild.Domain;
using RestChild.Web.Common;
using RestChild.Web.Controllers.WebApi;
using RestChild.Web.Models;
using RestChild.Web.Models.Limits;

namespace RestChild.Web.Controllers
{
	[Authorize]
	public partial class LimitsController : BaseController
	{
		public WebApi.LimitsController ApiController { get; set; }

		public WebVocabularyController VocController { get; set; }

		public WebApi.OrganizationController OrgController { get; set; }

		public StateController StateController { get; set; }

		public PdfController Pdf { get; set; }

		public WebBtiDistrictsController DistrictController { get; set; }

		public override void SetUnitOfWorkInRefClass(IUnitOfWork unitOfWork)
		{
			ApiController.SetUnitOfWorkInRefClass(unitOfWork);
			StateController.SetUnitOfWorkInRefClass(unitOfWork);
			VocController.SetUnitOfWorkInRefClass(unitOfWork);
			DistrictController.SetUnitOfWorkInRefClass(unitOfWork);
			Pdf.SetUnitOfWorkInRefClass(unitOfWork);
			OrgController.SetUnitOfWorkInRefClass(unitOfWork);
		}

		#region Работа со списками организаций

		[HttpGet]
		public ActionResult ListOfChildsList(long? limitOnOrganizationId, string stateCode)
		{
			SetUnitOfWorkInRefClass();
			return
				ListOfChildsList(new ListOfChildsListModel
				{
					LimitOnOrganizationId = limitOnOrganizationId,
					StringStateCode = stateCode
				});
		}

		[HttpPost]
		public ActionResult ListOfChildsList(ListOfChildsListModel model)
		{
			SetUnitOfWorkInRefClass();
			if (!Security.HasRight(AccessRightEnum.Limits.LimitToOiv) && !Security.HasRightForSomeOrganization(AccessRightEnum.Limits.LimitByOrganization) && !Security.HasRightForSomeOrganization(AccessRightEnum.Limits.LimitChildInOrganization))
			{
				return RedirectToAvalibleAction();
			}

			var res = Esep.SaveSignsIfNeed(UnitOfWork);
			if (res != null && res.Any())
			{
				var first = res.First();

				using (var tran = UnitOfWork.GetTransactionScope())
				{
					ApiController.ChangeOrganizationListState(first.EntityId, first.ActionCode, first.SignInfoId, first.Commentary);
					tran.Complete();
				}
			}

			model = model ?? new ListOfChildsListModel();
			model.YearsOfRest = ApiController.GetYearsOfRest();
			var newmodel = ApiController.ListOfChildsList(model);
			newmodel.YearsOfRest = model.YearsOfRest;

			if (!string.IsNullOrEmpty(model.StringStateCode) && newmodel.LimitOnOrganization != null)
			{
				newmodel.Errors = ApiController.CheckOrganizationLimitStateChange(newmodel.LimitOnOrganization.Id,
					model.StringStateCode);
			}

			if (newmodel.NullSafe(m => m.LimitOnOrganization.StateId) == StateMachineStateEnum.Limit.Organization.Brought && newmodel.LimitOnOrganization != null &&
				Security.HasRight(AccessRightEnum.Limits.LimitChildInOrganization, newmodel.LimitOnOrganization.OrganizationId ?? 0))
			{
				newmodel.State.PreNoStatusActions = newmodel.State.PreNoStatusActions ?? new List<NoStatusAction>();
				newmodel.State.PreNoStatusActions.Add(new NoStatusAction
				{
					Controller = "Limits",
					Action = "ListOfChildsEdit",
					ActionParameters = new { limitOrganizationId = newmodel.LimitOnOrganization.Id },
					IconClass = "glyphicon glyphicon-plus",
					ButtonClass = "btn btn-primary",
					Name = "Добавить список"
				});
			}

			if (newmodel.State != null && newmodel.LimitOnOrganization != null && newmodel.LimitOnOrganization.Id > 0)
			{
				newmodel.State.PostNoStatusActions = newmodel.State.PostNoStatusActions ?? new List<NoStatusAction>();
				newmodel.State.PostNoStatusActions.Add(

					new NoStatusAction
					{
						Name = "Печать",
						IconClass = "glyphicon-print",
						Controller = "Pdf",
						Action = "GetOrganizationChilds",
						ActionParameters = new {id = newmodel.LimitOnOrganization.Id},
					}
				);

				newmodel.State.PostNoStatusActions.Add(

					new NoStatusAction
					{
						Name = "Список отдыхающих",
						IconClass = "glyphicon-print",
						Controller = "Pdf",
						Action = "GetOrganizationChildsExcel",
						ActionParameters = new { id = newmodel.LimitOnOrganization.Id },
					}
				);
			}

			if (newmodel.LimitOnOrganization?.HistoryLinkId != null && newmodel.State != null)
			{
				newmodel.State.PostNoStatusActions = newmodel.State.PostNoStatusActions ?? new List<NoStatusAction>();
				newmodel.State.PostNoStatusActions.Add(new NoStatusAction
				{
					Name = "История",
					ButtonClass = "btn btn-default btn-hystory-link",
					SomeAddon = $"data-history-id=\"{newmodel.LimitOnOrganization.HistoryLinkId}\""
				});
			}

			return View(newmodel);
		}

		/// <summary>
		/// изменение статуса списка.
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		public ActionResult ListOfChildsChangeStatus(ListOfChildsListModel model)
		{
			SetUnitOfWorkInRefClass();
			if (model.LimitOnOrganizationId.HasValue && model.LimitOnOrganizationId > 0 && !string.IsNullOrEmpty(model.StringStateCode))
			{
				var action = StateController.GetAction(model.StringStateCode);
				if (action != null && action.NeedSign)
				{
					var errors = ApiController.CheckOrganizationLimitStateChange(model.LimitOnOrganizationId.Value, model.StringStateCode);
					if (errors != null && errors.Any())
					{
						return RedirectToAction("ListOfChildsList",
							new
							{
								limitOnOrganizationId = model.LimitOnOrganizationId,
								stateCode = model.StringStateCode
							});
					}

					var data = Pdf.GetDataForSign(model.LimitOnOrganizationId.Value, SignTypeEnum.ListOfOrganization);
					if (data != null)
					{
						data.ActionCode = model.StringStateCode;
						data.Commentary = model.StringCommentaryCode;
						var esep = new Esep();
						var upload = esep.UploadFilesToEsep(new List<DataForSign> { data });
						var url = esep.UrlToEsep(upload.Select(u => u.FileAccessCode).ToList(), Esep.FullReturnUrl(Url.Action("ListOfChildsList", "Limits", new
						{
							limitOnOrganizationId = model.LimitOnOrganizationId,
						})), Guid.NewGuid().ToString());

						return Redirect(url);
					}
				}

				var result = ApiController.ChangeOrganizationListState(model.LimitOnOrganizationId.Value, model.StringStateCode, null,
					model.StringCommentaryCode);
				UnitOfWork.SaveChanges();

				return RedirectToAction("ListOfChildsList",
					new
					{
						limitOnOrganizationId = model.LimitOnOrganizationId,
						stateCode = result ? string.Empty : model.StringStateCode
					});
			}

			return RedirectToAction("Organization");
		}

		[HttpGet]
		public ActionResult Organization(long? organizationId, long? yearOfRestId)
		{
			SetUnitOfWorkInRefClass();
			return Organization(new OrganizationModel { OrganizationId = organizationId, YearOfRestId = yearOfRestId });
		}

		[HttpPost]
		public ActionResult Organization(OrganizationModel model)
		{
			SetUnitOfWorkInRefClass();
			if (!Security.HasRight(AccessRightEnum.Limits.LimitToOiv) && !Security.HasRightForSomeOrganization(AccessRightEnum.Limits.LimitByOrganization) && !Security.HasRightForSomeOrganization(AccessRightEnum.Limits.LimitChildInOrganization))
			{
				return RedirectToAvalibleAction();
			}

			model = model ?? new OrganizationModel();

			model.ListOfYears = ApiController.GetYearsOfRest().ToList();

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

			if (model.OrganizationId.HasValue && string.IsNullOrEmpty(model.OrganizationName))
			{
				var org = UnitOfWork.GetById<Organization>(model.OrganizationId.Value);
				model.OrganizationName = org.Name;
			}

			if (!model.YearOfRestId.HasValue)
			{
				var curYear = model.ListOfYears.FirstOrDefault(y => y.Year == DateTime.Now.Year) ??
				              model.ListOfYears.LastOrDefault();
				if (curYear != null)
				{
					model.YearOfRestId = curYear.Id;
				}
			}

			if (model.YearOfRestId.HasValue && model.OrganizationId.HasValue)
			{
				model.Items = ApiController.GetTourOrganizationList(model.YearOfRestId.Value, model.OrganizationId.Value);
			}
			else
			{
				model.Items = new GroupedLimitItemModel{SubGroups = new List<GroupedLimitItemModel>(), Items = new List<LimitItemModel>()};
			}

			return View(model);
		}

      /// <summary>
      /// редактирование "списка детей"
      /// </summary>
		public ActionResult ListOfChildsEdit(long? id, long? limitOrganizationId, string stateCode)
		{
			SetUnitOfWorkInRefClass();
			if (!Security.HasRight(AccessRightEnum.Limits.LimitToOiv) && !Security.HasRightForSomeOrganization(AccessRightEnum.Limits.LimitByOrganization) && !Security.HasRightForSomeOrganization(AccessRightEnum.Limits.LimitChildInOrganization))
			{
				return RedirectToAvalibleAction();
			}

			var list = ApiController.GetListOfChild(id ?? 0) ?? new ListOfChilds
			{
				StateId = StateMachineStateEnum.Limit.List.Formation,
				Childs = new List<Child>(),
				Attendants = new List<Applicant>(),
				LimitOnOrganization = ApiController.GetLimitOnOrganization(limitOrganizationId??0),
				IsDeleted = false,
				IsLast = true,
				CountChild = 0,
				CountAttendants = 0,
			};

			if (list.Id == 0 && list.LimitOnOrganization != null)
			{
				var tour = list.LimitOnOrganization.Tour;
				if (tour != null)
				{
					list.Tour = tour;
					list.TourId = tour.Id;
					list.PlaceOfRestId = tour.Hotels.NullSafe(h => h.PlaceOfRestId);
					list.PlaceOfRest = tour.Hotels.NullSafe(h => h.PlaceOfRest);
					list.TimeOfRestId = tour.TimeOfRestId;
					list.TimeOfRest = tour.TimeOfRest;
				}

				list.LimitOnOrganizationId = list.LimitOnOrganization.Id;
				list.TypeOfLimitListId = list.LimitOnOrganization.TypeOfLimitListId;
			}
			else if (list.LimitOnOrganization == null)
			{
				return RedirectToAction("ListOfChildsList");
			}

			if (list.State == null)
			{
				list.State = StateController.GetState(list.StateId ?? StateMachineStateEnum.Limit.List.Formation);
			}

			if (list.Id == 0)
			{
				var limits = ApiController.GetLimitForChildList(string.Empty, null).ToList();
				if (!limits.Any())
				{
					return RedirectToAction("ListOfChildsList");
				}

				if (limits.Count == 1)
				{
					list.LimitOnOrganization = limits.FirstOrDefault().NullSafe(l=>l.LimitOnOrganization);
					list.LimitOnOrganizationId = list.LimitOnOrganization?.Id;
				}
			}

			var param = list.LimitOnOrganization != null
				? new
				{
					yearOfRestId = list.LimitOnOrganization.LimitOnVedomstvo.YearOfRestId,
					limitOfOrganizationId = list.LimitOnOrganizationId
				}
				: null;

			var docs = VocController.GetDocumentsType();
			var childIds = list.Childs?.Select(c => c.Id).ToList() ?? new List<long>();
			var attendantIds = list.Attendants?.Select(c => c.Id).ToList() ?? new List<long>();
			var addonServices =
				UnitOfWork.GetSet<AddonServicesLink>()
					.Where(
						l =>
						(l.ChildId.HasValue && childIds.Contains(l.ChildId.Value))
						|| (l.ApplicantId.HasValue && attendantIds.Contains(l.ApplicantId.Value)))
					.ToList();

			if (list.Childs != null)
			{
				foreach (var child in list.Childs)
				{
					child.AddonServices =
						addonServices.Where(s => s.ChildId == child.Id && s.AddonServices != null).Select(s => s.AddonServices).Distinct().ToList();
				}
			}

			if (list.Attendants != null)
			{
				foreach (var attendant in list.Attendants)
				{
					attendant.AddonServices =
						addonServices.Where(s => s.ApplicantId == attendant.Id && s.AddonServices != null).Select(s => s.AddonServices).Distinct().ToList();
				}
			}

			var addonServiceses = list.Tour != null
				? UnitOfWork.GetSet<AddonServices>()
					.Where(
						s =>
							s.Hotels.Any(h => h.Id == list.Tour.HotelsId) && s.IsActive &&
							s.StateId != StateMachineStateEnum.AddonService.Forming && s.StateId != StateMachineStateEnum.Deleted &&
							!s.ForForeign)
					.ToList()
				: new List<AddonServices>();

			var postNoStatusActions = new List<NoStatusAction>();
			if (list.StateId != StateMachineStateEnum.Limit.List.Formation && !list.IsDeleted && list.Childs != null && list.Childs.Any(c => !c.IsDeleted && c.Payed))
			{
				postNoStatusActions.Add(new NoStatusAction
											{
												Action = "GetCertificateForChildList",
												Controller = "Pdf",
												Name = "Путевка",
												IconClass = "glyphicon glyphicon-print",
												ActionParameters = new { childListId = list.Id }
											});
			}

         if (list.StateId == StateMachineStateEnum.Limit.List.Formation && !list.IsDeleted && list.Id > 0)
         {
            postNoStatusActions.Add(new NoStatusAction
            {
               Name = "Скопировать отдыхающих",
               JsFunction = "GroupCopyDialog()"
            });
         }

         var actions = StateController.GetActions(list.State, StateMachineEnum.LimitListState)
				.Where(
					s =>
						s.ToStateId != StateMachineStateEnum.Limit.List.IncludedPayment ||
						list.LimitOnOrganization.StateId == StateMachineStateEnum.Limit.Organization.Confirmed)
				.ToList();

			if (list.TypeOfLimitListId == (long) TypeOfLimitListEnum.Orphan)
			{
				actions.RemoveAll(s => s.ToStateId == StateMachineStateEnum.Limit.List.IncludedPayment);
			}

			var model = new ListsOfChildModel(list)
			{
				TimeOfRests = VocController.GetTimesOfRestWithoutFilter((long) TypeOfRestEnum.ChildRest),
				TypeOfRestrictions = UnitOfWork.GetSet<TypeOfRestriction>().Where(t=>t.IsActive).ToList(),
				ListOfChildsCategorys = UnitOfWork.GetSet<ListOfChildsCategory>().ToList(),
				PlaceOfRests = VocController.GetPlacesOfRestInternal(true),
				DocumentTypesAttendat = docs.Where(d => d.ForOther && !d.ForForeign).ToList(),
				DocumentTypesChild = docs.Where(d => d.ForChild && !d.ForForeign).ToList(),
				AddonServicesForChilds = addonServiceses.Where(
					s =>
						s.TypeOfServiceId == (long) ServiceEnum.SpecializedPlaceChild ||
						s.TypeOfServiceId == (long) ServiceEnum.SpecializedTransportChild).ToList(),
				AddonServicesForAttendants = addonServiceses.Where(
					s =>
						s.TypeOfServiceId == (long) ServiceEnum.SpecializedPlaceAttendant ||
						s.TypeOfServiceId == (long) ServiceEnum.SpecializedTransportAttendant).ToList(),
				State = new ViewModelState
				{
					Actions = actions,
					PostNoStatusActions = postNoStatusActions,
					State = list.State,
					Title = "Список",
					ActionSelector = ".stringStateCode",
					FormSelector = ".main-form",
					NeedSaveButton =
						list.StateId == StateMachineStateEnum.Limit.List.Formation ||
						Security.HasRight(AccessRightEnum.Limit.List.EditInAllStates),
					NeedRemoveButton = list.StateId == StateMachineStateEnum.Limit.List.Formation,
					CanReturn = true,
					ReturnAction = "ListOfChildsList",
					ReturnController = "Limits",
					ReturnParametr = param,
					NotSaved = list.Id == 0
				},
				Errors =
					string.IsNullOrEmpty(stateCode) ? new List<string>() : ApiController.CheckListChildStateChange(list.Id, stateCode),
				CanEdit = list.StateId == StateMachineStateEnum.Limit.List.Formation ||
				          Security.HasRight(AccessRightEnum.Limit.List.EditInAllStates)
			};

			if (!Security.HasRight(AccessRightEnum.Limits.LimitChildInOrganization,
				list.LimitOnOrganization != null ? list.LimitOnOrganization.OrganizationId ?? 0 : 0))
			{
				model.CanEdit = false;
				model.State.Actions = new List<StateMachineAction>();
			}

			model.ListOfChildsCategorys.Insert(0, null);

			ViewBag.Districts =
				DistrictController.Get()
					.InsertAt(new BtiDistrict
					{
						Id = 0,
						Name = "-- Не выбрано --"
					});

			if (model.Data.HistoryLinkId.HasValue)
			{
				model.State.PostNoStatusActions = model.State.PostNoStatusActions ?? new List<NoStatusAction>();
				model.State.PostNoStatusActions.Add(new NoStatusAction
				{
					Name = "История",
					ButtonClass = "btn btn-default btn-hystory-link",
					SomeAddon = $"data-history-id=\"{model.Data.HistoryLinkId}\""
				});
			}

			model.ChildrenInAnotherLists = ApiController.FindDoubles(id ?? 0);
			model.SimilarChildrenInAnotherLists = model.Data.TypeOfLimitListId == (long)TypeOfLimitListEnum.Orphan ? new List<Child>() : ApiController.FindSimilar(id ?? 0);
			model.ChildrenInSameTimeRequests = ApiController.FindSameTimeRequests(id ?? 0);
			model.ApplicantsInSameTimeRequests = ApiController.FindSameTimeAttendants(id ?? 0);
         model.ListOfLimitsToCopy = ApiController.FindLimitsToCopy(id ?? 0);
         model.ApplicantsInAnotherLists = ApiController.FindApplicantDoubles(id ?? 0);


			return View(model);
		}

        [HttpGet]
        public ActionResult ListOfChildsEditSave()
        {
            return RedirectToAction(nameof(Organization));
        }

        [HttpPost]
		public ActionResult ListOfChildsEditSave(ListsOfChildModel model)
		{
			SetUnitOfWorkInRefClass();
			if (!Security.HasRightForSomeOrganization(AccessRightEnum.Limits.LimitChildInOrganization))
			{
				return RedirectToAvalibleAction();
			}

			ListOfChilds data = model.BuildData();

			using (var tran = UnitOfWork.GetTransactionScope())
			{
				data = ApiController.SaveListOfChilds(data);

				if (model.StringStateCode == "Delete")
				{
					var p = ApiController.DeleteListChild(model.Data.Id);
					tran.Complete();
					return RedirectToAction("ListOfChildsList", p);
				}

				if (!string.IsNullOrEmpty(model.StringStateCode))
				{
					var resCode = ApiController.ChangeListChildState(data.Id, model.StringStateCode)
						? string.Empty
						: model.StringStateCode;

					tran.Complete();
					return RedirectToAction("ListOfChildsEdit", "Limits", new { id = data.Id, stateCode = resCode });
				}

				tran.Complete();
			}

			return RedirectToAction("ListOfChildsEdit", "Limits", new { id = data.Id });

		}

      [HttpPost]
      public ActionResult ListOfChildsMerge(long From, long To)
      {
         SetUnitOfWorkInRefClass();
         if (!Security.HasRightForSomeOrganization(AccessRightEnum.Limits.LimitChildInOrganization))
         {
            return RedirectToAvalibleAction();
         }

         ApiController.MergeChildren(From, To);
         return new HttpStatusCodeResult(HttpStatusCode.OK);
      }

      #endregion

      #region Работа с квотами организаций

      public ActionResult OrganizationChangeStatus(OrganizationListModel model)
		{
			SetUnitOfWorkInRefClass();
			if (model.Data != null && model.Data.Id > 0 && !string.IsNullOrEmpty(model.StringStateCode))
			{
				var action = StateController.GetAction(model.StringStateCode);
				if (action != null && action.NeedSign)
				{
					var errors = ApiController.CheckVedomstvoLimitStateChange(model.Data.Id, model.StringStateCode);
					if (errors != null && errors.Any())
					{
						var item = ApiController.GetLimitOnVedomstvo(model.Data.Id);
						return RedirectToAction("OrganizationList",
							new
							{
								yearOfRestId = item.YearOfRestId,
								limitOnVedomstvoId = model.Data.Id,
								stateCode = model.StringStateCode
							});
					}

					var data = Pdf.GetDataForSign(model.Data.Id, SignTypeEnum.ListOfOiv);
					if (data != null)
					{
						data.ActionCode = model.StringStateCode;
						data.Commentary = model.StringCommentaryCode;
						var esep = new Esep();
						var upload = esep.UploadFilesToEsep(new List<DataForSign> { data });
						var url = esep.UrlToEsep(upload.Select(u => u.FileAccessCode).ToList(), Esep.FullReturnUrl(Url.Action("OrganizationList", "Limits",
							new
							{
								limitOnVedomstvoId = model.Data.Id,
								stateCode = string.Empty
							})), Guid.NewGuid().ToString());
						return Redirect(url);
					}
				}

				using (var tran = UnitOfWork.GetTransactionScope())
				{
					var result = ApiController.ChangeVedomstvoListState(model.Data.Id, model.StringStateCode, null, model.StringCommentaryCode);
					var item = ApiController.GetLimitOnVedomstvo(model.Data.Id);
					tran.Complete();
					return RedirectToAction("OrganizationList",
						new
						{
							yearOfRestId = item.YearOfRestId,
							limitOnVedomstvoId = item.Id,
							stateCode = result ? string.Empty : model.StringStateCode
						});
				}
			}

			return RedirectToAction("OrganizationList", new { model });
		}

		[HttpGet]
		public ActionResult OrganizationList(long? yearOfRestId, long? limitOnVedomstvoId, string stateCode, long? tlid = null)
		{
			SetUnitOfWorkInRefClass();
			return
				OrganizationList(
					new OrganizationListModel(new LimitOnVedomstvo
					{
						YearOfRestId = yearOfRestId,
						Id = limitOnVedomstvoId ?? 0
					})
					{
						StringStateCode = stateCode,
						TypeLimitId =  tlid ?? (long)TypeOfLimitListEnum.Profile
					});
		}


		[HttpPost]
		public ActionResult OrganizationList(OrganizationListModel model)
		{
			SetUnitOfWorkInRefClass();
			if (!Security.HasRight(AccessRightEnum.Limits.LimitToOiv) && !Security.HasRightForSomeOrganization(AccessRightEnum.Limits.LimitByOrganization))
			{
				return RedirectToAvalibleAction();
			}

			var res = Esep.SaveSignsIfNeed(UnitOfWork);
			if (res != null && res.Any())
			{
				var first = res.First();

				using (var tran = UnitOfWork.GetTransactionScope())
				{
					if (first.SignType == SignTypeEnum.ListOfOrganization)
					{
						ApiController.ChangeOrganizationListState(first.EntityId, first.ActionCode, first.SignInfoId, first.Commentary);
					}

					if (first.SignType == SignTypeEnum.ListOfOiv)
					{
						ApiController.ChangeVedomstvoListState(first.EntityId, first.ActionCode, first.SignInfoId, first.Commentary);
					}

					tran.Complete();
				}
			}

			var listOfYears = ApiController.GetYearsOfRest().ToList();
			var curYear = listOfYears.FirstOrDefault(y => y.Year == DateTime.Now.Year) ?? listOfYears.LastOrDefault();
			var vedomstvos = ApiController.GetVedForRight(string.Empty, model?.Data?.YearOfRestId ?? curYear?.Id ?? 0).Where(v=>v!=null).ToList();

			var data = new LimitOnVedomstvo(new LimitOnVedomstvo
			{
				YearOfRestId = curYear?.Id,
				OrganizationId =
					vedomstvos.Count == 1 ? vedomstvos.First().Id : (long?) null
			});

			if (vedomstvos.Count == 1)
			{
				data = vedomstvos.First();
			}

			if (model?.Data != null && model.Data.Id > 0 && vedomstvos.Any(v=>v.Id == model.Data.Id))
			{
				data = ApiController.GetLimitOnVedomstvo(model.Data.Id);
			}

			var vm = new OrganizationListModel(data)
			{
				ListOfYears = listOfYears,
				Vedomstvos = vedomstvos,
				TypeLimit = UnitOfWork.GetById<TypeOfLimitList>(model?.TypeLimitId ?? (long)TypeOfLimitListEnum.Profile),
				TypeLimitId = model?.TypeLimitId ?? (long)TypeOfLimitListEnum.Profile
			};

			if (data.Id > 0 && data.YearOfRestId.HasValue && !string.IsNullOrEmpty(model?.StringStateCode))
			{
				vm.Errors = ApiController.CheckVedomstvoLimitStateChange(data.Id, model.StringStateCode);
			}

			vm.State = new ViewModelState
			{
				Actions =
					StateController.GetActions(vm.Data.State, StateMachineEnum.LimitOrganizationState)
						.Where(
							a =>
								Security.HasRight(AccessRightEnum.Limits.LimitByOrganization, vm.NullSafe(v=>v.Data.OrganizationId)??0) && !new[]
								{
									AccessRightEnum.Limit.Oiv.Formation, AccessRightEnum.Limit.Oiv.Brought
								}.Contains(a.ActionCode))
						.ToList(),
				State = vm.Data.State,
				Title =
					vm.Data.Id > 0
						? $"<h4>{vm.Data.NullSafe(d => d.Organization.Name)}</h4><h5> Год кампании: {vm.NullSafe(v => v.Data.YearOfRest.Name)}<br/> Размер квоты: {vm.Data.NullSafe(d => d.Volume)}</h5>"
						: string.Empty,
				ActionSelector = ".stringStateCode",
				FormSelector = ".postForm",
				CommentSelector = ".stringCommentaryCode",
				ActionWithComment = new List<string>{AccessRightEnum.Limit.Oiv.OnCompletion},
				PostNoStatusActions = new List<NoStatusAction>
				{
					new NoStatusAction
					{
						Name = "Печать",
						IconClass = "glyphicon-print",
						Controller = "Pdf",
						Action = "GetOivChilds",
						ActionParameters = new {id = vm.Data.Id}
					},
					new NoStatusAction
					{
						Name = "Список отдыхающих",
						IconClass = "glyphicon-print",
						Controller = "Pdf",
						Action = "GetOivChildsExcel",
						ActionParameters = new {id = vm.Data.Id}
					}
				},
				Sign = vm.Data.SignInfo
			};

			if (vm.Data?.HistoryLinkId != null && vm.State != null)
			{
				vm.State.PostNoStatusActions = vm.State.PostNoStatusActions ?? new List<NoStatusAction>();
				vm.State.PostNoStatusActions.Add(new NoStatusAction
				{
					Name = "История",
					ButtonClass = "btn btn-default btn-hystory-link",
					SomeAddon = $"data-history-id=\"{vm.Data.HistoryLinkId}\""
				});
			}

			vm.Vedomstvos.Insert(0, null);
			return View(vm);
		}
		#endregion

		#region Работа с квотами ведомства

		[HttpGet]
		public ActionResult VedomstvoList(long? yearId, string stateCode, long? tlid = null)
		{
			SetUnitOfWorkInRefClass();
			return VedomstvoList(new VedomstvoListModel(new YearOfRest {Id = yearId ?? 0})
			{
				StringStateCode = stateCode
			});
		}

		[HttpPost]
		public ActionResult VedomstvoList(VedomstvoListModel model)
		{
			SetUnitOfWorkInRefClass();
			if (!Security.HasRight(AccessRightEnum.Limits.LimitToOiv))
			{
				return RedirectToAvalibleAction();
			}

			var years = ApiController.GetYearsOfRest().ToList();

			var year = years.FirstOrDefault(y => y.Year == DateTime.Now.Year) ?? years.LastOrDefault();

			if (model?.Data != null && model.Data.Id > 0)
			{
				year = years.FirstOrDefault(y => y.Id == model.Data.Id) ?? year;
			}

			var tlids = WebApi.LimitsController.GetAccesedTypeLimitList();

			var vm = new VedomstvoListModel(year)
			{
				ListOfYears = years,
				Vedomstvos = OrgController.Get(0, string.Empty).ToList(),
				TypeOfLimitLists = UnitOfWork.GetSet<TypeOfLimitList>().Where(t=> tlids.Contains(t.Id)).OrderBy(t=>t.Name).ToList()
			};

			vm.State = new ViewModelState
			{
				Actions = StateController.GetActions(vm.Data.State, StateMachineEnum.LimitOivState),
				State = vm.Data.State,
				Title = $"Квоты по ОИВ на {vm.Data.NullSafe(y => y.Name)} год",
				ActionSelector = ".stringStateCode",
				FormSelector = ".postForm",
				PostNoStatusActions = new List<NoStatusAction>
				{
					new NoStatusAction
					{
						Name = "Печать",
						IconClass = "glyphicon-print",
						Controller = "Pdf",
						Action = "GetCityChilds",
						ActionParameters = new {id = vm.Data.Id}
					}
				},
				Sign = vm.Data.SignInfo
			};

			vm.Vedomstvos.Insert(0, null);

			return View(vm);
		}
		#endregion

		#region Общий список детей

		[HttpGet]
		public ActionResult GiftedChildren(long? yearId, long? vedomstvoId, string name, int? page, bool? included, bool? excluded)
		{
			SetUnitOfWorkInRefClass();
			return
				GiftedChildren(new GiftedChildrenModel(new List<Child>(), page??1, 1, 0)
				{
					YearId = yearId,
					VedomstvoId = vedomstvoId,
					Name = name,
					Included = included ?? true,
					Excluded = excluded ?? false
				});
		}

		/// <summary>
		/// общий список одарённых детей.
		/// </summary>
		/// <returns></returns>
		[HttpPost]
		public ActionResult GiftedChildren(GiftedChildrenModel model)
		{
			SetUnitOfWorkInRefClass();
			model = ApiController.GiftedChildren(model ?? new GiftedChildrenModel());

			model.ListOfYears = ApiController.GetYearsOfRest().ToList();

			model.Vedomstvos.Insert(0, null);

			var docs = VocController.GetDocumentsType();
			model.DocumentTypesChild = docs.Where(d => d.ForChild && !d.ForForeign).ToList();

			if (!model.YearId.HasValue)
			{
				model.YearId = model.ListOfYears.Where(y => y.Year == DateTime.Now.Year).Select(y => (long?)y.Id).FirstOrDefault() ?? model.ListOfYears.Select(y => y.Id).LastOrDefault();
			}

			model.Vedomstvos = ApiController.GetVedForRight(string.Empty, model.YearId ?? 0).ToList();

			return View(model);
		}

		#endregion
	}
}
