using System.Collections.Generic;
using RestChild.Domain;

namespace RestChild.Web.Models.Limits
{
	public class OrganizationListModel : ViewModelBase<LimitOnVedomstvo>
	{
		public OrganizationListModel() : base(new LimitOnVedomstvo())
		{
		}

		public OrganizationListModel(LimitOnVedomstvo data) : base(data)
		{
		}

		/// <summary>
		/// тип квоты
		/// </summary>
		public long? TypeLimitId { get; set; }

		/// <summary>
		/// тип квоты
		/// </summary>
		public TypeOfLimitList TypeLimit { get; set; }

		/// <summary>
		/// статус
		/// </summary>
		public ViewModelState State { get; set; }

		/// <summary>
		/// Код действия.
		/// </summary>
		public string StringStateCode { get; set; }

		/// <summary>
		/// комментарий.
		/// </summary>
		public string StringCommentaryCode { get; set; }
		

		/// <summary>
		/// список возможных годов.
		/// </summary>
		public List<YearOfRest> ListOfYears { get; set; }

		/// <summary>
		/// список ведомств
		/// </summary>
		public List<LimitOnVedomstvo> Vedomstvos { get; set; }

		/// <summary>
		/// список ошибок
		/// </summary>
		public List<string> Errors { get; set; }
	}
}