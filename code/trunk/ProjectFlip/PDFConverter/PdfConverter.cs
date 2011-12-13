#region

using System.Diagnostics;
using System.IO;
using System.Reflection;
using ProjectFlip.Converter.Interfaces;
using ProjectFlip.Converter.Pdf.Properties;
using log4net;

#endregion

namespace ProjectFlip.Converter.Pdf
{
    public class PdfConverter : IConverter
    {
        public static int SecondsToWait = 30;
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public PdfConverter()
        {
            AcrobatLocation = Settings.Default["AcrobatLocation"] as string;
        }

        #region IConverter Members

        public string AcrobatLocation { get; set; }

        public bool Convert(string from, string to)
        {
            if (!RequirementsOk(from, to)) return false;

            var args = "/N /T " + from + " \"Microsoft XPS Document Writer\" /t \"" + to + "\"";
            var successful = ExecCommand(AcrobatLocation, args);
            if (successful) Logger.Debug("XPS generated successful");
            else Logger.Error("XPS could not be generated: Timeout!");
            return true;
        }

        #endregion

        /// <summary>
        /// Checks if the requirements to convert a PFD to a XPS are ok.
        /// </summary>
        /// <param name="from">From path.</param>
        /// <param name="to">To path.</param>
        /// <returns>Wether the requirements are met or not</returns>
        /// <remarks></remarks>
        private bool RequirementsOk(string from, string to)
        {
            if (!File.Exists(AcrobatLocation))
            {
                Logger.Error(string.Format("PDF Reader (AcroRd32.exe) not found at {0}.", AcrobatLocation));
                return false;
            }
            if (!File.Exists(from))
            {
                Logger.Error("Input PDF not found");
                return false;
            }
            if (File.Exists(to))
            {
                Logger.Error("Destination file already exists");
                return false;
            }
            return true;
        }

        /// <summary>
        /// Executes the specified command. If the process doesn't finish in the given time it'll be terminated
        /// </summary>
        /// <param name="exe">The file to be executed.</param>
        /// <param name="args">The arguments</param>
        /// <returns></returns>
        private bool ExecCommand(string exe, string args)
        {
            Logger.Debug("Executing '" + exe + " " + args);
            var proc = new Process {StartInfo = {FileName = exe, Arguments = args}, EnableRaisingEvents = false};
            proc.Start();
            var res = proc.WaitForExit(SecondsToWait*1000);
            if (!res && !proc.HasExited)
            {
                proc.Kill();
                Logger.Warn(string.Format("Killed Process {0}", proc.Id));
            }
            return res;
        }
    }
}