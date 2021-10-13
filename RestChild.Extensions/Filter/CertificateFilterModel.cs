using RestChild.Domain;

namespace RestChild.Extensions.Filter
{
    public class CertificateFilterModel : CertificateSearchFilterModel
    {
        public CommonPagedList<Certificate> Certificates { get; set; }
    }
}
