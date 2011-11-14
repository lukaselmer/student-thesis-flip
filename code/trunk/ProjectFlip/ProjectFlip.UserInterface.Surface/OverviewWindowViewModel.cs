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
        private IProjectNote _currentProjectNote;
        private readonly IDictionary<MetadataType, IList<IMetadata>> _criteria;
        
        private readonly List<IMetadata> _filters = new List<IMetadata>();
        private bool _isDetailViewVisible;
        private bool _isFilterViewVisible;
        private CollectionView _subcriteria;
        private ICollectionView _filtersCollectionView;


        public OverviewWindowViewModel(IProjectNotesService projectNotesService)
        {
            ProjectNotes = new ListCollectionView(projectNotesService.ProjectNotes) {Filter = FilterCallback};
            ProjectNotes.CurrentChanged += OnCurrentProjectNoteChanged; 
            CurrentProjectNote = ((IProjectNote)ProjectNotes.CurrentItem);
            Filters = new CollectionView(_filters);
            _criteria = projectNotesService.Metadata;
            Maincriteria = new CollectionView(_criteria.Keys);
            Maincriteria.CurrentChanged += OnCurrentMainCriteriaChanged;
            
            ShowDetailsCommand = new Command(pn => { if (pn != null) ProjectNotes.MoveCurrentTo(pn); IsDetailViewVisible = true; });
            HideDetailsCommand = new Command(o => IsDetailViewVisible = false);
            ShowFilterCommand = new Command(OnShowFilter);
            HideFilterCommand = new Command(o => IsFilterViewVisible = false);
            NavigateToLeftCommand = new Command(o => MoveToPrevious());
            NavigateToRightCommand = new Command(o => MoveToNext());
            DeleteButtonCommand = new Command(OnDeleteButtonCommand);

            RemoveFilterCommand = new Command(RemoveFilter); 
            AddFilterCommand = new Command(AddFilter);
        }

        public CollectionView Maincriteria { get; private set; }
        public CollectionView Subcriteria
        {
            get { return _subcriteria; }
            private set { _subcriteria = value;
                Notify("Subcriteria");
            }
        }

        private void OnCurrentMainCriteriaChanged(object sender, EventArgs e)
        {
            IList<IMetadata> value;
            _criteria.TryGetValue((MetadataType) Maincriteria.CurrentItem, out value);
            Subcriteria = new CollectionView(value);
        }

        private void OnShowFilter(object o)
        {
            IsFilterViewVisible = true;
        }

        private void OnDeleteButtonCommand(object obj)
        {
            Console.WriteLine(((IProjectNote)ProjectNotes.CurrentItem).Title);
        }

        public IProjectNote CurrentProjectNote
        {
            get { return _currentProjectNote; }
            private set { 
                _currentProjectNote = value;
                Notify("CurrentProjectNote");
            }
        }

        public IDictionary<MetadataType,IList<IMetadata>> Criteria
        {
            get { return _criteria; }
        }

        public ICollectionView Filters {
            get { return _filtersCollectionView; }
        private set { _filtersCollectionView = value;
            ProjectNotes.Refresh();
            Notify("Filters");
        } }

        public ICollectionView ProjectNotes { get; private set; }
        public ICommand ShowDetailsCommand { get; private set; }
        public ICommand HideDetailsCommand { get; private set; }
        public ICommand ShowFilterCommand { get; private set; }
        public ICommand HideFilterCommand { get; private set; }
        public Command NavigateToLeftCommand { get; private set; }
        public Command NavigateToRightCommand { get; private set; }
        public ICommand AddFilterCommand { get; private set; }
        public ICommand RemoveFilterCommand { get; private set; }
        public ICommand DeleteButtonCommand { get; set; }

        public bool IsDetailViewVisible
        {
            get { return _isDetailViewVisible; }
            set
            {
                _isDetailViewVisible = value;
                Notify("IsDetailViewVisible");
            }
        }

        public bool IsFilterViewVisible
        {
            get { return _isFilterViewVisible; }
            set
            {
                _isFilterViewVisible = value;
                Notify("IsFilterViewVisible");
            }
        }

        private void RemoveFilter(object filter)
        {
            _filters.Remove((IMetadata)filter);
            Filters.Refresh();
            ProjectNotes.Refresh();
        }

        private void AddFilter(object filter)
        {
            _filters.Add((IMetadata)filter);
            Filters.Refresh();
            ProjectNotes.Refresh();
            IsFilterViewVisible = false;
        }

        private void OnCurrentProjectNoteChanged(object sender, EventArgs e)
        {
            if (ProjectNotes.IsCurrentAfterLast || ProjectNotes.IsCurrentBeforeFirst) return;
            CurrentProjectNote = ((IProjectNote) ProjectNotes.CurrentItem);
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