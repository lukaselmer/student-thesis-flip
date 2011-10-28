using System.Collections.Generic;
using System.Windows.Documents;
using ProjectFlip.Services.Interfaces;


namespace ProjectFlip.UserInterface.Surface
{
    public class DetailWindowViewModel : ViewModelBase
    {
        public List<IProjectNote> ProjectNotes { get; private set; }
        public IProjectNote CurrentProjectNote { get; private set; }
        public IDocumentPaginatorSource Document { get; private set; }

        public DetailWindowViewModel(IProjectNotesService projectNotesService, IProjectNote projectNote)
        {
            ProjectNotes = projectNotesService.ProjectNotes;
            CurrentProjectNote = ProjectNotes[0];
            Document = CurrentProjectNote.Document;

        }

    }

}
