using System.Collections.Generic;
using System.IO;
using System.Linq;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using RestChild.Comon;
using RestChild.Comon.Enumeration;
using RestChild.Comon.OpenXmlExtensions;
using RestChild.DAL.RepositoryExtensions;
using RestChild.Domain;
using DocumentType = RestChild.Domain.DocumentType;

namespace RestChild.DocumentGeneration
{
    public static partial class WordProcessor
    {
        /// <summary>
        ///     Уведомление о регистрации заявления
        /// </summary>
        public static IDocument NotificationRegistration(IUnitOfWork unitOfWork, Account account, long requestId)
        {
            var request = unitOfWork.GetById<Request>(requestId);
            if (request.StatusId == (long)StatusEnum.RegistrationDecline)
            {
                return NotificationRegistrationDecline(request);
            }

            if (request.TypeOfRestId == (long)TypeOfRestEnum.CompensationYouthRest)
            {
                return NotificationCompensationRegistration(request, account);
            }

            if (request.TypeOfRestId == (long)TypeOfRestEnum.Compensation)
            {
                if (request.Child?.FirstOrDefault().BenefitTypeId == (long)BenefitTypeEnum.CompensationOrphan)
                {
                    return NotificationCompensationRegistrationOrphans(request, account);
                }

                return NotificationCompensationRegistrationForPoors(request, account);
            }

            return NotificationBasicRegistration(request, account, request.SourceId == (long)SourceEnum.Mpgu);
        }

