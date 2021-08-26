using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

using RestChild.Comon;

namespace RestChild.Web.Common
{
	public static class StaticHelpers
	{
		/// <summary>
		/// Генерация хеша для ребенка или сопровождающего
		/// </summary>
		public static string GenerateKey(
			string firstName,
			string lastName,
			string middleName,
			string documentSeria,
			string documentNumber)
		{
			var res = string.Format(
				"{0}|{1}|{2}|{3}|{4}",
				(lastName ?? string.Empty).Trim().ToLowerInvariant().Replace("ё", "е"),
				(firstName ?? string.Empty).Trim().ToLowerInvariant().FormatEx().Replace("ё", "е"),
				(middleName ?? string.Empty).Trim().ToLowerInvariant().FormatEx().Replace("ё", "е"),
				(documentSeria ?? string.Empty).Trim().ToLowerInvariant().FormatEx(),
				(documentNumber ?? string.Empty).Trim().ToLowerInvariant().FormatEx());

			if (res.Length > 400)
			{
				using (MD5 md5Hash = MD5.Create())
				{
					return string.Format(
						"{0}|{1}|{2}|{3}|---",
						(lastName ?? string.Empty).Trim().ToLowerInvariant().Replace("ё", "е"),
						(firstName ?? string.Empty).Trim().ToLowerInvariant().FormatEx().Replace("ё", "е"),
						(middleName ?? string.Empty).Trim().ToLowerInvariant().FormatEx().Replace("ё", "е"),
						Convert.ToBase64String(md5Hash.ComputeHash(Encoding.UTF8.GetBytes(res))));
				}
			}

			return res;
		}

		public static string GenerateKeySame(string firstName, string lastName, DateTime? birthDate)
		{
			return string.Format(
				"{0}|{1}|{2}",
				(lastName ?? string.Empty).ToLowerInvariant().FormatEx().Replace("ё", "е"),
				(firstName ?? string.Empty).ToLowerInvariant().FormatEx().Replace("ё", "е"),
				birthDate.FormatEx());
		}

		public static int? GetAgeInYears(DateTime? dateOfBirth, DateTime? today = null)
		{
			if (dateOfBirth.HasValue)
			{
				var todayi = today ?? DateTime.Today;
				var age = todayi.Year - dateOfBirth.Value.Year;
				if (dateOfBirth.Value > todayi.AddYears(-age)) age--;
				return age;
			}

			return null;
		}
	}
}
