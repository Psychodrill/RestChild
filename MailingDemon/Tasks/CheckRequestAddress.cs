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
    ///     отправка запросов адреса
    /// </summary>
    [Task]
    public class CheckRequestAddress : BaseTask
    {
        [XmlElement("config")] public ConfigBalance Config { get; set; }

        protected override void Execute()
        {
            using (var unitOfWork = new UnitOfWork())
            {
                Logger.Info("CheckRequestAddress started");

                SendRelative(unitOfWork);

                Logger.Info("CheckRequestAddress finished");
            }
        }

        /// <summary>
        ///     отправка проверки адреса
        /// </summary>
        /// <param name="unitOfWork"></param>
        private void SendRelative(UnitOfWork unitOfWork)
        {
            var query = unitOfWork.GetSet<Request>()
                .Where(r => !r.NeedSendForPassport && !r.IsDeleted && !r.NeedSendForBenefit && !r.NeedSendToRelative &&
                            !r.NeedSendForSnils &&
                            r.NeedSendForRegistrationByPassport);

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
                        WebRequest.Create(ConfigurationManager.AppSettings["CheckRequestAddress"] + r);
                    using (var res = req.GetResponse())
                    {
                        res.Close();
                    }
                }
                catch (Exception ex)
                {
                    Logger.Error("Error in CheckRequestAddress", ex);
                }
            }
        }
    }
}
