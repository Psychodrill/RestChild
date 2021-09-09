using System;
using System.Collections;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Web;
using System.Text;
using System.Threading;
using System.Xml.Linq;
using IBM.WMQ;
using RestChild.Comon;
using RestChild.DAL;
using RestChild.Domain;
using RestChild.Security.Logger;
using UtilsSmev;

namespace MailingDemon.Tasks
{
    /// <summary>
    ///    Базовый обмен с ЕТП v6.1 отправитель
    /// </summary>
    [Task]
    public class ExchangeBaseRegistryV6 : BaseTask
    {
        protected const int NumberOfMessages = 10000;

        private readonly string _certificate = ConfigurationManager.AppSettings["clientCertificate"] ?? string.Empty;

        private readonly X509FindType _findType = (X509FindType) Enum.Parse(typeof(X509FindType),
            ConfigurationManager.AppSettings["clientCertFindType"], true);

        private readonly string _pinCode = ConfigurationManager.AppSettings["clientCertPinCode"] ?? string.Empty;

        private readonly bool _saveToExchangeUtc =
            string.Equals(ConfigurationManager.AppSettings["ExchangeBaseRegistryV6LogSaveEXT"], bool.TrueString,
                StringComparison.OrdinalIgnoreCase);

        private readonly StoreLocation _storeLocation = (StoreLocation) Enum.Parse(typeof(StoreLocation),
            ConfigurationManager.AppSettings["clientCertStoreLocation"], true);

        private readonly StoreName _storeName = (StoreName) Enum.Parse(typeof(StoreName),
            ConfigurationManager.AppSettings["clientCertStoreName"], true);

        /// <summary>
        ///    Экспорт данных в БР
        /// </summary>
        protected override void Execute()
        {
            using (var unitOfWork = new UnitOfWork())
            {
                try
                {
                    Logger.Info("ExchangeBaseRegistryV6 started");
                    Logger.Info("ExchangeBaseRegistryV6 geting msg");

                    // отправлять обмен в очереди
                    var types = unitOfWork.GetSet<ExchangeBaseRegistryType>()
                        .Where(t => !t.IsDeleted && t.SendMessage).Select(t => t.Id).ToList().Select(t => (long?) t)
                        .ToArray();

                    var ebr10209 = unitOfWork.GetSet<RestChild.Domain.ExchangeBaseRegistry>().Where(ss =>
                            !ss.IsProcessed && !ss.NotActual && !ss.IsIncoming && !ss.SendDate.HasValue &&
                            types.Contains(ss.ExchangeBaseRegistryTypeId))
                        .OrderBy(ss => ss.Id).Take(NumberOfMessages).ToArray();

                    Logger.Info("ExchangeBaseRegistryV6 sending msg");

                    PutMessage(unitOfWork, ConfigurationManager.AppSettings["MqBaseRegistryOutcoming"], ebr10209);

                    Logger.Info("ExchangeBaseRegistryV6 sending complete");

                    Logger.Info("ExchangeBaseRegistryV6 finished");
                }
                catch (Exception ex)
                {
                    Logger.Error("Ошибка работы ExchangeBaseRegistryV6", ex);
                }
            }
        }

        /// <summary>
        ///    положить сообщение в очередь
        /// </summary>
        protected void PutMessage(IUnitOfWork unitOfWork, string queueName,
            RestChild.Domain.ExchangeBaseRegistry[] messages, int? msgDelay = null)
        {
            try
            {
                var store = new X509Store(_storeName, _storeLocation);
                store.Open(OpenFlags.ReadOnly);
                //сертификат
                var coll = store.Certificates.Find(_findType, _certificate, true);
                if (coll.Count == 0)
                    throw new FileNotFoundException(
                        $"Сертификат клиента не найден. Отпечаток {_certificate}");

                var serviceCert = coll[0];

                // mq properties
                var properties = new Hashtable
                {
                    {MQC.CONNECT_OPTIONS_PROPERTY, MQC.MQCNO_RECONNECT},
                    {MQC.CONNECTION_NAME_PROPERTY, ConfigurationManager.AppSettings["MqConnectionName"]},
                    {MQC.CHANNEL_PROPERTY, ConfigurationManager.AppSettings["MqChanelProperty"]}
                };

                if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["MqUser"]))
                {
                    properties.Add(MQC.USER_ID_PROPERTY, ConfigurationManager.AppSettings["MqUser"]);
                    properties.Add(MQC.PASSWORD_PROPERTY, ConfigurationManager.AppSettings["MqPassword"]);
                }

