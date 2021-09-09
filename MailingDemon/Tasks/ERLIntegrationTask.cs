using RestChild.Comon;
using RestChild.DAL;
using RestChild.ERL;
using System;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using RestChild.Domain;
using System.Collections.Generic;
using AutoMapper.Internal;
using RestChild.Comon.Enumeration;
using RestChild.DAL.RepositoryExtensions;
using System.Text;
using System.Net;
using RestChild.Comon.ERL;

namespace MailingDemon.Tasks
{
    /// <summary>
    ///     Интеграция с ЕРЛ
    /// </summary>
    [Task]
    public class ERLIntegrationTask : BaseTask
    {
        private const string sender = "12";
        private const string body = "body";

        protected override void Execute()
        {
            Logger.Info("ERLIntegrationTask start...");
            using (var unitOfWork = new UnitOfWork())
            {
                ERLPersonSend(unitOfWork);
                ERLPersonBenefitSend(unitOfWork);
                ERL24QueueSend(unitOfWork);
            }
            Logger.Info("ERLIntegrationTask finish");
        }


        /// <summary>
        ///     Отправка данных в ЕРЛ для их идентификации (поток 2.1)
        /// </summary>
        private void ERLPersonSend(IUnitOfWork UnitOfWork)
        {
            string erl21queuename = System.Configuration.ConfigurationManager.AppSettings["ERLConnectionQueue21Name"] ?? "queue.aisdo.oiv.to_erl.2_1";

            long tiks = DateTime.Now.AddHours(-3).Ticks;

            var children = UnitOfWork.GetSet<ERLPersonStatus>().Where(ss => (ss.ERLMessageId == null || (ss.ERLMessageId != null && ss.PersonUid == null && ss.LastUpdateTick <= tiks)) && ss.ChildId != null).Take(500).Select(ss => ss.Child).ToList();

            var message_id = Guid.NewGuid();

            var oivPersonsIncoming = new RestChild.ERL.V202003.oiv_persons_incoming
            {
                identity = new RestChild.ERL.V202003.messageIdentityWithSession
                {
                    message_id = message_id.ToString(),
                    sender = sender,
                    session = new RestChild.ERL.V202003.session
                    {
                        count = children.Count(),
                        session_id = Guid.NewGuid().ToString()
                    }
                },
                persons = children.Select(ss => new RestChild.ERL.V202003.person_incoming
                {
                    person = new RestChild.ERL.V202003.person
                    {
                        citizen = new RestChild.ERL.V202003.citizen
                        {
                            birthday = ss.DateOfBirth.Value,
                            sex = ss.Male ? RestChild.ERL.V202003.sex.M : RestChild.ERL.V202003.sex.F,
                            firstname = ss.FirstName,
                            surname = ss.LastName,
                            patronymic = ss.MiddleName,
                            snils = ss.Snils,
                            citizen_pk = ss.ChildUniqe?.Children.FirstOrDefault(sx => sx.ERLPersons?.Any(sy => sy.PersonUid != null) == true)?.ERLPersons.FirstOrDefault(sy => sy.PersonUid != null)?.PersonUid.ToString()
                        },
                        identity_document = new RestChild.ERL.V202003.identity_document
                        {
                            doctype_pk = ss.DocumentTypeId == 20005 || ss.DocumentTypeId == 30005 || ss.DocumentTypeId == 50005 || ss.DocumentTypeId == 60005 ? "9" :
                                           ss.DocumentTypeId == 22 ? "3" :
                                           ss.DocumentTypeId == 23 ? "17" : "15",
                            docauthority_name = ss.DocumentSubjectIssue,
                            doc_issuedate = ss.DocumentDateOfIssue.Value,
                            number = ss.DocumentNumber,
                            serial = ss.DocumentSeria
                        }
                    },
                    provider_identifier = ss.ChildUniqeId.HasValue ? ss.ChildUniqeId.Value.ToString() : "c" + ss.Id.ToString(),
                    citizen_addresses = new RestChild.ERL.V202003.citizen_address[1] { new RestChild.ERL.V202003.citizen_address {
                            address_type = RestChild.ERL.V202003.citizen_addressAddress_type.Item2,
                            address = ss.Address.BtiAddressId.HasValue ? new RestChild.ERL.V202003.address {
                                bti_identity = ss.Address.BtiAddress.Id.ToString(),
                                address_text = ss.Address.BtiAddress.FullAddress
                            } : new RestChild.ERL.V202003.address {
                                address_text = ss.Address.Name,
                                flat = ss.Address.Appartment
                            }
                        }
                    }
                }).ToArray()
            };

            if (children.Count() > 0)
            {
                var env = new RestChild.ERL.V202003.Envelope
                {
                    Body = new RestChild.ERL.V202003.Body
                    {
                        Id = body,
                        Item = oivPersonsIncoming
                    }
                };

                SendMsg(env, erl21queuename);

                foreach (var child in children)
                {
                    var erlStatus = UnitOfWork.GetSet<ERLPersonStatus>().First(ss => ss.ChildId == child.Id);
                    erlStatus.ERLMessageId = message_id;
                    UnitOfWork.SaveChanges();
                }
            }
        }

