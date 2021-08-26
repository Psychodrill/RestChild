using System.Collections.Generic;

namespace RestChild.Comon.ToExcel
{
    public class HtmlReportRow
    {
        /// <summary>
        ///     столбцы
        /// </summary>
        public IList<HtmlReportCell> Cells { get; set; }

        public string Class { get; set; }

        public string SortKeys { get; set; }

        public string LinkId { get; set; }
    }
}
