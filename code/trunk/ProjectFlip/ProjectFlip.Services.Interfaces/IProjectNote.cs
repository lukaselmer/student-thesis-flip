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
        IList<string> Focus { get; }
        IList<string> Services { get; }
        IList<string> Technologies { get; }
        IList<string> Applications { get; }
        IList<string> Tools { get; }
        DateTime Published { get; }
        string Filename { get; }
        string FilepathPdf { get; }
        string FilepathXps { get; }
        string FilepathImage { get; }
        string Url { get; }

        IList<string> Line { set; }

        BitmapImage Image { get; }

        /// <summary>
        /// Gets or sets the document.
        /// </summary>
        IDocumentPaginatorSource Document { get; }
    }
}