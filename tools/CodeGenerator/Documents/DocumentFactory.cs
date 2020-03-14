using System;
using System.IO;
using Gisd.Sped.Schema;
using Independentsoft.Office;
using Independentsoft.Office.Word;
using Independentsoft.Office.Word.Fields;
using Independentsoft.Office.Word.Sections;
using Independentsoft.Office.Word.Tables;

namespace CodeGenerator.Documents
{
    internal static class DocumentFactory
    {
        public static WordDocument CreateDocument(Student student, Goal goal, string teacherName, string gradingPeriod, Year year, string campus)
        {
            WordDocument doc = new WordDocument();

            doc = doc.AddTitle("Special Education");
            doc = doc.AddTitle("Data Collection Sheet");
            doc = doc.AddStudentInfo(student.Name, student.Id, student.Dob, student.Gender, student.Grade, campus, year, teacherName, gradingPeriod);
            doc = doc.AddGoal(goal);

            PageMargins margins = new PageMargins();
            margins.Bottom = 720;  // 1 inch
            margins.Left = 720; // 1 inch
            margins.Right = 720; //1440; // 1 inch
            margins.Top = 720; //1440; // 1 inch

            //PageSize pageSize = new PageSize(12240, 15840); //8.5 x 11 inch
            PageSize pageSize = new PageSize(15840, 12240); //8.5 x 11 inch
            pageSize.PageOrientation = PageOrientation.Landscape;

            Section section = new Section();
            section.PageSize = pageSize;
            section.PageMargins = margins;

            doc.Body.Section = section;



            //doc.Settings.DocumentProtection = new DocumentProtection();
            //doc.Settings.DocumentProtection.ProtectionType = DocumentProtectionType.Forms;
            return doc;
        }


