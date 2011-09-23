using System;
using System.Collections.Generic;

namespace PdfConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            if (!CheckForValidArgs(args))
            {
                PrintUsage();
                return;
            }
            if (!PdfConverter.HasValidAcrobatLocation())
            {
                Console.WriteLine("PDF Reader (AcroRd32.exe) not found.");
                return;
            }
            new PdfConverter().Convert(args[0], args[1]);
        }

        private static bool CheckForValidArgs(ICollection<string> args)
        {
            return args.Count == 2;
        }

        private static void PrintUsage()
        {
            Console.WriteLine("Converts a PDF to a XPS file.\n\n" +
                              "PDFConverter source destination");
        }

    }
}
