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
        ///     Тип лагеря
        /// </summary>
        private static void TypeOfCamp(Context context)
        {
            context.TypeOfCamp.AddOrUpdate(t => t.Id,
                new TypeOfCamp
                {
                    Id = (long) TypeOfCampEnum.IsStationary,
                    Name = "Стационарный",
                    IsActive = true,
                },

                new TypeOfCamp
                {
                    Id = (long) TypeOfCampEnum.IsCamping,
                    Name = "Палаточный",
                    IsActive = true,
                });

                SetEidAndLastUpdateTicks(context.TypeOfCamp.ToList());
            context.SaveChanges();
        }
    }
}
