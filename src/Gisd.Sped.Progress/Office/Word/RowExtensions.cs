using Independentsoft.Office.Word;
using Independentsoft.Office.Word.Tables;

namespace Gisd.Sped.Progress
{
    public static class RowExtensions
    {
        public static Row ProcessRow(this Row source, bool first = false, bool second = false, bool third = false, bool fourth = false)
        {


            for (int i = 0; i < 11; i++)
            {
                var cell = new Cell().SetCell();

                source.Add(cell);

                switch (i)
                {
                    case 0:
                        cell.Width = new Width(TableWidthUnit.Point, 12000);
                        cell.VerticallyMergedCell = new VerticallyMergedCell();

                        if (first)
                        {
                            cell.VerticallyMergedCell.Type = MergeCellType.Restart;
                        }

                        break;
                    default:
                        cell.Width = new Width(TableWidthUnit.Point, 2000);
                        if (first)
                        {
                            if (i > 0)
                            {
                                cell.TopBorder = InvisibleBorders.Top;
                                cell.LeftBorder = new LeftBorder(StandardBorderStyle.None);
                                cell.RightBorder = new RightBorder(StandardBorderStyle.None);
                                cell.VerticalAlignment = VerticalAlignmentType.Center;
                                if (i == 1)
                                {
                                    cell.HorizontallyMergedCell = new HorizontallyMergedCell
                                    {
                                        Type = MergeCellType.Restart
                                    };
                                    cell.Content.Add(
                                            WordFactory.Paragraph(
                                                WordFactory.Text("Objective Result: ______________")).Center());
                                }
                                else
                                {
                                    cell.HorizontallyMergedCell = new HorizontallyMergedCell
                                    {
                                        Type = MergeCellType.Continue
                                    };
                                }

                            }
                            else
                            {
                                cell.TopBorder = InvisibleBorders.Top;
                                cell.RightBorder = new RightBorder(StandardBorderStyle.None);
                            }
                            source.Height = new RowHeight(HeightRule.Exact, 1100);
                        }
                        if (second)
                        {
                            if (i > 0)
                            {
                                cell.Content.Add(new Paragraph()
                                    .WithRuns(
                                    new Run().Stylize("Trial " + i.ToString(), 20, Defaults.FontFamily))
                                    .SetHorizontalAlignment(HorizontalAlignmentType.Center)
                                    .SetVerticalAlignment(VerticalTextAlignment.Center)
                                    .SetSpacing(4, 0));
                                cell.Shading = new Shading(ShadingPattern.Percent20);
                                cell.VerticalAlignment = VerticalAlignmentType.Center;
                            }
                            source.Height = new RowHeight(HeightRule.Exact, 300);
                        }
                        if (third)
                        {
                            if (i > 0)
                            {
                                cell.Content.Add(new Paragraph().WithRuns(new Run().Stylize("Y", 20, Defaults.FontFamily))
                                    .SetHorizontalAlignment(HorizontalAlignmentType.Center)
                                    .SetVerticalAlignment(VerticalTextAlignment.Bottom));
                                //    .SetHorizontalAlignment(HorizontalAlignmentType.Center));
                                cell.BottomBorder = new BottomBorder(StandardBorderStyle.None);
                                cell.VerticalAlignment = VerticalAlignmentType.Center;
                                cell.TopMarginException = new Width(TableWidthUnit.Point, 50);
                            }
                            source.Height = new RowHeight(HeightRule.Exact, 500);
                        }
                        if (fourth)
                        {
                            if (i > 0)
                            {
                                cell.Content.Add(new Paragraph().WithRuns(new Run().Stylize("N", 20, Defaults.FontFamily))
                                    .SetHorizontalAlignment(HorizontalAlignmentType.Center)
                                    .SetVerticalAlignment(VerticalTextAlignment.Center));
                                cell.TopBorder = InvisibleBorders.Top;
                                cell.VerticalAlignment = VerticalAlignmentType.Center;
                                cell.TopMarginException = new Width(TableWidthUnit.Point, 50);
                            }
                            source.Height = new RowHeight(HeightRule.Exact, 500);
                        }
                        break;
                }
            }

            return source;
        }
    }
}
