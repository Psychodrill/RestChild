using System.Collections.Generic;
using RestChild.Domain;

namespace RestChild.Web.Models
{
    public class OrganizationCampViewModel : ViewModelBase<Domain.MonitoringHotel>
    {
        public OrganizationCampViewModel() : this(new Domain.MonitoringHotel())
        {

        }

        public OrganizationCampViewModel(Domain.MonitoringHotel data) : base(data)
        {
        }

        /// <summary>
        ///     Список регионов
        /// </summary>
        public ICollection<StateDistrict> Regions { get; set; }
    }
}
