using System;
using System.Collections;
using System.Reflection;
using System.Text;

namespace MailingDemon.Common
{
	public class Helpers
	{
		public static bool IsNullOrEmpty(string s)
		{
			return string.IsNullOrEmpty(s);
		}

		public static string NullToEmpty(string s)
		{
			if (s != null)
			{
				return s;
			}
			return string.Empty;
		}

		public static bool Equals(byte[] a, byte[] b)
		{
			if (a == null || b == null)
			{
				return a == b;
			}
			long longLength = a.LongLength;
			if (longLength != b.LongLength)
			{
				return false;
			}
			for (long num = 0L; num < longLength; num += 1L)
			{
				if (checked(a[(int) ((IntPtr) num)] != b[(int) ((IntPtr) num)]))
				{
					return false;
				}
			}
			return true;
		}

		public static string EnumerableToString(IEnumerable enumerable)
		{
			var stringBuilder = new StringBuilder();
			foreach (object current in enumerable)
			{
				stringBuilder.AppendFormat("{0} ", current);
			}

			return stringBuilder.ToString();
		}

		public static int FindClassesWithAttributes(Assembly ass, ArrayList storage, params Type[] attributeTypes)
		{
			int num = 0;
			Type[] types = ass.GetTypes();
			foreach (Type type in types)
			{
				if (type.IsClass)
				{
					foreach (Type type2 in attributeTypes)
					{
						if (Attribute.IsDefined(type, type2))
						{
							storage.Add(type);
							num++;
						}
					}
				}
			}
			return num;
		}
	}
}