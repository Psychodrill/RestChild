using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using RestChild.Comon;
using RestChild.Comon.Enumeration;
using RestChild.Domain;
using RestChild.Extensions.Extensions;
using RestChild.Web.Controllers;

namespace RestChild.Web.Models
{
   using Security = RestChild.Web.Controllers.Security;

	public class TransportInfoModel : ViewModelBase<TransportInfo>
	{
		public TransportInfoModel(TransportInfo data)
			: base(data)
		{
			InsertLinks(data.People);

			FlightStatistics = data.People.GroupBy(g => new {DirectoryFlightsId = g.DirectoryFlightsId ?? 0, g.DateDeparture})
				.Select(t => new Tuple<string, DateTime?, int>(t.FirstOrDefault()?.DirectoryFlights?.GetName() ?? "Не распределено", t.FirstOrDefault()?.DateDeparture, t.Count())).ToList();

			Memo = data.Memo;
		}

		public TransportInfoModel()
			: base(new TransportInfo())
		{
			FlightStatistics = new List<Tuple<string, DateTime?, int>>();
		}

		public override TransportInfo BuildData()
		{
			Data.Memo = Memo;
			return base.BuildData();
		}

		/// <summary>
		/// статистика по рейсам
		/// </summary>
		public List<Tuple<string, DateTime?, int>> FlightStatistics { get; set; }

		/// <summary>
		///     Транспорт в лагеря
		/// </summary>
		public List<TransportCampModel> TransportToCamps { get; set; }

		/// <summary>
		///     Транспорт в отели
		/// </summary>
		public List<TransportHotelModel> TransportToHotels { get; set; }

		public ViewModelState State { get; set; }
		public string StateMachineActionString { get; set; }
		public bool IsEditable { get; set; }

		/// <summary>
		///     Справочник рейсов
		/// </summary>
		public List<DirectoryFlights> DirectoryFlights { get; set; }

		/// <summary>
		///     Причины отказа от обратного билета
		/// </summary>
		public List<NotNeedTicketReason> NotNeedTicketReasons { get; set; }

		/// <summary>
		///     Памятка
		/// </summary>
		[AllowHtml]
		public string Memo { get; set; }

