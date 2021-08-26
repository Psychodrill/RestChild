using System;
using System.Data.Entity.Migrations;
using System.Linq;
using RestChild.Domain;

namespace RestChild.DAL.Configurations
{
    internal static partial class Configuration
    {
        /// <summary>
        ///     Виды записи в журнале
        /// </summary>
        private static void BoutJournalType(Context context)
        {
            context.BoutJournalType.AddOrUpdate(t => t.Id,
                new BoutJournalType
                {
                    Id = 1,
                    Eid = 1,
                    Name = "Событие",
                    IsActive = true,
                    LastUpdateTick = DateTime.Now.Ticks
                },
                new BoutJournalType
                {
                    Id = 2,
                    Eid = 2,
                    Name = "Происшествие",
                    IsActive = true,
                    LastUpdateTick = DateTime.Now.Ticks
                },
                new BoutJournalType
                {
                    Id = 3,
                    Eid = 3,
                    Name = "Отчет",
                    IsActive = true,
                    LastUpdateTick = DateTime.Now.Ticks
                });
        }
    }
}
