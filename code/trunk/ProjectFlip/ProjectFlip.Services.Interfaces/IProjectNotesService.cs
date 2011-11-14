#region

using System.Collections.Generic;

#endregion

namespace ProjectFlip.Services.Interfaces
{
    public interface IProjectNotesService
    {
        List<IProjectNote> ProjectNotes { get; }
        IDictionary<IMetadataType, IList<IMetadata>> Metadata { get; }
    }
}