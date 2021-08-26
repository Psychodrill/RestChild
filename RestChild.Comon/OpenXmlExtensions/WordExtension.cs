using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Wordprocessing;

namespace RestChild.Comon.OpenXmlExtensions
{
    /// <summary>
    ///     Расширения свойств Word
    /// </summary>
    public static class WordExtension
    {
        /// <summary>
        ///     Выровнять по центру
        /// </summary>
        public static ParagraphProperties CenterAlign(this ParagraphProperties value)
        {
            var alignment = new Justification {Val = JustificationValues.Center};
            value.AppendChild(alignment);
            return value;
        }

        /// <summary>
        ///     Убрать нижний отступ
        /// </summary>
        public static ParagraphProperties NoSpacing(this ParagraphProperties value)
        {
            var spacingBetweenLines = new SpacingBetweenLines {After = "0"};
            value.AppendChild(spacingBetweenLines);
            return value;
        }

        /// <summary>
        ///     Задать ширину таблицы
        /// </summary>
        public static TableCellProperties Width(this TableCellProperties value, string width)
        {
            var w = new TableCellWidth {Type = TableWidthUnitValues.Pct, Width = new StringValue(width)};
            value.AppendChild(w);
            return value;
        }

        /// <summary>
        ///     Задать выравнивание по вертикали
        /// </summary>
        public static TableCellProperties CenterVAlign(this TableCellProperties value)
        {
            var alignment = new TableCellVerticalAlignment {Val = TableVerticalAlignmentValues.Center};
            value.AppendChild(alignment);
            return value;
        }

        /// <summary>
        ///     Задать выравнивание по правому краю
        /// </summary>
        public static ParagraphProperties RightAlign(this ParagraphProperties value)
        {
            var alignment = new Justification {Val = JustificationValues.Right};
            value.AppendChild(alignment);
            return value;
        }

        /// <summary>
        ///     Задать выравнивание по левому краю
        /// </summary>
        public static ParagraphProperties LeftAlign(this ParagraphProperties value)
        {
            var alignment = new Justification {Val = JustificationValues.Left};
            value.AppendChild(alignment);
            return value;
        }

        /// <summary>
        ///     Задать шрифт
        /// </summary>
        public static RunProperties SetFont(this RunProperties value, string fontName = "Times New Roman")
        {
            var runFont = new RunFonts {Ascii = fontName, HighAnsi = fontName, ComplexScript = fontName};
            value.AppendChild(runFont);
            return value;
        }

        /// <summary>
        ///     Задать размер шрифта
        /// </summary>
        public static RunProperties SetFontSize(this RunProperties value, string size = "22")
        {
            var sizeItem = new FontSize {Val = new StringValue(size)};
            value.AppendChild(sizeItem);
            return value;
        }

        /// <summary>
        ///     Задать шрифт степени
        /// </summary>
        public static RunProperties SetFontSizeSupperscript(this RunProperties value, string size = "22")
        {
            var sizeItem = new FontSizeComplexScript {Val = new StringValue(size)};
            var vAlignment = new VerticalTextAlignment {Val = VerticalPositionValues.Superscript};
            value.AppendChild(sizeItem);
            value.AppendChild(vAlignment);
            return value;
        }

        /// <summary>
        ///     Задать жирный шрифт
        /// </summary>
        public static RunProperties Bold(this RunProperties value)
        {
            var bold = new Bold {Val = OnOffValue.FromBoolean(true)};
            value.AppendChild(bold);
            return value;
        }

        /// <summary>
        ///     Задать подчеркнутый шрифт
        /// </summary>
        public static RunProperties Underline(this RunProperties value)
        {
            var bold = new Underline {Val = UnderlineValues.Single};
            value.AppendChild(bold);
            return value;
        }

        /// <summary>
        ///     Задать верхнюю границу
        /// </summary>
        public static TableCellBorders TopBorder(this TableCellBorders borders, string color = "000000", uint size = 1,
            BorderValues format = BorderValues.Single, uint space = 0)
        {
            borders.TopBorder = new TopBorder
            {
                Space = new UInt32Value(space),
                Color = color,
                Size = new UInt32Value(size),
                Val = format
            };

            return borders;
        }

