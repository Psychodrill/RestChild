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
        ///     Виды транспорта в рамках заявления
        /// </summary>
        private static void TypeOfTransportInRequest(Context context)
        {
            context.TypeOfTransportInRequest.AddOrUpdate(t => t.Id,
                new TypeOfTransportInRequest
                {
                    Id = (long) TypeOfTransportInRequestEnum.Plane,
                    Name = "Воздушный транспорт (самолет)",
                    IsActive = true
                },
                new TypeOfTransportInRequest
                {
                    Id = (long) TypeOfTransportInRequestEnum.Train,
                    Name = "Назменый транспорт (поезд)",
                    IsActive = true
                });

            SetEidAndLastUpdateTicks(context.TypeOfTransportInRequest.ToList());
            context.SaveChanges();
        }
    }
}
