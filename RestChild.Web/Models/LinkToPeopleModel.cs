using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using RestChild.Domain;

namespace RestChild.Web.Models
{
	public class LinkToPeopleModel : ViewModelBase<LinkToPeople>
	{
		public LinkToPeopleModel(LinkToPeople data)
			: base(data)
		{
			NotNeedTicket = !data.NeedTicket;
		}

		public LinkToPeopleModel()
			: base(new LinkToPeople())
		{
			
		}

		public override LinkToPeople BuildData()
		{
			var data = base.BuildData();
			data.NeedTicket = !NotNeedTicket;
			return data;
		}

		/// <summary>
		/// Не нужен билет
		/// </summary>
		public bool NotNeedTicket { get; set; }
	}
}