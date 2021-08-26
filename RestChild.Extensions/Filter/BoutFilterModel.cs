using System.Collections.Generic;
using RestChild.Domain;

namespace RestChild.Extensions.Filter
{
    public class BoutFilterModel
    {
        public BoutFilterModel()
        {
            PageNumber = 1;
        }

        public long? GroupedTimeOfRestId { get; set; }
        public long? HotelTypeId { get; set; }
        public long? HotelsId { get; set; }
        public long? YearOfRestId { get; set; }
        public long? StateId { get; set; }

        public CommonPagedList<Bout> Result { get; set; }

        public ICollection<GroupedTimeOfRest> GroupedTimesOfRest { get; set; }
        public ICollection<StateMachineState> States { get; set; }
        public ICollection<YearOfRest> YearsOfRest { get; set; }
        public List<HotelType> HotelTypes { get; set; }
        public Hotels Hotels { get; set; }

        public int PageNumber { get; set; }
    }
}
