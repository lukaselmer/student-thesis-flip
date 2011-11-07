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
        IMetadata Sector { get; }
        IMetadata Customer { get; }
        // ReSharper disable ReturnTypeCanBeEnumerable.Global
        IList<IMetadata> Focus { get; }
        IList<IMetadata> Services { get; }
        IList<IMetadata> Technologies { get; }
        IList<IMetadata> Applications { get; }
        IList<IMetadata> Tools { get; }
        // ReSharper restore ReturnTypeCanBeEnumerable.Global
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