using RestChild.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestChild.Web.Models.Monitoring
{
    /// <summary>
    ///     Модель данных формы малых форм досуга
    /// </summary>
    public class SmallLeisureInfoGBUModel : ViewModelBase<MonitoringSmallLeisureInfoGBU>
    {
        public SmallLeisureInfoGBUModel() : this(new MonitoringSmallLeisureInfoGBU())
        {
        }

        public SmallLeisureInfoGBUModel(MonitoringSmallLeisureInfoGBU data) : base(data)
        {

        }

        public override MonitoringSmallLeisureInfoGBU BuildData()
        {
            return base.BuildData();
        }
    }
}
