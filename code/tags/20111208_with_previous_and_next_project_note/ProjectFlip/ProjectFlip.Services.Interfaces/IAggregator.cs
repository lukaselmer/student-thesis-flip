namespace ProjectFlip.Services.Interfaces
{
    public interface IAggregator
    {
        void LoadMapping();
        void SaveMapping();
        IMetadata AggregateMetadata(IMetadata metadata);
    }
}