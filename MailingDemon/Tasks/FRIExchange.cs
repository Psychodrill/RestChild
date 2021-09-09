using System;
using System.Collections;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Web;
using System.Text;
using System.Threading;
using System.Web.Configuration;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using IBM.WMQ;
using RestChild.Comon;
using RestChild.Comon.Enumeration;
using RestChild.DAL;
using RestChild.Domain;
using RestChild.MPGUIntegration.V61;
using RestChild.Security.Logger;
using UtilsSmev;
using Person = RestChild.MPGUIntegration.V61.Person;

namespace MailingDemon.Tasks
{
    /// <summary>
    ///     Взаимодействие с ФРИ
    /// </summary>
    [Task]
    public class FRIExchange : BaseTask
    {
        protected override void Execute()
        {
            Logger.Info("FRIExchange started");

            using (var unitOfWork = new UnitOfWork())
            {
                Logger.Info("FRIExchange generating ExchangeBaseRegistry entities start");

                var requests = unitOfWork.GetSet<Request>().Where(r => !r.IsDeleted && r.NeedSendForFRI).ToList();

                // для всех персон (детей и сопровождающих) в заявлениях где требуется обмен с ФРИ создаем записи в ExchangeBaseRegistry
                foreach (var request in requests)
                {
                    if (request.Applicant.IsInvalid)
                    {
                        var reqNum = request.RequestNumber;
                        var messageV6 = GetCoordinateMessageV6(SetServiceProperties(request.Applicant),
                            ((long) ExchangeBaseRegistryTypeEnum.FRIExchange).ToString(), reqNum + "/1", reqNum);

                        unitOfWork.AddEntity(new RestChild.Domain.ExchangeBaseRegistry
                        {
                            RequestGuid = Guid.NewGuid().ToString(),
                            ServiceNumber = reqNum,
                            ApplicantId = request.ApplicantId,
                            RequestText = Serialization.Serializer(messageV6),
                            IsProcessed = false,
                            IsIncoming = false,
                            OperationType = "FRIExchange",
                            ExchangeBaseRegistryTypeId =
                                (long) ExchangeBaseRegistryTypeEnum.FRIExchange,
                        });
                    }

                    if (request.Child.Any(c => c.IsInvalid))
                    {
                        var children = request.Child.Where(c => c.IsInvalid).ToList();
                        foreach (var child in children)
                        {
                            var reqNum = request.RequestNumber;
                            var messageV6 = GetCoordinateMessageV6(SetServiceProperties(child),
                                ((long) ExchangeBaseRegistryTypeEnum.FRIExchange).ToString(), reqNum + "/1", reqNum);

                            unitOfWork.AddEntity(new RestChild.Domain.ExchangeBaseRegistry
                            {
                                RequestGuid = Guid.NewGuid().ToString(),
                                ServiceNumber = reqNum,
                                ChildId = child.Id,
                                RequestText = Serialization.Serializer(messageV6),
                                IsProcessed = false,
                                IsIncoming = false,
                                OperationType = "FRIExchange",
                                ExchangeBaseRegistryTypeId =
                                    (long) ExchangeBaseRegistryTypeEnum.FRIExchange,
                            });
                        }
                    }
                    request.NeedSendForFRI = false;
                    unitOfWork.SaveChanges();
                }

                Logger.Info("FRIExchange generating ExchangeBaseRegistry entities finish");
            }
        }

        /// <summary>
        ///     Сформировать текст запроса
        /// </summary>
        private string SetServiceProperties(object obj)
        {
            var sp = new PersonDataRequest();

            if (obj is Child child)
            {
                sp.lastname = child.LastName;
                sp.firstname = child.FirstName;
                sp.secondname = child.MiddleName;
                sp.birthdate = child.DateOfBirth ?? new DateTime();
                sp.snils = child.Snils;
                sp.ismale = child.Male;
                sp.issuedatefrom = DateTime.Now;
                sp.issuedateto = DateTime.Now;
                sp.buroname = string.Empty;
                sp.regionname = string.Empty;
                sp.senderinfo = string.Empty;
            }
            else if (obj is Applicant applicant)
            {
                sp.lastname = applicant.LastName;
                sp.firstname = applicant.FirstName;
                sp.secondname = applicant.MiddleName;
                sp.birthdate = applicant.DateOfBirth ?? new DateTime();
                sp.snils = applicant.Snils;
                sp.ismale = applicant.Male ?? true;
                sp.issuedatefrom = DateTime.Now;
                sp.issuedateto = DateTime.Now;
                sp.buroname = string.Empty;
                sp.regionname = string.Empty;
                sp.senderinfo = string.Empty;
            }

            var sb = new StringBuilder();

            sb.Append("<ServiceProperties>");
            sb.Append($"<lastname>{sp.lastname}</lastname>");
            sb.Append($"<firstname>{sp.firstname}</firstname>");
            sb.Append($"<secondname>{sp.secondname}</secondname>");
            sb.Append($"<birthdate>{sp.birthdate}</birthdate>");
            sb.Append($"<snils>{sp.snils}</snils>");
            sb.Append($"<ismale>{sp.ismale}</ismale>");
            sb.Append($"<senderinfo>{sp.senderinfo}</senderinfo>");
            sb.Append($"<issuedatefrom>{sp.issuedatefrom}</issuedatefrom>");
            sb.Append($"<issuedateto>{sp.issuedateto}</issuedateto>");
            sb.Append($"<buroname>{sp.buroname}</buroname>");
            sb.Append($"<regionname>{sp.regionname}</regionname>");
            sb.Append("<testmsg/>");
            sb.Append("</ServiceProperties>");

            return sb.ToString();
        }

         /// <summary>
        ///     получить сообщение по V6.1
        /// </summary>
        private CoordinateTaskMessage GetCoordinateMessageV6(string request,
            string documentTypeCode, string taskNumber, string serviceNumber)
        {
            var message = Guid.NewGuid().ToString();
            var doc = new XmlDocument();
            doc.LoadXml(request);
            var par = doc.DocumentElement;

            return new CoordinateTaskMessage
            {
                CoordinateTaskDataMessage = new CoordinateTaskData
                {
                    Data = new TaskDataType
                    {
                        DocumentTypeCode = documentTypeCode,
                        IncludeBinaryView = false,
                        IncludeXmlView = true,
                        Parameter = par
                    },
                    Task = new TaskType
                    {
                        MessageId = message,
                        TaskId = message,
                        TaskDate = DateTime.Now,
                        TaskNumber = taskNumber
                    }
                }
            };
        }
    }
}
