using System;
using RestChild.Domain;

namespace RestChild.Extensions.Filter
{
    public class BookingMosgorturReestrWorkingDaysFilterModel
    {
        public BookingMosgorturReestrWorkingDaysFilterModel()
        {
            PageNumber = 1;
            Date = DateTime.Now.Date;
        }

        public DateTime Date { get; set; }

        public int PageNumber { get; set; }
        public long? DepartmentId { get; set; }

        public CommonPagedList<MGTWorkingDay> Result { get; set; }
    }
}
