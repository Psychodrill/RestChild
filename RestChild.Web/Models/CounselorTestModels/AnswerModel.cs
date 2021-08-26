using RestChild.Domain;

namespace RestChild.Web.Models.CounselorTestModels
{
	public class AnswerModel : ViewModelBase<CounselorTestAnswer>
	{
		public AnswerModel() : base(new CounselorTestAnswer())
		{
		}

		public AnswerModel(CounselorTestAnswer data) : base(data)
		{
		}
	}
}
