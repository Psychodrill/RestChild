using System;
using System.Configuration;
using System.Threading;
using RestChild.ERL;
using RestChild.ERL.V202003;

namespace MailingDemon.Tasks
{
    /// <summary>
    ///     Прослушивание ответов из ЕРЛ
    /// </summary>
    [Task]
    public class ERLQueueResponseListener : BaseTask
    {
        protected override void Execute()
        {
            try
            {
                Logger.Info("ERLQueueResponseListener start...");
                var erlResponseQueueName = ConfigurationManager.AppSettings["ERLConnectionQueueResponseName"] ?? "queue.aisdo.oiv.from_erl.response";

                using (var q1 = MQListener.ListenQueue(erlResponseQueueName, new QueueAnswerListener(Logger)))
                {
                    Logger.Info("ERLQueueResponseListener sleep 3 minutes...");
                    Thread.Sleep(new TimeSpan(0, 3, 0));
                    Logger.Info("ERLQueueResponseListener closing queue...");
                }

                Logger.Info("ERLQueueResponseListener closing queue done");
                Logger.Info("ERLQueueResponseListener finish");
            }
            catch (Exception mqe)
            {
                Logger.Error($"ERLQueueResponseListener error: {mqe.Message}", mqe);
            }
        }
    }
}
