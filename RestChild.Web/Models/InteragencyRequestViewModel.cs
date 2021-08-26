using System.Collections.Generic;
using System.Linq;

using RestChild.Comon;
using RestChild.Comon.Enumeration;
using RestChild.Domain;
using RestChild.Web.Controllers;

namespace RestChild.Web.Models
{
   using Security = RestChild.Web.Controllers.Security;

	public class InteragencyRequestViewModel : ViewModelBase<InteragencyRequest>
	{
		public InteragencyRequestViewModel(InteragencyRequest data, IEnumerable<Child> childs, IEnumerable<CheckBoxViewModel<BenefitType>> benefitTypes, IEnumerable<InteragencyRequestRegionModel> regions)
			: this(data, benefitTypes, regions)
		{
			Results =
				childs.Select(
					r =>
						new InteragencyRequestResultViewModel(new InteragencyRequestResult
						{
							Child = r,
							ChildId = r.Id,
							InteragencyRequest = data,
							InteragencyRequestId = data.Id
						}) {Child = r}).ToList();

			IsEditable = Data.StatusInteragencyRequestId != (long) StatusInteragencyRequestEnum.Answered && Security.HasRight(AccessRightEnum.InteragencyRequestManage);
		}

		/// <summary>
		/// доступна редактирование.
		/// </summary>
		public bool IsEditable { get; set; }

		/// <summary>
		/// статусы запроса
		/// </summary>
		public IList<StatusInteragencyRequest> Status { get; set; }

		/// <summary>
		/// статусы результата
		/// </summary>
		public IList<StatusResult> StatusResults { get; set; }

		/// <summary>
		/// активность которая происходит.
		/// </summary>
		public string ProcessedAction { get; set; }

		/// <summary>
		/// Типы льгот
		/// </summary>
		public IList<CheckBoxViewModel<BenefitType>> BenefitTypes { get; set; }

		/// <summary>
		/// Районы
		/// </summary>
		public IList<InteragencyRequestRegionModel> Regions { get; set; }

		public InteragencyRequestViewModel(InteragencyRequest data, IEnumerable<InteragencyRequestResultViewModel> results, IEnumerable<CheckBoxViewModel<BenefitType>> benefitTypes, IEnumerable<InteragencyRequestRegionModel> regions)
			: base(data)
		{
			Results = results.ToList();
			BenefitTypes = benefitTypes.ToList();
			Regions = regions.OrderBy(r => r.NullSafe(req => req.Data.Name)).ToList();
			IsEditable = Data.StatusInteragencyRequestId != (long)StatusInteragencyRequestEnum.Answered && Security.HasRight(AccessRightEnum.InteragencyRequestManage);
		}

		public InteragencyRequestViewModel(InteragencyRequest data, IEnumerable<CheckBoxViewModel<BenefitType>> benefitTypes, IEnumerable<InteragencyRequestRegionModel> regions)
			: this(data, new List<InteragencyRequestResultViewModel>(), benefitTypes, regions)
		{
			IsEditable = Data.StatusInteragencyRequestId != (long)StatusInteragencyRequestEnum.Answered && Security.HasRight(AccessRightEnum.InteragencyRequestManage);

		}

		public InteragencyRequestViewModel()
			: this(new InteragencyRequest(), new List<InteragencyRequestResultViewModel>(), new List<CheckBoxViewModel<BenefitType>>(), new List<InteragencyRequestRegionModel>())
		{
			IsEditable = Data.StatusInteragencyRequestId != (long)StatusInteragencyRequestEnum.Answered && Security.HasRight(AccessRightEnum.InteragencyRequestManage);
		}

		public IList<InteragencyRequestResultViewModel> Results { get; set; }

		public string RequestNumberEm { get; set; }
		public string RequestDateEm { get; set; }

		public string AnswerNumberEm { get; set; }
		public string AnswerDateEm { get; set; }

		public override bool CheckModel(string action = null)
		{
			IsValid = true;

			if (Data == null)
			{
				IsValid = false;
				ErrorMessage = "Нет данных для проверки";
				return IsValid.Value;
			}

			if (Data.StatusInteragencyRequestId != (long)StatusInteragencyRequestEnum.Draft || action == "checkAll")
			{
				if (Data.RequestNumber == null)
				{
					IsValid = false;
					RequestNumberEm = RequaredField;
				}

				if (Data.RequsetDate == null)
				{
					IsValid = false;
					RequestDateEm = RequaredField;
				}
			}


			if (Data.StatusInteragencyRequestId == (long)StatusInteragencyRequestEnum.Answered || action == "checkAll")
			{
				if (Data.AnswerNumber == null)
				{
					IsValid = false;
					AnswerNumberEm = RequaredField;
				}

				if (Data.AnswerDate == null)
				{
					IsValid = false;
					AnswerDateEm = RequaredField;
				}

				foreach (var result in Results)
				{
					IsValid &= result.CheckModel();
				}
			}

			return IsValid ?? false;
		}
	}
}
