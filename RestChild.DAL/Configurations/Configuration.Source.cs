using System.Data.Entity.Migrations;
using System.Linq;
using RestChild.Domain;

namespace RestChild.DAL.Configurations
{
    internal static partial class Configuration
    {
        /// <summary>
        ///     Источники заявления
        /// </summary>
        private static void Source(Context context)
        {
            context.Source.AddOrUpdate(s => s.Id,
                new Source
                {
                    Id = 3,
                    Name = "Сайт",
                    IsActive = true,
                    Commercial = true
                },
                new Source
                {
                    Id = 4,
                    Name = "Оператор МГТ",
                    IsActive = true,
                    Commercial = true
                },
                new Source
                {
                    Id = 5,
                    Name = "Партнер",
                    IsActive = true,
                    Commercial = true
                });

            SetEidAndLastUpdateTicks(context.Source.ToList());
            context.SaveChanges();
        }
    }
}
