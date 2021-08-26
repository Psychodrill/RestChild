using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using RestChild.Comon.Enumeration;
using RestChild.DAL.RepositoryExtensions;
using RestChild.Domain;
using RestChild.Web.Models.CounselorTestModels;

namespace RestChild.Web.Controllers.WebApi
{
	/// <summary>
	/// тестирование вожатых и группы обчения
	/// </summary>
	[Authorize]
	public class CounselorTestController : BaseController
	{
		/// <summary>
		///
		/// </summary>
		/// <param name="search"></param>
		/// <returns></returns>
		public List<CounselorTest> GetTests(string search)
		{
			if (string.IsNullOrWhiteSpace(search))
			{
				return new List<CounselorTest>();
			}

			search = search.ToLower();

			return
				UnitOfWork.GetSet<CounselorTest>()
					.Where(t => t.StateId == StateMachineStateEnum.CounselorTest.Formed && t.Name.ToLower().Contains(search))
					.OrderBy(t => t.Name)
					.ThenBy(t => t.Id)
					.Take(15)
					.ToList()
					.Select(c => new CounselorTest(c))
					.ToList();
		}

		/// <summary>
		/// прошедшие тесты
		/// </summary>
		/// <param name="trainingCounselorsResultId"></param>
		/// <returns></returns>
		public List<TrainingCounselorsTest> GetCounselorsTest(long trainingCounselorsResultId)
		{
			return
				UnitOfWork.GetSet<TrainingCounselorsTest>()
					.Where(c => c.TrainingCounselorsResultId == trainingCounselorsResultId && c.IsLastAttempt).OrderBy(c=>c.GroupTest.DateStart).ToList();
		}

		/// <summary>
		/// прошедшие тесты
		/// </summary>
		/// <returns></returns>
		public List<TrainingCounselorsTest> GetGroupCounselorsTest(long groupTestId)
		{
			return
				UnitOfWork.GetSet<TrainingCounselorsTest>()
					.Where(c => c.GroupTestId == groupTestId && c.IsLastAttempt).ToList().OrderBy(c => c.TrainingCounselorsResult?.AdministratorTour?.GetFio()??c.TrainingCounselorsResult?.Counselors?.GetFio()).ToList();
		}

		/// <summary>
		/// поиск вожатых и администраторов смен
		/// </summary>
		public List<TrainingCounselorsResultModel> GetStudents(string search, bool? firstStage)
		{
			if (string.IsNullOrWhiteSpace(search))
			{
				return new List<TrainingCounselorsResultModel>();
			}

			var items = search.ToLower().Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);

			var counselors = UnitOfWork.GetSet<Counselors>().Where(c => c.StateId != StateMachineStateEnum.Deleted);
			var administratorTour = UnitOfWork.GetSet<AdministratorTour>().Where(c => c.StateId != StateMachineStateEnum.Deleted);

			if (firstStage == true)
			{
				counselors = counselors.Where(c => c.StateId == StateMachineStateEnum.Counselor.Request);
				administratorTour = administratorTour.Where(a => a.StateId == StateMachineStateEnum.Deleted);
			}

			foreach (var s in items)
			{
				counselors = counselors.Where(c => c.LastName.ToLower().Contains(s) || c.FirstName.ToLower().Contains(s) || c.MiddleName.ToLower().Contains(s));
				administratorTour = administratorTour.Where(c => c.LastName.ToLower().Contains(s) || c.FirstName.ToLower().Contains(s) || c.MiddleName.ToLower().Contains(s));
			}

			var dtos = counselors.OrderBy(c => c.LastName)
				.ThenBy(c => c.FirstName)
				.ThenBy(c => c.MiddleName)
				.OrderBy(c => c.Id).Take(10).ToList()
					.Select(
						c =>
							new TrainingCounselorsResultModel(new TrainingCounselorsResult
							{
								Counselors = new Counselors(c),
								CounselorsId = c.Id
							})).ToList();

