#region

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
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

        public IDictionary<MetadataType, IList<IMetadata>> Metadata { get; private set; }

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
                Published = Convert.ToDateTime(value[10]);
                InitFileValues(value);
                InitMetadata(value);
            }
        }

        private void InitMetadata(IList<string> value)
        {
            Metadata = new Dictionary<MetadataType, IList<IMetadata>>();
            AddToMetadata(MetadataType.Sector, value[3]);
            AddToMetadata(MetadataType.Customer, value[4]);
            AddToMetadata(MetadataType.Focus, value[5]);
            AddToMetadata(MetadataType.Services, value[6]);
            AddToMetadata(MetadataType.Technologies, value[7]);
            AddToMetadata(MetadataType.Applications, value[8]);
            AddToMetadata(MetadataType.Tools, value[9]);
        }

        private void AddToMetadata(MetadataType metadataType, string line)
        {
            var metadataList = SharepointStringDeserializer.Deserialize(line, metadataType);
            var aggregatedList = metadataList.Select(Aggregator.AggregateMetadata).ToList();
            aggregatedList.ForEach(m =>
                {
                    if (!Metadata.ContainsKey(m.Type)) Metadata[m.Type] = new List<IMetadata>();
                    Metadata[m.Type].Add(m);
                });
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
        }

        private void InitFileValues(IList<string> value)
        {
            Filename = value[13];
            var pdfRegex = new Regex(@"\.pdf$");
            FilepathPdf = FilepathFolder + @"\Pdf\" + Filename;
            FilepathXps = FilepathFolder + @"\Xps\" + pdfRegex.Replace(Filename, ".xps");
            FilepathImage = FilepathFolder + @"\Images\" + pdfRegex.Replace(Filename, ".bmp");
        }
    }
}