﻿using System;
using System.IO;
using System.Windows.Documents;
using System.Windows.Media.Imaging;
using System.Windows.Xps.Packaging;
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
          }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        public IDocumentPaginatorSource Document
        {
            get
            {
                var doc = new XpsDocument(@"..\..\..\Resources\Xps\test.xps", FileAccess.Read);
                return doc.GetFixedDocumentSequence();
            }
        }

        /// <summary>
        /// Gets or sets the image.
        /// </summary>
        public BitmapImage Image
        {
            get
            {
                var img = new BitmapImage(new Uri(@"D:\Flip Project 2.0\preparation\ProjectFlipPrototype\Resources\Xps\test.jpg"));
                return img;
            }
        }

        /// <summary>
        /// Gets or sets the view count.
        /// </summary>
        public int ViewCount { get; set; }
    }
}
