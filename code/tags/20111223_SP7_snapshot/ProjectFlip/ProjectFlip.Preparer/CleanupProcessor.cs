#region

using System.IO;
using ProjectFlip.Services.Interfaces;

#endregion

namespace ProjectFlip.Preparer
{
    /// <summary>
    /// This processor is responsible for the cleanup if something went wrong.
    /// </summary>
    /// <remarks></remarks>
    public class CleanupProcessor : IProcessor
    {
        #region Other

        /// <summary>
        /// Cleans up the according PDF and XPS file if the image could not
        /// be generated. The image / XPS generation could have failed when
        /// the PDF / XPS document was corrupted.
        /// </summary>
        /// <param name="projectNote">The project note.</param>
        /// <returns></returns>
        /// <remarks></remarks>
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