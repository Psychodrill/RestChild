using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using RestChild.Comon;
using RestChild.Comon.Enumeration;
using RestChild.Extensions.Filter;
using RestChild.Domain;
using RestChild.Web.Controllers.WebApi;
using RestChild.Web.Models;

namespace RestChild.Web.Controllers
{
	[Authorize]
	public class CounselorsController : BaseController
	{
		public WebCounselorsController ApiController { get; set; }

		public StateController ApiStateController { get; set; }

		public WebVocabularyController VocabularyController { get; set; }

		public override void SetUnitOfWorkInRefClass(IUnitOfWork unitOfWork)
		{
			base.SetUnitOfWorkInRefClass(unitOfWork);
			ApiController.SetUnitOfWorkInRefClass(unitOfWork);
			ApiStateController.SetUnitOfWorkInRefClass(unitOfWork);
		}

		public ActionResult Search(CounselorsFilterModel filter)
		{
         if (!Security.HasAnyRights(new[] { AccessRightEnum.CounselorsManage }))
         {
            return RedirectToAvalibleAction();
         }
         SetUnitOfWorkInRefClass(UnitOfWork);
			filter = filter ?? new CounselorsFilterModel();
			filter.Result = ApiController.Get(filter);
			filter.States = ApiController.GetStates();
			return View("CounselorsList", filter);
		}

		public ActionResult Insert()
		{
			SetUnitOfWorkInRefClass(UnitOfWork);
			var counselor = new Counselors
								{
									StateId = StateMachineStateEnum.Counselor.Editing,
									State = ApiStateController.GetState(StateMachineStateEnum.Counselor.Editing)
								};

			return GetCounselorViewResult(counselor, null);
		}

		public ActionResult Update(long id, string activeTab)
		{
			SetUnitOfWorkInRefClass(UnitOfWork);
			var counselor = ApiController.Get(id);
			if (counselor == null)
			{
				return RedirectToAction("Search");
			}

			return GetCounselorViewResult(counselor, null, activeTab);
		}

		public ActionResult Save(CounselorsModel model)
		{
			SetUnitOfWorkInRefClass(UnitOfWork);
			var counselor = model.BuildData();
			if ((!ModelState.IsValid || !ValidateModel(model)) && (counselor.StateId == StateMachineStateEnum.Counselor.Editing || counselor.StateId == StateMachineStateEnum.Counselor.OnReworking))
			{
				return GetCounselorViewResult(counselor, model.StateMachineActionString);
			}

			if (model.IsMale.HasValue)
			{
				counselor.Male = model.IsMale.Value;
			}

			if (model.Skills != null)
			{
				counselor.Skill = model.Skills.Where(s => s.IsSelected).Select(s => new CounselorSkill()
				{
					Id = s.CounselorSkillId ?? 0,
					CounselorsId = counselor.Id,
					OtherText = s.Text,
					SkillId = s.SkillId,
					SkillVocabularyId = s.SkillVocabularyId
				}).ToList();
			}

			if (counselor.Id == 0)
			{
				counselor.DateCreate = DateTime.Now;
				counselor.DateUpdate = counselor.DateCreate;
				counselor.AccountId = Security.GetCurrentAccountId();
				counselor.HistoryLink = new HistoryLink();
				if (!string.IsNullOrEmpty(model.PhotoUrl))
				{
					counselor.Files = new List<CounselorFile>()
										{
											new CounselorFile()
												{
													FileName = "photo",
													FileUrl = model.PhotoUrl,
													Counselors = counselor,
												}
										};
				}

				ApiController.Post(counselor);
			}
			else
			{
				if (counselor.StateId == StateMachineStateEnum.Counselor.Editing || counselor.StateId == StateMachineStateEnum.Counselor.OnReworking)
				{
					counselor.Files = counselor.Files ?? new List<CounselorFile>();
					counselor.Files.Add(new CounselorFile()
					{
						FileName = "photo",
						FileUrl = model.PhotoUrl,
						Counselors = counselor,
						CounselorsId = counselor.Id
					});
					counselor.DateUpdate = DateTime.Now;
					ApiController.Put(counselor.Id, counselor);
				}

				if (!string.IsNullOrEmpty(model.StateMachineActionString))
				{
					var isOk = ApiController.ChangeState(counselor.Id, model.StateMachineActionString);
					return RedirectToAction("Update", new { id = counselor.Id, actionCode = isOk ? null : model.StateMachineActionString, activeTab = model.ActiveTab });
				}
			}
			return RedirectToAction("Update", new { Id = counselor.Id, activeTab = model.ActiveTab });
		}

