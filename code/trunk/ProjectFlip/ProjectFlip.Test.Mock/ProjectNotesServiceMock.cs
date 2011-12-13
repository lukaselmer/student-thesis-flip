#region

using System.Collections.Generic;
using System.Linq;
using ProjectFlip.Services.Interfaces;

#endregion

namespace ProjectFlip.Test.Mock
{
    public class ProjectNotesServiceMock : IProjectNotesService
    {
        #region Declarations

        private IDictionary<IMetadataType, ICollection<IMetadata>> _metadata =
            new Dictionary<IMetadataType, ICollection<IMetadata>>
            {
                {
                    new MetadataTypeMock {Name = "Name"},
                    new List<IMetadata> {new MetadataMock {Description = "Description"}}
                    }
            };

        #endregion

        #region Constructor

        public ProjectNotesServiceMock(int count)
        {
            ProjectNotes = new List<IProjectNote>(Enumerable.Range(0, count).Select(i => new ProjectNoteMock()));
        }

        #endregion

        #region Properties

        public List<IProjectNote> ProjectNotes { get; private set; }

        public IDictionary<IMetadataType, ICollection<IMetadata>> Metadata { get { return _metadata; } set { _metadata = value; } }

        #endregion
    }
}