        /// <summary>
        ///     Уведомление о отказе в регистрации заявления (1035)
        /// </summary>
        private static IDocument NotificationRegistrationDecline(Request request)
        {
            var forMpguPortal = request.SourceId == (long)SourceEnum.Mpgu;

            if (forMpguPortal)
            {
                return PDFDocuments.PdfProcessor.NotificationRegistrationDecline(request);
            }

            using (var ms = new MemoryStream())
            {
                using (var wordDocument = WordprocessingDocument.Create(ms, WordprocessingDocumentType.Document))
                {
                    var mainPart = wordDocument.AddMainDocumentPart();
                    var doc = new Document(new Body());

                    DocumentHeaderRegistration(doc);

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                new SpacingBetweenLines { After = Size20 }),
                            new Run(new RunProperties().SetFont().SetFontSize(Size28).Bold(), new Text(Space))));
                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                new SpacingBetweenLines { After = Size20 }),
                            new Run(new RunProperties().SetFont().SetFontSize(Size28).Bold(), new Text(Space))));

                    doc.AppendChild(new Paragraph(
                        new ParagraphProperties(new Justification { Val = JustificationValues.Center }),
                        new Run(new RunProperties().SetFont().SetFontSize(Size28).Bold(),
                            new Text(
                                "Уведомление об отказе в регистрации заявления о предоставлении услуг отдыха и оздоровления"))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                new SpacingBetweenLines { After = Size20 }),
                            new Run(new RunProperties().SetFont().SetFontSize(Size28).Bold(), new Text(Space))));

                    var titleRequestRunProperties = new RunProperties().SetFont().SetFontSize();

                    var titleRequestRunPropertiesBold = titleRequestRunProperties.CloneNode(true);
                    titleRequestRunPropertiesBold.AppendChild(new Bold());

                    var titleRequestRunPropertiesItalic = titleRequestRunProperties.CloneNode(true);
                    titleRequestRunPropertiesItalic.AppendChild(new Italic());

                    var applicant = request.Applicant ?? new Applicant
                    { DocumentType = new DocumentType { Name = string.Empty } };

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                new SpacingBetweenLines { After = Size20 }),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Дата обращения: ")
                                { Space = SpaceProcessingModeValues.Preserve }),
                            new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                new Text(request.DateRequest.FormatEx()))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                new SpacingBetweenLines { After = Size20 }),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Данные обратившегося лица: ") { Space = SpaceProcessingModeValues.Preserve }),
                            new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                new Text(
                                    $"{applicant.LastName} {applicant.FirstName} {applicant.MiddleName}, {applicant.DateOfBirth.FormatExGR(string.Empty)}"))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                new SpacingBetweenLines { After = Size20 }),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Контактная информация: ") { Space = SpaceProcessingModeValues.Preserve }),
                            new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                new Text(applicant.Phone + ", " + applicant.Email))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                new SpacingBetweenLines { After = Size20 }),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Цель обращения: ") { Space = SpaceProcessingModeValues.Preserve }),
                            new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                new Text("получение услуг отдыха и оздоровления"))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                new SpacingBetweenLines { After = Size20 }),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Вид услуги: ") { Space = SpaceProcessingModeValues.Preserve }),
                            new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                new Text(request.TypeOfRest?.Name))));

                    if (request.Child != null)
                    {
                        foreach (var child in request.Child.Where(c => !c.IsDeleted))
                        {
                            doc.AppendChild(
                                new Paragraph(
                                    new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                        new SpacingBetweenLines { After = Size20 }),
                                    new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                        new Text(
                                                "Данные ребёнка/лица из числа детей-сирот и детей, оставшихся без попечения родителей: ")
                                        { Space = SpaceProcessingModeValues.Preserve }),
                                    new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                        new Text(
                                            $"{child.LastName} {child.FirstName} {child.MiddleName}, {child.DateOfBirth.FormatExGR(string.Empty)}"))));
                        }
                    }

                    if (request.TypeOfRestId == (long)TypeOfRestEnum.YouthRestCamps ||
                        request.TypeOfRestId == (long)TypeOfRestEnum.YouthRestOrphanCamps)
                    {
                        doc.AppendChild(
                            new Paragraph(
                                new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                    new SpacingBetweenLines { After = Size20 }),
                                new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                    new Text(
                                            "Данные ребёнка/лица из числа детей-сирот и детей, оставшихся без попечения родителей: ")
                                    { Space = SpaceProcessingModeValues.Preserve }),
                                new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                    new Text(
                                        $"{applicant.LastName} {applicant.FirstName} {applicant.MiddleName}, {applicant.DateOfBirth.FormatExGR(string.Empty)}"))));
                    }

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Both },
                                new SpacingBetweenLines { After = Size20 }),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Основание отказа в регистрации заявления: ")
                                { Space = SpaceProcessingModeValues.Preserve }),
                            new Run(titleRequestRunProperties.CloneNode(true),
                                new Text(
                                    "Порядок организации отдыха и оздоровления детей, находящихся в трудной жизненной ситуации, утвержденный постановлением Правительства Москвы от 22 февраля 2017 г. № 56-ПП \"Об организации отдыха и оздоровления детей, находящихся в трудной жизненной ситуации\" (далее — Порядок)."))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Both },
                                new SpacingBetweenLines { After = Size20 }),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Причина отказа в регистрации заявления: ")
                                { Space = SpaceProcessingModeValues.Preserve }),
                            new Run(titleRequestRunProperties.CloneNode(true),
                                new Text(
                                    "пункт 5.11.1 Порядка: \"Наличие в отношении одного и того же ребёнка, лица из числа детей-сирот и детей, оставшихся без попечения родителей, другого заявления о предоставлении услуг отдыха и оздоровления в текущем календарном году\"."))));

                    mainPart.Document = doc;
                }

                return new DocumentResult
                {
                    FileBody = ms.ToArray(),
                    FileName = "Уведомление об отказе в регистрации заявления.docx",
                    MimeType = mimeType,
                    MimeTypeShort = extention
                };
            }
        }

        /// <summary>
        ///     Уведомление о регистрации заявления на выплату компенсации (Лица из числа детей сирот (18-23))
        /// </summary>
        private static IDocument NotificationCompensationRegistration(Request request, Account account)
        {
            using (var ms = new MemoryStream())
            {
                using (var wordDocument = WordprocessingDocument.Create(ms, WordprocessingDocumentType.Document))
                {
                    var mainPart = wordDocument.AddMainDocumentPart();
                    var doc = new Document(new Body());

                    DocumentHeaderRegistration(doc);

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                new SpacingBetweenLines { After = Size20 }),
                            new Run(new RunProperties().SetFont().SetFontSize(Size28).Bold(), new Text(Space))));
                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                new SpacingBetweenLines { After = Size20 }),
                            new Run(new RunProperties().SetFont().SetFontSize(Size28).Bold(), new Text(Space))));

                    doc.AppendChild(new Paragraph(
                        new ParagraphProperties(new Justification { Val = JustificationValues.Center }),
                        new Run(new RunProperties().SetFont().SetFontSize(Size28).Bold(),
                            new Text("Уведомление о регистрации заявления на выплату компенсации"),
                            new Break(),
                            new Text("за самостоятельно приобретенную лицом из числа детей-сирот и детей, оставшихся без попечения родителей, путевку для отдыха и оздоровления"))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                new SpacingBetweenLines { After = Size20 }),
                            new Run(new RunProperties().SetFont().SetFontSize(Size28).Bold(), new Text(Space))));

                    var titleRequestRunProperties = new RunProperties().SetFont().SetFontSize();

                    var titleRequestRunPropertiesBold = titleRequestRunProperties.CloneNode(true);
                    titleRequestRunPropertiesBold.AppendChild(new Bold());

                    var titleRequestRunPropertiesItalic = titleRequestRunProperties.CloneNode(true);
                    titleRequestRunPropertiesItalic.AppendChild(new Italic());

                    var applicant = request.Applicant ?? new Applicant
                    { DocumentType = new DocumentType { Name = string.Empty } };

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                new SpacingBetweenLines { After = Size20 }),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Дата и время регистрации заявления: ")
                                { Space = SpaceProcessingModeValues.Preserve }),
                            new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                new Text(request.DateRequest.FormatEx()))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                new SpacingBetweenLines { After = Size20 }),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Номер заявления: ") { Space = SpaceProcessingModeValues.Preserve }),
                            new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                new Text(request.RequestNumber.FormatEx()))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                new SpacingBetweenLines { After = Size20 }),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Данные заявителя: ") { Space = SpaceProcessingModeValues.Preserve }),
                            new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                new Text(
                                    $"{applicant.LastName} {applicant.FirstName} {applicant.MiddleName}, {applicant.DateOfBirth.FormatExGR(string.Empty)}"))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                new SpacingBetweenLines { After = Size20 }),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Контактная информация: ") { Space = SpaceProcessingModeValues.Preserve }),
                            new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                new Text(applicant.Phone + ", " + applicant.Email))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                new SpacingBetweenLines { After = Size20 }),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Вид услуги: ") { Space = SpaceProcessingModeValues.Preserve }),
                            new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                new Text(request.TypeOfRest?.Name))));

                    if (request.Child != null)
                    {
                        foreach (var child in request.Child.Where(c => !c.IsDeleted))
                        {
                            doc.AppendChild(
                                new Paragraph(
                                    new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                        new SpacingBetweenLines { After = Size20 }),
                                    new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                        new Text(
                                                "Данные лица из числа детей-сирот, и детей оставшихся без попечения родителей: ")
                                        { Space = SpaceProcessingModeValues.Preserve }),
                                    new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                        new Text(
                                            $"{child.LastName} {child.FirstName} {child.MiddleName}, {child.DateOfBirth.FormatExGR(string.Empty)}"))));

                            doc.AppendChild(
                                new Paragraph(
                                    new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                        new SpacingBetweenLines { After = Size20 }),
                                    new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                        new Text("Льготная категория: ")
                                        { Space = SpaceProcessingModeValues.Preserve }),
                                    new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                        new Text($"{child.BenefitType?.Name}"))));
                        }
                    }

                    //Наркоманы и МГТ хотя чтобы было так
                    if (request.Child == null || !request.Child.Any())
                    {
                        doc.AppendChild(
                            new Paragraph(
                                new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                    new SpacingBetweenLines { After = Size20 }),
                                new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                    new Text(
                                            "Данные лица из числа детей-сирот, и детей оставшихся без попечения родителей: ")
                                    { Space = SpaceProcessingModeValues.Preserve }),
                                new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                    new Text(
                                        $"{applicant.LastName} {applicant.FirstName} {applicant.MiddleName}, {applicant.DateOfBirth.FormatExGR(string.Empty)}"))));

                        doc.AppendChild(
                            new Paragraph(
                                new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                    new SpacingBetweenLines { After = Size20 }),
                                new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                    new Text("Льготная категория: ")
                                    { Space = SpaceProcessingModeValues.Preserve }),
                                new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                    new Text($"{applicant.BenefitType?.Name}"))));
                    }


                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                new SpacingBetweenLines { After = Size20 }),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Перечень предоставленных заявителем документов: ")
                                { Space = SpaceProcessingModeValues.Preserve })));

                    var docs = new List<string>
                    {
                        "документ, удостоверяющий личность обратившегося лица",
                        "страховой номер обязательного пенсионного страхования (СНИЛС) лица из числа детей-сирот и детей, оставшихся без попечения родителей",
                        "доверенность, подтверждающая полномочия представителя, уполномоченного лицом из числа детей-сирот и детей, оставшихся без попечения родителей, на подачу заявления на выплату компенсации за самостоятельно приобретенную путевку (в случае подачи заявления на выплату компенсации за самостоятельно приобретенную путевку таким представителем)",
                        "страховой номер обязательного пенсионного страхования (СНИЛС) представителя, уполномоченного лицом из числа детей-сирот и детей, оставшихся без попечения родителей, на подачу заявления на выплату компенсации за самостоятельно приобретенную путевку (в случае подачи заявления на выплату компенсации за самостоятельно приобретенную путевку таким представителем)",
                        "документы, подтверждающие отдых и оздоровление, и оплату услуг отдыха и оздоровления лицом из числа детей-сирот и детей, оставшихся без попечения родителей",
                        "сведения о кредитной организации и открытом лица из числа детей-сирот и детей, оставшихся без попечения родителей для выплаты компенсации за самостоятельно приобретенную лицом их числа детей-сирот и детей, оставшихся без попечения родителей путевку для отдыха и оздоровления"
                    };

                    AddTableDocsList(doc, docs);

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                new SpacingBetweenLines { After = Size20 }),
                            new Run(new RunProperties().SetFont().SetFontSize(Size28).Bold(), new Text(Space))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Both },
                                new SpacingBetweenLines { After = Size20 },
                                new Indentation { FirstLine = FirstLineIndentation600.ToString() }),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text(
                                        "Заявление")
                                { Space = SpaceProcessingModeValues.Preserve }),
                            new Run(titleRequestRunProperties.CloneNode(true),
                                new Text(
                                        " на выплату компенсации за самостоятельно приобретенную лицом из числа детей-сирот и детей, оставшихся без попечения родителей, путевку для отдыха и оздоровления ")
                                { Space = SpaceProcessingModeValues.Preserve }),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("является неотъемлемой частью данного уведомления")
                                { Space = SpaceProcessingModeValues.Preserve }),
                            new Run(titleRequestRunProperties.CloneNode(true),
                                new Text(".")
                                { Space = SpaceProcessingModeValues.Preserve })
                        ));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Both },
                                new SpacingBetweenLines { After = Size20 },
                                new Indentation { FirstLine = FirstLineIndentation600.ToString() }),
                            new Run(titleRequestRunProperties.CloneNode(true),
                                new Text(
                                        "О результате рассмотрения Вы будете уведомлены способом и формой информирования, указанной в заявлении.")
                                { Space = SpaceProcessingModeValues.Preserve })));

                    SignBlockNotification2019(doc, account, "Подпись заявителя, подтверждающая получение уведомления о регистрации заявления на выплату компенсации за самостоятельно приобретенную лицом из числа детей-сирот и детей, оставшихся без попечения родителей, путевку для отдыха и оздоровления:");

                    mainPart.Document = doc;
                }

                return new DocumentResult
                {
                    FileBody = ms.ToArray(),
                    FileName = "Уведомление о регистрации.docx",
                    MimeType = mimeType,
                    MimeTypeShort = extention
                };
            }
        }

        /// <summary>
        ///     Уведомление о регистрации заявления на выплату компенсации (Дети сироты)
        /// </summary>
        private static IDocument NotificationCompensationRegistrationOrphans(Request request, Account account)
        {
            using (var ms = new MemoryStream())
            {
                using (var wordDocument = WordprocessingDocument.Create(ms, WordprocessingDocumentType.Document))
                {
                    var mainPart = wordDocument.AddMainDocumentPart();
                    var doc = new Document(new Body());

                    DocumentHeaderRegistration(doc);

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                new SpacingBetweenLines { After = Size20 }),
                            new Run(new RunProperties().SetFont().SetFontSize(Size28).Bold(), new Text(Space))));
                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                new SpacingBetweenLines { After = Size20 }),
                            new Run(new RunProperties().SetFont().SetFontSize(Size28).Bold(), new Text(Space))));

                    doc.AppendChild(new Paragraph(
                        new ParagraphProperties(new Justification { Val = JustificationValues.Center }),
                        new Run(new RunProperties().SetFont().SetFontSize(Size28).Bold(),
                            new Text("Уведомление о регистрации заявления на выплату компенсации"),
                            new Break(),
                            new Text("за самостоятельно приобретенную родителями или иными законными представителями путевку для отдыха и оздоровления"),
                            new Break(),
                            new Text("(дети-сироты и дети, оставшиеся без попечения родителей, находящиеся под опекой, попечительством, в том числе"),
                            new Break(),
                            new Text("в приемной или патронатной семье)"))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                new SpacingBetweenLines { After = Size20 }),
                            new Run(new RunProperties().SetFont().SetFontSize(Size28).Bold(), new Text(Space))));

                    var titleRequestRunProperties = new RunProperties().SetFont().SetFontSize();

                    var titleRequestRunPropertiesBold = titleRequestRunProperties.CloneNode(true);
                    titleRequestRunPropertiesBold.AppendChild(new Bold());

                    var titleRequestRunPropertiesItalic = titleRequestRunProperties.CloneNode(true);
                    titleRequestRunPropertiesItalic.AppendChild(new Italic());

                    var applicant = request.Applicant ?? new Applicant
                    { DocumentType = new DocumentType { Name = string.Empty } };

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                new SpacingBetweenLines { After = Size20 }),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Дата и время регистрации заявления: ")
                                { Space = SpaceProcessingModeValues.Preserve }),
                            new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                new Text(request.DateRequest.FormatEx()))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                new SpacingBetweenLines { After = Size20 }),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Номер заявления: ") { Space = SpaceProcessingModeValues.Preserve }),
                            new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                new Text(request.RequestNumber.FormatEx()))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                new SpacingBetweenLines { After = Size20 }),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Данные заявителя: ") { Space = SpaceProcessingModeValues.Preserve }),
                            new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                new Text(
                                    $"{applicant.LastName} {applicant.FirstName} {applicant.MiddleName}, {applicant.DateOfBirth.FormatExGR(string.Empty)}"))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                new SpacingBetweenLines { After = Size20 }),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Контактная информация: ") { Space = SpaceProcessingModeValues.Preserve }),
                            new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                new Text(applicant.Phone + ", " + applicant.Email))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                new SpacingBetweenLines { After = Size20 }),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Вид услуги: ") { Space = SpaceProcessingModeValues.Preserve }),
                            new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                new Text(request.TypeOfRest?.Name))));

                    if (request.Child != null)
                    {
                        foreach (var child in request.Child.Where(c => !c.IsDeleted))
                        {
                            doc.AppendChild(
                                new Paragraph(
                                    new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                        new SpacingBetweenLines { After = Size20 }),
                                    new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                        new Text("Данные ребёнка: ") { Space = SpaceProcessingModeValues.Preserve }),
                                    new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                        new Text(
                                            $"{child.LastName} {child.FirstName} {child.MiddleName}, {child.DateOfBirth.FormatExGR(string.Empty)}"))));

                            doc.AppendChild(
                                new Paragraph(
                                    new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                        new SpacingBetweenLines { After = Size20 }),
                                    new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                        new Text("Льготная категория ребёнка: ")
                                        { Space = SpaceProcessingModeValues.Preserve }),
                                    new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                        new Text($"{child.BenefitType?.Name}"))));
                        }
                    }

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                new SpacingBetweenLines { After = Size20 }),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Перечень предоставленных заявителем документов: ")
                                { Space = SpaceProcessingModeValues.Preserve })));

                    var docs = new List<string>
                    {
                        "заявление на выплату компенсации за самостоятельно приобретенную путевку",
                        "документ, удостоверяющий личность обратившегося лица",
                        "документ, подтверждающий полномочия законного представителя – опекуна, попечителя, приемного родителя, патронатного воспитателя",
                        "$документ, удостоверяющий личность ребёнка-сироты, ребенка, оставшегося без попечения родителей: ",
                        "#для ребёнка в возрасте до 14 лет – свидетельство о рождении ребенка или документ, подтверждающий факт рождения и регистрации ребенка, выданный в установленном порядке (в случае рождения ребенка на территории иностранного государства)",
                        "#для ребёнка, достигшего возраста 14 лет, – паспорт гражданина Российской Федерации, паспорт гражданина иностранного государства (в случае наличия гражданства иностранного государства)",
                        "страховой номер обязательного пенсионного страхования (СНИЛС) ребёнка-сироты, ребенка, оставшегося без попечения родителей",
                        "страховой номер обязательного пенсионного страхования (СНИЛС) законного представителя ребёнка-сироты, ребенка, оставшегося без попечения родителей",
                        "страховой номер обязательного пенсионного страхования (СНИЛС) сопровождающего лица (в случае сопровождения ребёнка-сироты, ребенка, оставшегося без попечения родителей во время отдыха и оздоровления)",
                        "документы, подтверждающие отдых и оздоровление ребёнка-сироты, ребенка, оставшегося без попечения родителей, и оплату законным представителем услуг отдыха и оздоровления",
                        "сведения о кредитной организации и открытом счете законного представителя в кредитной организации для выплаты компенсации за самостоятельно приобретенную законными представителями путевку для отдыха и оздоровления",
                        "доверенность, подтверждающая полномочия доверенного лица законного представителя (в случае подачи заявления на получение компенсации за приобретенную путевку доверенным лицом законного представителя ребёнка)"
                    };

                    AddTableDocsList(doc, docs);

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                new SpacingBetweenLines { After = Size20 }),
                            new Run(new RunProperties().SetFont().SetFontSize(Size28).Bold(), new Text(Space))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Both },
                                new SpacingBetweenLines { After = Size20 },
                                new Indentation { FirstLine = FirstLineIndentation600.ToString() }),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text(
                                        "Заявление")
                                { Space = SpaceProcessingModeValues.Preserve }),
                            new Run(titleRequestRunProperties.CloneNode(true),
                                new Text(
                                        " на выплату компенсации за самостоятельно приобретенную родителями или иными законными представителями путевку для отдыха и оздоровления (дети - сироты и дети, оставшиеся без попечения родителей, находящиеся под опекой, попечительством, в том числе в приемной или патронатной семье) ")
                                { Space = SpaceProcessingModeValues.Preserve }),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("является неотъемлемой частью данного уведомления")
                                { Space = SpaceProcessingModeValues.Preserve }),
                            new Run(titleRequestRunProperties.CloneNode(true),
                                new Text(".")
                                { Space = SpaceProcessingModeValues.Preserve })
                        ));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Both },
                                new SpacingBetweenLines { After = Size20 }),
                            new Run(titleRequestRunProperties.CloneNode(true),
                                new Text(
                                        "О результате рассмотрения Вы будете уведомлены способом и формой информирования, указанной в заявлении.")
                                { Space = SpaceProcessingModeValues.Preserve })));


                    SignBlockNotification2019(doc, account, "Подпись заявителя, подтверждающая получение уведомления о регистрации заявления на выплату компенсации за самостоятельно приобретенную родителями или иными законными представителями путевку для отдыха и оздоровления (дети-сироты и дети, оставшиеся без попечения родителей, находящиеся под опекой, попечительством, в том числе в приемной или патронатной семье):");

                    mainPart.Document = doc;
                }

                return new DocumentResult
                {
                    FileBody = ms.ToArray(),
                    FileName = "Уведомление о регистрации.docx",
                    MimeType = mimeType,
                    MimeTypeShort = extention
                };
            }
        }

        /// <summary>
        ///     Уведомление о регистрации заявления на выплату компенсации (Малоимущие)
        /// </summary>
        private static IDocument NotificationCompensationRegistrationForPoors(Request request, Account account)
        {
            using (var ms = new MemoryStream())
            {
                using (var wordDocument = WordprocessingDocument.Create(ms, WordprocessingDocumentType.Document))
                {
                    var mainPart = wordDocument.AddMainDocumentPart();
                    var doc = new Document(new Body());

                    DocumentHeaderRegistration(doc);

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                new SpacingBetweenLines { After = Size20 }),
                            new Run(new RunProperties().SetFont().SetFontSize(Size28).Bold(), new Text(Space))));
                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                new SpacingBetweenLines { After = Size20 }),
                            new Run(new RunProperties().SetFont().SetFontSize(Size28).Bold(), new Text(Space))));

                    doc.AppendChild(new Paragraph(
                        new ParagraphProperties(new Justification { Val = JustificationValues.Center }),
                        new Run(new RunProperties().SetFont().SetFontSize(Size28).Bold(),
                            new Text("Уведомление о регистрации заявления на выплату компенсации"),
                            new Break(),
                            new Text("за самостоятельно приобретенную родителями или иными законными представителями путевку для отдыха и оздоровления"),
                            new Break(),
                            new Text("(дети из малообеспеченных семей)"))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                new SpacingBetweenLines { After = Size20 }),
                            new Run(new RunProperties().SetFont().SetFontSize(Size28).Bold(), new Text(Space))));

                    var titleRequestRunProperties = new RunProperties().SetFont().SetFontSize();

                    var titleRequestRunPropertiesBold = titleRequestRunProperties.CloneNode(true);
                    titleRequestRunPropertiesBold.AppendChild(new Bold());

                    var titleRequestRunPropertiesItalic = titleRequestRunProperties.CloneNode(true);
                    titleRequestRunPropertiesItalic.AppendChild(new Italic());

                    var applicant = request.Applicant ?? new Applicant
                    { DocumentType = new DocumentType { Name = string.Empty } };

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                new SpacingBetweenLines { After = Size20 }),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Дата и время регистрации заявления: ")
                                { Space = SpaceProcessingModeValues.Preserve }),
                            new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                new Text(request.DateRequest.FormatEx()))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                new SpacingBetweenLines { After = Size20 }),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Номер заявления: ") { Space = SpaceProcessingModeValues.Preserve }),
                            new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                new Text(request.RequestNumber.FormatEx()))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                new SpacingBetweenLines { After = Size20 }),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Данные заявителя: ") { Space = SpaceProcessingModeValues.Preserve }),
                            new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                new Text(
                                    $"{applicant.LastName} {applicant.FirstName} {applicant.MiddleName}, {applicant.DateOfBirth.FormatExGR(string.Empty)}"))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                new SpacingBetweenLines { After = Size20 }),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Контактная информация: ") { Space = SpaceProcessingModeValues.Preserve }),
                            new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                new Text(applicant.Phone + ", " + applicant.Email))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                new SpacingBetweenLines { After = Size20 }),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Вид услуги: ") { Space = SpaceProcessingModeValues.Preserve }),
                            new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                new Text(request.TypeOfRest?.Name))));

                    if (request.Child != null)
                    {
                        foreach (var child in request.Child.Where(c => !c.IsDeleted))
                        {
                            doc.AppendChild(
                                new Paragraph(
                                    new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                        new SpacingBetweenLines { After = Size20 }),
                                    new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                        new Text("Данные ребёнка: ") { Space = SpaceProcessingModeValues.Preserve }),
                                    new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                        new Text(
                                            $"{child.LastName} {child.FirstName} {child.MiddleName}, {child.DateOfBirth.FormatExGR(string.Empty)}"))));

                            doc.AppendChild(
                                new Paragraph(
                                    new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                        new SpacingBetweenLines { After = Size20 }),
                                    new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                        new Text("Льготная категория ребёнка: ")
                                        { Space = SpaceProcessingModeValues.Preserve }),
                                    new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                        new Text($"{child.BenefitType?.Name}"))));
                        }
                    }

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                new SpacingBetweenLines { After = Size20 }),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Перечень предоставленных заявителем документов: ")
                                { Space = SpaceProcessingModeValues.Preserve })));

                    var docs = new List<string>
                    {
                        "заявление на выплату компенсации за самостоятельно приобретенную путевку",
                        "документ, удостоверяющий личность обратившегося лица",
                        "документ, подтверждающий полномочия законного представителя – опекуна, попечителя, приемного родителя, патронатного воспитателя (в случае подачи заявления на выплату компенсации за приобретенную путевку обратившимся лицом из числа законных представителей – опекунов, попечителей, приемных родителей, патронатных воспитателей ребёнка)",
                        "$документ, удостоверяющий личность ребёнка: ",
                        "#для ребёнка в возрасте до 14 лет – свидетельство о рождении ребенка или документ, подтверждающий факт рождения и регистрации ребенка, выданный в установленном порядке (в случае рождения ребенка на территории иностранного государства)",
                        "#для ребёнка, достигшего возраста 14 лет, – паспорт гражданина Российской Федерации, паспорт гражданина иностранного государства (в случае наличия гражданства иностранного государства)",
                        "страховой номер обязательного пенсионного страхования (СНИЛС) ребёнка",
                        "страховой номер обязательного пенсионного страхования (СНИЛС) родителя, иного законного представителя",
                        "документы, подтверждающие факт родства родителя с ребёнком (в случае если иные представленные документы не содержат необходимой информации)",
                        "документ, содержащий сведения о месте жительства ребёнка в городе Москве (в случае если в документе, удостоверяющем личность ребенка, отсутствуют сведения о его месте жительства в городе Москве)",
                        "документы, подтверждающие отдых и оздоровление ребёнка и оплату родителем услуг отдыха и оздоровления",
                        "сведения о кредитной организации и открытом счете родителя, иного законного представителя в кредитной организации для выплаты компенсации за самостоятельно приобретенную родителями или иными законными представителями путевку для отдыха и оздоровления",
                        "доверенность, подтверждающая полномочия доверенного лица родителя (в случае подачи заявления на получение компенсации за приобретенную путевку доверенным лицом родителя, иного законного представителя ребёнка)"
                    };

                    AddTableDocsList(doc, docs);

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                new SpacingBetweenLines { After = Size20 }),
                            new Run(new RunProperties().SetFont().SetFontSize(Size28).Bold(), new Text(Space))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Both },
                                new SpacingBetweenLines { After = Size20 },
                                new Indentation { FirstLine = FirstLineIndentation600.ToString() }),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text(
                                        "Заявление")
                                { Space = SpaceProcessingModeValues.Preserve }),
                            new Run(titleRequestRunProperties.CloneNode(true),
                                new Text(
                                        " на выплату компенсации за самостоятельно приобретенную родителями или иными законными представителями путевку для отдыха и оздоровления (дети из малообеспеченных семей) ")
                                { Space = SpaceProcessingModeValues.Preserve }),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("является неотъемлемой частью данного уведомления")
                                { Space = SpaceProcessingModeValues.Preserve }),
                            new Run(titleRequestRunProperties.CloneNode(true),
                                new Text(".")
                                { Space = SpaceProcessingModeValues.Preserve })
                        ));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Both },
                                new SpacingBetweenLines { After = Size20 },
                                new Indentation { FirstLine = FirstLineIndentation600.ToString() }),
                            new Run(titleRequestRunProperties.CloneNode(true),
                                new Text(
                                        "О результате рассмотрения Вы будете уведомлены способом и формой информирования, указанной в заявлении.")
                                { Space = SpaceProcessingModeValues.Preserve })));


                    SignBlockNotification2019(doc, account, "Подпись заявителя, подтверждающая получение уведомления о регистрации заявления на выплату компенсации за самостоятельно приобретенную родителями или иными законными представителями путевку для отдыха и оздоровления (дети из малообеспеченных семей):");

                    mainPart.Document = doc;
                }

                return new DocumentResult
                {
                    FileBody = ms.ToArray(),
                    FileName = "Уведомление о регистрации.docx",
                    MimeType = mimeType,
                    MimeTypeShort = extention
                };
            }
        }

        /// <summary>
        ///     Уведомление о регистрации заявления о предоставлении услуг отдыха и оздоровления
        /// </summary>
        private static IDocument NotificationBasicRegistration(Request request, Account account, bool forMpguPortal = false)
        {
            var youth = request.TypeOfRestId == (long)TypeOfRestEnum.YouthRestCamps ||
                        request.TypeOfRestId == (long)TypeOfRestEnum.YouthRestOrphanCamps;

            if (forMpguPortal)
            {
                return PDFDocuments.PdfProcessor.NotificationBasicRegistration(request, youth);
            }


            using (var ms = new MemoryStream())
            {
                using (var wordDocument = WordprocessingDocument.Create(ms, WordprocessingDocumentType.Document))
                {
                    var mainPart = wordDocument.AddMainDocumentPart();
                    var doc = new Document(new Body());

                    DocumentHeaderRegistration(doc);

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                new SpacingBetweenLines { After = Size20 }),
                            new Run(new RunProperties().SetFont().SetFontSize(Size28).Bold(), new Text(Space))));
                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                new SpacingBetweenLines { After = Size20 }),
                            new Run(new RunProperties().SetFont().SetFontSize(Size28).Bold(), new Text(Space))));


                    if (!youth)
                    {
                        var elems = new List<OpenXmlElement>();
                        elems.Add(new RunProperties().SetFont().SetFontSize(Size28).Bold());
                        elems.Add(new Text("Уведомление о регистрации заявления о предоставлении услуг отдыха"));
                        elems.Add(new Break());
                        elems.Add(new Text("и оздоровления, поданного с помощью работника"));
                        elems.Add(new Break());
                        elems.Add(new Text("ГАУК \"МОСГОРТУР\""));

                        doc.AppendChild(new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Center }),
                            new Run(elems)
                        ));
                    }
                    else
                    {
                        var elems = new List<OpenXmlElement>();
                        elems.Add(new RunProperties().SetFont().SetFontSize(Size28).Bold());
                        elems.Add(new Text("Уведомление о регистрации заявления о предоставлении услуг отдыха"));
                        elems.Add(new Break());
                        elems.Add(new Text("и оздоровления лицу из числа детей-сирот и детей, оставшихся"));
                        elems.Add(new Break());
                        elems.Add(new Text("без попечения родителей, поданного с помощью работника"));
                        elems.Add(new Break());
                        elems.Add(new Text("ГАУК \"МОСГОРТУР\""));

                        doc.AppendChild(new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Center }),
                            new Run(elems)
                        ));
                    }

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                new SpacingBetweenLines { After = Size20 }),
                            new Run(new RunProperties().SetFont().SetFontSize(Size28).Bold(), new Text(Space))));

                    var titleRequestRunProperties = new RunProperties().SetFont().SetFontSize();

                    var titleRequestRunPropertiesBold = titleRequestRunProperties.CloneNode(true);
                    titleRequestRunPropertiesBold.AppendChild(new Bold());

                    var titleRequestRunPropertiesItalic = titleRequestRunProperties.CloneNode(true);
                    titleRequestRunPropertiesItalic.AppendChild(new Italic());

                    var documentRunPropertiesItalic = new RunProperties().SetFont().SetFontSize(Size16);
                    documentRunPropertiesItalic.AppendChild(new Italic());

                    var applicant = request.Applicant ??
                                    new Applicant { DocumentType = new DocumentType { Name = string.Empty } };

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                new SpacingBetweenLines { After = Size20 }),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Дата и время регистрации заявления: ")
                                { Space = SpaceProcessingModeValues.Preserve }),
                            new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                new Text(request.DateRequest.FormatEx()))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                new SpacingBetweenLines { After = Size20 }),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Номер заявления: ") { Space = SpaceProcessingModeValues.Preserve }),
                            new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                new Text(request.RequestNumber.FormatEx()))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Both },
                                new SpacingBetweenLines { After = Size20 }),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Данные заявителя: ") { Space = SpaceProcessingModeValues.Preserve }),
                            new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                new Text(
                                    $"{applicant.LastName} {applicant.FirstName} {applicant.MiddleName}, {applicant.DateOfBirth.FormatExGR(string.Empty)}"))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Both },
                                new SpacingBetweenLines { After = Size20 }),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Контактная информация: ") { Space = SpaceProcessingModeValues.Preserve }),
                            new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                new Text(applicant.Phone + ", " + applicant.Email))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Both },
                                new SpacingBetweenLines { After = Size20 }),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Вид услуги: ") { Space = SpaceProcessingModeValues.Preserve }),
                            new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                new Text(youth
                                    ? "бесплатная путевка для отдыха и оздоровления"
                                    : request.TypeOfRest?.Name))));

                    foreach (var child in request.Child?.Where(c => !c.IsDeleted) ?? new List<Child>())
                    {
                        doc.AppendChild(
                            new Paragraph(
                                new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                    new SpacingBetweenLines { After = Size20 }),
                                new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                    new Text("Данные ребёнка: ") { Space = SpaceProcessingModeValues.Preserve }),
                                new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                    new Text(
                                        $"{child.LastName} {child.FirstName} {child.MiddleName}, {child.DateOfBirth.FormatExGR(string.Empty)}"))));

                        doc.AppendChild(
                            new Paragraph(
                                new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                    new SpacingBetweenLines { After = Size20 }),
                                new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                    new Text("Льготная категория: ")
                                    { Space = SpaceProcessingModeValues.Preserve }),
                                new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                    new Text($"{child.BenefitType?.Name}"))));
                    }


                    if (youth)
                    {
                        doc.AppendChild(
                            new Paragraph(
                                new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                    new SpacingBetweenLines { After = Size20 }),
                                new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                    new Text(
                                            "Данные лица из числа детей-сирот и детей, оставшихся без попечения родителей:")
                                    { Space = SpaceProcessingModeValues.Preserve }),
                                new Break(),
                                new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                    new Text(
                                        $"{applicant.LastName} {applicant.FirstName} {applicant.MiddleName}, {applicant.DateOfBirth.FormatExGR(string.Empty)}"))));
                    }

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                new SpacingBetweenLines { After = Size20 }),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Перечень предоставленных заявителем документов: ")
                                { Space = SpaceProcessingModeValues.Preserve })));

                    var docs = youth
                        ? new List<string>
                            {
                                "Документ, удостоверяющий личность лица из числа детей-сирот и детей, оставшихся без попечения родителей",
                                "Документ, удостоверяющий личность доверенного лица на совершение действий в период проведения заявочной кампании (в случае подачи заявления о предоставлении услуг отдыха и оздоровления доверенным лицом на совершение действий в период проведения заявочной кампании)",
                                "Страховой номер индивидуального лицевого счета (СНИЛС) в системе индивидуального (персонифицированного) учета лица из числа детей-сирот и детей, оставшихся без попечения родителей",
                                "Страховой номер индивидуального лицевого счета (СНИЛС) в системе индивидуального (персонифицированного) учета доверенного лица на совершение действий в период проведения заявочной кампании (в случае подачи заявления о предоставлении услуг отдыха и оздоровления доверенным лицом на совершение действий в период проведения заявочной кампании)",
                                "Документ, подтверждающий сведения об отнесении лица к категории лиц из числа детей-сирот и детей, оставшихся без попечения родителей",
                                "Документ, подтверждающий сведения о месте жительства лица из числа детей-сирот и детей, оставшихся без попечения родителей, в городе Москве",
                                "Документ, подтверждающий полномочия доверенного лица на совершение действий в период проведения заявочной кампании (в случае подачи заявления о предоставлении услуг отдыха и оздоровления доверенным лицом на совершение действий в период проведения заявочной кампании)"
                            }
                        : new List<string>
                            {
                                "Документ, удостоверяющий личность родителя ребёнка или иного законного представителя – опекуна, попечителя, приемного родителя, патронатного воспитателя ребенка",
                                "Документ, удостоверяющий личность доверенного лица на совершение действий в период проведения заявочной кампании (в случае подачи заявления о предоставлении услуг отдыха и оздоровления доверенным лицом на совершение действий в период проведения заявочной кампании)",
                                "Страховой номер индивидуального лицевого счета (СНИЛС) в системе индивидуального (персонифицированного) учета родителя или законного представителя ребёнка",
                                "Страховой номер индивидуального лицевого счета (СНИЛС) в системе индивидуального (персонифицированного) учета доверенного лица на совершение действий в период проведения заявочной кампании (в случае подачи заявления о предоставлении услуг отдыха и оздоровления доверенным лицом на совершение действий в период проведения заявочной кампании)",
                                //"$Документ, удостоверяющий личность ребенка:",
                                "Документ, удостоверяющий личность ребёнка",
                                //"#для ребенка в возрасте до 14 лет – свидетельство о рождении ребенка или документ, подтверждающий факт рождения и регистрации ребенка, выданный в установленном порядке (в случае рождения ребенка на территории иностранного государства);",
                                //"#для ребенка, достигшего возраста 14 лет, – паспорт гражданина Российской Федерации, паспорт гражданина иностранного государства (в случае наличия гражданства иностранного государства)",
                                "Документ, подтверждающий сведения об отнесении ребёнка к одной из категорий детей, находящихся в трудной жизненной ситуации и указанных в пунктах 3.1.2-3.1.13 Порядка",
                                "Документ, подтверждающий сведения о месте жительства ребёнка, в городе Москве",
                                "Страховой номер индивидуального лицевого счета (СНИЛС) в системе индивидуального (персонифицированного) учета ребёнка",
                                "Сведения документа, удостоверяющего личность сопровождающего лица (в случае организации совместного выездного отдыха)",
                                "Страховой номер индивидуального лицевого счета (СНИЛС) в системе индивидуального (персонифицированного) учета сопровождающего лица (в случае организации совместного выездного отдыха)",
                                "Документ, подтверждающий полномочия доверенного лица на совершение действий в период проведения заявочной кампании (в случае подачи заявления о предоставлении услуг отдыха и оздоровления доверенным лицом на совершение действий в период проведения заявочной кампании)",
                                "Документ, подтверждающий полномочия доверенного лица для сопровождения во время отдыха и оздоровления (в случае организации совместного выездного отдыха и сопровождения ребёнка доверенным лицом для сопровождения во время отдыха и оздоровления)",
                                "Документы, подтверждающие полномочия заявителя, сопровождающего лица (в случае организации совместного выездного отдыха) из числа законных представителей – родителей, опекунов, попечителей, приемных родителей, патронатных воспитателей ребёнка"
                            };

                    AddTableDocsList(doc, docs);

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Both },
                                new Indentation { FirstLine = FirstLineIndentation600.ToString() },
                                new SpacingBetweenLines { After = Size20 }),
                            new Run(titleRequestRunProperties.CloneNode(true),
                                new Text("Ваше заявление о предоставлении услуг отдыха и оздоровления (далее – заявление) ")
                                    { Space = SpaceProcessingModeValues.Preserve }),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text(
                                        "зарегистрировано")
                                    { Space = SpaceProcessingModeValues.Preserve }),
                            new Run(titleRequestRunProperties.CloneNode(true),
                                new Text(" и является ")
                                    { Space = SpaceProcessingModeValues.Preserve }),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text(
                                        "неотъемлемой частью")
                                    { Space = SpaceProcessingModeValues.Preserve }),
                            new Run(titleRequestRunProperties.CloneNode(true),
                                new Text(
                                        " данного уведомления.")
                                    { Space = SpaceProcessingModeValues.Preserve })
                        ));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Both },
                                new Indentation { FirstLine = FirstLineIndentation600.ToString() },
                                new SpacingBetweenLines { After = Size20 }),
                            new Run(titleRequestRunProperties.CloneNode(true),
                                new Text("В случае если в срок, не превышающий 21 рабочий день с даты подачи, по Вашему заявлению ")
                                { Space = SpaceProcessingModeValues.Preserve }),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("не пришло уведомление об отказе в предоставлении услуг отдыха и оздоровления")
                                { Space = SpaceProcessingModeValues.Preserve }),
                            new Run(titleRequestRunProperties.CloneNode(true),
                                new Text($" по причинам, указанным в пунктах {(youth ? "9.1.2-9.1.4" : "9.1.2-9.1.6, 9.1.8-9.1.11")} Порядка организации отдыха")
                                { Space = SpaceProcessingModeValues.Preserve },
                                new Text(" и оздоровления детей, находящихся в трудной жизненной ситуации, утвержденного постановлением Правительства Москвы от 22 февраля 2017 г. № 56-ПП \"Об организации отдыха и оздоровления детей, находящихся в трудной жизненной ситуации\" (далее – Порядок), ")
                                { Space = SpaceProcessingModeValues.Preserve }),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Ваше заявление прошло все проверки")
                                { Space = SpaceProcessingModeValues.Preserve }),
                            new Run(titleRequestRunProperties.CloneNode(true),
                                new Text(" и будет рассмотрено на заседании Комиссии ГАУК \"МОСГОРТУР\" по включению сведений в предварительный Реестр получателей услуг отдыха и оздоровления (далее – Комиссия), которое состоится 16 января 2021 г.")
                                { Space = SpaceProcessingModeValues.Preserve })
                        ));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Both },
                                new Indentation { FirstLine = FirstLineIndentation600.ToString() },
                                new SpacingBetweenLines { After = Size20 }),
                            new Run(titleRequestRunProperties.CloneNode(true),
                                new Text(
                                        "По результатам заседания Комиссии на электронную почту, указанную в заявлении, не позднее ")
                                { Space = SpaceProcessingModeValues.Preserve }),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text(
                                        "6 февраля 2021 г.")
                                { Space = SpaceProcessingModeValues.Preserve }),
                            new Run(titleRequestRunProperties.CloneNode(true),
                                new Text(
                                        youth
                                            ? " будет направлено уведомление о необходимости выбора организации отдыха и оздоровления (допуск на второй этап заявочной кампании)."
                                            : " будет направлено одно из уведомлений: о необходимости выбора организации отдыха и оздоровления (допуск на второй этап заявочной кампании); о предоставлении сертификата на отдых и оздоровление; об отказе в предоставлении услуг отдыха и оздоровления по причине, указанной в пункте 9.1.1. Порядка.")
                                { Space = SpaceProcessingModeValues.Preserve })
                        ));

                    if (!youth)
                    {
                        doc.AppendChild(
                            new Paragraph(
                                new ParagraphProperties(new Justification { Val = JustificationValues.Both },
                                    new Indentation { FirstLine = FirstLineIndentation600.ToString() },
                                    new SpacingBetweenLines { After = Size20 }),
                                new Run(titleRequestRunProperties.CloneNode(true),
                                    new Text("При рассмотрении заявлений Комиссия руководствуется пунктом 3.9 Порядка, согласно которому в первую очередь рассматриваются заявления, в которых указан хотя бы один ребёнок, которому в период с 2018 по 2020 год не предоставлялись бесплатные путевки для отдыха и оздоровления (далее – бесплатные путевки) либо сертификаты на отдых и оздоровление (далее – сертификаты), а также не выплачивалась компенсация за самостоятельно приобретенную путевку (далее – компенсация).")
                                    { Space = SpaceProcessingModeValues.Preserve })
                            ));
                        doc.AppendChild(
                            new Paragraph(
                                new ParagraphProperties(new Justification { Val = JustificationValues.Both },
                                    new Indentation { FirstLine = FirstLineIndentation600.ToString() },
                                    new SpacingBetweenLines { After = Size20 }),
                                new Run(titleRequestRunProperties.CloneNode(true),
                                    new Text("Во вторую очередь рассматриваются заявления, где указаны дети, которым в любые два года из последних трех лет (с 2018 по 2020 год) не предоставлялись бесплатные путевки либо сертификаты, а также не выплачивалась компенсация.")
                                        { Space = SpaceProcessingModeValues.Preserve })
                            ));
                        doc.AppendChild(
                            new Paragraph(
                                new ParagraphProperties(new Justification { Val = JustificationValues.Both },
                                    new Indentation { FirstLine = FirstLineIndentation600.ToString() },
                                    new SpacingBetweenLines { After = Size20 }),
                                new Run(titleRequestRunProperties.CloneNode(true),
                                    new Text("В третью очередь рассматриваются заявления, где указаны дети, которым в любые два года из последних трех лет (с 2018 по 2020 год) предоставлялись бесплатные путевки либо сертификаты, а также выплачивалась компенсация.")
                                        { Space = SpaceProcessingModeValues.Preserve })
                            ));
                        doc.AppendChild(
                            new Paragraph(
                                new ParagraphProperties(new Justification { Val = JustificationValues.Both },
                                    new Indentation { FirstLine = FirstLineIndentation600.ToString() },
                                    new SpacingBetweenLines { After = Size20 }),
                                new Run(titleRequestRunProperties.CloneNode(true),
                                    new Text("Исключение составляют дети-сироты и дети, оставшиеся без попечения родителей, находящиеся под опекой, попечительством, в том числе в приемной или патронатной семье, и лица из числа детей-сирот и детей, оставшихся без попечения родителей, которым федеральным законодательством гарантировано ежегодное предоставление услуг отдыха и оздоровления.")
                                        { Space = SpaceProcessingModeValues.Preserve })
                            ));


                        doc.AppendChild(
                            new Paragraph(
                                new ParagraphProperties(new Justification { Val = JustificationValues.Both },
                                    new Indentation { FirstLine = FirstLineIndentation600.ToString() },
                                    new SpacingBetweenLines { After = Size20 }),
                                new Run(titleRequestRunProperties.CloneNode(true),
                                    new Text("По результатам второго этапа заявочной кампании, проводимого в период с 7 по 21 февраля 2021 г., уведомление о результате рассмотрения заявления (о предоставлении бесплатной путевки для отдыха и оздоровления; об отказе в предоставлении услуг отдыха и оздоровления в связи с неучастием заявителя во втором этапе заявочной кампании; о неучастии заявителя в выборе конкретной организации отдыха и оздоровления; об отказе в предоставлении услуг отдыха и оздоровления) будет направлено на электронную почту, указанную в заявлении, ")
                                        { Space = SpaceProcessingModeValues.Preserve }),
                                new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                    new Text("не позднее 22 февраля 2021 г.")
                                        { Space = SpaceProcessingModeValues.Preserve })
                            ));

                        doc.AppendChild(
                            new Paragraph(
                                new ParagraphProperties(new Justification { Val = JustificationValues.Both },
                                    new Indentation { FirstLine = FirstLineIndentation600.ToString() },
                                    new SpacingBetweenLines { After = Size20 }),
                                new Run(titleRequestRunProperties.CloneNode(true),
                                    new Text("Обращаем внимание, что Вы согласились с необходимостью соблюдения: Правил отдыха и оздоровления, Соглашения об осуществлении обязанностей по сопровождению детей во время отдыха и оздоровления родителем или иным законным представителем либо доверенным лицом для сопровождения во время отдыха и оздоровления (для совместного выездного отдыха); а также проинформированы: о перечне противопоказаний к отдыху и оздоровлению, утвержденном Министерством здравоохранения Российской Федерации (для индивидуального выездного отдыха), об отсутствии возможности замены сертификата на отдых и оздоровление на бесплатную путевку для отдыха и оздоровления (в случае выбора сертификата на отдых и оздоровление), об отсутствии возможности выбора сертификата на отдых и оздоровление в случае отказа на втором этапе заявочной кампании от всех предложенных организаций отдыха и оздоровления, о перечислении денежных средств организации, осуществляющей туроператорскую деятельность и (или) оказывающей услуги отдыха и оздоровления, при приобретении у такой организации услуг отдыха и оздоровления или туристского продукта для ребенка, либо ребёнка и лица, его сопровождающего с использованием сертификата на отдых и оздоровление (в случае выбора сертификата на отдых и оздоровление).")
                                        { Space = SpaceProcessingModeValues.Preserve })
                            ));
                    }
                    else
                    {
                        doc.AppendChild(
                            new Paragraph(
                                new ParagraphProperties(new Justification { Val = JustificationValues.Both },
                                    new Indentation { FirstLine = FirstLineIndentation600.ToString() },
                                    new SpacingBetweenLines { After = Size20 }),
                                new Run(titleRequestRunProperties.CloneNode(true),
                                    new Text("По результатам второго этапа заявочной кампании, проводимого в период с 7 по 21 февраля 2021 г., уведомление о результате рассмотрения заявления (о предоставлении бесплатной путевки для отдыха и оздоровления; об отказе в предоставлении услуг отдыха и оздоровления в связи с неучастием заявителя во втором этапе заявочной кампании; об отказе в предоставлении услуг отдыха и оздоровления) будет направлено на электронную почту, указанную в заявлении, ")
                                        { Space = SpaceProcessingModeValues.Preserve }),
                                new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                    new Text("не позднее 22 февраля 2021 г.")
                                        { Space = SpaceProcessingModeValues.Preserve })
                            ));

                        doc.AppendChild(
                            new Paragraph(
                                new ParagraphProperties(new Justification { Val = JustificationValues.Both },
                                    new Indentation { FirstLine = FirstLineIndentation600.ToString() },
                                    new SpacingBetweenLines { After = Size20 }),
                                new Run(titleRequestRunProperties.CloneNode(true),
                                    new Text("Обращаем внимание, что Вы согласились с необходимостью соблюдения Правил отдыха и оздоровления.")
                                        { Space = SpaceProcessingModeValues.Preserve })
                            ));

                    }

                    SignBlockNotification2020(doc, account, "Исполнитель:");

                    mainPart.Document = doc;
                }

                return new DocumentResult
                {
                    FileBody = ms.ToArray(),
                    FileName = "Уведомление о регистрации.docx",
                    MimeType = mimeType,
                    MimeTypeShort = extention
                };
            }
        }
    }
}
