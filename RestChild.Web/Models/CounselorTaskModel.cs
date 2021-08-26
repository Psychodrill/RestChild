using System;
using System.Collections.Generic;
using System.Web.Mvc;
using RestChild.Extensions.Filter;
using RestChild.Domain;

namespace RestChild.Web.Models
{
	/// <summary>
	/// модель для задачи
	/// </summary>
	public class CounselorTaskModel
	{
		public CounselorTaskModel()
		{
			DateTask = DateTime.Now.Date;
		}

		public bool NotNessary { get; set; }

		/// <summary>
		/// дата задачи
		/// </summary>
		public DateTime DateTask { get; set; }

		/// <summary>
		/// палновый срок задачи
		/// </summary>
		public DateTime? Deadline { get; set; }

		/// <summary>
		/// загловок
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// текст задания.
		/// </summary>
		[AllowHtml]
		public string Content { get; set; }

		/// <summary>
		/// родительский таск.
		/// </summary>
		public long? ParentTaskId { get; set; }

		/// <summary>
		/// кому задания раздавать
		/// </summary>
		public List<CounselorTaskPerformerModel> Performers { get; set; }

		/// <summary>
		/// фильтр по соисполнителям
		/// </summary>
		public CoworkersFilterModel CoworkersFilterModel { get; set; }

		/// <summary>
		/// файлы
		/// </summary>
		public virtual List<CounselorTaskFile> TaskFiles { get; set; }
	}
}
