#region

using System;
using System.Collections.Generic;

#endregion

namespace ProjectFlip.Services.Loader.Interfaces
{
    public interface IProjectNotesLoader
    {
        // ReSharper disable UnusedMemberInSuper.Global
        /// <summary>
        /// Gets or sets the filename of the metadata file, which is used to import the project notes
        /// </summary>
        /// <value>
        /// The filename.
        /// </value>
        string Filename { get; set; }

        /// <summary>
        /// Imports the elements from the metadata file
        /// </summary>
        /// <returns></returns>
        List<List<String>> Import();

        // ReSharper restore UnusedMemberInSuper.Global
    }
}