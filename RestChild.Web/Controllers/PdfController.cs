using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using OfficeOpenXml.Style;
using RestChild.Comon;
using RestChild.Comon.Enumeration;
using RestChild.Comon.ToExcel;
using RestChild.DAL.RepositoryExtensions;
using RestChild.DocumentGeneration.PDFDocuments;
using RestChild.Domain;
using RestChild.MPGUIntegration;
using RestChild.Web.Common;
using RestChild.Web.Controllers.WebApi;
using RestChild.Web.Models;
using RestChild.Web.Properties;

namespace RestChild.Web.Controllers
{
    /// <summary>
    ///     контролер для генерации Pdf
    /// </summary>
    //[AllowAnonymous]
    public class PdfController : BaseController
    {
        private const FileOptions FileFlagNoBuffering = (FileOptions) 0x20000000;
        protected BaseFont BaseBoldFont;
        protected BaseFont BaseFont;
        protected Font Font;
        protected Font FontBold;
        protected Font FontBoldUnderline;
        protected Font FontUnderline;
        protected int RegularFontSize;
        protected Font ReportHeaderFont;
        protected Font ReportSubHeaderFont;
        protected Font SmallFont;
        protected Font TableHeaderFont;
        protected Font TopHeaderFont;

        public PdfController(int mainFontSize, int headerFontSize, int regularFontSize)
        {
            RegularFontSize = regularFontSize;
            var fontPath = Environment.GetEnvironmentVariable("SystemRoot") + "\\fonts\\times.ttf";
            var boldFontPath = Environment.GetEnvironmentVariable("SystemRoot") + "\\fonts\\timesbd.ttf";
            BaseFont = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            BaseBoldFont = BaseFont.CreateFont(boldFontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);

            Font = new Font(BaseFont, regularFontSize);
            FontUnderline = new Font(BaseFont, regularFontSize, Font.UNDERLINE);
            SmallFont = new Font(BaseFont, regularFontSize - 2);
            FontBold = new Font(BaseBoldFont, regularFontSize);
            FontBoldUnderline = new Font(BaseBoldFont, regularFontSize, Font.UNDERLINE);

            TableHeaderFont = new Font(BaseBoldFont, headerFontSize);
            ReportSubHeaderFont = new Font(BaseBoldFont, headerFontSize);
            ReportHeaderFont = new Font(BaseBoldFont, mainFontSize);
            TopHeaderFont = new Font(BaseBoldFont, mainFontSize + 2);
        }

        public PdfController()
            : this(12, 10, 8)
        {
        }

        public CertificateController ApiCertificateController { get; set; }

        public override void SetUnitOfWorkInRefClass(IUnitOfWork unitOfWork)
        {
            base.SetUnitOfWorkInRefClass(unitOfWork);
            ApiCertificateController.SetUnitOfWorkInRefClass(unitOfWork);
        }

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
        ///     формирование сертификата
        /// </summary>
        public FileResult GetCertificateForRequest(long requestId)
        {
            return GetCertificateForRequestUk(requestId, Settings.Default.SecretKey);
        }

        /// <summary>
        ///     формирование сертификата для онлайн записи на прием в Мосгортур
        /// </summary>
        [Authorize]
        public FileResult GetCertificateForVisitBooking(long bookingId)
        {
            var filename = CertificateForVisitBooking(UnitOfWork, bookingId);

            if (string.IsNullOrWhiteSpace(filename))
            {
                return null;
            }

            var outputStream = new FileStream(filename, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite, 512,
                FileOptions.DeleteOnClose | FileFlagNoBuffering);
            HttpContext.Response.AddHeader("content-disposition", "attachment; filename=VisitBookingCertificate.pdf");
            return new FileStreamResult(outputStream, "application/pdf");
        }

        /// <summary>
        ///     формирование сертификата
        /// </summary>
        [AllowAnonymous]
        public FileResult GetCertificateForRequestUk(long requestId, string uniqueKey)
        {
            if (Settings.Default.SecretKey != uniqueKey && !Security.GetCurrentAccountId().HasValue)
            {
                return null;
            }

            var filename = CertificateForRequestTemporyFile(UnitOfWork, requestId);

            if (string.IsNullOrWhiteSpace(filename))
            {
                return null;
            }

            var outputStream = new FileStream(filename, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite, 512,
                FileOptions.DeleteOnClose | FileFlagNoBuffering);
            HttpContext.Response.AddHeader("content-disposition", "attachment; filename=certificate.pdf");
            return new FileStreamResult(outputStream, "application/pdf");
        }

        /// <summary>
        ///     сертификат для записи в МГТ
        /// </summary>
        private static string CertificateForVisitBooking(IUnitOfWork unitOfWork, long bookingId)
        {
            var visitBooking = unitOfWork.GetById<MGTBookingVisit>(bookingId);
            if (visitBooking.StatusId != 3)
            {
                return null;
            }

            var filename = GetTempFileName();
            using (var newStream = new FileStream(filename, FileMode.OpenOrCreate))
            {
                var _fontSize = 12;

                var fontPath = System.Web.HttpContext.Current?.Server.MapPath("~/Content/fonts/");
                var customFont = BaseFont.CreateFont(fontPath + "The Northern Block - Acrom Light.otf",
                    BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                var assembly = Assembly.Load("RestChild.Templates");

                using (var streamTemplate = assembly.GetManifestResourceStream("RestChild.Templates.visitBooking.pdf"))
                {
                    using (var reader = new PdfReader(streamTemplate))
                    {
                        using (var pdfStamper = new PdfStamper(reader, newStream, '1'))
                        {
                            var over = pdfStamper.GetOverContent(1);
                            over.BeginText();

                            over.SetFontAndSize(customFont, _fontSize);
                            over.ShowTextAligned(Element.ALIGN_LEFT, $"{new DateTime(visitBooking.LastUpdateTick):dd.MM.yyyy HH:mm}", 220, 817, 0);

                            over.SetFontAndSize(customFont, _fontSize);
                            over.ShowTextAligned(Element.ALIGN_LEFT,
                                visitBooking.Id.ToString(), 410,
                                817, 0);

                            if (visitBooking.Persons.Any(sx => sx.PersonTypeId == 1))
                            {
                                var applicant = visitBooking.Persons.First(sx => sx.PersonTypeId == 1);
                                over.SetFontAndSize(customFont, _fontSize);
                                over.ShowTextAligned(Element.ALIGN_LEFT,
                                    $"{applicant.LastName} {applicant.FirstName} {applicant.MiddleName}, {applicant.DateOfBirth:dd.MM.yyyy}",
                                    150, 777, 0);

                                over.SetFontAndSize(customFont, _fontSize);
                                over.ShowTextAligned(Element.ALIGN_LEFT,
                                    $"{applicant.Phone}, {applicant.Email}", 190, 747, 0);
                            }

                            over.SetFontAndSize(customFont, _fontSize);

                            writePdfTextDown(visitBooking.Target.Name, customFont, over, 165, 732, 477, Element.ALIGN_LEFT, _fontSize);

                            if (visitBooking.Children != null && visitBooking.Children.Any())
                            {
                                over.SetFontAndSize(customFont, _fontSize);
                                over.ShowTextAligned(Element.ALIGN_LEFT, "3 и более детей", 174, 675, 0);
                            }
                            else
                            {
                                over.SetFontAndSize(customFont, _fontSize);
                                over.ShowTextAligned(Element.ALIGN_LEFT, "1-2 ребенка", 174, 675, 0);
                            }

                            over.SetFontAndSize(customFont, _fontSize);
                            over.ShowTextAligned(Element.ALIGN_LEFT,
                                $"{visitBooking.VisitCell:dd.MM.yyyy HH:mm}", 500, 675, 0);

                            over.ShowTextAligned(Element.ALIGN_LEFT,
                                Utils.GeneratePin(visitBooking.Id), 450, 655, 0);

                            over.EndText();
                        }
                    }
                }
            }

            return filename;
        }

        internal static string CertificateForRequestTemporyFile(IUnitOfWork unitOfWork, long requestId)
        {
            var filename = GetTempFileName();

            var certArray = PdfProcessor.CertificateForRequestTemporaryFile(unitOfWork, requestId);
            using (var fs = new FileStream(filename, FileMode.OpenOrCreate))
            {
                fs.Write(certArray.FileBody, 0, certArray.FileBody.Length);
            }

            return filename;
        }

        /// <summary>
        ///     формирование сертификата для списка
        /// </summary>
        public ActionResult GetCertificateForChildList(long childListId)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);
            var list = UnitOfWork.GetById<ListOfChilds>(childListId);
            if (list == null || list.IsDeleted || list.Childs == null || !list.Childs.Any(c => !c.IsDeleted && c.Payed))
            {
                return RedirectToAction("OrganizationList", "Limits");
            }

            if (string.IsNullOrEmpty(list.CertificateNumber))
            {
                ApiCertificateController.GenerateCertificateNumber(list);
                if (!string.IsNullOrEmpty(list.CertificateNumber))
                {
                    UnitOfWork.SaveChanges();
                }
            }

            var children =
                list.Childs.Where(c => !c.IsDeleted && c.Payed)
                    .OrderBy(c => c.LastName)
                    .ThenBy(c => c.FirstName)
                    .ThenBy(c => c.MiddleName)
                    .ToList();
            var fontPath = Server.MapPath("~/Content/fonts/");
            var customFont = BaseFont.CreateFont(fontPath + "The Northern Block - Acrom Light.otf", BaseFont.IDENTITY_H,
                BaseFont.EMBEDDED);
            var assembly = Assembly.Load("RestChild.Templates");

