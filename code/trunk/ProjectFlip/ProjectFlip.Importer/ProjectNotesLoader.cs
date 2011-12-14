#region

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using ProjectFlip.Services.Loader.Interfaces;

#endregion

namespace ProjectFlip.Services.Loader
{
    /// <summary>
    /// Loads the project notes.
    /// </summary>
    /// <remarks></remarks>
    public class ProjectNotesLoader : IProjectNotesLoader
    {
        #region Declarations

        /// <summary>
        /// The lines of the text file.
        /// </summary>
        private List<List<string>> _lines;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        /// <remarks></remarks>
        public ProjectNotesLoader()
        {
            Filename = @"..\..\..\Resources\metadata.txt";
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the filename of the metadata file, which is used to import the project notes.
        /// </summary>
        /// <value>The filename.</value>
        /// <remarks></remarks>
        public string Filename { get; set; }

        #endregion

        #region Other

        /// <summary>
        /// Imports the elements from the metadata file.
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public List<List<String>> Import()
        {
            LoadText();
            CheckText();
            CleanFields();
            return Rows();
        }

        /// <summary>
        /// Cleans the fields.
        /// </summary>
        /// <remarks></remarks>
        private void CleanFields()
        {
            _lines.RemoveAll(line => line.Count != 19);
            _lines = _lines.ConvertAll(line => line.ConvertAll(f => f.Trim()));
        }

        /// <summary>
        /// Gets the rows.
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        private List<List<string>> Rows()
        {
            return _lines.GetRange(1, _lines.Count - 1);
        }

        /// <summary>
        /// Checks if the text file format is valid.
        /// </summary>
        /// <remarks></remarks>
        private void CheckText()
        {
            _lines.ForEach(
                line =>
                    Debug.Assert(line.Count == 19,
                        "Wrong text file: Expected each Line to contain 19 elements (18 tabs)."));
        }

        /// <summary>
        /// Loads the text from the file.
        /// </summary>
        /// <remarks></remarks>
        private void LoadText()
        {
            _lines = new List<List<string>>();
            using (var sr = new StreamReader(Filename))
            {
                String line;
                while ((line = sr.ReadLine()) != null)
                {
                    _lines.Add(new List<string>(line.Split('\t')));
                }
            }
        }

        #endregion
    }
}