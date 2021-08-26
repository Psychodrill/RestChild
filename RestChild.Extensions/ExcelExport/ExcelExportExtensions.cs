using System;
using System.Collections.Generic;

namespace RestChild.Extensions.ExcelExport
{
    public static class ExcelExportExtensions
    {
        public static void TryAddParameter(this List<Tuple<string, string>> parameters, string name, string value)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                parameters.Add(new Tuple<string, string>(name, value));
            }
        }
    }
}
