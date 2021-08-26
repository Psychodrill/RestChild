using RestChild.Domain;

namespace RestChild.Extensions.Filter
{
    public class RequestFilterModel : RequestSearchFilterModel
    {
        public CommonPagedList<Request> Requests { get; set; }
    }
}
