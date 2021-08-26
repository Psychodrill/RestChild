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
        ///     Типы отчета
        /// </summary>
        private static void ReportSheet(Context context)
        {
            context.ReportSheet.AddOrUpdate(
                s => s.Id,
                new ReportSheet
                {
                    Id = (long) ReportEnum.ReportSheet.ServiceStatistics,
                    ReportName = "Статистика оказания услуги",
                    SortOrder = 1,
                    CodeAccess = Guid.Parse(AccessRightEnum.Report.ServiceStatistics)
                });

            SetEidAndLastUpdateTicks(context.ReportSheet.ToList());
            context.SaveChanges();
        }
    }
}
