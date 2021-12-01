using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Configuration;
using System.Net.Mail;
using System.Threading;
using RestChild.Comon.SMS;
using RestChild.Mobile.DAL;
using RestChild.Mobile.Domain;


namespace MailingDemon.Tasks
{
    [Task]
    public class SendMobileSmsTask : BaseTask
    {
        /// <summary>
        ///     выполнение
        /// </summary>
        protected override void Execute()
        {
            Logger.Info("Mobile sms sending: started");

            var source = ConfigurationManager.AppSettings["SMSServiceSource"];

            using (var unitOfWork = new MobileUnitOfWork())
            {
                var smss = unitOfWork.GetSet<SendEmailAndSms>()
                    .Where(s => !s.IsSmsSended && (s.Phone ?? "") != ""
                                               && (s.SmsMessage ?? "") != "")
                    .OrderBy(s => s.Id).Take(500).ToList();

                foreach (var sms in smss)
                {
                    try
                    {
                        using (var client = new SMSSendClient(source))
                        {
                            client.SendMessage(sms);
                        }
                    }
                    catch (Exception e)
                    {
                        Logger.Error("Sms sending: Ошибка отправки sms сообщения.", e);
                    }

                    sms.IsSmsSended = true;
                    sms.DateSms = DateTime.Now;
                    unitOfWork.SaveChanges();
                }
            }

            Logger.Info("Mobile sms sending: finished");
        }
    }
}