        /// <summary>
        ///     Отправка полученных льгот в ЕРЛ (поток 2.3)
        /// </summary>
        private void ERLPersonBenefitSend(IUnitOfWork UnitOfWork)
        {
            string erl23queuename = System.Configuration.ConfigurationManager.AppSettings["ERLConnectionQueue23Name"] ?? "queue.aisdo.oiv.to_erl.2_3";

            //все уже внесённые и подтверждённые человеки
            var erlExistsRows = UnitOfWork.GetSet<ERLPersonStatus>().Where(sx => (sx.ChildId != null || sx.ApplicantId != null) && sx.ERLMessageId != null && sx.ERLCommited).AsQueryable();

            //идентификаторы всех уже внесённых детей и заявителей
            var erlExistsRowsChildIds = erlExistsRows.Where(sx => sx.ChildId != null).Select(ss => ss.ChildId).AsQueryable();
            var erlExistsRowsApplicantIds = erlExistsRows.Where(sx => sx.ApplicantId != null).Select(ss => ss.ApplicantId).AsQueryable();

            //все уже обработанные запросы на льготу для детей из предыдущего списка
            var erlExecRows = UnitOfWork.GetSet<ERLBenefitStatus>().Where(sx => erlExistsRows.Any(ss => ss.Id == sx.PersonId) && sx.RequestId != null).AsQueryable();


            var persons = UnitOfWork.GetSet<ERLPersonStatus>().Where(ss =>
                    //ребенок из тех детей/заявителей которые уже получены из ЕРЛ
                    (erlExistsRowsChildIds.Any(sx => sx == ss.ChildId) || erlExistsRowsApplicantIds.Any(sx => sx == ss.ApplicantId))
                    //по этому ребенку ещё не отправлена льгота
                    && !erlExecRows.Any(sx => (sx.Person.ChildId == ss.ChildId || sx.Person.ApplicantId == ss.ApplicantId) && (sx.RequestId == ss.Child.RequestId || sx.RequestId == ss.Applicant.RequestId)))
                .ToList();

            var message_id = Guid.NewGuid();

            var benefits = new RestChild.ERL.V202003.citizen_benefits_incoming
            {
                identity = new RestChild.ERL.V202003.messageIdentityWithSession
                {
                    message_id = message_id.ToString(),
                    sender = sender,
                    session = new RestChild.ERL.V202003.session
                    {
                        count = persons.Count,
                        session_id = Guid.NewGuid().ToString()
                    }
                },
                person_benefits = persons.Select(person => new RestChild.ERL.V202003.person_benefit
                {
                    citizen_pk = person.PersonUid.ToString(),
                    citizen_pkSpecified = !string.IsNullOrWhiteSpace(person.PersonUid.ToString()),
                    provider_identifier = person.ChildId.HasValue ? (person.Child.ChildUniqeId.HasValue ? person.Child.ChildUniqeId.Value.ToString() : "c" + person.ChildId.ToString()) : ("a" + person.ApplicantId.ToString()),
                    citizen_benefits = new RestChild.ERL.V202003.citizen_benefit[1] {
                        new RestChild.ERL.V202003.citizen_benefit {
                            action_code = 0,
                            benefittype_pk = person.ChildId.HasValue ? person.Child.Request.TypeOfRest.TypeOfRestERL.MSPCode : person.Applicant.Request.TypeOfRest.TypeOfRestERL.MSPCode,
                            statement_number = person.ChildId.HasValue ? person.Child.Request.RequestNumber : person.Applicant.Request.RequestNumber,
                            begin_date = person.ChildId.HasValue ? (person.Child.Request.Tour?.DateIncome ?? person.Child.Request.CertificateDate.Value) : (person.Applicant.Request.Tour?.DateIncome ?? person.Applicant.Request.CertificateDate.Value),
                            end_date = person.ChildId.HasValue ? (person.Child.Request.Tour?.DateOutcome ?? person.Child.Request.CertificateDate.Value) : (person.Applicant.Request.Tour?.DateOutcome ?? person.Applicant.Request.CertificateDate.Value),
                            decision = null,
                            decision_date = null,
                            decision_document_pk = null,
                            benefit_info = new RestChild.ERL.V202003.citizen_benefitBenefit_info {
                                ItemElementName = RestChild.ERL.V202003.ItemChoiceType2.exemption_form,
                                Item = new RestChild.ERL.V202003.citizen_benefitBenefit_infoExemption_form {
                                    amount = GetTourAmount(person),
                                    monetization = false,
                                    Item = 796.ToString(),
                                    ItemElementName = RestChild.ERL.V202003.ItemChoiceType1.measuryCode
                                }
                            },
                            benefitcategories = new RestChild.ERL.V202003.citizen_benefitBenefitcategories {
                                Items = new string[1] {
                                    GetPersonCategory(person, UnitOfWork)
                                }
                            },
                        }
                    }
                }).ToArray()
            };

            if (persons.Count() > 0)
            {
                var env = new RestChild.ERL.V202003.Envelope
                {
                    Body = new RestChild.ERL.V202003.Body
                    {
                        Id = body,
                        Item = benefits
                    }
                };

                SendMsg(env, erl23queuename);

                foreach (var p in persons)
                {
                    UnitOfWork.AddEntity(new ERLBenefitStatus()
                    {
                        PersonId = p.Id,
                        RequestId = p.ChildId.HasValue ? p.Child.RequestId : p.Applicant.RequestId,
                        ERLMessageId = message_id
                    });
                    UnitOfWork.SaveChanges();
                }
            }
        }

