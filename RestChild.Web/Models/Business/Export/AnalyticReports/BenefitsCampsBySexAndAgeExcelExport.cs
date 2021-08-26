using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using OfficeOpenXml.Style;
using RestChild.Comon;
using RestChild.Comon.ToExcel;
using RestChild.Web.Models.Business.Export.Models;

namespace RestChild.Web.Models.Business.Export.AnalyticReports
{

	public class BenefitsCampsBySexAndAgeExcelExport
	{
		private static ExcelTable<CampTimeOfRestInfo> GenerateExcel(AnalyticReportInfo<CampTimeOfRestInfo> data)
		{
			var columns = GenerateCampsColumnHeaders(data);
			var headers = GenerateHeaders(data);

			var excel = new ExcelTable<CampTimeOfRestInfo>(columns, headers, data.Data)
			{
				TableName = "Индивидуальный отдых. Распределение по году рождения."
			};
			return excel;
		}

		public static ExcelTable<CampTimeOfRestInfo> GetExcelData(AnalyticReportInfo<CampTimeOfRestInfo> data)
		{
			return GenerateExcel(data);
		}
		private static List<List<ExcelHeader<CampTimeOfRestInfo>>> GenerateHeaders(AnalyticReportInfo<CampTimeOfRestInfo> data)
		{
			var maxYearRange = data.SubHeaders.Count;

			var headers = new List<List<ExcelHeader<CampTimeOfRestInfo>>>
			{
				new List<ExcelHeader<CampTimeOfRestInfo>>()
				{
					new ExcelHeader<CampTimeOfRestInfo>() {Title = "Место отдыха", RowSpan = 3,Column = 1},
					new ExcelHeader<CampTimeOfRestInfo>() {Title = "Период", RowSpan = 3, Column = 2},
					new ExcelHeader<CampTimeOfRestInfo>() {Title = "Год рождения", RowSpan = 1,Column = 3, ColSpan = maxYearRange * 2},
					new ExcelHeader<CampTimeOfRestInfo>() {Title ="Всего", RowSpan = 2,Column = 3 + maxYearRange * 2, ColSpan = 2}
				}
			};

			var excelHeaders = new List<ExcelHeader<CampTimeOfRestInfo>>();
			headers.Add(excelHeaders);
			int startColumn = 3;
			foreach (var birthDateHeader in data.SubHeaders.OrderBy(i => i.Key))
			{
				excelHeaders.Add(new ExcelHeader<CampTimeOfRestInfo>() { Title = birthDateHeader.Value, ColSpan = 2, Column = startColumn });
				startColumn += 2;
			}

			excelHeaders = new List<ExcelHeader<CampTimeOfRestInfo>>();
			headers.Add(excelHeaders);
			
			for (int i = 0; i < maxYearRange; i++)
			{
				excelHeaders.Add(new ExcelHeader<CampTimeOfRestInfo>() { Title = "муж.", Column = 3 + i * 2 });
				excelHeaders.Add(new ExcelHeader<CampTimeOfRestInfo>() { Title = "жен.", Column = 4 + i * 2 });
			}

			excelHeaders.Add(new ExcelHeader<CampTimeOfRestInfo>() { Title = "муж.", Column = 3 + maxYearRange * 2 });
			excelHeaders.Add(new ExcelHeader<CampTimeOfRestInfo>() { Title = "жен.", Column = 4 + maxYearRange * 2 });

			return headers;
		}
		private static List<ExcelColumn<CampTimeOfRestInfo>> GenerateCampsColumnHeaders(AnalyticReportInfo<CampTimeOfRestInfo> data)
		{
			var manCountWidth = 6;

			var columns = new List<ExcelColumn<CampTimeOfRestInfo>>
			{
				new ExcelColumn<CampTimeOfRestInfo> {Title = "Место отдыха", Func = r => r.HotelName.FormatEx(false), Width = 31},
				new ExcelColumn<CampTimeOfRestInfo>() {Title = "Период", Func = r => r.TimeOfRestName, Width = 30}
			};

			foreach (var timeOfRestInfo in data.SubHeaders.OrderBy(i => i.Key))
			{
				columns.Add(new ExcelColumn<CampTimeOfRestInfo>()
				{
					Func = r =>
					{
						int manCount;
						return r.MaleCount.TryGetValue(timeOfRestInfo.Key, out manCount) ? manCount : 0;
					},
					Width = manCountWidth,
					HorizontalAlignment = ExcelHorizontalAlignment.Center
				});

				columns.Add(new ExcelColumn<CampTimeOfRestInfo>()
				{
					Func = r =>
					{
						int manCount;
						return r.FemaleCount.TryGetValue(timeOfRestInfo.Key, out manCount) ? manCount : 0;
					},
					Width = manCountWidth,
					HorizontalAlignment = ExcelHorizontalAlignment.Center
				});
			}

			columns.Add(new ExcelColumn<CampTimeOfRestInfo>()
			{
				Func = r => r.TotalTimeOfRestMaleCount,
				Width = manCountWidth,
				HorizontalAlignment = ExcelHorizontalAlignment.Center
			});

			columns.Add(new ExcelColumn<CampTimeOfRestInfo>()
			{
				Func  = r => r.TotalTimeOfRestFemaleCount,
				Width = manCountWidth,
				HorizontalAlignment = ExcelHorizontalAlignment.Center
			});

			columns = columns.Select(
				c =>
				{
					c.WordWrap = true;
					c.VerticalAlignment = ExcelVerticalAlignment.Center;
					return c;
				}).ToList();
			return columns;
		}
	}
}