#region

using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using System.Windows.Xps.Packaging;
using ProjectFlip.Services.Interfaces;

#endregion

namespace ProjectFlip.Preparer
{
    /// <summary>
    /// Extracts a image from the XPS document.
    /// </summary>
    /// <remarks></remarks>
    internal class ImageExtractorProcessor : IProcessor
    {
        #region Other

        /// <summary>
        /// Extracts a image from the XPS document.
        /// </summary>
        /// <param name="projectNote">The project note.</param>
        /// <returns></returns>
        /// <remarks>
        /// Normally, nnly the GUI thread uses the XpsDocument. Unfortunatly Microsoft did
        /// not publish a version which can be run in a non GUI thread. Therefore, the ApartmentState
        /// of the thread has to be set to ApartmentState.STA.
        /// 
        /// So this should be replaced as soon as there is a better alternative availible.
        /// </remarks>
        public bool Process(IProjectNote projectNote)
        {
            if (!File.Exists(projectNote.FilepathXps)) return false;
            if (File.Exists(projectNote.FilepathImage)) return false;

            var sta = new Thread(() => ExtractImage(projectNote.FilepathXps, projectNote.FilepathImage));
            // Normally, nnly the GUI thread uses the XpsDocument. Unfortunatly Microsoft did
            // not publish a version which can be run in a non GUI thread. Therefore, the ApartmentState
            // of the thread has to be set to ApartmentState.STA.
            sta.SetApartmentState(ApartmentState.STA);
            sta.Start();
            sta.Join();

            return true;
        }

        /// <summary>
        /// Extracts the image from the XPS file and saves it to the image path.
        /// </summary>
        /// <param name="xpsPath">The XPS path.</param>
        /// <param name="imagePath">The image path.</param>
        /// <returns></returns>
        /// <remarks>
        /// There is a known memory leak, <see cref="http://stackoverflow.com/questions/218681/opening-xps-document-in-net-causes-a-memory-leak"/>.
        /// In the finally block, the workaround is applied.
        /// </remarks>
        private static bool ExtractImage(string xpsPath, string imagePath)
        {
            try
            {
                ProcessDocument(xpsPath, imagePath);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                // Memory Leak in .NET FrameworkBug? See http://stackoverflow.com/questions/218681/opening-xps-document-in-net-causes-a-memory-leak
                // Workaround:
                // Executes: ContextLayoutManager.From(Dispatcher.CurrentDispatcher).UpdateLayout();
                var presentationCoreAssembly = Assembly.GetAssembly(typeof (UIElement));
                var contextLayoutManagerType = presentationCoreAssembly.GetType("System.Windows.ContextLayoutManager");
                var contextLayoutManager = contextLayoutManagerType.InvokeMember("From",
                    BindingFlags.InvokeMethod | BindingFlags.Static | BindingFlags.NonPublic, null, null,
                    new[] {Dispatcher.CurrentDispatcher});
                contextLayoutManagerType.InvokeMember("UpdateLayout",
                    BindingFlags.InvokeMethod | BindingFlags.NonPublic | BindingFlags.Instance, null,
                    contextLayoutManager, null);
                GC.Collect();
                // End of workaround
            }
        }

        /// <summary>
        /// Processes the document and extracts the image.
        /// </summary>
        /// <param name="xpsPath">The XPS path.</param>
        /// <param name="imagePath">The image path.</param>
        /// <remarks></remarks>
        private static void ProcessDocument(string xpsPath, string imagePath)
        {
            using (var xpsDoc = new XpsDocument(xpsPath, FileAccess.Read))
            {
                var docSeq = xpsDoc.GetFixedDocumentSequence();
                xpsDoc.Close();
                if (docSeq == null) return;

                var tempImagePath = imagePath + ".tmp";
                XpsToTempImage(tempImagePath, docSeq);
                TempImageToCroppedImage(imagePath, tempImagePath);
                File.Delete(tempImagePath);
            }
        }

        /// <summary>
        /// Extracts the full image from the IDocumentPaginatorSource and saves it to
        /// a temporary path.
        /// </summary>
        /// <param name="tempImagePath">The temp image path.</param>
        /// <param name="docSeq">The doc seq.</param>
        /// <remarks></remarks>
        private static void XpsToTempImage(string tempImagePath, IDocumentPaginatorSource docSeq)
        {
            using (var docPage = docSeq.DocumentPaginator.GetPage(0))
            {
                var renderTarget = new RenderTargetBitmap((int) docPage.Size.Width, (int) docPage.Size.Height, 96, 96,
                    PixelFormats.Default); // WPF (Avalon) units are 96dpi based
                renderTarget.Render(docPage.Visual);

                var encoder = new BmpBitmapEncoder(); // Choose type here ie: JpegBitmapEncoder, etc
                encoder.Frames.Add(BitmapFrame.Create(renderTarget));
                using (var pageOutStream = new FileStream(tempImagePath, FileMode.Create, FileAccess.Write))
                {
                    encoder.Save(pageOutStream);
                    pageOutStream.Close();
                }
            }
        }

        /// <summary>
        /// Cropps the temporary image (the full page view), so that the image at the position between
        /// 340x309 and 583x552 is cropped out. It then saves it to the real image path.
        /// </summary>
        /// <param name="imagePath">The image path.</param>
        /// <param name="tempImagePath">The temp image path.</param>
        /// <remarks></remarks>
        private static void TempImageToCroppedImage(string imagePath, string tempImagePath)
        {
            using (var v = Image.FromFile(tempImagePath))
            using (var bmpImage = new Bitmap(v)) // crop image from 340x309 - 583x552
            using (var bmpCrop = bmpImage.Clone(new Rectangle(340, 309, 243, 243), bmpImage.PixelFormat))
            {
                bmpCrop.Save(imagePath);
            }
        }

        #endregion
    }
}