using System;
using System.Collections.Generic;
using RestChild.Comon;
using RestChild.Domain;
using RestChild.Web.Logic;

namespace RestChild.Web.Extensions
{
   using Security = RestChild.Web.Controllers.Security;

	/// <summary>
	/// запись истории.
	/// </summary>
	public static class HistoryExtensions
	{
		/// <summary>
		/// запись в историю
		/// </summary>
		public static HistoryLink WriteHistory(this ILogic self, HistoryLink persisted, string message, string descr)
		{
			return self.WriteHistory(persisted, message, descr, null, null, null);
		}

		/// <summary>
		/// запись в историю
		/// </summary>
		public static HistoryLink WriteHistory(this ILogic self, HistoryLink persisted, string message, string descr, long? toStateId, long? fromStateId)
		{
			return self.WriteHistory(persisted, message, descr, toStateId, fromStateId, null);
		}

		/// <summary>
		/// запись в историю
		/// </summary>
		public static HistoryLink WriteHistory(this ILogic self, HistoryLink persisted, string message, string descr, long? toStateId, long? fromStateId, long? signInfoId)
		{
			persisted = persisted ?? self.UnitOfWork.AddEntity(new HistoryLink(), false);
			persisted.Historys = persisted.Historys ?? new List<History>();

			persisted.Historys.Add(self.UnitOfWork.AddEntity(new History
			{
				AccountId = Security.GetCurrentAccountId(),
				EventCode = message,
				DateChange = DateTime.Now,
				Commentary = descr,
				ToStateId = toStateId,
				FromStateId = fromStateId,
				SignInfoId = signInfoId,
				LastUpdateTick = DateTime.Now.Ticks,
				Link = persisted,
				LinkId = persisted.Id > 0 ? persisted.Id : (long?) null
			}, false));

			self.UnitOfWork.SaveChanges();

			return persisted;
		}

        /// <summary>
        /// запись в историю
        /// </summary>
        public static HistoryLink WriteHistory(IUnitOfWork unitOfWork, HistoryLink persisted, string message, string descr, long? toStateId = null, long? fromStateId = null, long? signInfoId = null)
        {
            persisted = persisted ?? unitOfWork.AddEntity(new HistoryLink(), false);
            persisted.Historys = persisted.Historys ?? new List<History>();

            persisted.Historys.Add(unitOfWork.AddEntity(new History
            {
                AccountId = Security.GetCurrentAccountId(),
                EventCode = message,
                DateChange = DateTime.Now,
                Commentary = descr,
                ToStateId = toStateId,
                FromStateId = fromStateId,
                SignInfoId = signInfoId,
                LastUpdateTick = DateTime.Now.Ticks,
                Link = persisted,
                LinkId = persisted.Id > 0 ? persisted.Id : (long?)null
            }, false));

            unitOfWork.SaveChanges();

            return persisted;
        }
    }
}
