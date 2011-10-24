using System.Collections.Generic;
using ProjectFlip.Services.Interfaces;

namespace ProjectFlip.UserInterface.Surface
{
    public class OverviewWindowViewModel : ViewModelBase
    {
        public OverviewWindowViewModel(IProjectNotesService projectNotesService)
        {
            ProjectNotes = projectNotesService.ProjectNotes;
        }

        public List<IProjectNote> ProjectNotes { get; private set; }
    }
}
