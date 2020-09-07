using System;
using Independentsoft.Office.Word;
using Independentsoft.Office.Word.Tables;

namespace Gisd.Sped.Progress
{
    public static class WordDocumentExtensions
    {
        public static WordDocument ProcessDocument(this WordDocument doc, Student student, AnnualGoal goal, string teacherName, string gradingPeriod, SchoolYearType year, string campus, bool last)
        {
            doc = doc.AddTitle("Special Education");
            doc = doc.AddTitle("Data Collection Sheet");
            doc = doc.AddStudentInfo(student.GetFullName(), student.LocalID, student.DateOfBirth, student.Gender, student.Grade, campus, year, teacherName, gradingPeriod);
            doc = doc.AddGoal(goal, true, last);
            return doc;
        }


        public static WordDocument AddParagraph(this WordDocument source, params Paragraph[] paragraphs)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (paragraphs != null)
            {
                foreach (var paragraph in paragraphs)
                {
                    source.Body.Add(paragraph);
                }
            }

            return source;
        }


        public static WordDocument AddGoal(this WordDocument source, AnnualGoal goal, bool singleDocument = false, bool last = false)
        {
            //Paragraph p1 = new Paragraph();
            //Paragraph p2 = new Paragraph();
            //Run number = new Run().Stylize(goal.Code, Defaults.FontSize, Defaults.FontFamily, false, true, true);
            //Run colon = new Run().Stylize(":", Defaults.FontSize, Defaults.FontFamily, false, true, false);
            //Run focus = new Run().Stylize(goal.Focus.ToString(), Defaults.FontSize, Defaults.FontFamily, false, true, true);
            //Run annualGoalLabel = new Run().Stylize("ANNUAL GOAL: ", Defaults.FontSize, Defaults.FontFamily, false, true, false);
            //Run annualGoal = new Run().Stylize(goal.ToStatement(), Defaults.FontSize, Defaults.FontFamily, italics: true, bold: true, underline: false);
            //p1.Add(number);
            //p1.Add(colon);
            //p1.Add(focus);
            //p2.Add(annualGoalLabel);
            //p2.Add(annualGoal);
            //source.Body.Add(p1);
            //source.Body.Add(p2);

            source = source.AddParagraph(
                WordFactory.Paragraph(
                    WordFactory.BoldText(goal.Code + ":"),
                    WordFactory.BoldUnderlineText(goal.GetFocus())),
                WordFactory.Paragraph(
                    WordFactory.BoldText("ANNUAL GOAL: "),
                    WordFactory.BoldItalicText(goal.ToStatement())));



            foreach (var objective in goal.Objectives.Objective)
            {
                source = source.AddObjective(objective);
            }

            if (singleDocument)
            {
                if (!last)
                {
                    source = source.AppendPageBreak();
                }
            }


            return source;
        }


        public static WordDocument AppendPageBreak(this WordDocument source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            source.Body.Add(WordFactory.Paragraph(WordFactory.PageBreak()));
            return source;
        }

        public static WordDocument AddObjective(this WordDocument source, Objective objective)
        {
            Table table = new Table(StandardBorderStyle.None);

            TableGrid grid = new TableGrid();
            grid.Columns.Add(new TableGridColumn(Defaults.LargeCellWidth));
            grid.Columns.Add(new TableGridColumn(Defaults.SmallCellWidth));
            grid.Columns.Add(new TableGridColumn(Defaults.SmallCellWidth));
            grid.Columns.Add(new TableGridColumn(Defaults.SmallCellWidth));
            grid.Columns.Add(new TableGridColumn(Defaults.SmallCellWidth));
            grid.Columns.Add(new TableGridColumn(Defaults.SmallCellWidth));
            grid.Columns.Add(new TableGridColumn(Defaults.SmallCellWidth));
            grid.Columns.Add(new TableGridColumn(Defaults.SmallCellWidth));
            grid.Columns.Add(new TableGridColumn(Defaults.SmallCellWidth));
            grid.Columns.Add(new TableGridColumn(Defaults.SmallCellWidth));
            grid.Columns.Add(new TableGridColumn(Defaults.SmallCellWidth));
            table.Grid = grid;

            var r1 = new Row().ProcessRow(true);
            var r2 = new Row().ProcessRow(false, true);
            var r3 = new Row().ProcessRow(false, false, true);
            var r4 = new Row().ProcessRow(false, false, false, true);
            //r2.Height = new RowHeight(HeightRule.Exact, 300);
            //r3.Height = new RowHeight(HeightRule.Exact, 400);
            //r4.Height = new RowHeight(HeightRule.Exact, 400);

            if (r1.Content[0] is Cell cell)
            {
                cell.Add(WordFactory.Paragraph(WordFactory.Text(objective.ToStatement(), 20)));
                cell.LeftMarginException = new Width(TableWidthUnit.Point, 100);
                cell.TopMarginException = new Width(TableWidthUnit.Point, 100);
                cell.BottomMarginException = new Width(TableWidthUnit.Point, 100);
                cell.RightMarginException = new Width(TableWidthUnit.Point, 100);
            }

            table = table.AddRow(r1, r2, r3, r4);

            source.Body.Add(table);

            source.Body.Add(WordFactory.Paragraph());

            return source;
        }

