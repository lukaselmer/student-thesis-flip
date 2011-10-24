using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using Microsoft.Surface.Presentation.Controls;
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
