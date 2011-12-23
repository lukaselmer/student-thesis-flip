namespace ProjectFlip.Services.Interfaces
{
    /// <summary>
    /// The Metadata object is a addition to a project note.
    /// This could be something like "Java", "Credit Suisse" or "Energy Sector"
    /// </summary>
    /// <remarks></remarks>
    public interface IMetadata
    {
        /// <summary>
        /// Gets the type.
        /// </summary>
        IMetadataType Type { get; }

        /// <summary>
        /// Gets the description.
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Determindes if the metadata matches one of the metadatas of the specified project note.
        /// </summary>
        /// <param name="projectNote">The project note.</param>
        /// <returns></returns>
        bool Match(IProjectNote projectNote);
    }
}