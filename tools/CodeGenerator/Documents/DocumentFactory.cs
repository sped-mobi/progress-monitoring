using System;
using System.IO;
using Gisd.Sped.Schema;
using Independentsoft.Office;
using Independentsoft.Office.Word;

namespace CodeGenerator.Documents
{
    internal static class DocumentFactory
    {
        public static WordDocument CreateDocument(Student student)
        {
            WordDocument doc = new WordDocument();

            doc = doc.AddStudentInfo(student.Name, student.Id, student.Dob, student.Gender, student.Grade);

            for (var i = 0; i < student.Goals.Count; i++)
            {
                if (i < 1)
                {
                    continue;
                }

                var goal = student.Goals[i];

                doc = doc.AddGoal(goal);
            }


            return doc;
        }

        private static WordDocument AddGoal(this WordDocument source, Goal goal)
        {
            Paragraph p1 = new Paragraph();
            Paragraph p2 = new Paragraph();

            Run number = new Run();
            number = number.Stylize(goal.Number, 20, "Arial", false, true, true);

            Run colon = new Run();
            colon.Stylize(":", 20, "Arial", false, true, false);

            Run focus = new Run();
            focus = focus.Stylize(goal.Focus, 20, "Arial", false, true, true);

            Run annualGoalLabel = new Run();
            annualGoalLabel = annualGoalLabel.Stylize("ANNUAL GOAL: ", 20, "Arial", false, true, false);

            Run annualGoal = new Run();
            annualGoal = annualGoal.Stylize(goal.AnnualGoal, 20, "Arial", italics: true, bold: true, underline: false);


            p1.Add(number);
            p1.Add(colon);
            p1.Add(focus);
            p2.Add(annualGoalLabel);
            p2.Add(annualGoal);
            p2.Add(new Run());

            source.Body.Add(p1);
            source.Body.Add(p2);

            return source;
        }



        private static WordDocument AddStudentInfo(this WordDocument source, string name, string id, DateTimeOffset dob, Gender gender, string grade)
        {
            Paragraph p = new Paragraph();
            Run title = new Run();
            title = title.Stylize("Special Education Goal Data Collection Sheet", 24, "Arial", false, true, false);
            p.Add(title);
            p.Add(new Run());
            Run info = new Run();
            info = info.Stylize($"Name: {name}\tDOB: {dob:yyyy-MM-dd}\tID#: {id}\tGender: {gender}\tGrade: {grade}");
            p.Add(info);
            p.Add(new Run());
            p.Add(new Run("Evaluator: _______________________________\tGrading Period: _______________________________"));
            p.Add(new Run());
            source.Body.Add(p);
            return source;

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


        public static void CreateDocuments(string jsonFilePath, string targetDirectory)
        {
            var collection = GetDataCollection(jsonFilePath);

            foreach (var student in collection.Students)
            {
                var document = CreateDocument(student);

                var fileName = "2019-2020_lchs_ale_marshall_gp5_" + student.Name.Replace(" ", "_").ToLower() + "_goal_documentation.docx";

                var newPath = Path.Combine(targetDirectory, fileName);

                document.Save(newPath, true);
            }
        }


        private static DataCollection GetDataCollection(string jsonFilePath)
        {
            return DataCollection.FromJson(File.ReadAllText(jsonFilePath));
        }
    }
}
