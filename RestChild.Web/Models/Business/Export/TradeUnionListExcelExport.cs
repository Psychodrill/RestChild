using System;
using System.Collections.Generic;
using System.Linq;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using RestChild.Comon;
using RestChild.Comon.ToExcel;
using RestChild.Web.Models.TradeUnion;
using RestChild.Domain;

namespace RestChild.Web.Models.Business.Export
{
	public class TradeUnionListExcelExport
	{
		public static string GenerateFile(TradeUnionSearch search)
		{
			var columns = new List<ExcelColumn<TradeUnionList>>
			{
				new ExcelColumn<TradeUnionList> {Title = "Лагерь", Func = t => t.Camp?.Name, Width = 50},
				new ExcelColumn<TradeUnionList> {Title = "Смена", Func = t => t.GroupedTimeOfRest?.Name, Width = 12},
				new ExcelColumn<TradeUnionList> {Title = "Время отдыха с", Func = t => t.DateFrom, Width = 18},
				new ExcelColumn<TradeUnionList> {Title = "Время отдыха по", Func = t => t.DateTo, Width = 18},
				new ExcelColumn<TradeUnionList> {Title = "Кол-во детей", Func = t => t.Campers?.Count, Width = 14},
				new ExcelColumn<TradeUnionList> {Title = "Статус", Func = t => t.State?.Name, Width = 25},
				new ExcelColumn<TradeUnionList> {Title = "Полная сумма, руб.", Func = t => t.Campers?.Sum(c => c.Summa ?? 0), Width = 20},
				new ExcelColumn<TradeUnionList> {Title = "Бюджетные средства, руб.", Func = t => t.Campers?.Sum(c => c.SummaBudget ?? 0), Width = 25},
				new ExcelColumn<TradeUnionList> {Title = "Средства профсоюза, руб.", Func = t => t.Campers?.Sum(c => c.SummaTradeUnion ?? 0), Width = 25},
				new ExcelColumn<TradeUnionList> {Title = "Средства предприятия, руб.", Func = t => t.Campers?.Sum(c => c.SummaOrganization ?? 0), Width = 25},
				new ExcelColumn<TradeUnionList> {Title = "Средства родителей, руб.", Func = t => t.Campers?.Sum(c => c.SummaParent ?? 0), Width = 25}
			};

			columns = columns.Select(c => {
					c.WordWrap = true;
					c.VerticalAlignment = ExcelVerticalAlignment.Center;
					return c;
				}).ToList();

			using (var excel = new ExcelTable<TradeUnionList>(columns))
			{
				const int startRow = 1;
				var excelWorksheet = excel.CreateExcelWorksheet("Списки профсоюзов");

				excel.TableName = "Списки профсоюзов";
				excel.Parameters = new List<Tuple<string, string>>
					{
						new Tuple<string, string>("Год кампании:", search.YearOfRests?.FirstOrDefault(i => i.Id == search.YearOfRestId)?.Name),
						new Tuple<string, string>("Лагерь:", search.Camp?.Name),
						new Tuple<string, string>("Смена:", search.TimeOfRests?.FirstOrDefault(i => i.Id == search.TimeOfRestId)?.Name),
						new Tuple<string, string>("Статус:", search.States?.FirstOrDefault(i => i.Id == search.StateId)?.Name)
					}
					.Where(i => !String.IsNullOrWhiteSpace(i.Item2))
					.ToList();

				excel.DataBind(excelWorksheet, search.Result, ExcelBorderStyle.Thin, startRow);
				return excel.CreateFileExcel();
			}
		}
	}
}
