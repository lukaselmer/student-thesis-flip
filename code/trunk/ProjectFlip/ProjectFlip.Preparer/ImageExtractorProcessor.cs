using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
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
                Thread sta = new Thread(() => ExtractImage(projectNote.FilepathXps, projectNote.FilepathImage));
                sta.SetApartmentState(ApartmentState.STA);
                sta.Start();
            }
            return false;
        }

        private void ExtractImage(string xpsPath, string imagePath)
        {
            Console.WriteLine("processing image...");
            if (!File.Exists(xpsPath)) return;
            Console.WriteLine("OOOOOOOOOOOOO");

            //xpsPath = @"D:\hsr\Semesterarbeit\svn\code\trunk\ProjectFlip\Resources\Xps\pn_063_d_sensor_linearantrieb_web.xps";


            XpsDocument xpsDoc = new XpsDocument(xpsPath, System.IO.FileAccess.Read);
            FixedDocumentSequence docSeq = xpsDoc.GetFixedDocumentSequence();
            if (docSeq == null) { xpsDoc.Close(); return; }

            DocumentPage docPage = docSeq.DocumentPaginator.GetPage(0);
            RenderTargetBitmap renderTarget = new RenderTargetBitmap(
                (int)docPage.Size.Width,
                (int)docPage.Size.Height,
                96, 96, // WPF (Avalon) units are 96dpi based
                PixelFormats.Default);
            // crop image from 340x309 - 583x552
            renderTarget.Render(docPage.Visual);

            BitmapEncoder encoder = new BmpBitmapEncoder();  // Choose type here ie: JpegBitmapEncoder, etc
            encoder.Frames.Add(BitmapFrame.Create(renderTarget));

            FileStream pageOutStream = new FileStream(imagePath + ".tmp", FileMode.Create, FileAccess.Write);
            encoder.Save(pageOutStream);
            pageOutStream.Close();

            var v = Image.FromFile(imagePath + ".tmp");
            Bitmap bmpImage = new Bitmap(v);
            Bitmap bmpCrop = bmpImage.Clone(new Rectangle(340, 309, 243, 243), bmpImage.PixelFormat);
            bmpCrop.Save(imagePath);
            v.Dispose();

            File.Delete(imagePath + ".tmp");
            xpsDoc.Close();

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
