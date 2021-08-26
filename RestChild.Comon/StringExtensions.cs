using System;
using System.CodeDom.Compiler;
using System.Text;
using System.Text.RegularExpressions;

namespace RestChild.Comon
{
    /// <summary>
    ///     Класс расширений для строк
    /// </summary>
    public static class StringExtensions
    {
        private static readonly string[] OrgChars =
            "А,Б,В,Г,Д,Е,Ё,Ж,З,И,Й,К,Л,М,Н,О,П,Р,С,Т,У,Ф,Х,Ц,Ч,Ш,Щ,Ъ,Ы,Ь,Э,Ю,Я,а,б,в,г,д,е,ё,ж,з,и,й,к,л,м,н,о,п,р,с,т,у,ф,х,ц,ч,ш,щ,ъ,ы,ь,э,ю,я"
                .Split(',');

        private static readonly string[] TransChars =
            "A,B,V,G,D,E,E,Zh,Z,I,I,K,L,M,N,O,P,R,S,T,U,F,H,Ts,Ch,Sh,Sch,″,I,′,E,YU,Ya,a,b,v,g,d,e,e,zh,z,i,j,k,l,m,n,o,p,r,s,t,u,f,h,ts,ch,sh,sch,s,i,′,e,u,ya"
                .Split(',');

        public static string FormatSize(this int size)
        {
            return FormatSize((long) size);
        }

        public static string FormatSize(this long size)
        {
            var units = new[] {"B", "KB", "MB", "GB", "TB"};
            var index = 0;
            var tmp = size;
            while (tmp > 1024)
            {
                tmp /= 1024;
                index++;
            }

            return string.Format("{0:N} {1}", tmp, units[index]);
        }


        public static string UppercaseFirst(this string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }

            var a = s.ToCharArray();
            a[0] = char.ToUpper(a[0]);
            return new string(a);
        }

        /// <summary>
        ///     Cuts the specified <paramref name="prefix" /> from <paramref name="value" />.
        /// </summary>
        /// <param name="value">The original string value.</param>
        /// <param name="prefix">The prefix to cut.</param>
        /// <returns>
        ///     String without <paramref name="prefix" /> if it was found;
        ///     otherwise, original <paramref name="value" />.
        /// </returns>
        public static string TryCutPrefix(this string value, string prefix)
        {
            if (!value.StartsWith(prefix))
            {
                return value;
            }

            return value.Substring(prefix.Length);
        }

        /// <summary>
        ///     Cuts the specified <paramref name="prefix" /> from <paramref name="value" />.
        /// </summary>
        /// <param name="value">The original string value.</param>
        /// <param name="prefix">The prefix to cut.</param>
        /// <param name="isCut">
        ///     Upon return contains <see langword="true" />
        ///     if prefix was cut, otherwise <see langword="false" />
        /// </param>
        /// <returns>
        ///     String without <paramref name="prefix" /> if it was found;
        ///     otherwise, original <paramref name="value" />.
        /// </returns>
        public static string TryCutPrefix(this string value, string prefix, out bool isCut)
        {
            if (!value.StartsWith(prefix))
            {
                isCut = false;
                return value;
            }

            isCut = true;
            return value.Substring(prefix.Length);
        }

        /// <summary>
        ///     Метод-расширение стандартного класса String. Убирает последнее вхождение переданного паттерна
        /// </summary>
        /// <param name="s">Входящая строка</param>
        /// <param name="search">Паттерн поиска</param>
        /// <returns>Возвращается строка в которой будет убрано последнее вхождение переданного паттерна</returns>
        public static string TrimLastOccurrence(this string s, string search)
        {
            return string.IsNullOrEmpty(s)
                ? string.Empty
                : s.LastIndexOf(search, StringComparison.Ordinal) + search.Length != s.Length
                    ? s
                    : s.Remove(s.LastIndexOf(search, StringComparison.Ordinal));
        }

        /// <summary>
        ///     Проверяет объект на наличие значения
        /// </summary>
        /// <param name="o">любой объект</param>
        /// <returns>пусто / не пусто</returns>
        public static bool IsNullOrEmpty(this object o)
        {
            return string.IsNullOrEmpty(o.NullableToString());
        }

