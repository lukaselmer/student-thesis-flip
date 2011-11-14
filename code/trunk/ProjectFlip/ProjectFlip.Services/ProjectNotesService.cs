#region

using System;
using System.Collections.Generic;
using System.IO;
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
        public IList<IList<IMetadata>> Metadata; 

        public ProjectNotesService(IProjectNotesLoader projectNotesLoader)
        {
            Aggregator.LoadMapping();
            _projectNotesLoader = projectNotesLoader;
            _projectNotes =
                new List<IProjectNote>(_projectNotesLoader.Import().ConvertAll(line => new ProjectNote { Line = line }));
            _projectNotes.RemoveAll(pn => !File.Exists(pn.FilepathXps));
            Aggregator.SaveMapping();
        }

        #region IProjectNotesService Members

        public List<IProjectNote> ProjectNotes { get { return _projectNotes; } }

        #endregion
    }
}