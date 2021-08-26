using System.Runtime.Serialization;
using RestChild.Domain;

namespace RestChild.Web.Models.CounselorTestModels
{
	/// <summary>
	///     модель для обучаемого
	/// </summary>
	[DataContract]
	public class TrainingCounselorsResultModel : ViewModelBase<TrainingCounselorsResult>
	{
		public TrainingCounselorsResultModel() : base(new TrainingCounselorsResult())
		{
		}

		public TrainingCounselorsResultModel(TrainingCounselorsResult data) : base(data)
		{
			Name = data.Counselors?.GetFio() ?? data.AdministratorTour?.GetFio();
			if (data.Counselors != null)
			{
				Name = Name + " (вожатый)";
			}
			else if (data.AdministratorTour != null)
			{
				Name = Name + " (администратор заезда)";
			}
		}

		/// <summary>
		///     наименование
		/// </summary>
		[DataMember(Name = "name", EmitDefaultValue = false)]
		public string Name { get; set; }
	}
}
