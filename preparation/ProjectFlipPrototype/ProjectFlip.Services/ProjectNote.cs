using System;
using System.IO;
using System.Windows.Documents;
using System.Windows.Media.Imaging;
using System.Windows.Xps.Packaging;
using ProjectFlip.Services.Interfaces;

namespace ProjectFlip.Services
{
    public class ProjectNote : IProjectNote
    {
        public ProjectNote(string file)
        {
            Filename = file;
            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(file);
            if (fileNameWithoutExtension != null)
            {
                Name = fileNameWithoutExtension.Replace("_", " ");
            }
          }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        public string Filename { get; set; }

        public IDocumentPaginatorSource Document
        {
            get
            {
                var doc = new XpsDocument(Filename, FileAccess.Read);
                
                //var doc = new XpsDocument(@"..\..\..\Resources\Xps\test.xps", FileAccess.Read);
                //var doc = new XpsDocument(@"C:\Users\Lukas Elmer\Desktop\tmp.xps", FileAccess.Read);
                return doc.GetFixedDocumentSequence();
            }
        }

        /// <summary>
        /// Gets or sets the image.
        /// </summary>
        public BitmapImage Image
        {
            get
            {
                var image = new BitmapImage();
                image.BeginInit();
                image.StreamSource = File.OpenRead(@"..\..\..\Resources\Xps\test.jpg");
                image.EndInit();
                return image;
            }
        }

        /// <summary>
        /// Gets or sets the view count.
        /// </summary>
        public int ViewCount { get; set; }
    }
}
