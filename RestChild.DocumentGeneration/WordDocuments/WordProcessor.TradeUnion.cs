using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using RestChild.Comon;
using RestChild.Comon.OpenXmlExtensions;
using RestChild.DAL;
using RestChild.DAL.RepositoryExtensions;
using RestChild.DocumentGeneration.Filters;
using RestChild.Domain;

namespace RestChild.DocumentGeneration
{
    public static partial class WordProcessor
    {
        /// <summary>
        ///     Перечень информации о детях
        /// </summary>
        public static IDocument TradeUnionWord(IUnitOfWork unitOfWork, TradeUnionWordFilter filter)
        {
            var list = unitOfWork.GetById<TradeUnionList>(filter.TradeUnionId);


            using (var ms = new MemoryStream())
            {
                using (var wordDocument = WordprocessingDocument.Create(ms, WordprocessingDocumentType.Document))
                {
                    var mainPart = wordDocument.AddMainDocumentPart();
                    var body = new Body();
                    var sectionPropertys = new SectionProperties();
                    sectionPropertys.AppendChild(new PageSize
                    { Orient = PageOrientationValues.Landscape, Width = 15840, Height = 12240 });
                    sectionPropertys.AppendChild(new PageMargin
                    {
                        Top = 851,
                        Right = 567U,
                        Bottom = 567,
                        Left = 567U,
                        Header = 720U,
                        Footer = 720U,
                        Gutter = 0U
                    });
                    body.AppendChild(sectionPropertys);
                    var doc = new Document(body);

                    mainPart.Document = doc;
                    var titleProp = new RunProperties().SetFont().SetFontSize("28");
                    var titlePropBold = new RunProperties().SetFont().SetFontSize("28").Bold();
                    doc.AppendChild(new Paragraph(
                        new ParagraphProperties(new Justification { Val = JustificationValues.Center }),
                        new Run(titleProp.CloneNode(true),
                            new Text(
                                "Перечень информации о детях, принявших участие в оздоровительных мероприятиях в организациях отдыха и оздоровления, с выделением субсидии из бюджета города Москвы"),
                            new Break()),
                        new Run(titlePropBold.CloneNode(true),
                            new Text($"{list?.Camp?.Name}"),
                            new Break()),
                        new Run(titlePropBold.CloneNode(true),
                            new Text(
                                $"{list?.GroupedTimeOfRest?.Name} {list?.YearOfRest?.Year} года (с {list?.DateFrom.FormatEx()} по {list?.DateTo.FormatEx()})")
                        )));

                    var table = new Table();

                    var tblProp = new TableProperties();

                    var tableMainStyle = new TableStyle { Val = "TableGrid" };
                    var tableMainWidth = new TableWidth { Width = "5000", Type = TableWidthUnitValues.Pct };
                    tblProp.Append(tableMainStyle, tableMainWidth);

                    table.AppendChild(tblProp);

                    var tg = new TableGrid(
                        new GridColumn { Width = "188" },
                        new GridColumn { Width = "616" },
                        new GridColumn { Width = "476" },
                        new GridColumn { Width = "1041" },
                        new GridColumn { Width = "851" },
                        new GridColumn { Width = "616" },
                        new GridColumn { Width = "571" },
                        new GridColumn { Width = "616" }
                    );
                    table.AppendChild(tg);

                    var tr = new TableRow();
                    tr.Text("№",
                        new TableCellProperties().Borders(new TableCellBorders().AllBorder()).CenterVAlign()
                            .Width("188"),
                        new ParagraphProperties().CenterAlign().NoSpacing(),
                        new RunProperties().SetFont().SetFontSize("20"));
                    tr.Text("Ф.И.О.",
                        new TableCellProperties().Borders(new TableCellBorders().AllBorder()).CenterVAlign()
                            .Width("616"),
                        new ParagraphProperties().CenterAlign().NoSpacing(),
                        new RunProperties().SetFont().SetFontSize("20"));
                    tr.Text("Дата рождения",
                        new TableCellProperties().Borders(new TableCellBorders().AllBorder()).CenterVAlign()
                            .Width("476"),
                        new ParagraphProperties().CenterAlign().NoSpacing(),
                        new RunProperties().SetFont().SetFontSize("20"));
                    tr.Text("Наименование и реквизиты документа, удостоверяющего личность",
                        new TableCellProperties().Borders(new TableCellBorders().AllBorder()).CenterVAlign()
                            .Width("1041"),
                        new ParagraphProperties().CenterAlign().NoSpacing(),
                        new RunProperties().SetFont().SetFontSize("20"));
                    tr.Text("Место жительства",
                        new TableCellProperties().Borders(new TableCellBorders().AllBorder()).CenterVAlign()
                            .Width("851"),
                        new ParagraphProperties().CenterAlign().NoSpacing(),
                        new RunProperties().SetFont().SetFontSize("20"));
                    tr.Text("ФИО законного представителя несовершеннолетнего лица",
                        new TableCellProperties().Borders(new TableCellBorders().AllBorder()).CenterVAlign()
                            .Width("616"),
                        new ParagraphProperties().CenterAlign().NoSpacing(),
                        new RunProperties().SetFont().SetFontSize("20"));
                    tr.Text("Контактный телефон законного представителя несовершеннолетнего лица",
                        new TableCellProperties().Borders(new TableCellBorders().AllBorder()).CenterVAlign()
                            .Width("571"),
                        new ParagraphProperties().CenterAlign().NoSpacing(),
                        new RunProperties().SetFont().SetFontSize("20"));
                    tr.Text("Место работы родителя",
                        new TableCellProperties().Borders(new TableCellBorders().AllBorder()).CenterVAlign()
                            .Width("616"),
                        new ParagraphProperties().CenterAlign().NoSpacing(),
                        new RunProperties().SetFont().SetFontSize("20"));
                    table.AppendChild(tr);

                    var children = list?.Campers?.Where(ss => !(filter.CameChildren ?? false) || ss.IsChecked).OrderBy(c => c.Child?.LastName).ThenBy(c => c.Child?.FirstName).ToList() ?? new List<TradeUnionCamper>();

                    var index = 1;
                    foreach (var child in children)
                    {
                        tr = new TableRow();
                        tr.Text(index.ToString(),
                            new TableCellProperties().Borders(new TableCellBorders().AllBorder()).CenterVAlign()
                                .Width("188"),
                            new ParagraphProperties().CenterAlign().NoSpacing(),
                            new RunProperties().SetFont().SetFontSize("20"));
                        tr.Text($"{child?.Child?.LastName} {child?.Child?.FirstName} {child?.Child?.MiddleName}",
                            new TableCellProperties().Borders(new TableCellBorders().AllBorder()).CenterVAlign()
                                .Width("616"),
                            new ParagraphProperties().CenterAlign().NoSpacing(),
                            new RunProperties().SetFont().SetFontSize("20"));
                        tr.Text($"{child?.Child?.DateOfBirth.FormatEx()}",
                            new TableCellProperties().Borders(new TableCellBorders().AllBorder()).CenterVAlign()
                                .Width("476"),
                            new ParagraphProperties().CenterAlign().NoSpacing(),
                            new RunProperties().SetFont().SetFontSize("20"));
                        tr.Text(
                            $"{child?.Child?.DocumentType?.Name} {child?.Child?.DocumentSeria} №{child?.Child?.DocumentNumber}, выдано {child?.Child?.DocumentSubjectIssue} {child?.Child?.DocumentDateOfIssue.FormatEx()}",
                            new TableCellProperties().Borders(new TableCellBorders().AllBorder()).CenterVAlign()
                                .Width("1041"),
                            new ParagraphProperties().CenterAlign().NoSpacing(),
                            new RunProperties().SetFont().SetFontSize("20"));
                        tr.Text(child?.Child?.Address?.ToString() ?? child?.AddressChild,
                            new TableCellProperties().Borders(new TableCellBorders().AllBorder()).CenterVAlign()
                                .Width("851"),
                            new ParagraphProperties().CenterAlign().NoSpacing(),
                            new RunProperties().SetFont().SetFontSize("20"));
                        tr.Text($"{child?.Parent?.LastName} {child?.Parent?.FirstName} {child?.Parent?.MiddleName}",
                            new TableCellProperties().Borders(new TableCellBorders().AllBorder()).CenterVAlign()
                                .Width("616"),
                            new ParagraphProperties().CenterAlign().NoSpacing(),
                            new RunProperties().SetFont().SetFontSize("20"));
                        tr.Text($"{child?.Parent?.Phone}",
                            new TableCellProperties().Borders(new TableCellBorders().AllBorder()).CenterVAlign()
                                .Width("571"),
                            new ParagraphProperties().CenterAlign().NoSpacing(),
                            new RunProperties().SetFont().SetFontSize("20"));
                        tr.Text($"{child?.ParentPlaceWork}",
                            new TableCellProperties().Borders(new TableCellBorders().AllBorder()).CenterVAlign()
                                .Width("616"),
                            new ParagraphProperties().CenterAlign().NoSpacing(),
                            new RunProperties().SetFont().SetFontSize("20"));
                        table.AppendChild(tr);
                        index++;
                    }

                    doc.AppendChild(table);

                    doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                new SpacingBetweenLines { After = Size20 }),
                            new Run(new RunProperties().SetFont().SetFontSize(Size16).Bold(), new Text(Space)))); doc.AppendChild(
                        new Paragraph(
                            new ParagraphProperties(new Justification { Val = JustificationValues.Left },
                                new SpacingBetweenLines { After = Size20 }),
                            new Run(new RunProperties().SetFont().SetFontSize(Size16).Bold(), new Text(Space))));


