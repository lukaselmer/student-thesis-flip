#region

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using System.Windows.Xps.Packaging;
using ProjectFlip.Services.Interfaces;

#endregion

namespace ProjectFlip.Services
{
    public class ProjectNote : NotifierModel, IProjectNote
    {
        public static string FilepathFolder = @"..\..\..\Resources";

        public int Id { get; private set; }
        public string Title { get; private set; } // Audit einer IT-Infrastruktur und Organisation
        public string Text { get; private set; } // In einem externen Audit untersucht Zühlke die IT und die ...

        public IDictionary<IMetadataType, ICollection<IMetadata>> Metadata { get; private set; }

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
            Metadata = new Dictionary<IMetadataType, ICollection<IMetadata>>();
            AddToMetadata("Sector", value[3]);
            AddToMetadata("Customer", value[4]);
            AddToMetadata("Focus", value[5]);
            AddToMetadata("Services", value[6]);
            AddToMetadata("Technologies", value[7]);
            AddToMetadata("Applications", value[8]);
            AddToMetadata("Tools", value[9]);
        }

        private void AddToMetadata(string metadataType, string line)
        {
            var metadataList = SharepointStringDeserializer.Deserialize(line, MetadataType.Get(metadataType));
            if (Aggregator == null) Aggregator = new Aggregator();
            var aggregatedList = metadataList.Select(Aggregator.AggregateMetadata).ToList();
            aggregatedList.ForEach(m =>
                {
                    if (!Metadata.ContainsKey(m.Type)) Metadata[m.Type] = new SortedSet<IMetadata>(new MetadataComparer());
                    Metadata[m.Type].Add(m);
                });
        }

        private IDocumentPaginatorSource _document;
        private DispatcherTimer _timer;

        public void Preload()
        {
            if (_document != null) return;
            lock (this)
            {
                if (_timer != null) return;

                var dispatcher = Application.Current == null ? Dispatcher.CurrentDispatcher : Application.Current.Dispatcher;

                _timer = new DispatcherTimer(TimeSpan.FromMilliseconds(500), DispatcherPriority.Background,
                    (s, e) => LoadDocument(), dispatcher);
                _timer.Start();
            }
        }

        public IDocumentPaginatorSource Document
        {
            get
            {
                Preload();
                return _document;
            }
            private set
            {
                if (_timer != null) _timer.Stop();
                _document = value;
                Notify("Document");
            }
        }

        private void LoadDocument()
        {
            var doc = new XpsDocument(FilepathXps, FileAccess.Read);
            Document = doc.GetFixedDocumentSequence();
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

        public IAggregator Aggregator { private get; set; }

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