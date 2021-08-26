using System;
using System.Collections;
using System.Configuration;
using System.Linq;
using System.ServiceModel.Web;
using System.Text;
using System.Threading;
using System.Xml.Linq;
using IBM.WMQ;
using log4net;
using RestChild.Comon;
using RestChild.Comon.Dto.Booking;
using RestChild.Comon.Enumeration;
using RestChild.Comon.Exchange;
using RestChild.DAL;
using RestChild.Domain;
using RestChild.Security.Logger;
using Booking = RestChild.Booking.Logic.Booking;

namespace MailingDemon.Tasks
{
   /// <summary>
   ///    взаимодействие с очередями
   /// </summary>
   [Task]
   public class ScheduleWatcher : BaseTask
   {
      protected const int NumberOfMsgs = 10000;

      protected override void Execute()
      {
         using (var unitOfWork = new UnitOfWork())
         {
            Logger.Info("ScheduleWatcher started");

            PutMessages(unitOfWork, ConfigurationManager.AppSettings["MqRequestOutcoming"]);
            PutMessages(unitOfWork, ConfigurationManager.AppSettings["MqRequestStatus"]);
            PutErrorMessages(unitOfWork, ConfigurationManager.AppSettings["MqRequestIncoming"],
               ConfigurationManager.AppSettings["MqRequestIncomingError"]);

            Logger.Info("ScheduleWatcher finished");
         }
      }

      protected void PutErrorMessages(IUnitOfWork unitOfWork, string queueNameFilter, string queueName)
      {
         try
         {
            var fromCode = ConfigurationManager.AppSettings["exchangeBaseRegistryFromCode"];
            var toCode = ConfigurationManager.AppSettings["exchangeBaseRegistryToCode"];
            var errorCode = ConfigurationManager.AppSettings["errorStatusCode"].IntParse();
            var errorMessage = ConfigurationManager.AppSettings["errorStatusMessage"];

            // mq properties
            // mq properties
            var properties = new Hashtable
            {
               {MQC.CONNECT_OPTIONS_PROPERTY, MQC.MQCNO_RECONNECT},
               {MQC.CONNECTION_NAME_PROPERTY, ConfigurationManager.AppSettings["MqConnectionName"]},
               {MQC.CHANNEL_PROPERTY, ConfigurationManager.AppSettings["MqChanelProperty"]}
            };
            //Environment.SetEnvironmentVariable("MQCHLLIB", ApplicationBinPath);
            //Environment.SetEnvironmentVariable("MQCHLTAB", "AMQCLCHL.TAB");

            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["MqUser"]))
            {
               properties.Add(MQC.USER_ID_PROPERTY, ConfigurationManager.AppSettings["MqUser"]);
               properties.Add(MQC.PASSWORD_PROPERTY, ConfigurationManager.AppSettings["MqPassword"]);
            }

