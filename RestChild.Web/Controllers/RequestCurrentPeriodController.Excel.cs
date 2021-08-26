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
using RestChild.Web.Models;

namespace RestChild.Web.Controllers
{
	public partial class RequestCurrentPeriodController
	{
		private void SetValue(ExcelWorksheet sheet, object value, int row, int col)
		{
			using (var cell = sheet.Cells[row, col])
			{
				//cell.Style.Border.BorderAround(ExcelBorderStyle.Thin);
				//cell.Style.Border.Top.Style = ExcelBorderStyle.Thin;
				//cell.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
				//cell.Style.Border.Left.Style = ExcelBorderStyle.Thin;
				//cell.Style.Border.Right.Style = ExcelBorderStyle.Thin;
				cell.Style.WrapText = true;
				//cell.Style.Font.SetFromFont(new Font("Times New Roman", 12));
				//cell.Style.Font.Size = 10;
				//cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
				//cell.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
				cell.Value = value;
			}
		}

		private void SetValue(ExcelWorksheet sheet, object value, int row, int col, int toRow, int toCol)
		{
			using (var cell = sheet.Cells[row, col, toRow, toCol])
			{
				cell.Merge = true;
				//cell.Style.Border.BorderAround(ExcelBorderStyle.Thin);
				//cell.Style.Border.Top.Style = ExcelBorderStyle.Thin;
				//cell.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
				//cell.Style.Border.Left.Style = ExcelBorderStyle.Thin;
				//cell.Style.Border.Right.Style = ExcelBorderStyle.Thin;
				cell.Style.WrapText = true;
				//cell.Style.Font.SetFromFont(new Font("Times New Roman", 12));
				//cell.Style.Font.Size = 10;
				//cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
				//cell.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
				cell.Value = value;
				if (value is DateTime)
				{
					cell.Style.Numberformat.Format = "dd.MM.yyyy HH:mm:ss";
				}
			}
		}

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
		public ActionResult ExcelYearList(long id)
		{
			SetUnitOfWorkInRefClass(UnitOfWork);
			string tempFile;
			using (var pkg = new ExcelPackage())
			{
				var sheet = pkg.Workbook.Worksheets.Add("Общие сведения");

				var model = PrepareYearsModel(id);
				LimitGeneralSheet(sheet, model);


				foreach (var limit in model.Limits.Values.Where(l => l.Data.TypeOfRestId.HasValue).ToList())
				{
					limit.ViewRequestType = 1;
					var sheetData = pkg.Workbook.Worksheets.Add(limit.Data.Point);
					LimitToSheet(sheetData, limit);
				}

				tempFile = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
				pkg.SaveAs(new FileInfo(tempFile));

			}

			return FileAndDeleteOnClose(tempFile, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
				"Квоты.xlsx");
		}

		/// <summary>
		/// выгрузка сведений
		/// </summary>
		protected ActionResult ExcelList(LimitEditModel model)
		{
			SetUnitOfWorkInRefClass(UnitOfWork);
			string tempFile;
			using (var pkg = new ExcelPackage())
			{
				var sheet = pkg.Workbook.Worksheets.Add(model.Data.Point);
				LimitToSheet(sheet, model);
				tempFile = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
				pkg.SaveAs(new FileInfo(tempFile));

			}

            var fileName = model.Data.Name;
            if (fileName.Length > 100)
            {
                fileName = fileName.Substring(0, 97) + "...";
            }

			return FileAndDeleteOnClose(tempFile, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
				$"{fileName}.xlsx");
		}

		private void LimitGeneralSheet(ExcelWorksheet sheet, YearOfRestModel model)
		{
			var row = 1;
			var cols = new[] {"№", "Квота", "Размер квоты", "Поступило заявлений всего", "Отобрано отдыхающих"};
			var col = 1;
			foreach (var c in cols)
			{
				SetHeaderValue(sheet, c, row, col);
				col++;
			}

			row++;
			sheet.Column(2).Width = 71;

			foreach (var limit in model.Limits.Values)
			{
				SetValue(sheet, limit.Data.Point, row, 1);
				SetValue(sheet, limit.Data.Name, row, 2);
				SetValue(sheet, limit.Data.Limit, row, 3);
				SetValue(sheet, limit.FactCount, row, 4);
				SetValue(sheet, limit.FactIncludedCount, row, 5);
				row++;
			}
		}

