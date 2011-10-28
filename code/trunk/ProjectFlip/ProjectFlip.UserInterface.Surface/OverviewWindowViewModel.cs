using System.Collections.Generic;
using ProjectFlip.Services.Interfaces;

namespace ProjectFlip.UserInterface.Surface
{
    public class OverviewWindowViewModel : ViewModelBase
    {
        public OverviewWindowViewModel(IProjectNotesService projectNotesService)
        {
            ProjectNotes = projectNotesService.ProjectNotes;
            ProjectNoteViewModels = new List<ProjectNoteViewModel>();
            ProjectNotes.ForEach(pn => ProjectNoteViewModels.Add(new ProjectNoteViewModel(projectNotesService, pn)));
        }

        public List<IProjectNote> ProjectNotes {get; private set; }

        public List<ProjectNoteViewModel> ProjectNoteViewModels { get; private set; }
    }
}
