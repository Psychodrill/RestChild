using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace RestChild.Domain
{
	public partial class Child
	{
		[NotMapped]
		public long IndexField { get; set; }

		[NotMapped]
		public IList<AddonServices> AddonServices { get; set; }

		/// <summary>
		/// Возраст в годах
		/// </summary>
		/// <param name="dateOfCalculation">Дата для которой рассчитывается возраст</param>
		/// <returns></returns>
		public int? GetAgeInYears(DateTime? dateOfCalculation = null)
		{
			if (DateOfBirth.HasValue)
			{
				var today = dateOfCalculation ?? DateTime.Today;
				var age = today.Year - DateOfBirth.Value.Year;
				if (DateOfBirth.Value > today.AddYears(-age))
				{
					age--;
				}

				return age;
			}

			return null;
		}

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
