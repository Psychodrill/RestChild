using System.Data.Entity.Migrations;
using System.Linq;
using RestChild.Comon.Enumeration;
using RestChild.Domain;

namespace RestChild.DAL.Configurations
{
    internal static partial class Configuration
    {
        /// <summary>
        ///     Заполнение наименований административных округов москвы
        /// </summary>
        private static void BtiDistrict(Context context)
        {
            context.BtiDistrict.AddOrUpdate(r => r.Id, new BtiDistrict
            {
                Id = 1,
                Givz = 1,
                Name = "Центральный административный округ",
                IsVisible = true,
                Okato = 45286
            },
            new BtiDistrict
            {
                Id = 2,
                Givz = 3,
                Name = "Северо-Восточный административный округ",
                IsVisible = true,
                Okato = 45280
            },
            new BtiDistrict
            {
                Id = 3,
                Givz = 4,
                Name = "Восточный административный округ",
                IsVisible = true,
                Okato = 45263
            },
            new BtiDistrict
            {
                Id = 4,
                Givz = 5,
                Name = "Юго-Восточный административный округ",
                IsVisible = true,
                Okato = 45290
            },
            new BtiDistrict
            {
                Id = 5,
                Givz = 6,
                Name = "Южный административный округ",
                IsVisible = true,
                Okato = 45296
            },
            new BtiDistrict
            {
                Id = 6,
                Givz = 7,
                Name = "Юго-Западный административный округ",
                IsVisible = true,
                Okato = 45293
            },
            new BtiDistrict
            {
                Id = 7,
                Givz = 8,
                Name = "Западный административный округ",
                IsVisible = true,
                Okato = 45268
            },
            new BtiDistrict
            {
                Id = 8,
                Givz = 9,
                Name = "Северо-Западный административный округ",
                IsVisible = true,
                Okato = 45283
            },
            new BtiDistrict
            {
                Id = 9,
                Givz = 2,
                Name = "Северный административный округ",
                IsVisible = true,
                Okato = 45280
            },
            new BtiDistrict
            {
                Id = 10,
                Givz = 10,
                Name = "Зеленоградский административный округ",
                IsVisible = true,
                Okato = 45272
            },
            new BtiDistrict
            {
                Id = 11,
                Givz = 12,
                Name = "Троицкий административный округ",
                IsVisible = true,
                Okato = 45298
            },
            new BtiDistrict
            {
                Id = 12,
                Givz = 11,
                Name = "Новомосковский административный округ",
                IsVisible = true,
                Okato = 45297
            });

            SetEidAndLastUpdateTicks(context.BtiDistrict.ToList());
            context.SaveChanges();
        }
    }
}
