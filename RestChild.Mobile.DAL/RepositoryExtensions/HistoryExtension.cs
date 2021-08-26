using System;
using System.Collections.Generic;
using RestChild.Mobile.Domain;

namespace RestChild.Mobile.DAL.RepositoryExtensions
{
    public static class HistoryExtension
    {
        /// <summary>
        ///     запись в историю
        /// </summary>
        public static Link WriteHistory(this IUnitOfWorkMobile self, Link persisted, string message, string description,
            long? accountId = null)
        {
            return self.WriteHistory(persisted, message, description, null, null, null, accountId);
        }

        /// <summary>
        ///     запись в историю
        /// </summary>
        public static Link WriteHistory(this IUnitOfWorkMobile self, Link persisted, string message, string description,
            long? toStateId, long? fromStateId, long? accountId = null)
        {
            return self.WriteHistory(persisted, message, description, toStateId, fromStateId, null, accountId);
        }

        /// <summary>
        ///     запись в историю
        /// </summary>
        public static Link WriteHistory(this IUnitOfWorkMobile self, Link persisted, string message, string description,
            long? toStateId, long? fromStateId, long? signInfoId, long? accountId)
        {
            persisted = persisted ?? self.AddEntity(new Link());
            persisted.Historys = persisted.Historys ?? new List<History>();

            persisted.Historys.Add(self.AddEntity(new History
            {
                AccountExternalId = accountId,
                EventCode = message,
                DateChange = DateTime.Now,
                Commentary = description,
                ToStateId = toStateId,
                FromStateId = fromStateId,
                LastUpdateTick = DateTime.Now.Ticks
            }));

            self.SaveChanges();

            return persisted;
        }
    }
}
