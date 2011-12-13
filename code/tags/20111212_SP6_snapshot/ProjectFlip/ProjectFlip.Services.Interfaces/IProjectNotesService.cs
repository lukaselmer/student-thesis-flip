#region

using System.Collections.Generic;

#endregion

namespace ProjectFlip.Services.Interfaces
{
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