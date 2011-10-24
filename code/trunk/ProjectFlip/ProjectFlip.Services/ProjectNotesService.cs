using System.Collections.Generic;
using System.IO;
using ProjectFlip.Services.Interfaces;
using ProjectFlip.Services.Loader.Interfaces;

namespace ProjectFlip.Services
{
    public class ProjectNotesService : IProjectNotesService
    {
        private readonly IProjectNotesLoader _projectNotesLoader;
        private readonly List<IProjectNote> _projectNotes;

        public ProjectNotesService(IProjectNotesLoader projectNotesLoader)
        {
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
