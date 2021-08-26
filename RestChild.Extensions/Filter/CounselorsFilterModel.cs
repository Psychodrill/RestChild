using System.Collections.Generic;
using RestChild.Domain;

namespace RestChild.Extensions.Filter
{
    public class CounselorsFilterModel
    {
        public CounselorsFilterModel()
        {
            PageNumber = 1;
        }

        public string Name { get; set; }
        public int? AgeFrom { get; set; }
        public int? AgeTo { get; set; }
        public bool? IsMale { get; set; }
        public long? StateId { get; set; }
        public CommonPagedList<Counselors> Result { get; set; }
        public int PageNumber { get; set; }
        public bool OnlyVacant { get; set; }
        public long? VacantForPartyId { get; set; }
        public long? VacantForBoutId { get; set; }
        public string AddButtonClass { get; set; }
        public long? PedPartyId { get; set; }
        public string PedPartyName { get; set; }
        public ICollection<StateMachineState> States { get; set; }
    }
}
