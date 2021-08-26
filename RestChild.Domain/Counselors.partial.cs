using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestChild.Domain
{
	public partial class Counselors
	{
		/// <summary>
		/// ФИО
		/// </summary>
		/// <returns></returns>
		public string GetFio()
		{
			var name = new[] { this.LastName, this.FirstName, this.MiddleName };
			return string.Join(" ", name.Where(s => !string.IsNullOrEmpty(s)));
		}
	}
}
