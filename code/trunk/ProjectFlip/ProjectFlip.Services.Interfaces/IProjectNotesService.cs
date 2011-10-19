using System.Collections.Generic;

namespace ProjectFlip.Services.Interfaces
{
    public interface IProjectNotesService
    {
        List<IProjectNote> ProjectNotes { get; }
    }
}