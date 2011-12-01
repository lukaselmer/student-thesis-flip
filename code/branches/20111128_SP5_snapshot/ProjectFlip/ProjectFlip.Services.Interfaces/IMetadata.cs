namespace ProjectFlip.Services.Interfaces
{
    public interface IMetadata
    {
        IMetadataType Type { get; }
        string Description { get; }

        bool Match(IProjectNote projectNote);
    }
}
