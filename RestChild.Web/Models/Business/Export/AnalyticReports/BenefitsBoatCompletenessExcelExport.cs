using System.Collections.Generic;
using OfficeOpenXml.Style;
using RestChild.Comon.ToExcel;
using RestChild.Web.Models.Business.Export.Models;

namespace RestChild.Web.Models.Business.Export.AnalyticReports
{
	public static class BenefitsBoatCompletenessExcelExport
	{
		public static BaseExcelTable GenerateChildRestExcel(
			AnalyticReportInfo<BenefitsByBoatCompletenessInfo> data,
			string tableName,
			string worksheetName)
		{
			return AnalyticReportExcelGenerator.GenerateExcel(data, tableName, worksheetName, GenerateChildRestColumns(),new List<List<ExcelHeader<BenefitsByBoatCompletenessInfo>>>());
		}

		public static BaseExcelTable GenerateFamilyRestExcel(
			AnalyticReportInfo<BenefitsFamilyRestByBoatCompletenessInfo> data,
			string tableName,
			string worksheetName)
		{
			return AnalyticReportExcelGenerator.GenerateExcel(data, tableName, worksheetName, GenerateFamilyRestColumns(), GenerateHeaders<BenefitsFamilyRestByBoatCompletenessInfo>());
		}

		private static List<List<ExcelHeader<T>>> GenerateHeaders<T>()
		{
			var headers = new List<List<ExcelHeader<T>>>
			{
				new List<ExcelHeader<T>>
				{
					new ExcelHeader<T> {Title = "Место отдыха", RowSpan = 2, Column = 1},
					new ExcelHeader<T> {Title = "Период", RowSpan = 2, Column = 2},
					new ExcelHeader<T> {Title = "План", RowSpan = 2, Column = 3},
					new ExcelHeader<T> {Title = "Забронировано", RowSpan = 1, Column = 4, ColSpan = 4},
					new ExcelHeader<T> {Title = "Факт", RowSpan = 1, Column = 8, ColSpan = 2}
				},
				new List<ExcelHeader<T>>
				{
					new ExcelHeader<T> {Title = "Всего", Column = 4},
					new ExcelHeader<T> {Title = "% от плана", Column = 5},
					new ExcelHeader<T> {Title = "Детей", Column = 6},
					new ExcelHeader<T> {Title = "Сопровожд.", Column = 7},
					new ExcelHeader<T> {Title = "Детей", Column = 8},
					new ExcelHeader<T> {Title = "Сопровожд.", Column = 9}
				}
			};
			return headers;
		}

		private static List<ExcelColumn<BenefitsByBoatCompletenessInfo>> GenerateChildRestColumns()
		{
			var columns = new List<ExcelColumn<BenefitsByBoatCompletenessInfo>>
			{
				new ExcelColumn<BenefitsByBoatCompletenessInfo> {Func = r => r.HotelName, Title = "Место отдыха", Width = 31},
				new ExcelColumn<BenefitsByBoatCompletenessInfo> {Func = r => r.TimeOfRest, Title = "Период", Width = 30},
				new ExcelColumn<BenefitsByBoatCompletenessInfo> {Func = r => r.Planned, Title = "План", Width = 7},
				new ExcelColumn<BenefitsByBoatCompletenessInfo> {Func = r => r.Booked, Title = "Забронировано", Width = 7},
				new ExcelColumn<BenefitsByBoatCompletenessInfo> {Func = r => $"{r.PlannedPercent:p}", Title = "% от плана", Width = 12},
				new ExcelColumn<BenefitsByBoatCompletenessInfo> {Func = r => r.Fact, Title = "Факт", Width = 7},
				new ExcelColumn<BenefitsByBoatCompletenessInfo>
				{
					Func = r => $"{r.BookedPercent:p}",
					Title = "% от забронированного",
					Width = 10
				}
			};

			return columns;
		}

		private static List<ExcelColumn<BenefitsFamilyRestByBoatCompletenessInfo>> GenerateFamilyRestColumns()
		{
			var columns = new List<ExcelColumn<BenefitsFamilyRestByBoatCompletenessInfo>>
			{
				new ExcelColumn<BenefitsFamilyRestByBoatCompletenessInfo> {Func = r => r.HotelName,  Width = 50},
				new ExcelColumn<BenefitsFamilyRestByBoatCompletenessInfo> {Func = r => r.TimeOfRest, Width = 35},
				new ExcelColumn<BenefitsFamilyRestByBoatCompletenessInfo> {Func = r => r.Planned, Width = 7},
				new ExcelColumn<BenefitsFamilyRestByBoatCompletenessInfo> {Func = r => r.BookedTotal, Width = 7},
				new ExcelColumn<BenefitsFamilyRestByBoatCompletenessInfo> {Func = r => $"{r.PlannedPercent:p}", Width = 10},
				new ExcelColumn<BenefitsFamilyRestByBoatCompletenessInfo> {Func = r => r.BookedChild, Width = 7},
				new ExcelColumn<BenefitsFamilyRestByBoatCompletenessInfo> {Func = r => r.BookedAttendant, Width = 7},
				new ExcelColumn<BenefitsFamilyRestByBoatCompletenessInfo> {Func = r => r.FactChild,Width = 7},
				new ExcelColumn<BenefitsFamilyRestByBoatCompletenessInfo> {Func = r => r.FactAttendant,Width = 7}
			};

			return columns;
		}
	}

	public class AnalyticReportExcelGenerator
	{
		public static ExcelTable<T> GenerateExcel<T>(AnalyticReportInfo<T> data, string tableName, string worksheetName,
			List<ExcelColumn<T>> columns, List<List<ExcelHeader<T>>> headers)
		{
			var excel = new ExcelTable<T>(columns, headers, data.Data)
			{
				TableName = tableName,
			};

			return excel;
		}
	}
}