                    //блок подписей
                    var table2 = new Table();

                    var tableProperties2 = new TableProperties();
                    var tableWidth2 = new TableWidth{ Width = "0", Type = TableWidthUnitValues.Auto };
                    var tableLook2 = new TableLook{ Val = "04A0", FirstRow = true, LastRow = false, FirstColumn = true, LastColumn = false, NoHorizontalBand = false, NoVerticalBand = true };

                    tableProperties2.Append(tableWidth2);
                    tableProperties2.Append(tableLook2);

                    var tableGrid2 = new TableGrid();
                    var gridColumn9 = new GridColumn{ Width = "3417" };
                    var gridColumn10 = new GridColumn{ Width = "3417" };
                    var gridColumn11 = new GridColumn{ Width = "2916" };

                    tableGrid2.Append(gridColumn9);
                    tableGrid2.Append(gridColumn10);
                    tableGrid2.Append(gridColumn11);

                    var tableRow5 = new TableRow{ RsidTableRowMarkRevision = "00232403", RsidTableRowAddition = "004950F8", RsidTableRowProperties = "00751945", ParagraphId = "68AE8A96", TextId = "77777777" };

                    var tableCell33 = new TableCell();

                    var tableCellProperties33 = new TableCellProperties();
                    var tableCellWidth33 = new TableCellWidth { Width = "3417", Type = TableWidthUnitValues.Dxa };
                    var shading1 = new Shading { Val = ShadingPatternValues.Clear, Color = "auto", Fill = "auto" };

                    tableCellProperties33.Append(tableCellWidth33);
                    tableCellProperties33.Append(shading1);

                    var paragraph37 = new Paragraph { RsidParagraphMarkRevision = "004950F8", RsidParagraphAddition = "004950F8", RsidParagraphProperties = "00751945", RsidRunAdditionDefault = "004950F8", ParagraphId = "12AB6F73", TextId = "77777777" };

                    var paragraphProperties36 = new ParagraphProperties();
                    var autoSpaceDE3 = new AutoSpaceDE { Val = false };
                    var autoSpaceDN3 = new AutoSpaceDN { Val = false };
                    var adjustRightIndent3 = new AdjustRightIndent { Val = false };
                    var spacingBetweenLines34 = new SpacingBetweenLines { After = "0" };
                    var justification34 = new Justification { Val = JustificationValues.Center };

                    var paragraphMarkRunProperties3 = new ParagraphMarkRunProperties();
                    var runFonts26 = new RunFonts { Ascii = "Times New Roman", HighAnsi = "Times New Roman", EastAsia = "Calibri", ComplexScript = "Times New Roman" };
                    var fontSize26 = new FontSize { Val = "20" };
                    var fontSizeComplexScript4 = new FontSizeComplexScript{ Val = "20" };
                    var underline1 = new Underline { Val = UnderlineValues.Single };
                    var languages4 = new Languages { EastAsia = "en-US" };

                    paragraphMarkRunProperties3.Append(runFonts26);
                    paragraphMarkRunProperties3.Append(fontSize26);
                    paragraphMarkRunProperties3.Append(fontSizeComplexScript4);
                    paragraphMarkRunProperties3.Append(underline1);
                    paragraphMarkRunProperties3.Append(languages4);

                    paragraphProperties36.Append(autoSpaceDE3);
                    paragraphProperties36.Append(autoSpaceDN3);
                    paragraphProperties36.Append(adjustRightIndent3);
                    paragraphProperties36.Append(spacingBetweenLines34);
                    paragraphProperties36.Append(justification34);
                    paragraphProperties36.Append(paragraphMarkRunProperties3);

                    paragraph37.Append(paragraphProperties36);

                    var paragraph38 = new Paragraph{ RsidParagraphMarkRevision = "00232403", RsidParagraphAddition = "004950F8", RsidParagraphProperties = "00751945", RsidRunAdditionDefault = "004950F8", ParagraphId = "55DF56A2", TextId = "77777777" };

                    var paragraphProperties37 = new ParagraphProperties();
                    var autoSpaceDE4 = new AutoSpaceDE{ Val = false };
                    var autoSpaceDN4 = new AutoSpaceDN{ Val = false };
                    var adjustRightIndent4 = new AdjustRightIndent{ Val = false };
                    var spacingBetweenLines35 = new SpacingBetweenLines{ After = "0" };
                    var justification35 = new Justification{ Val = JustificationValues.Center };

                    var paragraphMarkRunProperties4 = new ParagraphMarkRunProperties();
                    var runFonts27 = new RunFonts{ Ascii = "Times New Roman", HighAnsi = "Times New Roman", EastAsia = "Calibri", ComplexScript = "Times New Roman" };
                    var fontSize27 = new FontSize{ Val = "20" };
                    var fontSizeComplexScript5 = new FontSizeComplexScript{ Val = "20" };
                    var underline2 = new Underline{ Val = UnderlineValues.Single };
                    var languages5 = new Languages{ EastAsia = "en-US" };

                    paragraphMarkRunProperties4.Append(runFonts27);
                    paragraphMarkRunProperties4.Append(fontSize27);
                    paragraphMarkRunProperties4.Append(fontSizeComplexScript5);
                    paragraphMarkRunProperties4.Append(underline2);
                    paragraphMarkRunProperties4.Append(languages5);

                    paragraphProperties37.Append(autoSpaceDE4);
                    paragraphProperties37.Append(autoSpaceDN4);
                    paragraphProperties37.Append(adjustRightIndent4);
                    paragraphProperties37.Append(spacingBetweenLines35);
                    paragraphProperties37.Append(justification35);
                    paragraphProperties37.Append(paragraphMarkRunProperties4);

                    var run23 = new Run{ RsidRunProperties = "00232403" };

                    var runProperties23 = new RunProperties();
                    var runFonts28 = new RunFonts{ Ascii = "Times New Roman", HighAnsi = "Times New Roman", EastAsia = "Calibri", ComplexScript = "Times New Roman" };
                    var fontSize28 = new FontSize{ Val = "20" };
                    var fontSizeComplexScript6 = new FontSizeComplexScript{ Val = "20" };
                    var languages6 = new Languages{ EastAsia = "en-US" };

                    runProperties23.Append(runFonts28);
                    runProperties23.Append(fontSize28);
                    runProperties23.Append(fontSizeComplexScript6);
                    runProperties23.Append(languages6);
                    var text22 = new Text { Text = "______" };
                    
                    run23.Append(runProperties23);
                    run23.Append(text22);

                    var run24 = new Run{ RsidRunProperties = "00232403" };

                    var runProperties24 = new RunProperties();
                    var runFonts29 = new RunFonts{ Ascii = "Times New Roman", HighAnsi = "Times New Roman", EastAsia = "Calibri", ComplexScript = "Times New Roman" };
                    var fontSize29 = new FontSize{ Val = "20" };
                    var fontSizeComplexScript7 = new FontSizeComplexScript{ Val = "20" };
                    var underline3 = new Underline{ Val = UnderlineValues.Single };
                    var languages7 = new Languages{ EastAsia = "en-US" };

                    runProperties24.Append(runFonts29);
                    runProperties24.Append(fontSize29);
                    runProperties24.Append(fontSizeComplexScript7);
                    runProperties24.Append(underline3);
                    runProperties24.Append(languages7);
                    var text23 = new Text{ Text = "Главный бухгалтер" };

                    run24.Append(runProperties24);
                    run24.Append(text23);

                    var run25 = new Run{ RsidRunProperties = "00232403" };

                    var runProperties25 = new RunProperties();
                    var runFonts30 = new RunFonts{ Ascii = "Times New Roman", HighAnsi = "Times New Roman", EastAsia = "Calibri", ComplexScript = "Times New Roman" };
                    var fontSize30 = new FontSize{ Val = "20" };
                    var fontSizeComplexScript8 = new FontSizeComplexScript{ Val = "20" };
                    var languages8 = new Languages{ EastAsia = "en-US" };

                    runProperties25.Append(runFonts30);
                    runProperties25.Append(fontSize30);
                    runProperties25.Append(fontSizeComplexScript8);
                    runProperties25.Append(languages8);
                    var text24 = new Text{ Text = "_______" };

                    run25.Append(runProperties25);
                    run25.Append(text24);

                    paragraph38.Append(paragraphProperties37);
                    paragraph38.Append(run23);
                    paragraph38.Append(run24);
                    paragraph38.Append(run25);

                    var paragraph39 = new Paragraph{ RsidParagraphMarkRevision = "00232403", RsidParagraphAddition = "004950F8", RsidParagraphProperties = "00751945", RsidRunAdditionDefault = "004950F8", ParagraphId = "740B661B", TextId = "77777777" };

                    var paragraphProperties38 = new ParagraphProperties();
                    var autoSpaceDE5 = new AutoSpaceDE{ Val = false };
                    var autoSpaceDN5 = new AutoSpaceDN{ Val = false };
                    var adjustRightIndent5 = new AdjustRightIndent{ Val = false };
                    var spacingBetweenLines36 = new SpacingBetweenLines{ After = "0" };
                    var justification36 = new Justification{ Val = JustificationValues.Center };

