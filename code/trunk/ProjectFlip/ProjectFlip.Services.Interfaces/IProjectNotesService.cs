#region

using System.Collections.Generic;

#endregion

namespace ProjectFlip.Services.Interfaces
{
    /// <summary>
    /// The project note service is responsible for loading the project notes
    /// and the according metadata.
    /// </summary>
    /// <remarks></remarks>
    public interface IProjectNotesService
    {
        /// <summary>
        /// Gets the project notes.
        /// </summary>
        List<IProjectNote> ProjectNotes { get; }

        /// <summary>
        /// Gets the metadata.
        /// </summary>
        IDictionary<IMetadataType, ICollection<IMetadata>> Metadata { get; }
    }
}