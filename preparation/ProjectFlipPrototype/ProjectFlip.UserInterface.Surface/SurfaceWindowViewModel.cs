using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Microsoft.Surface.Presentation.Controls;
using ProjectFlip.Services.Interfaces;

namespace ProjectFlip.UserInterface.Surface
{
    public class SurfaceWindowViewModel : INotifyPropertyChanged
    {
        public SurfaceWindowViewModel(IProjectNotesService projectNotesService)
        {
            ProjectNotes = projectNotesService.ProjectNotes;
        }

        private void Notify(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public List<IProjectNote> ProjectNotes { get; private set; } 
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
