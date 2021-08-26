using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using RestChild.Domain;

namespace RestChild.Web.Models
{
	public class AdministratorTourModel : ViewModelBase<AdministratorTour>
	{
		public ViewModelState State { get; set; }

		public string StateMachineActionString { get; set; }

		public bool IsEditable { get; set; }

		public bool? IsMale { get; set; }

		public ICollection<DocumentType> DocumentTypes { get; set; } 

		public AdministratorTourModel()
			:base(new AdministratorTour())
		{
			
		}

		public AdministratorTourModel(AdministratorTour administrator)
			: base(administrator)
		{
			IsMale = administrator.Male;
		}

		public override AdministratorTour BuildData()
		{
			if (IsMale.HasValue)
			{
				Data.Male = IsMale.Value;
			}

			return base.BuildData();
		}
	}
}