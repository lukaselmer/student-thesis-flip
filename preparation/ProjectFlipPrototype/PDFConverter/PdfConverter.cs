using System;
using System.Diagnostics;
using System.IO;

namespace ProjectFlip.PdfConverter
{
    internal class PdfConverter
    {
        public PdfConverter()
        {
            AcrobatLocation = @"C:\Program Files (x86)\Adobe\Reader 10.0\Reader\AcroRd32.exe";
        }

        public static string AcrobatLocation { get; set; }

        public void Convert(string from, string to)
        {
            if (!RequirementsOk(from, to)) return;

            string args = "/N /T " + from + " \"Microsoft XPS Document Writer\" /t " + to;
            Console.WriteLine(ExecCommand(AcrobatLocation, args)
                                  ? "XPS generated successful"
                                  : "XPS could not be generated: Timeout!");
        }

        private static bool RequirementsOk(string from, string to)
        {
            if (!FileExists(AcrobatLocation))
            {
                Console.WriteLine("PDF Reader (AcroRd32.exe) not found.");
                return false;
            }
            if (!FileExists(from))
            {
                Console.WriteLine("Input PDF not found");
                return false;
            }
            if (FileExists(to))
            {
                Console.WriteLine("Destination file already exists");
                return false;
            }
            return true;
        }

        private static bool ExecCommand(string exe, string args)
        {
            var proc = new Process { StartInfo = { FileName = exe, Arguments = args }, EnableRaisingEvents = false };
            proc.Start();
            bool res = proc.WaitForExit(10000);
            if (!res) proc.Kill();
            return res;
        }

        private static bool FileExists(string possibleAcrobatLocation)
        {
            return File.Exists(possibleAcrobatLocation);
        }
    }
}