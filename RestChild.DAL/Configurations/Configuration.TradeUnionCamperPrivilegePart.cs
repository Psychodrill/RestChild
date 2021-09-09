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
        ///     Кэшбэк признак льготы
        /// </summary>
        private static void TradeUnionCamperPrivilegePart(Context context)
        {
            context.TradeUnionCamperPrivilegePart.AddOrUpdate(s => s.Id,
                new TradeUnionCamperPrivilegePart
                {
                    Id = (long) TradeUnionCamperPrivilegePartEnum.FullPrivilege,
                    Name = "100% льгота",
                    IsActive = true
                },
                new TradeUnionCamperPrivilegePart
                {
                    Id = (long) TradeUnionCamperPrivilegePartEnum.NoPrivilege,
                    Name = "Не льготная категория",
                    IsActive = true
                },
                new TradeUnionCamperPrivilegePart
                {
                    Id = (long) TradeUnionCamperPrivilegePartEnum.PartlyPrivilege,
                    Name = "Льготы менее 100% стоимости",
                    IsActive = true
                });
        }
    }
}
