using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using OfficeOpenXml.Style;
using RestChild.Comon;
using RestChild.Comon.ToExcel;
using RestChild.Web.Models.Business.Export.Models;

namespace RestChild.Web.Models.Business.Export.AnalyticReports
{
	public class BenefitsFamilyRestBySexAndAgeExcelExport
	{
		private static ExcelTable<FamilyTimeOfRestInfo> GenerateExcel(AnalyticReportInfo<FamilyTimeOfRestInfo> data)
		{
			var columns = GenerateCampsColumnHeaders(data);
			var headers = GenerateHeaders(data);

			var excel = new ExcelTable<FamilyTimeOfRestInfo>(columns, headers, data.Data)
			{
				TableName = "Совместный отдых. Распределение по году рождения"
			};
			return excel;
		}

		public static ExcelTable<FamilyTimeOfRestInfo> GetExcelData(AnalyticReportInfo<FamilyTimeOfRestInfo> data)
		{
			return GenerateExcel(data);
		}

		private static List<List<ExcelHeader<FamilyTimeOfRestInfo>>> GenerateHeaders(AnalyticReportInfo<FamilyTimeOfRestInfo> data)
		{
			var maxYearRange = data.SubHeaders.Count;

			var headers = new List<List<ExcelHeader<FamilyTimeOfRestInfo>>>
			{
				new List<ExcelHeader<FamilyTimeOfRestInfo>>()
				{
					new ExcelHeader<FamilyTimeOfRestInfo>() {Title = "Место отдыха", RowSpan = 3,Column = 1},
					new ExcelHeader<FamilyTimeOfRestInfo>() {Title = "Период", RowSpan = 3, Column = 2},
					new ExcelHeader<FamilyTimeOfRestInfo>() {Title = "Год рождения", RowSpan = 1,Column = 3, ColSpan = maxYearRange * 2},
					new ExcelHeader<FamilyTimeOfRestInfo>() {Title ="Сопровождающие", RowSpan = 2,Column = 3 + maxYearRange * 2, ColSpan = 2}
				}
			};

			var excelHeaders = new List<ExcelHeader<FamilyTimeOfRestInfo>>();
			headers.Add(excelHeaders);
			int startColumn = 3;
			foreach (var birthDateHeader in data.SubHeaders.OrderBy(i => i.Key))
			{
				excelHeaders.Add(new ExcelHeader<FamilyTimeOfRestInfo>() { Title = birthDateHeader.Value.ToString(), ColSpan = 2, Column = startColumn });
				startColumn += 2;
			}

			excelHeaders = new List<ExcelHeader<FamilyTimeOfRestInfo>>();
			headers.Add(excelHeaders);

			for (int i = 0; i < maxYearRange; i++)
			{
				excelHeaders.Add(new ExcelHeader<FamilyTimeOfRestInfo>() { Title = "муж.", Column = 3 + i * 2 });
				excelHeaders.Add(new ExcelHeader<FamilyTimeOfRestInfo>() { Title = "жен.", Column = 4 + i * 2 });
			}

			excelHeaders.Add(new ExcelHeader<FamilyTimeOfRestInfo>() { Title = "муж.", Column = 3 + maxYearRange * 2 });
			excelHeaders.Add(new ExcelHeader<FamilyTimeOfRestInfo>() { Title = "жен.", Column = 4 + maxYearRange * 2 });

			return headers;
		}

		private static List<ExcelColumn<FamilyTimeOfRestInfo>> GenerateCampsColumnHeaders(AnalyticReportInfo<FamilyTimeOfRestInfo> data)
		{
			var manCountWidth = 6;

			var columns = new List<ExcelColumn<FamilyTimeOfRestInfo>>
			{
				new ExcelColumn<FamilyTimeOfRestInfo> {Title = "Место отдыха", Func = r => r.HotelName.FormatEx(false), Width = 31},
				new ExcelColumn<FamilyTimeOfRestInfo>() {Title = "Период", Func = r => r.TimeOfRestName, Width = 30}
			};

			foreach (var timeOfRestInfo in data.SubHeaders.OrderBy(i => i.Key))
			{
				columns.Add(new ExcelColumn<FamilyTimeOfRestInfo>()
				{
					Func = r =>
					{
						int manCount;
						return r.MaleCount.TryGetValue(timeOfRestInfo.Key, out manCount) ? manCount : 0;
					},
					Width = manCountWidth,
					HorizontalAlignment = ExcelHorizontalAlignment.Center
				});

				columns.Add(new ExcelColumn<FamilyTimeOfRestInfo>()
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

			columns.Add(new ExcelColumn<FamilyTimeOfRestInfo>()
			{
				Func = r => r.AttendantsMaleCount,
				Width = manCountWidth,
				HorizontalAlignment = ExcelHorizontalAlignment.Center
			});

			columns.Add(new ExcelColumn<FamilyTimeOfRestInfo>()
			{
				Func = r => r.AttendantsFemaleCount,
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
