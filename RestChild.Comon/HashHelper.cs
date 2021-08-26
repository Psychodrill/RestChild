// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HashHelper.cs" company="SDK Garant">
//   Copyright(C)2008-2011
// </copyright>
// <summary>
//   The hash helper.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Security.Cryptography;
using System.Text;

namespace RestChild.Comon
{
    /// <summary>The hash helper.</summary>
    public static class HashHelper
    {
        /// <summary>
        ///     Получает md5 хэш для строки
        /// </summary>
        /// <param name="input">
        ///     Строка, которую хешируем
        /// </param>
        /// <returns>
        ///     Хэш в виде байтового массива
        /// </returns>
        public static byte[] Md5Hash(string input)
        {
            var data = Encoding.Unicode.GetBytes(input);
            return Md5Hash(data);
        }

        /// <summary>
        ///     Получает md5 хэш для строки
        /// </summary>
        /// <param name="input">
        ///     Строка, которую хешируем
        /// </param>
        /// <returns>
        ///     Хэш в виде форматированной hex строки
        /// </returns>
        public static string Md5HashString(string input)
        {
            var data = Encoding.Unicode.GetBytes(input);
            return Md5HashString(data);
        }

        /// <summary>
        ///     Получает md5 хэш для строки
        /// </summary>
        /// <param name="input">
        ///     Данные, которые хешируем
        /// </param>
        /// <returns>
        ///     Хэш в виде байтового массива
        /// </returns>
        public static byte[] Md5Hash(byte[] input)
        {
            var hasher = MD5.Create();
            return hasher.ComputeHash(input);
        }

        /// <summary>
        ///     Получает md5 хэш набора байт
        /// </summary>
        /// <param name="input">
        ///     Данные, которые хешируем
        /// </param>
        /// <returns>
        ///     Хэш в виде форматированной hex строки
        /// </returns>
        public static string Md5HashString(byte[] input)
        {
            var hash = Md5Hash(input);
            var builder = new StringBuilder();

            for (var i = 0; i < hash.Length; i++)
            {
                builder.Append(hash[i].ToString("x2"));
            }

            return builder.ToString();
        }

        /// <summary>
        ///     Вычисление хэша по нескольким элементам
        /// </summary>
        /// <param name="items">Массив элементов</param>
        /// <returns>Идентификатор полученный через хэш</returns>
        public static Guid GetId(params object[] items)
        {
            var sb = new StringBuilder();
            foreach (var item in items)
            {
                sb.AppendFormat("_{0}_", item);
            }

            var data = Md5Hash(sb.ToString());
            return new Guid(data);
        }

        /// <summary>The get id by hash.</summary>
        /// <param name="text">Input text.</param>
        /// <returns>Hashed item</returns>
        public static Guid GetIdByHash(string text)
        {
            var data = Md5Hash(text);
            return new Guid(data);
        }
    }
}
