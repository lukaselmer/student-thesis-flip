#region

using System.IO;
using ProjectFlip.Services.Interfaces;

#endregion

namespace ProjectFlip.Preparer
{
    public class CleanupProcessor : IProcessor
    {
        #region Other

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