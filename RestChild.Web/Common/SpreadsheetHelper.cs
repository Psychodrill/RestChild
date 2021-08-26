using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using DocumentFormat.OpenXml.Spreadsheet;

namespace RestChild.Web.Common
{
	public static class SpreadsheetHelper
	{
		/// <summary>
		/// Получить или вставить ячейку в лист
		/// </summary>
		/// <param name="ws"></param>
		/// <param name="addressName"></param>
		/// <returns></returns>
		public static Cell GetOrInsertCellInWorksheet(Worksheet ws, string addressName)
		{
			SheetData sheetData = ws.GetFirstChild<SheetData>();
			Cell cell = null;

			UInt32 rowNumber = GetRowIndex(addressName);
			Row row = GetOrCreateRow(sheetData, rowNumber);

			Cell refCell = row.Elements<Cell>().FirstOrDefault(c => c.CellReference.Value == addressName);
			if (refCell != null)
			{
				cell = refCell;
			}
			else
			{
				cell = CreateCell(row, addressName);
			}
			return cell;
		}


		private static Cell CreateCell(Row row, String address)
		{
			Cell cellResult;
			Cell refCell = row.Elements<Cell>().FirstOrDefault(cell => String.Compare(cell.CellReference.Value, address, StringComparison.OrdinalIgnoreCase) > 0);

			cellResult = new Cell();
			cellResult.CellReference = address;

			row.InsertBefore(cellResult, refCell);
			return cellResult;
		}

		private static Row GetOrCreateRow(SheetData wsData, UInt32 rowIndex)
		{
			var row = wsData.Elements<Row>().FirstOrDefault(r => r.RowIndex.Value == rowIndex);
			if (row == null)
			{
				row = new Row { RowIndex = rowIndex };
				wsData.Append(row);
			}
			return row;
		}

		private static UInt32 GetRowIndex(string address)
		{
			UInt32 l;
			UInt32 result = 0;

			for (int i = 0; i < address.Length; i++)
			{
				if (UInt32.TryParse(address.Substring(i, 1), out l))
				{
					var rowPart = address.Substring(i, address.Length - i);
					if (UInt32.TryParse(rowPart, out l))
					{
						result = l;
						break;
					}
				}
			}
			return result;
		}
	}
}