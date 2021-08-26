using System.Collections.Generic;
using System.Runtime.Serialization;

namespace RestChild.Comon.Dto.SearchRestChild
{
    [DataContract]
    public class SearchIndexResult
    {
        [DataMember] public ICollection<IndexRestChildDto> ResManPage { get; set; }

        [DataMember] public int TotalRestManCount { get; set; }
    }
}
