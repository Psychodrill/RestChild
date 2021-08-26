using System;
using System.Data.Entity.Migrations;
using System.Linq;
using RestChild.Comon.Enumeration;
using RestChild.Domain;

namespace RestChild.DAL.Configurations
{
    internal static partial class Configuration
    {
        /// <summary>
        ///     Формы отдыха и оздоровления
        /// </summary>
        private static void FormOfRest(Context context)
        {
            context.FormOfRest.AddOrUpdate(r => r.Id, new FormOfRest
                {
                    Id = 1,
                    Name = "Отдых детей в возрасте от 2 до 7 лет включительно",
                    TypeOfRestId = (long) TypeOfRestEnum.SpecializedСampFamily,
                    IsDeleted = true
                },
                new FormOfRest
                {
                    Id = 2,
                    Name = "Отдых детей в возрасте от 7 до 17 лет включительно",
                    TypeOfRestId = (long) TypeOfRestEnum.SpecializedСamp,
                    IsDeleted = true
                },
                new FormOfRest
                {
                    Id = 3,
                    Name = "Отдых от 18 до 23 лет включительно",
                    TypeOfRestId = (long) TypeOfRestEnum.SpecializedСampFamily,
                    IsDeleted = true
                },
                new FormOfRest
                {
                    Id = 4,
                    Name = "Отдых воспитанников в возрасте 2-7 лет в сопровождении",
                    IsDeleted = false,
                    TypeOfRestId = (long) TypeOfRestEnum.SpecializedСampFamily
                },
                new FormOfRest
                {
                    Id = 5,
                    Name = "Отдых воспитанников в возрасте 7-17 лет в сопровождении",
                    IsDeleted = false,
                    TypeOfRestId = (long) TypeOfRestEnum.SpecializedСamp
                },
                new FormOfRest
                {
                    Id = 6,
                    Name = "Отдых воспитанников в возрасте 18-23 лет в сопровождении",
                    IsDeleted = false,
                    TypeOfRestId = (long) TypeOfRestEnum.SpecializedСampFamily,
                },
                new FormOfRest
                {
                    Id = 7,
                    Name = "Отдых воспитанников в возрасте 7-17 лет без сопровождения",
                    IsDeleted = false,
                    TypeOfRestId = (long) TypeOfRestEnum.SpecializedСamp
                },
                new FormOfRest
                {
                    Id = 8,
                    Name = "Отдых воспитанников в возрасте 18-23 лет без сопровождения",
                    IsDeleted = false,
                    TypeOfRestId = (long) TypeOfRestEnum.SpecializedСampFamily
                });

            SetEidAndLastUpdateTicks(context.FormOfRest.ToList());
            context.SaveChanges();
        }
    }
}
