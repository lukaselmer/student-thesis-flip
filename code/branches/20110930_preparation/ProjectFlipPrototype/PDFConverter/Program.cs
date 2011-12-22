﻿using System;
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
                Console.ReadLine();
                return;
            }
            var p = new Converter.Pdf.PdfConverter();
            if(args.Length == 3) p.AcrobatLocation = args[2];
            p.Convert(args[0], args[1]);
            Console.ReadLine();
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