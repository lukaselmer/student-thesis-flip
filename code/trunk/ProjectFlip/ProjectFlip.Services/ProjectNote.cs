#region

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Documents;
using System.Windows.Media.Imaging;
using System.Windows.Xps.Packaging;
using ProjectFlip.Services.Interfaces;

#endregion

namespace ProjectFlip.Services
{
    public class ProjectNote : IProjectNote
    {
        public static string FilepathFolder = @"..\..\..\Resources";

        #region IProjectNote Members

        public int Id { get; private set; }
        public string Title { get; private set; } // Audit einer IT-Infrastruktur und Organisation
        public string Text { get; private set; } // In einem externen Audit untersucht Zühlke die IT und die ...
        public string Sector { get; private set; } // Banking & Financial Services
        public string Customer { get; private set; } // HYPO Capital Management AG
        public IList<IMetadata> Focus { get; private set; } // Software Solutions
        public IList<IMetadata> Services { get; private set; } //"_ Tecogy Cong;#__ Tecogy Con;#__ Teo Eise
        public IList<IMetadata> Technologies { get; private set; } // Java EE
        public IList<IMetadata> Applications { get; private set; } //Information Systems
        public IList<IMetadata> Tools { get; private set; } // Eclipse;#Java Enterprise Edition;#Oracle;#SOAP;#XSL
        public DateTime Published { get; private set; }
        public string Filename { get; private set; }
        public string FilepathPdf { get; private set; }
        public string FilepathXps { get; private set; }
        public string FilepathImage { get; private set; }

        public string Url { get { return "http://www.zuehlke.com/uploads/tx_zepublications/" + Filename; } }

        public IList<string> Line
        {
            set
            {
                Debug.Assert(value.Count == 19);

                Id = Convert.ToInt32(value[0]);
                InitStringValues(value);
                InitListValues(value);
                Published = Convert.ToDateTime(value[10]);
                InitFileValues(value);
            }
        }

        public IDocumentPaginatorSource Document
        {
            get
            {
                var doc = new XpsDocument(FilepathXps, FileAccess.Read);
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
            }
        }

        #endregion

        private void InitStringValues(IList<string> value)
        {
            Title = value[1];
            Text = value[2];
            Sector = value[3];
            Customer = value[4];
        }

        private void InitListValues(IList<string> value)
        {
            Focus = ConvertToList(value[5], MetadataType.Focus);
            Services = ConvertToList(value[6], MetadataType.Services);
            Technologies = ConvertToList(value[7], MetadataType.Technologies);
            Applications = ConvertToList(value[8], MetadataType.Applications);
            Tools = ConvertToList(value[9], MetadataType.Tools);
        }

        private void InitFileValues(IList<string> value)
        {
            Filename = value[13];
            var pdfRegex = new Regex(@"\.pdf$");
            FilepathPdf = FilepathFolder + @"\Pdf\" + Filename;
            FilepathXps = FilepathFolder + @"\Xps\" + pdfRegex.Replace(Filename, ".xps");
            FilepathImage = FilepathFolder + @"\Images\" + pdfRegex.Replace(Filename, ".bmp");
        }

        private static IList<IMetadata> ConvertToList(string line, MetadataType type)
        {
            return SharepointStringDeserializer.ToList(line, type);
        }
    }
}