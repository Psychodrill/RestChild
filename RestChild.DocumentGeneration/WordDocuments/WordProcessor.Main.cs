using System;
using System.Collections.Generic;
using System.Linq;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Wordprocessing;
using MimeTypes;
using RestChild.Comon;
using RestChild.Comon.Enumeration;
using RestChild.Comon.OpenXmlExtensions;
using RestChild.Domain;

namespace RestChild.DocumentGeneration
{
    /// <summary>
    ///     Процессор документов Word
    /// </summary>
    public static partial class WordProcessor
    {
        //пробел
        public const string Space = " ";

        //размеры шрифтов
        private const string Size16 = "16";
        private const string Size18 = "18";
        private const string Size20 = "20";
        private const string Size22 = "22";
        private const string Size24 = "24";
        private const string Size28 = "28";

        //отступы
        private const string SpacingBetweenLinesAfter = "20";
        private const string SpacingBetweenLinesLine = "240";

        //константы для подписи в подвале документа
        private const int FirstColumn = 2731; // исполнитель/ответственный
        private const int SecondColumn = 3931; // ФИО
        private const int ThirdColumn = 55; // разделитель
        private const int FourthColumn = FirstColumn; // Подпись
        private const int FifthColumn = ThirdColumn; // второй разделитель
        private const int SixthColumn = 1804; // Дата

        //отступ первой строки
        private const int FirstLineIndentation600 = 600;

        public const string FederalLaw = "Порядок организации отдыха и оздоровления детей, находящихся в трудной жизненной ситуации, утвержденный постановлением Правительства Москвы от 22 февраля 2017 г. № 56-ПП \"Об организации отдыха и оздоровления детей, находящихся в трудной жизненной ситуации\" (далее – Порядок).";
        public const string FederalShort2020Law = "Порядок организации отдыха и оздоровления детей на основании путевок для отдыха и оздоровления при выездном отдыхе с полной оплатой стоимости путевки для отдыха и оздоровления за счет средств бюджета города Москвы с датами заезда, начиная с 1 апреля по 27 июля 2020 г., в другое время либо замены указанных путевок на сертификат на отдых и оздоровление";
        public const string FederalShort2021Law = "Порядок организации отдыха и оздоровления детей, находящихся в трудной жизненной ситуации, утвержденный постановлением Правительства Москвы от 22 февраля 2017 г. № 56-ПП \"Об организации отдыха и оздоровления детей, находящихся в трудной жизненной ситуации\"";
        public const string FederalLawReference = "пункты 8.9 и 9.17 Порядка организации отдыха и оздоровления детей, находящихся в трудной жизненной ситуации, утвержденный постановлением Правительства Москвы от 22 февраля 2017 г. № 56-ПП \"Об организации отдыха и оздоровления детей, находящихся в трудной жизненной ситуации\" (далее – Порядок).";
        public const string ParticipateNotification = "в случае неучастия заявителя, подавшего заявление о предоставлении услуг отдыха и оздоровления в отношении организации индивидуального выездного отдыха либо совместного выездного отдыха ребенка, находящегося в трудной жизненной ситуации и указанного в пункте 3.1.2 Порядка, лица из числа детей-сирот и детей, оставшихся без попечения родителей, во втором этапе заявочной кампании, услуга отдыха и оздоровления не считается оказанной.";

        private const string extention = ".docx";
        private static readonly string mimeType = MimeTypeMap.GetMimeType(extention);

        #region Utilst

        private static void DocumentHeaderRegistration(Document doc)
        {
            var titleProp = new RunProperties().SetFont().SetFontSize().Bold();
            doc.AppendChild(new Paragraph(new ParagraphProperties(new Justification {Val = JustificationValues.Center},
                    new SpacingBetweenLines {After = SpacingBetweenLinesAfter, Line = SpacingBetweenLinesLine}),
                new Run(titleProp.CloneNode(true), new Text("ДЕПАРТАМЕНТ КУЛЬТУРЫ ГОРОДА МОСКВЫ"))));
            doc.AppendChild(new Paragraph(new ParagraphProperties(new Justification {Val = JustificationValues.Center},
                    new SpacingBetweenLines {After = SpacingBetweenLinesAfter, Line = SpacingBetweenLinesLine}),
                new Run(titleProp.CloneNode(true),
                    new Text("Государственное автономное учреждение культуры города Москвы"))));
            doc.AppendChild(new Paragraph(new ParagraphProperties(new Justification {Val = JustificationValues.Center},
                    new SpacingBetweenLines {After = SpacingBetweenLinesAfter, Line = SpacingBetweenLinesLine}),
                new Run(titleProp.CloneNode(true), new Text("\"Московское агентство организации отдыха и туризма\""))));
            var pp = new ParagraphProperties(new Justification {Val = JustificationValues.Center},
                new SpacingBetweenLines {After = SpacingBetweenLinesAfter, Line = SpacingBetweenLinesLine});
            pp.AppendChild(new ParagraphBorders().BottomBorder("000000", 3));
            doc.AppendChild(new Paragraph(pp, new Run(titleProp.CloneNode(true), new Text("(ГАУК \"МОСГОРТУР\")") ,new Break())));
        }

        /// <summary>
        ///     Формирование таблицы перечня документов
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="Docs"></param>
        private static void AddTableDocsList(Document doc, IEnumerable<string> Docs)
        {
            const string _rowonewight = "1000";
            const string _rowtwowight = "9266";

            var tblProp = new TableProperties(
                new TableBorders(
                    new TopBorder {Val = new EnumValue<BorderValues>(BorderValues.Single)},
                    new BottomBorder {Val = new EnumValue<BorderValues>(BorderValues.Single)},
                    new LeftBorder {Val = new EnumValue<BorderValues>(BorderValues.Single)},
                    new RightBorder {Val = new EnumValue<BorderValues>(BorderValues.Single)},
                    new InsideHorizontalBorder {Val = new EnumValue<BorderValues>(BorderValues.Single)},
                    new InsideVerticalBorder {Val = new EnumValue<BorderValues>(BorderValues.Single)}));
            var captionRunProperties = new RunProperties
            {
                RunFonts = new RunFonts
                {
                    Ascii = "Times New Roman",
                    HighAnsi = "Times New Roman",
                    ComplexScript = "Times New Roman"
                },
                FontSize = new FontSize {Val = "20"},
                Italic = new Italic()
            };

            var table = new Table();
            table.AppendChild(tblProp.CloneNode(true));

            var _pgf = new List<string>();

            foreach (var _doc in Docs)
            {
                if (_doc.StartsWith("#"))
                {
                    _pgf.Add(_doc.Substring(1));

                    continue;
                }

                if (_doc.StartsWith("$"))
                {
                    _pgf.Add(_doc.Substring(1));
                }
                else
                {
                    if (_pgf.Any())
                    {
                        var _row = new TableRow();
                        var _cell_one = new TableCell();
                        _cell_one.Append(
                            new TableCellProperties(
                                new TableCellWidth {Type = TableWidthUnitValues.Dxa, Width = _rowonewight}),
                            new TableCellVerticalAlignment {Val = TableVerticalAlignmentValues.Bottom});
                        _cell_one.AppendChild(new Paragraph(
                            new ParagraphProperties(
                                new Justification {Val = JustificationValues.Left}),
                            new Run(captionRunProperties.CloneNode(true),
                                new Text("  "))));
                        _row.AppendChild(_cell_one);

                        var _cell_two = new TableCell();
                        _cell_two.Append(
                            new TableCellProperties(
                                new TableCellWidth {Type = TableWidthUnitValues.Dxa, Width = _rowtwowight},
                                new VerticalMerge {Val = MergedCellValues.Restart}
                            ),
                            new TableCellVerticalAlignment {Val = TableVerticalAlignmentValues.Bottom});
                        foreach (var _p in _pgf)
                        {
                            _cell_two.Append(new Paragraph(
                                new ParagraphProperties(
                                    new Justification {Val = JustificationValues.Left}),
                                new Run(captionRunProperties.CloneNode(true),
                                    new Text(_p))));
                        }

                        _row.AppendChild(_cell_two);
                        table.AppendChild(_row);

                        for (var i = _pgf.Count() > 1 ? 2 : 3; i < _pgf.Count(); i++)
                        {
                            var __row = new TableRow();
                            var __cell_one = new TableCell();
                            __cell_one.Append(
                                new TableCellProperties(
                                    new TableCellWidth {Type = TableWidthUnitValues.Dxa, Width = _rowonewight}),
                                new TableCellVerticalAlignment {Val = TableVerticalAlignmentValues.Bottom});
                            __cell_one.AppendChild(new Paragraph(
                                new ParagraphProperties(
                                    new Justification {Val = JustificationValues.Left}),
                                new Run(captionRunProperties.CloneNode(true),
                                    new Text("  "))));
                            __row.AppendChild(__cell_one);

                            var __cell_two = new TableCell();
                            __cell_two.Append(
                                new TableCellProperties(
                                    new TableCellWidth {Type = TableWidthUnitValues.Dxa, Width = _rowtwowight},
                                    new VerticalMerge {Val = MergedCellValues.Continue}
                                ),
                                new TableCellVerticalAlignment {Val = TableVerticalAlignmentValues.Bottom});
                            __cell_two.Append(new Paragraph(
                                new ParagraphProperties(
                                    new Justification {Val = JustificationValues.Left}),
                                new Run(captionRunProperties.CloneNode(true),
                                    new Text("  "))));

                            __row.AppendChild(__cell_two);
                            table.AppendChild(__row);
                        }

                        _pgf.Clear();
                    }

                    var row = new TableRow();
                    var cell_one = new TableCell();
                    cell_one.Append(
                        new TableCellProperties(
                            new TableCellWidth {Type = TableWidthUnitValues.Dxa, Width = _rowonewight}),
                        new TableCellVerticalAlignment {Val = TableVerticalAlignmentValues.Bottom});
                    cell_one.AppendChild(new Paragraph(
                        new ParagraphProperties(
                            new Justification {Val = JustificationValues.Left}),
                        new Run(captionRunProperties.CloneNode(true),
                            new Text("  "))));
                    row.AppendChild(cell_one);

                    var cell_two = new TableCell();
                    cell_two.Append(
                        new TableCellProperties(
                            new TableCellWidth {Type = TableWidthUnitValues.Dxa, Width = _rowtwowight}),
                        new TableCellVerticalAlignment {Val = TableVerticalAlignmentValues.Bottom});
                    cell_two.AppendChild(new Paragraph(
                        new ParagraphProperties(
                            new Justification {Val = JustificationValues.Left}),
                        new Run(captionRunProperties.CloneNode(true),
                            new Text(_doc))));
                    row.AppendChild(cell_two);
                    table.AppendChild(row);
                }
            }

            doc.AppendChild(table);
        }

