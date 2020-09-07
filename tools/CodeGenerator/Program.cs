using System;
using System.Diagnostics;
using System.Text;
using System.Threading;
using Gisd.Sped.Progress;

namespace CodeGenerator
{
    public static class Program
    {
        private const string _filePath = @"C:\Users\bmarshall.SPED\Source\Repos\progress-monitoring\src\Gisd.Sped.Progress\Schema\XML\sped-config.xml";

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

            Process.Start("g:\\temp\\");
        }

        private static void TestDocumentFactory()
        {
            DocumentFactory.CreateDocuments(_filePath, "g:\\temp\\", "Brad Marshall", "Grading Period 1", true);
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
