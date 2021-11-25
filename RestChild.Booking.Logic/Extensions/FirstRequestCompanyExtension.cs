using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Xml;
using RestChild.Common.Service.ServiceReference;
using RestChild.Comon;
using RestChild.Comon.Dto;
using RestChild.Comon.Dto.Booking;
using RestChild.Comon.Enumeration;
using RestChild.Comon.Exchange;
using RestChild.DAL.RepositoryExtensions;
using RestChild.Domain;
using RestChild.Security;
using RequestService = RestChild.Common.Service.ServiceReference.RequestService;

namespace RestChild.Booking.Logic.Extensions
{
    /// <summary>
    ///     расширения для работы с заявкой
    /// </summary>
    public static class FirstRequestCompanyExtension
    {
        /// <summary>
        ///     живые статусы
        /// </summary>
        public static long?[] LiveRequestStatuses =
        {
            (long?) StatusEnum.CertificateIssued,
            (long?) StatusEnum.Send,
            (long?) StatusEnum.WaitApplicant,
            (long?) StatusEnum.OperatorCheck,
            (long?) StatusEnum.ApplicantCome,
            (long?) StatusEnum.DecisionIsMade,
            (long?) StatusEnum.DecisionMaking,
            (long?) StatusEnum.DecisionMakingCovid,
            (long?) StatusEnum.Ranging,
            (long?) StatusEnum.IncludedInList,
            (long?) StatusEnum.WaitApplicantMoney
        };

        public static readonly long?[] RequestDeclineStatuses =
        {
            (long?) StatusEnum.Draft, (long) StatusEnum.ErrorRequest, (long) StatusEnum.Reject,
            (long) StatusEnum.CancelByApplicant, (long) StatusEnum.RegistrationDecline
        };

        //private static readonly Dictionary<long?, long?> DelayedSendingStatus =
        //    Settings.Default.DelayedSendingStatus?.Cast<string>()
        //        .Select(v => v.Split(';'))
        //        .Where(v => v.Length == 2)
        //        .Select(v => new { id = v[0].LongParse(), dec = v[1].LongParse() })
        //        .ToDictionary(v => v.id, v => v.dec) ?? new Dictionary<long?, long?>();

        /// <summary>
        ///     получить ранг
        /// </summary>
        public static int GetRank(this DetailInfo[] details)
        {
            if (details == null)
            {
                return 3;
            }

            if (!details.Any())
            {
                return 3;
            }

            if (details.Length == 1)
            {
                return 2;
            }

            return 1;
        }

        /// <summary>
        ///     получить текущего пользователя
        /// </summary>
        /// <returns></returns>
        public static long? GetCurrentAccountId()
        {
            return
                ((ClaimsIdentity) HttpContext.Current?.User?.Identity)?.Claims
                ?.FirstOrDefault(c => c.Type == ClaimTypes.Sid)?.Value?.LongParse();
        }

        /// <summary>
        ///     года кампании для заявления по виду обращения
        /// </summary>
        public static List<YearOfRest> GetYearsForTypeOfRest(this IUnitOfWork unitOfWork, long? typeOfRestId = null,
            DateTime? dateRequest = null)
        {
            if (!typeOfRestId.HasValue)
            {
                return new List<YearOfRest>();
            }

            if (typeOfRestId == (long) TypeOfRestEnum.Compensation
                || typeOfRestId == (long) TypeOfRestEnum.CompensationYouthRest)
            {
                return unitOfWork.GetSet<YearOfRest>().ToList().OrderBy(y => y.Name).Select(y => new YearOfRest(y))
                    .ToList();
            }

            // получаем год кампании для которых идет первая заявочная кампания
            var date = (dateRequest ?? DateTime.Now).Date;
            var year =
                unitOfWork.GetSet<YearOfRest>()
                    .Where(y => y.DateFirstStage <= date && y.DateFirstStageClose >= date)
                    .OrderByDescending(y => y.Name)
                    .FirstOrDefault();
            if (year != null)
            {
                return new List<YearOfRest> {new YearOfRest(year)};
            }

            return new List<YearOfRest>();
        }

        /// <summary>
        ///     отправка статуса заявления при событии.
        /// </summary>
        public static void SendChangeStatusByEvent(this IUnitOfWork unitOfWork, Request entity, Guid eventCode,
            DateTime? planDate = null)
        {
            if (entity.Repared)
            {
                return;
            }

            var exchangeSystemCode = ConfigurationManager.AppSettings["exchangeSystemCode"] ?? "9000063";
            //очередной костыль для невнятной проблемы с пропадающим систем кодом
            if (exchangeSystemCode.IsNullOrEmpty())
                exchangeSystemCode = "9000063";
            var fromCode = ConfigurationManager.AppSettings["exchangeBaseRegistryFromCode"];
            var toCode = ConfigurationManager.AppSettings["exchangeMpguToCode"];

            var serviceNumber = entity.RequestNumber;
            var year = (DateTime.Now.Year % 100).ToString(CultureInfo.InvariantCulture);

            var serviceCode = entity.IsFirstCompany
                ? entity.TypeOfRest?.ServiceCodeFirstCompany
                : entity.TypeOfRest?.ServiceCode;

            if (string.IsNullOrWhiteSpace(serviceNumber) && !string.IsNullOrWhiteSpace(serviceCode))
            {
                var exchangeBaseRegistryCode = $"{fromCode}-{exchangeSystemCode}-{serviceCode}";
                serviceNumber = "000000" + unitOfWork.GetNextNumber(exchangeBaseRegistryCode + year);
                serviceNumber =
                    $"{exchangeBaseRegistryCode}-{serviceNumber.Substring(serviceNumber.Length - 6)}/{year}";

                entity.RequestNumber = serviceNumber;
                entity.DateRequest = entity.DateRequest ?? DateTime.Now;
            }
            else if (string.IsNullOrWhiteSpace(serviceNumber) &&
                     entity.TypeOfRestId == (long) TypeOfRestEnum.Compensation)
            {
                entity.DateRequest = entity.DateRequest ?? DateTime.Now;
                var numberKey = entity.Child?.FirstOrDefault()?.BenefitTypeId ==
                                (long) BenefitTypeEnum.CompensationOrphan
                    ? "2"
                    : "1";
                serviceNumber = "00000" + unitOfWork.GetNextNumber("КОМП" + "-" + numberKey + "-" + year);
                serviceNumber = string.Format("{2}-{0}-{1}", serviceNumber.Substring(serviceNumber.Length - 5), year,
                    numberKey);
                entity.RequestNumber = serviceNumber;
            }
            else if (string.IsNullOrWhiteSpace(serviceNumber) &&
                     entity.TypeOfRestId == (long) TypeOfRestEnum.CompensationYouthRest)
            {
                entity.DateRequest = entity.DateRequest ?? DateTime.Now;
                var numberKey = "3";
                serviceNumber = "00000" + unitOfWork.GetNextNumber("КОМП" + "-" + numberKey + "-" + year);
                serviceNumber = string.Format("{2}-{0}-{1}", serviceNumber.Substring(serviceNumber.Length - 5), year,
                    numberKey);
                entity.RequestNumber = serviceNumber;
            }

            if (string.IsNullOrWhiteSpace(serviceCode))
            {
                return;
            }

            var subStatus =
                unitOfWork
                    .GetSet<RequestStatusChainForMpgu>()
                    .FirstOrDefault(
                        s => s.RequestEvent.EventCode == eventCode &&
                             (!s.YearOfRestId.HasValue || s.YearOfRestId == entity.YearOfRestId));

            //Отказ от сертификата (не уважительная причина) ветка сертификатов
            if (subStatus?.Id == (long) StatusEnum.CancelByApplicant + 20000 &&
                (entity.TypeOfRest?.Id == (long) TypeOfRestEnum.Money ||
                 entity.TypeOfRest?.ParentId == (long) TypeOfRestEnum.Money))
            {
                subStatus = unitOfWork.GetById<RequestStatusChainForMpgu>((long) StatusEnum.CancelByApplicant +
                                                                          2020050000) ?? subStatus;
            }

            if (subStatus != null)
            {
                var statusesForSend = subStatus.Statuses.OrderBy(s => s.OrderField).ToList();

                // отправка цепочки статусов
                foreach (var s in statusesForSend)
                {
                    var message = new StatusMessage
                    {
                        ServiceNumber = serviceNumber,
                        ResponseDate = DateTime.Now,
                        StatusCode = Convert.ToInt32(s.Status),
                        ReasonCode = s.ReasonCode,
                        Note = s.Commentary
                    };

                    if (s.Status == 1050)
                    {
                        message.Note = message.Note?.Replace("зарегистрировано",
                            $"зарегистрировано под № {entity.RequestNumber} от {entity.DateRequest.FormatEx()}");
                    }

                    //message.Documents = new ServiceDocument[] {new ServiceDocument{}};

                    message.Note = message.Note?.Replace("\n", "<br/>");

                    unitOfWork.AddEntity(new ExchangeUTS
                    {
                        DateCreate = message.ResponseDate ?? DateTime.Now,
                        Incoming = false,
                        Message = Serialization.Serializer(message),
                        Processed = false,
                        FromOrgCode = fromCode,
                        ToOrgCode = toCode,
                        MessageId = Guid.NewGuid().ToString(),
                        QueueName = ConfigurationManager.AppSettings["MqRequestStatus"],
                        ServiceNumber = entity.RequestNumber,
                        RequestId = entity.Id,
                        ToState = s.Status,
                        TypeOfRestId = entity.TypeOfRestId,
                        DateToSend = planDate
                    });
                }
            }

            unitOfWork.SaveChanges();
        }

