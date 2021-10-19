using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using RestChild.Comon;
using RestChild.Comon.Enumeration;
using RestChild.DAL;
using RestChild.DAL.RepositoryExtensions;
using RestChild.Domain;
using RestChild.MPGUIntegration;
using RestChild.MPGUIntegration.V61;

namespace MailingDemon.Tasks
{
    [Task]
    public class MPGUIntegrationParseTask : BaseTask
    {
        private readonly string _mqMpguRequestIncomingErrorQueue =
           ConfigurationManager.AppSettings["MqMPGURequestIncomingError"];

        private readonly string _mqMpguRequestIncomingQueue = ConfigurationManager.AppSettings["MqMPGURequestIncoming"];

        private readonly string _mqMpguStatusIncomingErrorQueue =
           ConfigurationManager.AppSettings["MqMPGURequestStatusIncomingError"];

        private readonly string _mqMpguStatusIncomingQueue =
           ConfigurationManager.AppSettings["MqMPGURequestStatusIncoming"];

        private readonly string _mqMpguStatusOutcomingQueue =
           ConfigurationManager.AppSettings["MqMPGURequestStatusOutcoming"];

        protected override void Execute()
        {
            using (var unitOfWork = new UnitOfWork())
            {
                ParseMpguRequestIncoming(unitOfWork);
                ParseMpguStatusIncoming(unitOfWork);
                SetVisitBookingsStatus(unitOfWork);
            }
        }

