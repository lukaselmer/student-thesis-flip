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
        private readonly List<IProjectNote> _projectNotes;
        private readonly IProjectNotesLoader _projectNotesLoader;
        private readonly IDictionary<IMetadataType, IList<IMetadata>> _metadata;

        public ProjectNotesService(IProjectNotesLoader projectNotesLoader)
        {
            Aggregator.LoadMapping();
            _projectNotesLoader = projectNotesLoader;
            _projectNotes =
                new List<IProjectNote>(_projectNotesLoader.Import().ConvertAll(line => new ProjectNote { Line = line }));
            _projectNotes.RemoveAll(pn => !File.Exists(pn.FilepathXps));
            _metadata = new Dictionary<IMetadataType, IList<IMetadata>>();
            _projectNotes.ForEach(pn => pn.Metadata.ToList().ForEach(ms => ms.Value.ToList().ForEach(m =>
                {
                    if (!_metadata.ContainsKey(m.Type)) _metadata[m.Type] = new List<IMetadata>();
                    if (!_metadata[m.Type].Contains(m)) _metadata[m.Type].Add(m);
                })));
            Aggregator.SaveMapping();
        }

        #region IProjectNotesService Members

        public List<IProjectNote> ProjectNotes { get { return _projectNotes; } }

        public IDictionary<IMetadataType, IList<IMetadata>> Metadata
        {
            get { return _metadata; }
        }

        #endregion
    }
}