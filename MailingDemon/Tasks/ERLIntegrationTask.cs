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
using RestChild.Comon.Enumeration;
using RestChild.DAL.RepositoryExtensions;
using System.Text;
using System.Net;
using log4net;
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

        private static readonly long?[] compensations = new long?[] { (long)TypeOfRestERLEnum.CompensationYouthRest, (long)TypeOfRestERLEnum.Compensation };

        protected override void Execute()
        {
            Logger?.Info("ERLIntegrationTask start...");
            using (var unitOfWork = new UnitOfWork())
            {
                ERLFillTable(unitOfWork, Logger);
                ERLPersonSend(unitOfWork, Logger);
                ERLPersonBenefitSend(unitOfWork, Logger);
                ERL24QueueSend(unitOfWork, Logger);
            }
            Logger?.Info("ERLIntegrationTask finish");
        }

        /// <summary>
        ///     Заполнение таблицы интеграции с ЕРЛ
        /// </summary>
        private static void ERLFillTable(IUnitOfWork UnitOfWork, ILog Logger = null)
        {
            var requests = UnitOfWork.GetSet<Request>().Where(ss =>
                !ss.IsDeleted && ss.IsLast && !ss.IsDraft && ss.YearOfRest.Year >= 2019
                && ss.DeclineReasonId == null && ss.StatusId == (long)StatusEnum.CertificateIssued
                && ss.TypeOfRest.TypeOfRestERL.IsActive

            ).AsQueryable();

            var rr = requests.GroupBy(sx =>
                sx.TypeOfRest.TypeOfRestERLId == (long)TypeOfRestERLEnum.FreeRestChild && sx.Attendant.Any(aa => aa.IsAccomp && !aa.IsDeleted) ?
                (long)TypeOfRestERLEnum.FreeRestChildAndApplicant : sx.TypeOfRest.TypeOfRestERLId
            ).ToDictionary(sx => sx.Key, sy => sy.ToList());


            if(!rr.Any())
                return;

            var applicants = new List<Applicant>();
            var children = new List<Child>();


            var epss = UnitOfWork.GetSet<ERLPersonStatus>().AsQueryable();

            for (var index = rr.Count - 1; index >= 0; index--)
            {
                var elem = rr.ElementAt(index).Value;
                if (elem.Any(ss => ss.TypeOfRest.TypeOfRestERL.UseApplicant))
                {
                    var rust = elem.Where(ss => ss.TypeOfRest.TypeOfRestERL.UseApplicant)
                        .Select(ss => ss.Applicant)
                        .Where(ss => epss.All(sx => sx.ApplicantId != ss.Id)).Take(5).ToList();

                    applicants.AddRange(rust);
                }
                else
                {
                    var rust = elem.Where(ss => !ss.TypeOfRest.TypeOfRestERL.UseApplicant)
                        .SelectMany(ss => ss.Child)
                        .Where(ss => epss.All(sx => sx.ChildId != ss.Id) && ss.BenefitType.BenefitTypeERL.IsActive
                        ).Take(5).ToList();

                    children.AddRange(rust);
                }
            }

            for (var index = children.Count - 1; index >= 0; index--)
            {
                var child = children[index];
                var sentChild = UnitOfWork.GetSet<ERLPersonStatus>()
                    .FirstOrDefault(sx => child.Snils != null && sx.Child.Snils == child.Snils);

                var _s = new ERLPersonStatus()
                {
                    ChildId = child.Id,
                    PersonUid = sentChild?.PersonUid,
                    ERLCommited = true,
                    ERLMessageId = Guid.Empty
                };

                UnitOfWork.AddEntity(_s);
                UnitOfWork.SaveChanges();
            }

            for (var index = applicants.Count - 1; index >= 0; index--)
            {
                var applicant = applicants[index];
                var sentApplicant = UnitOfWork.GetSet<ERLPersonStatus>().FirstOrDefault(sx =>
                    applicant.Snils != null && sx.Applicant.Snils == applicant.Snils);

                var _s = new ERLPersonStatus()
                {
                    ApplicantId = applicant.Id,
                    PersonUid = sentApplicant?.PersonUid,
                    ERLCommited = true,
                    ERLMessageId = Guid.Empty
                };

                UnitOfWork.AddEntity(_s);
                UnitOfWork.SaveChanges();
            }
        }

        /// <summary>
        ///     Отправка данных в ЕРЛ для их идентификации (поток 2.1)
        /// </summary>
        private static void ERLPersonSend(IUnitOfWork UnitOfWork, ILog Logger = null)
        {
            var erl21queuename = System.Configuration.ConfigurationManager.AppSettings["ERLConnectionQueue21Name"] ?? "queue.aisdo.oiv.to_erl.2_1";

            var ticks = DateTime.Now.AddHours(-3).Ticks;

            var persons = UnitOfWork.GetSet<ERLPersonStatus>().Where(ss => ss.ERLMessageId == Guid.Empty || (ss.PersonUid == null && ss.ERLMessageId != Guid.Empty && ss.LastUpdateTick <= ticks)).Take(250).ToList();

            var message_id = Guid.NewGuid();

            var oivPersonsIncoming = new RestChild.ERL.V202003.oiv_persons_incoming
            {
                identity = new RestChild.ERL.V202003.messageIdentityWithSession
                {
                    message_id = message_id.ToString(),
                    sender = sender,
                    session = new RestChild.ERL.V202003.session
                    {
                        count = persons.Count(),
                        session_id = Guid.NewGuid().ToString()
                    }
                }
            };

            var pers = new List<RestChild.ERL.V202003.person_incoming>();
            foreach (var person in persons)
            {
                var pi = new RestChild.ERL.V202003.person_incoming();
                var pe = new RestChild.ERL.V202003.person();

                var c = new RestChild.ERL.V202003.citizen();
                var id = new RestChild.ERL.V202003.identity_document();
                var ca = new RestChild.ERL.V202003.citizen_address();

                if (person.ChildId.HasValue)
                {
                    c.birthday = person.Child.DateOfBirth.Value;
                    c.sex = person.Child.Male ? RestChild.ERL.V202003.sex.M : RestChild.ERL.V202003.sex.F;
                    c.firstname = person.Child.FirstName;
                    c.surname = person.Child.LastName;
                    c.patronymic = person.Child.MiddleName;
                    c.snils = person.Child.Snils;
                    c.citizen_pk = person.Child.ChildUniqe?.Children.FirstOrDefault(sx => sx.ERLPersons?.Any(sy => sy.PersonUid != null) == true)?.ERLPersons.FirstOrDefault(sy => sy.PersonUid != null)?.PersonUid.ToString();

                    id.doctype_pk = person.Child.DocumentTypeId == 20005 || person.Child.DocumentTypeId == 30005 || person.Child.DocumentTypeId == 50005 || person.Child.DocumentTypeId == 60005 ? "9" :
                        person.Child.DocumentTypeId == 22 ? "3" :
                        person.Child.DocumentTypeId == 23 ? "17" : "15";
                    id.docauthority_name = person.Child.DocumentSubjectIssue;
                    id.doc_issuedate = person.Child.DocumentDateOfIssue.Value;
                    id.number = person.Child.DocumentNumber;
                    id.serial = person.Child.DocumentSeria;

                    pi.provider_identifier = $"c{(person.Child.ChildUniqeId.HasValue ? $"u{person.Child.ChildUniqeId.Value}" : person.Child.Id.ToString())}";

                    ca.address_type = RestChild.ERL.V202003.citizen_addressAddress_type.Item2;
                    if (person.Child.Address != null)
                    {
                        ca.address = person.Child.Address.BtiAddressId.HasValue ? new RestChild.ERL.V202003.address
                        {
                            bti_identity = person.Child.Address.BtiAddress.Id.ToString(),
                            address_text = person.Child.Address.BtiAddress.FullAddress
                        } : new RestChild.ERL.V202003.address
                        {
                            address_text = person.Child.Address.Name,
                            flat = person.Child.Address.Appartment
                        };
                    }
                }
                else if (person.ApplicantId.HasValue)
                {
                    c.birthday = person.Applicant.DateOfBirth.Value;
                    c.sex = person.Applicant.Male.Value ? RestChild.ERL.V202003.sex.M : RestChild.ERL.V202003.sex.F;
                    c.firstname = person.Applicant.FirstName;
                    c.surname = person.Applicant.LastName;
                    c.patronymic = person.Applicant.MiddleName;
                    c.snils = person.Applicant.Snils;
                    c.citizen_pk = person.PersonUid?.ToString();

                    id.doctype_pk = person.Applicant.DocumentTypeId == 20005 || person.Applicant.DocumentTypeId == 30005 || person.Applicant.DocumentTypeId == 50005 || person.Applicant.DocumentTypeId == 60005 ? "9" :
                        person.Applicant.DocumentTypeId == 22 ? "3" :
                        person.Applicant.DocumentTypeId == 23 ? "17" : "15";
                    id.docauthority_name = person.Applicant.DocumentSubjectIssue;
                    id.doc_issuedate = person.Applicant.DocumentDateOfIssue.Value;
                    id.number = person.Applicant.DocumentNumber;
                    id.serial = person.Applicant.DocumentSeria;

                    pi.provider_identifier = "a" + person.Applicant.Id.ToString();

                    ca.address_type = RestChild.ERL.V202003.citizen_addressAddress_type.Item2;

                    if(person.Applicant.Address != null)
                    {
                        ca.address = person.Applicant.Address.BtiAddressId.HasValue ? new RestChild.ERL.V202003.address
                        {
                            bti_identity = person.Applicant.Address.BtiAddress.Id.ToString(),
                            address_text = person.Applicant.Address.BtiAddress.FullAddress
                        } : new RestChild.ERL.V202003.address
                        {
                            address_text = person.Applicant.Address.Name,
                            flat = person.Applicant.Address.Appartment
                        };
                    }

                }

                pe.citizen = c;
                pe.identity_document = id;

                pi.citizen_addresses = new RestChild.ERL.V202003.citizen_address[1] { ca };
                pi.person = pe;

                pers.Add(pi);
            }

            oivPersonsIncoming.persons = pers.ToArray();


            if (persons.Any())
            {
                var env = new RestChild.ERL.V202003.Envelope
                {
                    Body = new RestChild.ERL.V202003.Body
                    {
                        Id = body,
                        Item = oivPersonsIncoming
                    }
                };

                SendMsg(env, erl21queuename, Logger);

                foreach (var person in persons)
                {
                    var erlStatus = UnitOfWork.GetSet<ERLPersonStatus>().First(ss => ss.Id == person.Id);
                    erlStatus.ERLMessageId = message_id;
                    UnitOfWork.SaveChanges();
                }
            }
        }

        /// <summary>
        ///     Отправка полученных льгот в ЕРЛ (поток 2.3)
        /// </summary>
        private static void ERLPersonBenefitSend(IUnitOfWork UnitOfWork, ILog Logger = null)
        {
            var erl23queuename = System.Configuration.ConfigurationManager.AppSettings["ERLConnectionQueue23Name"] ?? "queue.aisdo.oiv.to_erl.2_3";

            var q = UnitOfWork.GetSet<Request>().Where(ss => !ss.IsDeleted && ss.ApplicantId != null).AsQueryable();

            var persons = UnitOfWork.GetSet<ERLPersonStatus>()
                //уже подтвержденные в ЕРЛ люди
                .Where(sx => (sx.ChildId != null || sx.ApplicantId != null) && sx.ERLMessageId != null && sx.ERLCommited)
                //уже отправленные льготы
                .Where(sx => !sx.Benefits.Any())
                //цены "льгот" для года заявления заполнены
                .Where(sx =>
                    // ребёнок ->  компенсация
                    compensations.Contains(sx.Child.Request.TypeOfRest.TypeOfRestERLId) ||
                    // ребёнок ->  не компенсация
                    sx.Child.Request.YearOfRest.Prices.Any(ss => ss.TypeOfRestId == sx.Child.Request.TypeOfRestId && ss.Price > 0) ||

                    // не ребёнок -> компенсация
                    q.Any(ss => ss.ApplicantId == sx.Applicant.Id && compensations.Contains(ss.TypeOfRest.TypeOfRestERLId)) ||
                    // не ребёнок -> не компенсация
                    q.Any(ss => ss.ApplicantId == sx.Applicant.Id && ss.YearOfRest.Prices.Any(sy => sy.Price > 0 && sy.TypeOfRestId == ss.TypeOfRestId))

                ).Take(100).ToList();

            var msp_kostyl = UnitOfWork.GetById<TypeOfRestERL>((long)TypeOfRestERLEnum.FreeRestChildAndApplicant).MSPCode;

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
                }
            };

            var bnft = new List<RestChild.ERL.V202003.person_benefit>();

            foreach (var person in persons)
            {
                Request request = null;
                if(person.ChildId.HasValue)
                {
                    request = person.Child.Request;
                }
                else if(person.ApplicantId.HasValue)
                {
                    request = UnitOfWork.GetSet<Request>().FirstOrDefault(ss => ss.ApplicantId == person.ApplicantId);
                }

                if(request == null)
                {
                    continue;
                }


                var pb = new RestChild.ERL.V202003.person_benefit
                {
                    citizen_pk = person.PersonUid.ToString(),
                    citizen_pkSpecified = !string.IsNullOrWhiteSpace(person.PersonUid.ToString()),
                    citizen_benefits = new RestChild.ERL.V202003.citizen_benefit[1] {
                        new RestChild.ERL.V202003.citizen_benefit {
                            action_code = 0,
                        }
                    }
                };

                if (person.ChildId.HasValue && request.TypeOfRest.TypeOfRestERLId == (long)TypeOfRestERLEnum.FreeRestChildAndApplicant && request.Attendant.Any(aa => aa.IsAccomp && !aa.IsDeleted))
                {
                    pb.citizen_benefits[0].benefittype_pk = msp_kostyl;
                }
                else
                {
                    pb.citizen_benefits[0].benefittype_pk = request.TypeOfRest.TypeOfRestERL.MSPCode;
                }

                pb.citizen_benefits[0].statement_number = request.RequestNumber;
                if(request.TypeOfRest.ParentId == (long)TypeOfRestEnum.CompensationGroup)
                {
                    if(person.ChildId.HasValue)
                    {
                        pb.citizen_benefits[0].begin_date = request.InformationVouchers.Select(ss => ss.DateFrom.Value).First();
                        pb.citizen_benefits[0].end_date = request.InformationVouchers.Select(ss => ss.DateTo.Value).First();
                    }
                    else
                    {
                        pb.citizen_benefits[0].begin_date = request.InformationVouchers.Select(ss => ss.DateFrom.Value).First();
                        pb.citizen_benefits[0].end_date = request.InformationVouchers.Select(ss => ss.DateTo.Value).First();
                    }
                }
                else
                {
                    pb.citizen_benefits[0].begin_date = request.Tour?.DateIncome ?? request.CertificateDate.Value;
                    pb.citizen_benefits[0].end_date = request.Tour?.DateOutcome ?? request.CertificateDate.Value;
                }
                pb.citizen_benefits[0].decision = null;
                pb.citizen_benefits[0].decision_date = null;
                pb.citizen_benefits[0].decision_document_pk = null;

                pb.citizen_benefits[0].benefit_info = new RestChild.ERL.V202003.citizen_benefitBenefit_info
                {
                    ItemElementName = RestChild.ERL.V202003.ItemChoiceType2.exemption_form,
                    Item = new RestChild.ERL.V202003.citizen_benefitBenefit_infoExemption_form
                    {
                        amount = GetTourAmount(person, UnitOfWork),
                        monetization = false,
                        Item = 796.ToString(),
                        ItemElementName = RestChild.ERL.V202003.ItemChoiceType1.measuryCode
                    }
                };
                pb.citizen_benefits[0].benefitcategories = new RestChild.ERL.V202003.citizen_benefitBenefitcategories
                {
                    Items = new string[1] {
                        GetPersonCategory(person, UnitOfWork)
                    }
                };

                bnft.Add(pb);
            }

            benefits.person_benefits = bnft.ToArray();

            if (persons.Any())
            {
                var env = new RestChild.ERL.V202003.Envelope
                {
                    Body = new RestChild.ERL.V202003.Body
                    {
                        Id = body,
                        Item = benefits
                    }
                };

                SendMsg(env, erl23queuename, Logger);

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
        private static void SendMsg(RestChild.ERL.V202003.Envelope envelope, string queueName, ILog Logger = null)
        {
            var msg = string.Empty;
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

                    Logger?.Info($"ERLIntegrationMessage: {msg}");
                }
            }
            MQSender.Send(queueName, msg);
        }

        /// <summary>
        ///     Вычисление стоимости тура
        /// </summary>
        private static string GetTourAmount(ERLPersonStatus person, IUnitOfWork UnitOfWork)
        {
            var request = person.ChildId.HasValue ? person.Child.Request : UnitOfWork.GetSet<Request>().FirstOrDefault(ss => ss.ApplicantId == person.ApplicantId);

            decimal price = 0;

            if (compensations.Contains(request.TypeOfRest.TypeOfRestERLId))
            {
                if(person.ChildId.HasValue)
                {
                    price = person.Child.RequestInformationVoucher.AttendantsPrice.Select(ss => ss.AmountOfCompensation.Value).FirstOrDefault();
                }
                else
                {
                    price = request.InformationVouchers.Select(ss => ss.AttendantsPrice.Select(sx => sx.AmountOfCompensation.Value).FirstOrDefault()).FirstOrDefault();
                }

            }
            else
            {
                price = request.YearOfRest.Prices.Where(ss => ss.TypeOfRestId == request.TypeOfRestId).Select(ss => ss.Price).FirstOrDefault();
            }

            var priceCoop = Convert.ToInt32(Math.Floor(price * 100));

            return priceCoop.ToString();
        }

        /// <summary>
        ///     Вычисление ЛК в ИС Социум
        /// </summary>
        private static string GetPersonCategory(ERLPersonStatus person, IUnitOfWork UnitOfWork)
        {
            var child = person.Child;
            var applicant = person.Applicant;

            if (applicant != null)
            {
                return UnitOfWork.GetById<BenefitTypeERL>(15).LCCode;

            }
            else if (child != null)
            {
                return child.BenefitType.BenefitTypeERL.LCCode;
            }

            throw new Exception("Вычисление ЛК не удалось");
        }

        /// <summary>
        ///     Отправка полученных льгот в ЕРЛ (поток 2.4)
        /// </summary>
        private static void ERL24QueueSend(IUnitOfWork UnitOfWork, ILog Logger = null)
        {
            var erl24ftpadress = System.Configuration.ConfigurationManager.AppSettings["ERLQueue24Address"];
            var erl24ftpusername = System.Configuration.ConfigurationManager.AppSettings["ERLQueue24UserName"];
            var erl24ftppass = System.Configuration.ConfigurationManager.AppSettings["ERLQueue24Pass"] ?? string.Empty;

            var tasks = UnitOfWork.GetSet<ExchangeUTS>().Where(ss => !ss.Incoming && !ss.IsError && !ss.Processed && ss.DateToSend <= DateTime.Now && ss.QueueName == ERLConstants.ERLQueue24ExchangeUTS).ToList();
            foreach (var task in tasks)
            {
                if (long.TryParse(task.Message, out long j))
                {
                    try
                    {
                        var benefits = UnitOfWork.GetSet<ERLBenefitStatus>().Where(ss => ss.BenefitUid != null && !ss.Queue24Sended && (ss.Person.Child.Request.YearOfRestId == j || ss.Person.Applicant.Request.YearOfRestId == j)).ToList();
                        var csv = new StringBuilder();
                        foreach (var b in benefits)
                        {
                            csv.AppendLine($"{b.Person.PersonUid},{(b.Person.ChildId.HasValue ? b.Person.ChildId : b.Person.ApplicantId)},{b.Request.RequestNumber},{b.Request.TypeOfRest.TypeOfRestERL.MSPCode},{GetTourAmount(b.Person, UnitOfWork)},1,{string.Format("{0:yyyyMMdd}", b.Request.DateOutcome)}");
                        }

                        FtpWebRequest request = (FtpWebRequest)WebRequest.Create(erl24ftpadress);
                        request.Method = WebRequestMethods.Ftp.UploadFile;
                        if (!string.IsNullOrWhiteSpace(erl24ftpusername))
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

                        if (complete)
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
