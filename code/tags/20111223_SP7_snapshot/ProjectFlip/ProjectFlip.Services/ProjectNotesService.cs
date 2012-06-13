﻿#region

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ProjectFlip.Services.Interfaces;
using ProjectFlip.Services.Loader.Interfaces;

#endregion

namespace ProjectFlip.Services
{
    /// <summary>
    /// The project note service is responsible for loading the project notes
    /// and the according metadata.
    /// </summary>
    /// <remarks></remarks>
    public class ProjectNotesService : IProjectNotesService
    {
        #region Declarations

        /// <summary>
        /// The metadata of all project notes.
        /// </summary>
        private readonly IDictionary<IMetadataType, ICollection<IMetadata>> _metadata;

        /// <summary>
        /// The project notes.
        /// </summary>
        private readonly List<IProjectNote> _projectNotes;

        /// <summary>
        /// The projecct notes loader.
        /// </summary>
        private readonly IProjectNotesLoader _projectNotesLoader;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectNotesService"/> class.
        /// </summary>
        /// <param name="projectNotesLoader">The project notes loader.</param>
        /// <remarks></remarks>
        public ProjectNotesService(IProjectNotesLoader projectNotesLoader)
        {
            var aggregator = new Aggregator();
            aggregator.LoadMapping();
            _projectNotesLoader = projectNotesLoader;
            _projectNotes =
                new List<IProjectNote>(
                    _projectNotesLoader.Import().ConvertAll(
                        line => new ProjectNote { Aggregator = aggregator, Line = line }));
            _projectNotes.RemoveAll(pn => !File.Exists(pn.FilepathXps));
            _metadata = new Dictionary<IMetadataType, ICollection<IMetadata>>();
            Action<IMetadata> func = m =>
                                     {
                                         if (!_metadata.ContainsKey(m.Type)) _metadata[m.Type] = new SortedSet<IMetadata>(new MetadataComparer());
                                         if (!_metadata[m.Type].Contains(m)) _metadata[m.Type].Add(m);
                                     };
            _projectNotes.ForEach(pn => pn.Metadata.ToList().ForEach(ms => ms.Value.ToList().ForEach(func)));
            aggregator.SaveMapping();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets all project notes.
        /// </summary>
        /// <remarks></remarks>
        public List<IProjectNote> ProjectNotes { get { return _projectNotes; } }

        /// <summary>
        /// Gets all metadata.
        /// </summary>
        /// <remarks></remarks>
        public IDictionary<IMetadataType, ICollection<IMetadata>> Metadata { get { return _metadata; } }

        #endregion
    }
}