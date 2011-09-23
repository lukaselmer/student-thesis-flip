﻿using System.IO;
using System.Windows.Media.Imaging;
using ProjectFlip.Services.Interfaces;

namespace ProjectFlip.Services
{
    public class ProjectNote : IProjectNote
    {
        public ProjectNote(string file)
        {
            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(file);
            if (fileNameWithoutExtension != null)
            {
                Name = fileNameWithoutExtension.Replace("_", " ");
            }

            Image = new BitmapImage();
            Image.BeginInit();
            Image.StreamSource = File.OpenRead(file);
            Image.EndInit();
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }
       
        /// <summary>
        /// Gets or sets the image.
        /// </summary>
        public BitmapImage Image { get; set; }

        /// <summary>
        /// Gets or sets the view count.
        /// </summary>
        public int ViewCount { get; set; }
    }
}