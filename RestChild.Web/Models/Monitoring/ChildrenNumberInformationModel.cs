using RestChild.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestChild.Web.Models.Monitoring
{
    /// <summary>
    ///     Модель формы сведений о численности детей
    /// </summary>
    public class ChildrenNumberInformationModel : ViewModelBase<MonitoringChildrenNumberInformation>
    {
        public ChildrenNumberInformationModel() : this(new MonitoringChildrenNumberInformation())
        {
        }

        public ChildrenNumberInformationModel(MonitoringChildrenNumberInformation data) : base(data)
        {
            Files = data.LinkToFiles?.Files?.ToDictionary(ss => ss.Id.ToString(), sx => sx) ?? new Dictionary<string, FileOrLink>();
            HotelDatas = data?.HotelDatas?.ToDictionary(sx => sx.Id.ToString(), sy => new HotelDataModel(sy)) ?? new Dictionary<string, HotelDataModel>();
        }

        public override MonitoringChildrenNumberInformation BuildData()
        {
            var res = base.BuildData();
            res.HotelDatas = HotelDatas?.Values.Select(ss => ss.BuildData()).ToList() ?? new List<MonitoringHotelData>();
            res.LinkToFiles = res.LinkToFilesId.HasValue ? new LinkToFile { Id = res.LinkToFilesId.Value } : new LinkToFile();
            res.LinkToFiles.Files = Files?.Where(ss => !string.IsNullOrWhiteSpace(ss.Key)).Select(ss => ss.Value).ToList() ?? new List<FileOrLink>();
            return res;
        }

        /// <summary>
        ///     Сведения об организации отдыха и оздоравления
        /// </summary>
        public IDictionary<string, HotelDataModel> HotelDatas { get; set; }

        /// <summary>
        ///     Статус
        /// </summary>
        public ViewModelState State { get; set; }

        /// <summary>
        ///     Статус в который будет переведено
        /// </summary>
        public string StateMachineActionString { get; set; }

        /// <summary>
        ///     Возможные года компании
        /// </summary>
        public IList<YearOfRest> YearsOfRest { get; set; }

        /// <summary>
        ///     Возможные организации
        /// </summary>
        public IList<Organization> Organisations { get; set; }

        /// <summary>
        ///     Файлы
        /// </summary>
        public Dictionary<string, FileOrLink> Files { get; set; }
    }
}
