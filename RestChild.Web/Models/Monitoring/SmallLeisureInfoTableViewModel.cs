using RestChild.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestChild.Web.Models.Monitoring
{
    /// <summary>
    ///     Данные для построения таблицы о малых формах досуга
    /// </summary>
    public class SmallLeisureInfoTableViewModel
    {
        /// <summary>
        ///     Сокращенное название учреждения
        /// </summary>
        public string ShortName { get; set; }
        /// <summary>
        ///     Коллекция данных о малых формах досуга
        /// </summary>
        public List<MonitoringSmallLeisureInfoData> MonitoringSmallLeisureInfoDatas { get; set; }
    }
}
