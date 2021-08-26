using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using RestChild.Comon;
using RestChild.Comon.Enumeration;
using RestChild.Domain;
using RestChild.Extensions.Extensions;

namespace RestChild.Web.Models
{
	/// <summary>
	///     модель услуг.
	/// </summary>
	public class TourServiceModel : ViewModelBase<AddonServices>
	{
		public TourServiceModel() : base(new AddonServices())
		{
			Prices = new List<AddonServicesPrice>();
			Services = new List<TourServiceModel>();
		}

		public TourServiceModel(AddonServices data, bool isEditable) : base(data)
		{
			Prices = data.Prices?.ToList() ?? new List<AddonServicesPrice>();
			ConditionsForAccompanying = data.ConditionsForAccompanying;
			Description = data.Description;
			AnnouncementEvent = data.AnnouncementEvent;
			IsEditable = isEditable;
			Services = data.LinkServices?.Where(s => s.IsActive).Where(l=>l.LinkServiceId == data.Id).Select(s => new TourServiceModel(s, isEditable)).ToList() ?? new List<TourServiceModel>();
			DirectoryFlightsId = data.TourTransport?.DirectoryFlightsId ?? data.Parent?.TourTransport?.DirectoryFlightsId;
			DirectoryFlights = data.TourTransport?.DirectoryFlights.GetShortName() ?? data.Parent?.TourTransport?.DirectoryFlights.GetShortName();
		}

		/// <summary>
		/// Можно ли редактировать
		/// </summary>
		public bool IsEditable { get; set; }

		public IList<AddonServicesPrice> Prices { get; set; }

		/// <summary>
		/// рейс
		/// </summary>
		public long? DirectoryFlightsId { get; set; }

		public string DirectoryFlights { get; set; }

		[AllowHtml]
		public string Description { get; set; }

		/// <summary>
		/// Анонс мероприятия
		/// </summary>
		[AllowHtml]
		public string AnnouncementEvent { get; set; }

		/// <summary>
		/// Условия для сопровождающих
		/// </summary>
		[AllowHtml]
		public string ConditionsForAccompanying { get; set; }

		/// <summary>
		/// дочерние услуги
		/// </summary>
		public IList<TourServiceModel> Services { get; set; }

		/// <summary>
		///     стоимость
		/// </summary>
		/// <returns></returns>
		public override AddonServices BuildData()
		{
			var d = base.BuildData();
			d.Prices = Prices ?? new List<AddonServicesPrice>();
			d.Description = Description;
			d.AnnouncementEvent = AnnouncementEvent;
			d.ConditionsForAccompanying = ConditionsForAccompanying;
			d.LinkServices = Services?.Select(s => s.BuildData()).ToList() ?? new List<AddonServices>();
			if (d.TypeOfServiceId == (long) ServiceEnum.TransferAero || d.TypeOfServiceId == (long) ServiceEnum.TransferTrain ||
			    d.TypeOfServiceId == (long) ServiceEnum.TransferAuto)
			{
				d.TourTransport = new TourTransport
				{
					DirectoryFlightsId = DirectoryFlightsId,
					Id = d.TourTransportId ?? 0
				};
			}

			foreach (var ls in d.LinkServices.ToList())
			{
				if (d.Id > 0)
				{
					ls.LinkServiceId = d.Id;
				}
				else
				{
					ls.LinkService = d;
				}

				d.LinkServices.AddRange(ls.LinkServices);
				ls.LinkServices = null;
			}

 			return d;
		}
	}
}