        /// <summary>
        ///     отправка статуса заявления.
        /// </summary>
        public static void SendChangeStatus(this IUnitOfWork unitOfWork, Request entity, long statusId, string comment,
            string action, DateTime? planDate = null, long? accountId = null, bool isFromMpgu = false)
        {

            // Отложенная отправка статуса 1060
            if (statusId == 1055 && !planDate.HasValue)
            {
                planDate = DateTime.Now.AddDays(2);
            }

            // Отложенная отправка статуса 1080
            //if (statusId == 1080 && !planDate.HasValue)
            //{
            //    planDate = DateTime.Now.AddDays(2);
            //}

            var exchangeSystemCode = ConfigurationManager.AppSettings["exchangeSystemCode"] ?? "9000063";
            //очередной костыль для невнятной проблемы с пропадающим систем кодом
            if (exchangeSystemCode.IsNullOrEmpty())
                exchangeSystemCode = "9000063";
            var fromCode = ConfigurationManager.AppSettings["exchangeBaseRegistryFromCode"];
            var toCode = ConfigurationManager.AppSettings["exchangeMpguToCode"];

            var serviceNumber = entity.RequestNumber;
            var year = (DateTime.Now.Year % 100).ToString(CultureInfo.InvariantCulture);

            var serviceCode = entity.IsFirstCompany
                ? entity.TypeOfRest?.ServiceCodeFirstCompany
                : entity.TypeOfRest?.ServiceCode;

            if (string.IsNullOrWhiteSpace(serviceNumber) && !string.IsNullOrWhiteSpace(serviceCode))
            {
                var exchangeBaseRegistryCode = $"{fromCode}-{exchangeSystemCode}-{serviceCode}";
                serviceNumber = "000000" + unitOfWork.GetNextNumber(exchangeBaseRegistryCode + year);
                serviceNumber =
                    $"{exchangeBaseRegistryCode}-{serviceNumber.Substring(serviceNumber.Length - 6)}/{year}";

                entity.RequestNumber = serviceNumber;
                entity.DateRequest = entity.DateRequest ?? DateTime.Now;
            }
            else if (string.IsNullOrWhiteSpace(serviceNumber) &&
                     entity.TypeOfRestId == (long) TypeOfRestEnum.Compensation)
            {
                entity.DateRequest = entity.DateRequest ?? DateTime.Now;
                var numberKey =
                    entity.Child?.FirstOrDefault()?.BenefitTypeId == (long) BenefitTypeEnum.CompensationOrphan
                        ? "2"
                        : "1";
                serviceNumber = "00000" + unitOfWork.GetNextNumber("КОМП" + "-" + numberKey + "-" + year);
                serviceNumber = string.Format("{2}-{0}-{1}", serviceNumber.Substring(serviceNumber.Length - 5), year,
                    numberKey);
                entity.RequestNumber = serviceNumber;
            }
            else if (string.IsNullOrWhiteSpace(serviceNumber) &&
                     entity.TypeOfRestId == (long) TypeOfRestEnum.CompensationYouthRest)
            {
                entity.DateRequest = entity.DateRequest ?? DateTime.Now;
                var numberKey = "3";
                serviceNumber = "00000" + unitOfWork.GetNextNumber("КОМП" + "-" + numberKey + "-" + year);
                serviceNumber = string.Format("{2}-{0}-{1}", serviceNumber.Substring(serviceNumber.Length - 5), year,
                    numberKey);
                entity.RequestNumber = serviceNumber;
            }

            if (string.IsNullOrWhiteSpace(serviceNumber))
            {
                return;
            }

            if (entity.Repared)
            {
                return;
            }

            var subStatuses =
                unitOfWork
                    .GetSet<RequestStatusChainForMpgu>().Where(s => !s.RequestEventId.HasValue)
                    .Where(s => s.StatusAction == null || s.StatusAction.Code == action)
                    .Where(s => s.StatusId == null || s.StatusId == statusId)
                    .Where(s => s.IsFirstCompany == null || s.IsFirstCompany == entity.IsFirstCompany)
                    .Where(s => s.RequestOnMoney == null || s.RequestOnMoney == entity.RequestOnMoney)
                    .Where(s => s.YearOfRestId == null || s.YearOfRestId == entity.YearOfRestId)
                    .Where(s => s.DeclineReasonId == null || s.DeclineReasonId == entity.DeclineReasonId)
                    .OrderBy(s => s.YearOfRestId.HasValue ? 0 : 1)
                    .ThenBy(s => s.StatusActionId.HasValue ? 0 : 1)
                    .ThenBy(s => s.IsFirstCompany.HasValue ? 0 : 1)
                    .ThenBy(s => s.RequestOnMoney.HasValue ? 0 : 1)
                    .ThenBy(s => s.DeclineReasonId.HasValue ? 0 : 1)
                    .ThenBy(s => s.StatusId.HasValue ? 0 : 1)
                    .ToList();

            var subStatus = subStatuses.FirstOrDefault();

            //Пришла информация с портала о выборе
            if ((subStatus?.Id == (long) StatusEnum.DecisionIsMade ||
                 subStatus?.Id == (long) StatusEnum.DecisionIsMade + 10000) && isFromMpgu)
            {
                subStatus = unitOfWork.GetById<RequestStatusChainForMpgu>(
                    (long) StatusEnum.CertificateIssued + 202010000) ?? subStatus;
            }

            //Пришла информация с портала о выборе
            if (subStatus?.Id == (long) StatusEnum.Reject + 140000 && isFromMpgu)
            {
                subStatus = unitOfWork.GetById<RequestStatusChainForMpgu>(
                    (long) StatusEnum.Reject + 150000) ?? subStatus;
            }

            //Отказ от сертификата (не уважительная причина) ветка сертификатов
            if (subStatus?.Id == (long) StatusEnum.CancelByApplicant + 20000 &&
                (entity.TypeOfRest?.Id == (long) TypeOfRestEnum.Money ||
                 entity.TypeOfRest?.ParentId == (long) TypeOfRestEnum.Money))
            {
                subStatus = unitOfWork.GetById<RequestStatusChainForMpgu>(
                    (long) StatusEnum.CancelByApplicant + 2020050000) ?? subStatus;
            }

            //Отказ от сертификата (уважительная причина) ветка сертификатов
            if (subStatus?.Id == (long) StatusEnum.CancelByApplicant &&
                (entity.TypeOfRest?.Id == (long) TypeOfRestEnum.Money ||
                 entity.TypeOfRest?.ParentId == (long) TypeOfRestEnum.Money))
            {
                subStatus = unitOfWork.GetById<RequestStatusChainForMpgu>(
                    (long) StatusEnum.CancelByApplicant + 2020040000) ?? subStatus;
            }


            if (subStatus?.Statuses == null || !subStatus.Statuses.Any())
            {
                var message = new StatusMessage
                {
                    ServiceNumber = serviceNumber,
                    ResponseDate = DateTime.Now,
                    StatusCode = Convert.ToInt32(entity.Status.ExternalUid),
                    Note = string.IsNullOrEmpty(comment)
                        ? entity.Status.MpguComment +
                          (statusId == (long) StatusEnum.Reject || statusId == (long) StatusEnum.CancelByApplicant
                              ? $" {entity.NullSafe(m => m.DeclineReason.Name)}"
                              : string.Empty)
                        : comment
                };

                RequestStatusForMpgu r = null;

                if ((entity.TypeOfRestId == (long) TypeOfRestEnum.Compensation ||
                     entity.TypeOfRestId == (long) TypeOfRestEnum.CompensationYouthRest) &&
                    (entity.StatusId == (long) StatusEnum.Send ||
                     entity.StatusId == (long) StatusEnum.CertificateIssued ||
                     entity.StatusId == (long) StatusEnum.Reject))
                {
                    message.Note = CompensationTextGeneration(entity);
                    r = CompensationStatusChinDummyGeneration(entity);
                }

                if (entity.StatusId == (long) StatusEnum.CertificateIssued)
                {
                    AppendCertificateInfoMessage(entity, message, serviceNumber);
                }

                if (entity.StatusId != (long) StatusEnum.ErrorRequest &&
                    entity.SourceId == (long) SourceEnum.Operator && (r == null || r.SendEmail))
                {
                    SendEmailOnChangeStatus(unitOfWork, entity, entity.Status.Name, message.Note, planDate, r,
                        accountId);
                }

                if (entity.StatusId == (long) StatusEnum.DecisionIsMade)
                {
                    message.Note = string.Empty;
                    if (entity.RequestOnMoney)
                    {
                        message.Note += "Вы выбрали сертификат на выплату.";
                    }
                    else
                    {
                        var hotel = entity.Tour?.Hotels ?? entity?.Hotels;
                        var dateIncome = entity.Tour.DateIncome ?? entity.DateIncome;
                        var dateOutcome = entity.Tour.DateOutcome ?? entity.DateOutcome;
                        /*var countPlace = entity.CountPlace + entity.CountAttendants;
                        if ((entity.TypeOfRestId == (long) TypeOfRestEnum.YouthRestCamps ||
                             entity.TypeOfRestId == (long) TypeOfRestEnum.YouthRestOrphanCamps) && countPlace == 0)
                        {
                            countPlace = 1;
                        }*/

                        if (hotel != null && dateOutcome.HasValue && dateIncome.HasValue)
                        {
                            message.Note +=
                                $@"Вы выбрали: {hotel.Name}{(!string.IsNullOrWhiteSpace(hotel.Address) ? $", {hotel.Address}" : "")} - с {dateIncome:dd.MM.yyyy} по {dateOutcome:dd.MM.yyyy}.";
                        }
                    }
                }

                message.Note = message.Note.Replace("\n", "<br/>");

                unitOfWork.AddEntity(new ExchangeUTS
                {
                    DateCreate = message.ResponseDate ?? DateTime.Now,
                    Incoming = false,
                    Message = Serialization.Serializer(message),
                    Processed = false,
                    FromOrgCode = fromCode,
                    ToOrgCode = toCode,
                    MessageId = Guid.NewGuid().ToString(),
                    QueueName = ConfigurationManager.AppSettings["MqRequestStatus"],
                    ServiceNumber = entity.RequestNumber,
                    RequestId = entity.Id,
                    ToState = statusId,
                    BookingGuid = entity.BookingGuid,
                    TypeOfRestId = entity.TypeOfRestId,
                    DateToSend = planDate,
                    AccountId = accountId
                });
            }
            else
            {
                var statusesForSend = subStatus.Statuses.OrderBy(s => s.OrderField).ToList();

                // отправка цепочки статусов
                foreach (var s in statusesForSend)
                {
                    var message = new StatusMessage
                    {
                        ServiceNumber = serviceNumber,
                        ResponseDate = DateTime.Now,
                        StatusCode = Convert.ToInt32(s.Status),
                        ReasonCode = s.ReasonCode,
                        Note = s.Commentary
                    };

                    if (s.Status == (long) StatusEnum.Send)
                    {
                        message.Note = message.Note.Replace("зарегистрировано",
                            $"зарегистрировано под № {entity.RequestNumber} от {entity.DateRequest.FormatEx()}");
                    }

                    if (s.Status == (long) StatusEnum.DecisionIsMade ||
                        s.Status == (long) StatusEnum.EndOfCompanyFirstStep)
                    {
                        message.Note = message.Note.Replace("Вы выбрали:",
                            $"Вы выбрали: {entity.Tour?.Hotels?.Name}, {entity.Tour?.TimeOfRest?.Name}, с {entity.Tour?.DateIncome:dd.MM.yyyy} по {entity.Tour?.DateOutcome:dd.MM.yyyy}.");
                    }

                    if (s.Status == (long) StatusEnum.DecisionIsMade && message.ReasonCode == "1")
                    {
                        if (entity.RequestOnMoney)
                        {
                            message.Note = "Вы выбрали сертификат на выплату.";
                        }
                        else
                        {
                            var hotel = entity.Tour?.Hotels ?? entity?.Hotels;
                            var dateIncome = entity.Tour?.DateIncome ?? entity.DateIncome;
                            var dateOutcome = entity.Tour?.DateOutcome ?? entity.DateOutcome;
                            var countPlace = entity.CountPlace + entity.CountAttendants;

                            if ((entity.TypeOfRestId == (long) TypeOfRestEnum.YouthRestCamps ||
                                 entity.TypeOfRestId == (long) TypeOfRestEnum.YouthRestOrphanCamps) && countPlace == 0)
                            {
                                countPlace = 1;
                            }

                            if (hotel != null && dateOutcome.HasValue && dateIncome.HasValue)
                            {
                                message.Note =
                                    $@"Вы выбрали: {hotel.Name}{(!string.IsNullOrWhiteSpace(hotel.Address) ? $", {hotel.Address}" : "")} - с {dateIncome:dd.MM.yyyy} по {dateOutcome:dd.MM.yyyy}. Количество мест: {countPlace}.";
                            }
                            else
                            {
                                message.Note =
                                    "Вся необходимая информация о выборе путевки на отдых и оздоровление детей от заявителя получена.\n";
                            }
                        }
                    }

                    if (s.Status == (long) StatusEnum.CertificateIssued)
                    {
                        AppendCertificateInfoMessage(entity, message, serviceNumber);
                    }

                    if (entity.StatusId != (long) StatusEnum.ErrorRequest &&
                        entity.SourceId == (long) SourceEnum.Operator && s.SendEmail)
                    {
                        SendEmailOnChangeStatus(unitOfWork, entity, s.Name, message.Note, planDate, s, accountId);
                    }

                    if ((s.Chain?.StatusId ?? -1) == (long)StatusEnum.RegistrationDecline && s.Status == (long)StatusEnum.RegistrationDeclineBecauseDuplicate && !s.ReasonCode.IsNullOrEmpty())
                    {
                        message.Note = comment;
                    }

                    message.Note = message.Note?.Replace("\n", "<br/>") ?? string.Empty;

                    unitOfWork.AddEntity(new ExchangeUTS
                    {
                        DateCreate = message.ResponseDate ?? DateTime.Now,
                        Incoming = false,
                        Message = Serialization.Serializer(message),
                        Processed = false,
                        FromOrgCode = fromCode,
                        ToOrgCode = toCode,
                        MessageId = Guid.NewGuid().ToString(),
                        QueueName = ConfigurationManager.AppSettings["MqRequestStatus"],
                        ServiceNumber = entity.RequestNumber,
                        RequestId = entity.Id,
                        ToState = s.Status,
                        BookingGuid = entity.BookingGuid,
                        TypeOfRestId = entity.TypeOfRestId,
                        DateToSend = planDate,
                        AccountId = accountId
                    });
                }
            }

            unitOfWork.SaveChanges();
        }

