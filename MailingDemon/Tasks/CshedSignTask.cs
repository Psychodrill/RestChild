using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Xml.Serialization;
using RestChild.Common.Service.ServiceReference;
using RestChild.Comon;
using RestChild.Comon.Enumeration;
using RestChild.Comon.Exchange;
using RestChild.CshedIntegration;
using RestChild.DAL;
using RestChild.DAL.RepositoryExtensions;
using RestChild.DocumentGeneration;
using RestChild.Domain;

namespace MailingDemon.Tasks
{
    /// <summary>
    ///     взаимодействие с РЦХЭД
    /// </summary>
    [Task]
    public class CshedSignTask : BaseTask
    {
        protected const int NumberOfMessages = 10000;

        [XmlElement("config")] public ConfigBalance Config { get; set; }

        /// <summary>
        ///     загрузка документа в РЦХЭД и подписание документа
        /// </summary>
        protected override void Execute()
        {
            try
            {
                Logger.Info("CshedSign started");
                using (var unitOfWork = new UnitOfWork())
                {
                    var errorServiceNumbers = new HashSet<string>();
                    var statuses = new HashSet<long>(GetMPGUStatusesToIntegrateWithCSHED(unitOfWork));
                    var query = unitOfWork.GetSet<ExchangeUTS>().Where(e => !e.Incoming && !e.Processed
                                                                                        && !e.IsSigned);

                    if (Config != null && Config.CountNodes > 0)
                    {
                        query = query.Where(q => q.RequestId % Config.CountNodes == Config.IndexNode);
                    }

                    // отправка только тех статусов по которым прошло время
                    var date = DateTime.Now;
                    query = query.Where(q => !q.DateToSend.HasValue || q.DateToSend < date);

                    var notSendStatusesToMpgu =
                        (ConfigurationManager.AppSettings["NotSendStausesToMpgu"] ?? string.Empty).Split(',')
                        .Select(t => t.LongParse()).Where(t => t.HasValue).Distinct().ToArray();

                    var requestStatus = ConfigurationManager.AppSettings["MqRequestStatus"];

                    query = query.Where(q =>
                        !q.ToState.HasValue || !notSendStatusesToMpgu.Contains(q.ToState) ||
                        q.QueueName != requestStatus);

                    var messages = query.OrderBy(s => s.Id).Take(NumberOfMessages).ToArray();

                    // получить все правила
                    var certificateToApplies =
                        unitOfWork.GetSet<CertificateToApply>().Include(c => c.Accounts).ToList();

                    //пользователь по умолчанию сейчас
                    var account =
                        unitOfWork.GetById<Account>(ConfigurationManager.AppSettings["CshedAccountToSign"]
                            .LongParse()) ?? new Account();


                    var dateStatus = DateTime.MinValue;
                    var requestId = (long) 0;
                    foreach (var message in messages)
                    {
                        if (!string.IsNullOrWhiteSpace(message.ServiceNumber) &&
                            errorServiceNumbers.Contains(message.ServiceNumber))
                        {
                            continue;
                        }

                        if (message.RequestId.HasValue && message.ToState.HasValue &&
                            notSendStatusesToMpgu.Contains(message.ToState))
                        {
                            continue;
                        }

                        if (message.Message.Contains("http://asguf.mos.ru/rkis_gu/coordinate/v6_1/") ||
                            message.Request?.SourceId == (long) SourceEnum.Operator)
                        {
                            // подпорка для отправки статусов
                            message.IsSigned = true;
                            unitOfWork.SaveChanges();
                            continue;
                        }

                        if (dateStatus >= DateTime.Now.AddMilliseconds(-100) && requestId == message.RequestId)
                        {
                            dateStatus = dateStatus.AddMilliseconds(100);
                        }
                        else
                        {
                            dateStatus = DateTime.Now;
                            requestId = message.RequestId ?? 0;
                        }

                        if (message.ToState.HasValue && statuses.Contains(message.ToState.Value))
                        {
                            try
                            {
                                var outMessage = Serialization.Deserialize<StatusMessage>(message.Message);

                                if (outMessage.ResponseDate < dateStatus)
                                {
                                    outMessage.ResponseDate = dateStatus;
                                }

                                var documents = new List<ServiceDocument>();
                                var reasonCode = outMessage.ReasonCode;
                                var sendFileToCshed = unitOfWork.GetSet<RequestStatusCshedSendAndSignDocument>()
                                    .Where(ss =>
                                        ss.MpguStatus.ReasonCode == reasonCode &&
                                        ss.MpguStatus.Status == message.ToState.Value)
                                    .Select(ss => new {ss.SignNeed, ss.DocumentPath}).Distinct().ToList();

                                foreach (var action in sendFileToCshed)
                                {
                                    var items = certificateToApplies
                                        .Where(c => c.NotificationType == action.DocumentPath).ToList();

                                    var item = items.FirstOrDefault(i => i.Accounts.Any(a =>
                                                                             a.AccountId == message.AccountId &&
                                                                             a.AccountId.HasValue && !a.ForExcept ||
                                                                             !message.AccountId.HasValue &&
                                                                             a.ForSystemAccount) &&
                                                                         i.Accounts.Where(a => a.ForExcept).All(a =>
                                                                             a.AccountId != message.AccountId &&
                                                                             a.AccountId.HasValue)) ??
                                               items.FirstOrDefault(i => i.ByDefault);

                                    var doc = DocumentSwitch.Switch(unitOfWork,
                                        message.Account ?? item?.Account ?? account, message.Request.Id,
                                        action.DocumentPath, message.Request.RequestOnMoney, sendStatusId: message.ToState);

                                    if (doc == null)
                                    {
                                        throw new Exception("Processing method not defined");
                                    }

                                    if (doc.FileBody.Length < 1)
                                    {
                                        Logger.Info($"CshedSign document not generated extId={message.Id}");
                                        continue;
                                    }

                                    var rft = unitOfWork.GetSet<RequestFileType>()
                                        .FirstOrDefault(t => t.Id == doc.RequestFileTypeId);

                                    doc.RequestId = message.RequestId.Value;
                                    doc.CodeAsGuf = rft.CodeAsGuf;
                                    doc.CodeChed = rft.CodeChed;
                                    doc.SsoId = message.Request?.SsoId;

                                    var docId = CshwdClient.SendDocumentToCshed(doc);
                                    var serviceDocument = new ServiceDocument
                                    {
                                        DocCode = rft.CodeAsGuf,
                                        DocFiles = new[]
                                        {
                                            new File
                                            {
                                                FileName = doc.FileName,
                                                FileIdInStore = docId.Trim('"'),
                                                IsFileInStore = true,
                                                StoreName = ConfigurationManager.AppSettings["CshedServerStore"]
                                            }
                                        }
                                    };

                                    if (string.IsNullOrWhiteSpace(docId))
                                    {
                                        throw new ArgumentNullException(nameof(docId));
                                    }

                                    if (action.SignNeed && message.Request?.SourceId == (long) SourceEnum.Mpgu)
                                    {
                                        CshwdClient.SignDocumentToCshed(doc, docId, item?.CertificateKey);
                                    }

                                    documents.Add(serviceDocument);
                                    if (message.Request.Id > 0)
                                    {
                                        unitOfWork.AddEntity(new RequestFile
                                        {
                                            RequestId = message.Request.Id,
                                            RequestFileTypeId = doc.RequestFileTypeId,
                                            FileTitle = doc.FileName,
                                            FileName = docId,
                                            RemoteSave = true,
                                            DataCreate = DateTime.Now
                                        });
                                    }
                                }

                                outMessage.Documents = documents.ToArray();

                                message.Message = Serialization.Serializer(outMessage);
                                message.IsSigned = true;
                            }
                            catch (Exception ex)
                            {
                                Logger.Error($"CshedSign Error ExId:{message.Id}", ex);
                                if (!string.IsNullOrWhiteSpace(message.ServiceNumber))
                                {
                                    errorServiceNumbers.Add(message.ServiceNumber);
                                }
                            }

                            unitOfWork.SaveChanges();
                        }
                        else
                        {
                            if (message.Request != null)
                            {
                                var outMessage = Serialization.Deserialize<StatusMessage>(message.Message);
                                if (outMessage.ResponseDate < dateStatus)
                                {
                                    outMessage.ResponseDate = dateStatus;
                                    message.Message = Serialization.Serializer(outMessage);
                                }
                            }

                            message.IsSigned = true;
                            unitOfWork.SaveChanges();
                        }
                    }

                    unitOfWork.SaveChanges();
                }

                Logger.Info("CshedSign finished");
            }
            catch (Exception ex)
            {
                Logger.Error("CshedSign Error", ex);
            }
        }

        /// <summary>
        ///     Статусы по которым нужно отправлять/подписывать документы в РЦХЭД
        /// </summary>
        private long[] GetMPGUStatusesToIntegrateWithCSHED(IUnitOfWork uw)
        {
            return uw.GetSet<RequestStatusCshedSendAndSignDocument>().Where(ss => ss.DocumentPath != string.Empty)
                .Select(ss => ss.MpguStatus.Status).Distinct().ToArray();
        }
    }
}
