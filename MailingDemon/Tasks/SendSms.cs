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

                var source = ConfigurationManager.AppSettings["SMSServiceSource"];

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

                var smss = unitOfWork.GetSet<SendEmailAndSms>().Where(s => !s.IsSmsSended
                                                                           && !string.IsNullOrEmpty(s.Phone)
                                                                           && (s.DateToSend == null ||
                                                                               s.DateToSend <= DateTime.Now))
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

                Logger.Info("SmsEmails finished");
            }
        }
    }
}
