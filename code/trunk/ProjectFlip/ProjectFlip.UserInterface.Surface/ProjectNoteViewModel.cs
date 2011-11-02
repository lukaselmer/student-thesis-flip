#region

using System.Windows.Input;
using ProjectFlip.Services.Interfaces;

#endregion

namespace ProjectFlip.UserInterface.Surface
{
    public class ProjectNoteViewModel
    {
        public ProjectNoteViewModel(IProjectNotesService service, IProjectNote projectNote)
        {
            ProjectNote = projectNote;
            ProjectNoteService = service;
            OpenWindowCommand = new Command(OpenNewWindow);
        }

        // ReSharper disable MemberCanBePrivate.Global
        // ReSharper disable UnusedAutoPropertyAccessor.Global
        public ICommand OpenWindowCommand { get; private set; }
        public IProjectNote ProjectNote { get; private set; }
        public IProjectNotesService ProjectNoteService { get; private set; }
        // ReSharper restore UnusedAutoPropertyAccessor.Global
        // ReSharper restore MemberCanBePrivate.Global

        private void OpenNewWindow(object sender)
        {
            var vm = new DetailWindowViewModel(ProjectNoteService, ProjectNote);
            var detailWindow = new DetailWindow(vm);
            detailWindow.Show();
            vm.CloseWindow += detailWindow.Close;
        }

    }
}