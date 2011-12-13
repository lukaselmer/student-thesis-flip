#region

using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using ProjectFlip.Services.Interfaces;

#endregion

namespace ProjectFlip.Preparer
{
    internal class DownloadProcessor : IProcessor
    {
        #region Other

        public bool Process(IProjectNote projectNote)
        {
            return File.Exists(projectNote.FilepathPdf) || Download(projectNote);
        }

        /// <summary>
        /// Downloads the specified project note.
        /// A loop is nessessary to find the existing version of the project note on the server
        /// Version count ranges between 0 and 09
        /// </summary>
        /// <param name="projectNote">The project note.</param>
        /// <param name="i">version number</param>
        /// <returns></returns>
        private bool Download(IProjectNote projectNote, int i = 0)
        {
            try
            {
                var regex = new Regex(@"\.pdf$");
                var downloadUrl = regex.Replace(projectNote.Url, (i == 0 ? "" : ("_0" + i)) + ".pdf");
                new WebClient().DownloadFile(downloadUrl, projectNote.FilepathPdf);
                return true;
            }
            catch (WebException e)
            {
                if (e.Status != WebExceptionStatus.ProtocolError) throw;
                return i < 9 && Download(projectNote, i + 1);
            }
        }

        #endregion
    }
}