        /// <summary>
        ///     Непосредственная отправка сообщений
        /// </summary>
        private void SendMsg(RestChild.ERL.V202003.Envelope envelope, string queueName)
        {
            string msg = string.Empty;
            using (StringWriter textWriter = new Utf8StringWriter())
            {
                using (var xw = XmlWriter.Create(textWriter, new XmlWriterSettings { Indent = true }))
                {
                    xw.WriteStartDocument(true);

                    var namespaces = new XmlSerializerNamespaces();
                    namespaces.Add("sign", "http://erl.msr.com/schemas/sign-package");
                    namespaces.Add("mq", "http://erl.msr.com/schemas/oiv/mq");
                    namespaces.Add(string.Empty, string.Empty);
                    namespaces.Add("xsi", "http://www.w3.org/2001/XMLSchema-instance");
                    namespaces.Add("xsd", "http://www.w3.org/2001/XMLSchema");

                    var xmlSerializer = new XmlSerializer(typeof(RestChild.ERL.V202003.Envelope));
                    xmlSerializer.Serialize(xw, envelope, namespaces);

                    msg = textWriter.ToString();

                    Logger.Info($"ERLIntegrationMessage: {msg}");
                }
            }
            MQSender.Send(queueName, msg);
        }

        /// <summary>
        ///     Вычисление стоимости тура
        /// </summary>
        private string GetTourAmount(ERLPersonStatus person)
        {
            var request = person.ChildId.HasValue ? person.Child.Request : person.Applicant.Request;

            if (request.TypeOfRest.TypeOfRestERLId == (long)TypeOfRestERLEnum.CompensationYouthRest || request.TypeOfRest.TypeOfRestERLId == (long)TypeOfRestERLEnum.Compensation)
                return person.Child.AmountOfCompensation.ToNullSafeString();

            var price = request.YearOfRest.Prices.Where(ss => ss.TypeOfRestId == request.TypeOfRestId).Select(ss => ss.Price).FirstOrDefault();

            var priceCoop = Convert.ToInt32(Math.Floor(price * 100));

            return priceCoop.ToString();
        }

