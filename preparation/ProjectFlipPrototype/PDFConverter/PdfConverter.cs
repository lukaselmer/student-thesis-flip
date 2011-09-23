using System;
using System.Linq;

namespace PdfConverter
{
    internal class PdfConverter
    {
        private static readonly string[] AcrobatLocations = new[] { @"C:\Program Files (x86)\Adobe\Reader 10.0\Reader\AcroRd32.exe" };

        public void Convert(string from, string to)
        {
            if (!(HasValidAcrobatLocation() && FileExists(from)))
            {
                throw new System.IO.FileNotFoundException();
            }
            if (FileExists(to))
            {
                Console.WriteLine("Destination file exists already");
                return;
            }

            var command = " /N /T " + from + " \"Microsoft XPS Document Writer\" /t " + to;
            Console.WriteLine(StartCommand(ValidAcrobatLocation(), command)
                                  ? "XPS generated successful"
                                  : "XPS could not be generated: Timeout!");
        }

        private bool StartCommand(string exe, string args)
        {
            var proc = new System.Diagnostics.Process { StartInfo = { FileName = exe, Arguments = args }, EnableRaisingEvents = false };
            proc.Start();
            var res = proc.WaitForExit(10000);
            if (!res) proc.Kill();
            return res;
        }

        public static bool HasValidAcrobatLocation()
        {
            return ValidAcrobatLocation() != "";
        }

        public static string ValidAcrobatLocation()
        {
            return AcrobatLocations.FirstOrDefault(FileExists);
        }

        private static bool FileExists(string possibleAcrobatLocation)
        {
            return System.IO.File.Exists(possibleAcrobatLocation);
        }
    }
}
