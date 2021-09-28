using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Http;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestChild.Booking.Logic.Extensions;
using RestChild.Comon;
using RestChild.Comon.Dto;
using RestChild.Comon.Dto.Commercial;
using RestChild.Comon.Enumeration;
using RestChild.Comon.Exchange.Cpmpk;
using RestChild.Comon.Exchange.PassportRegistration;
using RestChild.DAL.RepositoryExtensions;
using RestChild.Domain;
using RestChild.MPGUIntegration.V61;
using RestChild.Web.Properties;
using Person = RestChild.MPGUIntegration.V61.Person;

namespace RestChild.Web.Controllers.WebApi
{
    /// <summary>
    ///     обмен с АС УР БР
    /// </summary>
    public partial class WebExchangeController
    {
        /// <summary>
        ///     обновить запрос для пере-отправки
        /// </summary>
        private ExchangeBaseRegistry RefreshExchangeBaseRegistry(ExchangeBaseRegistry exchangeBaseRegistry, Request req)
        {
            exchangeBaseRegistry.RequestGuid = Guid.NewGuid().ToString();

            string taskNumber;

            if (req != null && req.TypeOfRestId != (long)TypeOfRestEnum.Compensation &&
                req.TypeOfRestId != (long)TypeOfRestEnum.CompensationYouthRest)
            {
                var count = UnitOfWork.GetSet<ExchangeBaseRegistry>().Count(v =>
                    v.ApplicantId == req.ApplicantId || v.Applicant.RequestId == req.Id ||
                    v.Child.RequestId == req.Id) + 1;
                taskNumber = $"{req.RequestNumber}/{count}";
            }
            else
            {
                var exchangeBaseRegistryCode = WebConfigurationManager.AppSettings["exchangeBaseRegistryCode"];
                taskNumber = $"{GetServiceNumber(exchangeBaseRegistryCode)}/1";
            }

            try
            {
                var element = Serialization.Deserialize<CoordinateTaskMessage>(exchangeBaseRegistry.RequestText);

                if (element.CoordinateTaskDataMessage?.Task != null)
                {
                    element.CoordinateTaskDataMessage.Task.TaskId = exchangeBaseRegistry.RequestGuid;
                    element.CoordinateTaskDataMessage.Task.MessageId = exchangeBaseRegistry.RequestGuid;
                    element.CoordinateTaskDataMessage.Task.TaskDate = DateTime.Now;
                    element.CoordinateTaskDataMessage.Task.TaskNumber = taskNumber;
                    exchangeBaseRegistry.ServiceNumber = taskNumber;
                    exchangeBaseRegistry.RequestText = Serialization.Serializer(element);
                    return exchangeBaseRegistry;
                }
            }
            catch
            {
            }

            return null;
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
                        TaskNumber = taskNumber,
                        ItemsElementName = new[]
                        {
                            MPGUIntegration.V61.ItemsChoiceType.ServiceNumber,
                            MPGUIntegration.V61.ItemsChoiceType.ServiceTypeCode
                        },
                        Items = new[]
                            { serviceNumber, WebConfigurationManager.AppSettings["serviceTypeCode"] ?? "048001" },
                        Department = new Department
                        {
                            Code = WebConfigurationManager.AppSettings["DepartmentCode"],
                            Inn = WebConfigurationManager.AppSettings["DepartmentInn"],
                            Name = WebConfigurationManager.AppSettings["DepartmentName"],
                            Ogrn = WebConfigurationManager.AppSettings["DepartmentOgrn"],
                            RegDate = new DateTime(2014, 12, 1),
                            SystemCode = WebConfigurationManager.AppSettings["exchangeSystemCode"] ?? "9000063"
                        },
                        Responsible = new Person
                        {
                            LastName = WebConfigurationManager.AppSettings["ResponsibleLastName"],
                            FirstName = WebConfigurationManager.AppSettings["ResponsibleFirstName"],
                            MiddleName = WebConfigurationManager.AppSettings["ResponsibleMiddleName"],
                            Email = WebConfigurationManager.AppSettings["ResponsibleEmail"],
                            Phone = WebConfigurationManager.AppSettings["ResponsiblePhone"],
                            JobTitle = WebConfigurationManager.AppSettings["ResponsibleJobTitle"]
                        }
                    }
                }
            };
        }

        /// <summary>
        ///     проверка заявителя по льготе
        /// </summary>
        [HttpPost]
        [HttpGet]
        public int CheckApplicantInBaseRegistry(string requestNumber, long[] applicantId, int count)
        {
            foreach (var appId in applicantId)
            {
                try
                {
                    var applicant = UnitOfWork.GetById<Applicant>(appId);
                    if (!applicant.DocumentTypeId.HasValue)
                    {
                        return count;
                    }

                    ResetCheckApplicantInBaseRegistry(appId, ExchangeBaseRegistryTypeEnum.Benefit);

                    var documentType = applicant.DocumentType ??
                                       UnitOfWork.GetById<DocumentType>(applicant.DocumentTypeId.Value);

                    var request =
                        $@"<ServiceProperties>{(!string.IsNullOrEmpty(applicant.Snils) ? $"<snils>{applicant.Snils}</snils>" : string.Empty)}<birthdate>{applicant.DateOfBirth:yyyy-MM-dd}T00:00:00Z</birthdate><doctype>{
                            documentType.BaseRegistryUid}</doctype><firstname>{applicant.FirstName}</firstname><lastname>{applicant.LastName
                            }</lastname><middlename>{applicant.MiddleName}</middlename><passport>{applicant.DocumentNumber
                            }</passport><passport_serie>{applicant.DocumentSeria}</passport_serie></ServiceProperties>";


                    var messageV6 = GetCoordinateMessageV6(request,
                        ((long)ExchangeBaseRegistryTypeEnum.Benefit).ToString(),
                        requestNumber + $"/{count++}", requestNumber);

                    UnitOfWork.AddEntity(new ExchangeBaseRegistry
                    {
                        IsIncoming = false,
                        RequestText = Serialization.Serializer(messageV6),
                        OperationType = "SendTask",
                        RequestGuid = messageV6.CoordinateTaskDataMessage.Task.TaskId,
                        ServiceNumber = messageV6.CoordinateTaskDataMessage.Task.TaskNumber,
                        Applicant = applicant,
                        ApplicantId = appId,
                        ExchangeBaseRegistryTypeId = (long)ExchangeBaseRegistryTypeEnum.Benefit
                    });

                    applicant.BenefitApprove = false;
                    applicant.BenefitApproveComment =
                        string.Format(@"В Базовый регистр направлен запрос {1}, {0} для подтверждения льготы",
                            messageV6.CoordinateTaskDataMessage.Task.TaskNumber,
                            DateTime.Now.FormatEx());

                    applicant.BenefitRequestDate = applicant.BenefitRequestDate ?? DateTime.Now;

                    UnitOfWork.SaveChanges();
                }
                catch (Exception ex)
                {
                    Logger.Error("Ошибка отправки в базовый регистр, applicantId: " + applicantId, ex);
                }
            }

            return count;
        }

        /// <summary>
        ///     Запроса паспортного досье по СНИЛС в ЕТП
        /// </summary>
        [HttpPost]
        [HttpGet]
        internal void BrGetPassportDataBySnils(Request req)
        {
            try
            {
                if (req == null)
                {
                    return;
                }

                var count = UnitOfWork.GetSet<ExchangeBaseRegistry>().Count(v =>
                    v.ApplicantId == req.ApplicantId || v.Applicant.RequestId == req.Id ||
                    v.Child.RequestId == req.Id) + 1;
                var requestNumber = req.RequestNumber;
                if (req.TypeOfRestId == (long)TypeOfRestEnum.Compensation ||
                    req.TypeOfRestId == (long)TypeOfRestEnum.CompensationYouthRest)
                {
                    var exchangeBaseRegistryCode = WebConfigurationManager.AppSettings["exchangeBaseRegistryCode"];
                    requestNumber = GetServiceNumber(exchangeBaseRegistryCode);
                    count = 1;
                }

                if (req.ApplicantId.HasValue && !string.IsNullOrWhiteSpace(req.Applicant?.Snils) &&
                    req.Applicant.DocumentTypeId % 100 == 1)
                {
                    ResetCheckApplicantInBaseRegistry(req.ApplicantId.Value,
                        ExchangeBaseRegistryTypeEnum.PassportDataBySNILS);

                    var request = BrPassportBySNILSMessage(req.Applicant.Snils);
                    var messageV6 = GetCoordinateMessageV6(request,
                        ((long)ExchangeBaseRegistryTypeEnum.PassportDataBySNILS).ToString(),
                        requestNumber + $"/{count++}", requestNumber);

                    UnitOfWork.AddEntity(new ExchangeBaseRegistry
                    {
                        IsIncoming = false,
                        RequestText = Serialization.Serializer(messageV6),
                        OperationType = "SendTask",
                        RequestGuid = messageV6.CoordinateTaskDataMessage.Task.TaskId,
                        ServiceNumber = messageV6.CoordinateTaskDataMessage.Task.TaskNumber,
                        Applicant = req.Applicant,
                        ApplicantId = req.ApplicantId,
                        ExchangeBaseRegistryTypeId = (long)ExchangeBaseRegistryTypeEnum.PassportDataBySNILS
                    });
                }

                if (req.Attendant != null && req.Attendant.Any())
                {
                    foreach (var c in req.Attendant)
                    {
                        ResetCheckApplicantInBaseRegistry(c.Id,
                            ExchangeBaseRegistryTypeEnum.PassportDataBySNILS);
                        if (string.IsNullOrWhiteSpace(c.Snils) || c.DocumentTypeId % 100 != 1)
                        {
                            continue;
                        }

                        var request = BrPassportBySNILSMessage(c.Snils);
                        var messageV6 = GetCoordinateMessageV6(request,
                            ((long)ExchangeBaseRegistryTypeEnum.PassportDataBySNILS).ToString(),
                            requestNumber + $"/{count++}", requestNumber);

                        UnitOfWork.AddEntity(new ExchangeBaseRegistry
                        {
                            IsIncoming = false,
                            RequestText = Serialization.Serializer(messageV6),
                            OperationType = "SendTask",
                            RequestGuid = messageV6.CoordinateTaskDataMessage.Task.TaskId,
                            ServiceNumber = messageV6.CoordinateTaskDataMessage.Task.TaskNumber,
                            Applicant = c,
                            ApplicantId = c.Id,
                            ExchangeBaseRegistryTypeId = (long)ExchangeBaseRegistryTypeEnum.PassportDataBySNILS
                        });
                    }
                }

                if (req.Child != null)
                {
                    foreach (var c in req.Child)
                    {
                        ResetCheckChildInBaseRegistry(c.Id, ExchangeBaseRegistryTypeEnum.PassportDataBySNILS);
                        if (string.IsNullOrWhiteSpace(c.Snils) || c.DocumentTypeId % 100 != 1)
                        {
                            continue;
                        }

                        var request = BrPassportBySNILSMessage(c.Snils);
                        var messageV6 = GetCoordinateMessageV6(request,
                            ((long)ExchangeBaseRegistryTypeEnum.PassportDataBySNILS).ToString(),
                            requestNumber + $"/{count++}", requestNumber);

                        UnitOfWork.AddEntity(new ExchangeBaseRegistry
                        {
                            IsIncoming = false,
                            RequestText = Serialization.Serializer(messageV6),
                            OperationType = "SendTask",
                            RequestGuid = messageV6.CoordinateTaskDataMessage.Task.TaskId,
                            ServiceNumber = messageV6.CoordinateTaskDataMessage.Task.TaskNumber,
                            Child = c,
                            ChildId = c.Id,
                            ExchangeBaseRegistryTypeId = (long)ExchangeBaseRegistryTypeEnum.PassportDataBySNILS
                        });
                    }
                }

                UnitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                Logger.Error("Ошибка отправки в базовый регистр запроса паспортного досье по СНИЛС", ex);
            }
        }

        /// <summary>
        ///     Запроса проверки регистрации по паспорту в АС УР
        /// </summary>
        [HttpPost]
        [HttpGet]
        internal void BrGetRegistrationDataByPassport(Request req)
        {
            try
            {
                if (req == null)
                {
                    return;
                }

                var count = UnitOfWork.GetSet<ExchangeBaseRegistry>().Count(v =>
                    v.ApplicantId == req.ApplicantId || v.Applicant.RequestId == req.Id ||
                    v.Child.RequestId == req.Id) + 1;

                var requestNumber = req.RequestNumber;
                if (req.TypeOfRestId == (long)TypeOfRestEnum.Compensation ||
                    req.TypeOfRestId == (long)TypeOfRestEnum.CompensationYouthRest)
                {
                    var exchangeBaseRegistryCode = WebConfigurationManager.AppSettings["exchangeBaseRegistryCode"];
                    requestNumber = GetServiceNumber(exchangeBaseRegistryCode);
                    count = 1;
                }

                if ((req.TypeOfRest?.Id == (long)TypeOfRestEnum.YouthRestCamps ||
                     req.TypeOfRest?.ParentId == (long)TypeOfRestEnum.YouthRestCamps) && req.ApplicantId.HasValue &&
                    req.Applicant.DocumentTypeId % 100 == 1)
                {
                    ResetCheckApplicantInBaseRegistry(req.ApplicantId.Value,
                        ExchangeBaseRegistryTypeEnum.PassportRegistration);

                    ResetCheckApplicantInBaseRegistry(req.ApplicantId.Value,
                        ExchangeBaseRegistryTypeEnum.GetPassportRegistration);

                    var sp = new ServiceProperties
                    {
                        firstname = req.Applicant.FirstName,
                        snils = req.Applicant.Snils,
                        passport_serie = req.Applicant.DocumentSeria,
                        passport = req.Applicant.DocumentNumber,
                        passport_date = req.Applicant.DocumentDateOfIssue.DateTimeToXml(),
                        lastname = req.Applicant.LastName,
                        middlename = req.Applicant.MiddleName,
                        address1_town = "Москва",
                        doctype = 1,
                        birthdate = req.Applicant.DateOfBirth.DateTimeToXml()
                    };

                    var request = BrGetRegistrationByPassportMessage(sp);
                    var messageV6 = GetCoordinateMessageV6(request,
                        ((long)ExchangeBaseRegistryTypeEnum.GetPassportRegistration).ToString(),
                        requestNumber + $"/{count++}", requestNumber);

                    UnitOfWork.AddEntity(new ExchangeBaseRegistry
                    {
                        IsIncoming = false,
                        RequestText = Serialization.Serializer(messageV6),
                        OperationType = "SendTask",
                        RequestGuid = messageV6.CoordinateTaskDataMessage.Task.TaskId,
                        ServiceNumber = messageV6.CoordinateTaskDataMessage.Task.TaskNumber,
                        Applicant = req.Applicant,
                        ApplicantId = req.ApplicantId,
                        ExchangeBaseRegistryTypeId = (long)ExchangeBaseRegistryTypeEnum.GetPassportRegistration
                    });
                }

                var benefitTypes =
                    System.Configuration.ConfigurationManager.AppSettings["OrphansInvalid"]?.Split(',')
                        .Select(v => v.LongParse()).Where(v => v.HasValue).ToArray() ??
                    new long?[] { 1, 11, 18, 34, 35, 38, 41, 48, 67 };

                if (req.Child != null)
                {
                    foreach (var c in req.Child.Where(c =>
                        benefitTypes.Contains(c.BenefitTypeId ?? 0) ||
                        benefitTypes.Contains(c.BenefitType?.SameBenefitId ?? 0)).ToArray())
                    {
                        ResetCheckChildInBaseRegistry(c.Id, ExchangeBaseRegistryTypeEnum.PassportRegistration);
                        ResetCheckChildInBaseRegistry(c.Id, ExchangeBaseRegistryTypeEnum.GetPassportRegistration);
                        if (c.DocumentTypeId % 100 != 1 && c.DocumentTypeId % 100 != 22)
                        {
                            continue;
                        }

                        var sp = new ServiceProperties
                        {
                            firstname = c.FirstName,
                            snils = c.Snils,
                            passport_serie = c.DocumentSeria,
                            passport = c.DocumentNumber,
                            passport_date = c.DocumentDateOfIssue.DateTimeToXml(),
                            doctype = (byte)(c.DocumentTypeId % 100 == 1 ? 1 : 3),
                            lastname = c.LastName,
                            middlename = c.MiddleName,
                            address1_town = "Москва",
                            birthdate = c.DateOfBirth.DateTimeToXml()
                        };

                        var request = BrGetRegistrationByPassportMessage(sp);
                        var messageV6 = GetCoordinateMessageV6(request,
                            ((long)ExchangeBaseRegistryTypeEnum.GetPassportRegistration).ToString(),
                            requestNumber + $"/{count++}", requestNumber);

                        UnitOfWork.AddEntity(new ExchangeBaseRegistry
                        {
                            IsIncoming = false,
                            RequestText = Serialization.Serializer(messageV6),
                            OperationType = "SendTask",
                            RequestGuid = messageV6.CoordinateTaskDataMessage.Task.TaskId,
                            ServiceNumber = messageV6.CoordinateTaskDataMessage.Task.TaskNumber,
                            Child = c,
                            ChildId = c.Id,
                            ExchangeBaseRegistryTypeId = (long)ExchangeBaseRegistryTypeEnum.GetPassportRegistration
                        });
                    }
                }

                UnitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                Logger.Error("Ошибка отправки в базовый регистр запроса данных регистрации по паспорту", ex);
            }
        }

        /// <summary>
        ///     генерация сообщения (CustomAttributes)
        /// </summary>
        /// <returns></returns>
        private string BrPassportBySNILSMessage(string snils)
        {
            return $"<ServiceProperties><snils>{snils}</snils></ServiceProperties>";
        }

        /*
        /// <summary>
        ///     генерация сообщения по заявителю  (CustomAttributes)
        /// </summary>
        private string BrRegistrationByPassportMessage(ServiceProperties sp)
        {
            var sb = new StringBuilder();
            sb.Append("<ServiceProperties>");
            sb.Append($"<firstname>{sp.firstname}</firstname>");
            sb.Append($"<middlename>{sp.middlename}</middlename>");
            sb.Append($"<lastname>{sp.lastname}</lastname>");
            sb.Append($"<birthdate>{sp.birthdate}</birthdate>");

            sb.Append($"<address1_line1>{sp.address1_line1?.Trim()}</address1_line1>");
            sb.Append($"<address1_line2>{sp.address1_line2?.Trim()}</address1_line2>");
            sb.Append($"<address1_line3>{sp.address1_line3?.Trim()}</address1_line3>");
            sb.Append($"<address1_line4>{sp.address1_line4?.Trim()}</address1_line4>");
            if (string.IsNullOrWhiteSpace(sp.address1_town))
            {
                sp.address1_town = "Москва";
            }

            sb.Append($"<address1_town>{sp.address1_town}</address1_town>");
            sb.Append("<address1_stateorprovince>45000000000</address1_stateorprovince>");
            sb.Append($"<address2_stateorprovince>45000000000</address2_stateorprovince>");
            //sb.Append($"<address2_stateorprovince>{sp.address2_stateorprovince}</address2_stateorprovince>");
            sb.Append($"<passport_serie>{sp.passport_serie}</passport_serie>");
            sb.Append($"<passport>{sp.passport}</passport>");
            sb.Append($"<passport_date>{sp.passport_date}</passport_date>");
            // sb.Append($"<snils>{sp.snils}</snils>");
            sb.Append($"<doctype>{sp.doctype}</doctype>");
            sb.Append("</ServiceProperties>");
            return sb.ToString();
        }*/

        /// <summary>
        ///     генерация сообщения по заявителю  (CustomAttributes)
        /// </summary>
        private string BrGetRegistrationByPassportMessage(ServiceProperties sp)
        {
            var sb = new StringBuilder();
            sb.Append("<ServiceProperties>");
            sb.Append($"<firstname>{sp.firstname}</firstname>");
            sb.Append($"<middlename>{sp.middlename}</middlename>");
            sb.Append($"<lastname>{sp.lastname}</lastname>");
            sb.Append($"<birthdate>{sp.birthdate}</birthdate>");
            sb.Append("<address1_stateorprovince>45000000000</address1_stateorprovince>");
            sb.Append($"<passport_serie>{sp.passport_serie}</passport_serie>");
            sb.Append($"<passport>{sp.passport}</passport>");
            sb.Append($"<passport_date>{sp.passport_date}</passport_date>");
            //sb.Append($"<snils>{sp.snils}</snils>");
            sb.Append($"<doctype>{sp.doctype}</doctype>");
            sb.Append("</ServiceProperties>");
            return sb.ToString();
        }

        /// <summary>
        ///     декодировать код документа
        /// </summary>
        private int DecodeDocumentType(DocumentType documentType)
        {
            if (documentType?.BaseRegistryUid == "5")
            {
                return 2;
            }

            if (documentType?.BaseRegistryUid == "10")
            {
                return 8;
            }

            return 1;
        }

        /// <summary>
        ///     проверка ребёнка на свидетельство о рождении СМЭВ
        /// </summary>
        internal BaseResponse AdditionalRelationshipSmev(BenefitCheckRequest model)
        {
            try
            {
                var documentType = UnitOfWork.GetById<DocumentType>(model.DocumentTypeId);

                var request =
                    $@"<ServiceProperties>
 <form_response>2</form_response>
 <act>1</act>
 <snils>{model.Snils}</snils>
 <lastname>{model.LastName}</lastname>
 <firstname>{model.FirstName}</firstname>
 <middlename>{model.MiddleName}</middlename>
 <birthdate>{model.DateOfBirth?.ToString("yyyy-MM-dd")}T00:00:00Z</birthdate>
 <doc_type>{documentType?.BaseRegistryUid}</doc_type>
 <doc_name>{documentType?.Name}</doc_name>
 <seriesnumber>{model.DocumentSeria} {model.DocumentNumber}</seriesnumber>
</ServiceProperties>";

                if (Settings.Default.SnilsTestRequest)
                {
                    request =
                        @"<ServiceProperties>
 <doc_id>a</doc_id>
 <form_response>2</form_response>
 <act>1</act>
 <actnumber>a</actnumber>
 <actdate>2018-08-13T00:00:00Z</actdate>
 <nameofregistrar>a</nameofregistrar>
 <code_zags>R0000000</code_zags>
 <snils>000 000 000 00</snils>
 <inn_fl>010000000000</inn_fl>
 <lastname>Иванов</lastname>
 <firstname>Иван</firstname>
 <middlename>Иванович</middlename>
 <birthdate>2018-08-13T00:00:00Z</birthdate>
 <testmsg/>
</ServiceProperties>";
                }

                var exchangeBaseRegistryCode = WebConfigurationManager.AppSettings["exchangeBaseRegistryCode"];
                var requestNumber = GetServiceNumber(exchangeBaseRegistryCode);

                var messageV6 = GetCoordinateMessageV6(request,
                    ((long)ExchangeBaseRegistryTypeEnum.RelationshipSmev).ToString(),
                    requestNumber + "/1", requestNumber);

                UnitOfWork.AddEntity(new ExchangeBaseRegistry
                {
                    IsIncoming = false,
                    RequestText = Serialization.Serializer(messageV6),
                    OperationType = "SendTask",
                    RequestGuid = messageV6.CoordinateTaskDataMessage.Task.TaskId,
                    ServiceNumber = messageV6.CoordinateTaskDataMessage.Task.TaskNumber,
                    ExchangeBaseRegistryTypeId = (long)ExchangeBaseRegistryTypeEnum.RelationshipSmev,
                    IsAddonRequest = true,
                    BirthDate = model.DateOfBirth,
                    SearchField =
                        $"{model.LastName?.ToLower().Trim()}|{model.FirstName?.ToLower().Trim()}|{model.MiddleName?.ToLower().Trim()}"
                });

                UnitOfWork.SaveChanges();
                return new BaseResponse
                {
                    Name = "Сформирован запрос в базовый регистр номер " +
                           messageV6.CoordinateTaskDataMessage.Task.TaskNumber
                };
            }
            catch (Exception ex)
            {
                Logger.Error("Ошибка отправки в базовый регистр запроса СНИЛС по ФИО", ex);
                return new BaseResponse { HasError = true, ErrorMessage = ex.Message };
            }
        }

        /// <summary>
        ///     Запрос информации ЦПМПК
        /// </summary>
        internal BaseResponse AdditionallyCheckCPMPK(BenefitCheckRequest model)
        {
            try
            {
                var requestText =
                    $"/app/document/do?fullName={HttpUtility.UrlEncode($"{model.LastName} {model.FirstName} {model.MiddleName}")}&dob={model.DateOfBirth.XmlToString()}";

                UnitOfWork.AddEntity(new ExchangeBaseRegistry
                {
                    RequestGuid = Guid.NewGuid().ToString(),
                    ChildId = null,
                    RequestText = requestText,
                    ResponseText = null,
                    SendDate = DateTime.Now,
                    ResponseDate = DateTime.Now,
                    IsProcessed = false,
                    IsIncoming = false,
                    OperationType = "cpmpkrequest",
                    //Success = false,
                    ExchangeBaseRegistryTypeId = (long)ExchangeBaseRegistryTypeEnum.CpmpkExchange,
                    ServiceNumber = "б/н",
                    ResponseGuid = "б/н",
                    EidSendStatus = 0
                });

                UnitOfWork.SaveChanges();

                return new BaseResponse
                {
                    Name = "Сформирован запрос информации ЦПМПК"
                };
            }
            catch (Exception ex)
            {
                Logger.Error("Ошибка запроса информации ЦПМПК", ex);
                return new BaseResponse { HasError = true, ErrorMessage = ex.Message };
            }
        }

        /// <summary>
        ///     Запрос информации об адресе регистрации
        /// </summary>
        internal BaseResponse AdditionallyCheckRegistrationAddress(BenefitCheckRequest model)
        {
            try
            {
                var documentType = UnitOfWork.GetById<DocumentType>(model.DocumentTypeId);

                var sp = new ServiceProperties
                {
                    firstname = model.FirstName,
                    middlename = model.MiddleName,
                    lastname = model.LastName,
                    birthdate = model.DateOfBirth.DateTimeToXml(),
                    address1_town = model.AddressCity,
                    address1_line1 = model.AddressLine1,
                    address1_line2 = model.AddressLine2,
                    address1_line3 = model.AddressLine3,
                    address1_line4 = model.AddressLine4,
                    passport_serie = model.DocumentSeria,
                    passport = model.DocumentNumber,
                    passport_date = model.DocumentDateOfIssue.DateTimeToXml(),
                    doctype = 1,
                };

                var sb = new StringBuilder();
                sb.Append("<ServiceProperties>");
                sb.Append($"<firstname>{sp.firstname}</firstname>");
                sb.Append($"<middlename>{sp.middlename}</middlename>");
                sb.Append($"<lastname>{sp.lastname}</lastname>");
                sb.Append($"<birthdate>{sp.birthdate}</birthdate>");

                sb.Append($"<address1_line1>{sp.address1_line1?.Trim()}</address1_line1>");
                sb.Append($"<address1_line2>{sp.address1_line2?.Trim()}</address1_line2>");
                sb.Append($"<address1_line3>{sp.address1_line3?.Trim()}</address1_line3>");
                sb.Append($"<address1_line4>{sp.address1_line4?.Trim()}</address1_line4>");
                if (string.IsNullOrWhiteSpace(sp.address1_town))
                {
                    sp.address1_town = "Москва";
                }

                sb.Append($"<address1_town>{sp.address1_town}</address1_town>");
                sb.Append("<address1_stateorprovince>45000000000</address1_stateorprovince>");
                sb.Append($"<address2_stateorprovince>45000000000</address2_stateorprovince>");
                sb.Append($"<passport_serie>{sp.passport_serie}</passport_serie>");
                sb.Append($"<passport>{sp.passport}</passport>");
                sb.Append($"<passport_date>{sp.passport_date}</passport_date>");
                sb.Append($"<doctype>{sp.doctype}</doctype>");
                sb.Append("</ServiceProperties>");

                var request = sb.ToString();


                var exchangeBaseRegistryCode = WebConfigurationManager.AppSettings["exchangeBaseRegistryCode"];
                var requestNumber = GetServiceNumber(exchangeBaseRegistryCode);

                var messageV6 = GetCoordinateMessageV6(request,
                    ((long)ExchangeBaseRegistryTypeEnum.PassportRegistration).ToString(),
                    requestNumber + "/1", requestNumber);

                UnitOfWork.AddEntity(new ExchangeBaseRegistry
                {
                    IsIncoming = false,
                    RequestText = Serialization.Serializer(messageV6),
                    OperationType = "SendTask",
                    RequestGuid = messageV6.CoordinateTaskDataMessage.Task.TaskId,
                    ServiceNumber = messageV6.CoordinateTaskDataMessage.Task.TaskNumber,
                    ExchangeBaseRegistryTypeId = (long)ExchangeBaseRegistryTypeEnum.PassportRegistration
                });

                UnitOfWork.SaveChanges();
                return new BaseResponse
                {
                    Name = "Сформирован запрос в базовый регистр номер " +
                           messageV6.CoordinateTaskDataMessage.Task.TaskNumber
                };
            }
            catch (Exception ex)
            {
                Logger.Error("Ошибка отправки в базовый регистр запроса адреса регистрации", ex);
                return new BaseResponse { HasError = true, ErrorMessage = ex.Message };
            }
        }

        /// <summary>
        ///     запрос информации о ребёнке родство
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        internal BaseResponse AdditionallyCheckRelative(BenefitCheckRequest model)
        {
            try
            {
                var documentType = UnitOfWork.GetById<DocumentType>(model.DocumentTypeId);


                var request =
                    $@"<ServiceProperties><lastname3>{model.LastName}</lastname3><firstname3>{model.FirstName}</firstname3><middlename>{model.MiddleNameParent}</middlename><middlename3>{model.MiddleName}</middlename3><birthdate3>{model.DateOfBirth:yyyy-MM-dd}T00:00:00</birthdate3><birthdate>{model.DateOfBirthParent:yyyy-MM-dd}T00:00:00</birthdate><firstname>{model.FirstNameParent}</firstname><lastname>{model.LastNameParent}</lastname><priznak>{(model.MaleParent ? 1 : 2)}</priznak></ServiceProperties>";


                if (string.IsNullOrWhiteSpace(documentType.BaseRegistryUid))
                {
                    return new BaseResponse { HasError = true, ErrorMessage = "Не найден вид документа." };
                }

                var exchangeBaseRegistryCode = WebConfigurationManager.AppSettings["exchangeBaseRegistryCode"];

                var requestNumber = GetServiceNumber(exchangeBaseRegistryCode);

                var messageV6 = GetCoordinateMessageV6(request,
                    ((long)ExchangeBaseRegistryTypeEnum.Relationship).ToString(),
                    requestNumber + "/1", requestNumber);

                var eb = UnitOfWork.AddEntity(new ExchangeBaseRegistry
                {
                    IsIncoming = true,
                    IsProcessed = true,
                    OperationType = "SendTask",
                    RequestText = Serialization.Serializer(messageV6),
                    RequestGuid = messageV6.CoordinateTaskDataMessage.Task.TaskId,
                    ServiceNumber = messageV6.CoordinateTaskDataMessage.Task.TaskNumber,
                    Child = null,
                    ChildId = null,
                    ExchangeBaseRegistryTypeId = (long)ExchangeBaseRegistryTypeEnum.Relationship,
                    IsAddonRequest = true,
                    BirthDate = model.DateOfBirth,
                    SearchField =
                        $"{model.LastName?.ToLower().Trim()}|{model.FirstName?.ToLower().Trim()}|{model.MiddleName?.ToLower().Trim()}",
                });

                eb.IsIncoming = false;
                eb.IsProcessed = false;
                UnitOfWork.SaveChanges();

                return new BaseResponse
                {
                    Name = "Сформирован запрос в базовый регистр номер " +
                           messageV6.CoordinateTaskDataMessage.Task.TaskNumber
                };
            }
            catch (Exception ex)
            {
                Logger.Error("Ошибка отправки в базовый регистр", ex);
                return new BaseResponse { HasError = true, ErrorMessage = ex.Message };
            }
        }

        /// <summary>
        ///     запрос информации о ребёнке
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        internal BaseResponse AdditionallyCheckBenefit(BenefitCheckRequest model)
        {
            try
            {
                var documentType = UnitOfWork.GetById<DocumentType>(model.DocumentTypeId);

                var request =
                    $@"<ServiceProperties>{(!string.IsNullOrEmpty(model.Snils) ? $"<snils>{model.Snils}</snils>" : string.Empty)}<birthdate>{model.DateOfBirth:yyyy-MM-dd}T00:00:00Z</birthdate><doctype>{
                        documentType.BaseRegistryUid}</doctype><firstname>{model.FirstName}</firstname><lastname>{model.LastName
                        }</lastname><middlename>{model.MiddleName}</middlename><passport>{model.DocumentNumber
                        }</passport><passport_serie>{model.DocumentSeria}</passport_serie></ServiceProperties>";

                if (string.IsNullOrWhiteSpace(documentType.BaseRegistryUid))
                {
                    return new BaseResponse { HasError = true, ErrorMessage = "Не найден вид документа." };
                }

                var exchangeBaseRegistryCode = WebConfigurationManager.AppSettings["exchangeBaseRegistryCode"];

                var requestNumber = GetServiceNumber(exchangeBaseRegistryCode);

                var messageV6 = GetCoordinateMessageV6(request,
                    ((long)ExchangeBaseRegistryTypeEnum.Benefit).ToString(),
                    requestNumber + "/1", requestNumber);

                var eb = UnitOfWork.AddEntity(new ExchangeBaseRegistry
                {
                    IsIncoming = true,
                    IsProcessed = true,
                    OperationType = "SendTask",
                    RequestText = Serialization.Serializer(messageV6),
                    RequestGuid = messageV6.CoordinateTaskDataMessage.Task.TaskId,
                    ServiceNumber = messageV6.CoordinateTaskDataMessage.Task.TaskNumber,
                    Child = null,
                    ChildId = null,
                    ExchangeBaseRegistryTypeId = (long)ExchangeBaseRegistryTypeEnum.Benefit,
                    IsAddonRequest = true,
                    BirthDate = model.DateOfBirth,
                    SearchField =
                        $"{model.LastName?.ToLower().Trim()}|{model.FirstName?.ToLower().Trim()}|{model.MiddleName?.ToLower().Trim()}"
                });

                eb.IsIncoming = false;
                eb.IsProcessed = false;
                UnitOfWork.SaveChanges();

                return new BaseResponse
                {
                    Name = "Сформирован запрос в базовый регистр номер " +
                           messageV6.CoordinateTaskDataMessage.Task.TaskNumber
                };
            }
            catch (Exception ex)
            {
                Logger.Error("Ошибка отправки в базовый регистр", ex);
                return new BaseResponse { HasError = true, ErrorMessage = ex.Message };
            }
        }

        /// <summary>
        ///     Запроса СНИЛС по ФИО в ЕТП
        /// </summary>
        internal BaseResponse AdditionallyGetSnils(BenefitCheckRequest model)
        {
            try
            {
                var exchangeBaseRegistryCode = WebConfigurationManager.AppSettings["exchangeBaseRegistryCode"];
                var requestNumber = GetServiceNumber(exchangeBaseRegistryCode);
                var documentType = UnitOfWork.GetById<DocumentType>(model.DocumentTypeId);
                var request = GetMessageSnilsByFio(model.FirstName, model.LastName,
                    model.MiddleName, model.DateOfBirth, model.Male,
                    DecodeDocumentType(documentType), model.DocumentSeria,
                    model.DocumentNumber, model.DocumentDateOfIssue ?? DateTime.Today,
                    model.DocumentSubjectIssue, model.Region, model.District, model.Settlement);
                var messageV6 = GetCoordinateMessageV6(request,
                    ((long)ExchangeBaseRegistryTypeEnum.SNILSByFio).ToString(), requestNumber + "/1",
                    requestNumber);

                UnitOfWork.AddEntity(new ExchangeBaseRegistry
                {
                    IsIncoming = false,
                    RequestText = Serialization.Serializer(messageV6),
                    OperationType = "SendTask",
                    RequestGuid = messageV6.CoordinateTaskDataMessage.Task.TaskId,
                    ServiceNumber = messageV6.CoordinateTaskDataMessage.Task.TaskNumber,
                    IsAddonRequest = true,
                    ExchangeBaseRegistryTypeId = (long)ExchangeBaseRegistryTypeEnum.SNILSByFio,
                    BirthDate = model.DateOfBirth,
                    SearchField =
                        $"{model.LastName?.ToLower().Trim()}|{model.FirstName?.ToLower().Trim()}|{model.MiddleName?.ToLower().Trim()}"
                });

                UnitOfWork.SaveChanges();
                return new BaseResponse
                {
                    Name = "Сформирован запрос в базовый регистр номер " +
                           messageV6.CoordinateTaskDataMessage.Task.TaskNumber
                };
            }
            catch (Exception ex)
            {
                Logger.Error("Ошибка отправки в базовый регистр запроса СНИЛС по ФИО", ex);
                return new BaseResponse { HasError = true, ErrorMessage = ex.Message };
            }
        }

        /// <summary>
        ///     Запроса паспорта по СНИЛС в ЕТП
        /// </summary>
        internal BaseResponse AdditionallyGetPassport(BenefitCheckRequest model)
        {
            try
            {
                var exchangeBaseRegistryCode = WebConfigurationManager.AppSettings["exchangeBaseRegistryCode"];
                var requestNumber = GetServiceNumber(exchangeBaseRegistryCode);
                var request = BrPassportBySNILSMessage(model.Snils);
                var messageV6 = GetCoordinateMessageV6(request,
                    ((long)ExchangeBaseRegistryTypeEnum.PassportDataBySNILS).ToString(), requestNumber + "/1",
                    requestNumber);

                UnitOfWork.AddEntity(new ExchangeBaseRegistry
                {
                    IsIncoming = false,
                    RequestText = Serialization.Serializer(messageV6),
                    OperationType = "SendTask",
                    RequestGuid = messageV6.CoordinateTaskDataMessage.Task.TaskId,
                    ServiceNumber = messageV6.CoordinateTaskDataMessage.Task.TaskNumber,
                    IsAddonRequest = true,
                    ExchangeBaseRegistryTypeId = (long)ExchangeBaseRegistryTypeEnum.PassportDataBySNILS,
                    BirthDate = model.DateOfBirth,
                    SearchField =
                        $"{model.LastName?.ToLower().Trim()}|{model.FirstName?.ToLower().Trim()}|{model.MiddleName?.ToLower().Trim()}"
                });

                UnitOfWork.SaveChanges();
                return new BaseResponse
                {
                    Name = "Сформирован запрос в базовый регистр номер " +
                           messageV6.CoordinateTaskDataMessage.Task.TaskNumber
                };
            }
            catch (Exception ex)
            {
                Logger.Error("Ошибка отправки в базовый регистр запроса СНИЛС по ФИО", ex);
                return new BaseResponse { HasError = true, ErrorMessage = ex.Message };
            }
        }

        /// <summary>
        ///     генерация сообщения (CustomAttributes)
        /// </summary>
        private string GetMessageSnilsByFio(string firstname, string lastname, string middlename,
            DateTime? birthdate, bool? isMale, int documentType, string documentSeries, string documentNumber,
            DateTime documentDateOfIssuer, string documentSubjectOfIssuer, string region, string district,
            string settlement)
        {
            if (Settings.Default.SnilsTestRequest)
            {
                return @"<ServiceProperties>
 <lastname>ИВАНОВ</lastname>
 <firstname>ИВАН</firstname>
 <middlename>ИВАНОВИЧ</middlename>
 <birthdate>1967-05-21T00:00:00</birthdate>
 <gendercode>1</gendercode>
 <placetype>ОСОБОЕ</placetype>
 <settlement>ЗАГОРСК</settlement>
 <district>ЛЕНИНСКИЙ</district>
 <region>МОСКОВСКАЯ ОБЛАСТЬ</region>
 <country>РФ</country>
 <doc_type>1</doc_type>
 <series>0005</series>
 <number>777777</number>
 <issuedate>1986-06-13T00:00:00</issuedate>
 <issuer>ОВД</issuer>
 <testmsg/>
</ServiceProperties>";
            }

            return
                $"<ServiceProperties><lastname>{lastname}</lastname><firstname>{firstname}</firstname><middlename>{middlename}</middlename><birthdate>{birthdate:yyyy-MM-dd}T00:00:00</birthdate><gendercode>{(isMale ?? false ? 1 : 2)}</gendercode> <placetype>ОСОБОЕ</placetype><settlement>{settlement}</settlement><district>{district}</district><region>{region}</region><doc_type>{documentType}</doc_type><series>{documentSeries}</series><number>{documentNumber}</number><issuedate>{documentDateOfIssuer:yyyy-MM-dd}T00:00:00</issuedate><issuer>{documentSubjectOfIssuer}</issuer></ServiceProperties>";
        }

        /// <summary>
        ///     проверка сопровождающего на снилс
        /// </summary>
        [HttpPost]
        [HttpGet]
        internal int CheckApplicantForSnils(string requestNumber, Applicant applicant, int count)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(applicant?.Snils))
                {
                    return count;
                }

                ResetCheckApplicantInBaseRegistry(applicant.Id, ExchangeBaseRegistryTypeEnum.Snils);
                ResetCheckApplicantInBaseRegistry(applicant.Id, ExchangeBaseRegistryTypeEnum.Snils2040);

                var request =
                    $@"<ServiceProperties><lastname>{applicant.LastName?.Trim()}</lastname><birthdate>{applicant.DateOfBirth?.ToString("yyyy-MM-dd")}T00:00:00Z</birthdate><firstname>{applicant.FirstName?.Trim()}</firstname>{(string.IsNullOrWhiteSpace(applicant.MiddleName) ? "" : $"<middlename>{applicant.MiddleName?.Trim()}</middlename>")}<snils>{applicant.Snils.Replace("-", "").Replace(" ", "")}</snils><gender>{(applicant.Male == true ? "1" : "2")}</gender></ServiceProperties>";

                if (Settings.Default.SnilsTestRequest)
                {
                    request = @"<ServiceProperties>
 <lastname>ПЕТИНА</lastname>
 <birthdate>1966-09-12T00:00:00Z</birthdate>
 <firstname>ЕЛЕНА</firstname>
 <middlename>ВЛАДИМИРОВНА</middlename>
 <snils>02773319862</snils>
 <gender>2</gender>
 <testmsg/>
</ServiceProperties>";
                }

                var messageV6 = GetCoordinateMessageV6(request,
                    ((long)ExchangeBaseRegistryTypeEnum.Snils2040).ToString(),
                    requestNumber + $"/{count++}", requestNumber);

                UnitOfWork.AddEntity(new ExchangeBaseRegistry
                {
                    IsIncoming = false,
                    RequestText = Serialization.Serializer(messageV6),
                    OperationType = "SendTask",
                    RequestGuid = messageV6.CoordinateTaskDataMessage.Task.TaskId,
                    ServiceNumber = messageV6.CoordinateTaskDataMessage.Task.TaskNumber,
                    Applicant = applicant,
                    ApplicantId = applicant.Id,
                    ExchangeBaseRegistryTypeId = (long)ExchangeBaseRegistryTypeEnum.Snils2040
                });

                UnitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                Logger.Error("Ошибка отправки в базовый регистр по СНИЛС", ex);
            }

            return count;
        }

        /// <summary>
        ///     проверка ребёнка на свидетельство о рождении ЕГР ЗАГС (11827)
        /// </summary>
        internal int CheckChildForRelationshipEGRZAGS(string requestNumber, Child child, int count)
        {
            try
            {
                if (string.IsNullOrEmpty(child?.Snils))
                {
                    return count;
                }

                ResetCheckChildInBaseRegistry(child.Id, ExchangeBaseRegistryTypeEnum.GetEGRZAGS);

                var request =
                    $@"<ServiceProperties xmlns="">
                        <base_code>01</base_code>
                        <quantity_doc>1</quantity_doc>
                        <rogdinflist>
                            <lastname>{child.LastName}</lastname>
                            <firstname>{child.FirstName}</firstname>
                            <middlename>{child.MiddleName}</middlename>
                            <birthdate>{child.DateOfBirth?.ToString("yyyy-MM-dd")}T00:00:00Z</birthdate>
                            <snils>{child.Snils}</snils>
                        </rogdinflist>
                    </ServiceProperties>";

                if (Settings.Default.SnilsTestRequest)
                {
                    request =
                        @"<ServiceProperties xmlns="">
               <base_code>01</base_code>
               <quantity_doc>1</quantity_doc>
               <rogdinflist>
                  <rogdinf>
                     <actnumber>1234</actnumber>
                     <actdate>1957-08-13T09:30:47Z</actdate>
                     <nameofregistrar>Отдел Государственной службы записи актов гражданского состояния Республики Ингушетия Джейрахского района</nameofregistrar>
                     <code_zags>R0600004</code_zags>
                     <member_type>1</member_type>
                     <lastname>Иванова</lastname>
                     <firstname>Мария</firstname>
                     <middlename>Петровна</middlename>
                     <birthdate>1957-08-13T09:30:47Z</birthdate>
                  </rogdinf>
               </rogdinflist>
            </ServiceProperties>";
                }

                var messageV6 = GetCoordinateMessageV6(request,
                    ((long)ExchangeBaseRegistryTypeEnum.GetEGRZAGS).ToString(),
                    requestNumber + $"/{count++}", requestNumber);

                UnitOfWork.AddEntity(new ExchangeBaseRegistry
                {
                    IsIncoming = false,
                    RequestText = Serialization.Serializer(messageV6),
                    OperationType = "SendTask",
                    RequestGuid = messageV6.CoordinateTaskDataMessage.Task.TaskId,
                    ServiceNumber = messageV6.CoordinateTaskDataMessage.Task.TaskNumber,
                    Child = child,
                    ChildId = child.Id,
                    ExchangeBaseRegistryTypeId = (long)ExchangeBaseRegistryTypeEnum.GetEGRZAGS
                });

                UnitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                Logger.Error("Ошибка отправки в базовый регистр по ЕГР ЗАГС", ex);
            }

            return count;
        }

        /// <summary>
        ///     проверка ребёнка на свидетельство о рождении СМЭВ
        /// </summary>
        internal int CheckChildForRelationshipSmev(string requestNumber, Child child, int count)
        {
            try
            {
                if (string.IsNullOrEmpty(child?.Snils))
                {
                    return count;
                }

                ResetCheckChildInBaseRegistry(child.Id, ExchangeBaseRegistryTypeEnum.RelationshipSmev);

                var request =
                    $@"<ServiceProperties>
 <form_response>2</form_response>
 <act>1</act>
 <snils>{child.Snils}</snils>
 <lastname>{child.LastName}</lastname>
 <firstname>{child.FirstName}</firstname>
 <middlename>{child.MiddleName}</middlename>
 <birthdate>{child.DateOfBirth?.ToString("yyyy-MM-dd")}T00:00:00Z</birthdate>
 <doc_type>{child.DocumentType?.BaseRegistryUid}</doc_type>
 <doc_name>{child.DocumentType?.Name}</doc_name>
 <seriesnumber>{child.DocumentSeria} {child.DocumentNumber}</seriesnumber>
</ServiceProperties>";

                if (Settings.Default.SnilsTestRequest)
                {
                    request =
                        @"<ServiceProperties>
 <doc_id>a</doc_id>
 <form_response>2</form_response>
 <act>1</act>
 <actnumber>a</actnumber>
 <actdate>2018-08-13T00:00:00Z</actdate>
 <nameofregistrar>a</nameofregistrar>
 <code_zags>R0000000</code_zags>
 <snils>000 000 000 00</snils>
 <inn_fl>010000000000</inn_fl>
 <lastname>Иванов</lastname>
 <firstname>Иван</firstname>
 <middlename>Иванович</middlename>
 <birthdate>2018-08-13T00:00:00Z</birthdate>
 <testmsg/>
</ServiceProperties>";
                }

                var messageV6 = GetCoordinateMessageV6(request,
                    ((long)ExchangeBaseRegistryTypeEnum.RelationshipSmev).ToString(),
                    requestNumber + $"/{count++}", requestNumber);

                UnitOfWork.AddEntity(new ExchangeBaseRegistry
                {
                    IsIncoming = false,
                    RequestText = Serialization.Serializer(messageV6),
                    OperationType = "SendTask",
                    RequestGuid = messageV6.CoordinateTaskDataMessage.Task.TaskId,
                    ServiceNumber = messageV6.CoordinateTaskDataMessage.Task.TaskNumber,
                    Child = child,
                    ChildId = child.Id,
                    ExchangeBaseRegistryTypeId = (long)ExchangeBaseRegistryTypeEnum.RelationshipSmev
                });

                UnitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                Logger.Error("Ошибка отправки в базовый регистр по свидетельство о рождении СМЭВ", ex);
            }

            return count;
        }

        /// <summary>
        ///     проверка ребёнка на СНИЛС
        /// </summary>
        internal int CheckChildForSnils(string requestNumber, long childId, int count)
        {
            try
            {
                var child = UnitOfWork.GetById<Child>(childId);
                if (string.IsNullOrEmpty(child?.Snils))
                {
                    return count;
                }

                ResetCheckChildInBaseRegistry(childId, ExchangeBaseRegistryTypeEnum.Snils);
                ResetCheckChildInBaseRegistry(childId, ExchangeBaseRegistryTypeEnum.Snils2040);

                var request =
                    $@"<ServiceProperties><lastname>{child.LastName?.Trim()}</lastname><birthdate>{child.DateOfBirth?.ToString("yyyy-MM-dd")}T00:00:00Z</birthdate><firstname>{child.FirstName?.Trim()}</firstname>{(string.IsNullOrWhiteSpace(child.MiddleName) ? "" : $"<middlename>{child.MiddleName?.Trim()}</middlename>")}<snils>{child.Snils.Replace("-", "").Replace(" ", "")}</snils><gender>{(child.Male ? "1" : "2")}</gender></ServiceProperties>";

                if (Settings.Default.SnilsTestRequest)
                {
                    request = @"<ServiceProperties>
 <lastname>ПЕТИНА</lastname>
 <birthdate>1966-09-12T00:00:00Z</birthdate>
 <firstname>ЕЛЕНА</firstname>
 <middlename>ВЛАДИМИРОВНА</middlename>
 <snils>02773319862</snils>
 <gender>2</gender>
 <testmsg/>
</ServiceProperties>";
                }

                var messageV6 = GetCoordinateMessageV6(request,
                    ((long)ExchangeBaseRegistryTypeEnum.Snils2040).ToString(),
                    requestNumber + $"/{count++}", requestNumber);

                UnitOfWork.AddEntity(new ExchangeBaseRegistry
                {
                    IsIncoming = false,
                    RequestText = Serialization.Serializer(messageV6),
                    OperationType = "SendTask",
                    RequestGuid = messageV6.CoordinateTaskDataMessage.Task.TaskId,
                    ServiceNumber = messageV6.CoordinateTaskDataMessage.Task.TaskNumber,
                    Child = child,
                    ChildId = childId,
                    ExchangeBaseRegistryTypeId = (long)ExchangeBaseRegistryTypeEnum.Snils2040
                });

                UnitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                Logger.Error("Ошибка отправки в базовый регистр по СНИЛС", ex);
            }

            return count;
        }

        /// <summary>
        ///     проверка ребенка в БР
        /// </summary>
        [HttpPost]
        [HttpGet]
        public int CheckChildInBaseRegistry(string requestNumber, long childId, int count)
        {
            try
            {
                var child = UnitOfWork.GetById<Child>(childId);
                if (child?.BenefitType == null || !child.DocumentTypeId.HasValue)
                {
                    return count;
                }

                ResetCheckChildInBaseRegistry(childId, ExchangeBaseRegistryTypeEnum.Benefit);

                var documentType = child.DocumentType ??
                                   UnitOfWork.GetById<DocumentType>(child.DocumentTypeId.Value);

                var request =
                    $@"<ServiceProperties>{(!string.IsNullOrEmpty(child.Snils) ? $"<snils>{child.Snils}</snils>" : string.Empty)}<birthdate>{child.DateOfBirth:yyyy-MM-dd}T00:00:00Z</birthdate><doctype>{
                        documentType.BaseRegistryUid}</doctype><firstname>{child.FirstName}</firstname><lastname>{child.LastName
                        }</lastname><middlename>{child.MiddleName}</middlename><passport>{child.DocumentNumber
                        }</passport><passport_serie>{child.DocumentSeria}</passport_serie></ServiceProperties>";

                var messageV6 = GetCoordinateMessageV6(request,
                    ((long)ExchangeBaseRegistryTypeEnum.Benefit).ToString(),
                    requestNumber + $"/{count++}", requestNumber);

                UnitOfWork.AddEntity(new ExchangeBaseRegistry
                {
                    IsIncoming = false,
                    RequestText = Serialization.Serializer(messageV6),
                    OperationType = "SendTask",
                    RequestGuid = messageV6.CoordinateTaskDataMessage.Task.TaskId,
                    ServiceNumber = messageV6.CoordinateTaskDataMessage.Task.TaskNumber,
                    Child = child,
                    ChildId = childId,
                    ExchangeBaseRegistryTypeId = (long)ExchangeBaseRegistryTypeEnum.Benefit
                });

                var documentTypeCert = child.DocumentTypeCertOfBirth ??
                                       UnitOfWork.GetById<DocumentType>(child.DocumentTypeCertOfBirthId);
                if (!string.IsNullOrWhiteSpace(child.DocumentNumberCertOfBirth) &&
                    !string.IsNullOrWhiteSpace(documentTypeCert?.BaseRegistryUid))
                {
                    var request2 =
                        $@"<ServiceProperties><birthdate>{child.DateOfBirth:yyyy-MM-dd}T00:00:00Z</birthdate><doctype>{
                            documentTypeCert.BaseRegistryUid}</doctype><firstname>{child.FirstName}</firstname><lastname>{
                                child.LastName}</lastname><middlename>{child.MiddleName}</middlename><passport>{child.DocumentNumberCertOfBirth
                                }</passport><passport_serie>{child.DocumentSeriaCertOfBirth}</passport_serie></ServiceProperties>";

                    var taskNumber = messageV6.CoordinateTaskDataMessage.Task.TaskNumber;
                    messageV6 = GetCoordinateMessageV6(request2,
                        ((long)ExchangeBaseRegistryTypeEnum.Benefit).ToString(),
                        requestNumber + $"/{count++}", requestNumber);

                    UnitOfWork.AddEntity(new ExchangeBaseRegistry
                    {
                        IsIncoming = false,
                        RequestText = Serialization.Serializer(messageV6),
                        OperationType = "SendTask",
                        RequestGuid = messageV6.CoordinateTaskDataMessage.Task.TaskId,
                        ServiceNumber = messageV6.CoordinateTaskDataMessage.Task.TaskNumber,
                        Child = child,
                        ChildId = childId,
                        ExchangeBaseRegistryTypeId = (long)ExchangeBaseRegistryTypeEnum.Benefit
                    });

                    child.BenefitApproveComment =
                        $@"В Базовый регистр направлен запросы {taskNumber}, {
                            messageV6.CoordinateTaskDataMessage.Task.TaskNumber}, {DateTime.Now.FormatEx()} для подтверждения льготы";
                }
                else
                {
                    child.BenefitApproveComment =
                        string.Format(@"В Базовый регистр направлен запрос {1}, {0} для подтверждения льготы",
                            messageV6.CoordinateTaskDataMessage.Task.TaskNumber,
                            DateTime.Now.FormatEx());
                }

                child.BenefitApprove = false;
                child.BenefitRequestDate = child.BenefitRequestDate ?? DateTime.Now;

                if (!child.BenefitApproveTypeId.HasValue ||
                    child.BenefitApproveTypeId == (long)BenefitApproveTypeEnum.NotApproved)
                {
                    child.BenefitApproveTypeId = (long)BenefitApproveTypeEnum.NotApprovedSendToBr;
                }

                UnitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                Logger.Error("Ошибка отправки в базовый регистр", ex);
            }

            return count;
        }

        /// <summary>
        ///     получить код услуги
        /// </summary>
        internal string GetServiceNumber(string exchangeBaseRegistryCode)
        {
            var year = (DateTime.Now.Year % 100).ToString(CultureInfo.InvariantCulture);
            var serviceNumber = "0000000" + UnitOfWork.GetNextNumber("br-" + exchangeBaseRegistryCode + year);
            serviceNumber =
                $"{exchangeBaseRegistryCode}-{serviceNumber.Substring(serviceNumber.Length - 7)}/{year}";
            return serviceNumber;
        }

        /// <summary>
        ///     проверка заявления в АС УР БР
        /// </summary>
        [HttpPost]
        [HttpGet]
        public void CheckRequestInBaseRegistryBenefit(long requestId)
        {
            var req = UnitOfWork.GetById<Request>(requestId);

            CheckRequestInBaseRegistryStatusSet(req);

            CheckRequestInBaseRegistryBenefitReq(req);
        }

        /// <summary>
        ///     проверка заявления в АС УР БР (исполнение)
        /// </summary>
        private void CheckRequestInBaseRegistryBenefitReq(Request req)
        {
            if (req?.Child == null)
            {
                return;
            }

            var count = UnitOfWork.GetSet<ExchangeBaseRegistry>().Count(v =>
                v.ApplicantId == req.ApplicantId || v.Applicant.RequestId == req.Id ||
                v.Child.RequestId == req.Id) + 1;

            var requestNumber = req.RequestNumber;
            if (req.TypeOfRestId == (long)TypeOfRestEnum.Compensation ||
                req.TypeOfRestId == (long)TypeOfRestEnum.CompensationYouthRest)
            {
                var exchangeBaseRegistryCode = WebConfigurationManager.AppSettings["exchangeBaseRegistryCode"];
                requestNumber = GetServiceNumber(exchangeBaseRegistryCode);
                count = 1;
            }

            var checkParentOnBenefit = false;

            foreach (var child in req.Child)
            {
                count = CheckChildInBaseRegistry(requestNumber, child.Id, count);

                if (child.BenefitTypeId == (long)BenefitTypeEnum.ParentsInvalid ||
                    (child.BenefitType ?? UnitOfWork.GetById<BenefitType>(child.BenefitTypeId)).SameBenefitId ==
                    (long)BenefitTypeEnum.ParentsInvalid)
                {
                    checkParentOnBenefit = true;
                }
            }

            if (checkParentOnBenefit)
            {
                if (req.BeneficiariesId == (long)BeneficiariesEnum.Applicant && req.ApplicantId.HasValue)
                {
                    CheckApplicantInBaseRegistry(requestNumber, new[] { req.ApplicantId.Value }, count);
                }
                else if (req.BeneficiariesId == (long)BeneficiariesEnum.SecondParent)
                {
                    var otherParent = req.Attendant?.FirstOrDefault();
                    if (otherParent != null)
                    {
                        CheckApplicantInBaseRegistry(requestNumber, new[] { otherParent.Id }, count);
                    }
                }
            }

            req.NeedSendForBenefit = false;
            UnitOfWork.SaveChanges();
        }

        /// <summary>
        ///     проверка заявления в АС УР БР
        /// </summary>
        [HttpPost]
        [HttpGet]
        public void CheckRequestInBaseRegistrySnils(long requestId)
        {
            var req = UnitOfWork.GetById<Request>(requestId);

            CheckRequestInBaseRegistryStatusSet(req);

            CheckRequestInBaseRegistrySnilsReq(req);
        }

        /// <summary>
        ///     проверка заявления в АС УР БР (исполнение)
        /// </summary>
        private void CheckRequestInBaseRegistrySnilsReq(Request req)
        {
            if (req?.Child == null)
            {
                return;
            }

            var count = UnitOfWork.GetSet<ExchangeBaseRegistry>().Count(v =>
                v.ApplicantId == req.ApplicantId || v.Applicant.RequestId == req.Id ||
                v.Child.RequestId == req.Id) + 1;

            var requestNumber = req.RequestNumber;
            if (req.TypeOfRestId == (long)TypeOfRestEnum.Compensation ||
                req.TypeOfRestId == (long)TypeOfRestEnum.CompensationYouthRest)
            {
                var exchangeBaseRegistryCode = WebConfigurationManager.AppSettings["exchangeBaseRegistryCode"];
                requestNumber = GetServiceNumber(exchangeBaseRegistryCode);
                count = 1;
            }

            // проверить на снилс если он заполнен
            if (!string.IsNullOrWhiteSpace(req.Applicant?.Snils))
            {
                count = CheckApplicantForSnils(requestNumber, req.Applicant, count);
            }

            // проверить на снилс если он заполнен
            foreach (var applicant in req.Attendant?.Where(a => !string.IsNullOrWhiteSpace(a.Snils)).ToList() ??
                                      new List<Applicant>())
            {
                count = CheckApplicantForSnils(requestNumber, applicant, count);
            }

            foreach (var child in req.Child)
            {
                count = CheckChildForSnils(requestNumber, child.Id, count);
            }

            req.NeedSendForSnils = false;
            UnitOfWork.SaveChanges();
        }

        /// <summary>
        ///     проверка заявления паспорта в АС УР БР
        /// </summary>
        [HttpPost]
        [HttpGet]
        public void CheckRequestInBaseRegistryPassport(long requestId)
        {
            var req = UnitOfWork.GetById<Request>(requestId);

            CheckRequestInBaseRegistryStatusSet(req);

            CheckRequestInBaseRegistryPassportReq(req);
        }

        /// <summary>
        ///     проверка заявления паспорта в АС УР БР (исполнение)
        /// </summary>
        private void CheckRequestInBaseRegistryPassportReq(Request req)
        {
            BrGetPassportDataBySnils(req);
            req.NeedSendForPassport = false;
            UnitOfWork.SaveChanges();
        }

        /// <summary>
        ///     отправка проверки по свидетельству о рождении
        /// </summary>
        [HttpPost]
        [HttpGet]
        public void CheckRequestInBaseRegistryRelatives(long requestId)
        {
            var req = UnitOfWork.GetById<Request>(requestId);

            CheckRequestInBaseRegistryStatusSet(req);

            CheckRequestInBaseRegistryRelativesReq(req);
        }

        /// <summary>
        ///     отправка проверки по свидетельству о рождении (исполнение)
        /// </summary>
        private void CheckRequestInBaseRegistryRelativesReq(Request req)
        {
            if (req?.Child == null)
            {
                return;
            }

            req.NeedSendToRelative = false;

            var count = UnitOfWork.GetSet<ExchangeBaseRegistry>().Count(v =>
                v.ApplicantId == req.ApplicantId || v.Applicant.RequestId == req.Id ||
                v.Child.RequestId == req.Id) + 1;
            var requestNumber = req.RequestNumber;
            if (req.TypeOfRestId == (long)TypeOfRestEnum.Compensation ||
                req.TypeOfRestId == (long)TypeOfRestEnum.CompensationYouthRest)
            {
                var exchangeBaseRegistryCode = WebConfigurationManager.AppSettings["exchangeBaseRegistryCode"];
                requestNumber = GetServiceNumber(exchangeBaseRegistryCode);
                count = 1;
            }

            if (req.SourceId == (long)SourceEnum.Mpgu &&
                req.TypeOfRestId != (long)TypeOfRestEnum.ChildRestFederalCamps)
            {
                foreach (var child in req.Child)
                {
                    //старый документ (3091)
                    //count = CheckChildForRelationshipSmev(requestNumber, child, count);

                    //новый документ (11827)
                    count = CheckChildForRelationshipEGRZAGS(requestNumber, child, count);
                }
            }

            UnitOfWork.SaveChanges();
        }

        /// <summary>
        ///     отправка проверки регистрации по паспорту
        /// </summary>
        [HttpPost]
        [HttpGet]
        public void CheckRequestInBaseRegistryRegistrationByPassport(long requestId)
        {
            var req = UnitOfWork.GetById<Request>(requestId);

            CheckRequestInBaseRegistryStatusSet(req);

            CheckRequestInBaseRegistryRegistrationByPassportReq(req);
        }

        /// <summary>
        ///     отправка проверки регистрации по паспорту (исполнение)
        /// </summary>
        private void CheckRequestInBaseRegistryRegistrationByPassportReq(Request req)
        {
            BrGetRegistrationDataByPassport(req);
            req.NeedSendForRegistrationByPassport = false;
            UnitOfWork.SaveChanges();
        }

        /// <summary>
        ///     Установка статуса 7704 при отправке запроса в БР (единая точка входа)
        /// </summary>
        /// <param name="req"></param>
        private void CheckRequestInBaseRegistryStatusSet(Request req)
        {
            if (req.IsFirstCompany)
            {
                if (UnitOfWork.GetSet<ExchangeUTS>().Any(ss =>
                    ss.RequestId == req.Id && !ss.Incoming && ss.ToState == (long)StatusEnum.OperatorCheck)) return;

                UnitOfWork.SendChangeStatusByEvent(req, RequestEventEnum.SendRequestBase);
            }
        }
    }
}
