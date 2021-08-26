using System.Data.Entity.Migrations;
using System.Linq;
using RestChild.Comon.Enumeration;
using RestChild.Domain;

namespace RestChild.DAL.Configurations
{
    internal static partial class Configuration
    {
        /// <summary>
        ///     Типы списков квот
        /// </summary>
        private static void TypeOfLimitList(Context context)
        {
            context.TypeOfLimitList.AddOrUpdate(
                t => t.Id,
                new TypeOfLimitList
                {
                    Id = (long) TypeOfLimitListEnum.Orphan,
                    Name = "Сироты"
                },
                new TypeOfLimitList
                {
                    Id = (long) TypeOfLimitListEnum.Profile,
                    Name = "Профильники"
                }
            );

            SetEidAndLastUpdateTicks(context.TypeOfLimitList.ToList());
            context.SaveChanges();
        }
    }
}