        public static WordDocument AppendCarriageReturn(this WordDocument source)
        {
            source.Body.Add(new Paragraph().WithRuns(new Run().AppendCarriageReturn()));
            return source;
        }

        public static WordDocument AddStudentInfo(this WordDocument source, string name, string id, string dob, GenderType gender, string grade, string campus, SchoolYearType schoolYear, string teacherName, string gradingPeriod)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            if (id == null)
                throw new ArgumentNullException(nameof(id));

            if (dob == null)
                throw new ArgumentNullException(nameof(dob));

            if (grade == null)
                throw new ArgumentNullException(nameof(grade));

            if (campus == null)
                throw new ArgumentNullException(nameof(campus));

            if (teacherName == null)
                throw new ArgumentNullException(nameof(teacherName));

            if (gradingPeriod == null)
                throw new ArgumentNullException(nameof(gradingPeriod));

            DateTime zeroTime = new DateTime(1, 1, 1);
            var age = DateTime.Now - DateTime.Parse(dob);
            int years = (zeroTime + age).Year - 1;

            string school_year;
            switch (schoolYear)
            {
                case SchoolYearType._2019_2020:
                    school_year = "2019-2020";
                    break;
                default:
                    school_year = "2020-2021";
                    break;
            }

            return source.AddParagraph(WordFactory.Paragraph(
                HorizontalAlignmentType.Left,
                          WordFactory.Text("Name: "),
                          WordFactory.UnderlineText(name).AppendTab(),
                          WordFactory.Text("\t DOB: "),
                          WordFactory.UnderlineText(dob).AppendTab(),
                          WordFactory.Text("\t ID#: "),
                          WordFactory.UnderlineText(id).AppendTab(),
                          WordFactory.Text("\t Age: "),
                          WordFactory.UnderlineText(years.ToString()).AppendTab(),
                          WordFactory.Text("\t Gender: "),
                          WordFactory.UnderlineText(gender.ToString()).AppendTab(),
                          WordFactory.Text("\t Grade: "),
                          WordFactory.UnderlineText(grade).AppendTab().AppendCarriageReturn().AppendCarriageReturn(),
                          WordFactory.Text("School Year: "),
                          WordFactory.UnderlineText(school_year).AppendTab(),
                          WordFactory.Text("\t Campus: "),
                          WordFactory.UnderlineText(campus).AppendTab(),
                          WordFactory.Text("\t Reviewer: "),
                          WordFactory.UnderlineText(teacherName).AppendTab().AppendCarriageReturn().AppendCarriageReturn(),
                          WordFactory.Text("Grading Period: "),
                          WordFactory.UnderlineText(gradingPeriod).AppendTab(),
                          WordFactory.Text("\t Documentation Date: "),
                          WordFactory.UnderlineText("                   ").AppendTab(),
                          WordFactory.Text("\t Goal Result: "),
                          WordFactory.UnderlineText("                   ").AppendTab().AppendCarriageReturn()
                          ));
        }

        public static WordDocument AddTitle(this WordDocument source, string title)
        {
            source.Body.Add(
                WordFactory.Paragraph(WordFactory.BoldText(title, Defaults.FontSize + 4, Defaults.FontFamily))
                    .SetHorizontalAlignment(HorizontalAlignmentType.Center));
            return source;
        }
    }
}
