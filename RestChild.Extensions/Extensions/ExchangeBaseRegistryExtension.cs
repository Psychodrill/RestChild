using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;
using RestChild.Comon;
using RestChild.Comon.Dto;
using RestChild.Comon.Enumeration;
using RestChild.Comon.Exchange.Cpmpk;
using RestChild.Comon.Exchange.EGRZagz;
using RestChild.Comon.Exchange.FGISFRI;
using RestChild.Comon.Exchange.Passport;
using RestChild.Comon.Exchange.PassportRegistration;
using RestChild.Comon.Exchange.Snils;
using RestChild.Comon.Exchange.Zagz;
using RestChild.Comon.Extensions;
using RestChild.Domain;
using RestChild.MPGUIntegration.V61;
using CoordinateStatusMessage = RestChild.Common.Service.ServiceReference.CoordinateStatusMessage;
using CoordinateTaskMessage = RestChild.Common.Service.ServiceReference.CoordinateTaskMessage;
using ErrorMessage = RestChild.Common.Service.ServiceReference.ErrorMessage;

namespace RestChild.Extensions.Extensions
{
    /// <summary>
    ///     экстеншен для парсенга ответа из БР
    /// </summary>
    public static class ExchangeBaseRegistryExtension
    {
        /// <summary>
        ///     сериализатор
        /// </summary>
        private static readonly XmlSerializer serializer = new XmlSerializer(typeof(CoordinateSendTaskStatusesMessage),
            "http://asguf.mos.ru/rkis_gu/coordinate/v6_1/");

        private static IEnumerable<ResidentPreferentialCategories> ProcessCheckBaseRegistrys(XmlElement element,
            string[] benefitCodes, string[] poors = null)
        {
            // ReSharper disable once PossibleNullReferenceException
            var ns = new XmlNamespaceManager(element.OwnerDocument.NameTable);
            ns.AddNamespace("ns", "http://asur/dszn/");
            ns.AddNamespace("ns2", "uri:citsp-br-social-support");

            var elements = element.SelectNodes("//ns:DetailsSearchResidentPreferentialCategories", ns);

            var res = new List<ResidentPreferentialCategories>();
            if (elements != null)
            {
                res.AddRange(elements.Cast<XmlElement>().Select(x => ProcessCheckBaseRegistry(x, benefitCodes, poors))
                    .Where(val => val != null));
            }

            return res;
        }

        private static IEnumerable<PaymentCheckResult> ProcessPayments(XmlElement element, string[] documentCodes)
        {
            // ReSharper disable once PossibleNullReferenceException
            var ns = new XmlNamespaceManager(element.OwnerDocument.NameTable);
            ns.AddNamespace("ns", "http://asur/dszn/");
            ns.AddNamespace("ns2", "uri:citsp-br-social-support");

            var elements = element.SelectNodes("//ns:DetailsSearchResidentProvidedSocialSupports", ns);

            var res = new List<PaymentCheckResult>();
            if (elements != null)
            {
                res.AddRange(elements.Cast<XmlElement>().Select(x => ProcessCheckPayments(x, documentCodes))
                    .Where(val => val != null));
            }

            return res;
        }

        /// <summary>
        ///     разбор сведений о свидетельстве о рождения
        /// </summary>
        private static RelationshipCheckResult ProcessRelationshipCheckResult(XmlElement element)
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            var ns = new XmlNamespaceManager(element?.OwnerDocument?.NameTable);
            ns.AddNamespace("ns", "http://nakhodka.ru/documentsservice/types");

