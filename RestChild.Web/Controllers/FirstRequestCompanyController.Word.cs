using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using RestChild.Comon;
using RestChild.Comon.Dto.Booking;
using RestChild.Comon.Enumeration;
using RestChild.Comon.OpenXmlExtensions;
using RestChild.DAL.RepositoryExtensions;
using RestChild.DocumentGeneration;
using RestChild.Domain;
using DocumentType = RestChild.Domain.DocumentType;
using Settings = RestChild.Web.Properties.Settings;

namespace RestChild.Web.Controllers
{
    public partial class FirstRequestCompanyController
    {
        /// <summary>
        ///     Получение печатной формы для межведомственного запроса
        /// </summary>
        /// <param name="childId"></param>
        /// <returns></returns>
        public ActionResult GetInteragencyRequestPrint(long childId)
        {
            using (var ms = new MemoryStream())
            {
                using (var wordDocument = WordprocessingDocument.Create(ms, WordprocessingDocumentType.Document))
                {
                    var mainPart = wordDocument.AddMainDocumentPart();
                    var doc = new Document(new Body());

                    GetInteragencyRequestDocument(childId, doc);

                    mainPart.Document = doc;
                }

                return File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                    "Межведомственный запрос.docx");
            }
        }

        /// <summary>
        ///     Получить печатную форму для межведомственного запроса
        /// </summary>
        internal void GetInteragencyRequestDocument(long childId, Document doc)
        {
            var mainRunProperties = new RunProperties();
            mainRunProperties.AppendChild(new RunFonts
            {
                Ascii = "Times New Roman",
                HighAnsi = "Times New Roman",
                ComplexScript = "Times New Roman"
            });
            mainRunProperties.AppendChild(new FontSize { Val = "24" });

            var mainTitleRunProperties = mainRunProperties.CloneNode(true);
            mainTitleRunProperties.AppendChild(new Bold());

            var mainSmallRunProperties = mainRunProperties.CloneNode(true);
            mainSmallRunProperties.RemoveAllChildren<FontSize>();
            mainSmallRunProperties.AppendChild(new FontSize { Val = "20" });

            var mainUnderlineRunProperties = mainRunProperties.CloneNode(true);

            var bottomBorders = new ParagraphBorders
            {
                BottomBorder =
                    new BottomBorder
                    {
                        Val = new EnumValue<BorderValues>(BorderValues.Single),
                        Size = 6,
                        Space = 1
                    }
            };

            doc.AppendChild(new Paragraph(
                new ParagraphProperties(new Justification { Val = JustificationValues.Center },
                    new SpacingBetweenLines { After = "20" }),
                new Run(mainTitleRunProperties.CloneNode(true),
                    new Text("МЕЖВЕДОМСТВЕННЫЙ ЗАПРОС"))));
            doc.AppendChild(new Paragraph(
                new ParagraphProperties(new Justification { Val = JustificationValues.Center },
                    new SpacingBetweenLines { After = "20" }),
                new Run(mainTitleRunProperties.CloneNode(true),
                    new Text("О ПРЕДСТАВЛЕНИИ ИНФОРМАЦИИ, "))));
            doc.AppendChild(new Paragraph(
                new ParagraphProperties(new Justification { Val = JustificationValues.Center },
                    new SpacingBetweenLines { After = "20" }),
                new Run(mainTitleRunProperties.CloneNode(true),
                    new Text("НЕОБХОДИМОЙ ДЛЯ ПРЕДОСТАВЛЕНИЯ ГОСУДАРСТВЕННОЙ УСЛУГИ"))));

            doc.AppendChild(new Paragraph(new ParagraphProperties(new SpacingBetweenLines { After = "20" })));
            doc.AppendChild(new Paragraph(new ParagraphProperties(new SpacingBetweenLines { After = "20" })));

            doc.AppendChild(new Paragraph(
                new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                    bottomBorders.CloneNode(true),
                    new SpacingBetweenLines { After = "20" }),
                new Run(mainRunProperties.CloneNode(true),
                    new Text(
                        "В целях предоставления государственной услуги по организации отдыха и оздоровления детей в соответствии с постановлением Правительства Москвы от 15 февраля 2011 г. № 29-ПП \"Об организации отдыха и оздоровления детей города Москвы в 2011 году и последующие годы\" просим представить в отношении"))));

