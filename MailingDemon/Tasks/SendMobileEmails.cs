using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Configuration;
using System.Net.Mail;
using System.Threading;
using RestChild.Mobile.DAL;
using RestChild.Mobile.Domain;

namespace MailingDemon.Tasks
{
    /// <summary>
    ///     отправка писем для мобильного приложения
    /// </summary>
    [Task]
    public class SendMobileEmails : BaseTask
    {
        /// <summary>
        ///     выполнение данных
        /// </summary>
        protected override void Execute()
        {
            using (var unitOfWork = new MobileUnitOfWork())
            {
                Logger.Info("SendMobileEmails started");

                var emails = unitOfWork.GetSet<SendEmailAndSms>().Where(s => !s.IsEmailSended).Take(500).ToList();
                var smtpSection = (SmtpSection) ConfigurationManager.GetSection("system.net/mailSettings/smtp");

                foreach (var email in emails)
                {
                    var success = true;
                    try
                    {
                        var disposables = new List<IDisposable>();
                        try
                        {
                            using (var mailMessage = new MailMessage(smtpSection.From, email.Email)
                            {
                                Subject = email.EmailTitle,
                                Body = email.EmailMessage,
                                IsBodyHtml = true
                            })
                            {
                                using (var client = new SmtpClient())
                                {
                                    client.Send(mailMessage);
                                    client.ServicePoint.CloseConnectionGroup(client.ServicePoint.ConnectionName);
                                }
                            }

                            email.DateEmail = DateTime.Now;
                        }
                        catch (SmtpException ex)
                        {
                            if (ex.Message.Contains(
                                "4.4.2 Message submission rate for this client has exceeded the configured limit"))
                            {
                                Logger.Warn("SendEmails error - submission rate for this client has exceeded");
                                Thread.Sleep(new TimeSpan(0, 0, 30));
                                success = false;
                            }
                            else if (!ex.Message.Contains("5.1.3 Invalid address"))
                            {
                                throw;
                            }
                            else
                            {
                                Logger.Warn("SendEmails error - Invalid address - " + email);
                            }
                        }
                        catch (FormatException ex)
                        {
                            Logger.Error("SendEmails Ошибка отправки сообщения по электронной почте.", ex);
                        }
                        finally
                        {
                            foreach (var x in disposables)
                            {
                                x.Dispose();
                            }
                        }

                        if (success)
                        {
                            email.IsEmailSended = true;
                            unitOfWork.SaveChanges();
                        }
                    }
                    catch (Exception e)
                    {
                        Logger.Error("SendEmails Ошибка отправки сообщения по электронной почте.", e);
                    }
                }

                Logger.Info("SendMobileEmails finished");
            }
        }
    }
}
