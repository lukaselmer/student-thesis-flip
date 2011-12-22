using ProjectFlip.Services.Interfaces;

namespace ProjectFlip.Test.Mock
{
    public class AggregatorMock : IAggregator
    {
        public void LoadMapping() { }
        public void SaveMapping() { }
        public IMetadata AggregateMetadata(IMetadata metadata) { return metadata; }
    }
}