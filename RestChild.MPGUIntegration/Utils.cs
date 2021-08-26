using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml;
using System.Xml.Serialization;
using RestChild.Comon;
using RestChild.Domain;
using RestChild.MPGUIntegration.V61;

namespace RestChild.MPGUIntegration
{
   /// <summary>
   ///    утилиты для для отправки сообщений в очереди
   /// </summary>
   public static class Utils
   {
      private const uint last_N_digits = 5;
      private static readonly string last_N_digits_mask = string.Empty;

      static Utils()
      {
         var sb = new StringBuilder("{0:");
         for (uint i = 1; i <= last_N_digits; i++)
         {
            sb.Append("0");
         }

         sb.Append("}");
         last_N_digits_mask = sb.ToString();
      }

      /// <summary>
      ///    Сгенерировать ПИНКод из номер записи
      /// </summary>
      /// <param name="Id"></param>
      /// <returns></returns>
      public static string GeneratePin(long Id)
      {
         if (Id < 1)
         {
            return string.Empty;
         }

         long res = 0;
         var p = Convert.ToInt32(Math.Pow(10, last_N_digits));
         if (Id >= p)
         {
            res = Id % p;
         }
         else
         {
            res = Id;
         }

         return string.Format(last_N_digits_mask, res);
      }

      private static XmlElement GetElement(string xml)
      {
         var doc = new XmlDocument();
         doc.LoadXml(xml);
         return doc.DocumentElement;
      }

      private static string StatusTitleToLower(string StatusTitle)
      {
         return char.ToLowerInvariant(StatusTitle[0]) + StatusTitle.Substring(1);
      }

      private static string SendStatusMessageXMLString(StatusMessageFilter filter)
      {
         var message = new CoordinateStatusMessage
         {
            CoordinateStatusDataMessage = new CoordinateStatusData
            {
               ServiceNumber = filter.ServiceNumber,
               Status = new StatusType
               {
                  StatusCode = filter.StatusCode,
                  StatusDate = filter.StatusDate,
                  StatusTitle = filter.StatusTitle
               }
            }
         };
         if (!string.IsNullOrWhiteSpace(filter.PINCode))
         {
            var sb = new StringBuilder();
            sb.Append("<ServiceProperties>");

            sb.Append($"<PINCode>{filter.PINCode}</PINCode>");

            if (filter.Cell.HasValue)
            {
               sb.AppendFormat("<Date>{0:dd.MM.yyyy}</Date><Time>{0:HH:mm}</Time>", filter.Cell.Value);
            }

            if (filter.BookingId.HasValue)
            {
               sb.AppendFormat("<BookingId>{0}</BookingId>", filter.BookingId.Value);
            }

            if (filter.Declarant.HasValue)
            {
               sb.Append("<BaseDeclarant>");
               sb.AppendFormat("<FirstName>{0}</FirstName>", filter.Declarant.Value.FirstName);
               sb.AppendFormat("<LastName>{0}</LastName>", filter.Declarant.Value.LastName);
               sb.AppendFormat("<MiddleName>{0}</MiddleName>", filter.Declarant.Value.MiddleName);
               sb.AppendFormat("<BirthDate>{0:dd.MM.yyyy}</BirthDate>", filter.Declarant.Value.BirthDate);
               sb.AppendFormat("<MobilePhone>{0}</MobilePhone>", filter.Declarant.Value.MobilePhone);
               sb.AppendFormat("<WorkPhone>{0}</WorkPhone>", filter.Declarant.Value.WorkPhone);
               sb.AppendFormat("<EMail>{0}</EMail>", filter.Declarant.Value.EMail);
               sb.Append("</BaseDeclarant>");
            }

            if (!string.IsNullOrWhiteSpace(filter.PurposeValue))
            {
               sb.AppendFormat("<PurposeValue>{0}</PurposeValue>", filter.PurposeValue);
            }

            if (!string.IsNullOrWhiteSpace(filter.KidsCountValue))
            {
               sb.AppendFormat("<KidsCountValue>{0}</KidsCountValue>", filter.KidsCountValue);
            }

            sb.Append("</ServiceProperties>");

            message.CoordinateStatusDataMessage.Documents = new ServiceDocument[1]
            {
               new ServiceDocument
               {
                  DocKind = new DictionaryItem
                  {
                     Code = "7769",
                     Name = "Иной документ"
                  },
                  CustomAttributes = GetElement(sb.ToString())
               }
            };
         }

         if (!string.IsNullOrWhiteSpace(filter.Note))
         {
            message.CoordinateStatusDataMessage.Note = filter.Note;
         }

         if (!string.IsNullOrWhiteSpace(filter.StatusReason))
         {
            message.CoordinateStatusDataMessage.Reason = new DictionaryItem
            {
               Code = filter.StatusReason,
               Name = filter.StatusTitle
            };
         }

         var coordinateStatusMessageSerializer = new XmlSerializer(typeof(CoordinateStatusMessage));
         string msg;
         using (var sww = new StringWriter())
         {
            using (var writer = XmlWriter.Create(sww))
            {
               coordinateStatusMessageSerializer.Serialize(writer, message);
               msg = sww.ToString();
            }
         }

         return msg;
      }

