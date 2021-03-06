using System.Collections.Generic;
using System.Configuration;
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
        ///     Уведомление о приостановлении рассмотрения (вызов)
        /// </summary>
        public static IDocument NotificationWaitApplicant(IUnitOfWork unitOfWork, Account account, long requestId)
        {
            var request = unitOfWork.GetById<Request>(requestId);

            //IEnumerable<Child> childs = unitOfWork.GetAll<Child>().Where(x=>x.RequestId==requestId);
            IEnumerable<Child> childs = unitOfWork.GetSet<Child>().Where(x => x.RequestId == requestId);
            IEnumerable<BenefitType> benefits = unitOfWork.GetAll<BenefitType>().Where(b => b.IsActive&&childs.Any(c=>c.BenefitTypeId==b.Id));


            return NotificationWaitApplicant(request, account, benefits);
        }

        /// <summary>
        ///     Уведомление о приостановлении рассмотрения (вызов)
        /// </summary>
        private static IDocument NotificationWaitApplicant(Request request, Account account, IEnumerable<BenefitType>benefits)
        {
            
            var forMpguPortal = request.SourceId == (long) SourceEnum.Mpgu;
            if (forMpguPortal)
            {
                return PDFDocuments.PdfProcessor.NotificationWaitApplicant(request, benefits, account);
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
                            new Run(
                                new RunProperties().SetFont().SetFontSize(Size28).Bold(),
                                new Text(Space))));

                    doc.AppendChild(new Paragraph(
                        new ParagraphProperties(new Justification {Val = JustificationValues.Center}),
                        new Run(new RunProperties().SetFont().SetFontSize(Size28).Bold(),
                            new Text("Уведомление о приостановлении рассмотрения заявления"),
                            new Break(),
                            new Text("о предоставлении услуг отдыха и оздоровления"))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(new RunProperties().SetFont().SetFontSize(Size28).Bold(), new Text(Space))));

                    var titleRequestRunProperties = new RunProperties().SetFont().SetFontSize();

                    var titleRequestRunPropertiesBold = titleRequestRunProperties.CloneNode(true);
                    titleRequestRunPropertiesBold.AppendChild(new Bold());

                    var titleRequestRunPropertiesItalic = new RunProperties().SetFont().SetFontSize();
                    titleRequestRunPropertiesItalic.AppendChild(new Italic());

                    var applicant = request.Applicant ?? new Applicant
                                        {DocumentType = new DocumentType {Name = string.Empty}};

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

                    foreach (var child in request.Child?.Where(c => !c.IsDeleted) ?? new List<Child>())
                    {
                        doc.AppendChild(
                            new Paragraph(
                                new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                    new SpacingBetweenLines { After = Size20 }),
                                new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                    new Text(
                                            "Данные ребенка/лица из числа детей-сирот и детей, оставшихся без попечения родителей: ")
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
                            new ParagraphProperties(new Justification {Val = JustificationValues.Both},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Этап рассмотрения заявления: ")
                                    {Space = SpaceProcessingModeValues.Preserve}),
                            new Run(titleRequestRunProperties.CloneNode(true),
                                new Text("рассмотрение заявления о предоставлении услуг отдыха")
                                    {Space = SpaceProcessingModeValues.Preserve},
                                new Break(),
                                new Text("и оздоровления приостановлено.") {Space = SpaceProcessingModeValues.Preserve}
                            )));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Both},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Основание: ") {Space = SpaceProcessingModeValues.Preserve}),
                            new Run(titleRequestRunProperties.CloneNode(true),
                                new Text("пункт 6.4 Порядка организации отдыха и оздоровления детей, находящихся в трудной жизненной ситуации, утвержденного постановлением Правительства Москвы") { Space = SpaceProcessingModeValues.Preserve },
                                new Break(),
                                new Text("от 22 февраля 2017 г. № 56-ПП \"Об организации отдыха и оздоровления детей, находящихся в трудной жизненной ситуации\": ") { Space = SpaceProcessingModeValues.Preserve }),
                               // new Break(),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("\"Необходимость личной явки заявителя в ГАУК \"МОСГОРТУР\".") { Space = SpaceProcessingModeValues.Preserve }
                                )));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Both},
                                new SpacingBetweenLines {After = Size20},
                                new Indentation {FirstLine = FirstLineIndentation600.ToString()}),
                            new Run(titleRequestRunProperties.CloneNode(true),
                                new Text(
                                        "Для подтверждения сведений, указанных в заявлении о предоставлении услуг отдыха")
                                    {Space = SpaceProcessingModeValues.Preserve},
                                new Break(),
                                new Text(" и оздоровления (далее – заявление), ")
                                    {Space = SpaceProcessingModeValues.Preserve}),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("в течение 10 рабочих дней со дня направления данного уведомления")
                                    {Space = SpaceProcessingModeValues.Preserve}),
                            new Run(titleRequestRunProperties.CloneNode(true),
                                new Text(
                                        " Вам необходимо явиться в офис ГАУК \"МОСГОРТУР\" по адресу: г. Москва, Малый Харитоньевский переулок, дом 6 строение 3.")
                                    {Space = SpaceProcessingModeValues.Preserve})));


                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Both},
                                new SpacingBetweenLines {After = Size20},
                                new Indentation {FirstLine = FirstLineIndentation600.ToString()}),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Прием в офисе ГАУК \"МОСГОРТУР\" осуществляется исключительно")
                                    {Space = SpaceProcessingModeValues.Preserve},
                                new Break(),
                                new Text("по предварительной записи. ") {Space = SpaceProcessingModeValues.Preserve}),
                            new Run(titleRequestRunProperties.CloneNode(true),
                                new Text("Запись производится через официальный портал Мэра")
                                    {Space = SpaceProcessingModeValues.Preserve},
                                new Break(),
                                new Text(
                                        "и Правительства Москвы mos.ru или при личном визите заявителя в офис ГАУК \"МОСГОРТУР\". Запись производится на свободную дату и время.")
                                    {Space = SpaceProcessingModeValues.Preserve}
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
                                new Text("При себе необходимо иметь: ") {Space = SpaceProcessingModeValues.Preserve})));


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
                                        "документ, подтверждающий полномочия заявителя, сопровождающего лица (в случае организации совместного выездного отдыха) из числа законных представителей – опекунов, попечителей, приемных родителей, патронатных воспитателей ребенка (договор о приемной семье, распоряжение об опеке, иные документы, устанавливающие статус ребенка);",
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

                    //if (benefits.Count() > 0 ||(request.TypeOfRestId == (long)TypeOfRestEnum.YouthRestOrphanCamps || request.TypeOfRestId == (long)TypeOfRestEnum.CompensationYouthRest))
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

                    //            docs.InsertRange(4,innerList);
                    //            docs.Remove("документ, подтверждающий отнесение ребенка к категории, указанной в заявлении;");
                    //            docs.Remove("* в случае если с момента рождения ребенка у родителя произошла смена фамилии, имени или отчества – необходимо также предоставить документы, подтверждающие данные изменения: свидетельство о браке, свидетельство о расторжении брака, свидетельство о перемене имени");
                    //        }
                    //        if (benefit.ExnternalUid.Contains("24"))//дети-инвалиды, дети с ограниченными возможностями...
                    //        {
                    //            docs.Insert(1,"документ, удостоверяющий личность ребенка;");
                    //            docs.Insert(3, "документ, подтверждающий отнесение ребенка к категории, указанной в заявлении (заключение медико-социальной экспертизы или заключение Центральной психолого-медико-педагогической комиссии города Москвы**);");
                    //            docs.Insert(5,"документ, подтверждающий полномочия доверенного лица для сопровождения во время отдыха и оздоровления (в случае организации совместного выездного отдыха и сопровождения ребенка доверенным лицом для сопровождения во время отдыха и оздоровления и подачи заявления о предоставлении услуг отдыха и оздоровления с использованием Портала) (нотариально заверенное согласие или доверенность);");
                    //            docs.Remove("документ, подтверждающий отнесение ребенка к категории, указанной в заявлении;");
                    //            docs.Add("** является документом, подтвержающим категорию, только в случае указания на необходимость предоставления ребенку специальных условий обучения по адаптированной основной образовательной программе");
                    //        }
                    //        if (benefit.ExnternalUid.Contains("48"))//дети из малообеспеченных семей
                    //        {

                    //            docs.Insert(1,"документ, удостоверяющий личность ребенка;");
                    //            docs.Insert(5,"документ, подтверждающий полномочия доверенного лица для сопровождения во время отдыха и оздоровления (в случае организации совместного выездного отдыха и сопровождения ребенка доверенным лицом для сопровождения во время отдыха и оздоровления и подачи заявления о предоставлении услуг отдыха и оздоровления с использованием Портала) (нотариально заверенное согласие или доверенность);");
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
                    //    doc.AppendChild(
                    //        new Paragraph(
                    //            new ParagraphProperties(new Justification {Val = JustificationValues.Both},
                    //                new SpacingBetweenLines {After = Size20},
                    //                new Indentation {FirstLine = FirstLineIndentation600.ToString()}),
                    //            new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                    //                new Text(docText)
                    //                    {Space = SpaceProcessingModeValues.Preserve})));
                    //}
                    if (innerListOrphans.Count>0)
                    {
                        doc.AppendChild(
                                new Paragraph(
                                    new ParagraphProperties(new Justification { Val = JustificationValues.Both },
                                        new SpacingBetweenLines { After = Size20 },
                                        new Indentation { FirstLine = FirstLineIndentation600.ToString() }),
                                    new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                        new Text("Дети-сироты и дети, оставшиеся без попечения родителей, находящиеся под опекой, попечительством, в том числе в приёмной или патронатной семье")
                                        { Space = SpaceProcessingModeValues.Preserve })));
                        foreach (var docText in innerListOrphans)
                        {


                            doc.AppendChild(
                                new Paragraph(
                                    new ParagraphProperties(new Justification { Val = JustificationValues.Both },
                                        new SpacingBetweenLines { After = Size20 },
                                        new Indentation { FirstLine = FirstLineIndentation600.ToString() }),
                                    new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                        new Text(docText)
                                        { Space = SpaceProcessingModeValues.Preserve })));
                        }

                    }

                    if (innerListDisabled.Count > 0)
                    {

                        doc.AppendChild(
    new Paragraph(
        new ParagraphProperties(new Justification { Val = JustificationValues.Both },
            new SpacingBetweenLines { After = Size20 },
            new Indentation { FirstLine = FirstLineIndentation600.ToString() }),
        new Run(titleRequestRunPropertiesBold.CloneNode(true),
            new Text("Дети-инвалиды и дети с ограниченными возможностями здоровья")
            { Space = SpaceProcessingModeValues.Preserve })));
                        foreach (var docText in innerListDisabled)
                        {


                            doc.AppendChild(
                                new Paragraph(
                                    new ParagraphProperties(new Justification { Val = JustificationValues.Both },
                                        new SpacingBetweenLines { After = Size20 },
                                        new Indentation { FirstLine = FirstLineIndentation600.ToString() }),
                                    new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                        new Text(docText)
                                        { Space = SpaceProcessingModeValues.Preserve })));
                        }

                    }

                    if (innerListLowIncome.Count > 0)
                    {

                        doc.AppendChild(
    new Paragraph(
        new ParagraphProperties(new Justification { Val = JustificationValues.Both },
            new SpacingBetweenLines { After = Size20 },
            new Indentation { FirstLine = FirstLineIndentation600.ToString() }),
        new Run(titleRequestRunPropertiesBold.CloneNode(true),
            new Text("Дети из малообеспеченных семей")
            { Space = SpaceProcessingModeValues.Preserve })));
                        foreach (var docText in innerListLowIncome)
                        {


                            doc.AppendChild(
                                new Paragraph(
                                    new ParagraphProperties(new Justification { Val = JustificationValues.Both },
                                        new SpacingBetweenLines { After = Size20 },
                                        new Indentation { FirstLine = FirstLineIndentation600.ToString() }),
                                    new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                        new Text(docText)
                                        { Space = SpaceProcessingModeValues.Preserve })));
                        }

                    }

                    if (innerListSacrifice.Count > 0)
                    {

                        doc.AppendChild(
    new Paragraph(
        new ParagraphProperties(new Justification { Val = JustificationValues.Both },
            new SpacingBetweenLines { After = Size20 },
            new Indentation { FirstLine = FirstLineIndentation600.ToString() }),
        new Run(titleRequestRunPropertiesBold.CloneNode(true),
            new Text("Дети - жертвы вооруженных и межнациональных конфликтов, экологических и техногенных катастроф, стихийных бедствий")
            { Space = SpaceProcessingModeValues.Preserve })));
                        foreach (var docText in innerListSacrifice)
                        {


                            doc.AppendChild(
                                new Paragraph(
                                    new ParagraphProperties(new Justification { Val = JustificationValues.Both },
                                        new SpacingBetweenLines { After = Size20 },
                                        new Indentation { FirstLine = FirstLineIndentation600.ToString() }),
                                    new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                        new Text(docText)
                                        { Space = SpaceProcessingModeValues.Preserve })));
                        }

                    }

                    if (innerListRefugee.Count > 0)
                    {

                        doc.AppendChild(
    new Paragraph(
        new ParagraphProperties(new Justification { Val = JustificationValues.Both },
            new SpacingBetweenLines { After = Size20 },
            new Indentation { FirstLine = FirstLineIndentation600.ToString() }),
        new Run(titleRequestRunPropertiesBold.CloneNode(true),
            new Text("Дети из семей беженцев и вынужденных переселенцев")
            { Space = SpaceProcessingModeValues.Preserve })));
                        foreach (var docText in innerListRefugee)
                        {


                            doc.AppendChild(
                                new Paragraph(
                                    new ParagraphProperties(new Justification { Val = JustificationValues.Both },
                                        new SpacingBetweenLines { After = Size20 },
                                        new Indentation { FirstLine = FirstLineIndentation600.ToString() }),
                                    new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                        new Text(docText)
                                        { Space = SpaceProcessingModeValues.Preserve })));
                        }

                    }

                    if (innerListExtreme.Count > 0)
                    {
                        doc.AppendChild(
    new Paragraph(
        new ParagraphProperties(new Justification { Val = JustificationValues.Both },
            new SpacingBetweenLines { After = Size20 },
            new Indentation { FirstLine = FirstLineIndentation600.ToString() }),
        new Run(titleRequestRunPropertiesBold.CloneNode(true),
            new Text("Дети, оказавшиеся в экстремальных условиях")
            { Space = SpaceProcessingModeValues.Preserve })));
                        foreach (var docText in innerListExtreme)
                        {


                            doc.AppendChild(
                                new Paragraph(
                                    new ParagraphProperties(new Justification { Val = JustificationValues.Both },
                                        new SpacingBetweenLines { After = Size20 },
                                        new Indentation { FirstLine = FirstLineIndentation600.ToString() }),
                                    new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                        new Text(docText)
                                        { Space = SpaceProcessingModeValues.Preserve })));
                        }

                    }

                    if (innerListViolence.Count > 0)
                    {
                        doc.AppendChild(
    new Paragraph(
        new ParagraphProperties(new Justification { Val = JustificationValues.Both },
            new SpacingBetweenLines { After = Size20 },
            new Indentation { FirstLine = FirstLineIndentation600.ToString() }),
        new Run(titleRequestRunPropertiesBold.CloneNode(true),
            new Text("Дети - жертвы насилия")
            { Space = SpaceProcessingModeValues.Preserve })));
                        foreach (var docText in innerListViolence)
                        {


                            doc.AppendChild(
                                new Paragraph(
                                    new ParagraphProperties(new Justification { Val = JustificationValues.Both },
                                        new SpacingBetweenLines { After = Size20 },
                                        new Indentation { FirstLine = FirstLineIndentation600.ToString() }),
                                    new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                        new Text(docText)
                                        { Space = SpaceProcessingModeValues.Preserve })));
                        }

                    }

                    if (innerListInvalid.Count > 0)
                    {
                        doc.AppendChild(
    new Paragraph(
        new ParagraphProperties(new Justification { Val = JustificationValues.Both },
            new SpacingBetweenLines { After = Size20 },
            new Indentation { FirstLine = FirstLineIndentation600.ToString() }),
        new Run(titleRequestRunPropertiesBold.CloneNode(true),
            new Text("Дети, жизнедеятельность которых объективно нарушена в результате сложившихся обстоятельств и которые не могут преодолеть данные обстоятельства самостоятельно или с помощью семьи")
            { Space = SpaceProcessingModeValues.Preserve })));
                        foreach (var docText in innerListInvalid)
                        {


                            doc.AppendChild(
                                new Paragraph(
                                    new ParagraphProperties(new Justification { Val = JustificationValues.Both },
                                        new SpacingBetweenLines { After = Size20 },
                                        new Indentation { FirstLine = FirstLineIndentation600.ToString() }),
                                    new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                        new Text(docText)
                                        { Space = SpaceProcessingModeValues.Preserve })));
                        }

                    }

                    if (innerListTerror.Count > 0)
                    {
                        doc.AppendChild(
    new Paragraph(
        new ParagraphProperties(new Justification { Val = JustificationValues.Both },
            new SpacingBetweenLines { After = Size20 },
            new Indentation { FirstLine = FirstLineIndentation600.ToString() }),
        new Run(titleRequestRunPropertiesBold.CloneNode(true),
            new Text("Дети, пострадавшие в результате террористических актов")
            { Space = SpaceProcessingModeValues.Preserve })));
                        foreach (var docText in innerListTerror)
                        {


                            doc.AppendChild(
                                new Paragraph(
                                    new ParagraphProperties(new Justification { Val = JustificationValues.Both },
                                        new SpacingBetweenLines { After = Size20 },
                                        new Indentation { FirstLine = FirstLineIndentation600.ToString() }),
                                    new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                        new Text(docText)
                                        { Space = SpaceProcessingModeValues.Preserve })));
                        }

                    }

                    if (innerListMilitary.Count > 0)
                    {
                        doc.AppendChild(
    new Paragraph(
        new ParagraphProperties(new Justification { Val = JustificationValues.Both },
            new SpacingBetweenLines { After = Size20 },
            new Indentation { FirstLine = FirstLineIndentation600.ToString() }),
        new Run(titleRequestRunPropertiesBold.CloneNode(true),
            new Text("Дети из семей военнослужащих и приравненных к ним лиц, погибших или получивших увечья (ранения, травмы, контузии) при исполнении ими обязанностей военной службы или служебных обязанностей")
            { Space = SpaceProcessingModeValues.Preserve })));
                        foreach (var docText in innerListMilitary)
                        {


                            doc.AppendChild(
                                new Paragraph(
                                    new ParagraphProperties(new Justification { Val = JustificationValues.Both },
                                        new SpacingBetweenLines { After = Size20 },
                                        new Indentation { FirstLine = FirstLineIndentation600.ToString() }),
                                    new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                        new Text(docText)
                                        { Space = SpaceProcessingModeValues.Preserve })));
                        }

                    }

                    if (innerListInvalidParents.Count > 0)
                    {
                        doc.AppendChild(
    new Paragraph(
        new ParagraphProperties(new Justification { Val = JustificationValues.Both },
            new SpacingBetweenLines { After = Size20 },
            new Indentation { FirstLine = FirstLineIndentation600.ToString() }),
        new Run(titleRequestRunPropertiesBold.CloneNode(true),
            new Text("Дети из семей, в которых оба или один родитель являются инвалидами")
            { Space = SpaceProcessingModeValues.Preserve })));
                        foreach (var docText in innerListInvalidParents)
                        {


                            doc.AppendChild(
                                new Paragraph(
                                    new ParagraphProperties(new Justification { Val = JustificationValues.Both },
                                        new SpacingBetweenLines { After = Size20 },
                                        new Indentation { FirstLine = FirstLineIndentation600.ToString() }),
                                    new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                        new Text(docText)
                                        { Space = SpaceProcessingModeValues.Preserve })));
                        }

                    }

                    if (innerListDeviant.Count > 0)
                    {
                        doc.AppendChild(
    new Paragraph(
        new ParagraphProperties(new Justification { Val = JustificationValues.Both },
            new SpacingBetweenLines { After = Size20 },
            new Indentation { FirstLine = FirstLineIndentation600.ToString() }),
        new Run(titleRequestRunPropertiesBold.CloneNode(true),
            new Text("Дети с отклонениями в поведении")
            { Space = SpaceProcessingModeValues.Preserve })));
                        foreach (var docText in innerListDeviant)
                        {


                            doc.AppendChild(
                                new Paragraph(
                                    new ParagraphProperties(new Justification { Val = JustificationValues.Both },
                                        new SpacingBetweenLines { After = Size20 },
                                        new Indentation { FirstLine = FirstLineIndentation600.ToString() }),
                                    new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                        new Text(docText)
                                        { Space = SpaceProcessingModeValues.Preserve })));
                        }

                    }

                    if (innerListOrphansYouth.Count > 0)
                    {
                        doc.AppendChild(
    new Paragraph(
        new ParagraphProperties(new Justification { Val = JustificationValues.Both },
            new SpacingBetweenLines { After = Size20 },
            new Indentation { FirstLine = FirstLineIndentation600.ToString() }),
        new Run(titleRequestRunPropertiesBold.CloneNode(true),
            new Text("Лица из числа детей-сирот и детей, оставшихся без попечения родителей")
            { Space = SpaceProcessingModeValues.Preserve })));
                        foreach (var docText in innerListOrphansYouth)
                        {


                            doc.AppendChild(
                                new Paragraph(
                                    new ParagraphProperties(new Justification { Val = JustificationValues.Both },
                                        new SpacingBetweenLines { After = Size20 },
                                        new Indentation { FirstLine = FirstLineIndentation600.ToString() }),
                                    new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                        new Text(docText)
                                        { Space = SpaceProcessingModeValues.Preserve })));
                        }

                    }

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(new RunProperties().SetFont().SetFontSize(Size28).Bold(), new Text(Space))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Both},
                                new SpacingBetweenLines {After = Size20},
                                new Indentation {FirstLine = FirstLineIndentation600.ToString()}),
                            new Run(titleRequestRunProperties.CloneNode(true),
                                new Text(
                                        "Неявка с соответствующими документами в течение срока приостановления рассмотрения заявления является основанием для отказа в предоставлении услуг отдыха и оздоровления.")
                                    {Space = SpaceProcessingModeValues.Preserve})));


                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                new SpacingBetweenLines { After = Size20 }),
                            new Run(new RunProperties().SetFont().SetFontSize(Size28).Bold(), new Text(Space))));


                    SignBlockNotification2020(doc, account, "Исполнитель:");

                    mainPart.Document = doc;
                }

                return new DocumentResult
                {
                    FileBody = ms.ToArray(),
                    FileName = "Уведомление о приостановлении рассмотрения заявления.docx",
                    MimeType = mimeType,
                    MimeTypeShort = extention
                };
            }
        }

        /// <summary>
        ///     Уведомление о необходимости выбора организации отдыха и оздоровления
        /// </summary>
        public static IDocument NotificationOrgChoose(Request request, Account account)
        {
            var forMpguPortal = request.SourceId == (long) SourceEnum.Mpgu;

            if (forMpguPortal)
            {
                return PDFDocuments.PdfProcessor.NotificationOrgChoose(request);
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
                            new Run(
                                new RunProperties().SetFont().SetFontSize(Size28).Bold(),
                                new Text(Space))));

                    doc.AppendChild(new Paragraph(
                        new ParagraphProperties(new Justification {Val = JustificationValues.Center}),
                        new Run(new RunProperties().SetFont().SetFontSize(Size28).Bold(),
                            new Text("Уведомление о необходимости выбора организации отдыха"),
                            new Break(),
                            new Text("и оздоровления")
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
                    titleRequestRunPropertiesItalic.AppendChild(new Italic());
                    var titleRequestRunPropertiesUnderline = titleRequestRunProperties.CloneNode(true);
                    titleRequestRunPropertiesUnderline.AppendChild(new Underline {Val = UnderlineValues.Single});

                    var applicant = request.Applicant ?? new Applicant
                                        {DocumentType = new DocumentType {Name = string.Empty}};

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Both },
                            new Run(titleRequestRunProperties.CloneNode(true),
                                new Text($"Уважаемый(ая) {applicant.LastName} {applicant.FirstName} {applicant.MiddleName},")
                                    { Space = SpaceProcessingModeValues.Preserve }))));


                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Both},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(titleRequestRunProperties.CloneNode(true),
                                new Text($"Ваше заявление от {request.DateRequest?.Date.FormatEx()} № {request.RequestNumber} о предоставлении услуг отдыха и оздоровления (далее – заявление) рассмотрено.")
                                    {Space = SpaceProcessingModeValues.Preserve})
                        ));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Both},
                                new Indentation {FirstLine = FirstLineIndentation600.ToString()},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(titleRequestRunProperties.CloneNode(true),
                                new Text("В период второго этапа заявочной кампании (с 7 по 21 февраля 2022 г.) Вам необходимо дополнить Ваше заявление сведениями о конкретной организации отдыха и оздоровления. Выбор конкретной организации отдыха и оздоровления осуществляется из числа предлагаемых ГАУК \"МОСГОРТУР\" в соответствии с указанными Вами на первом этапе заявочной кампании сведениями о приоритетном времени, направлении отдыха и оздоровления и количестве детей.")
                                    {Space = SpaceProcessingModeValues.Preserve})
                        ));


                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Both},
                                new Indentation {FirstLine = FirstLineIndentation600.ToString()},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(titleRequestRunProperties.CloneNode(true),
                                new Text("В случае подачи заявления через подсистему \"личный кабинет\" Портала Мэра и Правительства Москвы ")
                                    {Space = SpaceProcessingModeValues.Preserve}),
                            new Run(titleRequestRunPropertiesUnderline.CloneNode(true),
                                new Text("mos.ru")
                                    {Space = SpaceProcessingModeValues.Preserve}),
                            new Run(titleRequestRunProperties.CloneNode(true),
                                new Text(" (далее – подсистема \"личный кабинет\" Портала), дополнение формы заявления сведениями о конкретной организации отдыха и оздоровления осуществляется в подсистеме \"личный кабинет\" Портала.")
                                    {Space = SpaceProcessingModeValues.Preserve})
                        ));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Both},
                                new Indentation {FirstLine = FirstLineIndentation600.ToString()},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(titleRequestRunProperties.CloneNode(true),
                                new Text("В случае подачи заявления при личном обращении в офисе ГАУК \"МОСГОРТУР\", дополнение формы заявления сведениями о конкретной организации отдыха и оздоровления возможно только при личном обращении заявителя в офис ГАУК \"МОСГОРТУР\" по адресу: г. Москва, Малый Харитоньевский переулок д. 6, стр. 3.")
                                    {Space = SpaceProcessingModeValues.Preserve})
                        ));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Both},
                                new Indentation {FirstLine = FirstLineIndentation600.ToString()},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Прием в офисе ГАУК \"МОСГОРТУР\" осуществляется исключительно по предварительной записи.")
                                    {Space = SpaceProcessingModeValues.Preserve}),
                            new Run(titleRequestRunProperties.CloneNode(true),
                                new Text(" Запись производится через Портал Мэра и Правительства Москвы mos.ru (далее – Портала) или при личном визите заявителя в офис ГАУК \"МОСГОРТУР\". Запись производится на свободную дату и время.")
                                    {Space = SpaceProcessingModeValues.Preserve})
                        ));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Both},
                                new Indentation {FirstLine = FirstLineIndentation600.ToString()},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(titleRequestRunProperties.CloneNode(true),
                                new Text("В случае если на втором этапе заявочной кампании Вас не устроит ни один из вариантов организаций отдыха и оздоровления, предлагаемых ГАУК \"МОСГОРТУР\", на втором этапе заявочной кампании в период с 7 по 21 февраля 2022 г. Вам необходимо отказаться от всех предложенных организаций отдыха и оздоровления.")
                                    {Space = SpaceProcessingModeValues.Preserve})
                        ));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Both },
                                new Indentation { FirstLine = FirstLineIndentation600.ToString() },
                                new SpacingBetweenLines { After = Size20 }),
                            new Run(titleRequestRunProperties.CloneNode(true),
                                new Text("При подаче заявления через подсистему \"личный кабинет\" Портала для отказа от всех предложенных организаций отдыха и оздоровления необходимо нажать соответствующую \"галочку\" в интерактивном поле Портала: \"Я отказываюсь от предложенных вариантов организаций отдыха и оздоровления\".")
                                    { Space = SpaceProcessingModeValues.Preserve })
                        ));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Both },
                                new Indentation { FirstLine = FirstLineIndentation600.ToString() },
                                new SpacingBetweenLines { After = Size20 }),
                            new Run(titleRequestRunProperties.CloneNode(true),
                                new Text("При подаче заявления в офисе ГАУК \"МОСГОРТУР\" для отказа от всех предложенных организаций отдыха и оздоровления необходимо лично обратиться в офис ГАУК \"МОСГОРТУР\".")
                                    { Space = SpaceProcessingModeValues.Preserve })
                        ));


                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                new SpacingBetweenLines { After = Size20 }),
                            new Run(new RunProperties().SetFont().SetFontSize(Size28).Bold(), new Text(Space))));


                    SignWorkerBlock(doc, account, "Исполнитель:");

                    mainPart.Document = doc;
                }

                return new DocumentResult
                {
                    FileBody = ms.ToArray(),
                    FileName = "Уведомление о необходимости выбора организации отдыха и оздоровления.docx",
                    MimeType = mimeType,
                    MimeTypeShort = extention
                };
            }
        }

        /// <summary>
        ///     Уведомление о предоставлении сертификата или путёвки
        /// </summary>
        public static IDocument NotificationAboutDecision(IUnitOfWork unitOfWork, Account account, long requestId)
        {
            var request = unitOfWork.GetById<Request>(requestId);
            return NotificationAboutDecision(request, account);
        }

        /// <summary>
        ///     Уведомление о предоставлении сертификата или путёвки
        /// </summary>
        private static IDocument NotificationAboutDecision(Request request, Account account)
        {
            var money = new[]
                {
                    (long?) TypeOfRestEnum.Money
                    , (long?) TypeOfRestEnum.MoneyOn3To7
                    , (long?) TypeOfRestEnum.MoneyOn7To15
                    , (long?) TypeOfRestEnum.MoneyOn18
                    , (long?) TypeOfRestEnum.MoneyOnInvalidOn4To17
                    //,(long?) TypeOfRestEnum.RestWithParentsOrphan
                    //,(long?) TypeOfRestEnum.YouthRestOrphanCamps
                    //,(long?) TypeOfRestEnum.ChildRestOrphanCamps
                };

            if (request.StatusId == (long) StatusEnum.CertificateIssued &&
                (request.TypeOfRestId == (long) TypeOfRestEnum.Compensation ||
                 request.TypeOfRestId == (long) TypeOfRestEnum.CompensationYouthRest))
            {
                return NotificationAboutCompensationIssued(request, account);
            }

            if (money.Contains(request.TypeOfRestId))
            {
                return NotificationAboutCertificate(request, account);
            }


            return NotificationAboutTour(request, account);
        }


        /// <summary>
        ///     Уведомление о предоставлении путёвки (1075.1)
        /// </summary>
        private static IDocument NotificationAboutTour(Request request, Account account)
        {
            var forMpguPortal = request.SourceId == (long) SourceEnum.Mpgu;

            if(forMpguPortal)
            {
                return PDFDocuments.PdfProcessor.NotificationCertificate107501(request);
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
                            new Text("Уведомление о предоставлении бесплатной путевки для отдыха"),
                            new Break(),
                            new Text("и оздоровления"))));

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
                                new Text("Вид отдыха: ") {Space = SpaceProcessingModeValues.Preserve}),
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
                                                "Данные лиц, указанных в путевке ")
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
                                            "Данные лиц, указанных в путевке: ")
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
                                new Text("Услуга оказана."))));

                    /*doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Дата и время выбора варианта организации отдыха и оздоровления: ")
                                    {Space = SpaceProcessingModeValues.Preserve}),
                            new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                new Text($"{request.DateChangeStatus:dd.MM.yyyy HH:mm}"))));*/

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Номер путевки: ") {Space = SpaceProcessingModeValues.Preserve}),
                            new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                new Text(request.CertificateNumber))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Организация отдыха и оздоровления: ")
                                    {Space = SpaceProcessingModeValues.Preserve}),
                            new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                new Text(request.Tour?.Hotels?.Name))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Время отдыха: ") {Space = SpaceProcessingModeValues.Preserve}),
                            new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                new Text($"{request.TimeOfRest?.Name} ({request.Tour?.DateIncome.FormatEx()} - {request.Tour?.DateOutcome.FormatEx()})"))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Both},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Основание: ") {Space = SpaceProcessingModeValues.Preserve}),
                            new Run(titleRequestRunProperties.CloneNode(true),
                                new Text($"{FederalShort2021Law}."))));

                    if (!forMpguPortal)
                    {
                        doc.AppendChild(
                            new Paragraph(
                                new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                                    new SpacingBetweenLines {After = Size20}),
                                new Run(new RunProperties().SetFont().SetFontSize(Size28).Bold(), new Text(Space))));


                        SignBlockNotification2020(doc, account, "Исполнитель:");
                    }

                    mainPart.Document = doc;
                }

                return new DocumentResult
                {
                    FileBody = ms.ToArray(),
                    FileName = "Уведомление о предоставлении бесплатной путевки для отдыха и оздоровления.docx",
                    MimeType = mimeType,
                    MimeTypeShort = extention
                };
            }
        }

        /// <summary>
        ///     Уведомление о предоставлении сертификата (1075.2)
        /// </summary>
        private static IDocument NotificationAboutCertificate(Request request, Account account)
        {
            var forMpguPortal = request.SourceId == (long) SourceEnum.Mpgu;

            if (forMpguPortal)
            {
                return PDFDocuments.PdfProcessor.NotificationCertificate107502(request);
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
                            new Text("Уведомление о предоставлении сертификата на отдых и оздоровление"))));

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
                                new Text(request.TypeOfRest?.Name ?? "сертификат на отдых и оздоровление"))));


                    if (request.Child != null)
                    {
                        foreach (var child in request.Child.Where(c => !c.IsDeleted))
                        {
                            doc.AppendChild(
                                new Paragraph(
                                    new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                                        new SpacingBetweenLines {After = Size20}),
                                    new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                        new Text("Данные лиц, указанных в сертификате: ") {Space = SpaceProcessingModeValues.Preserve}),
                                    new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                        new Text(
                                            $"{child.LastName} {child.FirstName} {child.MiddleName}, {child.DateOfBirth.FormatExGR(string.Empty)}"))));

                            doc.AppendChild(
                                new Paragraph(
                                    new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                                        new SpacingBetweenLines {After = Size20}),
                                    new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                        new Text("Льготная категория ребенка: ")
                                            {Space = SpaceProcessingModeValues.Preserve}),
                                    new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                        new Text($"{child.BenefitType?.Name}"))));
                        }
                    }

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Результат рассмотрения заявления: ")
                                    {Space = SpaceProcessingModeValues.Preserve}),
                            new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                new Text("Услуга оказана."))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Номер сертификата: ") {Space = SpaceProcessingModeValues.Preserve}),
                            new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                new Text(request.CertificateNumber))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Both},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Основание: ") {Space = SpaceProcessingModeValues.Preserve}),
                            new Run(titleRequestRunProperties.CloneNode(true),
                                new Text(
                                    "Порядок организации отдыха и оздоровления детей, находящихся в трудной жизненной ситуации, утвержденный постановлением Правительства Москвы от 22 февраля 2017 г. № 56-ПП \"Об организации отдыха и оздоровления детей, находящихся в трудной жизненной ситуации\"."))));

                    if (!forMpguPortal)
                    {
                        doc.AppendChild(
                            new Paragraph(
                                new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                                    new SpacingBetweenLines {After = Size20}),
                                new Run(new RunProperties().SetFont().SetFontSize(Size28).Bold(), new Text(Space))));


                        SignBlockNotification2020(doc, account, "Исполнитель:");
                    }

                    mainPart.Document = doc;
                }

                return new DocumentResult
                {
                    FileBody = ms.ToArray(),
                    FileName = "Уведомление о предоставлении сертификата на отдых и оздоровление.docx",
                    MimeType = mimeType,
                    MimeTypeShort = extention
                };
            }
        }

        /// <summary>
        ///     Уведомления о предоставлении выплаты компенсации
        /// </summary>
        private static IDocument NotificationAboutCompensationIssued(Request request, Account account)
        {
            using (var ms = new MemoryStream())
            {
                using (var wordDocument = WordprocessingDocument.Create(ms, WordprocessingDocumentType.Document))
                {
                    var isCompensationYouth = request.TypeOfRestId == (long) TypeOfRestEnum.CompensationYouthRest;

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

                    if (isCompensationYouth)
                    {
                        doc.AppendChild(new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Center}),
                            new Run(new RunProperties().SetFont().SetFontSize(Size28).Bold(),
                                new Text("Уведомление о предоставлении выплаты компенсации за самостоятельно приобретенную лицом из числа детей-сирот и детей, оставшихся без попечения родителей, путевку для отдыха и оздоровления"))));
                    }
                    else
                    {
                        doc.AppendChild(new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Center}),
                            new Run(new RunProperties().SetFont().SetFontSize(Size28).Bold(),
                                new Text("Уведомление о предоставлении выплаты компенсации за самостоятельно приобретенную родителями или иными законными представителями путевку для отдыха и оздоровления"))));
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
                                                $"{(isCompensationYouth ? "Данные лица из числа детей-сирот и детей, оставшихся без попечения родителей" : "Данные о ребёнке")}: ")
                                            {Space = SpaceProcessingModeValues.Preserve}),
                                    new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                        new Text(
                                            $"{child.LastName} {child.FirstName} {child.MiddleName}, {child.DateOfBirth.FormatExGR(string.Empty)}"))));

                            doc.AppendChild(
                                new Paragraph(
                                    new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                                        new SpacingBetweenLines {After = Size20}),
                                    new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                        new Text(
                                                $"{(isCompensationYouth ? "Льготная категория" : "Льготная категория ребёнка")}: ")
                                            {Space = SpaceProcessingModeValues.Preserve}),
                                    new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                        new Text($"{child.BenefitType?.Name}"))));
                        }
                    }

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Результат рассмотрения заявления: ")
                                    {Space = SpaceProcessingModeValues.Preserve}),
                            new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                new Text("Услуга оказана."))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Both},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Основание: ") {Space = SpaceProcessingModeValues.Preserve}),
                            new Run(titleRequestRunProperties.CloneNode(true),
                                new Text("Порядок организации отдыха и оздоровления детей, находящихся в трудной жизненной ситуации, утвержденный постановлением Правительства Москвы от 22 февраля 2017 г. № 56-ПП \"Об организации отдыха и оздоровления детей, находящихся в трудной жизненной ситуации\"."))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                new SpacingBetweenLines { After = Size20 }),
                            new Run(new RunProperties().SetFont().SetFontSize(Size28).Bold(), new Text(Space))));

                    SignWorkerCompensationBlock(doc, account);

                    mainPart.Document = doc;
                }

                return new DocumentResult
                {
                    FileBody = ms.ToArray(),
                    FileName = "Уведомление о предоставлении выплаты компенсации за самостоятельно приобретенную путевку.docx",
                    MimeType = mimeType,
                    MimeTypeShort = extention
                };
            }
        }

        /// <summary>
        ///     Уведомление о запрос необходимой информации
        /// </summary>
        public static IDocument NotificationRequestInformation(Request request, Account account)
        {
            using (var ms = new MemoryStream())
            {
                using (var wordDocument = WordprocessingDocument.Create(ms, WordprocessingDocumentType.Document))
                {
                    var isCompensationYouth = request.TypeOfRestId == (long)TypeOfRestEnum.CompensationYouthRest;

                    var mainPart = wordDocument.AddMainDocumentPart();
                    var doc = new Document(new Body());

                    var titleRequestRunProperties = new RunProperties().SetFont().SetFontSize();

                    var titleRequestRunPropertiesBold = titleRequestRunProperties.CloneNode(true);
                    titleRequestRunPropertiesBold.AppendChild(new Bold());

                    var titleRequestRunPropertiesItalic = titleRequestRunProperties.CloneNode(true);
                    titleRequestRunPropertiesItalic.AppendChild(new Italic());

                    var applicant = request.Applicant ?? new Applicant { DocumentType = new DocumentType { Name = string.Empty } };

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

                    if (isCompensationYouth)
                    {
                        doc.AppendChild(new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Center }),
                            new Run(new RunProperties().SetFont().SetFontSize(Size28).Bold(),
                                new Text("Уведомление о необходимости предоставления дополнительной информации для принятия решения о выплате компенсации"),
                                new Break(),
                                new Text("за самостоятельно приобретенную лицом из числа детей-сирот и детей, оставшихся без попечения родителей, путевку для отдыха"),
                                new Break(),
                                new Text("и оздоровления"))));
                    }
                    else
                    {
                        doc.AppendChild(new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Center }),
                            new Run(new RunProperties().SetFont().SetFontSize(Size28).Bold(),
                                new Text("Уведомление о необходимости предоставления дополнительной информации для принятия решения о выплате компенсации"),
                                new Break(),
                                new Text("за самостоятельно приобретенную родителями или иными законными представителями путевку для отдыха и оздоровления"))));
                    }

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                new SpacingBetweenLines { After = Size20 }),
                            new Run(new RunProperties().SetFont().SetFontSize(Size28).Bold(), new Text(Space))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Center },
                                new SpacingBetweenLines { After = Size20 },
                                new Indentation { FirstLine = FirstLineIndentation600.ToString() }),
                            new Run(titleRequestRunProperties.CloneNode(true),
                                new Text(
                                    $"Уважаемый(ая) {applicant.LastName} {applicant.FirstName} {applicant.MiddleName}!"))));

                    var t = isCompensationYouth ? "лицом из числа детей-сирот и детей, оставшихся без попечения родителей," : "родителями или иными законными представителями";

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Both },
                                new SpacingBetweenLines { After = Size20 },
                                new Indentation { FirstLine = FirstLineIndentation600.ToString() }),
                            new Run(titleRequestRunProperties.CloneNode(true),
                                new Text($"Для принятия решения о выплате компенсации за самостоятельно приобретенную {t} путевку для отдыха и оздоровления по Вашему заявлению от {request.DateRequest:dd.MM.yyyy}г. № {request.RequestNumber} Вам необходимо в течение 10 рабочих дней с момента направления данного уведомления на электронную почту, указанную в заявлении, предоставить в ГАУК \"МОСГОРТУР\" следующую информацию:"))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Both },
                                new SpacingBetweenLines { After = Size20 }),
                            new Run(titleRequestRunProperties.CloneNode(true),
                                new Text(
                                    "..."))));

                    if (isCompensationYouth)
                    {
                        doc.AppendChild(
                            new Paragraph(
                                new ParagraphProperties(new Justification { Val = JustificationValues.Both },
                                    new SpacingBetweenLines { After = Size20 },
                                    new Indentation { FirstLine = FirstLineIndentation600.ToString() }),
                                new Run(titleRequestRunProperties.CloneNode(true),
                                    new Text("В случае непредоставления запрашиваемой информации в указанный срок, Вам будет отказано в осуществлении выплаты компенсации за самостоятельно приобретенную лицом из числа детей-сирот и детей, оставшихся без попечения родителей, путевку для отдыха и оздоровления."))));
                    }
                    else
                    {
                        doc.AppendChild(
                            new Paragraph(
                                new ParagraphProperties(new Justification { Val = JustificationValues.Both },
                                    new SpacingBetweenLines { After = Size20 },
                                    new Indentation { FirstLine = FirstLineIndentation600.ToString() }),
                                new Run(titleRequestRunProperties.CloneNode(true),
                                    new Text("В случае непредоставления запрашиваемой информации в указанный срок, Вам будет отказано в осуществлении выплаты компенсации за самостоятельно приобретенную родителями или иными законными представителями путевку для отдыха и оздоровления."))));
                    }

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

                    SignWorkerCompensationBlock(doc, account);

                    mainPart.Document = doc;
                }

                return new DocumentResult
                {
                    FileBody = ms.ToArray(),
                    FileName = "Уведомление о необходимости предоставления дополнительной информации.docx",
                    MimeType = mimeType,
                    MimeTypeShort = extention
                };
            }
        }

        /// <summary>
        ///     Уведомление (базовая развилка)
        /// </summary>
        public static IDocument NotificationDataSwitch(IUnitOfWork unitOfWork, Account account, long requestId)
        {
            var dr2 = ConfigurationManager.AppSettings["NotificationRefuseDeclineReasonWrongDocs"].LongParse() ?? 201904;
            var dr3 = ConfigurationManager.AppSettings["NotificationRefuseDeclineReasonQuota"].LongParse() ?? 201705;
            // отказ от вариантов
            var dr4 = ConfigurationManager.AppSettings["NotificationRefuseDeclineDiscardingOptions"].LongParse() ?? 201902;
            // неучастие в выборе альтернативного варианта (мини ЛОК 2020)
            var dr5 = ConfigurationManager.AppSettings["NotificationRefuseDeclineDiscardingChoose"].LongParse() ?? 201911;
            //неучастие заявителя во втором этапе заявочной кампании
            var dr6 = ConfigurationManager.AppSettings["NotParticipateInSecondStage"].LongParse() ?? 201911;


            var request = unitOfWork.GetById<Request>(requestId);

            if (request.StatusId == (long)StatusEnum.CancelByApplicant && !string.IsNullOrWhiteSpace(request.CertificateNumber))
            {
                var doc = NotificationDeadline(request, account);
                doc.RequestFileTypeId = (long) RequestFileTypeEnum.NotificationRefuse;
                return doc;
            }

            if (request.StatusId == (long)StatusEnum.CancelByApplicant)
            {
                return NotificationRefuse1090(request, account);
            }

            if (request.StatusId == (long)StatusEnum.Reject && request.DeclineReasonId == dr2)
            {
                var doc = NotificationRefuse10802(request, account);
                doc.RequestFileTypeId = (long) RequestFileTypeEnum.NotificationRefuse;
                return doc;
            }

            if (request.StatusId == (long)StatusEnum.Reject && request.DeclineReasonId == dr3)
            {
                var doc = NotificationRefuse10805(unitOfWork, request, account);
                doc.RequestFileTypeId = (long) RequestFileTypeEnum.NotificationRefuse;
                return doc;
            }

            if (request.StatusId == (long)StatusEnum.Reject && request.DeclineReasonId == dr6)
            {
                var doc = NotificationRefuse108013(unitOfWork, request, account);
                doc.RequestFileTypeId = (long)RequestFileTypeEnum.NotificationRefuse;
                return doc;
            }

            if (request.StatusId == (long)StatusEnum.Reject &&
                request.TypeOfRestId == (long)TypeOfRestEnum.Compensation)
            {
                var doc = NotificationRefuseCompensation(request, account);
                doc.RequestFileTypeId = (long) RequestFileTypeEnum.NotificationRefuse;
                return doc;
            }

            if (request.StatusId == (long)StatusEnum.Reject &&
                request.TypeOfRestId == (long)TypeOfRestEnum.CompensationYouthRest)
            {
                var doc = NotificationRefuseCompensationYouthRest(request, account);
                doc.RequestFileTypeId = (long) RequestFileTypeEnum.NotificationRefuse;
                return doc;
            }

            if (request.StatusId == (long)StatusEnum.Reject)
            {
                var doc = NotificationRefuse1080(request, account);
                doc.RequestFileTypeId = (long) RequestFileTypeEnum.NotificationRefuse;
                return doc;
            }

            var document = NotificationRefuseContent(request, account);
            document.RequestFileTypeId = (long) RequestFileTypeEnum.NotificationRefuse;
            return document;

        }
    }
}
