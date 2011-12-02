﻿#region

using System;
using System.Diagnostics;
using System.IO;
using ProjectFlip.Converter.Interfaces;

#endregion

namespace ProjectFlip.Converter.Pdf
{
    public class PdfConverter : IConverter
    {
        public static int SecondsToWait = 30;

        public PdfConverter()
        {
            AcrobatLocation = @"C:\Program Files (x86)\Adobe\Reader 10.0\Reader\AcroRd32.exe";
        }

        #region IConverter Members

        public string AcrobatLocation { get; set; }

        public bool Convert(string from, string to)
        {
            if (!RequirementsOk(from, to)) return false;

            var args = "/N /T " + from + " \"Microsoft XPS Document Writer\" /t \"" + to + "\"";
            Console.WriteLine(ExecCommand(AcrobatLocation, args)
                ? "XPS generated successful" : "XPS could not be generated: Timeout!");
            return true;
        }

        #endregion

        private bool RequirementsOk(string from, string to)
        {
            if (!File.Exists(AcrobatLocation))
            {
                Console.WriteLine("PDF Reader (AcroRd32.exe) not found.");
                return false;
            }
            if (!File.Exists(from))
            {
                Console.WriteLine("Input PDF not found");
                return false;
            }
            if (File.Exists(to))
            {
                Console.WriteLine("Destination file already exists");
                return false;
            }
            return true;
        }

        private bool ExecCommand(string exe, string args)
        {
            Console.WriteLine("Executing '" + exe + " " + args);
            var proc = new Process {StartInfo = {FileName = exe, Arguments = args}, EnableRaisingEvents = false};
            proc.Start();
            var res = proc.WaitForExit(SecondsToWait*1000);
            if (!res && !proc.HasExited) proc.Kill();
            return res;
        }
    }
}