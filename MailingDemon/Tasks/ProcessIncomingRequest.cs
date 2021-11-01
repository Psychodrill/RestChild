using System;
using System.Collections.Generic;
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
	///     парсинг входящих заявок.
	/// </summary>
	[Task]
	public class ProcessIncomingRequest : BaseTask
	{

		[XmlElement("config")]
		public ConfigBalance Config { get; set; }

		protected override void Execute()
		{
            //Костыль для запуска компании по таймингу 2021 год, кампания 2022 год
            //DateTime sdate = new DateTime();
            //var tp = DateTime.TryParse("02/11/2021 10:00:00", out sdate);
            //if (DateTime.Now > sdate)
                using (var unitOfWork = new UnitOfWork())
			{
				Logger.Info("ProcessIncomingRequest started");

				var qname1 = ConfigurationManager.AppSettings["MqRequestIncoming"];

				var query = unitOfWork.GetSet<ExchangeUTS>()
					.Where(e => e.Incoming && !e.Processed && !e.IsError && e.Id%Config.CountNodes == Config.IndexNode && e.QueueName == qname1)
					.Select(e => e.Id);

				List<long> ids = query.ToList();

				foreach (long id in ids)
				{
					try
					{
						WebRequest req = WebRequest.Create(ConfigurationManager.AppSettings["processRequests"] + id);
						using (var res = req.GetResponse())
						{
							res.Close();
						}
					}
					catch (Exception ex)
					{
						Logger.Error("ProcessIncomingRequest error", ex);
					}

				}

				Logger.Info("ProcessIncomingRequest finished");

				Logger.Info("ProcessIncomingRequest statuses started");
				var qname2 = ConfigurationManager.AppSettings["MqRequestStatusIncoming"];

				query = unitOfWork.GetSet<ExchangeUTS>()
					.Where(e => e.Incoming && !e.Processed && !e.IsError && e.Id % Config.CountNodes == Config.IndexNode && e.QueueName == qname2)
					.Select(e => e.Id);

				ids = query.ToList();

				foreach (long id in ids)
				{
					try
					{
						WebRequest req = WebRequest.Create(ConfigurationManager.AppSettings["processStatus"] + id);
						using (var res = req.GetResponse())
						{
							res.Close();
						}
					}
					catch (Exception ex)
					{
						Logger.Error("ProcessIncomingRequest status error", ex);
					}

				}

				Logger.Info("ProcessIncomingRequest status finished");
			}
		}
	}
}
