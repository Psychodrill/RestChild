using RestChild.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestChild.Web.Models.Monitoring
{
    /// <summary>
    ///     Модель организации отдыха сведений о численности детей
    /// </summary>
    public class HotelDataModel : ViewModelBase<MonitoringHotelData>
    {
        public HotelDataModel() : this(new MonitoringHotelData())
        {
        }

        public HotelDataModel(MonitoringHotelData data) : base(data)
        {
            Tours = data?.TourDatas?.ToDictionary(sx => sx.Id.ToString(), sy => new TourDataModel(sy)) ?? new Dictionary<string, TourDataModel>();
            RegionId = data?.Hotel?.RegionId;
            RegionName = data?.Hotel?.Region?.Name;
            Prefix = data?.Id.ToString();
        }

        public override MonitoringHotelData BuildData()
        {
            var res = base.BuildData();
            res.TourDatas = Tours?.Values.Select(ss => ss.BuildData()).ToList() ?? new List<MonitoringTourData>();
            return res;
        }

        /// <summary>
        ///     Сведения о турах (Заездах)
        /// </summary>
        public IDictionary<string, TourDataModel> Tours { get; set; }

        /// <summary>
        ///     Идентификатор выбора региона отдыха
        /// </summary>
        public long? RegionId { get; set; }

        /// <summary>
        ///     Название региона отдыха
        /// </summary>
        public string RegionName { get; set; }

        /// <summary>
        ///     Префикс в коллекции
        /// </summary>
        public string Prefix { get; set; }
    }
}
