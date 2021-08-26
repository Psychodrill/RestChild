using RestChild.Comon;
using RestChild.Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Configuration;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using RestChild.DAL;

namespace MailingDemon.Tasks
{
   [Task]
   public class SecurityEmailSendTask : BaseTask
   {
      protected override void Execute()
      {
         using (var unitOfWork = new UnitOfWork())
         {
            Logger.Info("SecurityEmailSend started");
            var _per = DateTime.Now.AddDays(-RestChild.Security.SecuritySettings.TimeLifePassword);
            var _section7 = _per.AddDays(7);
            var _section3 = _per.AddDays(3);


            var smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
            var accounts = unitOfWork.GetSet<Account>().Where(a => a.IsActive && !a.IsDeleted && !a.IsTemporyPassword && a.Password != null &&
            (
            (a.DateLastChangePassword != null && a.DateLastChangePassword <=_section7) || (a.DateLastChangePassword == null && a.DateUpdate <= _section7)
            )).ToList();

            foreach (var account in accounts)
            {
               using (var mailMessage = new MailMessage(smtpSection.From, account.Email)
               {
                  Subject = "Период действия пароля в системе «Аис «Отдых»",
                  IsBodyHtml = true
               })
               {
                  if(!account.DateLastChangePassword.HasValue)
                  {
                     account.DateLastChangePassword = account.DateUpdate.Value;
                  }

                  if(account.DateLastChangePassword.Value <= _per)
                  {
                     mailMessage.Body = "Период действия пароля в системе «Аис «Отдых» истек. Необходимо сменить пароль.";
                  }
                  else if (account.DateLastChangePassword.Value <= _section7)
                  {
                     mailMessage.Body = "Период действия пароля в системе «Аис «Отдых» заканчивается менее чем через 7 дней.";
                  }
                  else if (account.DateLastChangePassword.Value <= _section3)
                  {
                     mailMessage.Body = "Период действия пароля в системе «Аис «Отдых» заканчивается менее чем через 3 дня.";
                  }

                  using (var client = new SmtpClient())
                  {
                     client.Send(mailMessage);
                  }
               }
            }
            Logger.Info("SecurityEmailSend finished");
         }
      }
   }
}