        /// <summary>
        ///     Задать все границы
        /// </summary>
        public static TableCellBorders AllBorder(this TableCellBorders borders, string color = "000000", uint size = 1,
            BorderValues format = BorderValues.Single, uint space = 0)
        {
            return
                borders.TopBorder(color, size, format, space)
                    .RightBorder(color, size, format, space)
                    .BottomBorder(color, size, format, space)
                    .LeftBorder(color, size, format, space);
        }

        /// <summary>
        ///     Задать правую границу
        /// </summary>
        public static TableCellBorders RightBorder(this TableCellBorders borders, string color = "000000",
            uint size = 1,
            BorderValues format = BorderValues.Single, uint space = 0)
        {
            borders.RightBorder = new RightBorder
            {
                Space = new UInt32Value(space),
                Color = color,
                Size = new UInt32Value(size),
                Val = format
            };

            return borders;
        }

        /// <summary>
        ///     Задать нижнюю границу
        /// </summary>
        public static ParagraphBorders BottomBorder(this ParagraphBorders borders, string color = "000000",
            uint size = 1,
            BorderValues format = BorderValues.Single, uint space = 0)
        {
            borders.BottomBorder = new BottomBorder
            {
                Space = new UInt32Value(space),
                Color = color,
                Size = new UInt32Value(size),
                Val = format
            };

            return borders;
        }

        /// <summary>
        ///     Задать нижнюю границу
        /// </summary>
        public static TableCellBorders BottomBorder(this TableCellBorders borders, string color = "000000",
            uint size = 1,
            BorderValues format = BorderValues.Single, uint space = 0)
        {
            borders.BottomBorder = new BottomBorder
            {
                Space = new UInt32Value(space),
                Color = color,
                Size = new UInt32Value(size),
                Val = format
            };

            return borders;
        }

        /// <summary>
        ///     Задать левую границу
        /// </summary>
        public static TableCellBorders LeftBorder(this TableCellBorders borders, string color = "000000", uint size = 1,
            BorderValues format = BorderValues.Single, uint space = 0)
        {
            borders.LeftBorder = new LeftBorder
            {
                Space = new UInt32Value(space),
                Color = color,
                Size = new UInt32Value(size),
                Val = format
            };

            return borders;
        }

        /// <summary>
        ///     Задать границы
        /// </summary>
        public static TableCellProperties Borders(this TableCellProperties properties, TableCellBorders borders)
        {
            if (borders != null)
            {
                properties.AppendChild(borders);
            }

            return properties;
        }

        /// <summary>
        ///     Задать текст
        /// </summary>
        public static Paragraph Text(this Paragraph par, string text, ParagraphProperties p = null,
            RunProperties r = null)
        {
            if (par == null)
            {
                return null;
            }

            var run = new Run();
            if (r != null)
            {
                run.AppendChild(r);
            }

            run.AppendChild(new Text(text));

            if (p != null)
            {
                par.AppendChild(p);
            }

            par.AppendChild(run);

            return par;
        }

        /// <summary>
        ///     Задать текст
        /// </summary>
        public static TableCell Text(this TableCell cell, string text, TableCellProperties tcp,
            ParagraphProperties p = null, RunProperties r = null)
        {
            if (cell == null)
            {
                return null;
            }

            if (tcp != null)
            {
                cell.AppendChild(tcp);
            }

            cell.AppendChild(new Paragraph().Text(text, p, r));
            return cell;
        }

        /// <summary>
        ///     Задать текст
        /// </summary>
        public static TableCell Text(this TableRow row, string text, TableCellProperties tcp,
            ParagraphProperties p = null, RunProperties r = null)
        {
            var cell = new TableCell().Text(text, tcp, p, r);

            row?.AppendChild(cell);

            return cell;
        }
    }
}