      /// <summary>
      ///    отпрвка статуса
      /// </summary>
      public static void SendStatusMessage(IUnitOfWork unitOfWork, string mqMpguStatusOutcomingQueue,
         StatusMessageFilter filter)
      {
         //проверка на то что статус (не ошибочный) уже корректнго отправлялся
         var errorCodes = new int[2] {103099, 116999};
         if (!errorCodes.Contains(filter.StatusCode) && unitOfWork.GetSet<ExchangeUTS>().Any(ss =>
                ss.QueueName == mqMpguStatusOutcomingQueue && ss.ServiceNumber == filter.ServiceNumber &&
                ss.ToState == filter.StatusCode && (ss.Processed && !ss.IsError || !ss.Processed)))
         {
            return;
         }

         //задержка отправки сообщений (лютейший бред имени МПГУ)
         var msgDelay = GetQueueDelay(mqMpguStatusOutcomingQueue);
         if (msgDelay.HasValue && msgDelay.Value > 0)
         {
            new ManualResetEvent(false).WaitOne(msgDelay.Value);
            filter.StatusDate = DateTime.Now;
         }

         var msg = SendStatusMessageXMLString(filter);
         var eMassage = new ExchangeUTS
         {
            Processed = false,
            Incoming = false,
            DateCreate = filter.StatusDate,
            QueueName = mqMpguStatusOutcomingQueue,
            Message = msg,
            FromOrgCode = "2064",
            ToOrgCode = "0001",
            LastUpdateTick = DateTime.Now.Ticks,
            ServiceNumber = filter.ServiceNumber,
            ToState = filter.StatusCode
         };

         unitOfWork.AddEntity(eMassage);
         //UnitOfWork.SaveChanges();
      }

      /// <summary>
      ///    Создание сообщения об ошибке для очереди в МПГУ
      /// </summary>
      public static void SendErrorStatus(IUnitOfWork UnitOfWork, string Message, string QueueName)
      {
         var eMassage = new ExchangeUTS
         {
            Processed = false,
            Incoming = false,
            QueueName = QueueName,
            Message = Message,
            LastUpdateTick = DateTime.Now.Ticks
         };

         UnitOfWork.AddEntity(eMassage);
         //UnitOfWork.SaveChanges();
      }

      /// <summary>
      ///    Выборка длинны задержки между отправлениями в очередь
      /// </summary>
      /// <param name="queueName">Имя очереди</param>
      /// <returns></returns>
      public static int? GetQueueDelay(string queueName)
      {
         var Queues = ConfigurationManager.AppSettings["MqMPGUDelayQueues"];
         if (string.IsNullOrWhiteSpace(Queues) || string.IsNullOrWhiteSpace(queueName))
         {
            return null;
         }

         var Queues_array = Queues.Split(',');
         if (Queues_array.Length < 1)
         {
            return null;
         }

         if (Queues_array.Any(s => string.Equals(queueName, s, StringComparison.OrdinalIgnoreCase)))
         {
            return 1200;
         }

         return null;
      }
   }
}
