namespace ProjectFlip.Services.Interfaces
{
    /// <summary>
    /// The MetadataType object is a category for the metadata.
    /// This could be something like "Technologies", "Customers" or "Sectors"
    /// </summary>
    /// <remarks></remarks>
    public interface IMetadataType
    {
        /// <summary>
        /// Gets the name
        /// </summary>
        string Name { get; }
    }
}