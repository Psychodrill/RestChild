using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestChild.Comon;
using RestChild.Domain;

namespace RestChild.Web.Models
{
	public class CounselorsModel : ViewModelBase<Counselors>
	{
		public CounselorsModel()
			: base(new Counselors())
		{
		}

		public CounselorsModel(Counselors counselor)
			: base(counselor)
		{
		}

		public ViewModelState State { get; set; }
		public string StateMachineActionString { get; set; }
		public string ActiveTab { get; set; }
		public bool IsEditable { get; set; }
		public bool? IsMale { get; set; }
		public ICollection<DocumentType> DocumentTypes { get; set; }
		public ICollection<StateDistrict> StateDistricts { get; set; }
		public ICollection<MatrialStatus> MatrialStatuses { get; set; }
		public ICollection<MilitaryDuty> MilitaryDuties { get; set; }
		public ICollection<TypeOfEducation> TypeOfEducations { get; set; }
		public ICollection<TieColor> TieColors { get; set; }
		public List<IGrouping<SkillsGroup, CounselorSkillModel>> SkillGroups { get; set; }
		public List<CounselorSkillModel> Skills { get; set; }

		public List<ClothingSize> Sizes { get; set; }


		public void CalcRaiting()
		{
			if (Data.Rating.HasValue)
			{
				return;
			}

			if (Data.GoldenSail)
			{
				Data.Rating = (Data.Rating ?? 0) + 40;
			}

			if (Data.TieColor != null)
			{
				Data.Rating = (Data.Rating ?? 0) + Data.TieColor.Raiting;
			}

			if (Data.Comments != null && Data.Comments.Any())
			{
				Data.Rating = (Data.Rating ?? 0) - Data.Comments.Count(c => c.Rank <= 2) + Data.Comments.Count(c => c.Rank >=4);
			}
		}

		public string RaitingCalc
		{
			get
			{
				if (Data == null)
					return string.Empty;

				var res = new StringBuilder();
				if (Data.GoldenSail)
				{
					res.AppendLine("<li>Солнечный парус <i>(+40 баллов)</i></li>");
				}

				if (Data.TieColor != null)
				{
					res.AppendLine($"<li>{Data.TieColor.Name} <i>(+{Data.TieColor.Raiting.FormatEx()} баллов)</i></li>");
				}

				if (Data.Comments != null && Data.Comments.Any())
				{
					res.AppendLine($"<li>Рейтинг на сайте <i>({-Data.Comments.Count(c => c.Rank <= 2) + Data.Comments.Count(c => c.Rank >= 4)} баллов)</i></li>");
				}

				return $"<ul>{res}</ul>";
			}
			set { }
		}

		public string PhotoUrl { get; set; }

		public string PhotoShowUrl
		{
			get
			{
				if (!string.IsNullOrEmpty(PhotoUrl))
				{
					return "/DownloadImage.ashx/" + PhotoUrl;
				}
				return string.Empty;
			}
		}
	}
}
