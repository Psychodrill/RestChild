using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using RestChild.Comon;
using RestChild.Extensions.Filter;
using RestChild.Web.Controllers.WebApi;
using RestChild.Web.Models;
using RestChild.Comon.Enumeration;
using RestChild.DAL.RepositoryExtensions;
using RestChild.Domain;
using RestChild.Extensions.Extensions;
using RestChild.Web.Extensions;
using RestChild.Web.Properties;

namespace RestChild.Web.Controllers
{
	public class CounselorTaskController : BaseController
	{
		public WebApi.CounselorTaskController ApiController { get; set; }
		public WebHotelsController ApiHotelsController { get; set; }
		public WebRestTimeController ApiRestTimeController { get; set; }
		public WebSubjectOfRestController ApiSubjectOfRestController { get; set; }
		public WebRestYearController ApiRestYearController { get; set; }
		public WebHotelTypeController ApiHotelTypeController { get; set; }
		public StateController ApiStateController { get; set; }


		/// <summary>
		///     настройка контроллера
		/// </summary>
		/// <param name="unitOfWork"></param>
		public override void SetUnitOfWorkInRefClass(IUnitOfWork unitOfWork)
		{
			base.SetUnitOfWorkInRefClass(unitOfWork);
			ApiController.SetUnitOfWorkInRefClass(unitOfWork);
			ApiHotelsController.SetUnitOfWorkInRefClass(unitOfWork);
			ApiRestTimeController.SetUnitOfWorkInRefClass(unitOfWork);
			ApiSubjectOfRestController.SetUnitOfWorkInRefClass(unitOfWork);
			ApiRestYearController.SetUnitOfWorkInRefClass(unitOfWork);
			ApiHotelTypeController.SetUnitOfWorkInRefClass(unitOfWork);
			ApiStateController.SetUnitOfWorkInRefClass(unitOfWork);
		}

		/// <summary>
		///     поиск и создание задач и создание новых
		/// </summary>
		/// <returns></returns>
		public ActionResult List(CounselorTaskListModel model)
		{
			if (!Security.HasRight(AccessRightEnum.CounselorTask.View))
			{
				return RedirectToAvalibleAction();
			}

			SetUnitOfWorkInRefClass(UnitOfWork);

			var years = UnitOfWork.GetSet<YearOfRest>().OrderBy(y => y.Name).ToList();
			model = model ?? new CounselorTaskListModel();
			if (!model.YearOfRestId.HasValue || model.YearOfRestId == 0)
			{
				var curYear = years.FirstOrDefault(y => y.Year == DateTime.Now.Year) ??
							  years.LastOrDefault();
				if (curYear != null)
				{
					model.YearOfRestId = curYear.Id;
				}
			}

			model = ApiController.Search(model);

			model.Years = years;
			model.GroupedTimesOfRest = UnitOfWork.GetSet<GroupedTimeOfRest>().OrderBy(y => y.Name).ToList();
			model.Hotels = model.HotelsId.HasValue ? UnitOfWork.GetById<Hotels>(model.HotelsId.Value) : null;
			model.States = ApiController.GetStates();

			return View(model);
		}

		/// <summary>
		/// сохранение задачи
		/// </summary>
		[HttpPost]
		public ActionResult Save(CounselorTaskEditModel model)
		{
			SetUnitOfWorkInRefClass(UnitOfWork);

			if (!Security.HasRight(AccessRightEnum.CounselorTask.Edit) && !Security.HasRight(AccessRightEnum.CounselorTask.View))
			{
				return RedirectToAvalibleAction();
			}

			// удаление задачи.
			if (model.StateMachineActionString == "Delete")
			{
				var e = UnitOfWork.GetById<CounselorTask>(model.Data.Id);
				e.HistoryLink = ApiController.WriteHistory(e.HistoryLink, "Удаление задачи", string.Empty, StateMachineStateEnum.Deleted, e.StateId);
				e.HistoryLinkId = e.HistoryLink?.Id;
				e.StateId = StateMachineStateEnum.Deleted;


				UnitOfWork.Update(e);
				return RedirectToAction("List");
			}

			using (var tran = UnitOfWork.GetTransactionScope())
			{
				var entity = model.BuildData();
				entity = ApiController.Save(entity);

				if (!string.IsNullOrEmpty(model.StateMachineActionString))
				{
					var action = ApiStateController.GetAction(model.StateMachineActionString);
					if (action != null && action.ToStateId.HasValue && Security.HasRight(model.StateMachineActionString))
					{
						entity.HistoryLink = ApiController.WriteHistory(entity.HistoryLink, "Изменение статуса",
							$"c '{entity.NullSafe(e => e.State.Name)}' на '{action.NullSafe(a => a.ToState.Name)}'", action.ToStateId, entity.StateId);
						entity.HistoryLinkId = entity.HistoryLink?.Id;
                        entity.StateId = action.ToStateId;
						UnitOfWork.SaveChanges();

						var email = entity.GetEmail();
						if (!string.IsNullOrEmpty(email))
						{
							UnitOfWork.AddEntity(new SendEmailAndSms
							{
								IsSmsSended = true,
								DateCreate = DateTime.Now,
								EmailTitle = entity.GetSubject(),
								EmailMessage = entity.GetBody(),
								Email = email
							});
						}
					}
				}

				tran.Complete();
				return RedirectToAction("Edit", new {id = model.Data.Id});
			}
		}