        /// <summary>
        ///     добавление сертификата во вложение
        /// </summary>
        private static void SendEmailOnChangeStatus(IUnitOfWork unitOfWork, Request entity, string statusName,
            string statusDescription, DateTime? planDate, RequestStatusForMpgu r = null, long? accountId = null)
        {
            var sd = statusDescription;
            if (!string.IsNullOrWhiteSpace(sd))
            {
                sd =
                    $@"<div style=""padding-bottom: 15px;""><div style=""font-weight: bold;padding-bottom: 7px;"">Информация по заявке обновлена:</div><pre style=""font-family: arial; font-size: 14px; "">{statusDescription}</pre></div>";
            }


            var messageText =
                $@"<div style=""font-family: arial; font-size: 14px; padding-bottom: 30px;padding-top: 20px; padding-left:15px"">
                    <div style=""padding-bottom: 15px;"">Здравствуйте, {entity.Applicant.LastName} {entity.Applicant.FirstName} {entity.Applicant.MiddleName}!</div>
		            <div style=""padding-bottom: 15px;"">По Вашему заявлению произошли изменения {planDate ?? DateTime.Now:dd.MM.yyyy в HH:mm:ss}</div>
		            <div style=""padding-bottom: 15px;""><span style=""font-weight: bold;"">Номер заявления:</span>&nbsp;<span style=""font-style: italic;"">{entity.RequestNumber}</span></div>
		            <div style=""padding-bottom: 15px;""><span style=""font-weight: bold;"">Статус заявления:</span>&nbsp;<span style=""font-style: italic;"">{statusName}</span></div>
		            {sd}
                </div>
	            <div style=""font-family: arial; font-size: 14px;color: #afafaf; padding-left:15px""><hr/>С уважением,<br/>ГАУК ""МОСГОРТУР""</div>";

            var emailAndSms = new SendEmailAndSms
            {
                IsEmailSended = !entity.NeedEmail,
                IsSmsSended = !entity.NeedSms,
                DateCreate = DateTime.Now,
                Email = entity.Applicant.Email,
                Phone = entity.Applicant.Phone,
                EmailMessage = messageText,
                EmailTitle = "Путевки на отдых и оздоровление детей",
                SmsMessage = entity.Status?.SmsMessage,
                RequestId = entity.EntityId,
                StatusRequestId = entity.StatusId,
                DateEmail = planDate
            };

            if (string.IsNullOrEmpty(emailAndSms.EmailMessage) || string.IsNullOrEmpty(emailAndSms.Email))
            {
                emailAndSms.IsEmailSended = true;
            }
            else
            {
                emailAndSms.Attachments = new List<SendEmailAndSmsAttachment>();
                foreach (var cs in r?.CshedDocAndSign ?? new List<RequestStatusCshedSendAndSignDocument>(0))
                {
                    emailAndSms.Attachments.Add(
                        new SendEmailAndSmsAttachment
                        {
                            Name = cs.DocumentPath,
                            UrlToDownload = cs.DocumentPath + "|" + (accountId ?? GetCurrentAccountId())
                        });
                }
            }

            if (string.IsNullOrEmpty(emailAndSms.Phone))
            {
                emailAndSms.IsSmsSended = true;
            }

            if (!emailAndSms.IsEmailSended || !emailAndSms.IsSmsSended)
            {
                unitOfWork.AddEntity(emailAndSms);
            }
        }

