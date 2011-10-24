using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Xps.Packaging;
using Microsoft.Practices.Unity;
using ProjectFlip.Services.Interfaces;
using ProjectFlip.Services.Loader;

namespace ProjectFlip.Services
{
    public class ProjectNotesService : IProjectNotesService
    {
        private readonly ILogService _logService;
        private readonly IProjectNotesLoader _projectNotesLoader;
        private readonly List<IProjectNote> _projectNotes;

        public ProjectNotesService(ILogService logService, IProjectNotesLoader projectNotesLoader)
        {
            _logService = logService;
            _projectNotesLoader = projectNotesLoader;
            _projectNotes = new List<IProjectNote>(_projectNotesLoader.Import().ConvertAll(line => new ProjectNote().InitByLine(line)));
            _projectNotes.RemoveAll(pn => !File.Exists(pn.FilepathXps));
        }

        public List<IProjectNote> ProjectNotes
        {
            get { return _projectNotes; }
        }
    }
}
