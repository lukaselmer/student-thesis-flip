using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Media.Imaging;
using System.Windows.Xps.Packaging;

namespace ProjectFlip.Services
{
    internal class XpsDocumentHelper
    {
        public XpsDocumentHelper(string filepath)
        {
            Filepath = filepath;
        }

        protected string Filepath { get; set; }

        public BitmapImage Logo()
        {
            var doc = new XpsDocument(Filepath, FileAccess.Read);
            //if (!Filepath.EndsWith("pn_013_d_digitalsignal_web.xps")) return DefaultLogo();

            if (doc.FixedDocumentSequenceReader != null)
            {
                var list = new List<IXpsFixedDocumentReader>(doc.FixedDocumentSequenceReader.FixedDocuments);
                foreach (var xpsFixedDocumentReader in list)
                {
                    //SaveImages(Filepath, images);
                    return RandomImage(xpsFixedDocumentReader);
                }
            }

            return DefaultLogo();
        }

        private static BitmapImage RandomImage(IXpsFixedDocumentReader xpsFixedDocumentReader)
        {
            var images = xpsFixedDocumentReader.FixedPages[0].Images;
            var image = new BitmapImage();
            image.BeginInit();
            image.StreamSource = new List<XpsImage>(images)[new Random().Next(images.Count)].GetStream();
            image.EndInit();
            return image;
        }

//        private static void SaveImages(string filepath, IEnumerable<XpsImage> images)
//        {
//            var dir = filepath + @"_images\";
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
//            }
//        }

        private static BitmapImage DefaultLogo()
        {
            var image = new BitmapImage();
            image.BeginInit();
            image.StreamSource = File.OpenRead(@"..\..\..\Resources\Xps\test.jpg");
            image.EndInit();
            return image;
        }
    }
}