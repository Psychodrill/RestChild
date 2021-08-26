using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace RestChild.Comon.ToExcel
{
    [DataContract]
    public class ExcelColumn<T> : BaseExcelColumn
    {
        public ExcelColumn()
        {
            Width = 40;
            WordWrap = false;
            Sortable = true;
        }

        public Func<T, object> Func { get; set; }

        public Func<T, IEnumerable<object>> FuncMulti { get; set; }

        public Func<T, object> Indent { get; set; }

        public Func<T, object> LinkFunc { get; set; }

        public Func<T, object> ColorFunc { get; set; }

        public Func<ExcelColumn<T>, string> GetClassesFunc { get; set; }

        public override object GetLinkId(BaseExcelRow row)
        {
            var r = row as ExcelRow<T>;
            if (r != null && LinkFunc != null)
            {
                return LinkFunc(r.Data);
            }

            return null;
        }

        public override object GetColor(BaseExcelRow row)
        {
            var r = row as ExcelRow<T>;
            if (r != null && ColorFunc != null)
            {
                return ColorFunc(r.Data);
            }

            return null;
        }

        public override object GetIndent(BaseExcelRow row)
        {
            var r = row as ExcelRow<T>;
            if (r != null && Indent != null)
            {
                return Indent(r.Data);
            }

            return null;
        }

        public override IEnumerable<object> GetValues(BaseExcelRow row)
        {
            var r = row as ExcelRow<T>;
            if (r != null && FuncMulti != null)
            {
                return FuncMulti(r.Data);
            }

            return null;
        }

        public override object GetValue(BaseExcelRow row)
        {
            var r = row as ExcelRow<T>;
            if (r != null && Func != null)
            {
                return Func(r.Data);
            }

            return null;
        }

        public override string GetClasses()
        {
            if (GetClassesFunc != null)
            {
                return GetClassesFunc(this);
            }

            return base.GetClasses();
        }
    }
}
