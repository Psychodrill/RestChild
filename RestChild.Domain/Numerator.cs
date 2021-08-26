using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestChild.Comon;

namespace RestChild.Domain
{
	/// <summary>
	/// Генерация ключей
	/// </summary>
	public class Numerator
	{
		public long Id { get; set; }

		public string Key { get; set; }
	}
}
