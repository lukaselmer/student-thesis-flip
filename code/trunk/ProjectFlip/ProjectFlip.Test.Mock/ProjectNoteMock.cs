#region

using System;
using System.Collections.Generic;
using System.Windows.Documents;
using System.Windows.Media.Imaging;
using ProjectFlip.Services.Interfaces;

#endregion

namespace ProjectFlip.Test.Mock
{
    public class ProjectNoteMock : IProjectNote
    {
        private IDocumentPaginatorSource _document;

        // ReSharper disable UnusedAutoPropertyAccessor.Global

        #region IProjectNote Members

        public ProjectNoteMock()
        {
            Metadata = new Dictionary<IMetadataType, ICollection<IMetadata>>();
        }

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
        // ReSharper restore UnusedAutoPropertyAccessor.Global

        public IDocumentPaginatorSource Document { get { return _document = _document ?? new FixedDocument(); } }
        public void Preload() { }

        #endregion
    }
}