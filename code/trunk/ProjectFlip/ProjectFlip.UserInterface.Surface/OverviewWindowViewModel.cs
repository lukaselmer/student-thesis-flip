#region

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using ProjectFlip.Services;
using ProjectFlip.Services.Interfaces;

#endregion

namespace ProjectFlip.UserInterface.Surface
{
    public class OverviewWindowViewModel : ViewModelBase
    {
        // Resharper is not able to analize the xaml file correctly, therefore
        // it supposes to make the members private.

        // ReSharper disable MemberCanBePrivate.Global
        // ReSharper disable UnusedMember.Global
        // ReSharper disable UnusedAutoPropertyAccessor.Global
        private readonly List<IMetadata> _filters = new List<IMetadata>();
        private readonly GridLength _normalModeWidth = new GridLength(2.75, GridUnitType.Star);
        private readonly GridLength _readModeWidth = new GridLength(10, GridUnitType.Star);
        private GridLength _documentViewerWidth;
        private IProjectNote _currentProjectNote;
        private ICollectionView _filtersCollectionView;
        private ICollectionView _subcriteria;
        private bool _isDetailViewVisible;
        private bool _isFilterViewVisible;
        private bool _readModeActive;

        public OverviewWindowViewModel(IProjectNotesService projectNotesService)
        {
            ProjectNotes = new CyclicCollectionView<IProjectNote>(projectNotesService.ProjectNotes) { Filter = FilterCallback };
            TotalProjectNotes = ProjectNotes.Count;
            ProjectNotes.MoveCurrentTo(null);
            ProjectNotes.CurrentChanged += UpdateCurrentProjectNote;
            Filters = new CollectionView(_filters);
            Criteria = projectNotesService.Metadata;
            Maincriteria = new CollectionView(Criteria.Keys);
            Maincriteria.MoveCurrentToFirst();
            SetSubCriteria();

            ShowDetailsCommand = new Command(OnShowDetail);
            HideDetailsCommand = new Command(OnHideDetail);

            NavigateToLeftCommand = new Command(o => ProjectNotes.MoveCurrentToPrevious());
            NavigateToRightCommand = new Command(o => ProjectNotes.MoveCurrentToNext());

            ShowSubcriteriaCommand = new Command(OnCurrentMainCriteriaChanged);
            ToggleFilterCommand = new Command(OnShowFilter);

            RemoveFilterCommand = new Command(RemoveFilter);
            AddFilterCommand = new Command(AddFilter);

            DocumentViewerWidth = _normalModeWidth;

            ZoomInCommand = new Command(ToogleReadMode, o => ReadModeActive);
            ZoomOutCommand = new Command(ToogleReadMode, o => !ReadModeActive);
        }

        public bool ReadModeActive
        {
            get { return _readModeActive; }
            set
            {
                _readModeActive = value;
                Notify("ReadModeActive");
                DocumentViewerWidth = ReadModeActive ? _readModeWidth : _normalModeWidth;
                if (ZoomInCommand != null) ZoomInCommand.RaiseCanExecuteChanged();
                if (ZoomOutCommand != null) ZoomOutCommand.RaiseCanExecuteChanged();
            }
        }

        public GridLength DocumentViewerWidth
        {
            get { return _documentViewerWidth; }
            private set
            {
                _documentViewerWidth = value;
                Notify("DocumentViewerWidth");
            }
        }

        public int TotalProjectNotes { get; private set; }

        public ICollectionView Maincriteria { get; private set; }

        public ICollectionView Subcriteria
        {
            get { return _subcriteria; }
            private set
            {
                _subcriteria = value;
                Notify("Subcriteria");
            }
        }

        public IProjectNote CurrentProjectNote
        {
            get { return _currentProjectNote; }
            private set
            {
                _currentProjectNote = value;
                Notify("CurrentProjectNote");
            }
        }

        public IDictionary<IMetadataType, ICollection<IMetadata>> Criteria { get; set; }

        public ICollectionView Filters
        {
            get { return _filtersCollectionView; }
            private set
            {
                _filtersCollectionView = value;
                ProjectNotes.Refresh();
                Notify("Filters");
            }
        }

        public ICyclicCollectionView<IProjectNote> ProjectNotes { get; private set; }
        public ICommand ShowSubcriteriaCommand { get; private set; }
        public ICommand ShowDetailsCommand { get; private set; }
        public ICommand HideDetailsCommand { get; private set; }
        public ICommand ToggleFilterCommand { get; private set; }
        public ICommand NavigateToLeftCommand { get; private set; }
        public ICommand NavigateToRightCommand { get; private set; }
        public ICommand AddFilterCommand { get; private set; }
        public ICommand RemoveFilterCommand { get; private set; }
        public Command ZoomOutCommand { get; private set; }
        public Command ZoomInCommand { get; private set; }

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

        private void OnHideDetail(object o)
        {
            IsDetailViewVisible = false;
            ReadModeActive = false;
        }

        private void ToogleReadMode(object o)
        {
            ReadModeActive = !ReadModeActive;
        }

        private void UpdateCurrentProjectNote(object sender, EventArgs eventArgs)
        {
            CurrentProjectNote = ProjectNotes.CurrentItem;
            if (CurrentProjectNote != null) CurrentProjectNote.Preload();
        }

        private void OnShowDetail(object obj)
        {
            if (obj != null)
            {
                var pn = (IProjectNote)obj;
                ProjectNotes.MoveCurrentTo(pn);
                CurrentProjectNote = pn;
            }
            UpdateCurrentProjectNote(null, null);
            IsDetailViewVisible = CurrentProjectNote != null;
        }

        private void OnCurrentMainCriteriaChanged(object maincriteria)
        {
            Maincriteria.MoveCurrentTo(maincriteria);
            SetSubCriteria();
        }

        private void SetSubCriteria()
        {
            ICollection<IMetadata> value;
            Criteria.TryGetValue((IMetadataType)Maincriteria.CurrentItem, out value);
            Subcriteria = new CollectionView(value);
        }

        private void OnShowFilter(object o)
        {
            IsFilterViewVisible = !IsFilterViewVisible;
        }

        private void RemoveFilter(object filter)
        {
            _filters.Remove((IMetadata)filter);
            Filters.Refresh();
            ProjectNotes.Refresh();
            IsFilterViewVisible = ReadModeActive = IsDetailViewVisible = false;
        }

        private void AddFilter(object filter)
        {
            if (_filters.Contains(filter) || _filters.Count == 3)
            {
                Filters.Refresh();
                return;
            }
            _filters.Add((IMetadata)filter);
            Filters.Refresh();
            ProjectNotes.Refresh();
            IsDetailViewVisible = IsFilterViewVisible = false;

            ReadModeActive = false;
        }

        private bool FilterCallback(object projectNoteObj)
        {
            if (_filters.Count == 0) return true;
            var projectNote = (IProjectNote)projectNoteObj;
            return _filters.All(f => f.Match(projectNote));
        }

        // ReSharper restore UnusedMember.Global
        // ReSharper restore UnusedAutoPropertyAccessor.Global
        // ReSharper restore MemberCanBePrivate.Global
    }
}