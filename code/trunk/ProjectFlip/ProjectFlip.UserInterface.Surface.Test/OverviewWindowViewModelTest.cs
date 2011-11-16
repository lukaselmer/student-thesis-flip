using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectFlip.Services.Interfaces;
using ProjectFlip.Test.Mock;
using System.Linq;
using ProjectFlip.UserInterface.Surface;
using System;
using System.Windows.Data;
using System.Windows.Input;
using System.ComponentModel;
using System.Collections.Generic;

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
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for OverviewWindowViewModel Constructor & ProjectNotes Property
        ///</summary>
        [TestMethod]
        public void OverviewWindowViewModelProjectNotesTest()
        {
            var projectNotesService = new ProjectNotesServiceMock(5);
            var target = new OverviewWindowViewModel(projectNotesService);
            Assert.AreEqual(5, target.ProjectNotes.Cast<IProjectNote>().Count());
        }


        /// <summary>
        ///A test for AddFilter, RemoveFilter and FilterCallback
        ///</summary>
        [TestMethod()]
        [DeploymentItem("ProjectFlip.UserInterface.Surface.dll")]
        public void AddAndRemoveAndCallbackFilterTest()
        {
            var projectNotesService = new ProjectNotesServiceMock(5, "Sector");
            var n = projectNotesService.Metadata.Keys.ElementAt(0).Name;
            var target = new OverviewWindowViewModel_Accessor(projectNotesService);
            var filter = new MetadataMock(new MetadataTypeMock() {Name = "Sector"},"Oberkriterium");

            target.AddFilter(filter);
            Assert.AreEqual(target.IsFilterViewVisible, false);
            Assert.AreEqual(0, target.ProjectNotes.Cast<IProjectNote>().Count());
            target.RemoveFilter(filter);
            Assert.AreEqual(5, target.ProjectNotes.Cast<IProjectNote>().Count());
        }

        /// <summary>
        ///A test for OnShowFilter
        ///</summary>
        [TestMethod()]
        [DeploymentItem("ProjectFlip.UserInterface.Surface.dll")]
        public void OnShowFilterTest()
        {
            var projectNotesService = new ProjectNotesServiceMock(5);
            var target = new OverviewWindowViewModel_Accessor(projectNotesService);
            Assert.AreEqual(target.IsFilterViewVisible, false);
            target.OnShowFilter(new object());
            Assert.AreEqual(target.IsFilterViewVisible, true);
        }

        /// <summary>
        ///A test for MoxeToNext
        ///</summary>
        [TestMethod()]
        [DeploymentItem("ProjectFlip.UserInterface.Surface.dll")]
        public void MoveToNextTest()
        {
            IProjectNotesService projectNotesService = new ProjectNotesServiceMock(2);
            OverviewWindowViewModel_Accessor target = new OverviewWindowViewModel_Accessor(projectNotesService);
            target.MoveToNext();
            Assert.AreEqual(target.CurrentProjectNote, projectNotesService.ProjectNotes.ElementAt(1));
            target.MoveToNext();
            Assert.AreEqual(target.CurrentProjectNote, projectNotesService.ProjectNotes.ElementAt(0));
        }

        /// <summary>
        ///A test for MoveToPrevious
        ///</summary>
        [TestMethod()]
        [DeploymentItem("ProjectFlip.UserInterface.Surface.dll")]
        public void MoveToPreviousTest()
        {
            IProjectNotesService projectNotesService = new ProjectNotesServiceMock(2);
            OverviewWindowViewModel_Accessor target = new OverviewWindowViewModel_Accessor(projectNotesService);
            target.MoveToPrevious();
            Assert.AreEqual(target.CurrentProjectNote, projectNotesService.ProjectNotes.ElementAt(1));
            target.MoveToPrevious();
            Assert.AreEqual(target.CurrentProjectNote, projectNotesService.ProjectNotes.ElementAt(0));
        }

        /// <summary>
        ///A test for OnCurrentMainCriteriaChanged
        ///</summary>
        [TestMethod()]
        [DeploymentItem("ProjectFlip.UserInterface.Surface.dll")]
        public void OnCurrentMainCriteriaChangedTest()
        {
            IProjectNotesService projectNotesService = new ProjectNotesServiceMock(1);
            OverviewWindowViewModel_Accessor target = new OverviewWindowViewModel_Accessor(projectNotesService);
            Assert.IsNull(target.Subcriteria);
            target.OnCurrentMainCriteriaChanged(new object(), null);
            Assert.IsNotNull(target.Subcriteria);
        }

        /// <summary>
        ///A test for OnShowDetail
        ///</summary>
        [TestMethod()]
        [DeploymentItem("ProjectFlip.UserInterface.Surface.dll")]
        public void OnShowDetailTest()
        {
            IProjectNotesService projectNotesService = new ProjectNotesServiceMock(2);
            OverviewWindowViewModel_Accessor target = new OverviewWindowViewModel_Accessor(projectNotesService);
            Assert.IsFalse(target.IsDetailViewVisible);
            Assert.AreEqual(projectNotesService.ProjectNotes.ElementAt(0), target.CurrentProjectNote);
            target.OnShowDetail(projectNotesService.ProjectNotes.ElementAt(1));
            Assert.AreEqual(projectNotesService.ProjectNotes.ElementAt(1), target.CurrentProjectNote);
            Assert.IsTrue(target.IsDetailViewVisible);
        }
    }
}