            var filename = GetTempFileName();
            using (var resultStream = new FileStream(filename, FileMode.OpenOrCreate))
            using (var templateStream = new MemoryStream())
            using (
                var contentTemplateStream =
                    assembly.GetManifestResourceStream("RestChild.Templates.childListCertContent.pdf"))
            using (
                var headerTemplateStream =
                    assembly.GetManifestResourceStream("RestChild.Templates.childListCertHeaderFooter.pdf"))
            using (var headerReader = new PdfReader(headerTemplateStream))
            using (var contentReader = new PdfReader(contentTemplateStream))
            {
                using (var doc = new Document())
                {
                    using (var copy = new PdfCopy(doc, templateStream))
                    {
                        var pageCount = 1;
                        doc.Open();
                        copy.AddPage(copy.GetImportedPage(headerReader, 1));
                        var childrenCount = children.Count;
                        childrenCount -= 10;
                        while (childrenCount >= 0)
                        {
                            copy.AddPage(copy.GetImportedPage(contentReader, 1));
                            childrenCount -= 20;
                            pageCount++;
                        }

                        copy.AddPage(copy.GetImportedPage(headerReader, 2));
                        copy.CloseStream = false;
                        doc.Close();

                        templateStream.Seek(0, SeekOrigin.Begin);
                        using (var templateReader = new PdfReader(templateStream))
                        using (var stamper = new PdfStamper(templateReader, resultStream))
                        {
                            FormChildListCertificateFirstPage(stamper, customFont, list, children);
                            for (var i = 1; i < pageCount; i++)
                            {
                                FormChildLitCertificateNPage(stamper, customFont, list, children, i + 1);
                            }
                        }
                    }
                }
            }

            var outputStream = new FileStream(filename, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite, 512,
                FileOptions.DeleteOnClose | FileFlagNoBuffering);
            HttpContext.Response.AddHeader("content-disposition", "attachment; filename=certificate.pdf");
            return new FileStreamResult(outputStream, "application/pdf");
        }

        private void FormChildListCertificateFirstPage(PdfStamper stamper, BaseFont customfont, ListOfChilds list,
            List<Child> children)
        {
            var over = stamper.GetOverContent(1);
            var rect = new Rectangle(400, 378, 500, 390) {BackgroundColor = BaseColor.WHITE};
            over.Rectangle(rect);
            over.BeginText();
            over.SetFontAndSize(customfont, 10);
            writePdfText(list.Name.FormatEx(false), customfont, over, 66, 438, 715);
            writePdfText(
                list.NullSafe(l => l.LimitOnOrganization.Organization.Name).FormatEx(false),
                customfont,
                over,
                66,
                408,
                715);
            writePdfText(list.Tour.DateIncome.FormatEx(), customfont, over, 140, 378, 130);
            writePdfText(list.Tour.DateOutcome.FormatEx(), customfont, over, 287, 378, 130);
            writePdfText(list.NullSafe(l => l.Tour.Hotels.Name).FormatEx(false), customfont, over, 132, 355, 648);
            writePdfText(list.CertificateNumber.FormatEx(false), customfont, over, 682, 479, 93.54f);

            var childNumber = 1;
            var childrenCountInFirstPage = children.Count >= 10 ? 10 : children.Count;
            for (var i = 0; i < childrenCountInFirstPage; i++)
            {
                var firstField = $"{childNumber}. {children[i].GetFio().FormatEx(false)}";
                var secondField =
                    $"{children[i].DateOfBirth.FormatEx()}, {children[i].DocumentSeria.FormatEx(false)} {children[i].DocumentNumber.FormatEx(false)}";
                writePdfText(firstField, customfont, over, 66, 259 - i * 23.7f, 471, Element.ALIGN_LEFT);
                writePdfText(secondField, customfont, over, 540, 259 - i * 23.7f, 240);
                childNumber++;
            }

            over.EndText();
        }

        private void FormChildLitCertificateNPage(PdfStamper stamper, BaseFont customfont, ListOfChilds list,
            List<Child> childs, int pageNum)
        {
            var over = stamper.GetOverContent(pageNum);
            var firstChildNum = 10 + (pageNum - 2) * 20;
            var childrenInPage = childs.Count - firstChildNum > 20 ? 20 : childs.Count - firstChildNum;
            over.BeginText();
            for (var i = 0; i < childrenInPage; i++)
            {
                var firstField = $"{firstChildNum + 1 + i}. {childs[firstChildNum + i].GetFio().FormatEx(false)}";
                var secondField =
                    $"{childs[firstChildNum + i].DateOfBirth.FormatEx()}, {childs[firstChildNum + i].DocumentSeria.FormatEx(false)} {childs[firstChildNum + i].DocumentNumber.FormatEx(false)}";
                writePdfText(firstField, customfont, over, 66, 492 - i * 23.7f, 471, Element.ALIGN_LEFT);
                writePdfText(secondField, customfont, over, 540, 492 - i * 23.7f, 240);
            }

            writePdfText(pageNum.ToString(), customfont, over, 597, 535, 18.4f);
            writePdfText(list.CertificateNumber.FormatEx(false), customfont, over, 379, 536, 93.54f);
            over.EndText();
        }

        private static void writePdfText(string text, BaseFont font, PdfContentByte over, float xPos, float yPos, float width,
            int horizontalAlign = Element.ALIGN_CENTER, int fontSize = 10)
        {
            var headerTable = new PdfPTable(1) {TotalWidth = width};
            var p = new Phrase(new Chunk(text, new Font(font, fontSize)));
            var company = new PdfPCell(p);
            company.SetLeading(11, 0);
            company.HorizontalAlignment = horizontalAlign;
            company.VerticalAlignment = Element.ALIGN_BOTTOM;
            company.BorderWidth = 0;
            headerTable.AddCell(company);
            headerTable.WriteSelectedRows(0, 1, 0, 1, xPos, yPos + headerTable.TotalHeight, over);
        }

        private static void writePdfTextDown(string text, BaseFont font, PdfContentByte over, float xPos, float yPos, float width,
            int horizontalAlign = Element.ALIGN_CENTER, int fontSize = 10)
        {
            var headerTable = new PdfPTable(1) {TotalWidth = width};
            var p = new Phrase(new Chunk(text, new Font(font, fontSize)));
            var company = new PdfPCell(p);
            company.SetLeading(11, 0);
            company.HorizontalAlignment = horizontalAlign;
            company.VerticalAlignment = Element.ALIGN_TOP;
            company.BorderWidth = 0;
            headerTable.AddCell(company);
            headerTable.WriteSelectedRows(0, 1, 0, 1, xPos, yPos, over);
        }

        /// <summary>
        ///     отправка данных на подписание.
        /// </summary>
        public DataForSign GetDataForSign(long entityId, SignTypeEnum signType)
        {
            string fileName = null;
            var res = new DataForSign {EntityId = entityId, SignType = signType};

            switch (signType)
            {
                case SignTypeEnum.ListOfCity:
                    fileName = GetCityChildsFile(entityId);
                    res.Name = "children.pdf";
                    res.Title = "Список";
                    res.MimeType = "application/pdf";
                    break;
                case SignTypeEnum.ListOfOiv:
                    fileName = GetOivChildsFile(entityId);
                    res.Name = "children.pdf";
                    res.Title = "Список";
                    res.MimeType = "application/pdf";
                    break;
                case SignTypeEnum.ListOfOrganization:
                    fileName = GetOrganizationChildsFile(entityId);
                    res.Name = "children.pdf";
                    res.Title = "Список";
                    res.MimeType = "application/pdf";
                    break;
                case SignTypeEnum.TourInfo:
                    fileName = GetBlockOfPlace(entityId);
                    res.Name = "blockOfPlace.pdf";
                    res.Title = "Размещение";
                    res.MimeType = "application/pdf";
                    break;
            }

            if (!string.IsNullOrEmpty(fileName))
            {
                var data = System.IO.File.ReadAllBytes(fileName);
                System.IO.File.Delete(fileName);
                res.Content = data;
                return res;
            }

            return null;
        }

        #region Блок Мест

