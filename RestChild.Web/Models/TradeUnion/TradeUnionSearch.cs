using System;
using System.Collections.Generic;
using RestChild.Domain;
using RestChild.Extensions.Filter;
using RestChild.Comon.Enumeration;

namespace RestChild.Web.Models.TradeUnion
{
	/// <summary>
	///     поиск списков профсоюзов
	/// </summary>
	public class TradeUnionSearch
	{
		public long? YearOfRestId { get; set; }

		public List<YearOfRest> YearOfRests { get; set; }

		public long? CampId { get; set; }
		public Organization Camp { get; set; }

		public long? TradeUnionId { get; set; }

		/// <summary>
		/// только один лагерь
		/// </summary>
		public bool OnlyOneCamp { get; set; }

		/// <summary>
		/// только один профсоюз
		/// </summary>
		public bool OnlyOneTradeUnion { get; set; }

		public Organization TradeUnion { get; set; }

		public long? StateId { get; set; }

		public List<StateMachineState> States { get; set; }

		public long? TimeOfRestId { get; set; }

		public List<GroupedTimeOfRest> TimeOfRests { get; set; }

		public string Name { get; set; }

		public int PageNumber { get; set; }

		public CommonPagedList<TradeUnionList> Result { get; set; }

		/// <summary>
		/// Просрочен ли "Профсоюзный список" (необходимо ли его строку пометить цветом в гриде на странице поиска "Списки профсоюзов").
		/// </summary>
		public static bool IsExpired(TradeUnionList tradeUnionListItem)
		{
			// #22549 Сделать предупреждения за 5 и 10 дней соотвественно. Выделив просроченные метами.
			// #22880 Изменить сроки

			// Ivan Ushakov: на утверждение первый раз позднее 5 дней, до даты начала заезда
			if (tradeUnionListItem.StateId == StateMachineStateEnum.TradeUnion.OnAproving &&
				tradeUnionListItem.DateFrom.HasValue &&
				tradeUnionListItem.DateFrom.Value.AddDays(5) < DateTime.Today)
				return true;

			// Ivan Ushakov: Завершение вноса сведений позднее 10 дней, до даты окончания заезда
			if (tradeUnionListItem.StateId == StateMachineStateEnum.TradeUnion.Finish &&
				tradeUnionListItem.DateTo.HasValue &&
				tradeUnionListItem.DateTo.Value.AddDays(-5) < DateTime.Today)
				return true;

			return false;
		}
	}
}
