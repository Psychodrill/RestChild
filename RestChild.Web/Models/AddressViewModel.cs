using RestChild.Domain;

namespace RestChild.Web.Models
{
	public class AddressViewModel : ViewModelBase<Address>
	{
		public AddressViewModel(Address data) : base(data)
		{
         //this.ManualType = data.BtiDistrictId != null || data.BtiRegionId != null || data.Street != null || data.House != null || data.Corpus != null || data.Stroenie != null || data.Vladenie != null;
         this.ManualType = data.Street != null || data.House != null || data.Corpus != null || data.Stroenie != null || data.Vladenie != null;

         if (data.BtiDistrictId.HasValue && data.BtiDistrict != null)
         {
            Data.Region = data.BtiDistrict.Name;
         }
         if(data.BtiRegionId.HasValue && data.BtiRegion != null)
         {
            Data.District = data.BtiRegion.Name;
         }
		}

		public AddressViewModel()
			: base(new Address())
		{
			this.ManualType = true;
		}

		public bool ManualType { get; set; }

		public override Address BuildData()
		{
			if (this.ManualType)
			{
				return new Address()
				{
					Id = Data.Id,
					BtiDistrictId = Data.BtiDistrictId,
					BtiRegionId = Data.BtiRegionId,
					Street = Data.Street,
					House = Data.House,
					Corpus = Data.Corpus,
					Stroenie = Data.Stroenie,
					Vladenie = Data.Vladenie,
					Appartment = Data.Appartment
				};
			}
			else
			{
				var res = new Address()
            {
               Id = Data.Id,
               BtiAddressId = Data.BtiAddressId,
               Appartment = Data.Appartment,
               FiasId = Data.FiasId,
               Name = Data.Name,
               BtiDistrictId = Data.BtiDistrictId,
               BtiRegionId = Data.BtiRegionId,
               Region = Data.Region,
               District = Data.District,
            };

            return res;
			}
		}
	}
}
