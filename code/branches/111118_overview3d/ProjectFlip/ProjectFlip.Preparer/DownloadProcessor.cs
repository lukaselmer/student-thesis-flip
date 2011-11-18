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
        #region IProcessor Members

        public bool Process(IProjectNote projectNote)
        {
            return File.Exists(projectNote.FilepathPdf) || Download(projectNote);
        }

        #endregion

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
    }
}