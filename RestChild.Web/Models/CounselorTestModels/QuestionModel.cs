using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RestChild.Domain;

namespace RestChild.Web.Models.CounselorTestModels
{

	public class QuestionModel : ViewModelBase<CounselorTestQuestion>
	{
		public QuestionModel() : base(new CounselorTestQuestion {Answer = new List<CounselorTestAnswer>()})
		{
		}

		public QuestionModel(CounselorTestQuestion data) : base(data)
		{
			Variants = data?.Variants?.Where(a => !a.IsDeleted).OrderBy(a => a.SortOrder).Select(a => new VariantAnswerModel(a)).ToList() ?? new List<VariantAnswerModel>();
		}

		public List<VariantAnswerModel> Variants { get; set; }

		/// <summary>
		/// ИД тематики
		/// </summary>
		public Guid? SubjectUid { get; set; }

		public override CounselorTestQuestion BuildData()
		{
			if (Variants != null)
			{
				for (var i = 0; i < Variants.Count; i++)
				{
					Variants[i].Data.SortOrder = i;
				}
			}

			Data.Variants = Variants?.Select(a => a.BuildData()).ToList() ?? new List<CounselorTestAnswerVariant>();
			return base.BuildData();
		}
	}
}
