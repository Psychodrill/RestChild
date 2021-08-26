using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using RestChild.Domain;

namespace RestChild.Web.Models
{
	/// <summary>
	///     модель для редактирования задачь
	/// </summary>
	public class CounselorTaskEditModel : ViewModelBase<CounselorTask>
	{
		public CounselorTaskEditModel() : base(new CounselorTask())
		{
			TaskFiles = new List<CounselorTaskFile>();
			ReportFiles = new List<CounselorTaskReportFile>();
			ChildTasks = new List<CounselorTask>();
		}

		public CounselorTaskEditModel(CounselorTask data, List<CounselorTask> childTasks)
			: base(data)
		{
			ReportFiles = data.ReportFiles != null ? data.ReportFiles.ToList() : new List<CounselorTaskReportFile>();
			TaskFiles = data.Files != null ? data.Files.ToList() : new List<CounselorTaskFile>();
			Report = data.Report;
			Body = data.Body;
			ChildTasks = childTasks;
		}

		public override CounselorTask BuildData()
		{
			Data.ReportFiles = ReportFiles?.Where(f => f != null).ToList() ?? new List<CounselorTaskReportFile>();
			Data.Report = Report;
			Data.Files = TaskFiles?.Where(f=>f!=null).ToList() ?? new List<CounselorTaskFile>();
			Data.Body = Body;
			return base.BuildData();
		}

		/// <summary>
		/// Статус
		/// </summary>
		public ViewModelState State { get; set; }

		/// <summary>
		/// Идентификатор перехода
		/// </summary>
		public string StateMachineActionString { get; set; }

		/// <summary>
		/// доступность редактирование
		/// </summary>
		public bool IsEditable { get; set; }

		/// <summary>
		/// доступность редактирование
		/// </summary>
		public bool IsEditableTask { get; set; }


		[AllowHtml]
		public string Report { get; set; }

		[AllowHtml]
		public string Body { get; set; }

		/// <summary>
		/// файлы
		/// </summary>
		public List<CounselorTaskFile> TaskFiles { get; set; }

		/// <summary>
		/// файлы
		/// </summary>
		public List<CounselorTaskReportFile> ReportFiles { get; set; }

		/// <summary>
		/// дочернии таски
		/// </summary>
		public List<CounselorTask> ChildTasks { get; set; }
	}
}
