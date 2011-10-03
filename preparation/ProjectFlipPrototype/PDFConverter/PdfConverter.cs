using System;
using System.Diagnostics;
using System.IO;
using ProjectFlip.Converter.Interfaces;

namespace ProjectFlip.Converter.Pdf
{
    public class PdfConverter : IConverter
    {
        public PdfConverter()
            : this(@"C:\Program Files (x86)\Adobe\Reader 10.0\Reader\AcroRd32.exe")
        {
        }
        public PdfConverter(string acrobatLocation)
        {
            AcrobatLocation = acrobatLocation;
        }

        public string AcrobatLocation { get; set; }

        public bool Convert(string from, string to)
        {
            if (!RequirementsOk(from, to)) return false;

            string args = "/N /T " + from + " \"Microsoft XPS Document Writer\" /t " + to;
            Console.WriteLine(ExecCommand(AcrobatLocation, args)
                                  ? "XPS generated successful"
                                  : "XPS could not be generated: Timeout!");
            return true;
        }

        private bool RequirementsOk(string from, string to)
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

        private bool ExecCommand(string exe, string args)
        {
            var proc = new Process { StartInfo = { FileName = exe, Arguments = args }, EnableRaisingEvents = false };
            proc.Start();
            bool res = proc.WaitForExit(10000);
            if (!res) proc.Kill();
            return res;
        }

        private bool FileExists(string possibleAcrobatLocation)
        {
            return File.Exists(possibleAcrobatLocation);
        }
    }
}