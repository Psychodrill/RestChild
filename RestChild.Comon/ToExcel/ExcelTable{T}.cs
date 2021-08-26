using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace RestChild.Comon.ToExcel
{
    /// <summary>
    ///     выгрузка в Excel
    /// </summary>
    [DataContract]
    public class ExcelTable<T> : BaseExcelTable, IDisposable
    {
        public ExcelTable(List<ExcelColumn<T>> columns)
        {
            ColumnsInternal = columns;
        }

        public ExcelTable(List<ExcelColumn<T>> columns, IEnumerable<ExcelRow<T>> data)
        {
            ColumnsInternal = columns;
            DataInternal = data.ToList();
        }

        public ExcelTable(List<ExcelColumn<T>> columns, IEnumerable<T> data)
        {
            ColumnsInternal = columns;
            DataInternal = data.Select(t => new ExcelRow<T> {Data = t}).ToList();
        }

        public ExcelTable(List<ExcelColumn<T>> columns, List<List<ExcelHeader<T>>> headers, IEnumerable<T> data)
        {
            ColumnsInternal = columns;
            DataInternal = data.Select(t => new ExcelRow<T> {Data = t}).ToList();
            HeaderInternal = headers;
        }

        public ExcelTable(List<ExcelColumn<T>> columns, List<List<ExcelHeader<T>>> headers,
            IEnumerable<ExcelRow<T>> data)
        {
            ColumnsInternal = columns;
            DataInternal = data.ToList();
            HeaderInternal = headers;
        }

        public ExcelTable(ExcelPackage package, List<ExcelColumn<T>> columns)
        {
            Package = package;
            ColumnsInternal = columns;
        }

        public ExcelTable(ExcelPackage package, List<ExcelColumn<T>> columns, List<List<ExcelHeader<T>>> headers)
        {
            Package = package;
            ColumnsInternal = columns;
            HeaderInternal = headers;
        }

        public ExcelTable(List<ExcelColumn<T>> columns, List<List<ExcelHeader<T>>> headers)
        {
            ColumnsInternal = columns;
            HeaderInternal = headers;
        }

        public ExcelPackage Package { get; private set; }

        /// <summary>
        ///     заголовок.
        /// </summary>
        [DataMember]
        protected List<List<ExcelHeader<T>>> HeaderInternal { get; set; }

        public override List<List<BaseExcelHeader>> Header
        {
            get
            {
                return HeaderInternal?.ConvertAll(s => s.ConvertAll(x => (BaseExcelHeader) x)) ??
                       new List<List<BaseExcelHeader>>();
            }
        }

        [DataMember] public List<ExcelColumn<T>> ColumnsInternal { get; set; }

        [DataMember]
        public override List<BaseExcelColumn> Columns
        {
            get { return ColumnsInternal?.ConvertAll(x => (BaseExcelColumn) x); }
        }

        [DataMember] protected List<ExcelRow<T>> DataInternal { get; set; }

        [DataMember]
        public override List<BaseExcelRow> Data
        {
            get { return DataInternal?.ConvertAll(x => (BaseExcelRow) x); }
        }

        public void Dispose()
        {
            Package?.Dispose();
        }

        /// <summary>
        ///     установка данных
        /// </summary>
        /// <param name="data"></param>
        public void SetData(List<ExcelRow<T>> data)
        {
            DataInternal = data;
        }

        /// <summary>
        ///     сортировка по элементам.
        /// </summary>
        public void GenerateOrderNumber()
        {
            var list = DataInternal.ToList();
            DataInternal = list;
            var columns = ColumnsInternal.Where(c => c.Sortable).ToList();
            var columnInner = new ExcelColumn<T> {ColumnIndex = 0};

            for (var i = 0; i < list.Count; i++)
            {
                list[i].OrderKeys.Add(columnInner, i + 1);
            }

            var columnIndex = 1;

            foreach (var column in columns)
            {
                var column1 = column;
                column1.ColumnIndex = columnIndex;
                columnIndex++;
                var items = DataInternal.OrderBy(d => column1.Func(d.Data)).ToList();
                for (var i = 0; i < items.Count; i++)
                {
                    items[i].OrderKeys.Add(column1, i + 1);
                }
            }
        }

        public void DataBind()
        {
            DataBind(CreateExcelWorksheet("Данные"), ExcelBorderStyle.Thin, 1);
        }

        public override void DataBind(string worksheetName, ExcelBorderStyle borderStyle, int? startRow = null)
        {
            DataBind(CreateExcelWorksheet(worksheetName), borderStyle, startRow);
        }

        public void DataBind(string worksheetName, IEnumerable<T> data, ExcelBorderStyle borderStyle,
            int? startRow = null)
        {
            DataInternal = data.Select(t => new ExcelRow<T> {Data = t}).ToList();
            DataBind(CreateExcelWorksheet(worksheetName), borderStyle, startRow);
        }

        public void DataBind(string worksheetName, IEnumerable<ExcelRow<T>> data, ExcelBorderStyle borderStyle,
            int? startRow = null)
        {
            DataInternal = data.ToList();
            DataBind(CreateExcelWorksheet(worksheetName), borderStyle, startRow);
        }

        public void DataBind(ExcelWorksheet ws, IEnumerable<T> data, ExcelBorderStyle borderStyle, int? startRow = null)
        {
            DataInternal = data.Select(t => new ExcelRow<T> {Data = t}).ToList();
            DataBind(ws, borderStyle, startRow);
        }

        public int DataBind(ExcelWorksheet ws, ExcelBorderStyle borderStyle, int? startRow = null)
        {
            var column = 1;
            var row = startRow ?? 1;
            var columnsCount = Columns.Count;
            var rowCount = !ColumnsInternal.Any(c => c.MultiValueColumn)
                ? Data.Count
                : Data.Select(data => ColumnsInternal.Where(c => c.MultiValueColumn && c.FuncMulti != null)
                    .Select(c => c.GetValues(data)?.Count() ?? 0).Max()).Select(v => v < 1 ? 1 : v).Sum();

            if (Parameters != null)
            {
                if (!NotNeedDate)
                {
                    using (var range = ws.Cells[row, 1, row, columnsCount])
                    {
                        range.Merge = true;
                        range.Style.WrapText = true;
                        range.Value = $"Сформировано: {DateTime.Now:dd.MM.yyyy HH:mm}";
                        range.Style.Font.Bold = false;
                        range.Style.Font.Size = 11;
                        range.Style.Font.Color.SetColor(Color.LightGray);
                        range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        range.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    }

                    row++;
                }

                if (!string.IsNullOrEmpty(TableName))
                {
                    using (var range = ws.Cells[row, 1, row, columnsCount])
                    {
                        range.Merge = true;
                        range.Style.WrapText = true;
                        range.Value = TableName;
                        range.Style.Font.Bold = true;
                        range.Style.Font.Size = 16;
                        range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        range.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    }

                    row = row + 2;

                    foreach (var param in Parameters)
                    {
                        using (var range = ws.Cells[row, 1, row, 1 + ParameterNameColumnCount])
                        {
                            range.Style.WrapText = true;
                            range.Value = param.Item1;
                            range.Style.Font.Bold = false;
                            range.Style.Font.Size = 11;
                            range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                            range.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                            if (ParameterNameColumnCount > 0)
                            {
                                range.Merge = true;
                            }
                        }

                        using (var range = ws.Cells[row, 2 + ParameterNameColumnCount, row,
                            columnsCount < 2 + ParameterNameColumnCount ? 2 + ParameterNameColumnCount : columnsCount])
                        {
                            range.Merge = true;
                            range.Style.WrapText = true;
                            range.Value = param.Item2;
                            range.Style.Font.Bold = true;
                            range.Style.Font.Size = 11;
                            range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                            range.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        }

                        row++;
                    }
                }
            }

            row++;
            row = PrepareHeader(ws, borderStyle, row, column, rowCount);
            ws.View.FreezePanes(row, FixedColumns + 1);
            ws.Cells.Style.Fill.PatternType = ExcelFillStyle.Solid;
            ws.Cells.Style.Fill.BackgroundColor.SetColor(Color.White);
            foreach (var d in DataInternal)
            {
                column = 1;

                var rowCountInRow = !ColumnsInternal.Any(c => c.MultiValueColumn)
                    ? 1
                    : ColumnsInternal.Where(c => c.MultiValueColumn && c.FuncMulti != null)
                        .Select(c => c.FuncMulti(d.Data)?.Count() ?? 0).Max();

                if (rowCountInRow == 0)
                {
                    rowCountInRow = 1;
                }

                foreach (var excelColumn in ColumnsInternal)
                {
                    var color = excelColumn.GetColor(d);
                    if (!excelColumn.MultiValueColumn)
                    {
                        var val = excelColumn.Func(d.Data);
                        var indent = excelColumn.Indent != null ? excelColumn.Indent(d.Data) : null;
                        using (var range = ws.Cells[row, column, row + rowCountInRow - 1, column])
                        {
                            SetValue(range, val);

                            if (d.Bold)
                            {
                                range.Style.Font.Bold = true;
                            }

                            if (indent != null)
                            {
                                range.Style.Indent = Convert.ToInt32(indent);
                            }

                            range.Style.WrapText = true;

                            if (rowCountInRow > 1)
                            {
                                range.Merge = true;
                            }

                            if (color != null)
                            {
                                range.Style.Fill.BackgroundColor.SetColor((Color) color);
                            }
                        }
                    }
                    else
                    {
                        var values = excelColumn.FuncMulti(d.Data)?.ToArray() ?? new object[0];
                        var rowIndex = 0;
                        foreach (var val in values)
                        {
                            using (var range = ws.Cells[row + rowIndex, column])
                            {
                                SetValue(range, val);

                                if (d.Bold)
                                {
                                    range.Style.Font.Bold = true;
                                }

                                range.Style.WrapText = true;

                                if (color != null)
                                {
                                    range.Style.Fill.BackgroundColor.SetColor((Color) color);
                                }
                            }

                            rowIndex++;
                        }
                    }

                    column++;
                }

                if (d.Color.HasValue)
                {
                    ws.Cells[row, 1, row + rowCountInRow - 1, columnsCount].Style.Fill.PatternType =
                        ExcelFillStyle.Solid;
                    ws.Cells[row, 1, row + rowCountInRow - 1, columnsCount].Style.Fill.BackgroundColor
                        .SetColor(d.Color.Value);
                }

                if (Math.Abs(d.Height - BaseExcelRow.DefaultHeight) > 1)
                {
                    ws.Row(row).Height = d.Height;
                }

                row = row + rowCountInRow;
            }

            row = PrepareTotals(ws, borderStyle, row);

            return row;
        }

        /// <summary>
        ///     подготовка заголовка таблицы.
        /// </summary>
        private int PrepareHeader(ExcelWorksheet ws, ExcelBorderStyle borderStyle, int row, int column, int rowCount)
        {
            if (Header != null && Header.Any())
            {
                row = PrepareComplexHeader(ws, borderStyle, row, column, rowCount);
            }
            else
            {
                row = PrepareSimpleHeader(ws, borderStyle, row, column, rowCount);
            }

            return row;
        }

        /// <summary>
        ///     подготовка простого заголовка таблицы.
        /// </summary>
        private int PrepareTotals(ExcelWorksheet ws, ExcelBorderStyle borderStyle, int row)
        {
            if (TotalColumns == null || !TotalColumns.Any())
            {
                return row;
            }

            var column = 1;

            foreach (var excelColumn in TotalColumns)
            {
                using (var range = ws.Cells[row, column])
                {
                    SetValue(range, excelColumn.Value);
                    range.Style.Font.Bold = excelColumn.Bold;
                    range.Style.WrapText = excelColumn.WordWrap;
                    range.Style.Border.BorderAround(borderStyle);
                    range.Style.Border.Top.Style = borderStyle;
                    range.Style.Border.Bottom.Style = borderStyle;
                    range.Style.Border.Left.Style = borderStyle;
                    range.Style.Border.Right.Style = borderStyle;
                    range.Style.HorizontalAlignment = excelColumn.HorizontalAlignment;
                    range.Style.VerticalAlignment = excelColumn.VerticalAlignment;
                }

                column++;
            }

            row++;
            return row;
        }

        /// <summary>
        ///     подготовка простого заголовка таблицы.
        /// </summary>
        private int PrepareSimpleHeader(ExcelWorksheet ws, ExcelBorderStyle borderStyle, int row, int column,
            int rowCount)
        {
            foreach (var excelColumn in Columns)
            {
                ws.Column(column).Width = excelColumn.Width;
                using (var range = ws.Cells[row, column])
                {
                    range.Value = excelColumn.Title;
                    range.Style.Font.Bold = true;
                    range.Style.WrapText = true;
                    range.Style.Border.BorderAround(borderStyle);
                    range.Style.Border.Top.Style = borderStyle;
                    range.Style.Border.Bottom.Style = borderStyle;
                    range.Style.Border.Left.Style = borderStyle;
                    range.Style.Border.Right.Style = borderStyle;
                    range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                }

                using (var excelRange = ws.Cells[row + 1, column, row + (rowCount == 0 ? 1 : rowCount), column])
                {
                    excelRange.Style.Border.BorderAround(borderStyle);
                    excelRange.Style.Border.Top.Style = borderStyle;
                    excelRange.Style.Border.Bottom.Style = borderStyle;
                    excelRange.Style.Border.Left.Style = borderStyle;
                    excelRange.Style.Border.Right.Style = borderStyle;
                    excelRange.Style.WrapText = excelColumn.WordWrap;
                    excelRange.Style.HorizontalAlignment =
                        excelColumn.HorizontalAlignment ?? ExcelHorizontalAlignment.Left;
                    excelRange.Style.VerticalAlignment = excelColumn.VerticalAlignment ?? ExcelVerticalAlignment.Top;
                }

                column++;
            }

            row++;
            return row;
        }

        /// <summary>
        ///     подготовка группированного заголовка таблицы
        /// </summary>
        private int PrepareComplexHeader(ExcelWorksheet ws, ExcelBorderStyle borderStyle, int row, int column,
            int rowCount)
        {
            double? rowHeight = null;
            foreach (var rowHead in Header)
            {
                rowHeight = rowHead.Max(r => r.Height);
                foreach (var col in rowHead.Where(c => (c.ColSpan ?? 1) > 0).ToList())
                {
                    var excelRange = ws.Cells[row, col.Column, row + (col.RowSpan ?? 1) - 1,
                        col.Column + (col.ColSpan ?? 1) - 1];
                    if (col.RowSpan > 1 || col.ColSpan > 1)
                    {
                        excelRange.Merge = true;
                    }

                    excelRange.Value = col.Title;
                    excelRange.Style.Font.Bold = true;
                    excelRange.Style.Border.BorderAround(borderStyle);
                    excelRange.Style.Border.Top.Style = borderStyle;
                    excelRange.Style.Border.Bottom.Style = borderStyle;
                    excelRange.Style.Border.Left.Style = borderStyle;
                    excelRange.Style.Border.Right.Style = borderStyle;
                    excelRange.Style.WrapText = true;
                    excelRange.Style.HorizontalAlignment = col.HorizontalAlignment ?? ExcelHorizontalAlignment.Center;
                    excelRange.Style.VerticalAlignment = col.VerticalAlignment ?? ExcelVerticalAlignment.Center;
                    if (col.VerticalText)
                    {
                        excelRange.Style.TextRotation = 90;
                    }
                }

                if (rowHeight.HasValue)
                {
                    ws.Row(row).Height = rowHeight.Value;
                }

                row++;
            }

            if (rowCount != 0)
            {
                foreach (var excelColumn in Columns)
                {
                    ws.Column(column).Width = excelColumn.Width;
                    using (var excelRange = ws.Cells[row, column, row + rowCount - 1, column])
                    {
                        var excelStyle = excelRange.Style;
                        excelStyle.Border.BorderAround(borderStyle);
                        excelStyle.Border.Top.Style = borderStyle;
                        excelStyle.Border.Bottom.Style = borderStyle;
                        excelStyle.Border.Left.Style = borderStyle;
                        excelStyle.Border.Right.Style = borderStyle;
                        excelStyle.WrapText = excelColumn.WordWrap;
                        excelStyle.HorizontalAlignment =
                            excelColumn.HorizontalAlignment ?? ExcelHorizontalAlignment.Left;
                        excelStyle.VerticalAlignment = excelColumn.VerticalAlignment ?? ExcelVerticalAlignment.Bottom;
                    }

                    column++;
                }
            }

            return row;
        }

        public ExcelWorksheet CreateExcelWorksheet(string worksheetName)
        {
            if (Package == null)
            {
                Package = new ExcelPackage();
            }

            return Package.Workbook.Worksheets.Add(worksheetName);
        }

        public override string CreateFileExcel()
        {
            var tempFile = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            Package.SaveAs(new FileInfo(tempFile));
            return tempFile;
        }

        public override Stream CreateExcel()
        {
            var stream = new MemoryStream();
            Package.SaveAs(stream);
            stream.Seek(0, SeekOrigin.Begin);
            return stream;
        }
    }
}