		/// <summary>
		/// выгрузка по списка
		/// </summary>
		protected void LimitToSheet(ExcelWorksheet sheet, LimitEditModel model)
		{
			SetUnitOfWorkInRefClass(UnitOfWork);

			var entity = UnitOfWork.GetById<ListTravelers>(model?.Data?.Id);

			if (entity == null)
			{
				return;
			}

			model = model ?? new LimitEditModel();
			model.Data = entity;

			model.FactCount = UnitOfWork.GetSet<ListTravelersRequest>().Count(l => l.ListTravelersId == model.Data.Id);
			model.FactIncludedCount =
				UnitOfWork.GetSet<ListTravelersRequest>()
					.Where(l => l.ListTravelersId == model.Data.Id && l.IsIncluded)
					.Sum(l => (int?)(l.Request.TypeOfRestId == (long)TypeOfRestEnum.YouthRestOrphanCamps ? 1 : l.Request.CountPlace)) ?? 0;
			model.FactAttendantCount =
				UnitOfWork.GetSet<ListTravelersRequest>()
					.Where(l => l.ListTravelersId == model.Data.Id && l.IsIncluded)
					.Sum(l => (int?)(l.Request.TypeOfRestId == (long)TypeOfRestEnum.YouthRestOrphanCamps ? 0 : l.Request.CountAttendants)) ?? 0;

			var row = 1;
			var headers = new[]
			{
				$"{model.Data.Point} {model.Data.Name}",
				$"Размер квоты: {model.Data.Limit}",
				$"Фактически включено в квоту детей: {model.FactIncludedCount}{(model.FactAttendantCount>0?$", сопровождающих: {model.FactAttendantCount}":string.Empty)}"
			};
			var cols = new[] { "№", "Номер", "Номер ПГУ", "Дата заявления", "Отдыхающий", "Льгота", "Направления отдыха", "Время отдыха", "Кол-во баллов", "Очередь", "Включен в список" };

			sheet.Column(2).Width = 30;
			sheet.Column(4).Width = 20;
			sheet.Column(5).Width = 35;
			sheet.Column(6).Width = 36;
			sheet.Column(7).Width = 40;
            sheet.Column(8).Width = 25;
			sheet.Column(9).Width = 25;

			foreach (var s in headers)
			{
				using (var cell = sheet.Cells[row, 1, row, cols.Length])
				{
					cell.Merge = true;
					cell.Style.WrapText = true;
					cell.Style.Font.SetFromFont(new Font("Times New Roman", 12));
					cell.Style.Font.Bold = true;
					cell.Style.Font.Size = 12;
					cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
					cell.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
					cell.Value = s;
				}

				row++;
			}

			sheet.Name = model.Data.Point;
			var col = 1;
			foreach (var c in cols)
			{
				SetHeaderValue(sheet, c, row, col);
				col++;
			}

			row++;

			var query = FilterListTravelersRequests(model, entity);
			var entitys = query.OrderByDescending(r => r.Rank).ThenBy(r => r.DateRequest).ToList();
			var index = 1;
			foreach (var request in entitys)
			{
				var rowStart = row;
				if (request.Request.TypeOfRestId == (long) TypeOfRestEnum.YouthRestOrphanCamps)
				{
					SetValue(sheet, $"{request.Request.Applicant.LastName} {request.Request.Applicant.FirstName} {request.Request.Applicant.MiddleName}", row, 5);
					SetValue(sheet, "Отдыхающий", row, 6);
					row++;
				}
				else
				{
					foreach (var child in request.Request.Child)
					{
						SetValue(sheet, $"{child.LastName} {child.FirstName} {child.MiddleName}", row, 5);
						SetValue(sheet, child.BenefitType.Name, row, 6);
						row++;
					}
					foreach (var child in request.Request.Attendant)
					{
						SetValue(sheet, $"{child.LastName} {child.FirstName} {child.MiddleName}", row, 5);
						SetValue(sheet, "Сопровождающий", row, 6);
						row++;
					}
					if (request.Request.Applicant.IsAccomp)
					{
						SetValue(sheet, $"{request.Request.Applicant.LastName} {request.Request.Applicant.FirstName} {request.Request.Applicant.MiddleName}", row, 5);
						SetValue(sheet, "Сопровождающий", row, 6);
						row++;
					}
				}

				SetValue(sheet, index, rowStart, 1, row - 1, 1);
				SetValue(sheet, request.Request.RequestNumber, rowStart, 2, row - 1, 2);
				SetValue(sheet, request.Request.RequestNumberMpgu, rowStart, 3, row - 1, 3);
				SetValue(sheet, request.Request.DateRequest, rowStart, 4, row - 1, 4);

				SetValue(sheet, $"{request.Request.PlaceOfRest?.Name}, {string.Join(", ", request.Request.PlacesOfRest.Select(t => t.PlaceOfRest.Name))}", rowStart, 7, row - 1, 7);
				SetValue(sheet, $"{request.Request.TimeOfRest?.Name}, {string.Join(", ", request.Request.TimesOfRest.Select(t => t.TimeOfRest.Name))}", rowStart, 8, row - 1, 8);
				SetValue(sheet, request.Rank.HasValue ? (object)(Convert.ToDecimal(request.Rank.Value) / 10000) : "-", rowStart, 9, row - 1, 9);
				SetValue(sheet, request.Rank.HasValue ? (request.Rank == 100000 ? "1"
                    : request.Rank >= 50000 ? "2"
                    : "3") : "-", rowStart, 10, row - 1, 10);
                SetValue(sheet, request.IsIncluded ? "Да" : "Нет", rowStart, 11, row - 1, 11);
				index++;
			}

			foreach (var entry in UnitOfWork.Context.ChangeTracker.Entries())
			{
				entry.State = EntityState.Detached;
			}
		}
	}
}
