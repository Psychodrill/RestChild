using System;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Xml.Serialization;
using RestChild.DAL;
using RestChild.Domain;

namespace MailingDemon.Tasks
{
    /// <summary>
    /// Задача на отправку на проверку заявок в БР
    /// </summary>
    [Task]
    public class CheckRequestRelative : BaseTask
    {
        [XmlElement("config")]
        public ConfigBalance Config { get; set; }

        protected override void Execute()
        {
            using (var unitOfWork = new UnitOfWork())
            {
                Logger.Info("CheckRequestInBaseRegistry started");

                SendRelative(unitOfWork);

                Logger.Info("CheckRequestInBaseRegistry finished");
            }
        }

        /// <summary>
        /// отправка проверки родственных связей
        /// </summary>
        /// <param name="unitOfWork"></param>
        private void SendRelative(UnitOfWork unitOfWork)
        {
            var query = unitOfWork.GetSet<Request>()
                .Where(r => r.NeedSendToRelative && !r.IsDeleted && !r.NeedSendForBenefit);

            if (Config != null && Config.CountNodes > 0)
            {
                query = query.Where(e => e.Id % Config.CountNodes == Config.IndexNode);
            }

            var relativeIds =
                query.Select(o=>o.Id).OrderBy(o => o)
                    .Take(10000)
                    .ToList();

            foreach (var r in relativeIds)
            {
                try
                {
                    WebRequest req =
                        WebRequest.Create(ConfigurationManager.AppSettings["CheckRequestInBaseRegistryRelatives"] + r);
                    using (var res = req.GetResponse())
                    {
                        res.Close();
                    }
                }
                catch (Exception ex)
                {
                    Logger.Error("Error relative in CheckRequestInBaseRegistry", ex);
                }
            }
        }
    }
}
