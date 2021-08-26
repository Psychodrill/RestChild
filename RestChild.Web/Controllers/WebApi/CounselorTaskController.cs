using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Http;
using RestChild.Comon;
using RestChild.Comon.Enumeration;
using RestChild.Extensions.Extensions;
using RestChild.Extensions.Filter;
using RestChild.DAL.RepositoryExtensions;
using RestChild.Domain;
using RestChild.Web.Extensions;
using RestChild.Web.Models;
using RestChild.Web.Properties;

namespace RestChild.Web.Controllers.WebApi
{
	[Authorize]
	public class CounselorTaskController : BaseController
	{
		public const int CoworkersPageSize = 5;

		/// <summary>
		///     получить задачу
		/// </summary>
		internal CounselorTask GetCounselorTask(long id)
		{
			return UnitOfWork.GetById<CounselorTask>(id);
		}

		public List<StateMachineState> GetStates()
		{
			SetUnitOfWorkInRefClass(UnitOfWork);
			return
				UnitOfWork.GetSet<StateMachineState>()
					.Where(s => s.StateMachineId == (long?)StateMachineEnum.CounselorTask)
					.ToList()
					.Select(s => new StateMachineState(s))
					.ToList();
		}

		/// <summary>
		///     Поиск заезда
		/// </summary>
		/// <param name="filter">Фильтр</param>
		internal CounselorTaskListModel Search(CounselorTaskListModel filter)
		{
			SetUnitOfWorkInRefClass(UnitOfWork);
			filter = filter ?? new CounselorTaskListModel();
			int pageSize = Settings.Default.TablePageSize;
			var pageNumber = filter.PageNumberEx ?? filter.PageNumber;
			int startRecord = (pageNumber - 1) * pageSize;

			IQueryable<CounselorTask> query;
			if (Security.HasRight(AccessRightEnum.CounselorTask.View))
			{
				query = UnitOfWork.GetSet<CounselorTask>().Where(t => t.StateId != null && t.StateId != StateMachineStateEnum.Deleted);
			}
			else
			{
				return new CounselorTaskListModel(new List<CounselorTask>(), 1, 1, 0);
			}

			query = FilterSearch(query, filter);

			query = query.Where(q => !q.BaseTask);

			int totalCount = query.Count();
			List<CounselorTask> entity =
				query.OrderByDescending(d=>d.DateUpdate).ThenByDescending(b => b.Id).Skip(startRecord).Take(pageSize).ToList();
			return new CounselorTaskListModel(entity, pageNumber, pageSize, totalCount)
			{
				YearOfRestId = filter.YearOfRestId,
				Body = filter.Body,
				ExecutionEndDate = filter.ExecutionEndDate,
				ExecutionStartDate = filter.ExecutionStartDate,
				HotelsId = filter.HotelsId,
				StateId = filter.StateId,
				Subject = filter.Subject,
				TaskEndDate = filter.TaskEndDate,
				TaskStartDate = filter.TaskStartDate,
				GroupedTimeOfRestId = filter.GroupedTimeOfRestId
			};
		}

