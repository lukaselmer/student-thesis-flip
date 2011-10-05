using System;
using System.Collections.Generic;
using System.Windows.Documents;
using System.Windows.Media.Imaging;

namespace ProjectFlip.Services.Interfaces
{
    public interface IProjectNote
    {
        int Id { get; }
        string Title { get; }
        string Text { get; }
        string Sector { get; }
        string Customer { get; }
        string Focus { get; }
        IList<string> Services { get; }
        IList<string> Technologies { get; }
        IList<string> Applications { get; }
        IList<string> Tools { get; }
        DateTime Published { get; }
        string Filename { get; }
        string FilepathPdf { get; }
        string FilepathXps { get; }
        string Url { get; }

        BitmapImage Image { get; }

        /// <summary>
        /// Gets or sets the document.
        /// </summary>
        IDocumentPaginatorSource Document { get; }

        void InitByLine(IList<string> line);
    }
}