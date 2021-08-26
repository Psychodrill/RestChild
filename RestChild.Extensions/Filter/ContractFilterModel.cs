using System;
using System.Collections.Generic;
using RestChild.Domain;

namespace RestChild.Extensions.Filter
{
    public class ContractFilterModel
    {
        public ContractFilterModel()
        {
            PageNumber = 1;
        }

        public string SignNumber { get; set; }
        public DateTime? SignDate { get; set; }
        public long? OrganizationId { get; set; }
        public long? YearOfRestId { get; set; }
        public long? StateId { get; set; }
        public CommonPagedList<Contract> Result { get; set; }
        public int PageNumber { get; set; }
        public ICollection<StateMachineState> States { get; set; }
        public ICollection<Organization> Oivs { get; set; }
        public ICollection<YearOfRest> Years { get; set; }
    }
}
