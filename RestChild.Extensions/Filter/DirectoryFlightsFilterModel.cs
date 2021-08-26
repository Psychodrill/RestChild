using System.Collections.Generic;
using RestChild.Domain;

namespace RestChild.Extensions.Filter
{
    public class DirectoryFlightsFilterModel
    {
        public DirectoryFlightsFilterModel()
        {
            PageNumber = 1;
        }

        public long? YearOfRestId { get; set; }
        public long? TypeOfTransportId { get; set; }
        public long? DepartureId { get; set; }
        public long? ArrivalId { get; set; }
        public long? StateId { get; set; }
        public string DepartureCode { get; set; }
        public string ArrivalCode { get; set; }
        public string FilightNumber { get; set; }
        public string Code { get; set; }
        public long? ContractId { get; set; }

        public CommonPagedList<DirectoryFlights> Result { get; set; }
        public int PageNumber { get; set; }

        public int? PageSize { get; set; }

        public ICollection<TypeOfTransport> TypesOfTransports { get; set; }

        public ICollection<City> Cities { get; set; }

        public ICollection<YearOfRest> Years { get; set; }

        public ICollection<StateMachineState> States { get; set; }
        public bool ContractFiltered { get; set; }
    }
}
