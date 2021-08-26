using System.Data.Entity.Migrations;
using System;
using RestChild.Comon.Enumeration;
using RestChild.Domain;

namespace RestChild.DAL.Configurations
{
    internal static partial class Configuration
    {
        /// <summary>
        ///     Отчеты
        /// </summary>
        /// <param name="context"></param>
        private static void Reports(Context context)
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

            context.ReportTable.AddOrUpdate(
                s => s.Id,
                new ReportTable
                {
                    Id = ReportEnum.ReportTable.ServiceStatistics.ApplicantAwaitReport,
                    ReportSheetId = (long) ReportEnum.ReportSheet.ServiceStatistics,
                    Name = "Ожидание прихода заявителя",
                    SortOrder = 1,
                    CssClass = "main-report-table"
                },
                new ReportTable
                {
                    Id = ReportEnum.ReportTable.ServiceStatistics.MainStatistics,
                    ReportSheetId = (long) ReportEnum.ReportSheet.ServiceStatistics,
                    SortOrder = 2,
                    CssClass = "main-report-table"
                },
                new ReportTable
                {
                    Id = ReportEnum.ReportTable.ServiceStatistics.InteragencyAwait,
                    ReportSheetId = (long) ReportEnum.ReportSheet.ServiceStatistics,
                    SortOrder = 3,
                    CssClass = "main-report-table"
                });

            context.ReportTableHead.AddOrUpdate(
                s => s.Id,
                new ReportTableHead
                {
                    Id = ReportEnum.Heads.ServiceStatistics.ApplicantAwaitReport.Column1,
                    ReportTableId = ReportEnum.ReportTable.ServiceStatistics.ApplicantAwaitReport,
                    Name = "-",
                    SortOrder = 1
                },
                new ReportTableHead
                {
                    Id = ReportEnum.Heads.ServiceStatistics.ApplicantAwaitReport.Column2,
                    ReportTableId = ReportEnum.ReportTable.ServiceStatistics.ApplicantAwaitReport,
                    Name = "-",
                    SortOrder = 2
                },
                new ReportTableHead
                {
                    Id = ReportEnum.Heads.ServiceStatistics.ApplicantAwaitReport.Column3,
                    ReportTableId = ReportEnum.ReportTable.ServiceStatistics.ApplicantAwaitReport,
                    Name = "-",
                    SortOrder = 3
                },
                new ReportTableHead
                {
                    Id = ReportEnum.Heads.ServiceStatistics.ApplicantAwaitReport.Column4,
                    ReportTableId = ReportEnum.ReportTable.ServiceStatistics.ApplicantAwaitReport,
                    Name = "-",
                    SortOrder = 4
                },
                new ReportTableHead
                {
                    Id = ReportEnum.Heads.ServiceStatistics.ApplicantAwaitReport.Column5,
                    ReportTableId = ReportEnum.ReportTable.ServiceStatistics.ApplicantAwaitReport,
                    Name = "-",
                    SortOrder = 5
                },
                new ReportTableHead
                {
                    Id = ReportEnum.Heads.ServiceStatistics.MainStatistics.ColumnName,
                    ReportTableId = ReportEnum.ReportTable.ServiceStatistics.MainStatistics,
                    Name = "Статистика",
                    SortOrder = 1
                },
                new ReportTableHead
                {
                    Id = ReportEnum.Heads.ServiceStatistics.MainStatistics.ByHour,
                    ReportTableId = ReportEnum.ReportTable.ServiceStatistics.MainStatistics,
                    Name = "За час",
                    SortOrder = 2
                },
                new ReportTableHead
                {
                    Id = ReportEnum.Heads.ServiceStatistics.MainStatistics.ByDay,
                    ReportTableId = ReportEnum.ReportTable.ServiceStatistics.MainStatistics,
                    Name = "За сутки",
                    SortOrder = 3
                },
                new ReportTableHead
                {
                    Id = ReportEnum.Heads.ServiceStatistics.MainStatistics.ByWeek,
                    ReportTableId = ReportEnum.ReportTable.ServiceStatistics.MainStatistics,
                    Name = "За неделю",
                    SortOrder = 4
                },
                new ReportTableHead
                {
                    Id = ReportEnum.Heads.ServiceStatistics.MainStatistics.All,
                    ReportTableId = ReportEnum.ReportTable.ServiceStatistics.MainStatistics,
                    Name = "Всего",
                    SortOrder = 5
                }
            );

