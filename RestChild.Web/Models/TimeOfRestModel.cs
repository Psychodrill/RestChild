using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using RestChild.Domain;

namespace RestChild.Web.Models
{
	public class TimeOfRestModel : ViewModelBase<TimeOfRest>
	{
		public TimeOfRestModel() : base(new TimeOfRest())
		{
		}

		public TimeOfRestModel(TimeOfRest data) : base(data)
		{
			if (data.Year > 1900 && data.Year < 2200 && data.Month >= 1 && data.Month <= 12 && data.DayOfMonth >= 1 &&
			    data.DayOfMonth <= 31)
			{
				try
				{
					PeriodStart = new DateTime(data.Year, data.Month, data.DayOfMonth);
				}
				catch
				{
				}
			}

			YearOfRestId = data.YearOfRestId;
			TypeOfRestId = data.TypeOfRestId;
		}

		[Required(ErrorMessage = "\"Год кампании\" не может быть пустым")]
		public long? YearOfRestId { get; set; }


		[Required(ErrorMessage = "\"Вид отдыха\" не может быть пустым")]
		public long? TypeOfRestId { get; set; }

		[Required(ErrorMessage = "\"Дата начала\" не может быть пустым")]
		public DateTime? PeriodStart { get; set; }

		public override TimeOfRest BuildData()
		{
			if (PeriodStart.HasValue)
			{
				Data.Year = PeriodStart.Value.Year;
				Data.Month = PeriodStart.Value.Month;
				Data.DayOfMonth = PeriodStart.Value.Day;
			}
			else
			{
				Data.Year = 0;
				Data.Month = 0;
				Data.DayOfMonth = 0;
			}

			Data.YearOfRestId = YearOfRestId;
			Data.TypeOfRestId = TypeOfRestId;

			return base.BuildData();
		}

		public List<SelectListItem> RestYears { get; set; }

		public List<SelectListItem> RestTypes { get; set; }

		public List<SelectListItem> GroupedTimeOfRest { get; set; }
	}
}
