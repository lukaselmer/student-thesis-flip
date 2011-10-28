using System;
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

        private void OpenNewWindow(object parameter)
        {
            var detailWindow = new DetailWindow(new DetailWindowViewModel(ProjectNoteService, ProjectNote));
            detailWindow.Show();
            Application.Current.MainWindow = detailWindow;
        }

        public ICommand OpenWindowCommand { get; private set; }

        public IProjectNote ProjectNote { get; private set; }

        public IProjectNotesService ProjectNoteService { get; private set; }

        public String Title
        {
            get { return ProjectNote.Title; }
        }

        public BitmapImage Image
        {
            get { return ProjectNote.Image; }
        }


    }
}
