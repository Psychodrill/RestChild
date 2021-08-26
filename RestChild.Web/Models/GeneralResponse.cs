using System.Collections.Generic;
using System.Runtime.Serialization;
using RestChild.Web.Models.Limits;

namespace RestChild.Web.Models
{
	[DataContract(Name = "generalResponse")]
	public class GeneralResponse
	{
		public GeneralResponse()
		{
			
		}

		public GeneralResponse(string errorMessage)
		{
			Errors = new List<string>{errorMessage};
			IsError = true;
		}

		/// <summary>
		///     УРл для перехода если надо.
		/// </summary>
		[DataMember(Name = "url")]
		public string Url { get; set; }

		/// <summary>
		///     Ошибка
		/// </summary>
		[DataMember(Name = "errors")]
		public List<string> Errors { get; set; }

		/// <summary>
		///     Есть ли ошибка.
		/// </summary>
		[DataMember(Name = "isError")]
		public bool IsError { get; set; }

		/// <summary>
		/// Элемент
		/// </summary>
		[DataMember(Name = "item")]
		public LimitItemModel Item { get; set; }
	}
}