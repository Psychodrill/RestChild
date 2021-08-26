using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using RestChild.Comon;
using RestChild.Comon.Enumeration;
using RestChild.Extensions.Filter;
using RestChild.DAL.RepositoryExtensions;
using RestChild.Domain;
using RestChild.Web.Controllers.WebApi;
using RestChild.Web.Extensions;
using RestChild.Web.Models;
using RestChild.Web.Models.CounselorTestModels;
using RestChild.Web.Properties;

namespace RestChild.Web.Controllers
{
	/// <summary>
	///     работа с тестами
	/// </summary>
	public class CounselorTestController : BaseController
	{
		public WebApi.CounselorTestController ApiController { get; set; }
		public StateController ApiStateController { get; set; }

		public WebCounselorsController CounselorsController { get; set; }

		public override void SetUnitOfWorkInRefClass(IUnitOfWork unitOfWork)
		{
			base.SetUnitOfWorkInRefClass(unitOfWork);
			ApiStateController.SetUnitOfWorkInRefClass(unitOfWork);
			ApiController.SetUnitOfWorkInRefClass(unitOfWork);
			CounselorsController.SetUnitOfWorkInRefClass(unitOfWork);
        }

		// Поиск тестов.
		public ActionResult List(CounselorTestFilterModel model)
		{
			if (!Security.HasRight(AccessRightEnum.CounselorTest.View))
			{
				return RedirectToAvalibleAction();
			}

			SetUnitOfWorkInRefClass();

			model = model ?? new CounselorTestFilterModel();

			var query =
				UnitOfWork.GetSet<CounselorTest>().Where(t => t.StateId != null && t.StateId != StateMachineStateEnum.Deleted);

			if (model.StateId.HasValue && model.StateId > 0)
			{
				query = query.Where(q => q.StateId == model.StateId);
			}

			if (model.DateCreateStart.HasValue)
			{
				query = query.Where(q => q.DateCreate >= model.DateCreateStart);
			}

			if (model.DateCreateEnd.HasValue)
			{
				var de = model.DateCreateEnd.Value.AddDays(1);
				query = query.Where(q => q.DateCreate < de);
			}

			if (!string.IsNullOrWhiteSpace(model.SearchString))
			{
				var ss = model.SearchString.Trim().ToLower();
				query = query.Where(q => q.Name.ToLower().Contains(ss));
			}

			var totalCount = query.Count();

			var pageSize = Settings.Default.TablePageSize;
			var pageNumber = model.PageNumberEx ?? model.PageNumber;
			var startRecord = (pageNumber - 1)*pageSize;

			var entity =
				query.OrderByDescending(d => d.DateCreate).ThenByDescending(b => b.Id).Skip(startRecord).Take(pageSize).ToList();

			var res = new CounselorTestFilterModel(entity, pageNumber, model.PageSize, totalCount)
			{
				States = ApiStateController.GetStates(StateMachineEnum.CounselorTest),
				DateCreateStart = model.DateCreateStart,
				DateCreateEnd = model.DateCreateEnd,
				SearchString = model.SearchString,
				PageNumberEx = model.PageNumberEx,
				StateId = model.StateId
			};

			return View(res);
		}