		[HttpGet]
		public ActionResult CounselorsForAdd(CounselorsFilterModel filter)
		{
			filter = filter ?? new CounselorsFilterModel();
			filter.StateId = StateMachineStateEnum.Counselor.Approved;
			filter.Result = ApiController.Get(filter);
			return View("Assets/CounselorsList", filter);
		}

		private ActionResult GetCounselorViewResult(Counselors counselor, string actionCode, string activeTab = "")
		{
			var state = counselor.StateId.HasValue ? ApiStateController.GetState(counselor.StateId.Value) : null;
			var actions = counselor.Id != 0 && counselor.StateId.HasValue ? ApiStateController.GetActions(state, StateMachineEnum.CounselorState) : new List<StateMachineAction>();

			var counselorSkillDb = UnitOfWork.GetSet<CounselorSkill>();
			var skillVocabulariesDb = UnitOfWork.GetSet<SkillVocabulary>();
			var skills = UnitOfWork.GetSet<Skill>().Where(s => s.IsActive && s.SkillsGroup.IsActive)
				.Select(s => new { Skill = s, CounselorSkill = counselorSkillDb.FirstOrDefault(cs => cs.SkillId == s.Id && cs.CounselorsId == counselor.Id) })
				.ToList()
				.Select(s => new CounselorSkillModel()
					             {
						             SkillId = s.Skill.Id,
									 Skill = s.Skill,
									 CounselorSkillId = s.CounselorSkill?.Id,
									 IsSelected = s.CounselorSkill != null,
									 Text = s.CounselorSkill?.OtherText,
									 SkillVocabularyId = s.CounselorSkill?.SkillVocabularyId,
									 SkillVocabularies = skillVocabulariesDb.Where(v => v.SkillId == s.Skill.Id).ToList()
					             })
				.GroupBy(s => s.Skill.SkillsGroup)
				.ToList();
			var result = new CounselorsModel(counselor)
								{
									PhotoUrl = counselor.Files != null ? counselor.Files.Where(f => f.FileName == "photo").Select(f => f.FileUrl).FirstOrDefault() : null,
									IsEditable =
										counselor.StateId == StateMachineStateEnum.Counselor.Editing || counselor.StateId == StateMachineStateEnum.Counselor.OnReworking,
									State =
										new ViewModelState
											{
												Actions = actions,
												State = state,
												FormSelector = "#counselorForm",
												ActionSelector = "#StateMachineActionString",
												CanReturn = true,
												ReturnController = "Counselors",
												ReturnAction = "Search",
												NeedSaveButton =
													counselor.StateId == StateMachineStateEnum.Counselor.Editing || counselor.StateId == StateMachineStateEnum.Counselor.OnReworking,
												NeedRemoveButton =
													counselor.Id != 0 && counselor.StateId == StateMachineStateEnum.Counselor.Editing
											},
									IsMale = counselor.Id != 0 ? (bool?)counselor.Male : null,
									SkillGroups = skills,
									ActiveTab = activeTab
								};

			result.CalcRaiting();

			SetupVocabularies(result);
			if (counselor.HistoryLinkId.HasValue)
			{
				result.State.PostNoStatusActions = result.State.PostNoStatusActions ?? new List<NoStatusAction>();
				result.State.PostNoStatusActions.Add(new NoStatusAction
				{
					Name = "История",
					ButtonClass = "btn btn-default btn-hystory-link",
					SomeAddon = string.Format("data-history-id=\"{0}\"", counselor.HistoryLinkId)
				});
			}
			if (!string.IsNullOrEmpty(actionCode))
			{
				var errors = ApiController.GetErrorsOfChageStatus(counselor.Id, actionCode);
				if (errors != null && errors.Any())
				{
					foreach (var error in errors)
					{
						ModelState.AddModelError(string.Empty, error);
					}
				}
			}
			return View("CounselorsEdit", result);
		}

