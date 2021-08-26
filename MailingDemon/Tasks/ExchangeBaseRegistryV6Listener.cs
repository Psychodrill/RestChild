using System;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using IBM.WMQ;
using RestChild.Comon;
using RestChild.Domain;
using RestChild.MPGUIntegration.V618;

namespace MailingDemon.Tasks
{
    /// <summary>
    ///     Базовый обмен с ЕТП v6.1 слушатель
    /// </summary>
    [Task]
    public class ExchangeBaseRegistryV6Listener : MqListener
    {
        /// <summary>
        /// сериализатор
        /// </summary>
        private static XmlSerializer serializer = new XmlSerializer(typeof (CoordinateSendTaskStatusesMessage), "http://asguf.mos.ru/rkis_gu/coordinate/v6_1/");

        /// <summary>
        ///     финальные статусы
        /// </summary>
        private readonly int[] finalStatuses =
            (ConfigurationManager.AppSettings["ExchangeBaseRegistryV6FinalStatuses"] ?? string.Empty).Split(',')
            .Select(ss => Convert.ToInt32(ss)).ToArray();

        private readonly bool SaveToExchangeUTC =
            string.Equals(ConfigurationManager.AppSettings["ExchangeBaseRegistryV6LogSaveEXT"], bool.TrueString,
                StringComparison.OrdinalIgnoreCase);

        /// <summary>
        ///     обмен с БР
        /// </summary>
        protected override void Execute()
        {
            GetMessagesFromUts(ConfigurationManager.AppSettings["MqBaseRegistryIncoming"]);
        }

        /// <summary>
        ///     Сохранение сообщения
        /// </summary>
        protected override void SaveMessage(MQQueueManager manager, MQMessage message, string queueNameGet, IUnitOfWork unitOfWork)
        {
            var messageData = message.ReadBytes(message.MessageLength);
            var msgTxt = Encoding.UTF8.GetString(messageData);

            var msgId = SafeGetStringProperty(message, "MessageId");
            var serNum = SafeGetStringProperty(message, "ServiceNumber");

            if (SaveToExchangeUTC)
            {
                unitOfWork.AddEntity(new ExchangeUTS
                {
                    Incoming = true,
                    Processed = true,
                    QueueName = queueNameGet,
                    MessageId = msgId,
                    ServiceNumber = serNum,
                    DateCreate = DateTime.Now,
                    Message = msgTxt
                });
            }

            try
            {
                var msg = Serialization.Deserialize<CoordinateSendTaskStatusesMessage>(msgTxt, serializer);
                if (finalStatuses.Contains(msg?.CoordinateTaskStatusDataMessage?.Status?.StatusCode ?? 0))
                {
                    var tid = msg.CoordinateTaskStatusDataMessage.TaskId;
                    var br = unitOfWork.GetSet<RestChild.Domain.ExchangeBaseRegistry>()
                        .FirstOrDefault(ss => ss.RequestGuid == tid.ToLower());
                    if (br != null)
                    {
                        Logger.Info($"ExchangeBaseRegistry find id={br.Id}");
                        br.ResponseDate = DateTime.Now;
                        br.ResponseGuid = msg.CoordinateTaskStatusDataMessage.MessageId;
                        br.ResponseText = msgTxt;
                    }
                    else
                    {
                        Logger.Info($"ExchangeBaseRegistry not found for taskid={tid}");
                    }
                }
                else
                {
                    Logger.Info($"Message getted but status not from list statusId={msg?.CoordinateTaskStatusDataMessage?.Status?.StatusCode}, taskId={msg?.CoordinateTaskStatusDataMessage?.TaskId}");
                }
            }
            catch (Exception mqe)
            {
                Logger.Error($"ExchangeBaseRegistryV6Listener error: {mqe.Message}", mqe);
            }

            unitOfWork.SaveChanges();
            manager.Commit();
            message.ClearMessage();
        }
    }
}
