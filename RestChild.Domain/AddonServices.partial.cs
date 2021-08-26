using System.ComponentModel.DataAnnotations.Schema;

namespace RestChild.Domain
{
	public partial class AddonServices
	{
		[NotMapped]
		public bool InRequest { get; set; }

		/// <summary>
		/// наименование услуги
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			var res = Name;
			if (TypeOfService != null)
			{
				res += $" ({TypeOfService.Name})";
			}

			return res;
		}
	}
}