        /// <summary>
        /// Парсинг бронирований пришедших из МПГУ
        /// </summary>
        private void ParseMpguRequestIncoming(IUnitOfWork unitOfWork)
        {
            var q = unitOfWork.GetSet<ExchangeUTS>()
               .Where(ss =>
                  ss.Incoming && !ss.IsError && !ss.Processed &&
                  ss.QueueName.ToLower() == _mqMpguRequestIncomingQueue.ToLower()).OrderBy(ss => ss.Id).ToList();
            foreach (var eMessage in q)
            {
                string serviceNumber = null;
                var d1 = DateTime.Now;
                try
                {
                    //RestChild.MPGUIntegration.ServiceReference1.CoordinateMessage

                    var msg = Serialization.Deserialize<CoordinateMessage>(eMessage.Message);

                    if (msg.CoordinateDataMessage.SignService.CustomAttributes?.FirstChild == null)
                    {
                        throw new InvalidOperationException("CustomAttributes is null");
                    }

                    var customAttrs =
                       Serialization.Deserialize<ServiceProperties>(msg.CoordinateDataMessage.SignService
                          .CustomAttributes.OuterXml);

                    serviceNumber = msg.CoordinateDataMessage.Service.ServiceNumber;

                    RequestContact contact = null;
                    if (msg.CoordinateDataMessage.SignService.Contacts != null &&
                        msg.CoordinateDataMessage.SignService.Contacts.Any(ss => ss.Type == ContactType.Declarant))
                    {
                        contact =
                           msg.CoordinateDataMessage.SignService.Contacts.First(ss => ss.Type == ContactType.Declarant) as
                              RequestContact;
                    }

                    var b = unitOfWork.GetSet<MGTBookingVisit>().FirstOrDefault(s => s.Id == customAttrs.BookingId);
                    if (b == null)
                    {
                        throw new MPGUParseException("Пребронь не обнаружена");
                    }

                    if (b.StatusId == (long)MGTVisitBookingStatuses.PrebookingCanceled)
                    {
                        throw new MPGUParseException("Пребронь аннулирована");
                    }

                    if (b.StatusId == (long)MGTVisitBookingStatuses.BookingRegistered)
                    {
                        eMessage.Processed = true;
                        eMessage.IsSigned = true;
                        unitOfWork.SaveChanges();
                        continue;
                    }

                    b.ServiceNumber = serviceNumber;
                    b.MPGURegNum = msg.CoordinateDataMessage.Service.RegNum;
                    b.MPGURegDate = msg.CoordinateDataMessage.Service.RegDate;
                    var applicant = b.Persons.FirstOrDefault(p => p.PersonTypeId == 1);

                    if (applicant != null)
                    {
                        applicant.Email = string.IsNullOrWhiteSpace(contact.EMail) ? applicant.Email : contact.EMail;
                        applicant.Phone = !string.IsNullOrWhiteSpace(contact.MobilePhone) ? contact.MobilePhone :
                           !string.IsNullOrWhiteSpace(contact.WorkPhone) ? contact.WorkPhone :
                           !string.IsNullOrWhiteSpace(contact.HomePhone) ? contact.HomePhone : applicant.Phone;
                        applicant.LastName = contact.LastName;
                        applicant.FirstName = contact.FirstName;
                        applicant.MiddleName = contact.MiddleName;
                        applicant.DateOfBirth = contact.BirthDate ?? applicant.DateOfBirth;
                        applicant.Male = contact.Gender == GenderType.Male;
                        applicant.Snils = contact.Snils ?? applicant.Snils;
                    }

                    b.PINCode = Utils.GeneratePin(b.Id);
                    b.StatusId = (long)MGTVisitBookingStatuses.BookingRegistered;

                    if (b.Children.Any())
                    {
                        foreach (var c in b.Children)
                        {
                            c.StatusId = (long)MGTVisitBookingStatuses.BookingRegistered;
                        }
                    }

                    //send status 1050
                    Utils.SendStatusMessage(unitOfWork, _mqMpguStatusOutcomingQueue,
                       new StatusMessageFilter(serviceNumber, 1050, ConfigurationManager.AppSettings["MqMPGUStatus1050Name"],
                          d1)
                       {
                           Note = string.Format(ConfigurationManager.AppSettings["MqMPGUStatus1050Title"], b.VisitCell, b.Id,
                             b.PINCode, b.Target.Name.ToLower()),
                           PINCode = b.PINCode,
                           Cell = b.VisitCell,
                           BookingId = b.Id,
                           Declarant = contact == null
                             ? (StatusMessageFilter.BaseDeclarant?)null
                             : new StatusMessageFilter.BaseDeclarant
                             {
                                 BirthDate = contact.BirthDate,
                                 EMail = contact.EMail,
                                 FirstName = contact.FirstName,
                                 LastName = contact.LastName,
                                 MiddleName = contact.MiddleName,
                                 MobilePhone = contact.MobilePhone,
                                 WorkPhone = contact.WorkPhone
                             },
                           KidsCountValue = customAttrs.KidsCountValue,
                           PurposeValue = customAttrs.PurposeValue
                       });

                    //send status 10090
                    Utils.SendStatusMessage(unitOfWork, _mqMpguStatusOutcomingQueue,
                       new StatusMessageFilter(serviceNumber, 10090,
                          ConfigurationManager.AppSettings["MqMPGUStatus10090Name"], DateTime.Now));

                    eMessage.Processed = true;
                }
                catch (InvalidOperationException e)
                {
                    Utils.SendErrorStatus(unitOfWork, eMessage.Message, _mqMpguRequestIncomingErrorQueue);
                    eMessage.Processed = true;
                    eMessage.IsError = true;
                    eMessage.ErrorText = e.Message;
                    eMessage.ErrorDescription = e.StackTrace;
                }
                catch (MPGUParseException e)
                {
                    if (!string.IsNullOrWhiteSpace(serviceNumber))
                    {
                        Utils.SendStatusMessage(unitOfWork, _mqMpguStatusOutcomingQueue,
                           new StatusMessageFilter(serviceNumber, 103099, "технический сбой", d1)
                           {
                               Note = "Технический сбой. Не удалось записаться. Просим Вас записаться повторно."
                           });
                    }
                    else
                    {
                        Utils.SendErrorStatus(unitOfWork, eMessage.Message, _mqMpguRequestIncomingErrorQueue);
                    }

                    eMessage.Processed = true;
                    eMessage.IsError = true;
                    eMessage.ErrorText = e.Message;
                    eMessage.ErrorDescription = e.StackTrace;
                }
                catch (Exception e)
                {
                    Utils.SendErrorStatus(unitOfWork, eMessage.Message, _mqMpguRequestIncomingErrorQueue);
                    eMessage.Processed = true;
                    eMessage.IsError = true;
                    eMessage.ErrorText = e.Message;
                    eMessage.ErrorDescription = e.StackTrace;
                }

                unitOfWork.SaveChanges();
            }
        }

