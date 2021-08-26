using RestChild.Comon.Dto.SearchRestChild;

namespace RestChild.Extensions.Filter
{
    public class RestManFilterModel : RequestSearchFilterModel
    {
        public CommonPagedList<FullIndexRestChildDto> Records { get; set; }
    }
}
