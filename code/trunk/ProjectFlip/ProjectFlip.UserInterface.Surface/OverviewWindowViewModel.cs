#region

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
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
        private IProjectNote _currentProjectNote;
        private GridLength _documentViewerWidth;
        private ICollectionView _filtersCollectionView;
        private bool _isDetailViewVisible;
        private bool _isFilterViewVisible;
        private GridLength _leftButtonWidth;
        private IProjectNote _nextProjectNote;
        private IProjectNote _previousProjectNote;
        private GridLength _rightButtonWidth;
        private CollectionView _subcriteria;

        public OverviewWindowViewModel(IProjectNotesService projectNotesService)
        {
            DocumentViewerWidth = new GridLength(705);
            LeftButtonWidth = new GridLength(240);
            RightButtonWidth = new GridLength(1, GridUnitType.Star);

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
            HideDetailsCommand = new Command(o => IsDetailViewVisible = false);

            NavigateToLeftCommand = new Command(o => ProjectNotes.MoveCurrentToPrevious());
            NavigateToRightCommand = new Command(o => ProjectNotes.MoveCurrentToNext());

            ShowSubcriteriaCommand = new Command(OnCurrentMainCriteriaChanged);
            ToggleFilterCommand = new Command(OnShowFilter);

            RemoveFilterCommand = new Command(RemoveFilter);
            AddFilterCommand = new Command(AddFilter);
        }

        public int TotalProjectNotes { get; private set; }

        public CollectionView Maincriteria { get; private set; }

        public CollectionView Subcriteria
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

        public IProjectNote NextProjectNote
        {
            get { return _nextProjectNote; }
            private set
            {
                _nextProjectNote = value;
                Notify("NextProjectNote");
            }
        }

        public IProjectNote PreviousProjectNote
        {
            get { return _previousProjectNote; }
            private set
            {
                _previousProjectNote = value;
                Notify("PreviousProjectNote");
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
        public Command NavigateToLeftCommand { get; private set; }
        public Command NavigateToRightCommand { get; private set; }
        public ICommand AddFilterCommand { get; private set; }
        public ICommand RemoveFilterCommand { get; private set; }

        public GridLength DocumentViewerWidth
        {
            get { return _documentViewerWidth; }
            private set
            {
                _documentViewerWidth = value;
                Notify("DocumentViewerWidth");
            }
        }

        public GridLength LeftButtonWidth
        {
            get { return _leftButtonWidth; }
            private set
            {
                _leftButtonWidth = value;
                Notify("LeftButtonWidth");
            }
        }

        public GridLength RightButtonWidth
        {
            get { return _rightButtonWidth; }
            private set
            {
                _rightButtonWidth = value;
                Notify("RightButtonWidth");
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

        private void UpdateCurrentProjectNote(object sender, EventArgs eventArgs)
        {
            //Notify("ProjectNotes");
            PreviousProjectNote = ProjectNotes.Previous;
            CurrentProjectNote = ProjectNotes.CurrentItem;
            NextProjectNote = ProjectNotes.Next;
            if (CurrentProjectNote == null) return;
            PreviousProjectNote.Preload();
            CurrentProjectNote.Preload();
            NextProjectNote.Preload();
        }

        private void OnShowDetail(object obj)
        {
            if (obj != null)
            {
                var pn = (IProjectNote)obj;
                ProjectNotes.MoveCurrentTo(pn);
                CurrentProjectNote = pn;
            }
            //PreviousProjectNote = ProjectNotes.Previous;
            //CurrentProjectNote = ProjectNotes.CurrentItem;
            //NextProjectNote = ProjectNotes.Next;
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

        // ReSharper disable UnusedParameter.Global
        public void OnTouchUp(object sender, TouchEventArgs e)
        // ReSharper restore UnusedParameter.Global
        {
            //            if (e.TouchDevice == TouchAction.Move) return;

            //            if (e.TouchDevice == TouchAction.Move) return;
            // var k = e.TouchDevice;
            //            var l = SurfaceTouchDevice.;

            //            bool isFinger = true;
            //            SurfaceTouchDevice device = e.TouchDevice as SurfaceTouchDevice;
            //            if (device != null)
            //                {
            //                isFinger = device.Contact.IsFingerRecognized;
            //                }
            //Do something

            Console.WriteLine(@"------------------------touch----------------------");

            var docViewerIsSmall = (Math.Abs((new GridLength(705)).Value - DocumentViewerWidth.Value) < 1);

            DocumentViewerWidth = docViewerIsSmall ? new GridLength(1, GridUnitType.Star) : new GridLength(705);
            LeftButtonWidth = docViewerIsSmall ? new GridLength(70) : new GridLength(240);
            RightButtonWidth = docViewerIsSmall ? new GridLength(70) : new GridLength(1, GridUnitType.Star);

            ((DocumentViewer)sender).FitToWidth();
        }

        private void RemoveFilter(object filter)
        {
            _filters.Remove((IMetadata)filter);
            Filters.Refresh();
            ProjectNotes.Refresh();
            IsFilterViewVisible = false;
            IsDetailViewVisible = false;
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