        /// <summary>
        ///     запись в историю
        /// </summary>
        public static void WriteHistory(this IUnitOfWork unitOfWork, long id, string message, long? accountId)
        {
            unitOfWork.AddEntity(new HistoryRequest
            {
                AccountId = accountId ?? GetCurrentAccountId(),
                Operation = message,
                OperationDate = DateTime.Now,
                RequestId = id
            });

            var user = unitOfWork.GetById<Account>(accountId ?? GetCurrentAccountId());

            var browser = HttpContext.Current?.Request.UserAgent;

            unitOfWork.AddEntity(new SecurityJournal
            {
                SecurityJournalTypeId = (long) SecurityJournalEventType.Processes,
                EventName = "Изменение заявления",
                UserName = $"{user?.Name} ({user?.Id})",
                DateEvent = DateTime.Now,
                Description = $"Пользователь {user?.Name} ({user?.Id}) изменил заявление ({id}): {message}",
                Brouser = browser
            });
        }

        private static void AppendCertificateInfoMessage(Request entity, StatusMessage message, string serviceNumber)
        {
            var certificateInfo = new certificateInfo
            {
                applicant =
                    new certificateInfoApplicant
                    {
                        firstName = entity.Applicant.FirstName,
                        lastName = entity.Applicant.LastName,
                        middleName = entity.Applicant.MiddleName,
                        documentNumber =
                            string.Format("{2} серия: {0}, номер {1}", entity.Applicant.DocumentSeria,
                                entity.Applicant.DocumentNumber,
                                entity.Applicant.NullSafe(m => m.DocumentType.Name))
                    },
                attendants = entity.Attendant.Select(
                    c =>
                        new certificateInfoAttendant
                        {
                            dateOfBirth = (c.DateOfBirth ?? default).XmlToString(),
                            lastName = c.LastName,
                            middleName = c.MiddleName,
                            firstName = c.FirstName,
                            documentNumber =
                                string.Format("{2} серия: {0}, номер {1}", c.DocumentSeria, c.DocumentNumber,
                                    c.NullSafe(m => m.DocumentType.Name))
                        }).ToArray(),
                children =
                    entity.Child?.Select(
                        c =>
                            new certificateInfoChild
                            {
                                dateOfBirth = (c.DateOfBirth ?? default).XmlToString(),
                                lastName = c.LastName,
                                middleName = c.MiddleName,
                                firstName = c.FirstName,
                                documentNumber =
                                    string.Format("{2} серия: {0}, номер {1}", c.DocumentSeria, c.DocumentNumber,
                                        c.NullSafe(m => m.DocumentType.Name))
                            }).ToArray(),
                serviceNumber = string.IsNullOrEmpty(entity.CertificateNumber)
                    ? serviceNumber
                    : entity.CertificateNumber,
                serviceDate = (entity.DateChangeStatus ?? DateTime.Now).Date.XmlToString(),
                placeOfRest = entity.Tour?.Hotels.Name ?? "-",
                dateStart = (entity.Tour?.DateIncome ?? DateTime.MinValue).XmlToString(),
                dateEnd = (entity.Tour?.DateOutcome ?? DateTime.MinValue).XmlToString()
            };

            if (entity.Applicant.IsAccomp)
            {
                var attends = certificateInfo.attendants.ToList();
                attends.Add(new certificateInfoAttendant
                {
                    firstName = entity.Applicant.FirstName,
                    lastName = entity.Applicant.LastName,
                    middleName = entity.Applicant.MiddleName,
                    dateOfBirth = (entity.Applicant.DateOfBirth ?? default).XmlToString(),
                    documentNumber =
                        string.Format("{2} серия: {0}, номер {1}", entity.Applicant.DocumentSeria,
                            entity.Applicant.DocumentNumber,
                            entity.NullSafe(m => m.Applicant.DocumentType.Name))
                });
                certificateInfo.attendants = attends.ToArray();
            }

            if (!certificateInfo.attendants.Any() || (!entity.TypeOfRest?.NeedAccomodation ?? false))
            {
                certificateInfo.attendants = null;
            }

            var doc = new XmlDocument();
            doc.LoadXml(Serialization.Serializer(certificateInfo));

            message.Documents = new[] {new ServiceDocument {CustomAttributes = doc.DocumentElement}};
        }

