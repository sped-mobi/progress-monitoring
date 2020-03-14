using System;
using Independentsoft.Office.Word;

namespace Gisd.Sped.Office.Word
{

    public static class WordDocumentExtensions
    {

    }


    public static class TableExtensions
    {

    }


    public static class RowExtensions
    {

    }


    public static class CellExtensions
    {

    }

    public static class ParagraphExtensions
    {

        public static Paragraph Align(this Paragraph source, HorizontalAlignmentType horizontal, VerticalTextAlignment vertical)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            source.VerticalTextAlignment = vertical;
            source.HorizontalTextAlignment = horizontal;
            return source;
        }

        public static Paragraph AddRuns(this Paragraph paragraph, params Run[] runs)
        {
            if (paragraph is null)
            {
                throw new ArgumentNullException(nameof(paragraph));
            }

            foreach (var run in runs)
                paragraph.Add(run);
            return paragraph;
        }
    }

    public static class RunExtensions
    {
        public static Run AppendText(this Run source, string text)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentException("message", nameof(text));
            }

            source.AddText(text);
            return source;
        }

        public static Run AppendBoldText(this Run source, string text)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentException("message", nameof(text));
            }

            source.AddText(text);
            source.Bold = DefaultValues.True;
            return source;
        }

        public static Run AppendItalicText(this Run source, string text)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentException("message", nameof(text));
            }

            source.AddText(text);
            source.Italic = DefaultValues.True;
            return source;
        }

        public static Run AppendUnderlineText(this Run source, string text)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentException("message", nameof(text));
            }

            source.AddText(text);
            source.Underline = DefaultValues.SingleUnderline;
            return source;
        }

        public static Run AppendBoldUnderlineText(this Run source, string text)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentException("message", nameof(text));
            }

            source.AddText(text);
            source.Underline = DefaultValues.SingleUnderline;
            source.Bold = DefaultValues.True;
            return source;
        }

        public static Run AppendBoldItalicText(this Run source, string text)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentException("message", nameof(text));
            }

            source.AddText(text);
            source.Italic = DefaultValues.True;
            source.Bold = DefaultValues.True;
            return source;
        }

        public static Run AppendTab(this Run source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            source.AddTab();
            return source;
        }

        public static Run AppendCarriageReturn(this Run source, int iterations = 1)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            for (int i = 0; i < iterations; i++)
            {
                source.AddCarriageReturn();
            }

            return source;
        }
    }


    public static class WordFactory
    {
        public static Paragraph Paragraph(VerticalTextAlignment verticalAlignment, params Run[] runs)
        {
            var paragraph = Paragraph(runs);
            paragraph.VerticalTextAlignment = verticalAlignment;
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
            var run = Run();
            run = run.AppendBoldText(text);
            return run;
        }

        public static Run BoldUnderlineText(string text)
        {
            var run = Run();
            run = run.AppendBoldUnderlineText(text);
            return run;
        }

        public static Run Run()
        {
            var run = new Run
            {
                FontSize = DefaultValues.FontSize,
                AsciiFont = DefaultValues.FontFamily,
            };
            return run;
        }
    }
}