		private static IQueryable<CounselorTask> FilterSearch(IQueryable<CounselorTask> query, CounselorTaskListModel filter)
		{
			if (filter.YearOfRestId.HasValue)
			{
				query = query.Where(q => q.Bout.YearOfRestId == filter.YearOfRestId);
			}

			if (filter.GroupedTimeOfRestId.HasValue)
			{
				query = query.Where(q => q.Bout.GroupedTimeOfRestId == filter.GroupedTimeOfRestId);
			}

			if (filter.HotelsId.HasValue)
			{
				query = query.Where(q => q.Bout.HotelsId == filter.HotelsId);
			}

			if (filter.StateId.HasValue && filter.StateId > 0)
			{
				query = query.Where(q => q.StateId == filter.StateId);
			}

			if (filter.ExecutionStartDate.HasValue)
			{
				var esd = filter.ExecutionStartDate.Value.Date;
				query = query.Where(q => q.DatePlanFinish >= esd);
			}

			if (filter.ExecutionEndDate.HasValue)
			{
				var eed = filter.ExecutionEndDate.Value.Date.AddDays(1);
				query = query.Where(q => q.DatePlanFinish < eed);
			}

			if (filter.TaskStartDate.HasValue)
			{
				var esd = filter.TaskStartDate.Value.Date;
				query = query.Where(q => q.DateCreate >= esd);
			}

			if (filter.TaskEndDate.HasValue)
			{
				var eed = filter.TaskEndDate.Value.Date.AddDays(1);
				query = query.Where(q => q.DateCreate < eed);
			}

			if (!string.IsNullOrWhiteSpace(filter.Body))
			{
				query = query.Where(q => q.Body.ToLower().Contains(filter.Body.ToLower()));
			}

			if (!string.IsNullOrWhiteSpace(filter.Subject))
			{
				query = query.Where(q => q.Subject.ToLower().Contains(filter.Subject.ToLower()));
			}

			if (filter.NotNessesary == 1)
			{
				query = query.Where(q => !q.NotNecessary);
			}

			if (filter.NotNessesary == 2)
			{
				query = query.Where(q => q.NotNecessary);
			}

			if (!Security.HasRight(AccessRightEnum.CounselorTask.ViewAll))
			{
				var currentAccountId = Security.GetCurrentAccountId();
				query =
					query.Where(
						q =>
							q.Author.AccountId == currentAccountId || q.Author.AdministratorTour.LinkedAccountId == currentAccountId ||
							q.Author.Counselors.LinkedAccountId == currentAccountId ||
							q.Executor.AccountId == currentAccountId || q.Executor.AdministratorTour.LinkedAccountId == currentAccountId ||
							q.Executor.Counselors.LinkedAccountId == currentAccountId);
			}

			return query;
		}

		/// <summary>
		/// создание задачи
		/// </summary>
		internal void CreateTask(CounselorTaskModel model)
		{
			using (var tran = UnitOfWork.GetTransactionScope())
			{
				ResponsibilityForTask executor = null;
				CounselorTask baseTask = null;
				if (model.ParentTaskId.HasValue)
				{
					baseTask = UnitOfWork.GetById<CounselorTask>(model.ParentTaskId.Value);
					executor = baseTask.Executor;
				}
				else
				{
					executor = new ResponsibilityForTask
					{
						AccountId = Security.GetCurrentAccountId(),
						CounselorTaskExecutorTypeId = (long)TaskExecutorTypeEnum.MosgorturOperator
					};

					var addministrator = UnitOfWork.GetSet<AdministratorTour>().FirstOrDefault(a => a.LinkedAccountId == executor.AccountId);

					if (addministrator != null)
					{
						executor.CounselorTaskExecutorTypeId = (long)TaskExecutorTypeEnum.Administrator;
						executor.AdministratorTourId = addministrator.Id;
						executor.AdministratorTour = addministrator;
					}

					var counselors = UnitOfWork.GetSet<Counselors>().FirstOrDefault(a => a.LinkedAccountId == executor.AccountId);

					if (counselors != null)
					{
						executor.CounselorTaskExecutorTypeId = (long)TaskExecutorTypeEnum.SeniorCounselor;
						executor.CounselorsId = counselors.Id;
						executor.Counselors = counselors;
					}

					executor = UnitOfWork.AddEntity(executor);

					if (model.Performers.Count >= 1)
					{
						baseTask = UnitOfWork.AddEntity(new CounselorTask
						{
							Author = executor,
							AuthorId = executor.Id,
							Body = model.Content,
							Subject = model.Title,
							DateUpdate = DateTime.Now,
							BaseTask = true,
							DateCreate = model.DateTask,
							DatePlanFinish = model.Deadline,
							StateId =
								model.NotNessary ? StateMachineStateEnum.CounselorTask.Sended : StateMachineStateEnum.CounselorTask.Delivered,
							NotNecessary = model.NotNessary,
							Files =
								(model.TaskFiles?.Where(f => f != null) ?? new List<CounselorTaskFile>()).Select(f => new CounselorTaskFile(f) {DataCreate = DateTime.Now})
									.ToList()
						});
					}
					if (baseTask != null)
					{
						baseTask.HistoryLink = this.WriteHistory(baseTask.HistoryLink, "Первое сохранение.", string.Empty);
						baseTask.HistoryLinkId = baseTask.HistoryLink?.Id;
						UnitOfWork.SaveChanges();
					}
				}

				foreach (var perfomer in model.Performers)
				{
					var perfomerEntity = UnitOfWork.AddEntity(new ResponsibilityForTask
					{
						CounselorTaskExecutorTypeId = perfomer.CoworkerType,
						AdministratorTourId =
							perfomer.CoworkerType == (long) TaskExecutorTypeEnum.Administrator ? perfomer.PerformerId : (long?) null,
						CounselorsId =
							perfomer.CoworkerType == (long) TaskExecutorTypeEnum.Counselor ||
							perfomer.CoworkerType == (long) TaskExecutorTypeEnum.SeniorCounselor ||
							perfomer.CoworkerType == (long) TaskExecutorTypeEnum.SwingCounselor
								? perfomer.PerformerId
								: (long?) null,
						BoutId = perfomer.BoutId,
						PartyId = perfomer.PartyId > 0 ? perfomer.PartyId : null
					});

					var persistent = UnitOfWork.AddEntity(new CounselorTask
					{
						Author = executor,
						DateUpdate = DateTime.Now,
						AuthorId = executor == null ? (long?) null : executor.Id,
						Body = model.Content,
						Subject = model.Title,
						DateCreate = model.DateTask,
						DatePlanFinish = model.Deadline,
						StateId = model.NotNessary ? StateMachineStateEnum.CounselorTask.Sended : StateMachineStateEnum.CounselorTask.Delivered,
						Files = (model.TaskFiles?.Where(f=>f != null) ?? new List<CounselorTaskFile>()).Select(f => new CounselorTaskFile(f) { DataCreate = DateTime.Now }).ToList(),
						ParentId = baseTask != null ? baseTask.Id : (long?) null,
						BaseTask = false,
						ExecutorId = perfomerEntity.Id,
						Executor = perfomerEntity,
						NotNecessary = model.NotNessary,
						BoutId = perfomerEntity.BoutId
					});

					persistent.HistoryLink = this.WriteHistory(persistent.HistoryLink, "Первое сохранение.", string.Empty);
					persistent.HistoryLinkId = persistent.HistoryLink?.Id;

					var email = persistent.GetEmail();
					if (!string.IsNullOrEmpty(email))
					{
						UnitOfWork.AddEntity(new SendEmailAndSms
						{
							IsSmsSended = true,
							DateCreate = DateTime.Now,
							EmailTitle = persistent.GetSubject(),
							EmailMessage = persistent.GetBody(),
							Email = email
						});
					}

					UnitOfWork.SaveChanges();
				}

				tran.Complete();
			}
		}

