using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.Mvc;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using RestChild.Comon;
using RestChild.Comon.Dto.Booking;
using RestChild.Comon.Enumeration;
using RestChild.Comon.OpenXmlExtensions;
using RestChild.DAL;
using RestChild.DAL.RepositoryExtensions;
using RestChild.Domain;
using Slepov.Russian.Morpher;

namespace RestChild.Web.Controllers
{
    public class RequestNotitificationController : BaseController
    {

		private static RunProperties RunProperties(string size = "26", bool bold = false)
		{
			var titleRequestRunProperties = new RunProperties();
			titleRequestRunProperties.AppendChild(new RunFonts
			{
				Ascii = "Times New Roman",
				HighAnsi = "Times New Roman",
				ComplexScript = "Times New Roman"
			});
			titleRequestRunProperties.AppendChild(new FontSize { Val = size });
			if (bold)
			{
				titleRequestRunProperties.AppendChild(new Bold());
			}
			return titleRequestRunProperties;
		}

		/// <summary>
		/// Заявление на отказ от путевки
		/// </summary>
		public ActionResult NotificationOnUpdateInformation(long id)
		{
			var request = UnitOfWork.GetById<Request>(id);

			if (request == null || request.IsDeleted)
			{
				return RedirectToAction("RequestList", "FirstRequestCompany");
			}

			MemoryStream ms;

			using (ms = new MemoryStream())
			{
				using (var wordDocument = WordprocessingDocument.Create(ms, WordprocessingDocumentType.Document))
				{
					var mainPart = wordDocument.AddMainDocumentPart();
					var doc = new Document(new Body());

					mainPart.Document = doc;

					var section = new SectionProperties();
					section.AppendChild(new PageMargin { Bottom = 1000, Top = 1000, Left = 1000, Right = 1000 });
					mainPart.Document.Body.AppendChild(section);

					var name = RequestHeaderToNotification(doc, request);

					doc.AppendChild(new Paragraph(new ParagraphProperties(new Justification { Val = JustificationValues.Center }),
						new Run(RunProperties(), new Text(" "))));

					doc.AppendChild(new Paragraph(new ParagraphProperties(new Justification { Val = JustificationValues.Center }),
						new Run(RunProperties(), new Text("ЗАЯВЛЕНИЕ"))));
					doc.AppendChild(new Paragraph(new ParagraphProperties(new Justification { Val = JustificationValues.Center }),
						new Run(RunProperties(), new Text("на уточнение сведений в заявлении на предоставлении путевки для отдыха и оздоровления"))));
					doc.AppendChild(new Paragraph(new ParagraphProperties(new Justification { Val = JustificationValues.Center }),
						new Run(RunProperties(), new Text(" "))));

					doc.AppendChild(new Paragraph(new ParagraphProperties(new Justification { Val = JustificationValues.Both }, new Indentation { FirstLine = "500" }),
						new Run(RunProperties(),
							new Text(
								$"По заявлению от {(PdfController.GetDayMonth(request.DateRequest))} № {request.RequestNumber} {(string.IsNullOrWhiteSpace(request.RequestNumberMpgu) ? "" : $"(номер МПГУ: {request.RequestNumberMpgu})")} на предоставление путевки для отдыха и оздоровления прошу считать правильными сведения, указанные в приложенных документах."))));

					doc.AppendChild(new Paragraph(new ParagraphProperties(new Justification { Val = JustificationValues.Left }),
						new Run(RunProperties(), new Text(" "))));

					doc.AppendChild(new Paragraph(new ParagraphProperties(new Justification { Val = JustificationValues.Left }, new Indentation { FirstLine = "500" }),
						new Run(RunProperties(), new Text("Приложение: на ____ л. в ____ экз."))));

					AppendSignToNotification(doc);
				}

				return File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "ЗАЯВЛЕНИЕ на отказ от заявления.docx");
			}
		}