		private void SetupVocabularies(CounselorsModel model)
		{
			model.DocumentTypes = ApiController.GetAvailableDocumentTypes().Where(d=>!d.ForForeign).ToList();
			model.MatrialStatuses = VocabularyController.GetMatrialStatuses();
			model.MilitaryDuties = VocabularyController.GetMilitaryDuties();
			model.StateDistricts = VocabularyController.GetStateDistricts();
			model.TieColors = ApiController.GetTieColors();
			model.TypeOfEducations = VocabularyController.GetTypesOfEducation();
			model.Sizes = UnitOfWork.GetSet<ClothingSize>().OrderBy(c => c.Id).ToList().Select(c => new ClothingSize(c)).ToList();
		}

		private bool ValidateModel(CounselorsModel model)
		{
			if (model == null || model.Data == null)
			{
				ModelState.AddModelError(string.Empty, "ОШибка передачи данных");
				return false;
			}

			var counselor = model.BuildData();
			bool isOk = true;

			if (string.IsNullOrEmpty(counselor.FirstName))
			{
				isOk = false;
				ModelState.AddModelError("Data.FirstName", "Необходимо указать имя");
			}

			if (string.IsNullOrEmpty(counselor.MiddleName) && !counselor.HaveMiddleName)
			{
				isOk = false;
				ModelState.AddModelError("Data.MiddleName", "Необходимо указать отчество");
			}

			if (string.IsNullOrEmpty(counselor.LastName))
			{
				isOk = false;
				ModelState.AddModelError("Data.LastName", "Необходимо указать фамилию");
			}

			if (!counselor.DocumentTypeId.HasValue)
			{
				isOk = false;
				ModelState.AddModelError("Data.DocumentTypeId", "Необходимо указать документ, подтверждающий личность");
			}

			if (!model.IsMale.HasValue)
			{
				isOk = false;
				ModelState.AddModelError("IsMale", "Необходимо указать пол");
			}

			if (!counselor.DateOfBirth.HasValue)
			{
				isOk = false;
				ModelState.AddModelError("Data.DateOfBirth", "Необходимо указать дату рождения");
			}

			if (string.IsNullOrEmpty(counselor.PlaceOfBirth))
			{
				isOk = false;
				ModelState.AddModelError("Data.PlaceOfBirth", "Необходимо указать место рождения");
			}

			if (string.IsNullOrEmpty(counselor.DocumentSeria))
			{
				isOk = false;
				ModelState.AddModelError("Data.DocumentSeria", "Необходимо указать серию документа");
			}

			if (string.IsNullOrEmpty(counselor.DocumentNumber))
			{
				isOk = false;
				ModelState.AddModelError("Data.DocumentNumber", "Необходимо указать номер документа");
			}

			if (!counselor.DocumentDateOfIssue.HasValue)
			{
				isOk = false;
				ModelState.AddModelError("Data.DocumentDateOfIssue", "Необходимо указать дату выдачи документа");
			}

			if (string.IsNullOrEmpty(counselor.DocumentSubjectIssue))
			{
				isOk = false;
				ModelState.AddModelError("Data.DocumentSubjectIssue", "Необходимо указать, кем выдан документ");
			}

			if (!counselor.StateId.HasValue)
			{
				isOk = false;
				ModelState.AddModelError(string.Empty, "Некорректный статус");
			}

			return isOk;
		}
	}
}
