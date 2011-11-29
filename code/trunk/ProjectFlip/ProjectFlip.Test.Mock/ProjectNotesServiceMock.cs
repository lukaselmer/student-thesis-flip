#region

using System.Collections.Generic;
using System.Linq;
using ProjectFlip.Services.Interfaces;

#endregion

namespace ProjectFlip.Test.Mock
{
    public class ProjectNotesServiceMock : IProjectNotesService
    {
        private IDictionary<IMetadataType, ICollection<IMetadata>> _metadata =
            new Dictionary<IMetadataType, ICollection<IMetadata>>
            {
                {
                    new MetadataTypeMock {Name = "Name"},
                    new List<IMetadata> {new MetadataMock {Description = "Description"}}
                    }
            };

        public ProjectNotesServiceMock(int count)
        {
            ProjectNotes = new List<IProjectNote>(Enumerable.Range(0, count).Select(i => new ProjectNoteMock()));
        }

        #region IProjectNotesService Members

        public List<IProjectNote> ProjectNotes { get; private set; }

        public IDictionary<IMetadataType, ICollection<IMetadata>> Metadata { get { return _metadata; } set { _metadata = value; } }

        #endregion
    }

    public class MetadataTypeMock : IMetadataType
    {
        #region IMetadataType Members

        public string Name { get; set; }

        #endregion
    }

    public class MetadataMock : IMetadata
    {
        public MetadataMock() {}

        public MetadataMock(IMetadataType type, string description)
        {
            Type = type;
            Description = description;
        }

        #region IMetadata Members

        public IMetadataType Type { get; set; }

        public string Description { get; set; }

        public bool Match(IProjectNote projectNote)
        {
            return false;
        }

        #endregion
    }
}