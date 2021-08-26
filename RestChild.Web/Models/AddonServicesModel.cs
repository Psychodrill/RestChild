using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using RestChild.Comon.Dto.Commercial;
using RestChild.Domain;

namespace RestChild.Web.Models
{
	/// <summary>
	/// модель по доп услугам.
	/// </summary>
	public class AddonServicesModel : ViewModelBase<AddonServices>
	{
		public AddonServicesModel() : this(new AddonServices())
		{
			Hotels = new List<BaseResponse>();
			Tags = new List<BaseResponse>();
			Prices = new List<AddonServicesPrice>();
			Citys = new List<City>();
		}

		public AddonServicesModel(AddonServices data) : base(data)
		{
			IsRequared = data.Requared ?? false;
			Hotels = data.Hotels?.Select(h => new BaseResponse {Id = h.Id, Name = h.Name}).ToList() ?? new List<BaseResponse>();
			Tags = data.Tags?.Select(h => new BaseResponse { Id = h.Id, Name = h.Name }).ToList() ?? new List<BaseResponse>();
			Prices = data.Prices?.ToList() ?? new List<AddonServicesPrice>();
			Citys = new List<City>();
			Description = data.Description;
			AnnouncementEvent = data.AnnouncementEvent;
			ConditionsForAccompanying = data.ConditionsForAccompanying;
		}

		/// <summary>
		/// типы услуг.
		/// </summary>
		public List<SelectListItem> TypeOfServices { get; set; }

		/// <summary>
		/// Города
		/// </summary>
		public IList<City> Citys { get; set; }

		public override AddonServices BuildData()
		{
			var data = base.BuildData();
			data.Hotels = Hotels?.Select(h=>new Hotels{Id = h.Id ?? 0, Name = h.Name}).ToList() ?? new List<Hotels>();
			data.Tags = Tags?.Select(h => new Tag { Id = h.Id ?? 0, Name = h.Name }).ToList() ?? new List<Tag>();
			data.Requared = IsRequared;
			data.GeneralService = true;
			data.Prices = Prices ?? new List<AddonServicesPrice>();
			data.Description = Description;
			data.AnnouncementEvent = AnnouncementEvent;
			data.ConditionsForAccompanying = ConditionsForAccompanying;
			return data;
		}

		public List<BaseResponse> Hotels { get; set; }

		public List<BaseResponse> Tags { get; set; }

		public List<TypePriceCalculation> TypePriceCalculations { get; set; }

		/// <summary>
		/// Статус
		/// </summary>
		public ViewModelState State { get; set; }

		/// <summary>
		/// Идентификатор перехода
		/// </summary>
		public string StateMachineActionString { get; set; }

		public bool IsEditable { get; set; }

		public bool IsRequared { get; set; }

		public string ActiveTab { get; set; }

		public string NameEm { get; set; }

		public string TypeOfServiceEm { get; set; }

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

		public override bool CheckModel(string action = null)
		{
			var hasError = false;
			if (string.IsNullOrEmpty(Data.Name))
			{
				NameEm = RequaredField;
				hasError = true;
			}

			if (!Data.TypeOfServiceId.HasValue)
			{
				TypeOfServiceEm = RequaredField;
				hasError = true;
			}

			return !hasError && base.CheckModel(action);
		}

		public IList<AddonServicesPrice> Prices { get; set; }

	}
}