        /// <summary>
        /// Парсинг стусов бронирования пришедших из МПГУ
        /// </summary>
        private void ParseMpguStatusIncoming(IUnitOfWork unitOfWork)
        {
            var q = unitOfWork.GetSet<ExchangeUTS>()
               .Where(ss =>
                  ss.Incoming && !ss.IsError && !ss.Processed &&
                  ss.QueueName.ToLower() == _mqMpguStatusIncomingQueue.ToLower()).OrderBy(ss => ss.Id).ToList();
            foreach (var eMessage in q)
            {
                string serviceNumber;
                try
                {
                    var msg = Serialization.Deserialize<CoordinateStatusMessage>(eMessage.Message);
                    serviceNumber = msg.CoordinateStatusDataMessage.ServiceNumber;

                    var booking = unitOfWork.GetSet<MGTBookingVisit>()
                       .FirstOrDefault(ss => ss.ServiceNumber.ToLower() == serviceNumber.ToLower());
                    //запрос отзыва заявления
                    if (msg.CoordinateStatusDataMessage.Status.StatusCode == 1069)
                    {
                        long stStatus = 0;

                        if (booking == null)
                        {
                            Utils.SendStatusMessage(unitOfWork, _mqMpguStatusOutcomingQueue,
                               new StatusMessageFilter(serviceNumber, 116999,
                                  ConfigurationManager.AppSettings["MqMPGUStatus116999Name"], DateTime.Now)
                               {
                                   Note = ConfigurationManager.AppSettings["MqMPGUStatus116999Title"]
                               });
                        }
                        else if (booking.StatusId == (long)MGTVisitBookingStatuses.BookingRegistered &&
                                 DateTime.Now >= booking.VisitCell)
                        {
                            Utils.SendStatusMessage(unitOfWork, _mqMpguStatusOutcomingQueue,
                               new StatusMessageFilter(serviceNumber, 1169,
                                  ConfigurationManager.AppSettings["MqMPGUStatus1169Name"], DateTime.Now));
                        }
                        else if (booking.StatusId == (long)MGTVisitBookingStatuses.BookingRegistered)
                        {
                            Utils.SendStatusMessage(unitOfWork, _mqMpguStatusOutcomingQueue,
                               new StatusMessageFilter(serviceNumber, 1090,
                                  ConfigurationManager.AppSettings["MqMPGUStatus1090Name"], DateTime.Now)
                               {
                                   Note = string.Format(ConfigurationManager.AppSettings["MqMPGUStatus1090Title"], booking.Id)
                               });
                            stStatus = (long)MGTVisitBookingStatuses.BookingCanceled;
                        }
                        else if (booking.StatusId > (long)MGTVisitBookingStatuses.BookingCanceled)
                        {
                            Utils.SendStatusMessage(unitOfWork, _mqMpguStatusOutcomingQueue,
                               new StatusMessageFilter(serviceNumber, 1169,
                                  ConfigurationManager.AppSettings["MqMPGUStatus1169Name"], DateTime.Now));
                        }
                        else
                        {
                            Utils.SendStatusMessage(unitOfWork, _mqMpguStatusOutcomingQueue,
                               new StatusMessageFilter(serviceNumber, 116999,
                                  ConfigurationManager.AppSettings["MqMPGUStatus116999Name"], DateTime.Now)
                               {
                                   Note = ConfigurationManager.AppSettings["MqMPGUStatus116999Title"]
                               });
                        }


                        if (stStatus > 0)
                        {
                            if (stStatus == (long)MGTVisitBookingStatuses.BookingCanceled)
                            {
                                var historyLink = WriteHistory(unitOfWork, booking.HistoryLink, "Заявление отозвано заявителем", null);
                                booking.HistoryLink = historyLink;
                                booking.HistoryLinkId = historyLink?.Id;
                            }

                            booking.StatusId = stStatus;
                            if (booking.Children.Any())
                            {
                                foreach (var c in booking.Children)
                                {
                                    c.StatusId = stStatus;
                                }
                            }

                        }
                    }
                    else
                    {
                        throw new Exception("Неизвестный статус");
                    }

                    eMessage.Processed = true;
                }
                catch (InvalidOperationException e)
                {
                    Utils.SendErrorStatus(unitOfWork, eMessage.Message, _mqMpguStatusIncomingErrorQueue);
                    eMessage.Processed = true;
                    eMessage.IsError = true;
                    eMessage.ErrorText = e.Message;
                    eMessage.ErrorDescription = e.StackTrace;
                }

                catch (Exception e)
                {
                    Utils.SendErrorStatus(unitOfWork, eMessage.Message, _mqMpguStatusIncomingErrorQueue);
                    eMessage.Processed = true;
                    eMessage.IsError = true;
                    eMessage.ErrorText = e.Message;
                    eMessage.ErrorDescription = e.StackTrace;
                }

                unitOfWork.SaveChanges();
            }
        }

