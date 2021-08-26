using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using RestChild.Domain;

namespace RestChild.Web.Models
{
	public class TransportRequestModel
	{
		public long Id { get; set; }
		/// <summary>
		/// Номер заявления
		/// </summary>
		public string RequestNumber { get; set; }

		/// <summary>
		/// Отдыхающие
		/// </summary>
		public List<LinkToPeopleModel> Campers { get; set; }

		/// <summary>
		/// Заявление на дополнительные места
		/// </summary>
		public List<TransportRequestModel> AdditionalPlacesModels { get; set; }
	}
}