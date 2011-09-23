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
            if (args.Length == 3) PdfConverter.AcrobatLocation = args[2];
            new PdfConverter().Convert(args[0], args[1]);
        }

        private static bool CheckForValidArgs(ICollection<string> args)
        {
            return args.Count == 2 || args.Count == 3;
        }

        private static void PrintUsage()
        {
            Console.WriteLine("Converts a PDF to a XPS file.\n\n" +
                              "PDFConverter source destination [AcroRd32_path]");
        }

    }
}