            doc.AppendChild(new Paragraph(
                new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                    bottomBorders.CloneNode(true),
                    new SpacingBetweenLines { After = "20" }),
                new Run(mainUnderlineRunProperties.CloneNode(true), new Text(""))));

            doc.AppendChild(new Paragraph(
                new ParagraphProperties(new Justification { Val = JustificationValues.Center },
                    new SpacingBetweenLines { After = "10" }),
                new Run(mainSmallRunProperties.CloneNode(true),
                    new Text("(Ф.И.О. лица, подающего заявление, его местонахождение"))));

            doc.AppendChild(new Paragraph(
                new ParagraphProperties(new Justification { Val = JustificationValues.Center },
                    new SpacingBetweenLines { After = "10" }),
                new Run(mainSmallRunProperties.CloneNode(true),
                    new Text(
                        "(для юридического лица)/место жительства (для физического лица) либо иные сведения, необходимые "))));

            doc.AppendChild(new Paragraph(new ParagraphProperties(new Justification { Val = JustificationValues.Center }),
                new Run(mainSmallRunProperties.CloneNode(true),
                    new Text("для представления информации)"))));

            doc.AppendChild(new Paragraph(
                new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                    bottomBorders.CloneNode(true),
                    new SpacingBetweenLines { After = "20" }),
                new Run(mainRunProperties.CloneNode(true),
                    new Text(
                        "следующую информацию об отнесении к льготной категории \"дети из малообеспеченной семьи\" ребенка заявителя"))));

            doc.AppendChild(new Paragraph(
                new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                    bottomBorders.CloneNode(true),
                    new SpacingBetweenLines { After = "20" }),
                new Run(mainUnderlineRunProperties.CloneNode(true), new Text(""))));

            doc.AppendChild(new Paragraph(
                new ParagraphProperties(new Justification { Val = JustificationValues.Center },
                    new SpacingBetweenLines { After = "10" }),
                new Run(mainSmallRunProperties.CloneNode(true),
                    new Text("(Ф.И.О. лица, подающего заявление, его местонахождение"))));

            doc.AppendChild(new Paragraph(
                new ParagraphProperties(new Justification { Val = JustificationValues.Center },
                    new SpacingBetweenLines { After = "10" }),
                new Run(mainSmallRunProperties.CloneNode(true),
                    new Text(
                        "(для юридического лица)/место жительства (для физического лица) либо иные сведения, необходимые "))));

            doc.AppendChild(new Paragraph(new ParagraphProperties(new Justification { Val = JustificationValues.Center }),
                new Run(mainSmallRunProperties.CloneNode(true),
                    new Text("для представления информации)"))));

            doc.AppendChild(new Paragraph(
                new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                    new SpacingBetweenLines { After = "20" }),
                new Run(mainRunProperties.CloneNode(true),
                    new Text("Ответ на межведомственный запрос просим направить по"))));

            doc.AppendChild(new Paragraph(
                new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                    bottomBorders.CloneNode(true),
                    new SpacingBetweenLines { After = "20" }),
                new Run(mainUnderlineRunProperties.CloneNode(true), new Text(""))));

            doc.AppendChild(new Paragraph(new ParagraphProperties(new Justification { Val = JustificationValues.Center }),
                new Run(mainSmallRunProperties.CloneNode(true),
                    new Text(
                        "(указать адрес электронной почты Исполнителя, номер факса, по которым надлежит направить ответ)"))));

            doc.AppendChild(new Paragraph(new ParagraphProperties(new Justification { Val = JustificationValues.Left }),
                new Run(mainRunProperties.CloneNode(true),
                    new Text("в срок до ______________________________."))));

            doc.AppendChild(new Paragraph(new ParagraphProperties(new Justification { Val = JustificationValues.Left }),
                new Run(mainSmallRunProperties.CloneNode(true),
                    new Text("(указать срок ожидаемого ответа)"))));

            InteragencyDocSign(doc);
        }

        /// <summary>
        ///     Подпись документа
        /// </summary>
        private void InteragencyDocSign(Document doc)
        {
            var account = Security.GetCurrentAccount() ?? new Account();
            var mainRunProperties = new RunProperties();
            mainRunProperties.AppendChild(new RunFonts
            {
                Ascii = "Times New Roman",
                HighAnsi = "Times New Roman",
                ComplexScript = "Times New Roman"
            });
            mainRunProperties.AppendChild(new FontSize { Val = "24" });

            var captionRunProperties = new RunProperties();
            captionRunProperties.AppendChild(new RunFonts
            {
                Ascii = "Times New Roman",
                HighAnsi = "Times New Roman",
                ComplexScript = "Times New Roman"
            });
            captionRunProperties.AppendChild(new FontSize { Val = "18" });

            var tblProp = new TableProperties(
                new TableBorders(
                    new TopBorder { Val = new EnumValue<BorderValues>(BorderValues.None) },
                    new BottomBorder { Val = new EnumValue<BorderValues>(BorderValues.None) },
                    new LeftBorder { Val = new EnumValue<BorderValues>(BorderValues.None) },
                    new RightBorder { Val = new EnumValue<BorderValues>(BorderValues.None) },
                    new InsideHorizontalBorder { Val = new EnumValue<BorderValues>(BorderValues.None) },
                    new InsideVerticalBorder { Val = new EnumValue<BorderValues>(BorderValues.None) }));

            doc.AppendChild(new Paragraph(new ParagraphProperties(new Justification { Val = JustificationValues.Left }),
                new Run(mainRunProperties.CloneNode(true),
                    new Text("Уведомление выдал:"))));

            var table = new Table();
            table.AppendChild(tblProp.CloneNode(true));

            var row = new TableRow();

            var cell = new TableCell();
            cell.Append(new TableCellProperties(
                    new TableCellWidth { Type = TableWidthUnitValues.Dxa, Width = "2931" },
                    new TableCellBorders(new BottomBorder { Val = new EnumValue<BorderValues>(BorderValues.Single) })),
                new TableCellVerticalAlignment { Val = TableVerticalAlignmentValues.Bottom });
            cell.AppendChild(new Paragraph(
                new ParagraphProperties(new Justification { Val = JustificationValues.Center }),
                new Run(mainRunProperties.CloneNode(true),
                    new Text(account.Position.FormatEx()))));
            row.AppendChild(cell);


            cell = new TableCell();
            cell.Append(new TableCellProperties(
                    new TableCellWidth { Type = TableWidthUnitValues.Dxa, Width = "250" }),
                new TableCellVerticalAlignment { Val = TableVerticalAlignmentValues.Bottom });
            cell.AppendChild(new Paragraph(new Run(new Text(" "))));
            row.AppendChild(cell);

            cell = new TableCell();
            cell.Append(new TableCellProperties(
                    new TableCellWidth { Type = TableWidthUnitValues.Dxa, Width = "1604" },
                    new TableCellBorders(new BottomBorder { Val = new EnumValue<BorderValues>(BorderValues.Single) })),
                new TableCellVerticalAlignment { Val = TableVerticalAlignmentValues.Bottom });
            cell.AppendChild(new Paragraph(new Run(new Text(" "))));
            row.AppendChild(cell);

            cell = new TableCell();
            cell.Append(new TableCellProperties(
                    new TableCellWidth { Type = TableWidthUnitValues.Dxa, Width = "250" }),
                new TableCellVerticalAlignment { Val = TableVerticalAlignmentValues.Bottom });
            cell.AppendChild(new Paragraph(new Run(new Text(" "))));
            row.AppendChild(cell);

            cell = new TableCell();
            cell.AppendChild(new TableCellProperties(
                new TableCellWidth { Type = TableWidthUnitValues.Dxa, Width = "2731" },
                new TableCellBorders(new BottomBorder { Val = new EnumValue<BorderValues>(BorderValues.Single) }),
                new TableCellVerticalAlignment { Val = TableVerticalAlignmentValues.Bottom }
            ));

            cell.AppendChild(new Paragraph(
                new ParagraphProperties(new Justification { Val = JustificationValues.Center }),
                new Run(mainRunProperties.CloneNode(true),
                    new Text(account.Name.FormatEx()))));
            row.AppendChild(cell);

            table.AppendChild(row);
            row = new TableRow();

            cell = new TableCell();
            cell.Append(new TableCellProperties(new TableCellWidth { Type = TableWidthUnitValues.Dxa, Width = "2931" }),
                new TableCellVerticalAlignment { Val = TableVerticalAlignmentValues.Bottom });
            cell.AppendChild(new Paragraph(
                new ParagraphProperties(new Justification { Val = JustificationValues.Center }),
                new Run(captionRunProperties.CloneNode(true),
                    new Text("(должность)"))));
            row.AppendChild(cell);

            cell = new TableCell();
            cell.Append(new TableCellProperties(
                    new TableCellWidth { Type = TableWidthUnitValues.Dxa, Width = "250" }),
                new TableCellVerticalAlignment { Val = TableVerticalAlignmentValues.Bottom });
            cell.AppendChild(new Paragraph(new Run(new Text(" "))));
            row.AppendChild(cell);

            cell = new TableCell();
            cell.Append(new TableCellProperties(
                    new TableCellWidth { Type = TableWidthUnitValues.Dxa, Width = "1604" }),
                new TableCellVerticalAlignment { Val = TableVerticalAlignmentValues.Bottom });
            cell.AppendChild(new Paragraph(
                new ParagraphProperties(new Justification { Val = JustificationValues.Center }),
                new Run(captionRunProperties.CloneNode(true), new Text("(подпись)"))));
            row.AppendChild(cell);

            cell = new TableCell();
            cell.Append(new TableCellProperties(
                    new TableCellWidth { Type = TableWidthUnitValues.Dxa, Width = "250" }),
                new TableCellVerticalAlignment { Val = TableVerticalAlignmentValues.Bottom });
            cell.AppendChild(new Paragraph(new Run(new Text(" "))));
            row.AppendChild(cell);

            cell = new TableCell();
            cell.Append(new TableCellProperties(
                    new TableCellWidth { Type = TableWidthUnitValues.Dxa, Width = "2731" }),
                new TableCellVerticalAlignment { Val = TableVerticalAlignmentValues.Bottom });
            cell.AppendChild(new Paragraph(
                new ParagraphProperties(new Justification { Val = JustificationValues.Center }),
                new Run(captionRunProperties.CloneNode(true), new Text("(расшифровка подписи)"))));
            row.AppendChild(cell);

            table.AppendChild(row);

            doc.AppendChild(table);
        }

        /// <summary>
        ///     заявление.
        /// </summary>
        public ActionResult RequestDoc(long id)
        {
            SetUnitOfWorkInRefClass(UnitOfWork);

            var request = ApiController.RequestEdit(id);

            if (request == null || request.IsDeleted)
            {
                return RedirectToAction("RequestList");
            }

            var isNotYouthRest = request.TypeOfRestId != (long)TypeOfRestEnum.YouthRestOrphanCamps &&
                                 request.TypeOfRestId != (long)TypeOfRestEnum.YouthRestCamps;

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

                    AppendHeader(doc);
                    var generalBlock = new List<Tuple<string, string>>
                    {
                        new Tuple<string, string>("Номер заявления", request.RequestNumber.FormatEx()),
                        new Tuple<string, string>("Дата заявления", request.DateRequest.FormatEx("dd.MM.yyyy")),
                        new Tuple<string, string>("Источник заявления", request.Source.Name.FormatEx("-", false))
                    };

                    if (!string.IsNullOrEmpty(request.RequestNumberMpgu))
                    {
                        generalBlock.Add(new Tuple<string, string>("Номер заявления МПГУ", request.RequestNumberMpgu));
                    }

                    if (request.DeclineReason != null)
                    {
                        generalBlock.Add(new Tuple<string, string>("Причина отказа", request.DeclineReason.Name));
                    }

                    if (!string.IsNullOrWhiteSpace(request.StatusApplicant) && isNotYouthRest)
                    {
                        var statusApplicant = GetStatusApplicantName(request.StatusApplicant);

                        if (!string.IsNullOrWhiteSpace(statusApplicant))
                        {
                            generalBlock.Add(new Tuple<string, string>("Заявление подает", statusApplicant));
                        }
                    }

                    AppendBlock(doc, "Общие сведения о заявлении", generalBlock);

                    var typeAndTypeRest = new List<Tuple<string, string>>
                    {
                        new Tuple<string, string>("Цель обращения",
                            request.NullSafe(r => r.TypeOfRest.Name.FormatEx("-", false)))
                    };

                    if (request.TypeOfRestId != (long)TypeOfRestEnum.Compensation &&
                        request.TypeOfRestId != (long)TypeOfRestEnum.CompensationYouthRest &&
                        request.TypeOfRestId != (long)TypeOfRestEnum.MoneyOn3To7 &&
                        request.TypeOfRestId != (long)TypeOfRestEnum.MoneyOn7To15)
                    {
                        if (request.IsFirstCompany)
                        {
                            var firstTimeOfRest = request.TimesOfRest?.Where(ss => ss.Order > 0)
                                .Select(ss => ss.TimeOfRest).FirstOrDefault();
                            var secondTimeOfRest = request.TimesOfRest?.Where(ss => ss.Order > 0)
                                .Select(ss => ss.TimeOfRest).LastOrDefault();
                            if (secondTimeOfRest?.Id == firstTimeOfRest?.Id)
                            {
                                secondTimeOfRest = null;
                            }

                            typeAndTypeRest.Add(new Tuple<string, string>("Приоритетное время отдыха",
                                request.TimeOfRest?.Name.FormatEx("-", false)));
                            typeAndTypeRest.Add(new Tuple<string, string>("Дополнительные времена отдыха",
                                firstTimeOfRest?.Name?.FormatEx("-", false)));
                            typeAndTypeRest.Add(new Tuple<string, string>(" ",
                                secondTimeOfRest?.Name?.FormatEx("-", false)));
                        }
                        else
                        {
                            typeAndTypeRest.Add(new Tuple<string, string>("Время отдыха",
                                request.NullSafe(r => r.TimeOfRest.Name.FormatEx("-", false)) +
                                (request.Tour != null
                                    ? $" {request.Tour.DateIncome.FormatEx()}-{request.Tour.DateOutcome.FormatEx()}"
                                    : string.Empty)));
                        }

                        if (request.SubjectOfRest != null)
                        {
                            typeAndTypeRest.Add(new Tuple<string, string>("Тематика смены",
                                request.SubjectOfRest.Name.FormatEx("-", false)));
                        }

                        if (request.TransferTo != null)
                        {
                            typeAndTypeRest.Add(new Tuple<string, string>(
                                "Проезд из города Москвы к месту отдыха",
                                request.TransferTo.Name.FormatEx("-", false)));
                        }

                        if (request.TransferFrom != null)
                        {
                            typeAndTypeRest.Add(new Tuple<string, string>(
                                "Проезд из места отдыха в город Москву",
                                request.TransferFrom.Name.FormatEx("-", false)));
                        }
                    }
                    else
                    {
                        typeAndTypeRest.Add(new Tuple<string, string>("Год кампании",
                            request.YearOfRest?.Name.FormatEx("-", false)));
                    }

                    AppendBlock(doc, "Цель обращения", typeAndTypeRest);

                    if (request.TypeOfRestId != (long)TypeOfRestEnum.Compensation &&
                        request.TypeOfRestId != (long)TypeOfRestEnum.CompensationYouthRest &&
                        request.TypeOfRestId != (long)TypeOfRestEnum.MoneyOn3To7 &&
                        request.TypeOfRestId != (long)TypeOfRestEnum.MoneyOn7To15)
                    {
                        if (!request.IsFirstCompany)
                        {
                            AppendBlock(doc, "Место отдыха", new List<Tuple<string, string>>
                            {
                                new Tuple<string, string>("Регион",
                                    request.NullSafe(r => r.PlaceOfRest.Name.FormatEx("-", false))),
                                new Tuple<string, string>("Организация отдыха и оздоровления",
                                    request.NullSafe(r => r.Tour.Hotels.Name.FormatEx("-", false)) +
                                    $" ({request.NullSafe(r => r.Tour.Hotels.Address.FormatEx("-", false))})")
                            });
                        }
                        else
                        {
                            var firstPlaceOfRest = request.PlacesOfRest?.Where(ss => ss.Order > 0)
                                .OrderBy(ss => ss.Order).Select(ss => ss.PlaceOfRest).FirstOrDefault();
                            var secondPlaceOfRest = request.PlacesOfRest?.Where(ss => ss.Order > 0)
                                .OrderBy(ss => ss.Order).Select(ss => ss.PlaceOfRest).LastOrDefault();
                            if (secondPlaceOfRest?.Id == firstPlaceOfRest?.Id)
                            {
                                secondPlaceOfRest = null;
                            }

                            AppendBlock(doc, "Место отдыха", new List<Tuple<string, string>>
                            {
                                new Tuple<string, string>("Приоритетный регион отдыха",
                                    request.NullSafe(r => r.PlaceOfRest.Name.FormatEx("-", false))),
                                new Tuple<string, string>("Дополнительные регионы отдыха",
                                    firstPlaceOfRest?.Name?.FormatEx("-", false)),
                                new Tuple<string, string>(" ", secondPlaceOfRest?.Name?.FormatEx("-", false))
                            });
                        }

                        if (isNotYouthRest)
                        {
                            var placement = new List<Tuple<string, string>>
                            {
                                new Tuple<string, string>("Детей",
                                    request.NullSafe(r => r.CountPlace.FormatEx("-", false)))
                            };

                            if (request.CountAttendants > 0)
                            {
                                placement.Add(new Tuple<string, string>("Сопровождающих",
                                    request.NullSafe(r => r.CountAttendants.FormatEx("-", false))));
                            }

                            if (request.BookingGuid.HasValue)
                            {
                                var bookings =
                                    UnitOfWork.GetSet<Domain.Booking>()
                                        .Where(b => b.Code == request.BookingGuid &&
                                                    b.TourVolume.TypeOfRoomsId.HasValue)
                                        .ToList();
                                if (bookings.Any())
                                {
                                    var locations =
                                        bookings.Select(
                                            b =>
                                                new LocationRequest
                                                {
                                                    Count = b.CountRooms,
                                                    Name = b.TourVolume.TypeOfRooms.Name,
                                                    RoomId = b.TourVolume.TypeOfRoomsId ?? 0
                                                }).ToList();

                                    var placementText = new StringBuilder();

                                    foreach (var location in locations)
                                    {
                                        placementText.AppendFormat("{0} (количество номеров: {1})\n", location.Name,
                                            location.Count);
                                    }

                                    placement.Add(new Tuple<string, string>("Размещение", placementText.ToString()));
                                }
                            }

                            AppendBlock(doc, "Размещение", placement);
                        }
                        else
                        {
                            AppendBlock(doc, "Размещение", new List<Tuple<string, string>>
                            {
                                new Tuple<string, string>("Отдыхающих", "1")
                            });
                        }
                    }
                    else if (request.TypeOfRestId != (long)TypeOfRestEnum.Compensation
                             && request.TypeOfRestId != (long)TypeOfRestEnum.CompensationYouthRest &&
                             !string.IsNullOrWhiteSpace(request.BankName))
                    {
                        AppendBlock(doc, "Банковские реквизиты", new List<Tuple<string, string>>
                        {
                            new Tuple<string, string>("Наименование банка", request.BankName.FormatEx("-", false)),
                            new Tuple<string, string>("ИНН банка", request.BankInn.FormatEx("-", false)),
                            new Tuple<string, string>("КПП банка", request.BankKpp.FormatEx("-", false)),
                            new Tuple<string, string>("БИК", request.BankBik.FormatEx("-", false)),
                            new Tuple<string, string>("Расчетный счет", request.BankAccount.FormatEx("-", false)),
                            new Tuple<string, string>("Корреспондентский счёт", request.BankCorr.FormatEx("-", false)),
                            new Tuple<string, string>("Номер карты", request.BankCardNumber.FormatEx("-", false)),
                            new Tuple<string, string>("Фамилия получателя", request.BankLastName.FormatEx("-", false)),
                            new Tuple<string, string>("Имя получателя", request.BankFirstName.FormatEx("-", false)),
                            new Tuple<string, string>("Отчество получателя",
                                request.BankMiddleName.FormatEx("-", false))
                        });
                    }
                    else
                    {
                        AppendBlock(doc, "Путевки", null);

                        foreach (var iv in request.InformationVouchers)
                        {
                            var tuples = new List<Tuple<string, string>>
                            {
                                new Tuple<string, string>("Цель обращения",
                                    iv.NullSafe(r => r.Type.Name.FormatEx("-", false))),
                                new Tuple<string, string>("Наименование организации",
                                    iv.NullSafe(r => r.OrganizationName.FormatEx("-", false))),
                                new Tuple<string, string>("Дата начала", iv?.DateFrom.FormatEx()),
                                new Tuple<string, string>("Дата окончания", iv?.DateTo.FormatEx()),
                                new Tuple<string, string>("Стоимость (руб.)", iv?.Price.FormatEx()),
                                new Tuple<string, string>("Стоимость дороги(руб)", iv?.CostOfRide.FormatEx()),
                                new Tuple<string, string>("Количество отдохнувших", iv?.CountPeople.FormatEx())
                            };

                            foreach (var ap in iv?.AttendantsPrice ?? new List<RequestInformationVoucherAttendant>())
                            {
                                tuples.Add(new Tuple<string, string>(
                                    $"{ap?.Applicant?.LastName} {ap?.Applicant?.FirstName} {ap?.Applicant?.MiddleName}",
                                    $"Стоимость путевки: {ap?.Price.FormatEx().Trim()};\nСтоимость дороги: {ap?.CostOfRide.FormatEx().Trim()}"));
                            }

                            AppendBlock(doc, "", tuples);
                        }

                        if (!string.IsNullOrWhiteSpace(request.BankName))
                        {
                            AppendBlock(doc, "Банковские реквизиты", new List<Tuple<string, string>>
                            {
                                new Tuple<string, string>("Наименование банка", request.BankName.FormatEx("-", false)),
                                new Tuple<string, string>("ИНН банка", request.BankInn.FormatEx("-", false)),
                                new Tuple<string, string>("КПП банка", request.BankKpp.FormatEx("-", false)),
                                new Tuple<string, string>("БИК", request.BankBik.FormatEx("-", false)),
                                new Tuple<string, string>("Лицевой счет", request.BankAccount.FormatEx("-", false)),
                                new Tuple<string, string>("Номер карты", request.BankCardNumber.FormatEx("-", false))
                            });
                        }
                    }

                    AppendBlock(doc, "Сведения о заявителе", new List<Tuple<string, string>>
                    {
                        new Tuple<string, string>("Фамилия",
                            request.NullSafe(r => r.Applicant.LastName.FormatEx("-", false))),
                        new Tuple<string, string>("Имя",
                            request.NullSafe(r => r.Applicant.FirstName.FormatEx("-", false))),
                        new Tuple<string, string>("Отчество",
                            request.NullSafe(r => r.Applicant.MiddleName.FormatEx("-", false))),
                        new Tuple<string, string>("Пол", request?.Applicant?.Male ?? false ? "Мужской" : "Женский"),
                        new Tuple<string, string>("Дата рождения",
                            request.NullSafe(r => r.Applicant.DateOfBirth.FormatEx("-", false))),
                        new Tuple<string, string>("Место рождения",
                            request.NullSafe(r => r.Applicant.PlaceOfBirth.FormatEx("-", false))),
                        new Tuple<string, string>("СНИЛС",
                            request.NullSafe(r => r.Applicant.Snils.FormatEx("-", false))),
                        new Tuple<string, string>("Телефон",
                            request.NullSafe(r => r.Applicant.Phone.FormatEx("-", false))),
                        new Tuple<string, string>("Email",
                            request.NullSafe(r => r.Applicant.Email.FormatEx("-", false))),
                        new Tuple<string, string>("Тип документа, удостоверяющего личность",
                            request.NullSafe(r => r.Applicant.DocumentType.Name.FormatEx("-", false))),
                        new Tuple<string, string>("Серия и номер",
                            request.NullSafe(
                                r => r.Applicant.DocumentSeria.FormatEx("-", false) + " " +
                                     r.Applicant.DocumentNumber.FormatEx("-", false))),
                        new Tuple<string, string>("Когда выдан документ",
                            request.NullSafe(r => r.Applicant.DocumentDateOfIssue.FormatEx("dd.MM.yyyy", "-"))),
                        new Tuple<string, string>("Кем выдан документ",
                            request.NullSafe(r => r.Applicant.DocumentSubjectIssue.FormatEx("-", false))),
                        new Tuple<string, string>("Заявитель является сопровождающим",
                            request.NullSafe(r => r.Applicant.IsAccomp) ? "Да" : "Нет"),
                        new Tuple<string, string>("Заявление подается представителем заявителя",
                            request.NullSafe(r => r.AgentApplicant ?? false) ? "Да" : "Нет")
                    });

                    if (request.AgentApplicant ?? false)
                    {
                        var attendantAgent = request?.Attendant?.FirstOrDefault(a => a.IsAgent);

                        var block = new List<Tuple<string, string>>
                        {
                            new Tuple<string, string>("Фамилия",
                                request.NullSafe(r => r.Agent.LastName.FormatEx("-", false))),
                            new Tuple<string, string>("Имя",
                                request.NullSafe(r => r.Agent.FirstName.FormatEx("-", false))),
                            new Tuple<string, string>("Отчество",
                                request.NullSafe(r => r.Agent.MiddleName.FormatEx("-", false))),
                            new Tuple<string, string>("Пол", request?.Agent?.Male ?? false ? "Мужской" : "Женский"),
                            new Tuple<string, string>("Дата рождения",
                                request.NullSafe(r => r.Agent.DateOfBirth.FormatEx("-", false))),
                            new Tuple<string, string>("Место рождения",
                                request.NullSafe(r => r.Agent.PlaceOfBirth.FormatEx("-", false))),
                            new Tuple<string, string>("Телефон",
                                request.NullSafe(r => r.Agent.Phone.FormatEx("-", false))),
                            new Tuple<string, string>("Email",
                                request.NullSafe(r => r.Agent.Email.FormatEx("-", false))),
                            new Tuple<string, string>("Тип документа, удостоверяющего личность",
                                request.NullSafe(r => r.Agent.DocumentType.Name.FormatEx("-", false))),
                            new Tuple<string, string>("Серия и номер",
                                request.NullSafe(
                                    r => r.Agent.DocumentSeria.FormatEx("-", false) + " " +
                                         r.Agent.DocumentNumber.FormatEx("-", false))),
                            new Tuple<string, string>("Когда выдан документ",
                                request.NullSafe(r => r.Agent.DocumentDateOfIssue.FormatEx("dd.MM.yyyy", "-"))),
                            new Tuple<string, string>("Кем выдан документ",
                                request.NullSafe(r => r.Agent.DocumentSubjectIssue.FormatEx("-", false))),
                            new Tuple<string, string>("Представитель заявителя является сопровождающим",
                                (attendantAgent != null).FormatEx())
                        };

                        if (request.StatusApplicant == "4" && request.Child != null && request.Child.Any())
                        {
                            var name = request.RepresentInterest?.Name ?? request.Agent?.StatusByChild?.Name;

                            if (string.IsNullOrWhiteSpace(name))
                            {
                                if (request.IsFirstCompany && (
                                    request.TypeOfRestId == (long)TypeOfRestEnum.ChildRest ||
                                    request.TypeOfRestId == (long)TypeOfRestEnum.RestWithParents ||
                                    request.TypeOfRestId == (long)TypeOfRestEnum.RestWithParentsPoor ||
                                    request.TypeOfRestId == (long)TypeOfRestEnum.RestWithParentsComplex ||
                                    request.TypeOfRestId == (long)TypeOfRestEnum.RestWithParentsInvalid ||
                                    request.TypeOfRestId ==
                                    (long)TypeOfRestEnum.RestWithParentsInvalidOrphanComplex ||
                                    request.TypeOfRestId == (long)TypeOfRestEnum.ChildRestCamps ||
                                    request.TypeOfRestId == (long)TypeOfRestEnum.TentChildrenCamp ||
                                    request.TypeOfRestId == (long)TypeOfRestEnum.ChildRestFederalCamps))
                                {
                                    name = request?.Applicant?.Male == true ? "Отец" : "Мать";
                                }
                            }

                            if (!string.IsNullOrWhiteSpace(name))
                            {
                                block.Insert(0, new Tuple<string, string>("Представляет интересы", name));
                            }
                        }

                        AppendBlock(doc, "Сведения о представителе заявителя", block);

                        AppendBlock(doc, "Сведения о доверенности на подачу заявления", new List<Tuple<string, string>>
                        {
                            new Tuple<string, string>("Дата выдачи доверенности",
                                request.NullSafe(r => r.Agent.ProxyDateOfIssure.FormatEx("dd.MM.yyyy", "-"))),
                            new Tuple<string, string>("Дата окончания срока действия доверенности",
                                request.NullSafe(r => r.Agent.ProxyEndDate.FormatEx("dd.MM.yyyy", "-"))),
                            new Tuple<string, string>("ФИО нотариуса",
                                request.NullSafe(r => r.Agent.NotaryName.FormatEx("-", false))),
                            new Tuple<string, string>("Номер доверенности",
                                request.NullSafe(r => r.Agent.ProxyNumber.FormatEx("-", false)))
                        });

                        if (attendantAgent != null)
                        {
                            AppendBlock(doc, "Сведения о доверенности на сопровождение", new List<Tuple<string, string>>
                            {
                                new Tuple<string, string>("Дата выдачи доверенности",
                                    attendantAgent.NullSafe(r => r.ProxyDateOfIssure.FormatEx("-", false))),
                                new Tuple<string, string>("Дата окончания срока действия доверенности",
                                    attendantAgent.NullSafe(r => r.ProxyEndDate.FormatEx("-", false))),
                                new Tuple<string, string>("ФИО нотариуса",
                                    attendantAgent.NullSafe(r => r.NotaryName.FormatEx("-", false))),
                                new Tuple<string, string>("Номер доверенности",
                                    attendantAgent.NullSafe(r => r.ProxyNumber.FormatEx("-", false)))
                            });
                        }
                    }

                    if (request.Child != null && request.Child.Any(c => !c.IsDeleted))
                    {
                        AppendTitle(doc, "Сведения о детях");
                        foreach (var child in request.Child.Where(c => !c.IsDeleted).ToList())
                        {
                            AppendBlock(doc, "", new List<Tuple<string, string>>
                            {
                                new Tuple<string, string>("Фамилия",
                                    child.NullSafe(r => r.LastName.FormatEx("-", false))),
                                new Tuple<string, string>("Имя", child.NullSafe(r => r.FirstName.FormatEx("-", false))),
                                new Tuple<string, string>("Отчество",
                                    child.NullSafe(r => r.MiddleName.FormatEx("-", false))),
                                new Tuple<string, string>("Пол", child.NullSafe(r => r.Male) ? "Мужской" : "Женский"),
                                new Tuple<string, string>("Дата рождения",
                                    child.NullSafe(r => r.DateOfBirth.FormatEx("dd.MM.yyyy", "-"))),
                                new Tuple<string, string>("Место рождения",
                                    child.NullSafe(r => r.PlaceOfBirth.FormatEx("-", false))),
                                new Tuple<string, string>("Тип документа, удостоверяющего личность",
                                    child.NullSafe(r => r.DocumentType.Name.FormatEx("-", false))),
                                new Tuple<string, string>("Серия и номер",
                                    child.NullSafe(r =>
                                        r.DocumentSeria.FormatEx("-", false) + " " +
                                        r.DocumentNumber.FormatEx("-", false))),
                                new Tuple<string, string>("Когда выдан документ",
                                    child.NullSafe(r => r.DocumentDateOfIssue.FormatEx("dd.MM.yyyy", "-"))),
                                new Tuple<string, string>("Кем выдан документ",
                                    child.NullSafe(r => r.DocumentSubjectIssue.FormatEx("-", false))),
                                new Tuple<string, string>("СНИЛС", child.NullSafe(r => r.Snils.FormatEx("-", false)))
                            });
                            if (child.BenefitType != null)
                            {
                                AppendBlock(doc, "Сведения о категории льготы ребенка",
                                    new List<Tuple<string, string>>
                                    {
                                        new Tuple<string, string>("Категория",
                                            child.NullSafe(r => r.BenefitType.Name.FormatEx("-", false))),
                                        new Tuple<string, string>("Вид ограничения",
                                            child.NullSafe(r => r.TypeOfRestriction.Name.FormatEx("-", false))),
                                        new Tuple<string, string>("Подвид ограничения",
                                            child.NullSafe(r => r.TypeOfSubRestriction.Name.FormatEx("-", false))),
                                        new Tuple<string, string>("",
                                            child.NullSafe(r => r.BenefitAnswerComment.FormatEx("-", false)))
                                    }.Where(t => t.Item2 != "-").ToList());

                                if (child.BenefitType.NeedApproveDocument)
                                {
                                    AppendBlock(doc,
                                        "Документ, подтверждающий, что ребенок находится в трудной жизненной ситуации",
                                        new List<Tuple<string, string>>
                                        {
                                            new Tuple<string, string>("Кем выдан документ",
                                                child.NullSafe(r => r.BenefitSubjectIssue.FormatEx("-", false))),
                                            new Tuple<string, string>("Когда выдан документ",
                                                child.NullSafe(r => r.BenefitDateOfIssure.FormatEx("dd.MM.yyyy", "-"))),
                                            new Tuple<string, string>("Номер документа",
                                                child.NullSafe(r => r.BenefitNumber.FormatEx("-", false)))
                                        });
                                }
                            }

                            if (!request.IsFirstCompany)
                            {
                                if (child.School != null && !child.SchoolNotPresent)
                                {
                                    AppendBlock(doc, "Образовательное учреждение", new List<Tuple<string, string>>
                                    {
                                        new Tuple<string, string>("",
                                            child.NullSafe(r => r.School.Name.FormatEx("-", false)))
                                    });
                                }

                                if (child.SchoolNotPresent)
                                {
                                    AppendBlock(doc, "Образовательное учреждение", new List<Tuple<string, string>>
                                    {
                                        new Tuple<string, string>("", "Нет в списке")
                                    });
                                }
                            }

                            if (request.NullSafe(r => r.PlaceOfRest.IsForegin))
                            {
                                AppendBlock(doc, "Документ удостоверяющий личность ребенка за рубежом",
                                    new List<Tuple<string, string>>
                                    {
                                        new Tuple<string, string>("Фамилия",
                                            child.NullSafe(r => r.ForeginLastName.FormatEx("-", false))),
                                        new Tuple<string, string>("Имя",
                                            child.NullSafe(r => r.ForeginName.FormatEx("-", false))),
                                        new Tuple<string, string>(
                                            "Тип документа, удостоверяющего личность ребенка за рубежом",
                                            child.NullSafe(r => r.ForeginType.Name.FormatEx("-", false))),
                                        new Tuple<string, string>("Серия и номер",
                                            child.NullSafe(r =>
                                                r.ForeginSeria.FormatEx("-", false) + " " +
                                                r.ForeginNumber.FormatEx("-", false))),
                                        new Tuple<string, string>("Дата выдачи",
                                            child.NullSafe(r => r.ForeginDateOfIssue.FormatEx("dd.MM.yyyy", "-"))),
                                        new Tuple<string, string>("Срок действия",
                                            child.NullSafe(r => r.ForeginDateEnd.FormatEx("dd.MM.yyyy", "-"))),
                                        new Tuple<string, string>("Кем выдан",
                                            child.NullSafe(r => r.ForeginSubjectIssue.FormatEx("-", false)))
                                    });
                            }

                            if (child?.Address?.BtiAddress != null)
                            {
                                AppendBlock(
                                    doc,
                                    "Адрес регистрации",
                                    new List<Tuple<string, string>>
                                    {
                                        new Tuple<string, string>("Адрес",
                                            child.Address.ToString().FormatEx("-", false))
                                    });
                            }
                            else if (!string.IsNullOrWhiteSpace(child?.Address?.Appartment)
                                     || !string.IsNullOrWhiteSpace(child?.Address?.Street)
                                     || !string.IsNullOrWhiteSpace(child?.Address?.House)
                                     || !string.IsNullOrWhiteSpace(child?.Address?.Corpus)
                                     || !string.IsNullOrWhiteSpace(child?.Address?.Vladenie)
                                     || !string.IsNullOrWhiteSpace(child?.Address?.Stroenie))
                            {
                                AppendBlock(
                                    doc,
                                    "Адрес регистрации",
                                    new List<Tuple<string, string>>
                                    {
                                        new Tuple<string, string>("Адрес",
                                            (child.Address?.ToString()).FormatEx("-", false))
                                    });
                            }

                            if (child.ApplicantId.HasValue)
                            {
                                AppendBlock(
                                    doc,
                                    "Сопровождающий",
                                    new List<Tuple<string, string>>
                                    {
                                        new Tuple<string, string>("ФИО",
                                            $"{child.Applicant.LastName.FormatEx("-", false)} {child.Applicant.FirstName.FormatEx("-", false)} {child.Applicant.MiddleName.FormatEx("-", false)}"),
                                        new Tuple<string, string>("Статус по отношению к ребенку",
                                            (child.StatusByChild?.Name).FormatEx("-", false))
                                    });
                            }

                            if (request.TypeOfRestId == (long)TypeOfRestEnum.Compensation ||
                                request.TypeOfRestId == (long)TypeOfRestEnum.CompensationYouthRest)
                            {
                                AppendBlock(
                                    doc,
                                    "Путевка",
                                    new List<Tuple<string, string>>
                                    {
                                        new Tuple<string, string>("Путевка",
                                            $"{child.RequestInformationVoucher.OrganizationName.FormatEx("-", false)} - {child.RequestInformationVoucher.DateFrom.FormatEx("-", false)}"),
                                        new Tuple<string, string>("Стоимость путевки (руб)",
                                            child.CostOfTour.FormatEx()),
                                        new Tuple<string, string>("Стоимость проезда (руб)",
                                            child.CostOfRide.FormatEx())
                                    });
                            }
                        }
                    }

                    if (request.Attendant != null && request.Attendant.Any(a => !a.IsAgent))
                    {
                        AppendBlock(doc, "Сведения о сопровождающих", null);
                        foreach (var attendant in request.Attendant.Where(a => !a.IsAgent)?.ToList() ??
                                                  new List<Applicant>())
                        {
                            var block = new List<Tuple<string, string>>
                            {
                                new Tuple<string, string>("Является доверенным лицом", attendant.IsProxy.FormatEx()),
                                new Tuple<string, string>("Фамилия",
                                    attendant.NullSafe(r => r.LastName.FormatEx("-", false))),
                                new Tuple<string, string>("Имя",
                                    attendant.NullSafe(r => r.FirstName.FormatEx("-", false))),
                                new Tuple<string, string>("Отчество",
                                    attendant.NullSafe(r => r.MiddleName.FormatEx("-", false))),
                                new Tuple<string, string>("Пол", attendant?.Male ?? false ? "Мужской" : "Женский"),
                                new Tuple<string, string>("СНИЛС", (attendant?.Snils).FormatEx("-", false)),
                                new Tuple<string, string>("Телефон",
                                    attendant.NullSafe(r => r.Phone.FormatEx("-", false))),
                                new Tuple<string, string>("Дата рождения",
                                    attendant.NullSafe(r => r.DateOfBirth.FormatEx("dd.MM.yyyy", "-"))),
                                new Tuple<string, string>("Место рождения",
                                    attendant.NullSafe(r => r.PlaceOfBirth.FormatEx("-", false))),
                                new Tuple<string, string>("Email",
                                    attendant.NullSafe(r => r.Email.FormatEx("-", false))),
                                new Tuple<string, string>("Тип документа, удостоверяющего личность",
                                    attendant.NullSafe(r => r.DocumentType.Name.FormatEx("-", false))),
                                new Tuple<string, string>("Серия и номер",
                                    attendant.NullSafe(r =>
                                        r.DocumentSeria.FormatEx("-", false) + " " +
                                        r.DocumentNumber.FormatEx("-", false))),
                                new Tuple<string, string>("Когда выдан документ",
                                    attendant.NullSafe(r => r.DocumentDateOfIssue.FormatEx("dd.MM.yyyy", "-"))),
                                new Tuple<string, string>("Кем выдан документ",
                                    attendant.NullSafe(r => r.DocumentSubjectIssue.FormatEx("-", false))),
                                new Tuple<string, string>("Статус по отношению к ребёнку",
                                    attendant.ApplicantType?.Name?.FormatEx("-", false))
                            };

                            AppendBlock(doc, "", block);
                            if (attendant.IsProxy)
                            {
                                AppendBlock(doc, "Сведения о доверенности на сопровождение",
                                    new List<Tuple<string, string>>
                                    {
                                        new Tuple<string, string>("Дата выдачи доверенности",
                                            attendant.NullSafe(r => r.ProxyDateOfIssure.FormatEx("-", false))),
                                        new Tuple<string, string>("Дата окончания срока действия доверенности",
                                            attendant.NullSafe(r => r.ProxyEndDate.FormatEx("-", false))),
                                        new Tuple<string, string>("ФИО нотариуса",
                                            attendant.NullSafe(r => r.NotaryName.FormatEx("-", false))),
                                        new Tuple<string, string>("Номер доверенности",
                                            attendant.NullSafe(r => r.ProxyNumber.FormatEx("-", false)))
                                    });
                            }
                        }
                    }

                    AppendFooterNotificationOfRegistration(doc);
                }

                return File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                    (request.RequestNumber ?? id.ToString(CultureInfo.InvariantCulture)) + ".docx");
            }
        }

        /// <summary>
        ///     Наименование заявителя
        /// </summary>
        internal static string GetStatusApplicantName(string statusApplicant)
        {
            switch (statusApplicant)
            {
                case "1":
                    statusApplicant = "Отец";
                    break;
                case "2":
                    statusApplicant = "Мать";
                    break;
                case "3":
                    statusApplicant = "Законный представитель";
                    break;
                case "4":
                    statusApplicant = "Доверенное лицо";
                    break;
                case "5":
                    statusApplicant = "Лицо из числа детей-сирот и детей, оставшихся без попечения родителей";
                    break;
            }

            return statusApplicant;
        }

        /// <summary>
        ///     добавление блока
        /// </summary>
        private static void AppendBlock(Document doc, string title, IEnumerable<Tuple<string, string>> items)
        {
            var titleParagraphProperties = new ParagraphProperties();
            var blockTitleRunProperties = new RunProperties();
            blockTitleRunProperties.AppendChild(new RunFonts
            {
                Ascii = "Times New Roman",
                HighAnsi = "Times New Roman",
                ComplexScript = "Times New Roman"
            });
            blockTitleRunProperties.AppendChild(new FontSize { Val = "28" });
            blockTitleRunProperties.AppendChild(new Bold());
            titleParagraphProperties.AppendChild(new SpacingBetweenLines
            {
                Before = "300",
                After = "100",
                LineRule = LineSpacingRuleValues.Exact
            });
            titleParagraphProperties.AppendChild(new KeepNext());

            doc.AppendChild(new Paragraph(titleParagraphProperties, new Run(blockTitleRunProperties, new Text(title))));

            var table = new Table();

            var tblProp = new TableProperties(
                new TableBorders(
                    new TopBorder { Val = new EnumValue<BorderValues>(BorderValues.None) },
                    new BottomBorder { Val = new EnumValue<BorderValues>(BorderValues.None) },
                    new LeftBorder { Val = new EnumValue<BorderValues>(BorderValues.None) },
                    new RightBorder { Val = new EnumValue<BorderValues>(BorderValues.None) },
                    new InsideHorizontalBorder { Val = new EnumValue<BorderValues>(BorderValues.None) },
                    new InsideVerticalBorder { Val = new EnumValue<BorderValues>(BorderValues.None) }));

            table.AppendChild(tblProp);

            if (items != null)
            {
                foreach (var item in items)
                {
                    var recordTitle = item.Item1;
                    var recordVal = item.Item2;
                    var row = new TableRow();
                    var cell1 = new TableCell();
                    var cell2 = new TableCell();
                    var run1Properties = new RunProperties();
                    var run2Properties = new RunProperties();

                    run1Properties.AppendChild(new RunFonts
                    {
                        Ascii = "Times New Roman",
                        HighAnsi = "Times New Roman",
                        ComplexScript = "Times New Roman"
                    });
                    run1Properties.AppendChild(new FontSize { Val = "24" });

                    run2Properties.AppendChild(new RunFonts
                    {
                        Ascii = "Times New Roman",
                        HighAnsi = "Times New Roman",
                        ComplexScript = "Times New Roman"
                    });
                    run2Properties.AppendChild(new FontSize { Val = "24" });
                    run2Properties.AppendChild(new Bold());

                    cell1.AppendChild(new TableCellProperties(
                        new TableCellWidth { Type = TableWidthUnitValues.Dxa, Width = "3500" }));
                    cell1.AppendChild(new Paragraph(new Run(run1Properties, new Text(recordTitle))));
                    cell2.AppendChild(new Paragraph(new ParagraphProperties(new Indentation { Left = "500" }),
                        new Run(run2Properties, new Text(recordVal))));

                    row.AppendChild(cell1);
                    row.AppendChild(cell2);
                    table.AppendChild(row);
                }

                doc.AppendChild(table);
            }
        }

        /// <summary>
        ///     добавление заглавия
        /// </summary>
        private static void AppendTitle(Document doc, string title)
        {
            var titleParagraphProperties = new ParagraphProperties();
            var blockTitleRunProperties = new RunProperties();
            blockTitleRunProperties.AppendChild(new RunFonts
            {
                Ascii = "Times New Roman",
                HighAnsi = "Times New Roman",
                ComplexScript = "Times New Roman"
            });
            blockTitleRunProperties.AppendChild(new FontSize { Val = "28" });
            blockTitleRunProperties.AppendChild(new Bold());
            titleParagraphProperties.AppendChild(new SpacingBetweenLines
            {
                Before = "300",
                LineRule = LineSpacingRuleValues.Exact
            });
            titleParagraphProperties.AppendChild(new KeepNext());

            doc.AppendChild(new Paragraph(titleParagraphProperties, new Run(blockTitleRunProperties, new Text(title))));

            var table = new Table();

            var tblProp = new TableProperties(
                new TableBorders(
                    new TopBorder { Val = new EnumValue<BorderValues>(BorderValues.None) },
                    new BottomBorder { Val = new EnumValue<BorderValues>(BorderValues.None) },
                    new LeftBorder { Val = new EnumValue<BorderValues>(BorderValues.None) },
                    new RightBorder { Val = new EnumValue<BorderValues>(BorderValues.None) },
                    new InsideHorizontalBorder { Val = new EnumValue<BorderValues>(BorderValues.None) },
                    new InsideVerticalBorder { Val = new EnumValue<BorderValues>(BorderValues.None) }));

            table.AppendChild(tblProp);
        }

        /// <summary>
        ///     добавление заголовка
        /// </summary>
        private static void AppendHeader(Document doc)
        {
            var titleParagraphProperties = new ParagraphProperties();
            var blockTitleRunProperties = new RunProperties();
            blockTitleRunProperties.AppendChild(new RunFonts
            {
                Ascii = "Times New Roman",
                HighAnsi = "Times New Roman",
                ComplexScript = "Times New Roman"
            });
            blockTitleRunProperties.AppendChild(new FontSize { Val = "24" });
            titleParagraphProperties.AppendChild(new Justification { Val = JustificationValues.Center });
            titleParagraphProperties.AppendChild(new Indentation { Left = "6000" });

            doc.AppendChild(new Paragraph(titleParagraphProperties,
                new Run(blockTitleRunProperties,
                    new Text(
                        "Государственному автономному учреждению культуры города Москвы «Московское агентство организации отдыха и туризма»"))));


            doc.AppendChild(new Paragraph(new ParagraphProperties(new Justification { Val = JustificationValues.Center }),
                new Run(MakeTitleRequestRunProperties(),
                    new Text(
                        "Заявление о предоставлении услуг отдыха и оздоровления (в части сведений, заполняемых в период проведения первого этапа заявочной кампании в целях организации отдыха и оздоровления)"))));
        }

        private static void AppendFooterNotificationOfRegistration(Document doc)
        {
            var assembly = Assembly.Load("RestChild.Templates");
            var resourceName = "RestChild.Templates.NotificationRegistrationFooter.docx";
            var chunk = doc.MainDocumentPart.AddAlternativeFormatImportPart(
                AlternativeFormatImportPartType.WordprocessingML, "AltChunkIdFooter");
            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                chunk.FeedData(stream);
            }

            var altChunk = new AltChunk { Id = "AltChunkIdFooter" };
            doc.InsertAfter(altChunk, doc.Elements().Last());
        }

        private static RunProperties MakeTitleRequestRunProperties()
        {
            var titleRequestRunProperties = new RunProperties();
            titleRequestRunProperties.AppendChild(new RunFonts
            {
                Ascii = "Times New Roman",
                HighAnsi = "Times New Roman",
                ComplexScript = "Times New Roman"
            });
            titleRequestRunProperties.AppendChild(new FontSize { Val = "28" });
            titleRequestRunProperties.AppendChild(new Bold());
            return titleRequestRunProperties;
        }

        private void SignSimpleBlockNotification(Document doc)
        {
            var account = Security.GetCurrentAccount() ?? new Account();
            var titleRequestRunProperties = new RunProperties();
            titleRequestRunProperties.AppendChild(new RunFonts
            {
                Ascii = "Times New Roman",
                HighAnsi = "Times New Roman",
                ComplexScript = "Times New Roman"
            });
            titleRequestRunProperties.AppendChild(new FontSize { Val = "24" });

            var tblProp = new TableProperties(
                new TableBorders(
                    new TopBorder { Val = new EnumValue<BorderValues>(BorderValues.None) },
                    new BottomBorder { Val = new EnumValue<BorderValues>(BorderValues.None) },
                    new LeftBorder { Val = new EnumValue<BorderValues>(BorderValues.None) },
                    new RightBorder { Val = new EnumValue<BorderValues>(BorderValues.None) },
                    new InsideHorizontalBorder { Val = new EnumValue<BorderValues>(BorderValues.None) },
                    new InsideVerticalBorder { Val = new EnumValue<BorderValues>(BorderValues.None) }));
            var captionRunProperties = new RunProperties().SetFont().SetFontSize("16");

            var table = new Table();
            table.AppendChild(tblProp.CloneNode(true));

            var row = new TableRow();

            var cell = new TableCell();
            cell.Append(new TableCellProperties(
                    new TableCellWidth { Type = TableWidthUnitValues.Dxa, Width = "2731" }),
                new TableCellVerticalAlignment { Val = TableVerticalAlignmentValues.Bottom });
            cell.AppendChild(new Paragraph(new ParagraphProperties(new Justification { Val = JustificationValues.Left }),
                new Run(titleRequestRunProperties.CloneNode(true),
                    new Text("Уведомление выдал:"))));
            row.AppendChild(cell);


            cell = new TableCell();
            cell.AppendChild(new TableCellProperties(
                new TableCellWidth { Type = TableWidthUnitValues.Dxa, Width = "5717" },
                new TableCellBorders(new BottomBorder { Val = new EnumValue<BorderValues>(BorderValues.Single) }),
                new TableCellVerticalAlignment { Val = TableVerticalAlignmentValues.Bottom }
            ));
            cell.AppendChild(new Paragraph(
                new ParagraphProperties(new Justification { Val = JustificationValues.Center }),
                new Run(titleRequestRunProperties.CloneNode(true),
                    new Text(account.Name.FormatEx() +
                             (string.IsNullOrWhiteSpace(account.Position) ? "" : $", {account.Position}")))));
            row.AppendChild(cell);

            cell = new TableCell();
            cell.Append(new TableCellProperties(
                    new TableCellWidth { Type = TableWidthUnitValues.Dxa, Width = "55" }),
                new TableCellVerticalAlignment { Val = TableVerticalAlignmentValues.Bottom });
            cell.AppendChild(new Paragraph(
                new ParagraphProperties(new Justification { Val = JustificationValues.Center }),
                new Run(titleRequestRunProperties.CloneNode(true),
                    new Text(" "))));
            row.AppendChild(cell);

            cell = new TableCell();
            cell.AppendChild(new TableCellProperties(
                new TableCellWidth { Type = TableWidthUnitValues.Dxa, Width = "1804" },
                new TableCellBorders(new BottomBorder { Val = new EnumValue<BorderValues>(BorderValues.Single) }),
                new TableCellVerticalAlignment { Val = TableVerticalAlignmentValues.Bottom }));
            cell.AppendChild(new Paragraph(
                new ParagraphProperties(new Justification { Val = JustificationValues.Center }),
                new Run(titleRequestRunProperties.CloneNode(true),
                    new Text(DateTime.Now.Date.FormatEx()))));
            row.AppendChild(cell);

            table.AppendChild(row);
            // -----------------------------------------------------------
            row = new TableRow();

            cell = new TableCell();
            cell.Append(new TableCellProperties(
                    new TableCellWidth { Type = TableWidthUnitValues.Dxa, Width = "2731" }),
                new TableCellVerticalAlignment { Val = TableVerticalAlignmentValues.Bottom });
            cell.AppendChild(new Paragraph(
                new ParagraphProperties(new Justification { Val = JustificationValues.Center }),
                new Run(titleRequestRunProperties.CloneNode(true),
                    new Text(" "))));
            row.AppendChild(cell);


            cell = new TableCell();
            cell.Append(new TableCellProperties(
                    new TableCellWidth { Type = TableWidthUnitValues.Dxa, Width = "5717" }),
                new TableCellVerticalAlignment { Val = TableVerticalAlignmentValues.Bottom });
            cell.AppendChild(new Paragraph(
                new ParagraphProperties(new Justification { Val = JustificationValues.Center }),
                new Run(captionRunProperties.CloneNode(true), new Text("(ФИО работника, должность)"))));
            row.AppendChild(cell);

            cell = new TableCell();
            cell.Append(new TableCellProperties(
                    new TableCellWidth { Type = TableWidthUnitValues.Dxa, Width = "55" }),
                new TableCellVerticalAlignment { Val = TableVerticalAlignmentValues.Bottom });
            cell.AppendChild(new Paragraph(new Run(new Text(" "))));
            row.AppendChild(cell);

            cell = new TableCell();
            cell.Append(new TableCellProperties(
                    new TableCellWidth { Type = TableWidthUnitValues.Dxa, Width = "1804" }),
                new TableCellVerticalAlignment { Val = TableVerticalAlignmentValues.Bottom });
            cell.AppendChild(new Paragraph(
                new ParagraphProperties(new Justification { Val = JustificationValues.Center }),
                new Run(titleRequestRunProperties.CloneNode(true), captionRunProperties.CloneNode(true),
                    new Text("(дата)"))));
            row.AppendChild(cell);

            table.AppendChild(row);

            doc.AppendChild(table);
        }

        private static void SignBlockNotification(Document doc, string applicantName, bool notificationAccepted = true)
        {
            var account = Security.GetCurrentAccount() ?? new Account();
            var titleRequestRunProperties = new RunProperties();
            titleRequestRunProperties.AppendChild(new RunFonts
            {
                Ascii = "Times New Roman",
                HighAnsi = "Times New Roman",
                ComplexScript = "Times New Roman"
            });
            titleRequestRunProperties.AppendChild(new FontSize { Val = "24" });

            var tblProp = new TableProperties(
                new TableBorders(
                    new TopBorder { Val = new EnumValue<BorderValues>(BorderValues.None) },
                    new BottomBorder { Val = new EnumValue<BorderValues>(BorderValues.None) },
                    new LeftBorder { Val = new EnumValue<BorderValues>(BorderValues.None) },
                    new RightBorder { Val = new EnumValue<BorderValues>(BorderValues.None) },
                    new InsideHorizontalBorder { Val = new EnumValue<BorderValues>(BorderValues.None) },
                    new InsideVerticalBorder { Val = new EnumValue<BorderValues>(BorderValues.None) }));
            var captionRunProperties = new RunProperties().SetFont().SetFontSize("16");

            var table = new Table();
            table.AppendChild(tblProp.CloneNode(true));

            var row = new TableRow();

            var cell = new TableCell();
            cell.Append(new TableCellProperties(
                    new TableCellWidth { Type = TableWidthUnitValues.Dxa, Width = "2731" }),
                new TableCellVerticalAlignment { Val = TableVerticalAlignmentValues.Bottom });
            cell.AppendChild(new Paragraph(new ParagraphProperties(new Justification { Val = JustificationValues.Left }),
                new Run(titleRequestRunProperties.CloneNode(true),
                    new Text("Уведомление выдал:"))));
            row.AppendChild(cell);


            cell = new TableCell();
            cell.AppendChild(new TableCellProperties(
                new TableCellWidth { Type = TableWidthUnitValues.Dxa, Width = "2731" },
                new TableCellBorders(new BottomBorder { Val = new EnumValue<BorderValues>(BorderValues.Single) }),
                new TableCellVerticalAlignment { Val = TableVerticalAlignmentValues.Bottom }
            ));
            cell.AppendChild(new Paragraph(
                new ParagraphProperties(new Justification { Val = JustificationValues.Center }),
                new Run(titleRequestRunProperties.CloneNode(true),
                    new Text(account.Name.FormatEx()))));
            row.AppendChild(cell);

            cell = new TableCell();
            cell.Append(new TableCellProperties(
                    new TableCellWidth { Type = TableWidthUnitValues.Dxa, Width = "55" }),
                new TableCellVerticalAlignment { Val = TableVerticalAlignmentValues.Bottom });
            cell.AppendChild(new Paragraph(new Run(new Text(" "))));
            row.AppendChild(cell);

            cell = new TableCell();
            cell.Append(new TableCellProperties(
                    new TableCellWidth { Type = TableWidthUnitValues.Dxa, Width = "2931" },
                    new TableCellBorders(new BottomBorder { Val = new EnumValue<BorderValues>(BorderValues.Single) })),
                new TableCellVerticalAlignment { Val = TableVerticalAlignmentValues.Bottom });
            cell.AppendChild(new Paragraph(new Run(new Text(" "))));
            row.AppendChild(cell);

            cell = new TableCell();
            cell.Append(new TableCellProperties(
                    new TableCellWidth { Type = TableWidthUnitValues.Dxa, Width = "55" }),
                new TableCellVerticalAlignment { Val = TableVerticalAlignmentValues.Bottom });
            cell.AppendChild(new Paragraph(
                new ParagraphProperties(new Justification { Val = JustificationValues.Center }),
                new Run(titleRequestRunProperties.CloneNode(true),
                    new Text(" "))));
            row.AppendChild(cell);

            cell = new TableCell();
            cell.AppendChild(new TableCellProperties(
                new TableCellWidth { Type = TableWidthUnitValues.Dxa, Width = "1804" },
                new TableCellBorders(new BottomBorder { Val = new EnumValue<BorderValues>(BorderValues.Single) }),
                new TableCellVerticalAlignment { Val = TableVerticalAlignmentValues.Bottom }));
            cell.AppendChild(new Paragraph(
                new ParagraphProperties(new Justification { Val = JustificationValues.Center }),
                new Run(titleRequestRunProperties.CloneNode(true),
                    new Text(DateTime.Now.Date.FormatEx()))));
            row.AppendChild(cell);

            table.AppendChild(row);
            // -----------------------------------------------------------
            row = new TableRow();

            cell = new TableCell();
            cell.Append(new TableCellProperties(
                    new TableCellWidth { Type = TableWidthUnitValues.Dxa, Width = "2731" }),
                new TableCellVerticalAlignment { Val = TableVerticalAlignmentValues.Bottom });
            cell.AppendChild(new Paragraph(
                new ParagraphProperties(new Justification { Val = JustificationValues.Center }),
                new Run(titleRequestRunProperties.CloneNode(true),
                    new Text(" "))));
            row.AppendChild(cell);


            cell = new TableCell();
            cell.Append(new TableCellProperties(
                    new TableCellWidth { Type = TableWidthUnitValues.Dxa, Width = "2731" }),
                new TableCellVerticalAlignment { Val = TableVerticalAlignmentValues.Bottom });
            cell.AppendChild(new Paragraph(
                new ParagraphProperties(new Justification { Val = JustificationValues.Center }),
                new Run(captionRunProperties.CloneNode(true), new Text("(Ф.И.О. работника)"))));
            row.AppendChild(cell);

            cell = new TableCell();
            cell.Append(new TableCellProperties(
                    new TableCellWidth { Type = TableWidthUnitValues.Dxa, Width = "55" }),
                new TableCellVerticalAlignment { Val = TableVerticalAlignmentValues.Bottom });
            cell.AppendChild(new Paragraph(new Run(new Text(" "))));
            row.AppendChild(cell);

            cell = new TableCell();
            cell.Append(new TableCellProperties(
                    new TableCellWidth { Type = TableWidthUnitValues.Dxa, Width = "2931" }),
                new TableCellVerticalAlignment { Val = TableVerticalAlignmentValues.Bottom });
            cell.AppendChild(new Paragraph(
                new ParagraphProperties(new Justification { Val = JustificationValues.Center }),
                new Run(titleRequestRunProperties.CloneNode(true), captionRunProperties.CloneNode(true),
                    new Text("(подпись)"))));
            row.AppendChild(cell);

            cell = new TableCell();
            cell.Append(new TableCellProperties(
                    new TableCellWidth { Type = TableWidthUnitValues.Dxa, Width = "55" }),
                new TableCellVerticalAlignment { Val = TableVerticalAlignmentValues.Bottom });
            cell.AppendChild(new Paragraph(new Run(new Text(" "))));
            row.AppendChild(cell);

            cell = new TableCell();
            cell.Append(new TableCellProperties(
                    new TableCellWidth { Type = TableWidthUnitValues.Dxa, Width = "1804" }),
                new TableCellVerticalAlignment { Val = TableVerticalAlignmentValues.Bottom });
            cell.AppendChild(new Paragraph(
                new ParagraphProperties(new Justification { Val = JustificationValues.Center }),
                new Run(titleRequestRunProperties.CloneNode(true), captionRunProperties.CloneNode(true),
                    new Text("(дата)"))));
            row.AppendChild(cell);

            table.AppendChild(row);

            doc.AppendChild(table);

            if (notificationAccepted)
            {
                table = new Table();
                //---------------------------------------------------------------------------
                row = new TableRow();

                table.AppendChild(tblProp.CloneNode(true));

                cell = new TableCell();
                cell.Append(new TableCellProperties(
                        new TableCellWidth { Type = TableWidthUnitValues.Dxa, Width = "2731" }),
                    new TableCellVerticalAlignment { Val = TableVerticalAlignmentValues.Bottom });
                cell.AppendChild(new Paragraph(
                    new ParagraphProperties(new Justification { Val = JustificationValues.Left }),
                    new Run(titleRequestRunProperties.CloneNode(true),
                        new Text("Уведомление принял:"))));
                row.AppendChild(cell);

                cell = new TableCell();
                cell.Append(new TableCellProperties(
                        new TableCellWidth { Type = TableWidthUnitValues.Dxa, Width = "2731" },
                        new TableCellBorders(new BottomBorder
                        { Val = new EnumValue<BorderValues>(BorderValues.Single) })),
                    new TableCellVerticalAlignment { Val = TableVerticalAlignmentValues.Bottom });
                cell.AppendChild(new Paragraph(
                    new ParagraphProperties(new Justification { Val = JustificationValues.Center }),
                    new Run(titleRequestRunProperties.CloneNode(true),
                        new Text(string.IsNullOrWhiteSpace(applicantName) ? "  " : applicantName))));
                row.AppendChild(cell);


                cell = new TableCell();
                cell.Append(new TableCellProperties(
                        new TableCellWidth { Type = TableWidthUnitValues.Dxa, Width = "55" }),
                    new TableCellVerticalAlignment { Val = TableVerticalAlignmentValues.Bottom });
                cell.AppendChild(new Paragraph(new Run(new Text(" "))));
                row.AppendChild(cell);

                cell = new TableCell();
                cell.Append(new TableCellProperties(
                        new TableCellWidth { Type = TableWidthUnitValues.Dxa, Width = "2931" },
                        new TableCellBorders(new BottomBorder
                        { Val = new EnumValue<BorderValues>(BorderValues.Single) })),
                    new TableCellVerticalAlignment { Val = TableVerticalAlignmentValues.Bottom });
                cell.AppendChild(new Paragraph(
                    new ParagraphProperties(new Justification { Val = JustificationValues.Left }),
                    new Run(titleRequestRunProperties.CloneNode(true),
                        new Text(" "))));
                row.AppendChild(cell);

                cell = new TableCell();
                cell.Append(new TableCellProperties(
                        new TableCellWidth { Type = TableWidthUnitValues.Dxa, Width = "55" }),
                    new TableCellVerticalAlignment { Val = TableVerticalAlignmentValues.Bottom });
                cell.AppendChild(new Paragraph(new Run(new Text(" "))));
                row.AppendChild(cell);

                cell = new TableCell();
                cell.Append(new TableCellProperties(
                        new TableCellWidth { Type = TableWidthUnitValues.Dxa, Width = "1804" },
                        new TableCellBorders(new BottomBorder
                        { Val = new EnumValue<BorderValues>(BorderValues.Single) })),
                    new TableCellVerticalAlignment { Val = TableVerticalAlignmentValues.Bottom });
                cell.AppendChild(new Paragraph(
                    new ParagraphProperties(new Justification { Val = JustificationValues.Center }),
                    new Run(titleRequestRunProperties.CloneNode(true),
                        new Text(DateTime.Now.Date.FormatEx()))));
                row.AppendChild(cell);

                table.AppendChild(row);

                // --------------------------------------------------------------------------------------
                row = new TableRow();

                cell = new TableCell();
                cell.Append(new TableCellProperties(
                        new TableCellWidth { Type = TableWidthUnitValues.Dxa, Width = "2731" }),
                    new TableCellVerticalAlignment { Val = TableVerticalAlignmentValues.Bottom });
                cell.AppendChild(new Paragraph(
                    new ParagraphProperties(new Justification { Val = JustificationValues.Center }),
                    new Run(titleRequestRunProperties.CloneNode(true),
                        new Text(" "))));
                row.AppendChild(cell);

                cell = new TableCell();
                cell.Append(new TableCellProperties(
                        new TableCellWidth { Type = TableWidthUnitValues.Dxa, Width = "2731" }),
                    new TableCellVerticalAlignment { Val = TableVerticalAlignmentValues.Bottom });
                cell.AppendChild(new Paragraph(
                    new ParagraphProperties(new Justification { Val = JustificationValues.Center }),
                    new Run(captionRunProperties.CloneNode(true), new Text("(Ф.И.О. заявителя)"))));
                row.AppendChild(cell);


                cell = new TableCell();
                cell.Append(new TableCellProperties(
                        new TableCellWidth { Type = TableWidthUnitValues.Dxa, Width = "55" }),
                    new TableCellVerticalAlignment { Val = TableVerticalAlignmentValues.Bottom });
                cell.AppendChild(new Paragraph(new Run(new Text(" "))));
                row.AppendChild(cell);

                cell = new TableCell();
                cell.Append(new TableCellProperties(
                        new TableCellWidth { Type = TableWidthUnitValues.Dxa, Width = "2931" }),
                    new TableCellVerticalAlignment { Val = TableVerticalAlignmentValues.Bottom });
                cell.AppendChild(new Paragraph(
                    new ParagraphProperties(new Justification { Val = JustificationValues.Center }),
                    new Run(captionRunProperties.CloneNode(true), new Text("(подпись)"))));
                row.AppendChild(cell);

                cell = new TableCell();
                cell.Append(new TableCellProperties(
                        new TableCellWidth { Type = TableWidthUnitValues.Dxa, Width = "55" }),
                    new TableCellVerticalAlignment { Val = TableVerticalAlignmentValues.Bottom });
                cell.AppendChild(new Paragraph(new Run(new Text(" "))));
                row.AppendChild(cell);

                cell = new TableCell();
                cell.Append(new TableCellProperties(
                        new TableCellWidth { Type = TableWidthUnitValues.Dxa, Width = "1804" }),
                    new TableCellVerticalAlignment { Val = TableVerticalAlignmentValues.Bottom });
                cell.AppendChild(new Paragraph(
                    new ParagraphProperties(new Justification { Val = JustificationValues.Center }),
                    new Run(captionRunProperties.CloneNode(true), new Text("(дата)"))));
                row.AppendChild(cell);

                table.AppendChild(row);

                doc.AppendChild(table);
            }
        }

        private void DocumentHeader(Document doc, string requestNumber)
        {
            var titleProp = new RunProperties().SetFont().SetFontSize("28").Bold();
            doc.AppendChild(new Paragraph(new ParagraphProperties(new Justification { Val = JustificationValues.Center }),
                new Run(titleProp.CloneNode(true), new Text("ДЕПАРТАМЕНТ КУЛЬТУРЫ ГОРОДА МОСКВЫ"))));
            doc.AppendChild(new Paragraph(new ParagraphProperties(new Justification { Val = JustificationValues.Center }),
                new Run(titleProp.CloneNode(true),
                    new Text("Государственное автономное учреждение культуры города Москвы"))));
            doc.AppendChild(new Paragraph(new ParagraphProperties(new Justification { Val = JustificationValues.Center }),
                new Run(titleProp.CloneNode(true), new Text("\"Московское агентство организации отдыха и туризма\""))));
            var pp = new ParagraphProperties(new Justification { Val = JustificationValues.Center });
            pp.AppendChild(new ParagraphBorders().BottomBorder("000000", 3));
            doc.AppendChild(new Paragraph(pp, new Run(titleProp.CloneNode(true), new Text("(ГАУК «МОСГОРТУР»)"))));
            var titleRequestRunProperties = new RunProperties().SetFont().SetFontSize();

            doc.AppendChild(
                new Paragraph(
                    new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                        new SpacingBetweenLines { After = "20" }),
                    new Run(titleRequestRunProperties.CloneNode(true),
                        new Text("№ " + requestNumber))));
        }

        private static void DocumentHeaderRegistration(Document doc)
        {
            const string spacingBetweenLinesAfter = "20";
            const string spacingBetweenLinesLine = "240";

            var titleProp = new RunProperties().SetFont().SetFontSize().Bold();
            doc.AppendChild(new Paragraph(new ParagraphProperties(new Justification { Val = JustificationValues.Center },
                    new SpacingBetweenLines { After = spacingBetweenLinesAfter, Line = spacingBetweenLinesLine }),
                new Run(titleProp.CloneNode(true), new Text("ДЕПАРТАМЕНТ КУЛЬТУРЫ ГОРОДА МОСКВЫ"))));
            doc.AppendChild(new Paragraph(new ParagraphProperties(new Justification { Val = JustificationValues.Center },
                    new SpacingBetweenLines { After = spacingBetweenLinesAfter, Line = spacingBetweenLinesLine }),
                new Run(titleProp.CloneNode(true),
                    new Text("Государственное автономное учреждение культуры города Москвы"))));
            doc.AppendChild(new Paragraph(new ParagraphProperties(new Justification { Val = JustificationValues.Center },
                    new SpacingBetweenLines { After = spacingBetweenLinesAfter, Line = spacingBetweenLinesLine }),
                new Run(titleProp.CloneNode(true), new Text("\"Московское агентство организации отдыха и туризма\""))));
            var pp = new ParagraphProperties(new Justification { Val = JustificationValues.Center },
                new SpacingBetweenLines { After = spacingBetweenLinesAfter, Line = spacingBetweenLinesLine });
            pp.AppendChild(new ParagraphBorders().BottomBorder("000000", 3));
            doc.AppendChild(new Paragraph(pp, new Run(titleProp.CloneNode(true), new Text("(ГАУК \"МОСГОРТУР\")"))));
        }

        /// <summary>
        ///     уведомление об отсутствии мест.
        /// </summary>
        public ActionResult NotificationEmpty(long typeOfRestId, long? timeOfRestId, int countChildren,
            int countAttendant, long? placeOfRestId)
        {
            MemoryStream ms;
            using (ms = new MemoryStream())
            {
                using (var wordDocument = WordprocessingDocument.Create(ms, WordprocessingDocumentType.Document))
                {
                    var typeOfRest = RestTypeController.GetOrDefault(typeOfRestId);
                    var timeOfRest = timeOfRestId.HasValue ? RestTimeController.GetOrDefault(timeOfRestId.Value) : null;
                    var placeOfRest = placeOfRestId.HasValue
                        ? RestPlaceController.GetOrDefault(placeOfRestId.Value)
                        : null;

                    var mainPart = wordDocument.AddMainDocumentPart();
                    var doc = new Document(new Body());
                    DocumentHeader(doc, "__________________");

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                new SpacingBetweenLines { After = "20" }),
                            new Run(new RunProperties().SetFont().SetFontSize("28").Bold(), new Text(" "))));
                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                new SpacingBetweenLines { After = "20" }),
                            new Run(new RunProperties().SetFont().SetFontSize("28").Bold(), new Text(" "))));

                    doc.AppendChild(new Paragraph(
                        new ParagraphProperties(new Justification { Val = JustificationValues.Center }),
                        new Run(new RunProperties().SetFont().SetFontSize("28").Bold(),
                            new Text(
                                "Уведомление об отсутствии в наличии путевки на отдых и оздоровление в организации отдыха по выбранным заявителем параметрам"))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                new SpacingBetweenLines { After = "20" }),
                            new Run(new RunProperties().SetFont().SetFontSize("28").Bold(), new Text(" "))));

                    var titleRequestRunProperties = new RunProperties().SetFont().SetFontSize();

                    var titleRequestRunPropertiesBold = titleRequestRunProperties.CloneNode(true);
                    titleRequestRunPropertiesBold.AppendChild(new Bold());

                    var titleRequestRunPropertiesItalic = titleRequestRunProperties.CloneNode(true);
                    titleRequestRunPropertiesItalic.AppendChild(new Italic());

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                new SpacingBetweenLines { After = "20" }),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Дата и время обращения: ") { Space = SpaceProcessingModeValues.Preserve }),
                            new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                new Text(" " + DateTime.Now.FormatEx()))));
                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                new SpacingBetweenLines { After = "20" }),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true), new Text("Данные заявителя: ")),
                            new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                new Text("____________________________________________________________"))));
                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                new SpacingBetweenLines { After = "20" }),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true), new Text("Контактная информация: ")),
                            new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                new Text("___________________________ __________________________"))));
                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Indentation { FirstLine = "4000" },
                                new SpacingBetweenLines { Line = "276" },
                                new Justification { Val = JustificationValues.Left }),
                            new Run(new RunProperties().SetFont().SetFontSize("16"),
                                new Text(
                                    "телефон                                                       (электронная почта) "))));
                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                new SpacingBetweenLines { After = "20" }),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Наименование запрашиваемой услуги:  ")
                                { Space = SpaceProcessingModeValues.Preserve }),
                            new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                new Text("__________________________________________________"))));
                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                new SpacingBetweenLines { After = "20" }),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Цель обращения: ") { Space = SpaceProcessingModeValues.Preserve }),
                            new Run(titleRequestRunPropertiesItalic.CloneNode(true), new Text(typeOfRest?.Name))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                new SpacingBetweenLines { After = "20" }),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Год кампании: ") { Space = SpaceProcessingModeValues.Preserve }),
                            new Run(titleRequestRunPropertiesItalic.CloneNode(true), new Text("2016"))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                new SpacingBetweenLines { After = "20" }),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Число детей льготных категорий: ")
                                { Space = SpaceProcessingModeValues.Preserve }),
                            new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                new Text(countChildren.FormatEx()))));
                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                new SpacingBetweenLines { After = "20" }),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Число сопровождающих: ") { Space = SpaceProcessingModeValues.Preserve }),
                            new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                new Text(countAttendant.FormatEx()))));

                    if (timeOfRest != null)
                    {
                        doc.AppendChild(
                            new Paragraph(
                                new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                    new SpacingBetweenLines { After = "20" }),
                                new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                    new Text("Время отдыха: ") { Space = SpaceProcessingModeValues.Preserve }),
                                new Run(titleRequestRunPropertiesItalic.CloneNode(true), new Text(timeOfRest.Name))));
                    }

                    if (placeOfRest != null)
                    {
                        doc.AppendChild(
                            new Paragraph(
                                new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                    new SpacingBetweenLines { After = "20" }),
                                new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                    new Text("Место отдыха: ") { Space = SpaceProcessingModeValues.Preserve }),
                                new Run(titleRequestRunPropertiesItalic.CloneNode(true), new Text(placeOfRest.Name))));
                    }

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                new SpacingBetweenLines { After = "20" }),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Результат рассмотрения обращения: ")
                                { Space = SpaceProcessingModeValues.Preserve }),
                            new Run(((RunProperties)titleRequestRunPropertiesItalic.CloneNode(true)).Underline(),
                                new Text(
                                    "Отсутствие в наличии путевки на отдых и оздоровление в организации отдыха по выбранным заявителем параметрам."))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                new SpacingBetweenLines { After = "20" }),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Основание: пункт 6 ") { Space = SpaceProcessingModeValues.Preserve }),
                            new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                new Text(
                                    "приложения к приказу Департамента культуры города Москвы от 3 марта 2016 г.  № 130 \"Об организации в 2016 году выездного отдыха и оздоровления детей льготных категорий, имеющих место жительства в города Москве\" – заявление подается на предоставление путевки на отдых и оздоровление из числа имеющихся в наличии путевок на отдых и оздоровление, закупленных для соответствующей категории и вида отдыха."))));
                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                new SpacingBetweenLines { After = "20" }),
                            new Run(new RunProperties().SetFont().SetFontSize("28").Bold(), new Text(" "))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                new SpacingBetweenLines { After = "20" }),
                            new Run(new RunProperties().SetFont().SetFontSize("28").Bold(), new Text(" "))));

                    SignBlockNotification(doc, " ");
                    mainPart.Document = doc;
                }

                return File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                    "Уведомление об отсутствии места.docx");
            }
        }

        /// <summary>
        ///     Запрос необходимой информации
        /// </summary>
        public ActionResult NotificationRequestInformation(long requestId)
        {
            var request = UnitOfWork.GetById<Request>(requestId);

            if (request == null)
            {
                return null;
            }

            var doc = WordProcessor.NotificationRequestInformation(request, Security.GetCurrentAccount());

            return File(doc.FileBody, doc.MimeType, doc.FileName);
        }

        /// <summary>
        ///     Уведомление о предоставлении сертификата или путёвки
        /// </summary>
        public ActionResult NotificationAboutDecision(long requestId)
        {
            var data = WordProcessor.NotificationAboutDecision(UnitOfWork, Security.GetCurrentAccount(), requestId);
            return File(data.FileBody, data.MimeType, data.FileName);
        }

        /// <summary>
        ///     Уведомление о предоставлении сертификата или путёвки (альтернативный)
        /// </summary>
        public ActionResult NotificationAboutTourAlternative(long requestId)
        {
            var data = WordProcessor.NotificationAboutTourAlternative(UnitOfWork, Security.GetCurrentAccount(), requestId);
            return File(data.FileBody, data.MimeType, data.FileName);
        }

        /// <summary>
        ///     Уведомление о выборе отдыха в 2021 году (альтернативный)
        /// </summary>
        public ActionResult NotificationAboutTourChoose(long requestId)
        {
            var data = WordProcessor.NotificationAboutTourChoose(UnitOfWork, Security.GetCurrentAccount(), requestId);
            return File(data.FileBody, data.MimeType, data.FileName);
        }

        /// <summary>
        ///     уведомление об регистрации.
        /// </summary>
        public ActionResult NotificationRegistration(long requestId)
        {
            var data = WordProcessor.NotificationRegistration(UnitOfWork, Security.GetCurrentAccount(), requestId);
            return File(data.FileBody, data.MimeType, data.FileName);
        }

        /// <summary>
        ///     Уведомление об отсутствии возможности замены сертификата на отдых и оздоровление на  бесплатную путевку для отдыха
        ///     и оздоровления (в случае выбора сертификата на отдых и оздоровление)
        /// </summary>
        public ActionResult NotificationLackOfPossibilityReplacement(long requestId)
        {
            MemoryStream ms;
            using (ms = new MemoryStream())
            {
                using (var wordDocument = WordprocessingDocument.Create(ms, WordprocessingDocumentType.Document))
                {
                    var mainPart = wordDocument.AddMainDocumentPart();
                    var doc = new Document(new Body());

                    var request = UnitOfWork.GetById<Request>(requestId);

                    DocumentHeaderRegistration(doc);

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                new SpacingBetweenLines { After = "20" }),
                            new Run(new RunProperties().SetFont().SetFontSize("28").Bold(), new Text(" "))));
                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                new SpacingBetweenLines { After = "20" }),
                            new Run(new RunProperties().SetFont().SetFontSize("28").Bold(), new Text(" "))));

                    doc.AppendChild(new Paragraph(
                        new ParagraphProperties(new Justification { Val = JustificationValues.Center }),
                        new Run(new RunProperties().SetFont().SetFontSize("28").Bold(),
                            new Text(
                                "Уведомление об отсутствии возможности замены сертификата на отдых и оздоровление на  бесплатную путевку для отдыха и оздоровления (в случае выбора сертификата на отдых и оздоровление)"))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                new SpacingBetweenLines { After = "20" }),
                            new Run(new RunProperties().SetFont().SetFontSize("28").Bold(), new Text(" "))));

                    var titleRequestRunProperties = new RunProperties().SetFont().SetFontSize();
                    var titleRequestRunPropertiesBold = titleRequestRunProperties.CloneNode(true);
                    titleRequestRunPropertiesBold.AppendChild(new Bold());

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Both },
                                new SpacingBetweenLines { After = "20" },
                                new Indentation { FirstLine = "600" }),
                            new Run(titleRequestRunProperties.CloneNode(true),
                                new Text(
                                        "В соответствии с постановлением Правительства Москвы от 22 февраля 2017 г. № 56-ПП \"Об организации отдыха и оздоровления детей, находящихся в трудной жизненной ситуации\" в случае выбора сертификата на отдых и оздоровление (далее – сертификат) замена на бесплатную путевку для отдыха и оздоровления (далее – бесплатная путевка) ")
                                { Space = SpaceProcessingModeValues.Preserve }),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("не допускается.")
                                { Space = SpaceProcessingModeValues.Preserve })));

                    var applicant = request?.Applicant ??
                                    new Applicant { DocumentType = new DocumentType { Name = string.Empty } };

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                new Indentation { FirstLine = "500" },
                                new SpacingBetweenLines { After = "20" }),
                            new Run(titleRequestRunProperties.CloneNode(true),
                                new Text(
                                    $"Я, {applicant.LastName} {applicant.FirstName} {applicant.MiddleName}, подтверждаю, что ознакомлен(а) с отсутствием возможности замены сертификата на бесплатную путевку.")
                                {
                                    Space = SpaceProcessingModeValues.Preserve
                                })));

                    WordProcessor.SignBlockNotification2020(doc, Security.GetCurrentAccount(), "Исполнитель:");
                    mainPart.Document = doc;
                }

                return File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                    "Уведомление об отсутствии возможности замены .docx");
            }
        }

        /// <summary>
        ///     Уведомление об ознакомлении с перечнем противопоказаний к отдыху и оздоровлению, утвержденным Министерством
        ///     здравоохранения Российской Федерации.
        /// </summary>
        public ActionResult NotificationAcquaintance(long requestId)
        {
            MemoryStream ms;
            using (ms = new MemoryStream())
            {
                using (var wordDocument = WordprocessingDocument.Create(ms, WordprocessingDocumentType.Document))
                {
                    var mainPart = wordDocument.AddMainDocumentPart();
                    var doc = new Document(new Body());

                    var request = UnitOfWork.GetById<Request>(requestId);

                    DocumentHeaderRegistration(doc);

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                new SpacingBetweenLines { After = "20" }),
                            new Run(new RunProperties().SetFont().SetFontSize("28").Bold(), new Text(" "))));
                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                new SpacingBetweenLines { After = "20" }),
                            new Run(new RunProperties().SetFont().SetFontSize("28").Bold(), new Text(" "))));

                    doc.AppendChild(new Paragraph(
                        new ParagraphProperties(new Justification { Val = JustificationValues.Center }),
                        new Run(new RunProperties().SetFont().SetFontSize("28").Bold(),
                            new Text(
                                "Уведомление об ознакомлении с перечнем противопоказаний к отдыху и оздоровлению, утвержденным Министерством здравоохранения Российской Федерации"))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Both },
                                new SpacingBetweenLines { After = "20" }),
                            new Run(new RunProperties().SetFont().SetFontSize("28").Bold(), new Text(" "))));

                    var titleRequestRunProperties = new RunProperties().SetFont().SetFontSize();

                    var body = new[]
                    {
                        "В соответствии с приказом Министерства здравоохранения Российской Федерации от 13 июня 2018 г. № 327н \"Об утверждении порядка оказания медицинской помощи несовершеннолетним в период оздоровления и организованного отдыха\" в организации отдыха и оздоровления (далее – детские лагеря) направляются дети, не имеющие следующих медицинских противопоказаний для пребывания в детских лагерях:",
                        "соматические заболевания в острой и подострой стадии, хронические заболевания в стадии обострения, в стадии декомпенсации;",
                        "инфекционные и паразитарные болезни, в том числе с поражением глаз и кожи, инфестации (педикулез, чесотка) – в период до окончания срока изоляции;",
                        "установленный диагноз \"бактерионосительство возбудителей кишечных инфекций, дифтерии\";",
                        "активный туберкулез любой локализации;",
                        "наличие контакта с инфекционными больными в течение 21 календарного дня перед заездом в детский лагерь;",
                        "отсутствие профилактических прививок в случае возникновения массовых инфекционных заболеваний или при угрозе возникновения эпидемий;",
                        "злокачественные новообразования, требующие лечения, в том числе проведения химиотерапии;",
                        "эпилепсия с текущими приступами, в том числе резистентная к проводимому лечению;",
                        "эпилепсия с медикаментозной ремиссией менее 1 года;",
                        "кахексия;",
                        "психические расстройства и расстройства поведения, вызванные употреблением психоактивных веществ, а также иные психические расстройства и расстройства поведения в состоянии обострения и (или) представляющие опасность для ребенка и окружающих;",
                        "хронические заболевания, требующие соблюдения назначенного лечащим врачом режима лечения (диета, прием лекарственных препаратов для медицинского применения и специализированных продуктов лечебного питания) (для детских лагерей палаточного типа);",
                    };

                    foreach (var s in body)
                    {
                        doc.AppendChild(
                            new Paragraph(
                                new ParagraphProperties(new Justification { Val = JustificationValues.Both },
                                    new Indentation { FirstLine = "500" },
                                    new SpacingBetweenLines { After = "20" }),
                                new Run(titleRequestRunProperties.CloneNode(true),
                                    new Text(s)
                                    {
                                        Space = SpaceProcessingModeValues.Preserve
                                    })));
                    }

                    var applicant = request?.Applicant ??
                                    new Applicant { DocumentType = new DocumentType { Name = string.Empty } };

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Both },
                                new SpacingBetweenLines { After = "20" }),
                            new Run(new RunProperties().SetFont().SetFontSize("28").Bold(), new Text(" "))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                new Indentation { FirstLine = "500" },
                                new SpacingBetweenLines { After = "20" }),
                            new Run(titleRequestRunProperties.CloneNode(true),
                                new Text(
                                    $"Я, {applicant.LastName} {applicant.FirstName} {applicant.MiddleName}, подтверждаю, что ознакомлен(а) с перечнем противопоказаний к отдыху и оздоровлению, утвержденным Министерством здравоохранения Российской Федерации.")
                                {
                                    Space = SpaceProcessingModeValues.Preserve
                                })));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Both },
                                new SpacingBetweenLines { After = "20" }),
                            new Run(new RunProperties().SetFont().SetFontSize("28").Bold(), new Text(" "))));

                    WordProcessor.SignBlockNotification2020(doc, Security.GetCurrentAccount(), "Исполнитель:");
                    mainPart.Document = doc;
                }

                return File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                    "Уведомление об ознакомлении с перечнем.docx");
            }
        }

        /// <summary>
        ///     Уведомление о непризнании отказа от осуществления выездного отдыха отказом по уважительным причинам
        /// </summary>
        public ActionResult NotificationNonRecognition(long requestId)
        {
            MemoryStream ms;
            using (ms = new MemoryStream())
            {
                using (var wordDocument = WordprocessingDocument.Create(ms, WordprocessingDocumentType.Document))
                {
                    var mainPart = wordDocument.AddMainDocumentPart();
                    var doc = new Document(new Body());

                    var request = UnitOfWork.GetById<Request>(requestId);

                    DocumentHeaderRegistration(doc);

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                new SpacingBetweenLines { After = "20" }),
                            new Run(new RunProperties().SetFont().SetFontSize("28").Bold(), new Text(" "))));
                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                new SpacingBetweenLines { After = "20" }),
                            new Run(new RunProperties().SetFont().SetFontSize("28").Bold(), new Text(" "))));

                    doc.AppendChild(new Paragraph(
                        new ParagraphProperties(new Justification { Val = JustificationValues.Center }),
                        new Run(new RunProperties().SetFont().SetFontSize("28").Bold(),
                            new Text(
                                "Уведомление о непризнании отказа от осуществления выездного отдыха отказом по уважительным причинам"))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                new SpacingBetweenLines { After = "20" }),
                            new Run(new RunProperties().SetFont().SetFontSize("28").Bold(), new Text(" "))));

                    var titleRequestRunProperties = new RunProperties().SetFont().SetFontSize();

                    var titleRequestRunPropertiesBold = titleRequestRunProperties.CloneNode(true);
                    titleRequestRunPropertiesBold.AppendChild(new Bold());

                    var titleRequestRunPropertiesItalic = titleRequestRunProperties.CloneNode(true);

                    var applicant = request.NullSafe(r => r.Applicant) ??
                                    new Applicant { DocumentType = new DocumentType { Name = string.Empty } };

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                new SpacingBetweenLines { After = "20" }),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Дата и время регистрации заявления: ")
                                { Space = SpaceProcessingModeValues.Preserve }),
                            new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                new Text(request.DateRequest.FormatEx()))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                new SpacingBetweenLines { After = "20" }),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Номер заявления: ") { Space = SpaceProcessingModeValues.Preserve }),
                            new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                new Text(request.RequestNumber.FormatEx()))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                new SpacingBetweenLines { After = "20" }),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Данные заявителя: ") { Space = SpaceProcessingModeValues.Preserve }),
                            new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                new Text(
                                    $"{applicant.LastName} {applicant.FirstName} {applicant.MiddleName}, {applicant.DateOfBirth.FormatEx(string.Empty)}"))));


                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                new SpacingBetweenLines { After = "20" }),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Контактная информация: ") { Space = SpaceProcessingModeValues.Preserve }),
                            new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                new Text(applicant.Phone + ", " + applicant.Email))));

                    var strings = new[]
                    {
                        " ",
                        "Ваше заявление об отказе от предоставленной бесплатной путевки для отдыха и оздоровления рассмотрено.",
                        "В соответствии с постановлением Правительства Москвы от 22 февраля 2017 г. № 56-ПП \"Об организации отдыха и оздоровления детей, находящихся в трудной жизненной ситуации\", отказ от осуществления выездного отдыха на основании предоставленной бесплатной путевки для отдыха и оздоровления подтверждается документами о наличии уважительных причин.",
                        "К уважительным причинам относятся:",
                        "заболевание, травма ребенка, лица из числа детей - сирот и детей, оставшихся без попечения родителей; ",
                        "заболевание, травма сопровождающего лица (в случае организации совместного выездного отдыха); ",
                        "необходимость осуществления сопровождающим лицом ухода за больным членом семьи (в случае организации совместного выездного отдыха); ",
                        "карантин ребенка, карантин лица из числа детей-сирот и детей, оставшихся без попечения родителей, карантин лица, проживающего совместно с ребенком, лицом из числа детей-сирот и детей, оставшихся без попечения родителей, а также в случае организации совместного выездного отдыха карантин сопровождающего лица; ",
                        "смерть близкого родственника (родителя, бабушки, дедушки, брата, сестры, дяди, тети).",
                        "Предоставленный Вами документ не является документом, подтверждающим уважительную причину неиспользования предоставленной бесплатной путевки для отдыха и оздоровления.",
                        $"Учитывая изложенное, оформить отказ от бесплатной путевки для отдыха и оздоровления № {request.CertificateNumber} в установленном порядке не представляется возможным.",
                        " ",
                        " ",
                        " "
                    };

                    foreach (var s in strings)
                    {
                        doc.AppendChild(
                            new Paragraph(
                                new ParagraphProperties(new Justification { Val = JustificationValues.Both },
                                    new SpacingBetweenLines { After = "20" }, new Indentation { FirstLine = "500" }),
                                new Run(titleRequestRunPropertiesItalic.CloneNode(true), new Text(s))));
                    }

                    SignSimpleBlockNotification(doc);
                    mainPart.Document = doc;
                }

                return File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                    "Уведомление о непризнании отказа.docx");
            }
        }

        /// <summary>
        ///     Уведомление о непризнании отказа от осуществления выездного отдыха отказом по уважительным причинам (в связи с
        ///     подачей заявления на отказ после установленного срока)
        /// </summary>
        public ActionResult NotificationNonRecognitionByTime(long requestId)
        {
            MemoryStream ms;
            using (ms = new MemoryStream())
            {
                using (var wordDocument = WordprocessingDocument.Create(ms, WordprocessingDocumentType.Document))
                {
                    var mainPart = wordDocument.AddMainDocumentPart();
                    var doc = new Document(new Body());

                    var request = UnitOfWork.GetById<Request>(requestId);

                    DocumentHeaderRegistration(doc);

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                new SpacingBetweenLines { After = "20" }),
                            new Run(new RunProperties().SetFont().SetFontSize("28").Bold(), new Text(" "))));
                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                new SpacingBetweenLines { After = "20" }),
                            new Run(new RunProperties().SetFont().SetFontSize("28").Bold(), new Text(" "))));

                    doc.AppendChild(new Paragraph(
                        new ParagraphProperties(new Justification { Val = JustificationValues.Center }),
                        new Run(new RunProperties().SetFont().SetFontSize("28").Bold(),
                            new Text(
                                "Уведомление о непризнании отказа от осуществления выездного отдыха отказом по уважительным причинам (в связи с подачей заявления на отказ после установленного срока)"))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                new SpacingBetweenLines { After = "20" }),
                            new Run(new RunProperties().SetFont().SetFontSize("28").Bold(), new Text(" "))));

                    var titleRequestRunProperties = new RunProperties().SetFont().SetFontSize();

                    var titleRequestRunPropertiesBold = titleRequestRunProperties.CloneNode(true);
                    titleRequestRunPropertiesBold.AppendChild(new Bold());

                    var titleRequestRunPropertiesItalic = titleRequestRunProperties.CloneNode(true);

                    var applicant = request.NullSafe(r => r.Applicant) ??
                                    new Applicant { DocumentType = new DocumentType { Name = string.Empty } };

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                new SpacingBetweenLines { After = "20" }),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Дата и время регистрации заявления: ")
                                { Space = SpaceProcessingModeValues.Preserve }),
                            new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                new Text(request.DateRequest.FormatEx()))));

                    if (request.TypeOfRestId != (long)TypeOfRestEnum.Compensation &&
                        request.TypeOfRestId != (long)TypeOfRestEnum.CompensationYouthRest)
                    {
                        doc.AppendChild(
                            new Paragraph(
                                new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                    new SpacingBetweenLines { After = "20" }),
                                new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                    new Text("Номер заявления: ") { Space = SpaceProcessingModeValues.Preserve }),
                                new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                    new Text(request.RequestNumber.FormatEx()))));
                    }

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                new SpacingBetweenLines { After = "20" }),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Данные заявителя: ") { Space = SpaceProcessingModeValues.Preserve }),
                            new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                new Text(
                                    $"{applicant.LastName} {applicant.FirstName} {applicant.MiddleName}, {applicant.DateOfBirth.FormatEx(string.Empty)}"))));


                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                new SpacingBetweenLines { After = "20" }),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Контактная информация: ") { Space = SpaceProcessingModeValues.Preserve }),
                            new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                new Text(applicant.Phone + ", " + applicant.Email))));

                    var strings = new[]
                    {
                        " ",
                        "Ваше заявление об отказе от предоставленной бесплатной путевки для отдыха и оздоровления рассмотрено.",
                        "В соответствии с постановлением Правительства Москвы от 22 февраля 2017 г. № 56-ПП \"Об организации отдыха и оздоровления детей, находящихся в трудной жизненной ситуации\", отказ от осуществления выездного отдыха на основании предоставленной бесплатной путевки для отдыха и оздоровления подтверждается документами о наличии уважительных причин.",
                        "К уважительным причинам относятся:",
                        "заболевание, травма ребенка, лица из числа детей - сирот и детей, оставшихся без попечения родителей;",
                        "заболевание, травма сопровождающего лица (в случае организации совместного выездного отдыха);",
                        "необходимость осуществления сопровождающим лицом ухода за больным членом семьи (в случае организации совместного выездного отдыха);",
                        "карантин ребенка, карантин лица из числа детей-сирот и детей, оставшихся без попечения родителей, карантин лица, проживающего совместно с ребенком, лицом из числа детей-сирот и детей, оставшихся без попечения родителей, а также в случае организации совместного выездного отдыха карантин сопровождающего лица;",
                        "смерть близкого родственника (родителя, бабушки, дедушки, брата, сестры, дяди, тети).",
                        $"Учитывая, что заявление об отказе от предоставленной путевки для отдыха и оздоровления № {request.CertificateNumber} было подано по истечении 60 календарных дней со дня начала периода отдыха и оздоровления, указанного в бесплатной путевке для отдыха и оздоровления, оформить отказ от осуществления выездного отдыха на основании бесплатной путевки для отдыха и оздоровления в установленном порядке не представляется возможным.",
                        " ",
                        " ",
                        " "
                    };

                    foreach (var s in strings)
                    {
                        doc.AppendChild(
                            new Paragraph(
                                new ParagraphProperties(new Justification { Val = JustificationValues.Both },
                                    new SpacingBetweenLines { After = "20" }, new Indentation { FirstLine = "500" }),
                                new Run(titleRequestRunPropertiesItalic.CloneNode(true), new Text(s))));
                    }

                    SignSimpleBlockNotification(doc);
                    mainPart.Document = doc;
                }

                return File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                    "Уведомление о непризнании отказа.docx");
            }
        }

        /// <summary>
        ///     Уведомление о приостановлении рассмотрения заявления о предоставлении услуг отдыха и оздоровления
        /// </summary>
        /// <param name="requestId"></param>
        /// <returns></returns>
        public ActionResult NotificationWaitApplicant(long requestId)
        {
            var data = WordProcessor.NotificationWaitApplicant(UnitOfWork, Security.GetCurrentAccount(), requestId);
            return File(data.FileBody, data.MimeType, data.FileName);
        }

        /// <summary>
        ///     Уведомление о непринятии отказа от рассмотрения поданного заявления на предоставление бесплатной путевки для отдыха
        ///     и оздоровления.
        /// </summary>
        public ActionResult NotificationRefuseRefuse(long requestId)
        {
            MemoryStream ms;
            using (ms = new MemoryStream())
            {
                using (var wordDocument = WordprocessingDocument.Create(ms, WordprocessingDocumentType.Document))
                {
                    var mainPart = wordDocument.AddMainDocumentPart();
                    var doc = new Document(new Body());

                    var request = UnitOfWork.GetById<Request>(requestId);

                    DocumentHeaderRegistration(doc);

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                new SpacingBetweenLines { After = "20" }),
                            new Run(new RunProperties().SetFont().SetFontSize("28").Bold(), new Text(" "))));
                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                new SpacingBetweenLines { After = "20" }),
                            new Run(new RunProperties().SetFont().SetFontSize("28").Bold(), new Text(" "))));

                    doc.AppendChild(new Paragraph(
                        new ParagraphProperties(new Justification { Val = JustificationValues.Center }),
                        new Run(new RunProperties().SetFont().SetFontSize("28").Bold(),
                            new Text(
                                "Уведомление о непринятии заявления об отзыве заявления о предоставлении услуг отдыха и оздоровления"))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                new SpacingBetweenLines { After = "20" }),
                            new Run(new RunProperties().SetFont().SetFontSize("28").Bold(), new Text(" "))));

                    var titleRequestRunProperties = new RunProperties().SetFont().SetFontSize();

                    var titleRequestRunPropertiesBold = titleRequestRunProperties.CloneNode(true);
                    titleRequestRunPropertiesBold.AppendChild(new Bold());

                    var titleRequestRunPropertiesItalic = titleRequestRunProperties.CloneNode(true);
                    titleRequestRunPropertiesItalic.AppendChild(new Italic());

                    var applicant = request.NullSafe(r => r.Applicant) ??
                                    new Applicant { DocumentType = new DocumentType { Name = string.Empty } };

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                new SpacingBetweenLines { After = "20" }),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Дата и время регистрации заявления: ")
                                { Space = SpaceProcessingModeValues.Preserve }),
                            new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                new Text(request.DateRequest.FormatEx()))));

                    if (request.TypeOfRestId != (long)TypeOfRestEnum.Compensation ||
                        request.TypeOfRestId != (long)TypeOfRestEnum.CompensationYouthRest)
                    {
                        doc.AppendChild(
                            new Paragraph(
                                new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                    new SpacingBetweenLines { After = "20" }),
                                new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                    new Text("Номер заявления: ") { Space = SpaceProcessingModeValues.Preserve }),
                                new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                    new Text(request.RequestNumber.FormatEx()))));
                    }

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                new SpacingBetweenLines { After = "20" }),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Данные заявителя: ") { Space = SpaceProcessingModeValues.Preserve }),
                            new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                new Text(
                                    $"{applicant.LastName} {applicant.FirstName} {applicant.MiddleName}, {applicant.DateOfBirth.FormatEx(string.Empty)}"))));


                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                new SpacingBetweenLines { After = "20" }),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Контактная информация: ") { Space = SpaceProcessingModeValues.Preserve }),
                            new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                new Text(applicant.Phone + ", " + applicant.Email))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                new SpacingBetweenLines { After = "20" }),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Причина обращения: ") { Space = SpaceProcessingModeValues.Preserve }),
                            new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                new Text(
                                    "прекращение рассмотрения поданного заявления о предоставлении услуг отдыха и оздоровления."))));
                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                new SpacingBetweenLines { After = "20" }),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Результат рассмотрения заявления: ")
                                { Space = SpaceProcessingModeValues.Preserve }),
                            new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                new Text(
                                    "отказ в приеме заявления об отзыве заявления о предоставлении услуг отдыха и оздоровления."))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                new SpacingBetweenLines { After = "20" }),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Основание: ") { Space = SpaceProcessingModeValues.Preserve }),
                            new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                new Text(
                                    "постановление Правительства Москвы от 22 февраля 2017 г. № 56-ПП \"Об организации отдыха и оздоровления детей, находящихся в трудной жизненной ситуации\""))));

                    SignBlockNotification(doc, $"{applicant.LastName} {applicant.FirstName} {applicant.MiddleName}");
                    mainPart.Document = doc;
                }

                return File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                    "Уведомление о непринятии отказа.docx");
            }
        }

        /// <summary>
        ///     уведомление об отказе.
        /// </summary>
        [Authorize]
        public ActionResult NotificationRefuse(long requestId)
        {
            var data = WordProcessor.NotificationDataSwitch(UnitOfWork, Security.GetCurrentAccount(), requestId);
            return File(data.FileBody, data.MimeType, data.FileName);
        }

        /// <summary>
        ///     получить данные по уведомлению
        /// </summary>
        internal static byte[] NotificationRefuseContent(IUnitOfWork uw, long requestId)
        {
            byte[] data;
            using (var ms = new MemoryStream())
            {
                using (var wordDocument = WordprocessingDocument.Create(ms, WordprocessingDocumentType.Document))
                {
                    var mainPart = wordDocument.AddMainDocumentPart();
                    var doc = new Document(new Body());

                    var request = uw.GetById<Request>(requestId);

                    DocumentHeaderRegistration(doc);

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                new SpacingBetweenLines { After = "20" }),
                            new Run(new RunProperties().SetFont().SetFontSize("28").Bold(), new Text(" "))));
                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                new SpacingBetweenLines { After = "20" }),
                            new Run(new RunProperties().SetFont().SetFontSize("28").Bold(), new Text(" "))));

                    var notificationName = "Уведомление о рассмотрении заявления";

                    if (request.StatusId == (long)StatusEnum.CancelByApplicant)
                    {
                        notificationName =
                            "Уведомление о прекращении рассмотрения поданного заявления о предоставлении услуг отдыха и оздоровления";
                    }
                    else if (request.StatusId == (long)StatusEnum.Reject)
                    {
                        notificationName = request.TypeOfRestId == (long)TypeOfRestEnum.Compensation ||
                                           request.TypeOfRestId == (long)TypeOfRestEnum.CompensationYouthRest
                            ? "Уведомление об отказе в выплате компенсации за самостоятельно приобретенную родителями или иными законными представителями путевку для отдыха и оздоровления"
                            : "Уведомление об отказе в предоставлении услуг отдыха и оздоровления";
                    }
                    else if (request.StatusId == (long)StatusEnum.CertificateIssued)
                    {
                        notificationName = request.TypeOfRestId == (long)TypeOfRestEnum.Compensation ||
                                           request.TypeOfRestId == (long)TypeOfRestEnum.CompensationYouthRest
                            ? "Уведомление о предоставлении выплаты компенсации за самостоятельно приобретенную родителями или иными законными представителями путевку для отдыха и оздоровления"
                            : "Уведомление о предоставлении бесплатной путевки для отдыха и оздоровления";
                    }

                    doc.AppendChild(new Paragraph(
                        new ParagraphProperties(new Justification { Val = JustificationValues.Center }),
                        new Run(new RunProperties().SetFont().SetFontSize("28").Bold(),
                            new Text(notificationName))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                new SpacingBetweenLines { After = "20" }),
                            new Run(new RunProperties().SetFont().SetFontSize("28").Bold(), new Text(" "))));

                    var titleRequestRunProperties = new RunProperties().SetFont().SetFontSize();

                    var titleRequestRunPropertiesBold = titleRequestRunProperties.CloneNode(true);
                    titleRequestRunPropertiesBold.AppendChild(new Bold());

                    var titleRequestRunPropertiesItalic = titleRequestRunProperties.CloneNode(true);

                    var applicant = request.NullSafe(r => r.Applicant) ??
                                    new Applicant { DocumentType = new DocumentType { Name = string.Empty } };

                    if ((request.TypeOfRestId == (long)TypeOfRestEnum.Compensation ||
                         request.TypeOfRestId == (long)TypeOfRestEnum.CompensationYouthRest) &&
                        request.StatusId == (long)StatusEnum.Reject)
                    {
                        doc.AppendChild(
                            new Paragraph(
                                new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                    new SpacingBetweenLines { After = "20" }),
                                new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                    new Text("Дата обращения: ") { Space = SpaceProcessingModeValues.Preserve }),
                                new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                    new Text(request.DateRequest?.Date.FormatEx()))));
                    }
                    else
                    {
                        doc.AppendChild(
                            new Paragraph(
                                new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                    new SpacingBetweenLines { After = "20" }),
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
                            new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                new SpacingBetweenLines { After = "20" }),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Номер заявления: ") { Space = SpaceProcessingModeValues.Preserve }),
                            new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                new Text(request.RequestNumber.FormatEx()))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                new SpacingBetweenLines { After = "20" }),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Данные заявителя: ") { Space = SpaceProcessingModeValues.Preserve }),
                            new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                new Text(
                                    $"{applicant.LastName} {applicant.FirstName} {applicant.MiddleName}, {applicant.DateOfBirth.FormatEx(string.Empty)}"))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                new SpacingBetweenLines { After = "20" }),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Контактная информация: ") { Space = SpaceProcessingModeValues.Preserve }),
                            new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                new Text(applicant.Phone + ", " + applicant.Email))));

                    if (!request.IsFirstCompany)
                    {
                        if (request.StatusId == (long)StatusEnum.CancelByApplicant)
                        {
                            doc.AppendChild(
                                new Paragraph(
                                    new ParagraphProperties(new Justification { Val = JustificationValues.Both },
                                        new SpacingBetweenLines { After = "20" }),
                                    new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                        new Text("Причина обращения: ") { Space = SpaceProcessingModeValues.Preserve }),
                                    new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                        new Text(
                                            "прекращение рассмотрения поданного заявления о предоставлении услуг отдыха и оздоровления по инициативе заявителя."))));

                            doc.AppendChild(
                                new Paragraph(
                                    new ParagraphProperties(new Justification { Val = JustificationValues.Both },
                                        new SpacingBetweenLines { After = "20" }),
                                    new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                        new Text("Результат рассмотрения заявления: ")
                                        { Space = SpaceProcessingModeValues.Preserve }),
                                    new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                        new Text("отозвано по инициативе заявителя в установленный Порядком срок."))));
                        }
                        else
                        {
                            if (request.TypeOfRestId != (long)TypeOfRestEnum.Compensation &&
                                request.TypeOfRestId != (long)TypeOfRestEnum.CompensationYouthRest)
                            {
                                doc.AppendChild(
                                    new Paragraph(
                                        new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                            new SpacingBetweenLines { After = "20" }),
                                        new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                            new Text("Наименование запрашиваемой услуги:  ")
                                            {
                                                Space = SpaceProcessingModeValues.Preserve
                                            }),
                                        new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                            new Text("___________________________________________"))));

                                doc.AppendChild(
                                    new Paragraph(
                                        new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                            new SpacingBetweenLines { After = "20" }),
                                        new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                            new Text("Цель обращения: ") { Space = SpaceProcessingModeValues.Preserve }),
                                        new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                            new Text(request.TypeOfRest?.Name))));

                                doc.AppendChild(
                                    new Paragraph(
                                        new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                            new SpacingBetweenLines { After = "20" }),
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
                                        new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                            new SpacingBetweenLines { After = "20" }),
                                        new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                            new Text("Время отдыха: ") { Space = SpaceProcessingModeValues.Preserve }),
                                        new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                            new Text("c " + request.Tour.DateIncome.FormatEx() + " по " +
                                                     request.Tour.DateOutcome.FormatEx()))));
                            }

                            if (request.TypeOfRestId != (long)TypeOfRestEnum.Compensation ||
                                request.StatusId != (long)StatusEnum.Reject)
                            {
                                AppendChildrenToDocument(doc, request);
                            }

                            if ((request.TypeOfRestId == (long)TypeOfRestEnum.Compensation
                                 || request.TypeOfRestId == (long)TypeOfRestEnum.CompensationYouthRest) &&
                                request.StatusId == (long)StatusEnum.Reject)
                            {
                                doc.AppendChild(
                                    new Paragraph(
                                        new ParagraphProperties(new Justification { Val = JustificationValues.Both },
                                            new SpacingBetweenLines { After = "20" }),
                                        new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                            new Text("Причина обращения: ")
                                            { Space = SpaceProcessingModeValues.Preserve }),
                                        new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                            new Text(
                                                "выплата компенсации за самостоятельно приобретенную родителями или иными законными представителями путевку."))));
                            }

                            doc.AppendChild(
                                new Paragraph(
                                    new ParagraphProperties(new Justification { Val = JustificationValues.Both },
                                        new SpacingBetweenLines { After = "20" }),
                                    new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                        new Text("Результат рассмотрения заявления: ")
                                        { Space = SpaceProcessingModeValues.Preserve }),
                                    new Run(titleRequestRunPropertiesItalic.CloneNode(true), new Text(
                                        (request.TypeOfRestId == (long)TypeOfRestEnum.Compensation ||
                                         request.TypeOfRestId == (long)TypeOfRestEnum.CompensationYouthRest) &&
                                        request.StatusId == (long)StatusEnum.Reject
                                            ? "отказ в предоставлении выплаты компенсации за самостоятельно приобретенную родителями или иными законными представителями путевку для отдыха и оздоровления."
                                            : request.Status.Name))));
                        }


                        if ((request.TypeOfRestId == (long)TypeOfRestEnum.Compensation ||
                             request.TypeOfRestId == (long)TypeOfRestEnum.CompensationYouthRest) &&
                            (request.StatusId == (long)StatusEnum.CertificateIssued ||
                             request.StatusId == (long)StatusEnum.Reject))
                        {
                            if (request.DeclineReason != null)
                            {
                                doc.AppendChild(
                                    new Paragraph(
                                        new ParagraphProperties(new Justification { Val = JustificationValues.Both },
                                            new SpacingBetweenLines { After = "20" }),
                                        new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                            new Text("Причина отказа в выплате: ")
                                            { Space = SpaceProcessingModeValues.Preserve }),
                                        new Run(titleRequestRunPropertiesItalic.CloneNode(true), new Text(
                                            request.DeclineReason.Name))));
                            }

                            doc.AppendChild(
                                new Paragraph(
                                    new ParagraphProperties(new Justification { Val = JustificationValues.Both },
                                        new SpacingBetweenLines { After = "20" }),
                                    new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                        new Text("Основание: ") { Space = SpaceProcessingModeValues.Preserve }),
                                    new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                        new Text(
                                            "постановление Правительства Москвы от 22 февраля 2017 г. № 56-ПП \"Об организации отдыха и оздоровления детей, находящихся в трудной жизненной ситуации\""))));
                        }
                        else if (request.DeclineReason != null)
                        {
                            doc.AppendChild(
                                new Paragraph(
                                    new ParagraphProperties(new Justification { Val = JustificationValues.Both },
                                        new SpacingBetweenLines { After = "20" }),
                                    new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                        new Text("Основание: ") { Space = SpaceProcessingModeValues.Preserve }),
                                    new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                        //new Text((request.TypeOfRestId == (long) TypeOfRestEnum.Compensation
                                        //	         ? "приказ Департамента культуры города Москвы от 12 января 2016 г. № 8 \"О выплате частичной компенсации стоимости самостоятельно приобретенной путевки на отдых и оздоровление детей и сопровождающих их лиц в 2016 году\", пункт "
                                        //	         : "") + request.DeclineReason.Name)
                                        new Text(
                                            "постановление Правительства Москвы от 22 февраля 2017 г. № 56-ПП \"Об организации отдыха и оздоровления детей, находящихся в трудной жизненной ситуации\"."))));
                        }
                    }
                    else if (request.StatusId == (long)StatusEnum.CancelByApplicant)
                    {
                        doc.AppendChild(
                            new Paragraph(
                                new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                    new SpacingBetweenLines { After = "20" }),
                                new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                    new Text("Причина обращения: ") { Space = SpaceProcessingModeValues.Preserve }),
                                new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                    new Text(
                                        "прекращение рассмотрения поданного заявления о предоставлении услуг отдыха и оздоровления по инициативе заявителя."))));
                        doc.AppendChild(
                            new Paragraph(
                                new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                    new SpacingBetweenLines { After = "20" }),
                                new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                    new Text("Результат рассмотрения заявления: ")
                                    { Space = SpaceProcessingModeValues.Preserve }),
                                new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                    new Text("отозвано по инициативе заявителя в установленный Порядком срок"))));

                        doc.AppendChild(
                            new Paragraph(
                                new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                    new SpacingBetweenLines { After = "20" }),
                                new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                    new Text("Основание: ") { Space = SpaceProcessingModeValues.Preserve }),
                                new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                    new Text(
                                        "постановление Правительства Москвы от 22 февраля 2017 г. № 56-ПП \"Об организации отдыха и оздоровления детей, находящихся в трудной жизненной ситуации\""))));

                        doc.AppendChild(
                            new Paragraph(
                                new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                    new SpacingBetweenLines { After = "20" }),
                                new Run(new RunProperties().SetFont().SetFontSize("28").Bold(), new Text(" "))));
                    }
                    else if (request.StatusId == (long)StatusEnum.Reject)
                    {
                        doc.AppendChild(
                            new Paragraph(
                                new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                    new SpacingBetweenLines { After = "20" }),
                                new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                    new Text("Вид услуги: ") { Space = SpaceProcessingModeValues.Preserve }),
                                new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                    new Text(request.TypeOfRest?.Name))));

                        AppendChildrenToDocument(doc, request);

                        doc.AppendChild(
                            new Paragraph(
                                new ParagraphProperties(new Justification { Val = JustificationValues.Both },
                                    new SpacingBetweenLines { After = "20" }),
                                new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                    new Text("Результат рассмотрения заявления: ")
                                    { Space = SpaceProcessingModeValues.Preserve }),
                                new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                    new Text("отказ в предоставлении услуг отдыха и оздоровления"))));

                        doc.AppendChild(
                            new Paragraph(
                                new ParagraphProperties(new Justification { Val = JustificationValues.Both },
                                    new SpacingBetweenLines { After = "20" }),
                                new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                    new Text("Причина отказа: ") { Space = SpaceProcessingModeValues.Preserve }),
                                new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                    new Text(request.DeclineReason?.Name ?? "-"))));

                        doc.AppendChild(
                            new Paragraph(
                                new ParagraphProperties(new Justification { Val = JustificationValues.Both },
                                    new SpacingBetweenLines { After = "20" }),
                                new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                    new Text("Основание: ") { Space = SpaceProcessingModeValues.Preserve }),
                                new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                    new Text(
                                        "постановление Правительства Москвы от 22 февраля 2017 г. № 56-ПП \"Об организации отдыха и оздоровления детей, находящихся в трудной жизненной ситуации\""))));

                        doc.AppendChild(
                            new Paragraph(
                                new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                    new SpacingBetweenLines { After = "20" }),
                                new Run(new RunProperties().SetFont().SetFontSize("28").Bold(), new Text(" "))));
                    }
                    else if (request.StatusId == (long)StatusEnum.CertificateIssued)
                    {
                        doc.AppendChild(
                            new Paragraph(
                                new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                    new SpacingBetweenLines { After = "20" }),
                                new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                    new Text("Вид отдыха: ") { Space = SpaceProcessingModeValues.Preserve }),
                                new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                    new Text(request.TypeOfRest?.Name))));

                        AppendChildrenToDocument(doc, request);

                        doc.AppendChild(
                            new Paragraph(
                                new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                    new SpacingBetweenLines { After = "20" }),
                                new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                    new Text("Результат рассмотрения заявления: ")
                                    { Space = SpaceProcessingModeValues.Preserve }),
                                new Run(titleRequestRunPropertiesItalic.CloneNode(true), new Text("услуга оказана"))));

                        doc.AppendChild(
                            new Paragraph(
                                new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                    new SpacingBetweenLines { After = "20" }),
                                new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                    new Text("Номер путевки: ") { Space = SpaceProcessingModeValues.Preserve }),
                                new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                    new Text(request.CertificateNumber))));

                        doc.AppendChild(
                            new Paragraph(
                                new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                    new SpacingBetweenLines { After = "20" }),
                                new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                    new Text("Организация отдыха и оздоровления: ")
                                    { Space = SpaceProcessingModeValues.Preserve }),
                                new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                    new Text(request.Hotels?.Name ?? request.Tour?.Hotels?.Name ?? string.Empty))));

                        doc.AppendChild(
                            new Paragraph(
                                new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                    new SpacingBetweenLines { After = "20" }),
                                new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                    new Text("Время отдыха: ") { Space = SpaceProcessingModeValues.Preserve }),
                                new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                    new Text(request.TimeOfRest.Name))));

                        doc.AppendChild(
                            new Paragraph(
                                new ParagraphProperties(new Justification { Val = JustificationValues.Both },
                                    new SpacingBetweenLines { After = "20" }),
                                new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                    new Text("Основание: ") { Space = SpaceProcessingModeValues.Preserve }),
                                new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                    new Text(
                                        "постановление Правительства Москвы от 22 февраля 2017 г. № 56-ПП \"Об организации отдыха и оздоровления детей, находящихся в трудной жизненной ситуации\""))));

                        doc.AppendChild(
                            new Paragraph(
                                new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                    new SpacingBetweenLines { After = "20" }),
                                new Run(new RunProperties().SetFont().SetFontSize("28").Bold(), new Text(" "))));
                    }

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                new SpacingBetweenLines { After = "20" }),
                            new Run(new RunProperties().SetFont().SetFontSize("28").Bold(), new Text(" "))));
                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                new SpacingBetweenLines { After = "20" }),
                            new Run(new RunProperties().SetFont().SetFontSize("28").Bold(), new Text(" "))));

                    SignBlockNotification(doc, $"{applicant.LastName} {applicant.FirstName} {applicant.MiddleName}",
                        request.StatusId != (long)StatusEnum.CertificateIssued);

                    mainPart.Document = doc;
                }

                data = ms.ToArray();
            }

            return data;
        }

        /// <summary>
        ///     Уведомление о необходимости выбора организации отдыха и оздоровления
        /// </summary>
        [AllowAnonymous]
        public ActionResult NotificationOrgChoose(long requestId, string uniqueKey)
        {
            if (Settings.Default.SecretKey != uniqueKey && !Security.GetCurrentAccountId().HasValue)
            {
                throw new HttpException(404, "UniqueKey error");
            }

            var request = UnitOfWork.GetById<Request>(requestId);

            var data = WordProcessor.NotificationOrgChoose(request, Security.GetCurrentAccount());
            return File(data.FileBody, data.MimeType, data.FileName);
        }

        private static void AppendChildrenToDocument(Document doc, Request request)
        {
            var titleRequestRunProperties = new RunProperties().SetFont().SetFontSize();

            var titleRequestRunPropertiesBold = titleRequestRunProperties.CloneNode(true);
            titleRequestRunPropertiesBold.AppendChild(new Bold());
            var titleRequestRunPropertiesItalic = titleRequestRunProperties.CloneNode(true);
            //titleRequestRunPropertiesItalic.AppendChild(new Italic());

            if (request.Child != null)
            {
                foreach (var child in request.Child.Where(c => !c.IsDeleted))
                {
                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                new SpacingBetweenLines { After = "20" }),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Данные о ребенке: ") { Space = SpaceProcessingModeValues.Preserve }),
                            new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                new Text(
                                    $"{child.LastName} {child.FirstName} {child.MiddleName}, {child.DateOfBirth.FormatEx(string.Empty)}"))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                new SpacingBetweenLines { After = "20" }),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Льготная категория ребенка: ") { Space = SpaceProcessingModeValues.Preserve }),
                            new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                new Text($"{child.BenefitType?.Name}"))));
                }
            }

            if (request.TypeOfRestId == (long)TypeOfRestEnum.YouthRestOrphanCamps)
            {
                var child = request.Applicant;
                doc.AppendChild(
                    new Paragraph(
                        new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                            new SpacingBetweenLines { After = "20" }),
                        new Run(titleRequestRunPropertiesBold.CloneNode(true),
                            new Text("Данные о ребенке: ") { Space = SpaceProcessingModeValues.Preserve }),
                        new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                            new Text(
                                $"{child.LastName} {child.FirstName} {child.MiddleName}, {child.DateOfBirth.FormatEx(string.Empty)}"))));

                doc.AppendChild(
                    new Paragraph(
                        new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                            new SpacingBetweenLines { After = "20" }),
                        new Run(titleRequestRunPropertiesBold.CloneNode(true),
                            new Text("Льготная категория ребенка: ") { Space = SpaceProcessingModeValues.Preserve }),
                        new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                            new Text(
                                "Лица, из числа детей-сирот и детей, оставшихся без попечения родителей, в возрасте от 18 до 23 лет (включительно), обучающиеся по образовательным программам среднего профессионального образования или образовательным программам высшего образования по очной форме обучения"))));
            }
        }

        /// <summary>
        ///     уведомление об отказе в установленный срок
        /// </summary>
        [Authorize]
        public ActionResult NotificationDeadline(long requestId)
        {
            var data = WordProcessor.NotificationDataSwitch(UnitOfWork, Security.GetCurrentAccount(), requestId);
            return File(data.FileBody, data.MimeType, data.FileName);
        }
    }
}
