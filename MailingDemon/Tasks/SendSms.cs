using System;
using System.Configuration;
using System.Linq;
using RestChild.Comon.SMS;
using RestChild.DAL;
using RestChild.Domain;

namespace MailingDemon.Tasks
{
    [Task]
    public class SendSms : BaseTask
    {
        protected override void Execute()
        {
            using (var unitOfWork = new UnitOfWork())
            {
                Logger.Info("Sms sending: started");

                //для теста
                //using (var client = new SMSSendClient(source))
                //{
                //    client.SendMessage(new SendEmailAndSms
                //    {
                //        Id = 667,
                //        Phone = "79161351765",
                //        SmsMessage = "Хуй пизда играли в поезда"
                //    });
                //}

                //return;

                Logger.Info("SmsEmails finished");
            }
        }
    }
}