                // create connection
                Logger.Info("Connecting to queue manager.. ");
                var dateTime = DateTime.Now;

                using (var queueManager = new MQQueueManager(ConfigurationManager.AppSettings["MqQueueMangerName"],
                    properties))
                {
                    Logger.Info($"Connecting to queue manager.. done ({DateTime.Now - dateTime})");
                    dateTime = DateTime.Now;
                    double average = 0;
                    using (var queue = queueManager.AccessQueue(queueName, MQC.MQOO_OUTPUT + MQC.MQOO_FAIL_IF_QUIESCING))
                    {
                        Logger.Info($"AccessQueue done ({DateTime.Now - dateTime})");
                        foreach (var exchangeBr in messages)
                        {
                            var dateTimeOperation = DateTime.Now;
                            dateTime = DateTime.Now;
                            var message = new MQMessage {CharacterSet = 1208};

                            var xDocument = XDocument.Parse(exchangeBr.RequestText);
                            var sb = new StringBuilder();
                            using (var sw = new StringWriterUtf8(sb))
                            {
                                xDocument.Save(sw);
                            }

                            //подписываем сертификат
                            var msg = Signer.SignMessageTopCaseV6(
                                sb.ToString()
                                    .Replace(" xmlns=\"http://asguf.mos.ru/rkis_gu/coordinate/v6_1/\"", "")
                                    .Replace("<CoordinateTaskMessage ","<CoordinateTaskMessage xmlns=\"http://asguf.mos.ru/rkis_gu/coordinate/v6_1/\" "),
                                serviceCert, _storeLocation,
                                _pinCode);

                            if (_saveToExchangeUtc)
                                unitOfWork.AddEntity(new ExchangeUTS
                                {
                                    DateCreate = dateTime,
                                    DateToSend = dateTime,
                                    Incoming = false,
                                    ServiceNumber = exchangeBr.ServiceNumber,
                                    IsError = false,
                                    IsSigned = true,
                                    MessageId = exchangeBr.RequestGuid,
                                    Processed = true,
                                    QueueName = queueName,
                                    Message = msg
                                });

                            var bytes = Encoding.UTF8.GetBytes(msg);
                            message.Write(bytes);
                            // Задаем свойства сообщения
                            var putMessageOptions = new MQPutMessageOptions();
                            putMessageOptions.Options += MQC.MQPMO_SYNC_RESPONSE;
                            Logger.Info($"Prepare message done ({DateTime.Now - dateTime})");
                            dateTime = DateTime.Now;

                            // помещаем сообщение в очередь
                            queue.Put(message, putMessageOptions);
                            Logger.Info($"Put message done ({DateTime.Now - dateTime})");
                            SecurityLogger.AddToLog(unitOfWork,
                                SecurityJournalEventType.OutSystemsInteractions, "Взаимодействия с ЕТП",
                                $"PutMessages (отправка сообщения в очередь ({queueName}))", "",
                                WebOperationContext.Current?.IncomingRequest.UserAgent);
                            dateTime = DateTime.Now;

                            Logger.Info($"Release booking done ({DateTime.Now - dateTime})");
                            dateTime = DateTime.Now;

                            exchangeBr.SendDate = DateTime.Now;

                            unitOfWork.SaveChanges();
                            Logger.Info($"Save changes done ({DateTime.Now - dateTime})");

                            average += (DateTime.Now - dateTimeOperation).TotalMilliseconds;

                            //задержка отправки сообщений
                            if (msgDelay.HasValue && msgDelay.Value > 0)
                                new ManualResetEvent(false).WaitOne(msgDelay.Value);
                        }

                        dateTime = DateTime.Now;
                        queue.Close();
                    }

                    Logger.Info($"Close queue done ({DateTime.Now - dateTime})");

                    if (messages.Length > 0)
                        Logger.Info($"Average one message ({average / messages.Length}) Milliseconds");

                    // disconnecting queue manager
                    Logger.Info("Disconnecting queue manager.. ");
                    dateTime = DateTime.Now;
                    queueManager.Disconnect();
                    Logger.Info($"Disconnecting queue manager.. done ({DateTime.Now - dateTime})");
                    queueManager.Close();
                }
            }
            catch (MQException mqe)
            {
                Logger.Error($"MQException caught: {mqe.ReasonCode} - {mqe.Message}", mqe);
            }
        }
    }
}
