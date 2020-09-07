using Independentsoft.Office.Word;
using Independentsoft.Office.Word.Tables;

namespace Gisd.Sped.Progress
{

    public static class TableExtensions
    {
        public static Table AddRow(this Table source, params Row[] rows)
        {
            if (source == null)
                throw new System.ArgumentNullException(nameof(source));

            if (rows == null)
                return source;

            foreach (var row in rows)
                source.Add(row);

            return source;
        }
    }

    public static class CellExtensions
    {
        public static Cell MergeVertical(this Cell source)
        {
            source.VerticallyMergedCell = new VerticallyMergedCell(MergeCellType.Continue);
            return source;
        }

        public static Cell MergeHorizontal(this Cell source)
        {
            source.HorizontallyMergedCell = new HorizontallyMergedCell
            {
                Type = MergeCellType.Continue
            };
            return source;
        }

        public static Cell SetCell(this Cell source,
            StandardBorderStyle top = StandardBorderStyle.SingleLine,
            StandardBorderStyle bottom = StandardBorderStyle.SingleLine,
            StandardBorderStyle left = StandardBorderStyle.SingleLine,
            StandardBorderStyle right = StandardBorderStyle.SingleLine)
        {
            source.TopBorder = new TopBorder(top);
            source.BottomBorder = new BottomBorder(bottom);
            source.LeftBorder = new LeftBorder(left);
            source.RightBorder = new RightBorder(right);
            source.Width = new Width(TableWidthUnit.Auto, 25);
            return source;
        }
    }
}
