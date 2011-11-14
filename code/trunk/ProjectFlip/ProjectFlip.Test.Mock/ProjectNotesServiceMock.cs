﻿#region

using System.Collections.Concurrent;
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

        public List<IProjectNote> ProjectNotes { get; private set; }

        #region IProjectNotesService Members

        public IDictionary<IMetadataType, IList<IMetadata>> Metadata
        {
            get {
                return new Dictionary<IMetadataType, IList<IMetadata>>()
                {
                    {
                        new MetadataTypeMock {Name = "Oberkriterium"}, new List<IMetadata> {
                           new MetadataMock() {Description = "Unterkriterium 1"}, new MetadataMock() {Description =  "Unterkriterium 2"} 
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
        public MetadataType Type { get; set; }

        public string Description { get; set; }

        public bool Match(IProjectNote projectNote)
        {
            return true;
        }
    }
}