                    var paragraphMarkRunProperties5 = new ParagraphMarkRunProperties();
                    var runFonts31 = new RunFonts{ Ascii = "Times New Roman", HighAnsi = "Times New Roman", EastAsia = "Calibri", ComplexScript = "Times New Roman" };
                    var fontSize31 = new FontSize{ Val = "20" };
                    var fontSizeComplexScript9 = new FontSizeComplexScript{ Val = "20" };
                    var underline4 = new Underline{ Val = UnderlineValues.Single };
                    var languages9 = new Languages{ EastAsia = "en-US" };

                    paragraphMarkRunProperties5.Append(runFonts31);
                    paragraphMarkRunProperties5.Append(fontSize31);
                    paragraphMarkRunProperties5.Append(fontSizeComplexScript9);
                    paragraphMarkRunProperties5.Append(underline4);
                    paragraphMarkRunProperties5.Append(languages9);

                    paragraphProperties38.Append(autoSpaceDE5);
                    paragraphProperties38.Append(autoSpaceDN5);
                    paragraphProperties38.Append(adjustRightIndent5);
                    paragraphProperties38.Append(spacingBetweenLines36);
                    paragraphProperties38.Append(justification36);
                    paragraphProperties38.Append(paragraphMarkRunProperties5);

                    paragraph39.Append(paragraphProperties38);

                    tableCell33.Append(tableCellProperties33);
                    tableCell33.Append(paragraph37);
                    tableCell33.Append(paragraph38);
                    tableCell33.Append(paragraph39);

                    var tableCell34 = new TableCell();

                    var tableCellProperties34 = new TableCellProperties();
                    var tableCellWidth34 = new TableCellWidth{ Width = "3417", Type = TableWidthUnitValues.Dxa };
                    var shading2 = new Shading{ Val = ShadingPatternValues.Clear, Color = "auto", Fill = "auto" };

                    tableCellProperties34.Append(tableCellWidth34);
                    tableCellProperties34.Append(shading2);

                    var paragraph40 = new Paragraph{ RsidParagraphMarkRevision = "00232403", RsidParagraphAddition = "004950F8", RsidParagraphProperties = "00751945", RsidRunAdditionDefault = "004950F8", ParagraphId = "7459112B", TextId = "77777777" };

                    var paragraphProperties39 = new ParagraphProperties();
                    var autoSpaceDE6 = new AutoSpaceDE{ Val = false };
                    var autoSpaceDN6 = new AutoSpaceDN{ Val = false };
                    var adjustRightIndent6 = new AdjustRightIndent{ Val = false };
                    var spacingBetweenLines37 = new SpacingBetweenLines{ After = "0" };
                    var justification37 = new Justification{ Val = JustificationValues.Center };

                    var paragraphMarkRunProperties6 = new ParagraphMarkRunProperties();
                    var runFonts32 = new RunFonts{ Ascii = "Times New Roman", HighAnsi = "Times New Roman", EastAsia = "Calibri", ComplexScript = "Times New Roman" };
                    var fontSize32 = new FontSize{ Val = "20" };
                    var fontSizeComplexScript10 = new FontSizeComplexScript{ Val = "20" };
                    var languages10 = new Languages{ EastAsia = "en-US" };

                    paragraphMarkRunProperties6.Append(runFonts32);
                    paragraphMarkRunProperties6.Append(fontSize32);
                    paragraphMarkRunProperties6.Append(fontSizeComplexScript10);
                    paragraphMarkRunProperties6.Append(languages10);

                    paragraphProperties39.Append(autoSpaceDE6);
                    paragraphProperties39.Append(autoSpaceDN6);
                    paragraphProperties39.Append(adjustRightIndent6);
                    paragraphProperties39.Append(spacingBetweenLines37);
                    paragraphProperties39.Append(justification37);
                    paragraphProperties39.Append(paragraphMarkRunProperties6);

                    paragraph40.Append(paragraphProperties39);

                    var paragraph41 = new Paragraph{ RsidParagraphMarkRevision = "00232403", RsidParagraphAddition = "004950F8", RsidParagraphProperties = "00751945", RsidRunAdditionDefault = "004950F8", ParagraphId = "17F4861A", TextId = "77777777" };

                    var paragraphProperties40 = new ParagraphProperties();
                    var autoSpaceDE7 = new AutoSpaceDE{ Val = false };
                    var autoSpaceDN7 = new AutoSpaceDN{ Val = false };
                    var adjustRightIndent7 = new AdjustRightIndent{ Val = false };
                    var spacingBetweenLines38 = new SpacingBetweenLines{ After = "0" };
                    var justification38 = new Justification{ Val = JustificationValues.Center };

                    var paragraphMarkRunProperties7 = new ParagraphMarkRunProperties();
                    var runFonts33 = new RunFonts{ Ascii = "Times New Roman", HighAnsi = "Times New Roman", EastAsia = "Calibri", ComplexScript = "Times New Roman" };
                    var fontSize33 = new FontSize{ Val = "20" };
                    var fontSizeComplexScript11 = new FontSizeComplexScript{ Val = "20" };
                    var languages11 = new Languages{ EastAsia = "en-US" };

                    paragraphMarkRunProperties7.Append(runFonts33);
                    paragraphMarkRunProperties7.Append(fontSize33);
                    paragraphMarkRunProperties7.Append(fontSizeComplexScript11);
                    paragraphMarkRunProperties7.Append(languages11);

                    paragraphProperties40.Append(autoSpaceDE7);
                    paragraphProperties40.Append(autoSpaceDN7);
                    paragraphProperties40.Append(adjustRightIndent7);
                    paragraphProperties40.Append(spacingBetweenLines38);
                    paragraphProperties40.Append(justification38);
                    paragraphProperties40.Append(paragraphMarkRunProperties7);

                    var run26 = new Run{ RsidRunProperties = "00232403" };

                    var runProperties26 = new RunProperties();
                    var runFonts34 = new RunFonts{ Ascii = "Times New Roman", HighAnsi = "Times New Roman", EastAsia = "Calibri", ComplexScript = "Times New Roman" };
                    var fontSize34 = new FontSize{ Val = "20" };
                    var fontSizeComplexScript12 = new FontSizeComplexScript{ Val = "20" };
                    var languages12 = new Languages{ EastAsia = "en-US" };

                    runProperties26.Append(runFonts34);
                    runProperties26.Append(fontSize34);
                    runProperties26.Append(fontSizeComplexScript12);
                    runProperties26.Append(languages12);
                    var text25 = new Text{ Space = SpaceProcessingModeValues.Preserve, Text = "________________________ " };

                    run26.Append(runProperties26);
                    run26.Append(text25);

                    paragraph41.Append(paragraphProperties40);
                    paragraph41.Append(run26);

                    var paragraph42 = new Paragraph{ RsidParagraphMarkRevision = "00232403", RsidParagraphAddition = "004950F8", RsidParagraphProperties = "00751945", RsidRunAdditionDefault = "004950F8", ParagraphId = "1AC94114", TextId = "77777777" };

                    var paragraphProperties41 = new ParagraphProperties();
                    var autoSpaceDE8 = new AutoSpaceDE{ Val = false };
                    var autoSpaceDN8 = new AutoSpaceDN{ Val = false };
                    var adjustRightIndent8 = new AdjustRightIndent{ Val = false };
                    var spacingBetweenLines39 = new SpacingBetweenLines{ After = "0" };
                    var justification39 = new Justification{ Val = JustificationValues.Center };

                    var paragraphMarkRunProperties8 = new ParagraphMarkRunProperties();
                    var runFonts35 = new RunFonts{ Ascii = "Times New Roman", HighAnsi = "Times New Roman", EastAsia = "Calibri", ComplexScript = "Times New Roman" };
                    var fontSize35 = new FontSize{ Val = "20" };
                    var fontSizeComplexScript13 = new FontSizeComplexScript{ Val = "20" };
                    var languages13 = new Languages{ EastAsia = "en-US" };

                    paragraphMarkRunProperties8.Append(runFonts35);
                    paragraphMarkRunProperties8.Append(fontSize35);
                    paragraphMarkRunProperties8.Append(fontSizeComplexScript13);
                    paragraphMarkRunProperties8.Append(languages13);

                    paragraphProperties41.Append(autoSpaceDE8);
                    paragraphProperties41.Append(autoSpaceDN8);
                    paragraphProperties41.Append(adjustRightIndent8);
                    paragraphProperties41.Append(spacingBetweenLines39);
                    paragraphProperties41.Append(justification39);
                    paragraphProperties41.Append(paragraphMarkRunProperties8);

                    var run27 = new Run{ RsidRunProperties = "00232403" };

                    var runProperties27 = new RunProperties();
                    var runFonts36 = new RunFonts{ Ascii = "Times New Roman", HighAnsi = "Times New Roman", EastAsia = "Calibri", ComplexScript = "Times New Roman" };
                    var fontSize36 = new FontSize{ Val = "20" };
                    var fontSizeComplexScript14 = new FontSizeComplexScript{ Val = "20" };
                    var languages14 = new Languages{ EastAsia = "en-US" };

                    runProperties27.Append(runFonts36);
                    runProperties27.Append(fontSize36);
                    runProperties27.Append(fontSizeComplexScript14);
                    runProperties27.Append(languages14);
                    var text26 = new Text{ Text = "(подпись)" };

