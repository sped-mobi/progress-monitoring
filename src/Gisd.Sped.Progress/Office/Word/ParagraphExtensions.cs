using System;
using Independentsoft.Office.Word;

namespace Gisd.Sped.Progress
{
    public static class ParagraphExtensions
    {


        public static Paragraph SetSpacing(this Paragraph source, int before, int after)
        {
            source.Spacing = new Spacing
            {
                Before = before,
                After = after,
            };
            return source;
        }

        public static Paragraph Center(this Paragraph source)
        {
            source = source.CenterHorizontally();
            return source.CenterVertically();
        }

        public static Paragraph CenterHorizontally(this Paragraph source)
        {
            return source.SetHorizontalAlignment(HorizontalAlignmentType.Center);
        }

        public static Paragraph CenterVertically(this Paragraph source)
        {
            return source.SetVerticalAlignment(VerticalTextAlignment.Center);
        }


        public static Paragraph SetHorizontalAlignment(this Paragraph source, HorizontalAlignmentType alignment)
        {
            source.HorizontalTextAlignment = alignment;
            return source;
        }

        public static Paragraph SetVerticalAlignment(this Paragraph source, VerticalTextAlignment alignment)
        {
            source.VerticalTextAlignment = alignment;
            return source;
        }

        public static Paragraph WithRuns(this Paragraph source, params Run[] runs)
        {
            if (runs != null && runs.Length > 0)
                foreach (var run in runs)
                    source.Add(run);

            return source;
        }

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
}
