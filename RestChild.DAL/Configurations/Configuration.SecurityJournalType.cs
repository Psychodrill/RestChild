using System.Data.Entity.Migrations;
using System.Linq;
using RestChild.Comon.Enumeration;
using RestChild.Domain;

namespace RestChild.DAL.Configurations
{
    internal static partial class Configuration
    {
        /// <summary>
        ///     Типы журналов безопасности
        /// </summary>
        private static void SecurityJournalType(Context context)
        {
            context.SecurityJournalType.AddOrUpdate(s => s.Id, new SecurityJournalType
                {
                    Id = 1,
                    Name = "Security Intsenders"
                }, new SecurityJournalType
                {
                    Id = 2,
                    Name = "Processes"
                }, new SecurityJournalType
                {
                    Id = 3,
                    Name = "Rights And Roles"
                }
                , new SecurityJournalType
                {
                    Id = 4,
                    Name = "User Data Change"
                }
                , new SecurityJournalType
                {
                    Id = 5,
                    Name = "OutSystemsInteractions"
                }
            );

            SetEidAndLastUpdateTicks(context.SecurityJournalType.ToList());
            context.SaveChanges();

        }
    }
}