            var res = new RelationshipCheckResult
            {
                // ReSharper disable once PossibleNullReferenceException
                ChildBirthDate = element.SelectSingleNode(".//ns:Child/ns:BirthDate", ns)?.InnerXml.XmlToDateTime(),
                ChildLastName = element.SelectSingleNode(".//ns:Child/ns:LastName", ns)?.InnerXml,
                ChildFirstName = element.SelectSingleNode(".//ns:Child/ns:FirstName", ns)?.InnerXml,
                ChildBirthPlace = element.SelectSingleNode(".//ns:Child/ns:BirthPlace", ns)?.InnerXml,
                ChildPatronymic = element.SelectSingleNode(".//ns:Child/ns:Patronymic", ns)?.InnerXml,

                MotherBirthDate = element.SelectSingleNode(".//ns:Mother/ns:BirthDate", ns)?.InnerXml.XmlToDateTime(),
                MotherLastName = element.SelectSingleNode(".//ns:Mother/ns:LastName", ns)?.InnerXml,
                MotherPatronymic = element.SelectSingleNode(".//ns:Mother/ns:Patronymic", ns)?.InnerXml,
                MotherCitizenship = element.SelectSingleNode(".//ns:Mother/ns:Citizenship", ns)?.InnerXml,
                MotherFirstName = element.SelectSingleNode(".//ns:Mother/ns:FirstName", ns)?.InnerXml,

                FatherBirthDate = element.SelectSingleNode(".//ns:Father/ns:BirthDate", ns)?.InnerXml.XmlToDateTime(),
                FatherLastName = element.SelectSingleNode(".//ns:Father/ns:LastName", ns)?.InnerXml,
                FatherPatronymic = element.SelectSingleNode(".//ns:Father/ns:Patronymic", ns)?.InnerXml,
                FatherCitizenship = element.SelectSingleNode(".//ns:Father/ns:Citizenship", ns)?.InnerXml,
                FatherFirstName = element.SelectSingleNode(".//ns:Father/ns:FirstName", ns)?.InnerXml,

                ActRequisitesActDate = element.SelectSingleNode(".//ns:ActRequisites/ns:ActDate", ns)?.InnerXml
                    .XmlToDateTime(),
                ActRequisitesActNumber = element.SelectSingleNode(".//ns:ActRequisites/ns:ActNumber", ns)?.InnerXml,
                ActRequisitesNameOfRegistrar =
                    element.SelectSingleNode(".//ns:ActRequisites/ns:NameOfRegistrar", ns)?.InnerXml,
                CertlssueDate = element.SelectSingleNode(".//ns:CertRequisites/ns:CertDate", ns)?.InnerXml
                    .XmlToDateTime(),
                CertNumber = element.SelectSingleNode(".//ns:CertRequisites/ns:CertNumber", ns)?.InnerXml
                    ?.Replace(" ", ""),
                CertSeries = element.SelectSingleNode(".//ns:CertRequisites/ns:CertSeries", ns)?.InnerXml
                    ?.Replace(" ", "")
            };

            return res;
        }

        private static ResidentPreferentialCategories ProcessCheckBaseRegistry(XmlElement element,
            string[] benefitCodes, string[] poors = null)
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            var ns = new XmlNamespaceManager(element?.OwnerDocument?.NameTable);
            ns.AddNamespace("ns", "http://asur/dszn/");
            ns.AddNamespace("ns2", "uri:citsp-br-social-support");

            var res = new ResidentPreferentialCategories
            {
                // ReSharper disable once PossibleNullReferenceException
                EndDate = element.SelectSingleNode(".//ns:EndDate", ns).NullSafe(n => n.InnerXml).XmlToDateTime(),
                // имя льготы
                Preferentical = element.SelectSingleNode(".//ns:NamePreferentialCategory", ns)
                    .NullSafe(n => n.InnerText),
                // код льготы
                PreferenticalCode = element.SelectSingleNode(".//ns:PreferentialCategoryId", ns)
                    .NullSafe(n => n.InnerText),
                StartDate = element.SelectSingleNode(".//ns:StartDate", ns).NullSafe(n => n.InnerXml).XmlToDateTime(),
                StartReasonName = element.SelectSingleNode(".//ns:StartDateReasons/ns:ReasonDocument/ns2:Name", ns)
                    .NullSafe(n => n.InnerText),
                StartReasonSeries = element.SelectSingleNode(".//ns:StartDateReasons/ns:ReasonDocument/ns2:Series", ns)
                    .NullSafe(n => n.InnerText),
                StartReasonNumber = element.SelectSingleNode(".//ns:StartDateReasons/ns:ReasonDocument/ns2:Number", ns)
                    .NullSafe(n => n.InnerText),
                StartReasonPlaceOfIssue = element
                    .SelectSingleNode(".//ns:StartDateReasons/ns:ReasonDocument/ns2:PlaceOfIssue", ns)
                    .NullSafe(n => n.InnerText),
                StartReasonDateOfIssue = element
                    .SelectSingleNode(".//ns:StartDateReasons/ns:ReasonDocument/ns2:DateOfIssue", ns)
                    .NullSafe(n => n.InnerText).XmlToDateTime(),
                EndReasonName = element.SelectSingleNode(".//ns:EndDateReasons/ns:ReasonDocument/ns2:Name", ns)
                    .NullSafe(n => n.InnerText),
                EndReasonSeries = element.SelectSingleNode(".//ns:EndDateReasons/ns:ReasonDocument/ns2:Series", ns)
                    .NullSafe(n => n.InnerText),
                EndReasonNumber = element.SelectSingleNode(".//ns:EndDateReasons/ns:ReasonDocument/ns2:Number", ns)
                    .NullSafe(n => n.InnerText),
                EndReasonPlaceOfIssue = element
                    .SelectSingleNode(".//ns:EndDateReasons/ns:ReasonDocument/ns2:PlaceOfIssue", ns)
                    .NullSafe(n => n.InnerText),
                EndReasonDateOfIssue = element
                    .SelectSingleNode(".//ns:EndDateReasons/ns:ReasonDocument/ns2:DateOfIssue", ns)
                    .NullSafe(n => n.InnerText).XmlToDateTime()
            };

