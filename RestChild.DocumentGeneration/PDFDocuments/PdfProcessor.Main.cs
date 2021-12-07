using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Wordprocessing;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using Newtonsoft.Json;
using RestChild.Comon;
using RestChild.Comon.Dto;
using RestChild.Comon.Enumeration;
using RestChild.Comon.OpenXmlExtensions;
using RestChild.Domain;
using Document = iTextSharp.text.Document;
using DocumentType = RestChild.Domain.DocumentType;
using Font = iTextSharp.text.Font;
using PageSize = iTextSharp.text.PageSize;
using Paragraph = iTextSharp.text.Paragraph;

namespace RestChild.DocumentGeneration.PDFDocuments
{
    /// <summary>
    ///     Процессор документов PDF
    /// </summary>
    public static partial class PdfProcessor
    {
        private static readonly BaseFont BaseFont = BaseFont.CreateFont(
            Environment.GetEnvironmentVariable("SystemRoot") + "\\fonts\\times.ttf", BaseFont.IDENTITY_H,
            BaseFont.EMBEDDED);

        private static readonly BaseFont BaseBoldFont = BaseFont.CreateFont(
            Environment.GetEnvironmentVariable("SystemRoot") + "\\fonts\\timesbd.ttf", BaseFont.IDENTITY_H,
            BaseFont.EMBEDDED);

        private static readonly BaseFont BaseItalicFont = BaseFont.CreateFont(
            Environment.GetEnvironmentVariable("SystemRoot") + "\\fonts\\timesi.ttf", BaseFont.IDENTITY_H,
            BaseFont.EMBEDDED);

        private static readonly Font HeaderFont = new Font(BaseBoldFont, 11);
        private static readonly Font TitleFont = new Font(BaseBoldFont, 14);
        private static readonly Font MainText = new Font(BaseFont, 11);
        private static readonly Font MainItalicText = new Font(BaseItalicFont, 11);
        private static readonly Font SmallText = new Font(BaseFont, 8);

        private static readonly float firstLine = 70;
        private static readonly float firstLine_small = 30;

        /// <summary>
        ///     Уведомление об отказе в предоставлении услуг отдыха и оздоровления (базовый)
        /// </summary>
        internal static IDocument NotificationRefuse1080(Request request)
        {
            var applicant = request.Applicant ?? new Applicant {DocumentType = new DocumentType {Name = string.Empty}};

            using (var ms = new MemoryStream())
            {
                using (var document = new Document(PageSize.A4, 60, 40, 40, 40))
                {
                    var writer = PdfWriter.GetInstance(document, ms);

                    document.Open();
                    writer.SpaceCharRatio = PdfWriter.NO_SPACE_CHAR_RATIO;

                    PdfAddHeader(document);

                    PdfAddParagraph(document, WordProcessor.Space, 0, 1, Element.ALIGN_LEFT, 0, MainText);
                    PdfAddParagraph(document, WordProcessor.Space, 0, 1, Element.ALIGN_LEFT, 0, MainText);

                    PdfAddParagraph(document, "Уведомление об отказе в предоставлении услуг отдыха и оздоровления", 0,
                        1, Element.ALIGN_CENTER, 0, TitleFont);

                    PdfAddParagraph(document, WordProcessor.Space, 0, 1, Element.ALIGN_LEFT, 0, MainText);

                    PdfAddParagraph(document, 0, 1, Element.ALIGN_LEFT, 0,
                        new Chunk("Дата и время регистрации заявления: ", HeaderFont),
                        new Chunk(request.DateRequest.FormatEx(), MainItalicText));
                    PdfAddParagraph(document, 0, 1, Element.ALIGN_LEFT, 0, new Chunk("Номер заявления: ", HeaderFont),
                        new Chunk(request.RequestNumber.FormatEx(), MainItalicText));
                    PdfAddParagraph(document, 0, 1, Element.ALIGN_LEFT, 0, new Chunk("Данные заявителя: ", HeaderFont),
                        new Chunk(
                            $"{applicant.LastName} {applicant.FirstName} {applicant.MiddleName}, {applicant.DateOfBirth.FormatExGR(string.Empty)}",
                            MainItalicText));
                    PdfAddParagraph(document, 0, 1, Element.ALIGN_LEFT, 0,
                        new Chunk("Контактная информация: ", HeaderFont),
                        new Chunk(applicant.Phone + ", " + applicant.Email, MainItalicText));
                    PdfAddParagraph(document, 0, 1, Element.ALIGN_LEFT, 0, new Chunk("Вид услуги: ", HeaderFont),
                        new Chunk(request.TypeOfRest?.Name, MainItalicText));

                    foreach (var child in request.Child?.Where(c => !c.IsDeleted) ?? new List<Child>())
                    {
                        PdfAddParagraph(document, 0, 1, Element.ALIGN_LEFT, 0,
                            new Chunk(
                                "Данные ребенка/лица из числа детей-сирот и детей, оставшихся без попечения родителей: ",
                                HeaderFont),
                            new Chunk("\n", MainItalicText),
                            new Chunk(
                                $"{child.LastName} {child.FirstName} {child.MiddleName}, {child.DateOfBirth.FormatExGR(string.Empty)}",
                                MainItalicText));
                        PdfAddParagraph(document, 0, 1, Element.ALIGN_LEFT, 0,
                            new Chunk("Льготная категория: ", HeaderFont),
                            new Chunk($"{child.BenefitType?.Name}", MainItalicText));
                    }

                    if (request.TypeOfRestId == (long) TypeOfRestEnum.YouthRestCamps ||
                        request.TypeOfRestId == (long) TypeOfRestEnum.YouthRestOrphanCamps)
                    {
                        PdfAddParagraph(document, 0, 1, Element.ALIGN_LEFT, 0,
                            new Chunk(
                                "Данные ребёнка/лица из числа детей-сирот и детей, оставшихся без попечения родителей: ",
                                HeaderFont),
                            new Chunk("\n", MainItalicText),
                            new Chunk(
                                $"{applicant.LastName} {applicant.FirstName} {applicant.MiddleName}, {applicant.DateOfBirth.FormatExGR(string.Empty)}",
                                MainItalicText));
                        PdfAddParagraph(document, 0, 1, Element.ALIGN_LEFT, 0,
                            new Chunk("Льготная категория: ", HeaderFont),
                            new Chunk(
                                "Дети-сироты и дети, оставшиеся без попечения родителей, в возрасте от 18 до 23 лет",
                                MainItalicText));
                    }

                    PdfAddParagraph(document, 0, 1, Element.ALIGN_LEFT, 0,
                        new Chunk("Результат рассмотрения заявления: ", HeaderFont),
                        new Chunk("отказ в предоставлении услуг отдыха и оздоровления.", MainItalicText));
                    PdfAddParagraph(document, 0, 1, Element.ALIGN_JUSTIFIED, 0, new Chunk("Основание отказа: ", HeaderFont),
                        new Chunk(WordProcessor.FederalLaw, MainText));
                    PdfAddParagraph(document, 0, 1, Element.ALIGN_JUSTIFIED, 0, new Chunk("Причина отказа: ", HeaderFont),
                        new Chunk(request.DeclineReason?.Name, MainText));

                    document.Close();
                }

                return new DocumentResult
                {
                    FileBody = ms.ToArray(),
                    FileName = "Уведомление об отказе в предоставлении услуг отдыха и оздоровления.pdf",
                    MimeType = MimeType,
                    MimeTypeShort = Extension
                };
            }
        }

        /// <summary>
        ///     1080.2 Уведомление об отказе в предоставлении услуг отдыха и оздоровления (Представление документов, несоответствующих требованиям)
        /// </summary>
        internal static IDocument NotificationRefuse10802(Request request)
        {
            var applicant = request.Applicant ?? new Applicant { DocumentType = new DocumentType { Name = string.Empty } };

            using (var ms = new MemoryStream())
            {
                using (var document = new Document(PageSize.A4, 60, 40, 40, 40))
                {
                    var writer = PdfWriter.GetInstance(document, ms);

                    document.Open();
                    writer.SpaceCharRatio = PdfWriter.NO_SPACE_CHAR_RATIO;

                    PdfAddHeader(document);

                    PdfAddParagraph(document, WordProcessor.Space, 0, 1, Element.ALIGN_LEFT, 0, MainText);
                    PdfAddParagraph(document, WordProcessor.Space, 0, 1, Element.ALIGN_LEFT, 0, MainText);

                    PdfAddParagraph(document, "Уведомление об отказе в предоставлении услуг отдыха и оздоровления\nв связи с представлением документов, не соответствующих требованиям", 0,
                        1, Element.ALIGN_CENTER, 0, TitleFont);

                    PdfAddParagraph(document, WordProcessor.Space, 0, 1, Element.ALIGN_LEFT, 0, MainText);

                    PdfAddParagraph(document, 0, 1, Element.ALIGN_LEFT, 0,
                        new Chunk("Дата и время регистрации заявления: ", HeaderFont),
                        new Chunk(request.DateRequest.FormatEx(), MainItalicText));
                    PdfAddParagraph(document, 0, 1, Element.ALIGN_LEFT, 0, new Chunk("Номер заявления: ", HeaderFont),
                        new Chunk(request.RequestNumber.FormatEx(), MainItalicText));
                    PdfAddParagraph(document, 0, 1, Element.ALIGN_LEFT, 0, new Chunk("Данные заявителя: ", HeaderFont),
                        new Chunk(
                            $"{applicant.LastName} {applicant.FirstName} {applicant.MiddleName}, {applicant.DateOfBirth.FormatExGR(string.Empty)}",
                            MainItalicText));
                    PdfAddParagraph(document, 0, 1, Element.ALIGN_LEFT, 0,
                        new Chunk("Контактная информация: ", HeaderFont),
                        new Chunk(applicant.Phone + ", " + applicant.Email, MainItalicText));
                    PdfAddParagraph(document, 0, 1, Element.ALIGN_LEFT, 0, new Chunk("Вид услуги: ", HeaderFont),
                        new Chunk(request.TypeOfRest?.Name, MainItalicText));
                    if (request.Child != null)
                    {
                        foreach (var child in request.Child.Where(c => !c.IsDeleted))
                        {
                            PdfAddParagraph(document, 0, 1, Element.ALIGN_LEFT, 0,
                                new Chunk(
                                    "Данные ребёнка/лица из числа детей-сирот и детей, оставшихся без попечения родителей: ",
                                    HeaderFont),
                                new Chunk(
                                    $"{child.LastName} {child.FirstName} {child.MiddleName}, {child.DateOfBirth.FormatExGR(string.Empty)}",
                                    MainItalicText));
                            PdfAddParagraph(document, 0, 1, Element.ALIGN_LEFT, 0,
                                new Chunk("Льготная категория: ", HeaderFont),
                                new Chunk($"{child.BenefitType?.Name}", MainItalicText));
                        }
                    }

                    if (request.TypeOfRestId == (long)TypeOfRestEnum.YouthRestCamps ||
                        request.TypeOfRestId == (long)TypeOfRestEnum.YouthRestOrphanCamps)
                    {
                        PdfAddParagraph(document, 0, 1, Element.ALIGN_LEFT, 0,
                            new Chunk(
                                "Данные ребёнка/лица из числа детей-сирот и детей, оставшихся без попечения родителей:",
                                HeaderFont),
                            new Chunk("\n", MainText),
                            new Chunk(
                                $"{applicant.LastName} {applicant.FirstName} {applicant.MiddleName}, {applicant.DateOfBirth.FormatExGR(string.Empty)}",
                                MainItalicText));
                        PdfAddParagraph(document, 0, 1, Element.ALIGN_LEFT, 0,
                            new Chunk("Льготная категория: ", HeaderFont),
                            new Chunk(
                                "Дети-сироты и дети, оставшиеся без попечения родителей, в возрасте от 18 до 23 лет",
                                MainItalicText));
                    }

                    PdfAddParagraph(document, 0, 1, Element.ALIGN_JUSTIFIED, 0,
                        new Chunk("Результат рассмотрения заявления: ", HeaderFont),
                        new Chunk("отказ в предоставлении услуг отдыха и оздоровления.", MainItalicText));
                    PdfAddParagraph(document, 0, 1, Element.ALIGN_JUSTIFIED, 0, new Chunk("Основание отказа: ", HeaderFont),
                        new Chunk(WordProcessor.FederalLaw, MainText));
                    PdfAddParagraph(document, 0, 1, Element.ALIGN_JUSTIFIED, 0, new Chunk("Причина отказа: ", HeaderFont),
                        new Chunk(request.DeclineReason?.Name, MainText));

                    PdfAddParagraph(document, WordProcessor.Space, 0, 1, Element.ALIGN_LEFT, 0, MainText);
                    PdfAddParagraph(document, "Уважаемый заявитель!", 0, 1, Element.ALIGN_CENTER, 0, HeaderFont);
                    PdfAddParagraph(document, "Просим обратить внимание на следующую информацию.", 0, 1, Element.ALIGN_CENTER, 0, HeaderFont);
                    PdfAddParagraph(document, WordProcessor.Space, 0, 1, Element.ALIGN_LEFT, 0, MainText);

                    PdfAddParagraph(document, "В случае принятия решения о повторной подаче заявления, рекомендуем убедиться в корректности внесенных в заявление сведений.", 0, 1, Element.ALIGN_JUSTIFIED, firstLine_small, MainItalicText);
                    PdfAddParagraph(document, "Обращаем внимание, что сведения, указанные в заявлении, должны полностью соответствовать сведениям, указанным в СНИЛС и документе, удостоверяющем личность заявителя, ребёнка/детей (в том числе следует обратить внимание на соответствие написания букв \"е-ё\").", 0, 1, Element.ALIGN_JUSTIFIED, firstLine_small, MainItalicText);
                    PdfAddParagraph(document, "В случае если сведения о Вас и ребёнке/детях внесены в \"личный кабинет\" Портала Мэра и Правительства Москвы (при подаче заявления данные отразились в полях автоматически), рекомендуем Вам проверить корректность внесенных сведений, а именно: сверить данные, указанные в \"личном кабинете\" Портала Мэра и Правительства Москвы и данные, указанные в СНИЛС и документе, удостоверяющем личность: фамилия, имя, отчество, пол, дата рождения.", 0, 1, Element.ALIGN_JUSTIFIED, firstLine_small, MainItalicText);
                    PdfAddParagraph(document, "В случае если Вами обнаружена ошибка в \"личном кабинете\" Портала Мэра и Правительства Москвы, ее необходимо исправить.", 0, 1, Element.ALIGN_JUSTIFIED, firstLine_small, MainItalicText);
                    PdfAddParagraph(document, "Дополнительно сообщаем, что в случае обнаружения расхождений сведений в СНИЛС и документе, удостоверяющем личность, Вам необходимо актуализировать документы в соответствующих органах, приведя их к единообразию.", 0, 1, Element.ALIGN_JUSTIFIED, firstLine_small, MainItalicText);

                    document.Close();
                }

                return new DocumentResult
                {
                    FileBody = ms.ToArray(),
                    FileName = "Уведомление об отказе в предоставлении услуг отдыха и оздоровления.pdf",
                    MimeType = MimeType,
                    MimeTypeShort = Extension
                };
            }
        }

