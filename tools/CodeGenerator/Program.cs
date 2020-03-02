using System;
using System.Text;
using Independentsoft.Office;
using Independentsoft.Office.Word;

namespace CodeGenerator
{
    public static class Program
    {
        private static StringBuilder Builder;
        private static ClipboardWriter Writer;
        public static void Main()
        {
            Before();

            WordDocument doc = new WordDocument();

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

            doc.Save("D:\\output.docx", true);


            WriteLine();
            Write("Press any key to continue...");
            After();
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
