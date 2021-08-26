using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using RestChild.Comon.ToExcel;
using RestChild.Web.Models.Business.Export.Models;

namespace RestChild.Web.Models.Business.Export.AnalyticReports
{
	public class BenefitsCampsByCategoryAndDistrictExport
	{
		public static BaseExcelTable GetExcel(AnalyticReportInfo<BenefitCategoryAndDistrict> data,string tableName,string worksheetName)
		{
			var columns = BenefitsCampsByCategoryAndDistrictBuilder.GenerateColumns(data);
			var headers = BenefitsCampsByCategoryAndDistrictBuilder.GenerateHeaders(data);

			var result = data.Data.Select(d => new ExcelRow<BenefitCategoryAndDistrict> {Data = d, Bold = d.IsTimeOfRestTotal}).ToList();

			var item = result.FirstOrDefault();
			if (item != null)
			{
				item.Bold = true;
			}

			var excel = new ExcelTable<BenefitCategoryAndDistrict>(columns, headers, result)
			{
				TableName = tableName,
			};

			return excel;
		}
	}

	public static class BenefitsCampsByCategoryAndDistrictBuilder
	{
		public static List<List<ExcelHeader<T>>> GenerateHeaders<T>(AnalyticReportInfo<T> data)
		{
			var subHeadersCount = data.SubHeaders.Count;
			var headers = new List<List<ExcelHeader<T>>>
			{
				new List<ExcelHeader<T>>()
				{
					new ExcelHeader<T>() {Title = "Категория льготы", RowSpan = 2, Column = 1},
					new ExcelHeader<T>() {Title = "Всего", RowSpan = 2, Column = 2},
					new ExcelHeader<T>() {Title = "Административный округ", RowSpan = 1, Column = 3, ColSpan = subHeadersCount},
				}
			};

			var excelHeaders = new List<ExcelHeader<T>>();
			headers.Add(excelHeaders);
			int column = 3;
			foreach (var birthDateHeader in data.SubHeaders.OrderBy(i => i.Key))
			{
				excelHeaders.Add(new ExcelHeader<T>() { Title = birthDateHeader.Value.ToString(), Column = column });
				column += 1;
			}

			return headers;
		}

		public static List<ExcelColumn<BenefitCategoryAndDistrict>> GenerateColumns(AnalyticReportInfo<BenefitCategoryAndDistrict> data)
		{
			var manCountWidth = 15;

			var columns = new List<ExcelColumn<BenefitCategoryAndDistrict>>
			{
				new ExcelColumn<BenefitCategoryAndDistrict> { Func = r => r.BenefitCategory, Width = 31},
				new ExcelColumn<BenefitCategoryAndDistrict>() { Func = r => r.TotalCount, Width = 30,HorizontalAlignment = ExcelHorizontalAlignment.Center}
			};

			foreach (var benefitInfo in data.SubHeaders.OrderBy(i => i.Key))
			{
				columns.Add(new ExcelColumn<BenefitCategoryAndDistrict>()
				{
					Func = r =>
					{
						int cnt;
						return r.CountByDistrict.TryGetValue(benefitInfo.Key, out cnt) ? cnt : 0;
					},
					Width = manCountWidth,
					HorizontalAlignment = ExcelHorizontalAlignment.Center,
					WordWrap = true,
					VerticalAlignment = ExcelVerticalAlignment.Center
				});
			}

			return columns;
		}

		public static void MakeCellBold(ExcelRange range)
		{
			range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
			range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
			range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
			range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
			range.Style.Font.Bold = true;
			range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
		}
	}
}