		/// <summary>
		/// получение теста для сохранения.
		/// </summary>
		public ActionResult Edit(long? id)
		{
			if (!Security.HasRight(AccessRightEnum.CounselorTest.View))
			{
				return RedirectToAvalibleAction();
			}

			SetUnitOfWorkInRefClass();

			var entity = UnitOfWork.GetById<CounselorTest>(id) ?? new CounselorTest
			{
				StateId = StateMachineStateEnum.CounselorTest.Editing,
				DateCreate = DateTime.Now,
				Questions = new List<CounselorTestQuestion>()
			};

			var isEditable = ((Security.HasRight(AccessRightEnum.CounselorTest.Edit)) &&
								  entity.StateId == StateMachineStateEnum.CounselorTest.Editing);

			var state = entity.State ?? UnitOfWork.GetById<StateMachineState>(entity.StateId);

			var actions = entity.Id != 0 && entity.StateId.HasValue
				? ApiStateController.GetActions(state, StateMachineEnum.CounselorTest)
				: new List<StateMachineAction>();

			var model = new Models.CounselorTestModels.TestModel(entity)
			{
				IsEditable = isEditable,
				State =
					new ViewModelState
					{
						Actions = actions,
						State = state,
						FormSelector = "#form",
						ActionSelector = "#StateMachineActionString",
						CanReturn = true,
						ReturnController = "CounselorTest",
						ReturnAction = "List",
						NeedSaveButton = isEditable,
						NeedRemoveButton = entity.StateId == StateMachineStateEnum.CounselorTest.Editing,
						JsFunctionToAction = "confirmStateButtonTask",
						PostNoStatusActions = new List<NoStatusAction>()
					}
			};

			model.State.PostNoStatusActions.Add(new NoStatusAction
			{
				Name = "История",
				ButtonClass = "btn btn-default btn-hystory-link",
				SomeAddon = $"data-history-id=\"{model.Data.HistoryLinkId}\""
			});

			return View(model);
		}

		/// <summary>
		/// получение теста для сохранения.
		/// </summary>
		public ActionResult GroupEdit(long? id)
		{
			if (!Security.HasRight(AccessRightEnum.TrainingCounselors.Edit))
			{
				return RedirectToAvalibleAction();
			}

			SetUnitOfWorkInRefClass();

			var entity = UnitOfWork.GetById<TrainingCounselors>(id) ?? new TrainingCounselors
			{
				StateId = StateMachineStateEnum.TrainingCounselors.Editing
			};

			var isEditable = ((Security.HasRight(AccessRightEnum.TrainingCounselors.Edit)) &&
			                      entity.StateId == StateMachineStateEnum.TrainingCounselors.Editing);

			var isEditableFormed = ((Security.HasRight(AccessRightEnum.TrainingCounselors.Edit)) &&
								  entity.StateId == StateMachineStateEnum.TrainingCounselors.Formed);


			var state = entity.State ?? UnitOfWork.GetById<StateMachineState>(entity.StateId);

			var actions = entity.Id != 0 && entity.StateId.HasValue
				? ApiStateController.GetActions(state, StateMachineEnum.TrainingCounselors)
				: new List<StateMachineAction>();

			var model = new Models.CounselorTestModels.GroupModel(entity)
			{
				IsEditable = isEditable,
				IsEditableFormed = isEditableFormed,
				State =
					new ViewModelState
					{
						Actions = actions,
						State = state,
						FormSelector = "#form",
						ActionSelector = "#StateMachineActionString",
						CanReturn = true,
						ReturnController = "CounselorTest",
						ReturnAction = "List",
						NeedSaveButton = isEditable || isEditableFormed,
						NeedRemoveButton = entity.StateId == StateMachineStateEnum.TrainingCounselors.Editing,
						JsFunctionToAction = "confirmStateButtonTask",
						PostNoStatusActions = new List<NoStatusAction>()
					}
			};

			model.State.PostNoStatusActions.Add(new NoStatusAction
			{
				Name = "История",
				ButtonClass = "btn btn-default btn-hystory-link",
				SomeAddon = $"data-history-id=\"{model.Data.HistoryLinkId}\""
			});

			return View(model);
		}