		/// <summary>
		///     редактирование
		/// </summary>
		/// <returns></returns>
		public ActionResult Edit(long id, bool? response, bool? markAsReaded)
		{
			SetUnitOfWorkInRefClass(UnitOfWork);
			response = response ?? false;
			var task = UnitOfWork.GetById<CounselorTask>(id);
			if ((!Security.HasRight(AccessRightEnum.CounselorTask.Edit) && !Security.HasRight(AccessRightEnum.CounselorTask.View)) || task.StateId == StateMachineStateEnum.Deleted)
			{
				return RedirectToAvalibleAction();
			}

			if (response.Value && task.Author != null && task.Executor != null)
			{
				var newTask = new CounselorTask
				{
					NotNecessary = true,
					Parent = task,
					ParentId = task.Id,
					Author = task.Executor,
					AuthorId = task.ExecutorId,
					Executor = task.Author,
					ExecutorId = task.AuthorId,
					DateCreate = DateTime.Now.Date,
					Files = new List<CounselorTaskFile>(),
					Bout = task.Bout,
					BoutId = task.BoutId,
					StateId = StateMachineStateEnum.CounselorTask.Sended,
					State = new StateMachineState { Id = StateMachineStateEnum.CounselorTask.Sended , Name = "Черновик"},
					BaseTask = task.BaseTask,
					Subject = $"Re: {task.Subject}"
				};

				task = newTask;
			}

			var curAccountId = Security.GetCurrentAccountId();

			if (markAsReaded.HasValue && markAsReaded.Value && task.Id > 0
				&& task.StateId == StateMachineStateEnum.CounselorTask.Sended
				&& (task.Executor?.AccountId == curAccountId || task.Executor?.AdministratorTour?.LinkedAccountId == curAccountId))
			{
				var stateInt = ApiStateController.GetState(StateMachineStateEnum.CounselorTask.Sended);
				task.State = stateInt;
				task.StateId = stateInt.Id;
				UnitOfWork.SaveChanges();
			}

			var state = task.StateId.HasValue ? task.State : null;
			var actions = task.Id != 0 && task.StateId.HasValue
				? ApiStateController.GetActions(state, StateMachineEnum.CounselorTask)
				: new List<StateMachineAction>();
			var isEditable = Security.HasRight(AccessRightEnum.CounselorTask.Edit) && task.StateId == StateMachineStateEnum.CounselorTask.Delivered;

			var currentAccountId = Security.GetCurrentAccountId() ?? 0;

			var isAuthorView = (currentAccountId == task.NullSafe(t => t.Author.AccountId) ||
			          currentAccountId == task.NullSafe(t => t.Author.AdministratorTour.LinkedAccountId) ||
			          currentAccountId == task.NullSafe(t => t.Author.Counselors.LinkedAccountId));


			var isExecutorView = (currentAccountId == task.NullSafe(t => t.Executor.AccountId) ||
					  currentAccountId == task.NullSafe(t => t.Executor.AdministratorTour.LinkedAccountId) ||
					  currentAccountId == task.NullSafe(t => t.Executor.Counselors.LinkedAccountId));

			if (!isAuthorView && !Security.HasRight(AccessRightEnum.CounselorTask.ViewAll))
			{
				actions.RemoveAll(a => a.ActionCode == AccessRightEnum.CounselorTask.ToSolved);
				actions.RemoveAll(a => a.ActionCode == AccessRightEnum.CounselorTask.ToCompletion);
			}

			if (!isExecutorView && !Security.HasRight(AccessRightEnum.CounselorTask.ViewAll))
			{
				actions.RemoveAll(a => a.ActionCode == AccessRightEnum.CounselorTask.ToDelivered);
			}

			var isEditableTask = ((Security.HasRight(AccessRightEnum.CounselorTask.EditDeliveredTask) ||
			                        (Security.HasRight(AccessRightEnum.CounselorTask.Edit) &&
			                         isAuthorView)) &&
			                       task.StateId == StateMachineStateEnum.CounselorTask.Delivered) || (task.StateId == StateMachineStateEnum.CounselorTask.Sended && task.Id == 0);
			var model = new CounselorTaskEditModel(task,
				UnitOfWork.GetSet<CounselorTask>()
					.Where(t => t.ParentId == id && t.StateId.HasValue && t.StateId != StateMachineStateEnum.Deleted)
					.ToList())
			{
				IsEditable = isEditable,
				IsEditableTask =
					isEditableTask,
				State =
					new ViewModelState
					{
						Actions = actions,
						State = state,
						FormSelector = "#form",
						ActionSelector = "#StateMachineActionString",
						CanReturn = true,
						ReturnController = "CounselorTask",
						ReturnAction = "List",
						NeedSaveButton = isEditable || isEditableTask,
						NeedRemoveButton = task.StateId == StateMachineStateEnum.CounselorTask.Delivered,
						JsFunctionToAction = "confirmStateButtonTask"
					}
			};

			model.State.PostNoStatusActions = model.State.PostNoStatusActions ?? new List<NoStatusAction>();
			if (!response.Value)
			{
				if (model.NullSafe(m => m.Data.Executor.AccountId) == currentAccountId ||
				    model.NullSafe(m => m.Data.Executor.AdministratorTour.LinkedAccountId) == currentAccountId ||
				    model.NullSafe(m => m.Data.Executor.Counselors.LinkedAccountId) == currentAccountId)
				{
					model.State.PostNoStatusActions.Add(new NoStatusAction
					{
						Name = "Написать сообщение",
						ButtonClass = "btn btn-default",
						Action = "Edit",
						ActionParameters = new {@id = model.Data.Id, response = true},
						Controller = "CounselorTask"
					});
				}

				model.State.PostNoStatusActions.Add(new NoStatusAction
				{
					Name = "История",
					ButtonClass = "btn btn-default btn-hystory-link",
					SomeAddon = string.Format("data-history-id=\"{0}\"", model.Data.HistoryLinkId)
				});
			}

			return View(model);
		}

