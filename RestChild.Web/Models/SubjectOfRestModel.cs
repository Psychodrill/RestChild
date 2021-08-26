using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using RestChild.Domain;

namespace RestChild.Web.Models
{
	public class SubjectOfRestModel : ViewModelBase<SubjectOfRest>
	{
		public SubjectOfRestModel()
			: base(new SubjectOfRest{LinkToFile = new LinkToFile {Files = new List<FileOrLink>()}})
		{
			Photos = new List<FileOrLink>();
			OtherFiles = new List<FileOrLink>();
			Links = new List<FileOrLink>();
			Classifications = new List<SubjectOfRestClassification>();
        }

		public SubjectOfRestModel(SubjectOfRest data)
			: base(data)
		{
			DescriptionHtml = data.DescriptionHtml;
			data.LinkToFile = data.LinkToFile ?? new LinkToFile();

            if (data.LinkToFile != null && data.LinkToFile.Files != null)
			{
				Photos = data.LinkToFile.Files.Where(f => f.IsPhoto).ToList();
				OtherFiles = data.LinkToFile.Files.Where(f => !f.IsPhoto && !f.IsVideo).ToList();
				Links = data.LinkToFile.Files.Where(f => f.IsVideo).ToList();
			}
			else
			{
				Photos = new List<FileOrLink>();
				OtherFiles = new List<FileOrLink>();
				Links = new List<FileOrLink>();
			}
		}

		[AllowHtml]
		public string DescriptionHtml { get; set; }

		public List<FileOrLink> Photos { get; set; }

		public List<FileOrLink> OtherFiles { get; set; }

		public List<FileOrLink> Links { get; set; }

		public List<SubjectOfRestClassification> Classifications { get; set; }

		public override SubjectOfRest BuildData()
		{
			var res = base.BuildData();
			res.DescriptionHtml = DescriptionHtml;
			res.LinkToFile = res.LinkToFile ?? new LinkToFile();

			res.LinkToFile.Files = res.LinkToFile.Files ?? new List<FileOrLink>();

			foreach (var file in Photos.Where(p=>p!=null).ToList())
			{
				file.IsPhoto = true;
				file.IsVideo = false;
				res.LinkToFile.Files.Add(file);
			}

			foreach (var file in Links.Where(p => p != null).ToList())
			{
				file.IsPhoto = false;
				file.IsVideo = true;
				res.LinkToFile.Files.Add(file);
			}

			foreach (var file in OtherFiles.Where(p => p != null).ToList())
			{
				file.IsPhoto = false;
				file.IsVideo = false;
				res.LinkToFile.Files.Add(file);
			}

			return res;
		}
	}
}