        /// <summary>
        ///     Уведомление об отказе в предоставлении услуг отдыха и оздоровления (Отсутствие квоты)
        /// </summary>
        internal static IDocument NotificationRefuse10805(IUnitOfWork unitOfWork, Request request, IEnumerable<int> years, IDbSet<ListTravelersRequest> listTravelersRequest )
        {
            var applicant = request.Applicant ?? new Applicant { DocumentType = new DocumentType { Name = string.Empty } };

           // var listTravelersRequest = unitOfWork.GetSet<ListTravelersRequest>().FirstOrDefault(ss => ss.RequestId == request.Id);

            using (var ms = new MemoryStream())
            {
                using (var document = new Document(PageSize.A4, 60, 40, 40, 40))
                {
                    var writer = PdfWriter.GetInstance(document, ms);

                    document.Open();
                    writer.SpaceCharRatio = PdfWriter.NO_SPACE_CHAR_RATIO;

                    PdfAddHeader(document);

                    PdfAddParagraph(document, WordProcessor.Space, 0, 1, Element.ALIGN_LEFT, 0, MainText);
                    PdfAddParagraph(document, WordProcessor.Space, 0, 1, Element.ALIGN_LEFT, 0, MainText);

                    PdfAddParagraph(document, "Уведомление о результатах рассмотрения заявления о предоставлении услуг отдыха и оздоровления", 0,
                        1, Element.ALIGN_CENTER, 0, TitleFont);

                    PdfAddParagraph(document, WordProcessor.Space, 0, 1, Element.ALIGN_LEFT, 0, MainText);

                    PdfAddParagraph(document, 0, 1, Element.ALIGN_LEFT, 0,
                        new Chunk("Дата и время регистрации заявления: ", HeaderFont),
                        new Chunk(request.DateRequest.FormatEx(), MainItalicText));
                    PdfAddParagraph(document, 0, 1, Element.ALIGN_LEFT, 0, new Chunk("Номер заявления: ", HeaderFont),
                        new Chunk(request.RequestNumber.FormatEx(), MainItalicText));
                    PdfAddParagraph(document, 0, 1, Element.ALIGN_LEFT, 0, new Chunk("Данные заявителя: ", HeaderFont),
                        new Chunk(
                            $"{applicant.LastName} {applicant.FirstName} {applicant.MiddleName}, {applicant.DateOfBirth.FormatExGR(string.Empty)}",
                            MainItalicText));
                    PdfAddParagraph(document, 0, 1, Element.ALIGN_JUSTIFIED, 0,
                        new Chunk("Контактная информация: ", HeaderFont),
                        new Chunk(applicant.Phone + ", " + applicant.Email, MainItalicText));


                    PdfAddParagraph(document, 0, 1, Element.ALIGN_JUSTIFIED, 0, new Chunk("Вид услуги: ", HeaderFont),
                        new Chunk("бесплатная путевка для отдыха и оздоровления/сертификат на отдых и оздоровление", MainItalicText));

                    foreach (var child in request.Child?.Where(c => !c.IsDeleted) ?? new List<Child>())
                    {
                        PdfAddParagraph(document, 0, 1, Element.ALIGN_JUSTIFIED, 0,
                            new Chunk(
                                "Данные ребёнка:",
                                HeaderFont),
                            new Chunk("\n", MainText),
                            new Chunk(
                                $"{child.LastName} {child.FirstName} {child.MiddleName}, {child.DateOfBirth.FormatExGR(string.Empty)}",
                                MainItalicText));
                        PdfAddParagraph(document, 0, 1, Element.ALIGN_JUSTIFIED, 0,
                            new Chunk("Льготная категория: ", HeaderFont),
                            new Chunk($"{child.BenefitType?.Name}", MainItalicText));
                    }

                    if (request.TypeOfRestId == (long)TypeOfRestEnum.YouthRestCamps ||
                        request.TypeOfRestId == (long)TypeOfRestEnum.YouthRestOrphanCamps)
                    {
                        PdfAddParagraph(document, 0, 1, Element.ALIGN_JUSTIFIED, 0,
                            new Chunk(
                                "Данные ребёнка/лица из числа детей-сирот и детей, оставшихся без попечения родителей: ",
                                HeaderFont),
                            new Chunk(
                                $"{applicant.LastName} {applicant.FirstName} {applicant.MiddleName}, {applicant.DateOfBirth.FormatExGR(string.Empty)}",
                                MainItalicText));
                        PdfAddParagraph(document, 0, 1, Element.ALIGN_JUSTIFIED, 0,
                            new Chunk("Льготная категория: ", HeaderFont),
                            new Chunk(
                                "Дети-сироты и дети, оставшиеся без попечения родителей, в возрасте от 18 до 23 лет",
                                MainItalicText));
                    }

                    PdfAddParagraph(document, 0, 1, Element.ALIGN_JUSTIFIED, 0,
                        new Chunk("Результат рассмотрения заявления: ", HeaderFont),
                        new Chunk($"учитывая получение услуг отдыха и оздоровления в предыдущие годы и исходя из даты и времени подачи заявления, оказание услуг отдыха и оздоровления в {request.TimeOfRest?.Year ?? DateTime.Now.Year} году не представляется возможным.", MainText));
                    PdfAddParagraph(document, 0, 1, Element.ALIGN_JUSTIFIED, 0, new Chunk("Основание: ", HeaderFont),
                        new Chunk("пункты 3.9. и 9.1.1. Порядка организации отдыха и оздоровления детей, находящихся в трудной жизненной ситуации, утвержденного постановлением Правительства Москвы от 22 февраля 2017 г. № 56-ПП \"Об организации отдыха и оздоровления детей, находящихся в трудной жизненной ситуации\".", MainText));

                    //if (request.Child != null && request.Child.Any(c => !c.IsDeleted) && listTravelersRequest != null && listTravelersRequest.Details.Any(ss => ss.Detail != "[]"))
                    //{
                        var details = listTravelersRequest?.SelectMany(d => d.Details)
                                   .Where(ss => ss.Detail != "[]")
                                   .Select(ss => ss.Detail)
                                   .SelectMany(JsonConvert.DeserializeObject<DetailInfo[]>).ToList();

                        var firstLine = true;

                        foreach (var child in request.Child.Where(c => !c.IsDeleted).ToList())
                        {
                            var requestIds = details.Where(ss => ss.ChildId == child.Id).Select(ss => ss.Id).Distinct().ToArray();
                            if (requestIds.Length > 0)
                            {
                                var requests = unitOfWork.GetSet<Request>().Where(ss => requestIds.Contains(ss.Id) && years.Contains(ss.YearOfRest.Year)).ToList();
                                if (firstLine)
                                {
                                    PdfAddParagraph(document, WordProcessor.Space, 0, 1, Element.ALIGN_LEFT, 0, MainText);


                                    PdfAddParagraph(document, 0, 1, Element.ALIGN_LEFT, 0, new Chunk("Информация об услугах, оказанных ребёнку/детям в течение последних 3-х лет", HeaderFont));

                                    firstLine = false;
                                }

                                PdfAddParagraph(document, 0, 10, Element.ALIGN_LEFT, 0,
                                    new Chunk($"{child.LastName} {child.FirstName} {child.MiddleName}, {child.DateOfBirth.FormatExGR(string.Empty)}, {child.Snils}", MainText));

                                
                                GetPdfTable( requests, document, years);
                            }
                        }

                    document.Close();

                }

                return new DocumentResult
                {
                    FileBody = ms.ToArray(),
                    FileName = "Уведомление об отказе в предоставлении услуг отдыха и оздоровления.pdf",
                    MimeType = MimeType,
                    MimeTypeShort = Extension
                };
            }
        }

        /// <summary>
        ///     Уведомление формируемое при не участии в выборе альтернативной путёвки
        /// </summary>
        internal static IDocument NotificationRefuse108013(Request request)
        {
            var applicant = request.Applicant ?? new Applicant { DocumentType = new DocumentType { Name = string.Empty } };

            using (var ms = new MemoryStream())
            {
                using (var document = new Document(PageSize.A4, 60, 40, 40, 40))
                {
                    var writer = PdfWriter.GetInstance(document, ms);

                    document.Open();
                    writer.SpaceCharRatio = PdfWriter.NO_SPACE_CHAR_RATIO;

                    PdfAddHeader(document);

                    PdfAddParagraph(document, WordProcessor.Space, 0, 1, Element.ALIGN_LEFT, 0, MainText);
                    PdfAddParagraph(document, WordProcessor.Space, 0, 1, Element.ALIGN_LEFT, 0, MainText);

                    var simple = false;
                    if (request.StatusId == (long) StatusEnum.CertificateIssued)
                    {
                        PdfAddParagraph(document,
                            "Уведомление о неучастии заявителя в выборе конкретной организации отдыха и оздоровления",
                            0, 1, Element.ALIGN_CENTER, 0, TitleFont);
                        simple = true;
                    }
                    else
                    {
                        PdfAddParagraph(document, 0, 1, Element.ALIGN_CENTER, 0,
                            new Chunk("Уведомление об отказе в предоставлении услуг отдыха и оздоровления", TitleFont),
                            Chunk.NEWLINE,
                            new Chunk("в связи с неучастием заявителя во втором этапе заявочной кампании", TitleFont));
                    }

                    PdfAddParagraph(document, WordProcessor.Space, 0, 1, Element.ALIGN_LEFT, 0, MainText);

                    PdfAddParagraph(document, 0, 1, Element.ALIGN_LEFT, 0,
                        new Chunk("Дата и время регистрации заявления: ", HeaderFont),
                        new Chunk(request.DateRequest.FormatEx(), MainItalicText));
                    PdfAddParagraph(document, 0, 1, Element.ALIGN_LEFT, 0, new Chunk("Номер заявления: ", HeaderFont),
                        new Chunk(request.RequestNumber.FormatEx(), MainItalicText));
                    PdfAddParagraph(document, 0, 1, Element.ALIGN_LEFT, 0, new Chunk("Данные заявителя: ", HeaderFont),
                        new Chunk(
                            $"{applicant.LastName} {applicant.FirstName} {applicant.MiddleName}, {applicant.DateOfBirth.FormatExGR(string.Empty)}",
                            MainItalicText));
                    PdfAddParagraph(document, 0, 1, Element.ALIGN_LEFT, 0,
                        new Chunk("Контактная информация: ", HeaderFont),
                        new Chunk(applicant.Phone + ", " + applicant.Email, MainItalicText));
                    PdfAddParagraph(document, 0, 1, Element.ALIGN_JUSTIFIED, 0, new Chunk("Вид услуги: ", HeaderFont),
                        new Chunk(request.TypeOfRest?.Name, MainItalicText));
                    if (request.Child != null)
                    {
                        foreach (var child in request.Child.Where(c => !c.IsDeleted))
                        {
                            PdfAddParagraph(document, 0, 1, Element.ALIGN_JUSTIFIED, 0,
                                new Chunk(
                                    simple ? "Данные ребенка/детей: " : "Данные ребенка/лица из числа детей-сирот и детей, оставшихся без попечения родителей: ",
                                    HeaderFont),
                                new Chunk(
                                    $"{child.LastName} {child.FirstName} {child.MiddleName}, {child.DateOfBirth.FormatExGR(string.Empty)}",
                                    MainItalicText));
                            PdfAddParagraph(document, 0, 1, Element.ALIGN_JUSTIFIED, 0,
                                new Chunk("Льготная категория: ", HeaderFont),
                                new Chunk($"{child.BenefitType?.Name}", MainItalicText));
                        }
                    }

                    if (request.TypeOfRestId == (long)TypeOfRestEnum.YouthRestCamps ||
                        request.TypeOfRestId == (long)TypeOfRestEnum.YouthRestOrphanCamps)
                    {
                        PdfAddParagraph(document, 0, 1, Element.ALIGN_LEFT, 0,
                            new Chunk(
                                "Данные ребенка (детей) /лица из числа детей-сирот и детей, оставшихся без попечения родителей: ",
                                HeaderFont),
                            new Chunk(
                                $"{applicant.LastName} {applicant.FirstName} {applicant.MiddleName}, {applicant.DateOfBirth.FormatExGR(string.Empty)}",
                                MainItalicText));
                        PdfAddParagraph(document, 0, 1, Element.ALIGN_JUSTIFIED, 0,
                            new Chunk("Льготная категория: ", HeaderFont),
                            new Chunk(
                                "Дети-сироты и дети, оставшиеся без попечения родителей, в возрасте от 18 до 23 лет",
                                MainItalicText));
                    }

                    PdfAddParagraph(document, 0, 1, Element.ALIGN_JUSTIFIED, 0,
                        new Chunk("Результат рассмотрения заявления: ", HeaderFont),
                        new Chunk(simple ? "Услуга оказана. Решение отрицательное." : "отказ в предоставлении услуг отдыха и оздоровления.",
                            MainItalicText));
                    if (simple)
                    {
                        PdfAddParagraph(document, 0, 1, Element.ALIGN_JUSTIFIED, 0, new Chunk("Основание отказа: ", HeaderFont),
                            new Chunk(
                                ("пункт 8.8. Порядка организации отдыха и оздоровления детей, находящихся"
                                ), MainText),
                            Chunk.NEWLINE,
                            new Chunk(
                                ("в трудной жизненной ситуации, утвержденного постановлением Правительства Москвы"
                                ), MainText),
                            Chunk.NEWLINE,
                            new Chunk(
                                ("от 22 февраля 2017 г. № 56-ПП \"Об организации отдыха и оздоровления детей, находящихся в трудной жизненной ситуации\""
                                )+ " (далее – Порядок).", MainText));
                    }
                    else
                    {
                        PdfAddParagraph(document, 0, 1, Element.ALIGN_JUSTIFIED_ALL, 0, new Chunk("Основание отказа: ", HeaderFont),
                            new Chunk(
                                ("пункты 8.9. и 9.1.7 Порядка организации отдыха и оздоровления детей, находящихся в трудной жизненной ситуации, утвержденного постановлением Правительства Москвы"
                                ), MainText)
                        );

                        PdfAddParagraph(document, 0, 1, Element.ALIGN_JUSTIFIED, 0,
                            new Chunk(
                                ("от 22 февраля 2017 г. № 56-ПП \"Об организации отдыха и оздоровления детей, находящихся в трудной жизненной ситуации\""
                                ) + " (далее – Порядок).", MainText)
                            );
                    }
                    if (simple)
                    {
                        PdfAddParagraph(document, 0, 1, Element.ALIGN_JUSTIFIED, 0, new Chunk("          ", HeaderFont),
                            new Chunk(("В случае неучастия заявителя, подавшего заявление о предоставлении услуг отдыха"
                                ), MainText),
                            Chunk.NEWLINE,
                            new Chunk(("и оздоровления в отношении организации индивидуального выездного отдыха либо совместного выездного отдыха ребенка, отнесенного к одной из категорий детей, находящихся в трудной жизненной ситуации и указанных в пунктах 3.1.3 и 3.1.4 Порядка, заявителя, подавшего заявление"
                                ), MainText),
                            Chunk.NEWLINE,
                            new Chunk(("о предоставлении услуг отдыха и оздоровления в отношении организации индивидуального выездного отдыха ребенка, отнесенного к одной из категорий детей, находящихся в трудной жизненной ситуации"
                                ), MainText),
                            Chunk.NEWLINE,
                            new Chunk(("и указанных в пунктах 3.1.5-3.1.13 Порядка, во втором этапе заявочной кампании, услуга отдыха и оздоровления считается предоставленной."
                                ), MainText));
                    }
                    else
                    {
                        PdfAddParagraph(document, 0, 1, Element.ALIGN_JUSTIFIED_ALL, 25,
                            new Chunk(("В случае неучастия заявителя, подавшего заявление о предоставлении услуг отдыха"
                                ), MainText));

                        PdfAddParagraph(document, 0, 1, Element.ALIGN_JUSTIFIED, 0,
                            new Chunk(("и оздоровления в отношении организации индивидуального выездного отдыха либо совместного выездного отдыха ребенка, находящегося в трудной жизненной ситуации и указанного в пункте 3.1.2 Порядка, лица из числа детей-сирот и детей, оставшихся без попечения родителей, во втором этапе заявочной кампании, услуга отдыха и оздоровления не считается оказанной."
                                ), MainText));
                    }

                    document.Close();
                }

                return new DocumentResult
                {
                    FileBody = ms.ToArray(),
                    FileName = "Уведомление в связи с неучастием.pdf",
                    MimeType = MimeType,
                    MimeTypeShort = Extension
                };
            }
        }

