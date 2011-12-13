#region

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ProjectFlip.Services.Interfaces;
using ProjectFlip.Services.Loader.Interfaces;

#endregion

namespace ProjectFlip.Services
{
    public class ProjectNotesService : IProjectNotesService
    {
        #region Declarations

        private readonly IDictionary<IMetadataType, ICollection<IMetadata>> _metadata;
        private readonly List<IProjectNote> _projectNotes;
        private readonly IProjectNotesLoader _projectNotesLoader;

        #endregion

        #region Constructor

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

        public List<IProjectNote> ProjectNotes { get { return _projectNotes; } }

        public IDictionary<IMetadataType, ICollection<IMetadata>> Metadata { get { return _metadata; } }

        #endregion
    }
}