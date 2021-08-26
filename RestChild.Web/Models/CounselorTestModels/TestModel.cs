using System.Collections.Generic;
using System.Linq;
using RestChild.Domain;

namespace RestChild.Web.Models.CounselorTestModels
{
	/// <summary>
	/// модель для редактирования тестов.
	/// </summary>
	public class TestModel : ViewModelBase<CounselorTest>
	{
		public TestModel() : base(new CounselorTest())
		{
			Questions = new List<QuestionModel>();
			Subjects = new List<CounselorTestSubjectModel>();
		}

		public TestModel(CounselorTest data) : base(data)
		{
			Subjects = data?.CounselorTestSubjects?.Where(c => !c.IsArchive).OrderBy(c => c.Id).Select(s => new CounselorTestSubjectModel(s)).ToList() ??
					   new List<CounselorTestSubjectModel>();

			var dict = Subjects.ToDictionary(s => s.Data.Id, s => s.UniqalId);

			Questions = data?.Questions?.Where(a=>!a.IsDeleted).OrderBy(a=>a.SortOrder).Select(a => new QuestionModel(a)).ToList() ?? new List<QuestionModel>();
			foreach (var question in Questions)
			{
				if (dict.ContainsKey(question.Data.CounselorTestSubjectId ?? 0))
				{
					question.SubjectUid = dict[question.Data.CounselorTestSubjectId ?? 0];
				}
			}
		}

		public List<QuestionModel> Questions { get; set; }

		public List<CounselorTestSubjectModel> Subjects { get; set; }

		/// <summary>
		/// сбор данных
		/// </summary>
		/// <returns></returns>
		public override CounselorTest BuildData()
		{
			if (Questions != null)
			{
				for (var i = 0; i < Questions.Count; i++)
				{
					Questions[i].Data.SortOrder = i;
				}
			}

			var id = -1;
			foreach (var sub in (Subjects ?? new List<CounselorTestSubjectModel>()).Where(m => m.Data.Id == 0).ToList())
			{
				sub.Data.Id = id;
				id--;
			}

			var dict = (Subjects ?? new List<CounselorTestSubjectModel>()).ToDictionary(s => s.UniqalId, s => s);

			foreach (var q in Questions ?? new List<QuestionModel>())
			{
				q.Data.CounselorTestSubject = null;

				if (q.SubjectUid.HasValue && dict.ContainsKey(q.SubjectUid))
				{
					q.Data.CounselorTestSubjectId = dict[q.SubjectUid].Data.Id;
				}
				else
				{
					q.Data.CounselorTestSubjectId = null;
				}
			}

			Data.Questions = Questions?.Select(a => a.BuildData()).ToList() ?? new List<CounselorTestQuestion>();
			Data.CounselorTestSubjects = Subjects?.Select(s => s.BuildData()).ToList() ?? new List<CounselorTestSubject>();
			return base.BuildData();
		}

		/// <summary>
		/// Статус
		/// </summary>
		public ViewModelState State { get; set; }

		/// <summary>
		/// Идентификатор перехода
		/// </summary>
		public string StateMachineActionString { get; set; }

		/// <summary>
		/// доступность редактирование
		/// </summary>
		public bool IsEditable { get; set; }
	}
}
