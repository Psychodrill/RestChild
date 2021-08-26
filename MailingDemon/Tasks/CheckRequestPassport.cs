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
    ///     Задача на отправку на проверку заявок в БР
    /// </summary>
    [Task]
    public class CheckRequestPassport : BaseTask
    {
        [XmlElement("config")] public ConfigBalance Config { get; set; }

        protected override void Execute()
        {
            using (var unitOfWork = new UnitOfWork())
            {
                Logger.Info("CheckRequestInBaseRegistry started");

                SendPassport(unitOfWork);

                Logger.Info("CheckRequestInBaseRegistry finished");
            }
        }

        /// <summary>
        ///     отправка проверки паспорта
        /// </summary>
        private void SendPassport(UnitOfWork unitOfWork)
        {
            var query = unitOfWork.GetSet<Request>()
                .Where(r => r.NeedSendForPassport && !r.IsDeleted && !r.NeedSendForBenefit && !r.NeedSendToRelative &&
                            !r.NeedSendForSnils);

            if (Config != null && Config.CountNodes > 0)
            {
                query = query.Where(e => e.Id % Config.CountNodes == Config.IndexNode);
            }

            var relativeIds =
                query.Select(o => o.Id).OrderBy(o => o)
                    .Take(10000)
                    .ToList();

            foreach (var r in relativeIds)
            {
                try
                {
                    var req =
                        WebRequest.Create(ConfigurationManager.AppSettings["CheckRequestInBaseRegistryPassport"] + r);
                    using (var res = req.GetResponse())
                    {
                        res.Close();
                    }
                }
                catch (Exception ex)
                {
                    Logger.Error("Error passport in CheckRequestInBaseRegistry", ex);
                }
            }
        }
    }
}
