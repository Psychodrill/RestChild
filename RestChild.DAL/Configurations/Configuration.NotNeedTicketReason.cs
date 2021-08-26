using System.Data.Entity.Migrations;
using System.Linq;
using RestChild.Comon.Enumeration;
using RestChild.Domain;

namespace RestChild.DAL.Configurations
{
    internal static partial class Configuration
    {
        /// <summary>
        ///     Причины по которой не нужен билет
        /// </summary>
        /// <param name="context"></param>
        private static void NotNeedTicketReason(Context context)
        {
            context.NotNeedTicketReason.AddOrUpdate(r => r.Id,
                new NotNeedTicketReason
                {
                    Id = (long) NotNeedTicketReasonEnum.ComeSingly,
                    Name = "Добирается самостоятельно",
                    IsActive = true
                },
                new NotNeedTicketReason
                {
                    Id = (long) NotNeedTicketReasonEnum.Hospitalized,
                    Name = "Госпитализирован",
                    IsActive = true
                },
                new NotNeedTicketReason
                {
                    Id = (long) NotNeedTicketReasonEnum.NotCome,
                    Name = "Не прибыл в место отдыха",
                    IsActive = true
                },
                new NotNeedTicketReason
                {
                    Id = (long) NotNeedTicketReasonEnum.LeftEarly,
                    Name = "Выехал досрочно",
                    IsActive = true
                });

            SetEidAndLastUpdateTicks(context.NotNeedTicketReason.ToList());
            context.SaveChanges();
        }
    }
}
