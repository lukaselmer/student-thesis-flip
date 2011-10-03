using System.Collections.Generic;
using System.IO;
using Microsoft.Practices.Unity;
using ProjectFlip.Services.Interfaces;
using ProjectFlip.Services.Loader;

namespace ProjectFlip.Services
{
    public class ProjectNotesService : IProjectNotesService
    {
        private readonly ILogService _logService;
        private readonly List<IProjectNote> _projectNotes = new List<IProjectNote>();

        public ProjectNotesService(ILogService logService)
        {
            _logService = logService;
            for (int i = 0; i < 3; i++)
            {
                _projectNotes.Add(new ProjectNote(@"..\..\..\Resources\Xps\test.xps"));
            }

            if (false)
            {
                var container = new UnityContainer();
                container.RegisterType<IProjectNotesLoader, ProjectNotesLoader>();
                var loader = container.Resolve<IProjectNotesLoader>();
                var list = loader.Import();
                foreach (var line in list)
                {
                    //    _projectNotes.Add(new ProjectNote());
                }
            }

            //_projectNotes.AddRange(
            //    new List<string>(
            //        Directory.GetFiles(@"C:\Users\Public\Pictures\Sample Pictures", "*.jpg"))
            //    .Select(f => new ProjectNote(f)));

            //var files = Directory.GetFiles(@"C:\Users\Public\Pictures\Sample Pictures", "*.jpg");
            //foreach (var file in files)
            //{
            //    _projectNotes.Add(new ProjectNote(file));
            //}
            //_logService.Log("Project notes loaded...");
        }

        public List<IProjectNote> ProjectNotes
        {
            get { return _projectNotes; }
        }
    }
}
