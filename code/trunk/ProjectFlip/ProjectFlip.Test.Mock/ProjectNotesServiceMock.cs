#region

using System.Collections.Generic;
using System.Linq;
using ProjectFlip.Services.Interfaces;

#endregion

namespace ProjectFlip.Test.Mock
{
    public class ProjectNotesServiceMock : IProjectNotesService
    {
        public ProjectNotesServiceMock(int count, string o = "")
        {
            ProjectNotes = new List<IProjectNote>(Enumerable.Range(0, count).Select(i => new ProjectNoteMock()));
        }

        #region IProjectNotesService Members

        public List<IProjectNote> ProjectNotes { get; private set; }

        #endregion
    }
}