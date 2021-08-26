using System;
using System.Collections.Generic;
using RestChild.Comon.Exchange.Zagz;

namespace RestChild.Comon.Extensions
{
    /// <summary>
    ///     расширение для работы со структурой ЗАГС
    /// </summary>
    public static class SmevZagzExtension
    {
        /// <summary>
        ///     получить Дату рождения родителя
        /// </summary>
        public static string GetParentBirthDate(this СведРодитТип entity)
        {
            if (entity == null)
            {
                return "-";
            }

            if (entity.Item is string item)
            {
                return item;
            }

            if (entity.Item is DateTime item1)
            {
                return item1.FormatEx();
            }

            return "-";
        }

        /// <summary>
        ///     получить Дату рождения родителя
        /// </summary>
        public static string GetChildBirthDate(this informResponseСведРегРождПрдСведРегСведРодившемся entity)
        {
            if (entity == null)
            {
                return "-";
            }

            if (entity.Item is string item)
            {
                return item;
            }

            if (entity.Item is DateTime item1)
            {
                return item1.FormatEx();
            }

            return "-";
        }


        /// <summary>
        ///     получить ФИО
        /// </summary>
        public static string GetFio(this ФИОПрТип entity)
        {
            if (entity == null)
            {
                return "-";
            }

            var res = new List<string>();

            if (entity.Item is string item)
            {
                res.Add(item);
            }

            if (entity.Item1 is string item1)
            {
                res.Add(item1);
            }

            if (entity.Item2 is string item2)
            {
                res.Add(item2);
            }

            return string.Join(" ", res);
        }
    }
}
