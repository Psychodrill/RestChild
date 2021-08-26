using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RestChild.Comon;
using RestChild.Comon.Enumeration;
using RestChild.Extensions.Filter;
using RestChild.DAL.RepositoryExtensions;
using RestChild.Domain;
using RestChild.Web.Properties;

namespace RestChild.Web.Controllers.WebApi
{
	[Authorize]
	public class WebAdministratorTourController : WebGenericRestController<AdministratorTour>
	{
		public StateController ApiStateController { get; set; }

		public override void SetUnitOfWorkInRefClass(IUnitOfWork unitOfWork)
		{
			base.SetUnitOfWorkInRefClass(unitOfWork);
			ApiStateController.SetUnitOfWorkInRefClass(unitOfWork);
		}

		/// <summary>
		/// создать пользователя.
		/// </summary>
		internal void CreateAccount(long id)
		{
			SetUnitOfWorkInRefClass(UnitOfWork);

			var administratorTour = UnitOfWork.GetById<AdministratorTour>(id);
			if (administratorTour == null)
			{
				return;
			}

			using (var tran = UnitOfWork.GetTransactionScope())
			{
				if (!administratorTour.LinkedAccountId.HasValue && !string.IsNullOrEmpty(administratorTour.Email))
				{
					var account = UnitOfWork.GetSet<Account>().FirstOrDefault(a => a.Login == administratorTour.Email);

					if (account == null)
					{

						var password = PasswordUtility.GeneratePassword(8);

						var role =
							UnitOfWork.GetSet<Role>().FirstOrDefault(r => r.Name.Contains(AccessRightEnum.AdministratorTour.CreateAccount)) ??
							UnitOfWork.AddEntity(new Role
							{
								Name = string.Format("Администратор заезда ({0})", AccessRightEnum.AdministratorTour.CreateAccount),
								CreateUserId = Security.GetCurrentAccountId(),
								AccessRights =
									UnitOfWork.GetSet<AccessRight>()
										.Where(
											a =>
												new[]
												{
													AccessRightEnum.CounselorTask.View, AccessRightEnum.CounselorTask.Edit,
													AccessRightEnum.CounselorTask.ToApproved, AccessRightEnum.CounselorTask.ToDelivered,
													AccessRightEnum.CounselorTask.ToSolved, AccessRightEnum.CounselorTask.ToCompletion,
													AccessRightEnum.CounselorTask.DeleteTask
												}.Contains(a.Code))
										.ToList()
							});

						var salt = PasswordUtility.GenerateSalt();
						account = UnitOfWork.AddEntity(new Account()
						{
							CreateUserId = Security.GetCurrentAccountId(),
							DateCreate = DateTime.Now,
							DateUpdate = DateTime.Now,
							Email = administratorTour.Email,
							Login = administratorTour.Email,
							IsActive = true,
							Name = administratorTour.GetFio(),
							Position = "Администратор заезда",
							Phone = administratorTour.Phone,
							Password = Convert.ToBase64String(PasswordUtility.GetPasswordHash(password, salt)),
							Salt = Convert.ToBase64String(salt)
						});

						administratorTour.Password = account.Password;
						administratorTour.Salt = administratorTour.Salt;

						UnitOfWork.AddEntity(new AccountRoles {Account = account, AccountId = account.Id, Role = role, RoleId = role.Id});

						UnitOfWork.AddEntity(new SendEmailAndSms()
						{
							IsSmsSended = true,
							DateCreate = DateTime.Now,
							EmailTitle = "Создание пользователя АИС Отдых (Администратор смены)",
							EmailMessage =
								string.Format("Для Вас был создан пользователь<br/> Имя пользователя: {0}<br/>Пароль: {1}<br/>Ссылка на систему <a href='{2}'>{2}</a>.",
									administratorTour.Email, password, Settings.Default.SystemUrl),
							Email = administratorTour.Email
						});
					}

					administratorTour.LinkedAccountId = account.Id;
					UnitOfWork.SaveChanges();

				}

				tran.Complete();
			}
		}

		internal bool ChangeStatus(long id, string actionStateCode)
		{
			SetUnitOfWorkInRefClass(UnitOfWork);
			var administratorTour = UnitOfWork.GetById<AdministratorTour>(id);

			if (administratorTour == null)
			{
				throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not found"));
			}

			if (actionStateCode == "Delete")
			{
				administratorTour.StateId = StateMachineStateEnum.Deleted;
			}
			else
			{
				if (GetErrorsOfChageStatus(id, actionStateCode).Any())
				{
					return false;
				}

				var action = ApiStateController.GetAction(actionStateCode);
				if (action != null && action.ToStateId.HasValue)
				{
					administratorTour.StateId = action.ToStateId;
				}
			}

			UnitOfWork.SaveChanges();
			return true;
		}

