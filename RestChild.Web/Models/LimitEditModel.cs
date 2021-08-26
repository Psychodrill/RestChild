using System;
using System.Collections.Generic;
using RestChild.Domain;

namespace RestChild.Web.Models
{
	/// <summary>
	/// модель для квоты
	/// </summary>
	public class LimitEditModel 
	{
		/// <summary>
		/// Данные квоты
		/// </summary>
		public ListTravelers Data { get; set; }

		/// <summary>
		/// номер страницы
		/// </summary>
		public int? PageNumber { get; set; }

		/// <summary>
		/// заявки
		/// </summary>
		public RestChild.Extensions.Filter.CommonPagedList<ListTravelersRequest> Requests { get; set; }

		/// <summary>
		/// плановое значение
		/// </summary>
		public long PlanCount { get; set; }

		/// <summary>
		/// фактически включено в квоту 
		/// </summary>
		public int FactCount { get; set; }

		/// <summary>
		/// фактически включено в квоту 
		/// </summary>
		public int FactIncludedCount { get; set; }

		/// <summary>
		/// фактически включено в квоту (сопровождающих)
		/// </summary>
		public int FactAttendantCount { get; set; }

		/// <summary>
		/// Действие
		/// </summary>
		public string Action { get; set; }

		/// <summary>
		/// количество для включения
		/// </summary>
		public int? CountToInclude { get; set; }

		#region Filter

		/// <summary>
		/// номер заявления
		/// </summary>
		public string RequestNumber { get; set; }

		/// <summary>
		/// льгота
		/// </summary>
		public long? BenefitTypeId { get; set; }

		/// <summary>
		/// время отдыха
		/// </summary>
		public long? TimeOfRestId { get; set; }

		/// <summary>
		/// напраление отдыха
		/// </summary>
		public long? PlaceOfRestId { get; set; }

		/// <summary>
		/// ФИО отдыхающего
		/// </summary>
		public string Fio { get; set; }

		/// <summary>
		/// Дата заявления с 
		/// </summary>
		public DateTime? DateRequestFrom { get; set; }

		/// <summary>
		/// Дата заявления по
		/// </summary>
		public DateTime? DateRequestTo { get; set; }

		/// <summary>
		/// Ранг с 
		/// </summary>
		public decimal? RankFrom { get; set; }

		/// <summary>
		/// Ранг по
		/// </summary>
		public decimal? RankTo { get; set; }

		/// <summary>
		/// Ранг с 
		/// </summary>
		public bool ExcludeRankFrom { get; set; }

		/// <summary>
		/// Ранг по
		/// </summary>
		public bool ExcludeRankTo { get; set; }

		/// <summary>
		/// Отображать заявления
		/// </summary>
		public long? ViewRequestType { get; set; }

		/// <summary>
		/// Статус проверки
		/// </summary>
		public long? StatusOfCheck { get; set; }

		#endregion


		#region

		/// <summary>
		/// льготы
		/// </summary>
		public IList<BenefitType> BenefitTypes { get; set; }

		/// <summary>
		/// направления отдыха
		/// </summary>
		public IList<PlaceOfRest> PlaceOfRests { get; set; }

		/// <summary>
		/// время отдыха
		/// </summary>
		public IList<TimeOfRest> TimeOfRests { get; set; }

		#endregion

	}
}