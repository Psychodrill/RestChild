using System.Collections.Generic;
using RestChild.Domain;

namespace RestChild.Extensions.Filter
{
    public class HotelsFilterModel
    {
        public HotelsFilterModel()
        {
            PageNumber = 1;
        }

        public string Name { get; set; }
        public string Address { get; set; }
        public long? Region { get; set; }
        public bool? Tv { get; set; }
        public bool? Fridge { get; set; }
        public bool? Shower { get; set; }
        public long? StateId { get; set; }
        public long? HotelTypeId { get; set; }
        public CommonPagedList<Hotels> Result { get; set; }
        public int PageNumber { get; set; }

        public long? TypeOfRest { get; set; }

        public long? CityId { get; set; }

        public bool? Habitat { get; set; }

        #region Справочники

        public ICollection<PlaceOfRest> Regions { get; set; }

        public IEnumerable<HotelType> HotelTypes { get; set; }

        public IList<StateMachineState> States { get; set; }

        #endregion
    }
}