            context.ReportTableRow.AddOrUpdate(
                s => s.Id,
                new ReportTableRow
                {
                    Id = ReportEnum.Rows.ServiceStatistics.ApplicantAwaitReport.Row1.Id,
                    TableId = ReportEnum.ReportTable.ServiceStatistics.ApplicantAwaitReport,
                    CssClass = "",
                    SortOrder = 1
                },
                new ReportTableRow
                {
                    Id = ReportEnum.Rows.ServiceStatistics.InteragencyAwait.Row1.Id,
                    TableId = ReportEnum.ReportTable.ServiceStatistics.InteragencyAwait,
                    CssClass = "",
                    SortOrder = 1
                },
                new ReportTableRow
                {
                    Id = ReportEnum.Rows.ServiceStatistics.MainStatistics.Requests.Id,
                    TableId = ReportEnum.ReportTable.ServiceStatistics.MainStatistics,
                    CssClass = "",
                    SortOrder = 1
                },
                new ReportTableRow
                {
                    Id = ReportEnum.Rows.ServiceStatistics.MainStatistics.Sertificates.Id,
                    TableId = ReportEnum.ReportTable.ServiceStatistics.MainStatistics,
                    CssClass = "",
                    SortOrder = 2
                },
                new ReportTableRow
                {
                    Id = ReportEnum.Rows.ServiceStatistics.MainStatistics.ApplicantAwait.Id,
                    TableId = ReportEnum.ReportTable.ServiceStatistics.MainStatistics,
                    CssClass = "",
                    SortOrder = 3
                },
                new ReportTableRow
                {
                    Id = ReportEnum.Rows.ServiceStatistics.MainStatistics.ServiceDenied.Id,
                    TableId = ReportEnum.ReportTable.ServiceStatistics.MainStatistics,
                    CssClass = "",
                    SortOrder = 4
                },
                new ReportTableRow
                {
                    Id = ReportEnum.Rows.ServiceStatistics.MainStatistics.ServiceRegisterDenied.Id,
                    TableId = ReportEnum.ReportTable.ServiceStatistics.MainStatistics,
                    CssClass = "",
                    SortOrder = 5
                },
                new ReportTableRow
                {
                    Id = ReportEnum.Rows.ServiceStatistics.MainStatistics.BaseRegisterAwait.Id,
                    TableId = ReportEnum.ReportTable.ServiceStatistics.MainStatistics,
                    CssClass = "",
                    SortOrder = 6
                },
                new ReportTableRow
                {
                    Id = ReportEnum.Rows.ServiceStatistics.MainStatistics.BaseRegisterResponsed.Id,
                    TableId = ReportEnum.ReportTable.ServiceStatistics.MainStatistics,
                    CssClass = "",
                    SortOrder = 7
                },
                new ReportTableRow
                {
                    Id = ReportEnum.Rows.ServiceStatistics.MainStatistics.MpguErrorMessages.Id,
                    TableId = ReportEnum.ReportTable.ServiceStatistics.MainStatistics,
                    CssClass = "",
                    SortOrder = 8
                }
            );

