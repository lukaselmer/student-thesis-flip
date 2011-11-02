#region

using System;
using System.Collections.Generic;
using System.Windows.Documents;
using System.Windows.Media.Imaging;

#endregion

namespace ProjectFlip.Services.Interfaces
{
    public interface IProjectNote
    {
        // ReSharper disable UnusedMemberInSuper.Global
        int Id { get; }
        string Title { get; }
        string Text { get; }
        string Sector { get; }
        string Customer { get; }
        IEnumerable<string> Focus { get; }
        IEnumerable<string> Services { get; }
        IEnumerable<string> Technologies { get; }
        IEnumerable<string> Applications { get; }
        IEnumerable<string> Tools { get; }
        DateTime Published { get; }
        string Filename { get; }
        string FilepathPdf { get; }
        string FilepathXps { get; }
        string FilepathImage { get; }
        string Url { get; }
        // ReSharper restore UnusedMemberInSuper.Global

        IList<string> Line { set; }

        // ReSharper disable UnusedMember.Global
        BitmapImage Image { get; }
        // ReSharper restore UnusedMember.Global

        /// <summary>
        /// Gets or sets the document.
        /// </summary>
        IDocumentPaginatorSource Document { get; }
    }
}