                    run27.Append(runProperties27);
                    run27.Append(text26);

                    paragraph42.Append(paragraphProperties41);
                    paragraph42.Append(run27);

                    tableCell34.Append(tableCellProperties34);
                    tableCell34.Append(paragraph40);
                    tableCell34.Append(paragraph41);
                    tableCell34.Append(paragraph42);

                    var tableCell35 = new TableCell();

                    var tableCellProperties35 = new TableCellProperties();
                    var tableCellWidth35 = new TableCellWidth{ Width = "2916", Type = TableWidthUnitValues.Dxa };
                    var shading3 = new Shading{ Val = ShadingPatternValues.Clear, Color = "auto", Fill = "auto" };

                    tableCellProperties35.Append(tableCellWidth35);
                    tableCellProperties35.Append(shading3);

                    var paragraph43 = new Paragraph{ RsidParagraphMarkRevision = "00232403", RsidParagraphAddition = "004950F8", RsidParagraphProperties = "00751945", RsidRunAdditionDefault = "004950F8", ParagraphId = "35D2677F", TextId = "77777777" };

                    var paragraphProperties42 = new ParagraphProperties();
                    var autoSpaceDE9 = new AutoSpaceDE{ Val = false };
                    var autoSpaceDN9 = new AutoSpaceDN{ Val = false };
                    var adjustRightIndent9 = new AdjustRightIndent{ Val = false };
                    var spacingBetweenLines40 = new SpacingBetweenLines{ After = "0" };
                    var justification40 = new Justification{ Val = JustificationValues.Center };

                    var paragraphMarkRunProperties9 = new ParagraphMarkRunProperties();
                    var runFonts37 = new RunFonts{ Ascii = "Times New Roman", HighAnsi = "Times New Roman", EastAsia = "Calibri", ComplexScript = "Times New Roman" };
                    var fontSize37 = new FontSize{ Val = "20" };
                    var fontSizeComplexScript15 = new FontSizeComplexScript{ Val = "20" };
                    var languages15 = new Languages{ EastAsia = "en-US" };

                    paragraphMarkRunProperties9.Append(runFonts37);
                    paragraphMarkRunProperties9.Append(fontSize37);
                    paragraphMarkRunProperties9.Append(fontSizeComplexScript15);
                    paragraphMarkRunProperties9.Append(languages15);

                    paragraphProperties42.Append(autoSpaceDE9);
                    paragraphProperties42.Append(autoSpaceDN9);
                    paragraphProperties42.Append(adjustRightIndent9);
                    paragraphProperties42.Append(spacingBetweenLines40);
                    paragraphProperties42.Append(justification40);
                    paragraphProperties42.Append(paragraphMarkRunProperties9);

                    paragraph43.Append(paragraphProperties42);

                    var paragraph44 = new Paragraph{ RsidParagraphMarkRevision = "00232403", RsidParagraphAddition = "004950F8", RsidParagraphProperties = "00751945", RsidRunAdditionDefault = "004950F8", ParagraphId = "63D934B2", TextId = "77777777" };

                    var paragraphProperties43 = new ParagraphProperties();
                    var autoSpaceDE10 = new AutoSpaceDE{ Val = false };
                    var autoSpaceDN10 = new AutoSpaceDN{ Val = false };
                    var adjustRightIndent10 = new AdjustRightIndent{ Val = false };
                    var spacingBetweenLines41 = new SpacingBetweenLines{ After = "0" };
                    var justification41 = new Justification{ Val = JustificationValues.Center };

                    var paragraphMarkRunProperties10 = new ParagraphMarkRunProperties();
                    var runFonts38 = new RunFonts{ Ascii = "Times New Roman", HighAnsi = "Times New Roman", EastAsia = "Calibri", ComplexScript = "Times New Roman" };
                    var fontSize38 = new FontSize{ Val = "20" };
                    var fontSizeComplexScript16 = new FontSizeComplexScript{ Val = "20" };
                    var languages16 = new Languages{ EastAsia = "en-US" };

                    paragraphMarkRunProperties10.Append(runFonts38);
                    paragraphMarkRunProperties10.Append(fontSize38);
                    paragraphMarkRunProperties10.Append(fontSizeComplexScript16);
                    paragraphMarkRunProperties10.Append(languages16);

                    paragraphProperties43.Append(autoSpaceDE10);
                    paragraphProperties43.Append(autoSpaceDN10);
                    paragraphProperties43.Append(adjustRightIndent10);
                    paragraphProperties43.Append(spacingBetweenLines41);
                    paragraphProperties43.Append(justification41);
                    paragraphProperties43.Append(paragraphMarkRunProperties10);

                    var run28 = new Run{ RsidRunProperties = "00232403" };

                    var runProperties28 = new RunProperties();
                    var runFonts39 = new RunFonts{ Ascii = "Times New Roman", HighAnsi = "Times New Roman", EastAsia = "Calibri", ComplexScript = "Times New Roman" };
                    var fontSize39 = new FontSize{ Val = "20" };
                    var fontSizeComplexScript17 = new FontSizeComplexScript{ Val = "20" };
                    var languages17 = new Languages{ EastAsia = "en-US" };

                    runProperties28.Append(runFonts39);
                    runProperties28.Append(fontSize39);
                    runProperties28.Append(fontSizeComplexScript17);
                    runProperties28.Append(languages17);
                    var text27 = new Text { Text = "___________________________" };

                    run28.Append(runProperties28);
                    run28.Append(text27);

                    paragraph44.Append(paragraphProperties43);
                    paragraph44.Append(run28);

                    var paragraph45 = new Paragraph{ RsidParagraphMarkRevision = "00232403", RsidParagraphAddition = "004950F8", RsidParagraphProperties = "00751945", RsidRunAdditionDefault = "004950F8", ParagraphId = "1EC78E65", TextId = "77777777" };

                    var paragraphProperties44 = new ParagraphProperties();
                    var autoSpaceDE11 = new AutoSpaceDE{ Val = false };
                    var autoSpaceDN11 = new AutoSpaceDN{ Val = false };
                    var adjustRightIndent11 = new AdjustRightIndent{ Val = false };
                    var spacingBetweenLines42 = new SpacingBetweenLines{ After = "0" };
                    var justification42 = new Justification{ Val = JustificationValues.Center };

                    var paragraphMarkRunProperties11 = new ParagraphMarkRunProperties();
                    var runFonts40 = new RunFonts{ Ascii = "Times New Roman", HighAnsi = "Times New Roman", EastAsia = "Calibri", ComplexScript = "Times New Roman" };
                    var fontSize40 = new FontSize{ Val = "20" };
                    var fontSizeComplexScript18 = new FontSizeComplexScript{ Val = "20" };
                    var languages18 = new Languages{ EastAsia = "en-US" };

                    paragraphMarkRunProperties11.Append(runFonts40);
                    paragraphMarkRunProperties11.Append(fontSize40);
                    paragraphMarkRunProperties11.Append(fontSizeComplexScript18);
                    paragraphMarkRunProperties11.Append(languages18);

                    paragraphProperties44.Append(autoSpaceDE11);
                    paragraphProperties44.Append(autoSpaceDN11);
                    paragraphProperties44.Append(adjustRightIndent11);
                    paragraphProperties44.Append(spacingBetweenLines42);
                    paragraphProperties44.Append(justification42);
                    paragraphProperties44.Append(paragraphMarkRunProperties11);

                    var run29 = new Run{ RsidRunProperties = "00232403" };

                    var runProperties29 = new RunProperties();
                    var runFonts41 = new RunFonts{ Ascii = "Times New Roman", HighAnsi = "Times New Roman", EastAsia = "Calibri", ComplexScript = "Times New Roman" };
                    var fontSize41 = new FontSize{ Val = "20" };
                    var fontSizeComplexScript19 = new FontSizeComplexScript{ Val = "20" };
                    var languages19 = new Languages{ EastAsia = "en-US" };

                    runProperties29.Append(runFonts41);
                    runProperties29.Append(fontSize41);
                    runProperties29.Append(fontSizeComplexScript19);
                    runProperties29.Append(languages19);
                    var text28 = new Text { Text = "(фамилия, инициалы)" };

                    run29.Append(runProperties29);
                    run29.Append(text28);

                    paragraph45.Append(paragraphProperties44);
                    paragraph45.Append(run29);

                    tableCell35.Append(tableCellProperties35);
                    tableCell35.Append(paragraph43);
                    tableCell35.Append(paragraph44);
                    tableCell35.Append(paragraph45);

                    tableRow5.Append(tableCell33);
                    tableRow5.Append(tableCell34);
                    tableRow5.Append(tableCell35);

                    TableRow tableRow6 = new TableRow{ RsidTableRowMarkRevision = "00232403", RsidTableRowAddition = "004950F8", RsidTableRowProperties = "00751945", ParagraphId = "5E09C7EA", TextId = "77777777" };

                    var tableCell36 = new TableCell();

                    var tableCellProperties36 = new TableCellProperties();
                    var tableCellWidth36 = new TableCellWidth{ Width = "3417", Type = TableWidthUnitValues.Dxa };
                    var shading4 = new Shading{ Val = ShadingPatternValues.Clear, Color = "auto", Fill = "auto" };

                    tableCellProperties36.Append(tableCellWidth36);
                    tableCellProperties36.Append(shading4);

