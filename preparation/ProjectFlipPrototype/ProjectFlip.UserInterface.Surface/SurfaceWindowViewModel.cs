﻿using System;
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
            Buttons = new List<SurfaceButton>();
            for (int i = 0; i < 10; i++)
            {
                SurfaceButton b = new SurfaceButton();
                b.Content = "text";
                Buttons.Add(b);
            }
        }

        private void Notify(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public List<SurfaceButton> Buttons { get; set; }
        public List<IProjectNote> ProjectNotes { get; private set; } 
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
