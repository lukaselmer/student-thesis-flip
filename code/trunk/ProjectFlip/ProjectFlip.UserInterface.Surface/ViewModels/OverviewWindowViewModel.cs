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
using ProjectFlip.UserInterface.Surface.Helpers;

#endregion

namespace ProjectFlip.UserInterface.Surface.ViewModels
{
    public class OverviewWindowViewModel : ViewModelBase
    {
        // Resharper is not able to analize the xaml file correctly, therefore
        // it supposes to make the members private.

        // ReSharper disable MemberCanBePrivate.Global
        // ReSharper disable UnusedMember.Global
        // ReSharper disable UnusedAutoPropertyAccessor.Global

        #region Declarations

        private readonly List<IMetadata> _filters = new List<IMetadata>();
        private readonly GridLength _normalModeWidth = new GridLength(2.75, GridUnitType.Star);
        private readonly GridLength _readModeWidth = new GridLength(10, GridUnitType.Star);
        private IProjectNote _currentProjectNote;
        private GridLength _documentViewerWidth;
        private ICollectionView _filtersCollectionView;
        private bool _isDetailViewVisible;
        private bool _isFilterViewVisible;
        private bool _isInfoViewVisible;
        private bool _readModeActive;
        private ICollectionView _subcriteria;

        #endregion

        #region Constructor


        public OverviewWindowViewModel(IProjectNotesService projectNotesService)
        {
            ProjectNotes = new CyclicCollectionView<IProjectNote>(projectNotesService.ProjectNotes) { Filter = FilterCallback };
            ProjectNotes.CurrentChanged += UpdateCurrentProjectNote;
            Filters = new CollectionView(_filters);
            Criteria = projectNotesService.Metadata;
            Maincriteria = new CollectionView(Criteria.Keys);
            Maincriteria.MoveCurrentToFirst();
            SetSubCriteria();

            DocumentViewerWidth = _normalModeWidth;

            InitCommands();
        }

        #endregion

        #region Properties


        public ICollectionView Maincriteria { get; private set; }

        public IDictionary<IMetadataType, ICollection<IMetadata>> Criteria { get; set; }

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
        public ICommand ToggleInfoCommand { get; private set; }

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

        public bool InfoViewVisible
        {
            get { return _isInfoViewVisible; }
            set
            {
                _isInfoViewVisible = value;
                Notify("InfoViewVisible");
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

        #endregion

        #region Other

        private void InitCommands()
        {
            ShowDetailsCommand = new Command(OnShowDetail);
            HideDetailsCommand = new Command(OnHideDetail);

            NavigateToLeftCommand = new Command(OnNavigateToLeft);
            NavigateToRightCommand = new Command(OnNavigateToRight);

            ShowSubcriteriaCommand = new Command(OnShowSubcriteria);
            ToggleFilterCommand = new Command(OnToggleFilter);

            RemoveFilterCommand = new Command(RemoveFilter);
            AddFilterCommand = new Command(AddFilter);

            ZoomInCommand = new Command(ToogleReadMode, o => !ReadModeActive);
            ZoomOutCommand = new Command(ToogleReadMode, o => ReadModeActive);

            ToggleInfoCommand = new Command(o => InfoViewVisible = !InfoViewVisible);
        }

        private void OnNavigateToRight(object obj)
        {
            ProjectNotes.MoveCurrentToNext();
            PreloadSideProjectNotes();
        }

        private void OnNavigateToLeft(object obj)
        {
            ProjectNotes.MoveCurrentToPrevious();
            PreloadSideProjectNotes();
        }

        private void PreloadSideProjectNotes()
        {
            ProjectNotes.Next.Preload();
            ProjectNotes.Previous.Preload();
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

        private void OnShowSubcriteria(object maincriteria)
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

        private void OnToggleFilter(object o)
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

        #endregion
    }
}