		internal string GenerateDiff(CounselorTask source, CounselorTask target)
		{
			var sb = new StringBuilder();
			if (source == null && target != null)
			{
				sb.AppendLineLi("Первое сохранение.");
			}
			else if (target != null)
			{
				if (source.Subject != target.Subject)
				{
					sb.AppendLineLi(string.Format("Изменено тема с '{0}' на '{1}'", source.Subject.FormatEx(), target.Subject.FormatEx()));
				}

				if (source.Body != target.Body)
				{
					sb.AppendLineLi(string.Format("Изменено задание с '{0}' на '{1}'", source.Body, target.Body));
				}

				if (source.DateCreate != target.DateCreate)
				{
					sb.AppendLineLi(string.Format("Изменено 'Дата постановки' с '{0}' на '{1}'", source.DateCreate.FormatEx(), target.DateCreate.FormatEx()));
				}

				if (source.DatePlanFinish != target.DatePlanFinish)
				{
					sb.AppendLineLi(string.Format("Изменено 'Срок исполнения(план)' с '{0}' на '{1}'", source.DatePlanFinish.FormatEx(), target.DatePlanFinish.FormatEx()));
				}

				if (source.DateFactFinish != target.DateFactFinish)
				{
					sb.AppendLineLi(string.Format("Изменено 'Срок исполнения(факт)' с '{0}' на '{1}'", source.DateFactFinish.FormatEx(), target.DateFactFinish.FormatEx()));
				}

				if (source.Report != target.Report)
				{
					sb.AppendLineLi(string.Format("Изменено 'Отчет о выполнении' с '{0}' на '{1}'", source.Report, target.Report));
				}

				if (target.Files != null && target.Files.Any(p => p!=null && p.Id == 0))
				{
					sb.AppendLineLi(string.Format("Добавлены файлы: {0}",
						string.Join(", ", target.Files.Where(p => p.Id == 0).Select(p => p.FileTitle.FormatEx()))));
				}

				if (target.Files != null && target.ReportFiles.Any(p => p != null && p.Id == 0))
				{
					sb.AppendLineLi(string.Format("Добавлены файлы отчета: {0}",
						string.Join(", ", target.ReportFiles.Where(p => p.Id == 0).Select(p => p.FileTitle.FormatEx()))));
				}

				var ids = target.Files != null ? target.Files.Where(p=>p != null).Select(p => p.Id).ToList() : new List<long>();

				if (source.Files != null && target.Files != null && source.Files.Where(p => p != null).Any(p => !ids.Contains(p.Id)))
				{
					sb.AppendLineLi(string.Format("Удалены файлы: {0}<span style='display:none'>{1}</span>",
						string.Join(", ", source.Files.Where(p => p != null).Where(p => !ids.Contains(p.Id)).Select(p => p.FileTitle.FormatEx())),
						string.Join(", ", source.Files.Where(p => p != null).Where(p => !ids.Contains(p.Id)).Select(p => p.FileName.FormatEx())))
						);
				}

				ids = target.ReportFiles != null ? target.ReportFiles.Where(p => p != null).Select(p => p.Id).ToList() : new List<long>();

				if (source.ReportFiles != null && target.ReportFiles != null && source.ReportFiles.Where(p => p != null).Any(p => !ids.Contains(p.Id)))
				{
					sb.AppendLineLi(string.Format("Удалены файлы отчета: {0}<span style='display:none'>{1}</span>",
						string.Join(", ", source.ReportFiles.Where(p => p != null).Where(p => !ids.Contains(p.Id)).Select(p => p.FileTitle.FormatEx())),
						string.Join(", ", source.ReportFiles.Where(p => p != null).Where(p => !ids.Contains(p.Id)).Select(p => p.FileName.FormatEx()))));
				}
			}

			return string.Format("<ul>{0}</ul>", sb);
		}

