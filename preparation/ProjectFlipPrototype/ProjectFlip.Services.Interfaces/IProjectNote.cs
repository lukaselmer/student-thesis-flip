using System.Windows.Media.Imaging;
using System.Windows.Xps.Packaging;


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
        XpsDocument Document { get; set; }

        }
}