                    var paragraph46 = new Paragraph{ RsidParagraphMarkRevision = "00232403", RsidParagraphAddition = "004950F8", RsidParagraphProperties = "00751945", RsidRunAdditionDefault = "004950F8", ParagraphId = "0B078ACA", TextId = "77777777" };

                    var paragraphProperties45 = new ParagraphProperties();
                    var autoSpaceDE12 = new AutoSpaceDE{ Val = false };
                    var autoSpaceDN12 = new AutoSpaceDN{ Val = false };
                    var adjustRightIndent12 = new AdjustRightIndent{ Val = false };
                    var spacingBetweenLines43 = new SpacingBetweenLines{ After = "0" };

                    var paragraphMarkRunProperties12 = new ParagraphMarkRunProperties();
                    var runFonts42 = new RunFonts{ Ascii = "Times New Roman", HighAnsi = "Times New Roman", EastAsia = "Calibri", ComplexScript = "Times New Roman" };
                    var fontSize42 = new FontSize{ Val = "20" };
                    var fontSizeComplexScript20 = new FontSizeComplexScript{ Val = "20" };
                    var languages20 = new Languages{ EastAsia = "en-US" };

                    paragraphMarkRunProperties12.Append(runFonts42);
                    paragraphMarkRunProperties12.Append(fontSize42);
                    paragraphMarkRunProperties12.Append(fontSizeComplexScript20);
                    paragraphMarkRunProperties12.Append(languages20);

                    paragraphProperties45.Append(autoSpaceDE12);
                    paragraphProperties45.Append(autoSpaceDN12);
                    paragraphProperties45.Append(adjustRightIndent12);
                    paragraphProperties45.Append(spacingBetweenLines43);
                    paragraphProperties45.Append(paragraphMarkRunProperties12);

                    paragraph46.Append(paragraphProperties45);

                    var paragraph47 = new Paragraph{ RsidParagraphMarkRevision = "00232403", RsidParagraphAddition = "004950F8", RsidParagraphProperties = "00751945", RsidRunAdditionDefault = "004950F8", ParagraphId = "3B64293F", TextId = "77777777" };

                    var paragraphProperties46 = new ParagraphProperties();
                    var autoSpaceDE13 = new AutoSpaceDE{ Val = false };
                    var autoSpaceDN13 = new AutoSpaceDN{ Val = false };
                    var adjustRightIndent13 = new AdjustRightIndent{ Val = false };
                    var spacingBetweenLines44 = new SpacingBetweenLines{ After = "0" };

                    var paragraphMarkRunProperties13 = new ParagraphMarkRunProperties();
                    var runFonts43 = new RunFonts{ Ascii = "Times New Roman", HighAnsi = "Times New Roman", EastAsia = "Calibri", ComplexScript = "Times New Roman" };
                    var fontSize43 = new FontSize{ Val = "20" };
                    var fontSizeComplexScript21 = new FontSizeComplexScript{ Val = "20" };
                    var languages21 = new Languages{ EastAsia = "en-US" };

                    paragraphMarkRunProperties13.Append(runFonts43);
                    paragraphMarkRunProperties13.Append(fontSize43);
                    paragraphMarkRunProperties13.Append(fontSizeComplexScript21);
                    paragraphMarkRunProperties13.Append(languages21);

                    paragraphProperties46.Append(autoSpaceDE13);
                    paragraphProperties46.Append(autoSpaceDN13);
                    paragraphProperties46.Append(adjustRightIndent13);
                    paragraphProperties46.Append(spacingBetweenLines44);
                    paragraphProperties46.Append(paragraphMarkRunProperties13);

                    paragraph47.Append(paragraphProperties46);

                    var paragraph48 = new Paragraph{ RsidParagraphMarkRevision = "00232403", RsidParagraphAddition = "004950F8", RsidParagraphProperties = "00751945", RsidRunAdditionDefault = "004950F8", ParagraphId = "0C8F22C4", TextId = "77777777" };

                    var paragraphProperties47 = new ParagraphProperties();
                    var autoSpaceDE14 = new AutoSpaceDE{ Val = false };
                    var autoSpaceDN14 = new AutoSpaceDN{ Val = false };
                    var adjustRightIndent14 = new AdjustRightIndent{ Val = false };
                    var spacingBetweenLines45 = new SpacingBetweenLines{ After = "0" };
                    var justification43 = new Justification{ Val = JustificationValues.Center };

                    var paragraphMarkRunProperties14 = new ParagraphMarkRunProperties();
                    var runFonts44 = new RunFonts{ Ascii = "Times New Roman", HighAnsi = "Times New Roman", EastAsia = "Calibri", ComplexScript = "Times New Roman" };
                    var fontSize44 = new FontSize{ Val = "20" };
                    var fontSizeComplexScript22 = new FontSizeComplexScript{ Val = "20" };
                    var languages22 = new Languages{ EastAsia = "en-US" };

                    paragraphMarkRunProperties14.Append(runFonts44);
                    paragraphMarkRunProperties14.Append(fontSize44);
                    paragraphMarkRunProperties14.Append(fontSizeComplexScript22);
                    paragraphMarkRunProperties14.Append(languages22);

                    paragraphProperties47.Append(autoSpaceDE14);
                    paragraphProperties47.Append(autoSpaceDN14);
                    paragraphProperties47.Append(adjustRightIndent14);
                    paragraphProperties47.Append(spacingBetweenLines45);
                    paragraphProperties47.Append(justification43);
                    paragraphProperties47.Append(paragraphMarkRunProperties14);

                    var run30 = new Run{ RsidRunProperties = "00232403" };

                    var runProperties30 = new RunProperties();
                    var runFonts45 = new RunFonts{ Ascii = "Times New Roman", HighAnsi = "Times New Roman", EastAsia = "Calibri", ComplexScript = "Times New Roman" };
                    var fontSize45 = new FontSize{ Val = "20" };
                    var fontSizeComplexScript23 = new FontSizeComplexScript{ Val = "20" };
                    var languages23 = new Languages{ EastAsia = "en-US" };

                    runProperties30.Append(runFonts45);
                    runProperties30.Append(fontSize45);
                    runProperties30.Append(fontSizeComplexScript23);
                    runProperties30.Append(languages23);
                    var text29 = new Text { Text = "____________________________ (наименование должности руководителя Организации)" };

                    run30.Append(runProperties30);
                    run30.Append(text29);

                    paragraph48.Append(paragraphProperties47);
                    paragraph48.Append(run30);

                    var paragraph49 = new Paragraph{ RsidParagraphMarkRevision = "00232403", RsidParagraphAddition = "004950F8", RsidParagraphProperties = "004950F8", RsidRunAdditionDefault = "004950F8", ParagraphId = "6A357A9D", TextId = "77777777" };

                    var paragraphProperties48 = new ParagraphProperties();
                    var autoSpaceDE15 = new AutoSpaceDE{ Val = false };
                    var autoSpaceDN15 = new AutoSpaceDN{ Val = false };
                    var adjustRightIndent15 = new AdjustRightIndent{ Val = false };
                    var spacingBetweenLines46 = new SpacingBetweenLines{ After = "0" };

                    var paragraphMarkRunProperties15 = new ParagraphMarkRunProperties();
                    var runFonts46 = new RunFonts{ Ascii = "Times New Roman", HighAnsi = "Times New Roman", EastAsia = "Calibri", ComplexScript = "Times New Roman" };
                    var fontSize46 = new FontSize{ Val = "20" };
                    var fontSizeComplexScript24 = new FontSizeComplexScript{ Val = "20" };
                    var languages24 = new Languages{ EastAsia = "en-US" };

                    paragraphMarkRunProperties15.Append(runFonts46);
                    paragraphMarkRunProperties15.Append(fontSize46);
                    paragraphMarkRunProperties15.Append(fontSizeComplexScript24);
                    paragraphMarkRunProperties15.Append(languages24);

                    paragraphProperties48.Append(autoSpaceDE15);
                    paragraphProperties48.Append(autoSpaceDN15);
                    paragraphProperties48.Append(adjustRightIndent15);
                    paragraphProperties48.Append(spacingBetweenLines46);
                    paragraphProperties48.Append(paragraphMarkRunProperties15);

                    var run31 = new Run{ RsidRunProperties = "00232403" };

                    var runProperties31 = new RunProperties();
                    var runFonts47 = new RunFonts{ Ascii = "Times New Roman", HighAnsi = "Times New Roman", EastAsia = "Calibri", ComplexScript = "Times New Roman" };
                    var fontSize47 = new FontSize{ Val = "20" };
                    var fontSizeComplexScript25 = new FontSizeComplexScript{ Val = "20" };
                    var languages25 = new Languages{ EastAsia = "en-US" };

                    runProperties31.Append(runFonts47);
                    runProperties31.Append(fontSize47);
                    runProperties31.Append(fontSizeComplexScript25);
                    runProperties31.Append(languages25);
                    var text30 = new Text { Text = "М.П." };

                    run31.Append(runProperties31);
                    run31.Append(text30);

                    paragraph49.Append(paragraphProperties48);
                    paragraph49.Append(run31);

                    var paragraph50 = new Paragraph{ RsidParagraphMarkRevision = "00232403", RsidParagraphAddition = "004950F8", RsidParagraphProperties = "00751945", RsidRunAdditionDefault = "004950F8", ParagraphId = "20ADC8D5", TextId = "77777777" };

