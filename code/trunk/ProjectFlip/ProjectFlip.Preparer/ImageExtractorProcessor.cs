using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using System.Windows.Xps.Packaging;
using ProjectFlip.Services.Interfaces;
using Image = System.Drawing.Image;

namespace ProjectFlip.Preparer
{
    class ImageExtractorProcessor : IProcessor
    {
        public bool Process(IProjectNote projectNote)
        {
            if (!File.Exists(projectNote.FilepathImage))
            {
                var sta = new Thread(() => ExtractImage(projectNote.FilepathXps, projectNote.FilepathImage));
                sta.SetApartmentState(ApartmentState.STA);
                sta.Start();
                sta.Join();
            }
            return false;
        }

        private static void ExtractImage(string xpsPath, string imagePath)
        {
            if (!File.Exists(xpsPath)) return;

            //xpsPath = @"D:\hsr\Semesterarbeit\svn\code\trunk\ProjectFlip\Resources\Xps\pn_063_d_sensor_linearantrieb_web.xps";


            var xpsDoc = new XpsDocument(xpsPath, FileAccess.Read);
            try
            {
                xpsDoc.GetFixedDocumentSequence();

                var docSeq = xpsDoc.GetFixedDocumentSequence();
                xpsDoc.Close();
                if (docSeq == null) { xpsDoc.Close(); return; }

                var docPage = docSeq.DocumentPaginator.GetPage(0);

                var renderTarget = new RenderTargetBitmap(
                    (int)docPage.Size.Width,
                    (int)docPage.Size.Height,
                    96, 96, // WPF (Avalon) units are 96dpi based
                    PixelFormats.Default);
                // crop image from 340x309 - 583x552
                renderTarget.Render(docPage.Visual);

                var encoder = new BmpBitmapEncoder();  // Choose type here ie: JpegBitmapEncoder, etc
                encoder.Frames.Add(BitmapFrame.Create(renderTarget));

                var pageOutStream = new FileStream(imagePath + ".tmp", FileMode.Create, FileAccess.Write);
                encoder.Save(pageOutStream);
                pageOutStream.Close();

                var v = Image.FromFile(imagePath + ".tmp");
                var bmpImage = new Bitmap(v);
                var bmpCrop = bmpImage.Clone(new Rectangle(340, 309, 243, 243), bmpImage.PixelFormat);
                bmpCrop.Save(imagePath);
                v.Dispose();

                File.Delete(imagePath + ".tmp");
                bmpImage.Dispose();
                bmpCrop.Dispose();
            }
            finally
            {
                // Memory Leak .NET FrameworkBug!? See http://stackoverflow.com/questions/218681/opening-xps-document-in-net-causes-a-memory-leak
                // Workaround:
                // Executes: ContextLayoutManager.From(Dispatcher.CurrentDispatcher).UpdateLayout();

                xpsDoc.Close();

                var presentationCoreAssembly = Assembly.GetAssembly(typeof(UIElement));
                var contextLayoutManagerType = presentationCoreAssembly.GetType("System.Windows.ContextLayoutManager");
                var contextLayoutManager = contextLayoutManagerType.InvokeMember("From",
                BindingFlags.InvokeMethod | BindingFlags.Static | BindingFlags.NonPublic, null, null, new[] { Dispatcher.CurrentDispatcher });
                contextLayoutManagerType.InvokeMember("UpdateLayout", BindingFlags.InvokeMethod | BindingFlags.NonPublic | BindingFlags.Instance, null, contextLayoutManager, null);

                GC.Collect();
                // End of workaround
            }









            //Rectangle rect = new Rectangle(340, 309, 100, 100);
            //CroppedBitmap x = new CroppedBitmap(encoder.Frames[0].pix, rect);

            //Rectangle rect = new Rectangle(340, 309, 100, 100);
            //Bitmap cropped = bitmap.Clone(rect, bitmap.PixelFormat);



            /*Uri uri = new Uri(string.Format("memorystream://{0}", xpsPath));
            FixedDocumentSequence seq;

            using (Package pack = Package.Open(xpsPath, FileMode.Open, FileAccess.Read, FileShare.Read))
            using (StorePackage(uri, pack))
            using (XpsDocument xps = new XpsDocument(pack, CompressionOption.Normal, uri.ToString()))
            {
                seq = xps.GetFixedDocumentSequence();
            }*/

            /*
            DocumentPaginator paginator = seq.DocumentPaginator;
            // I only want the first page for this example
            Visual visual = paginator.GetPage(0).Visual;
             * */

            /*
            var doc = new XpsDocument(xpsPath, FileAccess.Read);
            if (doc.FixedDocumentSequenceReader == null) return;
            //if (!Filepath.EndsWith("pn_013_d_digitalsignal_web.xps")) return DefaultLogo();

            //var visual = doc.GetFixedDocumentSequence().DocumentPaginator.GetPage(0).Visual;
            var visual = doc.GetFixedDocumentSequence().DocumentPaginator.GetPage(0).Visual;
            var fe = (FrameworkElement) visual;
            
            var bmp = new RenderTargetBitmap((int)fe.ActualWidth,
                                      (int)fe.ActualHeight, 96d, 96d, PixelFormats.Default);
            bmp.Render(fe);*/

            /*if (doc.FixedDocumentSequenceReader != null)
            {
                var list = new List<IXpsFixedDocumentReader>(doc.FixedDocumentSequenceReader.FixedDocuments);
                foreach (var xpsFixedDocumentReader in list)
                {
                    SaveImage(xpsPath, imagePath);
                }
            }*/

            /*Uri uri = new Uri(string.Format("memorystream://{0}", xpsPath));
            FixedDocumentSequence seq;

            using (Package pack = Package.Open(xpsPath))
            using (var xps = new XpsDocument(pack, CompressionOption.Normal, uri.ToString()))
            {
                seq = xps.GetFixedDocumentSequence();
            }

            FrameworkElement fe = (FrameworkElement)seq.DocumentPaginator.GetPage(0).Visual;

            RenderTargetBitmap bmp = new RenderTargetBitmap((int)fe.ActualWidth,
                                      (int)fe.ActualHeight, 96d, 96d, PixelFormats.Default);
            bmp.Render(fe);

            PngBitmapEncoder png = new PngBitmapEncoder();
            png.Frames.Add(BitmapFrame.Create(bmp));

            using (Stream stream = File.Create(imagePath))
            {
                png.Save(stream);
            }*/

        }

        //private void SaveImage(string xpsPath, string imagePath)
        //{
        //   var dir = filepath + @"_images\";
        //            if (Directory.Exists(dir)) Directory.Delete(dir, true);
        //            Directory.CreateDirectory(dir);
        //
        //            var i = 0;
        //            foreach (var xpsImage in images)
        //            {
        //                ++i;
        //                //var toSaveImage = new BitmapImage();
        //                //toSaveImage.BeginInit();
        //                //toSaveImage.StreamSource = xpsImage.GetStream();
        //                //toSaveImage.EndInit();
        //                var sw = new StreamWriter(dir + i + @".bmp");
        //
        //                var stream = xpsImage.GetStream();
        //                var bw = new BinaryWriter(sw.BaseStream);
        //                int b;
        //                while ((b = stream.ReadByte()) != -1)
        //                {
        //                    /*byte[] bb = new byte[1];
        //                            bb[0] = (byte) b;*/
        //                    bw.Write((byte)b);
        //                }
        //                bw.Flush();
        //                //sw.Write(xpsImage.GetStream());
        //
        //
        //                //xpsImage.GetStream()
        //                //sw.Write(xpsImage.GetStream());
        //                sw.Flush();
        //                sw.Close();
        //            } }

    }
}
