using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace RestChild.Comon.ToExcel
{
    [DataContract]
    public abstract class BaseExcelTable
    {
        public int FixedColumns { get; set; }

        public string TableName { get; set; }

        public bool NotNeedDate { get; set; }

        public int ParameterNameColumnCount { get; set; }

        /// <summary>
        ///     параметры отчета для отображения.
        /// </summary>
        [DataMember]
        public List<Tuple<string, string>> Parameters { get; set; }

        /// <summary>
        ///     Итого
        /// </summary>
        public virtual List<BaseExcelTotalColumn> TotalColumns { get; set; }

        /// <summary>
        ///     колонки
        /// </summary>
        public virtual List<BaseExcelColumn> Columns { get; set; }

        /// <summary>
        ///     заголовок.
        /// </summary>
        [DataMember]
        public virtual List<List<BaseExcelHeader>> Header { get; set; }

        [DataMember] public virtual List<BaseExcelRow> Data { get; set; }

        /// <summary>
        ///     Стороки для отчета
        /// </summary>
        public List<HtmlReportRow> Rows { get; set; }

        public abstract string CreateFileExcel();
        public abstract Stream CreateExcel();

        public abstract void DataBind(string worksheetName, ExcelBorderStyle borderStyle, int? startRow = null);

        /// <summary>
        ///     сформировать данные для Html отчета
        /// </summary>
        public virtual void BuildRowsForHtml()
        {
            Rows = new List<HtmlReportRow>();
            var columnsCount = Columns.Count;

            foreach (var data in Data)
            {
                var rowCountInRow = !Columns.Any(c => c.MultiValueColumn)
                    ? 1
                    : Columns.Where(c => c.MultiValueColumn)
                        .Select(c => c.GetValues(data)?.Count() ?? 0).Max();

                if (rowCountInRow == 0)
                {
                    rowCountInRow = 1;
                }

                var rows = Enumerable.Repeat(1, rowCountInRow).Select(i => new HtmlReportRow
                {
                    Cells = Enumerable.Range(1, columnsCount)
                        .Select(j => new HtmlReportCell {CellNumber = j, Skip = true})
                        .ToList(),
                    Class = data.GetClasses(),
                    SortKeys = data.GetSortKeys()
                }).ToList();

                for (var colNumber = 0; colNumber < Columns.Count; colNumber++)
                {
                    var column = Columns[colNumber];
                    var cell = rows[0].Cells[colNumber];
                    var indent = column.GetIndent(data);
                    cell.Skip = false;

                    cell.Class = column.GetClasses() + (data.Bold ? " bold-text" : string.Empty) +
                                 (indent != null && Convert.ToInt32(indent) != 0 ? " indent-left" : string.Empty);
                    cell.LinkId = column.GetLinkId(data).FormatEx(string.Empty);
                    cell.LinkControllerName = column.LinkControllerName;
                    cell.LinkMethodName = column.LinkMethodName;
                    if (!column.MultiValueColumn)
                    {
                        cell.Content = column.GetValue(data).FormatEx(string.Empty);
                        cell.RowSpan = rowCountInRow != 1 ? $"rowspan='{rowCountInRow}'" : string.Empty;
                    }
                    else
                    {
                        var values = column.GetValues(data)?.ToArray() ?? new object[0];
                        var countValues = values.Length;

                        if (countValues > rowCountInRow)
                        {
                            cell.RowSpan = rowCountInRow != 1 ? $"rowspan='{rowCountInRow}'" : string.Empty;
                        }
                        else
                        {
                            var indexRow = 0;
                            foreach (var value in values)
                            {
                                var indexedCell = rows[indexRow].Cells[colNumber];
                                indexedCell.Content = value.FormatEx();
                                indexedCell.Skip = false;
                                indexedCell.Class = cell.Class;
                                indexedCell.LinkControllerName = cell.LinkControllerName;
                                indexedCell.LinkMethodName = cell.LinkMethodName;
                                indexedCell.LinkId = cell.LinkId;
                                indexRow++;
                            }

                            var lastCell = rows[indexRow > 0 ? indexRow - 1 : 0].Cells[colNumber];
                            lastCell.RowSpan = indexRow != 0 && rowCountInRow - indexRow + 1 != 1
                                ? $"rowspan='{rowCountInRow - indexRow + 1}'"
                                : string.Empty;
                        }
                    }
                }

                Rows.AddRange(rows);
            }
        }

        /// <summary>
        ///     проставить значение в ячейку
        /// </summary>
        public static void SetValue(ExcelRange range, object val)
        {
            range.Value = val;
            if (val is DateTime)
            {
                var date = (DateTime) val;
                range.Style.Numberformat.Format =
                    "dd.MM.yyyy" + (date.Hour > 0 || date.Minute > 0 ? " HH:mm" : string.Empty);
            }
            else if (val is int || val is long || val is short)
            {
                range.Style.Numberformat.Format = "#,##0";
            }
            else if (val is decimal || val is float)
            {
                range.Style.Numberformat.Format = "#,##0.00";
            }
            else
            {
                range.Value = val.FormatEx(string.Empty, false);
                range.Style.WrapText = true;
            }
        }
    }
}
