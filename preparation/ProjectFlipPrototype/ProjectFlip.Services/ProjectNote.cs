using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Documents;
using System.Windows.Media.Imaging;
using System.Windows.Xps.Packaging;
using ProjectFlip.Services.Interfaces;

namespace ProjectFlip.Services
{
    public class ProjectNote : IProjectNote
    {

        public ProjectNote()
        {
        }

        public IProjectNote InitByLine(IList<string> line)
        {
            Debug.Assert(line.Count == 19);
            Id = Convert.ToInt32(line[0]);
            Title = line[1];
            Text = line[2];
            Sector = line[3];
            Customer = line[4];
            Focus = line[5];
            Services = ToList(line[6]);
            Technologies = ToList(line[7]);
            Applications = ToList(line[8]);
            Tools = ToList(line[9]);
            Published = Convert.ToDateTime(line[10]);
            Filename = line[13];
            FilepathPdf = @"..\..\..\Resources\Pdf\" + Filename;
            FilepathXps = @"..\..\..\Resources\Xps\" + new Regex(@"\.pdf$").Replace(Filename, ".xps");
            return this;
            //Filename = @"..\..\..\Resources\Xps\test.xps";
        }

        private static IList<string> ToList(string s)
        {
            return new List<string> { s };
        }

        public int Id { get; private set; }
        public string Title { get; private set; }
        public string Text { get; private set; }
        public string Sector { get; private set; }
        public string Customer { get; private set; }
        public string Focus { get; private set; }
        public IList<string> Services { get; private set; }
        public IList<string> Technologies { get; private set; }
        public IList<string> Applications { get; private set; }
        public IList<string> Tools { get; private set; }
        public DateTime Published { get; private set; }
        public string Filename { get; private set; }
        public string FilepathPdf { get; private set; }
        public string FilepathXps { get; private set; }

        public string Url
        {
            get { return "http://www.zuehlke.com/uploads/tx_zepublications/" + Filename; }
        }

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
