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

        /// <summary>
        /// This is the width of the project note in the detail view.
        /// </summary>
        private readonly GridLength _normalModeWidth = new GridLength(2.75, GridUnitType.Star);

        /// <summary>
        /// This is the width of the project note in the zoomed view.
        /// </summary>
        private readonly GridLength _readModeWidth = new GridLength(10, GridUnitType.Star);

        /// <summary>
        /// This is the current width of the project note
        /// </summary>
        private GridLength _documentViewerWidth;

        private readonly List<IMetadata> _filters = new List<IMetadata>();
        private IProjectNote _currentProjectNote;
        private ICollectionView _filtersCollectionView;
        private bool _isDetailViewVisible;
        private bool _isFilterViewVisible;
        private bool _isInfoViewVisible;
        private bool _isReadModeActive;
        private ICollectionView _subcriteria;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="OverviewWindowViewModel"/> class.
        /// </summary>
        /// <param name="projectNotesService">The project notes service.</param>
        /// <param name="gravatarsViewModel">The gravatars view model.</param>
        /// <remarks></remarks>
        public OverviewWindowViewModel(IProjectNotesService projectNotesService, GravatarsViewModel gravatarsViewModel)
        {
            GravatarsViewModel = gravatarsViewModel;
            ProjectNotes = new CyclicCollectionView<IProjectNote>(projectNotesService.ProjectNotes) { Filter = FilterCallback };
            ProjectNotes.CurrentChanged += UpdateCurrentProjectNote;

            Criteria = projectNotesService.Metadata;

            Filters = new CollectionView(_filters);
            Maincriteria = new CollectionView(Criteria.Keys);
            Maincriteria.MoveCurrentToFirst();
            SetSubCriteria();

            DocumentViewerWidth = _normalModeWidth;

            InitCommands();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the gravatars view model.
        /// </summary>
        /// <remarks></remarks>
        public GravatarsViewModel GravatarsViewModel { get; private set; }

        /// <summary>
        /// Gets the maincriteria. E.g. "Technology", "Sector", "Customer"
        /// </summary>
        /// <remarks></remarks>
        public ICollectionView Maincriteria { get; private set; }

        /// <summary>
        /// Gets or sets the criteria. E.g. "C++", "Energy", "Credit Suisse"
        /// </summary>
        /// <value>The criteria.</value>
        /// <remarks></remarks>
        public IDictionary<IMetadataType, ICollection<IMetadata>> Criteria { get; set; }

        /// <summary>
        /// Gets the project notes. They are used for the binding.
        /// </summary>
        /// <remarks></remarks>
        public ICyclicCollectionView<IProjectNote> ProjectNotes { get; private set; }

        #region Commands

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

        #endregion

        /// <summary>
        /// Gets or sets a value indicating whether the read mode is active. When it is
        /// active, the document will be zoomed in so the user can read easier.
        /// </summary>
        /// <value><c>true</c> if read mode is active; otherwise, <c>false</c>.</value>
        /// <remarks></remarks>
        public bool IsReadModeActive
        {
            get { return _isReadModeActive; }
            set
            {
                _isReadModeActive = value;
                Notify("IsReadModeActive");
                DocumentViewerWidth = IsReadModeActive ? _readModeWidth : _normalModeWidth;
                if (ZoomInCommand != null) ZoomInCommand.RaiseCanExecuteChanged();
                if (ZoomOutCommand != null) ZoomOutCommand.RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the info view is visible. In the info
        /// view, a short abstract of the project is shown with the team members who
        /// implemented it.
        /// </summary>
        /// <value><c>true</c> if thi info view is visible; otherwise, <c>false</c>.</value>
        /// <remarks></remarks>
        public bool IsInfoViewVisible
        {
            get { return _isInfoViewVisible; }
            set
            {
                _isInfoViewVisible = value;
                Notify("IsInfoViewVisible");
            }
        }

        /// <summary>
        /// Gets the width of the document viewer. Used for zooming / read mode.
        /// </summary>
        /// <remarks></remarks>
        public GridLength DocumentViewerWidth
        {
            get { return _documentViewerWidth; }
            private set
            {
                _documentViewerWidth = value;
                Notify("DocumentViewerWidth");
            }
        }

        /// <summary>
        /// Gets the subcriteria.
        /// </summary>
        /// <remarks></remarks>
        public ICollectionView Subcriteria
        {
            get { return _subcriteria; }
            private set
            {
                _subcriteria = value;
                Notify("Subcriteria");
            }
        }

        /// <summary>
        /// Gets the current project note.
        /// </summary>
        /// <remarks></remarks>
        public IProjectNote CurrentProjectNote
        {
            get { return _currentProjectNote; }
            private set
            {
                _currentProjectNote = value;
                Notify("CurrentProjectNote");
            }
        }


        /// <summary>
        /// Gets the filters.
        /// </summary>
        /// <remarks></remarks>
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

        /// <summary>
        /// Gets or sets a value indicating whether this instance is detail view visible.
        /// </summary>
        /// <value><c>true</c> if this instance is detail view visible; otherwise, <c>false</c>.</value>
        /// <remarks></remarks>
        public bool IsDetailViewVisible
        {
            get { return _isDetailViewVisible; }
            set
            {
                _isDetailViewVisible = value;
                Notify("IsDetailViewVisible");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is filter view visible.
        /// </summary>
        /// <value><c>true</c> if this instance is filter view visible; otherwise, <c>false</c>.</value>
        /// <remarks></remarks>
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

            ZoomInCommand = new Command(ToogleReadMode, o => !IsReadModeActive);
            ZoomOutCommand = new Command(ToogleReadMode, o => IsReadModeActive);

            ToggleInfoCommand = new Command(o => IsInfoViewVisible = !IsInfoViewVisible);
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
            IsReadModeActive = false;
        }

        private void ToogleReadMode(object o)
        {
            IsReadModeActive = !IsReadModeActive;
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
            IsFilterViewVisible = IsReadModeActive = IsDetailViewVisible = false;
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

            IsReadModeActive = false;
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