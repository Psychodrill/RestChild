using System.Collections.Generic;
using System.Linq;
using RestChild.Comon.Dto.Commercial;
using RestChild.Domain;

namespace RestChild.Web.Models
{
	public class OrganizationViewModel : ViewModelBase<Organization>
	{
		public OrganizationViewModel() : base(new Organization())
		{
			Banks = new List<OrganizationBank>();
			Contracts = new List<Contract>();
			Okveds = new List<BaseResponse>();
		}

		public OrganizationViewModel(Organization data) : base(data)
		{
			Parent = data.Parent;
			Banks = data.Bank?.Where(b=>b.LastUpdateTick != 0).ToList() ?? new List<OrganizationBank>();
			Contracts = new List<Contract>();
			Okveds = data.Okved?.ToList().Select(o => new BaseResponse {Id = o.Id, Name = o.Name, Code = o.Code}).ToList() ??
			         new List<BaseResponse>();
		}

		public Organization Parent { get; set; }

		public string SelectedOrganizationTypes { get; set; }

		/// <summary>
		///     регионы
		/// </summary>
		public List<StateDistrict> StateDistricts { get; set; }

		/// <summary>
		///     ОКВЭД
		/// </summary>
		public List<BaseResponse> Okveds { get; set; }

		/// <summary>
		/// банковские реквизиты
		/// </summary>
		public List<OrganizationBank> Banks { get; set; }

		/// <summary>
		/// договора контракты
		/// </summary>
		public List<Contract> Contracts { get; set; }

		public override Organization BuildData()
		{
			var data = base.BuildData();

			data.Bank = Banks ?? new List<OrganizationBank>();

			data.Okved = Okveds?.Select(o => new Okved {Id = o.Id ?? 0}).Where(o => o.Id != 0).ToList() ?? new List<Okved>();

			return data;
		}
	}
}