            res.CanUse = benefitCodes?.Contains(res.PreferenticalCode) ?? false;
            res.LowIncome = poors?.Contains(res.PreferenticalCode) ?? false;

            return res;
        }

        private static PaymentCheckResultVolume ProcessCheckPaymentVolume(XmlElement element)
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            var ns = new XmlNamespaceManager(element?.OwnerDocument?.NameTable);
            ns.AddNamespace("ns", "http://asur/dszn/");
            ns.AddNamespace("ns2", "uri:citsp-br-social-support");

            var res = new PaymentCheckResultVolume
            {
                // ReSharper disable once PossibleNullReferenceException
                AssignmentDate = element.SelectSingleNode(".//ns:AssignmentDate", ns)?.InnerText.XmlToDateTime(),
                // имя льготы
                AssignmentDateType = element.SelectSingleNode(".//ns:AssignmentDateType", ns)?.InnerText,
                Volume = element.SelectSingleNode(".//ns:Volume", ns)?.InnerText.DecimalParse()
            };

            return res;
        }

        /// <summary>
        ///     проверка по новому документу
        /// </summary>
        private static SnilsCheckResult ProcessCheckSnils2040(XmlElement element)
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            var ns = new XmlNamespaceManager(element?.OwnerDocument?.NameTable);
            ns.AddNamespace("ns", "http://kvs.pfr.com/snils-validation/1.0.1");

            // ReSharper disable once PossibleNullReferenceException
            var resultCheck = element.SelectSingleNode(".//ns:Result", ns)?.InnerText.ToLower() == "true";
            var res = new SnilsCheckResult
            {
                CheckResult = resultCheck ? "СНИЛС прошел валидацию" : "СНИЛС не прошёл валидацию",
                CanUse = resultCheck
            };

            return res;
        }

        private static SnilsCheckResult ProcessCheckSnils(XmlElement element)
        {
            var res = new SnilsCheckResult
            {
                // ReSharper disable once PossibleNullReferenceException
                CheckResult = element.SelectSingleNode(".//validation")?.InnerText
            };

            res.CanUse = res.CheckResult == "СНИЛС прошел валидацию";

            return res;
        }

        private static PaymentCheckResult ProcessCheckPayments(XmlElement element, string[] documentCodes)
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            var ns = new XmlNamespaceManager(element?.OwnerDocument?.NameTable);
            ns.AddNamespace("ns", "http://asur/dszn/");
            ns.AddNamespace("ns2", "uri:citsp-br-social-support");

            var res = new PaymentCheckResult
            {
                // ReSharper disable once PossibleNullReferenceException
                SocialSupportId = element.SelectSingleNode(".//ns:SocialSupportId", ns)?.InnerText,
                // имя льготы
                SocialSupportName = element.SelectSingleNode(".//ns:SocialSupportName", ns)?.InnerText,
                Volumes = element.SelectNodes(".//ns:SocialSupportVolume", ns)?.Cast<XmlElement>()
                    .Select(ProcessCheckPaymentVolume).ToArray() ?? new PaymentCheckResultVolume[0]
            };

            res.CanUse = documentCodes?.Contains(res.SocialSupportId) ?? false;

            return res;
        }

        /// <summary>
        ///     вид льготы
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static string GetNameCheck(this ExchangeBaseRegistryTypeEnum self)
        {
            switch (self)
            {
                case ExchangeBaseRegistryTypeEnum.Benefit:
                    return "проверку льгот";
                case ExchangeBaseRegistryTypeEnum.Relationship:
                    return "проверку родства";
                case ExchangeBaseRegistryTypeEnum.SNILSByFio:
                    return "получение СНИЛС";
                case ExchangeBaseRegistryTypeEnum.PassportDataBySNILS:
                    return "получение паспортного досье";
                case ExchangeBaseRegistryTypeEnum.GetEGRZAGS:
                case ExchangeBaseRegistryTypeEnum.RelationshipSmev:
                    return "проверку свидетельства о рождении";
                case ExchangeBaseRegistryTypeEnum.Snils:
                case ExchangeBaseRegistryTypeEnum.Snils2040:
                    return "проверку СНИЛС";
                case ExchangeBaseRegistryTypeEnum.Payments:
                    return "проверку получения ежемесячного пособия";
                case ExchangeBaseRegistryTypeEnum.CpmpkExchange:
                    return "проверку на наличие заключения ЦПМПК";
                case ExchangeBaseRegistryTypeEnum.PassportRegistration:
                    return "проверку адреса регистрации";
                case ExchangeBaseRegistryTypeEnum.GetPassportRegistration:
                    return "получение адреса регистрации";
                case ExchangeBaseRegistryTypeEnum.AisoLegalRepresentationCheck:
                    return "проверку законного представительства";
                case ExchangeBaseRegistryTypeEnum.FRIExchange:
                    return "проверку сведений ФРИ";
                case ExchangeBaseRegistryTypeEnum.GetFGISFRI:
                    return "проверку выписки сведеней об инвалиде";
            }

            return "-";
        }

        /// <summary>
        ///     разбор результата обмена с БР
        /// </summary>
        /// <param name="self">строка из БР</param>
        /// <param name="benefitCodes">допустимые коды льгот</param>
        /// <param name="documentCodes">допустимые коды документов</param>
        /// <param name="poors">коды для малообеспеченных</param>
        /// <returns></returns>
        public static BaseRegistryCheckResult Parse(this ExchangeBaseRegistry self, string[] benefitCodes = null,
            string[] documentCodes = null, string[] poors = null)
        {
            if (self == null || self.NotActual)
            {
                return null;
            }

            BenefitCheckRequest request = null;

            if (self.ExchangeBaseRegistryTypeId == (long) ExchangeBaseRegistryTypeEnum.CpmpkExchange)
            {
                request = new BenefitCheckRequest
                {
                    LastName = self.Child?.LastName,
                    FirstName = self.Child?.FirstName,
                    MiddleName = self.Child?.MiddleName,
                    DateOfBirth = self.Child?.DateOfBirth,
                    CpmpkResponse = JsonConvert.DeserializeObject<CpmpkResponseDto>(self.ResponseText)
                };
            }
            else if (self.ExchangeBaseRegistryTypeId ==
                     (long) ExchangeBaseRegistryTypeEnum.AisoLegalRepresentationCheck)
            {
                request = new BenefitCheckRequest
                {
                    LastName = self.Child?.LastName,
                    FirstName = self.Child?.FirstName,
                    MiddleName = self.Child?.MiddleName,
                    DateOfBirth = self.Child?.DateOfBirth,
                };
            }
            else if (self.ExchangeBaseRegistryTypeId == (long) ExchangeBaseRegistryTypeEnum.PassportRegistration ||
                     self.ExchangeBaseRegistryTypeId == (long) ExchangeBaseRegistryTypeEnum.GetPassportRegistration)
            {
                request = new BenefitCheckRequest
                {
                    LastName = self.Child?.LastName ?? self.Applicant?.LastName,
                    FirstName = self.Child?.FirstName ?? self.Applicant?.FirstName,
                    MiddleName = self.Child?.MiddleName ?? self.Applicant?.MiddleName,
                    DateOfBirth = self.Child?.DateOfBirth ?? self.Applicant?.DateOfBirth,
                };
            }
            else if (
                (self.ExchangeBaseRegistryTypeId == (long) ExchangeBaseRegistryTypeEnum.Benefit ||
                 self.ExchangeBaseRegistryTypeId == (long) ExchangeBaseRegistryTypeEnum.SNILSByFio ||
                 self.ExchangeBaseRegistryTypeId == (long) ExchangeBaseRegistryTypeEnum.RelationshipSmev) &&
                !string.IsNullOrWhiteSpace(self.RequestText))
            {
                CoordinateTaskMessage param = null;
                MPGUIntegration.V61.CoordinateTaskMessage paramV6 = null;
                if (self.RequestText.Contains("http://asguf.mos.ru/rkis_gu/coordinate/v6_1"))
                {
                    paramV6 = Serialization.Deserialize<MPGUIntegration.V61.CoordinateTaskMessage>(self.RequestText);
                }
                else
                {
                    param = Serialization.Deserialize<CoordinateTaskMessage>(self.RequestText);
                }

                var element = param?.TaskMessage?.Data?.Parameter ??
                              paramV6?.CoordinateTaskDataMessage?.Data?.Parameter;
                if (element != null)
                {
                    request = new BenefitCheckRequest
                    {
                        LastName = element.SelectSingleNode("//lastname")?.InnerText,
                        FirstName = element.SelectSingleNode("//firstname")?.InnerText,
                        MiddleName = element.SelectSingleNode("//middlename")?.InnerText,
                        DocumentSeria = element.SelectSingleNode("//passport_serie")?.InnerText,
                        DocumentNumber = element.SelectSingleNode("//passport")?.InnerText,
                        Snils = element.SelectSingleNode("//snils")?.InnerText,
                        DateOfBirth = element.SelectSingleNode("//birthdate")?.InnerText.XmlToDateTime()
                    };
                }
            }

            if (self.ExchangeBaseRegistryTypeId == (long) ExchangeBaseRegistryTypeEnum.PassportDataBySNILS &&
                !string.IsNullOrWhiteSpace(self.SearchField))
            {
                var names = self.SearchField.Split('|');
                request = new BenefitCheckRequest
                {
                    LastName = names[0],
                    FirstName = names[1],
                    MiddleName = names[2]
                };
            }

            var res = new BaseRegistryCheckResult
            {
                Approved = self.Success,
                Type =
                    self.ExchangeBaseRegistryTypeId.HasValue
                        ? (ExchangeBaseRegistryTypeEnum) self.ExchangeBaseRegistryTypeId
                        : ExchangeBaseRegistryTypeEnum.Benefit,
                RequestNumber = self.ServiceNumber,
                ResultAbsent = string.IsNullOrWhiteSpace(self.ResponseText),
                RequestDate = self.SendDate,
                Child = self.Child,
                Applicant = self.Applicant,
                BenefitCheckRequest = request,
            };

            if (res.Type == ExchangeBaseRegistryTypeEnum.AisoLegalRepresentationCheck)
            {
                res.AisoLegalRepresentationCheck = self.ResponseText;
            }

            if (res.ResultAbsent ||
                self.ExchangeBaseRegistryTypeId == (long) ExchangeBaseRegistryTypeEnum.CpmpkExchange ||
                self.ExchangeBaseRegistryTypeId == (long) ExchangeBaseRegistryTypeEnum.AisoLegalRepresentationCheck)
            {
                return res;
            }

            CoordinateSendTaskStatusesMessage responseMessageV6 = null;
            CoordinateStatusMessage responseMessage = null;
            ErrorMessage errorMessage = null;

            try
            {
                if (self.RequestText?.Contains("http://asguf.mos.ru/rkis_gu/coordinate/v6_1") ?? false)
                {
                    responseMessageV6 =
                        Serialization.Deserialize<CoordinateSendTaskStatusesMessage>(self.ResponseText, serializer);
                }
                else
                {
                    responseMessage = Serialization.Deserialize<CoordinateStatusMessage>(self.ResponseText);
                }
            }
            catch
            {
                errorMessage = Serialization.Deserialize<ErrorMessage>(self.ResponseText);
            }

            if (errorMessage != null)
            {
                res.ResultAbsent = true;
                res.Note = errorMessage.Error?.ErrorText;
            }
            else
            {
                res.ResultAbsent = false;
                res.Approved = responseMessage?.StatusMessage?.Result?.ResultCode == "1" ||
                               responseMessageV6?.CoordinateTaskStatusDataMessage?.Result?.ResultCode == 1;

                res.Status = responseMessage?.StatusMessage?.StatusCode ??
                             responseMessageV6?.CoordinateTaskStatusDataMessage?.Status?.StatusCode ?? 0;
                if (!(res.Approved ?? false))
                {
                    res.Note = responseMessage?.StatusMessage?.Note ??
                               responseMessageV6?.CoordinateTaskStatusDataMessage?.StatusNote;

                    if (res.Type == ExchangeBaseRegistryTypeEnum.Benefit && poors != null && poors.Any())
                    {
                        res.ApprovedLowIncome = false;
                    }

                    return res;
                }

                var resultData = new List<XmlElement>();

                if (responseMessage != null)
                {
                    resultData.AddRange(responseMessage?.StatusMessage?.Documents?.Select(id => id.CustomAttributes)
                        .Where(s => s != null).ToList() ?? new List<XmlElement>());
                }
                else
                {
                    var data = responseMessageV6?.CoordinateTaskStatusDataMessage?.Result?.XmlView;
                    if (data != null)
                    {
                        resultData.Add(data);
                    }
                }

                if (res.Type == ExchangeBaseRegistryTypeEnum.Benefit)
                {
                    res.BenefitCheckResult =
                        resultData
                            .SelectMany(s => ProcessCheckBaseRegistrys(s, benefitCodes, poors))
                            .ToList()
                            .ToList() ?? new List<ResidentPreferentialCategories>();

                    res.Approved = benefitCodes != null && benefitCodes.Any()
                        ? res.BenefitCheckResult.Any(b => b.CanUse)
                        : (bool?) null;
                    res.ApprovedLowIncome = poors != null && poors.Any()
                        ? res.BenefitCheckResult.Any(b => b.LowIncome)
                        : (bool?) null;
                }

                if (res.Type == ExchangeBaseRegistryTypeEnum.Relationship)
                {
                    res.RelationshipCheckResults = resultData
                        .Select(ProcessRelationshipCheckResult)
                        .ToList()
                        .ToList() ?? new List<RelationshipCheckResult>();
                }

                if (res.Type == ExchangeBaseRegistryTypeEnum.Snils2040)
                {
                    res.SnilsCheckResult = resultData
                        .Select(ProcessCheckSnils2040).FirstOrDefault();

                    res.Approved = res.SnilsCheckResult?.CanUse ?? false;
                }

                if (res.Type == ExchangeBaseRegistryTypeEnum.Snils)
                {
                    res.SnilsCheckResult = resultData
                        .Select(ProcessCheckSnils).FirstOrDefault();

                    res.Approved = res.SnilsCheckResult?.CanUse ?? false;
                }

                if (res.Type == ExchangeBaseRegistryTypeEnum.PassportRegistration)
                {
                    res.PassportRegistrationResponse = resultData
                        .Select(r => Serialization.Deserialize<Registration>(r.OuterXml)).FirstOrDefault();

                    res.Approved = res.PassportRegistrationResponse != null &&
                                   res.PassportRegistrationResponse?.DocumentStatus?.ToLower() == "y";
                }

                if (res.Type == ExchangeBaseRegistryTypeEnum.GetPassportRegistration)
                {
                    res.PassportRegistrationResponse = resultData
                        .Select(r => Serialization.Deserialize<Registration>(r.OuterXml)).FirstOrDefault();

                    res.Approved = res.PassportRegistrationResponse != null &&
                                   (res.PassportRegistrationResponse?.RegistrationType == "1" ||
                                    res.PassportRegistrationResponse?.RegistrationType == "3") &&
                                   res.PassportRegistrationResponse?.LocationRegion?.StartsWith("45") == true;
                }

                if (res.Type == ExchangeBaseRegistryTypeEnum.Payments)
                {
                    res.PaymentCheckResults = resultData
                        .SelectMany(s => ProcessPayments(s, documentCodes))
                        .ToList()
                        .ToList() ?? new List<PaymentCheckResult>();

                    res.Approved = res.PaymentCheckResults?.Any(b => b.CanUse) ?? false;
                }

                if (res.Type == ExchangeBaseRegistryTypeEnum.PassportDataBySNILS)
                {
                    res.Approved = false;
                    var xmlData = resultData.FirstOrDefault()?.OuterXml;
                    if (!string.IsNullOrWhiteSpace(xmlData))
                    {
                        res.Passport = Serialization.Deserialize<OutPassportType>(xmlData);
                        if (res.BenefitCheckRequest != null)
                        {
                            res.BenefitCheckRequest.Snils = res.Passport.SNILS;
                            res.BenefitCheckRequest.DateOfBirth = res.Passport.BirthDate.XmlToDateTime();
                            res.BenefitCheckRequest.LastName = res.Passport.LastName;
                            res.BenefitCheckRequest.FirstName = res.Passport.FirstName;
                            res.BenefitCheckRequest.MiddleName = res.Passport.Patronymic;
                        }

                        var documents = (res.Passport.Documents ?? new DocumentFMSType[0])
                            .Where(d => d.CodeDocument == "01" && d.StatusCode == "300").ToArray();
                        if (res.Child is Child child)
                        {
                            res.Approved = documents.Any(d =>
                                d.Number == child.DocumentNumber && d.Series == child.DocumentSeria);
                        }

                        if (res.Applicant is Applicant applicant)
                        {
                            res.Approved = documents.Any(d =>
                                d.Number == applicant.DocumentNumber && d.Series == applicant.DocumentSeria);
                        }
                    }
                }

                //проверка на родство по СМЭВ
                if (res.Type == ExchangeBaseRegistryTypeEnum.RelationshipSmev)
                {
                    var xmlData = resultData.FirstOrDefault()?.OuterXml;
                    if (!string.IsNullOrWhiteSpace(xmlData))
                    {
                        res.SmevZagzResponse = Serialization.Deserialize<informResponse>(xmlData);
                        if (res.Child is Child child)
                        {
                            //если есть ребенок то это заявление и по дефолту не подтверждено
                            res.Approved = false;
                            var requestChild = child.Request;

                            // если есть доверенность то дальше можно не проверять
                            if (requestChild.AgentApplicant == true
                                || requestChild.Attendant.Any(a => a.IsProxy))
                            {
                                return res;
                            }

                            // получили ФИО отца и матери
                            var fatherName = res.SmevZagzResponse.СведРегРожд?.ПрдСведРег?.СведОтец?.ФИО?.GetFio()
                                ?.ToLower();
                            var motherName = res.SmevZagzResponse.СведРегРожд?.ПрдСведРег?.СведМать?.ФИО?.GetFio()
                                ?.ToLower();

                            // фио заявителя
                            var applicantName =
                                $"{requestChild.Applicant.LastName} {requestChild.Applicant.FirstName} {requestChild.Applicant.MiddleName}"
                                    .Trim().ToLower();
                            if (applicantName != fatherName && applicantName != motherName)
                            {
                                //если заявитель не папа и не мама то не подтвердили
                                return res;
                            }

                            foreach (var attendant in requestChild.Attendant.ToList())
                            {
                                // фио сопровождающего
                                var attendantName = $"{attendant.LastName} {attendant.FirstName} {attendant.MiddleName}"
                                    .Trim().ToLower();
                                //если сопровождающие не папа и не мама то не подтвердили
                                if (attendantName != fatherName && attendantName != motherName)
                                {
                                    return res;
                                }
                            }

                            if (child.DocumentTypeId == (long) DocumentTypeEnum.CertOfBirth)
                            {
                                //если нашли документ удостоверяющий личность то не подтвердили
                                res.Approved = res.SmevZagzResponse.СведРегРожд?.СвидетРожд?.Any(d =>
                                    d.СерияСвидет?.Trim().ToLower() == child.DocumentSeria?.Trim().ToLower()
                                    && d.НомерСвидет?.Trim().ToLower() == child.DocumentNumber?.Trim().ToLower());
                            }
                        }
                    }
                }

                //проверка на родство по СМЭВ v3
                if (res.Type == ExchangeBaseRegistryTypeEnum.GetEGRZAGS)
                {
                    var xmlData = resultData.FirstOrDefault()?.OuterXml;
                    if (!string.IsNullOrWhiteSpace(xmlData))
                    {
                        res.EGRZagzResponse = Serialization.Deserialize<ROGDINFResponse>(xmlData);
                        if (res.Child is Child child)
                        {
                            //если есть ребенок то это заявление и по дефолту не подтверждено
                            res.Approved = true;
                            var requestChild = child.Request;

                            // если есть доверенность то дальше можно не проверять
                            if (requestChild.AgentApplicant == true
                                || requestChild.Attendant.Any(a => a.IsProxy))
                            {
                                return res;
                            }

                            // получили ФИО отца и матери
                            var parents = res.EGRZagzResponse.СведОтветАГС[0]?.СведРегРожд[0]?.ПрдСведРег;
                            var father = parents?.Item2?.GetFather();
                            var mother = parents?.Item1?.GetMother();

                            var fatherName = father?.ФИО?.GetFio().ToLower();
                            var motherName = mother?.ФИО?.GetFio().ToLower();

                            //// фио заявителя
                            //var applicantName =
                            //    $"{requestChild.Applicant.LastName} {requestChild.Applicant.FirstName} {requestChild.Applicant.MiddleName}"
                            //        .Trim().ToLower();
                            //if (applicantName != fatherName && applicantName != motherName)
                            //{
                            //    //если заявитель не папа и не мама то не подтвердили
                            //    return res;
                            //}

                            //foreach (var attendant in requestChild.Attendant.ToList())
                            //{
                            //    // фио сопровождающего
                            //    var attendantName = $"{attendant.LastName} {attendant.FirstName} {attendant.MiddleName}"
                            //        .Trim().ToLower();
                            //    //если сопровождающие не папа и не мама то не подтвердили
                            //    if (attendantName != fatherName && attendantName != motherName)
                            //    {
                            //        return res;
                            //    }
                            //}

                            if (child.DocumentTypeId == (long)DocumentTypeEnum.CertOfBirth)
                            {
                                // если нашли документ удостоверяющий личность то не подтвердили
                                res.Approved = res.EGRZagzResponse.СведОтветАГС[0]?.СведРегРожд[0]?.СвидетРожд.Any(d =>
                                   ((string)d.Item)?.Trim().ToLower() == child.DocumentSeria?.Trim().ToLower()
                                   && ((string)d.Item1)?.Trim().ToLower() == child.DocumentNumber?.Trim().ToLower());
                            }
                        }
                    }
                }

                //получение СНИЛС по данным
                if (res.Type == ExchangeBaseRegistryTypeEnum.SNILSByFio)
                {
                    var xmlData = resultData.FirstOrDefault()?.OuterXml;
                    if (!string.IsNullOrWhiteSpace(xmlData))
                    {
                        res.SnilsInfo = Serialization.Deserialize<SnilsByAdditionalDataResponse>(xmlData);
                    }
                }

                // проверка в ФРИ
                if (res.Type == ExchangeBaseRegistryTypeEnum.FRIExchange)
                {
                    var xmlData = resultData.FirstOrDefault()?.OuterXml;
                    if (!string.IsNullOrWhiteSpace(xmlData))
                    {
                        res.FRIResponse = Serialization.Deserialize<DisabilityExtractResponse>(xmlData);
                    }
                }

                //Получение выписки сведеней об инвалиде
                //TODO
                if (res.Type == ExchangeBaseRegistryTypeEnum.GetFGISFRI)
                {
                    var xmlData = resultData.FirstOrDefault()?.OuterXml;
                    if (!string.IsNullOrWhiteSpace(xmlData))
                    {
                        var FGISFRIResponse = Serialization.Deserialize<ExtractionInvalidDataResponse>(xmlData);
                        res.FGISFRIResponse = FGISFRIResponse;

                    }
                }

            }
            return res;
        }
    }
}