        /// <summary>
        /// Установка статуса бронирования
        /// </summary>
        private void SetVisitBookingsStatus(IUnitOfWork unitOfWork)
        {
            Logger.Info("SetVisitBookingsStatus started");
            var q = unitOfWork.GetSet<MGTBookingVisit>()
               .Where(ss =>
                  (ss.StatusId == (long)MGTVisitBookingStatuses.PrebookingRegistered ||
                   ss.StatusId == (long)MGTVisitBookingStatuses.BookingRegistered) && ss.ParrentId == null)
               .OrderBy(ss => ss.Id).ToList();
            foreach (var booking in q)
            {
                var d1 = DateTime.Now;
                var d2 = DateTime.Now.AddMinutes(-10).Ticks;
                var d3 = DateTime.Now.AddMinutes(-15);

                try
                {
                    long stStatus = 0;

                    if (booking.StatusId == (long)MGTVisitBookingStatuses.PrebookingRegistered &&
                        d2 > booking.LastUpdateTick)
                    {
                        stStatus = (long)MGTVisitBookingStatuses.PrebookingCanceled;
                    }
                    else if (booking.StatusId == (long)MGTVisitBookingStatuses.BookingRegistered)
                    {
                        //время визита настало
                        if (d1 > booking.VisitCell)
                        {
                            if (!string.IsNullOrWhiteSpace(booking.MPGURegNum))
                            {
                                Utils.SendStatusMessage(unitOfWork, _mqMpguStatusOutcomingQueue,
                                   new StatusMessageFilter(booking.ServiceNumber, 10190,
                                      ConfigurationManager.AppSettings["MqMPGUStatus10190Name"], DateTime.Now));
                            }

                            //SendStatusMessage(UnitOfWork, booking.ServiceNumber, 10190, "Отзыв заявления невозможен", DateTime.Now);
                        }

                        //Заявитель не явился
                        if (d3 > booking.VisitCell)
                        {
                            if (!string.IsNullOrWhiteSpace(booking.MPGURegNum))
                            {
                                Utils.SendStatusMessage(unitOfWork, _mqMpguStatusOutcomingQueue,
                                   new StatusMessageFilter(booking.ServiceNumber, 1080,
                                      ConfigurationManager.AppSettings["MqMPGUStatus1080.2Name"], DateTime.Now)
                                   {
                                       StatusReason = "2"
                                   });
                            }

                            //SendStatusMessage(UnitOfWork, booking.ServiceNumber, 1080, "Заявитель не явился на прием", DateTime.Now, null, null, null, null, "2");
                            stStatus = (long)MGTVisitBookingStatuses.BookingUnvisited;
                        }
                    }

                    if (stStatus > 0)
                    {
                        var HistoryLink = WriteHistory(unitOfWork, booking.HistoryLink, "Изменение статуса записи на визит (с портала)", StatusChangeMessage(unitOfWork, booking, stStatus));
                        booking.StatusId = stStatus;
                        booking.HistoryLink = HistoryLink;
                        booking.HistoryLinkId = HistoryLink?.Id;

                        if (booking.Children.Any())
                        {
                            foreach (var c in booking.Children)
                            {
                                c.StatusId = stStatus;
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Logger.Error($"SetVisitBookingsStatus Error (BookingId:{booking.Id})", e);
                }

                unitOfWork.SaveChanges();
            }

            Logger.Info("SetVisitBookingsStatus finished");
        }

        /// <summary>
        /// Сообщение об изменении стауса бронирования
        /// </summary>
        private string StatusChangeMessage(IUnitOfWork unitOfWork, MGTBookingVisit persisted, long? newStatus)
        {
            var status = unitOfWork.GetById<MGTVisitBookingStatus>(newStatus);
            return $"<ul><li>Изменен статус, старое значение: '{persisted.Status?.Name.FormatEx(string.Empty)}', новое значение: '{status?.Name.FormatEx(string.Empty)}'</li></ul>";
        }

        /// <summary>
        ///
        /// </summary>
        private HistoryLink WriteHistory(IUnitOfWork unitOfWork, HistoryLink persisted, string message, string descr)
        {
            persisted = persisted ?? unitOfWork.AddEntity(new HistoryLink(), false);
            persisted.Historys = persisted.Historys ?? new List<History>();

            persisted.Historys.Add(unitOfWork.AddEntity(new History
            {
                AccountId = null,
                EventCode = message,
                DateChange = DateTime.Now,
                Commentary = descr,
                LastUpdateTick = DateTime.Now.Ticks,
                Link = persisted,
                LinkId = persisted.Id > 0 ? persisted.Id : (long?)null
            }, false));

            unitOfWork.SaveChanges();

            return persisted;
        }
    }
}
