#region

using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectFlip.Services.Interfaces;
using ProjectFlip.Test.Mock;
using ProjectFlip.UserInterface.Surface.ViewModels;

#endregion

namespace ProjectFlip.UserInterface.Surface.Test
{
    /// <summary>
    ///This is a test class for OverviewWindowViewModelTest and is intended
    ///to contain all OverviewWindowViewModelTest Unit Tests
    ///</summary>
    [TestClass]
    public class OverviewWindowViewModelTest
    {
        /// <summary>
        ///A test for OverviewWindowViewModel Constructor & ProjectNotes Property
        ///</summary>
        [TestMethod]
        public void OverviewWindowViewModelProjectNotesTest()
        {
            var projectNotesService = new ProjectNotesServiceMock(5);
            var target = new OverviewWindowViewModel(projectNotesService, null);
            Assert.AreEqual(5, target.ProjectNotes.Count);
        }

        /// <summary>
        ///A test for AddFilter, RemoveFilter and FilterCallback
        ///</summary>
        [TestMethod, DeploymentItem("ProjectFlip.UserInterface.Surface.dll")]
        public void AddAndRemoveAndCallbackFilterTest()
        {
            var projectNotesService = new ProjectNotesServiceMock(5);
            var target = new OverviewWindowViewModel_Accessor(projectNotesService, null);
            var filter = new MetadataMock(new MetadataTypeMock {Name = "Sector"}, "Oberkriterium");

            target.AddFilter(filter);
            Assert.IsFalse(target.IsFilterViewVisible);
            Assert.IsFalse(target.IsDetailViewVisible);
            Assert.AreEqual(0, target.ProjectNotes.Count);
            target.RemoveFilter(filter);
            Assert.AreEqual(5, target.ProjectNotes.Count);
        }

        /// <summary>
        ///A test for AddFilter, RemoveFilter and FilterCallback
        ///</summary>
        [TestMethod, DeploymentItem("ProjectFlip.UserInterface.Surface.dll")]
        public void AddAndRemoveAndCallbackFilterWithVisibleDetailsTest()
        {
            var projectNotesService = new ProjectNotesServiceMock(5);
            var target = new OverviewWindowViewModel_Accessor(projectNotesService, null);
            var filter = new MetadataMock(new MetadataTypeMock {Name = "Sector"}, "Oberkriterium");
            target.IsDetailViewVisible = true;
            target.AddFilter(filter);
            Assert.IsFalse(target.IsFilterViewVisible);
            Assert.IsFalse(target.IsDetailViewVisible);
            Assert.AreEqual(0, target.ProjectNotes.Cast<IProjectNote>().Count());
            target.RemoveFilter(filter);
            Assert.AreEqual(5, target.ProjectNotes.Cast<IProjectNote>().Count());
        }

        /// <summary>
        ///A test for OnShowFilter
        ///</summary>
        [TestMethod, DeploymentItem("ProjectFlip.UserInterface.Surface.dll")]
        public void OnShowFilterTest()
        {
            var projectNotesService = new ProjectNotesServiceMock(5);
            var target = new OverviewWindowViewModel_Accessor(projectNotesService, null);
            Assert.IsFalse(target.IsFilterViewVisible);
            target.OnToggleFilter(new object());
            Assert.IsTrue(target.IsFilterViewVisible);
        }

        /// <summary>
        ///A test that it's not possible to add more than three filters
        ///</summary>
        [TestMethod, DeploymentItem("ProjectFlip.UserInterface.Surface.dll")]
        public void TryToAddMoreThanThreeElementsToFilterTest()
        {
            var projectNotesService = new ProjectNotesServiceMock(5);
            var target = new OverviewWindowViewModel_Accessor(projectNotesService, null);
            var filter1 = new MetadataMock(new MetadataTypeMock {Name = "Sector"}, "Kriterium 1");
            var filter2 = new MetadataMock(new MetadataTypeMock {Name = "Sector"}, "Kriterium 2");
            var filter3 = new MetadataMock(new MetadataTypeMock {Name = "Sector"}, "Kriterium 3");
            var filter4 = new MetadataMock(new MetadataTypeMock {Name = "Sector"}, "Kriterium 4");

            Assert.AreEqual(0, target.Filters.Cast<IMetadata>().Count());
            target.AddFilter(filter1);
            target.AddFilter(filter2);
            target.AddFilter(filter3);
            Assert.AreEqual(3, target.Filters.Cast<IMetadata>().Count());
            target.AddFilter(filter4);
            Assert.AreEqual(3, target.Filters.Cast<IMetadata>().Count());
            Assert.AreEqual(filter1, target.Filters.Cast<IMetadata>().ElementAt(0));
            Assert.AreEqual(filter2, target.Filters.Cast<IMetadata>().ElementAt(1));
            Assert.AreEqual(filter3, target.Filters.Cast<IMetadata>().ElementAt(2));
        }

