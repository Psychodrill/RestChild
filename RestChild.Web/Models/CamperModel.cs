using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

using RestChild.Domain;

namespace RestChild.Web.Models
{
	/// <summary>
	/// Отдыхающий (модель для выгрузки)
	/// </summary>
	public class CamperModel
	{
		public string HotelName { get; set; }
		public string PartyNumber { get; set; }
		public string Category { get; set; }
		public string RequestNumber { get; set; }
		public long? RequestId { get; set; }
		public string ListName { get; set; }
		public string Organization { get; set; }
		public string ChildCategory { get; set; }
		public string Name { get; set; }
		public string LastName { get; set; }
		public string FirstName { get; set; }
		public string MiddleName { get; set; }
		public string DocType { get; set; }
		public string DocSeries { get; set; }
		public string DocNumber { get; set; }
		public string DocIssue { get; set; }
		public DateTime? DocIssueDate { get; set; }
		public DateTime? BirthDate { get; set; }
		public string BirthPlace { get; set; }
		public string Wagon { get; set; }
		public string PlaceNum { get; set; }
		public string NotNeedTicket { get; set; }
		public string DirectoryFlightCode { get; set; }
		public string DirectoryFlightNum { get; set; }
		public string TimeOfDeparture { get; set; }
		public string TimeOfArrival { get; set; }
		public string DepartureCode { get; set; }
		public string DepartureDate { get; set; }
		public string ArrivalCode { get; set; }
		public string TransportType { get; set; }
		public string DepartureName { get; set; }
		public string ArrivalName{ get; set; }
		public string ApplicantName { get; set; }
		public string ApplicantLastName { get; set; }
		public string ApplicantFirstName { get; set; }
		public string ApplicantMiddleName { get; set; }
		public string ApplicantEmail { get; set; }
		public string ApplicantPhone { get; set; }
		public DateTime? DateIncome { get; set; }
		public DateTime? DateOutcome { get; set; }
		public Color? Color { get; set; }
		public string SubjectOfRest { get; set; }
		public string GroupedDateTime { get; set; }
		public bool? IsMale { get; set; }
		public string Address { get; set; }
		public string OrganizationName { get; set; }
		public string Certificate { get; set; }
		public string School { get; set; }
		public string Position { get; set; }
		public string TimeOfRest { get; set; }

		/// <summary>
		/// получение информации о родстве
		/// </summary>
		public string FatherFio { get; set; }
		public string FatherBirthDate { get; set; }
		public string MotherFio { get; set; }
		public string MotherBirthDate { get; set; }

		/// <summary>
		/// отказы от билетов
		/// </summary>
		public string NotNeedTicketReason { get; set; }
		public string NotNeedTicketReasonTo { get; set; }
		public string NotNeedTicketReasonFrom { get; set; }
		public string Phone { get; set; }
	}
}
