using System.Collections.Generic;
using RestChild.Comon.Enumeration;
using RestChild.Domain;

namespace RestChild.Web.Models.Limits
{
	/// <summary>
	///     модель для работы с заявками на квоты.
	/// </summary>
	public class LimitRequestModel
	{
		/// <summary>
		///     год компании.
		/// </summary>
		public long YearOfRestId { get; set; }

		/// <summary>
		///     Организация.
		/// </summary>
		public long? OrgId { get; set; }

		/// <summary>
		///     Организация наименование.
		/// </summary>
		public string OrgName { get; set; }

		/// <summary>
		///     Ведомство.
		/// </summary>
		public long? OivId { get; set; }

		/// <summary>
		/// строка для поиска
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		///     список возможных годов.
		/// </summary>
		public List<YearOfRest> ListOfYears { get; set; }

		/// <summary>
		///     заявки на организацию
		/// </summary>
		public List<LimitOnOrganizationRequest> Requests { get; set; }

		/// <summary>
		///     заявки на организацию
		/// </summary>
		public Dictionary<long, List<StateMachineAction>> Actions { get; set; }

		/// <summary>
		/// можно добавлять заявления.
		/// </summary>
		public bool CanAddRequest { get; set; }

		/// <summary>
		/// Доступные ОИВ
		/// </summary>
		public List<LimitOnVedomstvo> Oivs { get; set; }


		/// <summary>
		/// Время отдыха
		/// </summary>
		public List<GroupedTimeOfRest> TimeOfRests { get; set; }

		/// <summary>
		/// Категория
		/// </summary>
		public List<ListOfChildsCategory> Categorys { get; set; }


		/// <summary>
		/// можно выбирать организацию
		/// </summary>
		public bool CanSelectOrganization { get; set; }

		/// <summary>
		/// организация
		/// </summary>
		public Organization Organization { get; set; }

		public string GetClassByCode(string code)
		{
			var res = "glyphicon";
			switch (code)
			{
				case AccessRightEnum.Limit.Request.Edit:
					res = res + " glyphicon-pencil";
					break;
				case AccessRightEnum.Limit.Request.ToApprove:
				case AccessRightEnum.Limit.Request.Approve:
					res = res + " glyphicon-ok";
					break;
				case AccessRightEnum.Limit.Request.Decline:
					res = res + " glyphicon-remove-circle";
					break;
			}

			return res;
		}
	}
}
