using System;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Threading;
using System.Xml.Serialization;
using RestChild.Comon;
using RestChild.DAL;
using RestChild.Domain;

namespace MailingDemon.Tasks
{
	[Task]
	public class ExchangeBaseRegistry : BaseTask
	{
		[XmlElement("config")]
		public ExchangeBaseRegistryConfig Config { get; set; }

		protected override void Execute()
		{
			using (var unitOfWork = new UnitOfWork())
			{
				Logger.Info("ExchangeBaseRegistry started");

				var secondQuery = unitOfWork.GetSet<RestChild.Domain.ExchangeBaseRegistry>()
					.Where(b => !string.IsNullOrEmpty(b.ResponseGuid) && !b.IsProcessed && !b.IsIncoming && !b.NotActual);
                    secondQuery = secondQuery.Where(s => s.Child == null || (s.Child.Request != null &&
                                                                              !s.Child.Request.NeedSendForBenefit &&
                                                                              !s.Child.Request.NeedSendToRelative &&
                                                                              !s.Child.Request.NeedSendForParent &&
                                                                              !s.Child.Request.NeedSendForPassport &&
                                                                              !s.Child.Request.NeedSendForSnils &&
                                                                              !s.Child.Request.NeedSendForCPMPK &&
                                                                              !s.Child.Request
                                                                                  .NeedSendForAisoLegalRepresentation &&
                                                                              !s.Child.Request
                                                                                  .NeedSendForRegistrationByPassport));

				if (Config != null)
				{
					if (Config.DocType != 0)
					{
						secondQuery = secondQuery.Where(b => b.ExchangeBaseRegistryTypeId == Config.DocType);
					}
				}

				var messageToProcess = secondQuery.Select(b => b.Id).ToList();
                Logger.Info($"ExchangeBaseRegistry count={messageToProcess.Count}");
				foreach (var id in messageToProcess)
				{
					try
					{
						WebRequest req =
							WebRequest.Create(ConfigurationManager.AppSettings["sendAcknowledgementUrl"] + id);
						using (var res = req.GetResponse())
						{
							res.Close();
						}

                        Logger.Info($"ExchangeBaseRegistry sendAcknowledgementUrl={id}");
                    }
					catch (Exception ex)
					{
						Logger.Error(ex);
					}
				}

				Logger.Info("ExchangeBaseRegistry finished");
			}
		}
	}
}