        /// <summary>
        ///     определение статуса
        /// </summary>
        public static Request RequestChangeStatusInternal(this IUnitOfWork unitOfWork, string actionCode,
            Request request, long? declineReason = null, bool checkValidationStep = true, long? accountId = null,
            DateTime? planDate = null, bool isFromMpgu = false)
        {
            var action = unitOfWork.GetSet<StatusAction>()
                             .FirstOrDefault(sa =>
                                 sa.Code == actionCode && request.RequestOnMoney == sa.RequestOnMoney) ??
                         unitOfWork.GetSet<StatusAction>()
                             .FirstOrDefault(sa => sa.Code == actionCode && !sa.RequestOnMoney.HasValue);

            if (action == null)
            {
                return request;
            }

            if (checkValidationStep && action.FromStatus.All(s => s.Id != request.StatusId))
            {
                return request;
            }

            accountId ??= SecurityBasis.GetCurrentAccountId();

            var from = (request.Status?.Name ?? unitOfWork.GetById<Status>(request.StatusId ?? 0)?.Name).FormatEx();
            var to = (action.ToStatus?.Name ?? unitOfWork.GetById<Status>(action.ToStatusId ?? 0)?.Name).FormatEx();

            var declineReasonText = string.Empty;

            var certsTypesOfRest = unitOfWork.GetSet<TypeOfRest>().Where(ss =>
                ss.Id == (long) TypeOfRestEnum.Money || ss.ParentId == (long) TypeOfRestEnum.Money).Select(ss => ss.Id).ToList();

            if (actionCode == AccessRightEnum.Status.FcRepareRequest && certsTypesOfRest.Any(ss => request.TypeOfRestId == ss))
            {
                actionCode = AccessRightEnum.Status.FcToCertificateIssued;
                action = unitOfWork.GetSet<StatusAction>()
                             .FirstOrDefault(sa =>
                                 sa.Code == actionCode && request.RequestOnMoney == sa.RequestOnMoney) ??
                         unitOfWork.GetSet<StatusAction>()
                             .FirstOrDefault(sa => sa.Code == actionCode && !sa.RequestOnMoney.HasValue) ?? action;

                to = (action.ToStatus?.Name ?? unitOfWork.GetById<Status>(action.ToStatusId ?? 0)?.Name).FormatEx();
            }

            long?[] typesOfRest1080To10753 =
            {
                (long?) TypeOfRestEnum.ChildRestOrphanCamps, (long?) TypeOfRestEnum.RestWithParentsOrphan,
                (long?) TypeOfRestEnum.RestWithParentsComplex, (long?) TypeOfRestEnum.YouthRestOrphanCamps,
                (long?) TypeOfRestEnum.TentChildrenCampOrphan
            };

            if ((!typesOfRest1080To10753.Contains(request.TypeOfRestId)
                || (request.TypeOfRestId == (long?)TypeOfRestEnum.RestWithParentsComplex && !request.Child.Any(sx =>
                     sx.BenefitType?.Id == (long)BenefitTypeEnum.Orphans ||
                     sx.BenefitType?.SameBenefitId == (long)BenefitTypeEnum.Orphans)))
                && declineReason == Settings.Default.NotParticipateInSecondStage
                && action.ToStatusId == (long) StatusEnum.Reject)
            {
                actionCode = AccessRightEnum.RequestTo10753;
                action = unitOfWork.GetSet<StatusAction>()
                             .FirstOrDefault(sa =>
                                 sa.Code == actionCode && request.RequestOnMoney == sa.RequestOnMoney) ??
                         unitOfWork.GetSet<StatusAction>()
                             .FirstOrDefault(sa => sa.Code == actionCode && !sa.RequestOnMoney.HasValue) ?? action;

                to = (action.ToStatus?.Name ?? unitOfWork.GetById<Status>(action.ToStatusId ?? 0)?.Name).FormatEx();
            }

            if (action.Code == AccessRightEnum.RequestTo10753)
            {
                declineReason = Settings.Default.NotParticipateInSecondStage;
                declineReasonText = ", неучастие заявителя во втором этапе заявочной кампании";
            }
            else
            {
                var dr = unitOfWork.GetById<DeclineReason>(declineReason);
                if (dr != null)
                {
                    declineReasonText = $"({dr.Name})";
                }
            }

            unitOfWork.WriteHistory(request.Id, $"Изменение статуса c '{from}' на '{to}'{declineReasonText}",
                accountId);

            request.UpdateDate = DateTime.Now;

            if (request.StatusId != (long) StatusEnum.CertificateIssued &&
                action.ToStatusId == (long) StatusEnum.CertificateIssued)
            {
                request.MayFinalSend = true;
            }

            request.StatusId = action.ToStatusId;
            request.DateChangeStatus = DateTime.Now;
            request.IsDraft = false;

            if (declineReason.HasValue)
            {
                request.DeclineReasonId = declineReason.Value;
            }
            else if (action.ToStatusId != (long) StatusEnum.Reject &&
                     action.ToStatusId != (long) StatusEnum.CancelByApplicant &&
                     action.ToStatusId != (long) StatusEnum.CertificateIssued)
            {
                request.DeclineReasonId = null;
            }

            if (action.ToStatusId == (long) StatusEnum.CertificateIssued &&
                request.TypeOfRestId != (long) TypeOfRestEnum.Compensation &&
                request.TypeOfRestId != (long) TypeOfRestEnum.CompensationYouthRest &&
                string.IsNullOrWhiteSpace(request.CertificateNumber) &&
                !declineReason.HasValue)
            {
                unitOfWork.SetCertificateNumber(request);
            }

            if (action.ToStatusId == (long) StatusEnum.CertificateIssued ||
                action.ToStatusId == (long) StatusEnum.Reject ||
                action.ToStatusId == (long) StatusEnum.RegistrationDecline)
            {
                var children = unitOfWork.GetSet<Child>().Where(c => c.RequestId == request.Id).ToList();
                foreach (var child in children.Where(child => child.ContingentGuid.HasValue))
                {
                    child.EkisNeedSend = true;
                }
            }

            request = unitOfWork.Update(request);

            if (actionCode == AccessRightEnum.Status.ToSend ||
                actionCode == AccessRightEnum.Status.ToRegistrationDecline ||
                actionCode == AccessRightEnum.Status.ToRegistrationDeclineAttendant)
            {
                request = SendRequest(unitOfWork, request);
            }

            unitOfWork.SendChangeStatus(request, action.ToStatusId ?? 0, string.Empty, actionCode, planDate, accountId,
                isFromMpgu);
            request = unitOfWork.GetById<Request>(request.Id);
            UpdateIntervalDates(request);
            request.ForIndex = true;
            unitOfWork.SaveChanges();
            request.UpdateRequestInBooking();

            return request;
        }

