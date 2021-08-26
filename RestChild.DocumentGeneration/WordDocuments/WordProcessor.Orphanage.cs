using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using RestChild.Comon;
using RestChild.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestChild.DocumentGeneration
{
    public static partial class WordProcessor
    {
        /// <summary>
        ///     Список для ГИБДД
        /// </summary>
        public static IDocument OrphanagePupilGroupListsGibddList(ListOfChilds list)
        {
            using (var ms = new MemoryStream())
            {
                using (var wordDocument = WordprocessingDocument.Create(ms, WordprocessingDocumentType.Document))
                {
                    var mainPart = wordDocument.AddMainDocumentPart();

                    Document document1 = new Document();

                    Body body1 = new Body();

                    Paragraph paragraph1 = new Paragraph() { RsidParagraphMarkRevision = "003B19CE", RsidParagraphAddition = "001C30EE", RsidParagraphProperties = "001C30EE", RsidRunAdditionDefault = "003B19CE", ParagraphId = "664A1426", TextId = "77777777" };

                    ParagraphProperties paragraphProperties1 = new ParagraphProperties();
                    SpacingBetweenLines spacingBetweenLines1 = new SpacingBetweenLines() { After = "0", Line = "240", LineRule = LineSpacingRuleValues.Auto };
                    Justification justification1 = new Justification() { Val = JustificationValues.Center };

                    ParagraphMarkRunProperties paragraphMarkRunProperties1 = new ParagraphMarkRunProperties();
                    RunFonts runFonts1 = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman", ComplexScript = "Times New Roman" };
                    Bold bold1 = new Bold();
                    BoldComplexScript boldComplexScript1 = new BoldComplexScript();
                    FontSize fontSize1 = new FontSize() { Val = "28" };
                    FontSizeComplexScript fontSizeComplexScript1 = new FontSizeComplexScript() { Val = "28" };

                    paragraphMarkRunProperties1.Append(runFonts1);
                    paragraphMarkRunProperties1.Append(bold1);
                    paragraphMarkRunProperties1.Append(boldComplexScript1);
                    paragraphMarkRunProperties1.Append(fontSize1);
                    paragraphMarkRunProperties1.Append(fontSizeComplexScript1);

                    paragraphProperties1.Append(spacingBetweenLines1);
                    paragraphProperties1.Append(justification1);
                    paragraphProperties1.Append(paragraphMarkRunProperties1);

                    Run run1 = new Run() { RsidRunProperties = "003B19CE" };

                    RunProperties runProperties1 = new RunProperties();
                    RunFonts runFonts2 = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman", ComplexScript = "Times New Roman" };
                    Bold bold2 = new Bold();
                    BoldComplexScript boldComplexScript2 = new BoldComplexScript();
                    FontSize fontSize2 = new FontSize() { Val = "28" };
                    FontSizeComplexScript fontSizeComplexScript2 = new FontSizeComplexScript() { Val = "28" };

                    runProperties1.Append(runFonts2);
                    runProperties1.Append(bold2);
                    runProperties1.Append(boldComplexScript2);
                    runProperties1.Append(fontSize2);
                    runProperties1.Append(fontSizeComplexScript2);
                    Text text1 = new Text();
                    text1.Text = "Информация для организации автомобильного трансфера";

                    run1.Append(runProperties1);
                    run1.Append(text1);

                    paragraph1.Append(paragraphProperties1);
                    paragraph1.Append(run1);

                    Paragraph paragraph2 = new Paragraph() { RsidParagraphMarkRevision = "003B19CE", RsidParagraphAddition = "003B19CE", RsidParagraphProperties = "001C30EE", RsidRunAdditionDefault = "003B19CE", ParagraphId = "42F3D557", TextId = "77777777" };

                    ParagraphProperties paragraphProperties2 = new ParagraphProperties();
                    SpacingBetweenLines spacingBetweenLines2 = new SpacingBetweenLines() { After = "0", Line = "240", LineRule = LineSpacingRuleValues.Auto };
                    Justification justification2 = new Justification() { Val = JustificationValues.Center };

                    ParagraphMarkRunProperties paragraphMarkRunProperties2 = new ParagraphMarkRunProperties();
                    RunFonts runFonts3 = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman", ComplexScript = "Times New Roman" };
                    Bold bold3 = new Bold();
                    BoldComplexScript boldComplexScript3 = new BoldComplexScript();
                    Color color1 = new Color() { Val = "000000", ThemeColor = ThemeColorValues.Text1 };
                    FontSize fontSize3 = new FontSize() { Val = "28" };
                    FontSizeComplexScript fontSizeComplexScript3 = new FontSizeComplexScript() { Val = "28" };

                    paragraphMarkRunProperties2.Append(runFonts3);
                    paragraphMarkRunProperties2.Append(bold3);
                    paragraphMarkRunProperties2.Append(boldComplexScript3);
                    paragraphMarkRunProperties2.Append(color1);
                    paragraphMarkRunProperties2.Append(fontSize3);
                    paragraphMarkRunProperties2.Append(fontSizeComplexScript3);

                    paragraphProperties2.Append(spacingBetweenLines2);
                    paragraphProperties2.Append(justification2);
                    paragraphProperties2.Append(paragraphMarkRunProperties2);

                    paragraph2.Append(paragraphProperties2);

                    //первая таблица
                    Table table1 = new Table();

                    TableProperties tableProperties1 = new TableProperties();
                    TableStyle tableStyle1 = new TableStyle() { Val = "a3" };
                    TableWidth tableWidth1 = new TableWidth() { Width = "0", Type = TableWidthUnitValues.Auto };
                    TableLook tableLook1 = new TableLook() { Val = "04A0", FirstRow = true, LastRow = false, FirstColumn = true, LastColumn = false, NoHorizontalBand = false, NoVerticalBand = true };

                    var tableBorders1 = new TableBorders(
                        new TopBorder { Val = new EnumValue<BorderValues>(BorderValues.Single) },
                        new BottomBorder { Val = new EnumValue<BorderValues>(BorderValues.Single) },
                        new LeftBorder { Val = new EnumValue<BorderValues>(BorderValues.Single) },
                        new RightBorder { Val = new EnumValue<BorderValues>(BorderValues.Single) },
                        new InsideHorizontalBorder { Val = new EnumValue<BorderValues>(BorderValues.Single) },
                        new InsideVerticalBorder { Val = new EnumValue<BorderValues>(BorderValues.Single) });

                    tableProperties1.Append(tableStyle1);
                    tableProperties1.Append(tableWidth1);
                    tableProperties1.Append(tableLook1);
                    tableProperties1.Append(tableBorders1); 

                    TableGrid tableGrid1 = new TableGrid();
                    GridColumn gridColumn1 = new GridColumn() { Width = "672" };
                    GridColumn gridColumn2 = new GridColumn() { Width = "6203" };
                    GridColumn gridColumn3 = new GridColumn() { Width = "2470" };

                    tableGrid1.Append(gridColumn1);
                    tableGrid1.Append(gridColumn2);
                    tableGrid1.Append(gridColumn3);

                    table1.Append(tableProperties1);
                    table1.Append(tableGrid1);

                    {
                        TableRow tableRow1 = new TableRow() { RsidTableRowMarkRevision = "001C30EE", RsidTableRowAddition = "00917B74", RsidTableRowProperties = "00E20D38", ParagraphId = "353D4AF8", TextId = "77777777" };

                        TableCell tableCell1 = new TableCell();

                        TableCellProperties tableCellProperties1 = new TableCellProperties();
                        TableCellWidth tableCellWidth1 = new TableCellWidth() { Width = "672", Type = TableWidthUnitValues.Dxa };
                        TableCellVerticalAlignment tableCellVerticalAlignment1 = new TableCellVerticalAlignment() { Val = TableVerticalAlignmentValues.Center };

                        tableCellProperties1.Append(tableCellWidth1);
                        tableCellProperties1.Append(tableCellVerticalAlignment1);

                        Paragraph paragraph3 = new Paragraph() { RsidParagraphMarkRevision = "00FE30D3", RsidParagraphAddition = "00917B74", RsidParagraphProperties = "00FE30D3", RsidRunAdditionDefault = "00917B74", ParagraphId = "1F4C34C9", TextId = "77777777" };

                        ParagraphProperties paragraphProperties3 = new ParagraphProperties();
                        Justification justification3 = new Justification() { Val = JustificationValues.Center };

                        ParagraphMarkRunProperties paragraphMarkRunProperties3 = new ParagraphMarkRunProperties();
                        RunFonts runFonts4 = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman", ComplexScript = "Times New Roman" };
                        FontSize fontSize4 = new FontSize() { Val = "28" };
                        FontSizeComplexScript fontSizeComplexScript4 = new FontSizeComplexScript() { Val = "28" };

                        paragraphMarkRunProperties3.Append(runFonts4);
                        paragraphMarkRunProperties3.Append(fontSize4);
                        paragraphMarkRunProperties3.Append(fontSizeComplexScript4);

                        paragraphProperties3.Append(justification3);
                        paragraphProperties3.Append(paragraphMarkRunProperties3);

                        Run run2 = new Run() { RsidRunProperties = "00FE30D3" };

                        RunProperties runProperties2 = new RunProperties();
                        RunFonts runFonts5 = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman", ComplexScript = "Times New Roman" };
                        FontSize fontSize5 = new FontSize() { Val = "28" };
                        FontSizeComplexScript fontSizeComplexScript5 = new FontSizeComplexScript() { Val = "28" };

                        runProperties2.Append(runFonts5);
                        runProperties2.Append(fontSize5);
                        runProperties2.Append(fontSizeComplexScript5);
                        Text text2 = new Text();
                        text2.Text = "№ п/п/";

                        run2.Append(runProperties2);
                        run2.Append(text2);

                        paragraph3.Append(paragraphProperties3);
                        paragraph3.Append(run2);

                        tableCell1.Append(tableCellProperties1);
                        tableCell1.Append(paragraph3);

                        TableCell tableCell2 = new TableCell();

                        TableCellProperties tableCellProperties2 = new TableCellProperties();
                        TableCellWidth tableCellWidth2 = new TableCellWidth() { Width = "6203", Type = TableWidthUnitValues.Dxa };
                        TableCellVerticalAlignment tableCellVerticalAlignment2 = new TableCellVerticalAlignment() { Val = TableVerticalAlignmentValues.Center };

                        tableCellProperties2.Append(tableCellWidth2);
                        tableCellProperties2.Append(tableCellVerticalAlignment2);

                        Paragraph paragraph4 = new Paragraph() { RsidParagraphMarkRevision = "00FE30D3", RsidParagraphAddition = "00917B74", RsidParagraphProperties = "008622BB", RsidRunAdditionDefault = "00917B74", ParagraphId = "7AD0013B", TextId = "63787869" };

                        ParagraphProperties paragraphProperties4 = new ParagraphProperties();
                        Justification justification4 = new Justification() { Val = JustificationValues.Center };

                        ParagraphMarkRunProperties paragraphMarkRunProperties4 = new ParagraphMarkRunProperties();
                        RunFonts runFonts6 = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman", ComplexScript = "Times New Roman" };
                        FontSize fontSize6 = new FontSize() { Val = "28" };
                        FontSizeComplexScript fontSizeComplexScript6 = new FontSizeComplexScript() { Val = "28" };

                        paragraphMarkRunProperties4.Append(runFonts6);
                        paragraphMarkRunProperties4.Append(fontSize6);
                        paragraphMarkRunProperties4.Append(fontSizeComplexScript6);

                        paragraphProperties4.Append(justification4);
                        paragraphProperties4.Append(paragraphMarkRunProperties4);

                        Run run3 = new Run() { RsidRunProperties = "00FE30D3" };

                        RunProperties runProperties3 = new RunProperties();
                        RunFonts runFonts7 = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman", ComplexScript = "Times New Roman" };
                        FontSize fontSize7 = new FontSize() { Val = "28" };
                        FontSizeComplexScript fontSizeComplexScript7 = new FontSizeComplexScript() { Val = "28" };

                        runProperties3.Append(runFonts7);
                        runProperties3.Append(fontSize7);
                        runProperties3.Append(fontSizeComplexScript7);
                        Text text3 = new Text() { Space = SpaceProcessingModeValues.Preserve };
                        text3.Text = "ФИО детей-сирот и воспитанников, помещенных ";

                        run3.Append(runProperties3);
                        run3.Append(text3);

                        Run run4 = new Run() { RsidRunAddition = "00E67E42" };

                        RunProperties runProperties4 = new RunProperties();
                        RunFonts runFonts8 = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman", ComplexScript = "Times New Roman" };
                        FontSize fontSize8 = new FontSize() { Val = "28" };
                        FontSizeComplexScript fontSizeComplexScript8 = new FontSizeComplexScript() { Val = "28" };

                        runProperties4.Append(runFonts8);
                        runProperties4.Append(fontSize8);
                        runProperties4.Append(fontSizeComplexScript8);
                        Break break1 = new Break();

                        run4.Append(runProperties4);
                        run4.Append(break1);

                        Run run5 = new Run() { RsidRunProperties = "00FE30D3" };

                        RunProperties runProperties5 = new RunProperties();
                        RunFonts runFonts9 = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman", ComplexScript = "Times New Roman" };
                        FontSize fontSize9 = new FontSize() { Val = "28" };
                        FontSizeComplexScript fontSizeComplexScript9 = new FontSizeComplexScript() { Val = "28" };

                        runProperties5.Append(runFonts9);
                        runProperties5.Append(fontSize9);
                        runProperties5.Append(fontSizeComplexScript9);
                        Text text4 = new Text() { Space = SpaceProcessingModeValues.Preserve };
                        text4.Text = "в стационарные ";

                        run5.Append(runProperties5);
                        run5.Append(text4);

                        Run run6 = new Run();

                        RunProperties runProperties6 = new RunProperties();
                        RunFonts runFonts10 = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman", ComplexScript = "Times New Roman" };
                        FontSize fontSize10 = new FontSize() { Val = "28" };
                        FontSizeComplexScript fontSizeComplexScript10 = new FontSizeComplexScript() { Val = "28" };

                        runProperties6.Append(runFonts10);
                        runProperties6.Append(fontSize10);
                        runProperties6.Append(fontSizeComplexScript10);
                        Text text5 = new Text();
                        text5.Text = "учреждения";

                        run6.Append(runProperties6);
                        run6.Append(text5);

                        paragraph4.Append(paragraphProperties4);
                        paragraph4.Append(run3);
                        paragraph4.Append(run4);
                        paragraph4.Append(run5);
                        paragraph4.Append(run6);

                        tableCell2.Append(tableCellProperties2);
                        tableCell2.Append(paragraph4);

                        TableCell tableCell3 = new TableCell();

                        TableCellProperties tableCellProperties3 = new TableCellProperties();
                        TableCellWidth tableCellWidth3 = new TableCellWidth() { Width = "2470", Type = TableWidthUnitValues.Dxa };
                        TableCellVerticalAlignment tableCellVerticalAlignment3 = new TableCellVerticalAlignment() { Val = TableVerticalAlignmentValues.Center };

                        tableCellProperties3.Append(tableCellWidth3);
                        tableCellProperties3.Append(tableCellVerticalAlignment3);

                        Paragraph paragraph5 = new Paragraph() { RsidParagraphMarkRevision = "00FE30D3", RsidParagraphAddition = "00917B74", RsidParagraphProperties = "008622BB", RsidRunAdditionDefault = "00917B74", ParagraphId = "6B1239B8", TextId = "1FB04BCF" };

                        ParagraphProperties paragraphProperties5 = new ParagraphProperties();
                        Justification justification5 = new Justification() { Val = JustificationValues.Center };

                        ParagraphMarkRunProperties paragraphMarkRunProperties5 = new ParagraphMarkRunProperties();
                        RunFonts runFonts11 = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman", ComplexScript = "Times New Roman" };
                        FontSize fontSize11 = new FontSize() { Val = "28" };
                        FontSizeComplexScript fontSizeComplexScript11 = new FontSizeComplexScript() { Val = "28" };

                        paragraphMarkRunProperties5.Append(runFonts11);
                        paragraphMarkRunProperties5.Append(fontSize11);
                        paragraphMarkRunProperties5.Append(fontSizeComplexScript11);

                        paragraphProperties5.Append(justification5);
                        paragraphProperties5.Append(paragraphMarkRunProperties5);

                        Run run7 = new Run() { RsidRunProperties = "00FE30D3" };

                        RunProperties runProperties7 = new RunProperties();
                        RunFonts runFonts12 = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman", ComplexScript = "Times New Roman" };
                        FontSize fontSize12 = new FontSize() { Val = "28" };
                        FontSizeComplexScript fontSizeComplexScript12 = new FontSizeComplexScript() { Val = "28" };

                        runProperties7.Append(runFonts12);
                        runProperties7.Append(fontSize12);
                        runProperties7.Append(fontSizeComplexScript12);
                        Text text6 = new Text();
                        text6.Text = "Дата рождения";

                        run7.Append(runProperties7);
                        run7.Append(text6);

                        paragraph5.Append(paragraphProperties5);
                        paragraph5.Append(run7);

                        tableCell3.Append(tableCellProperties3);
                        tableCell3.Append(paragraph5);

                        tableRow1.Append(tableCell1);
                        tableRow1.Append(tableCell2);
                        tableRow1.Append(tableCell3);

                        table1.Append(tableRow1);
                    }

                    var gg = list.GroupPupils.Where(ss => ss.OrganisatonAddresId != null).ToList().GroupBy(g => g.OrganisatonAddresId);
                    int i = 1;
                    foreach (var a in gg)
                    {
                        {
                            TableRow tableRow2 = new TableRow() { RsidTableRowMarkRevision = "001C30EE", RsidTableRowAddition = "00FE30D3", RsidTableRowProperties = "00E20D38", ParagraphId = "2AD6DBC6", TextId = "77777777" };

                            TableCell tableCell4 = new TableCell();

                            TableCellProperties tableCellProperties4 = new TableCellProperties();
                            TableCellWidth tableCellWidth4 = new TableCellWidth() { Width = "9345", Type = TableWidthUnitValues.Dxa };
                            GridSpan gridSpan1 = new GridSpan() { Val = 3 };
                            TableCellVerticalAlignment tableCellVerticalAlignment4 = new TableCellVerticalAlignment() { Val = TableVerticalAlignmentValues.Center };

                            tableCellProperties4.Append(tableCellWidth4);
                            tableCellProperties4.Append(gridSpan1);
                            tableCellProperties4.Append(tableCellVerticalAlignment4);

                            Paragraph paragraph6 = new Paragraph() { RsidParagraphMarkRevision = "00FE30D3", RsidParagraphAddition = "00FE30D3", RsidParagraphProperties = "00FE30D3", RsidRunAdditionDefault = "00FE30D3", ParagraphId = "0B71E124", TextId = "47E8F151" };

                            ParagraphProperties paragraphProperties6 = new ParagraphProperties();

                            ParagraphMarkRunProperties paragraphMarkRunProperties6 = new ParagraphMarkRunProperties();
                            RunFonts runFonts13 = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman", ComplexScript = "Times New Roman" };
                            Bold bold4 = new Bold();
                            FontSize fontSize13 = new FontSize() { Val = "28" };
                            FontSizeComplexScript fontSizeComplexScript13 = new FontSizeComplexScript() { Val = "28" };

                            paragraphMarkRunProperties6.Append(runFonts13);
                            paragraphMarkRunProperties6.Append(bold4);
                            paragraphMarkRunProperties6.Append(fontSize13);
                            paragraphMarkRunProperties6.Append(fontSizeComplexScript13);

                            paragraphProperties6.Append(paragraphMarkRunProperties6);

                            Run run8 = new Run() { RsidRunProperties = "00FE30D3" };

                            RunProperties runProperties8 = new RunProperties();
                            RunFonts runFonts14 = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman", ComplexScript = "Times New Roman" };
                            Bold bold5 = new Bold();
                            FontSize fontSize14 = new FontSize() { Val = "28" };
                            FontSizeComplexScript fontSizeComplexScript14 = new FontSizeComplexScript() { Val = "28" };

                            runProperties8.Append(runFonts14);
                            runProperties8.Append(bold5);
                            runProperties8.Append(fontSize14);
                            runProperties8.Append(fontSizeComplexScript14);
                            Text text7 = new Text();
                            text7.Text = $"Адрес № {i++}: ";

                            run8.Append(runProperties8);
                            run8.Append(text7);

                            Run run9 = new Run() { RsidRunAddition = "00B77DFD" };

                            RunProperties runProperties9 = new RunProperties();
                            RunFonts runFonts15 = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman", ComplexScript = "Times New Roman" };
                            Bold bold6 = new Bold();
                            FontSize fontSize15 = new FontSize() { Val = "28" };
                            FontSizeComplexScript fontSizeComplexScript15 = new FontSizeComplexScript() { Val = "28" };

                            runProperties9.Append(runFonts15);
                            runProperties9.Append(bold6);
                            runProperties9.Append(fontSize15);
                            runProperties9.Append(fontSizeComplexScript15);
                            Text text8 = new Text() { Space = SpaceProcessingModeValues.Preserve };
                            text8.Text = $" {a.FirstOrDefault().OrganisatonAddres.Address.Name}";

                            run9.Append(runProperties9);
                            run9.Append(text8);

                            paragraph6.Append(paragraphProperties6);
                            paragraph6.Append(run8);
                            paragraph6.Append(run9);

                            tableCell4.Append(tableCellProperties4);
                            tableCell4.Append(paragraph6);

                            tableRow2.Append(tableCell4);

                            table1.Append(tableRow2);
                        }

                        int j = 1;
                        foreach (var p in a.ToList())
                        {
                            TableRow tableRow3 = new TableRow() { RsidTableRowMarkRevision = "001C30EE", RsidTableRowAddition = "00917B74", RsidTableRowProperties = "00B830E4", ParagraphId = "788A9D53", TextId = "77777777" };

                            TableCell tableCell5 = new TableCell();

                            TableCellProperties tableCellProperties5 = new TableCellProperties();
                            TableCellWidth tableCellWidth5 = new TableCellWidth() { Width = "672", Type = TableWidthUnitValues.Dxa };
                            TableCellVerticalAlignment tableCellVerticalAlignment5 = new TableCellVerticalAlignment() { Val = TableVerticalAlignmentValues.Center };

                            tableCellProperties5.Append(tableCellWidth5);
                            tableCellProperties5.Append(tableCellVerticalAlignment5);

                            Paragraph paragraph7 = new Paragraph() { RsidParagraphMarkRevision = "00FE30D3", RsidParagraphAddition = "00917B74", RsidParagraphProperties = "00FE30D3", RsidRunAdditionDefault = "00917B74", ParagraphId = "0FBA0F87", TextId = "77777777" };

                            ParagraphProperties paragraphProperties7 = new ParagraphProperties();

                            ParagraphMarkRunProperties paragraphMarkRunProperties7 = new ParagraphMarkRunProperties();
                            RunFonts runFonts16 = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman", ComplexScript = "Times New Roman" };
                            FontSize fontSize16 = new FontSize() { Val = "28" };
                            FontSizeComplexScript fontSizeComplexScript16 = new FontSizeComplexScript() { Val = "28" };

                            paragraphMarkRunProperties7.Append(runFonts16);
                            paragraphMarkRunProperties7.Append(fontSize16);
                            paragraphMarkRunProperties7.Append(fontSizeComplexScript16);

                            paragraphProperties7.Append(paragraphMarkRunProperties7);

                            Run run10 = new Run() { RsidRunProperties = "00FE30D3" };

                            RunProperties runProperties10 = new RunProperties();
                            RunFonts runFonts17 = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman", ComplexScript = "Times New Roman" };
                            FontSize fontSize17 = new FontSize() { Val = "28" };
                            FontSizeComplexScript fontSizeComplexScript17 = new FontSizeComplexScript() { Val = "28" };

                            runProperties10.Append(runFonts17);
                            runProperties10.Append(fontSize17);
                            runProperties10.Append(fontSizeComplexScript17);
                            Text text9 = new Text();
                            text9.Text = $"{j++}.";

                            run10.Append(runProperties10);
                            run10.Append(text9);

                            paragraph7.Append(paragraphProperties7);
                            paragraph7.Append(run10);

                            tableCell5.Append(tableCellProperties5);
                            tableCell5.Append(paragraph7);

                            TableCell tableCell6 = new TableCell();

                            TableCellProperties tableCellProperties6 = new TableCellProperties();
                            TableCellWidth tableCellWidth6 = new TableCellWidth() { Width = "6203", Type = TableWidthUnitValues.Dxa };
                            TableCellVerticalAlignment tableCellVerticalAlignment6 = new TableCellVerticalAlignment() { Val = TableVerticalAlignmentValues.Center };

                            tableCellProperties6.Append(tableCellWidth6);
                            tableCellProperties6.Append(tableCellVerticalAlignment6);

                            Paragraph paragraph8 = new Paragraph() { RsidParagraphMarkRevision = "005A7013", RsidParagraphAddition = "00917B74", RsidParagraphProperties = "00FE30D3", RsidRunAdditionDefault = "005A7013", ParagraphId = "6FF2644A", TextId = "61911ABD" };

                            ParagraphProperties paragraphProperties8 = new ParagraphProperties();

                            ParagraphMarkRunProperties paragraphMarkRunProperties8 = new ParagraphMarkRunProperties();
                            RunFonts runFonts18 = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman", ComplexScript = "Times New Roman" };
                            FontSize fontSize18 = new FontSize() { Val = "28" };
                            FontSizeComplexScript fontSizeComplexScript18 = new FontSizeComplexScript() { Val = "28" };

                            paragraphMarkRunProperties8.Append(runFonts18);
                            paragraphMarkRunProperties8.Append(fontSize18);
                            paragraphMarkRunProperties8.Append(fontSizeComplexScript18);

                            paragraphProperties8.Append(paragraphMarkRunProperties8);
                            ProofError proofError1 = new ProofError() { Type = ProofingErrorValues.SpellStart };

                            Run run11 = new Run();

                            RunProperties runProperties11 = new RunProperties();
                            RunFonts runFonts19 = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman", ComplexScript = "Times New Roman" };
                            FontSize fontSize19 = new FontSize() { Val = "28" };
                            FontSizeComplexScript fontSizeComplexScript19 = new FontSizeComplexScript() { Val = "28" };

                            runProperties11.Append(runFonts19);
                            runProperties11.Append(fontSize19);
                            runProperties11.Append(fontSizeComplexScript19);
                            Text text10 = new Text();
                            text10.Text = p.Pupil.Child.GetFio();

                            run11.Append(runProperties11);
                            run11.Append(text10);

                            paragraph8.Append(paragraphProperties8);
                            paragraph8.Append(proofError1);
                            paragraph8.Append(run11);

                            tableCell6.Append(tableCellProperties6);
                            tableCell6.Append(paragraph8);

                            TableCell tableCell7 = new TableCell();

                            TableCellProperties tableCellProperties7 = new TableCellProperties();
                            TableCellWidth tableCellWidth7 = new TableCellWidth() { Width = "2470", Type = TableWidthUnitValues.Dxa };
                            TableCellVerticalAlignment tableCellVerticalAlignment7 = new TableCellVerticalAlignment() { Val = TableVerticalAlignmentValues.Center };

                            tableCellProperties7.Append(tableCellWidth7);
                            tableCellProperties7.Append(tableCellVerticalAlignment7);

                            Paragraph paragraph9 = new Paragraph() { RsidParagraphMarkRevision = "00FE30D3", RsidParagraphAddition = "00917B74", RsidParagraphProperties = "00FE30D3", RsidRunAdditionDefault = "005A7013", ParagraphId = "40FE5834", TextId = "46CBE13F" };

                            ParagraphProperties paragraphProperties9 = new ParagraphProperties();

                            ParagraphMarkRunProperties paragraphMarkRunProperties9 = new ParagraphMarkRunProperties();
                            RunFonts runFonts21 = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman", ComplexScript = "Times New Roman" };
                            FontSize fontSize21 = new FontSize() { Val = "28" };
                            FontSizeComplexScript fontSizeComplexScript21 = new FontSizeComplexScript() { Val = "28" };

                            paragraphMarkRunProperties9.Append(runFonts21);
                            paragraphMarkRunProperties9.Append(fontSize21);
                            paragraphMarkRunProperties9.Append(fontSizeComplexScript21);

                            paragraphProperties9.Append(paragraphMarkRunProperties9);

                            Run run13 = new Run();

                            RunProperties runProperties13 = new RunProperties();
                            RunFonts runFonts22 = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman", ComplexScript = "Times New Roman" };
                            FontSize fontSize22 = new FontSize() { Val = "28" };
                            FontSizeComplexScript fontSizeComplexScript22 = new FontSizeComplexScript() { Val = "28" };

                            runProperties13.Append(runFonts22);
                            runProperties13.Append(fontSize22);
                            runProperties13.Append(fontSizeComplexScript22);
                            Text text12 = new Text();
                            text12.Text = $"{p.Pupil.Child.DateOfBirth.FormatEx(string.Empty)}";

                            run13.Append(runProperties13);
                            run13.Append(text12);

                            paragraph9.Append(paragraphProperties9);
                            paragraph9.Append(run13);

                            tableCell7.Append(tableCellProperties7);
                            tableCell7.Append(paragraph9);

                            tableRow3.Append(tableCell5);
                            tableRow3.Append(tableCell6);
                            tableRow3.Append(tableCell7);

                            table1.Append(tableRow3);
                        }
                    }

                    Paragraph paragraph10 = new Paragraph() { RsidParagraphAddition = "001C30EE", RsidParagraphProperties = "00FE30D3", RsidRunAdditionDefault = "001C30EE", ParagraphId = "19850A2D", TextId = "77777777" };

                    ParagraphProperties paragraphProperties10 = new ParagraphProperties();

                    ParagraphMarkRunProperties paragraphMarkRunProperties10 = new ParagraphMarkRunProperties();
                    RunFonts runFonts23 = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman", ComplexScript = "Times New Roman" };
                    FontSize fontSize23 = new FontSize() { Val = "24" };
                    FontSizeComplexScript fontSizeComplexScript23 = new FontSizeComplexScript() { Val = "24" };

                    paragraphMarkRunProperties10.Append(runFonts23);
                    paragraphMarkRunProperties10.Append(fontSize23);
                    paragraphMarkRunProperties10.Append(fontSizeComplexScript23);

                    paragraphProperties10.Append(paragraphMarkRunProperties10);

                    paragraph10.Append(paragraphProperties10);



                    //вторая таблица
                    Table table2 = new Table();

                    TableProperties tableProperties2 = new TableProperties();
                    TableStyle tableStyle2 = new TableStyle() { Val = "a3" };
                    TableWidth tableWidth2 = new TableWidth() { Width = "0", Type = TableWidthUnitValues.Auto };
                    TableLook tableLook2 = new TableLook() { Val = "04A0", FirstRow = true, LastRow = false, FirstColumn = true, LastColumn = false, NoHorizontalBand = false, NoVerticalBand = true };
                    var tableBorders2 = new TableBorders(
                        new TopBorder { Val = new EnumValue<BorderValues>(BorderValues.Single) },
                        new BottomBorder { Val = new EnumValue<BorderValues>(BorderValues.Single) },
                        new LeftBorder { Val = new EnumValue<BorderValues>(BorderValues.Single) },
                        new RightBorder { Val = new EnumValue<BorderValues>(BorderValues.Single) },
                        new InsideHorizontalBorder { Val = new EnumValue<BorderValues>(BorderValues.Single) },
                        new InsideVerticalBorder { Val = new EnumValue<BorderValues>(BorderValues.Single) });

                    tableProperties2.Append(tableStyle2);
                    tableProperties2.Append(tableWidth2);
                    tableProperties2.Append(tableLook2);
                    tableProperties2.Append(tableBorders2);

                    TableGrid tableGrid2 = new TableGrid();
                    GridColumn gridColumn4 = new GridColumn() { Width = "676" };
                    GridColumn gridColumn5 = new GridColumn() { Width = "4793" };
                    GridColumn gridColumn6 = new GridColumn() { Width = "1547" };
                    GridColumn gridColumn7 = new GridColumn() { Width = "2329" };

                    tableGrid2.Append(gridColumn4);
                    tableGrid2.Append(gridColumn5);
                    tableGrid2.Append(gridColumn6);
                    tableGrid2.Append(gridColumn7);

                    table2.Append(tableProperties2);
                    table2.Append(tableGrid2);

                    {
                        TableRow tableRow4 = new TableRow() { RsidTableRowMarkRevision = "001C30EE", RsidTableRowAddition = "00917B74", RsidTableRowProperties = "00E20D38", ParagraphId = "6ABEDCDC", TextId = "77777777" };

                        TableCell tableCell8 = new TableCell();

                        TableCellProperties tableCellProperties8 = new TableCellProperties();
                        TableCellWidth tableCellWidth8 = new TableCellWidth() { Width = "676", Type = TableWidthUnitValues.Dxa };
                        TableCellVerticalAlignment tableCellVerticalAlignment8 = new TableCellVerticalAlignment() { Val = TableVerticalAlignmentValues.Center };

                        tableCellProperties8.Append(tableCellWidth8);
                        tableCellProperties8.Append(tableCellVerticalAlignment8);

                        Paragraph paragraph11 = new Paragraph() { RsidParagraphMarkRevision = "00FE30D3", RsidParagraphAddition = "00917B74", RsidParagraphProperties = "00821F51", RsidRunAdditionDefault = "00917B74", ParagraphId = "1FE426A2", TextId = "77777777" };

                        ParagraphProperties paragraphProperties11 = new ParagraphProperties();
                        Justification justification6 = new Justification() { Val = JustificationValues.Center };

                        ParagraphMarkRunProperties paragraphMarkRunProperties11 = new ParagraphMarkRunProperties();
                        RunFonts runFonts24 = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman", ComplexScript = "Times New Roman" };
                        FontSize fontSize24 = new FontSize() { Val = "28" };
                        FontSizeComplexScript fontSizeComplexScript24 = new FontSizeComplexScript() { Val = "28" };

                        paragraphMarkRunProperties11.Append(runFonts24);
                        paragraphMarkRunProperties11.Append(fontSize24);
                        paragraphMarkRunProperties11.Append(fontSizeComplexScript24);

                        paragraphProperties11.Append(justification6);
                        paragraphProperties11.Append(paragraphMarkRunProperties11);

                        Run run14 = new Run() { RsidRunProperties = "00FE30D3" };

                        RunProperties runProperties14 = new RunProperties();
                        RunFonts runFonts25 = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman", ComplexScript = "Times New Roman" };
                        FontSize fontSize25 = new FontSize() { Val = "28" };
                        FontSizeComplexScript fontSizeComplexScript25 = new FontSizeComplexScript() { Val = "28" };

                        runProperties14.Append(runFonts25);
                        runProperties14.Append(fontSize25);
                        runProperties14.Append(fontSizeComplexScript25);
                        Text text13 = new Text();
                        text13.Text = "№ п/п/";

                        run14.Append(runProperties14);
                        run14.Append(text13);

                        paragraph11.Append(paragraphProperties11);
                        paragraph11.Append(run14);

                        tableCell8.Append(tableCellProperties8);
                        tableCell8.Append(paragraph11);

                        TableCell tableCell9 = new TableCell();

                        TableCellProperties tableCellProperties9 = new TableCellProperties();
                        TableCellWidth tableCellWidth9 = new TableCellWidth() { Width = "4793", Type = TableWidthUnitValues.Dxa };
                        TableCellVerticalAlignment tableCellVerticalAlignment9 = new TableCellVerticalAlignment() { Val = TableVerticalAlignmentValues.Center };

                        tableCellProperties9.Append(tableCellWidth9);
                        tableCellProperties9.Append(tableCellVerticalAlignment9);

                        Paragraph paragraph12 = new Paragraph() { RsidParagraphMarkRevision = "00FE30D3", RsidParagraphAddition = "00917B74", RsidParagraphProperties = "00E20D38", RsidRunAdditionDefault = "00917B74", ParagraphId = "4B01B719", TextId = "61855BE1" };

                        ParagraphProperties paragraphProperties12 = new ParagraphProperties();
                        Justification justification7 = new Justification() { Val = JustificationValues.Center };

                        ParagraphMarkRunProperties paragraphMarkRunProperties12 = new ParagraphMarkRunProperties();
                        RunFonts runFonts26 = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman", ComplexScript = "Times New Roman" };
                        FontSize fontSize26 = new FontSize() { Val = "28" };
                        FontSizeComplexScript fontSizeComplexScript26 = new FontSizeComplexScript() { Val = "28" };

                        paragraphMarkRunProperties12.Append(runFonts26);
                        paragraphMarkRunProperties12.Append(fontSize26);
                        paragraphMarkRunProperties12.Append(fontSizeComplexScript26);

                        paragraphProperties12.Append(justification7);
                        paragraphProperties12.Append(paragraphMarkRunProperties12);

                        Run run15 = new Run() { RsidRunProperties = "00FE30D3" };

                        RunProperties runProperties15 = new RunProperties();
                        RunFonts runFonts27 = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman", ComplexScript = "Times New Roman" };
                        FontSize fontSize27 = new FontSize() { Val = "28" };
                        FontSizeComplexScript fontSizeComplexScript27 = new FontSizeComplexScript() { Val = "28" };

                        runProperties15.Append(runFonts27);
                        runProperties15.Append(fontSize27);
                        runProperties15.Append(fontSizeComplexScript27);
                        Text text14 = new Text() { Space = SpaceProcessingModeValues.Preserve };
                        text14.Text = "ФИО ";

                        run15.Append(runProperties15);
                        run15.Append(text14);

                        Run run16 = new Run();

                        RunProperties runProperties16 = new RunProperties();
                        RunFonts runFonts28 = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman", ComplexScript = "Times New Roman" };
                        FontSize fontSize28 = new FontSize() { Val = "28" };
                        FontSizeComplexScript fontSizeComplexScript28 = new FontSizeComplexScript() { Val = "28" };

                        runProperties16.Append(runFonts28);
                        runProperties16.Append(fontSize28);
                        runProperties16.Append(fontSizeComplexScript28);
                        Text text15 = new Text();
                        text15.Text = "Сопровождающих";

                        run16.Append(runProperties16);
                        run16.Append(text15);

                        paragraph12.Append(paragraphProperties12);
                        paragraph12.Append(run15);
                        paragraph12.Append(run16);

                        tableCell9.Append(tableCellProperties9);
                        tableCell9.Append(paragraph12);

                        TableCell tableCell10 = new TableCell();

                        TableCellProperties tableCellProperties10 = new TableCellProperties();
                        TableCellWidth tableCellWidth10 = new TableCellWidth() { Width = "1547", Type = TableWidthUnitValues.Dxa };
                        TableCellVerticalAlignment tableCellVerticalAlignment10 = new TableCellVerticalAlignment() { Val = TableVerticalAlignmentValues.Center };

                        tableCellProperties10.Append(tableCellWidth10);
                        tableCellProperties10.Append(tableCellVerticalAlignment10);

                        Paragraph paragraph13 = new Paragraph() { RsidParagraphMarkRevision = "00FE30D3", RsidParagraphAddition = "00917B74", RsidParagraphProperties = "00821F51", RsidRunAdditionDefault = "00917B74", ParagraphId = "1BDA199E", TextId = "77777777" };

                        ParagraphProperties paragraphProperties13 = new ParagraphProperties();
                        Justification justification8 = new Justification() { Val = JustificationValues.Center };

                        ParagraphMarkRunProperties paragraphMarkRunProperties13 = new ParagraphMarkRunProperties();
                        RunFonts runFonts29 = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman", ComplexScript = "Times New Roman" };
                        FontSize fontSize29 = new FontSize() { Val = "28" };
                        FontSizeComplexScript fontSizeComplexScript29 = new FontSizeComplexScript() { Val = "28" };

                        paragraphMarkRunProperties13.Append(runFonts29);
                        paragraphMarkRunProperties13.Append(fontSize29);
                        paragraphMarkRunProperties13.Append(fontSizeComplexScript29);

                        paragraphProperties13.Append(justification8);
                        paragraphProperties13.Append(paragraphMarkRunProperties13);

                        Run run17 = new Run() { RsidRunProperties = "00FE30D3" };

                        RunProperties runProperties17 = new RunProperties();
                        RunFonts runFonts30 = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman", ComplexScript = "Times New Roman" };
                        FontSize fontSize30 = new FontSize() { Val = "28" };
                        FontSizeComplexScript fontSizeComplexScript30 = new FontSizeComplexScript() { Val = "28" };

                        runProperties17.Append(runFonts30);
                        runProperties17.Append(fontSize30);
                        runProperties17.Append(fontSizeComplexScript30);
                        Text text16 = new Text();
                        text16.Text = "Дата рождения";

                        run17.Append(runProperties17);
                        run17.Append(text16);

                        paragraph13.Append(paragraphProperties13);
                        paragraph13.Append(run17);

                        tableCell10.Append(tableCellProperties10);
                        tableCell10.Append(paragraph13);

                        TableCell tableCell11 = new TableCell();

                        TableCellProperties tableCellProperties11 = new TableCellProperties();
                        TableCellWidth tableCellWidth11 = new TableCellWidth() { Width = "2329", Type = TableWidthUnitValues.Dxa };
                        TableCellVerticalAlignment tableCellVerticalAlignment11 = new TableCellVerticalAlignment() { Val = TableVerticalAlignmentValues.Center };

                        tableCellProperties11.Append(tableCellWidth11);
                        tableCellProperties11.Append(tableCellVerticalAlignment11);

                        Paragraph paragraph14 = new Paragraph() { RsidParagraphMarkRevision = "00FE30D3", RsidParagraphAddition = "00917B74", RsidParagraphProperties = "00821F51", RsidRunAdditionDefault = "00917B74", ParagraphId = "19158C95", TextId = "77777777" };

                        ParagraphProperties paragraphProperties14 = new ParagraphProperties();
                        Justification justification9 = new Justification() { Val = JustificationValues.Center };

                        ParagraphMarkRunProperties paragraphMarkRunProperties14 = new ParagraphMarkRunProperties();
                        RunFonts runFonts31 = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman", ComplexScript = "Times New Roman" };
                        FontSize fontSize31 = new FontSize() { Val = "28" };
                        FontSizeComplexScript fontSizeComplexScript31 = new FontSizeComplexScript() { Val = "28" };

                        paragraphMarkRunProperties14.Append(runFonts31);
                        paragraphMarkRunProperties14.Append(fontSize31);
                        paragraphMarkRunProperties14.Append(fontSizeComplexScript31);

                        paragraphProperties14.Append(justification9);
                        paragraphProperties14.Append(paragraphMarkRunProperties14);

                        Run run18 = new Run();

                        RunProperties runProperties18 = new RunProperties();
                        RunFonts runFonts32 = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman", ComplexScript = "Times New Roman" };
                        FontSize fontSize32 = new FontSize() { Val = "28" };
                        FontSizeComplexScript fontSizeComplexScript32 = new FontSizeComplexScript() { Val = "28" };

                        runProperties18.Append(runFonts32);
                        runProperties18.Append(fontSize32);
                        runProperties18.Append(fontSizeComplexScript32);
                        Text text17 = new Text();
                        text17.Text = "Контактный телефон";

                        run18.Append(runProperties18);
                        run18.Append(text17);

                        paragraph14.Append(paragraphProperties14);
                        paragraph14.Append(run18);

                        tableCell11.Append(tableCellProperties11);
                        tableCell11.Append(paragraph14);

                        tableRow4.Append(tableCell8);
                        tableRow4.Append(tableCell9);
                        tableRow4.Append(tableCell10);
                        tableRow4.Append(tableCell11);

                        table2.Append(tableRow4);
                    }

                    var gh = list.GroupCollaborators.Where(ss => ss.OrganisatonAddresId != null).ToList().GroupBy(g => g.OrganisatonAddresId);
                    i = 1;
                    foreach (var a in gh)
                    {
                        {
                            TableRow tableRow5 = new TableRow() { RsidTableRowMarkRevision = "001C30EE", RsidTableRowAddition = "00917B74", RsidTableRowProperties = "00E20D38", ParagraphId = "26A2766A", TextId = "77777777" };

                            TableCell tableCell12 = new TableCell();

                            TableCellProperties tableCellProperties12 = new TableCellProperties();
                            TableCellWidth tableCellWidth12 = new TableCellWidth() { Width = "9345", Type = TableWidthUnitValues.Dxa };
                            GridSpan gridSpan2 = new GridSpan() { Val = 4 };
                            TableCellVerticalAlignment tableCellVerticalAlignment12 = new TableCellVerticalAlignment() { Val = TableVerticalAlignmentValues.Center };

                            tableCellProperties12.Append(tableCellWidth12);
                            tableCellProperties12.Append(gridSpan2);
                            tableCellProperties12.Append(tableCellVerticalAlignment12);

                            Paragraph paragraph15 = new Paragraph() { RsidParagraphMarkRevision = "00FE30D3", RsidParagraphAddition = "00917B74", RsidParagraphProperties = "00821F51", RsidRunAdditionDefault = "00917B74", ParagraphId = "372BCE9F", TextId = "681DBDC7" };

                            ParagraphProperties paragraphProperties15 = new ParagraphProperties();

                            ParagraphMarkRunProperties paragraphMarkRunProperties15 = new ParagraphMarkRunProperties();
                            RunFonts runFonts33 = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman", ComplexScript = "Times New Roman" };
                            Bold bold7 = new Bold();
                            FontSize fontSize33 = new FontSize() { Val = "28" };
                            FontSizeComplexScript fontSizeComplexScript33 = new FontSizeComplexScript() { Val = "28" };

                            paragraphMarkRunProperties15.Append(runFonts33);
                            paragraphMarkRunProperties15.Append(bold7);
                            paragraphMarkRunProperties15.Append(fontSize33);
                            paragraphMarkRunProperties15.Append(fontSizeComplexScript33);

                            paragraphProperties15.Append(paragraphMarkRunProperties15);

                            Run run19 = new Run() { RsidRunProperties = "00FE30D3" };

                            RunProperties runProperties19 = new RunProperties();
                            RunFonts runFonts34 = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman", ComplexScript = "Times New Roman" };
                            Bold bold8 = new Bold();
                            FontSize fontSize34 = new FontSize() { Val = "28" };
                            FontSizeComplexScript fontSizeComplexScript34 = new FontSizeComplexScript() { Val = "28" };

                            runProperties19.Append(runFonts34);
                            runProperties19.Append(bold8);
                            runProperties19.Append(fontSize34);
                            runProperties19.Append(fontSizeComplexScript34);
                            Text text18 = new Text();
                            text18.Text = $"Адрес № {i++}: ";

                            run19.Append(runProperties19);
                            run19.Append(text18);

                            Run run20 = new Run();

                            RunProperties runProperties20 = new RunProperties();
                            RunFonts runFonts35 = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman", ComplexScript = "Times New Roman" };
                            Bold bold9 = new Bold();
                            FontSize fontSize35 = new FontSize() { Val = "28" };
                            FontSizeComplexScript fontSizeComplexScript35 = new FontSizeComplexScript() { Val = "28" };

                            runProperties20.Append(runFonts35);
                            runProperties20.Append(bold9);
                            runProperties20.Append(fontSize35);
                            runProperties20.Append(fontSizeComplexScript35);
                            Text text19 = new Text() { Space = SpaceProcessingModeValues.Preserve };
                            text19.Text = $" {a.FirstOrDefault().OrganisatonAddres.Address.Name}";

                            run20.Append(runProperties20);
                            run20.Append(text19);

                            paragraph15.Append(paragraphProperties15);
                            paragraph15.Append(run19);
                            paragraph15.Append(run20);

                            tableCell12.Append(tableCellProperties12);
                            tableCell12.Append(paragraph15);

                            tableRow5.Append(tableCell12);

                            table2.Append(tableRow5);
                        }

                        var j = 1;
                        foreach (var p in a.ToList())
                        {
                            TableRow tableRow6 = new TableRow() { RsidTableRowMarkRevision = "001C30EE", RsidTableRowAddition = "00917B74", RsidTableRowProperties = "00E20D38", ParagraphId = "40D2E31E", TextId = "77777777" };

                            TableCell tableCell13 = new TableCell();

                            TableCellProperties tableCellProperties13 = new TableCellProperties();
                            TableCellWidth tableCellWidth13 = new TableCellWidth() { Width = "676", Type = TableWidthUnitValues.Dxa };
                            TableCellVerticalAlignment tableCellVerticalAlignment13 = new TableCellVerticalAlignment() { Val = TableVerticalAlignmentValues.Center };

                            tableCellProperties13.Append(tableCellWidth13);
                            tableCellProperties13.Append(tableCellVerticalAlignment13);

                            Paragraph paragraph16 = new Paragraph() { RsidParagraphMarkRevision = "00FE30D3", RsidParagraphAddition = "00917B74", RsidParagraphProperties = "00821F51", RsidRunAdditionDefault = "00917B74", ParagraphId = "41A59361", TextId = "77777777" };

                            ParagraphProperties paragraphProperties16 = new ParagraphProperties();

                            ParagraphMarkRunProperties paragraphMarkRunProperties16 = new ParagraphMarkRunProperties();
                            RunFonts runFonts36 = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman", ComplexScript = "Times New Roman" };
                            FontSize fontSize36 = new FontSize() { Val = "28" };
                            FontSizeComplexScript fontSizeComplexScript36 = new FontSizeComplexScript() { Val = "28" };

                            paragraphMarkRunProperties16.Append(runFonts36);
                            paragraphMarkRunProperties16.Append(fontSize36);
                            paragraphMarkRunProperties16.Append(fontSizeComplexScript36);

                            paragraphProperties16.Append(paragraphMarkRunProperties16);

                            Run run21 = new Run() { RsidRunProperties = "00FE30D3" };

                            RunProperties runProperties21 = new RunProperties();
                            RunFonts runFonts37 = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman", ComplexScript = "Times New Roman" };
                            FontSize fontSize37 = new FontSize() { Val = "28" };
                            FontSizeComplexScript fontSizeComplexScript37 = new FontSizeComplexScript() { Val = "28" };

                            runProperties21.Append(runFonts37);
                            runProperties21.Append(fontSize37);
                            runProperties21.Append(fontSizeComplexScript37);
                            Text text20 = new Text();
                            text20.Text = $"{j++}.";

                            run21.Append(runProperties21);
                            run21.Append(text20);

                            paragraph16.Append(paragraphProperties16);
                            paragraph16.Append(run21);

                            tableCell13.Append(tableCellProperties13);
                            tableCell13.Append(paragraph16);

                            TableCell tableCell14 = new TableCell();

                            TableCellProperties tableCellProperties14 = new TableCellProperties();
                            TableCellWidth tableCellWidth14 = new TableCellWidth() { Width = "4793", Type = TableWidthUnitValues.Dxa };
                            TableCellVerticalAlignment tableCellVerticalAlignment14 = new TableCellVerticalAlignment() { Val = TableVerticalAlignmentValues.Center };

                            tableCellProperties14.Append(tableCellWidth14);
                            tableCellProperties14.Append(tableCellVerticalAlignment14);

                            Paragraph paragraph17 = new Paragraph() { RsidParagraphMarkRevision = "00FE30D3", RsidParagraphAddition = "00917B74", RsidParagraphProperties = "00821F51", RsidRunAdditionDefault = "005A7013", ParagraphId = "0813C609", TextId = "5DFE300D" };

                            ParagraphProperties paragraphProperties17 = new ParagraphProperties();

                            ParagraphMarkRunProperties paragraphMarkRunProperties17 = new ParagraphMarkRunProperties();
                            RunFonts runFonts38 = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman", ComplexScript = "Times New Roman" };
                            FontSize fontSize38 = new FontSize() { Val = "28" };
                            FontSizeComplexScript fontSizeComplexScript38 = new FontSizeComplexScript() { Val = "28" };

                            paragraphMarkRunProperties17.Append(runFonts38);
                            paragraphMarkRunProperties17.Append(fontSize38);
                            paragraphMarkRunProperties17.Append(fontSizeComplexScript38);

                            paragraphProperties17.Append(paragraphMarkRunProperties17);
                            ProofError proofError3 = new ProofError() { Type = ProofingErrorValues.SpellStart };

                            Run run22 = new Run();

                            RunProperties runProperties22 = new RunProperties();
                            RunFonts runFonts39 = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman", ComplexScript = "Times New Roman" };
                            FontSize fontSize39 = new FontSize() { Val = "28" };
                            FontSizeComplexScript fontSizeComplexScript39 = new FontSizeComplexScript() { Val = "28" };

                            runProperties22.Append(runFonts39);
                            runProperties22.Append(fontSize39);
                            runProperties22.Append(fontSizeComplexScript39);
                            Text text21 = new Text();
                            text21.Text = p.OrganisatonCollaborator.Applicant.GetFio();

                            run22.Append(runProperties22);
                            run22.Append(text21);

                            paragraph17.Append(paragraphProperties17);
                            paragraph17.Append(proofError3);
                            paragraph17.Append(run22);

                            tableCell14.Append(tableCellProperties14);
                            tableCell14.Append(paragraph17);

                            TableCell tableCell15 = new TableCell();

                            TableCellProperties tableCellProperties15 = new TableCellProperties();
                            TableCellWidth tableCellWidth15 = new TableCellWidth() { Width = "1547", Type = TableWidthUnitValues.Dxa };
                            TableCellVerticalAlignment tableCellVerticalAlignment15 = new TableCellVerticalAlignment() { Val = TableVerticalAlignmentValues.Center };

                            tableCellProperties15.Append(tableCellWidth15);
                            tableCellProperties15.Append(tableCellVerticalAlignment15);

                            Paragraph paragraph18 = new Paragraph() { RsidParagraphMarkRevision = "00FE30D3", RsidParagraphAddition = "00917B74", RsidParagraphProperties = "00821F51", RsidRunAdditionDefault = "005A7013", ParagraphId = "79A0F9EB", TextId = "7DF84B01" };

                            ParagraphProperties paragraphProperties18 = new ParagraphProperties();

                            ParagraphMarkRunProperties paragraphMarkRunProperties18 = new ParagraphMarkRunProperties();
                            RunFonts runFonts41 = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman", ComplexScript = "Times New Roman" };
                            FontSize fontSize41 = new FontSize() { Val = "28" };
                            FontSizeComplexScript fontSizeComplexScript41 = new FontSizeComplexScript() { Val = "28" };

                            paragraphMarkRunProperties18.Append(runFonts41);
                            paragraphMarkRunProperties18.Append(fontSize41);
                            paragraphMarkRunProperties18.Append(fontSizeComplexScript41);

                            paragraphProperties18.Append(paragraphMarkRunProperties18);

                            Run run24 = new Run();

                            RunProperties runProperties24 = new RunProperties();
                            RunFonts runFonts42 = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman", ComplexScript = "Times New Roman" };
                            FontSize fontSize42 = new FontSize() { Val = "28" };
                            FontSizeComplexScript fontSizeComplexScript42 = new FontSizeComplexScript() { Val = "28" };

                            runProperties24.Append(runFonts42);
                            runProperties24.Append(fontSize42);
                            runProperties24.Append(fontSizeComplexScript42);
                            Text text23 = new Text();
                            text23.Text = p.OrganisatonCollaborator.Applicant.DateOfBirth.FormatEx(string.Empty);

                            run24.Append(runProperties24);
                            run24.Append(text23);

                            paragraph18.Append(paragraphProperties18);
                            paragraph18.Append(run24);

                            tableCell15.Append(tableCellProperties15);
                            tableCell15.Append(paragraph18);

                            TableCell tableCell16 = new TableCell();

                            TableCellProperties tableCellProperties16 = new TableCellProperties();
                            TableCellWidth tableCellWidth16 = new TableCellWidth() { Width = "2329", Type = TableWidthUnitValues.Dxa };
                            TableCellVerticalAlignment tableCellVerticalAlignment16 = new TableCellVerticalAlignment() { Val = TableVerticalAlignmentValues.Center };

                            tableCellProperties16.Append(tableCellWidth16);
                            tableCellProperties16.Append(tableCellVerticalAlignment16);

                            Paragraph paragraph19 = new Paragraph() { RsidParagraphMarkRevision = "00FE30D3", RsidParagraphAddition = "00917B74", RsidParagraphProperties = "00821F51", RsidRunAdditionDefault = "005A7013", ParagraphId = "2FA0A47C", TextId = "137AC9D2" };

                            ParagraphProperties paragraphProperties19 = new ParagraphProperties();

                            ParagraphMarkRunProperties paragraphMarkRunProperties19 = new ParagraphMarkRunProperties();
                            RunFonts runFonts43 = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman", ComplexScript = "Times New Roman" };
                            FontSize fontSize43 = new FontSize() { Val = "28" };
                            FontSizeComplexScript fontSizeComplexScript43 = new FontSizeComplexScript() { Val = "28" };

                            paragraphMarkRunProperties19.Append(runFonts43);
                            paragraphMarkRunProperties19.Append(fontSize43);
                            paragraphMarkRunProperties19.Append(fontSizeComplexScript43);

                            paragraphProperties19.Append(paragraphMarkRunProperties19);

                            Run run25 = new Run();

                            RunProperties runProperties25 = new RunProperties();
                            RunFonts runFonts44 = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman", ComplexScript = "Times New Roman" };
                            FontSize fontSize44 = new FontSize() { Val = "28" };
                            FontSizeComplexScript fontSizeComplexScript44 = new FontSizeComplexScript() { Val = "28" };

                            runProperties25.Append(runFonts44);
                            runProperties25.Append(fontSize44);
                            runProperties25.Append(fontSizeComplexScript44);
                            Text text24 = new Text();
                            text24.Text = p.OrganisatonCollaborator.Applicant.Phone;

                            run25.Append(runProperties25);
                            run25.Append(text24);

                            paragraph19.Append(paragraphProperties19);
                            paragraph19.Append(run25);

                            tableCell16.Append(tableCellProperties16);
                            tableCell16.Append(paragraph19);

                            tableRow6.Append(tableCell13);
                            tableRow6.Append(tableCell14);
                            tableRow6.Append(tableCell15);
                            tableRow6.Append(tableCell16);

                            table2.Append(tableRow6);
                        }
                    }



                    Paragraph paragraph20 = new Paragraph() { RsidParagraphMarkRevision = "001C30EE", RsidParagraphAddition = "00917B74", RsidParagraphProperties = "00DD566E", RsidRunAdditionDefault = "00917B74", ParagraphId = "4EC3A709", TextId = "77777777" };

                    ParagraphProperties paragraphProperties20 = new ParagraphProperties();

                    ParagraphMarkRunProperties paragraphMarkRunProperties20 = new ParagraphMarkRunProperties();
                    RunFonts runFonts45 = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman", ComplexScript = "Times New Roman" };
                    FontSize fontSize45 = new FontSize() { Val = "24" };
                    FontSizeComplexScript fontSizeComplexScript45 = new FontSizeComplexScript() { Val = "24" };

                    paragraphMarkRunProperties20.Append(runFonts45);
                    paragraphMarkRunProperties20.Append(fontSize45);
                    paragraphMarkRunProperties20.Append(fontSizeComplexScript45);

                    paragraphProperties20.Append(paragraphMarkRunProperties20);

                    paragraph20.Append(paragraphProperties20);

                    SectionProperties sectionProperties1 = new SectionProperties() { RsidRPr = "001C30EE", RsidR = "00917B74", RsidSect = "000C0FCD" };
                    PageSize pageSize1 = new PageSize() { Width = (UInt32Value)11906U, Height = (UInt32Value)16838U };
                    PageMargin pageMargin1 = new PageMargin() { Top = 1134, Right = (UInt32Value)850U, Bottom = 1134, Left = (UInt32Value)1701U, Header = (UInt32Value)708U, Footer = (UInt32Value)708U, Gutter = (UInt32Value)0U };
                    Columns columns1 = new Columns() { Space = "708" };
                    DocGrid docGrid1 = new DocGrid() { LinePitch = 360 };

                    sectionProperties1.Append(pageSize1);
                    sectionProperties1.Append(pageMargin1);
                    sectionProperties1.Append(columns1);
                    sectionProperties1.Append(docGrid1);

                    body1.Append(paragraph1);
                    body1.Append(paragraph2);
                    body1.Append(table1);
                    body1.Append(paragraph10);
                    body1.Append(table2);
                    body1.Append(paragraph20);
                    body1.Append(sectionProperties1);

                    document1.Append(body1);

                    mainPart.Document = document1;
                }

                return new DocumentResult
                {
                    FileBody = ms.ToArray(),
                    FileName = "Список для ГИБДД.docx",
                    MimeType = mimeType,
                    MimeTypeShort = extention
                };
            }
        }
    }
}