                    var paragraphProperties49 = new ParagraphProperties();
                    var autoSpaceDE16 = new AutoSpaceDE{ Val = false };
                    var autoSpaceDN16 = new AutoSpaceDN{ Val = false };
                    var adjustRightIndent16 = new AdjustRightIndent{ Val = false };
                    var spacingBetweenLines47 = new SpacingBetweenLines{ After = "0" };
                    var justification44 = new Justification{ Val = JustificationValues.Center };

                    var paragraphMarkRunProperties16 = new ParagraphMarkRunProperties();
                    var runFonts48 = new RunFonts{ Ascii = "Times New Roman", HighAnsi = "Times New Roman", EastAsia = "Calibri", ComplexScript = "Times New Roman" };
                    var fontSize48 = new FontSize{ Val = "20" };
                    var fontSizeComplexScript26 = new FontSizeComplexScript{ Val = "20" };
                    var languages26 = new Languages{ EastAsia = "en-US" };

                    paragraphMarkRunProperties16.Append(runFonts48);
                    paragraphMarkRunProperties16.Append(fontSize48);
                    paragraphMarkRunProperties16.Append(fontSizeComplexScript26);
                    paragraphMarkRunProperties16.Append(languages26);

                    paragraphProperties49.Append(autoSpaceDE16);
                    paragraphProperties49.Append(autoSpaceDN16);
                    paragraphProperties49.Append(adjustRightIndent16);
                    paragraphProperties49.Append(spacingBetweenLines47);
                    paragraphProperties49.Append(justification44);
                    paragraphProperties49.Append(paragraphMarkRunProperties16);

                    paragraph50.Append(paragraphProperties49);

                    tableCell36.Append(tableCellProperties36);
                    tableCell36.Append(paragraph46);
                    tableCell36.Append(paragraph47);
                    tableCell36.Append(paragraph48);
                    tableCell36.Append(paragraph49);
                    tableCell36.Append(paragraph50);

                    var tableCell37 = new TableCell();

                    var tableCellProperties37 = new TableCellProperties();
                    var tableCellWidth37 = new TableCellWidth{ Width = "3417", Type = TableWidthUnitValues.Dxa };
                    var shading5 = new Shading{ Val = ShadingPatternValues.Clear, Color = "auto", Fill = "auto" };

                    tableCellProperties37.Append(tableCellWidth37);
                    tableCellProperties37.Append(shading5);

                    var paragraph51 = new Paragraph{ RsidParagraphMarkRevision = "00232403", RsidParagraphAddition = "004950F8", RsidParagraphProperties = "00751945", RsidRunAdditionDefault = "004950F8", ParagraphId = "45FE4728", TextId = "77777777" };

                    var paragraphProperties50 = new ParagraphProperties();
                    var autoSpaceDE17 = new AutoSpaceDE{ Val = false };
                    var autoSpaceDN17 = new AutoSpaceDN{ Val = false };
                    var adjustRightIndent17 = new AdjustRightIndent{ Val = false };
                    var spacingBetweenLines48 = new SpacingBetweenLines{ After = "0" };
                    var justification45 = new Justification{ Val = JustificationValues.Center };

                    var paragraphMarkRunProperties17 = new ParagraphMarkRunProperties();
                    var runFonts49 = new RunFonts{ Ascii = "Times New Roman", HighAnsi = "Times New Roman", EastAsia = "Calibri", ComplexScript = "Times New Roman" };
                    var fontSize49 = new FontSize{ Val = "20" };
                    var fontSizeComplexScript27 = new FontSizeComplexScript{ Val = "20" };
                    var languages27 = new Languages{ EastAsia = "en-US" };

                    paragraphMarkRunProperties17.Append(runFonts49);
                    paragraphMarkRunProperties17.Append(fontSize49);
                    paragraphMarkRunProperties17.Append(fontSizeComplexScript27);
                    paragraphMarkRunProperties17.Append(languages27);

                    paragraphProperties50.Append(autoSpaceDE17);
                    paragraphProperties50.Append(autoSpaceDN17);
                    paragraphProperties50.Append(adjustRightIndent17);
                    paragraphProperties50.Append(spacingBetweenLines48);
                    paragraphProperties50.Append(justification45);
                    paragraphProperties50.Append(paragraphMarkRunProperties17);

                    paragraph51.Append(paragraphProperties50);

                    var paragraph52 = new Paragraph{ RsidParagraphMarkRevision = "00232403", RsidParagraphAddition = "004950F8", RsidParagraphProperties = "00751945", RsidRunAdditionDefault = "004950F8", ParagraphId = "46FC90B7", TextId = "77777777" };

                    var paragraphProperties51 = new ParagraphProperties();
                    var autoSpaceDE18 = new AutoSpaceDE{ Val = false };
                    var autoSpaceDN18 = new AutoSpaceDN{ Val = false };
                    var adjustRightIndent18 = new AdjustRightIndent{ Val = false };
                    var spacingBetweenLines49 = new SpacingBetweenLines{ After = "0" };
                    var justification46 = new Justification{ Val = JustificationValues.Center };

                    var paragraphMarkRunProperties18 = new ParagraphMarkRunProperties();
                    var runFonts50 = new RunFonts{ Ascii = "Times New Roman", HighAnsi = "Times New Roman", EastAsia = "Calibri", ComplexScript = "Times New Roman" };
                    var fontSize50 = new FontSize{ Val = "20" };
                    var fontSizeComplexScript28 = new FontSizeComplexScript{ Val = "20" };
                    var languages28 = new Languages{ EastAsia = "en-US" };

                    paragraphMarkRunProperties18.Append(runFonts50);
                    paragraphMarkRunProperties18.Append(fontSize50);
                    paragraphMarkRunProperties18.Append(fontSizeComplexScript28);
                    paragraphMarkRunProperties18.Append(languages28);

                    paragraphProperties51.Append(autoSpaceDE18);
                    paragraphProperties51.Append(autoSpaceDN18);
                    paragraphProperties51.Append(adjustRightIndent18);
                    paragraphProperties51.Append(spacingBetweenLines49);
                    paragraphProperties51.Append(justification46);
                    paragraphProperties51.Append(paragraphMarkRunProperties18);

                    paragraph52.Append(paragraphProperties51);

                    var paragraph53 = new Paragraph{ RsidParagraphMarkRevision = "00232403", RsidParagraphAddition = "004950F8", RsidParagraphProperties = "00751945", RsidRunAdditionDefault = "004950F8", ParagraphId = "62488562", TextId = "77777777" };

                    var paragraphProperties52 = new ParagraphProperties();
                    var autoSpaceDE19 = new AutoSpaceDE{ Val = false };
                    var autoSpaceDN19 = new AutoSpaceDN{ Val = false };
                    var adjustRightIndent19 = new AdjustRightIndent{ Val = false };
                    var spacingBetweenLines50 = new SpacingBetweenLines{ After = "0" };
                    var justification47 = new Justification{ Val = JustificationValues.Center };

                    var paragraphMarkRunProperties19 = new ParagraphMarkRunProperties();
                    var runFonts51 = new RunFonts{ Ascii = "Times New Roman", HighAnsi = "Times New Roman", EastAsia = "Calibri", ComplexScript = "Times New Roman" };
                    var fontSize51 = new FontSize{ Val = "20" };
                    var fontSizeComplexScript29 = new FontSizeComplexScript{ Val = "20" };
                    var languages29 = new Languages{ EastAsia = "en-US" };

                    paragraphMarkRunProperties19.Append(runFonts51);
                    paragraphMarkRunProperties19.Append(fontSize51);
                    paragraphMarkRunProperties19.Append(fontSizeComplexScript29);
                    paragraphMarkRunProperties19.Append(languages29);

                    paragraphProperties52.Append(autoSpaceDE19);
                    paragraphProperties52.Append(autoSpaceDN19);
                    paragraphProperties52.Append(adjustRightIndent19);
                    paragraphProperties52.Append(spacingBetweenLines50);
                    paragraphProperties52.Append(justification47);
                    paragraphProperties52.Append(paragraphMarkRunProperties19);

                    var run32 = new Run{ RsidRunProperties = "00232403" };

                    var runProperties32 = new RunProperties();
                    var runFonts52 = new RunFonts{ Ascii = "Times New Roman", HighAnsi = "Times New Roman", EastAsia = "Calibri", ComplexScript = "Times New Roman" };
                    var fontSize52 = new FontSize{ Val = "20" };
                    var fontSizeComplexScript30 = new FontSizeComplexScript{ Val = "20" };
                    var languages30 = new Languages{ EastAsia = "en-US" };

                    runProperties32.Append(runFonts52);
                    runProperties32.Append(fontSize52);
                    runProperties32.Append(fontSizeComplexScript30);
                    runProperties32.Append(languages30);
                    var text31 = new Text{ Space = SpaceProcessingModeValues.Preserve, Text = "________________________ " };

                    run32.Append(runProperties32);
                    run32.Append(text31);

                    paragraph53.Append(paragraphProperties52);
                    paragraph53.Append(run32);

                    var paragraph54 = new Paragraph{ RsidParagraphMarkRevision = "00232403", RsidParagraphAddition = "004950F8", RsidParagraphProperties = "00751945", RsidRunAdditionDefault = "004950F8", ParagraphId = "1331F6B1", TextId = "77777777" };

