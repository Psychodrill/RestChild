using System;
using System.Collections.Generic;
using RestChild.Comon;
using RestChild.Domain;

namespace RestChild.Booking.Logic.Extensions
{
	public static class HistoryExtension
	{
		/// <summary>
		/// запись в историю
		/// </summary>
		public static HistoryLink WriteHistory(this IUnitOfWork self, HistoryLink persisted, string message, string descr, long? accountId = null)
		{
			return self.WriteHistory(persisted, message, descr, null, null, null, accountId);
		}

		/// <summary>
		/// запись в историю
		/// </summary>
		public static HistoryLink WriteHistory(this IUnitOfWork self, HistoryLink persisted, string message, string descr, long? toStateId, long? fromStateId, long? accountId = null)
		{
			return self.WriteHistory(persisted, message, descr, toStateId, fromStateId, null, accountId);
		}

		/// <summary>
		/// запись в историю
		/// </summary>
		public static HistoryLink WriteHistory(this IUnitOfWork self, HistoryLink persisted, string message, string descr, long? toStateId, long? fromStateId, long? signInfoId, long? accountId)
		{
			persisted = persisted ?? self.AddEntity(new HistoryLink());
			persisted.Historys = persisted.Historys ?? new List<History>();

			persisted.Historys.Add(self.AddEntity(new History
			{
				AccountId = accountId,
				EventCode = message,
				DateChange = DateTime.Now,
				Commentary = descr,
				ToStateId = toStateId,
				FromStateId = fromStateId,
				SignInfoId = signInfoId,
				LastUpdateTick = DateTime.Now.Ticks
			}));

			self.SaveChanges();

			return persisted;
		}
	}
}