		internal CounselorTask Save(CounselorTask model)
		{
			if (model.Id > 0)
			{
				var entity = UnitOfWork.GetById<CounselorTask>(model.Id);
				if (entity.StateId != model.StateId)
				{
					return entity;
				}

				var diff = GenerateDiff(entity, model);
				entity.Body = model.Body;
				entity.DateCreate = model.DateCreate;
				entity.DateFactFinish = model.DateFactFinish;
				entity.DatePlanFinish = model.DatePlanFinish;
				entity.Report = model.Report;
				entity.Subject = model.Subject;
				entity.DateUpdate = DateTime.MaxValue;

				entity = UnitOfWork.Update(entity);

				var fileToAdd = model.Files.Where(f => f.Id == 0).ToList();
				foreach (var file in fileToAdd)
				{
					file.DataCreate = DateTime.Now;
					file.CounselorTaskId = entity.Id;
					UnitOfWork.AddEntity(file);
				}

				var fileToRemove = entity.Files.Where(f => !model.Files.Select(mf => mf.Id).Contains(f.Id)).ToList();
				foreach (var file in fileToRemove)
				{
					UnitOfWork.Delete(file);
				}

				var fileReportToAdd = model.ReportFiles.Where(f => f.Id == 0).ToList();
				foreach (var file in fileReportToAdd)
				{
					file.DataCreate = DateTime.Now;
					file.CounselorTaskId = entity.Id;
					UnitOfWork.AddEntity(file);
				}

				var fileReportToRemove =
					entity.ReportFiles.Where(f => !model.ReportFiles.Select(mf => mf.Id).Contains(f.Id)).ToList();
				foreach (var file in fileReportToRemove)
				{
					UnitOfWork.Delete(file);
				}

				entity.HistoryLink = this.WriteHistory(entity.HistoryLink, "Сохранение", diff);
				entity.HistoryLinkId = entity.HistoryLink?.Id;
				entity.LastUpdateTick = DateTime.Now.Ticks;
				UnitOfWork.SaveChanges();
				return entity;
			}
			else
			{
				model.DateUpdate = DateTime.Now;

				var entity = UnitOfWork.AddEntity(model);
				entity.HistoryLink = this.WriteHistory(entity.HistoryLink, "Сохранение", string.Empty);
				entity.HistoryLinkId = entity.HistoryLink?.Id;
				entity.LastUpdateTick = DateTime.Now.Ticks;

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

				UnitOfWork.SaveChanges();

				return entity;
			}
		}

