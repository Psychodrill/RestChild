using System.Runtime.Serialization;
using RestChild.Comon.Enumeration;

namespace RestChild.Web.Models
{
	/// <summary>
	///     ДТО для дневника смен
	/// </summary>
	[DataContract]
	public class BoutJournalDto
	{
		/// <summary>
		/// уникальный идентификатор
		/// </summary>
		[DataMember(Name = "id", EmitDefaultValue = false)]
		public long Id { get; set; }

		/// <summary>
		/// автор
		/// </summary>
		[DataMember(Name = "author", EmitDefaultValue = false)]
		public string Author { get; set; }

		/// <summary>
		/// заголовок
		/// </summary>
		[DataMember(Name = "title", EmitDefaultValue = false)]
		public string Title { get; set; }

		/// <summary>
		/// номер отряда
		/// </summary>
		[DataMember(Name = "partyNumber", EmitDefaultValue = false)]
		public string PartyNumber { get; set; }

		/// <summary>
		/// дата события
		/// </summary>
		[DataMember(Name = "dateEvent", EmitDefaultValue = false)]
		public string DateEvent { get; set; }

		/// <summary>
		/// дата создания
		/// </summary>
		[DataMember(Name = "dateCreate", EmitDefaultValue = false)]
		public string DateCreate { get; set; }

		/// <summary>
		/// признак что архивные записи
		/// </summary>
		[DataMember(Name = "isArchive", EmitDefaultValue = false)]
		public bool IsArchive { get; set; }

		/// <summary>
		/// отображать ли на сайте
		/// </summary>
		[DataMember(Name = "forSite",EmitDefaultValue = false)]
		public bool ForSite { get; set; }

		/// <summary>
		/// тип журнала
		/// </summary>
		[DataMember(Name = "boutJournalType", EmitDefaultValue = false)]
		public long BoutJournalType { get; set; }

		/// <summary>
		/// тип журнала
		/// </summary>
		[DataMember(Name = "boutJournalTypeName", EmitDefaultValue = false)]
		public string BoutJournalTypeName { get; set; }

		/// <summary>
		/// вид происшествия
		/// </summary>
		[DataMember(Name = "incident", EmitDefaultValue = false)]
		public string Incident { get; set; }
	}
}