		/// <summary>
		///     создание
		/// </summary>
		/// <returns></returns>
		public ActionResult Create(long? parentTaskId, bool? notNessary)
		{
			if (!Security.HasRight(AccessRightEnum.CounselorTask.ToDelivered))
			{
				return RedirectToAvalibleAction();
			}

			SetUnitOfWorkInRefClass(UnitOfWork);


			var model = new CounselorTaskModel
							{
								CoworkersFilterModel = new CoworkersFilterModel
									                       {
										                       PageNumber = 1,
															   ParentTaskId = parentTaskId
									                       },
								ParentTaskId = parentTaskId,
								NotNessary = notNessary ?? false
							};

			if (parentTaskId.HasValue)
			{
				var pt = UnitOfWork.GetById<CounselorTask>(parentTaskId.Value);
				model.Deadline = pt.DatePlanFinish;
				model.Title = pt.Subject;
				model.Content = pt.Body;
				model.NotNessary = notNessary ?? pt.NotNecessary;
			}

			SetVocabulariesForCoworkers(model.CoworkersFilterModel);
			if (!model.CoworkersFilterModel.YearOfRestId.HasValue || model.CoworkersFilterModel.YearOfRestId == 0)
			{
				var curYear = model.CoworkersFilterModel.YearsOfRest.FirstOrDefault(y => y.Year == DateTime.Now.Year) ??
							  model.CoworkersFilterModel.YearsOfRest.LastOrDefault();
				if (curYear != null)
				{
					model.CoworkersFilterModel.YearOfRestId = curYear.Id;
				}
			}

			model.CoworkersFilterModel = GetCoworkersModel(model.CoworkersFilterModel);

			return View(model);
		}

		[HttpPost]
		public ActionResult CreateTask(CounselorTaskModel model)
		{
			if (!Security.HasRight(AccessRightEnum.CounselorTask.ToDelivered))
			{
				return RedirectToAvalibleAction();
			}

			SetUnitOfWorkInRefClass(UnitOfWork);
			ApiController.CreateTask(model);

			if (model.ParentTaskId.HasValue)
			{
				return RedirectToAction("Edit", new {@id = model.ParentTaskId.Value});
			}

			return RedirectToAction("List");
		}

		/// <summary>
		/// Поиск сотрудников
		/// </summary>
		/// <returns></returns>
		[HttpPost]
		public ActionResult GetCoworkers(CoworkersFilterModel filter)
		{
			SetUnitOfWorkInRefClass(UnitOfWork);
			filter = GetCoworkersModel(filter);

			return View("Coworkers", filter);
		}

		private CoworkersFilterModel GetCoworkersModel(CoworkersFilterModel filter)
		{
			filter = filter ?? new CoworkersFilterModel();
			filter.Result = ApiController.GetCoworkers(filter);
			SetVocabulariesForCoworkers(filter);

			filter.PageLast = filter.Result.TotalItemCount/WebApi.CounselorTaskController.CoworkersPageSize +
			                  (filter.Result.TotalItemCount%WebApi.CounselorTaskController.CoworkersPageSize > 0 ? 1 : 0) - 1;

			filter.PageStart = filter.PageNumber - 5;
			if (filter.PageStart < 0)
			{
				filter.PageStart = 0;
			}

			filter.PageEnd = filter.PageStart + 10;
			if (filter.PageEnd > filter.PageLast)
			{
				filter.PageEnd = filter.PageLast;
			}

			return filter;
		}

		private void SetVocabulariesForCoworkers(CoworkersFilterModel filter)
		{
			filter.HotelTypes = ApiHotelTypeController.Get().ToList();
			filter.GroupedTimeOfRest = UnitOfWork.GetSet<GroupedTimeOfRest>().OrderBy(g=>g.Name).ToList();
			filter.SubjectsOfRest = ApiSubjectOfRestController.Get().ToList();
			filter.YearsOfRest = ApiRestYearController.Get().ToList();
		}
	}
}
