using System.Text;

namespace RestChild.Domain
{
	public partial class RoomRates
	{
		public string AccommodationAndDiningOptionsString()
		{
			var res = new StringBuilder();
			if (Accommodation != null)
			{
				res.Append(Accommodation.Name);
			}

			if (DiningOptions != null)
			{
				if (Accommodation != null)
				{
					res.Append("; ");
				}

				res.Append(DiningOptions.Name);
			}

			return res.ToString();
		}

		public override string ToString()
		{
			var res = new StringBuilder();
			if (Accommodation != null)
			{
				res.Append(Accommodation.Name);
			}

			if (DiningOptions != null)
			{
				if (Accommodation != null)
				{
					res.Append("; ");
				}

				res.Append(DiningOptions.Name);
			}

			if (TypeOfRooms != null)
			{
				if (Accommodation != null || DiningOptions != null)
				{
					res.Append("; ");
				}

				res.Append(TypeOfRooms.Name);
			}

			return res.ToString();
		}
	}
}