			dtos.AddRange(
				administratorTour.OrderBy(c => c.LastName)
					.ThenBy(c => c.FirstName)
					.ThenBy(c => c.MiddleName)
					.OrderBy(c => c.Id)
					.Take(10)
					.ToList()
					.Select(
						c =>
							new TrainingCounselorsResultModel(new TrainingCounselorsResult
							{
								AdministratorTour = new AdministratorTour(c),
								AdministratorTourId = c.Id
							})).ToList());

			return dtos.OrderBy(c => c.Name).ToList();
		}

		internal CounselorTest UpdateTestEntityOnSave(CounselorTest currentEntity, CounselorTest entity)
		{
			currentEntity.CopyEntity(entity);
			foreach (var s in entity.CounselorTestSubjects)
			{
				var id = s.Id;
				s.LastUpdateTick = DateTime.Now.Ticks;
				s.CounselorTestId = entity.Id;
				var exists = currentEntity.CounselorTestSubjects.FirstOrDefault(c => c.Id == s.Id);
				if (exists != null)
				{
					exists.CopyEntity(s);
				}
				else
				{
					UnitOfWork.Context.Entry(s).State = EntityState.Added;
					UnitOfWork.SaveChanges();
					var questionsForUpdate = entity.Questions.Where(q => q.CounselorTestSubjectId == id);
					foreach (var q in questionsForUpdate)
					{
						q.CounselorTestSubjectId = s.Id;
					}
				}

				UnitOfWork.SaveChanges();
			}

			foreach (var question in entity.Questions)
			{
				var currentQuestion = currentEntity.Questions.FirstOrDefault(q => q.Id == question.Id);

				if (question.Id == 0 || currentQuestion == null)
				{
					question.Id = 0;
					question.CounselorTestId = entity.Id;
					question.TypeId = null;
					UnitOfWork.Context.Entry(question).State = EntityState.Added;
					UnitOfWork.SaveChanges();
					currentEntity.Questions.Add(question);
                }
				else
				{
					currentQuestion.CopyEntity(question);
					foreach (var variant in question.Variants)
					{
						var curentVariant = currentQuestion.Variants.FirstOrDefault(v => v.Id == variant.Id);
						if (variant.Id == 0 || curentVariant == null)
						{
							variant.Id = 0;
							variant.QuestionId = question.Id;
							variant.FileOrLinkId = null;
							UnitOfWork.Context.Entry(variant).State = EntityState.Added;
							currentQuestion.Variants.Add(variant);
						}
						else
						{
							curentVariant.CopyEntity(variant);
						}
					}
				}
			}

			UnitOfWork.SaveChanges();
			return currentEntity;
		}

		internal void RemoveQuestionsOnSave(CounselorTest currentEntity,  CounselorTest entity)
		{
			var questions = currentEntity.Questions.ToList();
			foreach (var q in questions)
			{
				var existQuestion = entity.Questions.FirstOrDefault(e => e.Id == q.Id);
				if (existQuestion == null)
				{
					if (q.Answer.Any())
					{
						q.IsDeleted = true;
						UnitOfWork.SaveChanges();
					}
					else
					{
						UnitOfWork.Delete<CounselorTestAnswerVariant>(q.Variants.ToList());
						UnitOfWork.Delete(q);
					}
				}
				else
				{
					var ids = existQuestion.Variants.Where(v => v.Id > 0).Select(v => v.Id).ToList();
					var varForDel = q.Variants.Where(v => !ids.Contains(v.Id)).ToList();
					foreach (var v in varForDel)
					{
						if (v.Answer.Any())
						{
							v.IsDeleted = true;
							UnitOfWork.SaveChanges();
						}
						else
						{
							UnitOfWork.Delete(v);
						}
					}
				}

			}

			UnitOfWork.SaveChanges();
		}

		internal void RemoveSubjectOnSave(CounselorTest currentEntity, CounselorTest entity)
		{
			var subjects = currentEntity.CounselorTestSubjects.ToList();
			foreach (var q in subjects)
			{
				var existsQuestion = entity.Questions.FirstOrDefault(e => e.Id == q.Id);
				if (existsQuestion == null)
				{
					q.IsArchive = true;
					UnitOfWork.SaveChanges();
				}
			}
		}
	}
}
