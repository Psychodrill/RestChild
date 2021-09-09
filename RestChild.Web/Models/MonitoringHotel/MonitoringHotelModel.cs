using System.Collections;
using System.Collections.Generic;
using RestChild.Domain;

namespace RestChild.Web.Models.MonitoringHotel
{
    /// <summary>
    ///     Модель объекта отдыха
    /// </summary>
    public class MonitoringHotelModel : ViewModelBase<Domain.MonitoringHotel>
    {
        public ViewModelState State { get; set; }

        public string StateMachineActionString { get; set; }

        public bool IsEditable { get; set; }

        public MonitoringHotelModel() : base(new Domain.MonitoringHotel())
        {
        }

        public MonitoringHotelModel(Domain.MonitoringHotel data) : base(data)
        {
        }

        /// <summary>
        ///     Коллекция регионов
        /// </summary>
        public IDictionary<long, string> Districts { get; set; }

        /// <summary>
        ///     Идентификатор региона
        /// </summary>
        public long? DistrictId { get; set; }
    }
}
