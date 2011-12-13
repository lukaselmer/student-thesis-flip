namespace ProjectFlip.Services.Interfaces
{
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