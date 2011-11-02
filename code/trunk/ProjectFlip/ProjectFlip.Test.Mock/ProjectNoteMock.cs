using System.Collections.Generic;
using ProjectFlip.Services.Interfaces;

namespace ProjectFlip.Test.Mock
{
    public class ProjectNoteMock : IProjectNote
    {
        // ReSharper disable UnusedAutoPropertyAccessor.Global
        public int Id { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public string Sector { get; set; }

        public string Customer { get; set; }

        public IList<string> Focus { get; set; }

        public IList<string> Services { get; set; }

        public IList<string> Technologies { get; set; }

        public IList<string> Applications { get; set; }

        public IList<string> Tools { get; set; }

        public System.DateTime Published { get; set; }

        public string Filename { get; set; }

        public string FilepathPdf { get; set; }

        public string FilepathXps { get; set; }

        public string FilepathImage { get; set; }

        public string Url { get; set; }

        public IList<string> Line { get; set; }

        public System.Windows.Media.Imaging.BitmapImage Image { get; set; }
        // ReSharper restore UnusedAutoPropertyAccessor.Global

        private System.Windows.Documents.IDocumentPaginatorSource _document;
        public System.Windows.Documents.IDocumentPaginatorSource Document
        {
            get
            {
                return _document = _document ?? new System.Windows.Documents.FixedDocument();
            }
        }
    }
}