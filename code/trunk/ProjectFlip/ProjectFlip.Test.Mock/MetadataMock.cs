#region

using ProjectFlip.Services.Interfaces;

#endregion

namespace ProjectFlip.Test.Mock
{
    public class MetadataMock : IMetadata
    {
        #region Constructor

        public MetadataMock() {}

        public MetadataMock(IMetadataType type, string description)
        {
            Type = type;
            Description = description;
        }

        #endregion

        #region Properties

        public IMetadataType Type { get; private set; }

        public string Description { get; set; }

        #endregion

        #region Other

        public bool Match(IProjectNote projectNote)
        {
            return false;
        }

        #endregion
    }
}