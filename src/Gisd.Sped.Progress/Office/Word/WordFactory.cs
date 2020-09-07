using Independentsoft.Office.Word;
using Independentsoft.Office.Word.Sections;
using Independentsoft.Office.Word.Tables;

namespace Gisd.Sped.Progress
{
    public static class WordFactory
    {
        public static WordDocument Document()
        {
            var doc = new WordDocument();
            PageMargins margins = new PageMargins();
            margins.Bottom = 360;  // 1 inch
            margins.Left = 360; // 1 inch
            margins.Right = 360; //1440; // 1 inch
            margins.Top = 360; //1440; // 1 inch

            //PageSize pageSize = new PageSize(12240, 15840); //8.5 x 11 inch
            PageSize pageSize = new PageSize(15840, 12240); //8.5 x 11 inch
            pageSize.PageOrientation = PageOrientation.Landscape;

            Section section = new Section();
            section.PageSize = pageSize;
            section.PageMargins = Defaults.PageMargins;

            doc.Body.Section = section;
            return doc;
        }


        public static Paragraph Paragraph(VerticalTextAlignment verticalAlignment, params Run[] runs)
        {
            var paragraph = Paragraph(runs);
            paragraph.VerticalTextAlignment = verticalAlignment;
            return paragraph;
        }

        public static Paragraph Paragraph(HorizontalAlignmentType horizontalAlignment, params Run[] runs)
        {
            var paragraph = Paragraph(runs);
            paragraph.HorizontalTextAlignment = horizontalAlignment;
            return paragraph;
        }

        public static Paragraph Paragraph(params Run[] runs)
        {
            Paragraph paragraph = new Paragraph();
            if (runs?.Length > 0)
            {
                paragraph.AddRuns(runs);
            }
            return paragraph;
        }

        public static Run BoldText(string text)
        {
            var run = Text();
            return run.AppendBoldText(text);
        }

        public static Run BoldText(string text, int fontSize, string fontFamily)
        {
            var run = Text();
            run = run.AppendBoldText(text);
            run.AsciiFont = fontFamily;
            run.FontSize = fontSize;
            return run;
        }

        public static Run BoldItalicText(string text)
        {
            var run = Text();
            return run.AppendBoldItalicText(text);
        }

        public static Run BoldItalicText(string text, string fontFamily)
        {
            var run = Text();
            run = run.AppendBoldItalicText(text);
            run.AsciiFont = fontFamily;
            return run;
        }

        public static Run BoldUnderlineText(string text)
        {
            var run = Text();
            return run.AppendBoldUnderlineText(text);
        }

        public static Run UnderlineText(string text)
        {
            var run = Text();
            return run.AppendUnderlineText(text);
        }

        public static Run Tab()
        {
            var run = Text();
            run.AddTab();
            return run;
        }



        public static Run PageBreak()
        {
            var run = Text();
            run.Add(new Break(BreakType.Page));
            return run;
        }

        public static Run Text()
        {
            return new Run
            {
                FontSize = Defaults.FontSize,
                AsciiFont = Defaults.FontFamily,
            };
        }

        public static Run Text(string text)
        {
            var run = new Run
            {
                FontSize = Defaults.FontSize,
                AsciiFont = Defaults.FontFamily,
            };

            run.AddText(text);

            return run;
        }

        public static Run Text(string text, int fontSize)
        {
            var run = Text(text);
            run.FontSize = fontSize;
            return run;
        }

        public static Run Text(string text, int fontSize, string fontFamily)
        {
            var run = Text(text, fontSize);
            run.AsciiFont = fontFamily;
            return run;
        }


        public static Row Row(params Cell[] cells)
        {
            var row = new Row();

            if (cells != null)
            {
                foreach (var cell in cells)
                {
                    row.Add(cell);
                }
            }

            return row;
        }
    }
}
