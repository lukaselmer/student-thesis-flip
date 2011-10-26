using System.IO;
using ProjectFlip.Services.Interfaces;

namespace ProjectFlip.Preparer
{
    public class CleanupProcessor : IProcessor
    {
        public bool Process(IProjectNote projectNote)
        {
            var success = true;
            if (!File.Exists(projectNote.FilepathImage))
            {
                try { File.Delete(projectNote.FilepathPdf); }
                catch { success = false; }
                try { File.Delete(projectNote.FilepathXps); }
                catch { success = false; }
            }
            return success;
        }
    }
}