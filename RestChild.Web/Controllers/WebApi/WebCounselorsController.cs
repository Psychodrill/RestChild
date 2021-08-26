using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
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
	public class WebCounselorsController : WebGenericRestController<Counselors>
	{
		public StateController ApiStateController { get; set; }

		public override void SetUnitOfWorkInRefClass(IUnitOfWork unitOfWork)
		{
			base.SetUnitOfWorkInRefClass(unitOfWork);
			ApiStateController.SetUnitOfWorkInRefClass(unitOfWork);
		}

		public CommonPagedList<Counselors> Get(CounselorsFilterModel filter)
		{
			var pageSize = Settings.Default.TablePageSize;
			var pageNumber = filter != null ? filter.PageNumber : 1;
			var startRecord = (pageNumber - 1)*pageSize;
			var query =
				UnitOfWork.GetSet<Counselors>().Where(c => c.StateId != null && c.StateId != StateMachineStateEnum.Deleted);
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
				if (filter.StateId.HasValue)
				{
					query = query.Where(c => c.StateId == filter.StateId.Value);
				}
				if (filter.OnlyVacant)
				{
					if (filter.VacantForPartyId.HasValue)
					{
						var targetParty = UnitOfWork.GetById<Party>(filter.VacantForPartyId.Value);
						var targetGroupedTime = targetParty.NullSafe(t => t.Bouts.GroupedTimeOfRestId);
						query = query.Where(c =>
							c.Partys.All(p =>
								(p.HotelsId != targetParty.HotelsId || p.Bouts.GroupedTimeOfRestId != targetGroupedTime))
								&& c.Bouts.All(b => b.HotelsId != targetParty.HotelsId || b.GroupedTimeOfRestId != targetGroupedTime)
								&& c.SwingBoats.All(b => b.HotelsId != targetParty.HotelsId || b.GroupedTimeOfRestId != targetGroupedTime));
					}

					if (filter.VacantForBoutId.HasValue)
					{
						var targetBout = UnitOfWork.GetById<Bout>(filter.VacantForBoutId.Value);
						query = query.Where(c =>
							c.Partys.All(p =>
								(p.HotelsId != targetBout.HotelsId || p.Bouts.GroupedTimeOfRestId != targetBout.GroupedTimeOfRestId))
								&& c.Bouts.All(b => b.HotelsId != targetBout.HotelsId || b.GroupedTimeOfRestId != targetBout.GroupedTimeOfRestId)
								&& c.SwingBoats.All(b => b.HotelsId != targetBout.HotelsId || b.GroupedTimeOfRestId != targetBout.GroupedTimeOfRestId));
					}
				}

				if (filter.PedPartyId.HasValue)
				{
					query = query.Where(i => i.PedPartyId == filter.PedPartyId.Value);
				}
			}

			var totalCount = query.Count();
			var entity =
				query.OrderBy(t => t.LastName).Skip(startRecord).Take(pageSize).ToList().Select(c => new Counselors(c, 2)).ToList();
			return new CommonPagedList<Counselors>(entity, pageNumber, pageSize, totalCount);
		}

		[Route("api/WebCounselors")]
		public List<Counselors> Get(string name)
		{
			return Get(new CounselorsFilterModel
			{
				Name = name,
				StateId = StateMachineStateEnum.Counselor.Approved
			}).ToList();
		}

		public override Counselors Post(Counselors entity)
		{
			if (entity == null)
			{
				return null;
			}
			using (var transaction = UnitOfWork.GetTransactionScope())
			{
				var files = entity.Files;
				var foreignPassports = entity.ForeginPassports;
				var highSchoolGraduations = entity.HighSchoolGraduations;
				var counselorCources = entity.CounselorCources;
				var comments = entity.Comments;
				var practices = entity.CounselorPractices;
				var skills = entity.Skill;

				entity.Files = null;
				entity.ForeginPassports = null;
				entity.HighSchoolGraduations = null;
				entity.CounselorCources = null;
				entity.Comments = null;
				entity.CounselorPractices = null;
				entity.Skill = null;


				entity.HistoryLink = UnitOfWork.AddEntity(new HistoryLink
															{
																Historys =
																	new List<History>
																		{
																			UnitOfWork.AddEntity(new History
																									{
																										AccountId = Security.GetCurrentAccountId(),
																										EventCode = "Создание вожатого",
																										DateChange = DateTime.Now,
																										Commentary = string.Empty
																									})
																		}
															});
				var result = base.Post(entity);

				result.Files = result.Files ?? new List<CounselorFile>();
				if (files != null && files.Any())
				{
					var photo = files.FirstOrDefault(f => f.FileName == "photo");
					var persistedPhoto = result.Files.FirstOrDefault(f => f.FileName == "photo");

					if (photo != null)
					{
						if (persistedPhoto != null)
						{
							persistedPhoto.FileUrl = photo.FileUrl;
						}
						else
						{
							result.Files.Add(
								UnitOfWork.AddEntity(new CounselorFile() { Id = 0, CounselorsId = entity.Id, FileName = "photo", FileUrl = photo.FileUrl }));
						}
					}
					else
					{
						entity.Files.Each(f => f.CounselorsId = null);
					}
				}

				if (foreignPassports != null)
				{
					foreach (var passport in foreignPassports)
					{
						passport.CounselorsId = result.Id;
						UnitOfWork.AddEntity(passport);
					}
				}

				if (highSchoolGraduations != null)
				{
					foreach (var graduation in highSchoolGraduations)
					{
						graduation.CounselorsId = result.Id;
						UnitOfWork.AddEntity(graduation);
					}
				}

				if (counselorCources != null)
				{
					foreach (var cource in counselorCources)
					{
						cource.CounselorsId = result.Id;
						UnitOfWork.AddEntity(cource);
					}
				}

				if (practices != null)
				{
					foreach (var practice in practices)
					{
						practice.CounselorsId = result.Id;
						UnitOfWork.AddEntity(practice);
					}
				}

				if (skills != null)
				{
					foreach (var skill in skills)
					{
						skill.CounselorsId = result.Id;
						UnitOfWork.AddEntity(skill);
					}
				}

				if (comments != null)
				{
					foreach (var comment in comments)
					{
						comment.CounselorsId = result.Id;
						UnitOfWork.AddEntity(comment);
					}
				}

				UnitOfWork.SaveChanges();

				transaction.Complete();
				return result;
			}
		}

		public override Counselors Put(long id, Counselors entity)
		{
			if (entity == null)
			{
				return null;
			}

			using (var transaction = UnitOfWork.GetTransactionScope())
			{
				var files = entity.Files;
				var foreignPassports = entity.ForeginPassports;
				var highSchoolGraduations = entity.TypeOfEducationId == (long)TypeOfEducationEnum.High ? entity.HighSchoolGraduations : new List<CounselorHighSchool>();
				var counselorCources = entity.CounselorCources;
				var comments = entity.Comments;
				var practices = entity.CounselorPractices;
				var skills = entity.Skill;

				entity.Files = null;
				entity.ForeginPassports = null;
				entity.HighSchoolGraduations = null;
				entity.CounselorCources = null;
				entity.Comments = null;
				entity.CounselorPractices = null;
				entity.Skill = null;

				if (entity.MilitaryDutyId != (long)MilitaryDutyEnum.Reservist)
				{
					entity.MIlitaryCategory = null;
					entity.MilitartStaff = null;
					entity.MilitaryRank = null;
					entity.MilitaryReserveCategory = null;
					entity.VusCodeName = null;
				}

				base.Put(id, entity);
				var persisted = UnitOfWork.GetById<Counselors>(id);

				entity.Files = entity.Files ?? new List<CounselorFile>();
				if (files != null && files.Any())
				{
					var photo = files.FirstOrDefault(f => f.FileName == "photo");
					var persistedPhoto = UnitOfWork.GetSet<CounselorFile>().FirstOrDefault(f => f.FileName == "photo" && f.CounselorsId == entity.Id);

					if (photo != null)
					{
						if (persistedPhoto != null)
						{
							persistedPhoto.FileUrl = photo.FileUrl;
							UnitOfWork.Context.Entry(persistedPhoto).State = EntityState.Modified;
						}
						else if (!string.IsNullOrWhiteSpace(photo.FileUrl))
						{
							entity.Files.Add(
								UnitOfWork.AddEntity(new CounselorFile() { Id = 0, CounselorsId = entity.Id, FileName = "photo", FileUrl = photo.FileUrl }));
						}
					}
					else
					{
						entity.Files.Each(f => f.CounselorsId = null);
					}
				}

				UnitOfWork.MergeCollection(foreignPassports, persisted.ForeginPassports,
					(s, d) =>
						{
							d.PassportIssue = s.PassportIssue;
							d.PassportIssueDate = s.PassportIssueDate;
							d.PassportNumber = s.PassportNumber;
							d.PassportValidityEndDate = s.PassportValidityEndDate;
						});



				UnitOfWork.MergeCollection(highSchoolGraduations, persisted.HighSchoolGraduations,
					(s, d) =>
						{
							d.Course = s.Course;
							d.Department = s.Department;
							d.EducationInstitutionName = s.EducationInstitutionName;
							d.GraduationYear = s.GraduationYear;
							d.Speciality = s.Speciality;
						});



				UnitOfWork.MergeCollection(counselorCources, persisted.CounselorCources,
					(s, d) =>
						{
							d.Name = s.Name;
							d.Year = s.Year;
						});



				UnitOfWork.MergeCollection(practices, persisted.CounselorPractices,
					(s, d) =>
					{
						d.Camp = s.Camp;
						d.Party = s.Party;
						d.Tour = s.Tour;
						d.Year = s.Year;
					});

				UnitOfWork.MergeCollection(skills, persisted.Skill,
					(s, d) =>
						{
							d.OtherText = s.OtherText;
							d.SkillId = s.SkillId;
							d.SkillVocabularyId = s.SkillVocabularyId;
						});

				if (comments != null)
				{
					persisted.Comments = persisted.Comments ?? new List<CouncelorComment>();
					foreach (var comment in comments)
					{
						UnitOfWork.Context.Entry(comment).State = EntityState.Added;
						comment.DateCreate = DateTime.Now;
						comment.CounselorsId = persisted.Id;
						persisted.Comments.Add(comment);
					}
				}




				persisted.HistoryLink = persisted.HistoryLink ?? UnitOfWork.AddEntity(new HistoryLink());
				persisted.HistoryLink.Historys = persisted.HistoryLink.Historys ?? new List<History>();
				persisted.HistoryLink.Historys.Add(UnitOfWork.AddEntity(new History
																			{
																				AccountId = Security.GetCurrentAccountId(),
																				EventCode = "Изменение вожатого",
																				DateChange = DateTime.Now,
																				Commentary = string.Empty
																			}));
				UnitOfWork.SaveChanges();
				transaction.Complete();
				return persisted;
			}
		}

		internal bool ChangeState(long id, string stateMachineActionString, bool checkOnErrors = true)
		{
			SetUnitOfWorkInRefClass(UnitOfWork);

			var counselor = UnitOfWork.GetById<Counselors>(id);
			if (counselor == null)
			{
				throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not found"));
			}

			using (var transaction = UnitOfWork.GetTransactionScope())
			{
				if (stateMachineActionString == "Delete")
				{
					counselor.HistoryLink = this.WriteHistory(counselor.HistoryLink, "Удаление", "Удаление вожатого",
						StateMachineStateEnum.Deleted, counselor.StateId);
					counselor.HistoryLinkId = counselor.HistoryLink?.Id;
					counselor.StateId = StateMachineStateEnum.Deleted;
				}
				else
				{
					if (checkOnErrors && GetErrorsOfChageStatus(id, stateMachineActionString).Any())
					{
						return false;
					}

					var action = ApiStateController.GetAction(stateMachineActionString);
					if (action?.ToStateId != null)
					{
						counselor.HistoryLink = this.WriteHistory(counselor.HistoryLink, "Изменение статуса вожатого",
							$"Изменение статуса вожатого с \"{counselor?.State.Name}\" на \"{action.NullSafe(a => a.ToState.Name)}\"",
							action.ToStateId, counselor.StateId);
						counselor.HistoryLinkId = counselor.HistoryLink?.Id;

						counselor.StateId = action.ToStateId;

						if (counselor.StateId == StateMachineStateEnum.Counselor.Approved && !string.IsNullOrWhiteSpace(counselor.Email) && !counselor.LinkedAccountId.HasValue)
						{
							string password = string.Empty;
							if (string.IsNullOrWhiteSpace(counselor.Password))
							{
								password = PasswordUtility.GeneratePassword(8);
								var salt = PasswordUtility.GenerateSalt();
								counselor.Password = Convert.ToBase64String(PasswordUtility.GetPasswordHash(password, salt));
								counselor.Salt = Convert.ToBase64String(salt);
							}

							if (!counselor.LinkedAccountId.HasValue)
							{
								var role =
									UnitOfWork.GetSet<Role>().FirstOrDefault(r => r.Name.Contains(AccessRightEnum.Bout.Counselor)) ??
									UnitOfWork.AddEntity(new Role
									{
										Name = $"Вожатый ({AccessRightEnum.Bout.Counselor})",
										CreateUserId = Security.GetCurrentAccountId(),
										AccessRights =
											UnitOfWork.GetSet<AccessRight>()
												.Where(
													a =>
														new[]
														{
													AccessRightEnum.Bout.Counselor
														}.Contains(a.Code))
												.ToList()
									});

								var account = UnitOfWork.GetSet<Account>().FirstOrDefault(a=>a.Login == counselor.Email) ?? UnitOfWork.AddEntity(new Account
								{
									CreateUserId = Security.GetCurrentAccountId(),
									DateCreate = DateTime.Now,
									DateUpdate = DateTime.Now,
									Email = counselor.Email,
									Login = counselor.Email,
									IsActive = true,
									Name = counselor.GetFio(),
									Position = "Вожатый",
									Phone = counselor.Phone,
									Password = counselor.Password,
									Salt = counselor.Salt,
									LastUpdateTick = DateTime.Now.Ticks
								});

								if (counselor.Password != account.Password)
								{
									password = null;
								}

								counselor.Password = account.Password;
								counselor.Salt = account.Salt;

								if (account.Roles.All(r => r.RoleId != role.Id))
								{
									UnitOfWork.AddEntity(new AccountRoles
									{
										Account = account,
										AccountId = account.Id,
										Role = role,
										RoleId = role.Id
									});
								}

								counselor.LinkedAccountId = account.Id;
							}

							if (!string.IsNullOrWhiteSpace(password))
							{
								UnitOfWork.AddEntity(new SendEmailAndSms
								{
									IsSmsSended = true,
									DateCreate = DateTime.Now,
									EmailTitle = "Создание пользователя сайта Мосгортура (Вожатый)",
									EmailMessage =
										$"Для Вас был создан пользователь<br/> Имя пользователя: {counselor.Email}<br/>Пароль: {password}<br/>",
									Email = counselor.Email,
									LastUpdateTick = DateTime.Now.Ticks
								});
							}
						}
					}
				}

				counselor.DateUpdate = DateTime.Now;
				counselor.LastUpdateTick = DateTime.Now.Ticks;
				UnitOfWork.SaveChanges();

				transaction.Complete();
				return true;
			}
		}

		public List<string> GetErrorsOfChageStatus(long id, string actionCode)
		{
			return new List<string>();
		}

		public List<DocumentType> GetAvailableDocumentTypes()
		{
			return
				UnitOfWork.GetSet<DocumentType>().Where(t => t.ForApplicant).ToList().Select(t => new DocumentType(t)).ToList();
		}

		public List<StateMachineState> GetStates()
		{
			return
				UnitOfWork.GetSet<StateMachineState>()
					.Where(s => s.StateMachineId == (long) StateMachineEnum.CounselorState)
					.ToList()
					.Select(s => new StateMachineState(s))
					.ToList();
		}

		public List<TieColor> GetTieColors()
		{
			return UnitOfWork.GetSet<TieColor>().Where(t=>t.IsActive).OrderBy(t=>t.Id).ToList();
		}
	}
}
