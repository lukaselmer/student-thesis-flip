using System;
using System.Collections.Generic;

namespace ProjectFlip.Converter.Pdf
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            if (!CheckForValidArgs(args))
            {
                PrintUsage();
                return;
            }
            var p = args.Length == 3 ? new Converter.Pdf.PdfConverter(args[2]) : new Converter.Pdf.PdfConverter();
            p.Convert(args[0], args[1]);
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