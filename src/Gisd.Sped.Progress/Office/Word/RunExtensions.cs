using System;
using Independentsoft.Office.Word;

namespace Gisd.Sped.Progress
{
    public static class RunExtensions
    {
        public static Run DefaultStylize(this Run source, string text, bool italics = false, bool bold = false, bool underline = false)
        {
            return source.Stylize(text, Defaults.FontSize, Defaults.FontFamily, italics, bold, underline);
        }

        public static Run Stylize(this Run source, string text, int fontSize = Defaults.FontSize, string font = Defaults.FontFamily, bool italics = false, bool bold = false, bool underline = false)
        {
            source.AddText(text);
            source.AsciiFont = font;
            source.FontSize = fontSize;

            if (bold)
            {
                source.Bold = Defaults.True;
            }

            if (underline)
            {
                source.Underline = Defaults.SingleUnderline;
            }

            if (italics)
            {
                source.Italic = Defaults.True;
            }

            return source;
        }

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
            source.FontSize = Defaults.FontSize;
            source.AsciiFont = Defaults.FontFamily;
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

            source.AppendText(text);
            source.Bold = Defaults.True;
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

            source.AppendText(text);
            source.Italic = Defaults.True;
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

            source.AppendText(text);
            source.Underline = Defaults.SingleUnderline;
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

            source.AppendText(text);
            source.Underline = Defaults.SingleUnderline;
            source.Bold = Defaults.True;
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

            source.AppendText(text);
            source.Italic = Defaults.True;
            source.Bold = Defaults.True;
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
}
