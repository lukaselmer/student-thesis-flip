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
    internal class ImageExtractorProcessor : IProcessor
    {
        #region IProcessor Members

        public bool Process(IProjectNote projectNote)
        {
            if (!File.Exists(projectNote.FilepathXps)) return false;
            if (File.Exists(projectNote.FilepathImage)) return false;

            var sta = new Thread(() => ExtractImage(projectNote.FilepathXps, projectNote.FilepathImage));
            sta.SetApartmentState(ApartmentState.STA);
            sta.Start();
            sta.Join();

            return true;
        }

        #endregion

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

        private static void TempImageToCroppedImage(string imagePath, string tempImagePath)
        {
            using (var v = Image.FromFile(tempImagePath))
            using (var bmpImage = new Bitmap(v)) // crop image from 340x309 - 583x552
            using (var bmpCrop = bmpImage.Clone(new Rectangle(340, 309, 243, 243), bmpImage.PixelFormat))
            {
                bmpCrop.Save(imagePath);
            }
        }
    }
}