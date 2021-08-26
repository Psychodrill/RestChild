using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestChild.Comon;

namespace RestChild.Booking.Logic.Extensions
{
	public static class CompareExtension
	{
		/// <summary>
		/// сравнение значений
		/// </summary>
		public static void Compare<T, TV>(this StringBuilder sb, T source, T target, string text, Func<T, TV> valFunc)
		{
			sb.Compare(source, target, text, valFunc, (arg => valFunc(arg)?.ToString()?.StripHtml().Trim()));
		}

		/// <summary>
		/// сравнение значений
		/// </summary>
		public static void Compare<T, TV>(this StringBuilder sb, T source, T target, string text, Func<T, TV> valFunc, Func<T, string> getVal)
		{
			var vs = valFunc(source);
			var vt = valFunc(target);
			if ((vs == null && vt != null) || (vs != null && vt == null) || (vs != null && !vs.Equals(vt)))
			{
				sb?.AppendLine($"<li>{text} старое значение: '{getVal(source)?.Trim()}', новое значение: '{getVal(target)?.Trim()}'</li>");
			}
		}

		/// <summary>
		/// получение разницы при сохранении услуг
		/// </summary>
		public static string Compare<T>(this ICollection<T> source, ICollection<T> target, Func<T, IUnitOfWork, string> nameFunc, Func<T, T, IUnitOfWork, string> diffFunc, IUnitOfWork uw) where T : IEntityBase
		{
			var res = new StringBuilder();

			var ids = target.Where(s => s.Id > 0).Select(s => s.Id).ToList();

			var serviceForDelete = source.Where(s => !ids.Contains(s.Id)).ToList();
			foreach (var sForDelete in serviceForDelete)
			{
				var name = nameFunc(sForDelete, uw);
				if (!string.IsNullOrWhiteSpace(name))
				{
					res.AppendLine($"<li>{name} - удалена</li>");
				}
			}

			foreach (var service in target)
			{
				var currentService = source.FirstOrDefault(s => s.Id == service.Id);
				if (currentService != null)
				{
					if (diffFunc != null)
					{
						var diff = diffFunc(currentService, service, uw);
						var name = nameFunc(currentService, uw);
						if (!string.IsNullOrWhiteSpace(diff) && !string.IsNullOrWhiteSpace(name))
						{
							res.AppendLine($"<li>{name} - изменена <ul>{diff}</ul></li>");
						}
					}
				}
				else
				{
					var name = nameFunc(service, uw);
					if (!string.IsNullOrWhiteSpace(name))
					{
						res.AppendLine($"<li>{name} - добавлена</li>");
					}
				}
			}

			return res.ToString();
		}

		public static string Compare<T, TV>(this ICollection<T> source, ICollection<T> target, Func<T, TV> cmpValFunc, Func<T, IUnitOfWork, string> nameFunc, Func<T, T, IUnitOfWork, string> diffFunc, IUnitOfWork uw) where T : IEntityBase
		{
			var res = new StringBuilder();

			var vals = target.Select(s => cmpValFunc(s)).ToList();

			var serviceForDelete = source.Where(s => !vals.Any(i => i.Equals(cmpValFunc(s)))).ToList();
			foreach (var sForDelete in serviceForDelete)
			{
				var name = nameFunc(sForDelete, uw);
				if (!string.IsNullOrWhiteSpace(name))
				{
					res.AppendLine($"<li>{name} - удалена</li>");
				}
			}

			foreach (var service in target)
			{
				var currentService = source.FirstOrDefault(s => cmpValFunc(s).Equals(cmpValFunc(service)));
				if (currentService != null)
				{
					if (diffFunc != null)
					{
						var diff = diffFunc(currentService, service, uw);
						var name = nameFunc(currentService, uw);
						if (!string.IsNullOrWhiteSpace(diff) && !string.IsNullOrWhiteSpace(name))
						{
							res.AppendLine($"<li>{name} - изменена <ul>{diff}</ul></li>");
						}
					}
				}
				else
				{
					var name = nameFunc(service, uw);
					if (!string.IsNullOrWhiteSpace(name))
					{
						res.AppendLine($"<li>{name} - добавлена</li>");
					}
				}
			}

			return res.ToString();
		}
	}
}

