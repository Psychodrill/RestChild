using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using iTextSharp.text;
using iTextSharp.text.pdf;
using MimeTypes;
using RestChild.Comon;
using RestChild.Comon.Enumeration;
using RestChild.DAL.RepositoryExtensions;
using RestChild.DocumentGeneration.Properties;
using RestChild.Domain;
using Slepov.Russian.Morpher;

namespace RestChild.DocumentGeneration.PDFDocuments
{
    public static partial class PdfProcessor
    {
        private const string Extension = ".pdf";
        private static readonly string MimeType = MimeTypeMap.GetMimeType(Extension);


        private static string arialPath = Environment.GetEnvironmentVariable("SystemRoot") + "\\fonts\\arial.ttf";
        private static string acromPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "AcromLight.otf");
        private  static string acromOtherPath = Path.Combine(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin"), "AcromLight.otf");

        private static BaseFont customFont = File.Exists(acromOtherPath) ? BaseFont.CreateFont(acromOtherPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED)
                                                                         : File.Exists(acromPath)
                                                                         ? BaseFont.CreateFont(acromPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED)
                                                                         : BaseFont.CreateFont(arialPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);

        private static Font font8 = new Font(customFont, 8);
        private static Font font10= new Font(customFont, 10);
        private static Font font12 = new Font(customFont, 12);
        private static Font font14 = new Font(customFont, 14);
        
        /// <summary>
        ///     Сертификаты, основная развилка
        /// </summary>
        public static IDocument CertificateForRequestTemporaryFile(IUnitOfWork unitOfWork, long requestId,
            long? sendStatusId = null)
        {
            var request = unitOfWork.GetById<Request>(requestId);
            if (request.TypeOfRestId != (long) TypeOfRestEnum.YouthRestOrphanCamps &&
                request.TypeOfRestId != (long) TypeOfRestEnum.MoneyOn18 ||
                request.StatusId != (long) StatusEnum.CertificateIssued &&
                sendStatusId != (long) StatusEnum.CertificateIssued)
            {
                if (request.Child == null || request.Child.All(c => c.IsDeleted) ||
                    request.StatusId != (long) StatusEnum.CertificateIssued &&
                    sendStatusId != (long) StatusEnum.CertificateIssued)
                {
                    return null;
                }
            }

            byte[] result;
            string fileName;

            using (var memoryStream = new MemoryStream())
            {
                var multiTypeRequest = request.Child.Count(c => !c.IsDeleted) > 1;
                
                if (request.RequestOnMoney)
                {
                    if (!multiTypeRequest)
                    {
                        fileName  =CertificateOnMoney(request, memoryStream);
                    }
                    else
                    {
                        fileName = CertificateOnMoneyMulti(request, memoryStream);
                    }
                }
                else
                {
                    if (!multiTypeRequest && request.CountAttendants <= 1)
                    {
                        fileName = CertificateOnRestSingle(request, memoryStream);
                    }
                    else
                    {
                        fileName = CertificateOnRestMulti(request, memoryStream);
                    }
                }

                result = memoryStream.ToArray();
            }

            return new DocumentResult
            {
                FileName = fileName,
                FileBody = result,
                MimeTypeShort = Extension,
                MimeType = MimeType
            };
        }

        /// <summary>
        ///     сертификат на отдых 1 ребенок
        /// </summary>
        private static string CertificateOnMoney(Request request, Stream newStream)
        {
            var applicant = request.Applicant ?? new Applicant {DocumentType = new DocumentType {Name = string.Empty}};

            var assembly = Assembly.Load("RestChild.Templates");

            var requestChildren = request.Child.Where(c => !c.IsDeleted).ToList();

            using (var templateStream = new MemoryStream())
            {
                using (var streamTemplate = assembly.GetManifestResourceStream("RestChild.Templates.singlePayment2021.pdf"))
                {
                    using (var readerTemplate = new PdfReader(streamTemplate))
                    {
                        using (var doc = new Document())
                        {
                            using (var copy = new PdfCopy(doc, templateStream))
                            {
                                doc.Open();
                                var childrenCount = requestChildren.Count;
                                while (childrenCount > 0)
                                {
                                    copy.AddPage(copy.GetImportedPage(readerTemplate, 1));
                                    childrenCount--;
                                }

                                if (request.TypeOfRestId == (long) TypeOfRestEnum.MoneyOn18)
                                {
                                    copy.AddPage(copy.GetImportedPage(readerTemplate, 1));
                                }

                                copy.AddPage(copy.GetImportedPage(readerTemplate, 2));
                                copy.CloseStream = false;
                                doc.Close();
                            }
                        }
                    }

                    templateStream.Seek(0, SeekOrigin.Begin);
                    using (var readerTemplate = new PdfReader(templateStream))
                    {
                        using (var pdfStamper = new PdfStamper(readerTemplate, newStream, '1'))
                        {
                            var page = 2;
                            if (request.TypeOfRestId == (long) TypeOfRestEnum.MoneyOn18)
                            {
                                var over = pdfStamper.GetOverContent(page);
                                over.BeginText();
                                over.SetFontAndSize(customFont, 14);
                                over.ShowTextAligned(Element.ALIGN_LEFT, request.CertificateDate?.ToLongDateString()?? request.DateChangeStatus?.ToLongDateString() ?? DateTime.Now.ToLongDateString(), 280, 458, 0);
                                over.ShowTextAligned(Element.ALIGN_LEFT, request.CertificateNumber.FormatEx(), 475, 458, 0);
                                over.SetFontAndSize(customFont, 12);

                                WriteByTable(over, font12, 270, 400, 500, $"{applicant.LastName}  {applicant.FirstName}  {applicant.MiddleName}".Trim());

                                WriteByTable(over, font12, 40, 366, 700, $"{applicant.DateOfBirth.FormatEx()},  {applicant.DocumentType.Name},  {applicant.DocumentSeria},  {applicant.DocumentNumber}".Trim(),1);

                                WriteByTable(over, font12, 270, 243, 500, $"{request.RequestNumber}, {applicant.LastName} {applicant.FirstName} {applicant.MiddleName}, {applicant.DocumentType.Name}, {applicant.DocumentSeria} {applicant.DocumentNumber}".Trim());
                                over.EndText();
                            }
                            else
                            {
                                foreach (var child in requestChildren)
                                {
                                    var over = pdfStamper.GetOverContent(page);
                                    page++;
                                    over.BeginText();
                                    over.SetFontAndSize(customFont, 14);
                                    over.ShowTextAligned(Element.ALIGN_LEFT, request.CertificateDate?.ToLongDateString() ?? request.DateChangeStatus?.ToLongDateString() ?? DateTime.Now.ToLongDateString(), 280, 458, 0);
                                    over.ShowTextAligned(Element.ALIGN_LEFT, request.CertificateNumber.FormatEx(), 475, 458, 0);

                                    WriteByTable(over, font12, 270, 400, 500, $"{child.LastName}  {child.FirstName}  {child.MiddleName}".Trim());

                                    WriteByTable(over, font12, 40, 366, 700, $"{child.DateOfBirth.FormatEx()},  {child.DocumentType.Name},  {child.DocumentSeria},  {child.DocumentNumber}".Trim(),1);

                                    if (applicant.IsAccomp || request.Attendant.Any(a => a.IsAccomp && !a.IsDeleted))
                                    {
                                        var attendant =
                                            (applicant.IsAccomp && !applicant.IsDeleted
                                                ? applicant
                                                : request.Attendant.FirstOrDefault(a => a.IsAccomp && !a.IsDeleted)) ??
                                            new Applicant { DocumentType = new DocumentType { Name = string.Empty } };

                                        WriteByTable(over, font12, 360, 333, 400, $"{attendant.LastName} {attendant.FirstName} {attendant.MiddleName}".Trim());

                                        WriteByTable(over, font12, 40, 293, 700, $"{attendant.DateOfBirth.FormatEx()}, {attendant.DocumentType.Name}, {attendant.DocumentSeria}, {attendant.DocumentNumber}".Trim(),1);

                                    }

                                    WriteByTable(over, font12, 270, 243, 500, $"{request.RequestNumber}, {applicant.LastName} {applicant.FirstName} {applicant.MiddleName}, {applicant.DocumentType.Name}, {applicant.DocumentSeria} {applicant.DocumentNumber}".Trim());

                                    over.EndText();
                                }
                            }
                        }
                    }
                }
            }
            return "Сертификат.pdf";
        }

        /// <summary>
        ///     сертификат на отдых много детей
        /// </summary>
        private static string CertificateOnMoneyMulti(Request request, Stream newStream)
        {
            var applicant = request.Applicant ?? new Applicant {DocumentType = new DocumentType {Name = string.Empty}};

            var assembly = Assembly.Load("RestChild.Templates");

            using (var templateStream = new MemoryStream())
            {
                using (var streamTemplate = assembly.GetManifestResourceStream("RestChild.Templates.multiPayment.pdf"))
                {
                    streamTemplate?.CopyTo(templateStream);
                    templateStream.Seek(0, SeekOrigin.Begin);

                    using (var readerTemplate = new PdfReader(templateStream))
                    {
                        using (var pdfStamper = new PdfStamper(readerTemplate, newStream, '1'))
                        {
                            var over = pdfStamper.GetOverContent(1);
                            over.BeginText();
                            over.SetFontAndSize(customFont, 12);
                            over.ShowTextAligned(Element.ALIGN_LEFT, request.CertificateDate?.ToShortDateString() ?? request.DateChangeStatus?.ToShortDateString() ?? DateTime.Now.ToShortDateString()/*GetDayMonth(request.CertificateDate)*/, 590, 495, 0);
                            over.ShowTextAligned(Element.ALIGN_LEFT, request.CertificateNumber.FormatEx(), 686, 495, 0);
                            over.SetFontAndSize(customFont, 10);

                            float row = 470;

                            var requestChildren = request.Child.Where(c => !c.IsDeleted).ToList();

                            bool isFirstString = true;
                            foreach (var child in requestChildren)
                            {
                                
                                over.SetFontAndSize(customFont, 12);
                                over.ShowTextAligned(Element.ALIGN_LEFT,
                                    $"{child.LastName} {child.FirstName} {child.MiddleName}", 180, row, 0);

                                WriteByTable(over, font12, 500, row, 230,
                                    $"{child.DateOfBirth.FormatEx()}, {child.DocumentSeria} {child.DocumentNumber}",
                                    Element.ALIGN_CENTER);
                                if (isFirstString)
                                {
                                    row = row - 24;
                                    isFirstString = false;
                                }
                                else
                                {
                                    row = row - 20.3f;
                                }


                            }
                            over.SetFontAndSize(customFont, 12);

                            WriteByTable(over, font12, 290, 145, 500, $"{request.RequestNumber}, {applicant.LastName} {applicant.FirstName} {applicant.MiddleName}, {applicant.DocumentType.Name}, {applicant.DocumentSeria} {applicant.DocumentNumber}".Trim());
                            over.EndText();

                            over = pdfStamper.GetOverContent(2);
                            over.BeginText();

                            over.SetFontAndSize(customFont, 12);
                            over.ShowTextAligned(Element.ALIGN_LEFT, request.CertificateDate?.ToShortDateString() ?? request.DateChangeStatus?.ToShortDateString() ?? DateTime.Now.ToShortDateString()/*GetDayMonth(request.CertificateDate)*/, 590, 495, 0);
                            over.ShowTextAligned(Element.ALIGN_LEFT, request.CertificateNumber.FormatEx(), 686, 495, 0);
                            over.SetFontAndSize(customFont, 10);

                            row = 456;
                            isFirstString = true;
                            var attendants = request.Attendant.Where(c => !c.IsDeleted).ToList();

                            foreach (var attendant in attendants)
                            {

                                over.SetFontAndSize(customFont, 12);
                                over.ShowTextAligned(Element.ALIGN_LEFT,
                                    $"{attendant.LastName} {attendant.FirstName} {attendant.MiddleName}", 200, row, 0);

                                WriteByTable(over, font12, 500, row, 230,
                                    $"{attendant.DateOfBirth.FormatEx()}, {attendant.DocumentSeria} {attendant.DocumentNumber}",
                                    Element.ALIGN_CENTER);
                                if (isFirstString)
                                {
                                    row = row - 27;
                                    isFirstString = false;
                                }
                                else
                                {
                                    row = row - 20.3f;
                                }

                            }

                            over.EndText();
                        }
                    }
                }
            }
            return "Сертификат.pdf";
        }

        /// <summary>
        ///     сертификат на отдых 1 ребенок
        /// </summary>
        private static string  CertificateOnRestSingle(Request request, Stream newStream)
        {
            var applicant = request.Applicant ?? new Applicant {DocumentType = new DocumentType {Name = string.Empty}};

            var assembly = Assembly.Load("RestChild.Templates");

            var requestChildren = request.Child.Where(c => !c.IsDeleted).ToList();

            using (var stream = assembly.GetManifestResourceStream("RestChild.Templates.single2021.pdf"))
            {
                using (var reader = new PdfReader(stream))
                {
                    using (var pdfStamper = new PdfStamper(reader, newStream, '1'))
                    {
                        var over = pdfStamper.GetOverContent(1);

                        var child = requestChildren.FirstOrDefault() ?? new Child
                            {DocumentType = new DocumentType {Name = string.Empty}};

                        if (request.TypeOfRestId == (long) TypeOfRestEnum.YouthRestOrphanCamps)
                        {
                            child = new Child
                            {
                                DocumentType =
                                    request.Applicant?.DocumentType ?? new DocumentType {Name = string.Empty},
                                LastName = request.Applicant?.LastName,
                                FirstName = request.Applicant?.FirstName,
                                MiddleName = request.Applicant?.MiddleName,
                                DocumentSeria = request.Applicant?.DocumentSeria,
                                DocumentNumber = request.Applicant?.DocumentNumber,
                                DateOfBirth = request.Applicant?.DateOfBirth
                            };
                        }

                        var y_delta = 38;

                        over.BeginText();
                        over.SetFontAndSize(customFont, 14);
                        over.ShowTextAligned(Element.ALIGN_LEFT, request.CertificateDate?.ToLongDateString()?? request.DateChangeStatus?.ToLongDateString() ?? DateTime.Now.ToLongDateString(), 275, 404 + y_delta, 0);
                        over.ShowTextAligned(Element.ALIGN_LEFT, request.CertificateNumber.FormatEx(), 470, 404 + y_delta, 0);

                        var text = $"{child.LastName} {child.FirstName} {child.MiddleName}".Trim();
                        WriteByTable(over, text.Length > 108 ? font10 : font12, 280, 365 + y_delta, 500, text);
                        over.ShowTextAligned(Element.ALIGN_LEFT, $"{child.DateOfBirth.FormatEx()}, {child.DocumentType.Name}, {child.DocumentSeria} {child.DocumentNumber}", 100, 334 + y_delta, 0);

                        if (request.TypeOfRestId != (long) TypeOfRestEnum.YouthRestOrphanCamps && (request.TypeOfRest?.NeedAccomodation ?? false))
                        {
                            var attendant =
                                (applicant.IsAccomp && !applicant.IsDeleted
                                    ? applicant
                                    : request.Attendant.FirstOrDefault(a => a.IsAccomp && !a.IsDeleted)) ??
                                new Applicant {DocumentType = new DocumentType {Name = string.Empty}};
                            var text_name = $"{attendant.LastName} {attendant.FirstName} {attendant.MiddleName}".Trim();
                            var text_docs = $"{attendant.DateOfBirth.FormatEx()}, {attendant.DocumentType.Name}, {attendant.DocumentSeria}, {attendant.DocumentNumber}";

                            WriteByTable(over, text.Length > 108 ? font10 : font12, 400, 295 + y_delta, 500, text_name);
                            WriteByTable(over, text.Length > 108 ? font10 : font12, 100, 258 + y_delta, 500, text_docs);
                        }

                        over.ShowTextAligned(Element.ALIGN_LEFT, request.NullSafe(r => r.Tour.DateIncome.Value.ToLongDateString()) ?? string.Empty, 180, 229 + y_delta, 0);

                        over.ShowTextAligned(Element.ALIGN_LEFT, request.NullSafe(r => r.Tour.DateOutcome.Value.ToLongDateString()) ?? string.Empty, 300, 229 + y_delta, 0);

                        over.ShowTextAligned(Element.ALIGN_LEFT, request.NullSafe(r => r.TransferTo.Name) ?? string.Empty, 300, 155 + y_delta, 0);

                        over.ShowTextAligned(Element.ALIGN_LEFT, request.NullSafe(r => r.TransferFrom.Name) ?? string.Empty, 300, 131 + y_delta, 0);

                        if (applicant.DocumentType != null)
                        {
                            text = $"{request.RequestNumber}, {$"{applicant.LastName} {applicant.FirstName} {applicant.MiddleName}".Trim()}, {applicant.DocumentType.Name}, {applicant.DocumentSeria} {applicant.DocumentNumber}";
                            WriteByTable(over, text.Length > 79 ? font10 : font12, 260, 106 + y_delta, 550, text);
                        }

                        over.EndText();

                        if (request.Tour != null)
                        {
                            var headerTable = new PdfPTable(1) {TotalWidth = 600};
                            var p = new Phrase(new Chunk(request.NullSafe(r => r.Tour.Hotels.Name),
                                new Font(customFont, 12)));
                            var company = new PdfPCell(p);
                            company.SetLeading(11, 0);
                            company.HorizontalAlignment = Element.ALIGN_LEFT;
                            company.VerticalAlignment = Element.ALIGN_BOTTOM;
                            company.BorderWidth = 0;
                            headerTable.AddCell(company);
                            headerTable.WriteSelectedRows(0, 1, 0, 1, 190, 190 + headerTable.TotalHeight + y_delta, over);
                        }
                    }
                }
            }
            return "Путёвка.pdf";
        }

        /// <summary>
        ///     сертификат на отдых множественный
        /// </summary>
        private static string CertificateOnRestMulti(Request request, Stream newStream)
        {

            var assembly = Assembly.Load("RestChild.Templates");

            using (var stream = assembly.GetManifestResourceStream("RestChild.Templates.multi2021.pdf"))
            {
                using (var reader = new PdfReader(stream))
                {
                    using (var pdfStamper = new PdfStamper(reader, newStream, '1'))
                    {
                        var over = pdfStamper.GetOverContent(1);

                        var applicant = request.Applicant ?? new Applicant
                            {DocumentType = new DocumentType {Name = string.Empty}};

                        var headerTextSize = 12;

                        over.BeginText();
                        over.SetFontAndSize(customFont, headerTextSize);
                        over.ShowTextAligned(Element.ALIGN_LEFT, request.CertificateDate?.ToShortDateString() ?? request.DateChangeStatus?.ToShortDateString() ?? DateTime.Now.ToShortDateString(), 565, 493, 0);
                        over.SetFontAndSize(customFont, headerTextSize);
                        over.ShowTextAligned(Element.ALIGN_LEFT, request.CertificateNumber.FormatEx(), 694, 493, 0);


                        float row = 466;
                        bool isFirstString = true;
                        var requestChildren = request.Child.Where(c => !c.IsDeleted).ToList();
                        foreach (var child in requestChildren)
                        {

                            over.SetFontAndSize(customFont, 12);
                            over.ShowTextAligned(Element.ALIGN_LEFT,
                                $"{child.LastName} {child.FirstName} {child.MiddleName}", 180, row, 0);

                            WriteByTable(over, font12, 500, row, 230,
                                $"{child.DateOfBirth.FormatEx()}, {child.DocumentSeria} {child.DocumentNumber}",
                                Element.ALIGN_CENTER);
                            if (isFirstString)
                            {
                                row = row - 24;
                                isFirstString = false;
                            }
                            else
                            {
                                row = row - 20.3f;
                            }
                        }

                        over.SetFontAndSize(customFont, 12);
                        over.ShowTextAligned(Element.ALIGN_LEFT,request.Tour?.DateIncome.Value.ToShortDateString(), 165, 140, 0);

                        over.ShowTextAligned(Element.ALIGN_LEFT,request.Tour?.DateOutcome.Value.ToShortDateString(), 270,140, 0);


                        var text = request.Tour?.Hotels?.Name ?? string.Empty;
                        WriteByTable(over, font12, 480, 140, 500, text);

                        over.ShowTextAligned(Element.ALIGN_LEFT, request.TransferTo?.Name ?? string.Empty, 285, 100, 0);

                        over.ShowTextAligned(Element.ALIGN_LEFT, request.TransferFrom?.Name ?? string.Empty, 285, 75, 0);

                        text =
                            $"{request.RequestNumber}, {$"{applicant.LastName} {applicant.FirstName} {applicant.MiddleName}".Trim()}, {applicant.DocumentType?.Name} {applicant.DocumentSeria} {applicant.DocumentNumber}";
                        WriteByTable(over, text.Length > 79 ? font10 : font12, 220, 50,550, text, Element.ALIGN_CENTER);

                        over.EndText();

                        // ---------------------------------------------------
                        // вторая страница
                        over = pdfStamper.GetOverContent(2);
                        over.BeginText();
                        over.SetFontAndSize(customFont, headerTextSize);
                        over.ShowTextAligned(Element.ALIGN_LEFT, request.CertificateDate?.ToShortDateString() ?? request.DateChangeStatus?.ToShortDateString() ?? DateTime.Now.ToShortDateString(), 570, 490, 0);
                        over.SetFontAndSize(customFont, headerTextSize);
                        over.ShowTextAligned(Element.ALIGN_LEFT, request.CertificateNumber.FormatEx(), 692, 490, 0);

                        row = 452;
                        isFirstString = true;
                        var attendants = request.Attendant.Where(c => !c.IsDeleted).ToList();

                        foreach (var attendant in attendants)
                        {

                            over.SetFontAndSize(customFont, 12);
                            over.ShowTextAligned(Element.ALIGN_LEFT,
                                $"{attendant.LastName} {attendant.FirstName} {attendant.MiddleName}", 200, row, 0);

                            WriteByTable(over, font12, 500, row, 230,
                                $"{attendant.DateOfBirth.FormatEx()}, {attendant.DocumentSeria} {attendant.DocumentNumber}",
                                Element.ALIGN_CENTER);
                            if (isFirstString)
                            {
                                row = row - 27;
                                isFirstString = false;
                            }
                            else
                            {
                                row = row - 20.3f;
                            }

                        }

                        over.EndText();
                    }
                }
            }
            return "Путёвка.pdf";
        }

        private static void WriteByTable(PdfContentByte over, Font font, float posX, float posY, float width,
            string value, int horizontalAlignment = Element.ALIGN_LEFT)
        {
            over.EndText();
            var table = new PdfPTable(1) {TotalWidth = width};
            var p = new Phrase(new Chunk(value, font));
            var company = new PdfPCell(p)
            {
                HorizontalAlignment = horizontalAlignment,
                VerticalAlignment = Element.ALIGN_TOP,
                BorderWidth = 0
            };

            table.AddCell(company);
            table.WriteSelectedRows(0, 1, 0, 1, posX, posY + table.TotalHeight, over);
            over.BeginText();
        }
    }
}
