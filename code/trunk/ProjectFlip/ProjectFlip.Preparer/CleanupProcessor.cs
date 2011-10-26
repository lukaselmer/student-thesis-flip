using System.IO;
using ProjectFlip.Services.Interfaces;

namespace ProjectFlip.Preparer
{
    public class CleanupProcessor : IProcessor
    {
        #region IProcessor Members

        public bool Process(IProjectNote projectNote)
        {
            if (!File.Exists(projectNote.FilepathImage))
            {
                try
                {
                    File.Delete(projectNote.FilepathPdf);
                    File.Delete(projectNote.FilepathXps);
                }
                catch
                {
                    return false;
                }
            }
            return true;
        }

        #endregion
    }
}