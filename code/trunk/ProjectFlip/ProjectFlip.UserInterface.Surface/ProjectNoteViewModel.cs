using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using ProjectFlip.Services.Interfaces;

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

        private void OpenNewWindow(object sender)
        {
            var vm = new DetailWindowViewModel(ProjectNoteService, ProjectNote);
            var detailWindow = new DetailWindow(vm);
            detailWindow.Show();
            vm.CloseWindow += detailWindow.Close;
        }

        public ICommand OpenWindowCommand { get; private set; }

        public IProjectNote ProjectNote { get; private set; }

        public IProjectNotesService ProjectNoteService { get; private set; }
    }
}