        public string GetBlockOfPlace(long id)
        {
            var filename = GetTempFileName();
            using (var stream = new FileStream(filename, FileMode.OpenOrCreate))
            {
                var document = new Document(PageSize.A4, 10, 10, 10, 10);

                PdfWriter.GetInstance(document, stream);

                document.Open();
                document.Add(new Paragraph("Размещение", ReportHeaderFont) {Alignment = Element.ALIGN_CENTER});

                var tour = UnitOfWork.GetById<Tour>(id);

                var table = new PdfPTable(2)
                {
                    WidthPercentage = 100,
                    SpacingAfter = 10,
                    ExtendLastRow = false
                };

                table.SetWidths(new[] {3, 7});

                table.Rows.Add(
                    new PdfPRow(new[]
                    {
                        new PdfPCell(new Phrase("Цель обращения", TableHeaderFont))
                            {HorizontalAlignment = Element.ALIGN_RIGHT},
                        new PdfPCell(new Phrase(tour.TypeOfRest.NullSafe(t => t.Name), Font))
                            {HorizontalAlignment = Element.ALIGN_LEFT}
                    }));
                table.Rows.Add(
                    new PdfPRow(new[]
                    {
                        new PdfPCell(new Phrase("Год кампании", TableHeaderFont))
                            {HorizontalAlignment = Element.ALIGN_RIGHT},
                        new PdfPCell(new Phrase(tour.YearOfRest.NullSafe(t => t.Name), Font))
                            {HorizontalAlignment = Element.ALIGN_LEFT}
                    }));
                table.Rows.Add(
                    new PdfPRow(new[]
                    {
                        new PdfPCell(new Phrase("Наименование места отдыха", TableHeaderFont))
                            {HorizontalAlignment = Element.ALIGN_RIGHT},
                        new PdfPCell(new Phrase(tour.Hotels.NullSafe(t => t.Name), Font))
                            {HorizontalAlignment = Element.ALIGN_LEFT}
                    }));
                table.Rows.Add(
                    new PdfPRow(new[]
                    {
                        new PdfPCell(new Phrase("Время отдыха", TableHeaderFont))
                            {HorizontalAlignment = Element.ALIGN_RIGHT},
                        new PdfPCell(new Phrase(tour.TimeOfRest.NullSafe(t => t.Name), Font))
                            {HorizontalAlignment = Element.ALIGN_LEFT}
                    }));
                table.Rows.Add(
                    new PdfPRow(new[]
                    {
                        new PdfPCell(new Phrase("Дата начала", TableHeaderFont))
                            {HorizontalAlignment = Element.ALIGN_RIGHT},
                        new PdfPCell(new Phrase(tour.DateIncome.FormatEx(), Font))
                            {HorizontalAlignment = Element.ALIGN_LEFT}
                    }));
                table.Rows.Add(
                    new PdfPRow(new[]
                    {
                        new PdfPCell(new Phrase("Дата окончания", TableHeaderFont))
                            {HorizontalAlignment = Element.ALIGN_RIGHT},
                        new PdfPCell(new Phrase(tour.DateOutcome.FormatEx(), Font))
                            {HorizontalAlignment = Element.ALIGN_LEFT}
                    }));

                if (tour.StartBooking.HasValue)
                {
                    table.Rows.Add(
                        new PdfPRow(new[]
                        {
                            new PdfPCell(new Phrase("Дата начала записи", TableHeaderFont))
                                {HorizontalAlignment = Element.ALIGN_RIGHT},
                            new PdfPCell(new Phrase(tour.StartBooking.FormatEx(), Font))
                            {
                                HorizontalAlignment = Element.ALIGN_LEFT
                            }
                        }));
                }

                if (tour.EndBooking.HasValue)
                {
                    table.Rows.Add(
                        new PdfPRow(new[]
                        {
                            new PdfPCell(new Phrase("Дата окончания записи", TableHeaderFont))
                                {HorizontalAlignment = Element.ALIGN_RIGHT},
                            new PdfPCell(new Phrase(tour.EndBooking.FormatEx(), Font))
                            {
                                HorizontalAlignment = Element.ALIGN_LEFT
                            }
                        }));
                }

                if (tour.SubjectOfRest != null)
                {
                    table.Rows.Add(
                        new PdfPRow(new[]
                        {
                            new PdfPCell(new Phrase("Тематика смены", TableHeaderFont))
                                {HorizontalAlignment = Element.ALIGN_RIGHT},
                            new PdfPCell(new Phrase(tour.SubjectOfRest.Name, Font))
                            {
                                HorizontalAlignment = Element.ALIGN_LEFT
                            }
                        }));
                }

                document.Add(table);

                document.Close();
            }

            return filename;
        }

        #endregion

        #region Квитанция

