using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
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
        public IProjectNote InitByLine(IList<string> line)
        {
            Debug.Assert(line.Count == 19);
            Id = Convert.ToInt32(line[0]);
            Title = line[1];
            Text = line[2];
            Sector = line[3];
            Customer = line[4];
            Focus = ConvertToList(line[5]);
            Services = ConvertToList(line[6]);
            Technologies = ConvertToList(line[7]);
            Applications = ConvertToList(line[8]);
            Tools = ConvertToList(line[9]);
            Published = Convert.ToDateTime(line[10]);

            Filename = line[13];
            var pdfRegex = new Regex(@"\.pdf$");
            FilepathPdf = @"..\..\..\Resources\Pdf\" + Filename;
            FilepathXps = @"..\..\..\Resources\Xps\" + pdfRegex.Replace(Filename, ".xps");
            FilepathImage = @"..\..\..\Resources\Images\" + pdfRegex.Replace(Filename, ".bmp");
            return this;
            //Filename = @"..\..\..\Resources\Xps\test.xps";
        }

        private static IList<string> ConvertToList(string line)
        {
            return new List<string> { line };
        }

        public int Id { get; private set; }
        public string Title { get; private set; } // Audit einer IT-Infrastruktur und Organisation
        public string Text { get; private set; } // In einem externen Audit untersucht Zühlke die IT und die Organisationsstruktur in Bezug auf Zukunftssicherheit, Betriebssicherheit, Ausfallssicherheit und strategische Ausrichtung sowie auf organisatorische Versäumnisse.
        public string Sector { get; private set; } // Banking & Financial Services
        public string Customer { get; private set; } // HYPO Capital Management AG
        public IList<string> Focus { get; private set; } // Software Solutions
        public IList<string> Services { get; private set; } //"_ Technology Consulting;#__ Technology Consultation;#__ Technology Expertise;#_ Methodology;#__ ZAAF"
        public IList<string> Technologies { get; private set; } // Java EE
        public IList<string> Applications { get; private set; } //Information Systems
        public IList<string> Tools { get; private set; } // Eclipse;#Java Enterprise Edition;#Oracle;#SOAP;#XSL
        public DateTime Published { get; private set; }
        public string Filename { get; private set; }
        public string FilepathPdf { get; private set; }
        public string FilepathXps { get; private set; }
        public string FilepathImage { get; private set; }

        public string Url
        {
            get { return "http://www.zuehlke.com/uploads/tx_zepublications/" + Filename; }
        }

        public IDocumentPaginatorSource Document
        {
            get
            {
                var doc = new XpsDocument(FilepathXps, FileAccess.Read);
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
                if (!File.Exists(FilepathImage)) return null;
                var image = new BitmapImage();
                image.BeginInit();
                image.StreamSource = File.OpenRead(FilepathImage);
                image.EndInit();
                return image;
                // return new BitmapImage(new Uri(FilepathImage, UriKind.Relative));
                /*var image = new BitmapImage();
                image.BeginInit();
                image.StreamSource = new List<XpsImage>(images)[new Random().Next(images.Count)].GetStream();
                image.EndInit();
                return image;*/
                //var helper = new XpsDocumentHelper(FilepathXps);
                //return helper.Logo();
                //return Bitmap.FromFile(FilepathImage);
            }
        }

        /// <summary>
        /// Gets or sets the view count.
        /// </summary>
        public int ViewCount { get; set; }
    }
}
