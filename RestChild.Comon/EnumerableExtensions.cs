using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace RestChild.Comon
{
    /// <summary>
    ///     Класс расширений для Enumerable
    /// </summary>
    public static class EnumerableExtensions
    {
        public static bool In<T>(this T source, params T[] list)
        {
            return list.Contains(source);
        }

        /// <summary>
        ///     FullOuterJoin
        /// </summary>
        /// <param name="outer">
        ///     outer
        /// </param>
        /// <param name="inner">
        ///     inner
        /// </param>
        /// <param name="outerKeySelector">
        ///     outerKeySelector
        /// </param>
        /// <param name="innerKeySelector">
        ///     innerKeySelector
        /// </param>
        /// <param name="resultSelector">
        ///     resultSelector
        /// </param>
        /// <typeparam name="TOuter">
        ///     TOuter
        /// </typeparam>
        /// <typeparam name="TInner">
        ///     TInner
        /// </typeparam>
        /// <typeparam name="TKey">
        ///     TKey
        /// </typeparam>
        /// <typeparam name="TResult">
        ///     TResult
        /// </typeparam>
        /// <returns>
        ///     FullOuterJoinTResult
        /// </returns>
        public static IEnumerable<TResult> FullOuterJoin<TOuter, TInner, TKey, TResult>(this IEnumerable<TOuter> outer,
            IEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector,
            Func<TOuter, TInner, TResult> resultSelector)
            where TInner : class
            where TOuter : class
        {
            var innerLookup = inner.ToLookup(innerKeySelector);
            var outerLookup = outer.ToLookup(outerKeySelector);

            var innerJoinItems = inner
                .Where(innerItem => !outerLookup.Contains(innerKeySelector(innerItem)))
                .Select(innerItem => resultSelector(null, innerItem));

            return outer
                .SelectMany(outerItem =>
                    {
                        var innerItems = innerLookup[outerKeySelector(outerItem)];

                        return innerItems.Any() ? innerItems : new TInner[] {null};
                    },
                    resultSelector)
                .Concat(innerJoinItems);
        }

        /// <summary>
        ///     To the name value collection.
        /// </summary>
        /// <param name="dictionary">
        ///     The dictionary.
        /// </param>
        /// <typeparam name="TValue">
        ///     Тип значения
        /// </typeparam>
        /// <returns>
        ///     Name Value Collection
        /// </returns>
        public static NameValueCollection ToNameValueCollection<TValue>(this Dictionary<string, TValue> dictionary)
        {
            var collection = new NameValueCollection();
            foreach (var d in dictionary)
            {
                collection.Add(d.Key, d.Value.NullableToString());
            }

            return collection;
        }

        /// <summary>
        ///     Преобразование перечисления в упорядоченный список/словарь
        /// </summary>
        /// <typeparam name="TKey">
        ///     Тип ключа
        /// </typeparam>
        /// <typeparam name="TValue">
        ///     Тип значения
        /// </typeparam>
        /// <param name="enumerable">
        ///     Перечисление источник
        /// </param>
        /// <param name="keyFunc">
        ///     Функция выбора ключа
        /// </param>
        /// <param name="order">
        ///     Функция упорядочивания
        /// </param>
        /// <returns>
        ///     Упорядоченный список/словарь
        /// </returns>
        public static SortedList<TKey, TValue> ToSortedList<TKey, TValue>(this IEnumerable<TValue> enumerable,
            Func<TValue, TKey> keyFunc, Func<TValue, TKey> order)
        {
            var ordered = enumerable.OrderBy(order);
            var result = new SortedList<TKey, TValue>();
            foreach (var value in ordered)
            {
                result.Add(keyFunc(value), value);
            }

            return result;
        }

        /// <summary>
        ///     Выполняет указанный экшен на элементах перечисления
        /// </summary>
        /// <param name="list">
        ///     Перечисление элементов
        /// </param>
        /// <param name="func">
        ///     Метод, который выполнять
        /// </param>
        /// <typeparam name="T">
        ///     Тип в перечислении
        /// </typeparam>
        public static void Each<T>(this IEnumerable<T> list, Action<T> func)
        {
            if (func == null || list == null)
            {
                return;
            }

            foreach (var elem in list)
            {
                func(elem);
            }
        }

        /// <summary>
        ///     Выполняет указанный экшен на элементах перечисления
        /// </summary>
        /// <param name="list">
        ///     Перечисление элементов
        /// </param>
        /// <param name="func">
        ///     Метод, который выполнять
        /// </param>
        /// <typeparam name="T">
        ///     Тип в перечислении
        /// </typeparam>
        public static void Each<T>(this IEnumerable<T> list, Action<T, int> func)
        {
            if (func == null || list == null)
            {
                return;
            }

            var i = 0;
            foreach (var elem in list)
            {
                func(elem, i);
                i++;
            }
        }

        /// <summary>
        ///     Обновляет значения в словаре используя данные из аргументов
        /// </summary>
        /// <param name="original">
        ///     Старый словарь
        /// </param>
        /// <param name="update">
        ///     Словарь с новыми значениями
        /// </param>
        /// <typeparam name="TKey">
        ///     Тип ключа в словаре
        /// </typeparam>
        /// <typeparam name="TValue">
        ///     Тип значения в словаре
        /// </typeparam>
        /// <returns>
        ///     Обновленный словарь
        /// </returns>
        public static IDictionary<TKey, TValue> Merge<TKey, TValue>(this IDictionary<TKey, TValue> original,
            IDictionary<TKey, TValue> update)
        {
            var newDict = new Dictionary<TKey, TValue>(original);
            foreach (var pair in update)
            {
                newDict[pair.Key] = pair.Value;
            }

            return newDict;
        }

        /// <summary>
        ///     Соединение перечисления в строку
        /// </summary>
        /// <param name="input">
        ///     Перечисление элементов
        /// </param>
        /// <param name="concatinator">
        ///     Строка используемая для соединения
        /// </param>
        /// <returns>
        ///     Соединенная строка
        /// </returns>
        public static string ToString(this NameValueCollection input, string concatinator)
        {
            var res = new List<string>(input.Count);

            foreach (var key in input.AllKeys)
            {
                res.Add(string.Format("Key: {0}, Value: {1}", key, input[key]));
            }

            return res.ToString(concatinator);
        }

        /// <summary>
        ///     Соединение перечисления в строку
        /// </summary>
        /// <param name="enumer">
        ///     Перечисление элементов
        /// </param>
        /// <param name="concatinator">
        ///     Строка используемая для соединения
        /// </param>
        /// <returns>
        ///     Соединенная строка
        /// </returns>
        public static string ToString(this IEnumerator enumer, string concatinator)
        {
            if (enumer == null)
            {
                return null;
            }

            return enumer.ToString<object>(concatinator, f => f.ToString());
        }

        public static string ToString<TEntry>(this IEnumerator enumer, string concatinator,
            Func<TEntry, string> selector)
        {
            if (enumer == null)
            {
                return null;
            }

            var sb = new StringBuilder();

            var moved = enumer.MoveNext();
            if (moved)
            {
                if (enumer.Current != null)
                {
                    sb.Append(selector((TEntry) enumer.Current));
                }

                while (moved)
                {
                    moved = enumer.MoveNext();
                    if (moved && enumer.Current != null)
                    {
                        sb.Append(concatinator);
                        sb.Append(selector((TEntry) enumer.Current));
                    }
                }
            }

            return sb.ToString();
        }

        /// <summary>
        ///     Соединение перечисления в строку
        /// </summary>
        /// <param name="input">
        ///     Перечисление элементов
        /// </param>
        /// <param name="concatinator">
        ///     Строка используемая для соединения
        /// </param>
        /// <returns>
        ///     Соединенная строка
        /// </returns>
        public static string ToString(this IEnumerable input, string concatinator)
        {
            if (input == null)
            {
                return null;
            }

            return input.Cast<object>().ToString(concatinator, f => f.ToString());
        }

        /// <summary>
        ///     Соединение перечисления в строку
        /// </summary>
        /// <typeparam name="T">
        ///     Тип перечисляемых элементов
        /// </typeparam>
        /// <param name="input">
        ///     Перечисление элементов
        /// </param>
        /// <param name="concatinator">
        ///     Строка используемая для соединения
        /// </param>
        /// <param name="func">
        ///     Преобразование элемента перечисления в строку
        /// </param>
        /// <returns>
        ///     Соединенная строка
        /// </returns>
        public static string ToString<T>(this IEnumerable<T> input, string concatinator, Func<T, string> func)
        {
            if (input == null)
            {
                return null;
            }

            var sb = new StringBuilder();

            var enumer = input.GetEnumerator();
            var moved = enumer.MoveNext();
            if (moved)
            {
                sb.Append(func(enumer.Current));

                while (moved)
                {
                    moved = enumer.MoveNext();
                    if (moved)
                    {
                        sb.Append(concatinator);
                        sb.Append(func(enumer.Current));
                    }
                }
            }

            return sb.ToString();
        }

        /// <summary>
        ///     Get the array slice between the two indexes.
        /// </summary>
        /// <typeparam name="T">
        ///     Тип текущего перечисления
        /// </typeparam>
        /// <param name="source">
        ///     The source.
        /// </param>
        /// <param name="start">
        ///     The start.
        /// </param>
        /// <param name="end">
        ///     The end.
        /// </param>
        /// <returns>
        ///     The slice.
        /// </returns>
        public static T[] Slice<T>(this T[] source, int start, int end)
        {
            // Handles negative ends.
            if (end < 0)
            {
                end = source.Length + end - 1;
            }

            var len = end - start + 1;

            var res = new T[len];
            for (var i = 0; i < len; i++)
            {
                res[i] = source[i + start];
            }

            return res;
        }

        /// <summary>
        ///     Converts the sequence to the <see cref="T:System.Collections.Generic.HashSet`1" />.
        /// </summary>
        /// <typeparam name="T">
        ///     The type of sequence item.
        /// </typeparam>
        /// <param name="source">
        ///     The sequence to convert.
        /// </param>
        /// <returns>
        ///     A new <see cref="T:System.Collections.Generic.HashSet`1" /> instance containing
        ///     all the unique items from the <paramref name="source" /> sequence.
        /// </returns>
        public static HashSet<T> ToHashSet<T>(this IEnumerable<T> source)
        {
            return new HashSet<T>(source);
        }

        /// <summary>
        ///     Сгрупированные элементы
        /// </summary>
        /// <typeparam name="TElement">
        ///     Element
        /// </typeparam>
        /// <typeparam name="TKey">
        ///     Key
        /// </typeparam>
        /// <param name="source">
        ///     The source.
        /// </param>
        /// <param name="getKey">
        ///     The get Key.
        /// </param>
        /// <returns>
        ///     Словарь сгрупированных элементов
        /// </returns>
        public static Dictionary<TKey, List<TElement>> GroupedDictionary<TElement, TKey>(
            this IEnumerable<TElement> source,
            Func<TElement, TKey> getKey)
        {
            return GroupedDictionary(source, getKey, f => f);
        }

        /// <summary>
        ///     Сгрупированные элементы
        /// </summary>
        /// <typeparam name="TElement">
        ///     Element
        /// </typeparam>
        /// <typeparam name="TKey">
        ///     Key
        /// </typeparam>
        /// <typeparam name="TResult">Result</typeparam>
        /// <param name="source">
        ///     The source.
        /// </param>
        /// <param name="getKey">
        ///     The get Key.
        /// </param>
        /// <param name="resSelector"></param>
        /// <returns>
        ///     Словарь сгрупированных элементов
        /// </returns>
        public static Dictionary<TKey, List<TResult>> GroupedDictionary<TElement, TKey, TResult>(
            this IEnumerable<TElement> source, Func<TElement, TKey> getKey, Func<TElement, TResult> resSelector)
        {
            var returnDict = new Dictionary<TKey, List<TResult>>();

            foreach (var o in source)
            {
                var key = getKey(o);

                if (!returnDict.ContainsKey(key))
                {
                    returnDict.Add(key, new List<TResult>());
                }

                returnDict[key].Add(resSelector(o));
            }

            return returnDict;
        }


        public static IList<T> InsertAt<T>(this IEnumerable<T> source, T element, int index = 0)
        {
            var v = source.ToList();
            v.Insert(0, element);
            return v;
        }

        public static void Merge<T>(this IList<T> source, IList<T> target) where T : class, IEntityBase
        {
            var ids = target.Select(t => t.Id).ToList();
            var idsExists = source.Select(s => s.Id).ToList();
            var forRemove = source.Where(r => !ids.Contains(r.Id)).ToList();
            foreach (var item in forRemove)
            {
                source.Remove(item);
            }

            var forAdd = target.Where(r => !idsExists.Contains(r.Id)).ToList();
            foreach (var item in forAdd)
            {
                source.Add(item);
            }
        }

        public static void AddRange<T>(this ICollection<T> source, ICollection<T> addon)
        {
            foreach (var item in addon)
            {
                source.Add(item);
            }
        }

        /// <summary>
        ///     Проверяет объект на наличие значения
        /// </summary>
        /// <param name="enumerable">любой объект</param>
        /// <returns>пусто / не пусто</returns>
        public static bool NotNullOrEmpty<T>(this IEnumerable<T> enumerable)
        {
            return enumerable != null && enumerable.Any();
        }
    }
}