        /// <summary>
        ///     Формирование таблицы об услугах, оказанных ребенку\детям в течение последних 3-х лет
        /// </summary>
        private static void AddTableChildTours(Document doc, IEnumerable<Request> requests, IEnumerable<int>years)
        {
            var titleRequestRunProperties = new RunProperties();
            titleRequestRunProperties.AppendChild(new RunFonts
            {
                Ascii = "Times New Roman",
                HighAnsi = "Times New Roman",
                ComplexScript = "Times New Roman"
            });
            titleRequestRunProperties.AppendChild(new FontSize {Val = Size22});

            var titleRequestRunPropertiesBold = titleRequestRunProperties.CloneNode(true);
            titleRequestRunPropertiesBold.AppendChild(new Bold());

            var tblProp = new TableProperties(
                new TableBorders(
                    new TopBorder {Val = new EnumValue<BorderValues>(BorderValues.Single)},
                    new BottomBorder {Val = new EnumValue<BorderValues>(BorderValues.Single)},
                    new LeftBorder {Val = new EnumValue<BorderValues>(BorderValues.Single)},
                    new RightBorder {Val = new EnumValue<BorderValues>(BorderValues.Single)},
                    new InsideHorizontalBorder {Val = new EnumValue<BorderValues>(BorderValues.Single)},
                    new InsideVerticalBorder {Val = new EnumValue<BorderValues>(BorderValues.Single)}));

            var table = new Table();
            table.AppendChild(tblProp.CloneNode(true));

            //первая строка (шапка)
            var row = new TableRow();
            var cell = new TableCell();
            cell.Append(new TableCellProperties(
                    new TableCellWidth {Type = TableWidthUnitValues.Dxa, Width = FirstColumn.ToString()}),
                new TableCellVerticalAlignment {Val = TableVerticalAlignmentValues.Bottom});
            cell.AppendChild(new Paragraph(
                new ParagraphProperties(new Justification {Val = JustificationValues.Left}),
                new Run(titleRequestRunPropertiesBold.CloneNode(true),
                    new Text("Год кампании"))));
            row.AppendChild(cell);

            cell = new TableCell();
            cell.Append(new TableCellProperties(
                    new TableCellWidth {Type = TableWidthUnitValues.Dxa, Width = SecondColumn.ToString()},
                    new TableCellBorders(new BottomBorder {Val = new EnumValue<BorderValues>(BorderValues.Single)})),
                new TableCellVerticalAlignment {Val = TableVerticalAlignmentValues.Bottom});
            cell.AppendChild(new Paragraph(
                new ParagraphProperties(new Justification {Val = JustificationValues.Left}),
                new Run(titleRequestRunPropertiesBold.CloneNode(true),
                    new Text("Вид услуги (путевка, сертификат, компенсация)"))));
            row.AppendChild(cell);

            cell = new TableCell();
            cell.Append(new TableCellProperties(
                    new TableCellWidth
                    {
                        Type = TableWidthUnitValues.Dxa,
                        Width = (ThirdColumn + FourthColumn + FifthColumn /*+ SixthColumn*/).ToString()
                    }),
                new TableCellVerticalAlignment {Val = TableVerticalAlignmentValues.Bottom});
            cell.AppendChild(new Paragraph(
                new Run(titleRequestRunPropertiesBold.CloneNode(true),
                    new Text("Организация отдыха и оздоровления (в случае предоставления путевки для отдыха и оздоровления), даты заезда"))));
            row.AppendChild(cell);
            table.AppendChild(row);

            var moneyTypes = new[]
            {
                (long?) TypeOfRestEnum.MoneyOn18, (long?) TypeOfRestEnum.MoneyOn3To7,
                (long?) TypeOfRestEnum.MoneyOn7To15, (long?) TypeOfRestEnum.MoneyOnInvalidOn4To17
            };

            //данные о предоставленных услугах

            foreach (var year in years)
            {

                Request request = requests.FirstOrDefault(req => req.YearOfRest.Year == year);

                //string year = request?.YearOfRest?.Name;
                //if (request != null)
                //{
                //    if (request.ParentRequestId.HasValue && request.YearOfRest?.Year - 1 == request?.ParentRequest.YearOfRest?.Year)
                //    {
                //        year = $"{request.ParentRequest.YearOfRest?.Name} (дополнительная кампания)";
                //    }
                //}
                //else if (request == null)
                //{
                //    year = $"{request.ParentRequest.YearOfRest?.Name} (дополнительная кампания)";
                //}

                row = new TableRow();
                cell = new TableCell();
                cell.Append(new TableCellProperties(
                        new TableCellWidth {Type = TableWidthUnitValues.Dxa, Width = FirstColumn.ToString()}),
                    new TableCellVerticalAlignment {Val = TableVerticalAlignmentValues.Bottom});
                cell.AppendChild(new Paragraph(
                    new ParagraphProperties(new Justification {Val = JustificationValues.Left}),
                    new Run(titleRequestRunProperties.CloneNode(true),
                        new Text(year.ToString()))));
                row.AppendChild(cell);

                cell = new TableCell();
                cell.Append(new TableCellProperties(
                        new TableCellWidth {Type = TableWidthUnitValues.Dxa, Width = SecondColumn.ToString()},
                        new TableCellBorders(new BottomBorder
                            {Val = new EnumValue<BorderValues>(BorderValues.Single)})),
                    new TableCellVerticalAlignment {Val = TableVerticalAlignmentValues.Bottom});
                cell.AppendChild(new Paragraph(
                    new ParagraphProperties(new Justification {Val = JustificationValues.Left}),
                    new Run(titleRequestRunProperties.CloneNode(true),
                        new Text(request?.TypeOfRest?.Name))));
                row.AppendChild(cell);

                cell = new TableCell();
                cell.Append(new TableCellProperties(
                        new TableCellWidth
                        {
                            Type = TableWidthUnitValues.Dxa,
                            Width = (ThirdColumn + FourthColumn + FifthColumn + SixthColumn).ToString()
                        }),
                    new TableCellVerticalAlignment {Val = TableVerticalAlignmentValues.Bottom});
                cell.AppendChild(new Paragraph(
                    new Run(titleRequestRunProperties.CloneNode(true),
                        new Text(request == null? Space:
                            request.TourId.HasValue
                            ? $"{request.Tour.Hotels?.Name}, c {request.Tour.DateIncome.FormatEx(string.Empty)} по {request.Tour.DateOutcome.FormatEx(string.Empty)}"
                            : (request.RequestOnMoney && !moneyTypes.Contains(request.TypeOfRestId)
                                ? "Осуществлен выбор сертификата на втором этапе заявочной кампании"
                                : Space)))));
                row.AppendChild(cell);
                table.AppendChild(row);
            }

            doc.AppendChild(table);
        }

