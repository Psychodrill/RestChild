using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Configuration;
using System.Net.Mail;
using System.Reflection;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Common.Logging;
using Common.Logging.Simple;
using log4net.Repository.Hierarchy;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Linq;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Store;
using RestChild.Booking.Logic.Indexing;
using RestChild.Booking.Logic.Logic;
using RestChild.Booking.Logic.LuceneHelpers;
using RestChild.Comon;
using RestChild.Comon.Dto;
using RestChild.Comon.Dto.SearchRestChild;
using RestChild.Comon.Enumeration;
using RestChild.Comon.Exchange;
using RestChild.DAL;
using RestChild.DAL.RepositoryExtensions;
using RestChild.DocumentGeneration.PDFDocuments;
using RestChild.Domain;
using RestChild.MPGUIntegration;
using ServiceDocument = RestChild.Common.Service.ServiceReference.ServiceDocument;
using Version = Lucene.Net.Util.Version;

namespace RestChild.ConsoleProjects
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var uw = new UnitOfWork())
            {
                //var q = uw.GetSet<Domain.Request>().Where(r => r.StatusId == 1075 && r.YearOfRest.Year >= 2018 && !r.TypeOfRest.Commercial && r.Child.Any(c => c.BenefitTypeId == 33 || c.BenefitTypeId == 41 || c.BenefitTypeId == 42 || c.BenefitTypeId == 43)).AsQueryable();

                //var cc_id = q.SelectMany(ss => ss.Child).Where(c => c.BenefitTypeId == 33 || c.BenefitTypeId == 41 || c.BenefitTypeId == 42 || c.BenefitTypeId == 43).Select(ss => ss.Id).Distinct().AsQueryable();

                //var cc_id_s = uw.GetSet<Domain.ERLPersonStatus>().Where(sx => (sx.ChildId != null) && sx.AnswerRecived).AsQueryable();

                //var children = uw.GetSet<Domain.Child>().Where(c => cc_id.Contains(c.Id) && !cc_id_s.Select(sx => sx.ChildId).Contains(c.Id)).OrderBy(sx => sx.Id).Skip(20).Take(5).ToArray();

                //ERL.ERLRepository repo = new ERL.ERLRepository(uw);

                //repo.ERLProcessPerson(children);

                ///var q = uw.GetById<Request>(1111);

                var cert = PdfProcessor.CertificateForRequestTemporaryFile(uw, 127482);
                using (var fs = new FileStream("C:\\Publish\\Сертификат.pdf", FileMode.OpenOrCreate, FileAccess.Write))
                {
                    fs.Write(cert.FileBody, 0, cert.FileBody.Length);
                }
            }
            /*
            LogManager.Adapter = new ConsoleOutLoggerFactoryAdapter() { Level = LogLevel.Info };

               //ImportZagz2017.ImportZagzFromExcel("D:\\Work\\Zagz2017.xlsx");

              ImportZagz2019.LoadData();

               /*if (args == null || args.Length != 1 || string.IsNullOrEmpty(args[0]) || !System.IO.File.Exists(args[0]))
               {
                   Console.WriteLine("ERROR: Missing required parameter 1 \"file to import\"");
               }
               else
               {
                   Console.WriteLine("Import started...");
                   ImportZagz2017.ImportZagzFromExcel(args[0]);
                   Console.WriteLine("Import finished.");
               }*/

            //Console.WriteLine("Freeing booking started...");
            //FreeBookings.ProcessFreeBooking2();
            //Console.WriteLine("Freeing booking finished.");*/
        }

        private static void GenerateSqlForClasses()
        {
            using (var f = System.IO.File.Create("d:\\sql.sql"))
            {
                Byte[] info = new UTF8Encoding(true).GetBytes(GetClasses());
                // Add some information to the file.
                f.Write(info, 0, info.Length);
            }
        }

        private static void GeneratePasswords()
        {
            using (var f = System.IO.File.Create("d:\\passwords.txt"))
            {
                for (int i = 0; i < 42; i++)
                {
                    var password = PasswordUtility.GeneratePassword(8);
                    var salt = PasswordUtility.GenerateSalt();
                    Byte[] info =
                        new UTF8Encoding(true).GetBytes(
                            $"{password} {Convert.ToBase64String(PasswordUtility.GetPasswordHash(password, salt))} {Convert.ToBase64String(salt)}\n");
                    f.Write(info, 0, info.Length);
                }
            }
        }


        public static string CreateSql()
        {
            var assembly = Assembly.GetAssembly(typeof(Request));
            var types =
                assembly.GetTypes()
                    .Where(
                        t =>
                            t.Name != "IEntityBase" && t.Name != "IStateEntity" && t.IsClass && !t.IsAbstract && !t.IsInterface &&
                            !t.IsSealed);

            var sb = new StringBuilder();

            foreach (var type in types)
            {
                sb.AppendLine($"update dbo.{type.Name} set Eid=Id");
            }

            return sb.ToString();
        }

        public static string GetClasses()
        {
            var assembly = Assembly.GetAssembly(typeof(Request));
            var types =
                assembly.GetTypes()
                    .Where(
                        t =>
                            t.Name != "IEntityBase" && t.Name != "IStateEntity" && t.IsClass && !t.IsAbstract && !t.IsInterface &&
                            !t.IsSealed);

            var sb = new StringBuilder();

            foreach (var type in types)
            {
                sb.AppendLine($"update dbo.{type.Name} set Eid=Id, EidSendStatus=0 where Eid is null");
            }

            return sb.ToString();
        }

        public static void ChangePasswords()
        {
            using (var files = System.IO.File.CreateText("c:\\accountPasswords.txt"))
            {
                using (var uw = new UnitOfWork())
                {
                    var accounts = uw.GetSet<Account>().Where(a => a.IsActive).OrderBy(a => a.Id).ToList();
                    foreach (var account in accounts)
                    {
                        var password = PasswordUtility.GeneratePassword(8);
                        var salt = PasswordUtility.GenerateSalt();
                        account.Password = Convert.ToBase64String(PasswordUtility.GetPasswordHash(password, salt));
                        account.Salt = Convert.ToBase64String(salt);
                        account.DateUpdate = DateTime.Now;
                        account.LastUpdateTick = DateTime.Now.Ticks;
                        var admins = uw.GetSet<AdministratorTour>().Where(a => a.LinkedAccountId == account.Id).ToList();
                        foreach (var admin in admins)
                        {
                            admin.Password = account.Password;
                            admin.Salt = account.Salt;
                            admin.DateUpdate = DateTime.Now;
                            admin.LastUpdateTick = DateTime.Now.Ticks;
                        }

                        var couns = uw.GetSet<Counselors>().Where(a => a.LinkedAccountId == account.Id).ToList();
                        foreach (var coun in couns)
                        {
                            coun.Password = account.Password;
                            coun.Salt = account.Salt;
                            coun.DateUpdate = DateTime.Now;
                            coun.LastUpdateTick = DateTime.Now.Ticks;
                        }

                        files.WriteLine($"{account.Id}:{account.Login}:{account.Name}:{account.Position}:{account.Email}:{password}");

                        uw.AddEntity(new SendEmailAndSms
                        {
                            DateCreate = DateTime.Now,
                            Email = account.Email,
                            EmailTitle = "АИС Отдых. Изменен пароль от учетной записи.",
                            EmailMessage = $"У Вас изменен пароль от учетной записи. Новый пароль: {password}",
                            IsSmsSended = true,
                            IsEmailSended = false,
                            LastUpdateTick = DateTime.Now.Ticks
                        });

                        uw.SaveChanges();
                    }
                }
            }
        }

        private static void SendDeclineStatused()
        {
            using (var uw = new UnitOfWork())
            {
                var ids = new long[]
                {
                    255029,
                    255177,
                    256465,
                    256658,
                    257116,
                    257247,
                    258651,
                    259317,
                    259404,
                    259737,
                    260003,
                    260420,
                    262378,
                    262914,
                    263186,
                    263551,
                    263742
                };

                foreach (var id in ids)
                {
                    var request = uw.GetById<Request>(id);
                    if (request != null && request.StatusId == 1080 && request.DeclineReasonId == 201603)
                    {
                        SendChangeStatus(uw, request, request.StatusId ?? 1080, request.DeclineReason.Name);
                    }
                }
            }
        }

        /// <summary>
        ///     отправка изменения статуса заявления.
        /// </summary>
        internal static void SendChangeStatus(IUnitOfWork unitOfWork, Request model, long statusId, string comment)
        {

            //string exchangeSystemCode = "9000063";
            string fromCode = "2064";
            string toCode = "200902";

            string serviceNumber = model.RequestNumber;

            if (string.IsNullOrWhiteSpace(model.TypeOfRest?.ServiceCode))
            {
                return;
            }

            var message = new StatusMessage
            {
                ServiceNumber = serviceNumber,
                ResponseDate = DateTime.Now,
                StatusCode = Convert.ToInt32(model.Status.ExternalUid),
                Note = string.IsNullOrEmpty(comment)
                    ? model.Status.MpguComment +
                      (statusId == (long)StatusEnum.Reject || statusId == (long)StatusEnum.CancelByApplicant
                          ? $" {model.NullSafe(m => m.DeclineReason.Name)}"
                          : string.Empty)
                    : comment
            };

            //message.Result = new RequestResult{}

            if (model.StatusId == (long)StatusEnum.CertificateIssued)
            {
                var certificateInfo = new certificateInfo
                {
                    applicant =
                        new certificateInfoApplicant
                        {
                            firstName = model.Applicant.FirstName,
                            lastName = model.Applicant.LastName,
                            middleName = model.Applicant.MiddleName,
                            documentNumber =
                                string.Format("{2} серия: {0}, номер {1}", model.Applicant.DocumentSeria, model.Applicant.DocumentNumber,
                                    model.Applicant.NullSafe(m => m.DocumentType.Name))
                        },
                    attendants = model.Attendant.Select(
                        c =>
                            new certificateInfoAttendant
                            {
                                dateOfBirth = (c.DateOfBirth ?? default(DateTime)).XmlToString(),
                                lastName = c.LastName,
                                middleName = c.MiddleName,
                                firstName = c.FirstName,
                                documentNumber =
                                    string.Format("{2} серия: {0}, номер {1}", c.DocumentSeria, c.DocumentNumber,
                                        c.NullSafe(m => m.DocumentType.Name))
                            }).ToArray(),
                    children =
                        model.Child?.Select(
                            c =>
                                new certificateInfoChild
                                {
                                    dateOfBirth = (c.DateOfBirth ?? default(DateTime)).XmlToString(),
                                    lastName = c.LastName,
                                    middleName = c.MiddleName,
                                    firstName = c.FirstName,
                                    documentNumber =
                                        string.Format("{2} серия: {0}, номер {1}", c.DocumentSeria, c.DocumentNumber,
                                            c.NullSafe(m => m.DocumentType.Name))
                                }).ToArray(),
                    serviceNumber = string.IsNullOrEmpty(model.CertificateNumber) ? serviceNumber : model.CertificateNumber,
                    serviceDate = (model.DateChangeStatus ?? DateTime.Now).Date.XmlToString(),
                    placeOfRest = model.Tour?.Hotels.Name,
                    dateStart = (model.Tour?.DateIncome ?? DateTime.MinValue).XmlToString(),
                    dateEnd = (model.Tour?.DateOutcome ?? DateTime.MinValue).XmlToString()
                };

                if (model.Applicant.IsAccomp)
                {
                    var attends = certificateInfo.attendants.ToList();
                    attends.Add(new certificateInfoAttendant
                    {
                        firstName = model.Applicant.FirstName,
                        lastName = model.Applicant.LastName,
                        middleName = model.Applicant.MiddleName,
                        dateOfBirth = (model.Applicant.DateOfBirth ?? default(DateTime)).XmlToString(),
                        documentNumber =
                            string.Format("{2} серия: {0}, номер {1}", model.Applicant.DocumentSeria, model.Applicant.DocumentNumber, model.NullSafe(m => m.Applicant.DocumentType.Name))
                    });
                    certificateInfo.attendants = attends.ToArray();
                }

                if (!certificateInfo.attendants.Any())
                {
                    certificateInfo.attendants = null;
                }

                var doc = new XmlDocument();
                doc.LoadXml(Serialization.Serializer(certificateInfo));

                message.Documents = new[] { new ServiceDocument { CustomAttributes = doc.DocumentElement } };
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
                QueueName = "CAMPS.STATUS_OUT",
                ServiceNumber = model.RequestNumber,
                RequestId = model.Id,
                ToState = statusId,
                BookingGuid = model.BookingGuid,
                TypeOfRestId = model.TypeOfRestId,
            });

            unitOfWork.SaveChanges();
        }

        private static void DeleteDocument()
        {
            using (var serviceWriter = LuceneConnection.GetIndexDirectory("RestChild"))
            using (var provider = new LuceneDataProvider(serviceWriter, LuceneConnection.LuceneVersion))
            using (var session = provider.OpenSession(RestChild.Booking.Logic.Logic.RestChildrenIndex.GetDocumentMapper()))
            {
                session.Delete(new IndexRestChildDto()
                {
                    Key = "c310356"
                });
                session.Commit();
            }
        }

        private static void TryQuery()
        {
            var dir = LuceneConnection.GetIndexDirectory("RestChild");

            //create an index searcher that will perform the search
            Lucene.Net.Search.IndexSearcher searcher = new Lucene.Net.Search.IndexSearcher(dir);

            //build a query object

            BooleanQuery booleanQuery = new BooleanQuery();

            var analyzer = new StandardAnalyzer(Version.LUCENE_30);

            var query = new QueryParser(Lucene.Net.Util.Version.LUCENE_30, "Organization", analyzer).Parse("199");

            var query2 = new QueryParser(Lucene.Net.Util.Version.LUCENE_30, "YearOfRest", analyzer).Parse("1");

            booleanQuery.Add(query, Occur.MUST);
            booleanQuery.Add(query2, Occur.MUST);

            var doc = searcher.Search(booleanQuery, 20);

            for (int i = 0; i < 20; i++)
            {
                Document d = searcher.Doc(doc.ScoreDocs[i].Doc);
            }
        }
        private static void FillLuceneIndex()
        {
            var restChildrenIndex = new RestChildrenIndex(LogManager.GetLogger<Program>());
            restChildrenIndex.RebuildIndex();
        }
        private static void CheckLuceneIndex()
        {
            var irc = new IndexRestChecker(LogManager.GetLogger<Program>());
            irc.Check();
        }

        private static void SomeJob()
        {
            using (var uw = new UnitOfWork())
            {
                var transports = uw.GetSet<TransportInfo>().ToList();

                foreach (var transport in transports)
                {
                    var boutsId =
                        uw.GetSet<LinkToPeople>().Where(l => l.TransportId == transport.Id).Select(l => l.BoutId).Distinct().ToList();
                    if (boutsId.Count > 1)
                    {
                        foreach (var boutId in boutsId)
                        {
                            if (boutId == transport.BoutId)
                            {
                                continue;
                            }

                            var bout = uw.GetById<Bout>(boutId ?? 0);
                            if (bout.TransportInfoFromId == transport.Id)
                            {
                                var nt = uw.AddEntity(new TransportInfo(transport, 0) { BoutId = boutId });
                                bout.TransportInfoFromId = nt.Id;
                                uw.Context.Entry(bout).State = EntityState.Modified;

                                var links = uw.GetSet<LinkToPeople>().Where(p => p.BoutId == boutId && p.TransportId == transport.Id).ToList();
                                foreach (var link in links)
                                {
                                    link.TransportId = nt.Id;
                                    uw.Context.Entry(link).State = EntityState.Modified;
                                }

                                uw.SaveChanges();
                            }
                            else if (bout.TransportInfoToId == transport.Id)
                            {
                                var nt = uw.AddEntity(new TransportInfo(transport, 0) { BoutId = boutId });
                                bout.TransportInfoToId = nt.Id;
                                uw.Context.Entry(bout).State = EntityState.Modified;

                                var links = uw.GetSet<LinkToPeople>().Where(p => p.BoutId == boutId && p.TransportId == transport.Id).ToList();
                                foreach (var link in links)
                                {
                                    link.TransportId = nt.Id;
                                    uw.Context.Entry(link).State = EntityState.Modified;
                                }

                                uw.SaveChanges();
                            }
                            else
                            {
                                Console.WriteLine("Странная связь.");
                            }
                        }
                    }
                }
            }
        }
    }
}
