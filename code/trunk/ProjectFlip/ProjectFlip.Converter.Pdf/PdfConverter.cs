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
        #region Declarations

        /// <summary>
        /// The seconds to wait until the acrobat reader process is forced to quit.
        /// </summary>
        public static int SecondsToWaitForAdobeReaderExit = (int) Settings.Default["SecondsToWaitForAdobeReaderExit"];

        /// <summary>
        /// The logger.
        /// </summary>
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        /// <remarks></remarks>
        public PdfConverter()
        {
            AcrobatLocation = Settings.Default["AcrobatLocation"] as string;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the location of the Adobe Acrobat Reader (Version X)
        /// Example path: C:\Program Files (x86)\Adobe\Reader 10.0\Reader\AcroRd32.exe
        /// </summary>
        /// <value>The acrobat location.</value>
        /// <remarks></remarks>
        public string AcrobatLocation { get; set; }

        #endregion

        #region Other

        /// <summary>
        /// Converts a pdf file to an XPS file
        /// </summary>
        /// <param name="from">Location of the pdf file</param>
        /// <param name="to">Location of the xps file</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool Convert(string from, string to)
        {
            if (!RequirementsOk(from, to)) return false;

            var args = "/N /T " + from + " \"Microsoft XPS Document Writer\" /t \"" + to + "\"";
            var successful = ExecCommand(AcrobatLocation, args);
            if (successful) Logger.Debug("XPS generated successful");
            else Logger.Error("XPS could not be generated: Timeout!");
            return true;
        }

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
            var res = proc.WaitForExit(SecondsToWaitForAdobeReaderExit*1000);
            if (!res && !proc.HasExited)
            {
                proc.Kill();
                Logger.Warn(string.Format("Killed Process {0}", proc.Id));
            }
            return res;
        }

        #endregion
    }
}