        /// <summary>
        ///     Формирование статической таблицы об услугах, оказанных ребенку\детям в течение последних 3-х лет
        /// </summary>
        private static void AddStaticTableChildTours(Document doc, IEnumerable<Request> requests, IEnumerable<long> yearIds)
        {

            var titleRequestRunProperties = new RunProperties();
            titleRequestRunProperties.AppendChild(new RunFonts
            {
                Ascii = "Times New Roman",
                HighAnsi = "Times New Roman",
                ComplexScript = "Times New Roman"
            });
            titleRequestRunProperties.AppendChild(new FontSize { Val = Size22 });

            var titleRequestRunPropertiesBold = titleRequestRunProperties.CloneNode(true);
            titleRequestRunPropertiesBold.AppendChild(new Bold());

            var tblProp = new TableProperties(
                new TableBorders(
                    new TopBorder { Val = new EnumValue<BorderValues>(BorderValues.Single) },
                    new BottomBorder { Val = new EnumValue<BorderValues>(BorderValues.Single) },
                    new LeftBorder { Val = new EnumValue<BorderValues>(BorderValues.Single) },
                    new RightBorder { Val = new EnumValue<BorderValues>(BorderValues.Single) },
                    new InsideHorizontalBorder { Val = new EnumValue<BorderValues>(BorderValues.Single) },
                    new InsideVerticalBorder { Val = new EnumValue<BorderValues>(BorderValues.Single) }));

            var table = new Table();
            table.AppendChild(tblProp.CloneNode(true));

            //первая строка (шапка)
            var row = new TableRow();
            var cell = new TableCell();
            cell.Append(new TableCellProperties(
                    new TableCellWidth { Type = TableWidthUnitValues.Dxa, Width = FirstColumn.ToString() }),
                new TableCellVerticalAlignment { Val = TableVerticalAlignmentValues.Bottom });
            cell.AppendChild(new Paragraph(
                new ParagraphProperties(new Justification { Val = JustificationValues.Left }),
                new Run(titleRequestRunPropertiesBold.CloneNode(true),
                    new Text("Год кампании"))));
            row.AppendChild(cell);

            cell = new TableCell();
            cell.Append(new TableCellProperties(
                    new TableCellWidth { Type = TableWidthUnitValues.Dxa, Width = SecondColumn.ToString() },
                    new TableCellBorders(new BottomBorder { Val = new EnumValue<BorderValues>(BorderValues.Single) })),
                new TableCellVerticalAlignment { Val = TableVerticalAlignmentValues.Bottom });
            cell.AppendChild(new Paragraph(
                new ParagraphProperties(new Justification { Val = JustificationValues.Left }),
                new Run(titleRequestRunPropertiesBold.CloneNode(true),
                    new Text("Вид услуги (путевка, сертификат, компенсация)"))));
            row.AppendChild(cell);

            cell = new TableCell();
            cell.Append(new TableCellProperties(
                    new TableCellWidth
                    {
                        Type = TableWidthUnitValues.Dxa,
                        Width = (ThirdColumn + FourthColumn + FifthColumn + SixthColumn).ToString()
                    }),
                new TableCellVerticalAlignment { Val = TableVerticalAlignmentValues.Bottom });
            cell.AppendChild(new Paragraph(
                new Run(titleRequestRunPropertiesBold.CloneNode(true),
                    new Text("Организация отдыха и оздоровления (в случае предоставления путевки для отдыха и оздоровления), даты заезда"))));
            row.AppendChild(cell);
            table.AppendChild(row);

            var moneyTypes = new[]
            {
                (long?) TypeOfRestEnum.MoneyOn18, (long?) TypeOfRestEnum.MoneyOn3To7,
                (long?) TypeOfRestEnum.MoneyOn7To15, (long?) TypeOfRestEnum.MoneyOnInvalidOn4To17
            };

            //данные о предоставленных льготах
            foreach (var request in requests)
            {
                var year = request.YearOfRest?.Name;
                if (request.ParentRequestId.HasValue &&
                    request.YearOfRest?.Year - 1 == request.ParentRequest.YearOfRest?.Year)
                {
                    year = $"{request.ParentRequest.YearOfRest?.Name} (дополнительная кампания)";
                }

                row = new TableRow();
                cell = new TableCell();
                cell.Append(new TableCellProperties(
                        new TableCellWidth { Type = TableWidthUnitValues.Dxa, Width = FirstColumn.ToString() }),
                    new TableCellVerticalAlignment { Val = TableVerticalAlignmentValues.Bottom });
                cell.AppendChild(new Paragraph(
                    new ParagraphProperties(new Justification { Val = JustificationValues.Left }),
                    new Run(titleRequestRunProperties.CloneNode(true),
                        new Text(year))));
                row.AppendChild(cell);

                cell = new TableCell();
                cell.Append(new TableCellProperties(
                        new TableCellWidth { Type = TableWidthUnitValues.Dxa, Width = SecondColumn.ToString() },
                        new TableCellBorders(new BottomBorder
                        { Val = new EnumValue<BorderValues>(BorderValues.Single) })),
                    new TableCellVerticalAlignment { Val = TableVerticalAlignmentValues.Bottom });
                cell.AppendChild(new Paragraph(
                    new ParagraphProperties(new Justification { Val = JustificationValues.Left }),
                    new Run(titleRequestRunProperties.CloneNode(true),
                        new Text(request.TypeOfRest?.Name))));
                row.AppendChild(cell);

                cell = new TableCell();
                cell.Append(new TableCellProperties(
                        new TableCellWidth
                        {
                            Type = TableWidthUnitValues.Dxa,
                            Width = (ThirdColumn + FourthColumn + FifthColumn + SixthColumn).ToString()
                        }),
                    new TableCellVerticalAlignment { Val = TableVerticalAlignmentValues.Bottom });
                cell.AppendChild(new Paragraph(
                    new Run(titleRequestRunProperties.CloneNode(true),
                        new Text(request.TourId.HasValue
                            ? $"{request.Tour.Hotels?.Name}, c {request.Tour.DateIncome.FormatEx(string.Empty)} по {request.Tour.DateOutcome.FormatEx(string.Empty)}"
                            : (request.RequestOnMoney && !moneyTypes.Contains(request.TypeOfRestId)
                                ? "Осуществлен выбор сертификата на втором этапе заявочной кампании"
                                : Space)))));
                row.AppendChild(cell);
                table.AppendChild(row);
            }

            doc.AppendChild(table);
        }


        /// <summary>
        ///     Блок подписией для ЛОК 2019
        /// </summary>
        private static void SignBlockNotification2019(Document doc, Account account, string signText = null)
        {
            if (string.IsNullOrWhiteSpace(signText))
            {
                signText =
                    "Подпись заявителя, подтверждающая получение уведомления о регистрации заявления на выплату компенсации за самостоятельно приобретенную родителями или иными законными представителями путевку для отдыха и оздоровления:";
            }

            SignWorkerBlock(doc, account);

            var titleRequestRunProperties = new RunProperties();
            titleRequestRunProperties.AppendChild(new RunFonts
            {
                Ascii = "Times New Roman",
                HighAnsi = "Times New Roman",
                ComplexScript = "Times New Roman"
            });
            titleRequestRunProperties.AppendChild(new FontSize {Val = "22"});

            var tblProp = new TableProperties(
                new TableBorders(
                    new TopBorder {Val = new EnumValue<BorderValues>(BorderValues.None)},
                    new BottomBorder {Val = new EnumValue<BorderValues>(BorderValues.None)},
                    new LeftBorder {Val = new EnumValue<BorderValues>(BorderValues.None)},
                    new RightBorder {Val = new EnumValue<BorderValues>(BorderValues.None)},
                    new InsideHorizontalBorder {Val = new EnumValue<BorderValues>(BorderValues.None)},
                    new InsideVerticalBorder {Val = new EnumValue<BorderValues>(BorderValues.None)}));
            var signRunProperties = new RunProperties().SetFont().SetFontSizeSupperscript();

            doc.AppendChild(
                new Paragraph(
                    new ParagraphProperties(new Justification {Val = JustificationValues.Both},
                        new SpacingBetweenLines {After = Size20},
                        new Indentation {FirstLine = FirstLineIndentation600.ToString()}),
                    new Run(titleRequestRunProperties.CloneNode(true),
                        new Text(signText) {Space = SpaceProcessingModeValues.Preserve})
                ));

            var table = new Table();
            //---------------------------------------------------------------------------
            var row = new TableRow();

            table.AppendChild(tblProp.CloneNode(true));

            var cell = new TableCell();
            cell.Append(new TableCellProperties(
                    new TableCellWidth {Type = TableWidthUnitValues.Dxa, Width = "1731"}),
                new TableCellVerticalAlignment {Val = TableVerticalAlignmentValues.Bottom});
            cell.AppendChild(new Paragraph(
                new ParagraphProperties(new Justification {Val = JustificationValues.Center}),
                new Run(titleRequestRunProperties.CloneNode(true),
                    new Text(Space))));
            row.AppendChild(cell);

            cell = new TableCell();
            cell.Append(new TableCellProperties(
                    new TableCellWidth {Type = TableWidthUnitValues.Dxa, Width = "2731"},
                    new TableCellBorders(new BottomBorder {Val = new EnumValue<BorderValues>(BorderValues.Single)})),
                new TableCellVerticalAlignment {Val = TableVerticalAlignmentValues.Bottom});
            cell.AppendChild(new Paragraph(
                new ParagraphProperties(new Justification {Val = JustificationValues.Center}),
                new Run(titleRequestRunProperties.CloneNode(true),
                    new Text(Space))));
            row.AppendChild(cell);


            cell = new TableCell();
            cell.Append(new TableCellProperties(
                    new TableCellWidth {Type = TableWidthUnitValues.Dxa, Width = "55"}),
                new TableCellVerticalAlignment {Val = TableVerticalAlignmentValues.Bottom});
            cell.AppendChild(new Paragraph(new Run(new Text(Space))));
            row.AppendChild(cell);

            cell = new TableCell();
            cell.Append(new TableCellProperties(
                    new TableCellWidth {Type = TableWidthUnitValues.Dxa, Width = "3931"},
                    new TableCellBorders(new BottomBorder {Val = new EnumValue<BorderValues>(BorderValues.Single)})),
                new TableCellVerticalAlignment {Val = TableVerticalAlignmentValues.Bottom});
            cell.AppendChild(new Paragraph(new ParagraphProperties(new Justification {Val = JustificationValues.Left}),
                new Run(titleRequestRunProperties.CloneNode(true),
                    new Text(Space))));
            row.AppendChild(cell);

            cell = new TableCell();
            cell.Append(new TableCellProperties(
                    new TableCellWidth {Type = TableWidthUnitValues.Dxa, Width = "55"}),
                new TableCellVerticalAlignment {Val = TableVerticalAlignmentValues.Bottom});
            cell.AppendChild(new Paragraph(new Run(new Text(Space))));
            row.AppendChild(cell);

            cell = new TableCell();
            cell.Append(new TableCellProperties(
                    new TableCellWidth {Type = TableWidthUnitValues.Dxa, Width = "1804"},
                    new TableCellBorders(new BottomBorder {Val = new EnumValue<BorderValues>(BorderValues.Single)})),
                new TableCellVerticalAlignment {Val = TableVerticalAlignmentValues.Bottom});
            cell.AppendChild(new Paragraph(
                new ParagraphProperties(new Justification {Val = JustificationValues.Center}),
                new Run(titleRequestRunProperties.CloneNode(true),
                    new Text(DateTime.Now.Date.FormatEx()))));
            row.AppendChild(cell);

            table.AppendChild(row);

            // --------------------------------------------------------------------------------------
            row = new TableRow();

            cell = new TableCell();
            cell.Append(new TableCellProperties(
                    new TableCellWidth {Type = TableWidthUnitValues.Dxa, Width = "2731"}),
                new TableCellVerticalAlignment {Val = TableVerticalAlignmentValues.Bottom});
            cell.AppendChild(new Paragraph(
                new ParagraphProperties(new Justification {Val = JustificationValues.Center}),
                new Run(titleRequestRunProperties.CloneNode(true),
                    new Text(Space))));
            row.AppendChild(cell);

            cell = new TableCell();
            cell.Append(new TableCellProperties(
                    new TableCellWidth {Type = TableWidthUnitValues.Dxa, Width = "2731"}),
                new TableCellVerticalAlignment {Val = TableVerticalAlignmentValues.Bottom});
            cell.AppendChild(new Paragraph(
                new ParagraphProperties(new Justification {Val = JustificationValues.Center}),
                new Run(signRunProperties.CloneNode(true), new Text("(подпись)"))));
            row.AppendChild(cell);


            cell = new TableCell();
            cell.Append(new TableCellProperties(
                    new TableCellWidth {Type = TableWidthUnitValues.Dxa, Width = "55"}),
                new TableCellVerticalAlignment {Val = TableVerticalAlignmentValues.Bottom});
            cell.AppendChild(new Paragraph(new Run(new Text(Space))));
            row.AppendChild(cell);

            cell = new TableCell();
            cell.Append(new TableCellProperties(
                    new TableCellWidth {Type = TableWidthUnitValues.Dxa, Width = "2931"}),
                new TableCellVerticalAlignment {Val = TableVerticalAlignmentValues.Bottom});
            cell.AppendChild(new Paragraph(
                new ParagraphProperties(new Justification {Val = JustificationValues.Center}),
                new Run(signRunProperties.CloneNode(true), new Text("(Ф.И.О. заявителя, расшифровка подписи)"))));
            row.AppendChild(cell);

            cell = new TableCell();
            cell.Append(new TableCellProperties(
                    new TableCellWidth {Type = TableWidthUnitValues.Dxa, Width = "55"}),
                new TableCellVerticalAlignment {Val = TableVerticalAlignmentValues.Bottom});
            cell.AppendChild(new Paragraph(new Run(new Text(Space))));
            row.AppendChild(cell);

            cell = new TableCell();
            cell.Append(new TableCellProperties(
                    new TableCellWidth {Type = TableWidthUnitValues.Dxa, Width = "1804"}),
                new TableCellVerticalAlignment {Val = TableVerticalAlignmentValues.Bottom});
            cell.AppendChild(new Paragraph(
                new ParagraphProperties(new Justification {Val = JustificationValues.Center}),
                new Run(signRunProperties.CloneNode(true), new Text("(дата)"))));
            row.AppendChild(cell);

            table.AppendChild(row);

            doc.AppendChild(table);
        }

