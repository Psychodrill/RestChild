using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestChild.Domain
{
	public partial class TypeOfRooms
	{
		public string GetConviencsString()
		{
			var conveniences = new List<string>();
			if (this.HaveFurniture)
			{
				conveniences.Add("мебель");
			}
			if (this.HaveBalcony)
			{
				conveniences.Add("балкон");
			}
			if (this.HaveTv)
			{
				conveniences.Add("телевизор");
			}
			if (this.HaveBath)
			{
				conveniences.Add("ванна");
			}
			if (this.HaveSatelliteTv)
			{
				conveniences.Add("спутниковое ТВ");
			}
			if (this.HaveShower)
			{
				conveniences.Add("душ");
			}
			if (this.HaveLocalTv)
			{
				conveniences.Add("местное ТВ");
			}
			if (this.HaveHairDryer)
			{
				conveniences.Add("фен");
			}
			if (this.HaveRadio)
			{
				conveniences.Add("радио");
			}
			if (this.HaveWc)
			{
				conveniences.Add("туалет");
			}
			if (this.HavePhone)
			{
				conveniences.Add("телефон");
			}
			if (this.HaveBidet)
			{
				conveniences.Add("биде");
			}
			if (this.HaveBar)
			{
				conveniences.Add("бар");
			}
			if (this.HaveAirConditioning)
			{
				conveniences.Add("кондиционер");
			}
			if (this.HaveSafe)
			{
				conveniences.Add("сейф");
			}
			if (this.HaveKitchen)
			{
				conveniences.Add("кухня");
			}
			if (this.HaveRefrigerator)
			{
				conveniences.Add("холодильник");
			}
			return string.Join(", ", conveniences);
		}

		public ICollection<string> GetConviencs()
		{
			var conveniences = new List<string>();
			if (this.HaveFurniture)
			{
				conveniences.Add("мебель");
			}
			if (this.HaveBalcony)
			{
				conveniences.Add("балкон");
			}
			if (this.HaveTv)
			{
				conveniences.Add("телевизор");
			}
			if (this.HaveBath)
			{
				conveniences.Add("ванна");
			}
			if (this.HaveSatelliteTv)
			{
				conveniences.Add("спутниковое ТВ");
			}
			if (this.HaveShower)
			{
				conveniences.Add("душ");
			}
			if (this.HaveLocalTv)
			{
				conveniences.Add("местное ТВ");
			}
			if (this.HaveHairDryer)
			{
				conveniences.Add("фен");
			}
			if (this.HaveRadio)
			{
				conveniences.Add("радио");
			}
			if (this.HaveWc)
			{
				conveniences.Add("туалет");
			}
			if (this.HavePhone)
			{
				conveniences.Add("телефон");
			}
			if (this.HaveBidet)
			{
				conveniences.Add("биде");
			}
			if (this.HaveBar)
			{
				conveniences.Add("бар");
			}
			if (this.HaveAirConditioning)
			{
				conveniences.Add("кондиционер");
			}
			if (this.HaveSafe)
			{
				conveniences.Add("сейф");
			}
			if (this.HaveKitchen)
			{
				conveniences.Add("кухня");
			}
			if (this.HaveRefrigerator)
			{
				conveniences.Add("холодильник");
			}
			return conveniences;
		}

		public override string ToString()
		{
			return string.Format("{0} (основных: {1}{2})", Name, CountBasePlace, CountAddonPlace > 0 ? string.Format("; дополнительных: {0}", CountAddonPlace) : string.Empty);
		}
	}
}