        /// <summary>
        ///     Уведомление о предоставлении бесплатной путевки для отдыха и оздоровления
        /// </summary>
        internal static IDocument NotificationCertificate107501(Request request)
        {
            var applicant = request.Applicant ?? new Applicant {DocumentType = new DocumentType {Name = string.Empty}};

            using (var ms = new MemoryStream())
            {
                using (var document = new Document(PageSize.A4, 60, 40, 40, 40))
                {
                    var writer = PdfWriter.GetInstance(document, ms);

                    document.Open();
                    writer.SpaceCharRatio = PdfWriter.NO_SPACE_CHAR_RATIO;

                    PdfAddHeader(document);

                    PdfAddParagraph(document, WordProcessor.Space, 0, 1, Element.ALIGN_LEFT, 0, MainText);
                    PdfAddParagraph(document, WordProcessor.Space, 0, 1, Element.ALIGN_LEFT, 0, MainText);

                    PdfAddParagraph(document,
                        "Уведомление о предоставлении бесплатной путевки для отдыха",
                        0, 1, Element.ALIGN_CENTER, 0, TitleFont);

                    PdfAddParagraph(document,
                        "и оздоровления",
                        0, 1, Element.ALIGN_CENTER, 0, TitleFont);

                    PdfAddParagraph(document, WordProcessor.Space, 0, 1, Element.ALIGN_LEFT, 0, MainText);

                    PdfAddParagraph(document, 0, 1, Element.ALIGN_LEFT, 0,
                        new Chunk("Дата и время регистрации заявления: ", HeaderFont),
                        new Chunk(request.DateRequest.FormatEx(), MainItalicText));
                    PdfAddParagraph(document, 0, 1, Element.ALIGN_LEFT, 0, new Chunk("Номер заявления: ", HeaderFont),
                        new Chunk(request.RequestNumber.FormatEx(), MainItalicText));
                    PdfAddParagraph(document, 0, 1, Element.ALIGN_LEFT, 0, new Chunk("Данные заявителя: ", HeaderFont),
                        new Chunk(
                            $"{applicant.LastName} {applicant.FirstName} {applicant.MiddleName}, {applicant.DateOfBirth.FormatExGR(string.Empty)}",
                            MainItalicText));
                    PdfAddParagraph(document, 0, 1, Element.ALIGN_LEFT, 0,
                        new Chunk("Контактная информация: ", HeaderFont),
                        new Chunk(applicant.Phone + ", " + applicant.Email, MainItalicText));
                    PdfAddParagraph(document, 0, 1, Element.ALIGN_LEFT, 0, new Chunk("Вид отдыха: ", HeaderFont),
                        new Chunk(request.TypeOfRest?.Name, MainItalicText));
                    if (request.Child != null)
                    {
                        foreach (var child in request.Child.Where(c => !c.IsDeleted))
                        {
                            PdfAddParagraph(document, 0, 1, Element.ALIGN_LEFT, 0,
                                new Chunk(
                                    "Данные лиц, указанных в путевке: ",
                                    HeaderFont),
                                new Chunk(
                                    $"{child.LastName} {child.FirstName} {child.MiddleName}, {child.DateOfBirth.FormatExGR(string.Empty)}",
                                    MainItalicText));
                            PdfAddParagraph(document, 0, 1, Element.ALIGN_LEFT, 0,
                                new Chunk("Льготная категория: ", HeaderFont),
                                new Chunk($"{child.BenefitType?.Name}", MainItalicText));
                        }
                    }

                    if (request.TypeOfRestId == (long) TypeOfRestEnum.YouthRestCamps ||
                        request.TypeOfRestId == (long) TypeOfRestEnum.YouthRestOrphanCamps)
                    {
                        PdfAddParagraph(document, 0, 1, Element.ALIGN_LEFT, 0,
                            new Chunk(
                                "Данные лиц, указанных в путевке: ",
                                HeaderFont),
                            new Chunk(
                                $"{applicant.LastName} {applicant.FirstName} {applicant.MiddleName}, {applicant.DateOfBirth.FormatExGR(string.Empty)}",
                                MainItalicText));
                        PdfAddParagraph(document, 0, 1, Element.ALIGN_LEFT, 0,
                            new Chunk("Льготная категория: ", HeaderFont),
                            new Chunk(
                                "Дети-сироты и дети, оставшиеся без попечения родителей, в возрасте от 18 до 23 лет",
                                MainItalicText));
                    }

                    PdfAddParagraph(document, 0, 1, Element.ALIGN_LEFT, 0,
                        new Chunk("Результат рассмотрения заявления: ", HeaderFont),
                        new Chunk("Услуга оказана.", MainItalicText));
                    /*PdfAddParagraph(document, 0, 1, Element.ALIGN_LEFT, 0,
                        new Chunk("Дата и время выбора варианта организации отдыха и оздоровления: ", HeaderFont),
                        new Chunk($"{request.DateChangeStatus:dd.MM.yyyy HH:mm}", MainItalicText));*/
                    PdfAddParagraph(document, 0, 1, Element.ALIGN_LEFT, 0, new Chunk("Номер путевки: ", HeaderFont),
                        new Chunk(request.CertificateNumber, MainItalicText));
                    PdfAddParagraph(document, 0, 1, Element.ALIGN_LEFT, 0,
                        new Chunk("Организация отдыха и оздоровления: ", HeaderFont),
                        new Chunk(request.Tour?.Hotels?.Name, MainItalicText));
                    PdfAddParagraph(document, 0, 1, Element.ALIGN_LEFT, 0, new Chunk("Время отдыха: ", HeaderFont),
                        new Chunk(
                            $"{request.TimeOfRest?.Name} ({request.Tour?.DateIncome.FormatEx()} - {request.Tour?.DateOutcome.FormatEx()})",
                            MainItalicText));
                    PdfAddParagraph(document, 0, 1, Element.ALIGN_JUSTIFIED, 0, new Chunk("Основание: ", HeaderFont),
                        new Chunk($"{WordProcessor.FederalShort2021Law}.", MainItalicText));

                    document.Close();
                }

                return new DocumentResult
                {
                    FileBody = ms.ToArray(),
                    FileName = "Уведомление о предоставлении бесплатной путевки для отдыха и оздоровления.pdf",
                    MimeType = MimeType,
                    MimeTypeShort = Extension
                };
            }
        }

        /// <summary>
        ///     Уведомление о предоставлении сертификата на отдых и оздоровление
        /// </summary>
        internal static IDocument NotificationCertificate107502(Request request)
        {
            var applicant = request.Applicant ?? new Applicant { DocumentType = new DocumentType { Name = string.Empty } };

            using (var ms = new MemoryStream())
            {
                using (var document = new Document(PageSize.A4, 60, 40, 40, 40))
                {
                    var writer = PdfWriter.GetInstance(document, ms);

                    document.Open();
                    writer.SpaceCharRatio = PdfWriter.NO_SPACE_CHAR_RATIO;

                    PdfAddHeader(document);

                    PdfAddParagraph(document, WordProcessor.Space, 0, 1, Element.ALIGN_LEFT, 0, MainText);
                    PdfAddParagraph(document, WordProcessor.Space, 0, 1, Element.ALIGN_LEFT, 0, MainText);

                    PdfAddParagraph(document,"Уведомление о предоставлении сертификата на отдых и оздоровление", 0, 1, Element.ALIGN_CENTER, 0, TitleFont);

                    PdfAddParagraph(document, WordProcessor.Space, 0, 1, Element.ALIGN_LEFT, 0, MainText);

                    PdfAddParagraph(document, 0, 1, Element.ALIGN_LEFT, 0, new Chunk("Дата и время регистрации заявления: ", HeaderFont),
                        new Chunk(request.DateRequest.FormatEx(), MainItalicText));
                    PdfAddParagraph(document, 0, 1, Element.ALIGN_LEFT, 0, new Chunk("Номер заявления: ", HeaderFont),
                        new Chunk(request.RequestNumber.FormatEx(), MainItalicText));
                    PdfAddParagraph(document, 0, 1, Element.ALIGN_LEFT, 0, new Chunk("Данные заявителя: ", HeaderFont),
                        new Chunk(
                            $"{applicant.LastName} {applicant.FirstName} {applicant.MiddleName}, {applicant.DateOfBirth.FormatExGR(string.Empty)}",
                            MainItalicText));
                    PdfAddParagraph(document, 0, 1, Element.ALIGN_LEFT, 0,
                        new Chunk("Контактная информация: ", HeaderFont),
                        new Chunk(applicant.Phone + ", " + applicant.Email, MainItalicText));
                    PdfAddParagraph(document, 0, 1, Element.ALIGN_JUSTIFIED, 0, new Chunk("Вид услуги: ", HeaderFont),
                        new Chunk(request.TypeOfRest?.Name ?? "сертификат на отдых и оздоровление", MainItalicText));

                    if (request.Child != null)
                    {
                        foreach (var child in request.Child.Where(c => !c.IsDeleted))
                        {
                            PdfAddParagraph(document, 0, 1, Element.ALIGN_LEFT, 0,
                                new Chunk(
                                    "Данные лиц, указанных в сертификате: ",
                                    HeaderFont),
                                new Chunk(
                                    $"{child.LastName} {child.FirstName} {child.MiddleName}, {child.DateOfBirth.FormatExGR(string.Empty)}",
                                    MainItalicText));
                            PdfAddParagraph(document, 0, 1, Element.ALIGN_JUSTIFIED, 0,
                                new Chunk("Льготная категория ребенка: ", HeaderFont),
                                new Chunk($"{child.BenefitType?.Name}", MainItalicText));
                        }
                    }

                    if (request.TypeOfRestId == (long)TypeOfRestEnum.MoneyOn18 ||
                        request.TypeOfRestId == (long)TypeOfRestEnum.YouthRestCamps ||
                        request.TypeOfRestId == (long)TypeOfRestEnum.YouthRestOrphanCamps)
                    {
                        PdfAddParagraph(document, 0, 1, Element.ALIGN_LEFT, 0,
                            new Chunk(
                                "Данные ребёнка/лица из числа детей-сирот и детей, оставшихся без попечения родителей: ",
                                HeaderFont),
                            new Chunk(
                                $"{applicant.LastName} {applicant.FirstName} {applicant.MiddleName}, {applicant.DateOfBirth.FormatExGR(string.Empty)}",
                                MainItalicText));
                        PdfAddParagraph(document, 0, 1, Element.ALIGN_LEFT, 0,
                            new Chunk("Льготная категория: ", HeaderFont),
                            new Chunk(
                                "Дети-сироты и дети, оставшиеся без попечения родителей, в возрасте от 18 до 23 лет",
                                MainItalicText));
                    }

                    PdfAddParagraph(document, 0, 1, Element.ALIGN_LEFT, 0,
                        new Chunk("Результат рассмотрения заявления: ", HeaderFont),
                        new Chunk("Услуга оказана.", MainItalicText));
                    PdfAddParagraph(document, 0, 1, Element.ALIGN_LEFT, 0,
                        new Chunk("Номер сертификата: ", HeaderFont),
                        new Chunk(request.CertificateNumber.FormatEx(), MainItalicText));


                    PdfAddParagraph(document, 0, 1, Element.ALIGN_JUSTIFIED, 0, new Chunk("Основание: ", HeaderFont),
                        new Chunk($"{WordProcessor.FederalShort2021Law}.", MainItalicText));

                    document.Close();
                }

                return new DocumentResult
                {
                    FileBody = ms.ToArray(),
                    FileName = "Уведомление о предоставлении сертификата на отдых и оздоровление.pdf",
                    MimeType = MimeType,
                    MimeTypeShort = Extension
                };
            }
        }