        /// <summary>
        ///     Блок подписией для ЛОК 2020
        /// </summary>
        public static void SignBlockNotification2020(Document doc, Account account, string functionName = Space)
        {
            var titleRequestRunProperties = new RunProperties();
            titleRequestRunProperties.AppendChild(new RunFonts
            {
                Ascii = "Times New Roman",
                HighAnsi = "Times New Roman",
                ComplexScript = "Times New Roman"
            });
            titleRequestRunProperties.AppendChild(new FontSize {Val = Size22});

            var tblProp = new TableProperties(
                new TableBorders(
                    new TopBorder {Val = new EnumValue<BorderValues>(BorderValues.None)},
                    new BottomBorder {Val = new EnumValue<BorderValues>(BorderValues.None)},
                    new LeftBorder {Val = new EnumValue<BorderValues>(BorderValues.None)},
                    new RightBorder {Val = new EnumValue<BorderValues>(BorderValues.None)},
                    new InsideHorizontalBorder {Val = new EnumValue<BorderValues>(BorderValues.None)},
                    new InsideVerticalBorder {Val = new EnumValue<BorderValues>(BorderValues.None)}));
            var signRunProperties = new RunProperties().SetFont().SetFontSizeSupperscript();

            var table = new Table();
            table.AppendChild(tblProp.CloneNode(true));


            //отступ - нулевая строка
            {
                doc.AppendChild(new Paragraph(
                new ParagraphProperties(new Justification { Val = JustificationValues.Both },
                new SpacingBetweenLines { After = Size20 })));
            }
            //первая строка
            {
                var row = new TableRow();

                var cell = new TableCell();
                cell.Append(new TableCellProperties(
                        new TableCellWidth {Type = TableWidthUnitValues.Dxa, Width = FirstColumn.ToString()}),
                    new TableCellVerticalAlignment {Val = TableVerticalAlignmentValues.Bottom});
                cell.AppendChild(new Paragraph(
                    new ParagraphProperties(new Justification {Val = JustificationValues.Center}, new SpacingBetweenLines { After="1"}),
                    new Run(titleRequestRunProperties.CloneNode(true),
                        new Text(Space))));
                row.AppendChild(cell);

                cell = new TableCell();
                cell.Append(new TableCellProperties(
                        new TableCellWidth {Type = TableWidthUnitValues.Dxa, Width = SecondColumn.ToString()},
                        new TableCellBorders(new BottomBorder
                            {Val = new EnumValue<BorderValues>(BorderValues.Single)})),
                    new TableCellVerticalAlignment {Val = TableVerticalAlignmentValues.Bottom});
                cell.AppendChild(new Paragraph(
                    new ParagraphProperties(new Justification {Val = JustificationValues.Center}, new SpacingBetweenLines { After = "1" }),
                    new Run(titleRequestRunProperties.CloneNode(true),
                        new Text(Space))));
                row.AppendChild(cell);

                cell = new TableCell();
                cell.Append(new TableCellProperties(
                        new TableCellWidth {Type = TableWidthUnitValues.Dxa, Width = ThirdColumn.ToString()}),
                    new TableCellVerticalAlignment {Val = TableVerticalAlignmentValues.Bottom});
                cell.AppendChild(new Paragraph(new ParagraphProperties( new SpacingBetweenLines { After = "1" }),new Run(new Text(Space))));
                row.AppendChild(cell);

                cell = new TableCell();
                cell.Append(new TableCellProperties(
                        new TableCellWidth {Type = TableWidthUnitValues.Dxa, Width = FourthColumn.ToString()},
                        new TableCellBorders(new BottomBorder
                            {Val = new EnumValue<BorderValues>(BorderValues.Single)})),
                    new TableCellVerticalAlignment {Val = TableVerticalAlignmentValues.Bottom});
                cell.AppendChild(new Paragraph(
                    new ParagraphProperties(new Justification {Val = JustificationValues.Left}, new SpacingBetweenLines { After = "1" }),
                    new Run(titleRequestRunProperties.CloneNode(true),
                        new Text(Space))));
                row.AppendChild(cell);

                cell = new TableCell();
                cell.Append(new TableCellProperties(
                        new TableCellWidth {Type = TableWidthUnitValues.Dxa, Width = FifthColumn.ToString()}),
                    new TableCellVerticalAlignment {Val = TableVerticalAlignmentValues.Bottom});
                cell.AppendChild(new Paragraph(new ParagraphProperties( new SpacingBetweenLines { After = "1" }), new Run(new Text(Space))));
                row.AppendChild(cell);

                cell = new TableCell();
                cell.Append(new TableCellProperties(
                        new TableCellWidth {Type = TableWidthUnitValues.Dxa, Width = SixthColumn.ToString()},
                        new TableCellBorders(new BottomBorder
                            {Val = new EnumValue<BorderValues>(BorderValues.Single)})),
                    new TableCellVerticalAlignment {Val = TableVerticalAlignmentValues.Bottom});
                cell.AppendChild(new Paragraph(
                    new ParagraphProperties(new Justification {Val = JustificationValues.Center}, new SpacingBetweenLines { After = "1" }),
                    new Run(titleRequestRunProperties.CloneNode(true),
                        new Text(DateTime.Now.Date.FormatEx()))));
                row.AppendChild(cell);

                table.AppendChild(row);
            }

            //вторая строка
            {
                var row = new TableRow();

                var cell = new TableCell();
                cell.Append(new TableCellProperties(
                        new TableCellWidth {Type = TableWidthUnitValues.Dxa, Width = FirstColumn.ToString()}),
                    new TableCellVerticalAlignment {Val = TableVerticalAlignmentValues.Bottom});
                cell.AppendChild(new Paragraph(
                    new ParagraphProperties(new Justification {Val = JustificationValues.Center}),
                    new Run(titleRequestRunProperties.CloneNode(true),
                        new Text(Space))));
                row.AppendChild(cell);

                cell = new TableCell();
                cell.Append(new TableCellProperties(
                        new TableCellWidth {Type = TableWidthUnitValues.Dxa, Width = SecondColumn.ToString()}),
                    new TableCellVerticalAlignment {Val = TableVerticalAlignmentValues.Bottom});
                cell.AppendChild(new Paragraph(
                    new ParagraphProperties(new Justification {Val = JustificationValues.Center}),
                    new Run(signRunProperties.CloneNode(true), new Text("(подпись заявителя)"))));
                row.AppendChild(cell);


                cell = new TableCell();
                cell.Append(new TableCellProperties(
                        new TableCellWidth {Type = TableWidthUnitValues.Dxa, Width = ThirdColumn.ToString()}),
                    new TableCellVerticalAlignment {Val = TableVerticalAlignmentValues.Bottom});
                cell.AppendChild(new Paragraph(new Run(new Text(Space))));
                row.AppendChild(cell);

                cell = new TableCell();
                cell.Append(new TableCellProperties(
                        new TableCellWidth {Type = TableWidthUnitValues.Dxa, Width = FourthColumn.ToString()}),
                    new TableCellVerticalAlignment {Val = TableVerticalAlignmentValues.Bottom});
                cell.AppendChild(new Paragraph(
                    new ParagraphProperties(new Justification {Val = JustificationValues.Center}),
                    new Run(signRunProperties.CloneNode(true), new Text("(ФИО заявителя, расшифровка подписи)"))));
                row.AppendChild(cell);

                cell = new TableCell();
                cell.Append(new TableCellProperties(
                        new TableCellWidth {Type = TableWidthUnitValues.Dxa, Width = FifthColumn.ToString()}),
                    new TableCellVerticalAlignment {Val = TableVerticalAlignmentValues.Bottom});
                cell.AppendChild(new Paragraph(new Run(new Text(Space))));
                row.AppendChild(cell);

                cell = new TableCell();
                cell.Append(new TableCellProperties(
                        new TableCellWidth {Type = TableWidthUnitValues.Dxa, Width = SixthColumn.ToString()}),
                    new TableCellVerticalAlignment {Val = TableVerticalAlignmentValues.Bottom});
                cell.AppendChild(new Paragraph(
                    new ParagraphProperties(new Justification {Val = JustificationValues.Center}),
                    new Run(signRunProperties.CloneNode(true), new Text("(дата)"))));
                row.AppendChild(cell);

                table.AppendChild(row);
            }

            //третья строка
            {
                var row = new TableRow();

                var cell = new TableCell();
                cell.Append(new TableCellProperties(
                        new TableCellWidth {Type = TableWidthUnitValues.Dxa, Width = FirstColumn.ToString()}),
                    new TableCellVerticalAlignment {Val = TableVerticalAlignmentValues.Bottom});
                cell.AppendChild(new Paragraph(
                    new ParagraphProperties(new Justification {Val = JustificationValues.Center}, new SpacingBetweenLines { After = "1" }),
                    new Run(titleRequestRunProperties.CloneNode(true),
                        new Text(functionName))));
                row.AppendChild(cell);

                cell = new TableCell();
                cell.Append(new TableCellProperties(
                        new TableCellWidth {Type = TableWidthUnitValues.Dxa, Width = SecondColumn.ToString()},
                        new TableCellBorders(new BottomBorder
                            {Val = new EnumValue<BorderValues>(BorderValues.Single)})),
                    new TableCellVerticalAlignment {Val = TableVerticalAlignmentValues.Bottom});
                cell.AppendChild(new Paragraph(
                    new ParagraphProperties(new Justification {Val = JustificationValues.Center}, new SpacingBetweenLines { After = "1" }),
                    new Run(titleRequestRunProperties.CloneNode(true),
                        new Text(
                            $"{account.Name.FormatEx()}{(string.IsNullOrWhiteSpace(account.Position) ? string.Empty : $", {account.Position}")}"))));
                row.AppendChild(cell);

                cell = new TableCell();
                cell.Append(new TableCellProperties(
                        new TableCellWidth {Type = TableWidthUnitValues.Dxa, Width = ThirdColumn.ToString()}),
                    new TableCellVerticalAlignment {Val = TableVerticalAlignmentValues.Bottom});
                cell.AppendChild(new Paragraph(new ParagraphProperties(new SpacingBetweenLines { After = "1" }), new Run(new Text(Space))));
                row.AppendChild(cell);

                cell = new TableCell();
                cell.Append(new TableCellProperties(
                        new TableCellWidth {Type = TableWidthUnitValues.Dxa, Width = FourthColumn.ToString()},
                        new TableCellBorders(new BottomBorder
                            {Val = new EnumValue<BorderValues>(BorderValues.Single)})),
                    new TableCellVerticalAlignment {Val = TableVerticalAlignmentValues.Bottom});
                cell.AppendChild(new Paragraph(
                    new ParagraphProperties(new Justification {Val = JustificationValues.Left}, new SpacingBetweenLines { After = "1" }),
                    new Run(titleRequestRunProperties.CloneNode(true),
                        new Text(Space))));
                row.AppendChild(cell);

                cell = new TableCell();
                cell.Append(new TableCellProperties(
                        new TableCellWidth {Type = TableWidthUnitValues.Dxa, Width = FifthColumn.ToString()}),
                    new TableCellVerticalAlignment {Val = TableVerticalAlignmentValues.Bottom});
                cell.AppendChild(new Paragraph(new ParagraphProperties(new SpacingBetweenLines { After = "1" }), new Run(new Text(Space))));
                row.AppendChild(cell);

                cell = new TableCell();
                cell.Append(new TableCellProperties(
                        new TableCellWidth {Type = TableWidthUnitValues.Dxa, Width = SixthColumn.ToString()}),
                    new TableCellVerticalAlignment {Val = TableVerticalAlignmentValues.Bottom});
                cell.AppendChild(new Paragraph(
                    new ParagraphProperties(new Justification {Val = JustificationValues.Center}, new SpacingBetweenLines { After = "1" }),
                    new Run(titleRequestRunProperties.CloneNode(true),
                        new Text(Space))));
                row.AppendChild(cell);

                table.AppendChild(row);
            }

            //четвёртая строка
            {
                var row = new TableRow();

                var cell = new TableCell();
                cell.Append(new TableCellProperties(
                        new TableCellWidth {Type = TableWidthUnitValues.Dxa, Width = FirstColumn.ToString()}),
                    new TableCellVerticalAlignment {Val = TableVerticalAlignmentValues.Bottom});
                cell.AppendChild(new Paragraph(
                    new ParagraphProperties(new Justification {Val = JustificationValues.Center}),
                    new Run(titleRequestRunProperties.CloneNode(true),
                        new Text(Space))));
                row.AppendChild(cell);

                cell = new TableCell();
                cell.Append(new TableCellProperties(
                        new TableCellWidth {Type = TableWidthUnitValues.Dxa, Width = SecondColumn.ToString()}),
                    new TableCellVerticalAlignment {Val = TableVerticalAlignmentValues.Bottom});
                cell.AppendChild(new Paragraph(
                    new ParagraphProperties(new Justification {Val = JustificationValues.Center}),
                    new Run(signRunProperties.CloneNode(true), new Text("(ФИО работника, должность)"))));
                row.AppendChild(cell);


                cell = new TableCell();
                cell.Append(new TableCellProperties(
                        new TableCellWidth {Type = TableWidthUnitValues.Dxa, Width = ThirdColumn.ToString()}),
                    new TableCellVerticalAlignment {Val = TableVerticalAlignmentValues.Bottom});
                cell.AppendChild(new Paragraph(new Run(new Text(Space))));
                row.AppendChild(cell);

                cell = new TableCell();
                cell.Append(new TableCellProperties(
                        new TableCellWidth {Type = TableWidthUnitValues.Dxa, Width = FourthColumn.ToString()}),
                    new TableCellVerticalAlignment {Val = TableVerticalAlignmentValues.Bottom});
                cell.AppendChild(new Paragraph(
                    new ParagraphProperties(new Justification {Val = JustificationValues.Center}),
                    new Run(signRunProperties.CloneNode(true), new Text("(подпись работника)"))));
                row.AppendChild(cell);

                cell = new TableCell();
                cell.Append(new TableCellProperties(
                        new TableCellWidth {Type = TableWidthUnitValues.Dxa, Width = FifthColumn.ToString()}),
                    new TableCellVerticalAlignment {Val = TableVerticalAlignmentValues.Bottom});
                cell.AppendChild(new Paragraph(new Run(new Text(Space))));
                row.AppendChild(cell);

                cell = new TableCell();
                cell.Append(new TableCellProperties(
                        new TableCellWidth {Type = TableWidthUnitValues.Dxa, Width = SixthColumn.ToString()}),
                    new TableCellVerticalAlignment {Val = TableVerticalAlignmentValues.Bottom});
                cell.AppendChild(new Paragraph(
                    new ParagraphProperties(new Justification {Val = JustificationValues.Center}),
                    new Run(signRunProperties.CloneNode(true), new Text(Space))));
                row.AppendChild(cell);

                table.AppendChild(row);
            }

            doc.AppendChild(table);
        }

