using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using RestChild.Comon;
using RestChild.Comon.Enumeration;
using RestChild.DAL.RepositoryExtensions;
using RestChild.Domain;

namespace RestChild.Web.Controllers
{
    /// <summary>
    ///     Сведения о численности детей
    /// </summary>
    public partial class MonitoringController
    {
        /// <summary>
        ///     Сведения о численности детей
        /// </summary>
        [HttpGet]
        [Route("Monitoring/ChildrenNumberInformationExcel")]
        public ActionResult ChildrenNumberInformationExcel(long? yearOfRestId = null, long? organisationId = null)
        {
            if (!Security.HasAnyRightsForSomeOrganization(AccessRightEnum.Monitoring.ChildrenNumberInformation.View)
                && !Security.HasRight(AccessRightEnum.Monitoring.CompleteFormDownload))
            {
                return RedirectToAvalibleAction();
            }

            var organizationIds = AccessRightEnum.Monitoring.ChildrenNumberInformation.View.GetSecurityOrganiztion();

            var yor = UnitOfWork.GetById<YearOfRest>(yearOfRestId);

            if (!Security.HasRight(AccessRightEnum.Monitoring.CompleteFormDownload) &&
                (organisationId == null || !organizationIds.Contains(organisationId)) || yor == null)
            {
                return RedirectToAvalibleAction();
            }

            var query = UnitOfWork.GetSet<MonitoringChildrenNumberInformation>()
                .Where(s => s.YearOfRestId == yearOfRestId);

            if (organisationId.HasValue)
            {
                query = query.Where(h => h.OrganisationId == organisationId);
            }

            var hotels = query.SelectMany(g => g.HotelDatas.Select(h => h.Hotel))
                .Distinct().OrderBy(h => h.FullName).ToList();

            var data = query.SelectMany(v => v.HotelDatas.SelectMany(s => s.TourDatas))
                .Include(v => v.HotelData).ToList();

            string tempFile;
            using (var pkg = new ExcelPackage())
            {
                var sheet = pkg.Workbook.Worksheets.Add("План-факт");

                var widths = new[] {4, 21, 45, 35, 13, 13, 13, 13};
                var headers = new[]
                {
                    "№ п/п",
                    "Наименование субъекта Российской Федерации",
                    "Наименование организации отдыха и оздоровления ",
                    "Фактический адрес местоположение",
                    "Дата заезда ",
                    "Дата выезда",
                    "Плановое количество детей ",
                    "Количество отдохнувших детей"
                };

                for (var i = 0; i < widths.Length; i++)
                {
                    sheet.Column(i + 1).Width = widths[i];
                    using (var excelRange = sheet.Cells[3, i + 1])
                    {
                        excelRange.Value = headers[i];
                        excelRange.Style.Border.BorderAround(ExcelBorderStyle.Thin);
                        excelRange.Style.Border.Top.Style = ExcelBorderStyle.Medium;
                        excelRange.Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                        if (i == 0)
                        {
                            excelRange.Style.Border.Left.Style = ExcelBorderStyle.Medium;
                        }

                        if (i + 1 == widths.Length)
                        {
                            excelRange.Style.Border.Right.Style = ExcelBorderStyle.Medium;
                        }

                        excelRange.Style.WrapText = true;
                        excelRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        excelRange.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        excelRange.Style.Font.SetFromFont(new Font("Times New Roman", 10));
                        excelRange.Style.Font.Bold = true;
                    }
                }

                using (var excelRange = sheet.Cells[2, 1, 2, 8])
                {
                    excelRange.Style.Border.BorderAround(ExcelBorderStyle.Medium);
                    excelRange.Style.WrapText = true;
                    excelRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    excelRange.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    excelRange.Style.Font.SetFromFont(new Font("Times New Roman", 10));
                    excelRange.Style.Font.Bold = true;
                    excelRange.Merge = true;
                    excelRange.Value =
                        "Сведения о численности детей, направленных на отдых и оздоровление за счет средств бюджета Правительства Москвы, и организациях отдыха детей и их оздоровления, участвующих в оздоровительной кампании";
                }

                sheet.Row(2).Height = 35;

                var index = 1;
                var row = 4;
                foreach (var hotelData in hotels)
                {
                    var toursData = data.Where(d => d.HotelData.HotelId == hotelData.Id)
                        .GroupBy(g => new {g.DateIn, g.DateOut})
                        .OrderBy(g => g.Key.DateIn).ToList();
                    var rows = toursData.Count;
                    if (rows > 0)
                    {
                        rows--;
                    }

                    FillCell(sheet, 1, row, rows, index, ExcelHorizontalAlignment.Center);
                    FillCell(sheet, 2, row, rows, hotelData.Region?.Name ?? string.Empty);
                    FillCell(sheet, 3, row, rows, hotelData.FullName ?? string.Empty);
                    FillCell(sheet, 4, row, rows, hotelData.FactAddress ?? string.Empty);


                    var rowIndex = 0;
                    foreach (var tourData in toursData)
                    {
                        FillCell(sheet, 5, row + rowIndex, 0, tourData.Key.DateIn.FormatEx(),
                            ExcelHorizontalAlignment.Center);
                        FillCell(sheet, 6, row + rowIndex, 0, tourData.Key.DateOut.FormatEx(),
                            ExcelHorizontalAlignment.Center);
                        FillCell(sheet, 7, row + rowIndex, 0, tourData.Sum(v => v.PlanChildrenCount ?? 0),
                            ExcelHorizontalAlignment.Center);
                        FillCell(sheet, 8, row + rowIndex, 0, tourData.Sum(v => v.FactChildrenCount ?? 0),
                            ExcelHorizontalAlignment.Center);
                        rowIndex++;
                    }

                    if (rowIndex == 0)
                    {
                        FillCell(sheet, 5, row + rowIndex, 0, string.Empty);
                        FillCell(sheet, 6, row + rowIndex, 0, string.Empty);
                        FillCell(sheet, 7, row + rowIndex, 0, string.Empty);
                        FillCell(sheet, 8, row + rowIndex, 0, string.Empty);
                    }

                    row += rows + 1;
                    index++;
                }

                using (var excelRange = sheet.Cells[3, 1, row - 1, 8])
                {
                    excelRange.Style.Border.BorderAround(ExcelBorderStyle.Medium);
                }

                tempFile = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
                pkg.SaveAs(new FileInfo(tempFile));
            }

            return FileAndDeleteOnClose(tempFile, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "Сведения об организациях отдыха и оздоровления.xlsx");
        }

        /// <summary>
        ///     заполнить ячейку
        /// </summary>
        private static void FillCell(ExcelWorksheet sheet, int column, int row, int rows, object index,
            ExcelHorizontalAlignment align = ExcelHorizontalAlignment.Left)
        {
            using (var excelRange = sheet.Cells[row, column, row + rows, column])
            {
                excelRange.Value = index;
                excelRange.Style.Border.BorderAround(ExcelBorderStyle.Thin);
                excelRange.Style.Font.SetFromFont(new Font("Times New Roman", 12));
                excelRange.Style.WrapText = true;
                excelRange.Style.HorizontalAlignment = align;
                excelRange.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                if (rows > 0)
                {
                    excelRange.Merge = true;
                }
            }
        }
    }
}
