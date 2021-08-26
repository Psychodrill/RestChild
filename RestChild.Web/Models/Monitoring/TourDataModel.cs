using RestChild.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestChild.Web.Models.Monitoring
{
    /// <summary>
    ///     Модель cведений об организации отдыха и оздоравления
    /// </summary>
    public class TourDataModel : ViewModelBase<MonitoringTourData>
    {
        public TourDataModel() : this(new MonitoringTourData())
        {

        }

        public TourDataModel(MonitoringTourData model) : base(model)
        {
        }

        public override MonitoringTourData BuildData()
        {
            return base.BuildData();
        }
    }
}
