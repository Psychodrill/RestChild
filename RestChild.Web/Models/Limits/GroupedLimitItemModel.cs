using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace RestChild.Web.Models.Limits
{
	[DataContract(Name = "groupedLimitItemModel")]
	public class GroupedLimitItemModel
	{
		/// <summary>
		/// Смена
		/// </summary>
		[DataMember(Name = "oivId")]
		public long? OivId { get; set; }

		/// <summary>
		/// Смена
		/// </summary>
		[DataMember(Name = "oivOrgId")]
		public long? OivOrgId { get; set; }

		/// <summary>
		/// Смена
		/// </summary>
		[DataMember(Name = "placeOfRestId")]
		public long? PlaceOfRestId { get; set; }

		/// <summary>
		/// Смена
		/// </summary>
		[DataMember(Name = "timeOfRestId")]
		public long? TimeOfRestId { get; set; }

		/// <summary>
		/// Заезд
		/// </summary>
		[DataMember(Name = "tourId")]
		public long? TourId { get; set; }

		/// <summary>
		/// наименование
		/// </summary>
		[DataMember(Name = "name")]
		public string Name { get; set; }

		/// <summary>
		/// объем
		/// </summary>
		[DataMember(Name = "volume")]
		public int Volume { get; set; }

		/// <summary>
		/// объем сформированый
		/// </summary>
		[IgnoreDataMember]
		public int FormedVolume { get; set; }

		/// <summary>
		/// подгруппы.
		/// </summary>
		[DataMember(Name = "subGroups")]
		public List<GroupedLimitItemModel> SubGroups { get; set; }

		/// <summary>
		/// элементы.
		/// </summary>
		[DataMember(Name = "items")]
		public List<LimitItemModel> Items { get; set; }

		/// <summary>
		/// Дата для сортировки.
		/// </summary>
		public DateTime? OrderDate { get; set; }

		/// <summary>
		/// Дата для сортировки.
		/// </summary>
		public string OrderString { get; set; }
	
	}
}