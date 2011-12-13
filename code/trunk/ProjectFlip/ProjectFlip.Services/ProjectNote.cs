#region

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using System.Windows.Xps.Packaging;
using ProjectFlip.Services.Interfaces;
using ProjectFlip.Services.Properties;

#endregion

namespace ProjectFlip.Services
{
    public class ProjectNote : NotifierModel, IProjectNote
    {
        #region Declarations

        public static string FilepathFolder = @"..\..\..\Resources";
        private IDocumentPaginatorSource _document;
        private DispatcherTimer _timer;

        #endregion

        #region Properties

        public IAggregator Aggregator { private get; set; }

        public int Id { get; private set; }
        public string Title { get; private set; } // Audit einer IT-Infrastruktur und Organisation
        public string Text { get; private set; } // In einem externen Audit untersucht Zühlke die IT und die ...

        public IDictionary<IMetadataType, ICollection<IMetadata>> Metadata { get; private set; }
        public DateTime Published { get; private set; }

        public string Filename { get; private set; }
        public string FilepathPdf { get; private set; }
        public string FilepathXps { get; private set; }
        public string FilepathImage { get; private set; }

        public string Url { get { return (Settings.Default["ProjectNotesUrl"] as string) + Filename; } }

        /// <summary>
        /// Gets or sets the XPS document. The document will be loaded asynchronous if it is not cached yet.
        /// </summary>
        /// <remarks></remarks>
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

        /// <summary>
        /// Initializes the Project Note with an array of strings
        /// </summary>
        /// <value>The contents of the project note.</value>
        /// <remarks></remarks>
        public IList<string> Line
        {
            set
            {
                if (value.Count != 19) throw new Exception("A line must contain exactly 19 elements");

                Id = Convert.ToInt32(value[0]);
                InitStringValues(value);
                Published = Convert.ToDateTime(value[10]);
                InitFileValues(value);
                InitMetadata(value);
            }
        }

        /// <summary>
        /// Gets the image.
        /// </summary>
        /// <remarks></remarks>
        public BitmapImage Image
        {
            get
            {
                if (!File.Exists(FilepathImage)) return null;
                var image = new BitmapImage();
                image.BeginInit();
                image.StreamSource = File.OpenRead(FilepathImage);
                image.EndInit();
                image.Freeze();
                return image;
            }
        }

        #endregion

        #region Other

        /// <summary>
        /// Preloads the XPS document of this project note.
        /// </summary>
        /// <remarks></remarks>
        public void Preload()
        {
            if (_document != null) return;
            lock (this)
            {
                if (_timer != null) return;

                var dispatcher = Application.Current == null
                    ? Dispatcher.CurrentDispatcher : Application.Current.Dispatcher;

                _timer = new DispatcherTimer(TimeSpan.FromMilliseconds(500), DispatcherPriority.Background,
                    (s, e) => LoadDocument(), dispatcher);
                _timer.Start();
            }
        }

        private void LoadDocument()
        {
            var doc = new XpsDocument(FilepathXps, FileAccess.Read);
            Document = doc.GetFixedDocumentSequence();
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

        #endregion
    }
}