                    var paragraphProperties53 = new ParagraphProperties();
                    var autoSpaceDE20 = new AutoSpaceDE{ Val = false };
                    var autoSpaceDN20 = new AutoSpaceDN{ Val = false };
                    var adjustRightIndent20 = new AdjustRightIndent{ Val = false };
                    var spacingBetweenLines51 = new SpacingBetweenLines{ After = "0" };
                    var justification48 = new Justification{ Val = JustificationValues.Center };

                    var paragraphMarkRunProperties20 = new ParagraphMarkRunProperties();
                    var runFonts53 = new RunFonts{ Ascii = "Times New Roman", HighAnsi = "Times New Roman", EastAsia = "Calibri", ComplexScript = "Times New Roman" };
                    var fontSize53 = new FontSize{ Val = "20" };
                    var fontSizeComplexScript31 = new FontSizeComplexScript{ Val = "20" };
                    var languages31 = new Languages{ EastAsia = "en-US" };

                    paragraphMarkRunProperties20.Append(runFonts53);
                    paragraphMarkRunProperties20.Append(fontSize53);
                    paragraphMarkRunProperties20.Append(fontSizeComplexScript31);
                    paragraphMarkRunProperties20.Append(languages31);

                    paragraphProperties53.Append(autoSpaceDE20);
                    paragraphProperties53.Append(autoSpaceDN20);
                    paragraphProperties53.Append(adjustRightIndent20);
                    paragraphProperties53.Append(spacingBetweenLines51);
                    paragraphProperties53.Append(justification48);
                    paragraphProperties53.Append(paragraphMarkRunProperties20);

                    var run33 = new Run{ RsidRunProperties = "00232403" };

                    var runProperties33 = new RunProperties();
                    var runFonts54 = new RunFonts{ Ascii = "Times New Roman", HighAnsi = "Times New Roman", EastAsia = "Calibri", ComplexScript = "Times New Roman" };
                    var fontSize54 = new FontSize{ Val = "20" };
                    var fontSizeComplexScript32 = new FontSizeComplexScript{ Val = "20" };
                    var languages32 = new Languages{ EastAsia = "en-US" };

                    runProperties33.Append(runFonts54);
                    runProperties33.Append(fontSize54);
                    runProperties33.Append(fontSizeComplexScript32);
                    runProperties33.Append(languages32);
                    var text32 = new Text { Text = "(подпись)" };

                    run33.Append(runProperties33);
                    run33.Append(text32);

                    paragraph54.Append(paragraphProperties53);
                    paragraph54.Append(run33);

                    tableCell37.Append(tableCellProperties37);
                    tableCell37.Append(paragraph51);
                    tableCell37.Append(paragraph52);
                    tableCell37.Append(paragraph53);
                    tableCell37.Append(paragraph54);

                    var tableCell38 = new TableCell();

                    var tableCellProperties38 = new TableCellProperties();
                    var tableCellWidth38 = new TableCellWidth{ Width = "2916", Type = TableWidthUnitValues.Dxa };
                    var shading6 = new Shading{ Val = ShadingPatternValues.Clear, Color = "auto", Fill = "auto" };

                    tableCellProperties38.Append(tableCellWidth38);
                    tableCellProperties38.Append(shading6);

                    var paragraph55 = new Paragraph{ RsidParagraphMarkRevision = "00232403", RsidParagraphAddition = "004950F8", RsidParagraphProperties = "00751945", RsidRunAdditionDefault = "004950F8", ParagraphId = "7ED1A1F7", TextId = "77777777" };

                    var paragraphProperties54 = new ParagraphProperties();
                    var autoSpaceDE21 = new AutoSpaceDE{ Val = false };
                    var autoSpaceDN21 = new AutoSpaceDN{ Val = false };
                    var adjustRightIndent21 = new AdjustRightIndent{ Val = false };
                    var spacingBetweenLines52 = new SpacingBetweenLines{ After = "0" };

                    var paragraphMarkRunProperties21 = new ParagraphMarkRunProperties();
                    var runFonts55 = new RunFonts{ Ascii = "Times New Roman", HighAnsi = "Times New Roman", EastAsia = "Calibri", ComplexScript = "Times New Roman" };
                    var fontSize55 = new FontSize{ Val = "20" };
                    var fontSizeComplexScript33 = new FontSizeComplexScript{ Val = "20" };
                    var languages33 = new Languages{ EastAsia = "en-US" };

                    paragraphMarkRunProperties21.Append(runFonts55);
                    paragraphMarkRunProperties21.Append(fontSize55);
                    paragraphMarkRunProperties21.Append(fontSizeComplexScript33);
                    paragraphMarkRunProperties21.Append(languages33);

                    paragraphProperties54.Append(autoSpaceDE21);
                    paragraphProperties54.Append(autoSpaceDN21);
                    paragraphProperties54.Append(adjustRightIndent21);
                    paragraphProperties54.Append(spacingBetweenLines52);
                    paragraphProperties54.Append(paragraphMarkRunProperties21);

                    paragraph55.Append(paragraphProperties54);

                    var paragraph56 = new Paragraph{ RsidParagraphMarkRevision = "00232403", RsidParagraphAddition = "004950F8", RsidParagraphProperties = "00751945", RsidRunAdditionDefault = "004950F8", ParagraphId = "4B3F6D60", TextId = "77777777" };

                    var paragraphProperties55 = new ParagraphProperties();
                    var autoSpaceDE22 = new AutoSpaceDE{ Val = false };
                    var autoSpaceDN22 = new AutoSpaceDN{ Val = false };
                    var adjustRightIndent22 = new AdjustRightIndent{ Val = false };
                    var spacingBetweenLines53 = new SpacingBetweenLines{ After = "0" };

                    var paragraphMarkRunProperties22 = new ParagraphMarkRunProperties();
                    var runFonts56 = new RunFonts{ Ascii = "Times New Roman", HighAnsi = "Times New Roman", EastAsia = "Calibri", ComplexScript = "Times New Roman" };
                    var fontSize56 = new FontSize{ Val = "20" };
                    var fontSizeComplexScript34 = new FontSizeComplexScript{ Val = "20" };
                    var languages34 = new Languages{ EastAsia = "en-US" };

                    paragraphMarkRunProperties22.Append(runFonts56);
                    paragraphMarkRunProperties22.Append(fontSize56);
                    paragraphMarkRunProperties22.Append(fontSizeComplexScript34);
                    paragraphMarkRunProperties22.Append(languages34);

                    paragraphProperties55.Append(autoSpaceDE22);
                    paragraphProperties55.Append(autoSpaceDN22);
                    paragraphProperties55.Append(adjustRightIndent22);
                    paragraphProperties55.Append(spacingBetweenLines53);
                    paragraphProperties55.Append(paragraphMarkRunProperties22);

                    paragraph56.Append(paragraphProperties55);

                    var paragraph57 = new Paragraph{ RsidParagraphMarkRevision = "00232403", RsidParagraphAddition = "004950F8", RsidParagraphProperties = "00751945", RsidRunAdditionDefault = "004950F8", ParagraphId = "5B53E466", TextId = "77777777" };

                    var paragraphProperties56 = new ParagraphProperties();
                    var autoSpaceDE23 = new AutoSpaceDE{ Val = false };
                    var autoSpaceDN23 = new AutoSpaceDN{ Val = false };
                    var adjustRightIndent23 = new AdjustRightIndent{ Val = false };
                    var spacingBetweenLines54 = new SpacingBetweenLines{ After = "0" };
                    var justification49 = new Justification{ Val = JustificationValues.Center };

                    var paragraphMarkRunProperties23 = new ParagraphMarkRunProperties();
                    var runFonts57 = new RunFonts{ Ascii = "Times New Roman", HighAnsi = "Times New Roman", EastAsia = "Calibri", ComplexScript = "Times New Roman" };
                    var fontSize57 = new FontSize{ Val = "20" };
                    var fontSizeComplexScript35 = new FontSizeComplexScript{ Val = "20" };
                    var languages35 = new Languages{ EastAsia = "en-US" };

                    paragraphMarkRunProperties23.Append(runFonts57);
                    paragraphMarkRunProperties23.Append(fontSize57);
                    paragraphMarkRunProperties23.Append(fontSizeComplexScript35);
                    paragraphMarkRunProperties23.Append(languages35);

                    paragraphProperties56.Append(autoSpaceDE23);
                    paragraphProperties56.Append(autoSpaceDN23);
                    paragraphProperties56.Append(adjustRightIndent23);
                    paragraphProperties56.Append(spacingBetweenLines54);
                    paragraphProperties56.Append(justification49);
                    paragraphProperties56.Append(paragraphMarkRunProperties23);

                    var run34 = new Run{ RsidRunProperties = "00232403" };

                    var runProperties34 = new RunProperties();
                    var runFonts58 = new RunFonts{ Ascii = "Times New Roman", HighAnsi = "Times New Roman", EastAsia = "Calibri", ComplexScript = "Times New Roman" };
                    var fontSize58 = new FontSize{ Val = "20" };
                    var fontSizeComplexScript36 = new FontSizeComplexScript{ Val = "20" };
                    var languages36 = new Languages{ EastAsia = "en-US" };

                    runProperties34.Append(runFonts58);
                    runProperties34.Append(fontSize58);
                    runProperties34.Append(fontSizeComplexScript36);
                    runProperties34.Append(languages36);
                    var text33 = new Text { Text = "___________________________" };

