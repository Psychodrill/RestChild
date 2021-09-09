using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RestChild.Extensions.Filter;
using RestChild.Web.Models.MonitoringHotel;

namespace RestChild.Web.Models.MonitoringHotel
{
    /// <summary>
    ///     Фильтр для объектов отдыха
    /// </summary>
    public class MonitoringHotelFilterModel : BaseFilterModel<Domain.MonitoringHotel>
    {
        /// <summary>
        ///     Название
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     ИНН
        /// </summary>
        public string INN { get; set; }
    }
}
