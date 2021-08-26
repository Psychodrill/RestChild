using RestChild.Domain;

namespace RestChild.Web.Models.CounselorTestModels
{
	public class GroupTestModel : ViewModelBase<TrainingCounselorsGroupTest>
	{
		public GroupTestModel() : base(new TrainingCounselorsGroupTest())
		{
		}

		public GroupTestModel(TrainingCounselorsGroupTest data) : base(data)
		{
		}
	}
}
