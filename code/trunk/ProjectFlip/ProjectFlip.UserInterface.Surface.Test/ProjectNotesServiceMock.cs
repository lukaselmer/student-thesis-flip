using System.Collections.Generic;
using System.IO;
using System.Windows.Xps.Packaging;
using ProjectFlip.Services;
using ProjectFlip.Services.Interfaces;

namespace ProjectFlip.UserInterface.Surface.Test
{
    class ProjectNotesServiceMock : IProjectNotesService
    {

        private readonly List<IProjectNote> _projectNotes;

        public ProjectNotesServiceMock(int count)
        {
            _projectNotes = new List<IProjectNote>();
            for (int i = 0; i < count; i++)
            {
                var pn = new ProjectNoteMock();
                _projectNotes.Add(pn);
            }
        }

        public List<IProjectNote> ProjectNotes
        {
            get { return _projectNotes; }
        }


    }
}
