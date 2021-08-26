using System.Runtime.Serialization;

namespace RestChild.Web.Models.Payments
{
	/// <summary>
	///     распарсенное ПП
	/// </summary>
	[DataContract]
	public class PaymentDto
	{
		/// <summary>
		///     номер ПП
		/// </summary>
		[DataMember(Name = "pn", EmitDefaultValue = false)]
		public string PaymentNumber { get; set; }

		/// <summary>
		///     Назначение платежа
		/// </summary>
		[DataMember(Name = "p", EmitDefaultValue = false)]
		public string Purpose { get; set; }

		/// <summary>
		///     сумма в ПП
		/// </summary>
		[DataMember(Name = "s", EmitDefaultValue = false)]
		public string Summa { get; set; }

		/// <summary>
		///     Имя в списке
		/// </summary>
		[DataMember(Name = "n", EmitDefaultValue = false)]
		public string NameInList { get; set; }

		/// <summary>
		///     Фамилия в списке
		/// </summary>
		[DataMember(Name = "ln", EmitDefaultValue = false)]
		public string LastNameInList { get; set; }

		/// <summary>
		///     Описание места отдыха
		/// </summary>
		[DataMember(Name = "d", EmitDefaultValue = false)]
		public string Description { get; set; }

		/// <summary>
		///     Сумма к оплате
		/// </summary>
		[DataMember(Name = "sp", EmitDefaultValue = false)]
		public decimal? SummToPay { get; set; }

		/// <summary>
		///     ИД ребёнка
		/// </summary>
		[DataMember(Name = "cid", EmitDefaultValue = false)]
		public long ChildId { get; set; }

		/// <summary>
		///     ИД размещения
		/// </summary>
		[DataMember(Name = "tid", EmitDefaultValue = false)]
		public long? TourId { get; set; }

		/// <summary>
		///     ИД списка
		/// </summary>
		[DataMember(Name = "lcid", EmitDefaultValue = false)]
		public long? ListChildId { get; set; }

		/// <summary>
		///     Оплачено
		/// </summary>
		[IgnoreDataMember]
		public bool Payed { get; set; }

		/// <summary>
		///     Есть фамилия
		/// </summary>
		[IgnoreDataMember]
		public bool LastNamePresent { get; set; }

		/// <summary>
		///     Сумма совпала
		/// </summary>
		[IgnoreDataMember]
		public bool SummaEqual { get; set; }
	}
}