        /// <summary>
        ///A test that it's not possible to add an filter twice
        ///</summary>
        [TestMethod, DeploymentItem("ProjectFlip.UserInterface.Surface.dll")]
        public void TryToAddElementTwiceToFilterTest()
        {
            var projectNotesService = new ProjectNotesServiceMock(5);
            var target = new OverviewWindowViewModel_Accessor(projectNotesService, null);
            var filter = new MetadataMock(new MetadataTypeMock {Name = "Sector"}, "Oberkriterium");

            Assert.AreEqual(0, target.Filters.Cast<IMetadata>().Count());
            target.AddFilter(filter);
            Assert.AreEqual(1, target.Filters.Cast<IMetadata>().Count());
            target.AddFilter(filter);
            Assert.AreEqual(1, target.Filters.Cast<IMetadata>().Count());
        }

        /// <summary>
        ///A test for MoxeToNext
        ///</summary>
        [TestMethod, DeploymentItem("ProjectFlip.UserInterface.Surface.dll")]
        public void MoveToNextTest()
        {
            IProjectNotesService projectNotesService = new ProjectNotesServiceMock(2);
            var target = new OverviewWindowViewModel_Accessor(projectNotesService, null);
            target.ProjectNotes.MoveCurrentToFirst();
            target.ProjectNotes.MoveCurrentToNext();
            Assert.AreEqual(target.CurrentProjectNote, projectNotesService.ProjectNotes.ElementAt(1));
            target.ProjectNotes.MoveCurrentToNext();
            Assert.AreEqual(target.CurrentProjectNote, projectNotesService.ProjectNotes.ElementAt(0));
        }

        /// <summary>
        ///A test for MoveToPrevious
        ///</summary>
        [TestMethod, DeploymentItem("ProjectFlip.UserInterface.Surface.dll")]
        public void MoveToPreviousTest()
        {
            IProjectNotesService projectNotesService = new ProjectNotesServiceMock(2);
            var target = new OverviewWindowViewModel_Accessor(projectNotesService, null);
            target.ProjectNotes.MoveCurrentToLast();
            target.ProjectNotes.MoveCurrentToPrevious();
            Assert.AreEqual(target.CurrentProjectNote, projectNotesService.ProjectNotes.ElementAt(0));
            target.ProjectNotes.MoveCurrentToPrevious();
            Assert.AreEqual(target.CurrentProjectNote, projectNotesService.ProjectNotes.ElementAt(1));
        }

        /// <summary>
        ///A test for OnShowDetail
        ///</summary>
        [TestMethod, DeploymentItem("ProjectFlip.UserInterface.Surface.dll")]
        public void OnShowDetailTest()
        {
            IProjectNotesService projectNotesService = new ProjectNotesServiceMock(2);
            var target = new OverviewWindowViewModel_Accessor(projectNotesService, null);
            Assert.IsFalse(target.IsDetailViewVisible);
            target.ProjectNotes.MoveCurrentToFirst();
            Assert.AreEqual(projectNotesService.ProjectNotes.ElementAt(0), target.CurrentProjectNote);
            var pn = projectNotesService.ProjectNotes[1];
            Assert.AreSame(pn, projectNotesService.ProjectNotes.ElementAt(1));
            target.OnShowDetail(pn);
            Assert.AreSame(pn, target.CurrentProjectNote);
            Assert.IsTrue(target.IsDetailViewVisible);
        }

        /// <summary>
        ///A test for HideDetailsCommand
        ///</summary>
        [TestMethod, DeploymentItem("ProjectFlip.UserInterface.Surface.dll")]
        public void HideDetailsCommandTest()
        {
            IProjectNotesService projectNotesService = new ProjectNotesServiceMock(2);
            var target = new OverviewWindowViewModel_Accessor(projectNotesService, null);
            target.HideDetailsCommand.Execute(null);
            Assert.IsFalse(target.IsDetailViewVisible);
        }

