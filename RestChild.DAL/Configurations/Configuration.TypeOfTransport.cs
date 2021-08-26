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
        ///     Виды транспорта
        /// </summary>
        private static void TypeOfTransport(Context context)
        {
            context.TypeOfTransport.AddOrUpdate(t => t.Id,
                new TypeOfTransport
                {
                    Id = (long) TypeOfTransportEnum.Aero,
                    Name = "Авиа",
                    Code = "Aero"
                },
                new TypeOfTransport
                {
                    Id = (long) TypeOfTransportEnum.Railway,
                    Name = "Ж/Д",
                    Code = "Railway"
                },
                new TypeOfTransport
                {
                    Id = (long) TypeOfTransportEnum.Auto,
                    Name = "Авто",
                    Code = "Auto"
                });

            SetEidAndLastUpdateTicks(context.TypeOfTransport.ToList());
            context.SaveChanges();
        }
    }
}