        /// <summary>
        ///     Уведомление о регистрации заявления
        /// </summary>
        internal static IDocument NotificationBasicRegistration(Request request, bool youth, Account account)
        {
            var applicant = request.Applicant ?? new Applicant { DocumentType = new DocumentType { Name = string.Empty } };

            using (var ms = new MemoryStream())
            {
                using (var document = new Document(PageSize.A4, 60, 40, 40, 40))
                {
                    var writer = PdfWriter.GetInstance(document, ms);

                    document.Open();
                    writer.SpaceCharRatio = PdfWriter.NO_SPACE_CHAR_RATIO;

                    PdfAddHeader(document);

                    PdfAddParagraph(document, WordProcessor.Space, 0, 1, Element.ALIGN_LEFT, 0, MainText);
                    PdfAddParagraph(document, WordProcessor.Space, 0, 1, Element.ALIGN_LEFT, 0, MainText);

                    if (youth)
                    {
                        //PdfAddParagraph(document, "", 0, 1, Element.ALIGN_CENTER, 0, TitleFont);
                        PdfAddParagraph(document, 0, 1, Element.ALIGN_CENTER, 0,
                            new Chunk("Уведомление о регистрации заявления о предоставлении услуг отдыха", TitleFont),
                            new Chunk("\n", TitleFont),
                            new Chunk("и оздоровления лицу из числа детей-сирот и детей, оставшихся без попечения родителей", TitleFont));
                    }
                    else
                    {
                        //PdfAddParagraph(document, "Уведомление о регистрации заявления о предоставлении услуг отдыха и оздоровления", 0, 1, Element.ALIGN_CENTER, 0, TitleFont);
                        PdfAddParagraph(document, 0, 1, Element.ALIGN_CENTER, 0,
                            new Chunk("Уведомление о регистрации заявления о предоставлении услуг отдыха", TitleFont),
                            new Chunk("\n", TitleFont),
                            new Chunk("и оздоровления", TitleFont));
                    }

                    PdfAddParagraph(document, WordProcessor.Space, 0, 1, Element.ALIGN_LEFT, 0, MainText);

                    PdfAddParagraph(document, 0, 1, Element.ALIGN_LEFT, 0, new Chunk("Дата и время регистрации заявления: ", HeaderFont),
                        new Chunk(request.DateRequest.FormatEx(), MainItalicText));

                    PdfAddParagraph(document, 0, 1, Element.ALIGN_LEFT, 0, new Chunk("Номер заявления: ", HeaderFont),
                        new Chunk(request.RequestNumber.FormatEx(), MainItalicText));
                    PdfAddParagraph(document, 0, 1, Element.ALIGN_LEFT, 0, new Chunk("Данные заявителя: ", HeaderFont),
                        new Chunk(
                            $"{applicant.LastName} {applicant.FirstName} {applicant.MiddleName}, {applicant.DateOfBirth.FormatExGR(string.Empty)}",
                            MainItalicText));
                    PdfAddParagraph(document, 0, 1, Element.ALIGN_LEFT, 0,
                        new Chunk("Контактная информация: ", HeaderFont),
                        new Chunk(applicant.Phone + ", " + applicant.Email, MainItalicText));

                    PdfAddParagraph(document, 0, 1, Element.ALIGN_LEFT, 0, new Chunk("Вид услуги: ", HeaderFont),
                        new Chunk(youth ? "бесплатная путевка для отдыха и оздоровления" : request.TypeOfRest?.Name, MainItalicText));

                    foreach (var child in request.Child?.Where(c => !c.IsDeleted) ?? new List<Child>())
                    {
                        PdfAddParagraph(document, 0, 1, Element.ALIGN_LEFT, 0,
                            new Chunk(
                                "Данные ребёнка:",
                                HeaderFont),
                            new Chunk("\n", TitleFont),
                            new Chunk(
                                $"{child.LastName} {child.FirstName} {child.MiddleName}, {child.DateOfBirth.FormatExGR(string.Empty)}",
                                MainItalicText));
                        PdfAddParagraph(document, 0, 1, Element.ALIGN_LEFT, 0,
                            new Chunk("Льготная категория: ", HeaderFont),
                            new Chunk($"{child.BenefitType?.Name}", MainItalicText));
                    }


                    if (youth)
                    {
                        PdfAddParagraph(document, 0, 1, Element.ALIGN_LEFT, 0,
                            new Chunk(
                                "Данные лица из числа детей-сирот и детей, оставшихся без попечения родителей: ",
                                HeaderFont),
                            new Chunk(
                                $"{applicant.LastName} {applicant.FirstName} {applicant.MiddleName}, {applicant.DateOfBirth.FormatExGR(string.Empty)}",
                                MainItalicText));
                    }


                    PdfAddParagraph(document, 0, 1, Element.ALIGN_JUSTIFIED, firstLine_small,
                        new Chunk("Ваше заявление о предоставлении услуг отдыха и оздоровления (далее – заявление) ", MainText),
                        new Chunk("зарегистрировано", HeaderFont),
                        new Chunk(".", MainText));



                    PdfAddParagraph(document, 0, 1, Element.ALIGN_JUSTIFIED, firstLine_small,
                        new Chunk("В случае если в срок, не превышающий 21 рабочий день с даты подачи, по Вашему заявлению ", MainText),
                        new Chunk("не пришло уведомление о необходимости личной явки заявителя в офис ГАУК \"МОСГОРТУР\" или уведомление об отказе в предоставлении услуг отдыха и оздоровления", HeaderFont),
                        new Chunk($" по причинам, указанным в пунктах {(youth ? "9.1.2-9.1.4, 9.1.6" : "9.1.2-9.1.6, 9.1.8-9.1.11")} Порядка организации отдыха и оздоровления детей, находящихся в трудной жизненной ситуации, утвержденного постановлением Правительства Москвы от 22 февраля 2017 г. № 56-ПП \"Об организации отдыха и оздоровления детей, находящихся в трудной жизненной ситуации\" (далее – Порядок), ", MainText),
                        new Chunk("Ваше заявление прошло все проверки", HeaderFont),
                        new Chunk(" и будет рассмотрено на заседании Комиссии ГАУК \"МОСГОРТУР\" по включению сведений в предварительный Реестр получателей услуг отдыха и оздоровления (далее – Комиссия), которое состоится 16 января 2022 г.", MainText)
                    );


                    PdfAddParagraph(document, 0, 1, Element.ALIGN_JUSTIFIED, firstLine_small,
                        new Chunk("По результатам заседания Комиссии не позднее ", MainText),
                        new Chunk("6 февраля 2022 г.", HeaderFont),
                        new Chunk(" в Ваш \"личный кабинет\" на Портале Мэра и Правительства Москвы будет направлено", MainText),
                        new Chunk( youth ?
                            " уведомление о необходимости выбора организации отдыха и оздоровления (допуск на второй этап заявочной кампании)." :
                            " одно из уведомлений: о необходимости выбора организации отдыха и оздоровления (допуск на второй этап заявочной кампании); о предоставлении сертификата на отдых и оздоровление; об отказе в предоставлении услуг отдыха и оздоровления по причине, указанной в пункте 9.1.1 Порядка.", MainText));

                    if (!youth)
                    {
                        PdfAddParagraph(document, 0, 1, Element.ALIGN_JUSTIFIED, firstLine_small,
                            new Chunk("При рассмотрении заявлений Комиссия руководствуется пунктом 3.9 Порядка, согласно которому в первую очередь рассматриваются заявления, в которых указан хотя бы один ребёнок, которому в период с 2019 по 2021 год не предоставлялись бесплатные путевки для отдыха и оздоровления (далее – бесплатные путевки) либо сертификаты на отдых и оздоровление (далее – сертификаты), а также не выплачивалась компенсация за самостоятельно приобретенную путевку (далее – компенсация).", MainText));

                        PdfAddParagraph(document, 0, 1, Element.ALIGN_JUSTIFIED, firstLine_small,
                            new Chunk("Во вторую очередь рассматриваются заявления, где указаны дети, которым в любые два года из последних трех лет (с 2019 по 2021 год) не предоставлялись бесплатные путевки либо сертификаты, а также не выплачивалась компенсация.", MainText));

                        PdfAddParagraph(document, 0, 1, Element.ALIGN_JUSTIFIED, firstLine_small,
                            new Chunk("В третью очередь рассматриваются заявления, где указаны дети, которым в любые два года из последних трех лет (с 2019 по 2021 год) предоставлялись бесплатные путевки либо сертификаты, а также выплачивалась компенсация.", MainText));

                        PdfAddParagraph(document, 0, 1, Element.ALIGN_JUSTIFIED, firstLine_small,
                            new Chunk("Исключение составляют дети-сироты и дети, оставшиеся без попечения родителей, находящиеся под опекой, попечительством, в том числе в приемной или патронатной семье, и лица из числа детей-сирот и детей, оставшихся без попечения родителей, которым федеральным законодательством гарантировано ежегодное предоставление услуг отдыха и оздоровления.", MainText));

                        PdfAddParagraph(document, 0, 1, Element.ALIGN_JUSTIFIED, firstLine_small,
                            new Chunk("По результатам второго этапа заявочной кампании, проводимого в период с 7 по 21 февраля 2022 г., уведомление о результате рассмотрения заявления (о предоставлении бесплатной путевки для отдыха и оздоровления; об отказе в предоставлении услуг отдыха и оздоровления в связи с неучастием заявителя во втором этапе заявочной кампании; о неучастии заявителя в выборе конкретной организации отдыха и оздоровления; об отказе в предоставлении услуг отдыха и оздоровления) направляется в \"личный кабинет\" на Портале Мэра и Правительства Москвы ", MainText),
                            new Chunk("не позднее 22 февраля 2022 г.", HeaderFont));

                        PdfAddParagraph(document, 0, 1, Element.ALIGN_JUSTIFIED, firstLine_small,
                            new Chunk("Обращаем внимание, что Вы согласились с необходимостью соблюдения: Правил отдыха и оздоровления, Соглашения об осуществлении обязанностей по сопровождению детей во время отдыха и оздоровления родителем или иным законным представителем либо доверенным лицом для сопровождения во время отдыха и оздоровления (для совместного выездного отдыха); а также проинформированы: о перечне противопоказаний к отдыху и оздоровлению, утвержденном Министерством здравоохранения Российской Федерации (для индивидуального выездного отдыха), об отсутствии возможности замены сертификата на отдых и оздоровление на бесплатную путевку для отдыха и оздоровления (в случае выбора сертификата на отдых и оздоровление), об отсутствии возможности замены бесплатной путевки для отдыха и оздоровления на сертификат на отдых и оздоровление (в случае выбора бесплатной путевки для отдыха и оздоровления), об отсутствии возможности выбора сертификата на отдых и оздоровление в случае отказа на втором этапе заявочной кампании от всех предложенных организаций отдыха и оздоровления, о перечислении денежных средств организации, осуществляющей туроператорскую деятельность и (или) оказывающей услуги отдыха и оздоровления, при приобретении у такой организации услуг отдыха и оздоровления или туристского продукта для ребёнка, либо ребенка и лица, его сопровождающего с использованием сертификата на отдых и оздоровление (в случае выбора сертификата на отдых и оздоровление). ", MainText));
                    }
                    else
                    {
                        PdfAddParagraph(document, 0, 1, Element.ALIGN_JUSTIFIED, firstLine_small,
                            new Chunk("По результатам второго этапа заявочной кампании, проводимого в период с 7 по 21 февраля 2022 г., уведомление о результате рассмотрения заявления (о предоставлении бесплатной путевки для отдыха и оздоровления, об отказе в предоставлении услуг отдыха и оздоровления в связи с неучастием заявителя во втором этапе заявочной кампании; об отказе в предоставлении услуг отдыха и оздоровления) будет направлено в Ваш \"личный кабинет\" на Портале Мэра и Правительства Москвы ", MainText),
                            new Chunk("не позднее 22 февраля 2022 г.", HeaderFont));

                        PdfAddParagraph(document, 0, 1, Element.ALIGN_JUSTIFIED, firstLine_small,
                            new Chunk("Обращаем внимание, что Вы согласились с необходимостью соблюдения Правил отдыха и оздоровления.", MainText));
                    }

                    //SignWorkerBlock(document, account);
                    document.Close();
                }

                return new DocumentResult
                {
                    FileBody = ms.ToArray(),
                    FileName = "Уведомление о регистрации.pdf",
                    MimeType = MimeType,
                    MimeTypeShort = Extension
                };
            }
        }