        /// <summary>
        ///A test for NavigateToLeftCommand
        ///</summary>
        [TestMethod, DeploymentItem("ProjectFlip.UserInterface.Surface.dll")]
        public void NavigateToLeftRightCommandTest()
        {
            IProjectNotesService projectNotesService = new ProjectNotesServiceMock(3);
            var target = new OverviewWindowViewModel_Accessor(projectNotesService, null);
            target.CurrentProjectNote = projectNotesService.ProjectNotes.ElementAt(0);
            Assert.AreEqual(target.CurrentProjectNote, projectNotesService.ProjectNotes.ElementAt(0));
            target.ProjectNotes.MoveCurrentToFirst();
            target.NavigateToLeftCommand.Execute(null);
            Assert.AreEqual(target.CurrentProjectNote, projectNotesService.ProjectNotes.ElementAt(2));
            target.NavigateToLeftCommand.Execute(null);
            Assert.AreEqual(target.CurrentProjectNote, projectNotesService.ProjectNotes.ElementAt(1));
            target.NavigateToRightCommand.Execute(null);
            Assert.AreEqual(target.CurrentProjectNote, projectNotesService.ProjectNotes.ElementAt(2));
            target.NavigateToRightCommand.Execute(null);
            Assert.AreEqual(target.CurrentProjectNote, projectNotesService.ProjectNotes.ElementAt(0));
        }

        // <summary>
        //  A test for OnCurrentMainCriteriaChanged
        // </summary>
        [TestMethod, DeploymentItem("ProjectFlip.UserInterface.Surface.dll")]
        public void OnCurrentMainCriteriaChangedTest()
        {
            var projectNotesService = new ProjectNotesServiceMock(1);
            var metadataTypeSector = new MetadataTypeMock {Name = "Sektor"};
            var metadataTypeCustomer = new MetadataTypeMock {Name = ""};
            var metadataSector = new MetadataMock {Description = "Sektorkriterium"};
            var metadataCustomer = new MetadataMock {Description = "Kundenkriterium"};
            projectNotesService.Metadata = new Dictionary<IMetadataType, ICollection<IMetadata>>
                                           {
                                               {metadataTypeSector, new List<IMetadata> {metadataSector}},
                                               {metadataTypeCustomer, new List<IMetadata> {metadataCustomer}}
                                           };

            var target = new OverviewWindowViewModel_Accessor(projectNotesService, null);
            target.ShowSubcriteriaCommand.Execute(metadataTypeCustomer);

            var c = target.Maincriteria.Cast<IMetadataType>().ElementAt(0);
            target.OnShowSubcriteria(c);
            Assert.AreEqual(target.Subcriteria.Cast<IMetadata>().ElementAt(0).Description, metadataSector.Description);
        }

        /// <summary>
        ///A test for OverviewWindowViewModel Constructor
        ///</summary>
        [TestMethod, DeploymentItem("ProjectFlip.UserInterface.Surface.dll")]
        public void OverviewWindowViewModelConstructorTest()
        {
            var projectNotesService = new ProjectNotesServiceMock(5);
            var target = new OverviewWindowViewModel_Accessor(projectNotesService, null);
            Assert.AreEqual(false, target.ReadModeActive);
            Assert.AreEqual(target._normalModeWidth, target.DocumentViewerWidth);
            Assert.AreEqual(false, target.ZoomOutCommand.CanExecute(null));
            Assert.AreEqual(true, target.ZoomInCommand.CanExecute(null));
            target.ZoomInCommand.Execute(null);
            Assert.AreEqual(true, target.ReadModeActive);
            Assert.AreEqual(target._readModeWidth, target.DocumentViewerWidth);
            Assert.AreEqual(false, target.ZoomInCommand.CanExecute(null));
            Assert.AreEqual(true, target.ZoomOutCommand.CanExecute(null));
            target.ZoomOutCommand.Execute(null);
            Assert.AreEqual(false, target.ReadModeActive);
            Assert.AreEqual(target._normalModeWidth, target.DocumentViewerWidth);
            Assert.AreEqual(true, target.ZoomInCommand.CanExecute(null));
            Assert.AreEqual(false, target.ZoomOutCommand.CanExecute(null));
        }
    }
}