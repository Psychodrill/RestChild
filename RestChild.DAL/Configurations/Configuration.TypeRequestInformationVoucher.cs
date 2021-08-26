using System;
using System.Data.Entity.Migrations;
using System.Linq;
using RestChild.Comon.Enumeration;
using RestChild.Domain;

namespace RestChild.DAL.Configurations
{
    internal static partial class Configuration
    {
        private static void TypeRequestInformationVoucher(Context context)
        {
            context.TypeRequestInformationVoucher.AddOrUpdate(t => t.Id,
                new TypeRequestInformationVoucher
                {
                    Id = (long) TypeRequestInformationVoucherEnum.RestChild,
                    Name = "Индивидуальный",
                    IsActive = true
                },
                new TypeRequestInformationVoucher
                {
                    Id = (long) TypeRequestInformationVoucherEnum.RestWithParent,
                    Name = "Совместный",
                    IsActive = true
                });

            SetEidAndLastUpdateTicks(context.TypeRequestInformationVoucher.ToList());
            context.SaveChanges();
        }
    }
}
