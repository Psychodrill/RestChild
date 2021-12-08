using System;
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
using RestChild.Extensions.Filter;
using RestChild.Web.Models;
using RestChild.Web.Models.MonitoringHotel;

namespace RestChild.Web.Controllers
{
	public partial class MonitoringHotelController
    {
		private void SetValue(ExcelWorksheet sheet, object value, int row, int col)
		{
			using (var cell = sheet.Cells[row, col])
			{
                cell.Style.Border.BorderAround(ExcelBorderStyle.Thin);
                cell.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                cell.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                cell.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                cell.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                cell.Style.WrapText = true;
                cell.Style.Font.SetFromFont(new Font("Times New Roman", 12));
                cell.Style.Font.Size = 10;
                cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                cell.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                cell.Value = value;
			}
		}

		//private void SetValue(ExcelWorksheet sheet, object value, int row, int col, int toRow, int toCol)
		//{
		//	using (var cell = sheet.Cells[row, col, toRow, toCol])
		//	{
		//		cell.Merge = true;
		//		//cell.Style.Border.BorderAround(ExcelBorderStyle.Thin);
		//		//cell.Style.Border.Top.Style = ExcelBorderStyle.Thin;
		//		//cell.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
		//		//cell.Style.Border.Left.Style = ExcelBorderStyle.Thin;
		//		//cell.Style.Border.Right.Style = ExcelBorderStyle.Thin;
		//		cell.Style.WrapText = true;
		//		//cell.Style.Font.SetFromFont(new Font("Times New Roman", 12));
		//		//cell.Style.Font.Size = 10;
		//		//cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
		//		//cell.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
		//		cell.Value = value;
		//		if (value is DateTime)
		//		{
		//			cell.Style.Numberformat.Format = "dd.MM.yyyy HH:mm:ss";
		//		}
		//	}
		//}

		private void SetHeaderValue(ExcelWorksheet sheet, object value, int row, int col)
		{
			using (var cell = sheet.Cells[row, col])
			{
				cell.Style.Border.BorderAround(ExcelBorderStyle.Thin);
				cell.Style.Border.Top.Style = ExcelBorderStyle.Thin;
				cell.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
				cell.Style.Border.Left.Style = ExcelBorderStyle.Thin;
				cell.Style.Border.Right.Style = ExcelBorderStyle.Thin;
				cell.Style.WrapText = true;
				cell.Style.Font.SetFromFont(new Font("Times New Roman", 12));
				cell.Style.Font.Bold = true;
				cell.Style.Font.Size = 10;
				cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
				cell.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
				cell.Value = value;
			}
		}

		/// <summary>
		/// выгрузка сведений
		/// </summary>
		public ActionResult ExcelMonitoringHotelList(MonitoringHotelFilterModel model)
		{
			SetUnitOfWorkInRefClass(UnitOfWork);
			string tempFile;
			using (var pkg = new ExcelPackage())
			{
				var sheet = pkg.Workbook.Worksheets.Add("Мониторинг объектов отдыха");
				Sheet(sheet, model);
				tempFile = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
				pkg.SaveAs(new FileInfo(tempFile));

			}

            var fileName = "Объекты отдыха";
            //if (fileName.Length > 100)
            //{
            //    fileName = fileName.Substring(0, 97) + "...";
            //}

			return FileAndDeleteOnClose(tempFile, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
				$"{fileName}.xlsx");
		}

		/// <summary>
		/// выгрузка по списка
		/// </summary>
		protected void Sheet(ExcelWorksheet sheet, MonitoringHotelFilterModel model)
		{
			SetUnitOfWorkInRefClass(UnitOfWork);
            var pageSize = 5000;

            model.PageNumber = model.PageNumber <= 0 ? 1 : model.PageNumber;
            var skip = (model.PageNumber - 1) * pageSize;
            var entity = GetMonitoringHotelsQuery(model);
            var totalCount = entity.Count();
            var list = entity.OrderBy(i => i.Id)
                .Skip(skip)
                .Take(pageSize)
                .ToArray();
            if (entity == null)
			{
				return;
			}

			model = model ?? new MonitoringHotelFilterModel();
			model.Results = new CommonPagedList<MonitoringHotel>(list, model.PageNumber, pageSize, totalCount);
            var row = 1;
			var cols = new[] { "№", "Полное название", "Сокращенное название", "Регион", "Фактический адрес", "ИНН" };

			sheet.Column(2).Width = 35;
            sheet.Column(3).Width = 20;
            sheet.Column(4).Width = 30;
			sheet.Column(5).Width = 40;
			sheet.Column(6).Width = 20;
            sheet.Name = "Объекты отдыха для мониторинга";
            var col = 1;
            foreach (var c in cols)
            {
                SetHeaderValue(sheet, c, row, col);
                col++;
            }
            row++;
			foreach (var hotel in entity)
			{
                SetValue(sheet, row-1, row, 1);
                SetValue(sheet, $"{hotel.FullName}", row, 2);
                SetValue(sheet, $"{hotel.ShortName}", row, 3);
                SetValue(sheet, $"{hotel.Region.Name}", row, 4);
                SetValue(sheet, $"{hotel.FactAddress}", row, 5);
                SetValue(sheet, $"{hotel.Inn}", row, 6);
                row++;
			}

			foreach (var entry in UnitOfWork.Context.ChangeTracker.Entries())
			{
				entry.State = EntityState.Detached;
			}
		}
	}
}
