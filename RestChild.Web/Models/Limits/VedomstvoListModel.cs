using System.Collections.Generic;
using System.Runtime.Serialization;
using RestChild.Domain;

namespace RestChild.Web.Models.Limits
{
	[DataContract(Name = "vedomstvoListModel")]
	public class VedomstvoListModel : ViewModelBase<YearOfRest>
	{
		/// <summary>
		/// список возможных годов.
		/// </summary>
		public List<YearOfRest> ListOfYears { get; set; }

		/// <summary>
		/// список типов квот
		/// </summary>
		public List<TypeOfLimitList> TypeOfLimitLists { get; set; }

		/// <summary>
		/// список ведомств
		/// </summary>
		public List<Organization> Vedomstvos { get; set; }

		/// <summary>
		/// статус
		/// </summary>
		public ViewModelState State { get; set; }

		/// <summary>
		/// Код действия.
		/// </summary>
		public string StringStateCode { get; set; }

		/// <summary>
		/// список ошибок
		/// </summary>
		public List<string> Errors { get; set; }

		public VedomstvoListModel() : base(new YearOfRest())
		{			
		}

		public VedomstvoListModel(YearOfRest data) : base(data)
		{
		}
	}
}