            context.ReportRowData.AddOrUpdate(
                s => s.Id,
                new ReportRowData
                {
                    Id = ReportEnum.Rows.ServiceStatistics.ApplicantAwaitReport.Row1.Column1,
                    RowId = ReportEnum.Rows.ServiceStatistics.ApplicantAwaitReport.Row1.Id,
                    Value = "-",
                    SortOrder = 1
                },
                new ReportRowData
                {
                    Id = ReportEnum.Rows.ServiceStatistics.ApplicantAwaitReport.Row1.Column2,
                    RowId = ReportEnum.Rows.ServiceStatistics.ApplicantAwaitReport.Row1.Id,
                    Value = "-",
                    SortOrder = 2
                },
                new ReportRowData
                {
                    Id = ReportEnum.Rows.ServiceStatistics.ApplicantAwaitReport.Row1.Column3,
                    RowId = ReportEnum.Rows.ServiceStatistics.ApplicantAwaitReport.Row1.Id,
                    Value = "-",
                    SortOrder = 3
                },
                new ReportRowData
                {
                    Id = ReportEnum.Rows.ServiceStatistics.ApplicantAwaitReport.Row1.Column4,
                    RowId = ReportEnum.Rows.ServiceStatistics.ApplicantAwaitReport.Row1.Id,
                    Value = "-",
                    SortOrder = 4
                },
                new ReportRowData
                {
                    Id = ReportEnum.Rows.ServiceStatistics.ApplicantAwaitReport.Row1.Column5,
                    RowId = ReportEnum.Rows.ServiceStatistics.ApplicantAwaitReport.Row1.Id,
                    Value = "-",
                    SortOrder = 5
                },
                new ReportRowData
                {
                    Id = ReportEnum.Rows.ServiceStatistics.InteragencyAwait.Row1.Column1,
                    RowId = ReportEnum.Rows.ServiceStatistics.InteragencyAwait.Row1.Id,
                    Value = "-",
                    SortOrder = 1
                },
                new ReportRowData
                {
                    Id = ReportEnum.Rows.ServiceStatistics.MainStatistics.Requests.ColumnName,
                    RowId = ReportEnum.Rows.ServiceStatistics.MainStatistics.Requests.Id,
                    Value = "Поступило заявлений/отдыхающих",
                    CssClass = "main-report-header",
                    SortOrder = 1
                },
                new ReportRowData
                {
                    Id = ReportEnum.Rows.ServiceStatistics.MainStatistics.Requests.ByHour,
                    RowId = ReportEnum.Rows.ServiceStatistics.MainStatistics.Requests.Id,
                    Value = "-",
                    SortOrder = 2
                },
                new ReportRowData
                {
                    Id = ReportEnum.Rows.ServiceStatistics.MainStatistics.Requests.ByDay,
                    RowId = ReportEnum.Rows.ServiceStatistics.MainStatistics.Requests.Id,
                    Value = "-",
                    SortOrder = 3
                },
                new ReportRowData
                {
                    Id = ReportEnum.Rows.ServiceStatistics.MainStatistics.Requests.ByWeek,
                    RowId = ReportEnum.Rows.ServiceStatistics.MainStatistics.Requests.Id,
                    Value = "-",
                    SortOrder = 4
                },
                new ReportRowData
                {
                    Id = ReportEnum.Rows.ServiceStatistics.MainStatistics.Requests.All,
                    RowId = ReportEnum.Rows.ServiceStatistics.MainStatistics.Requests.Id,
                    Value = "-",
                    SortOrder = 5
                },
                new ReportRowData
                {
                    Id = ReportEnum.Rows.ServiceStatistics.MainStatistics.Sertificates.ColumnName,
                    RowId = ReportEnum.Rows.ServiceStatistics.MainStatistics.Sertificates.Id,
                    Value = "Выдано путевок",
                    CssClass = "main-report-header",
                    SortOrder = 1
                },
                new ReportRowData
                {
                    Id = ReportEnum.Rows.ServiceStatistics.MainStatistics.Sertificates.ByHour,
                    RowId = ReportEnum.Rows.ServiceStatistics.MainStatistics.Sertificates.Id,
                    Value = "-",
                    SortOrder = 2
                },
                new ReportRowData
                {
                    Id = ReportEnum.Rows.ServiceStatistics.MainStatistics.Sertificates.ByDay,
                    RowId = ReportEnum.Rows.ServiceStatistics.MainStatistics.Sertificates.Id,
                    Value = "-",
                    SortOrder = 3
                },
                new ReportRowData
                {
                    Id = ReportEnum.Rows.ServiceStatistics.MainStatistics.Sertificates.ByWeek,
                    RowId = ReportEnum.Rows.ServiceStatistics.MainStatistics.Sertificates.Id,
                    Value = "-",
                    SortOrder = 4
                },
                new ReportRowData
                {
                    Id = ReportEnum.Rows.ServiceStatistics.MainStatistics.Sertificates.All,
                    RowId = ReportEnum.Rows.ServiceStatistics.MainStatistics.Sertificates.Id,
                    Value = "-",
                    SortOrder = 5
                },
                new ReportRowData
                {
                    Id = ReportEnum.Rows.ServiceStatistics.MainStatistics.ApplicantAwait.ColumnName,
                    RowId = ReportEnum.Rows.ServiceStatistics.MainStatistics.ApplicantAwait.Id,
                    Value = "Ожидание прихода заявителя",
                    CssClass = "main-report-header",
                    SortOrder = 1
                },
                new ReportRowData
                {
                    Id = ReportEnum.Rows.ServiceStatistics.MainStatistics.ApplicantAwait.ByHour,
                    RowId = ReportEnum.Rows.ServiceStatistics.MainStatistics.ApplicantAwait.Id,
                    Value = "-",
                    SortOrder = 2
                },
                new ReportRowData
                {
                    Id = ReportEnum.Rows.ServiceStatistics.MainStatistics.ApplicantAwait.ByDay,
                    RowId = ReportEnum.Rows.ServiceStatistics.MainStatistics.ApplicantAwait.Id,
                    Value = "-",
                    SortOrder = 3
                },
                new ReportRowData
                {
                    Id = ReportEnum.Rows.ServiceStatistics.MainStatistics.ApplicantAwait.ByWeek,
                    RowId = ReportEnum.Rows.ServiceStatistics.MainStatistics.ApplicantAwait.Id,
                    Value = "-",
                    SortOrder = 4
                },
                new ReportRowData
                {
                    Id = ReportEnum.Rows.ServiceStatistics.MainStatistics.ApplicantAwait.All,
                    RowId = ReportEnum.Rows.ServiceStatistics.MainStatistics.ApplicantAwait.Id,
                    Value = "-",
                    SortOrder = 5
                },
                new ReportRowData
                {
                    Id = ReportEnum.Rows.ServiceStatistics.MainStatistics.ServiceDenied.ColumnName,
                    RowId = ReportEnum.Rows.ServiceStatistics.MainStatistics.ServiceDenied.Id,
                    Value = "Отказ в предоставлении услуги",
                    CssClass = "main-report-header",
                    SortOrder = 1
                },
                new ReportRowData
                {
                    Id = ReportEnum.Rows.ServiceStatistics.MainStatistics.ServiceDenied.ByHour,
                    RowId = ReportEnum.Rows.ServiceStatistics.MainStatistics.ServiceDenied.Id,
                    Value = "-",
                    SortOrder = 2
                },
                new ReportRowData
                {
                    Id = ReportEnum.Rows.ServiceStatistics.MainStatistics.ServiceDenied.ByDay,
                    RowId = ReportEnum.Rows.ServiceStatistics.MainStatistics.ServiceDenied.Id,
                    Value = "-",
                    SortOrder = 3
                },
                new ReportRowData
                {
                    Id = ReportEnum.Rows.ServiceStatistics.MainStatistics.ServiceDenied.ByWeek,
                    RowId = ReportEnum.Rows.ServiceStatistics.MainStatistics.ServiceDenied.Id,
                    Value = "-",
                    SortOrder = 4
                },
                new ReportRowData
                {
                    Id = ReportEnum.Rows.ServiceStatistics.MainStatistics.ServiceDenied.All,
                    RowId = ReportEnum.Rows.ServiceStatistics.MainStatistics.ServiceDenied.Id,
                    Value = "-",
                    SortOrder = 5
                },
                new ReportRowData
                {
                    Id = ReportEnum.Rows.ServiceStatistics.MainStatistics.ServiceRegisterDenied.ColumnName,
                    RowId = ReportEnum.Rows.ServiceStatistics.MainStatistics.ServiceRegisterDenied.Id,
                    Value = "Отказ в регистрации заявления",
                    CssClass = "main-report-header",
                    SortOrder = 1
                },
                new ReportRowData
                {
                    Id = ReportEnum.Rows.ServiceStatistics.MainStatistics.ServiceRegisterDenied.ByHour,
                    RowId = ReportEnum.Rows.ServiceStatistics.MainStatistics.ServiceRegisterDenied.Id,
                    Value = "-",
                    SortOrder = 2
                },
                new ReportRowData
                {
                    Id = ReportEnum.Rows.ServiceStatistics.MainStatistics.ServiceRegisterDenied.ByDay,
                    RowId = ReportEnum.Rows.ServiceStatistics.MainStatistics.ServiceRegisterDenied.Id,
                    Value = "-",
                    SortOrder = 3
                },
                new ReportRowData
                {
                    Id = ReportEnum.Rows.ServiceStatistics.MainStatistics.ServiceRegisterDenied.ByWeek,
                    RowId = ReportEnum.Rows.ServiceStatistics.MainStatistics.ServiceRegisterDenied.Id,
                    Value = "-",
                    SortOrder = 4
                },
                new ReportRowData
                {
                    Id = ReportEnum.Rows.ServiceStatistics.MainStatistics.ServiceRegisterDenied.All,
                    RowId = ReportEnum.Rows.ServiceStatistics.MainStatistics.ServiceRegisterDenied.Id,
                    Value = "-",
                    SortOrder = 5
                },
                new ReportRowData
                {
                    Id = ReportEnum.Rows.ServiceStatistics.MainStatistics.BaseRegisterAwait.ColumnName,
                    RowId = ReportEnum.Rows.ServiceStatistics.MainStatistics.BaseRegisterAwait.Id,
                    Value = "Ожидает ответа из Базового регистра",
                    CssClass = "main-report-header",
                    SortOrder = 1
                },
                new ReportRowData
                {
                    Id = ReportEnum.Rows.ServiceStatistics.MainStatistics.BaseRegisterAwait.ByHour,
                    RowId = ReportEnum.Rows.ServiceStatistics.MainStatistics.BaseRegisterAwait.Id,
                    Value = "-",
                    SortOrder = 2
                },
                new ReportRowData
                {
                    Id = ReportEnum.Rows.ServiceStatistics.MainStatistics.BaseRegisterAwait.ByDay,
                    RowId = ReportEnum.Rows.ServiceStatistics.MainStatistics.BaseRegisterAwait.Id,
                    Value = "-",
                    SortOrder = 3
                },
                new ReportRowData
                {
                    Id = ReportEnum.Rows.ServiceStatistics.MainStatistics.BaseRegisterAwait.ByWeek,
                    RowId = ReportEnum.Rows.ServiceStatistics.MainStatistics.BaseRegisterAwait.Id,
                    Value = "-",
                    SortOrder = 4
                },
                new ReportRowData
                {
                    Id = ReportEnum.Rows.ServiceStatistics.MainStatistics.BaseRegisterAwait.All,
                    RowId = ReportEnum.Rows.ServiceStatistics.MainStatistics.BaseRegisterAwait.Id,
                    Value = "-",
                    SortOrder = 5
                },
                new ReportRowData
                {
                    Id = ReportEnum.Rows.ServiceStatistics.MainStatistics.BaseRegisterResponsed.ColumnName,
                    RowId = ReportEnum.Rows.ServiceStatistics.MainStatistics.BaseRegisterResponsed.Id,
                    Value = "Получены ответы в Базовом регистре",
                    CssClass = "main-report-header",
                    SortOrder = 1
                },
                new ReportRowData
                {
                    Id = ReportEnum.Rows.ServiceStatistics.MainStatistics.BaseRegisterResponsed.ByHour,
                    RowId = ReportEnum.Rows.ServiceStatistics.MainStatistics.BaseRegisterResponsed.Id,
                    Value = "-",
                    SortOrder = 2
                },
                new ReportRowData
                {
                    Id = ReportEnum.Rows.ServiceStatistics.MainStatistics.BaseRegisterResponsed.ByDay,
                    RowId = ReportEnum.Rows.ServiceStatistics.MainStatistics.BaseRegisterResponsed.Id,
                    Value = "-",
                    SortOrder = 3
                },
                new ReportRowData
                {
                    Id = ReportEnum.Rows.ServiceStatistics.MainStatistics.BaseRegisterResponsed.ByWeek,
                    RowId = ReportEnum.Rows.ServiceStatistics.MainStatistics.BaseRegisterResponsed.Id,
                    Value = "-",
                    SortOrder = 4
                },
                new ReportRowData
                {
                    Id = ReportEnum.Rows.ServiceStatistics.MainStatistics.BaseRegisterResponsed.All,
                    RowId = ReportEnum.Rows.ServiceStatistics.MainStatistics.BaseRegisterResponsed.Id,
                    Value = "-",
                    SortOrder = 5
                },
                new ReportRowData
                {
                    Id = ReportEnum.Rows.ServiceStatistics.MainStatistics.MpguErrorMessages.ColumnName,
                    RowId = ReportEnum.Rows.ServiceStatistics.MainStatistics.MpguErrorMessages.Id,
                    Value = "Количество некорректных сообщений от МПГУ",
                    CssClass = "main-report-header",
                    SortOrder = 1
                },
                new ReportRowData
                {
                    Id = ReportEnum.Rows.ServiceStatistics.MainStatistics.MpguErrorMessages.ByHour,
                    RowId = ReportEnum.Rows.ServiceStatistics.MainStatistics.MpguErrorMessages.Id,
                    Value = "-",
                    SortOrder = 2
                },
                new ReportRowData
                {
                    Id = ReportEnum.Rows.ServiceStatistics.MainStatistics.MpguErrorMessages.ByDay,
                    RowId = ReportEnum.Rows.ServiceStatistics.MainStatistics.MpguErrorMessages.Id,
                    Value = "-",
                    SortOrder = 3
                },
                new ReportRowData
                {
                    Id = ReportEnum.Rows.ServiceStatistics.MainStatistics.MpguErrorMessages.ByWeek,
                    RowId = ReportEnum.Rows.ServiceStatistics.MainStatistics.MpguErrorMessages.Id,
                    Value = "-",
                    SortOrder = 4
                },
                new ReportRowData
                {
                    Id = ReportEnum.Rows.ServiceStatistics.MainStatistics.MpguErrorMessages.All,
                    RowId = ReportEnum.Rows.ServiceStatistics.MainStatistics.MpguErrorMessages.Id,
                    Value = "-",
                    SortOrder = 5
                }
            );

            context.SaveChanges();
        }
    }
}
