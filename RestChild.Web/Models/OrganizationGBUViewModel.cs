using RestChild.Domain;

namespace RestChild.Web.Models
{
    /// <summary>
    ///     Модель ГБУ
    /// </summary>
    public class OrganizationGBUViewModel : ViewModelBase<MonitoringGBU>
    {
        public OrganizationGBUViewModel() : this(new MonitoringGBU())
        {
            
        }

        public OrganizationGBUViewModel(MonitoringGBU data) : base(data)
        {
        }
    }
}
