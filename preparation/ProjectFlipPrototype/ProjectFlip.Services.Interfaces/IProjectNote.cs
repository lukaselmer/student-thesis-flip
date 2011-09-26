using System.Windows.Documents;

namespace ProjectFlip.Services.Interfaces
{
    public interface IProjectNote
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Gets or sets the image.
        /// </summary>
        IDocumentPaginatorSource Document { get; }
    }
}