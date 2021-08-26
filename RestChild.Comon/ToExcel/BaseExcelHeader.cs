using System.Runtime.Serialization;
using OfficeOpenXml.Style;

namespace RestChild.Comon.ToExcel
{
    public abstract class BaseExcelHeader
    {
        [DataMember] public int Column { get; set; }

        [DataMember] public int? ColSpan { get; set; }

        [DataMember] public int? RowSpan { get; set; }

        [DataMember] public double Width { get; set; }

        [DataMember] public string Title { get; set; }

        [DataMember] public bool WordWrap { get; set; }

        [DataMember] public ExcelHorizontalAlignment? HorizontalAlignment { get; set; }

        [DataMember] public ExcelVerticalAlignment? VerticalAlignment { get; set; }

        [DataMember] public bool VerticalText { get; set; }

        [DataMember] public double? Height { get; set; }

        public abstract string GetTableHeadCellHtml();
    }
}
