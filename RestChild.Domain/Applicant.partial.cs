using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestChild.Domain
{
	public partial class Applicant
	{
		private int? _age;
		private DateTime? _dateOfBirth;

		[NotMapped]
		public IList<AddonServices> AddonServices { get; set; }

		[NotMapped]
		public long IndexField { get; set; }

		/// <summary>
		/// Возраст в годах
		/// </summary>
		/// <param name="dateOfCalculation">Дата для которой рассчитывается возраст</param>
		/// <returns></returns>
		public int? GetAgeInYears(DateTime? dateOfCalculation = null)
		{
			if (!DateOfBirth.HasValue)
			{
				return null;
			}

			if (!_age.HasValue || DateOfBirth != _dateOfBirth)
			{
				_dateOfBirth = DateOfBirth;
				var today = dateOfCalculation ?? DateTime.Today;
				_age = today.Year - DateOfBirth.Value.Year;
				if (DateOfBirth.Value > today.AddYears(-_age.Value)) _age--;
			}

			return _age;
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
