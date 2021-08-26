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
    public class CheckRequestSnils : BaseTask
	{
        [XmlElement("config")]
        public ConfigBalance Config { get; set; }
		protected override void Execute()
		{
			using (var unitOfWork = new UnitOfWork())
			{
				Logger.Info("CheckRequestInBaseRegistry started");

                SendSnils(unitOfWork);

                Logger.Info("CheckRequestInBaseRegistry finished");
			}
		}

        /// <summary>
        /// отправка проверки СНИЛС
        /// </summary>
        private void SendSnils(UnitOfWork unitOfWork)
        {
            var query = unitOfWork.GetSet<Request>()
                .Where(r => r.NeedSendForSnils && !r.IsDeleted && !r.NeedSendForBenefit && !r.NeedSendToRelative);

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
                        WebRequest.Create(ConfigurationManager.AppSettings["CheckRequestInBaseRegistrySnils"] + r);
                    using (var res = req.GetResponse())
                    {
                        res.Close();
                    }
                }
                catch (Exception ex)
                {
                    Logger.Error("Error snils in CheckRequestInBaseRegistry", ex);
                }
            }
        }
    }
}
