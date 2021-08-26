using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace RestChild.Comon.ToExcel
{
    [DataContract]
    public class ExcelRow<T> : BaseExcelRow
    {
        public ExcelRow()
        {
            Height = DefaultHeight;
            OrderKeys = new Dictionary<ExcelColumn<T>, int>();
        }

        /// <summary>
        ///     ключи для сортировки.
        /// </summary>
        public Dictionary<ExcelColumn<T>, int> OrderKeys { get; set; }

        [DataMember] public T Data { get; set; }

        public Func<ExcelRow<T>, string> GetClassesFunc { get; set; }

        public override string GetClasses()
        {
            var res = string.Empty;
            if (GetClassesFunc != null)
            {
                return GetClassesFunc(this);
            }

            return res;
        }

        /// <summary>
        ///     получение ключей для сортировки
        /// </summary>
        /// <returns></returns>
        public override string GetSortKeys()
        {
            var sb = new StringBuilder();
            foreach (var ok in OrderKeys)
            {
                sb.AppendFormat("g{0}='{1}' ", ok.Key.ColumnIndex.ToString(), ok.Value);
            }

            return sb.ToString();
        }
    }
}