        /// <summary>
        ///     Проверяет, есть ли в объекте значимое значение
        /// </summary>
        /// <param name="o">любой объект</param>
        /// <returns>пусто / не пусто</returns>
        public static bool IsNullOrWhiteSpace(this object o)
        {
            return string.IsNullOrWhiteSpace(o.NullableToString());
        }

        /// <summary>
        ///     Приводит объекты к строке проверяя на null
        /// </summary>
        /// <param name="o">любой объект</param>
        /// <returns>приведенный объект или string.Empty, если объект был null</returns>
        public static string NullableToString(this object o)
        {
            return o == null ? string.Empty : o.ToString();
        }

        /// <summary>
        ///     Делает из любой строки, строку валидную для УРЛ и не только
        /// </summary>
        /// <param name="txt">
        ///     Текст на вход
        /// </param>
        /// <returns>
        ///     готовая строка
        /// </returns>
        public static string GenerateSlug(this string txt)
        {
            var str = txt.Translit(false).RemoveAccent().ToLower();

            str = Regex.Replace(str, @"[^a-z0-9\s-\.\+_]", string.Empty, RegexOptions.Compiled);
            str = Regex.Replace(str, @"\s+", "-", RegexOptions.Compiled);

            return str;
        }

        /// <summary>
        ///     Убирает все акценты и т.п.
        /// </summary>
        /// <param name="txt">
        ///     Текст, для преобразования
        /// </param>
        /// <returns>
        ///     ASCII строка
        /// </returns>
        public static string RemoveAccent(this string txt)
        {
            var bytes = Encoding.GetEncoding("Cyrillic").GetBytes(txt);
            return Encoding.ASCII.GetString(bytes);
        }

        /// <summary>
        ///     Translate string
        /// </summary>
        /// <param name="txt">строка для транслита</param>
        /// <returns>ASCII строка</returns>
        public static string Translit(this string txt)
        {
            return Translit(txt, false);
        }

        /// <summary>
        ///     Translate string
        /// </summary>
        /// <param name="txt">строка для транслита</param>
        /// <param name="reverse">Reverse En to RU</param>
        /// <returns>ASCII строка</returns>
        public static string Translit(this string txt, bool reverse)
        {
            if (string.IsNullOrWhiteSpace(txt))
            {
                return txt;
            }

            if (reverse)
            {
                for (var i = 0; i < TransChars.Length; i++)
                {
                    txt = txt.Replace(TransChars[i], OrgChars[i]);
                }
            }
            else
            {
                for (var i = 0; i < OrgChars.Length; i++)
                {
                    txt = txt.Replace(OrgChars[i], TransChars[i]);
                }
            }

            return txt;
        }

        /// <summary>
        ///     Replaces the format item in a specified string with the string representation of a corresponding object in a
        ///     specified array.
        /// </summary>
        /// <param name="format">A composite format string</param>
        /// <param name="arguments">An object array that contains zero or more objects to format.</param>
        /// <returns>
        ///     A copy of format in which the format items have been replaced by the string representation of the
        ///     corresponding objects in args.
        /// </returns>
        public static string FormatWith(this string format, params object[] arguments)
        {
            return string.Format(format, arguments);
        }

        /// <summary>
        ///     Проверяет является ли валидным сис именем указанная строка
        /// </summary>
        /// <param name="item">
        ///     Сьрока для проверки
        /// </param>
        /// <returns>
        ///     The is valid sys name.
        /// </returns>
        public static bool IsValidSysName(this string item)
        {
            return CodeGenerator.IsValidLanguageIndependentIdentifier(item) &&
                   !Regex.IsMatch(item, "[^0-9a-z]+", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        }

        /// <summary>
        ///     Получает строковый MD5 хэш для указанной строки
        /// </summary>
        /// <param name="str">
        ///     Строка для получения хеша
        /// </param>
        /// <returns>
        ///     Md5 хэш
        /// </returns>
        public static string ToMd5Hash(this string str)
        {
            return HashHelper.Md5HashString(str);
        }

        /// <summary>
        ///     Преобразование строки 16ричных чисел в массив байт
        /// </summary>
        /// <param name="hexString">строка для преобразования </param>
        /// <returns>Преобразованный массив</returns>
        public static byte[] HexToByte(string hexString)
        {
            var returnBytes = new byte[hexString.Length / 2];
            for (var i = 0; i < returnBytes.Length; i++)
            {
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            }

            return returnBytes;
        }
    }
}
