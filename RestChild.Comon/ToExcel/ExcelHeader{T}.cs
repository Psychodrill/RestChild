using System.Runtime.Serialization;

namespace RestChild.Comon.ToExcel
{
    [DataContract]
    public class ExcelHeader<T> : BaseExcelHeader
    {
        public ExcelHeader()
        {
            Width = 40;
            WordWrap = false;
        }

        /// <summary>
        ///     сортировать по
        /// </summary>
        [DataMember]
        public ExcelColumn<T> SortBy { get; set; }

        /// <summary>
        ///     для того что бы было в одном месте.
        /// </summary>
        public override string GetTableHeadCellHtml()
        {
            return
                $"<th rowspan=\"{RowSpan ?? 1}\" colspan=\"{ColSpan ?? 1}\" {(SortBy != null ? $"onclick=\"sortByColumn(this, '{SortBy.ColumnIndex}')\" class=\"sortable-column\"" : string.Empty)}>{Title}{(SortBy != null ? "<i class=\"icon-chevron-up\"></i>" : string.Empty)}</th>";
        }
    }
}
