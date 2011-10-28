using System.Collections.Generic;
using System.IO;
using System.Windows.Documents;
using System.Windows.Xps.Packaging;
using ProjectFlip.Services;
using ProjectFlip.Services.Interfaces;

namespace ProjectFlip.UserInterface.Surface.Test
{
    class ProjectNotesServiceMock : IProjectNotesService
    {

        private readonly List<IProjectNote> _projectNotes;

        public ProjectNotesServiceMock(int count)
        {
            _projectNotes = new List<IProjectNote>();
            for (int i = 0; i < count; i++)
            {
                var pn = new ProjectNoteMock();
                _projectNotes.Add(pn);
            }
        }

        public List<IProjectNote> ProjectNotes
        {
            get { return _projectNotes; }
        }


    }

    internal class ProjectNoteMock : IProjectNote
    {
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

        public System.Windows.Documents.IDocumentPaginatorSource Document
        {
            get
            {
                return new System.Windows.Documents.FixedDocument();
            }
        }
    }
}
