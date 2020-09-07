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

        public static void CreateDocuments(string filePath, string targetDirectory, string evaluator, string gradingPeriod, bool singleDocument = false)
        {
            var collection = ConfigurationSerializer.Deserialize(filePath);

            if (!Directory.Exists(targetDirectory))
            {
                Directory.CreateDirectory(targetDirectory);
            }

            var schoolYear = collection.SchoolYear[0];

            if (singleDocument)
            {
                CreateSingleDocumentCore(targetDirectory, evaluator, gradingPeriod, schoolYear);
            }
            else
            {
                CreateMultipleDocumentsCore(targetDirectory, evaluator, gradingPeriod, schoolYear);
            }



        }

        private static void CreateSingleDocumentCore(string targetDirectory, string evaluator, string gradingPeriod, SchoolYear schoolYear)
        {
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

                var fileName = "2020–2021_oms_ale_marshall_gp1_" + student.GetFullName().Replace(" ", "_").ToLower() + "_goals_objectives_data_sheet.docx";
                var newPath = Path.Combine(targetDirectory, fileName);
                doc.Save(newPath, true);
            }
        }

        private static void CreateMultipleDocumentsCore(string targetDirectory, string evaluator, string gradingPeriod, SchoolYear schoolYear)
        {
            foreach (var student in schoolYear.Students.Student)
            {
                foreach (var goal in student.AnnualGoals.Goals)
                {
                    var document = CreateDocument(student, goal, evaluator, gradingPeriod, schoolYear.Identifier, student.Campus);

                    var fileName = "2020–2021_oms_ale_marshall_gp1_" + student.GetFullName().Replace(" ", "_").ToLower() + "_goal_" + goal.Code + "_data_sheet.docx";

                    var newPath = Path.Combine(targetDirectory, fileName);

                    document.Save(newPath, true);
                }
            }
        }
    }
}
