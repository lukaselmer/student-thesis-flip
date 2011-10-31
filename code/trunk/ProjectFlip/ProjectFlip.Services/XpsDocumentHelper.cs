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