        /// <summary>
        ///     Уведомление о приостановлении рассмотрения (вызов)
        /// </summary>
        internal static IDocument NotificationWaitApplicant(Request request, IEnumerable<BenefitType> benefits, Account account)
        {
            var applicant = request.Applicant ?? new Applicant { DocumentType = new DocumentType { Name = string.Empty } };

            using (var ms = new MemoryStream())
            {
                using (var document = new Document(PageSize.A4, 60, 40, 40, 40))
                {
                    var writer = PdfWriter.GetInstance(document, ms);
                    document.Open();
                    writer.SpaceCharRatio = PdfWriter.NO_SPACE_CHAR_RATIO;

                    PdfAddHeader(document);

                    PdfAddParagraph(document, WordProcessor.Space, 0, 1, Element.ALIGN_LEFT, 0, MainText);
                    PdfAddParagraph(document, WordProcessor.Space, 0, 1, Element.ALIGN_LEFT, 0, MainText);

                    PdfAddParagraph(document, "Уведомление о приостановлении рассмотрения заявления о предоставлении услуг отдыха и оздоровления", 0, 1, Element.ALIGN_CENTER, 0, TitleFont);

                    PdfAddParagraph(document, WordProcessor.Space, 0, 1, Element.ALIGN_LEFT, 0, MainText);

                    PdfAddParagraph(document, 0, 1, Element.ALIGN_LEFT, 0, new Chunk("Дата и время регистрации заявления: ", HeaderFont),
                        new Chunk(request.DateRequest.FormatEx(), MainItalicText));

                    PdfAddParagraph(document, 0, 1, Element.ALIGN_LEFT, 0, new Chunk("Номер заявления: ", HeaderFont),
                        new Chunk(request.RequestNumber.FormatEx(), MainItalicText));
                    PdfAddParagraph(document, 0, 1, Element.ALIGN_LEFT, 0, new Chunk("Данные заявителя: ", HeaderFont),
                        new Chunk(
                            $"{applicant.LastName} {applicant.FirstName} {applicant.MiddleName}, {applicant.DateOfBirth.FormatExGR(string.Empty)}",
                            MainItalicText));
                    PdfAddParagraph(document, 0, 1, Element.ALIGN_LEFT, 0,
                        new Chunk("Контактная информация: ", HeaderFont),
                        new Chunk(applicant.Phone + ", " + applicant.Email, MainItalicText));
                    PdfAddParagraph(document, 0, 1, Element.ALIGN_LEFT, 0, new Chunk("Вид услуги: ", HeaderFont),
                        new Chunk(request.TypeOfRest?.Name, MainItalicText));


                    foreach (var child in request.Child?.Where(c => !c.IsDeleted) ?? new List<Child>())
                    {
                        PdfAddParagraph(document, 0, 1, Element.ALIGN_LEFT, 0,
                            new Chunk(
                                "Данные ребёнка/лица из числа детей-сирот и детей, оставшихся без попечения родителей:",
                                HeaderFont),
                            new Chunk("\n", MainItalicText),
                            new Chunk(
                                $"{child.LastName} {child.FirstName} {child.MiddleName}, {child.DateOfBirth.FormatExGR(string.Empty)}",
                                MainItalicText));
                        PdfAddParagraph(document, 0, 1, Element.ALIGN_LEFT, 0,
                            new Chunk("Льготная категория: ", HeaderFont),
                            new Chunk($"{child.BenefitType?.Name}", MainItalicText));
                    }

                    if (request.TypeOfRestId == (long)TypeOfRestEnum.YouthRestCamps || request.TypeOfRestId == (long)TypeOfRestEnum.YouthRestOrphanCamps)
                    {
                        PdfAddParagraph(document, 0, 1, Element.ALIGN_LEFT, 0,
                            new Chunk(
                                "Данные ребёнка/лица из числа детей-сирот и детей, оставшихся без попечения родителей:",
                                HeaderFont),
                            new Chunk("\n", MainItalicText),
                            new Chunk(
                                $"{applicant.LastName} {applicant.FirstName} {applicant.MiddleName}, {applicant.DateOfBirth.FormatExGR(string.Empty)}",
                                MainItalicText));
                        PdfAddParagraph(document, 0, 1, Element.ALIGN_LEFT, 0,
                            new Chunk("Льготная категория: ", HeaderFont),
                            new Chunk(
                                "Дети-сироты и дети, оставшиеся без попечения родителей, в возрасте от 18 до 23 лет",
                                MainItalicText));
                    }

                    PdfAddParagraph(document, 0, 1, Element.ALIGN_JUSTIFIED, 0, new Chunk("Этап рассмотрения заявления: ", HeaderFont),
                        new Chunk("рассмотрение заявления о предоставлении услуг отдыха и оздоровления приостановлено.", MainText));

                    PdfAddParagraph(document, 0, 1, Element.ALIGN_JUSTIFIED, 0, new Chunk("Основание: ", HeaderFont),
                        new Chunk($"пункт 6.4 Порядка организации отдыха и оздоровления детей, находящихся в трудной жизненной ситуации, утвержденного постановлением Правительства Москвы от 22 февраля 2017 г. № 56-ПП \"Об организации отдыха и оздоровления детей, находящихся в трудной жизненной ситуации\": ", MainText),
                        new Chunk("\"Необходимость личной явки заявителя в ГАУК \"МОСГОРТУР\".", HeaderFont));

                    PdfAddParagraph(document, WordProcessor.Space, 0, 1, Element.ALIGN_LEFT, 0, MainText);

                    PdfAddParagraph(document, 0, 1, Element.ALIGN_JUSTIFIED, firstLine_small,
                        new Chunk("Для подтверждения сведений, указанных в заявлении о предоставлении услуг отдыха и оздоровления (далее – заявление), ", MainText),
                        new Chunk("в течение 10 рабочих дней со дня направления данного уведомления", HeaderFont),
                        new Chunk(" Вам необходимо явиться в офис ГАУК \"МОСГОРТУР\" по адресу: город Москва, Малый Харитоньевский переулок, дом 6, строение 3.", MainText));

                    PdfAddParagraph(document, 0, 1, Element.ALIGN_JUSTIFIED, firstLine_small,
                        new Chunk("Прием в офисе ГАУК \"МОСГОРТУР\" осуществляется исключительно по предварительной записи.", HeaderFont),
                        new Chunk(" Запись производится через официальный портал Мэра и Правительства Москвы mos.ru (далее – Портал) или при личном визите заявителя в офис ГАУК \"МОСГОРТУР\". Запись производится на свободную дату и время.", MainText));

                    PdfAddParagraph(document, WordProcessor.Space, 0, 1, Element.ALIGN_LEFT, 0, MainText);

                    PdfAddParagraph(document, 0, 1, Element.ALIGN_LEFT, 0,
                        new Chunk("При себе необходимо иметь:", HeaderFont));

                    //List<string> docs = new List<string>
                    //{
                    //    "документ, удостоверяющий личность заявителя;",
                    //    "документы, подтверждающие, что заявитель является родителем ребёнка (свидетельство о рождении ребенка, в случае если с момента рождения ребенка у родителя произошла смена фамилии, имени или отчества – необходимо также предоставить документы, подтверждающие данные изменения);",
                    //    "документ, подтверждающий место жительства ребёнка, лица из числа детей-сирот и детей, оставшихся без попечения родителей, в городе Москве;",
                    //    "документ, подтверждающий полномочия заявителя, сопровождающего лица (в случае организации совместного выездного отдыха) из числа законных представителей – родителей, опекунов, попечителей, приемных родителей, патронатных воспитателей ребёнка (договор о приемной семье, распоряжение об опеке, иные документы, устанавливающие статус ребенка);",
                    //   "документ, подтверждающий полномочия доверенного лица на совершение действий в период проведения заявочной кампании (в случае подачи заявления о предоставлении услуг отдыха и оздоровления с использованием Портала доверенным лицом на совершение действий в период проведения заявочной кампании) (нотариально заверенное согласие или доверенность);",
                    //    "документ, подтверждающий полномочия доверенного лица для сопровождения во время отдыха и оздоровления (в случае организации совместного выездного отдыха и сопровождения ребёнка доверенным лицом для сопровождения во время отдыха и оздоровления и подачи заявления о предоставлении услуг отдыха и оздоровления с использованием Портала) (нотариально заверенное согласие или доверенность);",
                    //    "документ, подтверждающий отнесение ребёнка, к одной из категорий детей, находящихся в трудной жизненной ситуации и указанных в пунктах 3.1.3, 3.1.5 - 3.1.13 Порядка, лица из числа детей-сирот к категории лиц из числа детей-сирот и детей, оставшихся без попечения родителей (заключение медико-социальной экспертизы, заключение Центральной психолого-медико-педагогической комиссии города Москвы, справки уполномоченного учреждения социальной защиты населения города Москвы и/или федеральных органов);"
                    //};

                    //доработка для наркоманов
                    List<string> innerListOrphans = new List<string>();
                    List<string> innerListDisabled = new List<string>();
                    List<string> innerListLowIncome = new List<string>();
                    List<string> innerListSacrifice = new List<string>();
                    List<string> innerListRefugee = new List<string>();
                    List<string> innerListExtreme = new List<string>();
                    List<string> innerListViolence = new List<string>();
                    List<string> innerListInvalid = new List<string>();
                    List<string> innerListTerror = new List<string>();
                    List<string> innerListMilitary = new List<string>();
                    List<string> innerListInvalidParents = new List<string>();
                    List<string> innerListDeviant = new List<string>();
                    List<string> innerListOrphansYouth = new List<string>();


                    if (benefits.Count() > 0 || (request.TypeOfRestId == (long)TypeOfRestEnum.YouthRestOrphanCamps || request.TypeOfRestId == (long)TypeOfRestEnum.CompensationYouthRest))
                    {


                        foreach (BenefitType benefit in benefits)
                        {
                            if (benefit.ExnternalUid.Contains("52"))//дети-сироты и оставшиеся без попечения родителей...
                            {
                                innerListOrphans = new List<string>
                                    {
                                        "документ, удостоверяющий личность заявителя;",
                                        "документы, подтверждающие, что заявитель является законным представителем ребенка: договор о приемной семье, распоряжение об опеке, иные документы, устанавливающие полномочия законного представителя ребенка;",
                                        "документ, удостоверяющий личность ребенка;",
                                        "документ, подтверждающий место жительства ребенка в городе Москве;",
                                        "документ, подтверждающий полномочия заявителя, сопровождающего лица (в случае организации совместного выездного отдыха) из числа законных представителей – опекунов, попечителей, приемных родителей, патронатных воспитателей ребенка(договор о приемной семье, распоряжение об опеке, иные документы, устанавливающие статус ребенка);",
                                        "документ, подтверждающий полномочия доверенного лица на совершение действий в период проведения заявочной кампании (в случае подачи заявления о предоставлении услуг отдыха и оздоровления с использованием Портала доверенным лицом на совершение действий в период проведения заявочной кампании) (нотариально заверенное согласие или доверенность);",
                                        "документ, подтверждающий полномочия доверенного лица для сопровождения во время отдыха и оздоровления (в случае организации совместного выездного отдыха и сопровождения ребенка доверенным лицом для сопровождения во время отдыха и оздоровления и подачи заявления о предоставлении услуг отдыха и оздоровления с использованием Портала) (нотариально заверенное согласие или доверенность).",

                                    };


                            }
                            if (benefit.ExnternalUid.Contains("24"))//дети-инвалиды, дети с ограниченными возможностями...
                            {

                                innerListDisabled = new List<string>
                                    {
                                        "документ, удостоверяющий личность заявителя;",
                                        "документы, подтверждающие, что заявитель является родителем (законным представителем) ребенка: свидетельство о рождении ребенка*, договор о приемной семье, распоряжение об опеке, иные документы, устанавливающие полномочия законного представителя ребенка;",
                                        "документ, подтверждающий место жительства ребенка в городе Москве;",
                                        "документ, удостоверяющий личность ребенка;",
                                        "документ, подтверждающий отнесение ребенка к категории, указанной в заявлении (заключение медико-социальной экспертизы или заключение Центральной психолого-медико-педагогической комиссии города Москвы**);",
                                        "документ, подтверждающий полномочия доверенного лица на совершение действий в период проведения заявочной кампании (в случае подачи заявления о предоставлении услуг отдыха и оздоровления с использованием Портала доверенным лицом на совершение действий в период проведения заявочной кампании) (нотариально заверенное согласие или доверенность);",
                                        "документ, подтверждающий полномочия доверенного лица для сопровождения во время отдыха и оздоровления (в случае организации совместного выездного отдыха и сопровождения ребенка доверенным лицом для сопровождения во время отдыха и оздоровления и подачи заявления о предоставлении услуг отдыха и оздоровления с использованием Портала) (нотариально заверенное согласие или доверенность);",
                                        "* в случае если с момента рождения ребенка у родителя произошла смена фамилии, имени или отчества – необходимо также предоставить документы, подтверждающие данные изменения: свидетельство о браке, свидетельство о расторжении брака, свидетельство о перемене имени;",
                                        "** является документом, подтверждающим категорию, только в случае указания на необходимость предоставления ребенку специальных условий обучения по адаптированной основной образовательной программе."
                                    };



                            }
                            if (benefit.ExnternalUid.Contains("48"))//дети из малообеспеченных семей
                            {

                                innerListLowIncome = new List<string>
                                    {
                                        "документ, удостоверяющий личность заявителя;",
                                        "документы, подтверждающие, что заявитель является родителем ребенка – свидетельство о рождении ребенка*;",

                                        "документ, удостоверяющий личность ребенка;",
                                        "документ, подтверждающий место жительства ребенка в городе Москве;",

                                        "документ, подтверждающий полномочия доверенного лица на совершение действий в период проведения заявочной кампании (в случае подачи заявления о предоставлении услуг отдыха и оздоровления с использованием Портала доверенным лицом на совершение действий в период проведения заявочной кампании) (нотариально заверенное согласие или доверенность);",
                                        "документ, подтверждающий полномочия доверенного лица для сопровождения во время отдыха и оздоровления (в случае организации совместного выездного отдыха и сопровождения ребенка доверенным лицом для сопровождения во время отдыха и оздоровления и подачи заявления о предоставлении услуг отдыха и оздоровления с использованием Портала) (нотариально заверенное согласие или доверенность);",
                                        "* в случае если с момента рождения ребенка у родителя произошла смена фамилии, имени или отчества – необходимо также предоставить документы, подтверждающие данные изменения: свидетельство о браке, свидетельство о расторжении брака, свидетельство о перемене имени."

                                    };
                            }

                            if (benefit.ExnternalUid.Contains("57,71,72"))
                            {
                                innerListSacrifice = new List<string>
                                    {
                                        "документ, удостоверяющий личность заявителя;",
                                        "документы, подтверждающие, что заявитель является родителем ребенка – свидетельство о рождении ребенка*;",

                                        "документ, подтверждающий место жительства ребенка в городе Москве;",
                                        "документ, подтверждающий отнесение ребенка к категории, указанной в заявлении;",
                                        "документ, подтверждающий полномочия доверенного лица на совершение действий в период проведения заявочной кампании (в случае подачи заявления о предоставлении услуг отдыха и оздоровления с использованием Портала доверенным лицом на совершение действий в период проведения заявочной кампании) (нотариально заверенное согласие или доверенность);",
                                        "* в случае если с момента рождения ребенка у родителя произошла смена фамилии, имени или отчества – необходимо также предоставить документы, подтверждающие данные изменения: свидетельство о браке, свидетельство о расторжении брака, свидетельство о перемене имени."

                                    };
                            }
                            if (benefit.BenefitTypeERLId == 7)
                            {
                                innerListRefugee = new List<string>
                                    {
                                        "документ, удостоверяющий личность заявителя;",
                                        "документы, подтверждающие, что заявитель является родителем ребенка – свидетельство о рождении ребенка*;",

                                        "документ, подтверждающий место жительства ребенка в городе Москве;",
                                        "документ, подтверждающий отнесение ребенка к категории, указанной в заявлении;",
                                        "документ, подтверждающий полномочия доверенного лица на совершение действий в период проведения заявочной кампании (в случае подачи заявления о предоставлении услуг отдыха и оздоровления с использованием Портала доверенным лицом на совершение действий в период проведения заявочной кампании) (нотариально заверенное согласие или доверенность);",
                                        "* в случае если с момента рождения ребенка у родителя произошла смена фамилии, имени или отчества – необходимо также предоставить документы, подтверждающие данные изменения: свидетельство о браке, свидетельство о расторжении брака, свидетельство о перемене имени."

                                    };
                            }
                            if (benefit.BenefitTypeERLId == 8)
                            {
                                innerListExtreme = new List<string>
                                    {
                                        "документ, удостоверяющий личность заявителя;",
                                        "документы, подтверждающие, что заявитель является родителем ребенка – свидетельство о рождении ребенка*;",

                                        "документ, подтверждающий место жительства ребенка в городе Москве;",
                                        "документ, подтверждающий отнесение ребенка к категории, указанной в заявлении;",
                                        "документ, подтверждающий полномочия доверенного лица на совершение действий в период проведения заявочной кампании (в случае подачи заявления о предоставлении услуг отдыха и оздоровления с использованием Портала доверенным лицом на совершение действий в период проведения заявочной кампании) (нотариально заверенное согласие или доверенность);",
                                        "* в случае если с момента рождения ребенка у родителя произошла смена фамилии, имени или отчества – необходимо также предоставить документы, подтверждающие данные изменения: свидетельство о браке, свидетельство о расторжении брака, свидетельство о перемене имени."

                                    };
                            }
                            if (benefit.BenefitTypeERLId == 9)
                            {
                                innerListViolence = new List<string>
                                    {
                                        "документ, удостоверяющий личность заявителя;",
                                        "документы, подтверждающие, что заявитель является родителем ребенка – свидетельство о рождении ребенка*;",

                                        "документ, подтверждающий место жительства ребенка в городе Москве;",
                                        "документ, подтверждающий отнесение ребенка к категории, указанной в заявлении;",
                                        "документ, подтверждающий полномочия доверенного лица на совершение действий в период проведения заявочной кампании (в случае подачи заявления о предоставлении услуг отдыха и оздоровления с использованием Портала доверенным лицом на совершение действий в период проведения заявочной кампании) (нотариально заверенное согласие или доверенность);",
                                        "* в случае если с момента рождения ребенка у родителя произошла смена фамилии, имени или отчества – необходимо также предоставить документы, подтверждающие данные изменения: свидетельство о браке, свидетельство о расторжении брака, свидетельство о перемене имени."

                                    };
                            }
                            if (benefit.BenefitTypeERLId == 10)
                            {
                                innerListInvalid = new List<string>
                                    {
                                        "документ, удостоверяющий личность заявителя;",
                                        "документы, подтверждающие, что заявитель является родителем ребенка – свидетельство о рождении ребенка*;",

                                        "документ, подтверждающий место жительства ребенка в городе Москве;",
                                        "документ, подтверждающий отнесение ребенка к категории, указанной в заявлении;",
                                        "документ, подтверждающий полномочия доверенного лица на совершение действий в период проведения заявочной кампании (в случае подачи заявления о предоставлении услуг отдыха и оздоровления с использованием Портала доверенным лицом на совершение действий в период проведения заявочной кампании) (нотариально заверенное согласие или доверенность);",
                                        "* в случае если с момента рождения ребенка у родителя произошла смена фамилии, имени или отчества – необходимо также предоставить документы, подтверждающие данные изменения: свидетельство о браке, свидетельство о расторжении брака, свидетельство о перемене имени."

                                    };
                            }
                            if (benefit.BenefitTypeERLId == 11)
                            {
                                innerListTerror = new List<string>
                                    {
                                        "документ, удостоверяющий личность заявителя;",
                                        "документы, подтверждающие, что заявитель является родителем ребенка – свидетельство о рождении ребенка*;",

                                        "документ, подтверждающий место жительства ребенка в городе Москве;",
                                        "документ, подтверждающий отнесение ребенка к категории, указанной в заявлении;",
                                        "документ, подтверждающий полномочия доверенного лица на совершение действий в период проведения заявочной кампании (в случае подачи заявления о предоставлении услуг отдыха и оздоровления с использованием Портала доверенным лицом на совершение действий в период проведения заявочной кампании) (нотариально заверенное согласие или доверенность);",
                                        "* в случае если с момента рождения ребенка у родителя произошла смена фамилии, имени или отчества – необходимо также предоставить документы, подтверждающие данные изменения: свидетельство о браке, свидетельство о расторжении брака, свидетельство о перемене имени."

                                    };
                            }
                            if (benefit.ExnternalUid.Contains("58,71,72"))
                            {
                                innerListMilitary = new List<string>
                                    {
                                        "документ, удостоверяющий личность заявителя;",
                                        "документы, подтверждающие, что заявитель является родителем ребенка – свидетельство о рождении ребенка*;",

                                        "документ, подтверждающий место жительства ребенка в городе Москве;",
                                        "документ, подтверждающий отнесение ребенка к категории, указанной в заявлении;",
                                        "документ, подтверждающий полномочия доверенного лица на совершение действий в период проведения заявочной кампании (в случае подачи заявления о предоставлении услуг отдыха и оздоровления с использованием Портала доверенным лицом на совершение действий в период проведения заявочной кампании) (нотариально заверенное согласие или доверенность);",
                                        "* в случае если с момента рождения ребенка у родителя произошла смена фамилии, имени или отчества – необходимо также предоставить документы, подтверждающие данные изменения: свидетельство о браке, свидетельство о расторжении брака, свидетельство о перемене имени."

                                    };
                            }
                            if (benefit.ExnternalUid.Contains("56"))
                            {
                                innerListInvalidParents = new List<string>
                                    {
                                        "документ, удостоверяющий личность заявителя;",
                                        "документы, подтверждающие, что заявитель является родителем ребенка – свидетельство о рождении ребенка*;",

                                        "документ, подтверждающий место жительства ребенка в городе Москве;",
                                        "документ, подтверждающий отнесение ребенка к категории, указанной в заявлении;",
                                        "документ, подтверждающий полномочия доверенного лица на совершение действий в период проведения заявочной кампании (в случае подачи заявления о предоставлении услуг отдыха и оздоровления с использованием Портала доверенным лицом на совершение действий в период проведения заявочной кампании) (нотариально заверенное согласие или доверенность);",
                                        "* в случае если с момента рождения ребенка у родителя произошла смена фамилии, имени или отчества – необходимо также предоставить документы, подтверждающие данные изменения: свидетельство о браке, свидетельство о расторжении брака, свидетельство о перемене имени."

                                    };
                            }
                            if (benefit.BenefitTypeERLId == 14)
                            {
                                innerListDeviant = new List<string>
                                    {
                                        "документ, удостоверяющий личность заявителя;",
                                        "документы, подтверждающие, что заявитель является родителем ребенка – свидетельство о рождении ребенка*;",

                                        "документ, подтверждающий место жительства ребенка в городе Москве;",
                                        "документ, подтверждающий отнесение ребенка к категории, указанной в заявлении;",
                                        "документ, подтверждающий полномочия доверенного лица на совершение действий в период проведения заявочной кампании (в случае подачи заявления о предоставлении услуг отдыха и оздоровления с использованием Портала доверенным лицом на совершение действий в период проведения заявочной кампании) (нотариально заверенное согласие или доверенность);",
                                        "* в случае если с момента рождения ребенка у родителя произошла смена фамилии, имени или отчества – необходимо также предоставить документы, подтверждающие данные изменения: свидетельство о браке, свидетельство о расторжении брака, свидетельство о перемене имени."

                                    };
                            }

                        }

                        if (request.TypeOfRestId == (long)TypeOfRestEnum.YouthRestOrphanCamps || request.TypeOfRestId == (long)TypeOfRestEnum.CompensationYouthRest)//лица из числа детей-сирот и детей, оставших без попечения...
                        {


                            innerListOrphansYouth = new List<string>
                                    {
                                        "документ, удостоверяющий личность заявителя;",
                                        "документ, подтверждающий место жительства лица из числа детей-сирот и детей, оставшихся без попечения родителей, в городе Москве;",

                                        "документ, подтверждающий полномочия доверенного лица на совершение действий в период проведения заявочной кампании (в случае подачи заявления о предоставлении услуг отдыха и оздоровления с использованием Портала доверенным лицом на совершение действий в период проведения заявочной кампании) (нотариально заверенное согласие или доверенность);",
                                        "документ, подтверждающий отнесение лица из числа детей-сирот и детей, оставшихся без попечения родителей, к категории лиц из числа детей-сирот и детей, оставшихся без попечения родителей."

                                    };


                        }



                    }

                    //if (benefits.Count() > 0 || (request.TypeOfRestId == (long)TypeOfRestEnum.YouthRestOrphanCamps || request.TypeOfRestId == (long)TypeOfRestEnum.CompensationYouthRest))
                    //{
                    //    docs.Clear();
                    //    docs.Add("документ, удостоверяющий личность заявителя;");

                    //    docs.Add("документ, подтверждающий место жительства ребенка в городе Москве;");
                    //    docs.Add("документ, подтверждающий отнесение ребенка к категории, указанной в заявлении;");
                    //    docs.Add("документы, подтверждающие, что заявитель является родителем (законным представителем) ребенка: свидетельство о рождении ребенка*, договор о приемной семье, распоряжение об опеке, иные документы, устанавливающие полномочия законного представителя ребенка;");
                    //    docs.Add("документ, подтверждающий полномочия доверенного лица на совершение действий в период проведения заявочной кампании (в случае подачи заявления о предоставлении услуг отдыха и оздоровления с использованием Портала доверенным лицом на совершение действий в период проведения заявочной кампании) (нотариально заверенное согласие или доверенность);");

                    //    docs.Add("* в случае если с момента рождения ребенка у родителя произошла смена фамилии, имени или отчества – необходимо также предоставить документы, подтверждающие данные изменения: свидетельство о браке, свидетельство о расторжении брака, свидетельство о перемене имени");
                    //    foreach (BenefitType benefit in benefits)
                    //    {
                    //        if (benefit.ExnternalUid.Contains("52"))//дети-сироты и оставшиеся без попечения родителей...
                    //        {
                    //            List<string> innerList = new List<string>
                    //            {
                    //                "документ, удостоверяющий личность ребенка;",
                    //                "документ, подтверждающий полномочия заявителя, сопровождающего лица(в случае организации совместного выездного отдыха) из числа законных представителей – опекунов, попечителей, приемных родителей, патронатных воспитателей ребенка(договор о приемной семье, распоряжение об опеке, иные документы, устанавливающие статус ребенка);",
                    //                "документ, подтверждающий полномочия доверенного лица для сопровождения во время отдыха и оздоровления (в случае организации совместного выездного отдыха и сопровождения ребенка доверенным лицом для сопровождения во время отдыха и оздоровления и подачи заявления о предоставлении услуг отдыха и оздоровления с использованием Портала) (нотариально заверенное согласие или доверенность);",

                    //            };


                    //            docs.InsertRange(4, innerList);
                    //            docs.Remove("документ, подтверждающий отнесение ребенка к категории, указанной в заявлении;");
                    //            docs.Remove("* в случае если с момента рождения ребенка у родителя произошла смена фамилии, имени или отчества – необходимо также предоставить документы, подтверждающие данные изменения: свидетельство о браке, свидетельство о расторжении брака, свидетельство о перемене имени");
                    //        }
                    //        if (benefit.ExnternalUid.Contains("24"))//дети-инвалиды, дети с ограниченными возможностями...
                    //        {
                    //            docs.Insert(1, "документ, удостоверяющий личность ребенка;");
                    //            docs.Insert(3, "документ, подтверждающий отнесение ребенка к категории, указанной в заявлении (заключение медико-социальной экспертизы или заключение Центральной психолого-медико-педагогической комиссии города Москвы**);");
                    //            docs.Insert(5, "документ, подтверждающий полномочия доверенного лица для сопровождения во время отдыха и оздоровления (в случае организации совместного выездного отдыха и сопровождения ребенка доверенным лицом для сопровождения во время отдыха и оздоровления и подачи заявления о предоставлении услуг отдыха и оздоровления с использованием Портала) (нотариально заверенное согласие или доверенность);");
                    //            docs.Remove("документ, подтверждающий отнесение ребенка к категории, указанной в заявлении;");
                    //            docs.Add("** является документом, подтвержающим категорию, только в случае указания на необходимость предоставления ребенку специальных условий обучения по адаптированной основной образовательной программе");
                    //        }
                    //        if (benefit.ExnternalUid.Contains("48"))//дети из малообеспеченных семей
                    //        {

                    //            docs.Insert(1, "документ, удостоверяющий личность ребенка;");
                    //            docs.Insert(5, "документ, подтверждающий полномочия доверенного лица для сопровождения во время отдыха и оздоровления (в случае организации совместного выездного отдыха и сопровождения ребенка доверенным лицом для сопровождения во время отдыха и оздоровления и подачи заявления о предоставлении услуг отдыха и оздоровления с использованием Портала) (нотариально заверенное согласие или доверенность);");
                    //            docs.Remove("документ, подтверждающий отнесение ребенка к категории, указанной в заявлении;");
                    //        }

                    //    }

                    //    if (request.TypeOfRestId == (long)TypeOfRestEnum.YouthRestOrphanCamps || request.TypeOfRestId == (long)TypeOfRestEnum.CompensationYouthRest)//лица из числа детей-сирот и детей, оставших без попечения...
                    //    {

                    //        docs.Insert(1, "документ, подтверждающий место жительства лица из числа детей-сирот и детей, оставшихся без попечения родителей, в городе Москве;");
                    //        docs.Insert(2, "документ, подтверждающий отнесение лица из числа детей-сирот и детей, оставшихся без попечения родителей, к категории лиц из числа детей-сирот и детей, оставшихся без попечения родителей.");
                    //        docs.Remove("документ, подтверждающий место жительства ребенка в городе Москве;");
                    //        docs.Remove("документы, подтверждающие, что заявитель является родителем (законным представителем) ребенка: свидетельство о рождении ребенка*, договор о приемной семье, распоряжение об опеке, иные документы, устанавливающие полномочия законного представителя ребенка;");
                    //        docs.Remove("документ, подтверждающий отнесение ребенка к категории, указанной в заявлении;");
                    //        docs.Remove("* в случае если с момента рождения ребенка у родителя произошла смена фамилии, имени или отчества – необходимо также предоставить документы, подтверждающие данные изменения: свидетельство о браке, свидетельство о расторжении брака, свидетельство о перемене имени");
                    //    }
                    //}

                    //foreach (var docText in docs)
                    //{
                    //    PdfAddParagraph(document, 0, 1, Element.ALIGN_JUSTIFIED, firstLine_small, new Chunk(docText, MainItalicText));
                    //}

                    ////PdfAddParagraph(document, 0, 1, Element.ALIGN_JUSTIFIED, firstLine_small, new Chunk("документ, удостоверяющий личность заявителя;", MainItalicText));
                    ////PdfAddParagraph(document, 0, 1, Element.ALIGN_JUSTIFIED, firstLine_small, new Chunk("документы, подтверждающие, что заявитель является родителем ребёнка (свидетельство о рождении ребёнка, в случае если с момента рождения ребенка у родителя произошла смена фамилии, имени или отчества – необходимо также предоставить документы, подтверждающие данные изменения);", MainItalicText));
                    ////PdfAddParagraph(document, 0, 1, Element.ALIGN_JUSTIFIED, firstLine_small, new Chunk("документ, подтверждающий место жительства ребёнка, лица из числа детей-сирот и детей, оставшихся без попечения родителей, в городе Москве;", MainItalicText));
                    ////PdfAddParagraph(document, 0, 1, Element.ALIGN_JUSTIFIED, firstLine_small, new Chunk("документ, подтверждающий полномочия заявителя, сопровождающего лица (в случае организации совместного выездного отдыха) из числа законных представителей – родителей, опекунов, попечителей, приемных родителей, патронатных воспитателей ребёнка (договор о приемной семье, распоряжение об опеке, иные документы, устанавливающие статус ребенка);", MainItalicText));
                    ////PdfAddParagraph(document, 0, 1, Element.ALIGN_JUSTIFIED, firstLine_small, new Chunk("документ, подтверждающий полномочия доверенного лица на совершение действий в период проведения заявочной кампании (в случае подачи заявления о предоставлении услуг отдыха и оздоровления с использованием Портала доверенным лицом на совершение действий в период проведения заявочной кампании) (нотариально заверенное согласие или доверенность);", MainItalicText));
                    ////PdfAddParagraph(document, 0, 1, Element.ALIGN_JUSTIFIED, firstLine_small, new Chunk("документ, подтверждающий полномочия доверенного лица для сопровождения во время отдыха и оздоровления (в случае организации совместного выездного отдыха и сопровождения ребёнка доверенным лицом для сопровождения во время отдыха и оздоровления и подачи заявления о предоставлении услуг отдыха и оздоровления с использованием Портала) (нотариально заверенное согласие или доверенность);", MainItalicText));
                    ////PdfAddParagraph(document, 0, 1, Element.ALIGN_JUSTIFIED, firstLine_small, new Chunk("документ, подтверждающий отнесение ребёнка, к одной из категорий детей, находящихся в трудной жизненной ситуации и указанных в пунктах 3.1.3, 3.1.5 - 3.1.13 Порядка, лица из числа детей-сирот к категории лиц из числа детей-сирот и детей, оставшихся без попечения родителей (заключение медико-социальной экспертизы, заключение Центральной психолого-медико-педагогической комиссии города Москвы, справки уполномоченного учреждения социальной защиты населения города Москвы и/или федеральных органов).", MainItalicText));

                    if (innerListOrphans.Count > 0)
                    {
                        PdfAddParagraph(document, 0, 1, Element.ALIGN_JUSTIFIED, firstLine_small, new Chunk("Дети-сироты и дети, оставшиеся без попечения родителей, находящиеся под опекой, попечительством, в том числе в приёмной или патронатной семье", HeaderFont));
                        
                        foreach (var docText in innerListOrphans)
                        {

                            PdfAddParagraph(document, 0, 1, Element.ALIGN_JUSTIFIED, firstLine_small, new Chunk(docText, MainItalicText));

                        }

                    }

                    if (innerListDisabled.Count > 0)
                    {
                        PdfAddParagraph(document, 0, 1, Element.ALIGN_JUSTIFIED, firstLine_small, new Chunk("Дети-инвалиды и дети с ограниченными возможностями здоровья", HeaderFont));

                        foreach (var docText in innerListDisabled)
                        {

                            PdfAddParagraph(document, 0, 1, Element.ALIGN_JUSTIFIED, firstLine_small, new Chunk(docText, MainItalicText));
                        }

                    }

                    if (innerListLowIncome.Count > 0)
                    {
                        PdfAddParagraph(document, 0, 1, Element.ALIGN_JUSTIFIED, firstLine_small, new Chunk("Дети из малообеспеченных семей", HeaderFont));

                        foreach (var docText in innerListLowIncome)
                        {


                            PdfAddParagraph(document, 0, 1, Element.ALIGN_JUSTIFIED, firstLine_small, new Chunk(docText, MainItalicText));
                        }

                    }

                    if (innerListSacrifice.Count > 0)
                    {
                        PdfAddParagraph(document, 0, 1, Element.ALIGN_JUSTIFIED, firstLine_small, new Chunk("Дети - жертвы вооруженных и межнациональных конфликтов, экологических и техногенных катастроф, стихийных бедствий", HeaderFont));

                        foreach (var docText in innerListSacrifice)
                        {

                            PdfAddParagraph(document, 0, 1, Element.ALIGN_JUSTIFIED, firstLine_small, new Chunk(docText, MainItalicText));
                        }

                    }

                    if (innerListRefugee.Count > 0)
                    {
                        PdfAddParagraph(document, 0, 1, Element.ALIGN_JUSTIFIED, firstLine_small, new Chunk("Дети из семей беженцев и вынужденных переселенцев", HeaderFont));

                        foreach (var docText in innerListRefugee)
                        {

                            PdfAddParagraph(document, 0, 1, Element.ALIGN_JUSTIFIED, firstLine_small, new Chunk(docText, MainItalicText));
                        }

                    }

                    if (innerListExtreme.Count > 0)
                    {
                        PdfAddParagraph(document, 0, 1, Element.ALIGN_JUSTIFIED, firstLine_small, new Chunk("Дети, оказавшиеся в экстремальных условиях", HeaderFont));

                        foreach (var docText in innerListExtreme)
                        {

                            PdfAddParagraph(document, 0, 1, Element.ALIGN_JUSTIFIED, firstLine_small, new Chunk(docText, MainItalicText));
                        }

                    }

                    if (innerListViolence.Count > 0)
                    {
                        PdfAddParagraph(document, 0, 1, Element.ALIGN_JUSTIFIED, firstLine_small, new Chunk("Дети - жертвы насилия", HeaderFont));

                        foreach (var docText in innerListViolence)
                        {

                            PdfAddParagraph(document, 0, 1, Element.ALIGN_JUSTIFIED, firstLine_small, new Chunk(docText, MainItalicText));
                        }

                    }

                    if (innerListInvalid.Count > 0)
                    {
                        PdfAddParagraph(document, 0, 1, Element.ALIGN_JUSTIFIED, firstLine_small, new Chunk("Дети, жизнедеятельность которых объективно нарушена в результате сложившихся обстоятельств и которые не могут преодолеть данные обстоятельства самостоятельно или с помощью семьи", HeaderFont));

                        foreach (var docText in innerListInvalid)
                        {


                            PdfAddParagraph(document, 0, 1, Element.ALIGN_JUSTIFIED, firstLine_small, new Chunk(docText, MainItalicText));
                        }

                    }

                    if (innerListTerror.Count > 0)
                    {
                        PdfAddParagraph(document, 0, 1, Element.ALIGN_JUSTIFIED, firstLine_small, new Chunk("Дети, пострадавшие в результате террористических актов", HeaderFont));

                        foreach (var docText in innerListTerror)
                        {

                            PdfAddParagraph(document, 0, 1, Element.ALIGN_JUSTIFIED, firstLine_small, new Chunk(docText, MainItalicText));
                        }

                    }

                    if (innerListMilitary.Count > 0)
                    {
                        PdfAddParagraph(document, 0, 1, Element.ALIGN_JUSTIFIED, firstLine_small, new Chunk("Дети из семей военнослужащих и приравненных к ним лиц, погибших или получивших увечья(ранения, травмы, контузии) при исполнении ими обязанностей военной службы или служебных обязанностей", HeaderFont));

                        foreach (var docText in innerListMilitary)
                        {

                            PdfAddParagraph(document, 0, 1, Element.ALIGN_JUSTIFIED, firstLine_small, new Chunk(docText, MainItalicText));
                        }

                    }

                    if (innerListInvalidParents.Count > 0)
                    {
                        PdfAddParagraph(document, 0, 1, Element.ALIGN_JUSTIFIED, firstLine_small, new Chunk("Дети из семей, в которых оба или один родитель являются инвалидами", HeaderFont));

                        foreach (var docText in innerListInvalidParents)
                        {

                            PdfAddParagraph(document, 0, 1, Element.ALIGN_JUSTIFIED, firstLine_small, new Chunk(docText, MainItalicText));
                        }

                    }

                    if (innerListDeviant.Count > 0)
                    {
                        PdfAddParagraph(document, 0, 1, Element.ALIGN_JUSTIFIED, firstLine_small, new Chunk("Дети с отклонениями в поведении", HeaderFont));

                        foreach (var docText in innerListDeviant)
                        {

                            PdfAddParagraph(document, 0, 1, Element.ALIGN_JUSTIFIED, firstLine_small, new Chunk(docText, MainItalicText));
                        }

                    }

                    if (innerListOrphansYouth.Count > 0)
                    {
                        PdfAddParagraph(document, 0, 1, Element.ALIGN_JUSTIFIED, firstLine_small, new Chunk("Лица из числа детей-сирот и детей, оставшихся без попечения родителей", HeaderFont));

                        foreach (var docText in innerListOrphansYouth)
                        {

                            PdfAddParagraph(document, 0, 1, Element.ALIGN_JUSTIFIED, firstLine_small, new Chunk(docText, MainItalicText));
                        }

                    }


                    PdfAddParagraph(document, WordProcessor.Space, 0, 1, Element.ALIGN_LEFT, 0, MainText);

                    PdfAddParagraph(document, 0, 1, Element.ALIGN_JUSTIFIED, 0, new Chunk("Неявка с соответствующими документами в течение срока приостановления рассмотрения заявления является основанием для отказа в предоставлении услуг отдыха и оздоровления.", MainText));


                    //SignWorkerBlock(document, account);

                    document.Close();
                }

                return new DocumentResult
                {
                    FileBody = ms.ToArray(),
                    FileName = "Уведомление о приостановлении рассмотрения заявления.pdf",
                    MimeType = MimeType,
                    MimeTypeShort = Extension
                };
            }
        }

