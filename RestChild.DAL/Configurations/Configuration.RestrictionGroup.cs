using System.Data.Entity.Migrations;
using System.Linq;
using RestChild.Comon.Enumeration;
using RestChild.Domain;

namespace RestChild.DAL.Configurations
{
    internal static partial class Configuration
    {
        /// <summary>
        ///     Группы ограничений воспитанников детских домов
        /// </summary>
        private static void RestrictionGroup(Context context)
        {
            context.RestrictionGroup.AddOrUpdate(r => r.Id,
                new RestrictionGroup
                {
                    Id = (long) RestrictionGroupEnum.WithAnAccessibleEnvironment,
                    Name = "С доступной средой",
                    Number = 1,
                    IsDeleted = false
                },
                new RestrictionGroup
                {
                    Id = (long) RestrictionGroupEnum.PartiallyAccessible,
                    Name = "С частично-доступной средой",
                    Number = 2,
                    IsDeleted = false
                },
                new RestrictionGroup
                {
                    Id = (long) RestrictionGroupEnum.NoAccessibleEnvironment,
                    Name = "Без доступной среды",
                    Number = 3,
                    IsDeleted = false
                });

            SetEidAndLastUpdateTicks(context.TypeOfGroupCheck.ToList());
            context.SaveChanges();
        }
    }
}
