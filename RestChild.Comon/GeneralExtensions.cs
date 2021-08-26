using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml;

namespace RestChild.Comon
{
    public static class GeneralExtensions
    {
        /// <summary>
        ///     Получить доступ к свойству объекта, если объект нулл, то возвращает значение по умолчанию
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static TResult NullSafeOld<TType, TResult>(this TType obj, Func<TType, TResult> func)
        {
            if (obj == null)
            {
                return default;
            }

            return func(obj);
        }

        public static int? GetAgeInYears(this DateTime? dateOfBirth, DateTime? today = null)
        {
            if (dateOfBirth.HasValue)
            {
                var todayi = today ?? DateTime.Today;
                var age = todayi.Year - dateOfBirth.Value.Year;
                if (dateOfBirth.Value > todayi.AddYears(-age))
                {
                    age--;
                }

                return age;
            }

            return null;
        }

        public static int GetDiffInYears(this DateTime endDate, DateTime startDate)
        {
            return startDate.Year - endDate.Year - (startDate.DayOfYear < endDate.DayOfYear ? 1 : 0);
        }

        public static string NullEmpty(this string str, string def = "NULL")
        {
            if (str == def)
            {
                return string.Empty;
            }

            return str;
        }

        /// <summary>
        ///     попытка разобрать время
        /// </summary>
        public static DateTime? TryParseTimeHHmm(this string dateString)
        {
            if (string.IsNullOrEmpty(dateString))
            {
                return null;
            }

            if (DateTime.TryParseExact(dateString, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None,
                out var date))
            {
                return date;
            }

            return null;
        }

        public static DateTime? TryParseDateDdMmYyyy(this string dateString)
        {
            if (string.IsNullOrEmpty(dateString))
            {
                return null;
            }

            DateTime date;
            if (DateTime.TryParseExact(dateString, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None,
                out date))
            {
                return date;
            }

            return null;
        }

        public static DateTime? TryParseDateYyyyMmDd(this string dateString)
        {
            if (string.IsNullOrEmpty(dateString))
            {
                return null;
            }

            DateTime date;
            if (DateTime.TryParseExact(dateString, "yyyy.MM.dd", CultureInfo.InvariantCulture, DateTimeStyles.None,
                out date))
            {
                return date;
            }

            return null;
        }