        /// <summary>
        ///     Уведомление о необходимости выбора организации отдыха и оздоровления
        /// </summary>
        internal static IDocument NotificationOrgChoose(Request request)
        {
            var applicant = request.Applicant ?? new Applicant {DocumentType = new DocumentType {Name = string.Empty}};

            using (var ms = new MemoryStream())
            {
                using (var document = new Document(PageSize.A4, 60, 40, 40, 40))
                {
                    var writer = PdfWriter.GetInstance(document, ms);
                    document.Open();
                    writer.SpaceCharRatio = PdfWriter.NO_SPACE_CHAR_RATIO;

                    PdfAddHeader(document);

                    PdfAddParagraph(document, WordProcessor.Space, 0, 1, Element.ALIGN_LEFT, 0, MainText);
                    PdfAddParagraph(document, WordProcessor.Space, 0, 1, Element.ALIGN_LEFT, 0, MainText);

                    PdfAddParagraph(document, "Уведомление о необходимости выбора организации отдыха и оздоровления", 0, 1, Element.ALIGN_CENTER, 0, TitleFont);

                    PdfAddParagraph(document, WordProcessor.Space, 0, 1, Element.ALIGN_LEFT, 0, MainText);

                    PdfAddParagraph(document, 0, 1, Element.ALIGN_CENTER, 0, new Chunk($"Уважаемый(ая) {applicant.LastName} {applicant.FirstName} {applicant.MiddleName},", MainText));
                    PdfAddParagraph(document, $"Ваше заявление от {request.DateRequest.FormatEx("dd.MM.yyyy")} г. № {request.RequestNumber} о предоставлении услуг отдыха и оздоровления (далее – заявление) рассмотрено.", 0, 1, Element.ALIGN_JUSTIFIED, 0, MainText);

                    PdfAddParagraph(document, 0, 1, Element.ALIGN_JUSTIFIED, firstLine_small, new Chunk("В период второго этапа заявочной кампании (с 7 по 21 февраля 2022 г.) Вам необходимо дополнить Ваше заявление сведениями о конкретной организации отдыха и оздоровления. Выбор конкретной организации отдыха и оздоровления осуществляется из числа предлагаемых ГАУК \"МОСГОРТУР\" в соответствии с указанными Вами на первом этапе заявочной кампании сведениями о приоритетном времени, направлении отдыха и оздоровления и количестве детей.", MainText)); ;

                    PdfAddParagraph(document, 0, 1, Element.ALIGN_JUSTIFIED, firstLine_small, new Chunk("В случае подачи заявления через подсистему \"личный кабинет\" Портала Мэра и Правительства Москвы mos.ru (далее – подсистема \"личный кабинет\" Портала), дополнение формы заявления сведениями о конкретной организации отдыха и оздоровления осуществляется в подсистеме \"личный кабинет\" Портала.", MainText));

                    PdfAddParagraph(document, 0, 1, Element.ALIGN_JUSTIFIED, firstLine_small, new Chunk("В случае подачи заявления при личном обращении в офисе ГАУК \"МОСГОРТУР\", дополнение формы заявления сведениями о конкретной организации отдыха и оздоровления возможно только при личном обращении заявителя в офис ГАУК \"МОСГОРТУР\" по адресу: г. Москва, Малый Харитоньевский переулок д. 6, стр. 3.", MainText));

                    PdfAddParagraph(document, 0, 1, Element.ALIGN_JUSTIFIED, firstLine_small, new Chunk("Прием в офисе ГАУК \"МОСГОРТУР\" осуществляется исключительно по предварительной записи.", HeaderFont),
                        new Chunk(" Запись производится через Портал Мэра и Правительства Москвы mos.ru  (далее – Портал) или при личном визите заявителя в офис ГАУК \"МОСГОРТУР\". Запись производится на свободную дату и время.", MainText));

                    PdfAddParagraph(document, 0, 1, Element.ALIGN_JUSTIFIED, firstLine_small, new Chunk("В случае если на втором этапе заявочной кампании Вас не устроит ни один из вариантов организаций отдыха и оздоровления, предлагаемых ГАУК \"МОСГОРТУР\", на втором этапе заявочной кампании в период с 7 по 21 февраля 2022 г. Вам необходимо отказаться от всех предложенных организаций отдыха и оздоровления.", MainText));

                    PdfAddParagraph(document, 0, 1, Element.ALIGN_JUSTIFIED, firstLine_small, new Chunk("При подаче заявления через подсистему \"личный кабинет\" Портала для отказа от всех предложенных организаций отдыха и оздоровления необходимо нажать соответствующую \"галочку\" в интерактивном поле Портала: \"Я отказываюсь от предложенных вариантов организаций отдыха и оздоровления\".", MainText));

                    PdfAddParagraph(document, 0, 1, Element.ALIGN_JUSTIFIED, firstLine_small, new Chunk("При подаче заявления в офисе ГАУК \"МОСГОРТУР\" для отказа от всех предложенных организаций отдыха и оздоровления необходимо лично обратиться в офис ГАУК \"МОСГОРТУР\".", MainText));

                    document.Close();
                }

                return new DocumentResult
                {
                    FileBody = ms.ToArray(),
                    FileName = "Уведомление о необходимости выбора организации отдыха и оздоровления.pdf",
                    MimeType = MimeType,
                    MimeTypeShort = Extension
                };
            }
        }

