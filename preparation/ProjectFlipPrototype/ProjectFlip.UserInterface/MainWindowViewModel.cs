using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Input;
using ProjectFlip.Services.Interfaces;

namespace HSR.ProjectFlip
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private string _filter;
        private readonly List<IProjectNote> _projectNotes;

        public MainWindowViewModel(IProjectNotesService projectNotesService)
        {
            _projectNotes = projectNotesService.ProjectNotes;
            ProjectNotes = new ListCollectionView(_projectNotes);
            ProjectNotes.Filter = FilterCallback;

            ClearFilterCommand = new Command(ClearFilter, p => !string.IsNullOrEmpty(Filter));
        }

        public Command ClearFilterCommand { get; private set; }

        public string Filter
        {
            get { return _filter; }
            set
            {
                _filter = value;
                ProjectNotes.Refresh();

                Notify("Filter");
                ClearFilterCommand.RaiseCanExecuteChanged();
            }
        }

        private void Notify(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private void ClearFilter(object parameter)
        {
            Filter = "";
        }

        public ListCollectionView ProjectNotes { get; set; }

        private bool FilterCallback(object obj)
        {
            if (string.IsNullOrEmpty(Filter))
            {
                return true;
            }

            var projectNote = (IProjectNote) obj;
            return projectNote.Name.ToLower().Contains(Filter.ToLower());
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
