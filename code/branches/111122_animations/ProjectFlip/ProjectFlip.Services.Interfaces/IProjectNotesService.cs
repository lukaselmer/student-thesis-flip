#region

using System.Collections.Generic;

#endregion

namespace ProjectFlip.Services.Interfaces
{
    public interface IProjectNotesService
    {
        List<IProjectNote> ProjectNotes { get; }
        IDictionary<IMetadataType, ICollection<IMetadata>> Metadata { get; }
    }
}