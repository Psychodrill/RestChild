using System.Runtime.Serialization;
using OfficeOpenXml.Style;

namespace RestChild.Comon.ToExcel
{
    /// <summary>
    ///     итого
    /// </summary>
    public class BaseExcelTotalColumn
    {
        public BaseExcelTotalColumn()
        {
            HorizontalAlignment = ExcelHorizontalAlignment.Left;
            VerticalAlignment = ExcelVerticalAlignment.Center;
            Bold = true;
        }


        [DataMember] public bool Sortable { get; set; }

        /// <summary>
        ///     код колонки.
        /// </summary>
        [DataMember]
        public int? ColumnIndex { get; set; }

        [DataMember] public double Width { get; set; }

        [DataMember] public string Title { get; set; }

        [DataMember] public bool WordWrap { get; set; }

        [DataMember] public bool Bold { get; set; }

        [DataMember] public ExcelHorizontalAlignment HorizontalAlignment { get; set; }

        [DataMember] public ExcelVerticalAlignment VerticalAlignment { get; set; }

        [DataMember] public virtual object Value { get; set; }

        public virtual string GetClasses()
        {
            var res = string.Empty;

            if (HorizontalAlignment == ExcelHorizontalAlignment.Left)
            {
                res += " td-ha-left";
            }

            if (HorizontalAlignment == ExcelHorizontalAlignment.Right)
            {
                res += " td-ha-right";
            }

            if (HorizontalAlignment == ExcelHorizontalAlignment.Center)
            {
                res += " td-ha-center";
            }

            if (VerticalAlignment == ExcelVerticalAlignment.Bottom)
            {
                res += " td-va-bottom";
            }

            if (VerticalAlignment == ExcelVerticalAlignment.Top)
            {
                res += " td-va-top";
            }

            if (VerticalAlignment == ExcelVerticalAlignment.Center)
            {
                res += " td-va-center";
            }

            return res;
        }
    }
}
