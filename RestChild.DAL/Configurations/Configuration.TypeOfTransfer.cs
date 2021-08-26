using System.Data.Entity.Migrations;
using System.Linq;
using RestChild.Comon.Enumeration;
using RestChild.Domain;

namespace RestChild.DAL.Configurations
{
    internal static partial class Configuration
    {
        /// <summary>
        ///     Виды транспорта
        /// </summary>
        private static void TypeOfTransfer(Context context)
        {
            context.TypeOfTransfer.AddOrUpdate(r => r.Id,
                new TypeOfTransfer
                {
                    Id = 1,
                    Name = "В составе организованной группы за счет средств бюджета города Москвы"
                },
                new TypeOfTransfer
                {
                    Id = 2,
                    Name = "Самостоятельный проезд за счет собственных средств"
                }
            );

            SetEidAndLastUpdateTicks(context.TypeOfTransfer.ToList());
            context.SaveChanges();
        }
    }
}
