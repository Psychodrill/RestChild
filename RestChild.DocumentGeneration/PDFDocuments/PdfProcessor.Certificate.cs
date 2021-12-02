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

            using (var memoryStream = new MemoryStream())
            {
                var multiTypeRequest = request.Child.Count(c => !c.IsDeleted) > 1;
                if (request.RequestOnMoney)
                {
                    if (!multiTypeRequest)
                    {
                        CertificateOnMoney(request, memoryStream);
                    }
                    else
                    {
                        CertificateOnMoneyMulti(request, memoryStream);
                    }
                }
                else
                {
                    if (!multiTypeRequest && request.CountAttendants <= 1)
                    {
                        CertificateOnRestSingle(request, memoryStream);
                    }
                    else
                    {
                        CertificateOnRestMulti(request, memoryStream);
                    }
                }

                result = memoryStream.ToArray();
            }

            return new DocumentResult
            {
                FileName = "Сертификат.pdf",
                FileBody = result,
                MimeTypeShort = Extension,
                MimeType = MimeType
            };
        }

        /// <summary>
        ///     сертификат на отдых 1 ребенок
        /// </summary>
        private static void CertificateOnMoney(Request request, Stream newStream)
        {
            var applicant = request.Applicant ?? new Applicant {DocumentType = new DocumentType {Name = string.Empty}};

            var arialPath = Environment.GetEnvironmentVariable("SystemRoot") + "\\fonts\\arial.ttf";
            var acromPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "AcromLight.otf");
            var acromOtherPath = Path.Combine(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin"), "AcromLight.otf");

            var customFont = File.Exists(acromOtherPath)
                ? BaseFont.CreateFont(acromOtherPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED)
                : File.Exists(acromPath)
                    ? BaseFont.CreateFont(acromPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED)
                    : BaseFont.CreateFont(arialPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);


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

                    var font = new Font(customFont, 12);

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
                                over.ShowTextAligned(Element.ALIGN_LEFT, GetDayMonth(request.CertificateDate), 280, 458, 0);
                                over.ShowTextAligned(Element.ALIGN_LEFT, request.CertificateNumber.FormatEx(), 475, 458, 0);
                                over.SetFontAndSize(customFont, 12);

                                WriteByTable(over, font, 270, 400, 500, $"{applicant.LastName}  {applicant.FirstName}  {applicant.MiddleName}".Trim());

                                WriteByTable(over, font, 250, 366, 500, $"{applicant.DateOfBirth.FormatEx()},  {applicant.DocumentType.Name},  {applicant.DocumentSeria},  {applicant.DocumentNumber}".Trim());

                                WriteByTable(over, font, 270, 243, 500, $"{request.RequestNumber}, {applicant.LastName} {applicant.FirstName} {applicant.MiddleName}, {applicant.DocumentType.Name}, {applicant.DocumentSeria} {applicant.DocumentNumber}".Trim());
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
                                    over.ShowTextAligned(Element.ALIGN_LEFT, GetDayMonth(request.CertificateDate), 280, 458, 0);
                                    over.ShowTextAligned(Element.ALIGN_LEFT, request.CertificateNumber.FormatEx(), 475, 458, 0);
                                    over.SetFontAndSize(customFont, 12);

                                    WriteByTable(over, font, 270, 400, 500, $"{child.LastName}  {child.FirstName}  {child.MiddleName}".Trim());

                                    WriteByTable(over, font, 250, 366, 500, $"{child.DateOfBirth.FormatEx()},  {child.DocumentType.Name},  {child.DocumentSeria},  {child.DocumentNumber}".Trim());

                                    if (applicant.IsAccomp || request.Attendant.Any(a => a.IsAccomp && !a.IsDeleted))
                                    {
                                        var attendant =
                                            (applicant.IsAccomp && !applicant.IsDeleted
                                                ? applicant
                                                : request.Attendant.FirstOrDefault(a => a.IsAccomp && !a.IsDeleted)) ??
                                            new Applicant { DocumentType = new DocumentType { Name = string.Empty } };

                                        WriteByTable(over, font, 360, 333, 500, $"{attendant.LastName} {attendant.FirstName} {attendant.MiddleName}".Trim());

                                        WriteByTable(over, font, 250, 293, 500, $"{attendant.DateOfBirth.FormatEx()}, {attendant.DocumentType.Name}, {attendant.DocumentSeria}, {attendant.DocumentNumber}".Trim());

                                    }

                                    WriteByTable(over, font, 270, 243, 500, $"{request.RequestNumber}, {applicant.LastName} {applicant.FirstName} {applicant.MiddleName}, {applicant.DocumentType.Name}, {applicant.DocumentSeria} {applicant.DocumentNumber}".Trim());

                                    over.EndText();
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        ///     сертификат на отдых много детей
        /// </summary>
        private static void CertificateOnMoneyMulti(Request request, Stream newStream)
        {
            var applicant = request.Applicant ?? new Applicant {DocumentType = new DocumentType {Name = string.Empty}};

            var arialPath = Environment.GetEnvironmentVariable("SystemRoot") + "\\fonts\\arial.ttf";
            var acromPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "AcromLight.otf");
            var acromOtherPath = Path.Combine(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin"), "AcromLight.otf");

            var customFont = File.Exists(acromOtherPath)
                ? BaseFont.CreateFont(acromOtherPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED)
                : File.Exists(acromPath)
                    ? BaseFont.CreateFont(acromPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED)
                    : BaseFont.CreateFont(arialPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);


            var assembly = Assembly.Load("RestChild.Templates");

            using (var templateStream = new MemoryStream())
            {
                using (var streamTemplate = assembly.GetManifestResourceStream("RestChild.Templates.multiPayment.pdf"))
                {
                    streamTemplate?.CopyTo(templateStream);
                    templateStream.Seek(0, SeekOrigin.Begin);

                    var font = new Font(customFont, 10);

                    using (var readerTemplate = new PdfReader(templateStream))
                    {
                        using (var pdfStamper = new PdfStamper(readerTemplate, newStream, '1'))
                        {
                            var over = pdfStamper.GetOverContent(1);
                            over.BeginText();
                            over.SetFontAndSize(customFont, 12);
                            over.ShowTextAligned(Element.ALIGN_LEFT, request.CertificateDate.Value.ToShortDateString()/*GetDayMonth(request.CertificateDate)*/, 590, 495, 0);
                            over.ShowTextAligned(Element.ALIGN_LEFT, request.CertificateNumber.FormatEx(), 690, 495, 0);
                            over.SetFontAndSize(customFont, 10);

                            float row = 470;

                            var requestChildren = request.Child.Where(c => !c.IsDeleted).ToList();

                            bool isFirstString = true;
                            foreach (var child in requestChildren)
                            {
                                
                                over.SetFontAndSize(customFont, 12);
                                over.ShowTextAligned(Element.ALIGN_LEFT,
                                    $"{child.LastName} {child.FirstName} {child.MiddleName}", 200, row, 0);

                                WriteByTable(over, font, 500, row, 230,
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

                            over.ShowTextAligned(Element.ALIGN_LEFT,
                                $@"{request.RequestNumber} {applicant.LastName} {applicant.FirstName} {applicant.MiddleName} {applicant.DocumentType.Name} {applicant.DocumentSeria} {applicant.DocumentNumber}",290, 145, 0);

                            over.EndText();
                            over = pdfStamper.GetOverContent(2);
                            over.BeginText();

                            over.SetFontAndSize(customFont, 8);
                            row = 456;
                            isFirstString = true;
                            var attendants = request.Attendant.Where(c => !c.IsDeleted).ToList();

                            foreach (var attendant in attendants)
                            {

                                over.SetFontAndSize(customFont, 12);
                                over.ShowTextAligned(Element.ALIGN_LEFT,
                                    $"{attendant.LastName} {attendant.FirstName} {attendant.MiddleName}", 200, row, 0);

                                WriteByTable(over, font, 500, row, 230,
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

                            //over.SetFontAndSize(customFont, 10);
                            //var price = Settings.Default.CertificateOnMoneyPricePerChild *
                            //            (requestChildren.Count);

                            //var declination = new Склонятель();
                            //over.ShowTextAligned(Element.ALIGN_LEFT,
                            //    $"{price:### ### ### ### ### ### ##0} ({declination.Пропись(price, "рублей")}) рублей",
                            //    198, 222, 0);
                            //row = 177;

                            //over.SetFontAndSize(customFont, 8);
                            //if (!string.IsNullOrWhiteSpace(request.BankLastName))
                            //{
                            //    over.ShowTextAligned(Element.ALIGN_LEFT,
                            //        $"Получатель: {$"{request.BankLastName} {request.BankFirstName} {request.BankMiddleName}".Trim()}",
                            //        420, row, 0);

                            //    row = row - 32;
                            //}
                            //else if (applicant.DocumentType != null)
                            //{
                            //    over.ShowTextAligned(Element.ALIGN_LEFT,
                            //        $"Получатель: {$"{applicant.LastName} {applicant.FirstName} {applicant.MiddleName}".Trim()}, {applicant.DocumentType.Name}, {applicant.DocumentSeria} {applicant.DocumentNumber}",
                            //        420, row, 0);

                            //    row = row - 32;
                            //}

                            //var account = new List<string>();
                            //if (!string.IsNullOrWhiteSpace(request.BankName))
                            //{
                            //    account.Add(
                            //        $"{request.BankName} {(!string.IsNullOrWhiteSpace(request.BankBik) ? $"(БИК:{request.BankBik}{(!string.IsNullOrWhiteSpace(request.BankInn) ? $", ИНН:{request.BankInn}" : string.Empty)}{(!string.IsNullOrWhiteSpace(request.BankKpp) ? $", КПП:{request.BankKpp}" : string.Empty)})" : string.Empty)}");
                            //}

                            //if (!string.IsNullOrWhiteSpace(request.BankAccount))
                            //{
                            //    account.Add($"р/с: {request.BankAccount}");
                            //}

                            //if (!string.IsNullOrWhiteSpace(request.BankCorr))
                            //{
                            //    account.Add($"к/с: {request.BankCorr}");
                            //}

                            //if (!string.IsNullOrWhiteSpace(request.BankCardNumber))
                            //{
                            //    account.Add($"Номер карты: {request.BankCardNumber}");
                            //}

                            //if (account.Any())
                            //{
                            //    WriteByTable(over, font, 95, row, 650, string.Join(", ", account));
                            //}

                            over.EndText();
                        }
                    }
                }
            }
        }

        /// <summary>
        ///     сертификат на отдых 1 ребенок
        /// </summary>
        private static void CertificateOnRestSingle(Request request, Stream newStream)
        {
            var applicant = request.Applicant ?? new Applicant {DocumentType = new DocumentType {Name = string.Empty}};

            var arialPath = Environment.GetEnvironmentVariable("SystemRoot") + "\\fonts\\arial.ttf";
            var acromPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "AcromLight.otf");
            var acromOtherPath = Path.Combine(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin"), "AcromLight.otf");

            var customFont = File.Exists(acromOtherPath)
                ? BaseFont.CreateFont(acromOtherPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED)
                : File.Exists(acromPath)
                    ? BaseFont.CreateFont(acromPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED)
                    : BaseFont.CreateFont(arialPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);


            var font = new Font(customFont, 10);
            var fontS = new Font(customFont, 8);

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
                        over.ShowTextAligned(Element.ALIGN_LEFT, GetDayMonth((request.CertificateDate ?? request.DateChangeStatus ?? DateTime.Now).Date), 280, 400 + y_delta, 0);
                        over.ShowTextAligned(Element.ALIGN_LEFT, request.CertificateNumber.FormatEx(), 470, 400 + y_delta, 0);

                        var text = $"{child.LastName} {child.FirstName} {child.MiddleName}".Trim();
                        WriteByTable(over, text.Length > 108 ? fontS : font, 300, 350 + y_delta, 400, text);
                        over.ShowTextAligned(Element.ALIGN_LEFT, $"{child.DateOfBirth.FormatEx()}, {child.DocumentType.Name}, {child.DocumentSeria} {child.DocumentNumber}", 100, 317 + y_delta, 0);

                        if (request.TypeOfRestId != (long) TypeOfRestEnum.YouthRestOrphanCamps && (request.TypeOfRest?.NeedAccomodation ?? false))
                        {
                            var attendant =
                                (applicant.IsAccomp && !applicant.IsDeleted
                                    ? applicant
                                    : request.Attendant.FirstOrDefault(a => a.IsAccomp && !a.IsDeleted)) ??
                                new Applicant {DocumentType = new DocumentType {Name = string.Empty}};
                            var text_name = $"{attendant.LastName} {attendant.FirstName} {attendant.MiddleName}".Trim();
                            var text_docs = $"{attendant.DateOfBirth.FormatEx()}, {attendant.DocumentType.Name}, {attendant.DocumentSeria}, {attendant.DocumentNumber}";

                            WriteByTable(over, text.Length > 108 ? fontS : font, 400, 278 + y_delta, 500, text_name);
                            WriteByTable(over, text.Length > 108 ? fontS : font, 100, 250 + y_delta, 500, text_docs);
                        }

                        over.ShowTextAligned(Element.ALIGN_LEFT, request.NullSafe(r => GetDayMonth(r.Tour.DateIncome)) ?? string.Empty, 180, 222 + y_delta, 0);

                        over.ShowTextAligned(Element.ALIGN_LEFT, request.NullSafe(r => GetDayMonth(r.Tour.DateOutcome)) ?? string.Empty, 300, 222 + y_delta, 0);

                        over.ShowTextAligned(Element.ALIGN_LEFT, request.NullSafe(r => r.TransferTo.Name) ?? string.Empty, 340, 150 + y_delta, 0);

                        over.ShowTextAligned(Element.ALIGN_LEFT, request.NullSafe(r => r.TransferFrom.Name) ?? string.Empty, 340, 126 + y_delta, 0);

                        if (applicant.DocumentType != null)
                        {
                            text = $"{request.RequestNumber}, {$"{applicant.LastName} {applicant.FirstName} {applicant.MiddleName}".Trim()}, {applicant.DocumentType.Name}, {applicant.DocumentSeria} {applicant.DocumentNumber}";
                            WriteByTable(over, text.Length > 108 ? fontS : font, 325, 100 + y_delta, 470, text);
                        }

                        over.EndText();

                        if (request.Tour != null)
                        {
                            var headerTable = new PdfPTable(1) {TotalWidth = 600};
                            var p = new Phrase(new Chunk(request.NullSafe(r => r.Tour.Hotels.Name),
                                new Font(customFont, 10)));
                            var company = new PdfPCell(p);
                            company.SetLeading(11, 0);
                            company.HorizontalAlignment = Element.ALIGN_LEFT;
                            company.VerticalAlignment = Element.ALIGN_BOTTOM;
                            company.BorderWidth = 0;
                            headerTable.AddCell(company);
                            headerTable.WriteSelectedRows(0, 1, 0, 1, 190, 185 + headerTable.TotalHeight + y_delta, over);
                        }
                    }
                }
            }
        }

        /// <summary>
        ///     сертификат на отдых множественный
        /// </summary>
        private static void CertificateOnRestMulti(Request request, Stream newStream)
        {
            var arialPath = Environment.GetEnvironmentVariable("SystemRoot") + "\\fonts\\arial.ttf";
            var acromPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "AcromLight.otf");
            var acromOtherPath = Path.Combine(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin"), "AcromLight.otf");

            var customFont = File.Exists(acromOtherPath)
                ? BaseFont.CreateFont(acromOtherPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED)
                : File.Exists(acromPath)
                    ? BaseFont.CreateFont(acromPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED)
                    : BaseFont.CreateFont(arialPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);


            var assembly = Assembly.Load("RestChild.Templates");
            var font = new Font(customFont, 8);
            using (var stream = assembly.GetManifestResourceStream("RestChild.Templates.multi2021.pdf"))
            {
                using (var reader = new PdfReader(stream))
                {
                    using (var pdfStamper = new PdfStamper(reader, newStream, '1'))
                    {
                        var over = pdfStamper.GetOverContent(1);

                        var applicant = request.Applicant ?? new Applicant
                            {DocumentType = new DocumentType {Name = string.Empty}};

                        var headerTextSize = 9.5F;

                        over.BeginText();
                        over.SetFontAndSize(customFont, headerTextSize);
                        over.ShowTextAligned(Element.ALIGN_LEFT, GetDayMonth((request.CertificateDate ?? request.DateChangeStatus ?? DateTime.Now).Date), 570, 493, 0);
                        over.SetFontAndSize(customFont, headerTextSize);
                        over.ShowTextAligned(Element.ALIGN_LEFT, request.CertificateNumber.FormatEx(), 694, 493, 0);

                        var i = 0;

                        var rows = new float[] {455, 432, 411, 391, 371, 351, 330, 311, 289, 268, 248, 227, 207, 187, 167};

                        var requestChildren = request.Child.Where(c => !c.IsDeleted).ToList();
                        foreach (var child in requestChildren)
                        {
                            float row3;
                            if (i > 14)
                            {
                                row3 = rows[0] - 21F * (i + 1);
                            }
                            else
                            {
                                row3 = rows[i];
                            }

                            WriteByTable(over, font, 180, row3, 250,
                                $"{child.LastName} {child.FirstName} {child.MiddleName}", Element.ALIGN_CENTER);

                            WriteByTable(over, font, 530, row3, 250,
                                $"{child.DateOfBirth.FormatEx()}, {child.DocumentSeria} {child.DocumentNumber}",
                                Element.ALIGN_CENTER);

                            i++;
                        }


                        float row2 = 134;

                        over.SetFontAndSize(customFont, 8);
                        over.ShowTextAligned(Element.ALIGN_LEFT, GetDayMonth(request.Tour?.DateIncome), 170, row2, 0);

                        over.ShowTextAligned(Element.ALIGN_LEFT, GetDayMonth(request.Tour?.DateOutcome), 275, row2, 0);


                        var text = request.Tour?.Hotels?.Name ?? string.Empty;
                        WriteByTable(over, font, 470, row2, 500, text);

                        over.ShowTextAligned(Element.ALIGN_LEFT, request.TransferTo?.Name ?? string.Empty, 285, 92, 0);

                        over.ShowTextAligned(Element.ALIGN_LEFT, request.TransferFrom?.Name ?? string.Empty, 285, 70, 0);

                        text =
                            $"{request.RequestNumber}, {$"{applicant.LastName} {applicant.FirstName} {applicant.MiddleName}".Trim()}, {applicant.DocumentType?.Name} {applicant.DocumentSeria} {applicant.DocumentNumber}";
                        WriteByTable(over, font, 285, 47, 480, text, Element.ALIGN_CENTER);

                        over.EndText();

                        // ---------------------------------------------------
                        // вторая страница
                        over = pdfStamper.GetOverContent(2);
                        over.BeginText();
                        over.SetFontAndSize(customFont, headerTextSize);
                        over.ShowTextAligned(Element.ALIGN_LEFT, GetDayMonth((request.CertificateDate ?? request.DateChangeStatus ?? DateTime.Now).Date), 570, 493, 0);
                        over.SetFontAndSize(customFont, headerTextSize);
                        over.ShowTextAligned(Element.ALIGN_LEFT, request.CertificateNumber.FormatEx(), 692, 493, 0);

                        i = 0;

                        if (applicant.IsAccomp && !applicant.IsDeleted)
                        {
                            var row3 = rows[i];

                            WriteByTable(over, font, 190, row3, 250,
                                $"{applicant.LastName} {applicant.FirstName} {applicant.MiddleName}",
                                Element.ALIGN_CENTER);

                            WriteByTable(over, font, 530, row3, 250,
                                $"{applicant.DateOfBirth.FormatEx()}, {applicant.DocumentSeria} {applicant.DocumentNumber}",
                                Element.ALIGN_CENTER);

                            i++;
                        }


                        foreach (var attendant in
                            request.Attendant?.Where(ss => ss.IsAccomp && !ss.IsDeleted).ToList() ??
                            new List<Applicant>(0))
                        {
                            float row3;
                            if (i > 14)
                            {
                                row3 = rows[0] - 21F * (i + 1);
                            }
                            else
                            {
                                row3 = rows[i];
                            }

                            WriteByTable(over, font, 190, row3, 250,
                                $"{attendant.LastName} {attendant.FirstName} {attendant.MiddleName}",
                                Element.ALIGN_CENTER);

                            WriteByTable(over, font, 530, row3, 250,
                                $"{attendant.DateOfBirth.FormatEx()}, {attendant.DocumentSeria} {attendant.DocumentNumber}",
                                Element.ALIGN_CENTER);

                            i++;
                        }


                        over.EndText();
                    }
                }
            }
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
