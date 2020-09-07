using System.IO;
using Independentsoft.Office.Word;

namespace Gisd.Sped.Progress
{
    public static class DocumentFactory
    {

        public static WordDocument CreateDocument(Student student, AnnualGoal goal, string teacherName, string gradingPeriod, SchoolYearType year, string campus)
        {
            WordDocument doc = WordFactory.Document();
            doc = doc.AddTitle("Special Education");
            doc = doc.AddTitle("Data Collection Sheet");
            doc = doc.AddStudentInfo(student.GetFullName(), student.LocalID, student.DateOfBirth, student.Gender, student.Grade, campus, year, teacherName, gradingPeriod);
            doc = doc.AddGoal(goal);
            return doc;
        }

        public static void CreateDocuments(string filePath, string targetDirectory, string evaluator, int period, OutputType output)
        {
            var collection = ConfigurationSerializer.Deserialize(filePath);



            if (!Directory.Exists(targetDirectory))
            {
                Directory.CreateDirectory(targetDirectory);
            }

            var schoolYear = collection.SchoolYear[0];

            switch (output)
            {
                case OutputType.SingleDocument:
                    CreateSingleDocument(targetDirectory, evaluator, period, schoolYear);
                    break;
                case OutputType.DocumentPerStudent:
                    CreateDocumentPerStudent(targetDirectory, evaluator, period, schoolYear);
                    break;
                case OutputType.DocumentPerGoal:
                    CreateDocumentPerGoal(targetDirectory, evaluator, period, schoolYear);
                    break;
            }
        }

        private static void CreateSingleDocument(string targetDirectory, string evaluator, int period, SchoolYear schoolYear)
        {
            var doc = WordFactory.Document();
            var gradingPeriod = $"Grading Period {period}";
            foreach (var student in schoolYear.Students.Student)
            {
                var lastNumber = student.AnnualGoals.Goals.Count - 1;
                for (var i = 0; i < student.AnnualGoals.Goals.Count; i++)
                {
                    var goal = student.AnnualGoals.Goals[i];
                    doc = doc.ProcessDocument(student, goal, evaluator, gradingPeriod, schoolYear.Identifier, student.Campus, false);
                }
            }


            var fileName = $"2020–2021_oms_ale_marshall_gp{period}_goals_objectives_data_sheet.docx";
            var newPath = Path.Combine(targetDirectory, fileName);
            doc.Save(newPath, true);
        }



        private static void CreateDocumentPerStudent(string targetDirectory, string evaluator, int period, SchoolYear schoolYear)
        {
            var gradingPeriod = $"Grading Period {period}";

            foreach (var student in schoolYear.Students.Student)
            {
                var doc = WordFactory.Document();

                var lastNumber = student.AnnualGoals.Goals.Count - 1;

                for (var i = 0; i < student.AnnualGoals.Goals.Count; i++)
                {
                    var goal = student.AnnualGoals.Goals[i];
                    var last = lastNumber == i;
                    doc = doc.ProcessDocument(student, goal, evaluator, gradingPeriod, schoolYear.Identifier, student.Campus, last);
                }

                var fileName = $"2020–2021_oms_ale_marshall_gp{period}_" + student.GetFullName().Replace(" ", "_").ToLower() + "_goals_objectives_data_sheet.docx";
                var newPath = Path.Combine(targetDirectory, fileName);
                doc.Save(newPath, true);
            }
        }

        private static void CreateDocumentPerGoal(string targetDirectory, string evaluator, int period, SchoolYear schoolYear)
        {
            var gradingPeriod = $"Grading Period {period}";

            foreach (var student in schoolYear.Students.Student)
            {
                foreach (var goal in student.AnnualGoals.Goals)
                {
                    var document = CreateDocument(student, goal, evaluator, gradingPeriod, schoolYear.Identifier, student.Campus);

                    var fileName = $"2020–2021_oms_ale_marshall_gp{period}_" + student.GetFullName().Replace(" ", "_").ToLower() + "_goal_" + goal.Code + "_data_sheet.docx";

                    var newPath = Path.Combine(targetDirectory, fileName);

                    document.Save(newPath, true);
                }
            }
        }
    }
}