        /// <summary>
        ///     список квитанций
        /// </summary>
        public FileResult GetPaymentListOfChilds(long id)
        {
            var limit = UnitOfWork.GetById<ListOfChilds>(id);

            if (limit != null &&
                limit.NullSafe(l => l.LimitOnOrganization.StateId) !=
                StateMachineStateEnum.Limit.Organization.Confirmed)
            {
                return null;
            }

            var fileName = GetPaymentListOfChildsFile(id);

            var outputStream = new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite, 512,
                FileOptions.DeleteOnClose | FileFlagNoBuffering);
            HttpContext.Response.AddHeader("content-disposition", "attachment; filename=paymentList.pdf");
            return new FileStreamResult(outputStream, "application/pdf");
        }

        /// <summary>
        ///     платежка на список.
        /// </summary>
        public string GetPaymentListOfChildsFile(long limitId)
        {
            var filename = GetTempFileName();
            using (var stream = new FileStream(filename, FileMode.OpenOrCreate))
            {
                var document = new Document(PageSize.A4, 20, 20, 20, 20);

                PdfWriter.GetInstance(document, stream);

                document.Open();

                var limit = UnitOfWork.GetById<ListOfChilds>(limitId);

                var index = 0;

                foreach (var child in limit.Childs)
                {
                    AddOnePaymentChild(document, child);
                    index++;
                    if (index == 2)
                    {
                        document.Add(Chunk.NEXTPAGE);
                        index = 0;
                    }
                }

                foreach (var attendant in limit.Attendants)
                {
                    AddOnePaymentAttendant(document, attendant);
                    index++;
                    if (index == 2)
                    {
                        document.Add(Chunk.NEXTPAGE);
                        index = 0;
                    }
                }

                document.Close();
            }

            return filename;
        }

        /// <summary>
        ///     платежка на ребенка.
        /// </summary>
        public string GetPaymentAttendantFile(long attendantId)
        {
            var filename = GetTempFileName();
            using (var stream = new FileStream(filename, FileMode.OpenOrCreate))
            {
                var document = new Document(PageSize.A4, 20, 20, 20, 20);

                PdfWriter.GetInstance(document, stream);

                document.Open();

                AddOnePaymentAttendant(document, UnitOfWork.GetById<Applicant>(attendantId));

                FillGeneralFooter(document);

                document.Close();
            }

            return filename;
        }

        /// <summary>
        ///     платежка на ребенка.
        /// </summary>
        public string GetChildPaymentFile(long childId)
        {
            var filename = GetTempFileName();
            using (var stream = new FileStream(filename, FileMode.OpenOrCreate))
            {
                var document = new Document(PageSize.A4, 20, 20, 20, 20);

                PdfWriter.GetInstance(document, stream);

                document.Open();

                AddOnePaymentChild(document, UnitOfWork.GetById<Child>(childId));

                document.Add(
                    new Paragraph(
                            "В соответствии с Порядком  организации отдыха и оздоровления детей в городе Москве, утвержденным  Постановлением Правительства Москвы от  15 февраля 2011 г. N 29-ПП «Об организации отдыха и оздоровления детей города Москвы в 2011 году и последующие годы», путевки на льготной основе с частичной оплатой их стоимости родителями (законными представителями) в размере 10% стоимости путевки (проезд к месту отдыха и обратно осуществляется за счет средств родителей) в период школьных каникул предоставляются:-  детям - лауреатам детских международных, федеральных, городских олимпиад, конкурсов;",
                            SmallFont)
                        {Alignment = Element.ALIGN_LEFT});
                document.Add(
                    new Paragraph(
                            "-  детям - участникам детских коллективов различной направленности, созданных в учреждениях, находящихся в ведении органов исполнительной власти города Москвы, детям - членам детских общественных объединений, следующим на отдых в составе организованных групп, детям - воспитанникам детских досуговых клубов по месту жительства;",
                            SmallFont)
                        {Alignment = Element.ALIGN_LEFT});
                document.Add(
                    new Paragraph(
                            "- детям, обучающимся в образовательных учреждениях, финансируемых за счет средств бюджета города Москвы, следующим в походы, экспедиции в составе организованных групп, формируемых образовательными учреждениями уполномоченного органа исполнительной власти города Москвы в сфере образования.",
                            SmallFont)
                        {Alignment = Element.ALIGN_LEFT});
                document.Add(
                    new Paragraph(
                            "Внимание!!! В графе «Платеж» следует указать ФИО ребенка и код, следующего на отдых.",
                            SmallFont)
                        {Alignment = Element.ALIGN_LEFT});
                FillGeneralFooter(document);

                document.Close();
            }

            return filename;
        }

        private void FillGeneralFooter(Document document)
        {
            document.Add(
                new Paragraph(
                        "В случае отказа от услуг по оздоровительному отдыху без уважительной причины денежные средства, полученные на расчетный счет ГАУК «Мосгортур», не возвращаются. Требования о возврате денежных средств без подтверждающих документов и предъявленные позднее 20 дней по окончании заезда не принимаются. Возврат денежных средств осуществляется за вычетом фактически понесенных затрат.",
                        SmallFont)
                    {Alignment = Element.ALIGN_LEFT});
            document.Add(
                new Paragraph(
                        "Администрация ГАУК «МОСГОРТУР» тел. 8 (499) 241 31 16 Сайт: www.mosgortur.ru",
                        SmallFont)
                    {Alignment = Element.ALIGN_LEFT});
        }

        private void AddOnePaymentChild(Document document, Child child)
        {
            if (child.NullSafe(c => c.ChildList.Tour) == null)
            {
                return;
            }

            var table = new PdfPTable(2)
            {
                WidthPercentage = 100,
                SpacingAfter = 10,
                ExtendLastRow = false
            };

            table.SetWidths(new[] {3, 7});

            var price = child.ChildList.Tour.TourPrice ?? 0;

            var firstPhase = GetChildBlock(child, price);
            var secondPhase = GetChildBlock(child, price);

            table.Rows.Add(
                new PdfPRow(new[]
                {
                    new PdfPCell(new Phrase("Платеж", Font))
                    {
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        VerticalAlignment = Element.ALIGN_TOP
                    },
                    firstPhase
                }));

            table.Rows.Add(
                new PdfPRow(new[]
                {
                    new PdfPCell(new Phrase("Квитанция\n\nКассир", Font))
                    {
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        VerticalAlignment = Element.ALIGN_BOTTOM
                    },
                    secondPhase
                }));


            document.Add(table);
        }

        private PdfPCell GetChildBlock(Child child, decimal price)
        {
            var firstPhase = new PdfPCell {HorizontalAlignment = Element.ALIGN_LEFT};

            var p = new Paragraph(
                    "Получатель: ", Font)
                {Alignment = Element.ALIGN_LEFT};
            p.Add(new Phrase("Департамент финансов города Москвы (ГАУК «МОСГОРТУР» л/с 2805651000451801)",
                FontBoldUnderline));
            firstPhase.AddElement(p);

            p = new Paragraph("КПП: ", Font)
            {
                Alignment = Element.ALIGN_LEFT
            };
            p.Add(new Phrase("770401001", FontUnderline));
            p.Add(new Phrase(" ИНН: ", Font));
            p.Add(new Phrase("7704747169", FontUnderline));
            p.Add(new Phrase(" Код ОКАТО: ", Font));
            p.Add(new Phrase("45286552000", FontUnderline));
            firstPhase.AddElement(p);

            p = new Paragraph("P/сч.: ", Font)
            {
                Alignment = Element.ALIGN_LEFT
            };
            p.Add(new Phrase("40601810245253000002 в ГУ Банка России по ЦФО", FontUnderline));
            firstPhase.AddElement(p);

            p = new Paragraph("БИК: ", Font)
            {
                Alignment = Element.ALIGN_LEFT
            };

            p.Add(new Phrase("044525000", FontUnderline));
            p.Add(new Phrase(" К/сч.: ", Font));
            p.Add(new Phrase("нет", FontUnderline));
            p.Add(new Phrase(" КБК: ", FontBold));
            p.Add(new Phrase("00000000000000000180 (сотруднику банка просьба заполнять обязательно)",
                FontBoldUnderline));
            firstPhase.AddElement(p);

            p = new Paragraph(
                    "Платеж: ",
                    Font)
                {Alignment = Element.ALIGN_LEFT};

            p.Add(
                new Phrase(
                    "(КЭСО 180)Возмещение стоимости путевки ",
                    FontUnderline));

            firstPhase.AddElement(p);
            firstPhase.AddElement(
                new Paragraph(
                        $"по договору № {child.NullSafe(c => c.ChildList.Tour.Contract.SignNumber)} номер смены {child.NullSafe(c => c.ChildList.Tour.TimeOfRest.Name)}",
                        FontUnderline)
                    {Alignment = Element.ALIGN_LEFT});
            firstPhase.AddElement(
                new Paragraph(
                        $"за ребенка {child.LastName} {child.FirstName} {child.MiddleName} (код 01-{child.Id}-{child.ChildListId})",
                        FontBold)
                    {Alignment = Element.ALIGN_LEFT});
            firstPhase.AddElement(new LineSeparator(0.1f, 100f, new BaseColor(10, 10, 10), Element.ALIGN_CENTER, -1));
            firstPhase.AddElement(new Paragraph("ФИО ребенка (СТРОКА ОБЯЗАТЕЛЬНА ДЛЯ ЗАПОЛНЕНИЯ) ", SmallFont)
            {
                Alignment = Element.ALIGN_CENTER
            });
            firstPhase.AddElement(
                new Paragraph(
                        "Плательщик: (Ф.И.О. родителя) _____________________________________________________________________",
                        Font)
                    {Alignment = Element.ALIGN_LEFT});
            firstPhase.AddElement(
                new Paragraph(
                        "Адрес плательщика: ____________________________________________________________________________",
                        Font)
                    {Alignment = Element.ALIGN_LEFT});
            firstPhase.AddElement(new Paragraph(
                    "ИНН плательщика: __________________ № л/сч. плательщика: __________________",
                    Font)
                {Alignment = Element.ALIGN_LEFT});

            firstPhase.AddElement(
                new Paragraph(
                        $"Сумма: {Math.Floor(price).FormatEx("### ### ### ### ##0")} руб. {Math.Floor((price - Math.Floor(price)) * 100)} коп.",
                        Font)
                    {Alignment = Element.ALIGN_LEFT});
            firstPhase.AddElement(new Paragraph(
                    "Подпись: ________________________ Дата: \" ____ \" __________  20___ г.",
                    Font)
                {Alignment = Element.ALIGN_LEFT});
            firstPhase.AddElement(new Paragraph(" ", Font));
            return firstPhase;
        }

        private void AddOnePaymentAttendant(Document document, Applicant attendant)
        {
            if (attendant.NullSafe(c => c.ChildList.Tour) == null)
            {
                return;
            }

            var table = new PdfPTable(2)
            {
                WidthPercentage = 100,
                SpacingAfter = 10,
                ExtendLastRow = false
            };

            table.SetWidths(new[] {3, 7});

            var price = attendant.ChildList.Tour.TourPriceAttendant ?? 0;

            var firstPhase = GetAttendantBlock(attendant, price);
            var secondPhase = GetAttendantBlock(attendant, price);

            table.Rows.Add(
                new PdfPRow(new[]
                {
                    new PdfPCell(new Phrase("Платеж", Font))
                    {
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        VerticalAlignment = Element.ALIGN_TOP
                    },
                    firstPhase
                }));

            table.Rows.Add(
                new PdfPRow(new[]
                {
                    new PdfPCell(new Phrase("Квитанция\n\nКассир", Font))
                    {
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        VerticalAlignment = Element.ALIGN_BOTTOM
                    },
                    secondPhase
                }));


            document.Add(table);
        }

        private PdfPCell GetAttendantBlock(Applicant applicant, decimal price)
        {
            var firstPhase = new PdfPCell {HorizontalAlignment = Element.ALIGN_LEFT};

            var p = new Paragraph(
                    "Получатель: ", Font)
                {Alignment = Element.ALIGN_LEFT};
            p.Add(new Phrase("Департамент финансов города Москвы (ГАУК «МОСГОРТУР» л/с 2805651000451801)",
                FontBoldUnderline));
            firstPhase.AddElement(p);

            p = new Paragraph("КПП: ", Font)
            {
                Alignment = Element.ALIGN_LEFT
            };
            p.Add(new Phrase("770401001", FontUnderline));
            p.Add(new Phrase(" ИНН: ", Font));
            p.Add(new Phrase("7704747169", FontUnderline));
            p.Add(new Phrase(" Код ОКАТО: ", Font));
            p.Add(new Phrase("45286552000", FontUnderline));
            firstPhase.AddElement(p);

            p = new Paragraph("P/сч.: ", Font)
            {
                Alignment = Element.ALIGN_LEFT
            };
            p.Add(new Phrase("40601810245253000002 в ГУ Банка России по ЦФО", FontUnderline));
            firstPhase.AddElement(p);

            p = new Paragraph("БИК: ", Font)
            {
                Alignment = Element.ALIGN_LEFT
            };

            p.Add(new Phrase("044525000", FontUnderline));
            p.Add(new Phrase(" К/сч.: ", Font));
            p.Add(new Phrase("нет", FontUnderline));
            p.Add(new Phrase(" КБК: ", FontBold));
            p.Add(new Phrase("00000000000000000180 (сотруднику банка просьба заполнять обязательно)",
                FontBoldUnderline));
            firstPhase.AddElement(p);

            p = new Paragraph(
                    "Платеж: ",
                    Font)
                {Alignment = Element.ALIGN_LEFT};

            p.Add(
                new Phrase(
                    "(КЭСО 180)Возмещение стоимости путевки ",
                    FontUnderline));

            firstPhase.AddElement(p);
            firstPhase.AddElement(
                new Paragraph(
                        $"по договору № КОМ. номер смены {applicant.NullSafe(c => c.ChildList.Tour.TimeOfRest.Name)}",
                        FontUnderline)
                    {Alignment = Element.ALIGN_LEFT});
            firstPhase.AddElement(
                new Paragraph(
                        $"за должностное лицо: {applicant.LastName} {applicant.FirstName} {applicant.MiddleName} (код 02-{applicant.Id}-{applicant.ChildListId})",
                        Font)
                    {Alignment = Element.ALIGN_LEFT});
            firstPhase.AddElement(new LineSeparator(0.1f, 100f, new BaseColor(10, 10, 10), Element.ALIGN_CENTER, -1));
            firstPhase.AddElement(new Paragraph("ФИО  должностного лица (СТРОКА ОБЯЗАТЕЛЬНА ДЛЯ ЗАПОЛНЕНИЯ)", SmallFont)
            {
                Alignment = Element.ALIGN_CENTER
            });
            firstPhase.AddElement(
                new Paragraph("Плательщик: _____________________________________________________________________",
                        Font)
                    {Alignment = Element.ALIGN_LEFT});
            firstPhase.AddElement(
                new Paragraph(
                        "Адрес плательщика: ____________________________________________________________________________",
                        Font)
                    {Alignment = Element.ALIGN_LEFT});

            firstPhase.AddElement(
                new Paragraph(
                        $"Сумма: {Math.Floor(price).FormatEx("### ### ### ### ##0")} руб. {Math.Floor((price - Math.Floor(price)) * 100)} коп.",
                        Font)
                    {Alignment = Element.ALIGN_LEFT});
            firstPhase.AddElement(new Paragraph(
                    "Подпись: ________________________ Дата: \" ____ \" __________  20___ г.",
                    Font)
                {Alignment = Element.ALIGN_LEFT});
            firstPhase.AddElement(new Paragraph(" ", Font));
            return firstPhase;
        }

        public FileResult GetPayment(long id)
        {
            var fileName = GetChildPaymentFile(id);

            var outputStream = new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite, 512,
                FileOptions.DeleteOnClose | FileFlagNoBuffering);
            HttpContext.Response.AddHeader("content-disposition", "attachment; filename=payment.pdf");
            return new FileStreamResult(outputStream, "application/pdf");
        }

        public FileResult GetPaymentAttendant(long id)
        {
            var fileName = GetPaymentAttendantFile(id);

            var outputStream = new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite, 512,
                FileOptions.DeleteOnClose | FileFlagNoBuffering);
            HttpContext.Response.AddHeader("content-disposition", "attachment; filename=payment.pdf");
            return new FileStreamResult(outputStream, "application/pdf");
        }

        #endregion

        #region Список детей ОИВ

        /// <summary>
        ///     формирование списка детей от ОИВ.
        /// </summary>
        public FileResult GetOivChilds(long id)
        {
            var fileName = GetOivChildsFile(id);

            var outputStream = new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite, 512,
                FileOptions.DeleteOnClose | FileFlagNoBuffering);
            HttpContext.Response.AddHeader("content-disposition", "attachment; filename=children.pdf");
            return new FileStreamResult(outputStream, "application/pdf");
        }

        /// <summary>
        ///     список ОИВ.
        /// </summary>
        public string GetOivChildsFile(long id)
        {
            var filename = GetTempFileName();
            using (var stream = new FileStream(filename, FileMode.OpenOrCreate))
            {
                var document = new Document(PageSize.A4, 10, 10, 10, 10);

                PdfWriter.GetInstance(document, stream);

                document.Open();
                document.Add(new Paragraph("ЗАЯВКА", ReportHeaderFont) {Alignment = Element.ALIGN_CENTER});
                document.Add(new Paragraph("на обеспечение выездного отдыха и оздоровления детей", ReportHeaderFont)
                {
                    Alignment = Element.ALIGN_CENTER
                });

                var limit = UnitOfWork.GetById<LimitOnVedomstvo>(id);

                if (limit != null)
                {
                    document.Add(new Paragraph(limit.Organization.Name, ReportHeaderFont)
                    {
                        Alignment = Element.ALIGN_CENTER,
                        SpacingAfter = 10
                    });
                }

                var table = new PdfPTable(4)
                {
                    WidthPercentage = 100,
                    SpacingAfter = 10,
                    ExtendLastRow = false
                };
                table.SetWidths(new[] {1, 7, 4, 7});
                table.Rows.Add(
                    new PdfPRow(new[]
                    {
                        new PdfPCell(new Phrase("№", TableHeaderFont)) {HorizontalAlignment = Element.ALIGN_CENTER},
                        new PdfPCell(new Phrase("Фамилия, имя, отчество", TableHeaderFont))
                            {HorizontalAlignment = Element.ALIGN_CENTER},
                        new PdfPCell(new Phrase("Дата рождения", TableHeaderFont))
                            {HorizontalAlignment = Element.ALIGN_CENTER},
                        new PdfPCell(new Phrase("Документ удостоверяющий личность", TableHeaderFont))
                        {
                            HorizontalAlignment = Element.ALIGN_CENTER
                        }
                    }));

                var index = 0;

                if (limit != null)
                {
                    foreach (
                        var orgLimit in
                        limit.VedomstvoLimit
                            .Where(l => l.StateId.HasValue && l.StateId != StateMachineStateEnum.Deleted)
                            .ToList())
                    {
                        var listOfChilds =
                            UnitOfWork.GetSet<ListOfChilds>()
                                .Where(
                                    l =>
                                        !l.IsDeleted && l.IsLast && l.StateId.HasValue &&
                                        l.StateId != StateMachineStateEnum.Deleted &&
                                        l.LimitOnOrganizationId == orgLimit.Id)
                                .ToList();

                        foreach (var list in listOfChilds)
                        {
                            var reportRow = new PdfPRow(new[]
                                {new PdfPCell(), new PdfPCell(), new PdfPCell(), new PdfPCell()});
                            var row = new PdfPRow(reportRow);
                            table.Rows.Add(row);
                            var cell = row.GetCells()[0];
                            cell.Colspan = 4;
                            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cell.Phrase =
                                new Phrase(
                                    $"{list.LimitOnOrganization.Organization.Name}, {list.Name}, {list.ListOfChildsCategory.NullSafe(p => p.Name)}",
                                    FontBold);

                            reportRow = new PdfPRow(new[]
                                {new PdfPCell(), new PdfPCell(), new PdfPCell(), new PdfPCell()});
                            row = new PdfPRow(reportRow);
                            table.Rows.Add(row);
                            cell = row.GetCells()[0];
                            cell.Colspan = 4;
                            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cell.Phrase = new Phrase("   Дети", Font);

                            foreach (var child in list.Childs)
                            {
                                reportRow = new PdfPRow(new[]
                                    {new PdfPCell(), new PdfPCell(), new PdfPCell(), new PdfPCell()});
                                row = new PdfPRow(reportRow);
                                table.Rows.Add(row);
                                var cells = row.GetCells();
                                cells[0].Phrase = new Phrase((++index).ToString(), Font);
                                cells[0].HorizontalAlignment = Element.ALIGN_CENTER;
                                cells[1].Phrase = new Phrase($"{child.LastName} {child.FirstName} {child.MiddleName}",
                                    Font);
                                cells[2].Phrase = new Phrase(child.DateOfBirth.FormatEx(), Font);
                                cells[3].Phrase =
                                    new Phrase(
                                        $"{child.NullSafe(c => c.DocumentType.Name).FormatEx()}, {child.DocumentSeria} {child.DocumentNumber}",
                                        Font);
                            }

                            if (list.Attendants.Any())
                            {
                                reportRow = new PdfPRow(new[]
                                    {new PdfPCell(), new PdfPCell(), new PdfPCell(), new PdfPCell()});
                                row = new PdfPRow(reportRow);
                                table.Rows.Add(row);
                                cell = row.GetCells()[0];
                                cell.Colspan = 4;
                                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                                cell.Phrase = new Phrase("   Сопровождающие", Font);
                            }

                            foreach (var attendant in list.Attendants)
                            {
                                reportRow = new PdfPRow(new[]
                                    {new PdfPCell(), new PdfPCell(), new PdfPCell(), new PdfPCell()});
                                row = new PdfPRow(reportRow);
                                table.Rows.Add(row);
                                var cells = row.GetCells();
                                cells[0].Phrase = new Phrase((++index).ToString(), Font);
                                cells[0].HorizontalAlignment = Element.ALIGN_CENTER;

                                cells[1].Phrase =
                                    new Phrase($"{attendant.LastName} {attendant.FirstName} {attendant.MiddleName}",
                                        Font);
                                cells[2].Phrase = new Phrase(attendant.DateOfBirth.FormatEx(), Font);
                                cells[3].Phrase =
                                    new Phrase(
                                        $"{attendant.NullSafe(c => c.DocumentType.Name).FormatEx()}, {attendant.DocumentSeria} {attendant.DocumentNumber}",
                                        Font);
                            }
                        }
                    }
                }

                document.Add(table);
                document.Close();
            }

            return filename;
        }

        /// <summary>
        ///     формирование списка детей от ОИВ в Excel.
        /// </summary>
        public ActionResult GetOivChildsExcel(long id)
        {
            var lists =
                UnitOfWork.GetSet<ListOfChilds>().Where(l => l.LimitOnOrganization.LimitOnVedomstvoId == id
                                                             && l.StateId.HasValue &&
                                                             l.StateId != StateMachineStateEnum.Deleted &&
                                                             !l.IsDeleted && l.IsLast
                                                             && l.LimitOnOrganization.StateId.HasValue &&
                                                             l.LimitOnOrganization.StateId !=
                                                             StateMachineStateEnum.Deleted
                                                             && l.LimitOnOrganization.LimitOnVedomstvo.StateId
                                                                 .HasValue &&
                                                             l.LimitOnOrganization.LimitOnVedomstvo.StateId !=
                                                             StateMachineStateEnum.Deleted)
                    .Include(l => l.Childs)
                    .Include(l => l.Attendants)
                    .Include(l => l.ListOfChildsCategory)
                    .Include(l => l.LimitOnOrganization)
                    .Include(l => l.Childs.Select(c => c.Address))
                    .Include(l => l.Childs.Select(c => c.School))
                    .Include(l => l.Childs.Select(c => c.DocumentType))
                    .Include(l => l.Attendants.Select(c => c.DocumentType))
                    .Include(l => l.Tour)
                    .Include(l => l.TimeOfRest)
                    .ToList();

            if (ChildrenListsExcel(lists, out var excel))
            {
                return null;
            }

            return File(excel.CreateExcel(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "Отдыхающие.xlsx");
        }

        /// <summary>
        ///     формирование списка детей от ОИВ в Excel.
        /// </summary>
        public ActionResult GetOrganizationChildsExcel(long id)
        {
            var lists =
                UnitOfWork.GetSet<ListOfChilds>().Where(l => l.LimitOnOrganizationId == id
                                                             && l.StateId.HasValue &&
                                                             l.StateId != StateMachineStateEnum.Deleted &&
                                                             !l.IsDeleted && l.IsLast
                                                             && l.LimitOnOrganization.StateId.HasValue &&
                                                             l.LimitOnOrganization.StateId !=
                                                             StateMachineStateEnum.Deleted)
                    .Include(l => l.Childs)
                    .Include(l => l.Attendants)
                    .Include(l => l.ListOfChildsCategory)
                    .Include(l => l.LimitOnOrganization)
                    .Include(l => l.Childs.Select(c => c.Address))
                    .Include(l => l.Childs.Select(c => c.School))
                    .Include(l => l.Childs.Select(c => c.DocumentType))
                    .Include(l => l.Attendants.Select(c => c.DocumentType))
                    .Include(l => l.Tour)
                    .Include(l => l.TimeOfRest)
                    .ToList();

            if (ChildrenListsExcel(lists, out var excel))
            {
                return null;
            }

            return File(excel.CreateExcel(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "Отдыхающие.xlsx");
        }

        private bool ChildrenListsExcel(List<ListOfChilds> lists, out ExcelTable<CamperModel> excel)
        {
            var campers = new List<CamperModel>();

            foreach (var list in lists)
            {
                if (list.Childs != null)
                {
                    campers.AddRange(
                        list.Childs.Where(c => !c.IsDeleted)
                            .OrderBy(c => c.LastName)
                            .ThenBy(c => c.FirstName)
                            .ThenBy(c => c.MiddleName)
                            .Select(
                                c =>
                                    new CamperModel
                                    {
                                        Category = "Ребенок",
                                        ListName = list.Name,
                                        ChildCategory = list.ListOfChildsCategory?.Name,
                                        Organization =
                                            list.LimitOnOrganization?.Organization?.Name,
                                        LastName = c.LastName,
                                        FirstName = c.FirstName,
                                        MiddleName = c.MiddleName,
                                        IsMale = c.Male,
                                        BirthDate = c.DateOfBirth,
                                        BirthPlace = c.PlaceOfBirth,
                                        DocType = c.DocumentType != null ? c.DocumentType.Name : string.Empty,
                                        DocSeries = c.DocumentSeria,
                                        DocNumber = c.DocumentNumber,
                                        DocIssue = c.DocumentSubjectIssue,
                                        DocIssueDate = c.DocumentDateOfIssue,
                                        Address = c.Address?.ToString() ?? string.Empty,
                                        School = c.School != null ? c.School.Name : string.Empty,
                                        ApplicantLastName = c.ContactLastName,
                                        ApplicantFirstName = c.ContactFirstName,
                                        ApplicantMiddleName = c.ContactMiddleName,
                                        ApplicantPhone = c.ContactPhone,
                                        TimeOfRest =
                                            list.Tour != null
                                                ? list.Tour.DateIncome.FormatEx() + " - " +
                                                  list.Tour.DateOutcome.FormatEx()
                                                : list.TimeOfRest.FormatEx() != null
                                                    ? list.TimeOfRest.Name
                                                    : string.Empty
                                    }));
                }

                if (list.Attendants != null)
                {
                    campers.AddRange(
                        list.Attendants.OrderBy(c => c.LastName).ThenBy(c => c.FirstName).ThenBy(c => c.MiddleName)
                            .Select(
                                c =>
                                    new CamperModel
                                    {
                                        Category = "Тренер / педагог",
                                        ListName = list.Name,
                                        ChildCategory = list.ListOfChildsCategory?.Name,
                                        Organization =
                                            list.LimitOnOrganization?.Organization?.Name,
                                        LastName = c.LastName,
                                        FirstName = c.FirstName,
                                        MiddleName = c.MiddleName,
                                        IsMale = c.Male,
                                        BirthDate = c.DateOfBirth,
                                        BirthPlace = c.PlaceOfBirth,
                                        DocType = c.DocumentType != null ? c.DocumentType.Name : string.Empty,
                                        DocSeries = c.DocumentSeria,
                                        DocNumber = c.DocumentNumber,
                                        DocIssue = c.DocumentSubjectIssue,
                                        DocIssueDate = c.DocumentDateOfIssue,
                                        Position = c.Position,
                                        TimeOfRest =
                                            list.Tour != null
                                                ? list.Tour.DateIncome.FormatEx() + " - " +
                                                  list.Tour.DateOutcome.FormatEx()
                                                : list.TimeOfRest.FormatEx() != null
                                                    ? list.TimeOfRest.Name
                                                    : string.Empty
                                    }));
                }
            }

            if (!campers.Any())
            {
                excel = null;
                return true;
            }

            excel =
                new ExcelTable<CamperModel>(
                    new List<ExcelColumn<CamperModel>>
                    {
                        new ExcelColumn<CamperModel>
                        {
                            Func = c => c.Category.FormatEx(false),
                            Title = "Отдыхающий",
                            WordWrap = true
                        },
                        new ExcelColumn<CamperModel>
                        {
                            Func = c => c.ListName.FormatEx(false),
                            Title = "Список",
                            WordWrap = true
                        },
                        new ExcelColumn<CamperModel>
                        {
                            Func = c => c.ChildCategory.FormatEx(false),
                            Title = "Категория детей",
                            WordWrap = true
                        },
                        new ExcelColumn<CamperModel>
                        {
                            Func = c => c.Organization.FormatEx(false),
                            Title = "Учреждение",
                            WordWrap = true
                        },
                        new ExcelColumn<CamperModel>
                        {
                            Func = c => c.TimeOfRest.FormatEx(false),
                            Title = "Время отдыха",
                            WordWrap = true
                        },
                        new ExcelColumn<CamperModel>
                        {
                            Func = c => c.LastName.FormatEx(false),
                            Title = "Фамилия",
                            WordWrap = true
                        },
                        new ExcelColumn<CamperModel>
                        {
                            Func = c => c.FirstName.FormatEx(false),
                            Title = "Имя",
                            WordWrap = true
                        },
                        new ExcelColumn<CamperModel>
                        {
                            Func = c => c.MiddleName.FormatEx(false),
                            Title = "Отчество",
                            WordWrap = true
                        },
                        new ExcelColumn<CamperModel>
                        {
                            Func =
                                c =>
                                    c.IsMale.FormatEx("-", "мужской", "женский"),
                            Title = "Пол",
                            WordWrap = true
                        },
                        new ExcelColumn<CamperModel>
                        {
                            Func = c => c.BirthDate.FormatEx(),
                            Title = "Дата рождения",
                            WordWrap = true
                        },
                        new ExcelColumn<CamperModel>
                        {
                            Func = c => c.BirthPlace.FormatEx(false),
                            Title = "Место рождения",
                            WordWrap = true
                        },
                        new ExcelColumn<CamperModel>
                        {
                            Func = c => c.DocType.FormatEx(false),
                            Title = "Документ",
                            WordWrap = true
                        },
                        new ExcelColumn<CamperModel>
                        {
                            Func = c => c.DocSeries.FormatEx(false),
                            Title = "Серия",
                            WordWrap = true
                        },
                        new ExcelColumn<CamperModel>
                        {
                            Func = c => c.DocNumber.FormatEx(false),
                            Title = "Номер",
                            WordWrap = true
                        },
                        new ExcelColumn<CamperModel>
                        {
                            Func = c => c.DocIssueDate.FormatEx(),
                            Title = "Дата выдачи",
                            WordWrap = true
                        },
                        new ExcelColumn<CamperModel>
                        {
                            Func = c => c.DocIssue.FormatEx(false),
                            Title = "Кем выдан",
                            WordWrap = true
                        },
                        new ExcelColumn<CamperModel>
                        {
                            Func = c => c.Address.FormatEx(false),
                            Title = "Адрес",
                            WordWrap = true
                        },
                        new ExcelColumn<CamperModel>
                        {
                            Func = c => c.School.FormatEx(false),
                            Title = "Школа",
                            WordWrap = true
                        },
                        new ExcelColumn<CamperModel>
                        {
                            Func = c => c.ApplicantLastName.FormatEx(false),
                            Title =
                                "Фамилия родителя (законного представителя)",
                            WordWrap = true
                        },
                        new ExcelColumn<CamperModel>
                        {
                            Func =
                                c => c.ApplicantFirstName.FormatEx(false),
                            Title =
                                "Имя родителя (законного представителя)",
                            WordWrap = true
                        },
                        new ExcelColumn<CamperModel>
                        {
                            Func =
                                c => c.ApplicantMiddleName.FormatEx(false),
                            Title =
                                "Отчество родителя (законного представителя)",
                            WordWrap = true
                        }
                    });
            excel.DataBind("Отдыхающие", campers, ExcelBorderStyle.Medium, 1);
            return false;
        }

        #endregion

        #region Формирование списка детей от города

        public FileResult GetCityChilds(long id)
        {
            var fileName = GetCityChildsFile(id);
            var outputStream = new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite, 512,
                FileOptions.DeleteOnClose | FileFlagNoBuffering);
            HttpContext.Response.AddHeader("content-disposition", "attachment; filename=childsCity.pdf");
            return new FileStreamResult(outputStream, "application/pdf");
        }

        /// <summary>
        ///     список ОИВ.
        /// </summary>
        public string GetCityChildsFile(long id)
        {
            var filename = GetTempFileName();
            using (var stream = new FileStream(filename, FileMode.OpenOrCreate))
            {
                var document = new Document(PageSize.A4, 10, 10, 10, 10);

                PdfWriter.GetInstance(document, stream);

                document.Open();
                document.Add(new Paragraph("СПИСОК", ReportHeaderFont) {Alignment = Element.ALIGN_CENTER});
                document.Add(new Paragraph("на обеспечение выездного отдыха и оздоровления детей", ReportHeaderFont)
                {
                    Alignment = Element.ALIGN_CENTER
                });

                var year = UnitOfWork.GetById<YearOfRest>(id);

                if (year != null)
                {
                    document.Add(new Paragraph("на " + year.Year + " год", ReportHeaderFont)
                    {
                        Alignment = Element.ALIGN_CENTER,
                        SpacingAfter = 10
                    });
                }

                var table = new PdfPTable(4)
                {
                    WidthPercentage = 100,
                    SpacingAfter = 10,
                    ExtendLastRow = false
                };
                table.SetWidths(new[] {1, 7, 4, 7});
                table.Rows.Add(
                    new PdfPRow(new[]
                    {
                        new PdfPCell(new Phrase("№", TableHeaderFont)) {HorizontalAlignment = Element.ALIGN_CENTER},
                        new PdfPCell(new Phrase("Фамилия, имя, отчество", TableHeaderFont))
                            {HorizontalAlignment = Element.ALIGN_CENTER},
                        new PdfPCell(new Phrase("Дата рождения", TableHeaderFont))
                            {HorizontalAlignment = Element.ALIGN_CENTER},
                        new PdfPCell(new Phrase("Документ удостоверяющий личность", TableHeaderFont))
                        {
                            HorizontalAlignment = Element.ALIGN_CENTER
                        }
                    }));

                var index = 0;

                if (year != null)
                {
                    var limits =
                        UnitOfWork.GetSet<LimitOnVedomstvo>()
                            .Where(l => l.YearOfRestId == year.Id && l.StateId.HasValue &&
                                        l.StateId != StateMachineStateEnum.Deleted)
                            .ToList();

                    foreach (var limit in limits)
                    {
                        var reportRow = new PdfPRow(new[]
                            {new PdfPCell(), new PdfPCell(), new PdfPCell(), new PdfPCell()});
                        var row = new PdfPRow(reportRow);
                        table.Rows.Add(row);
                        var cell = row.GetCells()[0];
                        cell.Colspan = 4;
                        cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cell.HorizontalAlignment = Element.ALIGN_CENTER;
                        cell.Phrase = new Phrase(limit.Organization.Name, ReportHeaderFont);

                        var count = 0;

                        foreach (
                            var orgLimit in
                            limit.VedomstvoLimit.Where(l =>
                                    l.StateId.HasValue && l.StateId != StateMachineStateEnum.Deleted)
                                .ToList())
                        {
                            var listOfChilds =
                                UnitOfWork.GetSet<ListOfChilds>()
                                    .Where(
                                        l =>
                                            !l.IsDeleted && l.IsLast && l.StateId.HasValue &&
                                            l.StateId != StateMachineStateEnum.Deleted &&
                                            l.LimitOnOrganizationId == orgLimit.Id)
                                    .ToList();

                            foreach (var list in listOfChilds)
                            {
                                reportRow = new PdfPRow(new[]
                                    {new PdfPCell(), new PdfPCell(), new PdfPCell(), new PdfPCell()});
                                row = new PdfPRow(reportRow);
                                table.Rows.Add(row);
                                cell = row.GetCells()[0];
                                cell.Colspan = 4;
                                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                                cell.Phrase =
                                    new Phrase(
                                        $"{list.LimitOnOrganization.Organization.Name}, {list.Name}, {list.ListOfChildsCategory.NullSafe(p => p.Name)}",
                                        FontBold);

                                reportRow = new PdfPRow(new[]
                                    {new PdfPCell(), new PdfPCell(), new PdfPCell(), new PdfPCell()});
                                row = new PdfPRow(reportRow);
                                table.Rows.Add(row);
                                cell = row.GetCells()[0];
                                cell.Colspan = 4;
                                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                                cell.Phrase = new Phrase("   Дети", Font);

                                foreach (var child in list.Childs)
                                {
                                    reportRow = new PdfPRow(new[]
                                        {new PdfPCell(), new PdfPCell(), new PdfPCell(), new PdfPCell()});
                                    row = new PdfPRow(reportRow);
                                    table.Rows.Add(row);
                                    var cells = row.GetCells();
                                    cells[0].Phrase = new Phrase((++index).ToString(), Font);
                                    cells[0].HorizontalAlignment = Element.ALIGN_CENTER;
                                    cells[1].Phrase = new Phrase(
                                        $"{child.LastName} {child.FirstName} {child.MiddleName}",
                                        Font);
                                    cells[2].Phrase = new Phrase(child.DateOfBirth.FormatEx(), Font);
                                    cells[3].Phrase =
                                        new Phrase(
                                            $"{child.NullSafe(c => c.DocumentType.Name).FormatEx()}, {child.DocumentSeria} {child.DocumentNumber}",
                                            Font);
                                    count++;
                                }

                                if (list.Attendants.Any())
                                {
                                    reportRow = new PdfPRow(new[]
                                        {new PdfPCell(), new PdfPCell(), new PdfPCell(), new PdfPCell()});
                                    row = new PdfPRow(reportRow);
                                    table.Rows.Add(row);
                                    cell = row.GetCells()[0];
                                    cell.Colspan = 4;
                                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                                    cell.Phrase = new Phrase("   Сопровождающие", Font);
                                }

                                foreach (var attendant in list.Attendants)
                                {
                                    reportRow = new PdfPRow(new[]
                                        {new PdfPCell(), new PdfPCell(), new PdfPCell(), new PdfPCell()});
                                    row = new PdfPRow(reportRow);
                                    table.Rows.Add(row);
                                    var cells = row.GetCells();
                                    cells[0].Phrase = new Phrase((++index).ToString(), Font);
                                    cells[0].HorizontalAlignment = Element.ALIGN_CENTER;

                                    cells[1].Phrase =
                                        new Phrase($"{attendant.LastName} {attendant.FirstName} {attendant.MiddleName}",
                                            Font);
                                    cells[2].Phrase = new Phrase(attendant.DateOfBirth.FormatEx(), Font);
                                    cells[3].Phrase =
                                        new Phrase(
                                            $"{attendant.NullSafe(c => c.DocumentType.Name).FormatEx()}, {attendant.DocumentSeria} {attendant.DocumentNumber}",
                                            Font);
                                    count++;
                                }
                            }
                        }

                        reportRow = new PdfPRow(new[] {new PdfPCell(), new PdfPCell(), new PdfPCell(), new PdfPCell()});
                        row = new PdfPRow(reportRow);
                        table.Rows.Add(row);
                        cell = row.GetCells()[0];
                        cell.Colspan = 4;
                        cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cell.Phrase = new Phrase($"Всего: {count} человек", ReportHeaderFont);
                    }
                }

                document.Add(table);
                document.Close();
            }

            return filename;
        }

        #endregion

        #region Список детей Организации

        /// <summary>
        ///     формирование списка детей от организации.
        /// </summary>
        public FileResult GetOrganizationChilds(long id)
        {
            var fileName = GetOrganizationChildsFile(id);

            var outputStream = new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite, 512,
                FileOptions.DeleteOnClose | FileFlagNoBuffering);
            HttpContext.Response.AddHeader("content-disposition", "attachment; filename=children.pdf");
            return new FileStreamResult(outputStream, "application/pdf");
        }

        /// <summary>
        ///     список ОИВ.
        /// </summary>
        public string GetOrganizationChildsFile(long id)
        {
            var filename = GetTempFileName();
            using (var stream = new FileStream(filename, FileMode.OpenOrCreate))
            {
                var document = new Document(PageSize.A4, 10, 10, 10, 10);

                PdfWriter.GetInstance(document, stream);

                document.Open();
                document.Add(new Paragraph("ЗАЯВКА", ReportHeaderFont) {Alignment = Element.ALIGN_CENTER});
                document.Add(new Paragraph("на обеспечение выездного отдыха и оздоровления детей", ReportHeaderFont)
                {
                    Alignment = Element.ALIGN_CENTER
                });

                var limit = UnitOfWork.GetById<LimitOnOrganization>(id);

                if (limit != null)
                {
                    document.Add(new Paragraph(limit.Organization.Name, ReportHeaderFont)
                    {
                        Alignment = Element.ALIGN_CENTER,
                        SpacingAfter = 10
                    });
                }

                var table = new PdfPTable(4)
                {
                    WidthPercentage = 100,
                    SpacingAfter = 10,
                    ExtendLastRow = false
                };
                table.SetWidths(new[] {1, 7, 4, 7});
                table.Rows.Add(
                    new PdfPRow(new[]
                    {
                        new PdfPCell(new Phrase("№", TableHeaderFont)) {HorizontalAlignment = Element.ALIGN_CENTER},
                        new PdfPCell(new Phrase("Фамилия, имя, отчество", TableHeaderFont))
                            {HorizontalAlignment = Element.ALIGN_CENTER},
                        new PdfPCell(new Phrase("Дата рождения", TableHeaderFont))
                            {HorizontalAlignment = Element.ALIGN_CENTER},
                        new PdfPCell(new Phrase("Документ удостоверяющий личность", TableHeaderFont))
                        {
                            HorizontalAlignment = Element.ALIGN_CENTER
                        }
                    }));

                var index = 0;

                if (limit != null)
                {
                    var listOfChildren =
                        UnitOfWork.GetSet<ListOfChilds>()
                            .Where(
                                l =>
                                    !l.IsDeleted && l.IsLast && l.StateId.HasValue &&
                                    l.StateId != StateMachineStateEnum.Deleted &&
                                    l.LimitOnOrganizationId == limit.Id)
                            .ToList();

                    foreach (var list in listOfChildren)
                    {
                        var reportRow = new PdfPRow(new[]
                            {new PdfPCell(), new PdfPCell(), new PdfPCell(), new PdfPCell()});
                        var row = new PdfPRow(reportRow);
                        table.Rows.Add(row);
                        var cell = row.GetCells()[0];
                        cell.Colspan = 4;
                        cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cell.Phrase =
                            new Phrase(
                                $"{limit.Organization.Name}, {list.Name}, {list.ListOfChildsCategory.NullSafe(p => p.Name)}",
                                FontBold);

                        reportRow = new PdfPRow(new[] {new PdfPCell(), new PdfPCell(), new PdfPCell(), new PdfPCell()});
                        row = new PdfPRow(reportRow);
                        table.Rows.Add(row);
                        cell = row.GetCells()[0];
                        cell.Colspan = 4;
                        cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cell.Phrase = new Phrase("   Дети", Font);

                        foreach (var child in list.Childs)
                        {
                            reportRow = new PdfPRow(new[]
                                {new PdfPCell(), new PdfPCell(), new PdfPCell(), new PdfPCell()});
                            row = new PdfPRow(reportRow);
                            table.Rows.Add(row);
                            var cells = row.GetCells();
                            cells[0].Phrase = new Phrase((++index).ToString(), Font);
                            cells[0].HorizontalAlignment = Element.ALIGN_CENTER;
                            cells[1].Phrase = new Phrase($"{child.LastName} {child.FirstName} {child.MiddleName}",
                                Font);
                            cells[2].Phrase = new Phrase(child.DateOfBirth.FormatEx(), Font);
                            cells[3].Phrase =
                                new Phrase(
                                    $"{child.NullSafe(c => c.DocumentType.Name).FormatEx()}, {child.DocumentSeria} {child.DocumentNumber}",
                                    Font);
                        }

                        if (list.Attendants.Any())
                        {
                            reportRow = new PdfPRow(new[]
                                {new PdfPCell(), new PdfPCell(), new PdfPCell(), new PdfPCell()});
                            row = new PdfPRow(reportRow);
                            table.Rows.Add(row);
                            cell = row.GetCells()[0];
                            cell.Colspan = 4;
                            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cell.Phrase = new Phrase("   Сопровождающие", Font);
                        }

                        foreach (var attendant in list.Attendants)
                        {
                            reportRow = new PdfPRow(new[]
                                {new PdfPCell(), new PdfPCell(), new PdfPCell(), new PdfPCell()});
                            row = new PdfPRow(reportRow);
                            table.Rows.Add(row);
                            var cells = row.GetCells();
                            cells[0].Phrase = new Phrase((++index).ToString(), Font);
                            cells[0].HorizontalAlignment = Element.ALIGN_CENTER;

                            cells[1].Phrase =
                                new Phrase($"{attendant.LastName} {attendant.FirstName} {attendant.MiddleName}",
                                    Font);
                            cells[2].Phrase = new Phrase(attendant.DateOfBirth.FormatEx(), Font);
                            cells[3].Phrase =
                                new Phrase(
                                    $"{attendant.NullSafe(c => c.DocumentType.Name).FormatEx()}, {attendant.DocumentSeria} {attendant.DocumentNumber}",
                                    Font);
                        }
                    }
                }

                document.Add(table);
                document.Close();
            }

            return filename;
        }

        #endregion

        #region Список детей Организации

        /// <summary>
        ///     формирование списка детей от организации.
        /// </summary>
        public FileResult GetListChilds(long id)
        {
            var fileName = GetListChildsFile(id);

            var outputStream = new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite, 512,
                FileOptions.DeleteOnClose | FileFlagNoBuffering);
            HttpContext.Response.AddHeader("content-disposition", "attachment; filename=children.pdf");
            return new FileStreamResult(outputStream, "application/pdf");
        }

        /// <summary>
        ///     список ОИВ.
        /// </summary>
        public string GetListChildsFile(long id)
        {
            var filename = GetTempFileName();
            using (var stream = new FileStream(filename, FileMode.OpenOrCreate))
            {
                var document = new Document(PageSize.A4, 10, 10, 10, 10);

                PdfWriter.GetInstance(document, stream);

                document.Open();
                document.Add(new Paragraph("ЗАЯВКА", ReportHeaderFont) {Alignment = Element.ALIGN_CENTER});
                document.Add(new Paragraph("на обеспечение выездного отдыха и оздоровления детей", ReportHeaderFont)
                {
                    Alignment = Element.ALIGN_CENTER
                });

                var list = UnitOfWork.GetById<ListOfChilds>(id);

                if (list != null)
                {
                    document.Add(new Paragraph(list.LimitOnOrganization.Organization.Name, ReportHeaderFont)
                    {
                        Alignment = Element.ALIGN_CENTER,
                        SpacingAfter = 10
                    });


                    var table = new PdfPTable(4)
                    {
                        WidthPercentage = 100,
                        SpacingAfter = 10,
                        ExtendLastRow = false
                    };
                    table.SetWidths(new[] {1, 7, 4, 7});
                    table.Rows.Add(
                        new PdfPRow(new[]
                        {
                            new PdfPCell(new Phrase("№", TableHeaderFont)) {HorizontalAlignment = Element.ALIGN_CENTER},
                            new PdfPCell(new Phrase("Фамилия, имя, отчество", TableHeaderFont))
                                {HorizontalAlignment = Element.ALIGN_CENTER},
                            new PdfPCell(new Phrase("Дата рождения", TableHeaderFont))
                                {HorizontalAlignment = Element.ALIGN_CENTER},
                            new PdfPCell(new Phrase("Документ удостоверяющий личность", TableHeaderFont))
                            {
                                HorizontalAlignment = Element.ALIGN_CENTER
                            }
                        }));

                    var index = 0;


                    var reportRow = new PdfPRow(new[] {new PdfPCell(), new PdfPCell(), new PdfPCell(), new PdfPCell()});
                    var row = new PdfPRow(reportRow);
                    table.Rows.Add(row);
                    var cell = row.GetCells()[0];
                    cell.Colspan = 4;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Phrase =
                        new Phrase(
                            $"{list.LimitOnOrganization.Organization.Name}, {list.Name}, {list.ListOfChildsCategory.NullSafe(p => p.Name)}",
                            FontBold);

                    reportRow = new PdfPRow(new[] {new PdfPCell(), new PdfPCell(), new PdfPCell(), new PdfPCell()});
                    row = new PdfPRow(reportRow);
                    table.Rows.Add(row);
                    cell = row.GetCells()[0];
                    cell.Colspan = 4;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Phrase = new Phrase("   Дети", Font);

                    foreach (var child in list.Childs)
                    {
                        reportRow = new PdfPRow(new[] {new PdfPCell(), new PdfPCell(), new PdfPCell(), new PdfPCell()});
                        row = new PdfPRow(reportRow);
                        table.Rows.Add(row);
                        var cells = row.GetCells();
                        cells[0].Phrase = new Phrase((++index).ToString(), Font);
                        cells[0].HorizontalAlignment = Element.ALIGN_CENTER;
                        cells[1].Phrase = new Phrase($"{child.LastName} {child.FirstName} {child.MiddleName}",
                            Font);
                        cells[2].Phrase = new Phrase(child.DateOfBirth.FormatEx(), Font);
                        cells[3].Phrase =
                            new Phrase(
                                $"{child.NullSafe(c => c.DocumentType.Name).FormatEx()}, {child.DocumentSeria} {child.DocumentNumber}",
                                Font);
                    }

                    if (list.Attendants.Any())
                    {
                        reportRow = new PdfPRow(new[] {new PdfPCell(), new PdfPCell(), new PdfPCell(), new PdfPCell()});
                        row = new PdfPRow(reportRow);
                        table.Rows.Add(row);
                        cell = row.GetCells()[0];
                        cell.Colspan = 4;
                        cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cell.Phrase = new Phrase("   Сопровождающие", Font);
                    }

                    foreach (var attendant in list.Attendants)
                    {
                        reportRow = new PdfPRow(new[] {new PdfPCell(), new PdfPCell(), new PdfPCell(), new PdfPCell()});
                        row = new PdfPRow(reportRow);
                        table.Rows.Add(row);
                        var cells = row.GetCells();
                        cells[0].Phrase = new Phrase((++index).ToString(), Font);
                        cells[0].HorizontalAlignment = Element.ALIGN_CENTER;

                        cells[1].Phrase =
                            new Phrase($"{attendant.LastName} {attendant.FirstName} {attendant.MiddleName}",
                                Font);
                        cells[2].Phrase = new Phrase(attendant.DateOfBirth.FormatEx(), Font);
                        cells[3].Phrase =
                            new Phrase(
                                $"{attendant.NullSafe(c => c.DocumentType.Name).FormatEx()}, {attendant.DocumentSeria} {attendant.DocumentNumber}",
                                Font);
                    }


                    document.Add(table);
                }

                document.Close();
            }

            return filename;
        }

        #endregion
    }
}
