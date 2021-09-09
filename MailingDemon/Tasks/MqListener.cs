using System;
using System.Collections;
using System.Configuration;
using System.IO;
using System.ServiceModel.Web;
using System.Text;
using IBM.WMQ;
using RestChild.Comon;
using RestChild.DAL;
using RestChild.Domain;
using RestChild.Security.Logger;

namespace MailingDemon.Tasks
{
   /// <summary>
   ///    прием сообщений из очереди
   /// </summary>
   [Task]
   public class MqListener : BaseTask
   {
      protected override void Execute()
      {
         GetMessagesFromUts(ConfigurationManager.AppSettings["MqRequestIncoming"]);
      }

      public static string SafeGetStringProperty(MQMessage message, string name)
      {
         try
         {
            return message.GetStringProperty(name);
         }
         catch
         {
            return null;
         }
      }

      /// <summary>
      ///    Получение заявлений из очереди
      /// </summary>
      protected void GetMessagesFromUts(string queueNameGet)
      {
         try
         {
            Logger.Info("MqListener start...");

            SecurityLogger.AddToLog(SecurityJournalEventType.OutSystemsInteractions, "Взаимодействия с ЕТП",
               $"GetMessagesFromUts (получение сообщения из очереди ({queueNameGet}))", "",
               WebOperationContext.Current?.IncomingRequest?.UserAgent);

            // mq properties
            var _properties = new Hashtable(); //{ { MQC.TRANSPORT_PROPERTY, MQC.TRANSPORT_MQSERIES_MANAGED } };
            _properties.Add(MQC.CONNECT_OPTIONS_PROPERTY, MQC.MQCNO_RECONNECT);
            //Environment.SetEnvironmentVariable("MQCHLLIB", ApplicationBinPath);
            //Environment.SetEnvironmentVariable("MQCHLTAB", "AMQCLCHL.TAB");

            _properties.Add(MQC.CONNECTION_NAME_PROPERTY, ConfigurationManager.AppSettings["MqConnectionName"]);
            _properties.Add(MQC.CHANNEL_PROPERTY, ConfigurationManager.AppSettings["MqChanelProperty"]);
            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["MqUser"]))
            {
               _properties.Add(MQC.USER_ID_PROPERTY, ConfigurationManager.AppSettings["MqUser"]);
               _properties.Add(MQC.PASSWORD_PROPERTY, ConfigurationManager.AppSettings["MqPassword"]);
            }

            // create connection
            Logger.Info("Connecting to queue manager.. ");
            var MqQueueMangerName = ConfigurationManager.AppSettings["MqQueueMangerName"];
            using (var queueManager = new MQQueueManager(MqQueueMangerName, _properties))
            {
               Logger.Info("Connecting to queue manager.. done");

               // accessing queue
               Logger.Info("Accessing queue " + queueNameGet + ".. ");
               using (var queue =
                  queueManager.AccessQueue(queueNameGet, MQC.MQOO_INPUT_AS_Q_DEF + MQC.MQOO_FAIL_IF_QUIESCING))
               {
                  Logger.Info("Accessing queue " + queueNameGet + ".. done");

                  // getting messages continuously
                  // creating a message object

                  for (var i = 0; i < 10; i++)
                  {
                     var message = new MQMessage();
                     try
                     {
                        var gmo = new MQGetMessageOptions
                        {
                           Options = MQC.MQGMO_FAIL_IF_QUIESCING | MQC.MQGMO_WAIT | MQC.MQGMO_SYNCPOINT,
                           WaitInterval = MQC.MQWI_UNLIMITED
                        };

                        queue.Get(message, gmo);

                        var unitOfWork = new UnitOfWork();
                        try
                        {
                           unitOfWork.AutoDetectChangesDisable();
                           SaveMessage(queueManager, message, queueNameGet, unitOfWork);

                           var gmo2 = new MQGetMessageOptions
                           {
                              Options = MQC.MQGMO_FAIL_IF_QUIESCING | MQC.MQGMO_WAIT | MQC.MQGMO_SYNCPOINT,
                              WaitInterval = 1000
                           };

                           unitOfWork.SaveChanges();

                           var index = 0;
                           while (true)
                           {
                              message = new MQMessage();
                              queue.Get(message, gmo2);
                              SaveMessage(queueManager, message, queueNameGet, unitOfWork);
                              index++;
                              if (index > 100)
                              {
                                 index = 0;
                                 unitOfWork.SaveChanges();
                              }
                           }
                        }
                        finally
                        {
                           try
                           {
                              unitOfWork.SaveChanges();
                           }
                           finally
                           {
                              unitOfWork.Dispose();
                           }
                        }
                     }
                     catch (MQException mqe)
                     {
                         if (mqe.ReasonCode == 2033)
                         {
                             Logger.Info("No message available");
                         }
                         else
                         {
                             Logger.Error($"MQException caught: {mqe.ReasonCode} - {mqe.Message}", mqe);
                         }
                     }
                  }

                  Logger.Info("Closing queue.. ");
                  queue.Close();
                  Logger.Info("Closing queue.. done");
               }


               // closing queue

               // disconnecting queue manager
               Logger.Info("Disconnecting queue manager.. ");
               queueManager.Disconnect();
               Logger.Info("Disconnecting queue manager.. done");
               queueManager.Close();
               Logger.Info("MqListener finish...");
            }
         }
         catch (MQException mqe)
         {
            Logger.Error($"MqListener error: {mqe.ReasonCode} - {mqe.Message}", mqe);
         }
      }

      protected virtual void SaveMessage(MQQueueManager manager, MQMessage message, string queueNameGet, IUnitOfWork unitOfWork)
      {
         var messageData = message.ReadBytes(message.MessageLength);
         var exchangeUts = new ExchangeUTS
         {
            Incoming = true,
            Message = Encoding.UTF8.GetString(messageData),
            DateCreate = DateTime.Now,
            QueueName = queueNameGet,
            Processed = false,
            FromOrgCode = SafeGetStringProperty(message, "FromOrgCode"),
            ToOrgCode = SafeGetStringProperty(message, "ToOrgCode"),
            ServiceNumber = SafeGetStringProperty(message, "ServiceNumber"),
            MessageId = SafeGetStringProperty(message, "MessageId")
         };

         unitOfWork.GetSet<ExchangeUTS>().Add(exchangeUts);
         unitOfWork.SaveChanges();
         message.ClearMessage();
         manager.Commit();
      }
   }
}
