#region

using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using ProjectFlip.Services.Interfaces;

#endregion

namespace ProjectFlip.Test.Mock
{
    public class ProjectNotesServiceMock : IProjectNotesService
    {
        public ProjectNotesServiceMock(int count)
        {
            ProjectNotes = new List<IProjectNote>(Enumerable.Range(0, count).Select(i => new ProjectNoteMock()));
        }

        public List<IProjectNote> ProjectNotes { get; private set; }

        #region IProjectNotesService Members

        public IDictionary<IMetadataType, ICollection<IMetadata>> Metadata
        {
            get
            {
                return new Dictionary<IMetadataType, ICollection<IMetadata>>()
                {
                    {
                        new MetadataTypeMock {Name = "Name"}, new List<IMetadata> {
                           new MetadataMock() {Description = "Description"} 
                        }
                    }      
                };
            }
        }

        #endregion
    }

    public class MetadataTypeMock : IMetadataType
    {
        public string Name { get; set; }
    }

    public class MetadataMock : IMetadata
    {
        public MetadataMock() { }
        public MetadataMock(IMetadataType type, string description)
        {
            Type = type;
            Description = description;
        }

        public IMetadataType Type { get; set; }

        public string Description { get; set; }

        public bool Match(IProjectNote projectNote)
        {
            return false;
        }
    }
}