using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PdfConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(args[0]);
            if (!CheckForValidArgs(args))
            {
                PrintUsage();
                return;
            }
            new PdfConverter().Convert(args[0], args[1]);
        }

        private static bool CheckForValidArgs(string[] args)
        {
            return args.Length == 2;
        }

        private static void PrintUsage()
        {
            Console.WriteLine("Converts a PDF to a XPS file.\n\n" +
                              "PDFConverter source destination");
        }

    }
}
