using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RestChild.Domain;

namespace RestChild.Web.Models
{
	public class BoutJournalModel : ViewModelBase<BoutJournal>
	{
		public BoutJournalModel() : base(new BoutJournal())
		{
		}

		public BoutJournalModel(BoutJournal data) : base(data)
		{
			Description = data.Description;
			FilesJournal = (data.Files?.ToList()) ?? new List<BoutJournalFile>();
			Author = data.Counselors != null
				? $"{data.Counselors.LastName} {data.Counselors.FirstName} {data.Counselors.MiddleName}"
				: (data.AdministratorTour != null
					? $"{data.AdministratorTour.LastName} {data.AdministratorTour.FirstName} {data.AdministratorTour.MiddleName}"
					: "-");
		}

		public List<Party> Partys { get; set; }

		public override BoutJournal BuildData()
		{
			Data.Description = Description;
			Data.Files = FilesJournal ?? new List<BoutJournalFile>();
			return base.BuildData();
		}

		public List<BoutJournalFile> FilesJournal { get; set; }

		[AllowHtml]
		public string Description { get; set; }

		public string Author { get; set; }
		public string Title { get; set; }
		public string DateTitle { get; set; }
		public string EventTitle { get; set; }
		public string DescriptionTitle { get; set; }

		public ICollection<CategoryIncident> Incidents { get; set; }

		public bool ShowForSiteOption { get; set; }
		public bool ShowCategoryIncident { get; set; }
	}
}