		/// <summary>
		/// сохранение нового тестирования.
		/// </summary>
		public ActionResult SaveGroup(Models.CounselorTestModels.GroupModel model)
		{
			if (!Security.HasRight(AccessRightEnum.TrainingCounselors.Edit))
			{
				return RedirectToAvalibleAction();
			}

			SetUnitOfWorkInRefClass();

			var entity = model.BuildData();
			using (var tran = UnitOfWork.GetTransactionScope())
			{
				if (entity.Id == 0)
				{
					entity.LastUpdateTick = DateTime.Now.Ticks;
					UnitOfWork.Context.Entry(entity).State = EntityState.Added;
					entity.HistoryLink = ApiController.WriteHistory(entity.HistoryLink, "Создание группы обучения", string.Empty);
					entity.HistoryLinkId = entity.HistoryLink.Id;
					UnitOfWork.SaveChanges();
				}
				else
				{
					var data = UnitOfWork.GetById<TrainingCounselors>(entity.Id);

					if (data.LastUpdateTick != entity.LastUpdateTick)
					{
						SetRedicted();
						return RedirectToAction("GroupEdit", new {@id = entity.Id});
					}

					UpdateTrainingGroup(data, entity);
					entity = data;
				}

				if (!string.IsNullOrEmpty(model.StateMachineActionString))
				{
					var testEntity = UnitOfWork.GetById<TrainingCounselors>(model.Data.Id);
					if (model.StateMachineActionString == "Delete")
					{
						testEntity.HistoryLink = ApiController.WriteHistory(entity.HistoryLink, "Изменение статуса",
							$"c '{testEntity.NullSafe(e => e.State.Name)}' на 'Удалено", StateMachineStateEnum.Deleted, entity.StateId);

						testEntity.HistoryLinkId = testEntity.HistoryLink?.Id;

						testEntity.LastUpdateTick = DateTime.Now.Ticks;
						testEntity.StateId = StateMachineStateEnum.Deleted;
						UnitOfWork.SaveChanges();
						tran.Complete();
						return RedirectToAction("GroupList");
					}

					if (!Security.HasRight(model.StateMachineActionString))
					{
						SetErrors(new List<string> {"У вас нет прав для выполнения данной операции"});
						UnitOfWork.SaveChanges();
						tran.Complete();
						return RedirectToAction("GroupEdit", new { @id = entity.Id });
					}

					var errors = new List<string>();
					if (model.StateMachineActionString == AccessRightEnum.TrainingCounselors.ToFormed)
					{
						if (string.IsNullOrWhiteSpace(testEntity.Name))
						{
							errors.Add("Не указано наименование группы");
						}
						if (!testEntity.StartTraining.HasValue)
						{
							errors.Add("Не указана дата начала обучения");
						}

						if (!testEntity.Results.Any() && !testEntity.ForSite)
						{
							errors.Add("В группе обучения нет ни одного вожатого");
						}

						if (testEntity.Results.Any(r => !r.CounselorsId.HasValue && !r.AdministratorTourId.HasValue))
						{
							errors.Add("В группе есть обучаемый которого не выбрали из реестра вожатых");
						}

						if (testEntity.Tests.Any(r => !r.CounselorTestId.HasValue))
						{
							errors.Add("В группе указано тестирование с не выбранным тестом");
						}

						if ((testEntity.Value ?? 0) < testEntity.Results.Count)
						{
							errors.Add("Количество мест в группе меньше количества обучаемых");
						}
					}

					if (model.StateMachineActionString == AccessRightEnum.TrainingCounselors.ToEducationFinished)
					{
						if (testEntity.Tests.All(r => !r.CounselorTest.IsFinalTest))
						{
							errors.Add("В группе нет ни одного тестирования с признаком финальный");
						}
					}

					if (errors.Any())
					{
						SetErrors(errors);
						UnitOfWork.SaveChanges();
						tran.Complete();
						return RedirectToAction("GroupEdit", new { @id = entity.Id });
					}

				}

				if (!string.IsNullOrEmpty(model.StateMachineActionString))
				{
					var action = ApiStateController.GetAction(model.StateMachineActionString);
					if (action != null && action.ToStateId.HasValue && Security.HasRight(model.StateMachineActionString))
					{
						entity.HistoryLink = ApiController.WriteHistory(entity.HistoryLink, "Изменение статуса",
							$"c '{entity.NullSafe(e => e.State.Name)}' на '{action.NullSafe(a => a.ToState.Name)}'", action.ToStateId, entity.StateId);

						entity.HistoryLinkId = entity.HistoryLink?.Id;

                        entity.LastUpdateTick = DateTime.Now.Ticks;
						entity.StateId = action.ToStateId;

						// закончено обучение.
						if (entity.StateId == StateMachineStateEnum.TrainingCounselors.EducationFinished)
						{
							foreach (var result in entity.Results.Where(r=>r.Counselors != null).ToList())
							{
								result.Counselors.HistoryLink = ApiController.WriteHistory(result.Counselors.HistoryLink,
									result.IsSuccess
										? "Успешно закончил обучение в группе и стал вожатым"
										: "Не успешно закончил обучение в группе и был отчислен", string.Empty, result.IsSuccess
										? StateMachineStateEnum.Counselor.Approved
										: StateMachineStateEnum.Counselor.RequestDecline, result.Counselors.StateId);
								result.Counselors.HistoryLinkId = result.Counselors.HistoryLink.Id;

								if (result.Counselors.StateId == StateMachineStateEnum.Counselor.Request)
								{
									CounselorsController.ChangeState(result.Counselors.Id, result.IsSuccess
										? AccessRightEnum.Counselor.ForAprove
										: AccessRightEnum.Counselor.RequestDecline, false);
								}
							}


						}

						UnitOfWork.SaveChanges();
					}
				}

				UnitOfWork.SaveChanges();

				// сохранение свзи с вожатыми
				if (entity.StateId == StateMachineStateEnum.TrainingCounselors.Formed)
				{
					UnitOfWork.Context.Entry(entity).State = EntityState.Detached;
					entity = UnitOfWork.GetById<TrainingCounselors>(entity.Id);

					foreach (var res in entity.Results.Where(r=>!r.DateExclude.HasValue).ToList())
					{
						foreach (var test in entity.Tests.Where(t=>!t.IsDeleted).ToList())
						{
							if (res.Tests.All(t => t.GroupTest.CounselorTestId != test.CounselorTestId))
							{
								var ct = UnitOfWork.AddEntity(new TrainingCounselorsTest
								{
									GroupTestId = test.Id,
									GroupTest = test,
									IsComplited = false,
									LastUpdateTick = DateTime.Now.Ticks,
									TrainingCounselorsResultId = res.Id,
									TrainingCounselorsResult = res,
									TestGuid = Guid.NewGuid(),
									IsLastAttempt = true
								}, false);


								var email = res?.Counselors?.Email ?? res?.AdministratorTour?.Email;
								var phone = res?.Counselors?.Phone ?? res?.AdministratorTour?.Phone;

								if (!string.IsNullOrWhiteSpace(email))
								{
									try
									{
										new System.Net.Mail.MailAddress(email);
										UnitOfWork.AddEntity(new SendEmailAndSms
										{
											IsSmsSended = !string.IsNullOrWhiteSpace(phone),
											DateCreate = DateTime.Now,
											Phone = phone,
											EmailTitle = "Тестирование в группе",
											EmailMessage =
												$@"C {test.DateStart.FormatEx()} по {test.DateEnd.FormatEx()} будет проводиться тестирование {
													test.CounselorTest?.Name}. Для прохождения тестирования необходимо перейти по ссылке <a href='{
													Settings.Default.SystemUrl}CounselorTest/Testing?guid={ct.TestGuid}' target='_blank'>Тестирование</a>/",
											Email = email
										});
									}
									catch (Exception ex)
									{
										Logger.Error("Ошибка формирования сообщения на почту", ex);
									}
								}
							}
						}
					}

					UnitOfWork.SaveChanges();
				}

