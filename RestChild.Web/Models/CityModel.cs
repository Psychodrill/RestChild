using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using RestChild.Domain;

namespace RestChild.Web.Models
{
	public class CityModel : ViewModelBase<City>
	{
		public CityModel()
			:base(new City())
		{
			
		}

		public CityModel(City city)
			:base(city)
		{
			
		}
	}
}