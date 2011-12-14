namespace ProjectFlip.Services.Interfaces
{
    /// <summary>
    /// The interface for the aggregator. The aggregator aggregates multiple
    /// metadatas and so maps metadata to some new metadata. This could be
    /// JavaBeans mapped to Java.
    /// </summary>
    /// <remarks></remarks>
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