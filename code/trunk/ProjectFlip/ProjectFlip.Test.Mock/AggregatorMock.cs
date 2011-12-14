#region

using ProjectFlip.Services.Interfaces;

#endregion

namespace ProjectFlip.Test.Mock
{
    /// <summary>
    /// The aggregator mock.
    /// </summary>
    /// <remarks></remarks>
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