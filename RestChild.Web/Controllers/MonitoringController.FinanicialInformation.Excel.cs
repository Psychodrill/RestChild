using System.Drawing;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using RestChild.Comon.Enumeration;
using RestChild.DAL.RepositoryExtensions;
using RestChild.Domain;

namespace RestChild.Web.Controllers
{
    /// <summary>
    ///     Сведения о финансировании оздоровительной кампании
    /// </summary>
    public partial class MonitoringController
    {
        /// <summary>
        ///     Сведения о численности детей
        /// </summary>
        [HttpGet]
        [Route("Monitoring/FinancialInformationExcel")]
        public ActionResult FinancialInformationExcel(long? yearOfRestId = null, long? organisationId = null)
        {
            if (!Security.HasAnyRightsForSomeOrganization(AccessRightEnum.Monitoring.FinanceInformation.View)
                && !Security.HasRight(AccessRightEnum.Monitoring.CompleteFormDownload))
            {
                return RedirectToAvalibleAction();
            }

            var organizationIds = AccessRightEnum.Monitoring.FinanceInformation.View.GetSecurityOrganiztion();

            var yor = UnitOfWork.GetById<YearOfRest>(yearOfRestId);

            if ((!Security.HasRight(AccessRightEnum.Monitoring.CompleteFormDownload) &&
                (organisationId == null || !organizationIds.Contains(organisationId))) || yor == null)
            {
                return RedirectToAvalibleAction();
            }

            var query = UnitOfWork.GetSet<MonitoringFinancialData>()
                .Where(s => s.FinanceInformation.YearOfRestId == yearOfRestId);

            if (organisationId.HasValue)
            {
                query = query.Where(h => h.FinanceInformation.OrganisationId == organisationId);
            }

            var items = query.ToList();

            string tempFile;
            using (var pkg = new ExcelPackage())
            {
                var sheet = pkg.Workbook.Worksheets.Add("Финансирование");

                using (var excelRange = sheet.Cells[2, 1, 2, 10])
                {
                    excelRange.Style.Border.BorderAround(ExcelBorderStyle.Medium);
                    excelRange.Style.WrapText = true;
                    excelRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    excelRange.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    excelRange.Style.Font.SetFromFont(new Font("Times New Roman", 9));
                    excelRange.Style.Font.Bold = true;
                    excelRange.Merge = true;
                    excelRange.Value =
                        "Сведения о финансировании оздоровительной кампании";
                }

                FillHeaderCell(sheet, 1, 3,
                    "Наименование", 2);
                FillHeaderCell(sheet, 2, 3,
                    "№ строки", 2);
                FillHeaderCell(sheet, 3, 3,
                    "Сведения о финансировании оздоровительной кампании субъекта РФ в указанный период", 0, 7);
                FillHeaderCell(sheet, 3, 4,
                    "Организации отдыха детей и их оздоровления сезонного действия или круглогодичного действия", 0, 7);

                var widths = new[] {42, 10, 15, 17, 14, 14, 14, 14, 14, 14};
                var headers = new[]
                {
                    "План на текущий год",
                    "Итого затрачено денежных средств на отчетную дату, из них:",
                    "Июнь",
                    "Июль",
                    "Август ",
                    "Сентябрь",
                    "Октябрь",
                    "Примечание"
                };

                for (var i = 0; i < widths.Length; i++)
                {
                    sheet.Column(i + 1).Width = widths[i];
                    FillHeaderCell(sheet, i + 1, 6,
                        (i + 1).ToString(), 0);

                    if (i >= 2)
                    {
                        FillHeaderCell(sheet, i + 1, 5,
                            headers[i - 2], 0);
                    }
                }

                var financialSources = UnitOfWork.GetSet<MonitoringFinancialSource>().OrderBy(f => f.Code).ToList();

                var row = 7;

                foreach (var fs in financialSources)
                {
                    var codes = fs.Children.Any() ? fs.Children.Select(c=>(long?)c.Id).ToArray() :
                        new[] {(long?)fs.Id};

                    var data = items.Where(f =>
                                       codes.Contains(
                                           f.MonitoringFinancialSourceId))
                                   .GroupBy(f => string.Empty).Select(f => new MonitoringFinancialData
                                   {
                                       Jan = f.Sum(v => v.Jan ?? 0),
                                       Feb = f.Sum(v => v.Feb ?? 0),
                                       Mar = f.Sum(v => v.Mar ?? 0),
                                       Apr = f.Sum(v => v.Apr ?? 0),
                                       May = f.Sum(v => v.May ?? 0),
                                       Jun = f.Sum(v => v.Jun ?? 0),
                                       Jul = f.Sum(v => v.Jul ?? 0),
                                       Aug = f.Sum(v => v.Aug ?? 0),
                                       Sep = f.Sum(v => v.Sep ?? 0),
                                       Oct = f.Sum(v => v.Oct ?? 0),
                                       Nov = f.Sum(v => v.Nov ?? 0),
                                       Dec = f.Sum(v => v.Dec ?? 0),
                                       Plan = f.Sum(v => v.Plan ?? 0),
                                       Comment = f.Count() > 1 ? string.Empty : f.FirstOrDefault()?.Comment
                                   }).FirstOrDefault() ??
                               new MonitoringFinancialData();

                    FillCell(sheet, 1, row, 0, fs.Name, ExcelHorizontalAlignment.Right);
                    FillCell(sheet, 2, row, 0, fs.Code, ExcelHorizontalAlignment.Center);
                    FillCell(sheet, 3, row, 0, data.Plan, ExcelHorizontalAlignment.Right);
                    FillCell(sheet, 4, row, 0,
                        (data.Jun ?? 0) + (data.Jul ?? 0) + (data.Aug ?? 0) + (data.Sep ?? 0) + (data.Oct ?? 0),
                        ExcelHorizontalAlignment.Right);
                    FillCell(sheet, 5, row, 0, data.Jun, ExcelHorizontalAlignment.Right);
                    FillCell(sheet, 6, row, 0, data.Jul, ExcelHorizontalAlignment.Right);
                    FillCell(sheet, 7, row, 0, data.Aug, ExcelHorizontalAlignment.Right);
                    FillCell(sheet, 8, row, 0, data.Sep, ExcelHorizontalAlignment.Right);
                    FillCell(sheet, 9, row, 0, data.Oct, ExcelHorizontalAlignment.Right);
                    FillCell(sheet, 10, row, 0, data.Comment);

                    if (fs.Children.Any())
                    {
                        using (var excelRange = sheet.Cells[row, 1, row, 10])
                        {
                            excelRange.Style.Border.BorderAround(ExcelBorderStyle.Medium);
                        }
                    }

                    row++;
                }

                using (var excelRange = sheet.Cells[3, 1, row - 1, 10])
                {
                    excelRange.Style.Border.BorderAround(ExcelBorderStyle.Medium);
                }

                tempFile = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
                pkg.SaveAs(new FileInfo(tempFile));
            }

            return FileAndDeleteOnClose(tempFile, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "Сведения о финансировании оздоровительной кампании.xlsx");
        }

        /// <summary>
        /// заполнить ячейку заголовка
        /// </summary>
        private static void FillHeaderCell(ExcelWorksheet sheet, int column, int row, string v, int rows = 0,
            int cols = 0)
        {
            using (var excelRange = sheet.Cells[row, column, row + rows, column + cols])
            {
                excelRange.Value = v;
                excelRange.Style.Border.BorderAround(ExcelBorderStyle.Medium);
                excelRange.Style.Font.SetFromFont(new Font("Times New Roman", 9));
                excelRange.Style.Font.Bold = true;
                excelRange.Style.WrapText = true;
                excelRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                excelRange.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                if (rows > 0 || cols > 0)
                {
                    excelRange.Merge = true;
                }
            }
        }
    }
}
