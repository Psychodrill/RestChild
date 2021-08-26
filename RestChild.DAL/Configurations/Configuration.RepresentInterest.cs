using System.Data.Entity.Migrations;
using System.Linq;
using RestChild.Comon.Enumeration;
using RestChild.Domain;

namespace RestChild.DAL.Configurations
{
    internal static partial class Configuration
    {
        /// <summary>
        ///     Типы представительства родственниками
        /// </summary>
        private static void RepresentInterest(Context context)
        {
            context.RepresentInterest.AddOrUpdate(r => r.Id,
                new RepresentInterest
                {
                    Id = 1,
                    Name = "Отца"
                },
                new RepresentInterest
                {
                    Id = 2,
                    Name = "Матери"
                },
                new RepresentInterest
                {
                    Id = 3,
                    Name = "Законного представителя"
                },
                new RepresentInterest
                {
                    Id = 4,
                    Name = "Лицо из числа детей-сирот и детей, оставшихся без попечения родителей "
                }
            );

            SetEidAndLastUpdateTicks(context.RepresentInterest.ToList());
            context.SaveChanges();
        }
    }
}
