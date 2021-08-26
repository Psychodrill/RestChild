using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using RestChild.Domain;

namespace RestChild.Web.Models
{
	public class DirectoryFlightsModel : ViewModelBase<DirectoryFlights>
	{
		public DirectoryFlightsModel(DirectoryFlights data)
			: base(data)
		{
			YearId = data.YearOfRestId ?? 0;
		}

		public DirectoryFlightsModel()
			:base(new DirectoryFlights())
		{
		}

		public override DirectoryFlights BuildData()
		{
			Data.YearOfRestId = YearId > 0 ? YearId : (long?)null;
			return base.BuildData();
		}

		public ViewModelState State{ get; set; }

		public string StateMachineActionString { get; set; }

		public bool IsEditable { get; set; }

		[Required(ErrorMessage = "\"Год кампании\" не может быть пустым")]
		public long YearId { get; set; }

		public ICollection<TypeOfTransport> TypesOfTransports { get; set; }

		public ICollection<City> Cities { get; set; }

		public ICollection<YearOfRest> Years { get; set; }
	}
}
