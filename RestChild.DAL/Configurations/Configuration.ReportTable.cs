using System.Data.Entity.Migrations;
using RestChild.Comon.Enumeration;
using RestChild.Domain;

namespace RestChild.DAL.Configurations
{
    internal static partial class Configuration
    {
        /// <summary>
        ///     Таблицы в отчетах
        /// </summary>
        /// <param name="context"></param>
        private static void ReportTable(Context context)
        {
            context.ReportTable.AddOrUpdate(
                s => s.Id,
                new ReportTable
                {
                    Id = ReportEnum.ReportTable.ServiceStatistics.BookingsWithoutRequests,
                    ReportSheetId = (long) ReportEnum.ReportSheet.ServiceStatistics,
                    SortOrder = 3,
                    CssClass = "main-report-table"
                });
        }
    }
}
