using System;
using System.Text;
using System.Threading;
using CodeGenerator.Documents;
using Independentsoft.Office;
using Independentsoft.Office.Word;
using Independentsoft.Office.Word.Tables;

namespace CodeGenerator
{
    public static class Program
    {
        private const string _filePath = @"D:\sandbox\vs\progress-monitoring\src\Gisd.Sped.Progress\Schema\progress.json";

        private static StringBuilder Builder;
        private static ClipboardWriter Writer;
        public static void Main()
        {
            Before();

            TestDocumentFactory();

            Console.WriteLine();
            Console.WriteLine("Complete!");
            Thread.Sleep(1000);
            //Write("Press any key to continue...");
            //After();
        }

        private static void TestDocumentFactory()
        {
            DocumentFactory.CreateDocuments(_filePath, "d:\\temp\\", "Brad Marshall", "Grading Period 5");
        }

        private static void TestVertical()
        {
            WordDocument doc = new WordDocument();

            TableGrid tableGrid = new TableGrid();
            tableGrid.Columns.Add(new TableGridColumn(498));
            tableGrid.Columns.Add(new TableGridColumn(2392));
            tableGrid.Columns.Add(new TableGridColumn(3600));
            tableGrid.Columns.Add(new TableGridColumn(1800));
            tableGrid.Columns.Add(new TableGridColumn(1186));

            Cell cell11 = new Cell();
            cell11.VerticallyMergedCell = new VerticallyMergedCell();
            cell11.VerticallyMergedCell.Type = MergeCellType.Restart;
            cell11.Width = new Width(TableWidthUnit.Point, 498);

            Cell cell12 = new Cell();
            cell12.Width = new Width(TableWidthUnit.Point, 2392);

            Cell cell13 = new Cell();
            cell13.Width = new Width(TableWidthUnit.Point, 3600);

            Cell cell14 = new Cell();
            cell14.Width = new Width(TableWidthUnit.Point, 1800);

            Cell cell15 = new Cell();
            cell15.Width = new Width(TableWidthUnit.Point, 1186);

            Row row1 = new Row();
            row1.Add(cell11);
            row1.Add(cell12);
            row1.Add(cell13);
            row1.Add(cell14);
            row1.Add(cell15);

            Cell cell21 = new Cell();
            cell21.VerticallyMergedCell = new VerticallyMergedCell();
            cell21.Width = new Width(TableWidthUnit.Point, 498);

            Cell cell22 = new Cell();
            cell22.GridSpan = 4;
            cell22.Width = new Width(TableWidthUnit.Point, 8978);

            Row row2 = new Row();
            row2.Add(cell21);
            row2.Add(cell22);

            Row row3 = new Row();
            var cell1 = new Cell();
            cell1.VerticallyMergedCell = new VerticallyMergedCell();
            cell1.Width = new Width(TableWidthUnit.Point, 498);

            row3.Add(cell1);
            row3.Add(new Cell());

            Table table1 = new Table(StandardBorderStyle.SingleLine);
            table1.Grid = tableGrid;
            table1.Add(row1);
            table1.Add(row2);
            table1.Add(row3);

            doc.Body.Add(table1);

            doc.Save("D:\\output.docx", true);
        }

        private static void ManualCreate()
        {
            var doc = new WordDocument();

            Run number = new Run();
            number = number.Stylize("1", 20, "Arial", false, true, true);

            Run colon = new Run();
            colon.Stylize(": ", 20, "Arial", false, true, false);

            Run focus = new Run();
            focus = focus.Stylize("Applied Marketable Skills" + Environment.NewLine, 20, "Arial", false, true, true);



            Paragraph p1 = new Paragraph();
            p1.Add(number);
            p1.Add(colon);
            p1.Add(focus);



            doc.Body.Add(p1);




            Paragraph p2 = new Paragraph();
            Run annualGoal = new Run();
            annualGoal = annualGoal.Stylize("ANNUAL GOAL: ", 20, "Arial", false, true, false);
            p2.Add(annualGoal);

            Run goalText = new Run();
            goalText = goalText.Stylize("From ARD year to the end of the 2019-2020 Academic year, when given verbal direct prompts, and verbal reminders to remain on tasks with open hands, Uriel will demonstrate attentiveness to self help activities independently with 70% accuracy.", 20, "Arial", true, true);

            p2.Add(goalText);

            doc.Body.Add(p2);

            Table table = new Table();


            var r1 = new Row().ProcessRow(true);
            var r2 = new Row().ProcessRow();
            var r3 = new Row().ProcessRow();
            var r4 = new Row().ProcessRow();
            var r5 = new Row().ProcessRow();

            table.Add(r1);
            table.Add(r2);
            table.Add(r3);
            table.Add(r4);
            table.Add(r5);





            doc.Body.Add(table);

            doc.Save("D:\\output.docx", true);
        }

        private static Run Stylize(this Run source, string text, int fontSize = 20, string font = "Arial", bool italics = false, bool bold = false, bool underline = false)
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

        private static void After()
        {
            Writer.Clip();
            Console.WriteLine(Builder);
            Console.ReadKey();
        }

        private static void Before()
        {
            Builder = new StringBuilder();
            Writer = ClipboardWriter.CreateForStringBuilder(Builder);
        }

        private static void Write(string value)
        {
            Writer.Write(value);
        }

        private static void WriteLine(string value)
        {
            Writer.WriteLine(value);
        }

        private static void WriteLine()
        {
            Writer.WriteLine();
        }

        private static IDisposable OpenBlock()
        {
            return Writer.OpenBlock();
        }
    }
}
