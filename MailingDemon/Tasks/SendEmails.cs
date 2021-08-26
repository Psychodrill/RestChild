using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Configuration;
using System.Net.Mail;
using System.Threading;
using RestChild.Comon;
using RestChild.DAL;
using RestChild.DAL.RepositoryExtensions;
using RestChild.DocumentGeneration;
using RestChild.Domain;

namespace MailingDemon.Tasks
{
    [Task]
    public class SendEmails : BaseTask
    {
        protected override void Execute()
        {
            using (var unitOfWork = new UnitOfWork())
            {
                Logger.Info("SendEmails started");

                var dt = DateTime.Now;

                var emails = unitOfWork.GetSet<SendEmailAndSms>().Where(s => !s.IsEmailSended && (s.DateToSend == null || s.DateToSend <= dt)).Take(500).ToList();
                var smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
                var account = unitOfWork.GetById<Account>(ConfigurationManager.AppSettings["CshedAccountToSign"].LongParse()) ?? new Account();

                foreach (var email in emails)
                {
                    var success = true;
                    try
                    {
                        var disposable = new List<IDisposable>();
                        try
                        {
                            using (var mailMessage = new MailMessage(smtpSection.From, email.Email)
                            {
                                Subject = email.EmailTitle,
                                Body = email.EmailMessage,
                                IsBodyHtml = true
                            })
                            {
                                if (email.Attachments != null && email.Attachments.Any())
                                {
                                    foreach (var attachment in email.Attachments)
                                    {
                                        if (email.RequestId.HasValue)
                                        {
                                            var url = attachment.UrlToDownload;
                                            var currentAccount = account;
                                            if (url.Contains("|"))
                                            {
                                                var items = url.Split('|');
                                                url = items[0];
                                                currentAccount = unitOfWork.GetById<Account>(items[1].LongParse()) ?? currentAccount;
                                            }

                                            var doc = DocumentSwitch.Switch(unitOfWork, currentAccount, email.Request.Id, url, email.Request.RequestOnMoney);
                                            if (doc?.FileBody != null)
                                            {
                                                if (doc.FileBody.Length > 0)
                                                {
                                                    var stream = new MemoryStream(doc.FileBody);
                                                    disposable.Add(stream);
                                                    var att = new Attachment(stream, doc.FileName);
                                                    disposable.Add(att);
                                                    mailMessage.Attachments.Add(att);
                                                    continue;
                                                }
                                            }
                                        }

                                        if (!string.IsNullOrEmpty(attachment.Name))
                                        {
                                            using (var webClient = new WebClient())
                                            {
                                                var response = webClient.DownloadData(attachment.UrlToDownload);
                                                var stream = new MemoryStream(response);
                                                disposable.Add(stream);
                                                var att = new Attachment(stream, attachment.Name);
                                                disposable.Add(att);
                                                mailMessage.Attachments.Add(att);
                                            }
                                        }
                                    }
                                }

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
                            if (ex.Message.Contains("4.4.2 Message submission rate for this client has exceeded the configured limit"))
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
                            foreach (var x in disposable)
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

                Logger.Info("SendEmails finished");
            }
        }
    }
}
