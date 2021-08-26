using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using RestChild.Domain;

namespace RestChild.Web.Models
{
	/// <summary>
	/// Район для межведомственного запроса. 
	/// </summary>
	public class InteragencyRequestRegionModel
	{
		/// <summary>
		/// Район
		/// </summary>
		public BtiRegion Data { get; set; }

		/// <summary>
		/// Количество детей по району
		/// </summary>
		public int ChildsCount { get; set; }
	}
}