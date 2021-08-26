using System;
using System.Collections.Generic;
using System.Linq;
using RestChild.Domain;

namespace RestChild.Web.Models.CounselorTestModels
{
	/// <summary>
	/// модель для тестирования.
	/// </summary>
	public class TestingModel : ViewModelBase<TrainingCounselorsTest>
	{
		public TestingModel() : base(new TrainingCounselorsTest())
		{
			Answers = new List<AnswerModel>();
		}

		public TestingModel(TrainingCounselorsTest data) : base(data)
		{
			Answers = data?.Answers.Select(a => new AnswerModel(a)).ToList() ?? new List<AnswerModel>();

			if (!Answers.Any())
			{
				Answers =
					data?.GroupTest?.CounselorTest?.Questions.Where(q => !q.IsDeleted).ToList()
						.Select(
							q =>
								new CounselorTestAnswer
								{
									CounselorTest = q.CounselorTest,
									CounselorTestId = q.CounselorTestId,
									QuestionId = q.Id,
									Question = q,
									TrainingCounselorsTestId = data.Id,
									TrainingCounselorsTest = data,
									TrainingCounselor = data.TrainingCounselorsResult,
									TrainingCounselorId = data.TrainingCounselorsResultId,
									LastUpdateTick = DateTime.Now.Ticks
								})
						.OrderBy(q => q.Question?.CounselorTestSubject?.Id)
						.ThenBy(q => q.Question.Id)
						.Select(a => new AnswerModel(a))
						.ToList() ?? new List<AnswerModel>();

				var subjects = data?.GroupTest?.CounselorTest?.CounselorTestSubjects ?? new List<CounselorTestSubject>();

				// генерация тестирования случайным способом
				foreach (var s in subjects)
				{
					var ans = Answers.Where(a => a?.Data?.Question?.CounselorTestSubjectId == s.Id).ToList();
					var rnd = new Random();

					while (ans.Count > s.QuestionCount)
					{
						var i = rnd.Next(0, ans.Count);
						var a = ans[i];
						Answers.Remove(a);
						ans.Remove(a);
					}
				}
			}
		}

		public List<AnswerModel> Answers { get; set; }

		public override TrainingCounselorsTest BuildData()
		{
			Data.Answers = Answers?.Select(a => a.BuildData()).ToList() ?? new List<CounselorTestAnswer>();
			return base.BuildData();
		}

		/// <summary>
		/// можно тестировать.
		/// </summary>
		public bool MayTesting { get; set; }

		/// <summary>
		/// можно смотреть.
		/// </summary>
		public bool MayView { get; set; }

		/// <summary>
		/// количество попыток
		/// </summary>
		public bool MayRetest { get; set; }

		/// <summary>
		/// нужно пройти другие тесты
		/// </summary>
		public bool NeedOtherTest { get; set; }

		/// <summary>
		/// другие тесты.
		/// </summary>
		public List<Tuple<string, string>> Testings { get; set; }

	}
}
