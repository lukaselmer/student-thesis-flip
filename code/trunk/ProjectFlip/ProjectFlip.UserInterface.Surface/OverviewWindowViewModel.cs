#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using ProjectFlip.Services.Interfaces;
using System.ComponentModel;

#endregion

namespace ProjectFlip.UserInterface.Surface
{
    public class OverviewWindowViewModel : ViewModelBase
    {
        private IDocumentPaginatorSource _document;
        private readonly List<IMetadata> _filters = new List<IMetadata>();
        private bool _isDetailViewVisible;

        public OverviewWindowViewModel(IProjectNotesService projectNotesService)
        {
            ProjectNotes = new ListCollectionView(projectNotesService.ProjectNotes);
            ProjectNotes.MoveCurrentToFirst();
            ProjectNotes.Filter = FilterCallback;
            ProjectNotes.CurrentChanged += OnCurrentProjectNoteChanged;

            Filters = new CollectionView(_filters);

            ShowDetailsCommand = new Command(o => IsDetailViewVisible = true);
            HideDetailsCommand = new Command(o => IsDetailViewVisible = false);
            NavigateToLeftCommand = new Command(o => MoveToPrevious());
            NavigateToRightCommand = new Command(o => MoveToNext());

            AddFilterCommand = new Command(RemoveFilter);
        }

        public IDocumentPaginatorSource Document
        {
            get { return _document; }
            private set
            {
                _document = value;
                Notify("Document");
            }
        }

        public ICollectionView ProjectNotes { get; private set; }
        public ICollectionView Filters { get; private set; }
        public ICommand ShowDetailsCommand { get; private set; }
        public ICommand HideDetailsCommand { get; private set; }
        public Command NavigateToLeftCommand { get; private set; }
        public Command NavigateToRightCommand { get; private set; }
        public ICommand AddFilterCommand { get; private set; }
        public ICommand RemoveFilterCommand { get; private set; }

        public bool IsDetailViewVisible
        {
            get { return _isDetailViewVisible; }
            set
            {
                _isDetailViewVisible = value;
                Notify("IsDetailViewVisible");
            }
        }

        private void RemoveFilter(object filter)
        {
            _filters.Remove((IMetadata)filter);
            Filters.Refresh();
        }

        private void OnCurrentProjectNoteChanged(object sender, EventArgs e)
        {
            Document = ((IProjectNote)ProjectNotes.CurrentItem).Document;
        }

        private bool FilterCallback(object projectNoteObj)
        {
            if (_filters.Count == 0) return true;
            var projectNote = (IProjectNote)projectNoteObj;
            return _filters.Any(f => f.Match(projectNote));
        }

        private void MoveToNext()
        {
            ProjectNotes.MoveCurrentToNext();
            if (ProjectNotes.IsCurrentAfterLast)
            {
                ProjectNotes.MoveCurrentToFirst();
            }
        }

        private void MoveToPrevious()
        {
            ProjectNotes.MoveCurrentToPrevious();
            if (ProjectNotes.IsCurrentBeforeFirst)
            {
                ProjectNotes.MoveCurrentToLast();
            }
        }
    }

}