            // create connection
            Logger.Info("Connecting to queue manager.. (PutErrorMessages)");
            using (
               var queueManager = new MQQueueManager(ConfigurationManager.AppSettings["MqQueueMangerName"], properties))
            {
               Logger.Info("Connecting to queue manager.. done");

               var notSendStausesToMpgu = ConfigurationManager.AppSettings["NotSendStausesToMpgu"];

               var query = unitOfWork.GetSet<ExchangeUTS>()
                  .Where(e => e.Incoming && !e.Processed && e.IsError && e.QueueName == queueNameFilter);

               if (!string.IsNullOrWhiteSpace(notSendStausesToMpgu))
               {
                  var statuses =
                     notSendStausesToMpgu.Split(',').Select(s => s.LongParse()).Where(l => l.HasValue).ToArray();
                  if (statuses.Any())
                  {
                     query = query.Where(q => !q.ToState.HasValue || !statuses.Contains(q.ToState));
                  }
               }

               var messages = query.Take(NumberOfMsgs).ToList();

               foreach (var exchangeUts in messages)
               {
                  if (string.IsNullOrEmpty(exchangeUts.ServiceNumber) || !errorCode.HasValue)
                  {
                     Logger.Info("PutErrorMessages exchangeUts.Id=" + exchangeUts.Id);
                     using (var queue =
                        queueManager.AccessQueue(queueName, MQC.MQOO_OUTPUT + MQC.MQOO_FAIL_IF_QUIESCING))
                     {
                        // Создание сообщения и передача его в очередь:
                        // создаем сообщение
                        var message = new MQMessage {CharacterSet = 1208};
                        message.SetStringProperty("FromOrgCode", exchangeUts.FromOrgCode);
                        message.SetStringProperty("ToOrgCode", exchangeUts.ToOrgCode);
                        message.SetStringProperty("ServiceNumber", exchangeUts.ServiceNumber);
                        message.SetStringProperty("MessageId", exchangeUts.MessageId);
                        message.SetStringProperty("RequestDateTime",
                           exchangeUts.DateCreate.ToString("yyyy-MM-ddTHH:mm"));


                        var sb = new StringBuilder();
                        try
                        {
                           var xdoc = XDocument.Parse(exchangeUts.Message);
                           var sw = new StringWriterUtf8(sb);
                           xdoc.Save(sw);
                        }
                        catch
                        {
                           sb.Append(exchangeUts.Message);
                        }

                        var bytes = Encoding.UTF8.GetBytes(sb.ToString());
                        message.Write(bytes);

                        // Задачем свойства сообщения
                        var putMessageOptions = new MQPutMessageOptions();
                        putMessageOptions.Options += MQC.MQPMO_SYNC_RESPONSE;

                        // помещаем сообщение в очередь
                        queue.Put(message, putMessageOptions);
                        /*
                        SecurityLogger.AddToLog(unitOfWork, SecurityJournalEventType.OutSystemsInteractions,
                           "Взаимодействия с ЕТП", $"PutErrorMessages (отправка сообщения об ошибке ({queueName}))", "",
                           WebOperationContext.Current?.IncomingRequest.UserAgent);
                        */
                        exchangeUts.Processed = true;
                        unitOfWork.Update(exchangeUts);
                        unitOfWork.SaveChanges();
                        queue.Close();
                     }

                     Logger.Info("PutErrorMessages sended exchangeUts.Id=" + exchangeUts.Id);
                  }
                  else
                  {
                     var message = new StatusMessage
                     {
                        ServiceNumber = exchangeUts.ServiceNumber,
                        ResponseDate = DateTime.Now,
                        StatusCode = errorCode,
                        Note = errorMessage
                     };

                     unitOfWork.AddEntity(new ExchangeUTS
                     {
                        DateCreate = message.ResponseDate ?? DateTime.Now,
                        Incoming = false,
                        Message = Serialization.Serializer(message),
                        Processed = false,
                        FromOrgCode = fromCode,
                        ToOrgCode = toCode,
                        MessageId = Guid.NewGuid().ToString(),
                        QueueName = ConfigurationManager.AppSettings["MqRequestStatus"],
                        ServiceNumber = exchangeUts.ServiceNumber
                     });

                     exchangeUts.Processed = true;
                     unitOfWork.Update(exchangeUts);
                     unitOfWork.SaveChanges();
                  }
               }

               // disconnecting queue manager
               Logger.Info("Disconnecting queue manager.. ");
               queueManager.Disconnect();
               Logger.Info("Disconnecting queue manager.. done");
            }
         }
         catch (MQException mqe)
         {
            Logger.Error($"MQException caught: {mqe.ReasonCode} - {mqe.Message}", mqe);
         }
      }

      protected virtual void PutMessages(IUnitOfWork unitOfWork, string queueName)
      {
         var dateTime = DateTime.Now;
         var query = GetQuery(unitOfWork, queueName);

         var messages = query.OrderBy(s => s.Id).Take(NumberOfMsgs).ToArray();
         Logger.Info($"Query messages done ({DateTime.Now - dateTime})");
         PutMessages(unitOfWork, queueName, messages, Logger);
      }

      protected IQueryable<ExchangeUTS> GetQuery(IUnitOfWork unitOfWork, string queueName, string notSendStausesSetting = "NotSendStausesToMpgu")
      {
         var notSendStausesToMpgu = string.IsNullOrWhiteSpace(notSendStausesSetting) ? string.Empty : ConfigurationManager.AppSettings[notSendStausesSetting];

         var query = unitOfWork.GetSet<ExchangeUTS>().Where(e => !e.Incoming && !e.Processed && e.QueueName == queueName && e.IsSigned);

         if (!string.IsNullOrWhiteSpace(notSendStausesToMpgu))
         {
            var statuses = notSendStausesToMpgu.Split(',').Select(s => s.LongParse()).Where(l => l.HasValue).ToArray();
            if (statuses.Any())
            {
               query = query.Where(q => !q.ToState.HasValue || !statuses.Contains(q.ToState));
            }
         }

         // отправка только тех статусов по которым прошло время
         var date = DateTime.Now;
         query = query.Where(q => !q.DateToSend.HasValue || q.DateToSend < date);
         return query;
      }

      protected static void PutMessages(IUnitOfWork unitOfWork, string queueName, ExchangeUTS[] messages, ILog logger, int? msgDelay = null)
      {
         try
         {
            // mq properties
            var properties = new Hashtable
            {
               {MQC.CONNECT_OPTIONS_PROPERTY, MQC.MQCNO_RECONNECT},
               {MQC.CONNECTION_NAME_PROPERTY, ConfigurationManager.AppSettings["MqConnectionName"]},
               {MQC.CHANNEL_PROPERTY, ConfigurationManager.AppSettings["MqChanelProperty"]}
            };
            //Environment.SetEnvironmentVariable("MQCHLLIB", ApplicationBinPath);
            //Environment.SetEnvironmentVariable("MQCHLTAB", "AMQCLCHL.TAB");

            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["MqUser"]))
            {
               properties.Add(MQC.USER_ID_PROPERTY, ConfigurationManager.AppSettings["MqUser"]);
               properties.Add(MQC.PASSWORD_PROPERTY, ConfigurationManager.AppSettings["MqPassword"]);
            }

            // create connection
            logger.Info("Connecting to queue manager.. ");
            var dateTime = DateTime.Now;
            MQQueueManager queueManager;
            using (queueManager = new MQQueueManager(ConfigurationManager.AppSettings["MqQueueMangerName"], properties))
            {
               logger.Info($"Connecting to queue manager.. done ({DateTime.Now - dateTime})");
               dateTime = DateTime.Now;
               double average = 0;

               using (var queue = queueManager.AccessQueue(queueName, MQC.MQOO_OUTPUT + MQC.MQOO_FAIL_IF_QUIESCING))
               {
                  logger.Info($"AccessQueue done ({DateTime.Now - dateTime})");

                  foreach (var exchangeUts in messages)
                  {
                     var dateTimeOperation = DateTime.Now;
                     dateTime = DateTime.Now;
                     // Создание сообщения и передача его в очередь:
                     // создаем сообщение
                     var message = new MQMessage {CharacterSet = 1208};
                     message.SetStringProperty("FromOrgCode", exchangeUts.FromOrgCode);
                     message.SetStringProperty("ToOrgCode", exchangeUts.ToOrgCode);
                     message.SetStringProperty("ServiceNumber", exchangeUts.ServiceNumber);
                     message.SetStringProperty("MessageId", exchangeUts.MessageId);
                     message.SetStringProperty("RequestDateTime", exchangeUts.DateCreate.ToString("yyyy-MM-ddTHH:mm"));

                     var xdoc = XDocument.Parse(exchangeUts.Message);
                     var sb = new StringBuilder();
                     using (var sw = new StringWriterUtf8(sb))
                     {
                        xdoc.Save(sw);
                     }

                     var bytes = Encoding.UTF8.GetBytes(sb.ToString());
                     message.Write(bytes);
                     // Задачем свойства сообщения
                     var putMessageOptions = new MQPutMessageOptions();
                     putMessageOptions.Options += MQC.MQPMO_SYNC_RESPONSE;
                     logger.Info($"Prepare message done ({DateTime.Now - dateTime})");
                     dateTime = DateTime.Now;

                     // помещаем сообщение в очередь
                     queue.Put(message, putMessageOptions);
                     logger.Info($"Put message done ({DateTime.Now - dateTime})");
                     /*SecurityLogger.AddToLog(unitOfWork, SecurityJournalEventType.OutSystemsInteractions,
                        "Взаимодействия с ЕТП", $"PutMessages (отправка сообщения в очередь ({queueName}))", "",
                        WebOperationContext.Current?.IncomingRequest.UserAgent);*/
                     dateTime = DateTime.Now;

                     // отмена бронирования если нужно.
                     if (
                        ConfigurationManager.AppSettings["MqRequestStatus"] == queueName &&
                        exchangeUts.TypeOfRestId.HasValue && exchangeUts.BookingGuid.HasValue
                        &&
                        new long?[]
                        {
                           (long) StatusEnum.Reject, (long) StatusEnum.CancelByApplicant,
                           (long) StatusEnum.RegistrationDecline, 7708, 7739
                        }.Contains(exchangeUts.ToState))
                     {
                        try
                        {
                           var releaseRequest = new BookingRequest
                           {
                              TypeOfRestId = exchangeUts.TypeOfRestId ?? 0,
                              BookingGuid = exchangeUts.BookingGuid
                           };

                           var client = Booking.GetServiceClient(releaseRequest);
                           try
                           {
                              var res = client.ReleaseBooking(releaseRequest);
                              if (res.IsError)
                              {
                                 exchangeUts.IsErrorOnReleaseBooking = true;
                                 logger.ErrorFormat(
                                    "Не произошло снятие бронирования. BookingGuid={0}, MessageId={1}, Error={2}",
                                    exchangeUts.BookingGuid, exchangeUts.MessageId, res.ErrorMessage);
                              }
                           }
                           finally
                           {
                              Booking.CloseClient(client);
                           }
                        }
                        catch (Exception ex)
                        {
                           exchangeUts.IsErrorOnReleaseBooking = true;
                           logger.Error(
                              $"Не произошло снятие бронирования. BookingGuid={exchangeUts.BookingGuid}, MessageId={exchangeUts.MessageId}",
                              ex);
                        }
                     }

                     logger.Info($"Release booking done ({DateTime.Now - dateTime})");
                     dateTime = DateTime.Now;

                     exchangeUts.Processed = true;
                     unitOfWork.SaveChanges();
                     logger.Info($"Save changes done ({DateTime.Now - dateTime})");

                     average += (DateTime.Now - dateTimeOperation).TotalMilliseconds;

                     //задержка отправки сообщений
                     if (msgDelay.HasValue && msgDelay.Value > 0)
                     {
                        new ManualResetEvent(false).WaitOne(msgDelay.Value);
                        //System.Threading.Thread.Sleep(msgDelay.Value);
                     }
                  }

                  dateTime = DateTime.Now;
                  queue.Close();
               }

               logger.Info($"Close queue done ({DateTime.Now - dateTime})");

               if (messages.Length > 0)
               {
                  logger.Info($"Avverage one message ({average / messages.Length}) Milliseconds");
               }

               // disconnecting queue manager
               logger.Info("Disconnecting queue manager.. ");
               dateTime = DateTime.Now;
               queueManager.Disconnect();
               logger.Info($"Disconnecting queue manager.. done ({DateTime.Now - dateTime})");
               queueManager.Close();
            }
         }
         catch (MQException mqe)
         {
            logger.Error($"MQException caught: {mqe.ReasonCode} - {mqe.Message}", mqe);
         }
      }
   }
}