        /// <summary>
        ///     Сведения о заявлении  (запись руками)
        /// </summary>
        public static void CertHandInput(Document doc)
        {
            var titleRequestRunProperties = new RunProperties();
            titleRequestRunProperties.AppendChild(new RunFonts
            {
                Ascii = "Times New Roman",
                HighAnsi = "Times New Roman",
                ComplexScript = "Times New Roman"
            });
            titleRequestRunProperties.AppendChild(new FontSize { Val = Size22 });

            var tblProp = new TableProperties(
                new TableBorders(
                    new TopBorder { Val = new EnumValue<BorderValues>(BorderValues.None) },
                    new BottomBorder { Val = new EnumValue<BorderValues>(BorderValues.None) },
                    new LeftBorder { Val = new EnumValue<BorderValues>(BorderValues.None) },
                    new RightBorder { Val = new EnumValue<BorderValues>(BorderValues.None) },
                    new InsideHorizontalBorder { Val = new EnumValue<BorderValues>(BorderValues.None) },
                    new InsideVerticalBorder { Val = new EnumValue<BorderValues>(BorderValues.None) }));
            var signRunProperties = new RunProperties().SetFont().SetFontSizeSupperscript();

            var table = new Table();
            table.AppendChild(tblProp.CloneNode(true));

            //первая строка
            {
                var row = new TableRow();

                var cell = new TableCell();
                cell.Append(new TableCellProperties(
                        new TableCellWidth { Type = TableWidthUnitValues.Dxa, Width = (FirstColumn + ThirdColumn).ToString() },
                        new TableCellBorders(new BottomBorder
                        { Val = new EnumValue<BorderValues>(BorderValues.Single) })),
                    new TableCellVerticalAlignment { Val = TableVerticalAlignmentValues.Bottom });
                cell.AppendChild(new Paragraph(
                    new ParagraphProperties(new Justification { Val = JustificationValues.Left }),
                    new Run(titleRequestRunProperties.CloneNode(true),
                        new Text(Space))));
                row.AppendChild(cell);

                cell = new TableCell();
                cell.Append(new TableCellProperties(
                        new TableCellWidth { Type = TableWidthUnitValues.Dxa, Width = (ThirdColumn * 3).ToString()}),
                    new TableCellVerticalAlignment { Val = TableVerticalAlignmentValues.Bottom });
                cell.AppendChild(new Paragraph(new Run(new Text(Space))));
                row.AppendChild(cell);

                cell = new TableCell();
                cell.Append(new TableCellProperties(
                        new TableCellWidth { Type = TableWidthUnitValues.Dxa, Width = (SecondColumn + ThirdColumn).ToString() },
                        new TableCellBorders(new BottomBorder
                        { Val = new EnumValue<BorderValues>(BorderValues.Single) })),
                    new TableCellVerticalAlignment { Val = TableVerticalAlignmentValues.Bottom });
                cell.AppendChild(new Paragraph(
                    new ParagraphProperties(new Justification { Val = JustificationValues.Left }),
                    new Run(titleRequestRunProperties.CloneNode(true),
                        new Text(Space))));
                row.AppendChild(cell);

                table.AppendChild(row);
            }

            //вторая строка
            {
                var row = new TableRow();

                var cell = new TableCell();
                cell.Append(new TableCellProperties(
                        new TableCellWidth { Type = TableWidthUnitValues.Dxa, Width = (FirstColumn + ThirdColumn).ToString() }),
                    new TableCellVerticalAlignment { Val = TableVerticalAlignmentValues.Bottom });
                cell.AppendChild(new Paragraph(
                    new ParagraphProperties(new Justification { Val = JustificationValues.Center }),
                    new Run(signRunProperties.CloneNode(true), new Text("(дата регистрации заявления)"))));
                row.AppendChild(cell);

                cell = new TableCell();
                cell.Append(new TableCellProperties(
                        new TableCellWidth { Type = TableWidthUnitValues.Dxa, Width = (ThirdColumn * 3).ToString() }),
                    new TableCellVerticalAlignment { Val = TableVerticalAlignmentValues.Bottom });
                cell.AppendChild(new Paragraph(new Run(new Text(Space))));
                row.AppendChild(cell);


                cell = new TableCell();
                cell.Append(new TableCellProperties(
                        new TableCellWidth { Type = TableWidthUnitValues.Dxa, Width = (SecondColumn + ThirdColumn).ToString() }),
                    new TableCellVerticalAlignment { Val = TableVerticalAlignmentValues.Bottom });
                cell.AppendChild(new Paragraph(
                    new ParagraphProperties(new Justification { Val = JustificationValues.Center }),
                    new Run(signRunProperties.CloneNode(true), new Text("(регистрационный номер заявления)"))));
                row.AppendChild(cell);

                table.AppendChild(row);
            }

            doc.AppendChild(table);
        }


