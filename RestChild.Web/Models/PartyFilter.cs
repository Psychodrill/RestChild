using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using RestChild.Domain;

namespace RestChild.Web.Models
{
	public class PartyFilter
	{
		#region Фильтр
		public long? BoutsId { get; set; }
		public long? HotelsId { get; set; }
		public long? GroupedTimeOfRestId { get; set; }
		public bool? OnlyNotAdded { get; set; }
		public bool OnlyBenefits { get; set; }
		public bool OnlyCommerce { get; set; }
		public bool OnlySpecilized { get; set; }
		public string Name { get; set; }
		public bool? IsMale { get; set; }
		public int? AgeFrom { get; set; }
		public int? AgeTo { get; set; }
		public bool IsGrouped { get; set; }
		public long? SubjectOfRestid { get; set; }
		public long? OpenedPartyId { get; set; }
		public bool IsEditable { get; set; }
		#endregion

		#region Сортировки
		public string OrderBy { get; set; }
		#endregion

		#region Возвращаемые данные
		public List<Child> UngroupedChilds { get; set; }
		public List<GroupedChilds> GroupedChilds { get; set; }
		public List<PartyModel> Parties { get; set; }
		public int AllChildrenCount { get; set; }
		public int VacantChildrenCount { get; set; }
		#endregion
		public StateMachineState BoutState { get; set; }
		public Bout Bout { get; set; }

		#region Справочники
		public ICollection<SubjectOfRest> SubjectsOfRest { get; set; }

		/// <summary>
		/// Причины отсутствия необходимости покупки обратного билета
		/// </summary>
		public List<NotNeedTicketReason> NotNeedTicketReasons { get; set; }

		/// <summary>
		/// Транспорт для вожатых
		/// </summary>
		public Dictionary<long, LinkToPeople> CounselorTransportForward { get; set; }

		/// <summary>
		/// Обратный транспорт для вожатых
		/// </summary>
		public Dictionary<long, LinkToPeople> CounselorTransportBackward { get; set; }
		#endregion

		/// <summary>
		/// Является ли пользователь администратором смены в заезде
		/// </summary>
		public bool IsBoutAdministartor { get; set; }


	}
}
