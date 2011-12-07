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
        private IProjectNote _nextProjectNote;
        private IProjectNote _previousProjectNote;
        private CollectionView _subcriteria;
        private bool _readModeActive;
        public bool ReadModeActive
        {
            get { return _readModeActive; }
            set
            {
                _readModeActive = value;
                Notify("ReadModeActive");
                if (ZoomInCommand != null) ZoomInCommand.RaiseCanExecuteChanged();
                if (ZoomOutCommand != null) ZoomOutCommand.RaiseCanExecuteChanged();
            }
        }

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

            ReadModeActive = false;
            NormalModeWidth = new GridLength(700);
            ReadModeWidth = new GridLength(10, GridUnitType.Star);
            DocumentViewerWidth = NormalModeWidth;

            ZoomInCommand = new Command(ToogleReadMode, o => ReadModeActive);
            ZoomOutCommand = new Command(ToogleReadMode, o => !ReadModeActive);
        }

        private Command _zoomOutCommand;
        public Command ZoomOutCommand
        {
            get { return _zoomOutCommand; }
            set
            {
                _zoomOutCommand = value;
                Notify("ZoomOutCommand");
            }
        }

        private Command _zoomInCommand;
        public Command ZoomInCommand
        {
            get { return _zoomInCommand; }
            set
            {
                _zoomInCommand = value;
                Notify("ZoomInCommand");
            }
        }

        private void OnHideDetail(object o)
        {
            IsDetailViewVisible = false;
            ReadModeActive = false;
            DocumentViewerWidth = NormalModeWidth;
        }

        private GridLength NormalModeWidth { get; set; }
        private GridLength ReadModeWidth { get; set; }

        private void ToogleReadMode(object o)
        {
            //var v = (DocumentViewer)o;
            DocumentViewerWidth = ReadModeActive ? NormalModeWidth : ReadModeWidth;
            ReadModeActive = !ReadModeActive;
            /*if (ReadModeActive)
            {
                //v.FitToHeight();
                //Console.WriteLine(v);
                DocumentViewerWidth = new GridLength(600);
                //DocumentViewerWidth = new GridLength(v.ExtentWidth);
                //DocumentViewerWidth = new GridLength(v.ExtentWidth);
            }
            else
            {
                DocumentViewerWidth = new GridLength(10, GridUnitType.Star);
                //v.FitToWidth();
            }*/

            //v.FitToWidth();

            //v.DocumentScrollInfo.
            //Console.WriteLine(v.ActualHeight);
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

            /*var docViewerIsSmall = (Math.Abs((new GridLength(705)).Value - DocumentViewerWidth.Value) < 1);

            DocumentViewerWidth = docViewerIsSmall
                ? new GridLength(10, GridUnitType.Star) : new GridLength(5, GridUnitType.Star);

            ((DocumentViewer)sender).FitToWidth();*/
        }

        private void RemoveFilter(object filter)
        {
            ReadModeActive = false;
            DocumentViewerWidth = NormalModeWidth;

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

            ReadModeActive = false;
            DocumentViewerWidth = NormalModeWidth;

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