        public static void SetCertificateNumber(this IUnitOfWork unitOfWork, Request request)
        {
            var number = unitOfWork.GetNextNumber($"CN-{request.YearOfRestId}");
            request.CertificateNumber = $"000000{number}/{request.YearOfRest.Name.Substring(2)}";
            request.CertificateNumber = request.CertificateNumber.Substring(request.CertificateNumber.Length - 9);
            request.CertificateDate = DateTime.Now;
        }

        /// <summary>
        ///     обновление информации о заявлении в сервисе бронирования
        /// </summary>
        public static void UpdateRequestInBooking(this Request request)
        {
            // обновление статуса заявления
            if (request.IsFirstCompany && !request.IsDeleted && !request.RequestOnMoney &&
                new long?[] {(long) StatusEnum.DecisionMakingCovid, (long) StatusEnum.DecisionMaking}.Contains(
                    request.StatusId))
            {
                var bookingClient = Booking.GetServiceClient(new BaseRequest {TypeOfRestId = request.TypeOfRestId});
                try
                {
                    bookingClient.UpdateRequest(request.TypeOfRestId ?? 0, request.Id);
                }
                finally
                {
                    Booking.CloseClient(bookingClient);
                }
            }
        }

        /// <summary>
        ///     Обновление временных интервалов
        /// </summary>
        public static void UpdateIntervalDates(this Request request)
        {
            if (request.StatusId != (long) StatusEnum.Draft && request.StatusId != (long) StatusEnum.Reject &&
                request.StatusId != (long) StatusEnum.RegistrationDecline
                && request.StatusId != (long) StatusEnum.ErrorRequest &&
                request.StatusId != (long) StatusEnum.CancelByApplicant)
            {
                if (request.Tour != null)
                {
                    var intervalStart = request.Tour.DateIncome?.Date.Ticks;
                    var intervalEnd = request.Tour.DateOutcome?.Date.AddDays(1).Ticks;

                    if (request.Child != null)
                    {
                        foreach (var child in request.Child)
                        {
                            if (child.IsDeleted)
                            {
                                continue;
                            }

                            child.IntervalStart = intervalStart;
                            child.IntervalEnd = intervalEnd;
                        }
                    }

                    if (request.Attendant != null)
                    {
                        foreach (var attendant in request.Attendant)
                        {
                            attendant.IntervalStart = intervalStart;
                            attendant.IntervalEnd = intervalEnd;
                        }
                    }

                    if (request.Applicant != null)
                    {
                        request.Applicant.IntervalStart = intervalStart;
                        request.Applicant.IntervalEnd = intervalEnd;
                    }
                }
            }
            else
            {
                if (request.Child != null)
                {
                    foreach (var child in request.Child)
                    {
                        child.IntervalStart = null;
                        child.IntervalEnd = null;
                    }
                }

                if (request.Attendant != null)
                {
                    foreach (var attendant in request.Attendant)
                    {
                        attendant.IntervalStart = null;
                        attendant.IntervalEnd = null;
                    }
                }

                if (request.Applicant != null)
                {
                    request.Applicant.IntervalStart = null;
                    request.Applicant.IntervalEnd = null;
                }
            }
        }

        /// <summary>
        ///     заполнение базовых сущностный
        /// </summary>
        private static void FillRequestBaseData(request target, Request source)
        {
            var items = new List<object>();
            var itemsElementName = new List<ItemsChoiceType>();

            items.Add(Convert.ToInt32(source.TypeOfRestId ?? 0));
            itemsElementName.Add(ItemsChoiceType.typeOfRest);

            items.Add(Convert.ToInt32(source.TimeOfRestId ?? 0));
            itemsElementName.Add(ItemsChoiceType.timeOfRest);

            items.Add(Convert.ToInt32(source.PlaceOfRestId ?? 0));
            itemsElementName.Add(ItemsChoiceType.placeOfRest);

            if (source.MainPlaces.HasValue)
            {
                items.Add(Convert.ToInt32(source.MainPlaces ?? 0));
                itemsElementName.Add(ItemsChoiceType.mainPlaces);
            }

            if (source.AdditionalPlaces.HasValue)
            {
                items.Add(Convert.ToInt32(source.AdditionalPlaces ?? 0));
                itemsElementName.Add(ItemsChoiceType.additionalPlaces);
            }

            if (source.SubjectOfRestId.HasValue)
            {
                items.Add(Convert.ToInt32(source.SubjectOfRestId ?? 0));
                itemsElementName.Add(ItemsChoiceType.subjectOfRest);
            }

            target.Items = items.ToArray();
            target.ItemsElementName = itemsElementName.ToArray();
        }

