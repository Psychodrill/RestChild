using RestChild.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestChild.Web.Models.Monitoring
{
    /// <summary>
    ///     Модель формы о финансировании
    /// </summary>
    public class FinancialInformationModel : ViewModelBase<MonitoringFinancialInformation>
    {
        public FinancialInformationModel() : this(new MonitoringFinancialInformation())
        {
        }

        public FinancialInformationModel(MonitoringFinancialInformation data) : base(data)
        {
            Files = data.LinkToFiles?.Files?.ToDictionary(ss => ss.Id.ToString(), sx => sx) ?? new Dictionary<string, FileOrLink>();
            FinancialData = data.FinantialDatas?.ToDictionary(ss => ss.Id.ToString(), sy => sy) ?? new Dictionary<string, MonitoringFinancialData>();
        }

        public override MonitoringFinancialInformation BuildData()
        {
            var res = base.BuildData();
            res.FinantialDatas = FinancialData?.Values.Select(ss => ss).ToList() ?? new List<MonitoringFinancialData>();
            res.LinkToFiles = res.LinkToFilesId.HasValue ? new LinkToFile { Id = res.LinkToFilesId.Value } : new LinkToFile();
            res.LinkToFiles.Files = Files?.Where(ss => !string.IsNullOrWhiteSpace(ss.Key)).Select(ss => ss.Value).ToList() ?? new List<FileOrLink>();
            return res;
        }

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
        ///     Список данных по финансированию
        /// </summary>
        public IDictionary<string, MonitoringFinancialData> FinancialData { get; set; }

        /// <summary>
        ///     Файлы
        /// </summary>
        public Dictionary<string, FileOrLink> Files { get; set; }
    }
}
