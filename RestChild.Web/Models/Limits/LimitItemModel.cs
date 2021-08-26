using System.Collections.Generic;
using System.Runtime.Serialization;
using RestChild.Domain;

namespace RestChild.Web.Models.Limits
{
	/// <summary>
	/// типовой список квоты
	/// </summary>
	[DataContract(Name = "limitItemModel")]
	public class LimitItemModel
	{
		/// <summary>
		///     ИД.
		/// </summary>
		[DataMember(Name = "id")]
		public long Id { get; set; }

		/// <summary>
		/// тип квоты
		/// </summary>
		[DataMember(Name = "tlId")]
		public long? TypeOfListId { get; set; }

		/// <summary>
		/// тип квоты
		/// </summary>
		[DataMember(Name = "tl")]
		public string TypeOfList { get; set; }

		/// <summary>
		/// учреждения Id
		/// </summary>
		[DataMember(Name = "organizationId")]
		public long? OrganizationId { get; set; }

		/// <summary>
		/// Имя учреждения
		/// </summary>
		[DataMember(Name = "organizationName")]
		public string OrganizationName { get; set; }

		/// <summary>
		/// Имя ведомство
		/// </summary>
		[DataMember(Name = "vedomstvoName")]
		public string VedomstvoName { get; set; }


		/// <summary>
		/// Год кампании
		/// </summary>
		[DataMember(Name = "yearOfRestId")]
		public long? YearOfRestId { get; set; }

		/// <summary>
		/// Регион отдыха
		/// </summary>
		[IgnoreDataMember]
		public PlaceOfRest PlaceOfRest { get; set; }

		/// <summary>
		/// Квота по организации
		/// </summary>
		[IgnoreDataMember]
		public LimitOnOrganization LimitOnOrganization { get; set; }

		/// <summary>
		/// Место отдыха
		/// </summary>
		[DataMember(Name = "placeOfRestName")]
		public string PlaceOfRestName { get; set; }

		/// <summary>
		/// Место отдыха
		/// </summary>
		[DataMember(Name = "placeOfRestId")]
		public long? PlaceOfRestId { get; set; }

		/// <summary>
		/// Смена
		/// </summary>
		[DataMember(Name = "timeOfRestId")]
		public long? TimeOfRestId { get; set; }

		/// <summary>
		/// Время отдыха
		/// </summary>
		[DataMember(Name = "timeOfRestName")]
		public string TimeOfRestName { get; set; }

		/// <summary>
		/// Время отдыха
		/// </summary>
		[IgnoreDataMember]
		public TimeOfRest TimeOfRest { get; set; }

		/// <summary>
		/// Заезд
		/// </summary>
		[DataMember(Name = "tourId")]
		public long? TourId { get; set; }

		/// <summary>
		/// Заезд
		/// </summary>
		[DataMember(Name = "tourName")]
		public string TourName { get; set; }

		/// <summary>
		/// Заезд
		/// </summary>
		[IgnoreDataMember]
		public Tour Tour { get; set; }

		/// <summary>
		/// Размер квоты
		/// </summary>
		[DataMember(Name = "volume")]
		public int Volume { get; set; }

		/// <summary>
		/// Размер заявок
		/// </summary>
		[DataMember(Name = "requestVolume")]
		public int RequestVolume { get; set; }

		/// <summary>
		/// Статус
		/// </summary>
		[DataMember(Name = "state")]
		public StateMachineState State { get; set; }

		/// <summary>
		/// Размер квоты
		/// </summary>
		[DataMember(Name = "actions")]
		public List<StateMachineAction> Actions { get; set; }
	}
}
