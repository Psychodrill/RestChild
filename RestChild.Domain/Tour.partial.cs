using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using RestChild.Comon;

namespace RestChild.Domain
{
	using System.Runtime.Serialization;

	public partial class Tour
	{
		[NotMapped]
		public ICollection<ListOfChilds> ChildLists { get; set; }

		[NotMapped]
		public IEnumerable<Child> Childs { get; set; }

		[NotMapped]
		public ICollection<Booking> Bookings { get; set; }

		/// <summary>
		/// Услуги
		/// </summary>
		[InverseProperty("Tour")]
		[DataMember(Name = "services", EmitDefaultValue = false)]
		public virtual ICollection<AddonServices> Services { get; set; }

		/// <summary>
		/// Описание заезда
		/// </summary>
		public override string ToString()
		{
			if (this?.TypeOfRest?.Commercial == true)
			{
				var hotel = TourAccommodations?.Select(a => a.Hotel).FirstOrDefault();

				var res = $"{Name}";
				if (NotFixedDate)
				{
					if (hotel != null)
					{
						res += $" ({hotel.Name})";
					}
					return res;
				}

				if (DateOutcome.HasValue && DateIncome.HasValue)
				{
					res += $" с {DateIncome.FormatEx()} по {DateOutcome.FormatEx()}";
				}
				else if (DateIncome.HasValue)
				{
					res += $" {DateIncome.FormatEx()}";
				}

				if (!(TourAccommodations?.Any(a => a.HotelId != hotel?.Id) ?? true))
				{
					res += $" ({hotel?.Name})";
				}

				return res;
			}

			return $"{(Hotels != null ? Hotels.Name : string.Empty)} - с {DateIncome:dd.MM.yyyy} по {DateOutcome:dd.MM.yyyy}";
		}

		[NotMapped]
		public int ChildrenCount { get; set; }
	}
}
