using System.Data.Entity.Migrations;
using System.Linq;
using RestChild.Comon.Enumeration;
using RestChild.Domain;

namespace RestChild.DAL.Configurations
{
    internal static partial class Configuration
    {
        /// <summary>
        ///     Виды группы проверки
        /// </summary>
        /// <param name="context"></param>
        private static void TypeOfGroupCheck(Context context)
        {
            context.TypeOfGroupCheck.AddOrUpdate(
                t => t.Id,
                new TypeOfGroupCheck
                {
                    Id = (long) TypeOfGroupCheckEnum.NotCheck,
                    Period = 0
                },
                new TypeOfGroupCheck
                {
                    Id = (long) TypeOfGroupCheckEnum.ForOneYear,
                    Period = 1
                },
                new TypeOfGroupCheck
                {
                    Id = (long) TypeOfGroupCheckEnum.ForTwoYears,
                    Period = 2
                },
                new TypeOfGroupCheck
                {
                    Id = (long) TypeOfGroupCheckEnum.ForSpecializedCamps,
                    Period = 1
                });

            SetEidAndLastUpdateTicks(context.TypeOfGroupCheck.ToList());
            context.SaveChanges();
        }
    }
}
