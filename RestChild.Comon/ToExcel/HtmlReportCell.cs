namespace RestChild.Comon.ToExcel
{
    public class HtmlReportCell
    {
        public string Content { get; set; }

        public string Class { get; set; }

        public string RowSpan { get; set; }

        public string ColSpan { get; set; }

        /// <summary>
        ///     Номер строки
        /// </summary>
        public int CellNumber { get; set; }

        public bool Skip { get; set; }

        public string LinkControllerName { get; set; }
        public string LinkMethodName { get; set; }
        public string LinkId { get; set; }
    }
}
