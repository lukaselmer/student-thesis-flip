using System.Collections.Generic;
using System.IO;
using ProjectFlip.Services.Interfaces;

namespace ProjectFlip.Services
{
    public class ProjectNotesService : IProjectNotesService
    {
        private readonly ILogService _logService;
        private readonly List<IProjectNote> _projectNotes = new List<IProjectNote>();

        public ProjectNotesService(ILogService logService)
        {
            _logService = logService;
            //_projectNotes.AddRange(
            //    new List<string>(
            //        Directory.GetFiles(@"C:\Users\Public\Pictures\Sample Pictures", "*.jpg"))
            //    .Select(f => new ProjectNote(f)));

            var files = Directory.GetFiles(@"C:\Users\Public\Pictures\Sample Pictures", "*.jpg");
            foreach (var file in files)
            {
                _projectNotes.Add(new ProjectNote(file));
            }
            _logService.Log("Project notes loaded...");
        }

        public List<IProjectNote> ProjectNotes
        {
            get { return _projectNotes; }
        }
    }
}
