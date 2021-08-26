using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using RestChild.Domain;

namespace RestChild.Web.Models
{
	public class PartyModel : ViewModelBase<Party>
	{
		/// <summary>
		/// Статус
		/// </summary>
		public ViewModelState State { get; set; }
		public BoutModel Bout { get; set; }

		public PartyModel()
			: this(new Party())
		{
			
		}

		public PartyModel(Party data)
			: base(data)
		{
			Bout = new BoutModel(this.Data.Bouts);
		}
	}
}