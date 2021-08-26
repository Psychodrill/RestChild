using System;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Xml.Serialization;
using RestChild.Comon;
using RestChild.DAL;
using RestChild.Domain;

namespace MailingDemon.Tasks
{
	/// <summary>
	/// Задача на отправку на проверку заявок в БР
	/// </summary>
	[Task]
	public class CheckRequestInBaseRegistry : BaseTask
	{
        [XmlElement("config")]
        public ConfigBalance Config { get; set; }

		protected override void Execute()
		{
			using (var unitOfWork = new UnitOfWork())
			{
				Logger.Info("CheckRequestInBaseRegistry started");

				SendBenefit(unitOfWork);

                Logger.Info("CheckRequestInBaseRegistry finished");
			}
		}

        /// <summary>
        /// проверка льготы
        /// </summary>
        private void SendBenefit(UnitOfWork unitOfWork)
        {
            var query = unitOfWork.GetSet<Request>()
                .Where(r => r.NeedSendForBenefit && !r.IsDeleted);

            if (Config != null && Config.CountNodes > 0)
            {
                query = query.Where(e => e.Id % Config.CountNodes == Config.IndexNode);
            }

            var benefitIds =
                query.Select(o=>o.Id).OrderBy(o => o)
                    .Take(10000)
                    .ToList();
            foreach (var r in benefitIds)
            {
                try
                {
                    WebRequest req =
                        WebRequest.Create(ConfigurationManager.AppSettings["CheckRequestInBaseRegistryBenefit"] + r);
                    using (var res = req.GetResponse())
                    {
                        res.Close();
                    }
                }
                catch (Exception ex)
                {
                    Logger.Error("Error benefit in CheckRequestInBaseRegistry", ex);
                }
            }
        }
    }
}
