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
        // ReSharper disable ReturnTypeCanBeEnumerable.Global

        /// <summary>
        /// Gets the id.
        /// </summary>
        int Id { get; }

        /// <summary>
        /// Gets the title.
        /// </summary>
        string Title { get; }

        /// <summary>
        /// Gets the text (short description).
        /// </summary>
        string Text { get; }

        /// <summary>
        /// Gets the metadata.
        /// </summary>
        IDictionary<IMetadataType, ICollection<IMetadata>> Metadata { get; }

        /// <summary>
        /// Gets the date of the publication.
        /// </summary>
        DateTime Published { get; }

        /// <summary>
        /// Gets the filename.
        /// </summary>
        string Filename { get; }

        /// <summary>
        /// Gets the filepath of the PDF document.
        /// </summary>
        string FilepathPdf { get; }

        /// <summary>
        /// Gets the filepath of the XPS document.
        /// </summary>
        string FilepathXps { get; }

        /// <summary>
        /// Gets the filepath of the image.
        /// </summary>
        string FilepathImage { get; }

        /// <summary>
        /// Gets the URL.
        /// </summary>
        string Url { get; }
        // ReSharper restore ReturnTypeCanBeEnumerable.Global
        // ReSharper restore UnusedMemberInSuper.Global

        /// <summary>
        /// Sets the line.
        /// The line contains the information about the metadata of a project note
        /// </summary>
        /// <value>
        /// The line.
        /// </value>
        IList<string> Line { set; }

        // ReSharper disable UnusedMember.Global
        /// <summary>
        /// Gets the image.
        /// </summary>
        BitmapImage Image { get; }
        // ReSharper restore UnusedMember.Global

        /// <summary>
        /// Gets or sets the document.
        /// </summary>
        IDocumentPaginatorSource Document { get; }

        /// <summary>
        /// Preloads this document.
        /// </summary>
        void Preload();
    }
}