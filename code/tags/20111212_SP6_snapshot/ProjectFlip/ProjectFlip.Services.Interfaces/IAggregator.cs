namespace ProjectFlip.Services.Interfaces
{
    public interface IAggregator
    {
        /// <summary>
        /// Loads the mapping of the metadata.
        /// </summary>
        void LoadMapping();

        /// <summary>
        /// Saves the mapping of the metadata.
        /// </summary>
        void SaveMapping();

        /// <summary>
        /// Aggregates the metadata.
        /// </summary>
        /// <param name="metadata">The metadata.</param>
        /// <returns></returns>
        IMetadata AggregateMetadata(IMetadata metadata);
    }
}