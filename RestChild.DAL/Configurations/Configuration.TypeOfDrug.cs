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
        ///     Типы наркотиков
        /// </summary>
        private static void TypeOfDrug(Context context)
        {
            context.TypeOfDrug.AddOrUpdate(r => r.Id, new TypeOfDrug
                {
                    Id = 1,
                    Name = "Общего действия"
                },
                new TypeOfDrug
                {
                    Id = 2,
                    Name = "Психотропные"
                },
                new TypeOfDrug
                {
                    Id = 3,
                    Name = "Сильнодействующие"
                });

            SetEidAndLastUpdateTicks(context.TypeOfDrug.ToList());
            context.SaveChanges();
        }
    }
}
