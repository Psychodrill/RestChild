using System.Data.Entity.Migrations;
using System.Linq;
using RestChild.Comon.Enumeration;
using RestChild.Domain;

namespace RestChild.DAL.Configurations
{
    internal static partial class Configuration
    {
        /// <summary>
        ///     Строки и данные в таблицы отчетов
        /// </summary>
        /// <param name="context"></param>
        private static void ReportTableRow(Context context)
        {
            context.ReportTableRow.AddOrUpdate(
                s => s.Id,
                new ReportTableRow
                {
                    Id = ReportEnum.Rows.ServiceStatistics.BookingsWithoutRequests.Row1.Id,
                    TableId = ReportEnum.ReportTable.ServiceStatistics.BookingsWithoutRequests,
                    CssClass = string.Empty,
                    SortOrder = 1
                }
            );

            context.ReportRowData.AddOrUpdate(
                s => s.Id,
                new ReportRowData
                {
                    Id = ReportEnum.Rows.ServiceStatistics.BookingsWithoutRequests.Row1.Column1,
                    RowId = ReportEnum.Rows.ServiceStatistics.BookingsWithoutRequests.Row1.Id,
                    Value = "Количество бронирований по которым нет заявлений: -",
                    SortOrder = 1
                }
            );
        }
    }
}