        public static Row ProcessRow(this Row source, bool first = false, bool second = false, bool third = false, bool fourth = false)
        {
            source.Height = new RowHeight(HeightRule.Exact, 750);


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
                                cell.TopBorder = new TopBorder(StandardBorderStyle.None);
                                cell.LeftBorder = new LeftBorder(StandardBorderStyle.None);
                                cell.RightBorder = new RightBorder(StandardBorderStyle.None);

                            }
                            else
                            {
                                cell.TopBorder = new TopBorder(StandardBorderStyle.None);
                                cell.RightBorder = new RightBorder(StandardBorderStyle.None);
                            }
                        }
                        if (second)
                        {
                            if (i > 0)
                            {

                                cell.Content.Add(new Paragraph()
                                    .WithRuns(
                                    new Run().Stylize("Trial " + i.ToString(), 20, DefaultFont))
                                    .SetHorizontalAlignment(HorizontalAlignmentType.Center)
                                    .SetVerticalAlignment(VerticalTextAlignment.Center)
                                    .SetSpacing(4, 0));
                                cell.Shading = new Shading(ShadingPattern.Percent20);
                                cell.VerticalAlignment = VerticalAlignmentType.Center;


                            }

                        }
                        if (third)
                        {
                            if (i > 0)
                            {

                                cell.Content.Add(new Paragraph().InsertCheckbox(" Yes")
                                    .SetHorizontalAlignment(HorizontalAlignmentType.Center));
                                cell.BottomBorder = new BottomBorder(StandardBorderStyle.None);
                                cell.VerticalAlignment = VerticalAlignmentType.Center;


                            }

                        }
                        if (fourth)
                        {
                            if (i > 0)
                            {
                                cell.Content.Add(new Paragraph().InsertCheckbox(" No")
                                    .SetHorizontalAlignment(HorizontalAlignmentType.Center));
                                cell.TopBorder = new TopBorder(StandardBorderStyle.None);
                                cell.VerticalAlignment = VerticalAlignmentType.Center;

                            }

                        }
                        break;
                }
            }


            return source;
        }

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

        private const int DefaultFontSize = 24;
        private const string DefaultFont = "Arial";


        private static WordDocument AddGoal(this WordDocument source, Goal goal)
        {
            Paragraph p1 = new Paragraph();
            Paragraph p2 = new Paragraph();
            Run number = new Run().Stylize(goal.Number, DefaultFontSize, DefaultFont, false, true, true);
            Run colon = new Run().Stylize(":", DefaultFontSize, DefaultFont, false, true, false);
            Run focus = new Run().Stylize(goal.Focus, DefaultFontSize, DefaultFont, false, true, true);
            Run annualGoalLabel = new Run().Stylize("ANNUAL GOAL: ", DefaultFontSize, DefaultFont, false, true, false);
            Run annualGoal = new Run().Stylize(goal.AnnualGoal, DefaultFontSize, DefaultFont, italics: true, bold: true, underline: false);
            p1.Add(number);
            p1.Add(colon);
            p1.Add(focus);
            p2.Add(annualGoalLabel);
            p2.Add(annualGoal);
            source.Body.Add(p1);
            source.Body.Add(p2);

            foreach (var objective in goal.Objectives)
            {
                source = source.AddObjective(objective);
            }




            return source;
        }

        private static WordDocument AddObjective(this WordDocument source, Objective objective)
        {
            Table table = new Table(StandardBorderStyle.None);

            TableGrid grid = new TableGrid();
            grid.Columns.Add(new TableGridColumn(12000));
            grid.Columns.Add(new TableGridColumn(2000));
            grid.Columns.Add(new TableGridColumn(2000));
            grid.Columns.Add(new TableGridColumn(2000));
            grid.Columns.Add(new TableGridColumn(2000));
            grid.Columns.Add(new TableGridColumn(2000));
            grid.Columns.Add(new TableGridColumn(2000));
            grid.Columns.Add(new TableGridColumn(2000));
            grid.Columns.Add(new TableGridColumn(2000));
            grid.Columns.Add(new TableGridColumn(2000));
            grid.Columns.Add(new TableGridColumn(2000));
            table.Grid = grid;

            var r1 = new Row().ProcessRow(true);
            var r2 = new Row().ProcessRow(false, true);
            var r3 = new Row().ProcessRow(false, false, true);
            var r4 = new Row().ProcessRow(false, false, false, true);
            if (r1.Content[0] is Cell cell)
            {
                cell.Add(new Paragraph().WithRuns(new Run().Stylize(objective.Text, 20)));
                cell.LeftMarginException = new Width(TableWidthUnit.Point, 100);
                cell.TopMarginException = new Width(TableWidthUnit.Point, 100);
                cell.BottomMarginException = new Width(TableWidthUnit.Point, 100);
                cell.RightMarginException = new Width(TableWidthUnit.Point, 100);
            }


            table.Add(r1);
            table.Add(r2);
            table.Add(r3);
            table.Add(r4);



            source.Body.Add(table);

            source.AppendCarriageReturn();

            return source;
        }

        private static WordDocument AppendCarriageReturn(this WordDocument source)
        {
            source.Body.Add(new Paragraph().WithRuns(new Run().AppendCarriageReturn()));
            return source;
        }

        private static WordDocument AddStudentInfo(this WordDocument source, string name, string id, DateTimeOffset dob, Gender gender, string grade, string campus, Year schoolYear, string teacherName, string gradingPeriod)
        {
            DateTime zeroTime = new DateTime(1, 1, 1);
            var age = DateTime.Now - dob;
            int years = (zeroTime + age).Year - 1;

            string school_year = "";
            switch (schoolYear)
            {
                case Year.Sy20192020:
                    school_year = "2019-2020";
                    break;
                case Year.Sy20202021:
                    school_year = "2020-2021";
                    break;
            }

            source.Body.Add(new Paragraph()
                .WithRuns(new Run()
                                .Stylize($"Name: ", DefaultFontSize, DefaultFont),
                          new Run()

                                .Stylize(name, DefaultFontSize, DefaultFont, false, false, true).AppendTab(),
                          new Run()
                                .AppendTab()
                                .Stylize($" DOB: ", DefaultFontSize, DefaultFont),
                          new Run()
                                .Stylize(dob.ToString("MM/dd/yyyy"), DefaultFontSize, DefaultFont, false, false, true).AppendTab(),
                          new Run()
                                .AppendTab()
                                .Stylize($" ID#: ", DefaultFontSize, DefaultFont),
                          new Run()
                                .Stylize(id, DefaultFontSize, DefaultFont, false, false, true).AppendTab(),
                          new Run()
                                .AppendTab()
                                .Stylize($" Age: ", DefaultFontSize, DefaultFont),
                          new Run()
                                .Stylize(years.ToString(), DefaultFontSize, DefaultFont, false, false, true).AppendTab(),
                          new Run()
                                .AppendTab()
                                .Stylize($" Gender: ", DefaultFontSize, DefaultFont),
                          new Run()
                                .Stylize(gender.ToString(), DefaultFontSize, DefaultFont, false, false, true).AppendTab(),
                          new Run()
                                .AppendTab()
                                .Stylize($" Grade: ", DefaultFontSize, DefaultFont),
                          new Run()
                                .Stylize(grade, DefaultFontSize, DefaultFont, false, false, true).AppendTab().AppendCarriageReturn().AppendCarriageReturn(),
                          new Run()
                                .Stylize($"School Year: ", DefaultFontSize, DefaultFont),
                          new Run()
                                .Stylize(school_year, DefaultFontSize, DefaultFont, false, false, true).AppendTab(),
                          new Run()
                                .AppendTab()
                                .Stylize($" Campus: ", DefaultFontSize, DefaultFont),
                          new Run()
                                .Stylize(campus, DefaultFontSize, DefaultFont, false, false, true).AppendTab().AppendCarriageReturn().AppendCarriageReturn(),
                          new Run()
                                .Stylize($"Teacher Name: ", DefaultFontSize, DefaultFont),
                          new Run()
                                .Stylize(teacherName, DefaultFontSize, DefaultFont, false, false, true).AppendTab(),
                          new Run()
                                .AppendTab()
                                .Stylize($" Grading Period: ", DefaultFontSize, DefaultFont),
                          new Run()
                                .Stylize(gradingPeriod, DefaultFontSize, DefaultFont, false, false, true).AppendTab(),
                          new Run()
                                .AppendTab()
                                .Stylize($" Documentation Date: ", DefaultFontSize, DefaultFont),
                          new Run()
                                .Stylize("            ", DefaultFontSize, DefaultFont, false, false, true).AppendTab().AppendCarriageReturn()
                          ).SetHorizontalAlignment(HorizontalAlignmentType.Left));
            return source;

        }

        private static WordDocument AddTitle(this WordDocument source, string title)
        {

            source.Body.Add(
                new Paragraph()
                    .WithRuns(new Run()
                                .Stylize(title, DefaultFontSize + 4, DefaultFont, false, true, false)
                                )
                    .SetHorizontalAlignment(HorizontalAlignmentType.Center));
            return source;
        }

        private static Paragraph SetVerticalAlignment(this Paragraph source, VerticalTextAlignment alignment)
        {

            source.VerticalTextAlignment = alignment;
            return source;
        }

        private static Paragraph SetSpacing(this Paragraph source, int before, int after)
        {

            source.Spacing = new Spacing
            {
                Before = before,
                After = after,
            };
            return source;
        }


        private static Paragraph SetHorizontalAlignment(this Paragraph source, HorizontalAlignmentType alignment)
        {
            source.HorizontalTextAlignment = alignment;
            return source;
        }

        private static Paragraph WithRuns(this Paragraph source, params Run[] runs)
        {
            if (runs != null && runs.Length > 0)
                foreach (var run in runs)
                    source.Add(run);

            return source;
        }

        private static Run AppendCarriageReturn(this Run source)
        {
            source.AddCarriageReturn();
            return source;
        }


        private static Paragraph InsertCheckbox(this Paragraph source, string label)
        {
            ComplexField startField = new ComplexField();
            startField.CharacterType = ComplexFieldCharacterType.Start;
            startField.FormFieldProperties.Enabled = ExtendedBoolean.True;
            startField.FormFieldProperties.CheckBoxFormFieldProperties.AutoSize = ExtendedBoolean.True;
            startField.FormFieldProperties.CheckBoxFormFieldProperties.Checked = ExtendedBoolean.False;
            var startRun = new Run();
            startRun.Add(startField);

            FormCheckBox checkBox = new FormCheckBox();
            FieldCode fieldCode = new FieldCode(checkBox);
            var fieldCodeRun = new Run();
            fieldCodeRun.Add(fieldCode);

            ComplexField separatorField = new ComplexField();
            separatorField.CharacterType = ComplexFieldCharacterType.Separator;
            var separatorRun = new Run();
            separatorRun.Add(separatorField);

            ComplexField endField = new ComplexField();
            endField.CharacterType = ComplexFieldCharacterType.End;
            var endRun = new Run();
            endRun.Add(endField);

            source.Add(startRun);
            source.Add(fieldCodeRun);
            source.Add(separatorRun);
            source.Add(endRun);

            source.Add(new Run().Stylize(label, 20));

            source = source.SetSpacing(0, 0);


            return source;
        }

        private static Run AppendTab(this Run source)
        {
            source.AddTab();
            return source;
        }

        private static Run Stylize(this Run source, string text, int fontSize = DefaultFontSize, string font = DefaultFont, bool italics = false, bool bold = false, bool underline = false)
        {
            source.AddText(text);
            source.AsciiFont = font;
            source.FontSize = fontSize;

            if (bold)
            {
                source.Bold = ExtendedBoolean.True;
            }

            if (underline)
            {
                source.Underline = new Underline(UnderlinePattern.Single);
            }

            if (italics)
            {
                source.Italic = ExtendedBoolean.True;
            }

            return source;
        }


        public static void CreateDocuments(string jsonFilePath, string targetDirectory, string evaluator, string gradingPeriod)
        {
            var collection = GetDataCollection(jsonFilePath);

            foreach (var student in collection.Students)
            {
                foreach (var goal in student.Goals)
                {
                    var document = CreateDocument(student, goal, evaluator, gradingPeriod, collection.Year, collection.Campus);

                    var fileName = "2019-2020_lchs_ale_marshall_gp5_" + student.Name.Replace(" ", "_").ToLower() + "_goal_" + goal.Number + "_data_sheet.docx";

                    var newPath = Path.Combine(targetDirectory, fileName);

                    document.Save(newPath, true);
                }


            }
        }


        private static DataCollection GetDataCollection(string jsonFilePath)
        {
            return DataCollection.FromJson(File.ReadAllText(jsonFilePath));
        }
    }
}
