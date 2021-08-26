using System;
using System.Collections.Generic;
using RestChild.Web.IdentificationService;

namespace RestChild.Web.Models
{
	/// <summary>
	/// модель поиска.
	/// </summary>
	public class IdentificationModel
	{
		public string Error { get; set; }
		public string Snils { get; set; }
		public string LastName { get; set; }
		public string FirstName { get; set; }
		public string MiddleName { get; set; }
		public string DocumentNumber { get; set; }
		public DateTime? BithDate { get; set; }
		public bool Male { get; set; }
		public Dictionary<int, IdentificationService.DocumentType> DocumentTypes { get; set; }
		public DeclarantSearchResult Declarant { get; set; }
		public Dictionary<Guid, RestChild.Web.IdentificationService.DeclarantDocument[]> Documents { get; set; }
	}
}
