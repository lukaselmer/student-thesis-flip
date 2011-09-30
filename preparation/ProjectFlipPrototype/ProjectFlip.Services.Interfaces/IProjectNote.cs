using System.Windows.Documents;
using System.Windows.Media.Imaging;

namespace ProjectFlip.Services.Interfaces
{
    public interface IProjectNote
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        string Name { get;}

        BitmapImage Image { get;}
        /// <summary>
        /// Gets or sets the document.
        /// </summary>
        IDocumentPaginatorSource Document { get;}
    }
}