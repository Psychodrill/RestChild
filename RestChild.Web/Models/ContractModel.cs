using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RestChild.Extensions.Filter;
using RestChild.Domain;
using RestChild.Web.Controllers.WebApi;

namespace RestChild.Web.Models
{
	public class ContractModel : ViewModelBase<Contract>
	{
		public ContractModel()
			:base(new Contract())
		{
			Agreements = new List<ContractAddonAgreement>();
		}

		public ContractModel(Contract contract)
			:base(contract)
		{
			Agreements = contract.AddonAgreements?.ToList() ?? new List<ContractAddonAgreement>();
		}

		public override Contract BuildData()
		{
			var data = base.BuildData();

			data.AddonAgreements = Agreements?.ToList() ?? new List<ContractAddonAgreement>();

			return data;
		}

		public List<ContractAddonAgreement> Agreements { get; set; }

		public bool IsEditable { get; set; }

		public ViewModelState State { get; set; }

		public string StateMachineActionString { get; set; }

		public ICollection<Organization> Oivs {get; set; }

		public ICollection<YearOfRest> Years { get; set; }

		public DirectoryFlightsFilterModel DirectoryFlightsFilterModel { get; set; }

		public ToursFilterModel ToursFilterModel { get; set; }
	}
}
