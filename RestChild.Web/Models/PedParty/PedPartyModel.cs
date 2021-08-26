using System.Collections.Generic;

namespace RestChild.Web.Models.PedParty
{
	public class PedPartyModel : ViewModelBase<Domain.PedParty>
	{
		public ViewModelState State { get; set; }

		public string StateMachineActionString { get; set; }

		public bool IsEditable { get; set; }
		public PedPartyModel():base(new Domain.PedParty())
		{
			
		}

		public PedPartyModel(Domain.PedParty data) : base(data)
		{
		}
	}
}