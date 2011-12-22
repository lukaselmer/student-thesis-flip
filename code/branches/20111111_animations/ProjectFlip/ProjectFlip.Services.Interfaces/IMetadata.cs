namespace ProjectFlip.Services.Interfaces
{
    public interface IMetadata
    {
        MetadataType Type { get; }
        string Description { get; }

        bool Match(IProjectNote projectNote);
    }
}
