using RestChild.Domain;

namespace RestChild.Web.Models
{
	public class InteragencyRequestResultViewModel : ViewModelBase<InteragencyRequestResult>
	{
		public InteragencyRequestResultViewModel(InteragencyRequestResult data)
			: base(data)
		{
		}

		public InteragencyRequestResultViewModel()
			: base(new InteragencyRequestResult())
		{
		}

		public string StatusResultEm { get; set; }

		public Child Child { get; set; }

		public override bool CheckModel(string action = null)
		{
			IsValid = true;
			if (!Data.StatusResultId.HasValue || Data.StatusResultId == 0)
			{
				IsValid = false;
				StatusResultEm = RequaredField;
			}
			return IsValid.Value;
		}
	}
}