		private void InsertLinks(ICollection<LinkToPeople> links)
		{
			if (links == null)
			{
				return;
			}

			if (!Security.HasRight(AccessRightEnum.Transport.View))
			{
				var orgs = AccessRightEnum.Transport.View.GetSecurityOrganiztion();
				links = links.Where(l => orgs.Contains(l.DirectoryFlights?.Contract?.SupplierId)).ToList();
			}

			links = links
				.Where(
					l =>
						!l.NullSafe(link => link.Child.NotComeInPlaceOfRest) && !l.NullSafe(link => link.Applicant.NotComeInPlaceOfRest))
				.ToList();
			var camps =
				links.Where(l => l.Bout.Hotels.HotelTypeId == (long) HotelTypeEnum.Camp).GroupBy(l => l.Bout.Hotels).ToList();
			var hotels =
				links.Where(l => l.Bout.Hotels.HotelTypeId == (long) HotelTypeEnum.Hotel).GroupBy(l => l.Bout.Hotels).ToList();

			TransportToCamps = new List<TransportCampModel>();
			foreach (var camp in camps)
			{
				var peopleByParties =
					camp.Where(
						l =>
							l.TypeOfLinkPeopleId == (long) TypeOfLinkPeopleEnum.Child ||
							l.TypeOfLinkPeopleId == (long) TypeOfLinkPeopleEnum.Counselor).Where(l=>l.Party!= null && l.Party.StateId != StateMachineStateEnum.Deleted && l.Party.StateId.HasValue).GroupBy(l => l.Party);
				TransportToCamps.Add(new TransportCampModel
				{
					Id = camp.NullSafe(c => c.Key.Id),
					Name = camp.NullSafe(c => c.Key.Name),
					Administrators =
						camp.Where(c => c.TypeOfLinkPeopleId == (long) TypeOfLinkPeopleEnum.Administrator)
							.Select(l => new LinkToPeopleModel(l)).OrderBy(s => s.Data.AdministratorTour?.LastName)
							.ToList(),
					SeniorCounselors =
						camp.Where(c => c.TypeOfLinkPeopleId == (long) TypeOfLinkPeopleEnum.SeniorCounselor)
							.Select(l => new LinkToPeopleModel(l)).OrderBy(s => s.Data.Counselors?.LastName)
							.ToList(),
					SwingCounselors =
						camp.Where(c => c.TypeOfLinkPeopleId == (long) TypeOfLinkPeopleEnum.SwingCounselor)
							.Select(l => new LinkToPeopleModel(l)).OrderBy(s => s.Data.Counselors?.LastName)
							.ToList(),
					Attendants =
						camp.Where(c => c.TypeOfLinkPeopleId == (long) TypeOfLinkPeopleEnum.Attendant)
							.Select(l => new LinkToPeopleModel(l)).OrderBy(s => s.Data.Applicant?.LastName)
							.ToList(),
					Parties = peopleByParties.Select(p => new TransportPartyModel
					{
						Id = p.NullSafe(k => k.Key.Id),
						Number = p.NullSafe(k => k.Key.PartyNumber),
						Children =
							p.Where(l => l.TypeOfLinkPeopleId == (long) TypeOfLinkPeopleEnum.Child && l.Child !=null)
								.Select(l => new LinkToPeopleModel(l)).OrderBy(c => c.Data?.Child?.LastName).ThenBy(c => c.Data?.Child?.FirstName).ToList(),
						Counselors =
							p.Where(l => l.TypeOfLinkPeopleId == (long) TypeOfLinkPeopleEnum.Counselor)
								.Select(l => new LinkToPeopleModel(l)).OrderBy(c => c.Data.Counselors.LastName).ThenBy(c => c.Data.Counselors.FirstName)
								.ToList()
					}).OrderBy(t => t.Number).ToList()
				});
			}

			TransportToHotels = new List<TransportHotelModel>();
			foreach (var hotel in hotels)
			{
				var campers =
					hotel.Where(
						c =>
							c.TypeOfLinkPeopleId == (long) TypeOfLinkPeopleEnum.Child ||
							c.TypeOfLinkPeopleId == (long) TypeOfLinkPeopleEnum.Attendant);
				var campersByRequests = campers.GroupBy(c => c.Request).ToList();
				var campersByRequestsWithAdditionalPlaces = campersByRequests.Where(c => !c.Key.ParentRequestId.HasValue).Select(
					g =>
					{
						var additionalPlaces = campersByRequests.Where(c => c.Key.ParentRequestId == g.Key.Id).ToList();
						return new TransportRequestModel
						{
							Id = g.NullSafe(gr => gr.Key.Id),
							RequestNumber = g.NullSafe(gr => gr.Key.RequestNumber),
							Campers = g.Select(l => new LinkToPeopleModel(l)).ToList(),
							AdditionalPlacesModels = additionalPlaces.Select(p => new TransportRequestModel
							{
								Id = p.Key.Id,
								RequestNumber = p.Key.RequestNumber,
								Campers = p.Select(l => new LinkToPeopleModel(l)).ToList()
							}).ToList()
						};
					}).ToList();
				TransportToHotels.Add(new TransportHotelModel
				{
					Id = hotel.NullSafe(c => c.Key.Id),
					Name = hotel.NullSafe(c => c.Key.Name),
					Administrators =
						hotel.Where(c => c.TypeOfLinkPeopleId == (long) TypeOfLinkPeopleEnum.Administrator)
							.Select(l => new LinkToPeopleModel(l))
							.ToList(),
					Requests = campersByRequestsWithAdditionalPlaces
				});
			}
		}

		public List<LinkToPeopleModel> GetLinksToPeople()
		{
			var result = new List<LinkToPeopleModel>();

			if (TransportToCamps != null)
			{
				var parties = TransportToCamps.Where(t => t.Parties != null).SelectMany(t => t.Parties).ToList();

				result.AddRange(TransportToCamps.Where(t => t.Administrators != null).SelectMany(t => t.Administrators));
				result.AddRange(TransportToCamps.Where(t => t.SeniorCounselors != null).SelectMany(t => t.SeniorCounselors));
				result.AddRange(TransportToCamps.Where(t => t.SwingCounselors != null).SelectMany(t => t.SwingCounselors));
				result.AddRange(parties.Where(p => p.Counselors != null).SelectMany(p => p.Counselors));
				result.AddRange(parties.Where(p => p.Children != null).SelectMany(p => p.Children));
			}

			if (TransportToHotels != null)
			{
				var requests = TransportToHotels.Where(t => t.Requests != null).SelectMany(t => t.Requests).ToList();

				result.AddRange(TransportToHotels.Where(t => t.Administrators != null).SelectMany(t => t.Administrators));
				result.AddRange(requests.Where(r => r.Campers != null).SelectMany(r => r.Campers));
				result.AddRange(
					requests.Where(r => r.AdditionalPlacesModels != null)
						.SelectMany(r => r.AdditionalPlacesModels)
						.Where(r => r.Campers != null)
						.SelectMany(r => r.Campers));
			}

			return result;
		}
	}
}
