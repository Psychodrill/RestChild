using System;
using System.Configuration;
using System.Threading;
using RestChild.ERL;
using RestChild.ERL.V202003;

namespace MailingDemon.Tasks
{
    /// <summary>
    ///     Прослушивание потока 2.5 из ЕРЛ
    /// </summary>
    [Task]
    public class ERLQueue25Listener : BaseTask
    {
        protected override void Execute()
        {
            try
            {
                Logger.Info("ERLQueue25Listener start...");

                var erlResponseQueueName = ConfigurationManager.AppSettings["ERLConnectionQueue25Name"] ?? "queue.aisdo.oiv.from_erl.2_5";

                using (MQListener.ListenQueue(erlResponseQueueName, new StandAloneQueueAnswerListener(Logger)))
                {
                    Logger.Info("ERLQueue25Listener sleep 3 minutes...");
                    Thread.Sleep(new TimeSpan(0, 3, 0));
                    Logger.Info("ERLQueue25Listener closing queue...");
                }

                Logger.Info("ERLQueue25Listener closing queue done");
                Logger.Info("ERLQueue25Listener finish");
            }
            catch (Exception mqe)
            {
                Logger.Error($"ERLQueue25Listener error: {mqe.Message}", mqe);
            }
        }
    }
}
