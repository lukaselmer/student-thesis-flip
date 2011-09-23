using System.Windows.Media.Imaging;

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
        BitmapImage Image { get; set; }

        /// <summary>
        /// Gets or sets the view count.
        /// </summary>
        int ViewCount { get; set; }
    }
}