        /// <summary>
        ///     Уведомление о прекращении рассмотрения поданного заявления о предоставлении услуг отдыха и оздоровления (отзыв)
        /// </summary>
        internal static IDocument NotificationRefuse1090(Request request)
        {
            var applicant = request.Applicant ?? new Applicant { DocumentType = new DocumentType { Name = string.Empty } };

            using (var ms = new MemoryStream())
            {
                using (var document = new Document(PageSize.A4, 60, 40, 40, 40))
                {
                    var writer = PdfWriter.GetInstance(document, ms);
                    document.Open();
                    writer.SpaceCharRatio = PdfWriter.NO_SPACE_CHAR_RATIO;

                    PdfAddHeader(document);

                    PdfAddParagraph(document, WordProcessor.Space, 0, 1, Element.ALIGN_LEFT, 0, MainText);
                    PdfAddParagraph(document, WordProcessor.Space, 0, 1, Element.ALIGN_LEFT, 0, MainText);

                    PdfAddParagraph(document, "Уведомление о прекращении рассмотрения поданного заявления\nо предоставлении услуг отдыха и оздоровления", 0, 1, Element.ALIGN_CENTER, 0, TitleFont);

                    PdfAddParagraph(document, WordProcessor.Space, 0, 1, Element.ALIGN_LEFT, 0, MainText);

                    PdfAddParagraph(document, 0, 1, Element.ALIGN_LEFT, 0, new Chunk("Данные заявителя: ", HeaderFont),
                        new Chunk(
                            $"{applicant.LastName} {applicant.FirstName} {applicant.MiddleName}, {applicant.DateOfBirth.FormatExGR(string.Empty)}",
                            MainItalicText));
                    PdfAddParagraph(document, 0, 1, Element.ALIGN_LEFT, 0,
                        new Chunk("Контактная информация: ", HeaderFont),
                        new Chunk(applicant.Phone + ", " + applicant.Email, MainItalicText));

                    PdfAddParagraph(document, 0, 1, Element.ALIGN_JUSTIFIED, 0, new Chunk("Причина обращения: ", HeaderFont),
                        new Chunk("прекращение рассмотрения поданного заявления о предоставлении услуг отдыха и оздоровления по инициативе заявителя.", MainText));

                    PdfAddParagraph(document, 0, 1, Element.ALIGN_JUSTIFIED, 0, new Chunk("Основание обращения: ", HeaderFont),
                        new Chunk($"пункт 5.13 Порядка организации отдыха и оздоровления детей, находящихся в трудной жизненной ситуации, утвержденного постановлением Правительства Москвы от 22 февраля 2017 г. № 56-ПП \"Об организации отдыха и оздоровления детей, находящихся в трудной жизненной ситуации\": \"Заявитель вправе отозвать заявление о предоставлении услуг отдыха и оздоровления в срок не позднее дня окончания стадии заявочной кампании по приему заявлений о предоставлении услуг отдыха и оздоровления (до 10 декабря {DateTime.Now.Year} г. включительно)\".", MainText));

                    PdfAddParagraph(document, 0, 1, Element.ALIGN_JUSTIFIED, 0, new Chunk("Информация о заявлении:", HeaderFont));

                    PdfAddParagraph(document, 0, 1, Element.ALIGN_JUSTIFIED, firstLine, new Chunk("дата и время регистрации заявления: ", HeaderFont),
                        new Chunk(request.DateRequest.FormatEx(), MainText));

                    PdfAddParagraph(document, 0, 1, Element.ALIGN_JUSTIFIED, firstLine, new Chunk("номер заявления: ", HeaderFont),
                        new Chunk(request.RequestNumber.FormatEx(), MainText));

                    PdfAddParagraph(document, 0, 1, Element.ALIGN_JUSTIFIED, 0, new Chunk("Результат рассмотрения заявления: ", HeaderFont),
                        new Chunk("отозвано по инициативе заявителя в установленный срок.", MainText));

                    PdfAddParagraph(document, 0, 1, Element.ALIGN_JUSTIFIED, 0, new Chunk("Дата прекращения рассмотрения заявления: ", HeaderFont),
                        new Chunk(request.DateChangeStatus.FormatEx(string.Empty), MainText));

                    document.Close();
                }

                return new DocumentResult
                {
                    FileBody = ms.ToArray(),
                    FileName = "Уведомление о прекращении рассмотрения поданного заявления о предоставлении услуг отдыха и оздоровления.pdf",
                    MimeType = MimeType,
                    MimeTypeShort = Extension
                };
            }
        }