				tran.Complete();
			}

			return RedirectToAction("GroupEdit", new { @id = entity.Id });
		}

		private void UpdateTrainingGroup(TrainingCounselors data, TrainingCounselors entity)
		{
			data.CopyEntity(entity);
			data.LastUpdateTick = DateTime.Now.Ticks;

			// обновление обучаемых
			var rIds = entity.Results.Where(r => r.Id > 0).Select(r => r.Id).ToList();
			var resulsForDelete = data.Results.Where(r => !rIds.Contains(r.Id)).ToList();
			foreach (var result in resulsForDelete)
			{
				foreach (var test in result.Tests.ToList())
				{
					foreach (var answer in test.Answers.ToList())
					{
						UnitOfWork.Delete(answer);
					}
					UnitOfWork.Delete(test);
				}

				UnitOfWork.Delete(result);
			}

			var resulsForUpdate = data.Results.Where(t => rIds.Contains(t.Id)).ToList();
			foreach (var test in resulsForUpdate)
			{
				var updateEntity = entity.Results.FirstOrDefault(t => t.Id == test.Id);
				test.CopyEntity(updateEntity);
				test.LastUpdateTick = DateTime.Now.Ticks;
			}

			var resulsForInsert = entity.Results.Where(t => t.Id == 0).ToList();
			foreach (var test in resulsForInsert)
			{
				test.TrainingCounselorsId = data.Id;
				test.LastUpdateTick = DateTime.Now.Ticks;
				UnitOfWork.AddEntity(test);
			}

			// обновление тестов
			var tIds = entity.Tests.Where(t => t.Id > 0).Select(t => t.Id).ToList();
			var testsForDelete = data.Tests.Where(t => !t.IsDeleted && !tIds.Contains(t.Id)).ToList();
			foreach (var test in testsForDelete)
			{
				test.IsDeleted = true;
				test.LastUpdateTick = DateTime.Now.Ticks;
			}

			var testsForUpdate = data.Tests.Where(t => !t.IsDeleted && tIds.Contains(t.Id)).ToList();
			foreach (var test in testsForUpdate)
			{
				var updateEntity = entity.Tests.FirstOrDefault(t => t.Id == test.Id);
				test.CopyEntity(updateEntity);
				test.LastUpdateTick = DateTime.Now.Ticks;
			}

			var testsForInsert = entity.Tests.Where(t => t.Id == 0).ToList();
			foreach (var test in testsForInsert)
			{
				test.TrainingCounselorsId = data.Id;
				test.LastUpdateTick = DateTime.Now.Ticks;
				UnitOfWork.AddEntity(test);
			}

			data.HistoryLink = ApiController.WriteHistory(data.HistoryLink, "Обновление сведений о группе обучения", string.Empty);
			data.HistoryLinkId = data.HistoryLink?.Id;

			UnitOfWork.SaveChanges();
		}

		/// <summary>
		/// сохранение нового тестирования.
		/// </summary>
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult SaveEdit(TestModel model)
		{
			if (!Security.HasRight(AccessRightEnum.CounselorTest.View))
			{
				return RedirectToAvalibleAction();
			}

			SetUnitOfWorkInRefClass();

			var entity = model.BuildData();

			using (var tran = UnitOfWork.GetTransactionScope())
			{
				var questions = entity.Questions;
				var subjects = entity.CounselorTestSubjects;
				entity.Questions = null;
				entity.CounselorTestSubjects = null;

				if (entity.Id == 0)
				{
					entity.LastUpdateTick = DateTime.Now.Ticks;
					UnitOfWork.Context.Entry(entity).State = EntityState.Added;
					entity.HistoryLink = ApiController.WriteHistory(entity.HistoryLink, "Создание теста", string.Empty);
					entity.HistoryLinkId = entity.HistoryLink.Id;
					UnitOfWork.SaveChanges();

					foreach (var s in subjects)
					{
						var id = s.Id;
						s.LastUpdateTick = DateTime.Now.Ticks;
						s.CounselorTestId = entity.Id;
						s.Id = 0;
						UnitOfWork.Context.Entry(s).State = EntityState.Added;
						UnitOfWork.SaveChanges();

						var questionsForUpdate = questions.Where(q => q.CounselorTestSubjectId == id);
						foreach (var q in questionsForUpdate)
						{
							q.CounselorTestSubjectId = s.Id;
						}
					}

					var order = 0;
					foreach (var q in questions)
					{
						if (q.CounselorTestSubjectId <= 0)
						{
							q.CounselorTestSubjectId = null;
						}

						q.LastUpdateTick = DateTime.Now.Ticks;
						q.CounselorTestId = entity.Id;
						q.SortOrder = order++;
						UnitOfWork.Context.Entry(q).State = EntityState.Added;
					}

					UnitOfWork.SaveChanges();
				}
				else
				{
					var data = UnitOfWork.GetById<CounselorTest>(entity.Id);

					if (data.LastUpdateTick != entity.LastUpdateTick)
					{
						SetRedicted();
						return RedirectToAction("Edit", new { @id = entity.Id });
					}

					if (data.StateId == StateMachineStateEnum.CounselorTest.Editing)
					{
						entity.Questions = questions;
						entity.CounselorTestSubjects = subjects;
						ApiController.RemoveQuestionsOnSave(data, entity);
						ApiController.RemoveSubjectOnSave(data, entity);
						entity = ApiController.UpdateTestEntityOnSave(data, entity);
						entity.LastUpdateTick = DateTime.Now.Ticks;
						entity.HistoryLink = ApiController.WriteHistory(entity.HistoryLink ?? UnitOfWork.GetById<HistoryLink>(entity.HistoryLinkId), "Обновление сведений по тестированию вожатых", string.Empty);
						entity.HistoryLinkId = entity.HistoryLink?.Id;
						UnitOfWork.SaveChanges();
					}
					else
					{
						entity = data;
					}
				}

				if (!string.IsNullOrEmpty(model.StateMachineActionString))
				{

					if (model.StateMachineActionString == AccessRightEnum.CounselorTest.ToFormed)
					{
						var eForCheck = UnitOfWork.GetById<CounselorTest>(model?.Data?.Id);
						var errors = CheckCounselorTest(eForCheck);
						if (errors.Any())
						{
							SetErrors(errors);
							return RedirectToAction("Edit", new { @id = eForCheck.Id });
						}
					}

					var action = ApiStateController.GetAction(model.StateMachineActionString);
					if (action != null && action.ToStateId.HasValue && Security.HasRight(model.StateMachineActionString))
					{
						entity.HistoryLink = ApiController.WriteHistory(entity.HistoryLink ?? UnitOfWork.GetById<HistoryLink>(entity.HistoryLinkId), "Изменение статуса",
							$"c '{entity.NullSafe(e => e.State.Name)}' на '{action.NullSafe(a => a.ToState.Name)}'", action.ToStateId, entity.StateId);
						entity.HistoryLinkId = entity.HistoryLink?.Id;
						entity.StateId = action.ToStateId;
						UnitOfWork.Context.Entry(entity).State = EntityState.Modified;
					}
				}

				UnitOfWork.SaveChanges();
				tran.Complete();
			}

			return RedirectToAction("Edit", new {@id = entity.Id});
		}

		private static List<string> CheckCounselorTest(CounselorTest eForCheck)
		{
			List<string> errors = new List<string>();
			if (string.IsNullOrEmpty(eForCheck?.Name))
			{
				errors.Add("Необходимо указать наименование тестирования");
			}

			if (!(eForCheck?.Questions?.Any(q => !q.IsDeleted) ?? false))
			{
				errors.Add("Должен быть хотя бы один вопрос");
			}

			if (eForCheck?.Questions?.Where(q => !q.IsDeleted)?.Any(q => q.Variants.Where(v => !v.IsDeleted).All(v => v.IsTrue)) ?? false)
			{
				errors.Add("У кажого вопроса должен быть хотя бы один неправильный ответ");
			}

			if (eForCheck?.Questions?.Where(q => !q.IsDeleted)?.Any(q => !q.Variants.Where(v => !v.IsDeleted).Any(v => v.IsTrue)) ?? false)
			{
				errors.Add("У каждого вопроса должен быть указан правильный ответ");
			}

			if (eForCheck?.Questions?.Where(q => !q.IsDeleted)?.Any(q => string.IsNullOrWhiteSpace(q.Question)) ?? false)
			{
				errors.Add("Не должно быть пустых вопросов");
			}

			if (eForCheck?.Questions?.Where(q=>!q.IsDeleted)?.Any(q => q.Variants.Where(v => !v.IsDeleted).Any(v=>string.IsNullOrWhiteSpace(v.Text))) ?? false)
			{
				errors.Add("Не должно быть пустых ответов на вопросы");
			}

			if (eForCheck?.CounselorTestSubjects?.Any(c => (c.QuestionCount ?? 0) == 0 && !c.IsArchive) ?? false)
			{
				errors.Add("Необходимо удалить все темы в которых нет вопросов");
			}

			if (eForCheck?.Questions?.Where(q => !q.IsDeleted)?.Any(q => (q.CounselorTestSubjectId ?? 0) <= 0) ?? false)
			{
				errors.Add("У каждого вопроса должена быть указана тема");
			}

			return errors;
		}


		[AllowAnonymous]
		public ActionResult Testing(Guid? guid)
		{
			SetUnitOfWorkInRefClass(UnitOfWork);

			var data =
				UnitOfWork.GetSet<TrainingCounselorsTest>().FirstOrDefault(t => t.TestGuid == guid) ?? new TrainingCounselorsTest();


			var countAttempt = data.TrainingCounselorsResult?.Tests.Count(t => t.GroupTestId == data.GroupTestId) ?? 0;

			var model = new TestingModel(data)
			{
				MayRetest = (!(data.GroupTest?.IsCountLimited ?? false) || countAttempt < data.GroupTest?.CountAttempts)
			};

			model.MayTesting = !model.Data.IsComplited &&
			                   model.Data?.GroupTest?.TrainingCounselors?.StateId == StateMachineStateEnum.TrainingCounselors.Formed &&
			                   model.Data?.GroupTest?.DateStart <= DateTime.Now.Date &&
			                   model.Data?.GroupTest?.DateEnd >= DateTime.Now.Date &&
			                   !((model.Data?.TrainingCounselorsResult?.DateExclude.HasValue) ?? false);

			model.MayView = model.Data.IsComplited && Security.GetCurrentAccountId().HasValue;


			if (data.Id > 0)
			{
				model = FirstSaveTesting(model);
			}

			if (data.GroupTest?.CounselorTest?.IsFinalTest ?? false)
			{
				var tests =
					data.TrainingCounselorsResult?.Tests.Where(
						t =>
							!t.GroupTest.CounselorTest.IsFinalTest && t.IsLastAttempt &&
							(!t.IsComplited || t.Rating > t.GroupTest.CounselorTest.CountErrorInTest)).ToList();

				if (tests?.Any() ?? false)
				{
					model.NeedOtherTest = true;
					model.Testings =
						tests.Select(t => new Tuple<string, string>($"{Settings.Default.SystemUrl}CounselorTest/Testing?guid={t.TestGuid}", t.GroupTest.CounselorTest.Name)).ToList();
				}
			}

			return View(model);
		}

		public ActionResult ListTest(long trainingCounselorsResultId)
		{
			SetUnitOfWorkInRefClass();
			return View(ApiController.GetCounselorsTest(trainingCounselorsResultId));
		}

		public ActionResult ListPupil(long groupId)
		{
			SetUnitOfWorkInRefClass();
			return View(ApiController.GetGroupCounselorsTest(groupId));
		}

		/// <summary>
		/// первое сохранение
		/// </summary>
		private TestingModel FirstSaveTesting(TestingModel model)
		{
			var answers = model.Answers.Select(a => a.BuildData()).ToList();
			foreach (var answer in answers)
			{
				if (answer.Id == 0)
				{
					answer.CounselorTestId = model.Data.Id;
					UnitOfWork.Context.Entry(answer).State = EntityState.Added;
				}
			}

			UnitOfWork.SaveChanges();
			return model;
		}

		[HttpPost]
		[AllowAnonymous]
		public ActionResult ReTesting(long id, long tick, Guid guid)
		{
			SetUnitOfWorkInRefClass();
			SetUnitOfWorkInRefClass();
			var entity = UnitOfWork.GetById<TrainingCounselorsTest>(id);
			if (entity != null && (entity.TestGuid == guid && entity.LastUpdateTick == tick))
			{
				var countAttempt = entity.TrainingCounselorsResult.Tests.Count(t => t.GroupTestId == entity.GroupTestId);
				if (!(entity.GroupTest?.IsCountLimited ?? false) || countAttempt < entity.GroupTest?.CountAttempts)
				{
					var newEntity = new TrainingCounselorsTest(entity) {Id = 0};
					entity.IsLastAttempt = false;
					entity.TestGuid = Guid.NewGuid();
					newEntity.IsComplited = false;
					newEntity.DateTesting = null;
					newEntity.LastUpdateTick = DateTime.Now.Ticks;
					UnitOfWork.AddEntity(newEntity);
				}
			}

			return RedirectToAction("Testing", new { guid = guid });
		}

		/// <summary>
		/// прохождение тестирования.
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public ActionResult SaveTesting(TestingModel model)
		{
			SetUnitOfWorkInRefClass();

			var data = model.BuildData();

			var entity = UnitOfWork.GetById<TrainingCounselorsTest>(data.Id);

			if (entity.LastUpdateTick != data.LastUpdateTick || entity.IsComplited)
			{
				SetRedicted();
				return RedirectToAction("Testing", new { guid = data.TestGuid });
			}

			using (var tran = UnitOfWork.GetTransactionScope())
			{
				entity.IsComplited = true;
				entity.LastUpdateTick = DateTime.Now.Ticks;
				entity.DateTesting = DateTime.Now;
				var errors = 0;
				foreach (var answer in data.Answers)
				{
					var exists = entity.Answers.FirstOrDefault(a => a.Id == answer.Id);
					CounselorTestAnswerVariant v = null;
                    if (exists != null)
					{
						exists.VariantId = answer.VariantId;
						v = UnitOfWork.GetById<CounselorTestAnswerVariant>(exists.VariantId);
					}


					if (!(v?.IsTrue ?? false))
					{
						errors++;
					}
				}

				entity.Rating = errors;

				if ((entity.GroupTest?.CounselorTest?.IsFinalTest ??false) && (entity.GroupTest?.CounselorTest?.CountErrorInTest >=errors) && entity.TrainingCounselorsResult != null)
				{
					entity.TrainingCounselorsResult.IsSuccess = true;
				}

				UnitOfWork.SaveChanges();
				tran.Complete();
			}

			return RedirectToAction("Testing", new {guid = data.TestGuid});
		}

		public ActionResult GroupList(CounselorGroupFilterModel model)
		{
			if (!Security.HasRight(AccessRightEnum.TrainingCounselors.View))
			{
				return RedirectToAvalibleAction();
			}

			SetUnitOfWorkInRefClass(UnitOfWork);

			model = model ?? new CounselorGroupFilterModel();

			var query =
				UnitOfWork.GetSet<TrainingCounselors>().Where(t => t.StateId != null && t.StateId != StateMachineStateEnum.Deleted);

			if (model.StateId.HasValue && model.StateId > 0)
			{
				query = query.Where(q => q.StateId == model.StateId);
			}

			if (model.DateCreateStart.HasValue)
			{
				query = query.Where(q => q.StartTraining >= model.DateCreateStart);
			}

			if (model.DateCreateEnd.HasValue)
			{
				var de = model.DateCreateEnd.Value.AddDays(1);
				query = query.Where(q => q.StartTraining < de);
			}

			if (!string.IsNullOrWhiteSpace(model.SearchString))
			{
				var ss = model.SearchString.Trim().ToLower();
				query = query.Where(q => q.Name.ToLower().Contains(ss));
			}

			var totalCount = query.Count();

			var pageSize = Settings.Default.TablePageSize;
			var pageNumber = model.PageNumberEx ?? model.PageNumber;
			var startRecord = (pageNumber - 1) * pageSize;

			var entity =
				query.OrderByDescending(d => d.StartTraining).ThenByDescending(b => b.Id).Skip(startRecord).Take(pageSize).ToList();

			var res = new CounselorGroupFilterModel(entity, pageNumber, model.PageSize, totalCount)
			{
				States = ApiStateController.GetStates(StateMachineEnum.TrainingCounselors),
				DateCreateStart = model.DateCreateStart,
				DateCreateEnd = model.DateCreateEnd,
				SearchString = model.SearchString,
				PageNumberEx = model.PageNumberEx,
				StateId = model.StateId
			};

			return View(res);
		}
	}
}
