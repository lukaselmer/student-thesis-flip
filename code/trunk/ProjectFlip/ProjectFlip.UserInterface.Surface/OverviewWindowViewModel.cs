#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
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
        
        private readonly List<IMetadata> _filters = new List<IMetadata>();
        private bool _isDetailViewVisible;
        private bool _isFilterViewVisible;
        private CollectionView _subcriteria;
        private ICollectionView _filtersCollectionView;
        private GridLength _documentViewerWidth;
        private GridLength _leftButtonWidth;
        private GridLength _rightButtonWidth;

        public OverviewWindowViewModel(IProjectNotesService projectNotesService)
        {
            DocumentViewerWidth = new GridLength(760);
            LeftButtonWidth = new GridLength(240);
            RightButtonWidth = new GridLength(1, GridUnitType.Star);

            ProjectNotes = new ListCollectionView(projectNotesService.ProjectNotes) {Filter = FilterCallback};
            ProjectNotes.CurrentChanged += OnCurrentProjectNoteChanged; 
            CurrentProjectNote = ((IProjectNote)ProjectNotes.CurrentItem);
            Filters = new CollectionView(_filters);
            Criteria  = projectNotesService.Metadata;
            Maincriteria = new CollectionView(Criteria.Keys);
            Maincriteria.MoveCurrentToFirst();
            SetSubCriteria();

            ShowSubcriteriaCommand = new Command(OnCurrentMainCriteriaChanged);
            ShowDetailsCommand = new Command(OnShowDetail);
            HideDetailsCommand = new Command(o => IsDetailViewVisible = false);
            ShowHideFilterCommand = new Command(OnShowFilter);
            
            NavigateToLeftCommand = new Command(o =>MoveToPrevious()); 
            NavigateToRightCommand = new Command(o => MoveToNext());

            RemoveFilterCommand = new Command(RemoveFilter); 
            AddFilterCommand = new Command(AddFilter);
        }

        private void OnShowDetail(object pn)
        {
            if (pn != null) ProjectNotes.MoveCurrentTo(pn);
            IsDetailViewVisible = true;
        }

        public CollectionView Maincriteria { get; private set; }
        public CollectionView Subcriteria
        {
            get { return _subcriteria; }
            private set { _subcriteria = value;
                Notify("Subcriteria");
            }
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

        public IProjectNote CurrentProjectNote
        {
            get { return _currentProjectNote; }
            private set { 
                _currentProjectNote = value;
                Notify("CurrentProjectNote");
            }
        }

        public IDictionary<IMetadataType, ICollection<IMetadata>> Criteria { get; set; }

        public ICollectionView Filters {
            get { return _filtersCollectionView; }
        private set { _filtersCollectionView = value;
            ProjectNotes.Refresh();
            Notify("Filters");
        } }

        public ICollectionView ProjectNotes { get; private set; }
        public ICommand ShowSubcriteriaCommand { get; private set; }
        public ICommand ShowDetailsCommand { get; private set; }
        public ICommand HideDetailsCommand { get; private set; }
        public ICommand ShowHideFilterCommand { get; private set; }
        public ICommand HideFilterCommand { get; private set; }
        public Command NavigateToLeftCommand { get; private set; }
        public Command NavigateToRightCommand { get; private set; }
        public ICommand AddFilterCommand { get; private set; }
        public ICommand RemoveFilterCommand { get; private set; }
        public ICommand DeleteButtonCommand { get; set; }

        public void OnTouchUp(object sender, TouchEventArgs e)
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

            bool docViewerIsSmall = (Math.Abs((new GridLength(760)).Value - DocumentViewerWidth.Value) < 1);

            DocumentViewerWidth = docViewerIsSmall ? new GridLength(1, GridUnitType.Star) : new GridLength(760);
            LeftButtonWidth = docViewerIsSmall ? new GridLength(70) : new GridLength(240);
            RightButtonWidth = docViewerIsSmall ? new GridLength(70) : new GridLength(1, GridUnitType.Star);
       
            ((DocumentViewer) sender).FitToWidth();
        }

        public void OnTouchDown(object sender, TouchEventArgs e)
        {
            Console.WriteLine("2------------------------Tab----------------------");
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
            if (_filters.Contains(filter) || _filters.Count == 3) return;
            _filters.Add((IMetadata)filter);
            Filters.Refresh();
            ProjectNotes.Refresh();
            IsFilterViewVisible = false;
            if (IsDetailViewVisible) IsDetailViewVisible = false;
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
            return _filters.All(f => f.Match(projectNote));
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