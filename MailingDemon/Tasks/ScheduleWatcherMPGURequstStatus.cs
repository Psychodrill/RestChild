using RestChild.Comon;
using RestChild.Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using RestChild.DAL;

namespace MailingDemon.Tasks
{
   [Task]
   public class ScheduleWatcherMPGURequstStatus : ScheduleWatcher
   {
      protected override void Execute()
      {
         using (var unitOfWork = new UnitOfWork())
         {
            Logger.Info("ScheduleWatcherMPGURequstStatus started");

            PutMessages(unitOfWork, ConfigurationManager.AppSettings["MqMPGURequestStatusOutcoming"]);
            PutErrorMessages(unitOfWork, ConfigurationManager.AppSettings["MqMPGURequestIncoming"], ConfigurationManager.AppSettings["MqMPGURequestIncomingError"]);
            PutErrorMessages(unitOfWork, ConfigurationManager.AppSettings["MqMPGURequestStatusIncoming"], ConfigurationManager.AppSettings["MqMPGURequestStatusIncomingError"]);

            Logger.Info("ScheduleWatcherMPGURequstStatus finished");
         }
      }

      protected override void PutMessages(IUnitOfWork unitOfWork, string queueName)
      {
         var dateTime = DateTime.Now;

         //Logger.Info($"Query MPGU messages start ({DateTime.Now - dateTime})");

         var query = GetQuery(unitOfWork, queueName, null);
         var messages_all = query.Take(NumberOfMsgs).ToArray();
         var ServiceNumbers = messages_all.Select(ss => ss.ServiceNumber).Distinct().OrderBy(ss => ss).ToArray();

         foreach(var ServiceNumber in ServiceNumbers)
         {
            var messages = messages_all.Where(ss => ss.ServiceNumber == ServiceNumber).OrderBy(ss => ss.Id).ToArray();
            PutMessages(unitOfWork, queueName, messages, Logger, RestChild.MPGUIntegration.Utils.GetQueueDelay(queueName));
         }

         Logger.Info($"Query MPGU messages done ({DateTime.Now - dateTime})");
      }
   }
}