        /// <summary>
        ///     Уведомление о отказе в регистрации заявления (1035)
        /// </summary>
        internal static IDocument NotificationRegistrationDecline(Request request)
        {
            var applicant = request.Applicant ?? new Applicant {DocumentType = new DocumentType {Name = string.Empty}};

            using (var ms = new MemoryStream())
            {
                using (var document = new Document(PageSize.A4, 60, 40, 40, 40))
                {
                    var writer = PdfWriter.GetInstance(document, ms);
                    document.Open();
                    writer.SpaceCharRatio = PdfWriter.NO_SPACE_CHAR_RATIO;

                    PdfAddHeader(document);

                    PdfAddParagraph(document, WordProcessor.Space, 0, 1, Element.ALIGN_LEFT, 0, MainText);
                    PdfAddParagraph(document, WordProcessor.Space, 0, 1, Element.ALIGN_LEFT, 0, MainText);

                    PdfAddParagraph(document,
                        "Уведомление об отказе в регистрации заявления о предоставлении услуг отдыха и оздоровления",
                        0, 1, Element.ALIGN_CENTER, 0, TitleFont);

                    PdfAddParagraph(document, WordProcessor.Space, 0, 1, Element.ALIGN_LEFT, 0, MainText);

                    PdfAddParagraph(document, 0, 1, Element.ALIGN_LEFT, 0,
                        new Chunk("Дата обращения: ", HeaderFont),
                        new Chunk(request.DateRequest.FormatEx(), MainItalicText));

                    PdfAddParagraph(document, 0, 1, Element.ALIGN_LEFT, 0, new Chunk("Данные обратившегося лица: ", HeaderFont),
                        new Chunk(
                            $"{applicant.LastName} {applicant.FirstName} {applicant.MiddleName}, {applicant.DateOfBirth.FormatExGR(string.Empty)}",
                            MainItalicText));
                    PdfAddParagraph(document, 0, 1, Element.ALIGN_LEFT, 0,
                        new Chunk("Контактная информация: ", HeaderFont),
                        new Chunk(applicant.Phone + ", " + applicant.Email, MainItalicText));

                    PdfAddParagraph(document, 0, 1, Element.ALIGN_LEFT, 0,
                        new Chunk("Цель обращения: ", HeaderFont),
                        new Chunk("получение услуг отдыха и оздоровления", MainItalicText));

                    PdfAddParagraph(document, 0, 1, Element.ALIGN_LEFT, 0,
                        new Chunk("Вид услуги: ", HeaderFont),
                        new Chunk(request.TypeOfRest?.Name, MainItalicText));

                    foreach (var child in request.Child?.Where(c => !c.IsDeleted) ?? new List<Child>())
                    {
                        PdfAddParagraph(document, 0, 1, Element.ALIGN_LEFT, 0,
                            new Chunk(
                                "Данные ребёнка/лица из числа детей-сирот и детей, оставшихся без попечения родителей:\n",
                                HeaderFont),
                            new Chunk(
                                $"{child.LastName} {child.FirstName} {child.MiddleName}, {child.DateOfBirth.FormatExGR(string.Empty)}",
                                MainItalicText));
                    }

                    if (request.TypeOfRestId == (long)TypeOfRestEnum.YouthRestCamps || request.TypeOfRestId == (long)TypeOfRestEnum.YouthRestOrphanCamps)
                    {
                        PdfAddParagraph(document, 0, 1, Element.ALIGN_LEFT, 0,
                            new Chunk(
                                "Данные ребёнка/лица из числа детей-сирот и детей, оставшихся без попечения родителей:\n",
                                HeaderFont),
                            new Chunk(
                                $"{applicant.LastName} {applicant.FirstName} {applicant.MiddleName}, {applicant.DateOfBirth.FormatExGR(string.Empty)}",
                                MainItalicText));
                    }

                    PdfAddParagraph(document, 0, 1, Element.ALIGN_JUSTIFIED, 0,
                        new Chunk("Основание отказа в регистрации заявления: ", HeaderFont),
                        new Chunk("Порядок организации отдыха и оздоровления детей, находящихся в трудной жизненной ситуации, утвержденный постановлением Правительства Москвы от 22 февраля 2017 г. № 56-ПП \"Об организации отдыха и оздоровления детей, находящихся в трудной жизненной ситуации\" (далее — Порядок).", MainText));

                    PdfAddParagraph(document, 0, 1, Element.ALIGN_JUSTIFIED, 0,
                        new Chunk("Причина отказа в регистрации заявления: ", HeaderFont),
                        //new Chunk("пункт 5.11.1 Порядка: \"Наличие в отношении одного и того же ребёнка, лица из числа детей - сирот и детей, оставшихся без попечения родителей, другого заявления о предоставлении услуг отдыха и оздоровления в текущем календарном году\".", MainText));
                        new Chunk(request.DeclineReason.Name, MainText));
                    document.Close();
                }

                return new DocumentResult
                {
                    FileBody = ms.ToArray(),
                    FileName = "Уведомление об отказе в регистрации заявления.pdf",
                    MimeType = MimeType,
                    MimeTypeShort = Extension
                };

            }
        }

        #region Utils

        /// <summary>
        ///     Название месяца по дате
        /// </summary>
        internal static string GetDayMonth(DateTime? date)
        {
            if (!date.HasValue)
            {
                return string.Empty;
            }

            var day = (date.Value.Day < 10 ? "0" : string.Empty) + date.Value.Day;

            var months = new[]
            {
                "января", "февраля", "марта", "апреля", "мая", "июня", "июля", "августа", "сентября", "октября",
                "ноября",
                "декабря"
            };

            return $"{day} {months[date.Value.Month - 1]} {date.Value.Year} г.";
        }

        /// <summary>
        ///     Добавить параграф
        /// </summary>
        private static void PdfAddParagraph(Document document, string text, float spacingBefore, float spacingAfter,
            int alignment, float firstLineIndent, Font font)
        {
            var p = new Paragraph(text, font)
            {
                SpacingBefore = spacingBefore,
                SpacingAfter = spacingAfter,
                Alignment = alignment,
                FirstLineIndent = firstLineIndent
            };
            document.Add(p);
        }

        /// <summary>
        ///     Добавить параграф
        /// </summary>
        private static void PdfAddParagraph(Document document, float spacingBefore, float spacingAfter, int alignment,
            float firstLineIndent, params Chunk[] chunks)
        {
            var phrase = new Phrase();
            phrase.AddRange(chunks);

            var p = new Paragraph(phrase)
            {
                SpacingBefore = spacingBefore,
                SpacingAfter = spacingAfter,
                Alignment = alignment,
                FirstLineIndent = firstLineIndent
            };
            document.Add(p);
        }

        /// <summary>
        ///     Шапка документа
        /// </summary>
        private static void PdfAddHeader(Document document)
        {
            PdfAddParagraph(document, "ДЕПАРТАМЕНТ КУЛЬТУРЫ ГОРОДА МОСКВЫ", 0, 1, Element.ALIGN_CENTER, 0, HeaderFont);
            PdfAddParagraph(document, "Государственное автономное учреждение культуры города Москвы", 0, 1,
                Element.ALIGN_CENTER, 0, HeaderFont);
            PdfAddParagraph(document, "\"Московское агентство организации отдыха и туризма\"", 0, 1,
                Element.ALIGN_CENTER, 0, HeaderFont);
            PdfAddParagraph(document, "(ГАУК \"МОСГОРТУР\")", 0, 1, Element.ALIGN_CENTER, 0, HeaderFont);
            var p = new Paragraph(new Chunk(new LineSeparator(0.0F, 100.0F, BaseColor.BLACK, Element.ALIGN_LEFT, 1)));
            document.Add(p);
        }

        private static void GetPdfTable(List<Request> requests, Document doc, IEnumerable<int>years)
        {
            PdfPTable pdfTable = new PdfPTable(3);
            pdfTable.SetWidthPercentage(new float[3] { 100, 220, 280 }, PageSize.A4);
            pdfTable.NormalizeHeadersFooters();
            pdfTable.SplitLate = false;
            //pdfTable.DefaultCell.Padding = 3;
            //pdfTable.WidthPercentage = 30;
            //pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfTable.DefaultCell.BorderWidth = 0.5f;

            PdfPCell pdfPCell; // Создаем ячейку

            pdfPCell = new PdfPCell(new Phrase("Год кампании", HeaderFont));
            pdfPCell.BorderColor = new BaseColor(0, 0, 0); // Цвет границы ячейки, по умолчанию черный
            pdfPCell.BackgroundColor = new BaseColor(255, 255, 255); // Цвет фона ячейки
            pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
            pdfPCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfTable.AddCell(pdfPCell);

            pdfPCell = new PdfPCell(new Phrase("Вид услуги (путевка, сертификат, компенсация)", HeaderFont));
            pdfPCell.BorderColor = new BaseColor(0, 0, 0); // Цвет границы ячейки, по умолчанию черный
            pdfPCell.BackgroundColor = new BaseColor(255, 255, 255); // Цвет фона ячейки
            pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
            pdfPCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfTable.AddCell(pdfPCell);

            pdfPCell = new PdfPCell(new Phrase("Организация отдыха и оздоровления (в случае предоставления путевки для отдыха и оздоровления), даты заезда", HeaderFont));
            pdfPCell.BorderColor = new BaseColor(0, 0, 0); // Цвет границы ячейки, по умолчанию черный
            pdfPCell.BackgroundColor = new BaseColor(255, 255, 255); // Цвет фона ячейки
            pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
            pdfPCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfTable.AddCell(pdfPCell);

            var moneyTypes = new[]
{
                (long?) TypeOfRestEnum.MoneyOn18, (long?) TypeOfRestEnum.MoneyOn3To7,
                (long?) TypeOfRestEnum.MoneyOn7To15, (long?) TypeOfRestEnum.MoneyOnInvalidOn4To17
            };

            foreach (int year in years)
            {
                Request request = requests.FirstOrDefault(req => req.YearOfRest.Year == year);

                pdfPCell = new PdfPCell(new Phrase(year.ToString(), HeaderFont));
                pdfPCell.BorderColor = new BaseColor(0, 0, 0);
                pdfPCell.BackgroundColor = new BaseColor(255, 255, 255);
                pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
                pdfPCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                pdfTable.AddCell(pdfPCell);

                pdfPCell = new PdfPCell(new Phrase(request?.TypeOfRest?.Name, HeaderFont));
                pdfPCell.BorderColor = new BaseColor(0, 0, 0);
                pdfPCell.BackgroundColor = new BaseColor(255, 255, 255);
                pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
                pdfPCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                pdfTable.AddCell(pdfPCell);

                pdfPCell = new PdfPCell(new Phrase(request == null ? "" :
                            request.TourId.HasValue
                            ? $"{request.Tour.Hotels?.Name}, c {request.Tour.DateIncome.FormatEx(string.Empty)} по {request.Tour.DateOutcome.FormatEx(string.Empty)}": (request.RequestOnMoney && !moneyTypes.Contains(request.TypeOfRestId)? "Осуществлен выбор сертификата на втором этапе заявочной кампании": ""), HeaderFont));
                pdfPCell.BorderColor = new BaseColor(0, 0, 0);
                pdfPCell.BackgroundColor = new BaseColor(255, 255, 255);
                pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
                pdfPCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                pdfTable.AddCell(pdfPCell);


            }

            doc.Add(pdfTable);
        }


        /// <summary>
        ///     Подпись работника
        /// </summary>
        private static void SignWorkerBlock(Document document, Account account, string name = "Принял:")//костыль, который надо обязательно переделать
        {
            
            account = account ?? new Account();
            var titleRequestRunProperties = new RunProperties();
            titleRequestRunProperties.AppendChild(new RunFonts
            {
                Ascii = "Times New Roman",
                HighAnsi = "Times New Roman",
                ComplexScript = "Times New Roman"
            });
            titleRequestRunProperties.AppendChild(new FontSize { Val = "22" });

            var tblProp = new TableProperties(
                new TableBorders(
                    new TopBorder { Val = new EnumValue<BorderValues>(BorderValues.None) },
                    new BottomBorder { Val = new EnumValue<BorderValues>(BorderValues.None) },
                    new LeftBorder { Val = new EnumValue<BorderValues>(BorderValues.None) },
                    new RightBorder { Val = new EnumValue<BorderValues>(BorderValues.None) },
                    new InsideHorizontalBorder { Val = new EnumValue<BorderValues>(BorderValues.None) },
                    new InsideVerticalBorder { Val = new EnumValue<BorderValues>(BorderValues.None) }));

            var captionRunProperties = new RunProperties().SetFont().SetFontSizeSupperscript();



            var table = new Table();
            table.AppendChild(tblProp.CloneNode(true));

            //отступ - нулевая строка
            PdfAddParagraph(document, 0, 20, Element.ALIGN_CENTER, 0);
                //document.AppendChild(new Paragraph(
                //new ParagraphProperties(new Justification { Val = JustificationValues.Both },
                //new SpacingBetweenLines { After = Size20 })));


            PdfAddParagraph(document, 0, 0, Element.ALIGN_JUSTIFIED, 0,
                            new Chunk("Исполнитель: ", MainText),
                            new Chunk("                                             ", MainText),
                            new Chunk("_________________________", MainText),
                            new Chunk("     ", MainText),
                            new Chunk("_________________________", MainText));
            PdfAddParagraph(document, 0, 0, Element.ALIGN_JUSTIFIED, 0,
                            new Chunk("                  ", SmallText),
                            new Chunk("                                                                                           ", SmallText),
                            new Chunk("(ФИО работника, должность)", SmallText),
                            new Chunk("                  ", SmallText),
                            new Chunk("      (подпись работника)   ", SmallText));

            
            //var row = new TableRow();

            //var cell = new TableCell();
            //cell.Append(new TableCellProperties(
            //        new TableCellWidth { Type = TableWidthUnitValues.Dxa, Width = "1231" }),
            //    new TableCellVerticalAlignment { Val = TableVerticalAlignmentValues.Bottom });
            //cell.Append(PdfAddParagraph(document, 0, 1, Element.ALIGN_CENTER, 0));
            //row.AppendChild(cell);


            //cell = new TableCell();
            //cell.Append(new TableCellProperties(
            //    new TableCellWidth { Type = TableWidthUnitValues.Dxa, Width = "6931" },
            //    new TableCellBorders(new BottomBorder { Val = new EnumValue<BorderValues>(BorderValues.Single) }),
            //    new TableCellVerticalAlignment { Val = TableVerticalAlignmentValues.Bottom }
            //));
            //cell.AppendChild(new Paragraph(
            //    new ParagraphProperties(new Justification { Val = JustificationValues.Center }),
            //    new Run(titleRequestRunProperties.CloneNode(true),
            //        new Text(
            //            $"{account?.Name?.FormatEx()}{(string.IsNullOrWhiteSpace(account?.Position) ? string.Empty : $", {account.Position}")}"))));
            //row.AppendChild(cell);

            //cell = new TableCell();
            //cell.Append(new TableCellProperties(
            //        new TableCellWidth { Type = TableWidthUnitValues.Dxa, Width = "55" }),
            //    new TableCellVerticalAlignment { Val = TableVerticalAlignmentValues.Bottom });
            //cell.AppendChild(new Paragraph(new Run(new Text(Space))));
            //row.AppendChild(cell);

            //cell = new TableCell();
            //cell.Append(new TableCellProperties(
            //        new TableCellWidth { Type = TableWidthUnitValues.Dxa, Width = "2931" },
            //        new TableCellBorders(new BottomBorder { Val = new EnumValue<BorderValues>(BorderValues.Single) })),
            //    new TableCellVerticalAlignment { Val = TableVerticalAlignmentValues.Bottom });
            //cell.AppendChild(new Paragraph(new Run(new Text(Space))));
            //row.AppendChild(cell);

            //table.AppendChild(row);
            //// -----------------------------------------------------------
            //row = new TableRow();

            //cell = new TableCell();
            //cell.Append(new TableCellProperties(
            //        new TableCellWidth { Type = TableWidthUnitValues.Dxa, Width = "1231" }),
            //    new TableCellVerticalAlignment { Val = TableVerticalAlignmentValues.Bottom });
            //cell.AppendChild(new Paragraph(
            //    new ParagraphProperties(new Justification { Val = JustificationValues.Center }),
            //    new Run(titleRequestRunProperties.CloneNode(true),
            //        new Text(Space))));
            //row.AppendChild(cell);


            //cell = new TableCell();
            //cell.Append(new TableCellProperties(
            //        new TableCellWidth { Type = TableWidthUnitValues.Dxa, Width = "6931" }),
            //    new TableCellVerticalAlignment { Val = TableVerticalAlignmentValues.Bottom });
            //cell.AppendChild(new Paragraph(
            //    new ParagraphProperties(new Justification { Val = JustificationValues.Center }),
            //    new Run(captionRunProperties.CloneNode(true), new Text("(Ф.И.О. работника, должность)"))));
            //row.AppendChild(cell);

            //cell = new TableCell();
            //cell.Append(new TableCellProperties(
            //        new TableCellWidth { Type = TableWidthUnitValues.Dxa, Width = "55" }),
            //    new TableCellVerticalAlignment { Val = TableVerticalAlignmentValues.Bottom });
            //cell.AppendChild(new Paragraph(new Run(new Text(Space))));
            //row.AppendChild(cell);

            //cell = new TableCell();
            //cell.Append(new TableCellProperties(
            //        new TableCellWidth { Type = TableWidthUnitValues.Dxa, Width = "2931" }),
            //    new TableCellVerticalAlignment { Val = TableVerticalAlignmentValues.Bottom });
            //cell.AppendChild(new Paragraph(
            //    new ParagraphProperties(new Justification { Val = JustificationValues.Center }),
            //    new Run(titleRequestRunProperties.CloneNode(true), captionRunProperties.CloneNode(true),
            //        new Text("(подпись работника)"))));
            //row.AppendChild(cell);


            //table.AppendChild(row);

            //doc.AppendChild(table);
        }


        #endregion
    }
}