        public static DateTime? TryParseDateDdMmYyyyHh(this string dateString)
        {
            if (string.IsNullOrEmpty(dateString))
            {
                return null;
            }

            DateTime date;
            if (DateTime.TryParseExact(dateString, "dd.MM.yyyy HH:mm:ss", CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out date))
            {
                return date;
            }

            if (DateTime.TryParseExact(dateString, "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out date))
            {
                return date;
            }

            if (DateTime.TryParseExact(dateString, "dd.MM.yyyy HH", CultureInfo.InvariantCulture, DateTimeStyles.None,
                out date))
            {
                return date;
            }

            if (DateTime.TryParseExact(dateString, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None,
                out date))
            {
                return date;
            }

            return null;
        }

        public static DateTime? TryParseDateYyyyMmDdHh(this string dateString)
        {
            if (string.IsNullOrEmpty(dateString))
            {
                return null;
            }

            DateTime date;
            if (DateTime.TryParseExact(dateString, "yyyy.MM.dd HH:mm:ss", CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out date))
            {
                return date;
            }

            if (DateTime.TryParseExact(dateString, "yyyy.MM.dd HH:mm", CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out date))
            {
                return date;
            }

            if (DateTime.TryParseExact(dateString, "yyyy.MM.dd HH", CultureInfo.InvariantCulture, DateTimeStyles.None,
                out date))
            {
                return date;
            }

            if (DateTime.TryParseExact(dateString, "yyyy.MM.dd", CultureInfo.InvariantCulture, DateTimeStyles.None,
                out date))
            {
                return date;
            }

            return null;
        }

        public static string ToDateYyyyMmDd(this DateTime date)
        {
            return date.ToString("yyyy.MM.dd");
        }

        public static string ToDateYyyyMmDdHh(this DateTime date)
        {
            return date.ToString("yyyy.MM.dd HH:mm");
        }

        public static string ToDateYyyyMmDd(this DateTime? date)
        {
            return date.HasValue ? date.Value.ToDateYyyyMmDd() : string.Empty;
        }

        public static string ToDateYyyyMmDdHh(this DateTime? date)
        {
            return date.HasValue ? date.Value.ToDateYyyyMmDdHh() : string.Empty;
        }

        public static TResult NullSafe<TType, TResult>(this TType obj, Expression<Func<TType, TResult>> expression)
        {
            if (obj == null)
            {
                return default;
            }

            try
            {
                var ev = new NullToDefaultVisitor(Expression.Default(expression.ReturnType));
                var ll = (Expression<Func<TType, TResult>>) ev.Visit(expression);
                Delegate cached = ll.Compile();
                return ((Func<TType, TResult>) cached)(obj);
            }
            catch
            {
                return default;
            }
        }

        public static void AppendLineP(this StringBuilder sb, string line)
        {
            sb?.AppendLine($"<p>{line}</p>");
        }

        public static void AppendLineDiv(this StringBuilder sb, string line)
        {
            sb?.AppendLine($"<div>{line}</div>");
        }

        public static void AppendLineLi(this StringBuilder sb, string line)
        {
            sb?.AppendLine($"<li>{line}</li>");
        }

        public static T ResetZeroFk<T>(this T target) where T : class
        {
            if (target == null)
            {
                return null;
            }

            var type = target.GetType();
            var properties = type.GetProperties().Where(prop => prop.IsDefined(typeof(ForeignKeyAttribute), false));

            foreach (var propertyInfo in properties)
            {
                var value = (long?) propertyInfo.GetValue(target);
                value = value.HasValue && value.Value != 0 ? value : null;
                propertyInfo.SetValue(target, value);
            }

            return target;
        }


        public static T ResetZeroFk<T>(this T target, Expression<Func<T, long?>> memberLamda) where T : class
        {
            var memberSelectorExpression = memberLamda.Body as MemberExpression;
            var property = memberSelectorExpression?.Member as PropertyInfo;
            if (property != null)
            {
                var value = (long?) property.GetValue(target);
                value = value.HasValue && value.Value != 0 ? value : null;
                property.SetValue(target, value);
            }

            return target;
        }

        public static DateTime? XmlToDateTime(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }

            try
            {
                if (value.Contains("+"))
                {
                    value = value.Substring(0, value.IndexOf("+", StringComparison.InvariantCulture));
                }

                return XmlConvert.ToDateTime(value, XmlDateTimeSerializationMode.Unspecified);
            }
            catch
            {
                return null;
            }
        }

        public static string XmlToString(this DateTime value)
        {
            try
            {
                return value.ToString("yyyy-MM-dd");
            }
            catch
            {
                return null;
            }
        }

        public static string XmlToString(this DateTime? value)
        {
            if (!value.HasValue)
            {
                return null;
            }

            try
            {
                return value.Value.ToString("yyyy-MM-dd");
            }
            catch
            {
                return null;
            }
        }

        public static string DateTimeToXml(this DateTime value)
        {
            try
            {
                return value.ToString("yyyy-MM-dd") + "T" + value.ToString("HH:mm:ss");
            }
            catch
            {
                return null;
            }
        }

        public static string DateTimeToXml(this DateTime? value)
        {
            if (!value.HasValue)
            {
                return null;
            }

            try
            {
                return value.Value.ToString("yyyy-MM-dd") + "T" + value.Value.ToString("HH:mm:ss");
            }
            catch
            {
                return null;
            }
        }

        public static string FormatEx(this bool? obj, string val = "Не определено", string trueStr = "Да",
            string falseStr = "Нет")
        {
            return obj.HasValue ? obj.Value ? trueStr : falseStr : val;
        }

        public static string FormatEx(this bool obj, string trueStr = "Да", string falseStr = "Нет")
        {
            return obj ? trueStr : falseStr;
        }

        public static string FormatExGender(this bool? obj, string val = "Не определено", string trueStr = "Мужской",
            string falseStr = "Женский")
        {
            return obj.HasValue ? obj.Value ? trueStr : falseStr : val;
        }

        public static string FormatExGender(this bool obj, string trueStr = "Мужской", string falseStr = "Женский")
        {
            return obj ? trueStr : falseStr;
        }

        public static string FormatEx(this DateTime obj, string format = "", string val = "-")
        {
            if (obj == DateTime.MinValue)
            {
                return val;
            }

            if (!string.IsNullOrEmpty(format))
            {
                return obj.ToString(format);
            }

            if (obj.Hour > 0 || obj.Minute > 0 || obj.Second > 0)
            {
                return obj.ToString("dd.MM.yyyy HH:mm");
            }

            return obj.ToString("dd.MM.yyyy");
        }

        public static string FormatEx(this object obj)
        {
            return obj.FormatEx("-", true);
        }

        public static string FormatEx(this object obj, string val)
        {
            return obj.FormatEx(val, true);
        }

        public static string FormatEx(this object obj, string val, bool needHtml)
        {
            if (obj == null)
            {
                return val;
            }

            if (obj is int)
            {
                return ((int) obj).FormatEx();
            }

            if (obj is decimal)
            {
                return ((decimal) obj).FormatEx(val);
            }

            if (obj is DateTime)
            {
                return ((DateTime) obj).FormatEx(string.Empty, val);
            }

            return obj.ToString().FormatEx(val, needHtml);
        }

        public static string FormatEx(this string obj)
        {
            return obj.FormatEx("-", true);
        }

        /// <summary>
        ///     форматировать СНИЛС
        /// </summary>
        public static string FormatSnils(this string obj)
        {
            if (string.IsNullOrWhiteSpace(obj) || obj.Length != 11)
            {
                return string.Empty;
            }

            return Regex.Replace(obj, @"(\d{3})(\d{3})(\d{3})(\d{2})", "$1-$2-$3 $4");
        }

        public static string FormatEx(this string obj, bool needHtml)
        {
            return obj.FormatEx("-", needHtml);
        }


        public static string FormatEx(this string obj, string val)
        {
            return obj.FormatEx(val, true);
        }

        public static string FormatEx(this string obj, string val, bool needHtml)
        {
            var s = obj ?? string.Empty;

            return needHtml ? HttpUtility.HtmlEncode(s).IfEmptyValue(val) : obj.IfEmptyValue(val);
        }

        public static bool? BoolParse(this string obj)
        {
            bool res;
            if (bool.TryParse(obj, out res))
            {
                return res;
            }

            return null;
        }

        public static long? LongParse(this string obj)
        {
            long res;
            if (long.TryParse(obj, out res))
            {
                return res;
            }

            return null;
        }

        public static int? IntParse(this string obj)
        {
            int res;
            if (int.TryParse(obj, out res))
            {
                return res;
            }

            return null;
        }

        public static int ToInt(this long? val)
        {
            if (val.HasValue)
            {
                try
                {
                    return Convert.ToInt32(val);
                }
                catch
                {
                    // ignored
                }
            }

            return default;
        }

        public static int ToInt(this long val)
        {
            try
            {
                return Convert.ToInt32(val);
            }
            catch
            {
                // ignored
            }

            return default;
        }


        public static decimal? DecimalParse(this string obj)
        {
            decimal res;
            if (decimal.TryParse(obj, NumberStyles.Number, CultureInfo.InvariantCulture, out res))
            {
                return res;
            }

            return null;
        }

        public static string FormatEx(this DateTime? obj, string format = "", string val = "-")
        {
            return obj.HasValue
                ? format.Contains("{") ? string.Format(format, obj) : obj.Value.FormatEx(format, val)
                : val;
        }

        public static string FormatExGR(this DateTime? obj, string format = "", string val = "-")
        {
            return obj.HasValue
                ? $"{(format.Contains("{") ? string.Format(format, obj) : obj.Value.FormatEx(format, val))} г.р."
                : val;
        }

        public static string FormatEx(this int obj)
        {
            return obj.ToString(CultureInfo.InvariantCulture);
        }

        public static string FormatEx(this int? obj, string val = "-")
        {
            return obj?.ToString(CultureInfo.InvariantCulture) ?? val;
        }


        public static string FormatEx(this decimal? obj, string format = "### ### ### ### ### ### ##0.00",
            string val = "-")
        {
            return obj.HasValue ? format.Contains("{") ? string.Format(format, obj) : obj.Value.ToString(format) : val;
        }

        public static string FormatEx(this decimal obj, string format = "### ### ### ### ### ### ##0.00")
        {
            return format.Contains("{") ? string.Format(format, obj) : obj.ToString(format);
        }

        public static string IfEmptyValue(this string obj, string val = "-")
        {
            return string.IsNullOrEmpty(obj) ? val : obj;
        }

        /// <summary>
        ///     получить обычный текст из html
        /// </summary>
        public static string StripHtml(this string htmlText)
        {
            if (string.IsNullOrWhiteSpace(htmlText))
            {
                return htmlText;
            }

            var reg = new Regex("<[^>]+>", RegexOptions.IgnoreCase);
            return reg.Replace(htmlText, "").Replace("&nbsp;", "");
        }

        public static TResult? NullSafeValue<TType, TResult>(this TType obj, Func<TType, TResult> func)
            where TResult : struct
            where TType : class
        {
            var result = obj == null ? (TResult?) null : func(obj);

            return result;
        }

        public static string GetDisplayValue(this object value)
        {
            var fieldInfo = value.GetType().GetField(value.ToString());

            var descriptionAttributes = fieldInfo.GetCustomAttributes(
                typeof(DisplayAttribute), false) as DisplayAttribute[];

            if (descriptionAttributes == null)
            {
                return string.Empty;
            }

            return descriptionAttributes.Length > 0 ? descriptionAttributes[0].Name : value.ToString();
        }

        public static Dictionary<TKey, string> GetDisplayAttributesByValue<TKey>(Type t)
        {
            var values = new Dictionary<TKey, string>();

            // ReSharper disable once LoopCanBeConvertedToQuery
            foreach (var fieldInfo in t.GetFields())
            {
                // ReSharper disable once AssignNullToNotNullAttribute
                values.Add((TKey) fieldInfo.GetValue(null), fieldInfo.GetCustomAttributesData()
                    .FirstOrDefault(i =>
                        i.AttributeType.Name == "DisplayAttribute")?
                    .NamedArguments
                    .FirstOrDefault(i => i.MemberName == "Name").TypedValue
                    .Value.ToString() ?? string.Empty);
            }

            return values;
        }
    }
}
