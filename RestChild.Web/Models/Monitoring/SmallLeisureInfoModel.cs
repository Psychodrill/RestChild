using RestChild.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestChild.Web.Models.Monitoring
{
    /// <summary>
    ///     Модель формы малых форм досуга
    /// </summary>
    public class SmallLeisureInfoModel : ViewModelBase<MonitoringSmallLeisureInfo>
    {
        public SmallLeisureInfoModel() : this(new MonitoringSmallLeisureInfo())
        {

        }

        public SmallLeisureInfoModel(MonitoringSmallLeisureInfo data) : base(data)
        {
            SmallLeisureInfoGBUs = data.SmallLeisureInfoGBUs?.ToDictionary(ss => ss.Id.ToString(), sy => new SmallLeisureInfoGBUModel(sy)) ?? new Dictionary<string, SmallLeisureInfoGBUModel>();
            Files = data.LinkToFiles?.Files?.ToDictionary(ss => ss.Id.ToString(), sx => sx) ?? new Dictionary<string, FileOrLink>();
            Months = new Dictionary<int, string>() {
                { 1, "Январь" },
                { 2, "Февраль" },
                { 3, "Март" },
                { 4, "Апрель" },
                { 5, "Май" },
                { 6, "Июнь" },
                { 7, "Июль" },
                { 8, "Август" },
                { 9, "Сетнябрь" },
                { 10, "Октябрь" },
                { 11, "Ноябрь" },
                { 12, "Декабрь" },
            };
        }

        public override MonitoringSmallLeisureInfo BuildData()
        {
            var res = base.BuildData();
            res.SmallLeisureInfoGBUs = SmallLeisureInfoGBUs?.Values?.Select(ss => ss.BuildData()).ToList() ?? new List<MonitoringSmallLeisureInfoGBU>();
            res.LinkToFiles = res.LinkToFilesId.HasValue ? new LinkToFile { Id = res.LinkToFilesId.Value } : new LinkToFile();
            res.LinkToFiles.Files = Files?.Where(ss => !string.IsNullOrWhiteSpace(ss.Key)).Select(ss => ss.Value).ToList() ?? new List<FileOrLink>();

            return res;
        }

        /// <summary>
        ///     Сведения об ГБУ
        /// </summary>
        public IDictionary<string, SmallLeisureInfoGBUModel> SmallLeisureInfoGBUs { get; set; }

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
        ///     Возможные ГБУ
        /// </summary>
        public IList<MonitoringGBU> GBUs { get; set; }

        /// <summary>
        ///     Возможные месяци
        /// </summary>
        public IReadOnlyDictionary<int, string> Months { get; private set; }

        /// <summary>
        ///     Файлы
        /// </summary>
        public Dictionary<string, FileOrLink> Files { get; set; }
    }
}
