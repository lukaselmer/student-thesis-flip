﻿using System.Collections.Generic;
using System.IO;
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
            _projectNotes = new List<IProjectNote>(_projectNotesLoader.Import().ConvertAll(line => new ProjectNote(line)));
            //for (int i = 0; i < 3; i++)
            //{
            //    _projectNotes.Add(new ProjectNote(@"..\..\..\Resources\Xps\test.xps"));
            //}

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
