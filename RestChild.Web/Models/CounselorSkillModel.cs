using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using RestChild.Domain;

namespace RestChild.Web.Models
{
	public class CounselorSkillModel
	{
		public long? CounselorSkillId { get; set; }
		public long SkillId { get; set; }
		public Skill Skill { get; set; }
		public bool IsSelected { get; set; }
		public string Text { get; set; }
		public long? SkillVocabularyId { get; set; }
		public bool NeedText { get; set; }
		public bool NeedVocabulary { get; set; }
		public List<SkillVocabulary> SkillVocabularies { get; set; } 
	}
}