		/// <summary>
		/// Поиск сотрудников
		/// </summary>
		/// <returns></returns>
		public CommonPagedList<IGrouping<Bout, Coworker>> GetCoworkers(CoworkersFilterModel filter)
		{
			var pageSize = CoworkersPageSize;
			var pageNumber = filter != null ?  filter.PageNumber : 1;
			var startRecord = (pageNumber - 1) * pageSize;
			var totalCount = 0;
			List<IGrouping<Bout, Coworker>> result = new List<IGrouping<Bout, Coworker>>();

			var currentAccountId = Security.GetCurrentAccountId();

			var isMosgortourOperator = Security.HasRight(AccessRightEnum.CounselorTask.ViewAll);
			var isAddministrator = isMosgortourOperator || UnitOfWork.GetSet<AdministratorTour>().Any(a => a.LinkedAccountId == currentAccountId);
			var isCounselors = isAddministrator || UnitOfWork.GetSet<Counselors>().Any(a => a.LinkedAccountId == currentAccountId);

			if (filter != null)
			{
				var query = UnitOfWork.GetSet<Bout>().Where(c => c.StateId != null && c.StateId != StateMachineStateEnum.Deleted);

				var dateOfCheck = DateTime.Now.Date;
				var dateStart = dateOfCheck.AddDays(-7);
				var dateEnd = dateOfCheck.AddDays(7);

				query = query.Where(c => c.Tours.Any(t => t.DateIncome <= dateEnd && t.DateOutcome >= dateStart));

				if (filter.ParentTaskId.HasValue)
				{
					var task = UnitOfWork.GetById<CounselorTask>(filter.ParentTaskId.Value);
					query = query.Where(b => b.Id == task.BoutId);
				}
				else
				{
					if (filter.SubjectOfRestId.HasValue)
					{
						query = query.Where(b => b.SubjectOfRestId == filter.SubjectOfRestId);
					}

					if (filter.GroupedTimeOfRestId.HasValue)
					{
						query = query.Where(b => b.GroupedTimeOfRestId == filter.GroupedTimeOfRestId);
					}

					if (filter.HotelTypeId.HasValue)
					{
						query = query.Where(b => b.Hotels.HotelTypeId == filter.HotelTypeId);
					}

					if (filter.YearOfRestId.HasValue)
					{
						query = query.Where(b => b.YearOfRestId == filter.YearOfRestId);
					}

					if (filter.HotelId.HasValue)
					{
						query = query.Where(b => b.HotelsId == filter.HotelId);
					}
				}

				if (filter.CoworkerType.HasValue)
				{
					if (filter.CoworkerType == (long)TaskExecutorTypeEnum.Administrator)
					{
						query = query.Where(b => b.AdministratorTours.Any());
					}

					if (filter.CoworkerType == (long)TaskExecutorTypeEnum.SeniorCounselor)
					{
						query = query.Where(b => b.SeniorCounselors.Any());
					}

					if (filter.CoworkerType == (long)TaskExecutorTypeEnum.SwingCounselor)
					{
						query = query.Where(b => b.SwingCounselors.Any());
					}

					if (filter.CoworkerType == (long)TaskExecutorTypeEnum.Counselor)
					{
						query = query.Where(b => b.Partys.Any(p => p.Counselors.Any()));
					}
				}
				else
				{
					query =
						query.Where(
							b =>
								b.AdministratorTours.Any() || b.SeniorCounselors.Any() || b.SwingCounselors.Any() ||
								b.Partys.Any(p => p.Counselors.Any()));
				}


				if (!isMosgortourOperator)
				{
					query =
						query.Where(
							q =>
								q.AdministratorTours.Any(a => a.LinkedAccountId == currentAccountId) ||
								q.SeniorCounselors.Any(sc => sc.LinkedAccountId == currentAccountId));
				}


				totalCount = query.Count();
				query = query.OrderBy(b => b.Id).Skip(startRecord).Take(pageSize);

				result = new List<IGrouping<Bout, Coworker>>();

				var name = filter.Name?.ToLower();

				if (filter.CoworkerType.HasValue)
				{
					if (filter.CoworkerType == (long)TaskExecutorTypeEnum.Administrator && isMosgortourOperator)
					{
						result =
							query.SelectMany(
								b => b.AdministratorTours.Where(a=> string.IsNullOrEmpty(name) || a.LastName.Contains(name) || a.FirstName.Contains(name) || a.MiddleName.Contains(name)),
								(b, a) => new Coworker {Administrator = a, Bout = b, TaskExecutorType = TaskExecutorTypeEnum.Administrator})
								.GroupBy(c => c.Bout)
								.ToList();
					}

					if (filter.CoworkerType == (long)TaskExecutorTypeEnum.SeniorCounselor && (isAddministrator))
					{
						result =
							query.SelectMany(
								b => b.SeniorCounselors.Where(a => string.IsNullOrEmpty(name) || a.LastName.Contains(name) || a.FirstName.Contains(name) || a.MiddleName.Contains(name)),
								(b, c) => new Coworker {Counselor = c, Bout = b, TaskExecutorType = TaskExecutorTypeEnum.SeniorCounselor})
								.GroupBy(c => c.Bout)
								.ToList();
					}

					if (filter.CoworkerType == (long)TaskExecutorTypeEnum.SwingCounselor && (isCounselors))
					{
						result =
							query.SelectMany(
								b => b.SwingCounselors.Where(a => string.IsNullOrEmpty(name) || a.LastName.Contains(name) || a.FirstName.Contains(name) || a.MiddleName.Contains(name)),
								(b, c) => new Coworker { Counselor = c, Bout = b, TaskExecutorType = TaskExecutorTypeEnum.SwingCounselor })
								.GroupBy(c => c.Bout)
								.ToList();
					}

					if (filter.CoworkerType == (long)TaskExecutorTypeEnum.Counselor && (isCounselors))
					{
						result =
							query.SelectMany(
								b => b.Partys.SelectMany(p => p.Counselors.Where(a => string.IsNullOrEmpty(name) || a.LastName.Contains(name) || a.FirstName.Contains(name) || a.MiddleName.Contains(name))),
								(b, c) => new Coworker { Counselor = c, Bout = b, TaskExecutorType = TaskExecutorTypeEnum.Counselor })
								.GroupBy(c => c.Bout)
								.ToList();
					}
				}
				else
				{
					List<Coworker> list = new List<Coworker>();

					if (isMosgortourOperator)
					{
						list.AddRange(query.SelectMany(
							b => b.AdministratorTours.Where(a => string.IsNullOrEmpty(name) || a.LastName.Contains(name) || a.FirstName.Contains(name) || a.MiddleName.Contains(name)),
							(b, a) => new Coworker {Administrator = a, Bout = b, TaskExecutorType = TaskExecutorTypeEnum.Administrator}));
					}

					if (isAddministrator)
					{
						list.AddRange(query.SelectMany(
							b => b.SeniorCounselors.Where(a => string.IsNullOrEmpty(name) || a.LastName.Contains(name) || a.FirstName.Contains(name) || a.MiddleName.Contains(name)),
							(b, c) => new Coworker {Counselor = c, Bout = b, TaskExecutorType = TaskExecutorTypeEnum.SeniorCounselor})
							.ToList());
					}

					if (isCounselors)
					{
						list.AddRange(query.SelectMany(
							b => b.SwingCounselors.Where(a => string.IsNullOrEmpty(name) || a.LastName.Contains(name) || a.FirstName.Contains(name) || a.MiddleName.Contains(name)),
							(b, c) => new Coworker {Counselor = c, Bout = b, TaskExecutorType = TaskExecutorTypeEnum.SwingCounselor})
							.ToList());

						foreach (var bout in query.ToList())
						{
							foreach (var party in bout.Partys.ToList())
							{
								list.AddRange(
									party.Counselors.Where(a => string.IsNullOrEmpty(name) || a.LastName.Contains(name) || a.FirstName.Contains(name) || a.MiddleName.Contains(name)).Select(
										c =>
											new Coworker {Counselor = c, Bout = bout, TaskExecutorType = TaskExecutorTypeEnum.Counselor, Party = party}));
							}
						}
					}

					list = list.OrderBy(l => l.GetFio()).ToList();

					result.AddRange(list.GroupBy(c => c.Bout).ToList());
				}

			}

			return new CommonPagedList<IGrouping<Bout, Coworker>>(result, pageNumber, pageSize, totalCount);
		}
	}
}