        /// <summary>
        ///     Подпись работника для компенсации
        /// </summary>
        private static void SignWorkerBlock(Document doc, Account account, string name = "Принял:")
        {
            //заглушка
            account = account ?? new Account();
            var titleRequestRunProperties = new RunProperties();
            titleRequestRunProperties.AppendChild(new RunFonts
            {
                Ascii = "Times New Roman",
                HighAnsi = "Times New Roman",
                ComplexScript = "Times New Roman"
            });
            titleRequestRunProperties.AppendChild(new FontSize {Val = "22"});

            var tblProp = new TableProperties(
                new TableBorders(
                    new TopBorder {Val = new EnumValue<BorderValues>(BorderValues.None)},
                    new BottomBorder {Val = new EnumValue<BorderValues>(BorderValues.None)},
                    new LeftBorder {Val = new EnumValue<BorderValues>(BorderValues.None)},
                    new RightBorder {Val = new EnumValue<BorderValues>(BorderValues.None)},
                    new InsideHorizontalBorder {Val = new EnumValue<BorderValues>(BorderValues.None)},
                    new InsideVerticalBorder {Val = new EnumValue<BorderValues>(BorderValues.None)}));

            var captionRunProperties = new RunProperties().SetFont().SetFontSizeSupperscript();



            var table = new Table();
            table.AppendChild(tblProp.CloneNode(true));

            //отступ - нулевая строка
            {
                doc.AppendChild(new Paragraph(
                new ParagraphProperties(new Justification { Val = JustificationValues.Both },
                new SpacingBetweenLines { After = Size20 })));
            }

            var row = new TableRow();

            var cell = new TableCell();
            cell.Append(new TableCellProperties(
                    new TableCellWidth {Type = TableWidthUnitValues.Dxa, Width = "1231"}),
                new TableCellVerticalAlignment {Val = TableVerticalAlignmentValues.Bottom});
            cell.AppendChild(new Paragraph(new ParagraphProperties(new Justification {Val = JustificationValues.Left}),
                new Run(titleRequestRunProperties.CloneNode(true),
                    new Text(name))));
            row.AppendChild(cell);


            cell = new TableCell();
            cell.Append(new TableCellProperties(
                new TableCellWidth {Type = TableWidthUnitValues.Dxa, Width = "6931"},
                new TableCellBorders(new BottomBorder {Val = new EnumValue<BorderValues>(BorderValues.Single)}),
                new TableCellVerticalAlignment {Val = TableVerticalAlignmentValues.Bottom}
            ));
            cell.AppendChild(new Paragraph(
                new ParagraphProperties(new Justification {Val = JustificationValues.Center}),
                new Run(titleRequestRunProperties.CloneNode(true),
                    new Text(
                        $"{account?.Name?.FormatEx()}{(string.IsNullOrWhiteSpace(account?.Position) ? string.Empty : $", {account.Position}")}"))));
            row.AppendChild(cell);

            cell = new TableCell();
            cell.Append(new TableCellProperties(
                    new TableCellWidth {Type = TableWidthUnitValues.Dxa, Width = "55"}),
                new TableCellVerticalAlignment {Val = TableVerticalAlignmentValues.Bottom});
            cell.AppendChild(new Paragraph(new Run(new Text(Space))));
            row.AppendChild(cell);

            cell = new TableCell();
            cell.Append(new TableCellProperties(
                    new TableCellWidth {Type = TableWidthUnitValues.Dxa, Width = "2931"},
                    new TableCellBorders(new BottomBorder {Val = new EnumValue<BorderValues>(BorderValues.Single)})),
                new TableCellVerticalAlignment {Val = TableVerticalAlignmentValues.Bottom});
            cell.AppendChild(new Paragraph(new Run(new Text(Space))));
            row.AppendChild(cell);

            table.AppendChild(row);
            // -----------------------------------------------------------
            row = new TableRow();

            cell = new TableCell();
            cell.Append(new TableCellProperties(
                    new TableCellWidth {Type = TableWidthUnitValues.Dxa, Width = "1231"}),
                new TableCellVerticalAlignment {Val = TableVerticalAlignmentValues.Bottom});
            cell.AppendChild(new Paragraph(
                new ParagraphProperties(new Justification {Val = JustificationValues.Center}),
                new Run(titleRequestRunProperties.CloneNode(true),
                    new Text(Space))));
            row.AppendChild(cell);


            cell = new TableCell();
            cell.Append(new TableCellProperties(
                    new TableCellWidth {Type = TableWidthUnitValues.Dxa, Width = "6931"}),
                new TableCellVerticalAlignment {Val = TableVerticalAlignmentValues.Bottom});
            cell.AppendChild(new Paragraph(
                new ParagraphProperties(new Justification {Val = JustificationValues.Center}),
                new Run(captionRunProperties.CloneNode(true), new Text("(Ф.И.О. работника, должность)"))));
            row.AppendChild(cell);

            cell = new TableCell();
            cell.Append(new TableCellProperties(
                    new TableCellWidth {Type = TableWidthUnitValues.Dxa, Width = "55"}),
                new TableCellVerticalAlignment {Val = TableVerticalAlignmentValues.Bottom});
            cell.AppendChild(new Paragraph(new Run(new Text(Space))));
            row.AppendChild(cell);

            cell = new TableCell();
            cell.Append(new TableCellProperties(
                    new TableCellWidth {Type = TableWidthUnitValues.Dxa, Width = "2931"}),
                new TableCellVerticalAlignment {Val = TableVerticalAlignmentValues.Bottom});
            cell.AppendChild(new Paragraph(
                new ParagraphProperties(new Justification {Val = JustificationValues.Center}),
                new Run(titleRequestRunProperties.CloneNode(true), captionRunProperties.CloneNode(true),
                    new Text("(подпись работника)"))));
            row.AppendChild(cell);


            table.AppendChild(row);

            doc.AppendChild(table);
        }

