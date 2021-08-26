using System;
using RestChild.Domain;

namespace RestChild.Web.Models.CounselorTestModels
{
	/// <summary>
	/// создание предметов.
	/// </summary>
	public class CounselorTestSubjectModel : ViewModelBase<CounselorTestSubject>
	{
		public CounselorTestSubjectModel() : base(new CounselorTestSubject())
		{
			UniqalId = Guid.NewGuid();
		}

		public CounselorTestSubjectModel(CounselorTestSubject data) : base(data)
		{
			UniqalId = Guid.NewGuid();
		}

		public Guid? UniqalId { get; set; }
	}
}
