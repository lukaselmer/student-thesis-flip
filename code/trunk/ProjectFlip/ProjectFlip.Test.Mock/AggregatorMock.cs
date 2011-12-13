#region

using ProjectFlip.Services.Interfaces;

#endregion

namespace ProjectFlip.Test.Mock
{
    public class AggregatorMock : IAggregator
    {
        #region Other

        public void LoadMapping() {}
        public void SaveMapping() {}

        public IMetadata AggregateMetadata(IMetadata metadata)
        {
            return metadata;
        }

        #endregion
    }
}