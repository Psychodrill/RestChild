using RestChild.Domain;

namespace RestChild.Web.Models.CounselorTestModels
{
	public class VariantAnswerModel : ViewModelBase<CounselorTestAnswerVariant>
	{
		public VariantAnswerModel() : base(new CounselorTestAnswerVariant())
		{
		}

		public VariantAnswerModel(CounselorTestAnswerVariant data) : base(data)
		{
		}
	}
}
