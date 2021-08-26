using System.Collections.Generic;
using System.Runtime.Serialization;
using OfficeOpenXml.Style;

namespace RestChild.Comon.ToExcel
{
    /// <summary>
    ///     базовая колонка
    /// </summary>
    public abstract class BaseExcelColumn
    {
        [DataMember] public bool Sortable { get; set; }

        /// <summary>
        ///     код колонки.
        /// </summary>
        [DataMember]
        public int? ColumnIndex { get; set; }

        [DataMember] public double Width { get; set; }

        [DataMember] public string Title { get; set; }

        [DataMember] public bool WordWrap { get; set; }

        [DataMember] public ExcelHorizontalAlignment? HorizontalAlignment { get; set; }

        [DataMember] public ExcelVerticalAlignment? VerticalAlignment { get; set; }

        [DataMember] public string LinkControllerName { get; set; }

        [DataMember] public string LinkMethodName { get; set; }

        [DataMember] public bool MultiValueColumn { get; set; }

        public abstract object GetValue(BaseExcelRow row);

        public abstract object GetIndent(BaseExcelRow row);

        public abstract object GetLinkId(BaseExcelRow row);

        public abstract object GetColor(BaseExcelRow row);

        public abstract IEnumerable<object> GetValues(BaseExcelRow row);

        public virtual string GetClasses()
        {
            var res = string.Empty;

            if (HorizontalAlignment.HasValue)
            {
                if (HorizontalAlignment.Value == ExcelHorizontalAlignment.Left)
                {
                    res += " td-ha-left";
                }

                if (HorizontalAlignment.Value == ExcelHorizontalAlignment.Right)
                {
                    res += " td-ha-right";
                }

                if (HorizontalAlignment.Value == ExcelHorizontalAlignment.Center)
                {
                    res += " td-ha-center";
                }
            }

            if (VerticalAlignment.HasValue)
            {
                if (VerticalAlignment.Value == ExcelVerticalAlignment.Bottom)
                {
                    res += " td-va-bottom";
                }

                if (VerticalAlignment.Value == ExcelVerticalAlignment.Top)
                {
                    res += " td-va-top";
                }

                if (VerticalAlignment.Value == ExcelVerticalAlignment.Center)
                {
                    res += " td-va-center";
                }
            }

            return res;
        }
    }
}