        /// <summary>
        ///     Отправить заявление
        /// </summary>
        public static Request SendRequest(this IUnitOfWork unitOfWork, Request entity)
        {
            var serviceCode = entity.IsFirstCompany
                ? entity.TypeOfRest?.ServiceCodeFirstCompany
                : entity.TypeOfRest?.ServiceCode;
            if (string.IsNullOrWhiteSpace(serviceCode))
            {
                return entity;
            }

            var exchangeSystemCode = ConfigurationManager.AppSettings["exchangeSystemCode"] ?? "9000063";
            //очередной костыль для невнятной проблемы с пропадающим систем кодом
            if (exchangeSystemCode.IsNullOrEmpty())
                exchangeSystemCode = "9000063";
            var fromCode = ConfigurationManager.AppSettings["exchangeBaseRegistryFromCode"];
            var toCode = ConfigurationManager.AppSettings["exchangeMpguToCode"];
            var keyRef = new Dictionary<long, int>();
            var index = 0;

            var serviceNumber = entity.RequestNumber;
            if (string.IsNullOrWhiteSpace(serviceNumber))
            {
                var year = (DateTime.Now.Year % 100).ToString(CultureInfo.InvariantCulture);
                var exchangeBaseRegistryCode = $"{fromCode}-{exchangeSystemCode}-{serviceCode}";
                serviceNumber = "0000000" + unitOfWork.GetNextNumber(exchangeBaseRegistryCode + year);
                serviceNumber =
                    $"{exchangeBaseRegistryCode}-{serviceNumber.Substring(serviceNumber.Length - 7)}/{year}";
            }

            var requestData = new request
            {
                needEmailSpecified = true,
                needSmsSpecified = true,
                needEmail = entity.NeedEmail,
                needSms = entity.NeedSms,
                dateRequest = entity.DateRequest.DateTimeToXml() ?? DateTime.Now.DateTimeToXml()
            };

            FillRequestBaseData(requestData, entity);

            if (entity.Applicant != null)
            {
                var applicant = entity.Applicant;
                requestData.declarant = new requestDeclarant
                {
                    lastName = applicant.LastName,
                    firstName = applicant.FirstName,
                    middleName = applicant.MiddleName,
                    middleNameAbsent = applicant.HaveMiddleName,
                    applicantType = applicant.ApplicantTypeId.ToInt(),
                    documentType = applicant.DocumentTypeId.ToInt(),
                    documentSeria = applicant.DocumentSeria,
                    documentNumber = applicant.DocumentNumber,
                    documentDateOfIssue = applicant.DocumentDateOfIssue.XmlToString(),
                    documentSubjectIssue = applicant.DocumentSubjectIssue,
                    phone = applicant.Phone,
                    email = applicant.Email,
                    placeOfBirth = applicant.PlaceOfBirth,
                    dateOfBirth = applicant.DateOfBirth.XmlToString(),
                    isAccomp = applicant.IsAccomp,
                    snils = applicant.Snils
                };

                if (applicant.ForeginTypeId.HasValue)
                {
                    requestData.declarant.foreginDocument = new requestDeclarantForeginDocument
                    {
                        documentDateOfIssue = applicant.ForeginDateOfIssue.XmlToString(),
                        documentNumber = applicant.ForeginNumber,
                        documentSeria = applicant.ForeginSeria,
                        documentSubjectIssue = applicant.ForeginSubjectIssue,
                        documentType = applicant.ForeginTypeId.ToInt(),
                        documentEndDate = applicant.ForeginDateEnd.XmlToString()
                    };
                }

                keyRef.Add(applicant.Id, index);
            }

            requestData.headDeclarant = entity.AgentApplicant ?? false;

            if (entity.Agent != null && (entity.AgentApplicant ?? false))
            {
                var agent = entity.Agent;
                requestData.head = new requestHead
                {
                    lastName = agent.LastName,
                    firstName = agent.FirstName,
                    middleName = agent.MiddleName,
                    middleNameAbsent = agent.HaveMiddleName,
                    documentType = agent.DocumentTypeId.ToInt(),
                    documentSeria = agent.DocumentSeria,
                    documentNumber = agent.DocumentNumber,
                    documentDateOfIssue = agent.DocumentDateOfIssue.XmlToString(),
                    documentSubjectIssue = agent.DocumentSubjectIssue,
                    phone = agent.Phone,
                    email = agent.Email,
                    proxyDateOfIssure = agent.ProxyDateOfIssure.XmlToString(),
                    proxyEndDate = agent.ProxyEndDate.XmlToString(),
                    notaryName = agent.NotaryName,
                    proxyNumber = agent.ProxyNumber
                };
            }

            requestData.attendants = new requestAttendants();

            var attendants = new List<requestAttendantsAttendant>();
            index++;
            foreach (var attendant in entity.Attendant)
            {
                keyRef.Add(attendant.Id, index);

                var item = new requestAttendantsAttendant
                {
                    lastName = attendant.LastName,
                    firstName = attendant.FirstName,
                    middleName = attendant.MiddleName,
                    middleNameAbsent = attendant.HaveMiddleName,
                    documentType = attendant.DocumentTypeId.ToInt(),
                    documentSeria = attendant.DocumentSeria,
                    documentNumber = attendant.DocumentNumber,
                    documentDateOfIssue = attendant.DocumentDateOfIssue.XmlToString(),
                    documentSubjectIssue = attendant.DocumentSubjectIssue,
                    email = attendant.Email,
                    number = index,
                    phone = attendant.Phone,
                    placeOfBirth = attendant.PlaceOfBirth,
                    dateOfBirth = attendant.DateOfBirth.XmlToString(),
                    snils = attendant.Snils
                };

                if (attendant.ForeginTypeId.HasValue)
                {
                    item.foreginDocument = new requestAttendantsAttendantForeginDocument
                    {
                        documentDateOfIssue = attendant.ForeginDateOfIssue.XmlToString(),
                        documentNumber = attendant.ForeginNumber,
                        documentSeria = attendant.ForeginSeria,
                        documentSubjectIssue = attendant.ForeginSubjectIssue,
                        documentType = attendant.ForeginTypeId.ToInt(),
                        documentEndDate = attendant.ForeginDateEnd.XmlToString()
                    };
                }

                index++;
                attendants.Add(item);
            }

            requestData.attendants.attendant = attendants.ToArray();

            requestData.childs = new requestChilds();

            var children = new List<requestChildsChild>();

            foreach (var child in entity.Child)
            {
                var item = new requestChildsChild
                {
                    lastName = child.LastName,
                    firstName = child.FirstName,
                    middleName = child.MiddleName,
                    middleNameAbsent = child.HaveMiddleName,
                    documentType = child.DocumentTypeId.ToInt(),
                    documentSeria = child.DocumentSeria,
                    documentNumber = child.DocumentNumber,
                    documentDateOfIssue = child.DocumentDateOfIssue.XmlToString(),
                    documentSubjectIssue = child.DocumentSubjectIssue,
                    registeredInMoscow = child.RegisteredInMoscow,
                    school = child.SchoolId.ToInt(),
                    schoolSpecified = child.SchoolId.HasValue,
                    schoolNotPresent = child.SchoolNotPresent,
                    sex = child.Male ? sexType.Item1 : sexType.Item2,
                    dateOfBirth = child.DateOfBirth.XmlToString(),
                    placeOfBirth = child.PlaceOfBirth,
                    snils = child.Snils
                };

                if (child.BenefitTypeId.HasValue)
                {
                    item.benefit = new requestChildsChildBenefit
                    {
                        benefitDate = child.BenefitDate.XmlToString(),
                        benefitEndDate = child.BenefitEndDate.XmlToString(),
                        benefitNeverEnd = child.BenefitNeverEnd,
                        benefitType = child.BenefitTypeId.ToInt(),
                        typeOfRestriction = Convert.ToInt32(child.TypeOfRestrictionId ?? 0),
                        typeOfRestrictionSpecified = child.TypeOfRestrictionId.HasValue,
                        isInvalid = child.IsInvalid,
                        groupInvalid = child.BenefitGroupInvalidId.ToInt(),
                        groupInvalidSpecified = child.BenefitGroupInvalidId.HasValue
                    };
                }

                if (child.ForeginTypeId.HasValue)
                {
                    item.foreginDocument = new requestChildsChildForeginDocument
                    {
                        documentDateOfIssue = child.ForeginDateOfIssue.XmlToString(),
                        documentNumber = child.ForeginNumber,
                        documentSeria = child.ForeginSeria,
                        documentSubjectIssue = child.ForeginSubjectIssue,
                        documentType = child.ForeginTypeId.ToInt(),
                        documentEndDate = child.ForeginDateEnd.XmlToString()
                    };
                }

                if (child.Address != null)
                {
                    var address = child.Address;
                    item.addressRegistration = new addressRegistration
                    {
                        addressUnom = address.BtiAddress?.Unom ?? 0,
                        addressUnomSpecified = address.BtiAddress != null,
                        appartment = address.Appartment,
                        corpus = address.Corpus,
                        house = address.House,
                        street = address.Street,
                        stroenie = address.Stroenie,
                        vladenie = address.Vladenie,
                        district = address.BtiDistrictId.ToInt(),
                        districtSpecified = address.BtiDistrictId.HasValue,
                        region = address.BtiRegionId.ToInt(),
                        regionSpecified = address.BtiRegionId.HasValue,
                        fiasid = address.FiasId,
                        addressName = address.Name
                    };
                }

                if (child.ApplicantId.HasValue)
                {
                    item.attendantChildNumber = keyRef[child.ApplicantId ?? 0];
                    item.attendantChildNumberSpecified = true;
                    item.statusByChild = child.StatusByChildId.ToInt();
                    item.statusByChildSpecified = child.StatusByChildId.HasValue;
                }

                children.Add(item);
            }

            requestData.childs.child = children.ToArray();

            var doc = new XmlDocument();
            doc.LoadXml(Serialization.Serializer(requestData));

            var message = new Message
            {
                Service = new RequestService
                {
                    ServiceNumber = serviceNumber,
                    CreatedByDepartment =
                        new Department
                        {
                            Code = ConfigurationManager.AppSettings["DepartmentCode"],
                            Inn = ConfigurationManager.AppSettings["DepartmentInn"],
                            Name = ConfigurationManager.AppSettings["DepartmentName"],
                            Ogrn = ConfigurationManager.AppSettings["DepartmentOgrn"]
                        }
                },
                CustomAttributes = doc.DocumentElement
            };

            entity.RequestNumber = serviceNumber;
            entity.DateRequest = DateTime.Now;

            entity = unitOfWork.Update(entity);

            unitOfWork.AddEntity(new ExchangeUTS
            {
                DateCreate = DateTime.Now,
                Incoming = false,
                Message = Serialization.Serializer(message),
                Processed = false,
                FromOrgCode = fromCode,
                ToOrgCode = toCode,
                MessageId = Guid.NewGuid().ToString(),
                QueueName = ConfigurationManager.AppSettings["MqRequestOutcoming"],
                ServiceNumber = serviceNumber,
                RequestId = entity.Id
            });

            unitOfWork.SaveChanges();

            return entity;
        }