                    run34.Append(runProperties34);
                    run34.Append(text33);

                    paragraph57.Append(paragraphProperties56);
                    paragraph57.Append(run34);

                    var paragraph58 = new Paragraph{ RsidParagraphMarkRevision = "00232403", RsidParagraphAddition = "004950F8", RsidParagraphProperties = "00751945", RsidRunAdditionDefault = "004950F8", ParagraphId = "3C6E05CE", TextId = "77777777" };

                    var paragraphProperties57 = new ParagraphProperties();
                    var autoSpaceDE24 = new AutoSpaceDE{ Val = false };
                    var autoSpaceDN24 = new AutoSpaceDN{ Val = false };
                    var adjustRightIndent24 = new AdjustRightIndent{ Val = false };
                    var spacingBetweenLines55 = new SpacingBetweenLines{ After = "0" };
                    var justification50 = new Justification{ Val = JustificationValues.Center };

                    var paragraphMarkRunProperties24 = new ParagraphMarkRunProperties();
                    var runFonts59 = new RunFonts{ Ascii = "Times New Roman", HighAnsi = "Times New Roman", EastAsia = "Calibri", ComplexScript = "Times New Roman" };
                    var fontSize59 = new FontSize{ Val = "20" };
                    var fontSizeComplexScript37 = new FontSizeComplexScript{ Val = "20" };
                    var languages37 = new Languages{ EastAsia = "en-US" };

                    paragraphMarkRunProperties24.Append(runFonts59);
                    paragraphMarkRunProperties24.Append(fontSize59);
                    paragraphMarkRunProperties24.Append(fontSizeComplexScript37);
                    paragraphMarkRunProperties24.Append(languages37);

                    paragraphProperties57.Append(autoSpaceDE24);
                    paragraphProperties57.Append(autoSpaceDN24);
                    paragraphProperties57.Append(adjustRightIndent24);
                    paragraphProperties57.Append(spacingBetweenLines55);
                    paragraphProperties57.Append(justification50);
                    paragraphProperties57.Append(paragraphMarkRunProperties24);

                    var run35 = new Run{ RsidRunProperties = "00232403" };

                    var runProperties35 = new RunProperties();
                    var runFonts60 = new RunFonts{ Ascii = "Times New Roman", HighAnsi = "Times New Roman", EastAsia = "Calibri", ComplexScript = "Times New Roman" };
                    var fontSize60 = new FontSize{ Val = "20" };
                    var fontSizeComplexScript38 = new FontSizeComplexScript{ Val = "20" };
                    var languages38 = new Languages{ EastAsia = "en-US" };

                    runProperties35.Append(runFonts60);
                    runProperties35.Append(fontSize60);
                    runProperties35.Append(fontSizeComplexScript38);
                    runProperties35.Append(languages38);
                    var text34 = new Text { Text = "(фамилия, инициалы)" };

                    run35.Append(runProperties35);
                    run35.Append(text34);

                    paragraph58.Append(paragraphProperties57);
                    paragraph58.Append(run35);

                    var paragraph59 = new Paragraph{ RsidParagraphMarkRevision = "00232403", RsidParagraphAddition = "004950F8", RsidParagraphProperties = "00751945", RsidRunAdditionDefault = "004950F8", ParagraphId = "63E18720", TextId = "77777777" };

                    var paragraphProperties58 = new ParagraphProperties();
                    var autoSpaceDE25 = new AutoSpaceDE{ Val = false };
                    var autoSpaceDN25 = new AutoSpaceDN{ Val = false };
                    var adjustRightIndent25 = new AdjustRightIndent{ Val = false };
                    var spacingBetweenLines56 = new SpacingBetweenLines{ After = "0" };

                    var paragraphMarkRunProperties25 = new ParagraphMarkRunProperties();
                    var runFonts61 = new RunFonts{ Ascii = "Times New Roman", HighAnsi = "Times New Roman", EastAsia = "Calibri", ComplexScript = "Times New Roman" };
                    var fontSize61 = new FontSize{ Val = "20" };
                    var fontSizeComplexScript39 = new FontSizeComplexScript{ Val = "20" };
                    var languages39 = new Languages{ EastAsia = "en-US" };

                    paragraphMarkRunProperties25.Append(runFonts61);
                    paragraphMarkRunProperties25.Append(fontSize61);
                    paragraphMarkRunProperties25.Append(fontSizeComplexScript39);
                    paragraphMarkRunProperties25.Append(languages39);

                    paragraphProperties58.Append(autoSpaceDE25);
                    paragraphProperties58.Append(autoSpaceDN25);
                    paragraphProperties58.Append(adjustRightIndent25);
                    paragraphProperties58.Append(spacingBetweenLines56);
                    paragraphProperties58.Append(paragraphMarkRunProperties25);

                    paragraph59.Append(paragraphProperties58);

                    tableCell38.Append(tableCellProperties38);
                    tableCell38.Append(paragraph55);
                    tableCell38.Append(paragraph56);
                    tableCell38.Append(paragraph57);
                    tableCell38.Append(paragraph58);
                    tableCell38.Append(paragraph59);

                    tableRow6.Append(tableCell36);
                    tableRow6.Append(tableCell37);
                    tableRow6.Append(tableCell38);

                    table2.Append(tableProperties2);
                    table2.Append(tableGrid2);
                    table2.Append(tableRow5);
                    table2.Append(tableRow6);

                    var paragraph60 = new Paragraph{ RsidParagraphMarkRevision = "004950F8", RsidParagraphAddition = "004950F8", RsidParagraphProperties = "004950F8", RsidRunAdditionDefault = "004950F8", ParagraphId = "38474E6D", TextId = "77777777" };

                    var paragraphProperties59 = new ParagraphProperties();
                    var autoSpaceDE26 = new AutoSpaceDE{ Val = false };
                    var autoSpaceDN26 = new AutoSpaceDN{ Val = false };
                    var adjustRightIndent26 = new AdjustRightIndent{ Val = false };
                    var spacingBetweenLines57 = new SpacingBetweenLines{ After = "0" };
                    var justification51 = new Justification{ Val = JustificationValues.Both };

                    var paragraphMarkRunProperties26 = new ParagraphMarkRunProperties();
                    var runFonts62 = new RunFonts{ Ascii = "Times New Roman", HighAnsi = "Times New Roman", EastAsia = "Calibri", ComplexScript = "Times New Roman" };
                    var fontSize62 = new FontSize{ Val = "20" };
                    var fontSizeComplexScript40 = new FontSizeComplexScript{ Val = "20" };
                    var languages40 = new Languages{ EastAsia = "en-US" };

                    paragraphMarkRunProperties26.Append(runFonts62);
                    paragraphMarkRunProperties26.Append(fontSize62);
                    paragraphMarkRunProperties26.Append(fontSizeComplexScript40);
                    paragraphMarkRunProperties26.Append(languages40);

                    paragraphProperties59.Append(autoSpaceDE26);
                    paragraphProperties59.Append(autoSpaceDN26);
                    paragraphProperties59.Append(adjustRightIndent26);
                    paragraphProperties59.Append(spacingBetweenLines57);
                    paragraphProperties59.Append(justification51);
                    paragraphProperties59.Append(paragraphMarkRunProperties26);

                    var run36 = new Run{ RsidRunProperties = "00232403" };

                    var runProperties36 = new RunProperties();
                    var runFonts63 = new RunFonts{ Ascii = "Times New Roman", HighAnsi = "Times New Roman", EastAsia = "Calibri", ComplexScript = "Times New Roman" };
                    var fontSize63 = new FontSize{ Val = "20" };
                    var fontSizeComplexScript41 = new FontSizeComplexScript{ Val = "20" };
                    var languages41 = new Languages{ EastAsia = "en-US" };

                    runProperties36.Append(runFonts63);
                    runProperties36.Append(fontSize63);
                    runProperties36.Append(fontSizeComplexScript41);
                    runProperties36.Append(languages41);
                    var text35 = new Text { Text = "«___» __________ 20___ г." };

                    run36.Append(runProperties36);
                    run36.Append(text35);

                    var run37 = new Run{ RsidRunProperties = "004950F8" };

                    var runProperties37 = new RunProperties();
                    var runFonts64 = new RunFonts{ Ascii = "Times New Roman", HighAnsi = "Times New Roman", EastAsia = "Calibri", ComplexScript = "Times New Roman" };
                    var fontSize64 = new FontSize{ Val = "20" };
                    var fontSizeComplexScript42 = new FontSizeComplexScript{ Val = "20" };
                    var languages42 = new Languages{ EastAsia = "en-US" };

                    runProperties37.Append(runFonts64);
                    runProperties37.Append(fontSize64);
                    runProperties37.Append(fontSizeComplexScript42);
                    runProperties37.Append(languages42);
                    var text36 = new Text { Space = SpaceProcessingModeValues.Preserve, Text = Space };

                    run37.Append(runProperties37);
                    run37.Append(text36);

                    paragraph60.Append(paragraphProperties59);
                    paragraph60.Append(run36);
                    paragraph60.Append(run37);

                    doc.AppendChild(table2);
                    doc.AppendChild(paragraph60);
                }


                return new DocumentResult
                {
                    FileBody = ms.ToArray(),
                    FileName = "Перечень.docx",
                    MimeType = mimeType,
                    MimeTypeShort = extention
                };
            }
        }
    }
}
