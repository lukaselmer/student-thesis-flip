#region

using System;
using System.Collections.Generic;
using System.Windows.Documents;
using System.Windows.Media.Imaging;
using ProjectFlip.Services.Interfaces;

#endregion

namespace ProjectFlip.Test.Mock
{
    /// <summary>
    /// The ProjectNote mock
    /// </summary>
    /// <remarks></remarks>
    public class ProjectNoteMock : IProjectNote
    {
        #region Declarations

        private IDocumentPaginatorSource _document;

        #endregion

        #region Constructor

        public ProjectNoteMock()
        {
            Metadata = new Dictionary<IMetadataType, ICollection<IMetadata>>();
        }

        #endregion

        #region Properties

        // ReSharper disable UnusedAutoPropertyAccessor.Global

        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }

        public IDictionary<IMetadataType, ICollection<IMetadata>> Metadata { get; set; }

        public DateTime Published { get; set; }
        public string Filename { get; set; }
        public string FilepathPdf { get; set; }
        public string FilepathXps { get; set; }
        public string FilepathImage { get; set; }
        public string Url { get; set; }
        public IList<string> Line { get; set; }
        public BitmapImage Image { get; set; }

        public IDocumentPaginatorSource Document { get { return _document = _document ?? new FixedDocument(); } }
        // ReSharper restore UnusedAutoPropertyAccessor.Global

        #endregion

        #region Other

        public void Preload() {}

        #endregion
    }
}