        /// <summary>
        ///     Генерация текста уведомления по компенсациям (сироты, малообеспеченные и прочие страдальцы)
        /// </summary>
        private static string CompensationTextGeneration(Request entity)
        {
            var isCompensation = entity.TypeOfRestId == (long) TypeOfRestEnum.Compensation;
            var isOrphan = entity.Child?.FirstOrDefault()?.BenefitTypeId == (long) BenefitTypeEnum.CompensationOrphan;

            if (entity.StatusId == (long) StatusEnum.Send)
            {
                if (isCompensation)
                {
                    return
                        @"Ваше заявление на выплату компенсации за самостоятельно приобретенную родителями или иными законными представителями путевку для отдыха и оздоровления (далее – заявление) зарегистрировано.</br>
                        Заявление будет рассмотрено в течение 30 календарных дней.</br>
                        В случае необходимости предоставления дополнительной информации для принятия решения о выплате компенсации за самостоятельно приобретенную родителями или иными законными представителями путевку для отдыха и оздоровления Вы будете проинформированы.</br>
                        Результат рассмотрения заявления будет направлен на электронную почту, указанную в заявлении.";
                }

                return
                    @"Ваше заявление на выплату компенсации за самостоятельно приобретенную путевку для отдыха и оздоровления (далее – заявление) зарегистрировано.</br>
                        Заявление будет рассмотрено в течение 30 календарных дней.</br>
                        В случае необходимости предоставления дополнительной информации для принятия решения о выплате компенсации за самостоятельно приобретенную путевку для отдыха и оздоровления Вы будете проинформированы.</br>
                        Результат рассмотрения заявления будет направлен на электронную почту, указанную в заявлении.";
            }

            if (entity.StatusId == (long) StatusEnum.CertificateIssued)
            {
                if (isCompensation)
                {
                    if (isOrphan)
                    {
                        return
                            @"Ваше заявление на выплату компенсации за самостоятельно приобретенную путевку для отдыха и оздоровления (далее –  заявление) рассмотрено.</br>
                            На основании предоставленных документов принято решение о выплате компенсации.</br>
                            Денежные средства будут перечислены на расчетный счет, указанный в заявлении в срок, не позднее 30 календарных дней со дня принятия нормативного правового акта Правительства Москвы об установлении величины прожиточного минимума в городе Москве на день подачи заявления на выплату компенсации за самостоятельно приобретенную путевку.";
                    }

                    return
                        @"Ваше заявление на выплату компенсации за самостоятельно приобретенную родителями или иными законными представителями путевку для отдыха и оздоровления (далее –  заявление) рассмотрено.</br>
                            На основании предоставленных документов принято решение о выплате компенсации.</br>
                            Денежные средства будут перечислены на расчетный счет, указанный в заявлении, не позднее 30 календарных дней со дня принятия решения о выплате компенсации.";
                }

                return
                    @"Ваше заявление на выплату компенсации за самостоятельно приобретенную путевку для отдыха и оздоровления (далее – заявление) рассмотрено.</br>
                        На основании предоставленных документов принято решение о выплате компенсации.</br>
                        Денежные средства будут перечислены на расчетный счет, указанный в заявлении в срок, не позднее 30 календарных дней со дня принятия нормативного правового акта Правительства Москвы об установлении величины прожиточного минимума в городе Москве на день подачи заявления на выплату компенсации за самостоятельно приобретенную путевку.";
            }

            if (entity.StatusId == (long) StatusEnum.Reject)
            {
                if (isCompensation)
                {
                    return
                        @"Ваше заявление на выплату компенсации за самостоятельно приобретенную родителями или иными законными представителями путевку для отдыха и оздоровления (далее – заявление) рассмотрено.</br>
                        На основании предоставленных документов принято решение об отказе в выплате компенсации. Основание отказа указано в уведомлении (во вложении).";
                }

                return
                    @"Ваше заявление на выплату компенсации за самостоятельно приобретенную путевку для отдыха и оздоровления (далее – заявление) рассмотрено.</br>
                        На основании предоставленных документов принято решение об отказе в выплате компенсации. Основание отказа указано в уведомлении (во вложении).";
            }

            return null;
        }

        /// <summary>
        ///     Генерация фантомных
        /// </summary>
        private static RequestStatusForMpgu CompensationStatusChinDummyGeneration(Request entity)
        {
            if (entity.TypeOfRestId == (long) TypeOfRestEnum.Compensation ||
                entity.TypeOfRestId == (long) TypeOfRestEnum.CompensationGroup ||
                entity.TypeOfRestId == (long) TypeOfRestEnum.CompensationYouthRest)
            {
                return null;
            }

            if (entity.StatusId == (long) StatusEnum.Send)
            {
                return new RequestStatusForMpgu
                {
                    CshedDocAndSign = new List<RequestStatusCshedSendAndSignDocument>
                    {
                        new RequestStatusCshedSendAndSignDocument
                        {
                            DocumentPath = DocumentGenerationEnum.NotificationRegistration
                        }
                    }
                };
            }

            if (entity.StatusId == (long) StatusEnum.CertificateIssued)
            {
                return new RequestStatusForMpgu
                {
                    CshedDocAndSign = new List<RequestStatusCshedSendAndSignDocument>
                    {
                        new RequestStatusCshedSendAndSignDocument
                        {
                            DocumentPath = DocumentGenerationEnum.NotificationAboutDecision
                        }
                    }
                };
            }

            if (entity.StatusId == (long) StatusEnum.Reject)
            {
                return new RequestStatusForMpgu
                {
                    CshedDocAndSign = new List<RequestStatusCshedSendAndSignDocument>
                    {
                        new RequestStatusCshedSendAndSignDocument
                        {
                            DocumentPath = DocumentGenerationEnum.NotificationRefuse
                        }
                    }
                };
            }

            return null;
        }
    }
}
