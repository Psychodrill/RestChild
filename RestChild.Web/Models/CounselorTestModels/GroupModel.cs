using System.Collections.Generic;
using System.Linq;
using RestChild.Domain;

namespace RestChild.Web.Models.CounselorTestModels
{
	/// <summary>
	/// модель группы обчения
	/// </summary>
	public class GroupModel : ViewModelBase<TrainingCounselors>
	{
		public GroupModel() : base(new TrainingCounselors())
		{
			Results = new List<TrainingCounselorsResultModel>();
			Tests = new List<GroupTestModel>();
		}

		public GroupModel(TrainingCounselors data) : base(data)
		{
			Results =
				data?.Results?.OrderBy(r => r.Counselors?.GetFio() ?? r.AdministratorTour?.GetFio())
					.Select(a => new TrainingCounselorsResultModel(a))
					.ToList() ?? new List<TrainingCounselorsResultModel>();
			Tests = data?.Tests?.Where(t=>!t.IsDeleted).OrderBy(t=>t.DateStart).Select(a => new GroupTestModel(a)).ToList() ?? new List<GroupTestModel>();

		}

		/// <summary>
		/// список обучаемых с результатами
		/// </summary>
		public List<TrainingCounselorsResultModel> Results { get; set; }

		/// <summary>
		/// список тестов группы
		/// </summary>
		public List<GroupTestModel> Tests { get; set; }

		/// <summary>
		///     Статус
		/// </summary>
		public ViewModelState State { get; set; }

		/// <summary>
		///     Идентификатор перехода
		/// </summary>
		public string StateMachineActionString { get; set; }

		/// <summary>
		///     доступность редактирование
		/// </summary>
		public bool IsEditable { get; set; }

		/// <summary>
		///     доступность редактирование
		/// </summary>
		public bool IsEditableFormed { get; set; }

		/// <summary>
		/// построение данных
		/// </summary>
		public override TrainingCounselors BuildData()
		{
			Data.Results = Results?.Select(a => a.BuildData()).ToList() ?? new List<TrainingCounselorsResult>();
			Data.Tests = Tests?.Select(a => a.BuildData()).ToList() ?? new List<TrainingCounselorsGroupTest>();
			return base.BuildData();
		}
	}
}
