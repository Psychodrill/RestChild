using RestChild.Domain;

namespace RestChild.Extensions.Filter
{
    public class CityFilterModel
    {
        public CityFilterModel()
        {
            PageNumber = 1;
        }

        public string Name { get; set; }
        public bool? HaveAero { get; set; }
        public bool? HaveRailway { get; set; }
        public bool? IsActive { get; set; }

        public CommonPagedList<City> Result { get; set; }
        public int PageNumber { get; set; }
    }
}
