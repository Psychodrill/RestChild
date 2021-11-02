using System.Collections.Generic;
using System.IO;
using System.Linq;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Newtonsoft.Json;
using RestChild.Comon;
using RestChild.Comon.Dto;
using RestChild.Comon.Enumeration;
using RestChild.Comon.OpenXmlExtensions;
using RestChild.DocumentGeneration.PDFDocuments;
using RestChild.Domain;
using DocumentType = RestChild.Domain.DocumentType;
using System;

// ReSharper disable once CheckNamespace
namespace RestChild.DocumentGeneration
{
    public static partial class WordProcessor
    {
        /// <summary>
        ///     Уведомление об отказе в компенсации за отдых и оздоровление (дети из малообеспеченных семей, дети-сироты и дети,
        ///     оставшиеся без попечения родителей)
        /// </summary>
        private static IDocument NotificationRefuseCompensation(Request request, Account account)
        {
            using (var ms = new MemoryStream())
            {
                using (var wordDocument = WordprocessingDocument.Create(ms, WordprocessingDocumentType.Document))
                {
                    var mainPart = wordDocument.AddMainDocumentPart();
                    var doc = new Document(new Body());

                    var titleRequestRunProperties = new RunProperties().SetFont().SetFontSize();

                    var titleRequestRunPropertiesBold = titleRequestRunProperties.CloneNode(true);
                    titleRequestRunPropertiesBold.AppendChild(new Bold());

                    var titleRequestRunPropertiesItalic = titleRequestRunProperties.CloneNode(true);
                    titleRequestRunPropertiesItalic.AppendChild(new Italic());

                    var applicant = request.Applicant ?? new Applicant
                        {DocumentType = new DocumentType {Name = string.Empty}};

                    DocumentHeaderRegistration(doc);

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(new RunProperties().SetFont().SetFontSize(Size28).Bold(), new Text(Space))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(new RunProperties().SetFont().SetFontSize(Size28).Bold(), new Text(Space))));

                    doc.AppendChild(new Paragraph(
                        new ParagraphProperties(new Justification {Val = JustificationValues.Center}),
                        new Run(new RunProperties().SetFont().SetFontSize(Size28).Bold(),
                            new Text(
                                "Уведомление об отказе в выплате компенсации за самостоятельно приобретенную родителями или иными законными представителями путевку для отдыха и оздоровления"))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(new RunProperties().SetFont().SetFontSize(Size28).Bold(), new Text(Space))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Дата обращения: ")
                                    {Space = SpaceProcessingModeValues.Preserve}),
                            new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                new Text(request.DateRequest.FormatEx()))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Номер заявления: ") {Space = SpaceProcessingModeValues.Preserve}),
                            new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                new Text(request.RequestNumber.FormatEx()))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Данные заявителя: ") {Space = SpaceProcessingModeValues.Preserve}),
                            new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                new Text(
                                    $"{applicant.LastName} {applicant.FirstName} {applicant.MiddleName}, {applicant.DateOfBirth.FormatExGR(string.Empty)}"))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Контактная информация: ") {Space = SpaceProcessingModeValues.Preserve}),
                            new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                new Text(applicant.Phone + ", " + applicant.Email))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Результат рассмотрения заявления: ")
                                    {Space = SpaceProcessingModeValues.Preserve}),
                            new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                new Text(
                                    "отказ в предоставлении выплаты компенсации за самостоятельно приобретенную родителями или иными законными представителями путевку для отдыха и оздоровления."))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Both},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Основание: ") {Space = SpaceProcessingModeValues.Preserve}),
                            new Run(titleRequestRunProperties.CloneNode(true),
                                new Text(FederalLaw))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Both},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Причина отказа в выплате: ") {Space = SpaceProcessingModeValues.Preserve}),
                            new Run(titleRequestRunProperties.CloneNode(true),
                                new Text(request.DeclineReason?.Name))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(new RunProperties().SetFont().SetFontSize(Size28).Bold(), new Text(Space))));

                    SignBlockNotification(doc, account,
                        $"{applicant.LastName} {applicant.FirstName} {applicant.MiddleName}");

                    mainPart.Document = doc;
                }

                return new DocumentResult
                {
                    FileBody = ms.ToArray(),
                    FileName = "Уведомление об отказе.docx",
                    MimeType = mimeType,
                    MimeTypeShort = extention
                };
            }
        }

        /// <summary>
        ///     Уведомление об отказе в предоставлении услуг отдыха и оздоровления (лица из числа детей-сирот и детей, оставшихся
        ///     без попечения родителей)
        /// </summary>
        private static IDocument NotificationRefuseCompensationYouthRest(Request request, Account account)
        {
            using (var ms = new MemoryStream())
            {
                using (var wordDocument = WordprocessingDocument.Create(ms, WordprocessingDocumentType.Document))
                {
                    var mainPart = wordDocument.AddMainDocumentPart();
                    var doc = new Document(new Body());

                    var titleRequestRunProperties = new RunProperties().SetFont().SetFontSize();

                    var titleRequestRunPropertiesBold = titleRequestRunProperties.CloneNode(true);
                    titleRequestRunPropertiesBold.AppendChild(new Bold());

                    var titleRequestRunPropertiesItalic = titleRequestRunProperties.CloneNode(true);
                    titleRequestRunPropertiesItalic.AppendChild(new Italic());

                    var applicant = request.Applicant ?? new Applicant
                        {DocumentType = new DocumentType {Name = string.Empty}};

                    DocumentHeaderRegistration(doc);

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(new RunProperties().SetFont().SetFontSize(Size28).Bold(), new Text(Space))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(new RunProperties().SetFont().SetFontSize(Size28).Bold(), new Text(Space))));

                    doc.AppendChild(new Paragraph(
                        new ParagraphProperties(new Justification {Val = JustificationValues.Center}),
                        new Run(new RunProperties().SetFont().SetFontSize(Size28).Bold(),
                            new Text(
                                "Уведомление об отказе в выплате компенсации за самостоятельно приобретенную лицом из числа детей-сирот и детей, оставшихся без попечения родителей, путевку для отдыха и оздоровления"))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(new RunProperties().SetFont().SetFontSize(Size28).Bold(), new Text(Space))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Дата обращения: ")
                                    {Space = SpaceProcessingModeValues.Preserve}),
                            new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                new Text(request.DateRequest.FormatEx()))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Номер заявления: ") {Space = SpaceProcessingModeValues.Preserve}),
                            new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                new Text(request.RequestNumber.FormatEx()))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Данные заявителя: ") {Space = SpaceProcessingModeValues.Preserve}),
                            new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                new Text(
                                    $"{applicant.LastName} {applicant.FirstName} {applicant.MiddleName}, {applicant.DateOfBirth.FormatExGR(string.Empty)}"))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Контактная информация: ") {Space = SpaceProcessingModeValues.Preserve}),
                            new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                new Text(applicant.Phone + ", " + applicant.Email))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Результат рассмотрения заявления: ")
                                    {Space = SpaceProcessingModeValues.Preserve}),
                            new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                new Text(
                                    "отказ в предоставлении выплаты компенсации за самостоятельно приобретенную лицом из числа детей-сирот и детей, оставшихся без попечения родителей, путевку для отдыха и оздоровления."))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Both},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Основание: ") {Space = SpaceProcessingModeValues.Preserve}),
                            new Run(titleRequestRunProperties.CloneNode(true),
                                new Text(FederalLaw))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Both},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Причина отказа в выплате: ") {Space = SpaceProcessingModeValues.Preserve}),
                            new Run(titleRequestRunProperties.CloneNode(true),
                                new Text(request.DeclineReason?.Name))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(new RunProperties().SetFont().SetFontSize(Size28).Bold(), new Text(Space))));

                    SignBlockNotification(doc, account,
                        $"{applicant.LastName} {applicant.FirstName} {applicant.MiddleName}");

                    mainPart.Document = doc;
                }

                return new DocumentResult
                {
                    FileBody = ms.ToArray(),
                    FileName = "Уведомление об отказе.docx",
                    MimeType = mimeType,
                    MimeTypeShort = extention
                };
            }
        }

        /// <summary>
        ///     Уведомление о прекращении рассмотрения поданного заявления (заявитель отказался сам)
        /// </summary>
        private static IDocument NotificationRefuse1090(Request request, Account account)
        {
            var forMpguPortal = request.SourceId == (long) SourceEnum.Mpgu;

            if (forMpguPortal)
            {
                return PdfProcessor.NotificationRefuse1090(request);
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
                            new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(new RunProperties().SetFont().SetFontSize(Size28).Bold(), new Text(Space))));
                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(new RunProperties().SetFont().SetFontSize(Size28).Bold(), new Text(Space))));

                    doc.AppendChild(new Paragraph(
                        new ParagraphProperties(new Justification {Val = JustificationValues.Center}),
                        new Run(new RunProperties().SetFont().SetFontSize(Size28).Bold(),
                            new Text("Уведомление о прекращении рассмотрения поданного заявления"),
                            new Break(),
                            new Text("о предоставлении услуг отдыха и оздоровления")
                        )));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(new RunProperties().SetFont().SetFontSize(Size28).Bold(), new Text(Space))));

                    var titleRequestRunProperties = new RunProperties().SetFont().SetFontSize();

                    var titleRequestRunPropertiesBold = titleRequestRunProperties.CloneNode(true);
                    titleRequestRunPropertiesBold.AppendChild(new Bold());

                    var titleRequestRunPropertiesItalic = titleRequestRunProperties.CloneNode(true);

                    var applicant = request.NullSafe(r => r.Applicant) ?? new Applicant
                        {DocumentType = new DocumentType {Name = string.Empty}};

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Данные заявителя: ") {Space = SpaceProcessingModeValues.Preserve}),
                            new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                new Text(
                                    $"{applicant.LastName} {applicant.FirstName} {applicant.MiddleName}, {applicant.DateOfBirth.FormatExGR(string.Empty)}"))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Контактная информация: ") {Space = SpaceProcessingModeValues.Preserve}),
                            new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                new Text($"{applicant.Phone}, {applicant.Email}"))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Both},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Причина обращения: ") {Space = SpaceProcessingModeValues.Preserve}),
                            new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                new Text(
                                    "прекращение рассмотрения поданного заявления о предоставлении услуг отдыха и оздоровления по инициативе заявителя."))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Both},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Основание обращения: ")
                                    {Space = SpaceProcessingModeValues.Preserve}),
                            new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                new Text(
                                    $"пункт 5.13 Порядка организации отдыха и оздоровления детей, находящихся в трудной жизненной ситуации, утвержденного постановлением Правительства Москвы от 22 февраля 2017 г. № 56-ПП \"Об организации отдыха и оздоровления детей, находящихся в трудной жизненной ситуации\": \"Заявитель вправе отозвать заявление о предоставлении услуг отдыха и оздоровления в срок не позднее дня окончания стадии заявочной кампании по приему заявлений о предоставлении услуг отдыха и оздоровления (до 10 декабря {DateTime.Now.Year} г. включительно)\".")
                            )));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(new RunProperties().SetFont().SetFontSize(Size28).Bold(), new Text(Space))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Both},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Информация о заявлении:") {Space = SpaceProcessingModeValues.Preserve})));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                                new Indentation {FirstLine = FirstLineIndentation600.ToString()},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Дата и время регистрации заявления: ")
                                {
                                    Space = SpaceProcessingModeValues.Preserve
                                }),
                            new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                new Text(request.DateRequest.FormatEx()))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                                new Indentation {FirstLine = FirstLineIndentation600.ToString()},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Номер заявления: ") {Space = SpaceProcessingModeValues.Preserve}),
                            new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                new Text(request.RequestNumber.FormatEx()))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Both},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Результат рассмотрения заявления: ")
                                    {Space = SpaceProcessingModeValues.Preserve}),
                            new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                new Text("отозвано по инициативе заявителя в установленный срок."))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Both},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Дата прекращения рассмотрения заявления: ")
                                    {Space = SpaceProcessingModeValues.Preserve}),
                            new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                new Text(request.DateChangeStatus.FormatEx(string.Empty)))));

                   SignBlockNotification(doc, account, $"{applicant.LastName} {applicant.FirstName} {applicant.MiddleName}");

                    mainPart.Document = doc;
                }

                return new DocumentResult
                {
                    FileBody = ms.ToArray(),
                    FileName = "Уведомление о прекращении рассмотрения поданного заявления.docx",
                    MimeType = mimeType,
                    MimeTypeShort = extention
                };
            }
        }

        /// <summary>
        ///     уведомление об отказе в установленный срок
        /// </summary>
        private static IDocument NotificationDeadline(Request request, Account account)
        {
            using (var ms = new MemoryStream())
            {
                using (var wordDocument = WordprocessingDocument.Create(ms, WordprocessingDocumentType.Document))
                {
                    var isCert = request.TypeOfRestId == (long)TypeOfRestEnum.MoneyOn3To7 ||
                                 request.TypeOfRestId == (long)TypeOfRestEnum.MoneyOn7To15 ||
                                 request.TypeOfRestId == (long)TypeOfRestEnum.RestWithParentsInvalid ||
                                 request.TypeOfRestId == (long)TypeOfRestEnum.RestWithParentsInvalidOrphanComplex ||
                                 request.TypeOfRestId == (long)TypeOfRestEnum.MoneyOnInvalidOn4To17;

                    var mainPart = wordDocument.AddMainDocumentPart();
                    var doc = new Document(new Body());

                    var titleRequestRunProperties = new RunProperties().SetFont().SetFontSize();

                    var titleRequestRunPropertiesBold = titleRequestRunProperties.CloneNode(true);
                    titleRequestRunPropertiesBold.AppendChild(new Bold());

                    var titleRequestRunPropertiesItalic = titleRequestRunProperties.CloneNode(true);
                    titleRequestRunPropertiesItalic.AppendChild(new Italic());

                    var applicant = request.Applicant ?? new Applicant
                        {DocumentType = new DocumentType {Name = string.Empty}};

                    DocumentHeaderRegistration(doc);

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(new RunProperties().SetFont().SetFontSize(Size28).Bold(), new Text(Space))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(new RunProperties().SetFont().SetFontSize(Size28).Bold(), new Text(Space))));

                    if (isCert)
                    {
                        doc.AppendChild(new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Center}),
                            new Run(new RunProperties().SetFont().SetFontSize(Size28).Bold(),
                                new Text(
                                    "Уведомление о сформированном отказе от использования сертификата на отдых и оздоровление"))));

                        doc.AppendChild(
                            new Paragraph(
                                new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                                    new SpacingBetweenLines {After = Size20}),
                                new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                    new Text(
                                            "Сведения заявления об отказе от использования сертификата на отдых и оздоровление:")
                                        {Space = SpaceProcessingModeValues.Preserve})));
                    }
                    else
                    {
                        doc.AppendChild(new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Center}),
                            new Run(new RunProperties().SetFont().SetFontSize(Size28).Bold(),
                                new Text("Уведомление о сформированном отказе от осуществления отдыха"),
                                new Break(),
                                new Text(
                                    "и оздоровления на основании предоставленной бесплатной путевки"),
                                new Break(),
                                new Text(
                                    "для отдыха и оздоровления"),
                                new Break())));
                        doc.AppendChild(
                            new Paragraph(
                                new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                                    new SpacingBetweenLines {After = Size20}),
                                new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                    new Text("Сведения заявления об отказе от осуществления отдыха и оздоровления:")
                                        {Space = SpaceProcessingModeValues.Preserve})));
                    }

                    CertHandInput(doc);


                    if (isCert)
                    {
                        doc.AppendChild(
                            new Paragraph(
                                new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                                    new SpacingBetweenLines {After = Size20}),
                                new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                    new Text("Номер сертификата: ") {Space = SpaceProcessingModeValues.Preserve}),
                                new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                    new Text(request.CertificateNumber.FormatEx()))));
                    }
                    else
                    {
                        doc.AppendChild(
                            new Paragraph(
                                new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                                    new SpacingBetweenLines {After = Size20}),
                                new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                    new Text("Номер путевки: ") {Space = SpaceProcessingModeValues.Preserve}),
                                new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                    new Text(request.CertificateNumber.FormatEx()))));
                    }

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Данные заявителя: ") {Space = SpaceProcessingModeValues.Preserve}),
                            new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                new Text(
                                    $"{applicant.LastName} {applicant.FirstName} {applicant.MiddleName}, {applicant.DateOfBirth.FormatExGR(string.Empty)}"))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Контактная информация: ") {Space = SpaceProcessingModeValues.Preserve}),
                            new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                new Text(applicant.Phone + ", " + applicant.Email))));

                    if (request.Child != null)
                    {
                        var first = true;
                        foreach (var child in request.Child.Where(c => !c.IsDeleted))
                        {
                            if (isCert)
                            {
                                doc.AppendChild(
                                    new Paragraph(
                                        new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                                            new SpacingBetweenLines {After = Size20}),
                                        new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                            new Text("Данные лиц, указанных в сертификате: ")
                                                {Space = SpaceProcessingModeValues.Preserve},
                                            new Break()),
                                        new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                            new Text(
                                                $"{child.LastName} {child.FirstName} {child.MiddleName}, {child.DateOfBirth.FormatExGR(string.Empty)}"))));
                            }
                            else
                            {
                                var req = child.Request;
                                var attendants = new List<Applicant>();
                                if (req.Applicant?.IsAccomp ?? false)
                                {
                                    attendants.Add(req.Applicant);
                                }
                                else
                                {
                                    attendants = req.Attendant.ToList();
                                }

                                var personsRun = new Run(titleRequestRunPropertiesItalic.CloneNode(true));

                                if (first)
                                {
                                    foreach (var at in attendants)
                                    {
                                        personsRun.AppendChild(new Text(
                                            $"{at.LastName} {at.FirstName} {at.MiddleName}, {at.DateOfBirth.FormatExGR(string.Empty)}"));
                                        personsRun.AppendChild(new Break());
                                    }
                                }

                                personsRun.AppendChild(new Text(
                                    $"{child.LastName} {child.FirstName} {child.MiddleName}, {child.DateOfBirth.FormatExGR(string.Empty)}"));

                                doc.AppendChild(
                                    new Paragraph(
                                        new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                                            new SpacingBetweenLines {After = Size20}),
                                        new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                            new Text(first
                                                ? "Данные лиц, указанных в путевке: "
                                                : string.Empty) {Space = SpaceProcessingModeValues.Preserve}, first ? new Break() : null),
                                        personsRun
                                        ));
                            }

                            if (first)
                            {
                                first = false;
                            }
                        }
                    }


                    if (isCert)
                    {
                        doc.AppendChild(
                            new Paragraph(
                                new ParagraphProperties(new Justification {Val = JustificationValues.Both},
                                    new SpacingBetweenLines {After = Size20}),
                                new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                    new Text("Результат рассмотрения заявления: ")
                                        {Space = SpaceProcessingModeValues.Preserve}),
                                new Run(titleRequestRunProperties.CloneNode(true),
                                    new Text(
                                        "отказ от использования сертификата на отдых"),
                                    new Break(),
                                    new Text(
                                        "и оздоровление по инициативе заявителя."))));

                        doc.AppendChild(
                            new Paragraph(
                                new ParagraphProperties(new Justification {Val = JustificationValues.Both},
                                    new SpacingBetweenLines {After = Size20}),
                                new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                    new Text("Основание: ") {Space = SpaceProcessingModeValues.Preserve}),
                                new Run(titleRequestRunProperties.CloneNode(true),
                                    new Text(
                                        //"постановление Правительства Москвы от 22 февраля 2017 г. № 56-ПП "
                                        "пункт 8(1).10 Порядка организации отдыха и оздоровления детей, находящихся"),
                                    new Break(),
                                    new Text(
                                        "в трудной жизненной ситуации, утверждённого постановлением Правительства Москвы "),
                                    new Break(),
                                    new Text(
                                        "от 22 февраля 2017 г. № 56-ПП \"Об организации отдыха и оздоровления детей, находящихся "),
                                    new Break(),
                                    new Text(
                                        "в трудной жизненной ситуации\"."))));
                    }
                    else
                    {
                        doc.AppendChild(
                            new Paragraph(
                                new ParagraphProperties(new Justification {Val = JustificationValues.Both},
                                    new SpacingBetweenLines {After = Size20}),
                                new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                    new Text("Результат рассмотрения заявления: ")
                                        {Space = SpaceProcessingModeValues.Preserve}),
                                new Run(titleRequestRunProperties.CloneNode(true),
                                    new Text(
                                        "отказ от осуществления отдыха и оздоровления"),
                                    new Break(),
                                    new Text(
                                        "на основании бесплатной путевки для отдыха и оздоровления по инициативе заявителя."))));

                        doc.AppendChild(
                            new Paragraph(
                                new ParagraphProperties(new Justification {Val = JustificationValues.Both},
                                    new SpacingBetweenLines {After = Size20}),
                                new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                    new Text("Основание: ") {Space = SpaceProcessingModeValues.Preserve}),
                                new Run(titleRequestRunProperties.CloneNode(true),
                                    new Text(
                                        "пункты 10.1 и 10.2 Порядка организации отдыха и оздоровления детей, находящихся в трудной жизненной ситуации, утвержденного постановлением Правительства Москвы"),
                                    new Break(),
                                    new Text(
                                        "от 22 февраля 2017 г. № 56 - ПП \"Об организации отдыха и оздоровления детей, находящихся"),
                                    new Break(),
                                    new Text(
                                        "в трудной жизненной ситуации\"."))));
                    }

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(new RunProperties().SetFont().SetFontSize(Size28).Bold(), new Text(Space))));

                    SignBlockNotification2020(doc, account, "Исполнитель:");

                    mainPart.Document = doc;
                }

                return new DocumentResult
                {
                    FileBody = ms.ToArray(),
                    FileName = "Уведомление об отказе в установленный срок.docx",
                    MimeType = mimeType,
                    MimeTypeShort = extention
                };
            }
        }

        /// <summary>
        ///     Уведомление об отказе в предоставлении услуг отдыха и оздоровления (базовый)
        /// </summary>
        private static IDocument NotificationRefuse1080(Request request, Account account)
        {
            var forMpguPortal = request.SourceId == (long) SourceEnum.Mpgu;

            if (forMpguPortal)
            {
                return PdfProcessor.NotificationRefuse1080(request);
            }

            using (var ms = new MemoryStream())
            {
                using (var wordDocument = WordprocessingDocument.Create(ms, WordprocessingDocumentType.Document))
                {
                    var mainPart = wordDocument.AddMainDocumentPart();
                    var doc = new Document(new Body());

                    var titleRequestRunProperties = new RunProperties().SetFont().SetFontSize();

                    var titleRequestRunPropertiesBold = titleRequestRunProperties.CloneNode(true);
                    titleRequestRunPropertiesBold.AppendChild(new Bold());

                    var titleRequestRunPropertiesItalic = titleRequestRunProperties.CloneNode(true);
                    titleRequestRunPropertiesItalic.AppendChild(new Italic());

                    var applicant = request.Applicant ?? new Applicant
                        {DocumentType = new DocumentType {Name = string.Empty}};

                    DocumentHeaderRegistration(doc);

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(new RunProperties().SetFont().SetFontSize(Size28).Bold(), new Text(Space))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(new RunProperties().SetFont().SetFontSize(Size28).Bold(), new Text(Space))));

                    doc.AppendChild(new Paragraph(
                        new ParagraphProperties(new Justification {Val = JustificationValues.Center}),
                        new Run(new RunProperties().SetFont().SetFontSize(Size28).Bold(),
                            new Text("Уведомление об отказе в предоставлении услуг отдыха и оздоровления"))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(new RunProperties().SetFont().SetFontSize(Size28).Bold(), new Text(Space))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Дата и время регистрации заявления: ")
                                    {Space = SpaceProcessingModeValues.Preserve}),
                            new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                new Text(request.DateRequest.FormatEx()))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Номер заявления: ") {Space = SpaceProcessingModeValues.Preserve}),
                            new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                new Text(request.RequestNumber.FormatEx()))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Данные заявителя: ") {Space = SpaceProcessingModeValues.Preserve}),
                            new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                new Text(
                                    $"{applicant.LastName} {applicant.FirstName} {applicant.MiddleName}, {applicant.DateOfBirth.FormatExGR(string.Empty)}"))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Контактная информация: ") {Space = SpaceProcessingModeValues.Preserve}),
                            new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                new Text(applicant.Phone + ", " + applicant.Email))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Вид услуги: ") {Space = SpaceProcessingModeValues.Preserve}),
                            new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                new Text(request.TypeOfRest?.Name))));


                    if (request.Child != null)
                    {
                        foreach (var child in request.Child.Where(c => !c.IsDeleted))
                        {
                            doc.AppendChild(
                                new Paragraph(
                                    new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                                        new SpacingBetweenLines {After = Size20}),
                                    new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                        new Text(
                                                "Данные ребенка/лица из числа детей-сирот и детей, оставшихся без попечения родителей: ")
                                            {Space = SpaceProcessingModeValues.Preserve}),
                                    new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                        new Text(
                                            $"{child.LastName} {child.FirstName} {child.MiddleName}, {child.DateOfBirth.FormatExGR(string.Empty)}"))));

                            doc.AppendChild(
                                new Paragraph(
                                    new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                                        new SpacingBetweenLines {After = Size20}),
                                    new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                        new Text("Льготная категория: ")
                                            {Space = SpaceProcessingModeValues.Preserve}),
                                    new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                        new Text($"{child.BenefitType?.Name}"))));
                        }
                    }

                    if (request.TypeOfRestId == (long) TypeOfRestEnum.YouthRestCamps ||
                        request.TypeOfRestId == (long) TypeOfRestEnum.YouthRestOrphanCamps)
                    {
                        doc.AppendChild(
                            new Paragraph(
                                new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                                    new SpacingBetweenLines {After = Size20}),
                                new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                    new Text(
                                            "Данные ребёнка/лица из числа детей-сирот и детей, оставшихся без попечения родителей: ")
                                        {Space = SpaceProcessingModeValues.Preserve}),
                                new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                    new Text(
                                        $"{applicant.LastName} {applicant.FirstName} {applicant.MiddleName}, {applicant.DateOfBirth.FormatExGR(string.Empty)}"))));
                        doc.AppendChild(
                            new Paragraph(
                                new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                                    new SpacingBetweenLines {After = Size20}),
                                new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                    new Text("Льготная категория: ")
                                        {Space = SpaceProcessingModeValues.Preserve}),
                                new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                    new Text(
                                        "Дети-сироты и дети, оставшиеся без попечения родителей, в возрасте от 18 до 23 лет"))));
                    }

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Результат рассмотрения заявления: ")
                                    {Space = SpaceProcessingModeValues.Preserve}),
                            new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                new Text("отказ в предоставлении услуг отдыха и оздоровления."))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Both},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Основание отказа: ") {Space = SpaceProcessingModeValues.Preserve}),
                            new Run(titleRequestRunProperties.CloneNode(true),
                                new Text(FederalLaw))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Both},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Причина отказа: ") {Space = SpaceProcessingModeValues.Preserve}),
                            new Run(titleRequestRunProperties.CloneNode(true),
                                new Text(request.DeclineReason?.Name))));


                    SignBlockNotification2020(doc, account, "Исполнитель:");
                    mainPart.Document = doc;
                }

                return new DocumentResult
                {
                    FileBody = ms.ToArray(),
                    FileName = "Уведомление об отказе в предоставлении услуг отдыха и оздоровления.docx",
                    MimeType = mimeType,
                    MimeTypeShort = extention
                };
            }
        }

        /// <summary>
        ///     Уведомление об отказе в предоставлении услуг отдыха и оздоровления в связи с представлением документов, не
        ///     соответствующих требованиям
        /// </summary>
        private static IDocument NotificationRefuse10802(Request request, Account account)
        {
            var forMpguPortal = request.SourceId == (long) SourceEnum.Mpgu;

            if (forMpguPortal)
            {
                return PdfProcessor.NotificationRefuse10802(request);
            }

            using (var ms = new MemoryStream())
            {
                using (var wordDocument = WordprocessingDocument.Create(ms, WordprocessingDocumentType.Document))
                {
                    var mainPart = wordDocument.AddMainDocumentPart();
                    var doc = new Document(new Body());

                    var titleRequestRunProperties = new RunProperties().SetFont().SetFontSize();

                    var titleRequestRunPropertiesBold = titleRequestRunProperties.CloneNode(true);
                    titleRequestRunPropertiesBold.AppendChild(new Bold());

                    var titleRequestRunPropertiesItalic = titleRequestRunProperties.CloneNode(true);
                    titleRequestRunPropertiesItalic.AppendChild(new Italic());

                    var applicant = request.Applicant ?? new Applicant
                        {DocumentType = new DocumentType {Name = string.Empty}};

                    DocumentHeaderRegistration(doc);

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(new RunProperties().SetFont().SetFontSize(Size28).Bold(), new Text(Space))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(new RunProperties().SetFont().SetFontSize(Size28).Bold(), new Text(Space))));

                    doc.AppendChild(new Paragraph(
                        new ParagraphProperties(new Justification {Val = JustificationValues.Center}),
                        new Run(new RunProperties().SetFont().SetFontSize(Size28).Bold(),
                            new Text("Уведомление об отказе в предоставлении услуг отдыха и оздоровления"),
                            new Break(),
                            new Text("в связи с представлением документов, не соответствующих требованиям"))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(new RunProperties().SetFont().SetFontSize(Size28).Bold(), new Text(Space))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Дата и время регистрации заявления: ")
                                    {Space = SpaceProcessingModeValues.Preserve}),
                            new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                new Text(request.DateRequest.FormatEx()))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Номер заявления: ") {Space = SpaceProcessingModeValues.Preserve}),
                            new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                new Text(request.RequestNumber.FormatEx()))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Данные заявителя: ") {Space = SpaceProcessingModeValues.Preserve}),
                            new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                new Text(
                                    $"{applicant.LastName} {applicant.FirstName} {applicant.MiddleName}, {applicant.DateOfBirth.FormatExGR(string.Empty)}"))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Контактная информация: ") {Space = SpaceProcessingModeValues.Preserve}),
                            new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                new Text(applicant.Phone + ", " + applicant.Email))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Вид услуги: ") {Space = SpaceProcessingModeValues.Preserve}),
                            new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                new Text(request.TypeOfRest?.Name))));


                    if (request.Child != null)
                    {
                        foreach (var child in request.Child.Where(c => !c.IsDeleted))
                        {
                            doc.AppendChild(
                                new Paragraph(
                                    new ParagraphProperties(new Justification {Val = JustificationValues.Both},
                                        new SpacingBetweenLines {After = Size20}),
                                    new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                        new Text(
                                                "Данные ребёнка/лица из числа детей сирот и детей, оставшихся без попечения родителей: ")
                                            {Space = SpaceProcessingModeValues.Preserve}),
                                    new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                        new Text(
                                            $"{child.LastName} {child.FirstName} {child.MiddleName}, {child.DateOfBirth.FormatExGR(string.Empty)}"))));

                            doc.AppendChild(
                                new Paragraph(
                                    new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                                        new SpacingBetweenLines {After = Size20}),
                                    new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                        new Text("Льготная категория: ")
                                            {Space = SpaceProcessingModeValues.Preserve}),
                                    new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                        new Text($"{child.BenefitType?.Name}"))));
                        }
                    }

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Both},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Результат рассмотрения заявления: ")
                                    {Space = SpaceProcessingModeValues.Preserve}),
                            new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                new Text("отказ в предоставлении услуг отдыха и оздоровления."))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Both},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Основание отказа: ") {Space = SpaceProcessingModeValues.Preserve}),
                            new Run(titleRequestRunProperties.CloneNode(true),
                                new Text(
                                    "Порядок организации отдыха и оздоровления детей, находящихся в трудной жизненной ситуации, утвержденный постановлением Правительства Москвы от 22 февраля 2017 г. № 56-ПП \"Об организации отдыха и оздоровления детей, находящихся в трудной жизненной ситуации\" (далее – Порядок)."))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Both},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Причина отказа: ") {Space = SpaceProcessingModeValues.Preserve}),
                            new Run(titleRequestRunProperties.CloneNode(true),
                                new Text(request.DeclineReason?.Name))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(new RunProperties().SetFont().SetFontSize(Size28).Bold(), new Text(Space))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Center},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Уважаемый заявитель!") {Space = SpaceProcessingModeValues.Preserve})));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Center},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Просим обратить внимание на следующую информацию.")
                                    {Space = SpaceProcessingModeValues.Preserve})));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Center},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(new RunProperties().SetFont().SetFontSize(Size28).Bold(), new Text(Space))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Both},
                                new Indentation {FirstLine = FirstLineIndentation600.ToString()},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                new Text(
                                        "В случае принятия решения о повторной подаче заявления, рекомендуем убедиться в корректности внесенных в заявление сведений.")
                                    {Space = SpaceProcessingModeValues.Preserve})));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Both},
                                new Indentation {FirstLine = FirstLineIndentation600.ToString()},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                new Text(
                                        "Обращаем внимание, что сведения, указанные в заявлении, должны полностью соответствовать сведениям, указанным в СНИЛС и документе, удостоверяющем личность заявителя, ребёнка/детей (в том числе следует обратить внимание на соответствие написания букв \"е - ё\").")
                                    {Space = SpaceProcessingModeValues.Preserve})));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Both},
                                new Indentation {FirstLine = FirstLineIndentation600.ToString()},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                new Text(
                                        "В случае если сведения о Вас и ребёнке/детях внесены в \"личный кабинет\" Портала Мэра и Правительства Москвы (при подаче заявления данные отразились в полях автоматически), рекомендуем Вам проверить корректность внесенных сведений, а именно: сверить данные, указанные в \"личном кабинете\" Портала Мэра и Правительства Москвы и данные, указанные в СНИЛС и документе, удостоверяющем личность: фамилия, имя, отчество, пол, дата рождения.")
                                    {Space = SpaceProcessingModeValues.Preserve})));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Both},
                                new Indentation {FirstLine = FirstLineIndentation600.ToString()},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                new Text(
                                        "В случае если Вами обнаружена ошибка в \"личном кабинете\" Портала Мэра и Правительства Москвы, ее необходимо исправить.")
                                    {Space = SpaceProcessingModeValues.Preserve})));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Both},
                                new Indentation {FirstLine = FirstLineIndentation600.ToString()},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                new Text(
                                        "Дополнительно сообщаем, что в случае обнаружения расхождений сведений в СНИЛС и документе, удостоверяющем личность, Вам необходимо актуализировать документы в соответствующих органах, приведя их к единообразию.")
                                    {Space = SpaceProcessingModeValues.Preserve})));

                    //SignBlockNotification(doc, account, $"{applicant.LastName} {applicant.FirstName} {applicant.MiddleName}");
                    SignBlockNotification2020(doc, account, "Исполнитель");
                    mainPart.Document = doc;
                }

                return new DocumentResult
                {
                    FileBody = ms.ToArray(),
                    FileName = "Уведомление об отказе в предоставлении услуг отдыха и оздоровления.docx",
                    MimeType = mimeType,
                    MimeTypeShort = extention
                };
            }
        }

        /// <summary>
        ///     Уведомление об отказе в предоставлении услуг отдыха (по квоте)
        /// </summary>
        private static IDocument NotificationRefuse10805(IUnitOfWork unitOfWork, Request request, Account account)
        {
            var forMpguPortal = request.SourceId == (long) SourceEnum.Mpgu;

            var lokYear = request.YearOfRest?.Year ?? 2021;
            var yearIds = unitOfWork.GetSet<YearOfRest>().Where(ss => ss.Year < lokYear).OrderByDescending(ss => ss.Year)
                .Take(3).Select(ss => ss.Id).ToList();

            if (forMpguPortal)
            {
                return PdfProcessor.NotificationRefuse10805(unitOfWork, request, yearIds);
            }

            var listTravelersRequest = unitOfWork.GetSet<ListTravelersRequest>()
                .FirstOrDefault(ss => ss.RequestId == request.Id);


            using (var ms = new MemoryStream())
            {
                using (var wordDocument = WordprocessingDocument.Create(ms, WordprocessingDocumentType.Document))
                {
                    var mainPart = wordDocument.AddMainDocumentPart();
                    var doc = new Document(new Body());

                    var titleRequestRunProperties = new RunProperties().SetFont().SetFontSize();

                    var titleRequestRunPropertiesBold = titleRequestRunProperties.CloneNode(true);
                    titleRequestRunPropertiesBold.AppendChild(new Bold());

                    var titleRequestRunPropertiesItalic = titleRequestRunProperties.CloneNode(true);
                    titleRequestRunPropertiesItalic.AppendChild(new Italic());

                    var applicant = request.Applicant ?? new Applicant
                        {DocumentType = new DocumentType {Name = string.Empty}};

                    DocumentHeaderRegistration(doc);

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(new RunProperties().SetFont().SetFontSize(Size28).Bold(), new Text(Space))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(new RunProperties().SetFont().SetFontSize(Size28).Bold(), new Text(Space))));

                    doc.AppendChild(new Paragraph(
                        new ParagraphProperties(new Justification {Val = JustificationValues.Center}),
                        new Run(new RunProperties().SetFont().SetFontSize(Size28).Bold(),
                            new Text(
                                "Уведомление о результатах рассмотрения заявления о предоставлении услуг отдыха и оздоровления"))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(new RunProperties().SetFont().SetFontSize(Size28).Bold(), new Text(Space))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Дата и время регистрации заявления: ")
                                    {Space = SpaceProcessingModeValues.Preserve}),
                            new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                new Text(request.DateRequest.FormatEx()))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Номер заявления: ") {Space = SpaceProcessingModeValues.Preserve}),
                            new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                new Text(request.RequestNumber.FormatEx()))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Данные заявителя: ") {Space = SpaceProcessingModeValues.Preserve}),
                            new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                new Text(
                                    $"{applicant.LastName} {applicant.FirstName} {applicant.MiddleName}, {applicant.DateOfBirth.FormatExGR(string.Empty)}"))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Контактная информация: ") {Space = SpaceProcessingModeValues.Preserve}),
                            new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                new Text(applicant.Phone + ", " + applicant.Email))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Вид услуги: ") {Space = SpaceProcessingModeValues.Preserve}),
                            new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                new Text(
                                    "бесплатная путевка для отдыха и оздоровления/сертификат на отдых и оздоровление"))));


                    foreach (var child in request.Child?.Where(c => !c.IsDeleted) ?? new List<Child>())
                    {
                        doc.AppendChild(
                            new Paragraph(
                                new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                                    new SpacingBetweenLines {After = Size20}),
                                new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                    new Text("Данные ребёнка: ") {Space = SpaceProcessingModeValues.Preserve}),
                                new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                    new Text(
                                        $"{child.LastName} {child.FirstName} {child.MiddleName}, {child.DateOfBirth.FormatExGR(string.Empty)}"))));

                        doc.AppendChild(
                            new Paragraph(
                                new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                                    new SpacingBetweenLines {After = Size20}),
                                new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                    new Text("Льготная категория ребёнка: ")
                                        {Space = SpaceProcessingModeValues.Preserve}),
                                new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                    new Text($"{child.BenefitType?.Name}"))));
                    }

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Both},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Результат рассмотрения заявления: ")
                                    {Space = SpaceProcessingModeValues.Preserve}),
                            new Run(titleRequestRunProperties.CloneNode(true),
                                new Text(
                                    $"учитывая получение услуг отдыха и оздоровления в предыдущие годы и исходя из даты и времени подачи заявления, оказание услуг отдыха и оздоровления в {request.TimeOfRest.Year} году не представляется возможным."))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Both},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Основание: ") {Space = SpaceProcessingModeValues.Preserve}),
                            new Run(titleRequestRunProperties.CloneNode(true),
                                new Text(
                                    "пункты 3.9. и 9.1.1. Порядка организации отдыха и оздоровления детей, находящихся в трудной жизненной ситуации, утвержденного постановлением Правительства Москвы от 22 февраля 2017 г. № 56-ПП \"Об организации отдыха и оздоровления детей, находящихся в трудной жизненной ситуации\"."))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                new SpacingBetweenLines { After = Size20 }),
                            new Run(new RunProperties().SetFont().SetFontSize(Size28).Bold(),
                                new Text(Space))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                new SpacingBetweenLines { After = Size20 }),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text(
                                        "Информация об услугах, оказанных ребёнку/детям в течение последних 3-х лет")
                                { Space = SpaceProcessingModeValues.Preserve })));


                    //if ((request.Child?.Any(c => !c.IsDeleted) ?? false) && listTravelersRequest != null &&
                    //    listTravelersRequest.Details.Any(ss => ss.Detail != "[]"))
                    //{
                    var details = listTravelersRequest?.Details.Where(ss => ss.Detail != "[]")
                        .Select(ss => ss.Detail).ToList().SelectMany(JsonConvert.DeserializeObject<DetailInfo[]>)
                        .ToArray();

                    IEnumerable<int> years = unitOfWork.GetSet<YearOfRest>().Where(x => yearIds.Contains(x.Id)).Select(x => x.Year).OrderBy(x=>x).ToList();

                    IEnumerable<Request> requests = new List<Request>();

                    foreach (var child in request.Child.Where(c => !c.IsDeleted).ToList())
                    {
                        var requestIds = details?.Where(ss => ss.ChildId == child.Id).Select(ss => ss.Id).Distinct().ToList()?? new List<long>();

                        requests = unitOfWork.GetSet<Request>().Where(re => requestIds.Any(req => req == re.Id)).ToList();

                        doc.AppendChild(
                            new Paragraph(new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                                          new SpacingBetweenLines {After = Size20}),
                                          new Run(titleRequestRunProperties.CloneNode(true),
                                                  new Text($"{child.LastName} {child.FirstName} {child.MiddleName}, {child.DateOfBirth.FormatExGR(string.Empty)}, {child.Snils}"))));

                        AddTableChildTours(doc, requests, years);

                    }

                    SignWorkerBlock(doc, account,"Исполнитель:");
                    mainPart.Document = doc;
                }

                return new DocumentResult
                {
                    FileBody = ms.ToArray(),
                    FileName = "Уведомление о результатах рассмотрения заявления.docx",
                    MimeType = mimeType,
                    MimeTypeShort = extention
                };
            }
        }

        /// <summary>
        ///     Уведомление об отказе в предоставлении услуг отдыха и оздоровления в связи с неучастием заявителя во втором этапе заявочной кампании
        /// </summary>
        private static IDocument NotificationRefuse108013(IUnitOfWork unitOfWork, Request request, Account account)
        {
            var forMpguPortal = request.SourceId == (long)SourceEnum.Mpgu;

            if (forMpguPortal)
            {
                return PdfProcessor.NotificationRefuse108013(request);
            }

            using (var ms = new MemoryStream())
            {
                using (var wordDocument = WordprocessingDocument.Create(ms, WordprocessingDocumentType.Document))
                {
                    var mainPart = wordDocument.AddMainDocumentPart();
                    var doc = new Document(new Body());

                    var titleRequestRunProperties = new RunProperties().SetFont().SetFontSize();

                    var titleRequestRunPropertiesBold = titleRequestRunProperties.CloneNode(true);
                    titleRequestRunPropertiesBold.AppendChild(new Bold());

                    var titleRequestRunPropertiesItalic = titleRequestRunProperties.CloneNode(true);
                    titleRequestRunPropertiesItalic.AppendChild(new Italic());

                    var applicant = request.Applicant ?? new Applicant
                    { DocumentType = new DocumentType { Name = string.Empty } };

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
                            new Text("Уведомление об отказе в предоставлении услуг отдыха и оздоровления"))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                new SpacingBetweenLines { After = Size20 }),
                            new Run(new RunProperties().SetFont().SetFontSize(Size28).Bold(), new Text(Space))));

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
                                                "Данные ребенка (детей) /лица из числа детей-сирот и детей, оставшихся без попечения родителей: ")
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

                    if (request.TypeOfRestId == (long)TypeOfRestEnum.YouthRestCamps ||
                        request.TypeOfRestId == (long)TypeOfRestEnum.YouthRestOrphanCamps)
                    {
                        doc.AppendChild(
                            new Paragraph(
                                new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                    new SpacingBetweenLines { After = Size20 }),
                                new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                    new Text(
                                            "Данные ребёнка (детей) /лица из числа детей-сирот и детей, оставшихся без попечения родителей: ")
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
                                    new Text(
                                        "Дети-сироты и дети, оставшиеся без попечения родителей, в возрасте от 18 до 23 лет"))));
                    }

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                new SpacingBetweenLines { After = Size20 }),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Результат рассмотрения заявления: ")
                                { Space = SpaceProcessingModeValues.Preserve }),
                            new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                new Text("отказ в предоставлении услуг отдыха и оздоровления."))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Both },
                                new SpacingBetweenLines { After = Size20 }),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Основание отказа: ") { Space = SpaceProcessingModeValues.Preserve }),
                            new Run(titleRequestRunProperties.CloneNode(true),
                                new Text(FederalLawReference))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Both },
                                new SpacingBetweenLines { After = Size20 }),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Причина отказа: ") { Space = SpaceProcessingModeValues.Preserve }),
                            new Run(titleRequestRunProperties.CloneNode(true),
                                new Text(ParticipateNotification))));


                    SignBlockNotification2020(doc, account, "Исполнитель:");
                    mainPart.Document = doc;
                }

                return new DocumentResult
                {
                    FileBody = ms.ToArray(),
                    FileName = "Уведомление об отказе в предоставлении услуг отдыха и оздоровления.docx",
                    MimeType = mimeType,
                    MimeTypeShort = extention
                };
            }
        }

        /// <summary>
        ///     получить данные по уведомлению
        /// </summary>
        public static IDocument NotificationRefuseContentEx(Request request)
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
                            new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(new RunProperties().SetFont().SetFontSize(Size28).Bold(), new Text(Space))));
                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(new RunProperties().SetFont().SetFontSize(Size28).Bold(), new Text(Space))));

                    doc.AppendChild(new Paragraph(
                        new ParagraphProperties(new Justification {Val = JustificationValues.Center}),
                        new Run(new RunProperties().SetFont().SetFontSize(Size28).Bold(),
                            new Text("Уведомление об отказе в предоставлении услуг отдыха и оздоровления"))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(new RunProperties().SetFont().SetFontSize(Size28).Bold(), new Text(Space))));

                    var titleRequestRunProperties = new RunProperties().SetFont().SetFontSize();

                    var titleRequestRunPropertiesBold = titleRequestRunProperties.CloneNode(true);
                    titleRequestRunPropertiesBold.AppendChild(new Bold());

                    var titleRequestRunPropertiesItalic = titleRequestRunProperties.CloneNode(true);

                    var applicant = request.NullSafe(r => r.Applicant) ??
                                    new Applicant {DocumentType = new DocumentType {Name = string.Empty}};


                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Дата и время регистрации заявления: ")
                                {
                                    Space = SpaceProcessingModeValues.Preserve
                                }),
                            new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                new Text(request.DateRequest.FormatEx()))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Номер заявления: ") {Space = SpaceProcessingModeValues.Preserve}),
                            new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                new Text(request.RequestNumber.FormatEx()))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Данные заявителя: ") {Space = SpaceProcessingModeValues.Preserve}),
                            new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                new Text(
                                    $"{applicant.LastName} {applicant.FirstName} {applicant.MiddleName}, {applicant.DateOfBirth.FormatExGR(string.Empty)}"))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Контактная информация: ") {Space = SpaceProcessingModeValues.Preserve}),
                            new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                new Text(applicant.Phone + ", " + applicant.Email))));


                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Вид услуги: ") {Space = SpaceProcessingModeValues.Preserve}),
                            new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                new Text(
                                    "бесплатная путевка для отдыха и оздоровления/сертификат на получение выплаты на самостоятельную организацию отдыха и оздоровления"))));

                    AppendChildrenToDocument(doc, request);

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Both},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Результат рассмотрения заявления: ")
                                    {Space = SpaceProcessingModeValues.Preserve}),
                            new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                new Text(
                                    "отказ в предоставлении услуг отдыха и оздоровления в связи с отсутствием квоты на отдых и оздоровление."))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Both},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Причина отказа: ") {Space = SpaceProcessingModeValues.Preserve}),
                            new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                new Text(
                                    "16 января 2019 г. проведено заседание комиссии в целях принятия решения о включении сведений о детях, находящихся в трудной жизненной ситуации, сопровождающих их лицах (в случае организации совместного выездного отдыха), лицах из числа детей сирот и детей, оставшихся без попечения родителей, в предварительный Реестр получателей услуг отдыха и оздоровления в соответствии с очередностью, которая учитывает предоставленные услуги отдыха и оздоровления в 2016-2018 годах, а также дату и время подачи заявления."))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Both},
                                new SpacingBetweenLines {After = Size20}, new Indentation {FirstLine = "500"}),
                            new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                new Text(
                                    "Установление очередности рассмотрения заявлений о предоставлении услуг отдыха и оздоровления осуществляется автоматизированной информационной системой \"Детский отдых\" в пределах утвержденного количества мест для соответствующего вида отдыха и оздоровления и соответствующей льготной категории детей."))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Both},
                                new SpacingBetweenLines {After = Size20}, new Indentation {FirstLine = "500"}),
                            new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                new Text(
                                    "В первую очередь рассмотрены заявления о предоставлении услуг отдыха и оздоровления детям, которым такие услуги в последние три года не предоставлялись."))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Both},
                                new SpacingBetweenLines {After = Size20}, new Indentation {FirstLine = "500"}),
                            new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                new Text(
                                    "В 2019 году детям, указанным в заявлениях, которым предоставлялись услуги отдыха и оздоровления в 2016-2018 годах, в связи с отсутствием мест в квоте на отдых и оздоровление для указанной льготной категории детей услуга не предоставляется и сведения о них не включены в предварительный Реестр получателей услуг отдыха и оздоровления."))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Both},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Основание: ") {Space = SpaceProcessingModeValues.Preserve}),
                            new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                new Text(
                                    "Порядок организации отдыха и оздоровления детей, находящихся в трудной жизненной ситуации, утвержденный постановлением Правительства Москвы от 22 февраля 2017 г. № 56-ПП \"Об организации отдыха и оздоровления детей, находящихся в трудной жизненной ситуации\"."))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(new RunProperties().SetFont().SetFontSize(Size28).Bold(), new Text(Space))));

                    mainPart.Document = doc;
                }

                return new DocumentResult
                {
                    FileBody = ms.ToArray(),
                    FileName = "Уведомление об отказе в предоставлении услуг отдыха и оздоровления.docx",
                    MimeType = mimeType,
                    MimeTypeShort = extention
                };
            }
        }

        /// <summary>
        ///     получить данные по уведомлению
        /// </summary>
        private static IDocument NotificationRefuseContent(Request request, Account account)
        {
            using (var ms = new MemoryStream())
            {
                string notificationName;
                using (var wordDocument = WordprocessingDocument.Create(ms, WordprocessingDocumentType.Document))
                {
                    var mainPart = wordDocument.AddMainDocumentPart();
                    var doc = new Document(new Body());

                    DocumentHeaderRegistration(doc);

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(new RunProperties().SetFont().SetFontSize(Size28).Bold(), new Text(Space))));
                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(new RunProperties().SetFont().SetFontSize(Size28).Bold(), new Text(Space))));

                    notificationName = "Уведомление о рассмотрении заявления";

                    if (request.StatusId == (long) StatusEnum.CancelByApplicant)
                    {
                        notificationName =
                            "Уведомление о прекращении рассмотрения поданного заявления о предоставлении услуг отдыха и оздоровления";
                    }
                    else if (request.StatusId == (long) StatusEnum.Reject)
                    {
                        notificationName = request.TypeOfRestId == (long) TypeOfRestEnum.Compensation ||
                                           request.TypeOfRestId == (long) TypeOfRestEnum.CompensationYouthRest
                            ? "Уведомление об отказе в выплате компенсации за самостоятельно приобретенную родителями или иными законными представителями путевку для отдыха и оздоровления"
                            : "Уведомление об отказе в предоставлении услуг отдыха и оздоровления";
                    }
                    else if (request.StatusId == (long) StatusEnum.CertificateIssued)
                    {
                        notificationName = request.TypeOfRestId == (long) TypeOfRestEnum.Compensation ||
                                           request.TypeOfRestId == (long) TypeOfRestEnum.CompensationYouthRest
                            ? "Уведомление о предоставлении выплаты компенсации за самостоятельно приобретенную родителями или иными законными представителями путевку для отдыха и оздоровления"
                            : "Уведомление о неучастии заявителя в выборе конкретной организации отдыха и оздоровления";
                    }

                    doc.AppendChild(new Paragraph(
                        new ParagraphProperties(new Justification {Val = JustificationValues.Center}),
                        new Run(new RunProperties().SetFont().SetFontSize(Size28).Bold(),
                            new Text(notificationName))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(new RunProperties().SetFont().SetFontSize(Size28).Bold(), new Text(Space))));

                    var titleRequestRunProperties = new RunProperties().SetFont().SetFontSize();

                    var titleRequestRunPropertiesBold = titleRequestRunProperties.CloneNode(true);
                    titleRequestRunPropertiesBold.AppendChild(new Bold());

                    var titleRequestRunPropertiesItalic = titleRequestRunProperties.CloneNode(true);

                    var applicant = request.NullSafe(r => r.Applicant) ??
                                    new Applicant {DocumentType = new DocumentType {Name = string.Empty}};

                    if ((request.TypeOfRestId == (long) TypeOfRestEnum.Compensation ||
                         request.TypeOfRestId == (long) TypeOfRestEnum.CompensationYouthRest) &&
                        request.StatusId == (long) StatusEnum.Reject)
                    {
                        doc.AppendChild(
                            new Paragraph(
                                new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                                    new SpacingBetweenLines {After = Size20}),
                                new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                    new Text("Дата обращения: ") {Space = SpaceProcessingModeValues.Preserve}),
                                new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                    new Text(request.DateRequest?.Date.FormatEx()))));
                    }
                    else
                    {
                        doc.AppendChild(
                            new Paragraph(
                                new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                                    new SpacingBetweenLines {After = Size20}),
                                new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                    new Text("Дата и время регистрации заявления: ")
                                    {
                                        Space = SpaceProcessingModeValues.Preserve
                                    }),
                                new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                    new Text(request.DateRequest.FormatEx()))));
                    }

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Номер заявления: ") {Space = SpaceProcessingModeValues.Preserve}),
                            new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                new Text(request.RequestNumber.FormatEx()))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Данные заявителя: ") {Space = SpaceProcessingModeValues.Preserve}),
                            new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                new Text(
                                    $"{applicant.LastName} {applicant.FirstName} {applicant.MiddleName}, {applicant.DateOfBirth.FormatEx(string.Empty)}"))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Контактная информация: ") {Space = SpaceProcessingModeValues.Preserve}),
                            new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                new Text(applicant.Phone + ", " + applicant.Email))));

                    if (!request.IsFirstCompany)
                    {
                        if (request.StatusId == (long) StatusEnum.CancelByApplicant)
                        {
                            doc.AppendChild(
                                new Paragraph(
                                    new ParagraphProperties(new Justification {Val = JustificationValues.Both},
                                        new SpacingBetweenLines {After = Size20}),
                                    new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                        new Text("Причина обращения: ") {Space = SpaceProcessingModeValues.Preserve}),
                                    new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                        new Text(
                                            "прекращение рассмотрения поданного заявления о предоставлении услуг отдыха и оздоровления по инициативе заявителя."))));

                            doc.AppendChild(
                                new Paragraph(
                                    new ParagraphProperties(new Justification {Val = JustificationValues.Both},
                                        new SpacingBetweenLines {After = Size20}),
                                    new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                        new Text("Результат рассмотрения заявления: ")
                                            {Space = SpaceProcessingModeValues.Preserve}),
                                    new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                        new Text("отозвано по инициативе заявителя в установленный Порядком срок."))));
                        }
                        else
                        {
                            if (request.TypeOfRestId != (long) TypeOfRestEnum.Compensation &&
                                request.TypeOfRestId != (long) TypeOfRestEnum.CompensationYouthRest)
                            {
                                doc.AppendChild(
                                    new Paragraph(
                                        new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                                            new SpacingBetweenLines {After = Size20}),
                                        new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                            new Text("Наименование запрашиваемой услуги:  ")
                                            {
                                                Space = SpaceProcessingModeValues.Preserve
                                            }),
                                        new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                            new Text("___________________________________________"))));

                                doc.AppendChild(
                                    new Paragraph(
                                        new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                                            new SpacingBetweenLines {After = Size20}),
                                        new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                            new Text("Цель обращения: ") {Space = SpaceProcessingModeValues.Preserve}),
                                        new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                            new Text(request.TypeOfRest?.Name))));

                                doc.AppendChild(
                                    new Paragraph(
                                        new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                                            new SpacingBetweenLines {After = Size20}),
                                        new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                            new Text("Организация отдыха и оздоровления: ")
                                            {
                                                Space = SpaceProcessingModeValues.Preserve
                                            }),
                                        new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                            new Text(request.Hotels?.Name ??
                                                     request.Tour?.Hotels?.Name ?? string.Empty))));
                            }

                            if (request.Tour != null)
                            {
                                doc.AppendChild(
                                    new Paragraph(
                                        new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                                            new SpacingBetweenLines {After = Size20}),
                                        new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                            new Text("Время отдыха: ") {Space = SpaceProcessingModeValues.Preserve}),
                                        new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                            new Text("c " + request.Tour.DateIncome.FormatEx() + " по " +
                                                     request.Tour.DateOutcome.FormatEx()))));
                            }

                            if (request.TypeOfRestId != (long) TypeOfRestEnum.Compensation
                                && request.TypeOfRestId != (long) TypeOfRestEnum.CompensationYouthRest ||
                                request.StatusId != (long) StatusEnum.Reject)
                            {
                                AppendChildrenToDocument(doc, request);
                            }

                            if ((request.TypeOfRestId == (long) TypeOfRestEnum.Compensation
                                 || request.TypeOfRestId == (long) TypeOfRestEnum.CompensationYouthRest) &&
                                request.StatusId == (long) StatusEnum.Reject)
                            {
                                doc.AppendChild(
                                    new Paragraph(
                                        new ParagraphProperties(new Justification {Val = JustificationValues.Both},
                                            new SpacingBetweenLines {After = Size20}),
                                        new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                            new Text("Причина обращения: ")
                                                {Space = SpaceProcessingModeValues.Preserve}),
                                        new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                            new Text(
                                                "выплата компенсации за самостоятельно приобретенную родителями или иными законными представителями путевку."))));
                            }

                            doc.AppendChild(
                                new Paragraph(
                                    new ParagraphProperties(new Justification {Val = JustificationValues.Both},
                                        new SpacingBetweenLines {After = Size20}),
                                    new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                        new Text("Результат рассмотрения заявления: ")
                                            {Space = SpaceProcessingModeValues.Preserve}),
                                    new Run(titleRequestRunPropertiesItalic.CloneNode(true), new Text(
                                        (request.TypeOfRestId == (long) TypeOfRestEnum.Compensation
                                         || request.TypeOfRestId == (long) TypeOfRestEnum.CompensationYouthRest) &&
                                        request.StatusId == (long) StatusEnum.Reject
                                            ? "отказ в предоставлении выплаты компенсации за самостоятельно приобретенную родителями или иными законными представителями путевку для отдыха и оздоровления."
                                            : request.Status.Name))));
                        }


                        if ((request.TypeOfRestId == (long) TypeOfRestEnum.Compensation
                             || request.TypeOfRestId == (long) TypeOfRestEnum.CompensationYouthRest) &&
                            (request.StatusId == (long) StatusEnum.CertificateIssued ||
                             request.StatusId == (long) StatusEnum.Reject))
                        {
                            if (request.DeclineReason != null)
                            {
                                doc.AppendChild(
                                    new Paragraph(
                                        new ParagraphProperties(new Justification {Val = JustificationValues.Both},
                                            new SpacingBetweenLines {After = Size20}),
                                        new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                            new Text("Причина отказа в выплате: ")
                                                {Space = SpaceProcessingModeValues.Preserve}),
                                        new Run(titleRequestRunPropertiesItalic.CloneNode(true), new Text(
                                            request.DeclineReason.Name))));
                            }

                            doc.AppendChild(
                                new Paragraph(
                                    new ParagraphProperties(new Justification {Val = JustificationValues.Both},
                                        new SpacingBetweenLines {After = Size20}),
                                    new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                        new Text("Основание: ") {Space = SpaceProcessingModeValues.Preserve}),
                                    new Run(titleRequestRunProperties.CloneNode(true),
                                        new Text(FederalLaw))));
                        }
                        else if (request.DeclineReason != null)
                        {
                            doc.AppendChild(
                                new Paragraph(
                                    new ParagraphProperties(new Justification {Val = JustificationValues.Both},
                                        new SpacingBetweenLines {After = Size20}),
                                    new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                        new Text("Основание: ") {Space = SpaceProcessingModeValues.Preserve}),
                                    new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                        //new Text((request.TypeOfRestId == (long) TypeOfRestEnum.Compensation
                                        //	         ? "приказ Департамента культуры города Москвы от 12 января 2016 г. № 8 \"О выплате частичной компенсации стоимости самостоятельно приобретенной путевки на отдых и оздоровление детей и сопровождающих их лиц в 2016 году\", пункт "
                                        //	         : "") + request.DeclineReason.Name)
                                        new Text(FederalLaw))));
                        }
                    }
                    else if (request.StatusId == (long) StatusEnum.CancelByApplicant)
                    {
                        doc.AppendChild(
                            new Paragraph(
                                new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                                    new SpacingBetweenLines {After = Size20}),
                                new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                    new Text("Причина обращения: ") {Space = SpaceProcessingModeValues.Preserve}),
                                new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                    new Text(
                                        "прекращение рассмотрения поданного заявления о предоставлении услуг отдыха и оздоровления по инициативе заявителя."))));
                        doc.AppendChild(
                            new Paragraph(
                                new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                                    new SpacingBetweenLines {After = Size20}),
                                new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                    new Text("Результат рассмотрения заявления: ")
                                        {Space = SpaceProcessingModeValues.Preserve}),
                                new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                    new Text("отозвано по инициативе заявителя в установленный Порядком срок"))));

                        doc.AppendChild(
                            new Paragraph(
                                new ParagraphProperties(new Justification {Val = JustificationValues.Both},
                                    new SpacingBetweenLines {After = Size20}),
                                new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                    new Text("Основание: ") {Space = SpaceProcessingModeValues.Preserve}),
                                new Run(titleRequestRunProperties.CloneNode(true),
                                    new Text(FederalLaw))));

                        doc.AppendChild(
                            new Paragraph(
                                new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                                    new SpacingBetweenLines {After = Size20}),
                                new Run(new RunProperties().SetFont().SetFontSize(Size28).Bold(), new Text(Space))));
                    }
                    else if (request.StatusId == (long) StatusEnum.Reject)
                    {
                        doc.AppendChild(
                            new Paragraph(
                                new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                                    new SpacingBetweenLines {After = Size20}),
                                new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                    new Text("Вид услуги: ") {Space = SpaceProcessingModeValues.Preserve}),
                                new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                    new Text(request.TypeOfRest?.Name))));

                        AppendChildrenToDocument(doc, request);

                        doc.AppendChild(
                            new Paragraph(
                                new ParagraphProperties(new Justification {Val = JustificationValues.Both},
                                    new SpacingBetweenLines {After = Size20}),
                                new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                    new Text("Результат рассмотрения заявления: ")
                                        {Space = SpaceProcessingModeValues.Preserve}),
                                new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                    new Text("отказ в предоставлении услуг отдыха и оздоровления"))));

                        doc.AppendChild(
                            new Paragraph(
                                new ParagraphProperties(new Justification {Val = JustificationValues.Both},
                                    new SpacingBetweenLines {After = Size20}),
                                new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                    new Text("Причина отказа: ") {Space = SpaceProcessingModeValues.Preserve}),
                                new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                    new Text(request.DeclineReason?.Name ?? "-"))));

                        doc.AppendChild(
                            new Paragraph(
                                new ParagraphProperties(new Justification {Val = JustificationValues.Both},
                                    new SpacingBetweenLines {After = Size20}),
                                new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                    new Text("Основание: ") {Space = SpaceProcessingModeValues.Preserve}),
                                new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                    new Text(FederalLaw))));

                        doc.AppendChild(
                            new Paragraph(
                                new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                                    new SpacingBetweenLines {After = Size20}),
                                new Run(new RunProperties().SetFont().SetFontSize(Size28).Bold(), new Text(Space))));
                    }
                    else if (request.StatusId == (long) StatusEnum.CertificateIssued)
                    {
                        doc.AppendChild(
                            new Paragraph(
                                new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                                    new SpacingBetweenLines {After = Size20}),
                                new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                    new Text("Вид услуги: ") {Space = SpaceProcessingModeValues.Preserve}),
                                new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                    new Text(request.TypeOfRest?.Name))));

                        AppendChildrenToDocument(doc, request);

                        doc.AppendChild(
                            new Paragraph(
                                new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                                    new SpacingBetweenLines {After = Size20}),
                                new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                    new Text("Результат рассмотрения заявления: ")
                                        {Space = SpaceProcessingModeValues.Preserve}),
                                new Run(titleRequestRunPropertiesItalic.CloneNode(true), new Text("Услуга оказана. Решение отрицательное"))));

                        //doc.AppendChild(
                        //    new Paragraph(
                        //        new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                        //            new SpacingBetweenLines {After = Size20}),
                        //        new Run(titleRequestRunPropertiesBold.CloneNode(true),
                        //            new Text("Номер путевки: ") {Space = SpaceProcessingModeValues.Preserve}),
                        //        new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                        //            new Text(request.CertificateNumber))));

                        //doc.AppendChild(
                        //    new Paragraph(
                        //        new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                        //            new SpacingBetweenLines {After = Size20}),
                        //        new Run(titleRequestRunPropertiesBold.CloneNode(true),
                        //            new Text("Организация отдыха и оздоровления: ")
                        //                {Space = SpaceProcessingModeValues.Preserve}),
                        //        new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                        //            new Text(request.Hotels?.Name ?? request.Tour?.Hotels?.Name ?? string.Empty))));

                        //doc.AppendChild(
                        //    new Paragraph(
                        //        new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                        //            new SpacingBetweenLines {After = Size20}),
                        //        new Run(titleRequestRunPropertiesBold.CloneNode(true),
                        //            new Text("Время отдыха: ") {Space = SpaceProcessingModeValues.Preserve}),
                        //        new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                        //            new Text(request.TimeOfRest?.Name ?? string.Empty))));

                        doc.AppendChild(
                            new Paragraph(
                                new ParagraphProperties(new Justification {Val = JustificationValues.Both},
                                    new SpacingBetweenLines {After = Size20}),
                                new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                    new Text("Основание: ") {Space = SpaceProcessingModeValues.Preserve}),
                                new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                    new Text(DeclineReasonParticipate))));

                        doc.AppendChild(
                            new Paragraph(
                                new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                                    new SpacingBetweenLines {After = Size20}),
                                new Run(new RunProperties().SetFont().SetFontSize(Size28).Bold(), new Text(Space))));

                        doc.AppendChild(
                            new Paragraph(
                                new ParagraphProperties(new Justification { Val = JustificationValues.Both },
                                    new SpacingBetweenLines { After = Size20 }),
                                new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                    new Text("    "+Participate) { Space = SpaceProcessingModeValues.Preserve })));

                    }

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(new RunProperties().SetFont().SetFontSize(Size28).Bold(), new Text(Space))));
                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(new RunProperties().SetFont().SetFontSize(Size28).Bold(), new Text(Space))));

                    if (request.StatusId == (long)StatusEnum.CertificateIssued)
                    {
                        SignBlockNotification2020(doc, account, "Исполнитель");
                    }
                    else
                    {
                        SignBlockNotification(doc, account,
                                            $"{applicant.LastName} {applicant.FirstName} {applicant.MiddleName}",
                                            request.StatusId != (long)StatusEnum.CertificateIssued);
                    }


                    mainPart.Document = doc;
                }

                return new DocumentResult
                {
                    FileBody = ms.ToArray(),
                    FileName = $"{notificationName}.docx",
                    MimeType = mimeType,
                    MimeTypeShort = extention
                };
            }
        }
    }
}
