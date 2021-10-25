using OfficeOpenXml.Style;
using RestChild.Comon.ToExcel;
using RestChild.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestChild.Web.Models.Business
{
    public class LeisureFacilitiesListExcelExport
    {
        public static string GenerateFile(IEnumerable<LeisureFacilities> filter)
        {
            var columns = new List<ExcelColumn<LeisureFacilities>>
            {
                new ExcelColumn<LeisureFacilities> {Title = "Полное название", Func = t => t.Fullname, Width = 25},
                new ExcelColumn<LeisureFacilities> {Title = "Сокращённое название", Func = t => t.Abbreviated, Width = 35},
                new ExcelColumn<LeisureFacilities> {Title = "Регион", Func = t => t.StateDistrict.Name, Width = 20},
                new ExcelColumn<LeisureFacilities> {Title = "Фактический адрес", Func = t => t.ActualAdress, Width = 30},
                new ExcelColumn<LeisureFacilities> {Title = "ИНН", Func = t => t.Inn, Width = 20},
            };

            columns = columns.Select(c => {
                c.WordWrap = true;
                c.VerticalAlignment = ExcelVerticalAlignment.Center;
                return c;
            }).ToList();

            using (var excel = new ExcelTable<LeisureFacilities>(columns))
            {
                const int startRow = 1;
                var excelWorksheet = excel.CreateExcelWorksheet("Списки объектов отдыха");

                excel.TableName = "Объекты отдыха";
                

                excel.DataBind(excelWorksheet, filter, ExcelBorderStyle.Thin, startRow);
                return excel.CreateFileExcel();
            }
        }


    }
}
