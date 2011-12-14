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
    /// <summary>
    /// The project note. A project note is a project reference
    /// for realized projects. For example, if a successful project has been
    /// finished whith the customer "Credit Suisse", programmed in "Java", the
    /// project note will show some more details. It is always availible as
    /// an A4 PDF paper print.
    /// </summary>
    /// <remarks></remarks>
    public class ProjectNote : NotifierModel, IProjectNote
    {
        #region Declarations

        /// <summary>
        /// Location where to find the resources (PDF, XPS, image)
        /// </summary>
        public static string FilepathFolder = @"..\..\..\Resources";

        /// <summary>
        /// The document (cache).
        /// </summary>
        private IDocumentPaginatorSource _document;

        /// <summary>
        /// The dispatcher timer is used to load the document.
        /// </summary>
        private DispatcherTimer _timer;

        #endregion

        #region Properties

        /// <summary>
        /// Sets the aggregator.
        /// </summary>
        /// <value>The aggregator.</value>
        /// <remarks></remarks>
        public IAggregator Aggregator { private get; set; }

        /// <summary>
        /// Gets the id.
        /// </summary>
        /// <remarks></remarks>
        public int Id { get; private set; }

        /// <summary>
        /// Gets the title.
        /// </summary>
        /// <remarks></remarks>
        public string Title { get; private set; } // E.g.: Audit einer IT-Infrastruktur und Organisation

        /// <summary>
        /// Gets the text (short description).
        /// </summary>
        /// <remarks></remarks>
        public string Text { get; private set; } // E.g.: In einem externen Audit untersucht Zühlke die IT und die ...

        /// <summary>
        /// Gets the metadata.
        /// </summary>
        /// <remarks></remarks>
        public IDictionary<IMetadataType, ICollection<IMetadata>> Metadata { get; private set; }

        /// <summary>
        /// Gets the date of the publication.
        /// </summary>
        /// <remarks></remarks>
        public DateTime Published { get; private set; }

        /// <summary>
        /// Gets the filename.
        /// </summary>
        /// <remarks></remarks>
        public string Filename { get; private set; }

        /// <summary>
        /// Gets the filepath of the PDF document.
        /// </summary>
        /// <remarks></remarks>
        public string FilepathPdf { get; private set; }

        /// <summary>
        /// Gets the filepath of the XPS document.
        /// </summary>
        /// <remarks></remarks>
        public string FilepathXps { get; private set; }

        /// <summary>
        /// Gets the filepath of the image.
        /// </summary>
        /// <remarks></remarks>
        public string FilepathImage { get; private set; }

        /// <summary>
        /// Gets the URL from where to download the project note.
        /// </summary>
        /// <remarks></remarks>
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
        /// Preloads the XPS document of this project note. If the document is not loaded yet, it
        /// is loaded asynchronous. This is important for the animations to perform nicely.
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

        /// <summary>
        /// Loads the document from the file system.
        /// </summary>
        /// <remarks></remarks>
        private void LoadDocument()
        {
            var doc = new XpsDocument(FilepathXps, FileAccess.Read);
            Document = doc.GetFixedDocumentSequence();
        }

        /// <summary>
        /// Initializes the metadata.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <remarks></remarks>
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

        /// <summary>
        /// Adds a metadata line to the metadata. A line can consist of multiple metadata, for
        /// exaple "Java", "JavaBeans", "Eclipse"
        /// </summary>
        /// <param name="metadataType">Type of the metadata.</param>
        /// <param name="line">The line.</param>
        /// <remarks></remarks>
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

        /// <summary>
        /// Inits the string values title and text.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <remarks></remarks>
        private void InitStringValues(IList<string> value)
        {
            Title = value[1];
            Text = value[2];
        }

        /// <summary>
        /// Inits the file path values for PDF, XPS and image.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <remarks></remarks>
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