		public CommonPagedList<AdministratorTour> Get(AdministratorTourFilterModel filter)
		{
			SetUnitOfWorkInRefClass(UnitOfWork);
			var pageSize = Settings.Default.TablePageSize;
			var pageNumber = filter != null ? filter.PageNumber : 1;
			var startRecord = (pageNumber - 1)*pageSize;
			var query =
				UnitOfWork.GetSet<AdministratorTour>().AsQueryable();
			if (filter != null)
			{
				if (filter.AgeFrom.HasValue)
				{
					var dateFrom = DateTime.Today.AddYears(-filter.AgeFrom.Value);
					query = query.Where(c => c.DateOfBirth <= dateFrom);
				}
				if (filter.AgeTo.HasValue)
				{
					var dateTo = DateTime.Today.AddYears(-filter.AgeTo.Value - 1).AddSeconds(1);
					query = query.Where(c => c.DateOfBirth >= dateTo);
				}
				if (filter.IsMale.HasValue)
				{
					query = query.Where(c => c.Male == filter.IsMale.Value);
				}
				if (!string.IsNullOrEmpty(filter.Name))
				{
					query = query.Where(c => (c.LastName + " " + c.FirstName + " " + c.MiddleName).ToLower().Contains(filter.Name.ToLower()));
				}
				if (filter.OnlyVacant)
				{
					if (filter.VacantForBoutId.HasValue)
					{
						var targetBout = UnitOfWork.GetById<Bout>(filter.VacantForBoutId.Value);
						if (targetBout != null)
						{
							if (targetBout.GroupedTimeOfRestId.HasValue)
							{
								query = query.Where(c => c.Bouts.All(p => p.GroupedTimeOfRestId != targetBout.GroupedTimeOfRestId));
							}
							else if (targetBout.DateIncome.HasValue && targetBout.DateOutcome.HasValue)
							{
								query = query.Where(c => c.Bouts.All(p => p.DateIncome <= targetBout.DateOutcome && p.DateOutcome >= targetBout.DateIncome));
							}
						}
					}
				}

				if (filter.StateId.HasValue)
				{
					query = query.Where(c => c.StateId == filter.StateId);
				}
			}

			var totalCount = query.Count();
			var entity =
				query.OrderBy(t => t.LastName).Skip(startRecord).Take(pageSize).ToList().Select(c => new AdministratorTour(c, 2)).ToList();
			return new CommonPagedList<AdministratorTour>(entity, pageNumber, pageSize, totalCount);
		}

		[Route("api/WebAdministratorTour")]
		public List<AdministratorTour> Get(string name)
		{
			SetUnitOfWorkInRefClass(UnitOfWork);
			return Get(new AdministratorTourFilterModel()
			{
				Name = name,
				StateId = StateMachineStateEnum.Counselor.Approved
			}).ToList();
		}

		public override AdministratorTour Post(AdministratorTour entity)
		{
			SetUnitOfWorkInRefClass(UnitOfWork);
			if (entity == null)
			{
				return null;
			}
			using (var transaction = UnitOfWork.GetTransactionScope())
			{
				var result = base.Post(entity);
				transaction.Complete();
				return result;
			}
		}

		public override AdministratorTour Put(long id, AdministratorTour entity)
		{
			SetUnitOfWorkInRefClass(UnitOfWork);
			if (entity == null)
			{
				return null;
			}

			using (var transaction = UnitOfWork.GetTransactionScope())
			{
				base.Put(id, entity);
				var persisted = UnitOfWork.GetById<AdministratorTour>(id);
				UnitOfWork.SaveChanges();
				transaction.Complete();
				return persisted;
			}
		}

		public bool ChangeState(long id, string stateMachineActionString)
		{
			SetUnitOfWorkInRefClass(UnitOfWork);
			var result = false;
			return result;

		}

		public List<string> GetErrorsOfChageStatus(long id, string actionCode)
		{
			SetUnitOfWorkInRefClass(UnitOfWork);
			return new List<string>();
		}

		public List<DocumentType> GetAvailableDocumentTypes()
		{
			SetUnitOfWorkInRefClass(UnitOfWork);
			return
				UnitOfWork.GetSet<DocumentType>().Where(t => t.ForApplicant).ToList().Select(t => new DocumentType(t)).ToList();
		}

		public List<StateMachineState> GetStates()
		{
			SetUnitOfWorkInRefClass(UnitOfWork);
			return UnitOfWork.GetSet<StateMachineState>()
				.Where(s => s.StateMachineId == (long)StateMachineEnum.AdministratorTour)
				.ToList()
				.Select(s => new StateMachineState(s))
				.ToList();
		}

		public List<AdministratorTour> GetAdministratorToursForAccount()
		{
			SetUnitOfWorkInRefClass(UnitOfWork);
			var accountId = Security.GetCurrentAccountId();
			return UnitOfWork.GetSet<AdministratorTour>().Where(t => t.LinkedAccountId == accountId).ToList();
		}
	}
}