        /// <summary>
        ///     Вычисление ЛК в ИС Социум
        /// </summary>
        private string GetPersonCategory(ERLPersonStatus person, IUnitOfWork UnitOfWork)
        {
            var child = person.Child;
            var applicant = person.Applicant;

            if(applicant != null)
            {
                return UnitOfWork.GetById<BenefitTypeERL>(15).LCCode;

            }
            else if(child != null)
            {
                return child.BenefitType.BenefitTypeERL.LCCode;
            }

            throw new Exception("Вычисление ЛК не удалось");
        }

        /// <summary>
        ///     Отправка полученных льгот в ЕРЛ (поток 2.4)
        /// </summary>
        private void ERL24QueueSend(IUnitOfWork UnitOfWork)
        {
            string erl24ftpadress = System.Configuration.ConfigurationManager.AppSettings["ERLQueue24Address"];
            string erl24ftpusername = System.Configuration.ConfigurationManager.AppSettings["ERLQueue24UserName"];
            string erl24ftppass = System.Configuration.ConfigurationManager.AppSettings["ERLQueue24Pass"] ?? string.Empty;

            var tasks = UnitOfWork.GetSet<ExchangeUTS>().Where(ss => !ss.Incoming && !ss.IsError && !ss.Processed && ss.DateToSend <= DateTime.Now && ss.QueueName == ERLConstants.ERLQueue24ExchangeUTS).ToList();
            foreach(var task in tasks)
            {
                if (long.TryParse(task.Message, out long j))
                {
                    try
                    {
                        var benefits = UnitOfWork.GetSet<ERLBenefitStatus>().Where(ss => ss.BenefitUid != null && !ss.Queue24Sended && (ss.Person.Child.Request.YearOfRestId == j || ss.Person.Applicant.Request.YearOfRestId == j)).ToList();
                        var csv = new StringBuilder();
                        foreach(var b in benefits)
                        {
                            csv.AppendLine($"{b.Person.PersonUid},{(b.Person.ChildId.HasValue ? b.Person.ChildId : b.Person.ApplicantId)},{b.Request.RequestNumber},{b.Request.TypeOfRest.TypeOfRestERL.MSPCode},{GetTourAmount(b.Person)},1,{string.Format("{0:yyyyMMdd}", b.Request.DateOutcome)}");
                        }

                        FtpWebRequest request = (FtpWebRequest)WebRequest.Create(erl24ftpadress);
                        request.Method = WebRequestMethods.Ftp.UploadFile;
                        if(!string.IsNullOrWhiteSpace(erl24ftpusername))
                        {
                            request.Credentials = new NetworkCredential(erl24ftpusername, erl24ftppass);
                        }

                        var fileContents = Encoding.UTF8.GetBytes(csv.ToString());
                        request.ContentLength = fileContents.Length;

                        using (Stream requestStream = request.GetRequestStream())
                        {
                            requestStream.Write(fileContents, 0, fileContents.Length);
                        }

                        bool complete = false;
                        using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                        {
                            complete = response.StatusCode == FtpStatusCode.CommandOK;
                        }

                        if(complete)
                        {
                            foreach (var b in benefits)
                            {
                                b.Queue24Sended = complete;
                                UnitOfWork.SaveChanges();
                            }
                        }
                    }
                    catch (Exception ez)
                    {
                        task.Processed = true;
                        task.IsError = true;
                        task.ErrorText = ez.Message;
                        task.ErrorDescription = ez.StackTrace;
                        UnitOfWork.SaveChanges();

                    }
                }
                else
                {
                    task.Processed = true;
                    task.IsError = true;
                    task.ErrorText = "Error in parse Message to YearOfRest identificator";
                    UnitOfWork.SaveChanges();
                }


            }
        }
    }
}