        private static void AppendChildrenToDocument(Document doc, Request request)
        {
            var titleRequestRunProperties = new RunProperties().SetFont().SetFontSize();

            var titleRequestRunPropertiesBold = titleRequestRunProperties.CloneNode(true);
            titleRequestRunPropertiesBold.AppendChild(new Bold());
            var titleRequestRunPropertiesItalic = titleRequestRunProperties.CloneNode(true);

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
                                        "Данные ребёнка/лица из числа детей-сирот и детей, оставшихся без попечения родителей: ")
                                    {Space = SpaceProcessingModeValues.Preserve}),
                            new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                new Text(
                                    $"{child.LastName} {child.FirstName} {child.MiddleName}, {child.DateOfBirth.FormatExGR(string.Empty)}"))));

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                                new SpacingBetweenLines {After = Size20}),
                            new Run(titleRequestRunPropertiesBold.CloneNode(true),
                                new Text("Льготная категория: ") {Space = SpaceProcessingModeValues.Preserve}),
                            new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                                new Text($"{child.BenefitType?.Name}"))));
                }
            }

            if (request.TypeOfRestId == (long) TypeOfRestEnum.YouthRestOrphanCamps)
            {
                var child = request.Applicant;
                doc.AppendChild(
                    new Paragraph(
                        new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                            new SpacingBetweenLines {After = Size20}),
                        new Run(titleRequestRunPropertiesBold.CloneNode(true),
                            new Text("Данные о ребенке: ") {Space = SpaceProcessingModeValues.Preserve}),
                        new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                            new Text(
                                $"{child.LastName} {child.FirstName} {child.MiddleName}, {child.DateOfBirth.FormatEx(string.Empty)}"))));

                doc.AppendChild(
                    new Paragraph(
                        new ParagraphProperties(new Justification {Val = JustificationValues.Left},
                            new SpacingBetweenLines {After = Size20}),
                        new Run(titleRequestRunPropertiesBold.CloneNode(true),
                            new Text("Льготная категория: ") {Space = SpaceProcessingModeValues.Preserve}),
                        new Run(titleRequestRunPropertiesItalic.CloneNode(true),
                            new Text(
                                "Лица, из числа детей-сирот и детей, оставшихся без попечения родителей, в возрасте от 18 до 23 лет (включительно), обучающиеся по образовательным программам среднего профессионального образования или образовательным программам высшего образования по очной форме обучения"))));
            }
        }

        private static void SignBlockNotification(Document doc, Account account, string applicantName,
            bool NotificationAccepted = true)
        {
            //заглушка
            account = account ?? new Account();
            var titleRequestRunProperties = new RunProperties();
            titleRequestRunProperties.AppendChild(new RunFonts
            {
                Ascii = "Times New Roman",
                HighAnsi = "Times New Roman",
                ComplexScript = "Times New Roman"
            });
            titleRequestRunProperties.AppendChild(new FontSize {Val = Size24});

            var tblProp = new TableProperties(
                new TableBorders(
                    new TopBorder {Val = new EnumValue<BorderValues>(BorderValues.None)},
                    new BottomBorder {Val = new EnumValue<BorderValues>(BorderValues.None)},
                    new LeftBorder {Val = new EnumValue<BorderValues>(BorderValues.None)},
                    new RightBorder {Val = new EnumValue<BorderValues>(BorderValues.None)},
                    new InsideHorizontalBorder {Val = new EnumValue<BorderValues>(BorderValues.None)},
                    new InsideVerticalBorder {Val = new EnumValue<BorderValues>(BorderValues.None)}));
            var captionRunProperties = new RunProperties().SetFont().SetFontSize("16");

            var table = new Table();
            table.AppendChild(tblProp.CloneNode(true));


            //отступ - нулевая строка
            {
                doc.AppendChild(new Paragraph(
                new ParagraphProperties(new Justification { Val = JustificationValues.Both },
                new SpacingBetweenLines { After = Size20 })));
            }

            var row = new TableRow();

            var cell = new TableCell();
            cell.Append(new TableCellProperties(
                    new TableCellWidth {Type = TableWidthUnitValues.Dxa, Width = "2731"}),
                new TableCellVerticalAlignment {Val = TableVerticalAlignmentValues.Bottom});
            cell.AppendChild(new Paragraph(new ParagraphProperties(new Justification {Val = JustificationValues.Left}),
                new Run(titleRequestRunProperties.CloneNode(true),
                    new Text("Уведомление выдал:"))));
            row.AppendChild(cell);


            cell = new TableCell();
            cell.Append(new TableCellProperties(
                new TableCellWidth {Type = TableWidthUnitValues.Dxa, Width = "2731"},
                new TableCellBorders(new BottomBorder {Val = new EnumValue<BorderValues>(BorderValues.Single)}),
                new TableCellVerticalAlignment {Val = TableVerticalAlignmentValues.Bottom}
            ));
            cell.AppendChild(new Paragraph(
                new ParagraphProperties(new Justification {Val = JustificationValues.Center}),
                new Run(titleRequestRunProperties.CloneNode(true),
                    new Text(account?.Name?.FormatEx()))));
            row.AppendChild(cell);

            cell = new TableCell();
            cell.Append(new TableCellProperties(
                    new TableCellWidth {Type = TableWidthUnitValues.Dxa, Width = "55"}),
                new TableCellVerticalAlignment {Val = TableVerticalAlignmentValues.Bottom});
            cell.AppendChild(new Paragraph(new Run(new Text(Space))));
            row.AppendChild(cell);

            cell = new TableCell();
            cell.Append(new TableCellProperties(
                    new TableCellWidth {Type = TableWidthUnitValues.Dxa, Width = "2931"},
                    new TableCellBorders(new BottomBorder {Val = new EnumValue<BorderValues>(BorderValues.Single)})),
                new TableCellVerticalAlignment {Val = TableVerticalAlignmentValues.Bottom});
            cell.AppendChild(new Paragraph(new Run(new Text(Space))));
            row.AppendChild(cell);

            cell = new TableCell();
            cell.Append(new TableCellProperties(
                    new TableCellWidth {Type = TableWidthUnitValues.Dxa, Width = "55"}),
                new TableCellVerticalAlignment {Val = TableVerticalAlignmentValues.Bottom});
            cell.AppendChild(new Paragraph(
                new ParagraphProperties(new Justification {Val = JustificationValues.Center}),
                new Run(titleRequestRunProperties.CloneNode(true),
                    new Text(Space))));
            row.AppendChild(cell);

            cell = new TableCell();
            cell.Append(new TableCellProperties(
                new TableCellWidth {Type = TableWidthUnitValues.Dxa, Width = "1804"},
                new TableCellBorders(new BottomBorder {Val = new EnumValue<BorderValues>(BorderValues.Single)}),
                new TableCellVerticalAlignment {Val = TableVerticalAlignmentValues.Bottom}));
            cell.AppendChild(new Paragraph(
                new ParagraphProperties(new Justification {Val = JustificationValues.Center}),
                new Run(titleRequestRunProperties.CloneNode(true),
                    new Text(DateTime.Now.Date.FormatEx()))));
            row.AppendChild(cell);

            table.AppendChild(row);
            // -----------------------------------------------------------
            row = new TableRow();

            cell = new TableCell();
            cell.Append(new TableCellProperties(
                    new TableCellWidth {Type = TableWidthUnitValues.Dxa, Width = "2731"}),
                new TableCellVerticalAlignment {Val = TableVerticalAlignmentValues.Bottom});
            cell.AppendChild(new Paragraph(
                new ParagraphProperties(new Justification {Val = JustificationValues.Center}),
                new Run(titleRequestRunProperties.CloneNode(true),
                    new Text(Space))));
            row.AppendChild(cell);


            cell = new TableCell();
            cell.Append(new TableCellProperties(
                    new TableCellWidth {Type = TableWidthUnitValues.Dxa, Width = "2731"}),
                new TableCellVerticalAlignment {Val = TableVerticalAlignmentValues.Bottom});
            cell.AppendChild(new Paragraph(
                new ParagraphProperties(new Justification {Val = JustificationValues.Center}),
                new Run(captionRunProperties.CloneNode(true), new Text("(Ф.И.О. работника)"))));
            row.AppendChild(cell);

            cell = new TableCell();
            cell.Append(new TableCellProperties(
                    new TableCellWidth {Type = TableWidthUnitValues.Dxa, Width = "55"}),
                new TableCellVerticalAlignment {Val = TableVerticalAlignmentValues.Bottom});
            cell.AppendChild(new Paragraph(new Run(new Text(Space))));
            row.AppendChild(cell);

            cell = new TableCell();
            cell.Append(new TableCellProperties(
                    new TableCellWidth {Type = TableWidthUnitValues.Dxa, Width = "2931"}),
                new TableCellVerticalAlignment {Val = TableVerticalAlignmentValues.Bottom});
            cell.AppendChild(new Paragraph(
                new ParagraphProperties(new Justification {Val = JustificationValues.Center}),
                new Run(titleRequestRunProperties.CloneNode(true), captionRunProperties.CloneNode(true),
                    new Text("(подпись)"))));
            row.AppendChild(cell);

            cell = new TableCell();
            cell.Append(new TableCellProperties(
                    new TableCellWidth {Type = TableWidthUnitValues.Dxa, Width = "55"}),
                new TableCellVerticalAlignment {Val = TableVerticalAlignmentValues.Bottom});
            cell.AppendChild(new Paragraph(new Run(new Text(Space))));
            row.AppendChild(cell);

            cell = new TableCell();
            cell.Append(new TableCellProperties(
                    new TableCellWidth {Type = TableWidthUnitValues.Dxa, Width = "1804"}),
                new TableCellVerticalAlignment {Val = TableVerticalAlignmentValues.Bottom});
            cell.AppendChild(new Paragraph(
                new ParagraphProperties(new Justification {Val = JustificationValues.Center}),
                new Run(titleRequestRunProperties.CloneNode(true), captionRunProperties.CloneNode(true),
                    new Text("(дата)"))));
            row.AppendChild(cell);

            table.AppendChild(row);

            doc.AppendChild(table);

            if (NotificationAccepted)
            {
                table = new Table();
                //---------------------------------------------------------------------------
                row = new TableRow();

                table.AppendChild(tblProp.CloneNode(true));

                cell = new TableCell();
                cell.Append(new TableCellProperties(
                        new TableCellWidth {Type = TableWidthUnitValues.Dxa, Width = "2731"}),
                    new TableCellVerticalAlignment {Val = TableVerticalAlignmentValues.Bottom});
                cell.AppendChild(new Paragraph(
                    new ParagraphProperties(new Justification {Val = JustificationValues.Left}),
                    new Run(titleRequestRunProperties.CloneNode(true),
                        new Text("Уведомление принял:"))));
                row.AppendChild(cell);

                cell = new TableCell();
                cell.Append(new TableCellProperties(
                        new TableCellWidth {Type = TableWidthUnitValues.Dxa, Width = "2731"},
                        new TableCellBorders(new BottomBorder
                            {Val = new EnumValue<BorderValues>(BorderValues.Single)})),
                    new TableCellVerticalAlignment {Val = TableVerticalAlignmentValues.Bottom});
                cell.AppendChild(new Paragraph(
                    new ParagraphProperties(new Justification {Val = JustificationValues.Center}),
                    new Run(titleRequestRunProperties.CloneNode(true),
                        new Text(string.IsNullOrWhiteSpace(applicantName) ? "  " : applicantName))));
                row.AppendChild(cell);


                cell = new TableCell();
                cell.Append(new TableCellProperties(
                        new TableCellWidth {Type = TableWidthUnitValues.Dxa, Width = "55"}),
                    new TableCellVerticalAlignment {Val = TableVerticalAlignmentValues.Bottom});
                cell.AppendChild(new Paragraph(new Run(new Text(Space))));
                row.AppendChild(cell);

                cell = new TableCell();
                cell.Append(new TableCellProperties(
                        new TableCellWidth {Type = TableWidthUnitValues.Dxa, Width = "2931"},
                        new TableCellBorders(new BottomBorder
                            {Val = new EnumValue<BorderValues>(BorderValues.Single)})),
                    new TableCellVerticalAlignment {Val = TableVerticalAlignmentValues.Bottom});
                cell.AppendChild(new Paragraph(
                    new ParagraphProperties(new Justification {Val = JustificationValues.Left}),
                    new Run(titleRequestRunProperties.CloneNode(true),
                        new Text(Space))));
                row.AppendChild(cell);

                cell = new TableCell();
                cell.Append(new TableCellProperties(
                        new TableCellWidth {Type = TableWidthUnitValues.Dxa, Width = "55"}),
                    new TableCellVerticalAlignment {Val = TableVerticalAlignmentValues.Bottom});
                cell.AppendChild(new Paragraph(new Run(new Text(Space))));
                row.AppendChild(cell);

                cell = new TableCell();
                cell.Append(new TableCellProperties(
                        new TableCellWidth {Type = TableWidthUnitValues.Dxa, Width = "1804"},
                        new TableCellBorders(new BottomBorder
                            {Val = new EnumValue<BorderValues>(BorderValues.Single)})),
                    new TableCellVerticalAlignment {Val = TableVerticalAlignmentValues.Bottom});
                cell.AppendChild(new Paragraph(
                    new ParagraphProperties(new Justification {Val = JustificationValues.Center}),
                    new Run(titleRequestRunProperties.CloneNode(true),
                        new Text(DateTime.Now.Date.FormatEx()))));
                row.AppendChild(cell);

                table.AppendChild(row);

                // --------------------------------------------------------------------------------------
                row = new TableRow();

                cell = new TableCell();
                cell.Append(new TableCellProperties(
                        new TableCellWidth {Type = TableWidthUnitValues.Dxa, Width = "2731"}),
                    new TableCellVerticalAlignment {Val = TableVerticalAlignmentValues.Bottom});
                cell.AppendChild(new Paragraph(
                    new ParagraphProperties(new Justification {Val = JustificationValues.Center}),
                    new Run(titleRequestRunProperties.CloneNode(true),
                        new Text(Space))));
                row.AppendChild(cell);

                cell = new TableCell();
                cell.Append(new TableCellProperties(
                        new TableCellWidth {Type = TableWidthUnitValues.Dxa, Width = "2731"}),
                    new TableCellVerticalAlignment {Val = TableVerticalAlignmentValues.Bottom});
                cell.AppendChild(new Paragraph(
                    new ParagraphProperties(new Justification {Val = JustificationValues.Center}),
                    new Run(captionRunProperties.CloneNode(true), new Text("(Ф.И.О. заявителя)"))));
                row.AppendChild(cell);


                cell = new TableCell();
                cell.Append(new TableCellProperties(
                        new TableCellWidth {Type = TableWidthUnitValues.Dxa, Width = "55"}),
                    new TableCellVerticalAlignment {Val = TableVerticalAlignmentValues.Bottom});
                cell.AppendChild(new Paragraph(new Run(new Text(Space))));
                row.AppendChild(cell);

                cell = new TableCell();
                cell.Append(new TableCellProperties(
                        new TableCellWidth {Type = TableWidthUnitValues.Dxa, Width = "2931"}),
                    new TableCellVerticalAlignment {Val = TableVerticalAlignmentValues.Bottom});
                cell.AppendChild(new Paragraph(
                    new ParagraphProperties(new Justification {Val = JustificationValues.Center}),
                    new Run(captionRunProperties.CloneNode(true), new Text("(подпись)"))));
                row.AppendChild(cell);

                cell = new TableCell();
                cell.Append(new TableCellProperties(
                        new TableCellWidth {Type = TableWidthUnitValues.Dxa, Width = "55"}),
                    new TableCellVerticalAlignment {Val = TableVerticalAlignmentValues.Bottom});
                cell.AppendChild(new Paragraph(new Run(new Text(Space))));
                row.AppendChild(cell);

                cell = new TableCell();
                cell.Append(new TableCellProperties(
                        new TableCellWidth {Type = TableWidthUnitValues.Dxa, Width = "1804"}),
                    new TableCellVerticalAlignment {Val = TableVerticalAlignmentValues.Bottom});
                cell.AppendChild(new Paragraph(
                    new ParagraphProperties(new Justification {Val = JustificationValues.Center}),
                    new Run(captionRunProperties.CloneNode(true), new Text("(дата)"))));
                row.AppendChild(cell);

                table.AppendChild(row);

                doc.AppendChild(table);
            }
        }

        private static void SignWorkerCompensationBlock(Document doc, Account account = null)
        {
            account = account ?? new Account();

            var titleRequestRunProperties = new RunProperties();
            titleRequestRunProperties.AppendChild(new RunFonts
            {
                Ascii = "Times New Roman",
                HighAnsi = "Times New Roman",
                ComplexScript = "Times New Roman"
            });
            titleRequestRunProperties.AppendChild(new FontSize {Val = Size24});

            var tblProp = new TableProperties(
                new TableBorders(
                    new TopBorder {Val = new EnumValue<BorderValues>(BorderValues.None)},
                    new BottomBorder {Val = new EnumValue<BorderValues>(BorderValues.None)},
                    new LeftBorder {Val = new EnumValue<BorderValues>(BorderValues.None)},
                    new RightBorder {Val = new EnumValue<BorderValues>(BorderValues.None)},
                    new InsideHorizontalBorder {Val = new EnumValue<BorderValues>(BorderValues.None)},
                    new InsideVerticalBorder {Val = new EnumValue<BorderValues>(BorderValues.None)}));
            var captionRunProperties = new RunProperties().SetFont().SetFontSize(Size18);

            var table = new Table();
            table.AppendChild(tblProp.CloneNode(true));

            var row = new TableRow();

            var cell = new TableCell();
            cell.Append(new TableCellProperties(
                    new TableCellWidth {Type = TableWidthUnitValues.Dxa, Width = "1231"}),
                new TableCellVerticalAlignment {Val = TableVerticalAlignmentValues.Bottom});
            cell.AppendChild(new Paragraph(new ParagraphProperties(new Justification {Val = JustificationValues.Left}),
                new Run(titleRequestRunProperties.CloneNode(true),
                    new Text("Уведомление выдал:"))));
            row.AppendChild(cell);


            cell = new TableCell();
            cell.AppendChild(new TableCellProperties(
                new TableCellWidth {Type = TableWidthUnitValues.Dxa, Width = "6931"},
                new TableCellBorders(new BottomBorder {Val = new EnumValue<BorderValues>(BorderValues.Single)}),
                new TableCellVerticalAlignment {Val = TableVerticalAlignmentValues.Bottom}
            ));
            cell.AppendChild(new Paragraph(
                new ParagraphProperties(new Justification {Val = JustificationValues.Center}),
                new Run(titleRequestRunProperties.CloneNode(true),
                    new Text(
                        $"{account.Name.FormatEx()}{(string.IsNullOrWhiteSpace(account.Position) ? string.Empty : $", {account.Position}")}"))));
            row.AppendChild(cell);

            cell = new TableCell();
            cell.Append(new TableCellProperties(
                    new TableCellWidth {Type = TableWidthUnitValues.Dxa, Width = "55"}),
                new TableCellVerticalAlignment {Val = TableVerticalAlignmentValues.Bottom});
            cell.AppendChild(new Paragraph(new Run(new Text(" "))));
            row.AppendChild(cell);

            cell = new TableCell();
            cell.AppendChild(new TableCellProperties(
                new TableCellWidth {Type = TableWidthUnitValues.Dxa, Width = "2931"},
                new TableCellBorders(new BottomBorder {Val = new EnumValue<BorderValues>(BorderValues.Single)}),
                new TableCellVerticalAlignment {Val = TableVerticalAlignmentValues.Bottom}));

            cell.AppendChild(new Paragraph(
                new ParagraphProperties(new Justification {Val = JustificationValues.Center}),
                new Run(titleRequestRunProperties.CloneNode(true), new Text(DateTime.Today.FormatEx()))));
            row.AppendChild(cell);

            table.AppendChild(row);
            // -----------------------------------------------------------
            row = new TableRow();

            cell = new TableCell();
            cell.Append(new TableCellProperties(
                    new TableCellWidth {Type = TableWidthUnitValues.Dxa, Width = "1231"}),
                new TableCellVerticalAlignment {Val = TableVerticalAlignmentValues.Bottom});
            cell.AppendChild(new Paragraph(
                new ParagraphProperties(new Justification {Val = JustificationValues.Center}),
                new Run(titleRequestRunProperties.CloneNode(true),
                    new Text(" "))));
            row.AppendChild(cell);


            cell = new TableCell();
            cell.Append(new TableCellProperties(
                    new TableCellWidth {Type = TableWidthUnitValues.Dxa, Width = "6931"}),
                new TableCellVerticalAlignment {Val = TableVerticalAlignmentValues.Bottom});
            cell.AppendChild(new Paragraph(
                new ParagraphProperties(new Justification {Val = JustificationValues.Center}),
                new Run(captionRunProperties.CloneNode(true), new Text("(Ф.И.О. работника, должность)"))));
            row.AppendChild(cell);

            cell = new TableCell();
            cell.Append(new TableCellProperties(
                    new TableCellWidth {Type = TableWidthUnitValues.Dxa, Width = "55"}),
                new TableCellVerticalAlignment {Val = TableVerticalAlignmentValues.Bottom});
            cell.AppendChild(new Paragraph(new Run(new Text(" "))));
            row.AppendChild(cell);

            cell = new TableCell();
            cell.Append(new TableCellProperties(
                    new TableCellWidth {Type = TableWidthUnitValues.Dxa, Width = "2931"}),
                new TableCellVerticalAlignment {Val = TableVerticalAlignmentValues.Bottom});
            cell.AppendChild(new Paragraph(
                new ParagraphProperties(new Justification {Val = JustificationValues.Center}),
                new Run(titleRequestRunProperties.CloneNode(true), captionRunProperties.CloneNode(true),
                    new Text("(дата)"))));
            row.AppendChild(cell);


            table.AppendChild(row);

            doc.AppendChild(table);
        }

        #endregion
    }
}