		/// <summary>
		/// Заявление на отказ от путевки
		/// </summary>
		public ActionResult RefusingDocumentCertificate(long id)
		{
			var request = UnitOfWork.GetById<Request>(id);

			if (request == null || request.IsDeleted)
			{
				return RedirectToAction("RequestList", "FirstRequestCompany");
			}

			MemoryStream ms;

			using (ms = new MemoryStream())
			{
				using (var wordDocument = WordprocessingDocument.Create(ms, WordprocessingDocumentType.Document))
				{
					var mainPart = wordDocument.AddMainDocumentPart();
					var doc = new Document(new Body());

					mainPart.Document = doc;

					var section = new SectionProperties();
					section.AppendChild(new PageMargin { Bottom = 1000, Top = 1000, Left = 1000, Right = 1000 });
					mainPart.Document.Body.AppendChild(section);

					var name = RequestHeaderToNotification(doc, request);

					doc.AppendChild(new Paragraph(new ParagraphProperties(new Justification { Val = JustificationValues.Center }),
						new Run(RunProperties(), new Text(" "))));

					doc.AppendChild(new Paragraph(new ParagraphProperties(new Justification { Val = JustificationValues.Center }),
						new Run(RunProperties(), new Text("ЗАЯВЛЕНИЕ"))));
					doc.AppendChild(new Paragraph(new ParagraphProperties(new Justification { Val = JustificationValues.Center }),
						new Run(RunProperties(), new Text("на отказ от путевки для отдыха и оздоровления"))));
					doc.AppendChild(new Paragraph(new ParagraphProperties(new Justification { Val = JustificationValues.Center }),
						new Run(RunProperties(), new Text(" "))));

					doc.AppendChild(new Paragraph(new ParagraphProperties(new Justification { Val = JustificationValues.Both }, new Indentation { FirstLine = "500" }),
						new Run(RunProperties(),
							new Text(
								$"Я, {name} отказываюсь от путевки для отдыха и оздоровления полученной {(PdfController.GetDayMonth(request.CertificateDate ?? request.DateChangeStatus ?? DateTime.Now))} № {request.CertificateNumber} в порядке, установленном постановление Правительства Москвы от 22 февраля 2017 г. № 56-ПП \"Об организации отдыха и оздоровления детей, находящихся в трудной жизненной ситуации\"."))));

					AppendSignToNotification(doc);
				}

				return File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "ЗАЯВЛЕНИЕ на отказ от путевки.docx");
			}
		}

		/// <summary>
		/// Заявление на отказ от заявления
		/// </summary>
		public ActionResult RefusingDocument(long id)
	    {
		    var request = UnitOfWork.GetById<Request>(id);

		    if (request == null || request.IsDeleted)
		    {
			    return RedirectToAction("RequestList", "FirstRequestCompany");
		    }

		    MemoryStream ms;

		    using (ms = new MemoryStream())
		    {
			    using (var wordDocument = WordprocessingDocument.Create(ms, WordprocessingDocumentType.Document))
			    {


					var mainPart = wordDocument.AddMainDocumentPart();
				    var doc = new Document(new Body());

				    mainPart.Document = doc;

				    var section = new SectionProperties();
				    section.AppendChild(new PageMargin {Bottom = 1000, Top = 1000, Left = 1000, Right = 1000});
				    mainPart.Document.Body.AppendChild(section);

					var name = RequestHeaderToNotification(doc, request);

				    doc.AppendChild(new Paragraph(new ParagraphProperties(new Justification { Val = JustificationValues.Center }),
						new Run(RunProperties(), new Text(" "))));

					doc.AppendChild(new Paragraph(new ParagraphProperties(new Justification { Val = JustificationValues.Center }),
						new Run(RunProperties(), new Text("ЗАЯВЛЕНИЕ"))));
					doc.AppendChild(new Paragraph(new ParagraphProperties(new Justification { Val = JustificationValues.Center }),
						new Run(RunProperties(), new Text("на отказ от заявления на предоставление путевки для отдыха и оздоровления"))));
					doc.AppendChild(new Paragraph(new ParagraphProperties(new Justification { Val = JustificationValues.Center }),
						new Run(RunProperties(), new Text(" "))));

				    doc.AppendChild(new Paragraph(new ParagraphProperties(new Justification {Val = JustificationValues.Both}, new Indentation { FirstLine = "500" }),
					    new Run(RunProperties(),
						    new Text(
							    $"Я, {name} отказываюсь от поданного мной заявления от {(PdfController.GetDayMonth(request.DateRequest))} № {request.RequestNumber} {(string.IsNullOrWhiteSpace(request.RequestNumberMpgu)?"":$"(номер МПГУ: {request.RequestNumberMpgu})")} на предоставление путевки для отдыха и оздоровления, в порядке, установленном постановление Правительства Москвы от от 22 февраля 2017 г. № 56-ПП \"Об организации отдыха и оздоровления детей, находящихся в трудной жизненной ситуации\"."))));

					AppendSignToNotification(doc);
			    }

				return File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "ЗАЯВЛЕНИЕ на отказ от заявления.docx");
			}
		}

	    private static void AppendSignToNotification(Document doc)
	    {
			doc.AppendChild(new Paragraph(new ParagraphProperties(new Justification { Val = JustificationValues.Left }),
				new Run(RunProperties(), new Text(" "))));
			doc.AppendChild(new Paragraph(new ParagraphProperties(new Justification { Val = JustificationValues.Left }),
				new Run(RunProperties(), new Text(" "))));

			doc.AppendChild(
			    new Paragraph(
				    new ParagraphProperties(new Justification {Val = JustificationValues.Left}, new Indentation {Left = "6000"}),
				    new Run(RunProperties(), new Text("\"___\" ___________ 20___г."))));
		    doc.AppendChild(
			    new Paragraph(
				    new ParagraphProperties(new Justification {Val = JustificationValues.Left}, new Indentation {Left = "7500"},
					    new SpacingBetweenLines {Line = "160", LineRule = LineSpacingRuleValues.Auto, Before = "0", After = "0"}),
				    new Run(RunProperties("16"), new Text("дата"))));
		    doc.AppendChild(
			    new Paragraph(
				    new ParagraphProperties(new Justification {Val = JustificationValues.Left}, new Indentation {Left = "6000"}),
				    new Run(RunProperties(), new Text("___________/___________________/"))));
		    doc.AppendChild(
			    new Paragraph(
				    new ParagraphProperties(new Justification {Val = JustificationValues.Left}, new Indentation {Left = "6500"},
					    new SpacingBetweenLines {Line = "160", LineRule = LineSpacingRuleValues.Auto, Before = "0", After = "0"}),
				    new Run(RunProperties("16"), new Text("подпись                          расшифровка"))));
	    }

	    private static string RequestHeaderToNotification(Document doc, Request request)
	    {
		    var name =
			    $"{request.Agent?.LastName ?? request.Applicant?.LastName} {request.Agent?.FirstName ?? request.Applicant?.FirstName} {request.Agent?.MiddleName ?? request.Applicant?.MiddleName}";

		    var document =
			    $"{request.Agent?.DocumentType?.Name ?? request.Applicant?.DocumentType?.Name} {request.Agent?.DocumentSeria ?? request.Applicant?.DocumentSeria} {request.Agent?.DocumentNumber ?? request.Applicant?.DocumentNumber}, выдан {request.Agent?.DocumentSubjectIssue ?? request.Applicant?.DocumentSubjectIssue}, {(request.Agent?.DocumentDateOfIssue ?? request.Applicant?.DocumentDateOfIssue).FormatEx()}";

		    var statusByChild = FirstRequestCompanyController.GetStatusApplicantName(request.StatusApplicant);

		    var phone = $"{request.Agent?.Phone ?? request.Applicant?.Phone}";

		    var email = $"{request.Agent?.Email ?? request.Applicant?.Email}";

		    var skl = new Склонятель();
		    var nameSkloniatel = skl.Проанализировать(name ?? string.Empty);

		    doc.AppendChild(
			    new Paragraph(
				    new ParagraphProperties(new Justification {Val = JustificationValues.Left}, new Indentation {Left = "5000"}),
				    new Run(RunProperties(),
					    new Text(
						    "Государственное автономное учреждение культуры города Москвы \"Московское агентство организации отдыха и туризма\""))));

		    doc.AppendChild(
			    new Paragraph(
				    new ParagraphProperties(new Justification {Val = JustificationValues.Left}, new Indentation {Left = "5000"}),
				    new Run(RunProperties(),
					    new Text($"от {(nameSkloniatel?.Родительный ?? name)}"))));
		    doc.AppendChild(
			    new Paragraph(
				    new ParagraphProperties(new Justification {Val = JustificationValues.Left}, new Indentation {Left = "5000"}),
				    new Run(RunProperties(),
					    new Text($"документ, удостоверяющий личность {document}"))));

		    doc.AppendChild(
			    new Paragraph(
				    new ParagraphProperties(new Justification {Val = JustificationValues.Left}, new Indentation {Left = "5000"}),
				    new Run(RunProperties(),
					    new Text($"статус по отношению к ребенку: {statusByChild}"))));
		    doc.AppendChild(
			    new Paragraph(
				    new ParagraphProperties(new Justification {Val = JustificationValues.Left}, new Indentation {Left = "5000"}),
				    new Run(RunProperties(),
					    new Text($"телефон: {phone}"))));
		    doc.AppendChild(
			    new Paragraph(
				    new ParagraphProperties(new Justification {Val = JustificationValues.Left}, new Indentation {Left = "5000"}),
				    new Run(RunProperties(),
					    new Text($"электронная почта: {email}"))));
		    return name;
	    }
    }
}
