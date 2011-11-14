#region

using System;
using System.Collections.Generic;
using System.IO;
using ComLib.Collections;
using ProjectFlip.Services.Interfaces;
using ProjectFlip.Services.Loader.Interfaces;

#endregion

namespace ProjectFlip.Services
{
    public class ProjectNotesService : IProjectNotesService
    {
        private readonly List<IProjectNote> _projectNotes;
        //private readonly Dictionary<MetadataType,List<IMetadata>> _filters;
        private readonly IProjectNotesLoader _projectNotesLoader;
        private IDictionary<MetadataType, IList<IMetadata>> _metadata; 

        public ProjectNotesService(IProjectNotesLoader projectNotesLoader)
        {
            Aggregator.LoadMapping();
            _projectNotesLoader = projectNotesLoader;
            Metadata = new Dictionary<MetadataType, IList<IMetadata>>();
            _projectNotes =
                new List<IProjectNote>(_projectNotesLoader.Import().ConvertAll(line => new ProjectNote { Line = line }));
            _projectNotes.RemoveAll(pn => !File.Exists(pn.FilepathXps));
            Aggregator.SaveMapping();
        }

        public IDictionary<MetadataType, IList<IMetadata>> Metadata
        {
            get { return _metadata; }
            private set { _metadata = value; }
        }

        #region IProjectNotesService Members

        public List<IProjectNote> ProjectNotes { get { return _projectNotes; } }

        #endregion
    }
}